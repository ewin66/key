using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ͳһ�����Ͳ��������ݷ��ʷ���
    /// </summary>
    internal interface IQCDataDal
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        DataSet GetDataSet();

        /// <summary>
        /// �����е�ʱ�޿��ƶ���
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        QcObject DataRow2QcObj(DataRow row);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="o"></param>
        void SaveRecord(QcObject o);

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="o"></param>
        void DeleteRecord(QcObject o);
    }
}
