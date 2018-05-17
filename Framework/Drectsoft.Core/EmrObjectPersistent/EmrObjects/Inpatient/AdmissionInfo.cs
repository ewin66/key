using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using DrectSoft.Common.Eop;



namespace DrectSoft.Common.Eop
{
    /// <summary>
    /// סԺ��Ϣ
    /// </summary>
    public class AdmissionInfo : EPBaseObject
    {
        #region properties
        /// <summary>
        /// ��Ժ������Ϣ
        /// </summary>
        public AdmissionBasicInfo AdmitInfo
        {
            get { return _admitInfo; }
            set { _admitInfo = value; }
        }
        private AdmissionBasicInfo _admitInfo;

        /// <summary>
        /// ��Ժ������Ϣ
        /// </summary>
        public AdmissionBasicInfo DischargeInfo
        {
            get { return _dischargeInfo; }
            set { _dischargeInfo = value; }
        }
        private AdmissionBasicInfo _dischargeInfo;

        /// <summary>
        /// ��Ժ;�������תԺ�ȣ�
        /// </summary>
        public CommonBaseCode AdmissionKind
        {
            get { return _admissionKind; }
            set { _admissionKind = value; }
        }
        private CommonBaseCode _admissionKind;

        /// <summary>
        /// ��Ժ���(��Ժʱ����)
        /// </summary>
        public CommonBaseCode AdmitStatus
        {
            get { return _admitStatus; }
            set { _admitStatus = value; }
        }
        private CommonBaseCode _admitStatus;

        /// <summary>
        /// סԺ����
        /// </summary>
        public decimal LengthOfStay
        {
            get
            {
                return _lengthOfStay;
            }
            set { _lengthOfStay = value; }
        }
        private decimal _lengthOfStay;

        /// <summary>
        /// X����Ϣ
        /// </summary>
        public string XRecordNo
        {
            get { return _xRecordNo; }
            set { _xRecordNo = value; }
        }
        private string _xRecordNo;

        /// <summary>
        /// ��Ժ��ʽ(��������ת��)
        /// </summary>
        public CommonBaseCode DischargeStatus
        {
            get { return _dischargeStatus; }
            set { _dischargeStatus = value; }
        }
        private CommonBaseCode _dischargeStatus;

        /// <summary>
        /// �������
        /// </summary>
        public ICDDiagnosis DiagnosisOfClinic
        {
            get { return _diagnosisOfClinic; }
            set { _diagnosisOfClinic = value; }
        }
        private ICDDiagnosis _diagnosisOfClinic;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public Employee ClinicDoctor
        {
            get { return _clinicDoctor; }
            set { _clinicDoctor = value; }
        }
        private Employee _clinicDoctor;

        /// <summary>
        /// �������(��ҽ)
        /// </summary>
        public ChineseDiagnosis ChineseDiagnosisOfClinic
        {
            get { return _chineseDiagnosisOfClinic; }
            set { _chineseDiagnosisOfClinic = value; }
        }
        private ChineseDiagnosis _chineseDiagnosisOfClinic;

        /// <summary>
        /// ����֢��(��ҽ)
        /// </summary>
        public ChineseDiagnosis ChineseDiagnosisOfClinic2
        {
            get { return _chineseDiagnosisOfClinic2; }
            set { _chineseDiagnosisOfClinic2 = value; }
        }
        private ChineseDiagnosis _chineseDiagnosisOfClinic2;

        /// <summary>
        /// ��������(��ҽʹ��)
        /// </summary>
        public string SolarTerm
        {
            get { return _solarTerm; }
            set { _solarTerm = value; }
        }
        private string _solarTerm;

        /// <summary>
        /// סԺҽ��
        /// </summary>
        public Employee Resident
        {
            get { return _resident; }
            set { _resident = value; }
        }
        private Employee _resident;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public Employee AttendingPhysician
        {
            get { return _attendingPhysician; }
            set { _attendingPhysician = value; }
        }
        private Employee _attendingPhysician;

        /// <summary>
        /// ����ҽʦ����
        /// </summary>
        public Employee Director
        {
            get { return _director; }
            set { _director = value; }
        }
        private Employee _director;

        /// <summary>
        /// ������
        /// </summary>
        public ChargeItem CareLevel
        {
            get { return _careLevel; }
            set { _careLevel = value; }
        }
        private ChargeItem _careLevel;

        /// <summary>
        /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
        /// </summary>
        public override string InitializeSentence
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// ��DataTable�а�����ֵ���˼�¼������
        /// </summary>
        public override string FilterCondition
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region ctors
        /// <summary>
        /// ��ʼ����ʵ��
        /// </summary>
        public AdmissionInfo()
            : base()
        { }

        /// <summary>
        /// ��DataRow��ʼ��ʵ��
        /// </summary>
        /// <param name="sourceRow"></param>
        public AdmissionInfo(DataRow sourceRow)
            : base(sourceRow)
        { }
        #endregion

        #region public methods
        /// <summary>
        /// ��ʼ�����е����ԣ������������͵������Լ�������
        /// </summary>
        public override void ReInitializeAllProperties()
        {
            try
            {
                if (AdmitInfo != null)
                    AdmitInfo.ReInitializeAllProperties();
                if (DischargeInfo != null)
                    DischargeInfo.ReInitializeAllProperties();
                if (AdmissionKind != null)
                    AdmissionKind.ReInitializeAllProperties();
                if (AdmitStatus != null)
                    AdmitStatus.ReInitializeAllProperties();
                if (DischargeStatus != null)
                    DischargeStatus.ReInitializeAllProperties();
                if (DiagnosisOfClinic != null)
                    DiagnosisOfClinic.ReInitializeAllProperties();
                if (ClinicDoctor != null)
                    ClinicDoctor.ReInitializeAllProperties();
                if (ChineseDiagnosisOfClinic != null)
                    ChineseDiagnosisOfClinic.ReInitializeAllProperties();
                if (ChineseDiagnosisOfClinic2 != null)
                    ChineseDiagnosisOfClinic2.ReInitializeAllProperties();
                if (Resident != null)
                    Resident.ReInitializeAllProperties();
                if (AttendingPhysician != null)
                    AttendingPhysician.ReInitializeAllProperties();
                if (Director != null)
                    Director.ReInitializeAllProperties();
                if (CareLevel != null)
                    CareLevel.ReInitializeAllProperties();
            }
            catch (Exception)
            {

            }
        }
        #endregion


        internal void EnsureLengthOfStay(string noOfHisFirstPage)
        {
            //�Բ��˳������㲡��סԺ����
            if ((DischargeInfo == null) || (DischargeInfo.StepOneDate == DateTime.MinValue))
                _lengthOfStay = -1;
            else
            {
                //��������-��������
                TimeSpan span = DischargeInfo.StepOneDate - AdmitInfo.StepTwoDate;
                _lengthOfStay = span.Days;
            }
            //����HIS �������㲡��סԺ����
            //��˵����Ƿ������
            // string sql = "select dbo.fun_zyb_calzyts (" + noOfHisFirstPage + ",1,getdate())";
            //LengthOfStay = Convert.ToDecimal(hisDataAccess.ExecuteScalar(sql));

        }

        //��Ϊ��ʱ�ⲿҲ�������գ����Ծͻᷢ���������ڲ��ԣ����Ƿֿ�����
        internal void EnsureLengthOfStay(decimal NoOfHisFirstPage, PatientBasicInfo patientBasic)
        {
            //�Բ��˳������㲡��סԺ����
            if ((DischargeInfo == null) || (DischargeInfo.StepOneDate == DateTime.MinValue))
                _lengthOfStay = -1;
            else
            {
                //��������-��������
                TimeSpan span = DischargeInfo.StepOneDate - AdmitInfo.StepTwoDate;
                _lengthOfStay = span.Days;
            }

            //         select dbo.fun_zyb_calzyts ('26',1,getdate())

            //select b.birth from ZY_BRSYK a
            //inner join ZY_BABYSYK b on (a.syxh=b.yesyxh)
            //where a.yebz=1 and b.syxh=26
            //����HIS �������㲡��סԺ����
            //��˵����Ƿ������
            //string sql = "select dbo.fun_zyb_calzyts (" + NoOfHisFirstPage + ",1,getdate())";
            //if (patientBasic.BabySequence == 1)
            //{
            //    sql = sql + "select dbo.ufnConvertDateString(b.birth,'TD') from ZY_BRSYK a inner join ZY_BABYSYK b on (a.syxh=b.yesyxh)" +
            //          "where a.yebz=1 and a.syxh=" + NoOfHisFirstPage + "";
            //}
            //DataSet dsHisBasicInfo = hisDataAccess.ExecuteDataSet(sql);
            //LengthOfStay = Convert.ToDecimal(dsHisBasicInfo.Tables[0].Rows[0][0].ToString());
            //if (patientBasic.BabySequence == 1 && dsHisBasicInfo.Tables[1] != null && dsHisBasicInfo.Tables[1].Rows.Count > 0)
            //    patientBasic.Birthday = Convert.ToDateTime(dsHisBasicInfo.Tables[1].Rows[0][0].ToString());

        }
    }
}
