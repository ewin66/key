﻿using System;
using System.Data;
using System.Data.SqlClient;
using DrectSoft.Common.Eop;
using DrectSoft.FrameWork.WinForm.Plugin;

namespace DrectSoft.Core.IEMMainPage
{
    public class IemMainPageManger
    {
        private IEmrHost m_app;
        private Inpatient CurrentInpatient;

        DataHelper m_DataHelper = new DataHelper();
        public IemMainPageManger(IEmrHost app, Inpatient _CurrentInpatient)
        {

            m_app = app;
            CurrentInpatient = _CurrentInpatient;
            //CurrentInpatient.ReInitializeAllProperties();
        }


        /// <summary>
        /// 病案首页病患信息
        /// </summary>
        public IemMainPageInfo IemInfo
        {
            get;
            set;
        }

        #region 根据首页序号得到病案首页的信息，并给界面赋值
        /// <summary>
        /// LOAD时获取病案首页信息
        /// </summary>
        public IemMainPageInfo GetIemInfo()
        {
            //首先去Iem_Mainpage_Basicinfo根据首页序号捞取资料，如果没有，则LOAD基本用户信息
            SqlParameter[] paraCollection = new SqlParameter[] { new SqlParameter("@NoOfInpat", CurrentInpatient.NoOfFirstPage) };
            DataSet dataSet = m_app.SqlHelper.ExecuteDataSet("IEM_MAIN_PAGE.usp_getieminfo_2012", paraCollection, CommandType.StoredProcedure);
            IemInfo = new IemMainPageInfo();
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                if (i == 0)
                    GetIemBasInfo(dataSet.Tables[i]);
                else if (i == 1)
                    GetItemDiagInfo(dataSet.Tables[i]);
                else if (i == 2)
                    //GetItemOperInfo(dataSet.Tables[i]);
                    IemInfo.IemOperInfo.Operation_Table = dataSet.Tables[i];
                else if (i == 3)
                    GetItemObsBaby(dataSet.Tables[i]);
                else if (i == 4)//取得病案首页的费用信息 
                    GetItemFeeInfo(dataSet.Tables[i]);
            }

            GetIemOthers(GetIemmainpageNO(CurrentInpatient.NoOfFirstPage));//取得诊断抢救路径管理等其他信息 王冀 2012 12 4
            if (IemInfo.IemBasicInfo.Iem_Mainpage_NO == "")
            {
                IemInfo.IemBasicInfo.PatNoOfHis = CurrentInpatient.NoOfHisFirstPage;
                IemInfo.IemBasicInfo.NoOfInpat = CurrentInpatient.NoOfFirstPage.ToString();
            }

            return IemInfo;
        }
        public Decimal GetIemmainpageNO(decimal NoOfFirstPage)
        {
            try
            {
                string sql = string.Format(@"select IEM_MAINPAGE_NO from iem_mainpage_basicinfo_2012 where noofinpat='{0}' ", NoOfFirstPage);
                DataTable dt = m_app.SqlHelper.ExecuteDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    return 0;
                }
                return Decimal.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 从数据库获取诊断以及抢救情况的值
        /// 王冀 2012 12 3
        /// </summary>
        /// <param name="NoOfFirstPage"></param>
        public void GetIemOthers(decimal NoOfFirstPage)
        {
            string sql = string.Format(@"select t.MAIN_DIAGNOSIS_CURECONDITION,t.DIAGNOSIS_CONDITIONS1,
t.DIAGNOSIS_CONDITIONS2,t.DIAGNOSIS_CONDITIONS3,t.DIAGNOSIS_CONDITIONS4,t.DIAGNOSIS_CONDITIONS5,
t.EMERGENCY_TIMES,t.EMERGENCY_SUCCESSFUL_TIMES,t.CP_STATUS,t.CREAT_USER,t.CREATE_TIME,t.MODIFY_USER,t.MODIFY_TIME,t.IEM_MAINPAGE_OTHER_NO,t.IEM_MAINPAGE_NO    from IEM_MAINPAGE_OTHER_2012 t 
where t.IEM_MAINPAGE_NO={0} and t.VALID=1", NoOfFirstPage);
            DataTable dt = m_app.SqlHelper.ExecuteDataTable(sql);

            if (dt.Rows.Count > 0)
            {
                IemInfo.IemOthers.Main_Diagnosis_Curecondition = dt.Rows[0]["MAIN_DIAGNOSIS_CURECONDITION"].ToString();
                IemInfo.IemOthers.Diagnosis_conditions1 = dt.Rows[0]["DIAGNOSIS_CONDITIONS1"].ToString();
                IemInfo.IemOthers.Diagnosis_conditions2 = dt.Rows[0]["DIAGNOSIS_CONDITIONS2"].ToString();
                IemInfo.IemOthers.Diagnosis_conditions3 = dt.Rows[0]["DIAGNOSIS_CONDITIONS3"].ToString();
                IemInfo.IemOthers.Diagnosis_conditions4 = dt.Rows[0]["DIAGNOSIS_CONDITIONS4"].ToString();
                IemInfo.IemOthers.Diagnosis_conditions5 = dt.Rows[0]["DIAGNOSIS_CONDITIONS5"].ToString();
                IemInfo.IemOthers.Emergency_times = dt.Rows[0]["EMERGENCY_TIMES"].ToString();
                IemInfo.IemOthers.Emergency_Successful_times = dt.Rows[0]["EMERGENCY_SUCCESSFUL_TIMES"].ToString();
                IemInfo.IemOthers.CP_status = dt.Rows[0]["CP_STATUS"].ToString();
                IemInfo.IemOthers.Creat_user = dt.Rows[0]["CREAT_USER"].ToString();
                IemInfo.IemOthers.Create_time = dt.Rows[0]["CREATE_TIME"].ToString();
                IemInfo.IemOthers.Modify_user = dt.Rows[0]["MODIFY_USER"].ToString();
                IemInfo.IemOthers.Modify_time = dt.Rows[0]["MODIFY_TIME"].ToString();
                IemInfo.IemOthers.IEM_MainPage_Other_NO = dt.Rows[0]["IEM_MAINPAGE_OTHER_NO"].ToString();
                IemInfo.IemOthers.IEM_MainPage_NO = dt.Rows[0]["IEM_MAINPAGE_NO"].ToString();
            }

        }
        /// <summary>
        /// 获得病案首页的费用信息
        /// add bu ywk  2012年10月16日 20:24:53
        /// </summary>
        /// <param name="dataTable"></param>
        private void GetItemFeeInfo(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)//有值
            {
                IemInfo.IemFeeInfo.Total = dataTable.Rows[0]["TotalFee"].ToString();//总费用
                IemInfo.IemFeeInfo.BloodFee = dataTable.Rows[0]["BloodFee"].ToString();//血液和血液制品类 血费
                IemInfo.IemFeeInfo.BLZDF = dataTable.Rows[0]["BLZDFee"].ToString();//诊断类 病理诊断费
                IemInfo.IemFeeInfo.Care = dataTable.Rows[0]["NurseFee"].ToString();//护理费
                IemInfo.IemFeeInfo.CMedical = dataTable.Rows[0]["ZCaoYFFee"].ToString();//中药类 中草药费
                IemInfo.IemFeeInfo.CPMedical = dataTable.Rows[0]["ZCYFFee"].ToString();// 中药类 中成药费
                IemInfo.IemFeeInfo.FSSZLF = dataTable.Rows[0]["FSSZLItemFee"].ToString();// 治疗类 非手术治疗项目费
                IemInfo.IemFeeInfo.JCYYCXCLF = dataTable.Rows[0]["JCYYCXYYCLFFee"].ToString();// 材类 检查用一次性医用材料费
                IemInfo.IemFeeInfo.KFF = dataTable.Rows[0]["KFFee"].ToString();//  康复类 康复费
                IemInfo.IemFeeInfo.KJYWF = dataTable.Rows[0]["KJYWFee"].ToString();//  西药类 抗菌药物费用
                IemInfo.IemFeeInfo.LCWLZLF = dataTable.Rows[0]["LCWLZLFee"].ToString();//  治疗类 临床物理治疗费
                IemInfo.IemFeeInfo.LCZDF = dataTable.Rows[0]["LCZDItemFee"].ToString();//  诊断类 临床诊断项目费
                IemInfo.IemFeeInfo.NXYZLZPF = dataTable.Rows[0]["NXYZLZPFFee"].ToString();//  血液和血液制品类 凝血因子类制品费
                IemInfo.IemFeeInfo.OtherFee = dataTable.Rows[0]["QTFee"].ToString();//  其他类：（24）其他费
                IemInfo.IemFeeInfo.OwnFee = dataTable.Rows[0]["OwnerFee"].ToString();//  自付金额
                IemInfo.IemFeeInfo.QDBLZPF = dataTable.Rows[0]["QDBLZPFFee"].ToString();//   球蛋白类制品费
                IemInfo.IemFeeInfo.SSYYCXCLF = dataTable.Rows[0]["SSYYCXYYCLFFee"].ToString();//   手术用一次性医用材料费
                IemInfo.IemFeeInfo.SSZLF = dataTable.Rows[0]["OperMedFee"].ToString();//   手术治疗费
                IemInfo.IemFeeInfo.SYSZDF = dataTable.Rows[0]["SYSZDFee"].ToString();//   实验室诊断费
                IemInfo.IemFeeInfo.XBYZLZPF = dataTable.Rows[0]["XBYZLZPFFee"].ToString();//   细胞因子类制品费
                IemInfo.IemFeeInfo.XDBLZPF = dataTable.Rows[0]["BDBLZPFFee"].ToString();//   白蛋白类制品费
                IemInfo.IemFeeInfo.XYF = dataTable.Rows[0]["XYMedFee"].ToString();//    西药费
                IemInfo.IemFeeInfo.YBYLFY = dataTable.Rows[0]["YbMedServFee"].ToString();//    一般医疗服务费
                IemInfo.IemFeeInfo.YBZLFY = dataTable.Rows[0]["YbMedOperFee"].ToString();//    一般治疗操作费
                IemInfo.IemFeeInfo.YXXZDF = dataTable.Rows[0]["YXXZDFee"].ToString();//  类 影像学诊断费
                IemInfo.IemFeeInfo.ZHQTFY = dataTable.Rows[0]["OtherInfo"].ToString();//  ZHQTFY
                IemInfo.IemFeeInfo.ZLYYCXCLF = dataTable.Rows[0]["ZLYYCXYYCLFFee"].ToString();//  治疗用一次性医用材料费
                IemInfo.IemFeeInfo.ZYZLF = dataTable.Rows[0]["ZYZLFee"].ToString();//  中医治疗费
                IemInfo.IemFeeInfo.MZF = dataTable.Rows[0]["MAZUIFEE"].ToString();// 麻醉费
                IemInfo.IemFeeInfo.SSF = dataTable.Rows[0]["ShouShuFee"].ToString();//  手术费
            }
        }

        #region 获取配置表中信息
        /// <summary>
        /// 得到配置信息  ywk 2012年6月27日 14:37:37
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfigValueByKey(string key)
        {
            string sql1 = " select * from appcfg where configkey = '" + key + "'  ";
            DataTable dt = m_app.SqlHelper.ExecuteDataTable(sql1, CommandType.Text);
            string config = string.Empty;
            if (dt.Rows.Count > 0)
            {
                config = dt.Rows[0]["value"].ToString();
            }
            return config;
        }
        #endregion
        #region 获取病案首页信息
        /// <summary>
        /// 基本信息
        /// </summary>
        /// <param name="dataTable"></param>
        private void GetIemBasInfo(DataTable dataTable)
        {
            #region 原来的取得首页默认空值的实体数据
            //PaintDefaultInfo paintDefInfo = new PaintDefaultInfo();
            //paintDefInfo.P_TypeNum = "1";//赋给默认值1
            //string sqldefa = string.Format(@" select t.typenum,t.tname,t.tvalue from iem_mainpage_Default t where t.typenum='{0}' ", paintDefInfo.P_TypeNum);
            //DataTable dtDefault = m_app.SqlHelper.ExecuteDataTable(sqldefa, CommandType.Text);

            //DataRow[] dtrowpro = dtDefault.Select("  tname='ProviceID' ");

            //DataRow[] dtrowcity = dtDefault.Select("  tname='CityID' ");

            //DataRow[] dtrowdins = dtDefault.Select("  tname='DistrictID' ");

            //DataRow[] dtrowpost = dtDefault.Select("  tname='PostNum' ");

            //DataRow[] dtrowturndeptid = dtDefault.Select("  tname='TurnInDeptID' ");//新增转入科别编号 默认值 add  by ywk 2012年3月27日10:36:26

            //DataRow[] dtrowturndeptname = dtDefault.Select("  tname='TurnInDeptName' ");//新增转入科别名称 默认值 add  by ywk 2012年3月27日10:46:11
            ////省
            //if (dtrowpro.Length > 0)
            //{
            //    string sqlpro = string.Format(@" select a.provinceid,a.provincename from s_province  a where a.provinceid='{0}' ", dtrowpro[0]["Tvalue"]);
            //    DataTable dtProvice = m_app.SqlHelper.ExecuteDataTable(sqlpro, CommandType.Text);
            //    paintDefInfo.ProviceID = dtProvice.Rows[0]["provinceid"].ToString();//省份ID
            //    paintDefInfo.Provicename = dtProvice.Rows[0]["provincename"].ToString();//省份名称
            //}
            ////市
            //if (dtrowcity.Length > 0)
            //{
            //    string sqlcity = string.Format(@" select b.cityid,b.cityname from s_city  b where b.cityid='{0}' ", dtrowcity[0]["Tvalue"]);
            //    DataTable dtCity = m_app.SqlHelper.ExecuteDataTable(sqlcity, CommandType.Text);
            //    paintDefInfo.CityID = dtCity.Rows[0]["cityid"].ToString();//城市ID
            //    paintDefInfo.Cityname = dtCity.Rows[0]["cityname"].ToString();//城市名称
            //}
            ////县
            //if (dtrowdins.Length > 0)
            //{
            //    string sqldis = string.Format(@" select c.districtid,c.districtname from s_district  c where c.districtid='{0}' ", dtrowdins[0]["Tvalue"]);
            //    DataTable dtDist = m_app.SqlHelper.ExecuteDataTable(sqldis, CommandType.Text);
            //    paintDefInfo.DintID = dtDist.Rows[0]["districtid"].ToString();//县ID
            //    paintDefInfo.Dintname = dtDist.Rows[0]["districtname"].ToString();//县名称
            //}
            ////邮编
            //if (dtrowpost.Length > 0)
            //{
            //    paintDefInfo.PostID = dtrowpost[0]["Tvalue"].ToString();//邮编 
            //}
            ////转入科别编号
            //if (dtrowturndeptid.Length > 0)
            //{
            //    paintDefInfo.TurnInDeptID = dtrowturndeptid[0]["Tvalue"].ToString();//转入科别编号
            //}
            ////转入科别名称
            //if (dtrowturndeptname.Length > 0)
            //{
            //    paintDefInfo.TurnInDeptName = dtrowturndeptname[0]["Tvalue"].ToString();//转入科别名称 
            //}
            #endregion
            #region 现在的取默认值
            //取默认值从inpatient_default表中取得
            //edit by ywk 2012年5月16日 14:35:14
            Iem_Default_PaientInfo DefaultInfo = new Iem_Default_PaientInfo();//存放默认值的实体
            SqlParameter[] sqlParam = new SqlParameter[] { };
            DataTable DefaultData = m_app.SqlHelper.ExecuteDataTable("iem_main_page.usp_GetDefaultInpatient", sqlParam, CommandType.StoredProcedure);//取出默认值的数据
            if (DefaultData.Rows.Count > 0)
            {
                DefaultInfo.PatNoOfHis = DefaultData.Rows[0]["PatNoOfHis"].ToString();
                DefaultInfo.NoOfInpat = DefaultData.Rows[0]["NoOfInpat"].ToString();
                DefaultInfo.InCount = DefaultData.Rows[0]["InCount"].ToString();
                DefaultInfo.SocialCare = DefaultData.Rows[0]["socialcare"].ToString();

                DefaultInfo.PayID = DefaultData.Rows[0]["PayID"].ToString();
                DefaultInfo.PayName = DefaultData.Rows[0]["PayName"].ToString();
                DefaultInfo.Name = DefaultData.Rows[0]["Name"].ToString();
                DefaultInfo.SexID = DefaultData.Rows[0]["SexID"].ToString();
                //DefaultInfo.Birth = DefaultData.Rows[0]["Birth"].ToString().Trim();

                DefaultInfo.Marital = DefaultData.Rows[0]["Marital"].ToString();
                DefaultInfo.JobID = DefaultData.Rows[0]["JobID"].ToString();
                DefaultInfo.JobName = DefaultData.Rows[0]["JobName"].ToString();

                //出生地
                DefaultInfo.CSD_ProvinceID = DefaultData.Rows[0]["PROVINCEID"].ToString();
                DefaultInfo.CSD_CityID = DefaultData.Rows[0]["COUNTYID"].ToString();
                DefaultInfo.CSD_DistrictID = DefaultData.Rows[0]["DISTRICTID"].ToString();
                DefaultInfo.CSD_ProvinceName = DefaultData.Rows[0]["csd_provincename"].ToString();
                DefaultInfo.CSD_CityName = DefaultData.Rows[0]["csd_cityname"].ToString();
                DefaultInfo.CSD_DistrictName = DefaultData.Rows[0]["csd_districtname"].ToString();

                //现住地址
                DefaultInfo.XZZ_ProvinceID = DefaultData.Rows[0]["XZZPROVICEID"].ToString();
                DefaultInfo.XZZ_CityID = DefaultData.Rows[0]["XZZCITYID"].ToString();
                DefaultInfo.XZZ_DistrictID = DefaultData.Rows[0]["XZZDISTRICTID"].ToString();
                DefaultInfo.XZZ_ProvinceName = DefaultData.Rows[0]["xzz_provincename"].ToString();
                DefaultInfo.XZZ_CityName = DefaultData.Rows[0]["xzz_cityname"].ToString();
                DefaultInfo.XZZ_DistrictName = DefaultData.Rows[0]["xzz_districtname"].ToString();
                DefaultInfo.XZZ_TEL = DefaultData.Rows[0]["XZZTEL"].ToString();
                DefaultInfo.XZZ_Post = DefaultData.Rows[0]["XZZPOST"].ToString();

                //户口地址
                DefaultInfo.HKDZ_ProvinceID = DefaultData.Rows[0]["HKDZPROVICEID"].ToString();
                DefaultInfo.HKDZ_CityID = DefaultData.Rows[0]["HKZDCITYID"].ToString();
                DefaultInfo.HKDZ_DistrictID = DefaultData.Rows[0]["HKZDDISTRICTID"].ToString();
                DefaultInfo.HKDZ_ProvinceName = DefaultData.Rows[0]["hkdz_provincename"].ToString();
                DefaultInfo.HKDZ_CityName = DefaultData.Rows[0]["hkdz_cityname"].ToString();
                DefaultInfo.HKDZ_DistrictName = DefaultData.Rows[0]["hkdz_districtname"].ToString();
                DefaultInfo.HKDZ_Post = DefaultData.Rows[0]["NATIVEPOST"].ToString();

                //籍贯
                DefaultInfo.JG_ProvinceID = DefaultData.Rows[0]["NATIVEPLACE_P"].ToString();
                DefaultInfo.JG_CityID = DefaultData.Rows[0]["NATIVEPLACE_C"].ToString();
                DefaultInfo.JG_ProvinceName = DefaultData.Rows[0]["jg_provincename"].ToString();
                DefaultInfo.JG_CityName = DefaultData.Rows[0]["jg_cityname"].ToString();


                DefaultInfo.NationID = DefaultData.Rows[0]["NationID"].ToString();
                DefaultInfo.NationName = DefaultData.Rows[0]["nationame"].ToString();
                DefaultInfo.NationalityID = DefaultData.Rows[0]["NationalityID"].ToString();
                DefaultInfo.NationalityName = DefaultData.Rows[0]["nationaltyname"].ToString();
                DefaultInfo.IDNO = DefaultData.Rows[0]["IDNO"].ToString();
                DefaultInfo.Organization = DefaultData.Rows[0]["Organization"].ToString();
                DefaultInfo.OfficePlace = DefaultData.Rows[0]["OfficePlace"].ToString();
                DefaultInfo.OfficeTEL = DefaultData.Rows[0]["OfficeTEL"].ToString();

                DefaultInfo.OfficePost = DefaultData.Rows[0]["OfficePost"].ToString();
                DefaultInfo.ContactPerson = DefaultData.Rows[0]["ContactPerson"].ToString();

                DefaultInfo.RelationshipID = DefaultData.Rows[0]["relationship"].ToString();
                DefaultInfo.RelationshipName = DefaultData.Rows[0]["relationshipname"].ToString();
                DefaultInfo.ContactAddress = DefaultData.Rows[0]["ContactAddress"].ToString();
                DefaultInfo.ContactTEL = DefaultData.Rows[0]["ContactTEL"].ToString();
                DefaultInfo.AdmitDate = DefaultData.Rows[0]["AdmitDate"].ToString();

                DefaultInfo.AdmitDeptID = DefaultData.Rows[0]["AdmitDept"].ToString();
                DefaultInfo.AdmitWardID = DefaultData.Rows[0]["AdmitWard"].ToString();

                DefaultInfo.Trans_AdmitDeptID = "default";

                DefaultInfo.Trans_AdmitDeptName = "无";

                DefaultInfo.OutWardDate = DefaultData.Rows[0]["OutHosDate"].ToString();//ywk  2012年5月9日23:01:57
                DefaultInfo.OutHosDeptID = DefaultData.Rows[0]["OutHosDept"].ToString();
                DefaultInfo.OutHosWardID = DefaultData.Rows[0]["OutHosWard"].ToString();
                DefaultInfo.ActualDays = DefaultData.Rows[0]["TOTALDAYS"].ToString();
                DefaultInfo.AdmitWay = DefaultData.Rows[0]["AdmitWay"].ToString();

                DefaultInfo.OutDiagID = DefaultData.Rows[0]["CLINICDIAGNOSIS"].ToString();
                DefaultInfo.OutDiagName = DefaultData.Rows[0]["OutDiagName"].ToString();

                //处理几个地址名称  （出生地  籍贯 现住址  户口地址） add by ywk 
                DefaultInfo.CSDAddress = DefaultData.Rows[0]["CSDAddress"].ToString();
                DefaultInfo.JGAddress = DefaultData.Rows[0]["JGAddress"].ToString();
                DefaultInfo.XZZAddress = DefaultData.Rows[0]["XZZAddress"].ToString();
                DefaultInfo.HKZZAddress = DefaultData.Rows[0]["HKAddress"].ToString();
            }
            else
            {
                DefaultInfo.PatNoOfHis = "";
                DefaultInfo.NoOfInpat = "";
                DefaultInfo.InCount = "";
                DefaultInfo.SocialCare = "";

                DefaultInfo.PayID = "";
                DefaultInfo.Name = "";
                DefaultInfo.SexID = "";
                DefaultInfo.Birth = "";

                DefaultInfo.Marital = "";
                DefaultInfo.JobID = "";

                //出生地
                DefaultInfo.CSD_ProvinceID = "";
                DefaultInfo.CSD_CityID = "";
                DefaultInfo.CSD_DistrictID = "";
                DefaultInfo.CSD_ProvinceName = "";
                DefaultInfo.CSD_CityName = "";
                DefaultInfo.CSD_DistrictName = "";

                //现住地址
                DefaultInfo.XZZ_ProvinceID = "";
                DefaultInfo.XZZ_CityID = "";
                DefaultInfo.XZZ_DistrictID = "";
                DefaultInfo.XZZ_ProvinceName = "";
                DefaultInfo.XZZ_CityName = "";
                DefaultInfo.XZZ_DistrictName = "";
                DefaultInfo.XZZ_TEL = "";
                DefaultInfo.XZZ_Post = "";

                //户口地址
                DefaultInfo.HKDZ_ProvinceID = "";
                DefaultInfo.HKDZ_CityID = "";
                DefaultInfo.HKDZ_DistrictID = "";
                DefaultInfo.HKDZ_ProvinceName = "";
                DefaultInfo.HKDZ_CityName = "";
                DefaultInfo.HKDZ_DistrictName = "";
                DefaultInfo.HKDZ_Post = "";

                //籍贯
                DefaultInfo.JG_ProvinceID = "";
                DefaultInfo.JG_CityID = "";
                DefaultInfo.JG_ProvinceName = "";
                DefaultInfo.JG_CityName = "";


                DefaultInfo.NationID = "";
                DefaultInfo.NationName = "";
                DefaultInfo.NationalityID = "";
                DefaultInfo.NationalityName = "";
                DefaultInfo.IDNO = "";
                DefaultInfo.Organization = "";
                DefaultInfo.OfficePlace = "";
                DefaultInfo.OfficeTEL = "";

                DefaultInfo.OfficePost = "";
                DefaultInfo.ContactPerson = "";

                DefaultInfo.RelationshipID = "";
                DefaultInfo.RelationshipName = "";
                DefaultInfo.ContactAddress = "";
                DefaultInfo.ContactTEL = "";
                DefaultInfo.AdmitDate = "";

                DefaultInfo.AdmitDeptID = "";
                DefaultInfo.AdmitWardID = "";
                DefaultInfo.AdmitWay = "";
                DefaultInfo.Trans_AdmitDeptID = "";

                DefaultInfo.Trans_AdmitDeptName = "";

                DefaultInfo.OutWardDate = "";//ywk  2012年5月9日23:01:57
                DefaultInfo.OutHosDeptID = "";
                DefaultInfo.OutHosWardID = "";
                DefaultInfo.ActualDays = "";

                DefaultInfo.OutDiagID = "";
                DefaultInfo.OutDiagName = "";

                //处理几个地址名称 （出生地  籍贯 现住址  户口地址） add by ywk 
                DefaultInfo.CSDAddress = "";
                DefaultInfo.JGAddress = "";
                DefaultInfo.XZZAddress = "";
                DefaultInfo.HKZZAddress = "";

                DefaultInfo.IsBaby = "";
                DefaultInfo.Mother = "";
            }
            #endregion

            //上面从默认值表里面取得的数据，现在改为如果Inpatient表里面有相应的字段，则从Inpatient表里面加载数据
            //此处将从Inpatient表里读到的数据，可加到上面的默认数据的实体中 add by ywk 2012年6月15日 08:54:13 

            SqlParameter[] sqlParam1 = new SqlParameter[] { new SqlParameter("@NoOfInpat", CurrentInpatient.NoOfFirstPage) };
            DataTable PatData = m_app.SqlHelper.ExecuteDataTable("iem_main_page.usp_GetInpatientByNo", sqlParam1, CommandType.StoredProcedure);//取出默认值的数据
            if (PatData.Rows.Count > 0)
            {
                DefaultInfo.PatNoOfHis = PatData.Rows[0]["PatNoOfHis"].ToString() == "" ? DefaultInfo.PatNoOfHis : PatData.Rows[0]["PatNoOfHis"].ToString();
                DefaultInfo.NoOfInpat = PatData.Rows[0]["NoOfInpat"].ToString() == "" ? DefaultInfo.NoOfInpat : PatData.Rows[0]["NoOfInpat"].ToString();
                DefaultInfo.InCount = PatData.Rows[0]["InCount"].ToString() == "" ? DefaultInfo.InCount : PatData.Rows[0]["InCount"].ToString();
                DefaultInfo.SocialCare = PatData.Rows[0]["SocialCare"].ToString() == "" ? DefaultInfo.SocialCare : PatData.Rows[0]["SocialCare"].ToString();

                DefaultInfo.PayID = PatData.Rows[0]["PayID"].ToString() == "" ? DefaultInfo.PayID : PatData.Rows[0]["PayID"].ToString();
                DefaultInfo.Name = PatData.Rows[0]["Name"].ToString() == "" ? DefaultInfo.Name : PatData.Rows[0]["Name"].ToString();
                DefaultInfo.SexID = PatData.Rows[0]["SexID"].ToString() == "" ? DefaultInfo.SexID : PatData.Rows[0]["SexID"].ToString();
                DefaultInfo.Birth = PatData.Rows[0]["Birth"].ToString() == "" ? DefaultInfo.Birth : PatData.Rows[0]["Birth"].ToString();

                DefaultInfo.Marital = PatData.Rows[0]["Marital"].ToString() == "" ? DefaultInfo.Marital : PatData.Rows[0]["Marital"].ToString();
                DefaultInfo.JobID = PatData.Rows[0]["JobID"].ToString() == "" ? DefaultInfo.JobID : PatData.Rows[0]["JobID"].ToString();
                DefaultInfo.JobName = PatData.Rows[0]["JobName"].ToString() == "" ? DefaultInfo.JobName : PatData.Rows[0]["JobName"].ToString();
                //出生地
                DefaultInfo.CSD_ProvinceID = PatData.Rows[0]["PROVINCEID"].ToString() == "" ? DefaultInfo.CSD_ProvinceID : PatData.Rows[0]["PROVINCEID"].ToString();
                DefaultInfo.CSD_CityID = PatData.Rows[0]["COUNTYID"].ToString() == "" ? DefaultInfo.CSD_CityID : PatData.Rows[0]["COUNTYID"].ToString();
                DefaultInfo.CSD_DistrictID = PatData.Rows[0]["DISTRICTID"].ToString() == "" ? DefaultInfo.CSD_DistrictID : PatData.Rows[0]["DISTRICTID"].ToString();
                DefaultInfo.CSD_ProvinceName = PatData.Rows[0]["CSD_ProvinceName"].ToString() == "" ? DefaultInfo.CSD_ProvinceName : PatData.Rows[0]["CSD_ProvinceName"].ToString();
                DefaultInfo.CSD_CityName = PatData.Rows[0]["CSD_CityName"].ToString() == "" ? DefaultInfo.CSD_CityName : PatData.Rows[0]["CSD_CityName"].ToString();
                DefaultInfo.CSD_DistrictName = PatData.Rows[0]["CSD_DistrictName"].ToString() == "" ? DefaultInfo.CSD_DistrictName : PatData.Rows[0]["CSD_DistrictName"].ToString();

                //现住地址
                DefaultInfo.XZZ_ProvinceID = PatData.Rows[0]["XZZPROVICEID"].ToString() == "" ? DefaultInfo.XZZ_ProvinceID : PatData.Rows[0]["XZZPROVICEID"].ToString();
                DefaultInfo.XZZ_CityID = PatData.Rows[0]["XZZCITYID"].ToString() == "" ? DefaultInfo.XZZ_CityID : PatData.Rows[0]["XZZCITYID"].ToString();
                DefaultInfo.XZZ_DistrictID = PatData.Rows[0]["XZZDISTRICTID"].ToString() == "" ? DefaultInfo.XZZ_DistrictID : PatData.Rows[0]["XZZDISTRICTID"].ToString();
                DefaultInfo.XZZ_ProvinceName = PatData.Rows[0]["XZZ_ProvinceName"].ToString() == "" ? DefaultInfo.XZZ_ProvinceName : PatData.Rows[0]["XZZ_ProvinceName"].ToString();
                DefaultInfo.XZZ_CityName = PatData.Rows[0]["XZZ_CityName"].ToString() == "" ? DefaultInfo.XZZ_CityName : PatData.Rows[0]["XZZ_CityName"].ToString();
                DefaultInfo.XZZ_DistrictName = PatData.Rows[0]["XZZ_DistrictName"].ToString() == "" ? DefaultInfo.XZZ_DistrictName : PatData.Rows[0]["XZZ_DistrictName"].ToString();
                DefaultInfo.XZZ_TEL = PatData.Rows[0]["XZZTEL"].ToString() == "" ? DefaultInfo.XZZ_TEL : PatData.Rows[0]["XZZTEL"].ToString();
                DefaultInfo.XZZ_Post = PatData.Rows[0]["XZZPOST"].ToString() == "" ? DefaultInfo.XZZ_Post : PatData.Rows[0]["XZZPOST"].ToString();

                //户口地址
                DefaultInfo.HKDZ_ProvinceID = PatData.Rows[0]["HKDZPROVICEID"].ToString() == "" ? DefaultInfo.HKDZ_ProvinceID : PatData.Rows[0]["HKDZPROVICEID"].ToString();
                DefaultInfo.HKDZ_CityID = PatData.Rows[0]["HKZDCITYID"].ToString() == "" ? DefaultInfo.HKDZ_CityID : PatData.Rows[0]["HKZDCITYID"].ToString();
                DefaultInfo.HKDZ_DistrictID = PatData.Rows[0]["HKZDDISTRICTID"].ToString() == "" ? DefaultInfo.HKDZ_DistrictID : PatData.Rows[0]["HKZDDISTRICTID"].ToString();
                DefaultInfo.HKDZ_ProvinceName = PatData.Rows[0]["HKDZ_ProvinceName"].ToString() == "" ? DefaultInfo.HKDZ_ProvinceName : PatData.Rows[0]["HKDZ_ProvinceName"].ToString();
                DefaultInfo.HKDZ_CityName = PatData.Rows[0]["HKDZ_CityName"].ToString() == "" ? DefaultInfo.HKDZ_CityName : PatData.Rows[0]["HKDZ_CityName"].ToString();
                DefaultInfo.HKDZ_DistrictName = PatData.Rows[0]["HKDZ_DistrictName"].ToString() == "" ? DefaultInfo.HKDZ_DistrictName : PatData.Rows[0]["HKDZ_DistrictName"].ToString();
                DefaultInfo.HKDZ_Post = PatData.Rows[0]["NATIVEPOST"].ToString() == "" ? DefaultInfo.HKDZ_Post : PatData.Rows[0]["NATIVEPOST"].ToString();

                //籍贯
                DefaultInfo.JG_ProvinceID = PatData.Rows[0]["NATIVEPLACE_P"].ToString() == "" ? DefaultInfo.JG_ProvinceID : PatData.Rows[0]["NATIVEPLACE_P"].ToString();
                DefaultInfo.JG_CityID = PatData.Rows[0]["NATIVEPLACE_C"].ToString() == "" ? DefaultInfo.JG_CityID : PatData.Rows[0]["NATIVEPLACE_C"].ToString();
                DefaultInfo.JG_ProvinceName = PatData.Rows[0]["JG_ProvinceName"].ToString() == "" ? DefaultInfo.JG_ProvinceName : PatData.Rows[0]["JG_ProvinceName"].ToString();
                DefaultInfo.JG_CityName = PatData.Rows[0]["JG_CityName"].ToString() == "" ? DefaultInfo.JG_CityName : PatData.Rows[0]["JG_CityName"].ToString();


                DefaultInfo.NationID = PatData.Rows[0]["NationID"].ToString() == "" ? DefaultInfo.NationID : PatData.Rows[0]["NationID"].ToString();
                DefaultInfo.NationName = PatData.Rows[0]["nationame"].ToString() == "" ? DefaultInfo.NationName : PatData.Rows[0]["nationame"].ToString();
                DefaultInfo.NationalityID = PatData.Rows[0]["NATIONALITYID"].ToString() == "" ? DefaultInfo.NationalityID : PatData.Rows[0]["NATIONALITYID"].ToString();
                DefaultInfo.NationalityName = PatData.Rows[0]["nationaltyname"].ToString() == "" ? DefaultInfo.NationalityName : PatData.Rows[0]["nationaltyname"].ToString();
                DefaultInfo.IDNO = PatData.Rows[0]["IDNO"].ToString() == "" ? DefaultInfo.IDNO : PatData.Rows[0]["IDNO"].ToString();
                DefaultInfo.Organization = PatData.Rows[0]["Organization"].ToString() == "" ? DefaultInfo.Organization : PatData.Rows[0]["Organization"].ToString();
                DefaultInfo.OfficePlace = PatData.Rows[0]["OfficePlace"].ToString() == "" ? DefaultInfo.OfficePlace : PatData.Rows[0]["OfficePlace"].ToString();
                DefaultInfo.OfficeTEL = PatData.Rows[0]["OfficeTEL"].ToString() == "" ? DefaultInfo.OfficeTEL : PatData.Rows[0]["OfficeTEL"].ToString();

                DefaultInfo.OfficePost = PatData.Rows[0]["OfficePost"].ToString() == "" ? DefaultInfo.OfficePost : PatData.Rows[0]["OfficePost"].ToString();
                DefaultInfo.ContactPerson = PatData.Rows[0]["ContactPerson"].ToString() == "" ? DefaultInfo.ContactPerson : PatData.Rows[0]["ContactPerson"].ToString();

                DefaultInfo.RelationshipID = PatData.Rows[0]["RELATIONSHIP"].ToString() == "" ? DefaultInfo.RelationshipID : PatData.Rows[0]["RELATIONSHIP"].ToString();
                DefaultInfo.RelationshipName = PatData.Rows[0]["RelationshipName"].ToString() == "" ? DefaultInfo.RelationshipName : PatData.Rows[0]["RelationshipName"].ToString();
                DefaultInfo.ContactAddress = PatData.Rows[0]["CONTACTADDRESS"].ToString() == "" ? DefaultInfo.ContactAddress : PatData.Rows[0]["CONTACTADDRESS"].ToString();
                DefaultInfo.ContactTEL = PatData.Rows[0]["CONTACTTEL"].ToString() == "" ? DefaultInfo.ContactTEL : PatData.Rows[0]["CONTACTTEL"].ToString();
                DefaultInfo.AdmitDate = PatData.Rows[0]["ADMITDATE"].ToString() == "" ? DefaultInfo.AdmitDate : PatData.Rows[0]["ADMITDATE"].ToString();

                DefaultInfo.AdmitDeptID = PatData.Rows[0]["ADMITDEPT"].ToString() == "" ? DefaultInfo.AdmitDeptID : PatData.Rows[0]["ADMITDEPT"].ToString();
                DefaultInfo.AdmitWardID = PatData.Rows[0]["ADMITWARD"].ToString() == "" ? DefaultInfo.AdmitWardID : PatData.Rows[0]["ADMITWARD"].ToString();
                DefaultInfo.AdmitWay = PatData.Rows[0]["AdmitWay"].ToString() == "" ? DefaultInfo.AdmitWay : PatData.Rows[0]["AdmitWay"].ToString();
                DefaultInfo.Trans_AdmitDeptID = DefaultInfo.Trans_AdmitDeptID;

                DefaultInfo.Trans_AdmitDeptName = DefaultInfo.Trans_AdmitDeptName;

                DefaultInfo.OutWardDate = PatData.Rows[0]["OUTWARDDATE"].ToString() == "" ? DefaultInfo.OutWardDate : PatData.Rows[0]["OUTWARDDATE"].ToString();//ywk  2012年5月9日23:01:57
                DefaultInfo.OutHosDeptID = PatData.Rows[0]["OUTHOSDEPT"].ToString() == "" ? DefaultInfo.OutHosDeptID : PatData.Rows[0]["OUTHOSDEPT"].ToString();
                DefaultInfo.OutHosWardID = PatData.Rows[0]["OUTHOSWARD"].ToString() == "" ? DefaultInfo.OutHosWardID : PatData.Rows[0]["OUTHOSWARD"].ToString();
                DefaultInfo.ActualDays = PatData.Rows[0]["TOTALDAYS"].ToString() == "" ? DefaultInfo.ActualDays : PatData.Rows[0]["TOTALDAYS"].ToString();
                //除了基本信息。Inpatient里还有门诊诊断
                DefaultInfo.OutDiagName = PatData.Rows[0]["OutDiagName"].ToString() == "" ? DefaultInfo.OutDiagName : PatData.Rows[0]["OutDiagName"].ToString();
                DefaultInfo.OutDiagID = PatData.Rows[0]["CLINICDIAGNOSIS"].ToString() == "" ? DefaultInfo.OutDiagID : PatData.Rows[0]["CLINICDIAGNOSIS"].ToString();
                //(除了基本信息还有)科主任，主治和主任医师
                DefaultInfo.Section_DirectorID = PatData.Rows[0]["CHIEF"].ToString();
                DefaultInfo.Section_DirectorName = PatData.Rows[0]["CHIEFNAME"].ToString();
                DefaultInfo.DirectorID = PatData.Rows[0]["CHIEF"].ToString();
                DefaultInfo.DirectorName = PatData.Rows[0]["CHIEFNAME"].ToString();
                DefaultInfo.Vs_EmployeeID = PatData.Rows[0]["ATTEND"].ToString();
                DefaultInfo.Vs_EmployeeName = PatData.Rows[0]["ATTENDNAME"].ToString();
                DefaultInfo.Resident_EmployeeID = PatData.Rows[0]["RESIDENT"].ToString();
                DefaultInfo.Resident_EmployeeName = PatData.Rows[0]["RESIDENTNAME"].ToString();

                //几个地址名称的处理 （出生地  籍贯 现住址  户口地址） add by ywk 
                DefaultInfo.CSDAddress = PatData.Rows[0]["CSDAddress"].ToString() == "" ? DefaultInfo.CSDAddress : PatData.Rows[0]["CSDAddress"].ToString();
                DefaultInfo.JGAddress = PatData.Rows[0]["JGAddress"].ToString() == "" ? DefaultInfo.JGAddress : PatData.Rows[0]["JGAddress"].ToString();
                DefaultInfo.XZZAddress = PatData.Rows[0]["XZZAddress"].ToString() == "" ? DefaultInfo.XZZAddress : PatData.Rows[0]["XZZAddress"].ToString();
                DefaultInfo.HKZZAddress = PatData.Rows[0]["HKAddress"].ToString() == "" ? DefaultInfo.HKZZAddress : PatData.Rows[0]["HKAddress"].ToString();

                DefaultInfo.IsBaby = PatData.Rows[0]["isbaby"].ToString();
                DefaultInfo.Mother = PatData.Rows[0]["mother"].ToString();

            }

            foreach (DataRow row in dataTable.Rows)
            {
                #region 赋值
                IemInfo.IemBasicInfo.Iem_Mainpage_NO = row["Iem_Mainpage_NO"].ToString();
                IemInfo.IemBasicInfo.PatNoOfHis = row["PatNoOfHis"].ToString();
                IemInfo.IemBasicInfo.NoOfInpat = row["NoOfInpat"].ToString();
                IemInfo.IemBasicInfo.InCount = row["InCount"].ToString();
                IemInfo.IemBasicInfo.SocialCare = row["socialcare"].ToString();

                IemInfo.IemBasicInfo.PayID = row["PayID"].ToString();
                IemInfo.IemBasicInfo.PayName = row["PayName"].ToString();
                IemInfo.IemBasicInfo.Name = row["Name"].ToString();
                IemInfo.IemBasicInfo.SexID = row["SexID"].ToString();
                IemInfo.IemBasicInfo.Birth = row["Birth"].ToString().Trim();

                IemInfo.IemBasicInfo.Marital = row["Marital"].ToString();
                IemInfo.IemBasicInfo.JobID = row["JobID"].ToString();
                IemInfo.IemBasicInfo.JobName = row["JobName"].ToString();

                //出生地
                IemInfo.IemBasicInfo.CSD_ProvinceID = row["csd_provinceid"].ToString();
                IemInfo.IemBasicInfo.CSD_CityID = row["csd_cityid"].ToString();
                IemInfo.IemBasicInfo.CSD_DistrictID = row["csd_districtid"].ToString();
                IemInfo.IemBasicInfo.CSD_ProvinceName = row["csd_provincename"].ToString();
                IemInfo.IemBasicInfo.CSD_CityName = row["csd_cityname"].ToString();
                IemInfo.IemBasicInfo.CSD_DistrictName = row["csd_districtname"].ToString();

                //现住地址
                IemInfo.IemBasicInfo.XZZ_ProvinceID = row["xzz_provinceid"].ToString();
                IemInfo.IemBasicInfo.XZZ_CityID = row["xzz_cityid"].ToString();
                IemInfo.IemBasicInfo.XZZ_DistrictID = row["xzz_districtid"].ToString();
                IemInfo.IemBasicInfo.XZZ_ProvinceName = row["xzz_provincename"].ToString();
                IemInfo.IemBasicInfo.XZZ_CityName = row["xzz_cityname"].ToString();
                IemInfo.IemBasicInfo.XZZ_DistrictName = row["xzz_districtname"].ToString();
                IemInfo.IemBasicInfo.XZZ_TEL = row["xzz_tel"].ToString();
                IemInfo.IemBasicInfo.XZZ_Post = row["xzz_post"].ToString();

                //户口地址
                IemInfo.IemBasicInfo.HKDZ_ProvinceID = row["hkdz_provinceid"].ToString();
                IemInfo.IemBasicInfo.HKDZ_CityID = row["hkdz_cityid"].ToString();
                IemInfo.IemBasicInfo.HKDZ_DistrictID = row["hkdz_districtid"].ToString();
                IemInfo.IemBasicInfo.HKDZ_ProvinceName = row["hkdz_provincename"].ToString();
                IemInfo.IemBasicInfo.HKDZ_CityName = row["hkdz_cityname"].ToString();
                IemInfo.IemBasicInfo.HKDZ_DistrictName = row["hkdz_districtname"].ToString();
                IemInfo.IemBasicInfo.HKDZ_Post = row["hkdz_post"].ToString();

                //籍贯
                IemInfo.IemBasicInfo.JG_ProvinceID = row["jg_provinceid"].ToString();
                IemInfo.IemBasicInfo.JG_CityID = row["jg_cityid"].ToString();
                IemInfo.IemBasicInfo.JG_ProvinceName = row["jg_provincename"].ToString();
                IemInfo.IemBasicInfo.JG_CityName = row["jg_cityname"].ToString();


                IemInfo.IemBasicInfo.NationID = row["NationID"].ToString();
                IemInfo.IemBasicInfo.NationName = row["NationName"].ToString();
                IemInfo.IemBasicInfo.NationalityID = row["NationalityID"].ToString();

                IemInfo.IemBasicInfo.NationalityName = row["NationalityName"].ToString();
                IemInfo.IemBasicInfo.IDNO = row["IDNO"].ToString();
                IemInfo.IemBasicInfo.Organization = row["Organization"].ToString();
                IemInfo.IemBasicInfo.OfficePlace = row["OfficePlace"].ToString();
                IemInfo.IemBasicInfo.OfficeTEL = row["OfficeTEL"].ToString();

                IemInfo.IemBasicInfo.OfficePost = row["OfficePost"].ToString();
                IemInfo.IemBasicInfo.ContactPerson = row["ContactPerson"].ToString();

                IemInfo.IemBasicInfo.RelationshipID = row["relationship"].ToString();
                IemInfo.IemBasicInfo.RelationshipName = row["RelationshipName"].ToString();
                IemInfo.IemBasicInfo.ContactAddress = row["ContactAddress"].ToString();
                IemInfo.IemBasicInfo.ContactTEL = row["ContactTEL"].ToString();
                IemInfo.IemBasicInfo.AdmitDate = row["AdmitDate"].ToString();

                IemInfo.IemBasicInfo.AdmitDeptID = row["AdmitDept"].ToString();
                IemInfo.IemBasicInfo.AdmitDeptName = row["AdmitDeptName"].ToString();
                IemInfo.IemBasicInfo.AdmitWardID = row["AdmitWard"].ToString();
                IemInfo.IemBasicInfo.AdmitWardName = row["AdmitWardName"].ToString();
                IemInfo.IemBasicInfo.Trans_AdmitDeptID = row["Trans_AdmitDept"].ToString();

                IemInfo.IemBasicInfo.Trans_AdmitDeptName = row["Trans_AdmitDeptName"].ToString();
                //IemInfo.IemBasicInfo.OutWardDate = row["OutWardDate"].ToString();
                IemInfo.IemBasicInfo.OutWardDate = row["OutHosDate"].ToString();//ywk  2012年5月9日23:01:57
                IemInfo.IemBasicInfo.OutHosDeptID = row["OutHosDept"].ToString();
                IemInfo.IemBasicInfo.OutHosDeptName = row["outhosdeptName"].ToString();
                IemInfo.IemBasicInfo.OutHosWardID = row["OutHosWard"].ToString();

                IemInfo.IemBasicInfo.OutHosWardName = row["OutHosWardName"].ToString();
                IemInfo.IemBasicInfo.ActualDays = row["actualdays"].ToString();

                ////基础信息模块
                IemInfo.IemBasicInfo.Is_Completed = row["is_completed"].ToString();
                IemInfo.IemBasicInfo.completed_time = row["completed_time"].ToString();
                IemInfo.IemBasicInfo.Valide = row["Valide"].ToString();
                IemInfo.IemBasicInfo.Create_User = row["Create_User"].ToString();
                IemInfo.IemBasicInfo.Create_Time = row["Create_Time"].ToString();
                IemInfo.IemBasicInfo.Modified_User = row["Modified_User"].ToString();
                IemInfo.IemBasicInfo.Modified_Time = row["Modified_Time"].ToString();

                IemInfo.IemBasicInfo.Autopsy_Flag = row["autopsy_flag"].ToString();

                //--2012国家卫生部表中病案首页新增内容
                IemInfo.IemBasicInfo.MonthAge = row["monthage"].ToString();
                IemInfo.IemBasicInfo.Weight = row["weight"].ToString();
                IemInfo.IemBasicInfo.InWeight = row["inweight"].ToString();
                IemInfo.IemBasicInfo.InHosType = row["inhostype"].ToString();
                IemInfo.IemBasicInfo.OutHosType = row["outhostype"].ToString();

                IemInfo.IemBasicInfo.ReceiveHosPital = row["receivehospital"].ToString();
                IemInfo.IemBasicInfo.ReceiveHosPital2 = row["receivehospital2"].ToString();
                IemInfo.IemBasicInfo.AgainInHospital = row["againinhospital"].ToString();
                IemInfo.IemBasicInfo.AgainInHospitalReason = row["againinhospitalreason"].ToString();
                IemInfo.IemBasicInfo.BeforeHosComaDay = row["beforehoscomaday"].ToString();

                IemInfo.IemBasicInfo.BeforeHosComaHour = row["beforehoscomahour"].ToString();
                IemInfo.IemBasicInfo.BeforeHosComaMinute = row["beforehoscomaminute"].ToString();
                IemInfo.IemBasicInfo.LaterHosComaDay = row["laterhoscomaday"].ToString();
                IemInfo.IemBasicInfo.LaterHosComaHour = row["laterhoscomahour"].ToString();
                IemInfo.IemBasicInfo.LaterHosComaMinute = row["laterhoscomaminute"].ToString();
                IemInfo.IemBasicInfo.CardNumber = row["cardnumber"].ToString();


                IemInfo.IemBasicInfo.Age = row["Age"].ToString();

                IemInfo.IemBasicInfo.HospitalName = m_DataHelper.GetHospitalName();
                IemInfo.IemBasicInfo.ZG_FLAG = row["ZG_FLAG"].ToString();
                //新增的入院病情 add by ywk 2012年6月26日10:22:44
                IemInfo.IemBasicInfo.AdmitInfo = row["AdmitInfo"].ToString();

                ///////诊断实体中
                //IemInfo.IemDiagInfo.AdmitInfo = row["AdmitInfo"].ToString();
                //IemInfo.IemDiagInfo.In_Check_Date = row["In_Check_Date"].ToString();
                //IemInfo.IemDiagInfo.ZymosisID = row["zymosis"].ToString();
                //IemInfo.IemDiagInfo.ZymosisName = row["zymosisName"].ToString();
                //IemInfo.IemDiagInfo.ZymosisState = row["ZymosisState"].ToString();

                IemInfo.IemDiagInfo.Pathology_Diagnosis_ID = row["pathology_diagnosis_id"].ToString();
                IemInfo.IemDiagInfo.Pathology_Diagnosis_Name = row["pathology_diagnosis_name"].ToString();
                IemInfo.IemDiagInfo.Pathology_Observation_Sn = row["pathology_observation_sn"].ToString();
                IemInfo.IemDiagInfo.Hurt_Toxicosis_ElementID = row["hurt_toxicosis_ele_id"].ToString();
                IemInfo.IemDiagInfo.Hurt_Toxicosis_Element = row["hurt_toxicosis_ele_name"].ToString();

                IemInfo.IemDiagInfo.Allergic_Drug = row["allergic_drug"].ToString();
                IemInfo.IemDiagInfo.Allergic_Flag = row["allergic_flag"].ToString();
                IemInfo.IemDiagInfo.BloodType = row["bloodtype"].ToString();
                IemInfo.IemDiagInfo.Rh = row["rh"].ToString();
                IemInfo.IemDiagInfo.Section_DirectorID = row["section_director"].ToString();

                IemInfo.IemDiagInfo.Section_DirectorName = row["section_directorName"].ToString();
                IemInfo.IemDiagInfo.DirectorID = row["director"].ToString();
                IemInfo.IemDiagInfo.DirectorName = row["directorName"].ToString();
                IemInfo.IemDiagInfo.Vs_EmployeeID = row["vs_employeeID"].ToString();
                IemInfo.IemDiagInfo.Vs_EmployeeName = row["vs_employeeName"].ToString();

                IemInfo.IemDiagInfo.Resident_EmployeeID = row["resident_employeeID"].ToString();
                IemInfo.IemDiagInfo.Resident_EmployeeName = row["resident_employeeName"].ToString();
                IemInfo.IemDiagInfo.Refresh_EmployeeID = row["refresh_employeeID"].ToString();
                IemInfo.IemDiagInfo.Refresh_EmployeeName = row["refresh_employeeName"].ToString();
                IemInfo.IemDiagInfo.Duty_NurseID = row["duty_nurse"].ToString();

                IemInfo.IemDiagInfo.Duty_NurseName = row["Duty_NurseName"].ToString();
                IemInfo.IemDiagInfo.InterneID = row["interne"].ToString();
                IemInfo.IemDiagInfo.InterneName = row["interneName"].ToString();
                IemInfo.IemDiagInfo.Coding_UserID = row["coding_user"].ToString();
                IemInfo.IemDiagInfo.Coding_UserName = row["coding_userName"].ToString();

                IemInfo.IemDiagInfo.Medical_Quality_Id = row["medical_quality_id"].ToString();
                IemInfo.IemDiagInfo.Quality_Control_DoctorID = row["quality_control_doctor"].ToString();
                IemInfo.IemDiagInfo.Quality_Control_DoctorName = row["quality_control_doctorName"].ToString();
                IemInfo.IemDiagInfo.Quality_Control_NurseID = row["quality_control_nurse"].ToString();
                IemInfo.IemDiagInfo.Quality_Control_NurseName = row["quality_control_nurseName"].ToString();

                IemInfo.IemDiagInfo.Quality_Control_Date = row["quality_control_date"].ToString();

                //IemInfo.IemDiagInfo.MenAndInHop = row["menandinhop"].ToString();
                //IemInfo.IemDiagInfo.InHopAndOutHop = row["inhopandouthop"].ToString();
                //IemInfo.IemDiagInfo.BeforeOpeAndAfterOper = row["beforeopeandafteroper"].ToString();
                //IemInfo.IemDiagInfo.LinAndBingLi = row["linandbingli"].ToString();
                //IemInfo.IemDiagInfo.InHopThree = row["inhopthree"].ToString();
                //IemInfo.IemDiagInfo.FangAndBingLi = row["fangandbingli"].ToString();
                ///////费用模块
                //IemInfo.IemFeeInfo.Ashes_Check = row["Ashes_Check"].ToString();
                //IemInfo.IemFeeInfo.IsFirstCase = row["is_first_case"].ToString();
                //IemInfo.IemFeeInfo.IsFollowing = row["is_following"].ToString();
                //IemInfo.IemFeeInfo.IsTeachingCase = row["is_teaching_case"].ToString();
                //IemInfo.IemFeeInfo.Following_Ending_Date = row["following_ending_date"].ToString();

                //IemInfo.IemFeeInfo.BloodType = row["blood_type_id"].ToString();
                //IemInfo.IemFeeInfo.Rh = row["Rh"].ToString();
                //IemInfo.IemFeeInfo.BloodReaction = row["blood_reaction_id"].ToString();
                //IemInfo.IemFeeInfo.Rbc = row["blood_rbc"].ToString();
                //IemInfo.IemFeeInfo.Plt = row["blood_plt"].ToString();

                //IemInfo.IemFeeInfo.Plasma = row["Blood_Plasma"].ToString();
                //IemInfo.IemFeeInfo.Wb = row["blood_wb"].ToString();
                //IemInfo.IemFeeInfo.Others = row["blood_others"].ToString();

                ////费用信息暂时假数据
                //IemInfo.IemFeeInfo.Total = "15000";
                //IemInfo.IemFeeInfo.Bed = "100";
                //IemInfo.IemFeeInfo.Care = "200";
                //IemInfo.IemFeeInfo.WMedical = "300";
                //IemInfo.IemFeeInfo.CPMedical = "400";
                //IemInfo.IemFeeInfo.CMedical = "500";
                //IemInfo.IemFeeInfo.Radiate = "600";
                //IemInfo.IemFeeInfo.Assay = "700";
                //IemInfo.IemFeeInfo.Ox = "800";
                //IemInfo.IemFeeInfo.Blood = "900";
                //IemInfo.IemFeeInfo.Mecical = "1000";
                //IemInfo.IemFeeInfo.Operation = "1100";
                //IemInfo.IemFeeInfo.Accouche = "1200";
                //IemInfo.IemFeeInfo.Ris = "1300";
                //IemInfo.IemFeeInfo.Anaesthesia = "1400";
                //IemInfo.IemFeeInfo.Baby = "1500";
                //IemInfo.IemFeeInfo.FollwBed = "1600";
                //IemInfo.IemFeeInfo.Others1 = "1700";
                //IemInfo.IemFeeInfo.Others2 = "1800";
                //IemInfo.IemFeeInfo.Others3 = "1900";
                //IemInfo.IemFeeInfo = GetIemFeeInfo(IemInfo);





                //IemInfo.IemBasicInfo.Xay_Sn = row["Xay_Sn"].ToString();
                //IemInfo.IemBasicInfo.Ct_Sn = row["Ct_Sn"].ToString();
                //IemInfo.IemBasicInfo.Mri_Sn = row["Mri_Sn"].ToString();
                //IemInfo.IemBasicInfo.Dsa_Sn = row["Dsa_Sn"].ToString();

                #endregion

                #region 对于几个地址的详细名称的处理
                IemInfo.IemBasicInfo.CSDAddress = row["CSDAddress"].ToString();
                IemInfo.IemBasicInfo.JGAddress = row["JGAddress"].ToString();
                IemInfo.IemBasicInfo.XZZAddress = row["XZZAddress"].ToString();
                IemInfo.IemBasicInfo.HKZZAddress = row["HKAddress"].ToString();
                #endregion

                #region 对于几个诊断符合情况的处理
                IemInfo.IemBasicInfo.MenAndInHop = row["MenAndInHop"].ToString();
                IemInfo.IemBasicInfo.InHopAndOutHop = row["InHopAndOutHop"].ToString();
                IemInfo.IemBasicInfo.BeforeOpeAndAfterOper = row["BeforeOpeAndAfterOper"].ToString();
                IemInfo.IemBasicInfo.LinAndBingLi = row["LinAndBingLi"].ToString();
                IemInfo.IemBasicInfo.InHopThree = row["InHopThree"].ToString();
                IemInfo.IemBasicInfo.FangAndBingLi = row["FangAndBingLi"].ToString();
                #endregion

                IemInfo.IemBasicInfo.IsBaby = row["IsBaby"].ToString();
                IemInfo.IemBasicInfo.Mother = row["Mother"].ToString();
                break;
            }

            #region 给实体赋空值
            if (dataTable.Rows.Count == 0)
            {
                IemInfo.IemBasicInfo.Iem_Mainpage_NO = "";
                IemInfo.IemBasicInfo.PatNoOfHis = DefaultInfo.PatNoOfHis;
                IemInfo.IemBasicInfo.NoOfInpat = DefaultInfo.NoOfInpat;
                IemInfo.IemBasicInfo.InCount = DefaultInfo.InCount;
                IemInfo.IemBasicInfo.SocialCare = DefaultInfo.SocialCare;

                IemInfo.IemBasicInfo.PayID = DefaultInfo.PayID;
                IemInfo.IemBasicInfo.PayName = DefaultInfo.PayName;
                IemInfo.IemBasicInfo.Name = DefaultInfo.Name;
                IemInfo.IemBasicInfo.SexID = DefaultInfo.SexID;
                //IemInfo.IemBasicInfo.Birth = DefaultInfo.Birth;

                IemInfo.IemBasicInfo.Marital = DefaultInfo.Marital;
                IemInfo.IemBasicInfo.JobID = DefaultInfo.JobID;
                IemInfo.IemBasicInfo.JobName = DefaultInfo.JobName;

                //出生地
                //IemInfo.IemBasicInfo.CSD_ProvinceID = "";
                //IemInfo.IemBasicInfo.CSD_CityID = "";
                //IemInfo.IemBasicInfo.CSD_DistrictID = "";
                //IemInfo.IemBasicInfo.CSD_ProvinceName = "";
                //IemInfo.IemBasicInfo.CSD_CityName = "";
                //IemInfo.IemBasicInfo.CSD_DistrictName = "";
                //出生地
                IemInfo.IemBasicInfo.CSD_ProvinceID = DefaultInfo.CSD_ProvinceID;//省份ID
                IemInfo.IemBasicInfo.CSD_CityID = DefaultInfo.CSD_CityID;//市ID
                IemInfo.IemBasicInfo.CSD_DistrictID = DefaultInfo.CSD_DistrictID;//县ID
                IemInfo.IemBasicInfo.CSD_ProvinceName = DefaultInfo.CSD_ProvinceName;//省份名称
                IemInfo.IemBasicInfo.CSD_CityName = DefaultInfo.CSD_CityName;//市名称
                IemInfo.IemBasicInfo.CSD_DistrictName = DefaultInfo.CSD_DistrictName;//县名称

                //现住地址
                //IemInfo.IemBasicInfo.XZZ_ProvinceID = "";
                //IemInfo.IemBasicInfo.XZZ_CityID = "";
                //IemInfo.IemBasicInfo.XZZ_DistrictID = "";
                //IemInfo.IemBasicInfo.XZZ_ProvinceName = "";
                //IemInfo.IemBasicInfo.XZZ_CityName = "";
                //IemInfo.IemBasicInfo.XZZ_DistrictName = "";
                //IemInfo.IemBasicInfo.XZZ_TEL = "";
                //IemInfo.IemBasicInfo.XZZ_Post = "";
                //现住地址
                IemInfo.IemBasicInfo.XZZ_ProvinceID = DefaultInfo.XZZ_ProvinceID;//省份ID
                IemInfo.IemBasicInfo.XZZ_CityID = DefaultInfo.XZZ_CityID;//市ID
                IemInfo.IemBasicInfo.XZZ_DistrictID = DefaultInfo.XZZ_DistrictID;//县ID
                IemInfo.IemBasicInfo.XZZ_ProvinceName = DefaultInfo.XZZ_ProvinceName;//省份名称
                IemInfo.IemBasicInfo.XZZ_CityName = DefaultInfo.XZZ_CityName;//市名称
                IemInfo.IemBasicInfo.XZZ_DistrictName = DefaultInfo.XZZ_DistrictName;//县名称
                IemInfo.IemBasicInfo.XZZ_TEL = DefaultInfo.XZZ_TEL;//电话
                IemInfo.IemBasicInfo.XZZ_Post = DefaultInfo.XZZ_Post;//邮编

                //户口地址
                //IemInfo.IemBasicInfo.HKDZ_ProvinceID = "";
                //IemInfo.IemBasicInfo.HKDZ_CityID = "";
                //IemInfo.IemBasicInfo.HKDZ_DistrictID = "";
                //IemInfo.IemBasicInfo.HKDZ_ProvinceName = "";
                //IemInfo.IemBasicInfo.HKDZ_CityName = "";
                //IemInfo.IemBasicInfo.HKDZ_DistrictName = "";
                //IemInfo.IemBasicInfo.HKDZ_Post = "";
                IemInfo.IemBasicInfo.HKDZ_ProvinceID = DefaultInfo.HKDZ_ProvinceID;//省份ID
                IemInfo.IemBasicInfo.HKDZ_CityID = DefaultInfo.HKDZ_CityID;//市ID
                IemInfo.IemBasicInfo.HKDZ_DistrictID = DefaultInfo.HKDZ_DistrictID; ;//县ID
                IemInfo.IemBasicInfo.HKDZ_ProvinceName = DefaultInfo.HKDZ_ProvinceName;//省份名称
                IemInfo.IemBasicInfo.HKDZ_CityName = DefaultInfo.HKDZ_CityName;//市名称
                IemInfo.IemBasicInfo.HKDZ_DistrictName = DefaultInfo.HKDZ_DistrictName;//县名称
                IemInfo.IemBasicInfo.HKDZ_Post = DefaultInfo.HKDZ_Post;//邮编

                //籍贯
                IemInfo.IemBasicInfo.JG_ProvinceID = DefaultInfo.JG_ProvinceID;
                IemInfo.IemBasicInfo.JG_CityID = DefaultInfo.JG_CityID;
                IemInfo.IemBasicInfo.JG_ProvinceName = DefaultInfo.JG_ProvinceName;
                IemInfo.IemBasicInfo.JG_CityName = DefaultInfo.JG_CityName;


                IemInfo.IemBasicInfo.NationID = DefaultInfo.NationID;
                IemInfo.IemBasicInfo.NationName = DefaultInfo.NationName;
                IemInfo.IemBasicInfo.NationalityID = DefaultInfo.NationalityID;


                IemInfo.IemBasicInfo.NationalityName = DefaultInfo.NationalityName;
                IemInfo.IemBasicInfo.IDNO = DefaultInfo.IDNO;
                IemInfo.IemBasicInfo.Organization = DefaultInfo.Organization;
                IemInfo.IemBasicInfo.OfficePlace = DefaultInfo.OfficePlace;
                IemInfo.IemBasicInfo.OfficeTEL = DefaultInfo.OfficeTEL;

                IemInfo.IemBasicInfo.OfficePost = DefaultInfo.OfficePost;//工作单位邮编
                IemInfo.IemBasicInfo.ContactPerson = DefaultInfo.ContactPerson;

                IemInfo.IemBasicInfo.RelationshipID = DefaultInfo.RelationshipID;
                IemInfo.IemBasicInfo.RelationshipName = DefaultInfo.RelationshipName;
                IemInfo.IemBasicInfo.ContactAddress = DefaultInfo.ContactAddress;
                IemInfo.IemBasicInfo.ContactTEL = DefaultInfo.ContactTEL;
                IemInfo.IemBasicInfo.AdmitDate = "";

                IemInfo.IemBasicInfo.AdmitDeptID = "";
                IemInfo.IemBasicInfo.AdmitDeptName = "";
                IemInfo.IemBasicInfo.AdmitWardID = "";
                IemInfo.IemBasicInfo.AdmitWardName = "";
                IemInfo.IemBasicInfo.Trans_AdmitDeptID = DefaultInfo.Trans_AdmitDeptID;//转院科别编号

                IemInfo.IemBasicInfo.Trans_AdmitDeptName = DefaultInfo.Trans_AdmitDeptName;//转院科别名称
                IemInfo.IemBasicInfo.OutWardDate = "";
                IemInfo.IemBasicInfo.OutHosDeptID = "";
                IemInfo.IemBasicInfo.OutHosDeptName = "";
                IemInfo.IemBasicInfo.OutHosWardID = "";

                IemInfo.IemBasicInfo.OutHosWardName = "";
                IemInfo.IemBasicInfo.ActualDays = DefaultInfo.ActualDays;

                ////基础信息模块
                IemInfo.IemBasicInfo.Is_Completed = "";
                IemInfo.IemBasicInfo.completed_time = "";
                IemInfo.IemBasicInfo.Valide = "";
                IemInfo.IemBasicInfo.Create_User = "";
                IemInfo.IemBasicInfo.Create_Time = "";
                IemInfo.IemBasicInfo.Modified_User = "";
                IemInfo.IemBasicInfo.Modified_Time = "";

                IemInfo.IemBasicInfo.Autopsy_Flag = "";

                //--2012国家卫生部表中病案首页新增内容
                //IemInfo.IemBasicInfo.MonthAge = "";
                //IemInfo.IemBasicInfo.Weight = "";
                //IemInfo.IemBasicInfo.InWeight = "";
                IemInfo.IemBasicInfo.MonthAge = "---";
                IemInfo.IemBasicInfo.Weight = "---";
                IemInfo.IemBasicInfo.InWeight = "---";
                IemInfo.IemBasicInfo.InHosType = DefaultInfo.AdmitWay;//入院途径
                IemInfo.IemBasicInfo.OutHosType = "";

                IemInfo.IemBasicInfo.ReceiveHosPital = "";
                IemInfo.IemBasicInfo.ReceiveHosPital2 = "";
                IemInfo.IemBasicInfo.AgainInHospital = "";
                IemInfo.IemBasicInfo.AgainInHospitalReason = "";
                //颅内昏迷的默认值为----  editor ywk 2012年5月30日 10:15:51
                IemInfo.IemBasicInfo.BeforeHosComaDay = "---";
                IemInfo.IemBasicInfo.BeforeHosComaHour = "---";
                IemInfo.IemBasicInfo.BeforeHosComaMinute = "---";
                IemInfo.IemBasicInfo.LaterHosComaDay = "---";
                IemInfo.IemBasicInfo.LaterHosComaHour = "---";
                IemInfo.IemBasicInfo.LaterHosComaMinute = "---";
                //IemInfo.IemBasicInfo.BeforeHosComaDay = "";
                //IemInfo.IemBasicInfo.BeforeHosComaHour = "";
                //IemInfo.IemBasicInfo.BeforeHosComaMinute = "";
                //IemInfo.IemBasicInfo.LaterHosComaDay = "";
                //IemInfo.IemBasicInfo.LaterHosComaHour = "";
                //IemInfo.IemBasicInfo.LaterHosComaMinute = "";
                IemInfo.IemBasicInfo.CardNumber = "---";//健康卡号默认为-


                IemInfo.IemBasicInfo.Age = CurrentInpatient.PersonalInformation.CurrentDisplayAge;

                IemInfo.IemBasicInfo.HospitalName = m_DataHelper.GetHospitalName();
                IemInfo.IemBasicInfo.ZG_FLAG = "";

                ///////诊断实体中
                //IemInfo.IemDiagInfo.AdmitInfo = row["AdmitInfo"].ToString();
                //IemInfo.IemDiagInfo.In_Check_Date = row["In_Check_Date"].ToString();
                //IemInfo.IemDiagInfo.ZymosisID = row["zymosis"].ToString();
                //IemInfo.IemDiagInfo.ZymosisName = row["zymosisName"].ToString();
                //IemInfo.IemDiagInfo.ZymosisState = row["ZymosisState"].ToString();

                IemInfo.IemDiagInfo.Pathology_Diagnosis_ID = "";
                IemInfo.IemDiagInfo.Pathology_Diagnosis_Name = "";
                IemInfo.IemDiagInfo.Pathology_Observation_Sn = "";
                IemInfo.IemDiagInfo.Hurt_Toxicosis_ElementID = "";
                IemInfo.IemDiagInfo.Hurt_Toxicosis_Element = "";
                //门诊诊断
                IemInfo.IemDiagInfo.OutDiagID = DefaultInfo.OutDiagID;
                IemInfo.IemDiagInfo.OutDiagName = DefaultInfo.OutDiagName;

                IemInfo.IemDiagInfo.Allergic_Drug = "";
                IemInfo.IemDiagInfo.Allergic_Flag = "";
                IemInfo.IemDiagInfo.BloodType = "";
                IemInfo.IemDiagInfo.Rh = "";



                #region 对于几个地址的详细名称的处理
                IemInfo.IemBasicInfo.CSDAddress = DefaultInfo.CSDAddress;
                IemInfo.IemBasicInfo.JGAddress = DefaultInfo.JGAddress;
                IemInfo.IemBasicInfo.XZZAddress = DefaultInfo.XZZAddress;
                IemInfo.IemBasicInfo.HKZZAddress = DefaultInfo.HKZZAddress;
                #endregion
                #region 对于几个诊断符合情况的处理
                IemInfo.IemBasicInfo.MenAndInHop = "";
                IemInfo.IemBasicInfo.InHopAndOutHop = "";
                IemInfo.IemBasicInfo.BeforeOpeAndAfterOper = "";
                IemInfo.IemBasicInfo.LinAndBingLi = "";
                IemInfo.IemBasicInfo.InHopThree = "";
                IemInfo.IemBasicInfo.FangAndBingLi = "";
                #endregion

                ////(除了基本信息还有)科主任，主治和主任医师
                //DefaultInfo.Section_DirectorID = PatData.Rows[0]["CHIEF"].ToString();
                //DefaultInfo.Section_DirectorName = PatData.Rows[0]["CHIEFNAME"].ToString();
                //DefaultInfo.DirectorID = PatData.Rows[0]["CHIEF"].ToString();
                //DefaultInfo.DirectorName = PatData.Rows[0]["CHIEFNAME"].ToString();
                //DefaultInfo.Vs_EmployeeID = PatData.Rows[0]["ATTEND"].ToString();
                //DefaultInfo.Vs_EmployeeName = PatData.Rows[0]["ATTENDNAME"].ToString();
                //DefaultInfo.Resident_EmployeeID = PatData.Rows[0]["RESIDENT"].ToString();
                //DefaultInfo.Resident_EmployeeName = PatData.Rows[0]["RESIDENTNAME"].ToString();

                //此处赋给默认值中主任及科室主任和主任医师的值 
                IemInfo.IemDiagInfo.Section_DirectorID = DefaultInfo.Section_DirectorID;
                IemInfo.IemDiagInfo.Section_DirectorName = DefaultInfo.Section_DirectorName;
                IemInfo.IemDiagInfo.DirectorID = DefaultInfo.DirectorID;
                IemInfo.IemDiagInfo.DirectorName = DefaultInfo.DirectorName;
                IemInfo.IemDiagInfo.Vs_EmployeeID = DefaultInfo.Vs_EmployeeID;
                IemInfo.IemDiagInfo.Vs_EmployeeName = DefaultInfo.Vs_EmployeeName;
                IemInfo.IemDiagInfo.Resident_EmployeeID = DefaultInfo.Resident_EmployeeID;
                IemInfo.IemDiagInfo.Resident_EmployeeName = DefaultInfo.Resident_EmployeeName;

                IemInfo.IemDiagInfo.Refresh_EmployeeID = "";
                IemInfo.IemDiagInfo.Refresh_EmployeeName = "";
                IemInfo.IemDiagInfo.Duty_NurseID = "";

                IemInfo.IemDiagInfo.Duty_NurseName = "";
                IemInfo.IemDiagInfo.InterneID = "";
                IemInfo.IemDiagInfo.InterneName = "";
                IemInfo.IemDiagInfo.Coding_UserID = "";
                IemInfo.IemDiagInfo.Coding_UserName = "";

                IemInfo.IemDiagInfo.Medical_Quality_Id = "";
                IemInfo.IemDiagInfo.Quality_Control_DoctorID = "";
                IemInfo.IemDiagInfo.Quality_Control_DoctorName = "";
                IemInfo.IemDiagInfo.Quality_Control_NurseID = "";
                IemInfo.IemDiagInfo.Quality_Control_NurseName = "";

                IemInfo.IemDiagInfo.Quality_Control_Date = "";

                ///////费用模块
                //IemInfo.IemFeeInfo.Ashes_Check = row["Ashes_Check"].ToString();
                //IemInfo.IemFeeInfo.IsFirstCase = row["is_first_case"].ToString();
                //IemInfo.IemFeeInfo.IsFollowing = row["is_following"].ToString();
                //IemInfo.IemFeeInfo.IsTeachingCase = row["is_teaching_case"].ToString();
                //IemInfo.IemFeeInfo.Following_Ending_Date = row["following_ending_date"].ToString();

                //IemInfo.IemFeeInfo.BloodType = row["blood_type_id"].ToString();
                //IemInfo.IemFeeInfo.Rh = row["Rh"].ToString();
                //IemInfo.IemFeeInfo.BloodReaction = row["blood_reaction_id"].ToString();
                //IemInfo.IemFeeInfo.Rbc = row["blood_rbc"].ToString();
                //IemInfo.IemFeeInfo.Plt = row["blood_plt"].ToString();

                //IemInfo.IemFeeInfo.Plasma = row["Blood_Plasma"].ToString();
                //IemInfo.IemFeeInfo.Wb = row["blood_wb"].ToString();
                //IemInfo.IemFeeInfo.Others = row["blood_others"].ToString();

                ////费用信息暂时假数据
                //IemInfo.IemFeeInfo.Total = "15000";
                //IemInfo.IemFeeInfo.Bed = "100";
                //IemInfo.IemFeeInfo.Care = "200";
                //IemInfo.IemFeeInfo.WMedical = "300";
                //IemInfo.IemFeeInfo.CPMedical = "400";
                //IemInfo.IemFeeInfo.CMedical = "500";
                //IemInfo.IemFeeInfo.Radiate = "600";
                //IemInfo.IemFeeInfo.Assay = "700";
                //IemInfo.IemFeeInfo.Ox = "800";
                //IemInfo.IemFeeInfo.Blood = "900";
                //IemInfo.IemFeeInfo.Mecical = "1000";
                //IemInfo.IemFeeInfo.Operation = "1100";
                //IemInfo.IemFeeInfo.Accouche = "1200";
                //IemInfo.IemFeeInfo.Ris = "1300";
                //IemInfo.IemFeeInfo.Anaesthesia = "1400";
                //IemInfo.IemFeeInfo.Baby = "1500";
                //IemInfo.IemFeeInfo.FollwBed = "1600";
                //IemInfo.IemFeeInfo.Others1 = "1700";
                //IemInfo.IemFeeInfo.Others2 = "1800";
                //IemInfo.IemFeeInfo.Others3 = "1900";
                //IemInfo.IemFeeInfo = GetIemFeeInfo(IemInfo);


                IemInfo.IemBasicInfo.IsBaby = DefaultInfo.IsBaby;
                IemInfo.IemBasicInfo.Mother = DefaultInfo.Mother;


                //IemInfo.IemBasicInfo.Xay_Sn = row["Xay_Sn"].ToString();
                //IemInfo.IemBasicInfo.Ct_Sn = row["Ct_Sn"].ToString();
                //IemInfo.IemBasicInfo.Mri_Sn = row["Mri_Sn"].ToString();
                //IemInfo.IemBasicInfo.Dsa_Sn = row["Dsa_Sn"].ToString();


                #region 赋基础值  取值地方待确定
                //(此处附基础值的地方，关于基本的信息，在上面已经取的是默认表的数据)
                IemInfo.IemBasicInfo.PatNoOfHis = CurrentInpatient.NoOfHisFirstPage.ToString();
                IemInfo.IemBasicInfo.InCount = CurrentInpatient.TimesOfAdmission.ToString();

                IemInfo.IemBasicInfo.PayID = CurrentInpatient.PaymentKind.Code.ToString();
                IemInfo.IemBasicInfo.PayName = CurrentInpatient.PaymentKind.Name.ToString();

                IemInfo.IemBasicInfo.Age = CurrentInpatient.PersonalInformation.CurrentDisplayAge;
                IemInfo.IemBasicInfo.HospitalName = m_DataHelper.GetHospitalName();
                if (CurrentInpatient.PersonalInformation != null)
                {
                    IemInfo.IemBasicInfo.SocialCare = CurrentInpatient.PersonalInformation.SocialInsuranceNo;
                    //IemInfo.IemBasicInfo.Age = CurrentInpatient.PersonalInformation.DisplayAge;
                    IemInfo.IemBasicInfo.Name = CurrentInpatient.PersonalInformation.PatientName;

                    if (CurrentInpatient.PersonalInformation.Sex != null)
                        IemInfo.IemBasicInfo.SexID = CurrentInpatient.PersonalInformation.Sex.Code;
                    if (CurrentInpatient.PersonalInformation.Birthday.CompareTo(DateTime.MinValue) != 0)
                    {
                        IemInfo.IemBasicInfo.Birth = CurrentInpatient.PersonalInformation.Birthday.ToString("yyyy-MM-dd");
                    }
                    //婚姻还是根据Inpatient_default表来赋值
                    //edit by ywk 2012年7月13日 11:49:10 
                    //if (CurrentInpatient.PersonalInformation.MarriageCondition != null)
                    //    IemInfo.IemBasicInfo.Marital = CurrentInpatient.PersonalInformation.MarriageCondition.Code;


                    if (CurrentInpatient.PersonalInformation.DepartmentOfWork != null)
                    {
                        //IemInfo.IemBasicInfo.JobID = CurrentInpatient.PersonalInformation.DepartmentOfWork.Occupation.Code;
                        //IemInfo.IemBasicInfo.JobName = CurrentInpatient.PersonalInformation.DepartmentOfWork.Occupation.Name;
                        //IemInfo.IemBasicInfo.OfficePlace = CurrentInpatient.PersonalInformation.DepartmentOfWork.CompanyName + CurrentInpatient.PersonalInformation.DepartmentOfWork.CompanyAddress;
                    }
                    if (IemInfo.IemBasicInfo.OfficePlace.Trim() == "[]")
                    {
                        IemInfo.IemBasicInfo.OfficePlace = "";
                    }

                    if (CurrentInpatient.PersonalInformation.DomiciliaryInfo != null)
                    {
                        //IemInfo.IemBasicInfo.ProvinceID = CurrentInpatient.PersonalInformation.DomiciliaryInfo.Province.Code;
                        //IemInfo.IemBasicInfo.ProvinceName = CurrentInpatient.PersonalInformation.DomiciliaryInfo.Province.Name;
                        //IemInfo.IemBasicInfo.CountyID = CurrentInpatient.PersonalInformation.DomiciliaryInfo.City.Code;
                        //IemInfo.IemBasicInfo.CountyID = CurrentInpatient.PersonalInformation.DomiciliaryInfo.City.Name;

                        //IemInfo.IemBasicInfo.NationalityID = CurrentInpatient.PersonalInformation.DomiciliaryInfo.Country.Code;
                        //IemInfo.IemBasicInfo.NationalityName = CurrentInpatient.PersonalInformation.DomiciliaryInfo.Country.Name;
                        //IemInfo.IemBasicInfo.NativeAddress = CurrentInpatient.PersonalInformation.DomiciliaryInfo.FullAddress;
                        //IemInfo.IemBasicInfo.NativeTEL = CurrentInpatient.PersonalInformation.DomiciliaryInfo.PhoneNo;
                        //IemInfo.IemBasicInfo.NativePost = CurrentInpatient.PersonalInformation.DomiciliaryInfo.Postalcode;

                    }
                    if (CurrentInpatient.PersonalInformation.Nation != null)
                    {
                        //IemInfo.IemBasicInfo.NationID = CurrentInpatient.PersonalInformation.Nation.Code;
                        //IemInfo.IemBasicInfo.NationName = CurrentInpatient.PersonalInformation.Nation.Name;
                    }

                    if (CurrentInpatient.PersonalInformation.LinkManInfo != null)
                    {
                        //IemInfo.IemBasicInfo.ContactPerson = CurrentInpatient.PersonalInformation.LinkManInfo.Name;
                        //IemInfo.IemBasicInfo.RelationshipID = CurrentInpatient.PersonalInformation.LinkManInfo.Relation.Code;
                        //IemInfo.IemBasicInfo.RelationshipName = CurrentInpatient.PersonalInformation.LinkManInfo.Relation.Name;
                        //IemInfo.IemBasicInfo.ContactAddress = CurrentInpatient.PersonalInformation.LinkManInfo.ContactAddress.FullAddress;
                        //IemInfo.IemBasicInfo.ContactTEL = CurrentInpatient.PersonalInformation.LinkManInfo.ContactAddress.PhoneNo;
                    }
                }
                if (CurrentInpatient.Recorder != null)
                    //IemInfo.IemBasicInfo.IDNO = CurrentInpatient.Recorder.IdentityNo;

                    #region 抓取有关病人入院信息的数据
                    DtInfoOfAdmission = GetPaientInfoData();
                if (DtInfoOfAdmission.Rows.Count > 0)
                {
                    IemInfo.IemBasicInfo.AdmitDate = DtInfoOfAdmission.Rows[0]["AdmitDate"].ToString();
                    IemInfo.IemBasicInfo.OutWardDate = DtInfoOfAdmission.Rows[0]["OutWardDate"].ToString();
                    IemInfo.IemBasicInfo.AdmitDeptID = DtInfoOfAdmission.Rows[0]["AdmitDept"].ToString();
                    IemInfo.IemBasicInfo.AdmitDeptName = DtInfoOfAdmission.Rows[0]["AdmitDeptName"].ToString();
                    IemInfo.IemBasicInfo.AdmitWardID = DtInfoOfAdmission.Rows[0]["AdmitWard"].ToString();
                    IemInfo.IemBasicInfo.AdmitWardName = DtInfoOfAdmission.Rows[0]["AdmitWardName"].ToString();
                    IemInfo.IemDiagInfo.Vs_EmployeeID = DtInfoOfAdmission.Rows[0]["ATTEND"].ToString();//主治医师
                    IemInfo.IemBasicInfo.OutHosDeptID = DtInfoOfAdmission.Rows[0]["OutHosDeptID"].ToString();
                    IemInfo.IemBasicInfo.OutHosDeptName = DtInfoOfAdmission.Rows[0]["outdeptname"].ToString();
                    IemInfo.IemBasicInfo.OutHosWardID = DtInfoOfAdmission.Rows[0]["OutWardID"].ToString();
                    IemInfo.IemBasicInfo.OutHosWardName = DtInfoOfAdmission.Rows[0]["OutWardName"].ToString();
                    IemInfo.IemDiagInfo.Resident_EmployeeID = DtInfoOfAdmission.Rows[0]["RESIDENT"].ToString();//住院医师
                    IemInfo.IemDiagInfo.Section_DirectorID = DtInfoOfAdmission.Rows[0]["CHIEF"].ToString();//科主任
                }
                //对于患者是婴儿，且年龄不足一周岁的，计算显示出年龄是多少个月 add by ywk 
                //string monthAge = PatientBasicInfo.CalcDisplayAge(DateTime.Parse(IemInfo.IemBasicInfo.Birth), DateTime.Parse(IemInfo.IemBasicInfo.AdmitDate));
                string monthAge = PatientBasicInfo.CalcDisplayAge(DateTime.Parse(IemInfo.IemBasicInfo.Birth), DateTime.Now);
                if (!monthAge.Contains("岁"))
                {
                    IemInfo.IemBasicInfo.MonthAge = monthAge;
                }
                //如果入院和出院时间都不为空，则算出住院天数 
                if (!string.IsNullOrEmpty(IemInfo.IemBasicInfo.AdmitDate) && !string.IsNullOrEmpty(IemInfo.IemBasicInfo.OutWardDate))
                {
                    int datediff = (DateTime.Parse(IemInfo.IemBasicInfo.OutWardDate) - DateTime.Parse(IemInfo.IemBasicInfo.AdmitDate)).Days;
                    if (datediff > 0)
                    {
                        IemInfo.IemBasicInfo.ActualDays = datediff.ToString();
                    }
                    else
                    {
                        IemInfo.IemBasicInfo.ActualDays = "";
                    }
                }


                    #endregion



                #region old
                if (CurrentInpatient.InfoOfAdmission != null)
                {
                    //if (CurrentInpatient.InfoOfAdmission.AdmitInfo.StepOneDate.CompareTo(DateTime.MinValue) != 0)
                    //{
                    //    IemInfo.IemBasicInfo.AdmitDate = CurrentInpatient.InfoOfAdmission.AdmitInfo.StepOneDate.ToString("yyyy-MM-dd") + " " + CurrentInpatient.InfoOfAdmission.AdmitInfo.StepOneDate.ToString("HH:mm:ss");

                    //}

                    //if (CurrentInpatient.InfoOfAdmission.DischargeInfo.StepOneDate.CompareTo(DateTime.MinValue) != 0)
                    //{

                    //    IemInfo.IemBasicInfo.OutWardDate = CurrentInpatient.InfoOfAdmission.DischargeInfo.StepOneDate.ToString("yyyy-MM-dd") + " " + CurrentInpatient.InfoOfAdmission.DischargeInfo.StepOneDate.ToString("HH:mm:ss");


                    //}

                    //IemInfo.IemBasicInfo.AdmitDeptID = CurrentInpatient.InfoOfAdmission.AdmitInfo.CurrentDepartment.Code;
                    //IemInfo.IemBasicInfo.AdmitDeptName = CurrentInpatient.InfoOfAdmission.AdmitInfo.CurrentDepartment.Name;

                    //IemInfo.IemBasicInfo.AdmitWardID = CurrentInpatient.InfoOfAdmission.AdmitInfo.CurrentWard.Code;
                    //IemInfo.IemBasicInfo.AdmitWardName = CurrentInpatient.InfoOfAdmission.AdmitInfo.CurrentWard.Name;

                    ////IemInfo.IemBasicInfo.OutHosDeptID = CurrentInpatient.in.AdmitInfo.CurrentDepartment.Code;
                    ////IemInfo.IemBasicInfo.OutHosDeptName = CurrentInpatient.InfoOfAdmission.AdmitInfo.CurrentDepartment.Name;

                    ////IemInfo.IemBasicInfo.OutHosWardID = CurrentInpatient.InfoOfAdmission.AdmitInfo.CurrentWard.Code;
                    ////IemInfo.IemBasicInfo.OutHosWardName = CurrentInpatient.InfoOfAdmission.AdmitInfo.CurrentWard.Name;

                    //IemInfo.IemDiagInfo.Vs_EmployeeID = CurrentInpatient.InfoOfAdmission.AttendingPhysician.Code;
                    //IemInfo.IemDiagInfo.Vs_EmployeeName = CurrentInpatient.InfoOfAdmission.AttendingPhysician.Name;

                    //IemInfo.IemDiagInfo.Resident_EmployeeID = CurrentInpatient.InfoOfAdmission.Resident.Code;
                    //IemInfo.IemDiagInfo.Resident_EmployeeName = CurrentInpatient.InfoOfAdmission.Resident.Name;

                    //IemInfo.IemDiagInfo.Section_DirectorID = CurrentInpatient.InfoOfAdmission.Director.Code;
                    //IemInfo.IemDiagInfo.Section_DirectorName = CurrentInpatient.InfoOfAdmission.Director.Name;
                }
                #endregion
                #endregion




            }
            #endregion

            if (IemInfo.IemBasicInfo.IsBaby == "1")//如果是婴儿
            {
                IemInfo.IemBasicInfo.MotherPatOfHis = GetPatData(IemInfo.IemBasicInfo.Mother).Rows[0]["patnoofhis"].ToString();
            }
        }
        DataTable DtInfoOfAdmission;
        /// <summary>
        /// 抓取有关病人的入院信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetPaientInfoData()
        {
            string sql = string.Format(@"
select admitdept,admitdate,admitward,admitbed,outwarddate,ATTEND,RESIDENT,CHIEF,dep.name admitdeptname ,w.name admitwardname,u1.name ATTENDName,u2.name RESIDENTName,u3.name CHIEFName,
dep1.name outdeptname,i.outhosdept OutHosDeptID,w1.name OutWardName,i.outhosward OutWardID from 
INPATIENT i left join department dep on i.admitdept=dep.id left join ward w on i.admitward=w.id left join users u1 on i.ATTEND=u1.id 
left join users u2 on i.RESIDENT=u2.id left join users u3 on i.CHIEF =u3.id  left join department dep1 on dep1.id=i.outhosdept  
left join ward w1 on i.outhosward=w1.id where noofinpat='{0}'  ", CurrentInpatient.NoOfFirstPage);
            return m_app.SqlHelper.ExecuteDataTable(sql, CommandType.Text);

        }

        /// <summary>
        /// 提取病人费用信息
        /// </summary>
        /// <param name="_IemInfo"></param>
        /// <returns></returns>
        public Iem_MainPage_Fee GetIemFeeInfo(IemMainPageInfo _IemInfo)
        {
            //add by yxy
            return new Iem_MainPage_Fee();

            /*if (_IemInfo == null || _IemInfo.IemBasicInfo == null)
                return new Iem_MainPage_Fee();
            IDataAccess sqlHelper = DataAccessFactory.GetSqlDataAccess("HISDB");

            if (sqlHelper == null)
            {
                m_app.CustomMessageBox.MessageShow("无法连接到HIS！", CustomMessageBoxKind.ErrorOk);
                return new Iem_MainPage_Fee();
            }
            //to do  yxy 提取HIS数据库中病人费用信息

            string sql = string.Format(@"SELECT     CONVERT(varchar(12), PatCode) AS PatID,FeeCode,
                                             CONVERT(varchar(12), FeeName) AS FeeName, CONVERT(float, SUM(Amount)) AS amount
                                            FROM  root.InnerRecipeCount WITH (nolock)
                                            where PatCode = '{0}'
                                            GROUP BY PatCode, FeeName,FeeCode", m_app.CurrentPatientInfo.NoOfHisFirstPage);//m_App.CurrentPatientInfo.NoOfHisFirstPage);
            //SqlParameter[] paraColl = new SqlParameter[] { new SqlParameter("@syxh", m_App.CurrentPatientInfo.NoOfHisFirstPage ) };
            //DataTable dataTable = sqlHelper.ExecuteDataTable("usp_bq_fymxcx", paraColl, CommandType.StoredProcedure);

            DataTable dataTable = sqlHelper.ExecuteDataTable(sql, CommandType.Text);
            //to do 赋值
            //to do 赋值
            Double totalFee = 0;
            Double OtherFee = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                totalFee = totalFee + Convert.ToDouble(row["amount"].ToString());
                OtherFee = OtherFee + Convert.ToDouble(row["amount"].ToString());

                //床费
                if (row["FeeName"].ToString().Trim() == "床位费")
                {
                    //_IemInfo.IemFeeInfo.Bed = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //护理费
                else if (row["FeeName"].ToString().Trim() == "护理费")
                {
                    _IemInfo.IemFeeInfo.Care = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //西药费
                else if (row["FeeName"].ToString().Trim() == "西药费")
                {
                    //_IemInfo.IemFeeInfo.WMedical = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }

                //中成药费
                else if (row["FeeName"].ToString().Trim() == "中成药费")
                {
                    //_IemInfo.IemFeeInfo.CPMedical = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //中草药费
                else if (row["FeeName"].ToString().Trim() == "草药费")
                {
                    //_IemInfo.IemFeeInfo.CMedical = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //放射费
                else if (row["FeeName"].ToString().Trim() == "放射费")
                {
                    //_IemInfo.IemFeeInfo.Radiate = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //检验
                else if (row["FeeName"].ToString().Trim() == "其它")
                {
                    //_IemInfo.IemFeeInfo.Assay = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //输氧费
                else if (row["FeeName"].ToString().Trim() == "输氧费")
                {
                    //_IemInfo.IemFeeInfo.Ox = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }

                //输血费
                else if (row["FeeName"].ToString().Trim() == "输血费")
                {
                    //_IemInfo.IemFeeInfo.Blood = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //诊查费
                else if (row["FeeName"].ToString().Trim() == "诊查费")
                {
                    //_IemInfo.IemFeeInfo.Mecical = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //手术费
                else if (row["FeeName"].ToString().Trim() == "手术费")
                {
                    //_IemInfo.IemFeeInfo.Operation = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //接生费
                else if (row["FeeName"].ToString().Trim() == "接生费")
                {
                    //_IemInfo.IemFeeInfo.Accouche = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //检查费
                else if (row["FeeName"].ToString().Trim() == "检查费")
                {
                    //_IemInfo.IemFeeInfo.Ris = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }

                //麻醉费
                else if (row["FeeName"].ToString().Trim() == "麻醉费")
                {
                    //_IemInfo.IemFeeInfo.Anaesthesia = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //治疗费
                else if (row["FeeName"].ToString().Trim() == "治疗费")
                {
                    //_IemInfo.IemFeeInfo.Others2 = row["amount"].ToString();
                    OtherFee = OtherFee - Convert.ToDouble(row["amount"].ToString());
                }
                //婴儿费   陪床费  药占比  检验费 未匹配


            }

            _IemInfo.IemFeeInfo.Total = totalFee.ToString();

            //_IemInfo.IemFeeInfo.Others3 = OtherFee.ToString();

            return _IemInfo.IemFeeInfo;
             */
        }

        /// <summary>
        /// 诊断
        /// </summary>
        /// <param name="dataTable"></param>
        private void GetItemDiagInfo(DataTable dataTable)
        {
            DataTable dt = IemInfo.IemDiagInfo.OutDiagTable;
            dt.Rows.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                #region 赋值

                DataRow dr = dt.NewRow();
                dr["Diagnosis_Name"] = row["Diagnosis_Name"].ToString();
                dr["Status_Id"] = row["Status_Id"].ToString();
                dr["Status_Name"] = row["Status_Name"].ToString();
                dr["Diagnosis_Type_Id"] = row["Diagnosis_Type_Id"].ToString();
                dr["Diagnosis_Code"] = row["Diagnosis_Code"].ToString();
                dt.Rows.Add(dr);

                if (dr["Diagnosis_Type_Id"].ToString() == "13")
                {
                    IemInfo.IemDiagInfo.OutDiagID = row["Diagnosis_Code"].ToString();

                    IemInfo.IemDiagInfo.OutDiagName = row["Diagnosis_Name"].ToString();
                }
                else if (dr["Diagnosis_Type_Id"].ToString() == "2")
                {
                    IemInfo.IemDiagInfo.InDiagID = row["Diagnosis_Code"].ToString();
                    IemInfo.IemDiagInfo.InDiagName = row["Diagnosis_Name"].ToString();
                }

                //新增的几个诊断
                dr["MenAndInHop"] = row["MenAndInHop"].ToString();
                dr["InHopAndOutHop"] = row["InHopAndOutHop"].ToString();
                dr["BeforeOpeAndAfterOper"] = row["BeforeOpeAndAfterOper"].ToString();
                dr["LinAndBingLi"] = row["LinAndBingLi"].ToString();
                dr["InHopThree"] = row["InHopThree"].ToString();
                dr["FangAndBingLi"] = row["FangAndBingLi"].ToString();


                dr["MenAndInHopName"] = row["MenAndInHopName"].ToString();
                dr["InHopAndOutHopName"] = row["InHopAndOutHopName"].ToString();
                dr["BeforeOpeAndAfterOperName"] = row["BeforeOpeAndAfterOperName"].ToString();
                dr["LinAndBingLiName"] = row["LinAndBingLiName"].ToString();
                dr["InHopThreeName"] = row["InHopThreeName"].ToString();
                dr["FangAndBingLiName"] = row["FangAndBingLiName"].ToString();
                dr["AdmitInfo"] = row["AdmitInfo"].ToString();
                dr["AdmitInfoName"] = row["AdmitInfoName"].ToString();
                #endregion
            }
            //IemInfo.IemDiagInfo.OutDiagID == "" ? IemInfo.IemBasicInfo.ou : IemInfo.IemDiagInfo.OutDiagID;


            IemInfo.IemDiagInfo.OutDiagTable = dt;
        }

        private void GetItemObsBaby(DataTable dataTable)
        {
            string dept = m_DataHelper.GetInpatientDept(CurrentInpatient.NoOfFirstPage.ToString());

            string config = m_DataHelper.GetConfigValueByKey("ShowObstetricsBabyDept");
            if (config.IndexOf(dept) < 0)
            {
                IemInfo.IemObstetricsBaby = null;
                return;
            }

            IemInfo.IemObstetricsBaby.OutBabyTable.Rows.Clear();

            int index = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                #region 赋值
                DataRow dr = IemInfo.IemObstetricsBaby.OutBabyTable.NewRow();
                dr["TC"] = row["TC"];
                dr["CC"] = row["CC"];
                dr["TB"] = row["TB"];
                dr["CFHYPLD"] = row["CFHYPLD"];
                dr["MIDWIFERY"] = row["Midwifery"];
                dr["SEX"] = row["Sex"];
                dr["APJ"] = row["APJ"];
                dr["HEIGH"] = row["Heigh"];
                dr["WEIGHT"] = row["Weight"];
                dr["CCQK"] = row["CCQK"];
                dr["BITHDAY"] = row["BithDay"];
                dr["FMFS"] = row["FMFS"];
                dr["CYQK"] = row["CYQK"];
                dr["CREATE_USER"] = row["CYQK"];
                dr["CREATE_TIME"] = row["CREATE_TIME"];
                dr["VALIDE"] = row["VALIDE"];
                dr["IBSBABYID"] = row["IBSBABYID"];
                IemInfo.IemObstetricsBaby.OutBabyTable.Rows.Add(dr);
                if (index == 0)
                {
                    IemInfo.IemObstetricsBaby.IEM_MainPage_NO = row["Iem_Mainpage_NO"].ToString();
                    IemInfo.IemObstetricsBaby.IBSBABYID = row["IBSBABYID"].ToString();
                    IemInfo.IemObstetricsBaby.TC = row["TC"].ToString();
                    IemInfo.IemObstetricsBaby.TB = row["TB"].ToString();
                    IemInfo.IemObstetricsBaby.CC = row["CC"].ToString();
                    IemInfo.IemObstetricsBaby.CFHYPLD = row["CFHYPLD"].ToString();
                    IemInfo.IemObstetricsBaby.Midwifery = row["Midwifery"].ToString();
                    IemInfo.IemObstetricsBaby.Sex = row["Sex"].ToString();

                    IemInfo.IemObstetricsBaby.APJ = row["APJ"].ToString();
                    IemInfo.IemObstetricsBaby.Heigh = row["Heigh"].ToString();
                    IemInfo.IemObstetricsBaby.Weight = row["Weight"].ToString();
                    IemInfo.IemObstetricsBaby.BithDay = row["BithDay"].ToString();
                    IemInfo.IemObstetricsBaby.CCQK = row["CCQK"].ToString();

                    IemInfo.IemObstetricsBaby.CYQK = row["CYQK"].ToString();
                    IemInfo.IemObstetricsBaby.FMFS = row["FMFS"].ToString();
                    IemInfo.IemObstetricsBaby.IEM_MainPage_ObstetricsBabyID = row["IEM_MAINPAGE_OBSBABYID"].ToString();
                }
                index++;


                #endregion
            }
        }

        #endregion

        #endregion

        #region 保存
        private string CanEdit { get { return m_DataHelper.GetConfigValueByKey("IsOpenSetPaientBaseInfo"); } }

        private string IsUpdateIdNO { get { return m_DataHelper.GetConfigValueByKey("IsUpdateIDNO"); } }

        public void SaveData(IemMainPageInfo m_iemInfo)
        {
            try
            {
                if (CurrentInpatient == null)
                    return;
                IemInfo = m_iemInfo;
                if (CanEdit == "1")//可以编辑再进行更新表
                {
                    UpdateInPaient(IemInfo, IsUpdateIdNO, m_app.SqlHelper);
                }

                if (IemInfo.IemBasicInfo.Iem_Mainpage_NO == "")//病案首页基本表中没有
                    //InsertIemInfo();
                    InsertIemInfo_2012();
                else
                    UpdateItemInfo_2012();

                //edit by ywk 
                //string canedit = m_DataHelper.GetConfigValueByKey("IsOpenSetPaientBaseInfo");


            }
            catch (Exception ex)
            {
                //m_Logger.Error(ex);
            }
            finally
            {
                //更新了病案首页的信息，也要把Inpatient表中的信息相应进行同步
                ////edit by ywk 
                //string canedit = m_DataHelper.GetConfigValueByKey("IsOpenSetPaientBaseInfo");
                //if (canedit == "1")//可以编辑再进行更新表
                //{
                //    string isupdateIdno = m_DataHelper.GetConfigValueByKey("IsUpdateIDNO");
                //    //if (isupdateIdno=="1")//进行更新 noofclinic字段
                //    //{

                //    //}
                //    UpdateInPaient(m_iemInfo, isupdateIdno, m_app.SqlHelper);
                //}
            }
        }


        /// <summary>
        /// 保存首页信息
        /// </summary>
        /// <param name="info"></param>
        private void InsertIemInfo_2012()
        {

            try
            {
                m_app.SqlHelper.BeginTransaction();

                EditIemBasicInfo_2012(this.IemInfo, "1");

                foreach (DataRow item in this.IemInfo.IemDiagInfo.OutDiagTable.Rows)
                    InserIemDiagnoseInfo_2012(item, m_app.SqlHelper, IemInfo.IemDiagInfo);

                foreach (DataRow item in this.IemInfo.IemOperInfo.Operation_Table.Rows)
                    InserIemOperInfo_2012(item, m_app.SqlHelper);

                //插入产妇婴儿情况
                foreach (DataRow item in this.IemInfo.IemObstetricsBaby.OutBabyTable.Rows)
                    InsertIemObstetricsBaby(item, m_app.SqlHelper);

                //InsertIemObstetricsBaby(this.IemInfo.IemObstetricsBaby, m_app.SqlHelper);

                // 新增的将信息加到病案首页的费用表中 add by ywk 2012年10月16日 18:50:06 

                EditIemFeeInfo_2012(this.IemInfo, "1");

                //新增诊断 抢救等信息 add by 王冀 2012 12 4
                EditIemOthersJS(this.IemInfo, "1");

                m_app.SqlHelper.CommitTransaction();

                m_app.CustomMessageBox.MessageShow("保存成功");
                GetIemInfo();
            }
            catch (Exception ex)
            {
                m_app.SqlHelper.RollbackTransaction();
            }

        }
        /// <summary>
        /// 将病案首页的费用信息加到电子病历的表中
        /// add  by ywk 2012年10月16日 18:52:46
        /// </summary>
        /// <param name="iemMainPageInfo"></param>
        /// <param name="p"></param>
        private void EditIemFeeInfo_2012(IemMainPageInfo iemInfo, string type)
        {
            #region 各个参数 edit by ywk 2012年10月16日 19:15:12
            SqlParameter paraedittype = new SqlParameter("@edittype", SqlDbType.VarChar, 14);
            paraedittype.Value = type;
            SqlParameter paraIem_Mainpage_NO = new SqlParameter("@Iem_Mainpage_NO", SqlDbType.VarChar, 12);
            paraIem_Mainpage_NO.Value = IemInfo.IemBasicInfo.Iem_Mainpage_NO;
            SqlParameter paraTotalFee = new SqlParameter("@TotalFee", SqlDbType.VarChar, 50);
            paraTotalFee.Value = iemInfo.IemFeeInfo.Total;// 总费用
            SqlParameter paraOwnerFee = new SqlParameter("@OwnerFee", SqlDbType.VarChar, 50);
            paraOwnerFee.Value = iemInfo.IemFeeInfo.OwnFee;//自付金额
            SqlParameter paraYbMedServFee = new SqlParameter("@YbMedServFee", SqlDbType.VarChar, 50);
            paraYbMedServFee.Value = iemInfo.IemFeeInfo.YBYLFY;//一般医疗服务费
            SqlParameter paraYbMedOperFee = new SqlParameter("@YbMedOperFee", SqlDbType.VarChar, 50);
            paraYbMedOperFee.Value = iemInfo.IemFeeInfo.YBZLFY;//一般治疗操作费
            SqlParameter paraNurseFee = new SqlParameter("@NurseFee", SqlDbType.VarChar, 50);
            paraNurseFee.Value = iemInfo.IemFeeInfo.Care;//护理费
            SqlParameter paraOtherInfo = new SqlParameter("@OtherInfo", SqlDbType.VarChar, 50);
            paraOtherInfo.Value = iemInfo.IemFeeInfo.ZHQTFY;// 综合类 其他费用
            SqlParameter paraBLZDFee = new SqlParameter("@BLZDFee", SqlDbType.VarChar, 50);
            paraBLZDFee.Value = iemInfo.IemFeeInfo.BLZDF;// 诊断类 病理诊断费
            SqlParameter paraSYSZDFee = new SqlParameter("@SYSZDFee", SqlDbType.VarChar, 50);
            paraSYSZDFee.Value = iemInfo.IemFeeInfo.SYSZDF;// 实验室诊断费
            SqlParameter paraYXXZDFee = new SqlParameter("@YXXZDFee", SqlDbType.VarChar, 50);
            paraYXXZDFee.Value = iemInfo.IemFeeInfo.YXXZDF;// 诊断类 影像学诊断费
            SqlParameter paraLCZDItemFee = new SqlParameter("@LCZDItemFee", SqlDbType.VarChar, 50);
            paraLCZDItemFee.Value = iemInfo.IemFeeInfo.LCZDF;// 诊断类 临床诊断项目费
            SqlParameter paraFSSZLItemFee = new SqlParameter("@FSSZLItemFee", SqlDbType.VarChar, 50);
            paraFSSZLItemFee.Value = iemInfo.IemFeeInfo.FSSZLF;// 非手术治疗项目费
            SqlParameter paraLCWLZLFee = new SqlParameter("@LCWLZLFee", SqlDbType.VarChar, 50);
            paraLCWLZLFee.Value = iemInfo.IemFeeInfo.LCWLZLF;//治疗类 临床物理治疗费
            SqlParameter paraOperMedFee = new SqlParameter("@OperMedFee", SqlDbType.VarChar, 50);
            paraOperMedFee.Value = iemInfo.IemFeeInfo.SSZLF;//治疗类 手术治疗费
            SqlParameter paraKFFee = new SqlParameter("@KFFee", SqlDbType.VarChar, 50);
            paraKFFee.Value = iemInfo.IemFeeInfo.KFF;//康复类 康复费
            SqlParameter paraZYZLFee = new SqlParameter("@ZYZLFee", SqlDbType.VarChar, 50);
            paraZYZLFee.Value = iemInfo.IemFeeInfo.ZYZLF;//中医类 中医治疗费
            SqlParameter paraXYMedFee = new SqlParameter("@XYMedFee", SqlDbType.VarChar, 50);
            paraXYMedFee.Value = iemInfo.IemFeeInfo.XYF;//西药类 西药费
            SqlParameter paraKJYWFee = new SqlParameter("@KJYWFee", SqlDbType.VarChar, 50);
            paraKJYWFee.Value = iemInfo.IemFeeInfo.KJYWF;//西药类 抗菌药物费用
            SqlParameter paraZCYFFee = new SqlParameter("@ZCYFFee", SqlDbType.VarChar, 50);
            paraZCYFFee.Value = iemInfo.IemFeeInfo.CPMedical;//中药类 中成药费
            SqlParameter paraZCaoYFFee = new SqlParameter("@ZCaoYFFee", SqlDbType.VarChar, 50);
            paraZCaoYFFee.Value = iemInfo.IemFeeInfo.CMedical;//中药类 中草药费
            SqlParameter paraBloodFee = new SqlParameter("@BloodFee", SqlDbType.VarChar, 50);
            paraBloodFee.Value = iemInfo.IemFeeInfo.BloodFee;//血液和血液制品类 血费
            SqlParameter paraBDBLZPFFee = new SqlParameter("@BDBLZPFFee", SqlDbType.VarChar, 50);
            paraBDBLZPFFee.Value = iemInfo.IemFeeInfo.XDBLZPF;//血液和血液制品类 白蛋白类制品费
            SqlParameter paraQDBLZPFFee = new SqlParameter("@QDBLZPFFee", SqlDbType.VarChar, 50);
            paraQDBLZPFFee.Value = iemInfo.IemFeeInfo.QDBLZPF;//血液和血液制品类 球蛋白类制品费
            SqlParameter paraNXYZLZPFFee = new SqlParameter("@NXYZLZPFFee", SqlDbType.VarChar, 50);
            paraNXYZLZPFFee.Value = iemInfo.IemFeeInfo.NXYZLZPF;//血液和血液制品类 凝血因子类制品费
            SqlParameter paraXBYZLZPFFee = new SqlParameter("@XBYZLZPFFee", SqlDbType.VarChar, 50);
            paraXBYZLZPFFee.Value = iemInfo.IemFeeInfo.XBYZLZPF;//血液和血液制品类 细胞因子类制品费
            SqlParameter paraJCYYCXYYCLFFee = new SqlParameter("@JCYYCXYYCLFFee", SqlDbType.VarChar, 50);
            paraJCYYCXYYCLFFee.Value = iemInfo.IemFeeInfo.JCYYCXCLF;//耗材类 检查用一次性医用材料费
            SqlParameter paraZLYYCXYYCLFFee = new SqlParameter("@ZLYYCXYYCLFFee", SqlDbType.VarChar, 50);
            paraZLYYCXYYCLFFee.Value = iemInfo.IemFeeInfo.ZLYYCXCLF;//耗材类 治疗用一次性医用材料费
            SqlParameter paraSSYYCXYYCLFFee = new SqlParameter("@SSYYCXYYCLFFee", SqlDbType.VarChar, 50);
            paraSSYYCXYYCLFFee.Value = iemInfo.IemFeeInfo.JCYYCXCLF;// 耗材类 手术用一次性医用材料费
            SqlParameter paraQTFee = new SqlParameter("@QTFee", SqlDbType.VarChar, 50);
            paraQTFee.Value = iemInfo.IemFeeInfo.OtherFee;//其他类：（24）其他费
            SqlParameter paraMemo1 = new SqlParameter("@Memo1", SqlDbType.VarChar, 50);
            paraMemo1.Value = "";//预留字段
            SqlParameter paraMemo2 = new SqlParameter("@Memo2", SqlDbType.VarChar, 50);
            paraMemo2.Value = "";//预留字段
            SqlParameter paraMemo3 = new SqlParameter("@Memo3", SqlDbType.VarChar, 50);
            paraMemo3.Value = "";//预留字段
            SqlParameter paraMaZuiFee = new SqlParameter("@MaZuiFee", SqlDbType.VarChar, 50);
            paraMaZuiFee.Value = iemInfo.IemFeeInfo.MZF;//麻醉费
            SqlParameter paraShouShuFee = new SqlParameter("@ShouShuFee", SqlDbType.VarChar, 50);
            paraShouShuFee.Value = iemInfo.IemFeeInfo.SSF;//手术费
            #endregion
            SqlParameter[] paraColl = new SqlParameter[] { paraedittype, paraIem_Mainpage_NO, paraTotalFee, paraOwnerFee, paraYbMedServFee, 
            paraYbMedOperFee,paraNurseFee,paraOtherInfo,paraBLZDFee,paraSYSZDFee,paraYXXZDFee,paraLCZDItemFee,paraFSSZLItemFee,
            paraLCWLZLFee,paraOperMedFee,paraKFFee,paraZYZLFee,paraXYMedFee,paraKJYWFee,paraZCYFFee,paraZCaoYFFee,paraBloodFee,
            paraBDBLZPFFee,paraQDBLZPFFee,paraNXYZLZPFFee,paraXBYZLZPFFee,paraJCYYCXYYCLFFee,paraZLYYCXYYCLFFee,paraSSYYCXYYCLFFee,
            paraQTFee,paraMemo1,paraMemo2,paraMemo3,paraMaZuiFee,paraShouShuFee};
            try
            {
                m_app.SqlHelper.ExecuteNoneQuery("IEM_MAIN_PAGE.usp_editiem_mainpage_feeinfo", paraColl, CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void EditIemOthersJS(IemMainPageInfo iemInfo, string type)
        {

            string MAIN_DIAGNOSIS_CURECONDITION = iemInfo.IemOthers.Main_Diagnosis_Curecondition;
            string DIAGNOSIS_CONDITIONS1 = iemInfo.IemOthers.Diagnosis_conditions1;
            string DIAGNOSIS_CONDITIONS2 = iemInfo.IemOthers.Diagnosis_conditions2;
            string DIAGNOSIS_CONDITIONS3 = iemInfo.IemOthers.Diagnosis_conditions3;
            string DIAGNOSIS_CONDITIONS4 = iemInfo.IemOthers.Diagnosis_conditions4;
            string DIAGNOSIS_CONDITIONS5 = iemInfo.IemOthers.Diagnosis_conditions5;
            string EMERGENCY_TIMES = iemInfo.IemOthers.Emergency_times;
            string EMERGENCY_SUCCESSFUL_TIMES = iemInfo.IemOthers.Emergency_Successful_times;
            string CP_STATUS = iemInfo.IemOthers.CP_status;
            string IEM_MAINPAGE_NO = iemInfo.IemBasicInfo.Iem_Mainpage_NO;
            string sql = "";
            if (type == "1")
            {
                string CREAT_USER = iemInfo.IemOthers.Creat_user;
                sql = string.Format(@"insert into IEM_MAINPAGE_OTHER_2012(IEM_MAINPAGE_OTHER_NO,IEM_MAINPAGE_NO,
MAIN_DIAGNOSIS_CURECONDITION,
DIAGNOSIS_CONDITIONS1,DIAGNOSIS_CONDITIONS2,DIAGNOSIS_CONDITIONS3,DIAGNOSIS_CONDITIONS4,DIAGNOSIS_CONDITIONS5,
EMERGENCY_TIMES,EMERGENCY_SUCCESSFUL_TIMES,
CP_STATUS,CREATE_TIME,CREAT_USER,VALID) 
values(seq_iem_mainpage_others.nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',sysdate,'{10}',1)",
          IEM_MAINPAGE_NO, MAIN_DIAGNOSIS_CURECONDITION, DIAGNOSIS_CONDITIONS1,
          DIAGNOSIS_CONDITIONS2, DIAGNOSIS_CONDITIONS3, DIAGNOSIS_CONDITIONS4,
          DIAGNOSIS_CONDITIONS5, EMERGENCY_TIMES, EMERGENCY_SUCCESSFUL_TIMES,
          CP_STATUS, CREAT_USER);

            }
            if (type == "2")
            {
                string MODIFY_USER = iemInfo.IemOthers.Creat_user;
                sql = string.Format(@"update IEM_MAINPAGE_OTHER_2012 
set MAIN_DIAGNOSIS_CURECONDITION='{0}',
DIAGNOSIS_CONDITIONS1='{1}',
DIAGNOSIS_CONDITIONS2='{2}',
DIAGNOSIS_CONDITIONS3='{3}',
DIAGNOSIS_CONDITIONS4='{4}',
DIAGNOSIS_CONDITIONS5='{5}',
EMERGENCY_TIMES='{6}',
EMERGENCY_SUCCESSFUL_TIMES='{7}',
CP_STATUS='{8}',
MODIFY_USER='{9}',
MODIFY_TIME=sysdate 
where IEM_MAINPAGE_NO='{10}'",
                MAIN_DIAGNOSIS_CURECONDITION, DIAGNOSIS_CONDITIONS1, DIAGNOSIS_CONDITIONS2,
                DIAGNOSIS_CONDITIONS3, DIAGNOSIS_CONDITIONS4, DIAGNOSIS_CONDITIONS5,
                EMERGENCY_TIMES, EMERGENCY_SUCCESSFUL_TIMES,
                CP_STATUS, MODIFY_USER, IEM_MAINPAGE_NO);

            }
            try
            {
                m_app.SqlHelper.ExecuteNoneQuery(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region insert

        /// <summary>
        /// 编辑首页基本信息部分
        /// </summary>
        /// edit by ywk 2012年6月26日10:11:45
        /// <param name="info"></param>
        /// <param name="type"></param>
        private void EditIemBasicInfo_2012(IemMainPageInfo info, string type)
        {

            info.IemBasicInfo.Create_User = m_app.User.DoctorId;

            #region
            SqlParameter paraedittype = new SqlParameter("@edittype", SqlDbType.VarChar, 14);
            paraedittype.Value = type;
            SqlParameter paraIEM_MAINPAGE_NO = new SqlParameter("@IEM_MAINPAGE_NO", SqlDbType.VarChar, 14);
            paraIEM_MAINPAGE_NO.Value = info.IemBasicInfo.Iem_Mainpage_NO;
            SqlParameter paraPATNOOFHIS = new SqlParameter("@PATNOOFHIS", SqlDbType.VarChar, 14);
            paraPATNOOFHIS.Value = info.IemBasicInfo.PatNoOfHis;
            SqlParameter paraNoOfInpat = new SqlParameter("@NOOFINPAT", SqlDbType.VarChar, 9);
            paraNoOfInpat.Value = info.IemBasicInfo.NoOfInpat;
            SqlParameter paraPayID = new SqlParameter("@PayID", SqlDbType.VarChar, 4);
            paraPayID.Value = info.IemBasicInfo.PayID;
            SqlParameter paraSocialCare = new SqlParameter("@SocialCare", SqlDbType.VarChar, 32);
            paraSocialCare.Value = info.IemBasicInfo.SocialCare;

            SqlParameter paraInCount = new SqlParameter("@InCount", SqlDbType.VarChar, 4);
            paraInCount.Value = info.IemBasicInfo.InCount;
            SqlParameter paraName = new SqlParameter("@Name", SqlDbType.VarChar, 64);
            paraName.Value = info.IemBasicInfo.Name;
            SqlParameter paraSexID = new SqlParameter("@SexID", SqlDbType.VarChar, 4);
            paraSexID.Value = info.IemBasicInfo.SexID;
            SqlParameter paraBirth = new SqlParameter("@Birth", SqlDbType.VarChar, 10);
            paraBirth.Value = info.IemBasicInfo.Birth.ToString().Trim();
            SqlParameter paraMarital = new SqlParameter("@Marital", SqlDbType.VarChar, 4);
            paraMarital.Value = info.IemBasicInfo.Marital;

            SqlParameter paraJobID = new SqlParameter("@JobID", SqlDbType.VarChar, 4);
            paraJobID.Value = info.IemBasicInfo.JobID;
            SqlParameter paraNationalityID = new SqlParameter("@NationalityID", SqlDbType.VarChar, 4);
            paraNationalityID.Value = info.IemBasicInfo.NationalityID;
            SqlParameter paraNationID = new SqlParameter("@NationID", SqlDbType.VarChar, 4);
            paraNationID.Value = info.IemBasicInfo.NationID;
            SqlParameter paraIDNO = new SqlParameter("@IDNO", SqlDbType.VarChar, 18);
            paraIDNO.Value = info.IemBasicInfo.IDNO;
            SqlParameter paraOrganization = new SqlParameter("@Organization", SqlDbType.VarChar, 64);
            paraOrganization.Value = info.IemBasicInfo.Organization;
            SqlParameter paraOfficePlace = new SqlParameter("@OfficePlace", SqlDbType.VarChar, 64);
            paraOfficePlace.Value = info.IemBasicInfo.OfficePlace;

            SqlParameter paraOfficeTEL = new SqlParameter("@OfficeTEL", SqlDbType.VarChar, 16);
            paraOfficeTEL.Value = info.IemBasicInfo.OfficeTEL;
            SqlParameter paraOfficePost = new SqlParameter("@OfficePost", SqlDbType.VarChar, 16);
            paraOfficePost.Value = info.IemBasicInfo.OfficePost;
            SqlParameter paraContactPerson = new SqlParameter("@ContactPerson", SqlDbType.VarChar, 32);
            paraContactPerson.Value = info.IemBasicInfo.ContactPerson;
            SqlParameter paraRelationship = new SqlParameter("@Relationship", SqlDbType.VarChar, 4);
            paraRelationship.Value = info.IemBasicInfo.RelationshipID;
            SqlParameter paraContactAddress = new SqlParameter("@ContactAddress", SqlDbType.VarChar, 255);
            paraContactAddress.Value = info.IemBasicInfo.ContactAddress;

            SqlParameter paraContactTEL = new SqlParameter("@ContactTEL", SqlDbType.VarChar, 16);
            paraContactTEL.Value = info.IemBasicInfo.ContactTEL;
            SqlParameter paraAdmitDate = new SqlParameter("@AdmitDate", SqlDbType.VarChar, 19);
            paraAdmitDate.Value = info.IemBasicInfo.AdmitDate;
            SqlParameter paraAdmitDept = new SqlParameter("@AdmitDept", SqlDbType.VarChar, 12);
            paraAdmitDept.Value = info.IemBasicInfo.AdmitDeptID;
            SqlParameter paraAdmitWard = new SqlParameter("@AdmitWard", SqlDbType.VarChar, 12);
            paraAdmitWard.Value = info.IemBasicInfo.AdmitWardID;

            SqlParameter paraTrans_AdmitDept = new SqlParameter("@Trans_AdmitDept", SqlDbType.VarChar, 12);
            paraTrans_AdmitDept.Value = info.IemBasicInfo.Trans_AdmitDeptID;
            SqlParameter paraOutWardDate = new SqlParameter("@OutWardDate", SqlDbType.VarChar, 19);
            paraOutWardDate.Value = info.IemBasicInfo.OutWardDate;
            SqlParameter paraOutHosDept = new SqlParameter("@OutHosDept", SqlDbType.VarChar, 12);
            paraOutHosDept.Value = info.IemBasicInfo.OutHosDeptID;
            SqlParameter paraOutHosWard = new SqlParameter("@OutHosWard", SqlDbType.VarChar, 12);
            paraOutHosWard.Value = info.IemBasicInfo.OutHosWardID;

            SqlParameter paraActual_Days = new SqlParameter("@ACTUALDAYS", SqlDbType.VarChar, 4);
            paraActual_Days.Value = info.IemBasicInfo.ActualDays;
            SqlParameter paraPATHOLOGY_DIAGNOSIS_NAME = new SqlParameter("@PATHOLOGY_DIAGNOSIS_NAME", SqlDbType.VarChar, 19);
            paraPATHOLOGY_DIAGNOSIS_NAME.Value = info.IemDiagInfo.Pathology_Diagnosis_Name;
            SqlParameter paraPATHOLOGY_OBSERVATION_SN = new SqlParameter("@PATHOLOGY_OBSERVATION_SN", SqlDbType.VarChar, 300);
            paraPATHOLOGY_OBSERVATION_SN.Value = info.IemDiagInfo.Pathology_Observation_Sn;
            SqlParameter paraALLERGIC_DRUG = new SqlParameter("@ALLERGIC_DRUG", SqlDbType.VarChar, 19);
            paraALLERGIC_DRUG.Value = info.IemDiagInfo.Allergic_Drug;
            SqlParameter paraSection_Director = new SqlParameter("@SECTION_DIRECTOR", SqlDbType.VarChar, 20);
            paraSection_Director.Value = info.IemDiagInfo.Section_DirectorID;

            SqlParameter paraDirector = new SqlParameter("@Director", SqlDbType.VarChar, 20);
            paraDirector.Value = info.IemDiagInfo.DirectorID;
            SqlParameter paraVs_Employee_Code = new SqlParameter("@Vs_Employee_Code", SqlDbType.VarChar, 20);
            paraVs_Employee_Code.Value = info.IemDiagInfo.Vs_EmployeeID;
            SqlParameter paraResident_Employee_Code = new SqlParameter("@Resident_Employee_Code", SqlDbType.VarChar, 20);
            paraResident_Employee_Code.Value = info.IemDiagInfo.Resident_EmployeeID;
            SqlParameter paraRefresh_Employee_Code = new SqlParameter("@Refresh_Employee_Code", SqlDbType.VarChar, 20);
            paraRefresh_Employee_Code.Value = info.IemDiagInfo.Refresh_EmployeeID;
            SqlParameter paraDUTY_NURSE = new SqlParameter("@DUTY_NURSE", SqlDbType.VarChar, 20);
            paraDUTY_NURSE.Value = info.IemDiagInfo.Duty_NurseID;

            SqlParameter paraInterne = new SqlParameter("@Interne", SqlDbType.VarChar, 20);
            paraInterne.Value = info.IemDiagInfo.InterneID;
            SqlParameter paraCoding_User = new SqlParameter("@Coding_User", SqlDbType.VarChar, 20);
            paraCoding_User.Value = info.IemDiagInfo.Coding_UserID;
            SqlParameter paraMedical_Quality_Id = new SqlParameter("@Medical_Quality_Id", SqlDbType.VarChar, 20);
            paraMedical_Quality_Id.Value = info.IemDiagInfo.Medical_Quality_Id;
            SqlParameter paraQuality_Control_Doctor = new SqlParameter("@Quality_Control_Doctor", SqlDbType.VarChar, 20);
            paraQuality_Control_Doctor.Value = info.IemDiagInfo.Quality_Control_DoctorID;
            SqlParameter paraQuality_Control_Nurse = new SqlParameter("@Quality_Control_Nurse", SqlDbType.VarChar, 20);
            paraQuality_Control_Nurse.Value = info.IemDiagInfo.Quality_Control_NurseID;

            SqlParameter paraQuality_Control_Date = new SqlParameter("@Quality_Control_Date", SqlDbType.VarChar, 19);
            paraQuality_Control_Date.Value = info.IemDiagInfo.Quality_Control_Date;



            SqlParameter paraBLOODTYPE = new SqlParameter("@BLOODTYPE", SqlDbType.VarChar, 20);
            paraBLOODTYPE.Value = info.IemDiagInfo.BloodType;
            SqlParameter paraRH = new SqlParameter("@RH", SqlDbType.VarChar, 20);
            paraRH.Value = info.IemDiagInfo.Rh;
            SqlParameter paraIS_COMPLETED = new SqlParameter("@IS_COMPLETED", SqlDbType.VarChar, 20);
            paraIS_COMPLETED.Value = info.IemBasicInfo.Is_Completed;
            SqlParameter paraCOMPLETED_TIME = new SqlParameter("@COMPLETED_TIME", SqlDbType.VarChar, 20);
            paraCOMPLETED_TIME.Value = info.IemBasicInfo.completed_time;

            SqlParameter paraCREATE_USER = new SqlParameter("@CREATE_USER", SqlDbType.VarChar, 20);
            paraCREATE_USER.Value = info.IemBasicInfo.Create_User;
            SqlParameter paraCREATE_TIME = new SqlParameter("@CREATE_TIME", SqlDbType.VarChar, 20);
            paraCREATE_TIME.Value = info.IemBasicInfo.Create_Time;
            SqlParameter paraMODIFIED_USER = new SqlParameter("@MODIFIED_USER", SqlDbType.VarChar, 20);
            paraMODIFIED_USER.Value = info.IemBasicInfo.Modified_User;
            SqlParameter paraMODIFIED_TIME = new SqlParameter("@MODIFIED_TIME", SqlDbType.VarChar, 20);
            paraMODIFIED_TIME.Value = info.IemBasicInfo.Modified_Time;
            SqlParameter paraZYMOSIS = new SqlParameter("@ZYMOSIS", SqlDbType.VarChar, 20);
            paraZYMOSIS.Value = "";

            SqlParameter paraHURT_TOXICOSIS_ELE_ID = new SqlParameter("@HURT_TOXICOSIS_ELE_ID", SqlDbType.VarChar, 20);
            paraHURT_TOXICOSIS_ELE_ID.Value = info.IemDiagInfo.Hurt_Toxicosis_ElementID;
            SqlParameter paraHURT_TOXICOSIS_ELE_Name = new SqlParameter("@HURT_TOXICOSIS_ELE_Name", SqlDbType.VarChar, 20);
            paraHURT_TOXICOSIS_ELE_Name.Value = info.IemDiagInfo.Hurt_Toxicosis_Element;
            SqlParameter paraPATHOLOGY_DIAGNOSIS_ID = new SqlParameter("@PATHOLOGY_DIAGNOSIS_ID", SqlDbType.VarChar, 20);
            paraPATHOLOGY_DIAGNOSIS_ID.Value = info.IemDiagInfo.Pathology_Diagnosis_ID;
            SqlParameter paraMONTHAGE = new SqlParameter("@MONTHAGE", SqlDbType.VarChar, 20);
            paraMONTHAGE.Value = info.IemBasicInfo.MonthAge;
            SqlParameter paraWEIGHT = new SqlParameter("@WEIGHT", SqlDbType.VarChar, 20);
            paraWEIGHT.Value = info.IemBasicInfo.Weight;

            SqlParameter paraINWEIGHT = new SqlParameter("@INWEIGHT", SqlDbType.VarChar, 20);
            paraINWEIGHT.Value = info.IemBasicInfo.InWeight;
            SqlParameter paraINHOSTYPE = new SqlParameter("@INHOSTYPE", SqlDbType.VarChar, 20);
            paraINHOSTYPE.Value = info.IemBasicInfo.InHosType;
            SqlParameter paraOUTHOSTYPE = new SqlParameter("@OUTHOSTYPE", SqlDbType.VarChar, 20);
            paraOUTHOSTYPE.Value = info.IemBasicInfo.OutHosType;
            SqlParameter paraRECEIVEHOSPITAL = new SqlParameter("@RECEIVEHOSPITAL", SqlDbType.VarChar, 20);
            paraRECEIVEHOSPITAL.Value = info.IemBasicInfo.ReceiveHosPital;
            SqlParameter paraRECEIVEHOSPITAL2 = new SqlParameter("@RECEIVEHOSPITAL2", SqlDbType.VarChar, 20);
            paraRECEIVEHOSPITAL2.Value = info.IemBasicInfo.ReceiveHosPital2;

            SqlParameter paraAGAININHOSPITAL = new SqlParameter("@AGAININHOSPITAL", SqlDbType.VarChar, 20);
            paraAGAININHOSPITAL.Value = info.IemBasicInfo.AgainInHospital;
            SqlParameter paraAGAININHOSPITALREASON = new SqlParameter("@AGAININHOSPITALREASON", SqlDbType.VarChar, 20);
            paraAGAININHOSPITALREASON.Value = info.IemBasicInfo.AgainInHospitalReason;
            SqlParameter paraBEFOREHOSCOMADAY = new SqlParameter("@BEFOREHOSCOMADAY", SqlDbType.VarChar, 20);
            paraBEFOREHOSCOMADAY.Value = info.IemBasicInfo.BeforeHosComaDay;
            SqlParameter paraBEFOREHOSCOMAHOUR = new SqlParameter("@BEFOREHOSCOMAHOUR", SqlDbType.VarChar, 20);
            paraBEFOREHOSCOMAHOUR.Value = info.IemBasicInfo.BeforeHosComaHour;
            SqlParameter paraBEFOREHOSCOMAMINUTE = new SqlParameter("@BEFOREHOSCOMAMINUTE", SqlDbType.VarChar, 20);
            paraBEFOREHOSCOMAMINUTE.Value = info.IemBasicInfo.BeforeHosComaMinute;

            SqlParameter paraLATERHOSCOMADAY = new SqlParameter("@LATERHOSCOMADAY", SqlDbType.VarChar, 20);
            paraLATERHOSCOMADAY.Value = info.IemBasicInfo.LaterHosComaDay;
            SqlParameter paraLATERHOSCOMAHOUR = new SqlParameter("@LATERHOSCOMAHOUR", SqlDbType.VarChar, 20);
            paraLATERHOSCOMAHOUR.Value = info.IemBasicInfo.LaterHosComaDay;
            SqlParameter paraLATERHOSCOMAMINUTE = new SqlParameter("@LATERHOSCOMAMINUTE", SqlDbType.VarChar, 20);
            paraLATERHOSCOMAMINUTE.Value = info.IemBasicInfo.LaterHosComaMinute;
            SqlParameter paraCARDNUMBER = new SqlParameter("@CARDNUMBER", SqlDbType.VarChar, 20);
            paraCARDNUMBER.Value = info.IemBasicInfo.CardNumber;
            SqlParameter paraALLERGIC_FLAG = new SqlParameter("@ALLERGIC_FLAG", SqlDbType.VarChar, 20);
            paraALLERGIC_FLAG.Value = info.IemDiagInfo.Allergic_Flag;

            SqlParameter paraAUTOPSY_FLAG = new SqlParameter("@AUTOPSY_FLAG", SqlDbType.VarChar, 20);
            paraAUTOPSY_FLAG.Value = info.IemBasicInfo.Autopsy_Flag;
            SqlParameter paraCSD_PROVINCEID = new SqlParameter("@CSD_PROVINCEID", SqlDbType.VarChar, 20);
            paraCSD_PROVINCEID.Value = info.IemBasicInfo.CSD_ProvinceID;
            SqlParameter paraCSD_CITYID = new SqlParameter("@CSD_CITYID", SqlDbType.VarChar, 20);
            paraCSD_CITYID.Value = info.IemBasicInfo.CSD_CityID;
            SqlParameter paraCSD_DISTRICTID = new SqlParameter("@CSD_DISTRICTID", SqlDbType.VarChar, 20);
            paraCSD_DISTRICTID.Value = info.IemBasicInfo.CSD_DistrictID;
            SqlParameter paraCSD_PROVINCENAME = new SqlParameter("@CSD_PROVINCENAME", SqlDbType.VarChar, 20);
            paraCSD_PROVINCENAME.Value = info.IemBasicInfo.CSD_ProvinceName;

            SqlParameter paraCSD_CITYNAME = new SqlParameter("@CSD_CITYNAME", SqlDbType.VarChar, 20);
            paraCSD_CITYNAME.Value = info.IemBasicInfo.CSD_CityName;
            SqlParameter paraCSD_DISTRICTNAME = new SqlParameter("@CSD_DISTRICTNAME", SqlDbType.VarChar, 20);
            paraCSD_DISTRICTNAME.Value = info.IemBasicInfo.CSD_DistrictName;
            SqlParameter paraXZZ_PROVINCEID = new SqlParameter("@XZZ_PROVINCEID", SqlDbType.VarChar, 20);
            paraXZZ_PROVINCEID.Value = info.IemBasicInfo.XZZ_ProvinceID;
            SqlParameter paraXZZ_CITYID = new SqlParameter("@XZZ_CITYID", SqlDbType.VarChar, 20);
            paraXZZ_CITYID.Value = info.IemBasicInfo.XZZ_CityID;
            SqlParameter paraXZZ_DISTRICTID = new SqlParameter("@XZZ_DISTRICTID", SqlDbType.VarChar, 20);
            paraXZZ_DISTRICTID.Value = info.IemBasicInfo.XZZ_DistrictID;

            SqlParameter paraXZZ_PROVINCENAME = new SqlParameter("@XZZ_PROVINCENAME", SqlDbType.VarChar, 20);
            paraXZZ_PROVINCENAME.Value = info.IemBasicInfo.XZZ_ProvinceName;
            SqlParameter paraXZZ_CITYNAME = new SqlParameter("@XZZ_CITYNAME", SqlDbType.VarChar, 20);
            paraXZZ_CITYNAME.Value = info.IemBasicInfo.XZZ_CityName;
            SqlParameter paraXZZ_DISTRICTNAME = new SqlParameter("@XZZ_DISTRICTNAME", SqlDbType.VarChar, 20);
            paraXZZ_DISTRICTNAME.Value = info.IemBasicInfo.XZZ_DistrictName;
            SqlParameter paraXZZ_TEL = new SqlParameter("@XZZ_TEL", SqlDbType.VarChar, 20);
            paraXZZ_TEL.Value = info.IemBasicInfo.XZZ_TEL;
            SqlParameter paraXZZ_POST = new SqlParameter("@XZZ_POST", SqlDbType.VarChar, 20);
            paraXZZ_POST.Value = info.IemBasicInfo.XZZ_Post;

            SqlParameter paraHKDZ_PROVINCEID = new SqlParameter("@HKDZ_PROVINCEID", SqlDbType.VarChar, 20);
            paraHKDZ_PROVINCEID.Value = info.IemBasicInfo.HKDZ_ProvinceID;
            SqlParameter paraHKDZ_CITYID = new SqlParameter("@HKDZ_CITYID", SqlDbType.VarChar, 20);
            paraHKDZ_CITYID.Value = info.IemBasicInfo.HKDZ_CityID;
            SqlParameter paraHKDZ_DISTRICTID = new SqlParameter("@HKDZ_DISTRICTID", SqlDbType.VarChar, 20);
            paraHKDZ_DISTRICTID.Value = info.IemBasicInfo.HKDZ_DistrictID;
            SqlParameter paraHKDZ_PROVINCENAME = new SqlParameter("@HKDZ_PROVINCENAME", SqlDbType.VarChar, 20);
            paraHKDZ_PROVINCENAME.Value = info.IemBasicInfo.HKDZ_ProvinceName;
            SqlParameter paraHKDZ_CITYNAME = new SqlParameter("@HKDZ_CITYNAME", SqlDbType.VarChar, 20);
            paraHKDZ_CITYNAME.Value = info.IemBasicInfo.HKDZ_CityName;

            SqlParameter paraHKDZ_DISTRICTNAME = new SqlParameter("@HKDZ_DISTRICTNAME", SqlDbType.VarChar, 20);
            paraHKDZ_DISTRICTNAME.Value = info.IemBasicInfo.HKDZ_DistrictName;
            SqlParameter paraHKDZ_POST = new SqlParameter("@HKDZ_POST", SqlDbType.VarChar, 20);
            paraHKDZ_POST.Value = info.IemBasicInfo.HKDZ_Post;
            SqlParameter paraJG_PROVINCEID = new SqlParameter("@JG_PROVINCEID", SqlDbType.VarChar, 20);
            paraJG_PROVINCEID.Value = info.IemBasicInfo.JG_ProvinceID;
            SqlParameter paraJG_CITYID = new SqlParameter("@JG_CITYID", SqlDbType.VarChar, 20);
            paraJG_CITYID.Value = info.IemBasicInfo.JG_CityID;
            SqlParameter paraJG_PROVINCENAME = new SqlParameter("@JG_PROVINCENAME", SqlDbType.VarChar, 20);
            paraJG_PROVINCENAME.Value = info.IemBasicInfo.JG_ProvinceName;
            SqlParameter paraJG_CITYNAME = new SqlParameter("@JG_CITYNAME", SqlDbType.VarChar, 20);
            paraJG_CITYNAME.Value = info.IemBasicInfo.JG_CityName;
            SqlParameter paraAge = new SqlParameter("@AGE", SqlDbType.VarChar, 20);
            paraAge.Value = info.IemBasicInfo.Age;
            SqlParameter parazg_flag = new SqlParameter("@zg_flag", SqlDbType.VarChar, 20);
            parazg_flag.Value = info.IemBasicInfo.ZG_FLAG;
            //新增入院病情 add by ywk 2012年6月26日10:12:53
            SqlParameter parazg_admitinofo = new SqlParameter("@admitinfo", SqlDbType.VarChar, 20);
            parazg_admitinofo.Value = info.IemBasicInfo.AdmitInfo;


            #region 新增的几个地址读取的具体名称，新加到参数里去
            SqlParameter paracsdaddress = new SqlParameter("@CSDADDRESS", SqlDbType.VarChar, 500);
            paracsdaddress.Value = info.IemBasicInfo.CSDAddress;//出生地
            SqlParameter parajgaddress = new SqlParameter("@JGADDRESS", SqlDbType.VarChar, 500);
            parajgaddress.Value = info.IemBasicInfo.JGAddress;//籍贯地址
            SqlParameter paraxzzdaddress = new SqlParameter("@XZZADDRESS", SqlDbType.VarChar, 500);
            paraxzzdaddress.Value = info.IemBasicInfo.XZZAddress;//现住址
            SqlParameter parahkdzaddress = new SqlParameter("@HKDZADDRESS", SqlDbType.VarChar, 500);
            parahkdzaddress.Value = info.IemBasicInfo.HKZZAddress;//户口地址
            #endregion

            #region 新增的诊断符合情况的
            SqlParameter paraMenAndInHop = new SqlParameter("@MenAndInHop", SqlDbType.VarChar, 12);
            paraMenAndInHop.Value = info.IemBasicInfo.MenAndInHop;
            SqlParameter paraInHopAndOutHop = new SqlParameter("@InHopAndOutHop", SqlDbType.VarChar, 12);
            paraInHopAndOutHop.Value = info.IemBasicInfo.InHopAndOutHop;
            SqlParameter paraBeforeOpeAndAfterOper = new SqlParameter("@BeforeOpeAndAfterOper", SqlDbType.VarChar, 12);
            paraBeforeOpeAndAfterOper.Value = info.IemBasicInfo.BeforeOpeAndAfterOper;
            SqlParameter paraLinAndBingLi = new SqlParameter("@LinAndBingLi", SqlDbType.VarChar, 12);
            paraLinAndBingLi.Value = info.IemBasicInfo.LinAndBingLi;
            SqlParameter paraInHopThree = new SqlParameter("@InHopThree", SqlDbType.VarChar, 12);
            paraInHopThree.Value = info.IemBasicInfo.InHopThree;
            SqlParameter paraFangAndBingLi = new SqlParameter("@FangAndBingLi", SqlDbType.VarChar, 12);
            paraFangAndBingLi.Value = info.IemBasicInfo.FangAndBingLi;

            #endregion
            SqlParameter[] paraColl = new SqlParameter[] { paraedittype  ,paraIEM_MAINPAGE_NO ,paraPATNOOFHIS ,paraNoOfInpat,paraPayID ,paraSocialCare,
                paraInCount,paraName ,paraSexID,paraBirth,paraMarital,
                paraJobID,paraNationalityID,paraNationID,paraIDNO,paraOrganization,paraOfficePlace,
                paraOfficeTEL,paraOfficePost,paraContactPerson,paraRelationship,paraContactAddress,
                paraContactTEL,paraAdmitDate,paraAdmitDept,paraAdmitWard,
                paraTrans_AdmitDept,paraOutWardDate,paraOutHosDept,paraOutHosWard,
                paraActual_Days,paraPATHOLOGY_DIAGNOSIS_NAME,paraPATHOLOGY_OBSERVATION_SN,paraALLERGIC_DRUG,paraSection_Director,
                paraDirector,paraVs_Employee_Code,paraResident_Employee_Code,paraRefresh_Employee_Code,paraDUTY_NURSE ,
                paraInterne,paraCoding_User,paraMedical_Quality_Id,paraQuality_Control_Doctor,paraQuality_Control_Nurse,
                paraQuality_Control_Date,
                paraBLOODTYPE,paraRH,paraIS_COMPLETED,paraCOMPLETED_TIME,
                paraCREATE_USER,paraCREATE_TIME,paraMODIFIED_USER,paraMODIFIED_TIME,paraZYMOSIS,
                paraHURT_TOXICOSIS_ELE_ID,paraHURT_TOXICOSIS_ELE_Name,paraPATHOLOGY_DIAGNOSIS_ID,paraMONTHAGE,paraWEIGHT,
                paraINWEIGHT ,paraINHOSTYPE,paraOUTHOSTYPE,paraRECEIVEHOSPITAL,paraRECEIVEHOSPITAL2,
                paraAGAININHOSPITAL,paraAGAININHOSPITALREASON,paraBEFOREHOSCOMADAY,paraBEFOREHOSCOMAHOUR,paraBEFOREHOSCOMAMINUTE,
                paraLATERHOSCOMADAY,paraLATERHOSCOMAHOUR,paraLATERHOSCOMAMINUTE,paraCARDNUMBER,paraALLERGIC_FLAG,
                paraAUTOPSY_FLAG,paraCSD_PROVINCEID,paraCSD_CITYID,paraCSD_DISTRICTID,paraCSD_PROVINCENAME,
                paraCSD_CITYNAME,paraCSD_DISTRICTNAME,paraXZZ_PROVINCEID,paraXZZ_CITYID,paraXZZ_DISTRICTID,
                paraXZZ_PROVINCENAME,paraXZZ_CITYNAME,paraXZZ_DISTRICTNAME,paraXZZ_TEL,paraXZZ_POST,
                paraHKDZ_PROVINCEID,paraHKDZ_CITYID,paraHKDZ_DISTRICTID,paraHKDZ_PROVINCENAME,paraHKDZ_CITYNAME,
                paraHKDZ_DISTRICTNAME,paraHKDZ_POST,paraJG_PROVINCEID,paraJG_CITYID,paraJG_PROVINCENAME,paraJG_CITYNAME,paraAge,
                parazg_flag,parazg_admitinofo,paracsdaddress,parajgaddress,paraxzzdaddress,parahkdzaddress, paraMenAndInHop,paraInHopAndOutHop,paraBeforeOpeAndAfterOper,paraLinAndBingLi,paraInHopThree,
            paraFangAndBingLi
        };


            #endregion

            string no = m_app.SqlHelper.ExecuteDataTable("IEM_MAIN_PAGE.usp_Edit_Iem_BasicInfo_2012", paraColl, CommandType.StoredProcedure).Rows[0][0].ToString();
            this.IemInfo.IemBasicInfo.Iem_Mainpage_NO = no;
        }

        /// <summary>
        /// insert diagnose info
        /// </summary>
        private void InserIemDiagnoseInfo_2012(DataRow info, IDataAccess sqlHelper, Iem_Mainpage_Diagnosis Iemm)
        {

            SqlParameter paraIem_Mainpage_NO = new SqlParameter("@Iem_Mainpage_NO", SqlDbType.VarChar, 12);
            paraIem_Mainpage_NO.Value = IemInfo.IemBasicInfo.Iem_Mainpage_NO;
            SqlParameter paraDiagnosis_Type_Id = new SqlParameter("@Diagnosis_Type_Id", SqlDbType.VarChar, 12);
            paraDiagnosis_Type_Id.Value = info["Diagnosis_Type_Id"];
            SqlParameter paraDiagnosis_Code = new SqlParameter("@Diagnosis_Code", SqlDbType.VarChar, 60);
            paraDiagnosis_Code.Value = info["Diagnosis_Code"];
            SqlParameter paraDiagnosis_Name = new SqlParameter("@Diagnosis_Name", SqlDbType.VarChar, 300);
            paraDiagnosis_Name.Value = info["Diagnosis_Name"];
            SqlParameter paraStatus_Id = new SqlParameter("@Status_Id", SqlDbType.VarChar, 12);
            paraStatus_Id.Value = info["Status_Id"];
            SqlParameter paraOrder_Value = new SqlParameter("@Order_Value", SqlDbType.VarChar, 3);
            paraOrder_Value.Value = info["Order_Value"].ToString() == "" ? "0" : info["Order_Value"].ToString();
            SqlParameter paraCreate_User = new SqlParameter("@Create_User", SqlDbType.VarChar, 10);
            paraCreate_User.Value = m_app.User.DoctorId;

            //新增的诊断相关
            SqlParameter paraMenAndInHop = new SqlParameter("@MenAndInHop", SqlDbType.VarChar, 12);
            paraMenAndInHop.Value = info["MenAndInHop"];
            SqlParameter paraInHopAndOutHop = new SqlParameter("@InHopAndOutHop", SqlDbType.VarChar, 12);
            paraInHopAndOutHop.Value = info["InHopAndOutHop"];
            SqlParameter paraBeforeOpeAndAfterOper = new SqlParameter("@BeforeOpeAndAfterOper", SqlDbType.VarChar, 12);
            paraBeforeOpeAndAfterOper.Value = info["BeforeOpeAndAfterOper"];
            SqlParameter paraLinAndBingLi = new SqlParameter("@LinAndBingLi", SqlDbType.VarChar, 12);
            paraLinAndBingLi.Value = info["LinAndBingLi"];
            SqlParameter paraInHopThree = new SqlParameter("@InHopThree", SqlDbType.VarChar, 12);
            paraInHopThree.Value = info["InHopThree"];
            SqlParameter paraFangAndBingLi = new SqlParameter("@FangAndBingLi", SqlDbType.VarChar, 12);
            paraFangAndBingLi.Value = info["FangAndBingLi"];
            //新增子入院病情
            SqlParameter paraAdmitInfo = new SqlParameter("@AdmitInfo", SqlDbType.VarChar, 12);
            paraAdmitInfo.Value = info["AdmitInfo"];
            SqlParameter[] paraColl = new SqlParameter[] { paraIem_Mainpage_NO, paraDiagnosis_Type_Id, paraDiagnosis_Code, paraDiagnosis_Name, paraStatus_Id, paraOrder_Value, paraCreate_User ,
            paraMenAndInHop,paraInHopAndOutHop,paraBeforeOpeAndAfterOper,paraLinAndBingLi,paraInHopThree,
            paraFangAndBingLi,paraAdmitInfo
            };

            sqlHelper.ExecuteNoneQuery("IEM_MAIN_PAGE.usp_edif_iem_mainpage_diag2012", paraColl, CommandType.StoredProcedure);
        }

        /// <summary>
        /// insert oper info
        /// </summary>
        private void InserIemOperInfo_2012(DataRow info, IDataAccess sqlHelper)
        {
            //info.Create_User = ;
            //info.IEM_MainPage_NO = ;
            SqlParameter paraIem_Mainpage_NO = new SqlParameter("@IEM_MainPage_NO", SqlDbType.Decimal);
            paraIem_Mainpage_NO.Value = this.IemInfo.IemBasicInfo.Iem_Mainpage_NO;
            SqlParameter paraOperation_Code = new SqlParameter("@Operation_Code", SqlDbType.VarChar, 60);
            paraOperation_Code.Value = info["Operation_Code"];
            SqlParameter paraOperation_Date = new SqlParameter("@Operation_Date", SqlDbType.VarChar, 19);
            paraOperation_Date.Value = info["Operation_Date"];
            SqlParameter paraOperation_Name = new SqlParameter("@Operation_Name", SqlDbType.VarChar, 300);
            paraOperation_Name.Value = info["Operation_Name"];
            SqlParameter paraExecute_User1 = new SqlParameter("@Execute_User1", SqlDbType.VarChar, 20);
            paraExecute_User1.Value = info["Execute_User1"];
            SqlParameter paraExecute_User2 = new SqlParameter("@Execute_User2", SqlDbType.VarChar, 20);
            paraExecute_User2.Value = info["Execute_User2"];
            SqlParameter paraExecute_User3 = new SqlParameter("@Execute_User3", SqlDbType.VarChar, 20);
            paraExecute_User3.Value = info["Execute_User3"];
            SqlParameter paraAnaesthesia_Type_Id = new SqlParameter("@Anaesthesia_Type_Id", SqlDbType.VarChar, 3);
            paraAnaesthesia_Type_Id.Value = info["Anaesthesia_Type_Id"];
            SqlParameter paraClose_Level = new SqlParameter("@Close_Level", SqlDbType.VarChar, 3);
            paraClose_Level.Value = info["Close_Level"];
            SqlParameter paraAnaesthesia_User = new SqlParameter("@Anaesthesia_User", SqlDbType.VarChar, 20);
            paraAnaesthesia_User.Value = info["Anaesthesia_User"];
            SqlParameter paraCreate_User = new SqlParameter("@Create_User", SqlDbType.VarChar, 10);
            paraCreate_User.Value = m_app.User.DoctorId;
            SqlParameter paraOPERATION_LEVEL = new SqlParameter("@OPERATION_LEVEL", SqlDbType.VarChar, 10);
            paraOPERATION_LEVEL.Value = info["operation_level"];
            //新增的是否择期手术是否无菌手术是否感染
            SqlParameter paraIsChooseDate = new SqlParameter("@IsChooseDate", SqlDbType.VarChar, 5);
            paraIsChooseDate.Value = info["IsChooseDate"];
            SqlParameter paraIsClearOpe = new SqlParameter("@IsClearOpe", SqlDbType.VarChar, 5);
            paraIsClearOpe.Value = info["IsClearOpe"];
            SqlParameter paraIsGanRan = new SqlParameter("@IsGanRan", SqlDbType.VarChar, 5);
            paraIsGanRan.Value = info["IsGanRan"];
            SqlParameter paraAnesthesiaLevel = new SqlParameter("@anesthesia_level", SqlDbType.VarChar, 5);
            paraAnesthesiaLevel.Value = info["anesthesia_level"];
            SqlParameter paraOpercomplicationCode = new SqlParameter("@opercomplication_code", SqlDbType.VarChar, 5);
            paraOpercomplicationCode.Value = info["opercomplication_code"];

            SqlParameter[] paraColl = new SqlParameter[] { paraIem_Mainpage_NO, paraOperation_Code, paraOperation_Date, paraOperation_Name, paraExecute_User1, paraExecute_User2, paraExecute_User3,
                paraAnaesthesia_Type_Id,paraClose_Level,paraAnaesthesia_User,paraCreate_User,paraOPERATION_LEVEL
            ,paraIsChooseDate,paraIsClearOpe,paraIsGanRan,paraAnesthesiaLevel,paraOpercomplicationCode};

            sqlHelper.ExecuteNoneQuery("IEM_MAIN_PAGE.usp_edit_iem_mainpage_oper2012", paraColl, CommandType.StoredProcedure);
        }


        /// <summary>
        /// 保存婴儿情况
        /// </summary>
        /// <param name="baby"></param>
        /// <param name="sqlHelper"></param>
        private void InsertIemObstetricsBaby(DataRow baby, IDataAccess sqlHelper)
        {
            SqlParameter paraIem_Mainpage_NO = new SqlParameter("@IEM_MainPage_NO", SqlDbType.Decimal);
            paraIem_Mainpage_NO.Value = this.IemInfo.IemBasicInfo.Iem_Mainpage_NO;
            SqlParameter paraID = new SqlParameter("@IBSBABYID", SqlDbType.VarChar, 1);
            paraID.Value = baby["IBSBABYID"];
            SqlParameter paraTC = new SqlParameter("@TC", SqlDbType.VarChar, 1);
            paraTC.Value = baby["TC"];
            SqlParameter paraCC = new SqlParameter("@CC", SqlDbType.VarChar, 1);
            paraCC.Value = baby["CC"];
            SqlParameter paraTB = new SqlParameter("@TB", SqlDbType.VarChar, 1);
            paraTB.Value = baby["TB"];
            SqlParameter paraCFHYPLD = new SqlParameter("@CFHYPLD", SqlDbType.VarChar, 1);
            paraCFHYPLD.Value = baby["CFHYPLD"];
            SqlParameter paraMIDWIFERY = new SqlParameter("@MIDWIFERY", SqlDbType.VarChar, 20);
            paraMIDWIFERY.Value = baby["MIDWIFERY"];
            SqlParameter paraSex = new SqlParameter("@SEX", SqlDbType.VarChar, 1);
            paraSex.Value = baby["SEX"];
            SqlParameter paraAPJ = new SqlParameter("@APJ", SqlDbType.VarChar, 10);
            paraAPJ.Value = baby["APJ"];
            SqlParameter paraHeigh = new SqlParameter("@HEIGH", SqlDbType.VarChar, 10);
            paraHeigh.Value = baby["HEIGH"];
            SqlParameter paraWeight = new SqlParameter("@WEIGHT", SqlDbType.VarChar, 10);
            paraWeight.Value = baby["WEIGHT"];
            SqlParameter paraCCQK = new SqlParameter("@CCQK", SqlDbType.VarChar, 1);
            paraCCQK.Value = baby["CCQK"];
            SqlParameter paraBITHDAY = new SqlParameter("@BITHDAY", SqlDbType.VarChar, 1);
            paraBITHDAY.Value = baby["BITHDAY"];
            SqlParameter paraFMFS = new SqlParameter("@FMFS", SqlDbType.VarChar, 1);
            paraFMFS.Value = baby["FMFS"];
            SqlParameter paraCYQK = new SqlParameter("@CYQK", SqlDbType.VarChar, 1);
            paraCYQK.Value = baby["CYQK"];
            SqlParameter paraCreate_User = new SqlParameter("@CREATE_USER", SqlDbType.VarChar, 10);
            paraCreate_User.Value = m_app.User.DoctorId;

            SqlParameter[] paraColl = new SqlParameter[] { paraIem_Mainpage_NO, paraID, paraTC, paraCC,paraTB,paraCFHYPLD,paraMIDWIFERY, paraSex,paraAPJ,
                        paraHeigh,paraWeight,paraCCQK,paraBITHDAY,paraFMFS,paraCYQK,paraCreate_User };

            sqlHelper.ExecuteNoneQuery("IEM_MAIN_PAGE.usp_insert_iem_main_ObsBaby", paraColl, CommandType.StoredProcedure);

        }

        /// <summary>
        /// 保存婴儿情况
        /// </summary>
        /// <param name="baby"></param>
        /// <param name="sqlHelper"></param>
        private void InsertIemObstetricsBaby(Iem_MainPage_ObstetricsBaby baby, IDataAccess sqlHelper)
        {
            baby.Create_User = m_app.User.DoctorId;
            baby.IEM_MainPage_NO = this.IemInfo.IemBasicInfo.Iem_Mainpage_NO.ToString();
            SqlParameter paraIem_Mainpage_NO = new SqlParameter("@iem_mainpage_no", SqlDbType.Decimal);
            paraIem_Mainpage_NO.Value = baby.IEM_MainPage_NO;
            SqlParameter paraID = new SqlParameter("@IBSBABYID", SqlDbType.VarChar, 1);
            paraID.Value = baby.IBSBABYID;
            SqlParameter paraTC = new SqlParameter("@TC", SqlDbType.VarChar, 1);
            paraTC.Value = baby.TC;
            SqlParameter paraCC = new SqlParameter("@CC", SqlDbType.VarChar, 1);
            paraCC.Value = baby.CC;
            SqlParameter paraTB = new SqlParameter("@TB", SqlDbType.VarChar, 1);
            paraTB.Value = baby.TB;
            SqlParameter paraCFHYPLD = new SqlParameter("@CFHYPLD", SqlDbType.VarChar, 1);
            paraCFHYPLD.Value = baby.CFHYPLD;
            SqlParameter paraMIDWIFERY = new SqlParameter("@MIDWIFERY", SqlDbType.VarChar, 20);
            paraMIDWIFERY.Value = baby.Midwifery;
            SqlParameter paraSex = new SqlParameter("@Sex", SqlDbType.VarChar, 1);
            paraSex.Value = baby.Sex;
            SqlParameter paraAPJ = new SqlParameter("@APJ", SqlDbType.VarChar, 10);
            paraAPJ.Value = baby.APJ;
            SqlParameter paraHeigh = new SqlParameter("@Heigh", SqlDbType.VarChar, 10);
            paraHeigh.Value = baby.Heigh;
            SqlParameter paraWeight = new SqlParameter("@Weight", SqlDbType.VarChar, 10);
            paraWeight.Value = baby.Weight;
            SqlParameter paraCCQK = new SqlParameter("@CCQK", SqlDbType.VarChar, 1);
            paraCCQK.Value = baby.CCQK;
            SqlParameter paraBITHDAY = new SqlParameter("@BITHDAY", SqlDbType.VarChar, 1);
            paraBITHDAY.Value = baby.BithDay;
            SqlParameter paraFMFS = new SqlParameter("@FMFS", SqlDbType.VarChar, 1);
            paraFMFS.Value = baby.FMFS;
            SqlParameter paraCYQK = new SqlParameter("@CYQK", SqlDbType.VarChar, 1);
            paraCYQK.Value = baby.CYQK;
            SqlParameter paraCreate_User = new SqlParameter("@Create_User", SqlDbType.VarChar, 10);
            paraCreate_User.Value = baby.Create_User;

            SqlParameter[] paraColl = new SqlParameter[] { paraIem_Mainpage_NO,paraID, paraTC, paraCC,paraTB,paraCFHYPLD,paraMIDWIFERY, paraSex,paraAPJ,
                        paraHeigh,paraWeight,paraCCQK,paraBITHDAY,paraFMFS,paraCYQK,paraCreate_User };

            sqlHelper.ExecuteNoneQuery("IEM_MAIN_PAGE.usp_insert_iem_main_ObsBaby", paraColl, CommandType.StoredProcedure);

        }



        #endregion

        #region update

        /// <summary>
        /// 更新首页信息
        /// </summary>
        /// <param name="info"></param>
        private void UpdateItemInfo_2012()
        {
            try
            {
                m_app.SqlHelper.BeginTransaction();

                EditIemBasicInfo_2012(IemInfo, "2");
                //UpdateIemBasicInfo(this.IemInfo, m_app.SqlHelper);

                // 先把之前的诊断，都给CANCLE
                UpdateIemDiagnoseInfo_2012(this.IemInfo, m_app.SqlHelper);
                foreach (DataRow item in this.IemInfo.IemDiagInfo.OutDiagTable.Rows)
                    InserIemDiagnoseInfo_2012(item, m_app.SqlHelper, IemInfo.IemDiagInfo);
                //InserIemDiagnoseInfo(item, m_app.SqlHelper);

                // 先把之前的手术，都给CANCLE
                //UpdateIemOperInfo(this.IemInfo, m_app.SqlHelper);
                UpdateIemOperInfo_2012(this.IemInfo, m_app.SqlHelper);
                foreach (DataRow item in this.IemInfo.IemOperInfo.Operation_Table.Rows)
                    InserIemOperInfo_2012(item, m_app.SqlHelper);
                //InserIemOperInfo(item, m_app.SqlHelper);


                //修改产妇婴儿系想你
                DeleteIemObstetricsBaby(IemInfo.IemObstetricsBaby, m_app.SqlHelper);

                //edit by ywk 2012年11月8日11:46:45  如果此时IemInfo.IemObstetricsBaby为null的话下面就不执行了
                if (IemInfo.IemObstetricsBaby != null)
                {
                    foreach (DataRow item in this.IemInfo.IemObstetricsBaby.OutBabyTable.Rows)
                        InsertIemObstetricsBaby(item, m_app.SqlHelper);

                }


                //InsertIemObstetricsBaby(IemInfo.IemObstetricsBaby, m_app.SqlHelper);

                // 新增的将信息加到病案首页的费用表中 add by ywk 2012年10月16日 19:58:20 更新操作
                if (!CheckPatInIemMainPage(IemInfo.IemBasicInfo.Iem_Mainpage_NO))//如果首页基本信息表中还没有就是插入
                {
                    EditIemFeeInfo_2012(this.IemInfo, "1");
                }
                else
                {
                    EditIemFeeInfo_2012(this.IemInfo, "2");
                }


                if (!CheckPatInIemMainPageOthers(IemInfo.IemBasicInfo.Iem_Mainpage_NO))
                {
                    EditIemOthersJS(this.IemInfo, "1");
                }
                else
                {
                    EditIemOthersJS(this.IemInfo, "2");
                }
                m_app.SqlHelper.CommitTransaction();

                m_app.CustomMessageBox.MessageShow("更新成功");
                GetIemInfo();
            }
            catch (Exception ex)
            {
                m_app.SqlHelper.RollbackTransaction();
            }
        }
        /// <summary>
        ///  c传入首页序号，iem_mainpage_basicinfo_2012中的主键，首页序号
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool CheckPatInIemMainPage(string iemmainpageno)
        {
            string sql = string.Format("select * from Iem_MainPage_FeeInfo where iem_mainpage_no='{0}'", iemmainpageno);
            if (m_app.SqlHelper.ExecuteDataTable(sql, CommandType.Text).Rows.Count > 0)//病案首页费用表已经存在
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// IEM_MAINPAGE_OTHER_2012中查找记录是否存在
        /// add by 王冀 2012 12 4
        /// </summary>
        /// <param name="iemmainpageno"></param>
        /// <returns></returns>
        private bool CheckPatInIemMainPageOthers(string iemmainpageno)
        {
            try
            {
                string sql = string.Format("select * from IEM_MAINPAGE_OTHER_2012 where IEM_MAINPAGE_NO='{0}'", iemmainpageno);
                if (m_app.SqlHelper.ExecuteDataTable(sql, CommandType.Text).Rows.Count > 0)//病案首页费用表已经存在
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateIemBasicInfo(IemMainPageInfo info, IDataAccess sqlHelper)
        {
            info.IemBasicInfo.Create_User = m_app.User.DoctorId;
            #region
            SqlParameter paraiem_mainpage_no = new SqlParameter("@iem_mainpage_no", SqlDbType.VarChar, 12);
            paraiem_mainpage_no.Value = IemInfo.IemBasicInfo.Iem_Mainpage_NO;
            SqlParameter paraPatNoOfHis = new SqlParameter("@PatNoOfHis", SqlDbType.VarChar, 14);
            paraPatNoOfHis.Value = info.IemBasicInfo.PatNoOfHis;
            SqlParameter paraNoOfInpat = new SqlParameter("@NoOfInpat", SqlDbType.VarChar, 9);
            paraNoOfInpat.Value = info.IemBasicInfo.NoOfInpat;
            SqlParameter paraPayID = new SqlParameter("@PayID", SqlDbType.VarChar, 4);
            paraPayID.Value = info.IemBasicInfo.PayID;
            SqlParameter paraSocialCare = new SqlParameter("@SocialCare", SqlDbType.VarChar, 32);
            paraSocialCare.Value = info.IemBasicInfo.SocialCare;

            SqlParameter paraInCount = new SqlParameter("@InCount", SqlDbType.VarChar, 4);
            paraInCount.Value = info.IemBasicInfo.InCount;
            SqlParameter paraName = new SqlParameter("@Name", SqlDbType.VarChar, 64);
            paraName.Value = info.IemBasicInfo.Name;
            SqlParameter paraSexID = new SqlParameter("@SexID", SqlDbType.VarChar, 4);
            paraSexID.Value = info.IemBasicInfo.SexID;
            SqlParameter paraBirth = new SqlParameter("@Birth", SqlDbType.VarChar, 10);
            paraBirth.Value = info.IemBasicInfo.Birth;
            SqlParameter paraMarital = new SqlParameter("@Marital", SqlDbType.VarChar, 4);
            paraMarital.Value = info.IemBasicInfo.Marital;

            SqlParameter paraJobID = new SqlParameter("@JobID", SqlDbType.VarChar, 4);
            paraJobID.Value = info.IemBasicInfo.JobID;
            //SqlParameter paraProvinceID = new SqlParameter("@ProvinceID", SqlDbType.VarChar, 10);
            //paraProvinceID.Value = info.IemBasicInfo.ProvinceID;
            //SqlParameter paraCountyID = new SqlParameter("@CountyID", SqlDbType.VarChar, 10);
            //paraCountyID.Value = info.IemBasicInfo.CountyID;
            SqlParameter paraNationID = new SqlParameter("@NationID", SqlDbType.VarChar, 4);
            paraNationID.Value = info.IemBasicInfo.NationID;
            SqlParameter paraNationalityID = new SqlParameter("@NationalityID", SqlDbType.VarChar, 4);
            paraNationalityID.Value = info.IemBasicInfo.NationalityID;

            SqlParameter paraIDNO = new SqlParameter("@IDNO", SqlDbType.VarChar, 18);
            paraIDNO.Value = info.IemBasicInfo.IDNO;
            SqlParameter paraOrganization = new SqlParameter("@Organization", SqlDbType.VarChar, 64);
            paraOrganization.Value = info.IemBasicInfo.Organization;
            SqlParameter paraOfficePlace = new SqlParameter("@OfficePlace", SqlDbType.VarChar, 64);
            paraOfficePlace.Value = info.IemBasicInfo.OfficePlace;
            SqlParameter paraOfficeTEL = new SqlParameter("@OfficeTEL", SqlDbType.VarChar, 16);
            paraOfficeTEL.Value = info.IemBasicInfo.OfficeTEL;
            SqlParameter paraOfficePost = new SqlParameter("@OfficePost", SqlDbType.VarChar, 16);
            paraOfficePost.Value = info.IemBasicInfo.OfficePost;

            //SqlParameter paraNativeAddress = new SqlParameter("@NativeAddress", SqlDbType.VarChar, 64);
            //paraNativeAddress.Value = info.IemBasicInfo.NativeAddress;
            //SqlParameter paraNativeTEL = new SqlParameter("@NativeTEL", SqlDbType.VarChar, 16);
            //paraNativeTEL.Value = info.IemBasicInfo.NativeTEL;
            //SqlParameter paraNativePost = new SqlParameter("@NativePost", SqlDbType.VarChar, 16);
            //paraNativePost.Value = info.IemBasicInfo.NativePost;
            SqlParameter paraContactPerson = new SqlParameter("@ContactPerson", SqlDbType.VarChar, 32);
            paraContactPerson.Value = info.IemBasicInfo.ContactPerson;
            SqlParameter paraRelationship = new SqlParameter("@Relationship", SqlDbType.VarChar, 4);
            paraRelationship.Value = info.IemBasicInfo.RelationshipID;

            SqlParameter paraContactAddress = new SqlParameter("@ContactAddress", SqlDbType.VarChar, 255);
            paraContactAddress.Value = info.IemBasicInfo.ContactAddress;
            SqlParameter paraContactTEL = new SqlParameter("@ContactTEL", SqlDbType.VarChar, 16);
            paraContactTEL.Value = info.IemBasicInfo.ContactTEL;
            SqlParameter paraAdmitDate = new SqlParameter("@AdmitDate", SqlDbType.VarChar, 19);
            paraAdmitDate.Value = info.IemBasicInfo.AdmitDate;
            SqlParameter paraAdmitDept = new SqlParameter("@AdmitDept", SqlDbType.VarChar, 12);
            paraAdmitDept.Value = info.IemBasicInfo.AdmitDeptID;
            SqlParameter paraAdmitWard = new SqlParameter("@AdmitWard", SqlDbType.VarChar, 12);
            paraAdmitWard.Value = info.IemBasicInfo.AdmitWardID;

            //SqlParameter paraDays_Before = new SqlParameter("@Days_Before", SqlDbType.VarChar, 4);
            //paraDays_Before.Value = info.IemBasicInfo.Days_Before;
            //SqlParameter paraTrans_Date = new SqlParameter("@Trans_Date", SqlDbType.VarChar, 19);
            //paraTrans_Date.Value = info.IemBasicInfo.Trans_Date;
            SqlParameter paraTrans_AdmitDept = new SqlParameter("@Trans_AdmitDept", SqlDbType.VarChar, 12);
            paraTrans_AdmitDept.Value = info.IemBasicInfo.Trans_AdmitDeptID;
            //SqlParameter paraTrans_AdmitWard = new SqlParameter("@Trans_AdmitWard", SqlDbType.VarChar, 12);
            //paraTrans_AdmitWard.Value = info.IemBasicInfo.Trans_AdmitWard;
            //SqlParameter paraTrans_AdmitDept_Again = new SqlParameter("@Trans_AdmitDept_Again", SqlDbType.VarChar, 12);
            //paraTrans_AdmitDept_Again.Value = info.IemBasicInfo.Trans_AdmitDept_Again;

            SqlParameter paraOutWardDate = new SqlParameter("@OutWardDate", SqlDbType.VarChar, 19);
            paraOutWardDate.Value = info.IemBasicInfo.OutWardDate;
            SqlParameter paraOutHosDept = new SqlParameter("@OutHosDept", SqlDbType.VarChar, 12);
            paraOutHosDept.Value = info.IemBasicInfo.OutHosDeptID;
            SqlParameter paraOutHosWard = new SqlParameter("@OutHosWard", SqlDbType.VarChar, 12);
            paraOutHosWard.Value = info.IemBasicInfo.OutHosWardID;
            SqlParameter paraActual_Days = new SqlParameter("@Actual_Days", SqlDbType.VarChar, 4);
            paraActual_Days.Value = info.IemBasicInfo.ActualDays;
            //SqlParameter paraDeath_Time = new SqlParameter("@Death_Time", SqlDbType.VarChar, 19);
            //paraDeath_Time.Value = info.IemBasicInfo.Death_Time;
            //SqlParameter paraDeath_Reason = new SqlParameter("@Death_Reason", SqlDbType.VarChar, 300);
            //paraDeath_Reason.Value = info.IemBasicInfo.Death_Reason;

            SqlParameter paraIs_Completed = new SqlParameter("@Is_Completed", SqlDbType.VarChar, 1);
            paraIs_Completed.Value = info.IemBasicInfo.Is_Completed;
            SqlParameter paracompleted_time = new SqlParameter("@completed_time", SqlDbType.VarChar, 19);
            paracompleted_time.Value = info.IemBasicInfo.completed_time;
            //SqlParameter paraCreate_User = new SqlParameter("@Create_User", SqlDbType.VarChar, 10);
            //paraCreate_User.Value = info.IemBasicInfo.Create_User;

            //SqlParameter paraXay_Sn = new SqlParameter("@Xay_Sn", SqlDbType.VarChar, 300);
            //paraXay_Sn.Value = info.IemBasicInfo.Xay_Sn;
            //SqlParameter paraCt_Sn = new SqlParameter("@Ct_Sn", SqlDbType.VarChar, 300);
            //paraCt_Sn.Value = info.IemBasicInfo.Ct_Sn;
            //SqlParameter paraMri_Sn = new SqlParameter("@Mri_Sn", SqlDbType.VarChar, 300);
            //paraMri_Sn.Value = info.IemBasicInfo.Mri_Sn;
            //SqlParameter paraDsa_Sn = new SqlParameter("@Dsa_Sn", SqlDbType.VarChar, 300);
            //paraDsa_Sn.Value = info.IemBasicInfo.Dsa_Sn;

            //SqlParameter paraAshes_Diagnosis_Name = new SqlParameter("@Ashes_Diagnosis_Name", SqlDbType.VarChar, 300);
            //paraAshes_Diagnosis_Name.Value = info.IemBasicInfo.Ashes_Diagnosis_Name;

            //SqlParameter paraAshes_Anatomise_Sn = new SqlParameter("@Ashes_Anatomise_Sn", SqlDbType.VarChar, 60);
            //paraAshes_Anatomise_Sn.Value = info.IemBasicInfo.Ashes_Anatomise_Sn;

            //////诊断实体中数据
            //SqlParameter paraAdmitInfo = new SqlParameter("@AdmitInfo", SqlDbType.VarChar, 4);
            //paraAdmitInfo.Value = info.IemDiagInfo.AdmitInfo;
            //SqlParameter paraIn_Check_Date = new SqlParameter("@In_Check_Date", SqlDbType.VarChar, 19);
            //paraIn_Check_Date.Value = info.IemDiagInfo.In_Check_Date;
            SqlParameter paraPathology_Diagnosis_Name = new SqlParameter("@Pathology_Diagnosis_Name", SqlDbType.VarChar, 300);
            paraPathology_Diagnosis_Name.Value = info.IemDiagInfo.Pathology_Diagnosis_Name;
            SqlParameter paraPathology_Observation_Sn = new SqlParameter("@Pathology_Observation_Sn", SqlDbType.VarChar, 60);
            paraPathology_Observation_Sn.Value = info.IemDiagInfo.Pathology_Observation_Sn;
            SqlParameter paraAllergic_Drug = new SqlParameter("@Allergic_Drug", SqlDbType.VarChar, 300);
            paraAllergic_Drug.Value = info.IemDiagInfo.Allergic_Drug;
            //SqlParameter paraHbsag = new SqlParameter("@Hbsag", SqlDbType.VarChar, 1);
            //paraHbsag.Value = info.IemDiagInfo.Hbsag;
            //SqlParameter paraHcv_Ab = new SqlParameter("@Hcv_Ab", SqlDbType.VarChar, 1);
            //paraHcv_Ab.Value = info.IemDiagInfo.Hcv_Ab;
            //SqlParameter paraHiv_Ab = new SqlParameter("@Hiv_Ab", SqlDbType.VarChar, 1);
            //paraHiv_Ab.Value = info.IemDiagInfo.Hiv_Ab;
            //SqlParameter paraOpd_Ipd_Id = new SqlParameter("@Opd_Ipd_Id", SqlDbType.VarChar, 1);
            //paraOpd_Ipd_Id.Value = info.IemDiagInfo.Opd_Ipd_Id;
            //SqlParameter paraIn_Out_Inpatinet_Id = new SqlParameter("@In_Out_Inpatinet_Id", SqlDbType.VarChar, 1);
            //paraIn_Out_Inpatinet_Id.Value = info.IemDiagInfo.In_Out_Inpatinet_Id;
            //SqlParameter paraBefore_After_Or_Id = new SqlParameter("@Before_After_Or_Id", SqlDbType.VarChar, 1);
            //paraBefore_After_Or_Id.Value = info.IemDiagInfo.Before_After_Or_Id;
            //SqlParameter paraClinical_Pathology_Id = new SqlParameter("@Clinical_Pathology_Id", SqlDbType.VarChar, 1);
            //paraClinical_Pathology_Id.Value = info.IemDiagInfo.Clinical_Pathology_Id;
            //SqlParameter paraPacs_Pathology_Id = new SqlParameter("@Pacs_Pathology_Id", SqlDbType.VarChar, 1);
            //paraPacs_Pathology_Id.Value = info.IemDiagInfo.Pacs_Pathology_Id;
            //SqlParameter paraSave_Times = new SqlParameter("@Save_Times", SqlDbType.VarChar, 4);
            //paraSave_Times.Value = info.IemDiagInfo.Save_Times;
            //SqlParameter paraSuccess_Times = new SqlParameter("@Success_Times", SqlDbType.VarChar, 4);
            //paraSuccess_Times.Value = info.IemDiagInfo.Success_Times;
            SqlParameter paraSection_Director = new SqlParameter("@Section_Director", SqlDbType.VarChar, 20);
            paraSection_Director.Value = info.IemDiagInfo.Section_DirectorID;
            SqlParameter paraDirector = new SqlParameter("@Director", SqlDbType.VarChar, 20);
            paraDirector.Value = info.IemDiagInfo.DirectorID;
            SqlParameter paraVs_Employee_Code = new SqlParameter("@Vs_Employee_Code", SqlDbType.VarChar, 20);
            paraVs_Employee_Code.Value = info.IemDiagInfo.Vs_EmployeeID;
            SqlParameter paraResident_Employee_Code = new SqlParameter("@Resident_Employee_Code", SqlDbType.VarChar, 20);
            paraResident_Employee_Code.Value = info.IemDiagInfo.Resident_EmployeeID;
            SqlParameter paraRefresh_Employee_Code = new SqlParameter("@Refresh_Employee_Code", SqlDbType.VarChar, 20);
            paraRefresh_Employee_Code.Value = info.IemDiagInfo.Refresh_EmployeeID;
            //SqlParameter paraMaster_Interne = new SqlParameter("@Master_Interne", SqlDbType.VarChar, 20);
            //paraMaster_Interne.Value = info.IemDiagInfo.Master_InterneID;
            SqlParameter paraInterne = new SqlParameter("@Interne", SqlDbType.VarChar, 20);
            paraInterne.Value = info.IemDiagInfo.InterneID;
            SqlParameter paraCoding_User = new SqlParameter("@Coding_User", SqlDbType.VarChar, 20);
            paraCoding_User.Value = info.IemDiagInfo.Coding_UserID;
            SqlParameter paraMedical_Quality_Id = new SqlParameter("@Medical_Quality_Id", SqlDbType.VarChar, 1);
            paraMedical_Quality_Id.Value = info.IemDiagInfo.Medical_Quality_Id;
            SqlParameter paraQuality_Control_Doctor = new SqlParameter("@Quality_Control_Doctor", SqlDbType.VarChar, 20);
            paraQuality_Control_Doctor.Value = info.IemDiagInfo.Quality_Control_DoctorID;
            SqlParameter paraQuality_Control_Nurse = new SqlParameter("@Quality_Control_Nurse", SqlDbType.VarChar, 20);
            paraQuality_Control_Nurse.Value = info.IemDiagInfo.Quality_Control_NurseID;
            SqlParameter paraQuality_Control_Date = new SqlParameter("@Quality_Control_Date", SqlDbType.VarChar, 19);
            paraQuality_Control_Date.Value = info.IemDiagInfo.Quality_Control_Date;

            //////费用实体中取数据
            //SqlParameter paraIs_First_Case = new SqlParameter("@Is_First_Case", SqlDbType.VarChar, 1);
            //paraIs_First_Case.Value = info.IemFeeInfo.IsFirstCase;
            //SqlParameter paraIs_Following = new SqlParameter("@Is_Following", SqlDbType.VarChar, 1);
            //paraIs_Following.Value = info.IemFeeInfo.IsFollowing;
            //SqlParameter paraFollowing_Ending_Date = new SqlParameter("@Following_Ending_Date", SqlDbType.VarChar, 19);
            //paraFollowing_Ending_Date.Value = info.IemFeeInfo.Following_Ending_Date;
            //SqlParameter paraIs_Teaching_Case = new SqlParameter("@Is_Teaching_Case", SqlDbType.VarChar, 1);
            //paraIs_Teaching_Case.Value = info.IemFeeInfo.IsTeachingCase;
            //SqlParameter paraBlood_Type_id = new SqlParameter("@Blood_Type_id", SqlDbType.VarChar, 3);
            //paraBlood_Type_id.Value = info.IemFeeInfo.BloodType;
            //SqlParameter paraRh = new SqlParameter("@Rh", SqlDbType.VarChar, 4);
            //paraRh.Value = info.IemFeeInfo.Rh;
            //SqlParameter paraBlood_Reaction_Id = new SqlParameter("@Blood_Reaction_Id", SqlDbType.VarChar, 4);
            //paraBlood_Reaction_Id.Value = info.IemFeeInfo.BloodReaction;
            //SqlParameter paraBlood_Rbc = new SqlParameter("@Blood_Rbc", SqlDbType.VarChar, 4);
            //paraBlood_Rbc.Value = info.IemFeeInfo.Rbc;
            //SqlParameter paraBlood_Plt = new SqlParameter("@Blood_Plt", SqlDbType.VarChar, 4);
            //paraBlood_Plt.Value = info.IemFeeInfo.Plt;
            //SqlParameter paraBlood_Plasma = new SqlParameter("@Blood_Plasma", SqlDbType.VarChar, 4);
            //paraBlood_Plasma.Value = info.IemFeeInfo.Plasma;
            //SqlParameter paraBlood_Wb = new SqlParameter("@Blood_Wb", SqlDbType.VarChar, 4);
            //paraBlood_Wb.Value = info.IemFeeInfo.Wb;
            //SqlParameter paraBlood_Others = new SqlParameter("@Blood_Others", SqlDbType.VarChar, 60);
            //paraBlood_Others.Value = info.IemFeeInfo.Others;

            //SqlParameter paraZymosis = new SqlParameter("@Zymosis", SqlDbType.VarChar, 300);
            //paraZymosis.Value = info.IemDiagInfo.ZymosisID;
            SqlParameter paraHurt_Toxicosis_Ele = new SqlParameter("@Hurt_Toxicosis_Ele", SqlDbType.VarChar, 300);
            paraHurt_Toxicosis_Ele.Value = info.IemDiagInfo.Hurt_Toxicosis_Element;
            //SqlParameter paraZymosisState = new SqlParameter("@ZymosisState", SqlDbType.VarChar, 300);
            //paraZymosisState.Value = info.IemDiagInfo.ZymosisState;

            SqlParameter[] paraColl = new SqlParameter[] {paraiem_mainpage_no, paraPatNoOfHis, paraNoOfInpat, paraPayID, paraSocialCare, paraInCount, paraName,
                paraSexID, paraBirth, paraMarital ,paraJobID,
                //paraProvinceID,paraCountyID,
                paraNationID,paraNationalityID,paraIDNO,paraOrganization,paraOfficePlace,
            paraOfficeTEL,paraOfficePost,
            //paraNativeAddress,paraNativeTEL,
            //paraNativePost,
            paraContactPerson,paraRelationship,paraContactAddress,paraContactTEL,
            paraAdmitDate,paraAdmitDept,paraAdmitWard,
            //paraDays_Before,paraTrans_Date,
            paraTrans_AdmitDept,
            //paraTrans_AdmitWard,
            //paraTrans_AdmitDept_Again,
            paraOutWardDate,
            paraOutHosDept,paraOutHosWard,paraActual_Days,
            //paraDeath_Time,paraDeath_Reason,
            //paraAdmitInfo,paraIn_Check_Date,
            paraPathology_Diagnosis_Name,paraPathology_Observation_Sn,
            //paraAshes_Diagnosis_Name, paraAshes_Anatomise_Sn,
            paraAllergic_Drug,
            //paraHbsag,paraHcv_Ab,paraHiv_Ab,paraOpd_Ipd_Id,paraIn_Out_Inpatinet_Id,paraBefore_After_Or_Id,
            //paraClinical_Pathology_Id,paraPacs_Pathology_Id,paraSave_Times,paraSuccess_Times,
            paraSection_Director,paraDirector,paraVs_Employee_Code,paraResident_Employee_Code,
            paraRefresh_Employee_Code,
            //paraMaster_Interne,
            paraInterne,paraCoding_User,paraMedical_Quality_Id,paraQuality_Control_Doctor,paraQuality_Control_Nurse,paraQuality_Control_Date,
            //paraXay_Sn,paraCt_Sn,paraMri_Sn,paraDsa_Sn,
            //paraIs_First_Case,paraIs_Following,paraFollowing_Ending_Date,paraIs_Teaching_Case,paraBlood_Type_id,paraRh,
            //paraBlood_Reaction_Id,paraBlood_Rbc,paraBlood_Plt,paraBlood_Plasma,paraBlood_Wb,paraBlood_Others,
            paraIs_Completed,paracompleted_time,
            //paraZymosis,
            paraHurt_Toxicosis_Ele
            //,paraZymosisState
            };

            #endregion

            string no = sqlHelper.ExecuteDataTable("IEM_MAIN_PAGE.usp_Upateiembasicinfo", paraColl, CommandType.StoredProcedure).Rows[0][0].ToString();
            this.IemInfo.IemBasicInfo.Iem_Mainpage_NO = no;
        }


        /// <summary>
        /// 取消之前的诊断信息
        /// </summary>
        private void UpdateIemDiagnoseInfo_2012(IemMainPageInfo info, IDataAccess sqlHelper)
        {
            string sql = string.Format(@"UPDATE iem_mainpage_diagnosis_2012
                                               SET cancel_user = '{0}',
                                                   valide      = 0,
                                                   cancel_time = TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss')
                                             WHERE iem_mainpage_no = '{1}'
                                               AND valide = 1;", m_app.User.Id, info.IemBasicInfo.Iem_Mainpage_NO);

            sqlHelper.ExecuteNoneQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 取消之前的手术信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="sqlHelper"></param>
        private void UpdateIemOperInfo_2012(IemMainPageInfo info, IDataAccess sqlHelper)
        {
            //            string sql = string.Format(@"UPDATE iem_mainpage_operation_2012
            //                                               SET valide      = 0,
            //                                                   cancel_user = '{0}',
            //                                                   cancel_time = TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss')
            //                                             WHERE iem_mainpage_no = '{1}'
            //                                               AND valide = 1", m_app.User.Id, info.IemOperInfo.IEM_MainPage_NO);

            string sql = string.Format(@"UPDATE iem_mainpage_operation_2012
                                               SET valide      = 0,
                                                   cancel_user = '{0}',
                                                   cancel_time = TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss')
                                             WHERE iem_mainpage_no = '{1}'
                                               AND valide = 1", m_app.User.Id, info.IemBasicInfo.Iem_Mainpage_NO);

            sqlHelper.ExecuteNoneQuery(sql, CommandType.Text);

        }

        /// <summary>
        /// 取消之前的产妇婴儿信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="sqlHelper"></param>
        private void DeleteIemObstetricsBaby(Iem_MainPage_ObstetricsBaby info, IDataAccess sqlHelper)
        {
            string sql = string.Format("delete IEM_MAINPAGE_OBSTETRICSBABY where IEM_MAINPAGE_NO = '{0}'", IemInfo.IemBasicInfo.Iem_Mainpage_NO);

            sqlHelper.ExecuteNoneQuery(sql, CommandType.Text);

        }

        #endregion
        #endregion

        #region 更新Inpatient表中的信息
        /// <summary>
        /// 更新病人信息表 edit by ywk
        /// </summary>
        private void UpdateInPaient(IemMainPageInfo info, string isupdateIdNO, IDataAccess sqlHelper)
        {
            #region 参数
            SqlParameter paraNoOfInpat = new SqlParameter("@NOOFINPAT", SqlDbType.VarChar, 9);
            paraNoOfInpat.Value = info.IemBasicInfo.NoOfInpat;
            SqlParameter paraName = new SqlParameter("@Name", SqlDbType.VarChar, 64);
            paraName.Value = info.IemBasicInfo.Name;//姓名
            SqlParameter paraSexID = new SqlParameter("@SexID", SqlDbType.VarChar, 4);
            paraSexID.Value = info.IemBasicInfo.SexID;//性别
            SqlParameter paraBirth = new SqlParameter("@Birth", SqlDbType.VarChar, 10);
            if (info.IemBasicInfo.Birth == null)
            {
                info.IemBasicInfo.Birth = "-";
            }
            paraBirth.Value = info.IemBasicInfo.Birth.ToString().Trim();//出生日期
            SqlParameter paraAge = new SqlParameter("@Age", SqlDbType.Int, 10);
            // paraAge.Value = info.IemBasicInfo.Age;//年龄 edit by ywk
            if (info.IemBasicInfo.AdmitDate == null || info.IemBasicInfo.Birth == null)
            {
                paraAge.Value = "-";
            }
            else
            {
                paraAge.Value = DateTime.Parse(info.IemBasicInfo.AdmitDate).Year - DateTime.Parse(info.IemBasicInfo.Birth).Year;
            }
            //paraAge.Value = Int32.Parse(info.IemBasicInfo.Age.ToString().Replace('岁', ' '));//年龄
            SqlParameter paraIDNO = new SqlParameter("@IDNO", SqlDbType.VarChar, 18);
            paraIDNO.Value = info.IemBasicInfo.IDNO;//身份证号
            SqlParameter paraMarital = new SqlParameter("@Marital", SqlDbType.VarChar, 4);
            paraMarital.Value = info.IemBasicInfo.Marital;//婚姻状况
            SqlParameter paraJobID = new SqlParameter("@JobID", SqlDbType.VarChar, 4);
            paraJobID.Value = info.IemBasicInfo.JobID;//职业
            SqlParameter paraCSD_PROVINCEID = new SqlParameter("@CSD_PROVINCEID", SqlDbType.VarChar, 20);
            paraCSD_PROVINCEID.Value = info.IemBasicInfo.CSD_ProvinceID;//出生地省
            SqlParameter paraCSD_CITYID = new SqlParameter("@CSD_CITYID", SqlDbType.VarChar, 20);
            paraCSD_CITYID.Value = info.IemBasicInfo.CSD_CityID;//出生地市
            SqlParameter paraNationID = new SqlParameter("@NationID", SqlDbType.VarChar, 4);
            paraNationID.Value = info.IemBasicInfo.NationID;//民族
            SqlParameter paraNationalityID = new SqlParameter("@NationalityID", SqlDbType.VarChar, 4);
            paraNationalityID.Value = info.IemBasicInfo.NationalityID;//国籍
            SqlParameter paraJG_PROVINCEID = new SqlParameter("@JG_PROVINCEID", SqlDbType.VarChar, 20);
            paraJG_PROVINCEID.Value = info.IemBasicInfo.JG_ProvinceID;//籍贯省
            SqlParameter paraJG_CITYID = new SqlParameter("@JG_CITYID", SqlDbType.VarChar, 20);
            paraJG_CITYID.Value = info.IemBasicInfo.JG_CityID;//籍贯市
            SqlParameter paraOfficePlace = new SqlParameter("@OfficePlace", SqlDbType.VarChar, 64);
            paraOfficePlace.Value = info.IemBasicInfo.OfficePlace;//工作单位地址
            SqlParameter paraOfficeTEL = new SqlParameter("@OfficeTEL", SqlDbType.VarChar, 16);
            paraOfficeTEL.Value = info.IemBasicInfo.OfficeTEL;//工作单位电话
            SqlParameter paraOfficePost = new SqlParameter("@OfficePost", SqlDbType.VarChar, 16);
            paraOfficePost.Value = info.IemBasicInfo.OfficePost;//工资单位邮编
            SqlParameter paraHKDZ_POST = new SqlParameter("@HKDZ_POST", SqlDbType.VarChar, 20);
            paraHKDZ_POST.Value = info.IemBasicInfo.HKDZ_Post;//户口住址邮编
            SqlParameter paraContactPerson = new SqlParameter("@ContactPerson", SqlDbType.VarChar, 32);
            paraContactPerson.Value = info.IemBasicInfo.ContactPerson;//联系人姓名
            SqlParameter paraRelationship = new SqlParameter("@Relationship", SqlDbType.VarChar, 4);
            paraRelationship.Value = info.IemBasicInfo.RelationshipID;//联系人关系
            SqlParameter paraContactAddress = new SqlParameter("@ContactAddress", SqlDbType.VarChar, 255);
            paraContactAddress.Value = info.IemBasicInfo.ContactAddress;//联系人地址 
            SqlParameter paraContactTEL = new SqlParameter("@ContactTEL", SqlDbType.VarChar, 16);
            paraContactTEL.Value = info.IemBasicInfo.ContactTEL;//联系人电话
            SqlParameter paraAdmitDept = new SqlParameter("@AdmitDept", SqlDbType.VarChar, 12);
            paraAdmitDept.Value = info.IemBasicInfo.AdmitDeptID;//入院科别
            SqlParameter paraAdmitWard = new SqlParameter("@AdmitWard", SqlDbType.VarChar, 12);
            paraAdmitWard.Value = info.IemBasicInfo.AdmitWardID;//入院病区
            SqlParameter paraAdmitDate = new SqlParameter("@AdmitDate", SqlDbType.VarChar, 19);
            paraAdmitDate.Value = info.IemBasicInfo.AdmitDate;//入院日期
            SqlParameter paraOutWardDate = new SqlParameter("@OutWardDate", SqlDbType.VarChar, 19);
            paraOutWardDate.Value = info.IemBasicInfo.OutWardDate;//出院日期
            SqlParameter paraOutHosDept = new SqlParameter("@OutHosDept", SqlDbType.VarChar, 12);
            paraOutHosDept.Value = info.IemBasicInfo.OutHosDeptID;//出院科别
            SqlParameter paraOutHosWard = new SqlParameter("@OutHosWard", SqlDbType.VarChar, 12);
            paraOutHosWard.Value = info.IemBasicInfo.OutHosWardID;//出院病区
            SqlParameter paraActual_Days = new SqlParameter("@ACTUALDAYS", SqlDbType.VarChar, 4);
            paraActual_Days.Value = info.IemBasicInfo.ActualDays;//住院天数


            //新增更新掉inpatient表里的AgeStr字段2012年5月9日9:18:54（泗县。。）ywk
            SqlParameter paraAgeStr = new SqlParameter("@AgeStr", SqlDbType.VarChar, 16);
            //paraAgeStr.Value = PatientBasicInfo.CalcDisplayAge(DateTime.Parse(info.IemBasicInfo.Birth),DateTime.Now);//病人的年龄（精确到月，天）
            //显示的时间，inpatient表的agestr字段跟病案首页输入的一致，age字段是入院时间-出生日期得到的
            paraAgeStr.Value = info.IemBasicInfo.Age;


            //新增付费方式字段的更新操作
            //add by ywk 2012年5月14日 15:59:34
            SqlParameter paraPatId = new SqlParameter("@PatID", SqlDbType.VarChar, 32);
            paraPatId.Value = info.IemBasicInfo.PayID;//付款方式

            //新增健康卡号方式字段的更新操作
            //add by ywk 2012年5月14日 15:59:34
            SqlParameter paraCardNo = new SqlParameter("@CardNo", SqlDbType.VarChar, 32);
            paraCardNo.Value = info.IemBasicInfo.CardNumber;//付款方式

            //2012年5月16日 09:49:51新增 （出生地）县、现住址（省，市，县，电话）、户口住址（省，市，县）add by ywk 
            SqlParameter paraCSDDistrictid = new SqlParameter("@Districtid", SqlDbType.VarChar, 32);
            paraCSDDistrictid.Value = info.IemBasicInfo.CSD_DistrictID;//（出生地）县
            SqlParameter paraXZZProviceid = new SqlParameter("@xzzproviceid", SqlDbType.VarChar, 32);
            paraXZZProviceid.Value = info.IemBasicInfo.XZZ_ProvinceID;//现住址（省
            SqlParameter paraXZZCityid = new SqlParameter("@xzzcityid", SqlDbType.VarChar, 32);
            paraXZZCityid.Value = info.IemBasicInfo.XZZ_CityID;//现住址（市
            SqlParameter paraXZZDistrictid = new SqlParameter("@xzzdistrictid", SqlDbType.VarChar, 32);
            paraXZZDistrictid.Value = info.IemBasicInfo.XZZ_DistrictID;//现住址（县
            SqlParameter paraXZZTel = new SqlParameter("@xzztel", SqlDbType.VarChar, 32);
            paraXZZTel.Value = info.IemBasicInfo.XZZ_TEL;//现住址（电话
            SqlParameter paraHKProviceid = new SqlParameter("@hkdzproviceid", SqlDbType.VarChar, 32);
            paraHKProviceid.Value = info.IemBasicInfo.HKDZ_ProvinceID;//户口住址（省
            SqlParameter paraHKCityid = new SqlParameter("@hkzdcityid", SqlDbType.VarChar, 32);
            paraHKCityid.Value = info.IemBasicInfo.HKDZ_CityID;//户口住址（市
            SqlParameter paraHKDistrictid = new SqlParameter("@hkzddistrictid", SqlDbType.VarChar, 32);
            paraHKDistrictid.Value = info.IemBasicInfo.HKDZ_DistrictID;//户口住址（县
            SqlParameter paraXZZPost = new SqlParameter("@xzzpost", SqlDbType.VarChar, 32);
            paraXZZPost.Value = info.IemBasicInfo.XZZ_Post;//现住址（邮编

            //SqlParameter paraCSDProviceName = new SqlParameter("@csdprovicename", SqlDbType.VarChar, 50);
            //paraCSDProviceName.Value = info.IemBasicInfo.CSD_ProvinceName;//出生地省名称
            //SqlParameter paraCSDCityName = new SqlParameter("@csdcityname", SqlDbType.VarChar, 50);
            //paraCSDCityName.Value = info.IemBasicInfo.CSD_CityName;//出生地市名称
            //SqlParameter paraCSDDistrictName = new SqlParameter("@csddistrictname", SqlDbType.VarChar, 50);
            //paraCSDDistrictName.Value = info.IemBasicInfo.CSD_DistrictName;//出生地县名称
            //SqlParameter paraJGProviceName = new SqlParameter("@jgprovicename", SqlDbType.VarChar, 50);
            //paraJGProviceName.Value = info.IemBasicInfo.JG_ProvinceName;//籍贯省名称
            //SqlParameter paraJGCityName = new SqlParameter("@jgcityname", SqlDbType.VarChar, 50);
            //paraJGCityName.Value = info.IemBasicInfo.JG_CityName;//籍贯市名称
            //SqlParameter paraXZZProviceName = new SqlParameter("@xzzprovicename", SqlDbType.VarChar, 50);
            //paraXZZProviceName.Value = info.IemBasicInfo.XZZ_ProvinceName;//现住址省名称D:\DrectSoft.Net\EMRNew\Core\病案首页\IEMMainPage_2012\IemMainPageManger.cs
            //SqlParameter paraXZZCityName = new SqlParameter("@xzzcityname", SqlDbType.VarChar, 50);
            //paraXZZCityName.Value = info.IemBasicInfo.XZZ_CityName;//现住址市名称
            //SqlParameter paraXZZDistrictName = new SqlParameter("@xzzdistrictname", SqlDbType.VarChar, 50);
            //paraXZZDistrictName.Value = info.IemBasicInfo.XZZ_DistrictName;//现住址县名称
            //SqlParameter paraHKZZProviceName = new SqlParameter("@hkzzprovicename", SqlDbType.VarChar, 50);
            //paraHKZZProviceName.Value = info.IemBasicInfo.HKDZ_ProvinceName;//户口住址省名称
            //SqlParameter paraHKZZCityName = new SqlParameter("@hkzzcityname", SqlDbType.VarChar, 50);
            //paraHKZZCityName.Value = info.IemBasicInfo.HKDZ_CityName;//户口住址市名称
            //SqlParameter paraHKZZDistrictName = new SqlParameter("@hkzzdistrictname", SqlDbType.VarChar, 50);
            //paraHKZZDistrictName.Value = info.IemBasicInfo.HKDZ_DistrictName;//户口住址县名称


            #region 新增的几个地址读取的具体名称，新加到参数里去
            SqlParameter paracsdaddress = new SqlParameter("@CSDADDRESS", SqlDbType.VarChar, 500);
            paracsdaddress.Value = info.IemBasicInfo.CSDAddress;//出生地
            SqlParameter parajgaddress = new SqlParameter("@JGADDRESS", SqlDbType.VarChar, 500);
            parajgaddress.Value = info.IemBasicInfo.JGAddress;//籍贯地址
            SqlParameter paraxzzdaddress = new SqlParameter("@XZZADDRESS", SqlDbType.VarChar, 500);
            paraxzzdaddress.Value = info.IemBasicInfo.XZZAddress;//现住址
            SqlParameter parahkdzaddress = new SqlParameter("@HKDZADDRESS", SqlDbType.VarChar, 500);
            parahkdzaddress.Value = info.IemBasicInfo.HKZZAddress;//户口地址

            #endregion

            SqlParameter paraIsUpdateIDNO = new SqlParameter("@isupdate", SqlDbType.VarChar, 5);
            paraIsUpdateIDNO.Value = isupdateIdNO;//新增一个字段用于判断是否将身份证号字段更新到noofclinic字段

            SqlParameter[] paraColl = new SqlParameter[] { paraNoOfInpat,paraName ,paraSexID,paraBirth,paraAge,paraIDNO,
            paraMarital, paraJobID, paraCSD_PROVINCEID,paraCSD_CITYID,paraNationID,paraNationalityID,paraJG_PROVINCEID,paraJG_CITYID,
            paraOfficePlace,
                paraOfficeTEL,paraOfficePost,paraHKDZ_POST,paraContactPerson,paraRelationship,paraContactAddress,
                paraContactTEL,paraAdmitDept,paraAdmitWard,paraAdmitDate
                ,paraOutWardDate,paraOutHosDept,paraOutHosWard, paraActual_Days,paraAgeStr,paraPatId,paraCardNo,
                paraCSDDistrictid,paraXZZProviceid,paraXZZCityid,paraXZZDistrictid,paraXZZTel,paraHKProviceid,paraHKCityid,paraHKDistrictid,paraXZZPost
                ,paraIsUpdateIDNO,paracsdaddress,parajgaddress,paraxzzdaddress,parahkdzaddress};
            try
            {
                //if (sqlHelper == null)
                //    DataAccessFactory.GetSqlDataAccess();
                //sqlHelper.BeginUseSingleConnection();
                sqlHelper.ExecuteNoneQuery("IEM_MAIN_PAGE.usp_Edit_Iem_PaientInfo", paraColl, CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                m_app.CustomMessageBox.MessageShow(ex.Message);
            }
            #endregion

        }
        #endregion
        /// <summary>
        /// 根据首页序号获得病人的信息 以判断是不是婴儿
        /// add by ywk 
        /// </summary>
        /// <param name="m_noofinpat"></param>
        /// <returns></returns>
        internal DataTable GetPatData(string m_noofinpat)
        {
            string sql = string.Format(@"select * from inpatient where noofinpat='{0}' ", m_noofinpat);
            return m_app.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
        }

    }
}
