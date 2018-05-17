using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Xml;
using DrectSoft.Core.TimeLimitQC;
using System.ComponentModel;
using System.Linq;
namespace DrectSoft.Core.TimeLimitQC
{
    #region EnumDutyLevel

    /// <summary>
    /// ����ҽ������
    /// </summary>
    public enum DutyLevel
    {
        /// <summary>
        /// ȫ������
        /// </summary>
        All = 0,
        /// <summary>
        /// ����ҽ��
        /// </summary>
        ZRYS = 1,
        /// <summary>
        /// ����ҽ��
        /// </summary>
        ZZYS = 2,
        /// <summary>
        /// ��λҽ��
        /// </summary>
        CWYS = 3,
        /// <summary>
        /// ������ҽ��
        /// </summary>
        FZRYS = 4
    }

    #endregion

    #region EnumRelationRuleDealType

    /// <summary>
    /// ��ع�����ʽ
    /// </summary>
    public enum RelateRuleDealType
    {
        /// <summary>
        /// ������
        /// </summary>
        [Description("������")]
        None = 0,

        /// <summary>
        /// ͬЧ����
        /// </summary>
        [Description("ͬЧ����")]
        SyncRule = 1,

        /// <summary>
        /// ȡ������
        /// </summary>
        [Description("ȡ������")]
        CancelRule = 2,

        /// <summary>
        /// ��������
        /// </summary>
        [Description("��������")]
        GenRule = 3,
    }
    #endregion

    #region Enum RuleDealType

    /// <summary>
    /// ������ʽ
    /// </summary>
    public enum RuleDealType
    {
        /// <summary>
        /// һ����
        /// </summary>
        [Description("һ����")]
        Once = 0,

        /// <summary>
        /// ѭ����Ч
        /// </summary>
        [Description("ѭ����Ч")]
        Loop = 1,

        /// <summary>
        /// ��������Ч(������������ʱ��¼״̬=ValidWait)
        /// </summary>
        [Description("��������Ч")]
        NeedTrigger = 2,

        /// <summary>
        /// �ڲ�������(������������ʱ��¼״̬=ValidNonVisible)
        /// </summary>
        [Description("�ڲ�������")]
        InnerForTrigger = 3,
    }
    #endregion

    #region QCRule

    /// <summary>
    /// ����
    /// </summary>
    public class QCRule
    {
        #region fields

        QCCondition _condition;
        QCResult _result;
        TimeSpan _timelimit;
        string _tipInfo = string.Empty;
        string _warnInfo = string.Empty;
        string _id;
        string _name;
        DutyLevel _dutylevel = DutyLevel.All;
        IList<QCRule> _relateRules = new List<QCRule>();
        RelateRuleDealType _relateDealType = RelateRuleDealType.None;
        RuleDealType _dealType = RuleDealType.Once;
        int _looptimes = 0;
        TimeSpan _looptimeinterval;
        bool _isNew;
        bool _invalid;
        static IList<QCRule> _allRules = new List<QCRule>();
        static QCRuleDal dal = new QCRuleDal();
        QCRuleGroup _group;
        string _preRelateIds = string.Empty;

        #endregion

        #region properties

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        [Browsable(false)]
        public string TipInfo
        {
            get { return _tipInfo; }
            set { _tipInfo = value; }
        }

        /// <summary>
        /// Υ����Ϣ
        /// </summary>
        [Browsable(false)]
        public string WarnInfo
        {
            get { return _warnInfo; }
            set { _warnInfo = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        [DisplayName("����")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [DisplayName("����")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        ///  ʱ��
        /// </summary>
        [Browsable(false)]
        public TimeSpan Timelimit
        {
            get { return _timelimit; }
            set { _timelimit = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [Browsable(false)]
        public QCCondition Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        [Browsable(false)]
        public QCResult Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary>
        /// ҽ������
        /// </summary>
        [Browsable(false)]
        public DutyLevel Dutylevel
        {
            get { return _dutylevel; }
            set { _dutylevel = value; }
        }

        /// <summary>
        /// ��ع���
        /// </summary>
        [Browsable(false)]
        public ReadOnlyCollection<QCRule> RelateRules
        {
            get
            {
                return new ReadOnlyCollection<QCRule>(_relateRules);
            }
        }

        /// <summary>
        /// ��ع���Id
        /// </summary>
        [Browsable(false)]
        public string RelateRuleIds
        {
            get
            {
                string idlist = string.Empty;
                if (_relateRules != null)
                    for (int i = 0; i < _relateRules.Count; i++)
                        idlist = idlist + _relateRules[i].Id + ",";
                return idlist;
            }
        }

        /// <summary>
        /// Ԥ�ȱ�����ع���Id,�������й�����ٴμ�����ع���
        /// </summary>
        internal string PreRelateRuleIds
        {
            get { return _preRelateIds; }
            set { _preRelateIds = value; }
        }

        /// <summary>
        /// ��ع�����ʽ
        /// </summary>
        [Browsable(false)]
        public RelateRuleDealType RelateDealType
        {
            get { return _relateDealType; }
            set { _relateDealType = value; }
        }

        /// <summary>
        /// ������ʽ
        /// </summary>
        [Browsable(false)]
        public RuleDealType DealType
        {
            get { return _dealType; }
            set { _dealType = value; }
        }

        /// <summary>
        /// ����ѭ������ʱ�Ĵ���
        /// </summary>
        [Browsable(false)]
        public int LoopTimes
        {
            get { return _looptimes; }
            set { _looptimes = value; }
        }

        /// <summary>
        /// ѭ������ʱ����
        /// </summary>
        [Browsable(false)]
        public TimeSpan LoopTimeInterVal
        {
            get { return _looptimeinterval; }
            set { _looptimeinterval = value; }
        }

        /// <summary>
        /// �¹���
        /// </summary>
        [Browsable(false)]
        public bool IsNew
        {
            get { return _isNew; }
        }

        /// <summary>
        /// ��Ч
        /// </summary>
        [Browsable(false)]
        public bool Invalid
        {
            get { return _invalid; }
            set { _invalid = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        [DisplayName("����")]
        public QCRuleGroup Group
        {
            get { return _group; }
            set { _group = value; }
        }

        #endregion

        #region ctor

        /// <summary>
        /// ����һ���յ��¹���
        /// </summary>
        public QCRule()
            : this(string.Empty, string.Empty, DutyLevel.All)
        {
            _isNew = true;
        }

        /// <summary>
        /// ����
        /// </summary>
        public QCRule(string id, string name, DutyLevel dutylevel)
        {
            _id = id;
            _name = name;
            _dutylevel = dutylevel;
        }
        #endregion

        /// <summary>
        /// �õ����еĹ���
        /// </summary>
        /// <param name="conditions">ʱ����������</param>
        /// <param name="results">ʱ�޽������</param>
        /// <returns></returns>
        public static IList<QCRule> GetAllRules(IList<QCCondition> conditions, IList<QCResult> results)
        {
            //if (_allRules == null || _allRules.Count == 0)
            {
                _allRules = dal.GetRulesList(conditions, results);
            }
            return _allRules;
        }

        /// <summary>
        /// �õ�ָ�������ʱ�޹���
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public static QCRule SelectQCRule(string ruleId)
        {
            foreach (QCRule qcr in _allRules)
            {
                if (qcr._id == ruleId) return qcr;
            }
            return null;
        }

        /// <summary>
        /// ȡ��ָ��������ʱ�޹���
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IList<QCRule> GetRulesByCondition(QCCondition condition)
        {
            IList<QCRule> ret = new List<QCRule>();
            foreach (QCRule qcr in _allRules)
            {
                if (qcr._condition.Id == condition.Id)
                    ret.Add(qcr.Clone());
            }
            return ret;
        }

        /// <summary>
        /// ȡ��ָ�������ʱ�޹���
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<QCRule> GetRulesByResult(QCResult result)
        {
            List<QCRule> ret = new List<QCRule>();


            foreach (QCRule qcr in _allRules)
            {
                if (qcr.Result == null) continue;
                if (qcr.Result.Id == result.Id)
                    ret.Add(qcr.Clone());
            }
            return ret;
        }

        /// <summary>
        /// �ַ��� -> TimeSpan��
        /// </summary>
        /// <param name="timelimit"></param>
        /// <returns></returns>
        public static TimeSpan LimitString2TimeSpan(string timelimit)
        {
            if (string.IsNullOrEmpty(timelimit)) return TimeSpan.Zero;

            int days = 0, hours = 0, minutes = 0, seconds = 0;

            int i;

            string temp = timelimit;

            i = timelimit.IndexOf("d");
            if (i >= 0)
            {
                if (i > 0) days = int.Parse(temp.Substring(0, i));
                temp = temp.Substring(i + 1, temp.Length - i - 1);
            }
            i = temp.IndexOf("h");
            if (i >= 0)
            {
                if (i > 0) hours = int.Parse(temp.Substring(0, i));
                temp = temp.Substring(i + 1, temp.Length - i - 1);
            }
            i = temp.IndexOf("n");
            if (i >= 0)
            {
                if (i > 0) minutes = int.Parse(temp.Substring(0, i));
                temp = temp.Substring(i + 1, temp.Length - i - 1);
            }
            i = temp.IndexOf("s");
            if (i >= 0)
            {
                if (i > 0) seconds = int.Parse(temp.Substring(0, i));
                temp = temp.Substring(i + 1, temp.Length - i - 1);
            }

            return new TimeSpan(days, hours, minutes, seconds);
        }

        /// <summary>
        /// TimeSpan�� -> �ַ���
        /// </summary>
        /// <param name="timelimit"></param>
        /// <returns></returns>
        public static string TimeSpan2LimitString(TimeSpan timelimit)
        {
            int days = 0, hours = 0, minutes = 0, seconds = 0;

            days = timelimit.Days;
            hours = timelimit.Hours;
            minutes = timelimit.Minutes;
            seconds = timelimit.Seconds;

            string ts = string.Empty;
            if (days != 0)
            {
                ts += days.ToString() + "d";
            }
            else
                ts += "d";
            if (hours != 0)
            {
                ts += hours.ToString() + "h";
            }
            else
                ts += "h";
            if (minutes != 0)
            {
                ts += minutes.ToString() + "n";
            }
            else
                ts += "n";
            if (seconds != 0)
            {
                ts += seconds.ToString() + "s";
            }
            else
                ts += "s";

            return ts;
        }

        /// <summary>
        /// �ж�����Id�Ƿ��Ѿ�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IdIsExisted(string id)
        {
            if (_allRules == null) return false;

            foreach (QCRule qcr in _allRules)
            {
                if (qcr.Id == id) return true;
            }
            return false;
        }

        /// <summary>
        /// ����ʱ�޹�������
        /// </summary>
        /// <param name="rule"></param>
        public static void SaveQCRule(QCRule rule)
        {
            dal.SaveRule(rule);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="rule"></param>
        public static void DeleteQCRule(QCRule rule)
        {
            dal.DeleteQCRule(rule);
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public QCRule Clone()
        {
            QCRule qcrc = new QCRule(_id, _name, _dutylevel);
            qcrc.Timelimit = _timelimit;
            qcrc.TipInfo = _tipInfo;
            qcrc.WarnInfo = _warnInfo;
            qcrc.RelateDealType = _relateDealType;
            qcrc.DealType = _dealType;
            qcrc.LoopTimes = _looptimes;
            qcrc.LoopTimeInterVal = _looptimeinterval;
            qcrc.Invalid = _invalid;
            if (_condition != null) qcrc.Condition = _condition.Clone();
            if (_result != null) qcrc.Result = _result.Clone();
            if (_group != null) qcrc.Group = _group.Clone();
            return qcrc;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._name;
        }

        #region relate rule operator

        /// <summary>
        /// �����ع��򼯺�
        /// </summary>
        public void ClearRelateRules()
        {
            _relateRules.Clear();
        }

        /// <summary>
        /// ������ع���
        /// </summary>
        /// <returns></returns>
        public bool AddRelateRule(QCRule rule)
        {
            if (!CheckRelateNoLoop(rule)) return false;
            if (!CheckRelateNoContain(rule)) return false;
            this._relateRules.Add(rule.Clone());
            return true;
        }

        bool CheckRelateNoLoop(QCRule rule)
        {
            if (rule.Id == this.Id) return false;
            if (rule.RelateRules != null)
            {
                foreach (QCRule subrule in rule.RelateRules)
                {
                    if (!CheckRelateNoLoop(subrule)) return false;
                }
            }
            return true;
        }

        bool CheckRelateNoContain(QCRule rule)
        {
            foreach (QCRule subrule in _relateRules)
            {
                if (subrule.Id == rule.Id) return false;
            }
            return true;
        }
        #endregion
    }

    #endregion

}
