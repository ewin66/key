using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;

using System.Reflection;
using System.ComponentModel;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ����ǰ������
    /// </summary>
    public class QCCondition:QcObject
    {
        #region fields

        QCConditionType _conditionType;
        static IList<QCCondition> _allConditions = new List<QCCondition>();

        #endregion

        #region properties

        public static QCConditionDal Dal = new QCConditionDal();

        [DisplayName("��������")]
        public QCConditionType ConditionType
        {
            get { return _conditionType; }
            set { _conditionType = value; }
        }

        #endregion

        #region ctor

        /// <summary>
        /// ������ʱ������
        /// </summary>
        public QCCondition()
            : this(string.Empty, string.Empty, true)
        {
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="id">��������</param>
        /// <param name="name">��������</param>
        public QCCondition(string id, string name)
            : this(id, name, false)
        {
        }

        /// <summary>
        /// ����ʱ�޹�������,ָ���Ƿ��½�
        /// </summary>
        public QCCondition(string id, string name, bool isnew)
            : base(id, name, isnew)
        {
            ObjType = QcObjType.Condition;
        }

        #endregion

        public override void SetQcObjInnerKind(object innerKind)
        {
            if (innerKind is QCConditionType)
            {
                _conditionType = (QCConditionType)innerKind;
            }
        }

        public override object GetQcObjInnerKind()
        {
            return _conditionType;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public new QCCondition Clone()
        {
            return base.Clone() as QCCondition;
        }

        /// <summary>
        /// �����ַ�����ʾ
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// �õ����еĹ�������
        /// </summary>
        /// <returns></returns>
        public static IList<QCCondition> AllConditions
        {
            get
            {
                _allConditions = Dal.GetConditionsList();
                return _allConditions;
            }
        }

        /// <summary>
        /// �ж�����Id�Ƿ��Ѿ�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IdIsExisted(string id)
        {
            foreach (QCCondition qcc in AllConditions)
            {
                if (qcc.Id == id) return true;
            }
            return false;
        }

        /// <summary>
        /// �õ�ָ�������ʱ�޹�������
        /// </summary>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        public static QCCondition SelectQCCondition(string conditionId)
        {
            foreach (QCCondition qcc in _allConditions)
            {
                if (qcc.Id == conditionId) return qcc;
            }
            return null;
        }

        /// <summary>
        /// ͨ��ָ�����������Ͷ���λ����
        /// </summary>
        /// <param name="conditionType"></param>
        /// <param name="conditionObject"></param>
        /// <returns></returns>
        public static QCCondition SelectQCCondition(QCConditionType conditionType, object conditionObject)
        {
            IList<QCCondition> kindConditions = new List<QCCondition>();
            foreach (QCCondition qcc in _allConditions)
            {
                if (qcc._conditionType == conditionType)
                    kindConditions.Add(qcc.Clone());
            }

            string objtype = Qcsv.ConditionType2String(conditionType);
            if (string.IsNullOrEmpty(objtype))
            {
                foreach (QCCondition qcc in kindConditions)
                {
                    if (qcc.JudgeSetting == conditionObject.ToString())
                        return qcc;
                }
            }
            else
            {
                foreach (QCCondition qcc in kindConditions)
                {
                    QCParams qcp = new QCParams(qcc.JudgeSetting);
                    if (Qcsv.JudgeObjIsEqualProps(Type.GetType(objtype), conditionObject, qcp))
                        return qcc;
                }
            }
            return null;
        }

        /// <summary>
        /// ����ʱ�޹�������
        /// </summary>
        /// <param name="condition"></param>
        public static void SaveQCCondition(QCCondition condition)
        {
            Dal.SaveCondition(condition);
        }
    }
}
