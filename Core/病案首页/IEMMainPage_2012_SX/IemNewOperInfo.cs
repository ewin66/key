using DrectSoft.Common.Ctrs.FORM;
using DrectSoft.Common.Library;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DrectSoft.Core.IEMMainPage                         //wangji   edit   2013 1 12
{
    public partial class IemNewOperInfo : DevBaseForm
    {

        private IEmrHost m_App;

        private Iem_MainPage_Operation m_IemOperInfo = new Iem_MainPage_Operation();
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Iem_MainPage_Operation IemOperInfo
        {
            get
            {
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

        //private const string SqlAllOper = @"select operation_code,operation_name from iem_mainpage_operation_sx where valid='1'";
        //private const string PY

        /// <summary>
        /// �õ��������������ݣ�ȫ���������䣬��enter������ҳ�棬������ҳ��
        /// </summary>
        //public void GetOperData()
        //{

        //}

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <param name="app">Ӧ�ó������ӿ�</param>
        /// <param name="operateType">�������ͣ�"edit","new"��</param>
        /// <param name="dtOper">"edit"��ʼ����</param>
        public IemNewOperInfo(IEmrHost app, string operateType, DataTable dtOper)
        {
            try
            {
                InitializeComponent();
                m_App = app;
                InitLookUpEditor();
                if (operateType == "edit")
                {
                    this.m_DataOper = dtOper;
                    this.FreshDataByDataOper();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GetFocus()
        {
            this.deOperDate.Focus();
        }


        private DataTable dtOper = new DataTable();
        private DataTable dtDoc = new DataTable();
        private DataTable dtDiag = new DataTable();
        public void GetFormLoadData()
        {
            try
            {
                string SqlAllOper = @"select py, wb, name, ID icd from operation where valid='1'";
                dtOper = m_App.SqlHelper.ExecuteDataTable(SqlAllOper, CommandType.Text);
                string SqlAllDoctor = @"SELECT py,wb, NAME,ID icd FROM users WHERE valid = 1 ORDER BY icd";
                dtDoc = m_App.SqlHelper.ExecuteDataTable(SqlAllDoctor, CommandType.Text);
                string SqlAllDiag = @"SELECT py,wb, NAME,icd FROM diagnosis WHERE valid = 1 union all SELECT py,wb, NAME,ID icd FROM diagnosisofchinese WHERE valid = 1 ORDER BY icd";
                dtDiag = m_App.SqlHelper.ExecuteDataTable(SqlAllDiag, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void IemNewOperInfo_Load(object sender, EventArgs e)
        {
            GetFormLoadData();
#if DEBUG
#else
            HideSbutton();
#endif
        }

        /// <summary>
        /// �ڴ�ҳ��ʱ����ҳ�����ݵ�ƥ�䣬�õ�ƥ������ݽ��
        /// </summary>
        private void getOPERResult()
        {
            try
            {
                //����
                if (!string.IsNullOrEmpty(lueOperCode.Text.Trim()) == true)
                {
                    string filter = string.Empty;

                    string NameFilter = " NAME= '{0}'";
                    filter += string.Format(NameFilter, lueOperCode.Text.Trim());
                    dtOper.DefaultView.RowFilter = filter;

                    int dataResult = dtOper.DefaultView.ToTable().Rows.Count;

                    if (dataResult > 0)
                    {
                        lueOperCode.DiaValue = lueOperCode.Text.Trim();
                        lueOperCode.DiaCode = dtOper.DefaultView.ToTable().Rows[0]["icd"].ToString();

                    }
                    if (dataResult == 0)
                    {
                        lueOperCode.DiaValue = lueOperCode.Text.Trim();
                        lueOperCode.DiaCode = "";
                    }

                }
                if (string.IsNullOrEmpty(lueOperCode.Text.Trim()) == true)
                {
                    string filter = string.Empty;
                    string NameFilter = "NAME='{0}'";
                    filter += string.Format(NameFilter, lueOperCode.Text.Trim());
                    dtOper.DefaultView.RowFilter = filter;

                    int dataResult = dtOper.DefaultView.ToTable().Rows.Count;
                    if (dataResult > 0)
                    {
                        lueOperCode.DiaValue = lueOperCode.Text.Trim();
                        lueOperCode.DiaCode = dtOper.DefaultView.ToTable().Rows[0]["icd"].ToString();
                    }
                    if (dataResult == 0)
                    {
                        lueOperCode.DiaValue = lueOperCode.Text.Trim();
                        lueOperCode.DiaCode = "";
                    }
                }
                //��������֢
                if (!string.IsNullOrEmpty(lueCompCode.Text.Trim()) == true)
                {
                    string filter = string.Empty;

                    string NameFilter = " NAME= '{0}'";
                    filter += string.Format(NameFilter, lueCompCode.Text.Trim());
                    dtDiag.DefaultView.RowFilter = filter;

                    int dataResult = dtDiag.DefaultView.ToTable().Rows.Count;

                    if (dataResult > 0)
                    {
                        lueCompCode.DiaValue = lueCompCode.Text.Trim();
                        lueCompCode.DiaCode = dtDiag.DefaultView.ToTable().Rows[0]["icd"].ToString();

                    }
                    if (dataResult == 0)
                    {
                        lueCompCode.DiaValue = lueCompCode.Text.Trim();
                        lueCompCode.DiaCode = "";
                    }

                }
                if (string.IsNullOrEmpty(lueCompCode.Text.Trim()) == true)
                {
                    string filter = string.Empty;
                    string NameFilter = "NAME='{0}'";
                    filter += string.Format(NameFilter, lueCompCode.Text.Trim());
                    dtDiag.DefaultView.RowFilter = filter;

                    int dataResult = dtDiag.DefaultView.ToTable().Rows.Count;
                    if (dataResult > 0)
                    {
                        lueCompCode.DiaValue = lueCompCode.Text.Trim();
                        lueCompCode.DiaCode = dtDiag.DefaultView.ToTable().Rows[0]["icd"].ToString();
                    }
                    if (dataResult == 0)
                    {
                        lueCompCode.DiaValue = lueCompCode.Text.Trim();
                        lueCompCode.DiaCode = "";
                    }
                }
                //����ҽʦ
                if (!string.IsNullOrEmpty(lueAnaesthesiaUser.Text.Trim()) == true)
                {
                    string filter = string.Empty;
                    string NameFilter = "NAME='{0}'";
                    filter += string.Format(NameFilter, lueAnaesthesiaUser.Text.Trim());
                    dtDoc.DefaultView.RowFilter = filter;

                    int dataResult = dtDoc.DefaultView.ToTable().Rows.Count;
                    if (dataResult > 0)
                    {
                        lueAnaesthesiaUser.DiaValue = lueAnaesthesiaUser.Text.Trim();
                        lueAnaesthesiaUser.DiaCode = dtDoc.DefaultView.ToTable().Rows[0]["icd"].ToString();
                    }
                    if (dataResult == 0)
                    {
                        lueAnaesthesiaUser.DiaValue = lueAnaesthesiaUser.Text.Trim();
                        lueAnaesthesiaUser.DiaCode = "";
                    }
                }
                if (string.IsNullOrEmpty(lueAnaesthesiaUser.Text.Trim()) == true)
                {
                    string filter = string.Empty;
                    string NameFilter = "NAME='{0}'";
                    filter += string.Format(NameFilter, lueAnaesthesiaUser.Text.Trim());
                    dtDoc.DefaultView.RowFilter = filter;

                    int dataResult = dtDoc.DefaultView.ToTable().Rows.Count;
                    if (dataResult > 0)
                    {
                        lueAnaesthesiaUser.DiaValue = lueAnaesthesiaUser.Text.Trim();
                        lueAnaesthesiaUser.DiaCode = dtDoc.DefaultView.ToTable().Rows[0]["icd"].ToString();
                    }
                    if (dataResult == 0)
                    {
                        lueAnaesthesiaUser.DiaValue = lueAnaesthesiaUser.Text.Trim();
                        lueAnaesthesiaUser.DiaCode = "";
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                getOPERResult();
                if (String.IsNullOrEmpty(this.lueOperCode.Text))
                {
                    m_App.CustomMessageBox.MessageShow("��ѡ��������Ϣ");
                    return;

                }
                //����
                if (!String.IsNullOrEmpty(this.lueOperCode.DiaCode))
                {
                    lueOperCode.Text = "";
                }
                else
                {
                    this.lueOperCode.DiaValue = lueOperCode.Text.Trim();

                    this.lueOperCode.DiaCode = "�Ǳ�������";
                }

                //��������֢
                if (!String.IsNullOrEmpty(this.lueCompCode.DiaCode))
                {
                    lueCompCode.Text = "";
                }
                else
                {
                    this.lueCompCode.DiaValue = lueCompCode.Text.Trim();
                    this.lueCompCode.DiaCode = "�Ǳ������";
                }
                //����ҽʦ
                if (!String.IsNullOrEmpty(this.lueAnaesthesiaUser.DiaCode))
                {
                    lueAnaesthesiaUser.Text = "";
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.lueAnaesthesiaUser.DiaValue = lueAnaesthesiaUser.Text.Trim();
                    this.lueAnaesthesiaUser.DiaCode = lueAnaesthesiaUser.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private DataTable m_DataTableDiag;

        /// <summary>
        /// ��ʼ���ؼ�  
        /// </summary>
        private void InitLookUpEditor()
        {
            try
            {
                BindLueOperData(lueExecute1, 11);
                BindLueOperData(lueExecute2, 11);
                BindLueOperData(lueExecute3, 11);
                BindLueData(lueCloseLevel, 15);
                BindLueData(lueAnaesthesiaType, 14);
                BindLueData(lueOperlevel, 18);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// �����ڵ�ǰ����������ʾ��View   edit by wangj  2013 1 12
        /// </summary>
        private void FreshDataByDataOper()
        {
            try
            {
                if (this.m_DataOper.Rows.Count == 0)
                {
                    return;
                }
                DataRow row = m_DataOper.Rows[0];
                //����
                lueOperCode.DiaCode = row["Operation_Code"].ToString();
                lueOperCode.DiaValue = row["Operation_Name"].ToString();
                lueOperCode.Text = lueOperCode.DiaValue;



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
                //����ҽʦ
                lueAnaesthesiaUser.DiaCode = row["Anaesthesia_User"].ToString();
                lueAnaesthesiaUser.DiaValue = row["Anaesthesia_User_Name"].ToString();
                lueAnaesthesiaUser.Text = lueAnaesthesiaUser.DiaValue;
                //��������֢
                lueCompCode.DiaCode = row["Complication_Code"].ToString();
                lueCompCode.DiaValue = row["Complication_Name"].ToString();
                lueCompCode.Text = lueCompCode.DiaValue;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GetDataOper()
        {
            try
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

                if (!m_DataOper.Columns.Contains("Complication_Code"))
                    m_DataOper.Columns.Add("Complication_Code");
                if (!m_DataOper.Columns.Contains("Complication_Name"))
                    m_DataOper.Columns.Add("Complication_Name");
                if (!m_DataOper.Columns.Contains("MAINOPERATION"))
                    m_DataOper.Columns.Add("MAINOPERATION");
                #endregion
                DataRow row = m_DataOper.NewRow();
                row["Operation_Code"] = lueOperCode.DiaCode;//1.CodeValue;
                row["Operation_Name"] = lueOperCode.DiaValue;//1.DisplayValue;
                row["Complication_Code"] = lueCompCode.DiaCode;
                row["Complication_Name"] = lueCompCode.DiaValue;

                if (deOperDate.DateTime.CompareTo(DateTime.MinValue) != 0)
                    row["Operation_Date"] = deOperDate.DateTime.ToShortDateString() + " " + teOperDate.Time.ToShortTimeString();

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
                row["Anaesthesia_User"] = lueAnaesthesiaUser.DiaCode;//.CodeValue;
                row["Anaesthesia_User_Name"] = lueAnaesthesiaUser.DiaValue;//.DisplayValue;
                row["MAINOPERATION"] = "";
                m_DataOper.Rows.Add(row);
                //m_DataOper.AcceptChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }


        #region ��LUE
        private void BindLueData(LookUpEditor lueInfo, Decimal queryType)
        {
            try
            {
                LookUpWindow lupInfo = new LookUpWindow();
                lupInfo.SqlHelper = m_App.SqlHelper;
                DataTable dataTable = GetEditroData(queryType);

                Dictionary<string, int> columnwidth = new Dictionary<String, Int32>();
                columnwidth.Add("����", lueInfo.Width);
                SqlWordbook sqlWordBook = new SqlWordbook("ID", dataTable, "ID", "Name", columnwidth, true);

                lueInfo.SqlWordbook = sqlWordBook;
                lueInfo.ListWindow = lupInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void BindLueOperData(LookUpEditor lueInfo, Decimal queryType)
        {
            try
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ��lue������Դ������ ���� ��λ
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private DataTable AddTableColumn(DataTable dataTable)
        {
            try
            {
                DataTable dataTableAdd = dataTable;
                if (!dataTableAdd.Columns.Contains("����"))
                    dataTableAdd.Columns.Add("����");
                foreach (DataRow row in dataTableAdd.Rows)
                    row["����"] = row["Name"].ToString();
                return dataTableAdd;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion



        private void HideSbutton()
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        private string MZDiagType = string.Empty;//�������
        private string GoType = string.Empty;//����������Type
        private string inputText = string.Empty;//��ȡ�ı����������

        private void lueOperCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == 13)//lueOperCode.Text.Trim() != null &&
                {
                    GoType = "operate";
                    MZDiagType = "operate";
                    inputText = lueOperCode.Text.Trim();

                    IemNewDiagInfo diagInfo = new IemNewDiagInfo(m_App, dtOper, GoType, MZDiagType, inputText);
                    if (diagInfo.GetFormResult())
                    {
                        diagInfo.ShowDialog();
                        if (diagInfo.IsClosed)
                        {
                            lueOperCode.Text = diagInfo.inText;
                            lueOperCode.DiaCode = diagInfo.inCode;
                            lueOperCode.DiaValue = diagInfo.inText;
                        }
                    }
                    else
                    {
                        lueOperCode.DiaCode = diagInfo.inCode;
                        lueOperCode.DiaValue = diagInfo.inText;
                        lueOperCode.Multiline = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }
        private void lueCompCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == 13)
                {
                    GoType = "operate";
                    MZDiagType = "diag";
                    inputText = lueCompCode.Text.Trim();

                    IemNewDiagInfo diagInfo = new IemNewDiagInfo(m_App, dtDiag, GoType, MZDiagType, inputText);
                    if (diagInfo.GetFormResult())
                    {
                        diagInfo.ShowDialog();
                        if (diagInfo.IsClosed)
                        {
                            lueCompCode.Text = diagInfo.inText;
                            lueCompCode.DiaCode = diagInfo.inCode;
                            lueCompCode.DiaValue = diagInfo.inText;
                        }
                    }
                    else
                    {
                        lueCompCode.DiaCode = diagInfo.inCode;
                        lueCompCode.DiaValue = diagInfo.inText;
                        lueCompCode.Multiline = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }
        private void lueAnaesthesiaUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    GoType = "operate";
                    MZDiagType = "anaesthetist";
                    inputText = lueAnaesthesiaUser.Text.Trim();

                    IemNewDiagInfo diagInfo = new IemNewDiagInfo(m_App, dtDoc, GoType, MZDiagType, inputText);
                    if (diagInfo.GetFormResult())
                    {
                        diagInfo.ShowDialog();
                        if (diagInfo.IsClosed)
                        {
                            lueAnaesthesiaUser.Text = diagInfo.inText;
                            lueAnaesthesiaUser.DiaCode = diagInfo.inCode;
                            lueAnaesthesiaUser.DiaValue = diagInfo.inText;
                        }
                    }
                    else
                    {
                        lueAnaesthesiaUser.DiaCode = diagInfo.inCode;
                        lueAnaesthesiaUser.DiaValue = diagInfo.inText;
                        lueAnaesthesiaUser.Multiline = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }


    }
}