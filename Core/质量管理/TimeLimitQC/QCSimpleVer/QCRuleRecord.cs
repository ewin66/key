using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.Core.TimeLimitQC;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ����ʱ�޼�¼
    /// </summary>
    public class QCRuleRecord
    {
        #region fields

        decimal _xh;
        int _patId;
        int _eprId;
        DateTime _conditionTime = DateTime.MinValue;
        CompleteType _conditionState;
        DateTime _resultTime = DateTime.MinValue;
        CompleteType _resultState;
        DateTime _createTime;
        string _createDoctor;
        string _dutyDoctor;
        RuleRecordState _ruleState;
        QCRule _rule;
        int _loopCount = 1;
        RecordState _recordState = RecordState.Valid;

        #endregion

        #region properties

        /// <summary>
        /// ��¼���
        /// </summary>
        public decimal Xh
        {
            get { return _xh; }
            internal set { _xh = value; }
        }

        /// <summary>
        /// ����Id
        /// </summary>
        public int EprId
        {
            get { return _eprId; }
            set { _eprId = value; }
        }

        /// <summary>
        /// ����Id
        /// </summary>
        public int PatId
        {
            get { return _patId; }
            set { _patId = value; }
        }

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public DateTime ConditionTime
        {
            get { return _conditionTime; }
            set { _conditionTime = value; }
        }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime ResultTime
        {
            get { return _resultTime; }
            set { _resultTime = value; }
        }

        /// <summary>
        /// �������״̬
        /// </summary>
        public CompleteType ConditionState
        {
            get { return _conditionState; }
            set { _conditionState = value; }
        }

        /// <summary>
        /// ������״̬
        /// </summary>
        public CompleteType ResultState
        {
            get { return _resultState; }
            set { _resultState = value; }
        }

        /// <summary>
        /// �����¼����ʱ��
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        /// <summary>
        /// ������¼��ҽ������
        /// </summary>
        public string CreateDoctor
        {
            get { return _createDoctor; }
            set { _createDoctor = value; }
        }

        /// <summary>
        /// ����ҽ������
        /// </summary>
        public string DutyDoctor
        {
            get { return _dutyDoctor; }
            set { _dutyDoctor = value; }
        }

        /// <summary>
        /// �����¼״̬
        /// </summary>
        public RuleRecordState RuleState
        {
            get { return _ruleState; }
            set { _ruleState = value; }
        }

        /// <summary>
        /// ��¼��ԵĹ���
        /// </summary>
        public QCRule Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        /// <summary>
        /// ѭ������
        /// </summary>
        public int LoopCount
        {
            get { return _loopCount; }
            set { _loopCount = value; }
        }

        /// <summary>
        /// ���ݼ�¼״̬
        /// </summary>
        public RecordState RecordState
        {
            get { return _recordState; }
            set { _recordState = value; }
        }
        #endregion

        #region ctor

        /// <summary>
        /// �����µļ�¼
        /// </summary>
        public QCRuleRecord(int patid, int eprid, QCRule rule)
        {
            _patId = patid;
            _eprId = eprid;
            _rule = rule;
            _xh = -1;
        }

        /// <summary>
        /// �������м�¼
        /// </summary>
        /// <param name="xh"></param>
        /// <param name="patid"></param>
        /// <param name="eprid"></param>
        /// <param name="rule"></param>
        public QCRuleRecord(decimal xh, int patid, int eprid, QCRule rule)
        {
            _patId = patid;
            _eprId = eprid;
            _rule = rule;
            _xh = xh;
        }
        #endregion
    }
}
