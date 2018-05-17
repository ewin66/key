using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ʱ�޿��ƶ�������
    /// </summary>
    public enum QcObjType
    { 
        /// <summary>
        /// ��
        /// </summary>
        None = 0,

        /// <summary>
        /// ʱ�޿��ƹ�������
        /// </summary>
        Condition = 1,

        /// <summary>
        /// ʱ�޿��ƹ������
        /// </summary>
        Result = 2,
    }

    /// <summary>
    /// ʱ�޿��ƶ���
    /// </summary>
    public abstract class QcObject
    {
        string _id;
        string _name;
        bool _isNew;
        string _judgeSetting;
        string _timeSetting;
        QcObjType _objType;

        protected QcObject(string id, string name, bool isNew)
        {
            _id = id;
            _name = name;
            _isNew = isNew;
        }

        #region properties

        /// <summary>
        /// ʱ�޶�������
        /// </summary>
        [Browsable(false)]
        public QcObjType ObjType
        {
            get { return _objType; }
            set { _objType = value; }
        }

        /// <summary>
        /// ʱ�޹�������Id
        /// </summary>
        [DisplayName("����")]
        public string Id
        {
            get { return _id; }
            internal set { _id = value; }
        }

        /// <summary>
        /// ʱ�޹�����������
        /// </summary>
        [DisplayName("����")]
        public string Name
        {
            get { return _name; }
            internal set { _name = value; }
        }

        /// <summary>
        /// ʱ�޹�����������
        /// </summary>
        [DisplayName("ʱ������")]
        public string JudgeSetting
        {
            get { return _judgeSetting; }
            set { _judgeSetting = value; }
        }

        /// <summary>
        /// ʱ������(��ʱ����)
        /// </summary>
        [Browsable(false)]
        public string TimeSetting
        {
            get { return _timeSetting; }
            set { _timeSetting = value; }
        }

        /// <summary>
        /// �Ƿ��½�
        /// </summary>
        [Browsable(false)]
        public bool IsNew
        {
            get { return _isNew; }
        }

        #endregion

        public abstract void SetQcObjInnerKind(object innerKind);

        public abstract object GetQcObjInnerKind();

        public virtual QcObject Clone()
        {
            QcObject qco = QcObjectFactory.CreateQcObjectByType(_objType, _id, _name);
            if (qco != null)
            {
                qco.JudgeSetting = _judgeSetting;
                qco.TimeSetting = _timeSetting;
            }
            return qco;
        }
    }

    /// <summary>
    /// ʱ�޿��ƶ��󹤳�
    /// </summary>
    public static class QcObjectFactory
    {
        /// <summary>
        /// ����ʱ�޿��ƶ���
        /// </summary>
        /// <param name="otype"></param>
        /// <returns></returns>
        public static QcObject CreateQcObjectByType(QcObjType otype)
        {
            switch (otype)
            {
                case QcObjType.Condition:
                    return new QCCondition();
                case QcObjType.Result:
                    return new QCResult();
                default:
                    return null;
            }
        }

        /// <summary>
        /// ����ʱ�޿��ƶ���,ָ����������
        /// </summary>
        /// <param name="otype"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static QcObject CreateQcObjectByType(QcObjType otype, string id, string name)
        {
            switch (otype)
            {
                case QcObjType.Condition:
                    return new QCCondition(id, name);
                case QcObjType.Result:
                    return new QCResult(id,name);
                default:
                    return null;
            }
        }
    }
}
