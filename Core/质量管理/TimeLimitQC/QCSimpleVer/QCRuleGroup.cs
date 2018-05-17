using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ʱ�޹������
    /// </summary>
    public class QCRuleGroup:IComparable
    {
        string _id;
        string _name;

        /// <summary>
        /// ����
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        static IList<QCRuleGroup> _allRuleGroups;
        static QCRuleGroupDal _dal = new QCRuleGroupDal();

        /// <summary>
        /// ����
        /// </summary>
        public QCRuleGroup(string id, string name)
        {
            _id = id;
            _name = name;
        }

        /// <summary>
        /// ���й������
        /// </summary>
        public static IList<QCRuleGroup> AllRuleGroups
        {
            get 
            {
                _allRuleGroups = _dal.GetRuleGroupsList();
                return _allRuleGroups;
            }
        }

        /// <summary>
        /// �õ�ָ�������ʱ�޹������
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static QCRuleGroup SelectQCRuleGroup(string groupId)
        {
            foreach (QCRuleGroup qcrg in _allRuleGroups)
            {
                if (qcrg._id == groupId) return qcrg;
            }
            return null;
        }

        /// <summary>
        /// override ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public QCRuleGroup Clone()
        {
            return new QCRuleGroup(_id, _name);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            QCRuleGroup qcrg = obj as QCRuleGroup;
            if (qcrg == null) return 1;
            return this.Id.CompareTo(qcrg.Id);
        }

        #endregion
    }
}
