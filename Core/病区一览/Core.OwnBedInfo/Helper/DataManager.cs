using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DrectSoft.Common;
using DrectSoft.Service;
using DrectSoft.Common.Eop;
using DrectSoft.FrameWork.WinForm.Plugin;


namespace DrectSoft.Core.OwnBedInfo
{
    /// <summary>
    /// ��ǰ����һ��������״̬
    /// </summary>
    public enum ViewState
    {
        /// <summary>
        /// ȫ������
        /// </summary>
        AllPats = 0,
        /// <summary>
        /// �ֹܲ���
        /// </summary>
        DistriPats = 1,
        /// <summary>
        /// ��ȫ���Ƿֹ�
        /// </summary>
        NoAllAndDistri = 2,
        /// <summary>
        /// ��Ժ����(����,��Ƭ��ʽ����һҳ)
        /// </summary>
        InHospitalCardFirst = 3,
        /// <summary>
        /// ��Ժ����(��������Ƭ��ʽ���ڶ�ҳ)
        /// </summary>
        InHospitalCardSecond = 4,
        /// <summary>
        /// ��Ժ����(��������Ƭ��ʽ������ҳ)
        /// </summary>
        InHospitalCardThird = 10,
        /// <summary>
        /// ��Ժ����(�������б���ʽ)
        /// </summary>
        InHospitalList = 5,
        /// <summary>
        /// ��ת������(����)
        /// </summary>
        ChangeWard = 6,
        /// <summary>
        /// ��Ժδ�鵵����(����)
        /// </summary>
        OutHospital = 7,
        /// <summary>
        /// ��ʷ���˲�ѯ(����)
        /// </summary>
        History = 8,
        /// <summary>
        /// �ǵ���״̬
        /// </summary>
        NoFunction = 9
    }



    public enum QueryState
    {
        /// <summary>
        /// ��
        /// </summary>
        None = 0,
        /// <summary>
        /// �����ڴ�����
        /// </summary>
        WardPat = 1,
        /// <summary>
        /// ��Ժ������δ�鵵����
        /// </summary>
        NotArchived = 2,
        /// <summary>
        /// ��ѯ��ʷ����
        /// </summary>
        QueryOutHostipal = 3
    }

    public enum QueryType
    {
        /// <summary>
        /// ȫ��
        /// </summary>
        ALL = 0,
        /// <summary>
        /// �ֹ�
        /// </summary>
        OWN = 1
    }



    /// <summary>
    /// ���ݲ�����
    /// </summary>
    public class DataManager : IDisposable
    {
        #region variable

        private static IEmrHost m_App;
        private static IDataAccess m_SqlHelper;

        internal static string m_DeptId;
        internal static string DeptId
        {
            get { return m_DeptId; }
            set { m_DeptId = value; }
        }
        internal static string WardId
        {
            get { return m_WardId; }
            set { m_WardId = value; }
        }
        internal static string m_WardId;
        private const int STShiftDept = (int)InpatientState.ShiftDept;
        private const int STOutWard = (int)InpatientState.OutWard;
        private const int STBalanced = (int)InpatientState.Balanced;

        private DataSet _searchData;
        /// <summary>
        /// ʵ����Ч��¼��
        /// </summary>
        internal int RowCount
        {
            get { return _rowCount; }
        }

        private int _rowCount;
        /// <summary>
        /// ��ת�����ĺ��ŵļ�¼������
        /// </summary>
        private static string SortIDChange = string.Format("{0} ASC", PatientWardField.FieldIdHorOrder);
        /// <summary>
        /// ����λ��������
        /// </summary>
        private static string SortBedCode = string.Format("{0} ASC", PatientWardField.Fieldcwdm);
        #endregion

        #region property
        private DataTable _tableWard;
        /// <summary>
        /// �������˱�
        /// </summary>
        internal DataTable TableWard
        {
            get
            {
                if (_tableWard == null)
                {
                    _dataLoaded = false;
                    GetWardData(WardId, DeptId, false);
                }
                return _tableWard;
            }
        }

        public void RefreshWardData()
        {
            GetWardData(WardId, DeptId, true);
        }


        #endregion

        #region ctor
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="app"></param>
        /// <param name="sqlHelper"></param>
        /// <param name="deptId">���Ҵ���</param>
        /// <param name="wardId">��������</param>
        /// <param name="isObstetric">����</param>
        public DataManager(IEmrHost app, string deptId
           , string wardId)
        {
            m_App = app;
            m_SqlHelper = app.SqlHelper;
            m_DeptId = deptId;
            m_WardId = wardId;
        }


        #endregion

        #region Control Event

        #endregion

        #region methods



        /// <summary>
        /// ȡ�ò�������
        /// </summary>
        /// <param name="wardId">��������</param>
        /// <param name="deptId">���Ҵ���</param>
        /// <param name="needReflesh"></param>
        /// <returns></returns>
        internal int GetWardData(string wardId, string deptId, bool needReflesh)
        {
            try
            {
                if (needReflesh)
                {
                    string result = m_App.RefreshPatientInfos();
                    if (!string.IsNullOrEmpty(result))
                    {
                        m_App.CustomMessageBox.MessageShow(result, CustomMessageBoxKind.WarningOk);
                        return _tableWard.Rows.Count;
                    }
                }
                _tableWard = null;
                _tableWard = m_App.PatientInfos.Tables["��λ��Ϣ"].Copy();
                _dataLoaded = true;
                _rowCount = _tableWard.Rows.Count;
                return _rowCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// ȡ��������RowWholeNumberΪ�����ļ�¼��
        /// </summary>
        /// <param name="count"></param>
        /// <param name="rowWholeNumber"></param>
        /// <returns></returns>
        internal static int GetMaxRow(int count, int rowWholeNumber)
        {
            return count / rowWholeNumber + ((count % rowWholeNumber == 0) ? 0 : 1);
        }

        /// <summary>
        /// ȡ�ò��˷�����Ϣ
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="wardId"></param>
        /// <returns></returns>
        internal DataTable GetPatientFee(string deptId, string wardId)
        {
            return null;
        }


        private bool _dataLoaded;
        /// <summary>
        /// �������װ��״̬
        /// </summary>
        internal bool DataLoaded
        {
            get { return _dataLoaded; }
        }



        /// <summary>
        /// ȡȫ��OR�ֹܲ�����λ��Ϣ
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="wardId"></param>
        /// <param name="queryType"></param>
        /// <returns></returns>
        internal DataTable GetCurrentBedInfos(string deptId, string wardId, QueryType queryType)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[] { 
                  new SqlParameter("ID", SqlDbType.VarChar, 4),
                  new SqlParameter("Deptids", SqlDbType.VarChar, 8),
                  new SqlParameter("Wardid", SqlDbType.VarChar, 8),
                  new SqlParameter("QueryType", SqlDbType.Int)};
                sqlParams[0].Value = m_App.User.Id; // 
                sqlParams[1].Value = deptId; // ���Ҵ���
                sqlParams[2].Value = wardId; // ��������
                sqlParams[3].Value = (int)queryType; // 
                DataTable dataTable = m_SqlHelper.ExecuteDataTable("usp_QueryInwardPatients", sqlParams, CommandType.StoredProcedure);
                dataTable.TableName = "����һ����λ��Ϣ";
                return dataTable;

            }
            catch (Exception ex)
            {
                m_App.CustomMessageBox.MessageShow("����һ����" + ex.Message + "", CustomMessageBoxKind.ErrorOk);
                return null;
            }
        }

        internal DataTable GetOutPatinet(string deptId, string wardId, QueryType queryType)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[] { 
                  new SqlParameter("ID", SqlDbType.VarChar, 4),
                  new SqlParameter("Deptids", SqlDbType.VarChar, 8),
                  new SqlParameter("Wardid", SqlDbType.VarChar, 8),
                  new SqlParameter("QueryType", SqlDbType.Int)};
                sqlParams[0].Value = m_App.User.Id; // 
                sqlParams[1].Value = deptId; // ���Ҵ���
                sqlParams[2].Value = wardId; // ��������
                sqlParams[3].Value = (int)queryType; // 
                DataTable dataTable = m_SqlHelper.ExecuteDataTable("usp_queryinwardpatientsout", sqlParams, CommandType.StoredProcedure);
                dataTable.TableName = "����һ����λ��Ϣ";
                return dataTable;

            }
            catch (Exception ex)
            {
                m_App.CustomMessageBox.MessageShow("����һ�����������ݷ���ӿ�ʧ��", CustomMessageBoxKind.ErrorOk);
                return null;
            }
        }


        internal const string InterfaceError = "���ýӿڷ���ʧ�ܣ�";

        public bool EmrDocIsLock(decimal noOfInpat)
        {
            string sql1 = " select count(*) from recorddetail where recorddetail.noofinpat = {0} AND recorddetail.valid = '1' ";
            string sql2 = " select count(*) from recorddetail where recorddetail.noofinpat = {0} AND recorddetail.islock = '4700' AND recorddetail.valid = '1' ";

            string num1 = m_App.SqlHelper.ExecuteScalar(string.Format(sql1, noOfInpat), CommandType.Text).ToString();
            string num2 = m_App.SqlHelper.ExecuteScalar(string.Format(sql2, noOfInpat), CommandType.Text).ToString();

            if (Convert.ToInt32(num1) > 0 && Convert.ToInt32(num2) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region IDisposable ��Ա

        /// <summary>
        /// ���
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// ͨ������ҽʦ�õ��������������
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns></returns>
        public DataTable GetEmrPoint(string doctorID)
        {
            SqlParameter[] sqlParams = new SqlParameter[] { 
                  new SqlParameter("doctorID", SqlDbType.VarChar, 8)
            };
            sqlParams[0].Value = doctorID;
            DataTable dataTable = m_SqlHelper.ExecuteDataTable("EMRQCMANAGER.usp_GetPointByDoctorID", sqlParams, CommandType.StoredProcedure);
            return dataTable;
        }

        /// <summary>
        /// ������Ժ����
        /// </summary>
        public static DataTable AllPats
        {
            get
            {
                if (_allPats == null)
                {
                    _allPats = m_SqlHelper.ExecuteDataTable(sql_Query);
                }
                return _allPats;
            }
        }

        private static DataTable _allPats;

        const string sql_Query = @" select a.patid     patid,
                                            a.noofinpat,
                                            a.NAME      patname,
                                            a.PY,a.WB,
                                            a.sexid     sex,
                                            a.agestr    agestr,
                                            b.NAME      sexname,
                                            a.outbed    bedid,
                                            trim(c.Name)      admitdeptname,
                                            trim(d.name)      outdeptname,
                                            a.admitdate,
                                            a.inwarddate
                                        from inpatient a
                                        LEFT JOIN dictionary_detail b
                                        ON b.detailid = a.sexid
                                        AND b.categoryid = '3'
                                        LEFT JOIN department c
                                        ON a.outhosdept = c.id
                                        LEFT JOIN department d
                                        ON a.admitdept = d.id
                                        where a.status in (1500, 1501, 1502, 1503)
                                        order by a.admitdate,a.inwarddate desc";

        /// <summary>
        /// Ӥ��ά�����棬ȡ�õ�ǰ���˵�Ӥ����Ϣ
        /// add  by  ywk 
        /// </summary>
        /// <param name="NOOFINPAT"></param>
        /// <returns></returns>
        public DataTable GetBabyInfo(string NOOFINPAT)
        {
            string sql = string.Format(@"select b.noofinpat as Mnoofinpat,a.name,a.sexid,a.age,a.agestr,a.birth,a.noofinpat,b.name as mname,t.name as sexname from inpatient a
            join inpatient b on a.mother=b.noofinpat join dictionary_detail t
            on a.sexid=t.detailid 
            where a.mother='{0}' and  t.categoryid = '3' order by a.birth desc ", NOOFINPAT);
            return m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
        }
        /// <summary>
        /// �趨Ӥ���еı�����������༭���������� ��
        /// </summary>
        /// <param name="m_patientInfo"></param>
        /// <param name="edittype"></param>
        public void SaveData(PatientEntity m_patientInfo, string edittype, int noofipat)
        {
            SqlParameter parNoofInpat = new SqlParameter("@Noofinpat", SqlDbType.Int, 14);
            parNoofInpat.Value = noofipat;
            SqlParameter parPatNoOfHis = new SqlParameter("@patnoofhis", SqlDbType.VarChar, 14);
            parPatNoOfHis.Value = m_patientInfo.PatNoOfHis;
            SqlParameter parNoofclinic = new SqlParameter("@Noofclinic", SqlDbType.VarChar, 32);
            parNoofclinic.Value = m_patientInfo.NoOfcClinic;
            SqlParameter parNoofrecord = new SqlParameter("@Noofrecord", SqlDbType.VarChar, 32);
            parNoofrecord.Value = m_patientInfo.NoOfRecord;
            SqlParameter parpatid = new SqlParameter("@patid", SqlDbType.VarChar, 32);
            parpatid.Value = m_patientInfo.PatID;
            SqlParameter parInnerpix = new SqlParameter("@Innerpix", SqlDbType.VarChar, 32);
            parInnerpix.Value = m_patientInfo.INNERPIX;
            SqlParameter paroutpix = new SqlParameter("@outpix", SqlDbType.VarChar, 32);
            paroutpix.Value = m_patientInfo.OUTPIX;
            SqlParameter parName = new SqlParameter("@Name", SqlDbType.VarChar, 32);
            parName.Value = m_patientInfo.Name;
            SqlParameter parpy = new SqlParameter("@py", SqlDbType.VarChar, 8);
            parpy.Value = m_patientInfo.PY;
            SqlParameter parwb = new SqlParameter("@wb", SqlDbType.VarChar, 8);
            parwb.Value = m_patientInfo.WB;
            SqlParameter parPayid = new SqlParameter("@payid", SqlDbType.VarChar, 4);
            parPayid.Value = m_patientInfo.PayID;
            SqlParameter parORIGIN = new SqlParameter("@ORIGIN", SqlDbType.VarChar, 4);
            parORIGIN.Value = m_patientInfo.ORIGIN;
            SqlParameter parInCount = new SqlParameter("@InCount", SqlDbType.Int);
            parInCount.Value = m_patientInfo.InCount;
            SqlParameter parSexID = new SqlParameter("@sexid", SqlDbType.VarChar, 4);
            parSexID.Value = m_patientInfo.SexID;
            SqlParameter parBirth = new SqlParameter("@Birth", SqlDbType.VarChar, 19);
            parBirth.Value = m_patientInfo.Birth;
            SqlParameter parAge = new SqlParameter("@Age", SqlDbType.Int);
            parAge.Value = m_patientInfo.Age;
            SqlParameter parAgeStr = new SqlParameter("@AgeStr", SqlDbType.VarChar, 16);
            parAgeStr.Value = m_patientInfo.AgeStr;
            SqlParameter parIDNO = new SqlParameter("@IDNO", SqlDbType.VarChar, 18);
            parIDNO.Value = m_patientInfo.IDNO;
            SqlParameter parMarital = new SqlParameter("@Marital", SqlDbType.VarChar, 12);
            parMarital.Value = m_patientInfo.Marital;
            SqlParameter parJobID = new SqlParameter("@JobID", SqlDbType.VarChar, 4);
            parJobID.Value = m_patientInfo.JobID;
            SqlParameter parCSDProvinceID = new SqlParameter("@CSDProvinceID", SqlDbType.VarChar, 14);
            parCSDProvinceID.Value = m_patientInfo.CSD_ProvinceID;
            SqlParameter parCSDCityID = new SqlParameter("@CSDCityID", SqlDbType.VarChar, 14);
            parCSDCityID.Value = m_patientInfo.CSD_CityID;
            SqlParameter parCSDDistrictID = new SqlParameter("@CSDDistrictID", SqlDbType.VarChar, 14);
            parCSDDistrictID.Value = m_patientInfo.CSD_DistrictID;
            SqlParameter parNationID = new SqlParameter("@NationID", SqlDbType.VarChar, 14);
            parNationID.Value = m_patientInfo.NationID;
            SqlParameter parNationalityID = new SqlParameter("@NationalityID", SqlDbType.VarChar, 14);
            parNationalityID.Value = m_patientInfo.NationalityID;
            SqlParameter parJGProvinceID = new SqlParameter("@JGProvinceID", SqlDbType.VarChar, 14);
            parJGProvinceID.Value = m_patientInfo.JG_ProvinceID;
            SqlParameter parJGCityID = new SqlParameter("@JGCityID", SqlDbType.VarChar, 14);
            parJGCityID.Value = m_patientInfo.JG_CityID;
            SqlParameter parOrganization = new SqlParameter("@Organization", SqlDbType.VarChar, 64);
            parOrganization.Value = m_patientInfo.Organization;
            SqlParameter parOfficePlace = new SqlParameter("@OfficePlace", SqlDbType.VarChar, 64);
            parOfficePlace.Value = m_patientInfo.OfficePlace;
            SqlParameter parOfficeTEL = new SqlParameter("@OfficeTEL", SqlDbType.VarChar, 16);
            parOfficeTEL.Value = m_patientInfo.OfficeTEL;
            SqlParameter parOfficePost = new SqlParameter("@OfficePost", SqlDbType.VarChar, 16);
            parOfficePost.Value = m_patientInfo.OfficePost;
            SqlParameter parHKDZProvinceID = new SqlParameter("@HKDZProvinceID", SqlDbType.VarChar, 14);
            parHKDZProvinceID.Value = m_patientInfo.HKDZ_ProvinceID;
            SqlParameter parHKDZCityID = new SqlParameter("@HKDZCityID", SqlDbType.VarChar, 14);
            parHKDZCityID.Value = m_patientInfo.HKDZ_CityID;
            SqlParameter parHKDZDistrictID = new SqlParameter("@HKDZDistrictID", SqlDbType.VarChar, 14);
            parHKDZDistrictID.Value = m_patientInfo.HKDZ_DistrictID;
            SqlParameter parNATIVEPOST = new SqlParameter("@NATIVEPOST", SqlDbType.VarChar, 16);
            parNATIVEPOST.Value = m_patientInfo.NATIVEPOST;
            SqlParameter parNATIVETEL = new SqlParameter("@NATIVETEL", SqlDbType.VarChar, 16);
            parNATIVETEL.Value = m_patientInfo.NATIVETEL;
            SqlParameter parNATIVEADDRESS = new SqlParameter("@NATIVEADDRESS", SqlDbType.VarChar, 64);
            parNATIVEADDRESS.Value = m_patientInfo.NATIVEADDRESS;
            SqlParameter parADDRESS = new SqlParameter("@ADDRESS", SqlDbType.VarChar, 255);
            parADDRESS.Value = m_patientInfo.ADDRESS;
            SqlParameter parContactPerson = new SqlParameter("@ContactPerson", SqlDbType.VarChar, 32);
            parContactPerson.Value = m_patientInfo.ContactPerson;
            SqlParameter parRelationshipID = new SqlParameter("@RelationshipID", SqlDbType.VarChar, 14);
            parRelationshipID.Value = m_patientInfo.RelationshipID;
            SqlParameter parContactAddress = new SqlParameter("@ContactAddress", SqlDbType.VarChar, 255);
            parContactAddress.Value = m_patientInfo.ContactAddress;
            SqlParameter parContactTEL = new SqlParameter("@ContactTEL", SqlDbType.VarChar, 16);
            parContactTEL.Value = m_patientInfo.ContactTEL;
            SqlParameter parCONTACTOFFICE = new SqlParameter("@CONTACTOFFICE", SqlDbType.VarChar, 64);
            parCONTACTOFFICE.Value = m_patientInfo.CONTACTOFFICE;
            SqlParameter parCONTACTPOST = new SqlParameter("@CONTACTPOST", SqlDbType.VarChar, 16);
            parCONTACTPOST.Value = m_patientInfo.CONTACTPOST;
            SqlParameter parOFFERER = new SqlParameter("@OFFERER", SqlDbType.VarChar, 64);
            parOFFERER.Value = m_patientInfo.OFFERER;
            SqlParameter parSocialCare = new SqlParameter("@SocialCare", SqlDbType.VarChar, 14);
            parSocialCare.Value = m_patientInfo.SocialCare;
            SqlParameter parINSURANCE = new SqlParameter("@INSURANCE", SqlDbType.VarChar, 32);
            parINSURANCE.Value = m_patientInfo.INSURANCE;
            SqlParameter parCARDNO = new SqlParameter("@CARDNO", SqlDbType.VarChar, 32);
            parCARDNO.Value = m_patientInfo.CARDNO;
            SqlParameter parADMITINFO = new SqlParameter("@ADMITINFO", SqlDbType.VarChar, 14);
            parADMITINFO.Value = m_patientInfo.ADMITINFO;
            SqlParameter parAdmitDeptID = new SqlParameter("@AdmitDeptID", SqlDbType.VarChar, 14);
            parAdmitDeptID.Value = m_patientInfo.AdmitDeptID;
            SqlParameter parAdmitWardID = new SqlParameter("@AdmitWardID", SqlDbType.VarChar, 14);
            parAdmitWardID.Value = m_patientInfo.AdmitWardID;
            SqlParameter parADMITBED = new SqlParameter("@ADMITBED", SqlDbType.VarChar, 14);
            parADMITBED.Value = m_patientInfo.ADMITBED;
            SqlParameter parAdmitDate = new SqlParameter("@AdmitDate", SqlDbType.VarChar, 19);
            parAdmitDate.Value = m_patientInfo.AdmitDate;
            SqlParameter parINWARDDATE = new SqlParameter("@INWARDDATE", SqlDbType.VarChar, 19);
            parINWARDDATE.Value = m_patientInfo.INWARDDATE;
            SqlParameter parADMITDIAGNOSIS = new SqlParameter("@ADMITDIAGNOSIS", SqlDbType.VarChar, 14);
            parADMITDIAGNOSIS.Value = m_patientInfo.ADMITDIAGNOSIS;
            SqlParameter parOutWardDate = new SqlParameter("@OutWardDate", SqlDbType.VarChar, 19);
            parOutWardDate.Value = m_patientInfo.OutWardDate;
            SqlParameter parOutHosDeptID = new SqlParameter("@OutHosDeptID", SqlDbType.VarChar, 14);
            parOutHosDeptID.Value = m_patientInfo.OutHosDeptID;
            SqlParameter parOutHosWardID = new SqlParameter("@OutHosWardID", SqlDbType.VarChar, 14);
            parOutHosWardID.Value = m_patientInfo.OutHosWardID;
            SqlParameter parOutBed = new SqlParameter("@OutBed", SqlDbType.VarChar, 14);
            parOutBed.Value = m_patientInfo.OutBed;
            SqlParameter parOUTHOSDATE = new SqlParameter("@OUTHOSDATE", SqlDbType.VarChar, 19);
            parOUTHOSDATE.Value = m_patientInfo.OUTHOSDATE;
            SqlParameter parOUTDIAGNOSIS = new SqlParameter("@OUTDIAGNOSIS", SqlDbType.VarChar, 14);
            parOUTDIAGNOSIS.Value = m_patientInfo.OUTDIAGNOSIS;

            SqlParameter parTOTALDAYS = new SqlParameter("@TOTALDAYS", SqlDbType.Int);
            parTOTALDAYS.Value = m_patientInfo.TOTALDAYS;
            SqlParameter parCLINICDIAGNOSIS = new SqlParameter("@CLINICDIAGNOSIS", SqlDbType.VarChar, 14);
            parCLINICDIAGNOSIS.Value = m_patientInfo.CLINICDIAGNOSIS;
            SqlParameter parSOLARTERMS = new SqlParameter("@SOLARTERMS", SqlDbType.VarChar, 16);
            parSOLARTERMS.Value = m_patientInfo.SOLARTERMS;
            SqlParameter parADMITWAY = new SqlParameter("@ADMITWAY", SqlDbType.VarChar, 14);
            parADMITWAY.Value = m_patientInfo.ADMITWAY;
            SqlParameter parOUTWAY = new SqlParameter("@OUTWAY", SqlDbType.VarChar, 14);
            parOUTWAY.Value = m_patientInfo.OUTWAY;
            SqlParameter parCLINICDOCTOR = new SqlParameter("@CLINICDOCTOR", SqlDbType.VarChar, 14);
            parCLINICDOCTOR.Value = m_patientInfo.CLINICDOCTOR;
            SqlParameter parRESIDENT = new SqlParameter("@RESIDENT", SqlDbType.VarChar, 14);
            parRESIDENT.Value = m_patientInfo.RESIDENT;
            SqlParameter parATTEND = new SqlParameter("@ATTEND", SqlDbType.VarChar, 14);
            parATTEND.Value = m_patientInfo.ATTEND;
            SqlParameter parCHIEF = new SqlParameter("@CHIEF", SqlDbType.VarChar, 14);
            parCHIEF.Value = m_patientInfo.CHIEF;
            SqlParameter parEDU = new SqlParameter("@EDU", SqlDbType.VarChar, 14);
            parEDU.Value = m_patientInfo.EDU;
            SqlParameter parEDUC = new SqlParameter("@EDUC", SqlDbType.Int);
            parEDUC.Value = m_patientInfo.EDUC;
            SqlParameter parRELIGION = new SqlParameter("@RELIGION", SqlDbType.VarChar, 32);
            parRELIGION.Value = m_patientInfo.RELIGION;
            SqlParameter parSTATUS = new SqlParameter("@STATUS", SqlDbType.Int);
            parSTATUS.Value = m_patientInfo.STATUS;
            SqlParameter parCRITICALLEVEL = new SqlParameter("@CRITICALLEVEL", SqlDbType.VarChar, 14);
            parCRITICALLEVEL.Value = m_patientInfo.CRITICALLEVEL;
            SqlParameter parATTENDLEVEL = new SqlParameter("@ATTENDLEVEL", SqlDbType.VarChar, 14);
            parATTENDLEVEL.Value = m_patientInfo.ATTENDLEVEL;
            SqlParameter parEMPHASIS = new SqlParameter("@EMPHASIS", SqlDbType.Int);
            parEMPHASIS.Value = m_patientInfo.EMPHASIS;
            SqlParameter parISBABY = new SqlParameter("@ISBABY", SqlDbType.Int);
            parISBABY.Value = m_patientInfo.ISBABY;
            SqlParameter parMOTHER = new SqlParameter("@MOTHER", SqlDbType.Int);
            parMOTHER.Value = m_patientInfo.MOTHER;
            SqlParameter parMEDICAREID = new SqlParameter("@MEDICAREID", SqlDbType.VarChar, 14);
            parMEDICAREID.Value = m_patientInfo.MEDICAREID;
            SqlParameter parMEDICAREQUOTA = new SqlParameter("@MEDICAREQUOTA", SqlDbType.Int);
            parMEDICAREQUOTA.Value = m_patientInfo.MEDICAREQUOTA;
            SqlParameter parVOUCHERSCODE = new SqlParameter("@VOUCHERSCODE", SqlDbType.VarChar, 14);
            parVOUCHERSCODE.Value = m_patientInfo.VOUCHERSCODE;
            SqlParameter parSTYLE = new SqlParameter("@STYLE", SqlDbType.VarChar, 14);
            parSTYLE.Value = m_patientInfo.STYLE;
            SqlParameter parOPERATOR = new SqlParameter("@OPERATOR", SqlDbType.VarChar, 14);
            parOPERATOR.Value = m_patientInfo.OPERATOR;
            SqlParameter parMEMO = new SqlParameter("@MEMO", SqlDbType.VarChar, 64);
            parMEMO.Value = m_patientInfo.MEMO;
            SqlParameter parCPSTATUS = new SqlParameter("@CPSTATUS", SqlDbType.Int);
            parCPSTATUS.Value = m_patientInfo.CPSTATUS;
            SqlParameter parOUTWARDBED = new SqlParameter("@OUTWARDBED", SqlDbType.Int);
            parOUTWARDBED.Value = m_patientInfo.OUTWARDBED;
            SqlParameter parXZZProvinceID = new SqlParameter("@XZZProvinceID", SqlDbType.VarChar, 14);
            parXZZProvinceID.Value = m_patientInfo.XZZ_ProvinceID;
            SqlParameter parXZZCityID = new SqlParameter("@XZZCityID", SqlDbType.VarChar, 14);
            parXZZCityID.Value = m_patientInfo.XZZ_CityID;
            SqlParameter parXZZDistrictID = new SqlParameter("@XZZDistrictID", SqlDbType.VarChar, 14);
            parXZZDistrictID.Value = m_patientInfo.XZZ_DistrictID;
            SqlParameter parXZZTEL = new SqlParameter("@XZZTEL", SqlDbType.VarChar, 14);
            parXZZTEL.Value = m_patientInfo.XZZ_TEL;
            SqlParameter parXZZPost = new SqlParameter("@XZZPost", SqlDbType.VarChar, 16);
            parXZZPost.Value = m_patientInfo.XZZ_Post;

            SqlParameter parEditType = new SqlParameter("@EditType", SqlDbType.VarChar, 16);
            parEditType.Value = edittype;

            SqlParameter[] paraColl = new SqlParameter[] {parNoofInpat, parPatNoOfHis,parNoofclinic,parNoofrecord,parpatid,
                 parInnerpix, paroutpix ,parName,  parpy,  parwb,  parPayid,  parORIGIN , parInCount ,parSexID , parBirth , 
                 parAge , parAgeStr,  parIDNO , parMarital, parJobID,  parCSDProvinceID,  parCSDCityID,  parCSDDistrictID , 
                 parNationID,   parNationalityID,  parJGProvinceID,  parJGCityID , parOrganization , parOfficePlace, parOfficeTEL,  
                 parOfficePost ,parHKDZProvinceID , parHKDZCityID,  parHKDZDistrictID, parNATIVEPOST , parNATIVETEL , parNATIVEADDRESS, 
                 parADDRESS,  parContactPerson,  parRelationshipID,  parContactAddress,  parContactTEL, parCONTACTOFFICE,  parCONTACTPOST ,
                parOFFERER,  parSocialCare , parINSURANCE , parCARDNO,  parADMITINFO , parAdmitDeptID ,parAdmitWardID ,
                parADMITBED,  parAdmitDate , parINWARDDATE,  parADMITDIAGNOSIS , parOutWardDate, parOutHosDeptID , parOutHosWardID ,
                parOutBed,parOUTHOSDATE,  parOUTDIAGNOSIS,  parTOTALDAYS,  parCLINICDIAGNOSIS, parSOLARTERMS ,
                parADMITWAY ,parOUTWAY, parCLINICDOCTOR,  parRESIDENT,  parATTEND,  parCHIEF, parEDU,  parEDUC,
                 parRELIGION,  parSTATUS , parCRITICALLEVEL, parATTENDLEVEL,  parEMPHASIS , parISBABY, parMOTHER,
                parMEDICAREID , parMEDICAREQUOTA , parVOUCHERSCODE, parSTYLE,  parOPERATOR, parMEMO, parCPSTATUS, parOUTWARDBED ,
                parXZZProvinceID , parXZZCityID,  parXZZDistrictID, parXZZTEL,  parXZZPost ,parEditType
            };

            try
            {
                m_App.SqlHelper.ExecuteNoneQuery("EMRPROC.usp_editBabyinfo", paraColl, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                MessageBox.Show("����" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// ��õ�ǰ������Ժ������Ϣ
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <returns></returns>
        public static DataTable GetCurrentPatient(string dept, string ward)
        {
            SqlParameter[] sqlParam = new SqlParameter[] 
                {
                  new SqlParameter("Deptids", SqlDbType.VarChar, 8),
                  new SqlParameter("Wardid", SqlDbType.VarChar, 8)
                };
            sqlParam[0].Value = dept;
            sqlParam[1].Value = ward;

            DataTable table1 = m_App.SqlHelper.ExecuteDataTable("usp_QueryInwardPatients", sqlParam, CommandType.StoredProcedure);
            return table1;
        }
        /// <summary>
        /// ���ݲ��˵���ҳ��ţ��õ�����Ӥ��������������ʾ����
        /// add by ywk 2012��6��8�� 09:47:53
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetPatsBabyContent(IEmrHost m_app, string noofinpat)
        {
            string Result = string.Empty;//����Ҫ���ص�����
            string sql = string.Format(@"select babycount,name from inpatient where noofinpat='{0}'", noofinpat);
            if (m_App == null)
            {
                m_App = m_app;
            }
            DataTable dt = m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            if (string.IsNullOrEmpty(dt.Rows[0]["babycount"].ToString()))
            {
                Result = dt.Rows[0]["Name"].ToString();

            }
            else
            {
                if (dt.Rows[0]["babycount"].ToString() == "0")
                {
                    Result = dt.Rows[0]["Name"].ToString();
                }
                else
                {
                    Result = dt.Rows[0]["Name"].ToString() + "��" + dt.Rows[0]["babycount"].ToString() + "��Ӥ����";
                }
            }
            return Result;
        }
        /// <summary>
        /// /���ݲ�����ҳ��Ż�������ӵĸ���
        /// add ywk
        /// </summary>
        /// <param name="noofinpat"></param>
        /// <returns></returns>
        public static bool HasBaby(string noofinpat)
        {
            string sql = string.Format(@"select babycount from inpatient where noofinpat='{0}'", noofinpat);
            DataTable dt = m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["babycount"].ToString() == "0")
                {
                    return false;
                }
                if (Int32.Parse(dt.Rows[0]["babycount"].ToString() == "" ?
                    "0" : dt.Rows[0]["babycount"].ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// ����Noofinpat��ò��˺������ӵ���Ϣ
        /// add ywk
        /// </summary>
        /// <param name="NOOfINPAT"></param>
        /// <returns></returns>
        internal static DataTable GetPatAndBaby(string NOOfINPAT)
        {
            string sql = string.Format(@"select a.noofinpat,a.name as patname,a.birth,a.age,a.agestr,a.sexid,
                        b.name as sexname,a.isbaby  from inpatient a    JOIN dictionary_detail b
                        on  b.detailid = a.sexid
                        where b.categoryid = '3' and (a.noofinpat='{0}' or a.mother='{0}') 
                        order by a.isbaby asc,a.birth desc ", NOOfINPAT);
            return m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
        }

        public DataTable GetQCTiXong()
        {
            string departId = m_App.User.CurrentDeptId;
            string doctorId = m_App.User.DoctorId;
            string strSql = string.Format(
@"select p.id rhpointid,p.noofinpat,p.doctorid,p.doctorname,p.problem_desc,p.reducepoint,i.name,qc.id rhqcId  from emr_rhpoint p 
            left join emr_rhqc_table qc  on qc.id=p.rhqc_table_id
            left join  inpatient i on qc.noofinpat=i.noofinpat
            where i.outhosdept='{0}' and p.doctorid='{1}'
            and qc.doctorstate='0'
             and p.valid='1' and stateid in ('8701','8703','8705')", departId, doctorId);
            DataTable dtQC = m_App.SqlHelper.ExecuteDataTable(strSql, CommandType.Text);
            return dtQC;
        }

        public void SetRHQCHasXiuGai(string noofInpat)
        {
            string strsql = string.Format(@" update emr_rhqc_table set doctorstate='1'where noofinpat='{0}'", noofInpat);
            m_App.SqlHelper.ExecuteNoneQuery(strsql, CommandType.Text);
        }


        /// <summary>
        /// �������������˲����б�
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataTable GetThreeLevelCheckList(string deptCode)
        {
            SqlParameter[] sqlParams = new SqlParameter[] { 
                  new SqlParameter("deptid", SqlDbType.VarChar, 8)
            };
            sqlParams[0].Value = deptCode;
            DataTable dataTable = m_SqlHelper.ExecuteDataTable("EMR_DOCTOR_STATION.usp_getthreelevelcheckList", sqlParams, CommandType.StoredProcedure);
            return dataTable;
        }

        /// <summary>
        /// ����סԺ����ID��ȡ��ҳ���
        /// </summary>
        /// <param name="patID"></param>
        /// <returns></returns>
        public string GetNoofInpatByPatID(string patID)
        {
            string noofInpat = "";
            string strsql = string.Format(@" select * from inpatient where patid ='{0}' ", patID);
            DataTable dt = m_App.SqlHelper.ExecuteDataTable(strsql, CommandType.Text);
            if (null != dt && dt.Rows.Count > 0)
            {
                noofInpat = dt.Rows[0]["noofinpat"].ToString();
            }
            return noofInpat;
        }

        /// <summary>
        /// ���ݵ�½�˿���Ȩ�޲�ѯδ�鵵����
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string GetAllUnRebackRecordsByDeptRights(string userid)
        {
            try
            {
                if (string.IsNullOrEmpty(userid))
                {
                    return "";
                }
                string deptStr = string.Empty;
                if (userid != "00")
                {
                    List<string[]> list = DS_BaseService.GetDeptAndWardInRight(userid);
                    if (null != list && list.Count() > 0)
                    {
                        deptStr = DS_Common.CombineSQLStringByList(list.Select(p => p[0]).ToList());
                    }
                }
                //�޸���� ���ݲ��˱��ѯδ�鵵���� ������ݲ�����
                string sqlStr = @"select distinct a.patid, a.noofinpat, a.NAME patname, a.PY, a.WB, a.sexid sex,a.agestr, b.NAME sexname, a.outbed bedid, trim(c.Name) admitdeptname, trim(d.name) outdeptname, substr(a.admitdate, 1, 16) as admitdate,substr(a.inwarddate, 1, 16) as inwarddate
   from inpatient a  LEFT JOIN inpatientchangeinfo i on i.noofinpat = a.noofinpat  LEFT JOIN dictionary_detail b   ON b.detailid = a.sexid and b.categoryid = '3'
  LEFT JOIN department c  ON a.admitdept = c.id
  LEFT JOIN department d ON a.outhosdept = d.id
 where (a.islock=4700 or a.islock=4702 or a.islock is null) ";

                deptStr = string.IsNullOrEmpty(deptStr) ? ("'" + DS_Common.currentUser.CurrentDeptId + "'") : deptStr;
                List<string> jobList = DS_BaseService.GetRolesByUserID(userid);
                if (null == jobList || jobList.Count() == 0 || (!jobList.Contains("66") && !jobList.Contains("111")))
                {
                    sqlStr += " and (i.newdeptid in(" + deptStr + ") or a.outhosdept in(" + deptStr + ")) ";// " and a.outhosdept in(" + (string.IsNullOrEmpty(deptStr) ? YD_Common.currentUser.CurrentDeptId : deptStr) + ") ";
                }
                sqlStr += " order by admitdeptname asc,admitdate desc,inwarddate desc ";

                return sqlStr;
            }
            catch (Exception ex)
            {
                throw new Exception("GetAllUnRebackRecordsByDeptRights" + ex.Message);
            }
        }

    }
}
