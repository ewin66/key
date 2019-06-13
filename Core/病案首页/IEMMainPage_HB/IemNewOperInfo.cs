using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;
using DrectSoft.Common.Ctrs.FORM;
using DrectSoft.Service;
using DrectSoft.Common.Library;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;
#pragma warning disable 0618
namespace DrectSoft.Core.IEMMainPage
{
    public partial class IemNewOperInfo : DevBaseForm
    {

        private IEmrHost m_App;
        //һЩ�ؼ��Ƿ���ʾ
        private bool controlEnableFlag;
        private Iem_MainPage_Operation m_IemOperInfo = new Iem_MainPage_Operation();
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Iem_MainPage_Operation IemOperInfo
        {
            get
            {
                GetUI();
                return m_IemOperInfo;
            }
            set
            {
                m_IemOperInfo = value;
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
            set
            {
                m_DataOper = new DataTable();
                m_DataOper = value.Clone();
                foreach (DataRow Row in value.Rows)
                {
                    m_DataOper.ImportRow(Row);
                }
            }
        }
        private DataTable m_DataOper;

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <param name="app">Ӧ�ó������ӿ�</param>
        /// <param name="operateType">�������ͣ�"edit","new"��</param>
        /// <param name="dtOper">"edit"��ʼ����</param>
        public IemNewOperInfo(IEmrHost app, string operateType, DataTable dtOper)
        {
            InitializeComponent();
            m_App = app;
            InitLookUpEditor();

            InitUIShowOrHide();

            if (operateType == "edit")
            {
                this.m_DataOper = dtOper;
                this.FreshDataByDataOper();
            }
        }

        private void IemNewOperInfo_Load(object sender, EventArgs e)
        {
#if DEBUG
#else
                        HideSbutton();
#endif
        }

        /// <summary>
        /// �ؼ��Ƿ���ʾ
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-17</date>
        /// </summary>
        private void InitUIShowOrHide()
        {
            IemMainPageManger IemM = new IemMainPageManger(m_App, m_App.CurrentPatientInfo);
            string cansee = IemM.GetConfigValueByKey("EmrInputConfig");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(cansee);
            controlEnableFlag = doc.GetElementsByTagName("IemPageContorlVisable")[0].InnerText == "1" ? true : false;
            if (controlEnableFlag)//�ɼ�
            {
                labelControl8.Visible = true;
                labelControl9.Visible = true;
                labelControl10.Visible = true;
                labelControl12.Visible = true;
                labelControl13.Visible = true;
                lueISChooseDate.Visible = true;
                lueIsClearOpe.Visible = true;
                lueISGanran.Visible = true;
                lueAnesthesiaLevel.Visible = true;
                lueComplications.Visible = true;
                this.Height = 275;

                BindAnesthesiaLevel();
                BindComplications();
            }
            else
            {
                labelControl8.Visible = false;
                labelControl9.Visible = false;
                labelControl10.Visible = false;
                labelControl12.Visible = false;
                labelControl13.Visible = false;
                lueISChooseDate.Visible = false;
                lueIsClearOpe.Visible = false;
                lueISGanran.Visible = false;
                lueAnesthesiaLevel.Visible = false;
                lueComplications.Visible = false;
                this.Height = 230;
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
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.lueOperCode.CodeValue))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.lueOperCode.Focus();
                m_App.CustomMessageBox.MessageShow("��ѡ����������");
            }
        }

        private DataTable m_DataTableDiag;
        private void InitLookUpEditor()
        {
            BindLueData(lueOperCode, 20);
            BindLueOperData(lueExecute1, 11);
            BindLueOperData(lueExecute2, 11);
            BindLueOperData(lueExecute3, 11);
            BindLueOperData(lueAnaesthesiaUser, 11);
            BindLueData(lueCloseLevel, 15);
            BindLueData(lueAnaesthesiaType, 14);
            BindLueData(lueOperlevel, 18);
            //�������Ƿ������������Ƿ��޾��������Ƿ��Ⱦ
            BindLueData(lueISChooseDate, 55);
            BindLueData(lueIsClearOpe, 55);
            BindLueData(lueISGanran, 55);
        }

        private void GetUI()
        {

            //m_IemOperInfo.Operation_Code = lueOperCode.CodeValue;
            //if (deOperDate.DateTime.CompareTo(DateTime.MinValue) != 0)
            //    m_IemOperInfo.Operation_Date = deOperDate.DateTime.ToShortDateString() + "" + teOperDate.Time.ToShortTimeString();
            //m_IemOperInfo.Operation_Name = lueOperCode.DisplayValue;
            //m_IemOperInfo.Execute_User1 = lueExecute1.CodeValue;
            //m_IemOperInfo.Execute_User2 = lueExecute2.CodeValue;
            //m_IemOperInfo.Execute_User3 = lueExecute3.CodeValue;
            //m_IemOperInfo.Anaesthesia_Type_Id = Convertmy.ToDecimal(lueAnaesthesiaType.CodeValue);
            //m_IemOperInfo.Close_Level = Convertmy.ToDecimal(lueCloseLevel.CodeValue);
            //m_IemOperInfo.Anaesthesia_User = lueAnaesthesiaUser.CodeValue;
        }

        /// <summary>
        /// �����ڵ�ǰ����������ʾ��View
        /// </summary>
        private void FreshDataByDataOper()
        {
            if (this.m_DataOper.Rows.Count == 0)
            {
                return;
            }
            DataRow row = m_DataOper.Rows[0];
            lueOperCode.CodeValue = row["Operation_Code"].ToString();
            if (row["Operation_Date"].ToString() != "")
            {
                deOperDate.DateTime = DateTime.Parse(DateTime.Parse(row["Operation_Date"].ToString()).ToShortDateString());
                teOperDate.Time = DateTime.Parse(DateTime.Parse(row["Operation_Date"].ToString()).ToShortTimeString());
            }
            lueOperlevel.CodeValue = row["operation_level"].ToString();
            lueExecute1.CodeValue = row["Execute_User1"].ToString();
            lueExecute2.CodeValue = row["Execute_User2"].ToString();
            lueExecute3.CodeValue = row["Execute_User3"].ToString();
            lueAnaesthesiaType.CodeValue = row["Anaesthesia_Type_Id"].ToString();
            lueCloseLevel.CodeValue = row["Close_Level"].ToString();
            lueAnaesthesiaUser.CodeValue = row["Anaesthesia_User"].ToString();
            lueISChooseDate.CodeValue = row["IsChooseDate"].ToString();
            lueIsClearOpe.CodeValue = row["IsClearOpe"].ToString();
            lueISGanran.CodeValue = row["IsGanRan"].ToString();
            if (controlEnableFlag)
            {
                lueAnesthesiaLevel.CodeValue = row["anesthesia_level"].ToString();
                lueComplications.CodeValue = row["opercomplication_code"].ToString();
            }
        }
        private void GetDataOper()
        {
            m_DataOper = new DataTable();
            #region
            if (!m_DataOper.Columns.Contains("Operation_Code"))
                m_DataOper.Columns.Add("Operation_Code");
            if (!m_DataOper.Columns.Contains("Operation_Date"))
                m_DataOper.Columns.Add("Operation_Date");
            if (!m_DataOper.Columns.Contains("Operation_Name"))
                m_DataOper.Columns.Add("Operation_Name");

            if (!m_DataOper.Columns.Contains("operation_level"))
                m_DataOper.Columns.Add("operation_level");
            if (!m_DataOper.Columns.Contains("operation_level_Name"))
                m_DataOper.Columns.Add("operation_level_Name");

            if (!m_DataOper.Columns.Contains("Execute_User1"))
                m_DataOper.Columns.Add("Execute_User1");
            if (!m_DataOper.Columns.Contains("Execute_User1_Name"))
                m_DataOper.Columns.Add("Execute_User1_Name");

            if (!m_DataOper.Columns.Contains("Execute_User2"))
                m_DataOper.Columns.Add("Execute_User2");
            if (!m_DataOper.Columns.Contains("Execute_User2_Name"))
                m_DataOper.Columns.Add("Execute_User2_Name");

            if (!m_DataOper.Columns.Contains("Execute_User3"))
                m_DataOper.Columns.Add("Execute_User3");
            if (!m_DataOper.Columns.Contains("Execute_User3_Name"))
                m_DataOper.Columns.Add("Execute_User3_Name");

            if (!m_DataOper.Columns.Contains("Anaesthesia_Type_Id"))
                m_DataOper.Columns.Add("Anaesthesia_Type_Id");
            if (!m_DataOper.Columns.Contains("Anaesthesia_Type_Name"))
                m_DataOper.Columns.Add("Anaesthesia_Type_Name");

            if (!m_DataOper.Columns.Contains("Close_Level"))
                m_DataOper.Columns.Add("Close_Level");
            if (!m_DataOper.Columns.Contains("Close_Level_Name"))
                m_DataOper.Columns.Add("Close_Level_Name");

            if (!m_DataOper.Columns.Contains("Anaesthesia_User"))
                m_DataOper.Columns.Add("Anaesthesia_User");
            if (!m_DataOper.Columns.Contains("Anaesthesia_User_Name"))
                m_DataOper.Columns.Add("Anaesthesia_User_Name");
            //�����Ƿ����������Ƿ��޾������Ƿ��Ⱦ
            if (!m_DataOper.Columns.Contains("IsChooseDateName"))
                m_DataOper.Columns.Add("IsChooseDateName");
            if (!m_DataOper.Columns.Contains("IsClearOpeName"))
                m_DataOper.Columns.Add("IsClearOpeName");
            if (!m_DataOper.Columns.Contains("IsGanRanName"))
                m_DataOper.Columns.Add("IsGanRanName");
            if (!m_DataOper.Columns.Contains("IsChooseDate"))
                m_DataOper.Columns.Add("IsChooseDate");
            if (!m_DataOper.Columns.Contains("IsClearOpe"))
                m_DataOper.Columns.Add("IsClearOpe");
            if (!m_DataOper.Columns.Contains("IsGanRan"))
                m_DataOper.Columns.Add("IsGanRan");
            if (!m_DataOper.Columns.Contains("Anesthesia_Level"))
                m_DataOper.Columns.Add("Anesthesia_Level");
            if (!m_DataOper.Columns.Contains("OperComplication_Code"))
                m_DataOper.Columns.Add("OperComplication_Code");

            #endregion
            FillUI();
            DataRow row = m_DataOper.NewRow();
            row["Operation_Code"] = lueOperCode.CodeValue;
            row["Operation_Name"] = lueOperCode.DisplayValue;
            if (deOperDate.DateTime.CompareTo(DateTime.MinValue) != 0)
            {
                row["Operation_Date"] = deOperDate.DateTime.ToShortDateString() + " " + teOperDate.Time.ToShortTimeString();
            }
            row["operation_level"] = lueOperlevel.CodeValue;
            row["operation_level_Name"] = lueOperlevel.DisplayValue;

            row["Execute_User1"] = lueExecute1.CodeValue;
            row["Execute_User1_Name"] = lueExecute1.DisplayValue;
            row["Execute_User2"] = lueExecute2.CodeValue;
            row["Execute_User2_Name"] = lueExecute2.DisplayValue;
            row["Execute_User3"] = lueExecute3.CodeValue;
            row["Execute_User3_Name"] = lueExecute3.DisplayValue;
            row["Anaesthesia_Type_Id"] = lueAnaesthesiaType.CodeValue;
            row["Anaesthesia_Type_Name"] = lueAnaesthesiaType.DisplayValue;
            row["Close_Level"] = lueCloseLevel.CodeValue;
            row["Close_Level_Name"] = lueCloseLevel.DisplayValue;
            row["Anaesthesia_User"] = lueAnaesthesiaUser.CodeValue;
            row["Anaesthesia_User_Name"] = lueAnaesthesiaUser.DisplayValue;

            //���������������Ƿ��޾������Ƿ��Ⱦ
            row["IsChooseDateName"] = GetIsOrNo(lueISChooseDate.CodeValue);
            row["IsClearOpeName"] = GetIsOrNo(lueIsClearOpe.CodeValue);
            row["IsGanRanName"] = GetIsOrNo(lueISGanran.CodeValue);

            row["IsChooseDate"] = lueISChooseDate.CodeValue;
            row["IsClearOpe"] = lueIsClearOpe.CodeValue;
            row["IsGanRan"] = lueISGanran.CodeValue;

            //����ּ�����������֢ add by cyq 2012-10-17
            row["Anesthesia_Level"] = lueAnesthesiaLevel.CodeValue;
            row["OperComplication_Code"] = lueComplications.CodeValue;
            m_DataOper.Rows.Add(row);
            //m_DataOper.AcceptChanges();

        }

        private string GetIsOrNo(string code)
        {
            string isorno = string.Empty;
            if (code == "1")
            {
                isorno = "��";
            }
            if (code == "2")
            {
                isorno = "��";
            }
            if (code == "0")
            {
                isorno = "δ֪";
            }
            return isorno;
        }

        private void FillUI()
        {
            //if (m_IemOperInfo == null || String.IsNullOrEmpty(m_IemOperInfo.Operation_Code))
            //    return;
            //lueOperCode.CodeValue = m_IemOperInfo.Operation_Code;
            //if (!String.IsNullOrEmpty(m_IemOperInfo.Operation_Date))
            //{
            //    deOperDate.DateTime = Convert.ToDateTime(m_IemOperInfo.Operation_Date);
            //    teOperDate.Time = Convert.ToDateTime(m_IemOperInfo.Operation_Date);
            //}
            ////lueOperCode.DisplayValue = m_IemOperInfo.Operation_Name;
            //lueExecute1.CodeValue = m_IemOperInfo.Execute_User1;
            //lueExecute2.CodeValue = m_IemOperInfo.Execute_User2;
            //lueExecute3.CodeValue = m_IemOperInfo.Execute_User3;
            //if (m_IemOperInfo.Anaesthesia_Type_Id != null)
            //    lueAnaesthesiaType.CodeValue = m_IemOperInfo.Anaesthesia_Type_Id.ToString();
            //if (m_IemOperInfo.Close_Level != null)
            //    lueCloseLevel.CodeValue = m_IemOperInfo.Close_Level.ToString();
            //lueAnaesthesiaUser.CodeValue = m_IemOperInfo.Anaesthesia_User;
        }

        #region ��LUE
        /// <summary>
        /// ���θ��ģ�����ҽԺ����Ҫȡ��HIS��ͼ
        /// edit by ywk 2013��7��22�� 11:42:33
        /// </summary>
        /// <param name="lueInfo"></param>
        /// <param name="queryType"></param>
        private void BindLueData(LookUpEditor lueInfo, Decimal queryType)
        {
            LookUpWindow lupInfo = new LookUpWindow();
            lupInfo.SqlHelper = m_App.SqlHelper;
            //�ж�������Ϣ����ȡ����Դ add by ywk 2013��7��22�� 11:43:30
            DataTable dataTable = null;
            //DataTable dataTable = GetEditroData(queryType);
            string getopertype = DS_SqlService.GetConfigValueByKey("GetOperationType") == "" ? "0" : DS_SqlService.GetConfigValueByKey("GetOperationType");
            //ֻ��������his��ȡ �������Ǵ�emr��ȡ
            if (queryType == 20)
            {
                if (getopertype == "0")//EMR
                {
                    dataTable = GetEditroData(queryType);
                }
                if (getopertype == "1")//HIS
                {
                    try
                    {
                        using (OracleConnection conn = new OracleConnection(DataAccessFactory.GetSqlDataAccess("HISDB").GetDbConnection().ConnectionString))
                        {
                            if (conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }
                            dataTable = new DataTable();
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = " SELECT   ID,  NAME, py , WB, name ����  FROM yd_operation ";
                            OracleDataAdapter myoadapt = new OracleDataAdapter(cmd.CommandText, conn);
                            myoadapt.Fill(dataTable);
                            //MessageBox.Show("���ȡ��HIS");
                        }
                    }
                    catch (Exception ex)//���쳣��˵������HIS�Ǳ�û�д���ͼ ��ȡEMR��
                    {
                        dataTable = GetEditroData(queryType);
                        //MessageBox.Show("���쳣�����ȡ��EMR"+ex.Message);
                    }
                }
            }
            else
            {
                dataTable = GetEditroData(queryType);
            }

            Dictionary<string, int> columnwidth = new Dictionary<String, Int32>();
            columnwidth.Add("����", lueInfo.Width);
            SqlWordbook sqlWordBook = new SqlWordbook("ID", dataTable, "ID", "Name", columnwidth, true);

            lueInfo.SqlWordbook = sqlWordBook;
            lueInfo.ListWindow = lupInfo;
        }
        private void BindLueOperData(LookUpEditor lueInfo, Decimal queryType)
        {
            LookUpWindow lupInfo = new LookUpWindow();
            lupInfo.SqlHelper = m_App.SqlHelper;
            if (m_DataTableDiag == null)
                m_DataTableDiag = GetEditroData(queryType);

            Dictionary<string, int> columnwidth = new Dictionary<String, Int32>();
            columnwidth.Add("����", lueInfo.Width);
            SqlWordbook sqlWordBook = new SqlWordbook("ID", m_DataTableDiag, "ID", "Name", columnwidth, true);

            lueInfo.SqlWordbook = sqlWordBook;
            lueInfo.ListWindow = lupInfo;
        }
        /// <summary>
        /// ������ּ�����������
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-17</date>
        /// </summary>
        private void BindAnesthesiaLevel()
        {
            lookUpWindowLevel.SqlHelper = m_App.SqlHelper;
            string sql = "select * from categorydetail where categoryid='90' order by name";
            DataTable dataTable = m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            dataTable.Columns["NAME"].Caption = "����ּ�";
            Dictionary<string, int> columnwidth = new Dictionary<string, int>();
            columnwidth.Add("NAME", 150);
            SqlWordbook sqlWordBook = new SqlWordbook("querybook", dataTable, "NAME", "NAME", columnwidth, true);

            lueAnesthesiaLevel.SqlWordbook = sqlWordBook;
        }
        /// <summary>
        /// ����������֢����������
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-17</date>
        /// </summary>
        private void BindComplications()
        {
            lookUpWindowComp.SqlHelper = m_App.SqlHelper;
            string sql = "select ID,icd_code as Code,Name,PY,WB from Complications where valid != 0  order by Name";
            DataTable dataTable = m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            dataTable.Columns["CODE"].Caption = "ICD����";
            dataTable.Columns["NAME"].Caption = "����";
            Dictionary<string, int> columnwidth = new Dictionary<string, int>();
            columnwidth.Add("CODE", 60);
            columnwidth.Add("NAME", 90);
            SqlWordbook sqlWordBook = new SqlWordbook("ID", dataTable, "Code", "Name", columnwidth, true);

            lueComplications.SqlWordbook = sqlWordBook;
        }
        /// <summary>
        /// ��ȡlue������Դ
        /// </summary>
        /// <param name="queryType"></param>
        /// <returns></returns>
        private DataTable GetEditroData(Decimal queryType)
        {
            SqlParameter paraType = new SqlParameter("@QueryType", SqlDbType.Decimal);
            paraType.Value = queryType;
            SqlParameter[] paramCollection = new SqlParameter[] { paraType };
            DataTable dataTable = AddTableColumn(m_App.SqlHelper.ExecuteDataTable("usp_GetLookUpEditorData", paramCollection, CommandType.StoredProcedure));
            return dataTable;
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
        #endregion

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

    }
}