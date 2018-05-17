using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Collections.ObjectModel;
using System.Data;
using System.ComponentModel;
using System.Linq;
namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ��������
    /// </summary>
    public class QCResult : QcObject
    {
        #region fields

        DateTime _operatorTime = DateTime.Today;
        QCResultType _type;
        static IList<QCResult> _allResults;

        #endregion

        #region properties

        public static QCResultDal Dal = new QCResultDal();

        [Browsable(false)]
        public DateTime OperatorTime
        {
            get { return _operatorTime; }
        }

        /// <summary>
        /// �������
        /// </summary>
        [DisplayName("��������")]
        public QCResultType ResultType
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// ��ش���
        /// </summary>
        public string QCCode
        {
            get { return _qcCode; }
            set { _qcCode = value; }
        }

        private string _qcCode;

        #endregion

        #region ctor

        /// <summary>
        /// ������ʱ�޲���
        /// </summary>
        public QCResult()
            : this(string.Empty, string.Empty, true)
        { }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public QCResult(string id, string name)
            : this(id, name, false)
        {
        }

        /// <summary>
        /// ����ʱ�޹���������,ָ���Ƿ��½�
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="isnew"></param>
        public QCResult(string id, string name, bool isnew)
            : base(id, name, isnew)
        {
            ObjType = QcObjType.Result;
        }

        #endregion

        /// <summary>
        /// ȡ�����ж��������
        /// </summary>
        /// <returns></returns>
        public static IList<QCResult> AllResults
        {
            get
            {
                //if (_allResults == null || _allResults.Count == 0)
                {
                    _allResults = Dal.GetResultsList();
                }
                return _allResults;
            }
        }

        /// <summary>
        /// �õ�ָ�������ʱ�޹�����
        /// </summary>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public static QCResult SelectQCResult(string resultId)
        {
            foreach (QCResult qcr in _allResults)
            {
                if (qcr.Id == resultId) return qcr;
            }
            return null;
        }

        /// <summary>
        /// ͨ��ָ�����������Ͷ���λ����
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="resultObject"></param>
        /// <returns></returns>
        public static QCResult SelectQCResult(QCResultType resultType, object resultObject)
        {
            IList<QCResult> kindResults = new List<QCResult>();
            foreach (QCResult qcr in _allResults)
            {
                if (qcr._type == resultType)
                    kindResults.Add(qcr.Clone());
            }

            string objtype = Qcsv.ResultType2String(resultType);
            if (string.IsNullOrEmpty(objtype))
            {
                foreach (QCResult qcr in kindResults)
                {
                    if (qcr.JudgeSetting == resultObject.ToString())
                        return qcr;
                }
            }
            else
            {
                foreach (QCResult qcr in kindResults)
                {
                    QCParams qcp = new QCParams(qcr.JudgeSetting);
                    if (Qcsv.JudgeObjIsEqualProps(Type.GetType(objtype), resultObject, qcp))
                        return qcr;
                }
            }
            return null;
        }

        /// <summary>
        /// ͨ����ش����ȡ��ؽ��
        /// </summary>
        /// <param name="QcCode"></param>
        /// <returns></returns>
        public static QCResult SelectQCResultByCode(string QcCode)
        {
            if (AllResults == null) return null;

            var qcresluts = AllResults.Where(qc => qc.QCCode.Equals(QcCode));

            if (qcresluts.Count() > 0)
                return qcresluts.First();
            return null;

        }

        /// <summary>
        /// �жϽ��Id�Ƿ��Ѿ�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IdIsExisted(string id)
        {
            foreach (QCResult qcr in AllResults)
            {
                if (qcr.Id == id) return true;
            }
            return false;
        }

        /// <summary>
        /// ����ʱ�޹�������
        /// </summary>
        /// <param name="result"></param>
        public static void SaveQCResult(QCResult result)
        {
            Dal.SaveResult(result);
        }

        static SqlParameter NewParam(string id, string type)
        {
            SqlParameter ret = null;
            if (type == "string")
                ret = new SqlParameter(id, SqlDbType.VarChar);
            if (type == "int")
                ret = new SqlParameter(id, SqlDbType.Int);
            return ret;
        }

        /// <summary>
        /// ����ʱ�޿��ƶ����ڲ�����
        /// </summary>
        /// <param name="innerKind"></param>
        public override void SetQcObjInnerKind(object innerKind)
        {
            if (innerKind is QCResultType)
            {
                _type = (QCResultType)innerKind;
            }
        }

        public override object GetQcObjInnerKind()
        {
            return _type;
        }

        /// <summary>
        /// clone
        /// </summary>
        /// <returns></returns>
        public new QCResult Clone()
        {
            return base.Clone() as QCResult;
        }

        /// <summary>
        /// tostring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}


