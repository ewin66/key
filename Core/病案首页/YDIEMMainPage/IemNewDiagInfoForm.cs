using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;
using DrectSoft.Common.Library;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;

namespace DrectSoft.Core.IEMMainPage
{
    public partial class IemNewDiagInfoForm : DevExpress.XtraEditors.XtraForm
    {
        private IEmrHost m_App;

        private Iem_Mainpage_Diagnosis m_IemDiagInfo = new Iem_Mainpage_Diagnosis();
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Iem_Mainpage_Diagnosis IemOperInfo
        {
            get
            {
                GetUI();
                return m_IemDiagInfo;
            }
            set
            {
                m_IemDiagInfo = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable DataOper
        {
            get
            {
                if (m_DataOper == null)
                    m_DataOper = new DataTable();

                GetDataOper();
                return m_DataOper;
            }
        }
        private DataTable m_DataOper;

        private int m_DiagnosisNo;
        private int m_MainpageNo;
        private string m_OPETYPE;//������ҳ�洫���Ĳ������ͱ�ʶ  add ywk
        private string m_DIAGCODE;//������ICD����
        private string m_STATUSID;//��������Ժ����
        private string m_AdmitInfo;//����������Ժ���� 
        private string m_StatusIDOut;//��Ժ���

        public IemNewDiagInfoForm(IEmrHost app, string operatetype, string diagcode, string statusid, string admitinfo)
        {
            try
            {
                InitializeComponent();
                m_App = app;
                InitLookUpEditor();

                InitInHosPatiResult();
                InitSubInHosPatiResult();
                InitOutHosPatiResult();

                m_OPETYPE = operatetype;
                m_DIAGCODE = diagcode;
                m_STATUSID = statusid;
                m_AdmitInfo = admitinfo;
                BridFormValue(m_OPETYPE, m_DIAGCODE, m_STATUSID, m_AdmitInfo);

                IemMainPageManger IemM = new IemMainPageManger(m_App, m_App.CurrentPatientInfo);
                string cansee = IemM.GetConfigValueByKey("EmrInputConfig");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(cansee);
                if (doc.GetElementsByTagName("IemPageContorlVisable")[0].InnerText == "0")//���ɼ�
                {
                    #region ע��
                    //labelControl4.Visible = false;
                    //chkMandZ0.Visible = false;
                    //chkMandZ1.Visible = false;
                    //chkMandZ2.Visible = false;
                    //chkMandZ3.Visible = false;
                    //labelControl5.Visible = false;
                    //chkRandC0.Visible = false;
                    //chkRandC1.Visible = false;
                    //chkRandC2.Visible = false;
                    //chkRandC3.Visible = false;
                    //labelControl6.Visible = false;
                    //labelControl7.Visible = false;
                    //labelControl8.Visible = false;
                    //labelControl9.Visible = false;
                    //chkSqAndSh0.Visible = false;
                    //chkSqAndSh1.Visible = false;
                    //chkSqAndSh2.Visible = false;
                    //chkSqAndSh3.Visible = false;
                    //chkLandB0.Visible = false;
                    //chkLandB1.Visible = false;
                    //chkLandB2.Visible = false;
                    //chkLandB3.Visible = false;
                    //chkRThree0.Visible = false;
                    //chkRThree1.Visible = false;
                    //chkRThree2.Visible = false;
                    //chkRThree3.Visible = false;
                    //chkFandB0.Visible = false;
                    //chkFandB1.Visible = false;
                    //chkFandB2.Visible = false;
                    //chkFandB3.Visible = false;
                    #endregion

                    #region ע�� by cyq 2012-12-25
                    //Point M = new Point(100, 100);
                    //Point M1 = new Point(280, 100);
                    //btnConfirm.Location = M;
                    //btnCancel.Location = M1;
                    #endregion
                }
                if (doc.GetElementsByTagName("IemPageContorlVisable")[0].InnerText == "1")//�ɼ�
                {
                    #region ע��
                    //labelControl4.Visible = true;
                    //chkMandZ0.Visible = true;
                    //chkMandZ1.Visible = true;
                    //chkMandZ2.Visible = true;
                    //chkMandZ3.Visible = true;
                    //labelControl5.Visible = true;
                    //chkRandC0.Visible = true;
                    //chkRandC1.Visible = true;
                    //chkRandC2.Visible = true;
                    //chkRandC3.Visible = true;
                    //labelControl6.Visible = true;
                    //labelControl7.Visible = true;
                    //labelControl8.Visible = true;
                    //labelControl9.Visible = true;
                    //chkSqAndSh0.Visible = true;
                    //chkSqAndSh1.Visible = true;
                    //chkSqAndSh2.Visible = true;
                    //chkSqAndSh3.Visible = true;
                    //chkLandB0.Visible = true;
                    //chkLandB1.Visible = true;
                    //chkLandB2.Visible = true;
                    //chkLandB3.Visible = true;
                    //chkRThree0.Visible = true;
                    //chkRThree1.Visible = true;
                    //chkRThree2.Visible = true;
                    //chkRThree3.Visible = true;
                    //chkFandB0.Visible = true;
                    //chkFandB1.Visible = true;
                    //chkFandB2.Visible = true;
                    //chkFandB3.Visible = true;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="app">����ӿ�</param>
        /// <param name="operatetype">������ʽ</param>
        /// <param name="diagcode">��ϵ�ICD����</param>
        /// <param name="statusid">��Ͻ������Ժ����)</param>
        /// <param name="admitinfo">����Ժ����</param>
        /// <param name="statusIdOut">��Ժ���</param>
        public IemNewDiagInfoForm(IEmrHost app, string operatetype, string diagcode, string statusid, string admitinfo, string statusIdOut)
        {
            try
            {
                InitializeComponent();
                m_App = app;
                InitLookUpEditor();

                InitInHosPatiResult();
                InitSubInHosPatiResult();
                InitOutHosPatiResult();

                m_OPETYPE = operatetype;
                m_DIAGCODE = diagcode;
                m_STATUSID = statusid;
                m_AdmitInfo = admitinfo;
                m_StatusIDOut = statusIdOut;
                BridFormValue(m_OPETYPE, m_DIAGCODE, m_STATUSID, m_AdmitInfo);

                IemMainPageManger IemM = new IemMainPageManger(m_App, m_App.CurrentPatientInfo);
                string cansee = IemM.GetConfigValueByKey("EmrInputConfig");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(cansee);

            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���ع���
        /// </summary>
        /// <param name="app"></param>
        /// <param name="operatetype"></param>
        /// <param name="diagcode"></param>
        /// <param name="statusid"></param>
        /// <param name="admitinfo"></param>
        /// <param name="statusIDOut"></param>
        /// <param name="diagnosisNo"></param>
        /// <param name="mainpageNo"></param>
        public IemNewDiagInfoForm(IEmrHost app, string operatetype, string diagcode, string statusid, string admitinfo, string statusIDOut, int diagnosisNo, int mainpageNo)
        {
            try
            {
                InitializeComponent();
                m_App = app;
                InitLookUpEditor();

                InitInHosPatiResult();
                InitSubInHosPatiResult();
                InitOutHosPatiResult();

                m_DiagnosisNo = diagnosisNo;
                m_MainpageNo = mainpageNo;
                m_OPETYPE = operatetype;
                m_DIAGCODE = diagcode;
                m_STATUSID = statusid;
                m_AdmitInfo = admitinfo;
                m_StatusIDOut = statusIDOut;
                BridFormValue(m_OPETYPE, m_DIAGCODE, m_STATUSID, m_AdmitInfo, m_StatusIDOut);

                IemMainPageManger IemM = new IemMainPageManger(m_App, m_App.CurrentPatientInfo);
                string cansee = IemM.GetConfigValueByKey("EmrInputConfig");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(cansee);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CanSeeContorl;

        /// <summary>
        /// ������ת��������״ֵ̬�����ƴ�ҳ�����ʾ���
        /// add by ywk 
        /// Modify by xlb 2013-05-22
        /// </summary>
        /// <param name="operatetype"></param>
        private void BridFormValue(string operatetype, string diagcode, string statusid, string admitinfo)
        {
            if (operatetype == "add")
            {
                this.lueOutDiag.CodeValue = "";
                this.lue_inHosPatiResult.EditValue = string.Empty;
                this.lue_subInHosPatiResult.EditValue = string.Empty;
            }
            if (operatetype == "edit")
            {
                if (!string.IsNullOrEmpty(diagcode))
                {
                    lueOutDiag.CodeValue = diagcode;
                }
                if (!string.IsNullOrEmpty(statusid))
                {
                    this.lue_inHosPatiResult.EditValue = statusid;

                    //����ϼ�ҳ�洫�������У�����ʾ����Ŀ�ļ����ؼ� add by ywk 2012��7��26��15:09:15
                    if (statusid == "1")
                    {
                        CanSeeContorl = "1";
                        lue_subInHosPatiResult.Visible = true;
                        //labelControl1.Visible = true;
                        //chkAdmitInfo1.Visible = true;
                        //chkAdmitInfo2.Visible = true;
                        //chkAdmitInfo3.Visible = true;
                        //chkAdmitInfo4.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(admitinfo))
                    {
                        lue_subInHosPatiResult.EditValue = admitinfo;
                    }
                }
                if (!string.IsNullOrEmpty(m_StatusIDOut))
                {
                    lue_outHosPatiResult.EditValue = m_StatusIDOut;
                }
                //�༭״̬������������������ϵķ������
                #region ������Ϸ�������ĸ�ѡ��
                IemMainPageManger IemM = new IemMainPageManger(m_App, m_App.CurrentPatientInfo);
                DataTable DTIemDiag = IemM.GetIemInfo().IemDiagInfo.OutDiagTable;
                DataTable NewDt = DTIemDiag.Clone();
                DataRow[] SpliteRows = DTIemDiag.Select("DIAGNOSIS_CODE='" + diagcode + "'");
                if (SpliteRows.Length > 0)
                {
                    for (int i = 0; i < SpliteRows.Length; i++)
                    {
                        NewDt.ImportRow(SpliteRows[i]);
                    }
                }
                if (NewDt.Rows.Count > 0)
                {
                    //if (NewDt.Rows[0]["AdmitInfo"].ToString() == "1")
                    //{
                    //    chkAdmitInfo1.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["AdmitInfo"].ToString() == "2")
                    //{
                    //    chkAdmitInfo2.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["AdmitInfo"].ToString() == "3")
                    //{
                    //    chkAdmitInfo3.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["AdmitInfo"].ToString() == "4")
                    //{
                    //    chkAdmitInfo4.Checked = true;
                    //}
                    //�����סԺ
                    //if (NewDt.Rows[0]["MENANDINHOP"].ToString() == "0")
                    //{
                    //    chkMandZ0.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["MENANDINHOP"].ToString() == "1")
                    //{
                    //    chkMandZ1.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["MENANDINHOP"].ToString() == "2")
                    //{
                    //    chkMandZ2.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["MENANDINHOP"].ToString() == "3")
                    //{
                    //    chkMandZ3.Checked = true;
                    //}
                    ////��Ժ�ͳ�Ժ
                    //if (NewDt.Rows[0]["INHOPANDOUTHOP"].ToString() == "0")
                    //{
                    //    chkRandC0.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["INHOPANDOUTHOP"].ToString() == "1")
                    //{
                    //    chkRandC1.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["INHOPANDOUTHOP"].ToString() == "2")
                    //{
                    //    chkRandC2.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["INHOPANDOUTHOP"].ToString() == "3")
                    //{
                    //    chkRandC3.Checked = true;
                    //}
                    ////��ǰ������
                    //if (NewDt.Rows[0]["BEFOREOPEANDAFTEROPER"].ToString() == "0")
                    //{
                    //    chkSqAndSh0.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["BEFOREOPEANDAFTEROPER"].ToString() == "1")
                    //{
                    //    chkSqAndSh1.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["BEFOREOPEANDAFTEROPER"].ToString() == "2")
                    //{
                    //    chkSqAndSh2.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["BEFOREOPEANDAFTEROPER"].ToString() == "3")
                    //{
                    //    chkSqAndSh3.Checked = true;
                    //}
                    ////�ٴ��Ͳ�����
                    //if (NewDt.Rows[0]["LINANDBINGLI"].ToString() == "0")
                    //{
                    //    chkLandB0.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["LINANDBINGLI"].ToString() == "1")
                    //{
                    //    chkLandB1.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["LINANDBINGLI"].ToString() == "2")
                    //{
                    //    chkLandB2.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["LINANDBINGLI"].ToString() == "3")
                    //{
                    //    chkLandB3.Checked = true;
                    //}
                    ////��Ժ������
                    //if (NewDt.Rows[0]["INHOPTHREE"].ToString() == "0")
                    //{
                    //    chkRThree0.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["INHOPTHREE"].ToString() == "1")
                    //{
                    //    chkRThree1.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["INHOPTHREE"].ToString() == "2")
                    //{
                    //    chkRThree2.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["INHOPTHREE"].ToString() == "3")
                    //{
                    //    chkRThree3.Checked = true;
                    //}
                    ////����Ͳ���
                    //if (NewDt.Rows[0]["FANGANDBINGLI"].ToString() == "0")
                    //{
                    //    chkFandB0.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["FANGANDBINGLI"].ToString() == "1")
                    //{
                    //    chkFandB1.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["FANGANDBINGLI"].ToString() == "2")
                    //{
                    //    chkFandB2.Checked = true;
                    //}
                    //if (NewDt.Rows[0]["FANGANDBINGLI"].ToString() == "3")
                    //{
                    //    chkFandB3.Checked = true;
                    //}
                }
                #endregion
            }

        }

        /// <summary>
        /// ������ת��������״ֵ̬�����ƴ�ҳ�����ʾ���
        /// add by ywk 
        /// </summary>
        /// <param name="operatetype"></param>
        private void BridFormValue(string operatetype, string diagcode, string statusid, string admitinfo, string statusIDOut)
        {
            if (operatetype == "add")
            {
                this.lueOutDiag.CodeValue = "";
                this.lue_inHosPatiResult.EditValue = string.Empty;
                this.lue_subInHosPatiResult.EditValue = string.Empty;
            }
            if (operatetype == "edit")
            {
                if (!string.IsNullOrEmpty(diagcode))
                {
                    lueOutDiag.CodeValue = diagcode;
                }
                if (!string.IsNullOrEmpty(statusid))
                {
                    this.lue_inHosPatiResult.EditValue = statusid;
                    this.lue_outHosPatiResult.EditValue = statusIDOut;

                    //����ϼ�ҳ�洫�������У�����ʾ����Ŀ�ļ����ؼ� add by ywk 2012��7��26��15:09:15
                    if (statusid == "1")
                    {
                        CanSeeContorl = "1";
                        lue_subInHosPatiResult.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(admitinfo))
                    {
                        lue_subInHosPatiResult.EditValue = admitinfo;
                    }
                }
                //�༭״̬������������������ϵķ������
                #region ������Ϸ�������ĸ�ѡ��
                IemMainPageManger IemM = new IemMainPageManger(m_App, m_App.CurrentPatientInfo);
                DataTable DTIemDiag = IemM.GetIemInfo().IemDiagInfo.OutDiagTable;
                DataTable NewDt = DTIemDiag.Clone();
                DataRow[] SpliteRows = DTIemDiag.Select("DIAGNOSIS_CODE='" + diagcode + "'");
                if (SpliteRows.Length > 0)
                {
                    for (int i = 0; i < SpliteRows.Length; i++)
                    {
                        NewDt.ImportRow(SpliteRows[i]);
                    }
                }
                #endregion
            }

        }

        private void IemNewDiagInfoForm_Load(object sender, EventArgs e)
        {

#if DEBUG
#else
            HideSbutton();
#endif
            //�����������Ժ�����ǲ���ʾ�� 
            labelControl1.Visible = false;
            lue_subInHosPatiResult.Visible = false;
            if (CanSeeContorl == "1")
            {
                labelControl1.Visible = true;
                lue_subInHosPatiResult.Visible = true;
            }
            this.ActiveControl = lueOutDiag;
        }


        private void InitLookUpEditor()
        {
            BindLueData(lueOutDiag, 12);
        }
        #region ��LUE
        private void BindLueData(LookUpEditor lueInfo, Decimal queryType)
        {
            try
            {
                LookUpWindow lupInfo = new LookUpWindow();
                lupInfo.SqlHelper = m_App.SqlHelper;
                DataTable dataTable = GetEditroData(queryType);

                dataTable.Columns["ID"].Caption = "��ϱ���";
                dataTable.Columns["NAME"].Caption = "�������";
                Dictionary<string, int> columnwidth = new Dictionary<String, Int32>();
                columnwidth.Add("ID", 90);
                columnwidth.Add("NAME", 210);
                SqlWordbook sqlWordBook = new SqlWordbook("ID", dataTable, "ID", "NAME", columnwidth, "ID//NAME//PY//WB");

                lueInfo.SqlWordbook = sqlWordBook;
                lueInfo.ListWindow = lupInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��ȡlue������Դ
        /// </summary>
        /// <param name="queryType"></param>
        /// <returns></returns>
        private DataTable GetEditroData(Decimal queryType)
        {
            try
            {
                SqlParameter paraType = new SqlParameter("@QueryType", SqlDbType.Decimal);
                paraType.Value = queryType;
                SqlParameter[] paramCollection = new SqlParameter[] { paraType };
                DataTable dataTable = AddTableColumn(m_App.SqlHelper.ExecuteDataTable("usp_GetLookUpEditorData", paramCollection, CommandType.StoredProcedure));
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// ��lue������Դ������ ���� ��λ
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private DataTable AddTableColumn(DataTable dataTable)
        {
            DataTable dataTableAdd = dataTable;
            if (!dataTableAdd.Columns.Contains("����"))
                dataTableAdd.Columns.Add("����");
            foreach (DataRow row in dataTableAdd.Rows)
                row["����"] = row["Name"].ToString();
            return dataTableAdd;
        }

        /// <summary>
        /// ��ʼ����Ժ����
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-25</date>
        private void InitInHosPatiResult()
        {
            try
            {
                DataTable dt = GetInHosPatiResult();

                lue_inHosPatiResult.Properties.DataSource = dt;
                lue_inHosPatiResult.Properties.DisplayMember = "NAME";
                lue_inHosPatiResult.Properties.ValueMember = "ID";
                lue_inHosPatiResult.Properties.ShowHeader = false;
                lue_inHosPatiResult.Properties.ShowFooter = false;

                LookUpColumnInfoCollection coll = lue_inHosPatiResult.Properties.Columns;
                coll.Add(new LookUpColumnInfo("ID".ToUpper()));
                coll.Add(new LookUpColumnInfo("NAME".ToUpper()));
                lue_inHosPatiResult.Properties.Columns["NAME"].Visible = true;
                lue_inHosPatiResult.Properties.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��ʼ������Ժ����
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-25</date>
        private void InitSubInHosPatiResult()
        {
            try
            {
                DataTable dt = GetSubInHosPatiResult();

                lue_subInHosPatiResult.Properties.DataSource = dt;
                lue_subInHosPatiResult.Properties.DisplayMember = "NAME";
                lue_subInHosPatiResult.Properties.ValueMember = "ID";
                lue_subInHosPatiResult.Properties.ShowHeader = false;
                lue_subInHosPatiResult.Properties.ShowFooter = false;

                LookUpColumnInfoCollection coll = lue_subInHosPatiResult.Properties.Columns;
                coll.Add(new LookUpColumnInfo("ID".ToUpper()));
                coll.Add(new LookUpColumnInfo("NAME".ToUpper()));
                lue_subInHosPatiResult.Properties.Columns["NAME"].Visible = true;
                lue_subInHosPatiResult.Properties.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��ʼ����Ժ����
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-25</date>
        private void InitOutHosPatiResult()
        {
            try
            {
                DataTable dt = GetOutHosPatiResult();

                lue_outHosPatiResult.Properties.DataSource = dt;
                lue_outHosPatiResult.Properties.DisplayMember = "NAME";
                lue_outHosPatiResult.Properties.ValueMember = "ID";
                lue_outHosPatiResult.Properties.ShowHeader = false;
                lue_outHosPatiResult.Properties.ShowFooter = false;

                LookUpColumnInfoCollection coll = lue_outHosPatiResult.Properties.Columns;
                coll.Add(new LookUpColumnInfo("ID".ToUpper()));
                coll.Add(new LookUpColumnInfo("NAME".ToUpper()));
                lue_outHosPatiResult.Properties.Columns["NAME"].Visible = true;
                lue_outHosPatiResult.Properties.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region ��ȡ���������ݼ�
        /// <summary>
        /// ��Ժ���� ���������ݼ�
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-25</date>
        /// <returns></returns>
        private DataTable GetInHosPatiResult()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");

                DataRow row1 = dt.NewRow();
                row1["ID"] = "";
                row1["NAME"] = "";
                dt.Rows.Add(row1);

                DataRow row2 = dt.NewRow();
                row2["ID"] = "1";
                row2["NAME"] = "��";
                dt.Rows.Add(row2);

                DataRow row3 = dt.NewRow();
                row3["ID"] = "2";
                row3["NAME"] = "�ٴ�δȷ��";
                dt.Rows.Add(row3);

                DataRow row4 = dt.NewRow();
                row4["ID"] = "3";
                row4["NAME"] = "�������";
                dt.Rows.Add(row4);

                DataRow row5 = dt.NewRow();
                row5["ID"] = "4";
                row5["NAME"] = "��";
                dt.Rows.Add(row5);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ����Ժ���� ���������ݼ�
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-25</date>
        /// <returns></returns>
        private DataTable GetSubInHosPatiResult()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");

                DataRow row1 = dt.NewRow();
                row1["ID"] = "";
                row1["NAME"] = "";
                dt.Rows.Add(row1);

                DataRow row2 = dt.NewRow();
                row2["ID"] = "1";
                row2["NAME"] = "Σ";
                dt.Rows.Add(row2);

                DataRow row3 = dt.NewRow();
                row3["ID"] = "2";
                row3["NAME"] = "��";
                dt.Rows.Add(row3);

                DataRow row4 = dt.NewRow();
                row4["ID"] = "3";
                row4["NAME"] = "һ��";
                dt.Rows.Add(row4);

                DataRow row5 = dt.NewRow();
                row5["ID"] = "4";
                row5["NAME"] = "��";
                dt.Rows.Add(row5);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��Ժ��� ���������ݼ�
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-25</date>
        /// <returns></returns>
        private DataTable GetOutHosPatiResult()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");

                DataRow row1 = dt.NewRow();
                row1["ID"] = "";
                row1["NAME"] = "";
                dt.Rows.Add(row1);

                DataRow row2 = dt.NewRow();
                row2["ID"] = "1";
                row2["NAME"] = "����";
                dt.Rows.Add(row2);

                DataRow row3 = dt.NewRow();
                row3["ID"] = "2";
                row3["NAME"] = "��ת";
                dt.Rows.Add(row3);

                DataRow row4 = dt.NewRow();
                row4["ID"] = "3";
                row4["NAME"] = "δ��";
                dt.Rows.Add(row4);

                DataRow row5 = dt.NewRow();
                row5["ID"] = "4";
                row5["NAME"] = "����";
                dt.Rows.Add(row5);

                DataRow row6 = dt.NewRow();
                row6["ID"] = "9";
                row6["NAME"] = "����";
                dt.Rows.Add(row6);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion

        private void GetUI()
        {

        }

        private void GetDataOper()
        {
            m_DataOper = new DataTable();

            #region
            //add by cyq 2012-12-25
            if (!m_DataOper.Columns.Contains("iem_mainpage_diagnosis_no"))
            {
                m_DataOper.Columns.Add("iem_mainpage_diagnosis_no");
            }
            if (!m_DataOper.Columns.Contains("iem_mainpage_no"))
            {
                m_DataOper.Columns.Add("iem_mainpage_no");
            }

            if (!m_DataOper.Columns.Contains("Diagnosis_Code"))
                m_DataOper.Columns.Add("Diagnosis_Code");
            if (!m_DataOper.Columns.Contains("Diagnosis_Name"))
                m_DataOper.Columns.Add("Diagnosis_Name");
            if (!m_DataOper.Columns.Contains("Status_Id"))
                m_DataOper.Columns.Add("Status_Id");
            if (!m_DataOper.Columns.Contains("Status_Name"))
                m_DataOper.Columns.Add("Status_Name");
            //add by cyq 2012-12-25
            if (!m_DataOper.Columns.Contains("Status_Id_Out"))
            {
                m_DataOper.Columns.Add("Status_Id_Out");
            }
            if (!m_DataOper.Columns.Contains("Status_Name_Out"))
            {
                m_DataOper.Columns.Add("Status_Name_Out");
            }

            if (!m_DataOper.Columns.Contains("MenAndInHop"))
                m_DataOper.Columns.Add("MenAndInHop");
            if (!m_DataOper.Columns.Contains("InHopAndOutHop"))
                m_DataOper.Columns.Add("InHopAndOutHop");
            if (!m_DataOper.Columns.Contains("BeforeOpeAndAfterOper"))
                m_DataOper.Columns.Add("BeforeOpeAndAfterOper");
            if (!m_DataOper.Columns.Contains("LinAndBingLi"))
                m_DataOper.Columns.Add("LinAndBingLi");
            if (!m_DataOper.Columns.Contains("InHopThree"))
                m_DataOper.Columns.Add("InHopThree");
            if (!m_DataOper.Columns.Contains("FangAndBingLi"))
                m_DataOper.Columns.Add("FangAndBingLi");

            if (!m_DataOper.Columns.Contains("MenAndInHopName"))
                m_DataOper.Columns.Add("MenAndInHopName");
            if (!m_DataOper.Columns.Contains("InHopAndOutHopName"))
                m_DataOper.Columns.Add("InHopAndOutHopName");
            if (!m_DataOper.Columns.Contains("BeforeOpeAndAfterOperName"))
                m_DataOper.Columns.Add("BeforeOpeAndAfterOperName");
            if (!m_DataOper.Columns.Contains("LinAndBingLiName"))
                m_DataOper.Columns.Add("LinAndBingLiName");
            if (!m_DataOper.Columns.Contains("InHopThreeName"))
                m_DataOper.Columns.Add("InHopThreeName");
            if (!m_DataOper.Columns.Contains("FangAndBingLiName"))
                m_DataOper.Columns.Add("FangAndBingLiName");

            if (!m_DataOper.Columns.Contains("AdmitInfo"))
                m_DataOper.Columns.Add("AdmitInfo");
            if (!m_DataOper.Columns.Contains("AdmitInfoName"))
                m_DataOper.Columns.Add("AdmitInfoName");
            #endregion

            FillUI();
            DataRow row = m_DataOper.NewRow();
            row["iem_mainpage_diagnosis_no"] = m_DiagnosisNo;
            row["iem_mainpage_no"] = m_MainpageNo;

            row["Diagnosis_Code"] = lueOutDiag.CodeValue;
            row["Diagnosis_Name"] = lueOutDiag.DisplayValue;

            //��Ժ����
            row["Status_Id"] = lue_inHosPatiResult.EditValue;
            row["Status_Name"] = lue_inHosPatiResult.Text;

            //�����ģ��ӣ���Ժ���� add by  ywk 
            row["AdmitInfo"] = lue_subInHosPatiResult.EditValue;
            row["AdmitInfoName"] = lue_subInHosPatiResult.Text;

            //��Ժ����
            row["Status_Id_Out"] = lue_outHosPatiResult.EditValue;
            row["Status_Name_Out"] = lue_outHosPatiResult.Text;

            #region ע��
            //�����סԺ
            //if (chkMandZ0.Checked == true)
            //{
            //    row["MenAndInHop"] = "0";
            //    row["MenAndInHopName"] = chkMandZ0.Tag.ToString();
            //}
            //else if (chkMandZ1.Checked)
            //{
            //    row["MenAndInHop"] = "1";
            //    row["MenAndInHopName"] = chkMandZ1.Tag.ToString();
            //}
            //else if (chkMandZ2.Checked)
            //{
            //    row["MenAndInHop"] = "2";
            //    row["MenAndInHopName"] = chkMandZ2.Tag.ToString();
            //}
            //else if (chkMandZ3.Checked)
            //{
            //    row["MenAndInHop"] = "3";
            //    row["MenAndInHopName"] = chkMandZ3.Tag.ToString();
            //}
            ////��Ժ�ͳ�Ժ
            //if (chkRandC0.Checked)
            //{
            //    row["InHopAndOutHop"] = "0";
            //    row["InHopAndOutHopName"] = chkRandC0.Tag.ToString();
            //}
            //else if (chkRandC1.Checked)
            //{
            //    row["InHopAndOutHop"] = "1";
            //    row["InHopAndOutHopName"] = chkRandC1.Tag.ToString();
            //}
            //else if (chkRandC2.Checked)
            //{
            //    row["InHopAndOutHop"] = "2";
            //    row["InHopAndOutHopName"] = chkRandC2.Tag.ToString();
            //}
            //else if (chkRandC3.Checked)
            //{
            //    row["InHopAndOutHop"] = "3";
            //    row["InHopAndOutHopName"] = chkRandC3.Tag.ToString();
            //}

            ////��ǰ������
            //if (chkSqAndSh0.Checked)
            //{
            //    row["BeforeOpeAndAfterOper"] = "0";
            //    row["BeforeOpeAndAfterOperName"] = chkSqAndSh0.Tag.ToString();
            //}
            //else if (chkSqAndSh1.Checked)
            //{
            //    row["BeforeOpeAndAfterOper"] = "1";
            //    row["BeforeOpeAndAfterOperName"] = chkSqAndSh1.Tag.ToString();
            //}
            //else if (chkSqAndSh2.Checked)
            //{
            //    row["BeforeOpeAndAfterOper"] = "2";
            //    row["BeforeOpeAndAfterOperName"] = chkSqAndSh2.Tag.ToString();
            //}
            //else if (chkSqAndSh3.Checked)
            //{
            //    row["BeforeOpeAndAfterOper"] = "3";
            //    row["BeforeOpeAndAfterOperName"] = chkSqAndSh3.Tag.ToString();
            //}
            ////�ٴ��Ͳ���
            //if (chkLandB0.Checked)
            //{
            //    row["LinAndBingLi"] = "0";
            //    row["LinAndBingLiName"] = chkLandB0.Tag.ToString();
            //}
            //else if (chkLandB1.Checked)
            //{
            //    row["LinAndBingLi"] = "1";
            //    row["LinAndBingLiName"] = chkLandB1.Tag.ToString();
            //}
            //else if (chkLandB2.Checked)
            //{
            //    row["LinAndBingLi"] = "2";
            //    row["LinAndBingLiName"] = chkLandB2.Tag.ToString();
            //}
            //else if (chkLandB3.Checked)
            //{
            //    row["LinAndBingLi"] = "3";
            //    row["LinAndBingLiName"] = chkLandB3.Tag.ToString();
            //}
            ////��Ժ������
            //if (chkRThree0.Checked)
            //{
            //    row["InHopThree"] = "0";
            //    row["InHopThreeName"] = chkRThree0.Tag.ToString();
            //}
            //else if (chkRThree1.Checked)
            //{
            //    row["InHopThree"] = "1";
            //    row["InHopThreeName"] = chkRThree1.Tag.ToString();
            //}
            //else if (chkRThree2.Checked)
            //{
            //    row["InHopThree"] = "2";
            //    row["InHopThreeName"] = chkRThree2.Tag.ToString();
            //}
            //else if (chkRThree3.Checked)
            //{
            //    row["InHopThree"] = "3";
            //    row["InHopThreeName"] = chkRThree3.Tag.ToString();
            //}
            ////����Ͳ���
            //if (chkFandB0.Checked)
            //{
            //    row["FangAndBingLi"] = "3";
            //    row["FangAndBingLiName"] = chkFandB0.Tag.ToString();
            //}
            //else if (chkFandB1.Checked)
            //{
            //    row["FangAndBingLi"] = "1";
            //    row["FangAndBingLiName"] = chkFandB1.Tag.ToString();
            //}
            //else if (chkFandB2.Checked)
            //{
            //    row["FangAndBingLi"] = "2";
            //    row["FangAndBingLiName"] = chkFandB2.Tag.ToString();
            //}
            //else if (chkFandB3.Checked)
            //{
            //    row["FangAndBingLi"] = "3";
            //    row["FangAndBingLiName"] = chkFandB3.Tag.ToString();
            //}
            #endregion

            m_DataOper.Rows.Add(row);
            //m_DataOper.AcceptChanges();

        }

        private void FillUI()
        {
            //if (m_IemDiagInfo == null || String.IsNullOrEmpty(m_IemDiagInfo.Diagnosis_Code))
            //    return;
            //lueOutDiag.CodeValue = m_IemDiagInfo.Diagnosis_Code;

            //if (m_IemDiagInfo.Status_Id == 1)
            //    chkStatus1.Checked = true;
            //if (m_IemDiagInfo.Status_Id == 2)
            //    chkStatus2.Checked = true;
            //if (m_IemDiagInfo.Status_Id == 3)
            //    chkStatus3.Checked = true;
            //if (m_IemDiagInfo.Status_Id == 4)
            //    chkStatus4.Checked = true;
            //if (m_IemDiagInfo.Status_Id == 5)
            //    chkStatus5.Checked = true;

        }

        /// <summary>
        /// ���ذ�ť
        /// </summary>
        private void HideSbutton()
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl.GetType() == typeof(LookUpEditor))
                    ((LookUpEditor)ctl).ShowSButton = false;
                else
                {
                    foreach (Control ct in ctl.Controls)
                    {
                        if (ct.GetType() == typeof(LookUpEditor))
                            ((LookUpEditor)ct).ShowSButton = false;
                    }
                }
            }
        }
        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.lueOutDiag.CodeValue))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                m_App.CustomMessageBox.MessageShow("��ѡ���Ժ���");
            }

        }
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }

        /// <summary>
        /// �˲���������ҽԺ����
        /// add by ywk  2012��7��26��14:56:07
        /// ��Ժ����У���Ժ���飬���ѡ���У�����ʾ��������
        /// edit by cyq 2012-12-25
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lue_inHosPatiResult_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lue_inHosPatiResult.EditValue.ToString() == "1")
                {
                    labelControl1.Visible = true;
                    lue_subInHosPatiResult.Visible = true;
                }
                else
                {
                    labelControl1.Visible = false;
                    lue_subInHosPatiResult.Visible = false;
                    lue_subInHosPatiResult.EditValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

    }
}
