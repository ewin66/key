using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ҽ���ֹܲ�����
    /// </summary>
    public class DoctorManagerPatient
    {
        int _noOfInpat;
        string _inpatientName;
        string _patID;
        string _attend;
        string _resident;
        string _chief;

        /// <summary>
        /// ������ҳ���
        /// </summary>
        public int NoOfInpat
        {
            get { return _noOfInpat; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string InpatientName
        {
            get { return _inpatientName; }
        }

        /// <summary>
        /// סԺ����
        /// </summary>
        public string PatID
        {
            get { return _patID; }
        }

        /// <summary>
        /// סԺҽ������
        /// </summary>
        public string Attend
        {
            get { return _attend; }
        }

        /// <summary>
        /// ����ҽ������
        /// </summary>
        public string Resident
        {
            get { return _resident; }
        }

        /// <summary>
        /// ����ҽ������
        /// </summary>
        public string Chief
        {
            get { return _chief; }
        }

        /// <summary>
        /// ����һ��ҽ���ֹܲ���
        /// </summary>
        /// <param name="NoOfInpat">������ҳ���</param>
        /// <param name="InpatientName">��������</param>
        /// <param name="PatID">סԺ����</param>
        /// <param name="Attend ">סԺҽ������</param>
        /// <param name="Resident">����ҽ������</param>
        /// <param name="Chief ">����ҽ������</param>
        public DoctorManagerPatient(int noOfInpat, string inpatientName, string patID,
            string attend, string resident, string chief)
        {
            _noOfInpat = noOfInpat;
            _inpatientName = inpatientName;
            _patID = patID;
            _attend = attend;
            _resident = resident;
            _chief = chief;
        }
    }
}
