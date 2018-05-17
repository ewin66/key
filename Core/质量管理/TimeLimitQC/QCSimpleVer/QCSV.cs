using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using DrectSoft.Core.TimeLimitQC;
using System.Reflection;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// �������ƣ�ʱ�ޣ��򻯰�
    /// </summary>
    public class Qcsv
    {
        #region fields

        IList<QCRule> _rules;
        IList<QCCondition> _conditions;
        IList<QCResult> _results;
        IList<QCRuleGroup> _groups;
        QCRuleRecordDal _rulerecorddal;
        DoctorManagerPatientInfoDal _patientdal;

        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        public Qcsv()
        {
            InitRules();
        }

        void InitRules()
        {
            _conditions = QCCondition.AllConditions;
            _results = QCResult.AllResults;
            _groups = QCRuleGroup.AllRuleGroups;
            _rules = QCRule.GetAllRules(_conditions, _results);
            _rulerecorddal = new QCRuleRecordDal();
            _patientdal = new DoctorManagerPatientInfoDal();
        }

        /// <summary>
        /// ����ҽ������ʱ�޼�¼
        /// </summary>
        public DataTable SelectDoctorRuleRecords(string doctorId)
        {
            DataSet dsRecords = _rulerecorddal.GetDoctorRulesDataSet(doctorId);
            if (dsRecords == null || dsRecords.Tables.Count == 0)
                return null;
            else
                return dsRecords.Tables[0];
        }

        Collection<QCRuleRecord> GetRuleRecordsCollection(DataTable table)
        {
            Collection<QCRuleRecord> qcrrs = new Collection<QCRuleRecord>();
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    QCRuleRecord qcrr = _rulerecorddal.DataRow2QCRuleRecord(table.Rows[i]);
                    qcrrs.Add(qcrr);
                }
            }

            return qcrrs;
        }

        public DataTable SelectPatientRuleRecords(int patid)
        {
            //Modified by wwj 2011-09-20
            //return SelectPatientRuleRecords(patid, true);
            return SelectPatientRuleRecords(patid, false);
        }

        /// <summary>
        /// ���������������ʱ�޼�¼
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="all">�Ƿ����м�¼,��������ɺ�δ���</param>
        /// <returns></returns>
        public DataTable SelectPatientRuleRecords(int patid, bool all)
        {
            DataSet dsRecords = null;
            if (all)
                dsRecords = _rulerecorddal.GetPatientRulesDataSet(patid);
            else
                dsRecords = _rulerecorddal.GetPatientUndoRulesDataSet(patid);

            if (dsRecords == null || dsRecords.Tables.Count == 0)
                return null;
            else
                return dsRecords.Tables[0];
        }

        /// <summary>
        /// ���ӹ����¼,Ĭ�ϵ�ǰʱ��
        /// </summary>
        /// <param name="patid">����Id</param>
        /// <param name="eprid">����Id</param>
        /// <param name="opdoctor">����ҽ��</param>
        /// <param name="conditionType"></param>
        /// <param name="conditionObject"></param>
        public void AddRuleRecord(int patid, int eprid, string opdoctor, QCConditionType conditionType, object conditionObject)
        {
            AddRuleRecord(patid, eprid, opdoctor, conditionType, conditionObject, DateTime.Now);
        }

        /// <summary>
        /// ���ӹ����¼
        /// </summary>
        /// <param name="patid">����Id</param>
        /// <param name="eprid">����Id</param>
        /// <param name="opdoctor">����ҽ��</param>
        /// <param name="conditionType">ʱ�޹����������</param>
        /// <param name="conditionObject">��������,������λ���������</param>
        /// <param name="conditionTime">��������ʱ��</param>
        public void AddRuleRecord(int patid, int eprid, string opdoctor, QCConditionType conditionType, object conditionObject, DateTime conditionTime)
        {
            
            DoctorManagerPatient dmp = _patientdal.SelectDoctorManagerPatient(patid);
            if (dmp == null) return;

            QCCondition qcc = QCCondition.SelectQCCondition(conditionType, conditionObject);
            if (qcc == null) return;

            IList<QCRule> rules = QCRule.GetRulesByCondition(qcc);
            foreach (QCRule rule in rules)
            {
                QCRuleRecord qcrr = new QCRuleRecord(patid, eprid, rule);
                qcrr.ConditionTime = conditionTime;
                qcrr.ConditionState = CompleteType.Completed;
                qcrr.RuleState = RuleRecordState.UndoIntime;
                qcrr.CreateDoctor = opdoctor;
                SetQcRuleRecordState(rule, qcrr);
                _rulerecorddal.InsertPatientRuleRecord(dmp, qcrr);
            }
        }

        /// <summary>
        /// ��������ȡ�����¹����¼ȡ��
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="eprid"></param>
        /// <param name="opdoctor"></param>
        /// <param name="conditionType"></param>
        /// <param name="conditionObject"></param>
        /// <param name="conditionTime"></param>
        public void CancelRuleRecord(int patid, int eprid, string opdoctor, QCConditionType conditionType, object conditionObject, DateTime conditionTime)
        {
            DoctorManagerPatient dmp = _patientdal.SelectDoctorManagerPatient(patid);
            if (dmp == null) return;

            QCCondition qcc = QCCondition.SelectQCCondition(conditionType, conditionObject);
            if (qcc == null) return;

            IList<QCRule> rules = QCRule.GetRulesByCondition(qcc);
            if (rules == null || rules.Count == 0) return;

            DataTable dtrulerecords = SelectPatientRuleRecords(patid, false);
            Collection<QCRuleRecord> rulerecords = GetRuleRecordsCollection(dtrulerecords);
            for (int i = 0; i < rulerecords.Count; i++)
            {
                QCRuleRecord qcrr = rulerecords[i];
                if (qcrr.ResultState == CompleteType.Completed) continue;
                QCRule existsrule = ((List<QCRule>)rules).Find(delegate(QCRule rule)
                {
                    return rule.Id == qcrr.Rule.Id;
                });
                qcrr.RecordState = RecordState.Invalid;
                _rulerecorddal.UpdatePatientRuleRecord(dmp, qcrr);
            }
        }

        /// <summary>
        /// ͨ����ش����ȡ��ؽ��
        /// </summary>
        /// <param name="qcCode"></param>
        public QCResult SelectQCReslut(string qcCode)
        {
            return QCResult.SelectQCResultByCode(qcCode);

        }

        /// <summary>
        /// ���¹����¼(Ĭ�ϵ�ǰʱ��)
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="eprid"></param>
        /// <param name="opdoctor"></param>
        /// <param name="resultType"></param>
        /// <param name="resultObject"></param>
        public void UpdateRuleRecord(int patid, int eprid, string opdoctor, QCResultType resultType, object resultObject)
        {
            UpdateRuleRecord(patid, eprid, opdoctor, resultType, resultObject, DateTime.Now);
        }


        /// <summary>
        /// ���¹����¼ʱ��
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="eprid"></param>
        /// <param name="opdoctor"></param>
        /// <param name="resultType"></param>
        /// <param name="resultObject"></param>
        /// <param name="resultTime"></param>
        public void UpdateRuleRecord(int patid, int eprid, string opdoctor, QCResultType resultType, QCResult resultObject, DateTime resultTime)
        {
            try
            {
                DoctorManagerPatient dmp = _patientdal.SelectDoctorManagerPatient(patid);
                if (dmp == null) return;

                QCResult qcr = resultObject;
                if (qcr == null) return;

                IList<QCRule> rules = QCRule.GetRulesByResult(qcr);
                if (rules == null || rules.Count == 0)
                {
                    return;
                }
                DataTable dtrulerecords = SelectPatientRuleRecords(patid, false);
                Collection<QCRuleRecord> rulerecords = GetRuleRecordsCollection(dtrulerecords);
                Collection<decimal> dealedXhs = new Collection<decimal>();

                for (int i = 0; i < rulerecords.Count; i++)
                {
                    QCRuleRecord qcrr = rulerecords[i];
                    //add by cyq 2012-12-07
                    if (null == qcrr.Rule)
                    {
                        continue;
                    }
                    if (qcrr.ResultState == CompleteType.Completed) continue;
                    if (((List<QCRule>)rules).Find(delegate(QCRule rule){return rule.Id == qcrr.Rule.Id;}) == null)
                    {
                        continue;
                    }
                    if (dealedXhs.IndexOf(qcrr.Xh) != -1) continue;
                    qcrr.ResultTime = resultTime;
                    qcrr.EprId = eprid;
                    qcrr.ResultState = CompleteType.Completed;
                    TimeSpan timelimit = qcrr.ResultTime - qcrr.ConditionTime;
                    if (timelimit <= qcrr.Rule.Timelimit)
                    {
                        qcrr.RuleState = RuleRecordState.DoIntime;
                    }
                    else
                    {
                        qcrr.RuleState = RuleRecordState.DoOuttime;
                    }
                    _rulerecorddal.UpdatePatientRuleRecord(dmp, qcrr);

                    Collection<QCRuleRecord> dealeds = DealRelateRuleRecords(rulerecords, patid, eprid, qcrr, dmp, resultTime, opdoctor);
                    foreach (QCRuleRecord dealrecord in dealeds)
                    {
                        dealedXhs.Add(dealrecord.Xh);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ���¹����¼(���ݷ����Ľ�������Ѿ����ڵĹ����¼)
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="eprid"></param>
        /// <param name="opdoctor"></param>
        /// <param name="resultType"></param>
        /// <param name="resultObject"></param>
        /// <param name="resultTime"></param>
        public void UpdateRuleRecord(int patid, int eprid, string opdoctor, QCResultType resultType, object resultObject, DateTime resultTime)
        {
            DoctorManagerPatient dmp = _patientdal.SelectDoctorManagerPatient(patid);
            if (dmp == null) return;

            QCResult qcr = QCResult.SelectQCResult(resultType, resultObject);
            if (qcr == null) return;

            IList<QCRule> rules = QCRule.GetRulesByResult(qcr);
            if (rules == null || rules.Count == 0) return;

            DataTable dtrulerecords = SelectPatientRuleRecords(patid, false);
            Collection<QCRuleRecord> rulerecords = GetRuleRecordsCollection(dtrulerecords);
            Collection<decimal> dealedXhs = new Collection<decimal>();

            for (int i = 0; i < rulerecords.Count; i++)
            {
                QCRuleRecord qcrr = rulerecords[i];
                if (qcrr.ResultState == CompleteType.Completed) continue;
                if (((List<QCRule>)rules).Find(
                            delegate(QCRule rule)
                            {
                                return rule.Id == qcrr.Rule.Id;
                            }
                        ) == null
                    ) continue;
                if (dealedXhs.IndexOf(qcrr.Xh) != -1) continue;
                qcrr.ResultTime = resultTime;
                qcrr.ResultState = CompleteType.Completed;
                TimeSpan timelimit = qcrr.ResultTime - qcrr.ConditionTime;
                if (timelimit <= qcrr.Rule.Timelimit)
                    qcrr.RuleState = RuleRecordState.DoIntime;
                else
                    qcrr.RuleState = RuleRecordState.DoOuttime;

                _rulerecorddal.UpdatePatientRuleRecord(dmp, qcrr);

                Collection<QCRuleRecord> dealeds = DealRelateRuleRecords(rulerecords, patid, eprid, qcrr, dmp, resultTime, opdoctor);
                foreach (QCRuleRecord dealrecord in dealeds)
                    dealedXhs.Add(dealrecord.Xh);
            }
        }

        /// <summary>
        /// edit by Yanqiao.Cai 2012-12-10
        /// 1��add try ... catch
        /// 2���޸�δ���������õ�ʵ��
        /// </summary>
        /// <return></return>
        Collection<QCRuleRecord> DealRelateRuleRecords(Collection<QCRuleRecord> rulerecords, int patid, int eprid, QCRuleRecord qcrr, DoctorManagerPatient dmp, DateTime resultTime, string opdoctor)
        {
            try
            {
                //�����ѭ��������ʱ�޹���,���������µĹ����¼
                //����ÿ�ղ���
                switch (qcrr.Rule.DealType)
                {
                    case RuleDealType.Loop:
                        DealLoopRule(patid, eprid, qcrr, dmp, resultTime, opdoctor);
                        break;
                    default:
                        break;
                }

                Collection<QCRuleRecord> relaterecords = GetRelateRuleRecords(rulerecords, qcrr);
                //������ع���
                if (null != qcrr && null != qcrr.Rule && qcrr.Rule.RelateRules != null && qcrr.Rule.RelateRules.Count > 0)
                {
                    switch (qcrr.Rule.RelateDealType)
                    {
                        case RelateRuleDealType.CancelRule:
                            _rulerecorddal.CancelRelateRuleRecord(qcrr);
                            break;
                        case RelateRuleDealType.SyncRule:
                            _rulerecorddal.SyncRelateRuleRecord(qcrr);
                            foreach (QCRuleRecord relateqcrr in relaterecords)
                            {
                                if (relateqcrr.Rule.DealType == RuleDealType.Loop)
                                    DealLoopRule(patid, eprid, relateqcrr, dmp, resultTime, opdoctor);
                            }
                            break;
                        case RelateRuleDealType.GenRule:
                            if (qcrr.Rule.DealType == RuleDealType.InnerForTrigger)
                                _rulerecorddal.TriggerRelateRuleRecord(qcrr);
                            break;
                        default:
                            break;
                    }
                }
                return relaterecords;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// edit by Yanqiao.Cai 2012-12-10
        /// 1��add try ... catch
        /// 2���޸�δ���������õ�ʵ��
        /// </summary>
        /// <return></return>
        bool ContainRule(string ruleId, IList<QCRule> rules)
        {
            try
            {
                if (null != rules && rules.Count > 0)
                {
                    foreach (QCRule qcr in rules)
                    {
                        if (ruleId == qcr.Id)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// edit by Yanqiao.Cai 2012-12-10
        /// 1��add try ... catch
        /// 2���޸�δ���������õ�ʵ��
        /// </summary>
        /// <return></return>
        Collection<QCRuleRecord> GetRelateRuleRecords(Collection<QCRuleRecord> records, QCRuleRecord record)
        {
            try
            {
                Collection<QCRuleRecord> relaterecords = new Collection<QCRuleRecord>();
                if (records != null && record != null)
                {
                    foreach (QCRuleRecord qcrr in records)
                    {
                        if (qcrr.ResultState == CompleteType.NonComplete && ContainRule(null == qcrr.Rule ? "" : qcrr.Rule.Id, null == record.Rule ? null : record.Rule.RelateRules))
                        {
                            relaterecords.Add(qcrr);
                        }
                    }
                }
                return relaterecords;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void DealLoopRule(int patid, int eprid, QCRuleRecord qcrr, DoctorManagerPatient dmp, DateTime resultTime, string opdoctor)
        {
            if (qcrr.LoopCount < qcrr.Rule.LoopTimes && qcrr.Rule.LoopTimes > 0)
            {
                QCRuleRecord qcrrloop = new QCRuleRecord(patid, eprid, qcrr.Rule);
                qcrrloop.ConditionTime = resultTime;
                qcrrloop.ConditionState = CompleteType.Completed;
                qcrrloop.RuleState = RuleRecordState.UndoIntime;
                qcrrloop.CreateDoctor = opdoctor;
                qcrrloop.LoopCount = qcrr.LoopCount + 1;
                _rulerecorddal.InsertPatientRuleRecord(dmp, qcrrloop);
            }
        }

        void SetQcRuleRecordState(QCRule rule, QCRuleRecord qcrr)
        {
            switch (rule.DealType)
            {
                case RuleDealType.Once:
                case RuleDealType.Loop:
                    qcrr.RecordState = RecordState.Valid;
                    break;
                case RuleDealType.NeedTrigger:
                    qcrr.RecordState = RecordState.ValidWait;
                    break;
                case RuleDealType.InnerForTrigger:
                    qcrr.RecordState = RecordState.ValidNonVisible;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ȡ��ָ���������͵�ʱ�޼�¼
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Collection<QCRuleRecord> GetRuleRecords(RuleDealType type)
        {
            Collection<QCRuleRecord> records = new Collection<QCRuleRecord>();
            DataSet ds = _rulerecorddal.GetUndoRulesDataSetByDealType(type);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    QCRuleRecord qcrr = _rulerecorddal.DataRow2QCRuleRecord(dt.Rows[i]);
                    if (qcrr != null) records.Add(qcrr);
                }
            }
            return records;
        }

        /// <summary>
        /// ���ù�����Ч,�޽���޶��Ĺ���
        /// </summary>
        /// <param name="rulerecord"></param>
        /// <param name="opdoctor"></param>
        public void EffectRuleRecord(QCRuleRecord rulerecord, string opdoctor)
        {
            rulerecord.RuleState = RuleRecordState.DoIntime;
            _rulerecorddal.EffectRuleRecord(rulerecord);
            DoctorManagerPatient dmp = _patientdal.SelectDoctorManagerPatient(rulerecord.PatId);

            DataTable dtrulerecords = SelectPatientRuleRecords(rulerecord.PatId, false);
            Collection<QCRuleRecord> rulerecords = GetRuleRecordsCollection(dtrulerecords);

            DealRelateRuleRecords(rulerecords, rulerecord.PatId, rulerecord.EprId, rulerecord, dmp, DateTime.Now, opdoctor);
        }

        /// <summary>
        /// ȡ���������Ͷ�Ӧ����
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string ConditionType2String(QCConditionType condtiontype)
        {
            switch (condtiontype)
            {
                case QCConditionType.AdviceChange:
                    return ConstRes.cstOrderType;
                case QCConditionType.PatStateChange:
                    return ConstRes.cstPatientType;
                case QCConditionType.EmrChange:
                    return ConstRes.cstEmrModelType;
                default:
                    break;
            }
            return string.Empty;
        }

        /// <summary>
        /// ȡ�ý�����͵Ķ�Ӧ��
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ResultType2String(QCResultType resulttype)
        {
            switch (resulttype)
            {
                case QCResultType.EmrChange:
                    return ConstRes.cstEmrModelType;
                default:
                    break;
            }
            return string.Empty;
        }

        /// <summary>
        /// �ж�һ��ʵ���Ƿ�����ָ�����͵Ĳ�������
        /// </summary>
        /// <param name="t"></param>
        /// <param name="conditionObject"></param>
        /// <param name="qcp"></param>
        /// <returns></returns>
        public static bool JudgeObjIsEqualProps(Type t, object conditionObject, QCParams qcp)
        {
            if (t == null || conditionObject == null || qcp == null || qcp.Settings.Count == 0) return false;
            if (!t.IsAssignableFrom(conditionObject.GetType())) return false;
            foreach (string key in qcp.Settings.Keys)
            {
                object value = conditionObject;
                Type proptype = t;
                string[] props = key.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < props.Length; i++)
                {
                    PropertyInfo pi = proptype.GetProperty(props[i], BindingFlags.Public | BindingFlags.Instance);
                    if (pi == null) return false;
                    value = pi.GetValue(value, null);
                    proptype = pi.PropertyType;
                }
                if (value == null) return false;
                string cmpvalue = value.GetType().IsEnum ? ((int)value).ToString() : value.ToString();
                if (!qcp.Settings[key].Check(cmpvalue)) return false;
            }
            return true;
        }
    }
}
