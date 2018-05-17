using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.Wordbook;
using DrectSoft.FrameWork.WinForm.Plugin;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DrectSoft.Core.Consultation;
using DrectSoft.Core;

namespace DrectSoft.Emr.QcManager
{
    public partial class UCConsultationInfoForMultiply : DevExpress.XtraEditors.XtraUserControl
    {
        private IEmrHost m_App;
        private string m_NoOfFirstPage;
        private string m_ConsultApplySN = string.Empty;
        private bool m_ReadOnly = false;

        public UCConsultationInfoForMultiply()
        {
            InitializeComponent();
        }

        public void Init(string noOfFirstPage, IEmrHost app, bool isNew/*�Ƿ�����������*/, bool readOnly/*�Ƿ�ֻ��*/, string consultApplySN)
        {
            m_App = app;
            m_NoOfFirstPage = noOfFirstPage;
            m_ReadOnly = readOnly;
            m_ConsultApplySN = consultApplySN;

            gridViewDept.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridViewDept.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewDept.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            RegisterEvent();
            InitInner(isNew);
        }

        private void InitInner(bool isNew)
        {
            BindData();
            SetDefaultValue();

            if (!isNew)
            {
                SetData();
                ControlVisible();
            }
            //����ص�Ĭ���Ǳ��������ڵĿ��� add by ywk 
            textEditLocation.Text = m_App.User.CurrentDeptName;
        }
        ///// <summary>
        ///// ���ݲ��˵���ҳ��Ż�ò������ڿ���
        ///// add by ywk 
        ///// </summary>
        ///// <param name="m_NoOfFirstPage"></param>
        ///// <returns></returns>
        //private string GetInpatDept(string m_NoOfFirstPage)
        //{
        //    string sql = string.Format(@"select * fr");
        //}

        private void ControlVisible()
        {
            if (m_ReadOnly)
            {
                DeleteButton.Visible = false;
                simpleButtonNew.Visible = false;
            }
        }


        /// <summary>
        /// ��ȡԭ����д�����ݣ���Ϊ�ؼ���ֵ
        /// </summary>
        private void SetData()
        {
            DataSet ds = DataAccess.GetConsultationDataSet(m_ConsultApplySN, "20");//, Convert.ToString((int)ConsultType.More));
            DataTable dtConsultApply = ds.Tables[0];
            DataTable dtConsultApplyDepartment = ds.Tables[1];

            if (dtConsultApply.Rows.Count > 0)
            {
                //������
                if (dtConsultApply.Rows[0]["UrgencyTypeID"].ToString() == Convert.ToString((int)UrgencyType.Normal))
                {
                    checkEditNormal.Checked = true;
                    checkEditEmergency.Checked = false;
                }
                else
                {
                    checkEditNormal.Checked = false;
                    checkEditEmergency.Checked = true;
                }

                //ժҪ
                memoEditAbstract.Text = dtConsultApply.Rows[0]["Abstract"].ToString();

                //����Ŀ��Ҫ��
                memoEditPurpose.Text = dtConsultApply.Rows[0]["Purpose"].ToString();

                //�����ʱ��
                dateEditConsultationDate.Text = dtConsultApply.Rows[0]["ConsultTime"].ToString().Split(' ')[0];
                timeEditConsultationTime.EditValue = dtConsultApply.Rows[0]["ConsultTime"].ToString().Split(' ')[1];

                //����ص�
                textEditLocation.Text = dtConsultApply.Rows[0]["ConsultLocation"].ToString().Split(' ')[0];

                //����ʱ��
                dateEditApplyDate.Text = dtConsultApply.Rows[0]["ApplyTime"].ToString().Split(' ')[0];
                timeEditApplyTime.Text = dtConsultApply.Rows[0]["ApplyTime"].ToString().Split(' ')[1];

                //����ҽʦ
                lookUpEditorApplyEmployee.CodeValue = dtConsultApply.Rows[0]["ApplyUser"].ToString();

                //������
                lookUpEditorDirector.CodeValue = dtConsultApply.Rows[0]["Director"].ToString();
            }

            if (dtConsultApplyDepartment.Rows.Count > 0)
            {
                gridControlDepartment.DataSource = dtConsultApplyDepartment;

                m_App.PublicMethod.ConvertGridDataSourceUpper(gridViewDept);
            }
        }

        #region ע�ᡢע���¼�
        private void RegisterEvent()
        {
            checkEditNormal.CheckedChanged += new EventHandler(checkEditNormal_CheckedChanged);
            checkEditEmergency.CheckedChanged += new EventHandler(checkEditEmergency_CheckedChanged);
            lookUpEditorHospital.CodeValueChanged += new EventHandler(lookUpEditorHospital_CodeValueChanged);
            simpleButtonNew.Click += new EventHandler(simpleButtonNew_Click);
            gridViewDept.MouseDown += new MouseEventHandler(gridViewDept_MouseDown);
        }

        private void UnRegisterEvent()
        {
            checkEditNormal.CheckedChanged -= new EventHandler(checkEditNormal_CheckedChanged);
            checkEditEmergency.CheckedChanged -= new EventHandler(checkEditEmergency_CheckedChanged);
            lookUpEditorHospital.CodeValueChanged -= new EventHandler(lookUpEditorHospital_CodeValueChanged);
            simpleButtonNew.Click -= new EventHandler(simpleButtonNew_Click);
            gridViewDept.MouseDown -= new MouseEventHandler(gridViewDept_MouseDown);
        }

        void gridViewDept_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hit = gridViewDept.CalcHitInfo(e.X, e.Y);

            if (hit.Column != null)
            {
                if (hit.Column.Name == "DeleteButton")
                {
                    if (m_App.CustomMessageBox.MessageShow(string.Format("�Ƿ�ɾ������ҽ����Ϣ��"), CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        int rowIndex = hit.RowHandle;
                        DataTable dataTableSource = gridControlDepartment.DataSource as DataTable;

                        if (rowIndex >= 0 && rowIndex < dataTableSource.Rows.Count)
                        {
                            dataTableSource.Rows.RemoveAt(rowIndex);
                            dataTableSource.AcceptChanges();
                        }
                    }
                }
            }
        }

        void lookUpEditorHospital_CodeValueChanged(object sender, EventArgs e)
        {
            //������ҽԺ��Ӱ��
            BindDepartment();
        }

        void checkEditEmergency_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditEmergency.Checked == true)
            {
                checkEditNormal.Checked = false;
            }
        }

        void checkEditNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditNormal.Checked == true)
            {
                checkEditEmergency.Checked = false;
            }
        }

        #endregion

        #region ��LookUpEditor����Դ

        private void BindData()
        {
            BindHospitalData();
            BindDepartment();
            BindApplyEmployee();
            BindApplyDirector();
            BindDoctorLevel();
            InitGridControlDataSource();
            BindDoctor();
        }

        #region ��ҽԺ
        /// <summary>
        /// ��ҽԺ
        /// </summary>
        private void BindHospitalData()
        {
            lookUpEditorHospital.Kind = WordbookKind.Sql;
            lookUpEditorHospital.ListWindow = lookUpWindowHospital;
            BindHospitalWordBook(GetConsultationData("1"));
        }

        /// <summary>
        /// ��ҽԺ������Դ
        /// </summary>
        /// <param name="dataTableData"></param>
        private void BindHospitalWordBook(DataTable dataTableData)
        {
            Dictionary<string, int> colWidths = new Dictionary<string, int>();
            colWidths.Add("ID", 70);
            colWidths.Add("NAME", 80);

            for (int i = 0; i < dataTableData.Columns.Count; i++)
            {
                if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "id")
                {
                    dataTableData.Columns[i].Caption = "ҽԺ����";
                }
                else if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "name")
                {
                    dataTableData.Columns[i].Caption = "ҽԺ����";
                }
            }

            //�õ������ֵ䣬ΪLookUpWindow�е�����Դ
            SqlWordbook wordBook = new SqlWordbook("Hospital"/*�ֵ��Ψһ��ʾ*/, dataTableData/*���ݼ�*/,
                "ID"/*ÿ�м�¼��Ψһ��ʾ*/, "NAME"/*��ʾ��LookUpEditer�е�ֵ*/
               , colWidths/*����ÿ�еĿ��*/, "ID//Name"/*����ҽԺ���룬ҽԺ���ƽ��в�ѯ*/);

            lookUpEditorHospital.SqlWordbook = wordBook;
        }

        #endregion

        #region ����������
        /// <summary>
        /// ����������
        /// </summary>
        private void BindDepartment()
        {
            lookUpEditorDepartment.Kind = WordbookKind.Sql;
            lookUpEditorDepartment.ListWindow = lookUpWindowDepartment;
            BindDepartmentWordBook(GetConsultationData("2"));
        }

        private void BindDepartmentWordBook(DataTable dataTableData)
        {
            for (int i = 0; i < dataTableData.Columns.Count; i++)
            {
                if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "id")
                {
                    dataTableData.Columns[i].Caption = "���ұ���";
                }
                else if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "name")
                {
                    dataTableData.Columns[i].Caption = "��������";
                }
            }

            Dictionary<string, int> colWidths = new Dictionary<string, int>();
            colWidths.Add("ID", 70);
            colWidths.Add("NAME", 80);
            SqlWordbook wordBook = new SqlWordbook("Department", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorDepartment.SqlWordbook = wordBook;
        }

        #endregion

        #region ������ҽ��
        /// <summary>
        /// ����������
        /// </summary>
        private void BindApplyEmployee()
        {
            lookUpEditorApplyEmployee.Kind = WordbookKind.Sql;
            lookUpEditorApplyEmployee.ListWindow = lookUpWindowApplyEmployee;
            BindApplyEmployeeWordBook(GetConsultationData("4"));
        }

        private void BindApplyEmployeeWordBook(DataTable dataTableData)
        {
            for (int i = 0; i < dataTableData.Columns.Count; i++)
            {
                if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "id")
                {
                    dataTableData.Columns[i].Caption = "ҽʦ����";
                }
                else if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "name")
                {
                    dataTableData.Columns[i].Caption = "ҽʦ����";
                }
            }

            Dictionary<string, int> colWidths = new Dictionary<string, int>();
            colWidths.Add("ID", 70);
            colWidths.Add("NAME", 80);
            SqlWordbook wordBook = new SqlWordbook("ApplyEmployee", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorApplyEmployee.SqlWordbook = wordBook;
        }

        #endregion

        #region ������ҽ��������
        /// <summary>
        /// ����������
        /// </summary>
        private void BindApplyDirector()
        {
            lookUpEditorDirector.Kind = WordbookKind.Sql;
            lookUpEditorDirector.ListWindow = lookUpWindowDirector;
            BindApplyDirectorWordBook(GetConsultationData("5"));
        }

        private void BindApplyDirectorWordBook(DataTable dataTableData)
        {
            for (int i = 0; i < dataTableData.Columns.Count; i++)
            {
                if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "id")
                {
                    dataTableData.Columns[i].Caption = "ҽʦ����";
                }
                else if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "name")
                {
                    dataTableData.Columns[i].Caption = "ҽʦ����";
                }
            }

            Dictionary<string, int> colWidths = new Dictionary<string, int>();
            colWidths.Add("ID", 70);
            colWidths.Add("NAME", 80);
            SqlWordbook wordBook = new SqlWordbook("Director", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorDirector.SqlWordbook = wordBook;
        }

        #endregion

        #region ��ҽʦ����
        /// <summary>
        /// ����������
        /// </summary>
        private void BindDoctorLevel()
        {
            lookUpEditorLevel.Kind = WordbookKind.Sql;
            lookUpEditorLevel.ListWindow = lookUpWindowLevel;
            BindDoctorLevelWordBook(GetConsultationData("9"));
        }

        private void BindDoctorLevelWordBook(DataTable dataTableData)
        {
            for (int i = 0; i < dataTableData.Columns.Count; i++)
            {
                if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "name")
                {
                    dataTableData.Columns[i].Caption = "ҽʦ����";
                }
            }

            Dictionary<string, int> colWidths = new Dictionary<string, int>();
            //colWidths.Add("ID", 10);
            colWidths.Add("NAME", 150);
            SqlWordbook wordBook = new SqlWordbook("Director", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorLevel.SqlWordbook = wordBook;
        }

        #endregion

        private void BindDoctor()
        {
 
             string sql = string.Format(@"select * from users where 1=2");

            lookUpWindowDoctor.SqlHelper = m_App.SqlHelper;

            DataTable Dept = m_App.SqlHelper.ExecuteDataTable(sql);

            Dept.Columns["ID"].Caption = "ҽ������";
            Dept.Columns["NAME"].Caption = "ҽ������";

            Dictionary<string, int> cols = new Dictionary<string, int>();

            cols.Add("ID", 70);
            cols.Add("NAME", 80);

            SqlWordbook deptWordBook = new SqlWordbook("querybook", Dept, "ID", "NAME", cols, "ID//NAME//py//wb");
            lookUpEditorDoctor.SqlWordbook = deptWordBook;
        }

        /// <summary>
        /// �õ�����Ҫ������
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        private DataTable GetConsultationData(string typeID)
        {
            if (DataAccess.App == null)
            {
                DataAccess.App = m_App;
            }
            DataTable dataTableConsultationData = new DataTable();

            if (typeID == "1")//ҽԺ
            {
                dataTableConsultationData = DataAccess.GetConsultationData("", typeID, "");
            }
            else if (typeID == "2")//��������
            {
                dataTableConsultationData = DataAccess.GetConsultationData("", typeID, lookUpEditorHospital.CodeValue.Trim());
            }
            else if (typeID == "3")//����ҽ��
            {
                dataTableConsultationData = DataAccess.GetConsultationData("", typeID, lookUpEditorDepartment.CodeValue.Trim());
            }
            else if (typeID == "4")//����ҽʦ
            {
                string wardID = m_App.User.CurrentWardId.ToString();
                dataTableConsultationData = DataAccess.GetConsultationData("", typeID, wardID);
            }
            else if (typeID == "5")//����ҽʦ������
            {
                string wardID = m_App.User.CurrentWardId.ToString();
                dataTableConsultationData = DataAccess.GetConsultationData("", typeID, wardID);
            }
            else if (typeID == "6")//CategoryDetail
            {
                dataTableConsultationData = DataAccess.GetConsultationData("", typeID, "20");
            }
            else if (typeID == "9")//ҽʦ����
            {
                dataTableConsultationData = DataAccess.GetConsultationData("", typeID, "20");
            }

            return dataTableConsultationData;
        }

        #endregion

        #region ����Ĭ��ֵ
        /// <summary>
        /// ����Ĭ��ֵ
        /// </summary>
        private void SetDefaultValue()
        {
            SetDefaultHospital();
            SetDefaultDate();
            SetDefaultApplyEmployee();
        }

        /// <summary>
        /// ѡ��Ĭ��ҽԺ
        /// </summary>
        private void SetDefaultHospital()
        {
            if (lookUpEditorHospital.SqlWordbook.BookData.Rows.Count > 0)
            {
                lookUpEditorHospital.CodeValue = lookUpEditorHospital.SqlWordbook.BookData.Rows[0]["ID"].ToString();
            }
        }

        /// <summary>
        /// ����Ĭ��ʱ��
        /// </summary>
        private void SetDefaultDate()
        {
            dateEditApplyDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            timeEditApplyTime.EditValue = System.DateTime.Now.ToString().Split(' ')[1];

            dateEditConsultationDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            timeEditConsultationTime.EditValue = System.DateTime.Now.ToString().Split(' ')[1];
        }

        /// <summary>
        /// ����Ĭ������ҽʦ
        /// </summary>
        private void SetDefaultApplyEmployee()
        {
            lookUpEditorApplyEmployee.CodeValue = m_App.User.Id;
        }
        #endregion

        public void Clear()
        {
            SetDefaultValue();
            checkEditNormal.Checked = false;
            checkEditEmergency.Checked = false;
            memoEditAbstract.Text = "";
            memoEditPurpose.Text = "";
            textEditLocation.Text = "";

            lookUpEditorDepartment.CodeValue = "";
            lookUpEditorLevel.CodeValue = "";
            lookUpEditorDirector.CodeValue = "";

            //��ղ����б�
            DataTable dataTableDepartment = gridControlDepartment.DataSource as DataTable;
            if (dataTableDepartment != null)
            {
                dataTableDepartment.Rows.Clear();
            }
        }


        void simpleButtonNew_Click(object sender, EventArgs e)
        {
            Insert();
        }

        private void Insert()
        {
            if (CheckDataDept())
            {
                InsertData();
            }
        }

        private bool CheckDataDept()
        {
            if (lookUpEditorHospital.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ������ҽԺ!", CustomMessageBoxKind.WarningOk);
                lookUpEditorHospital.Focus();
                return false;
            }
            else if (lookUpEditorDepartment.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ����������!", CustomMessageBoxKind.WarningOk);
                lookUpEditorDepartment.Focus();
                return false; 
            }
            else if (lookUpEditorLevel.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ������ҽʦ����!", CustomMessageBoxKind.WarningOk);
                lookUpEditorLevel.Focus();
                return false; 
            }

            DataTable dt = gridControlDepartment.DataSource as DataTable;
            
            for(int i = 0 ;i<dt.Rows.Count;i++) 
            {
                if (lookUpEditorDoctor.CodeValue=="")
                    continue ;
                if ( dt.Rows[i]["EmployeeID"].Equals(lookUpEditorDoctor.CodeValue))
                {
                    m_App.CustomMessageBox.MessageShow("�ü�¼�Ѿ�����!", CustomMessageBoxKind.WarningOk);
                    gridViewDept.FocusedRowHandle = i;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// �����б�������һ��
        /// </summary>
        private void InsertData()
        {
            DataTable dt = gridControlDepartment.DataSource as DataTable;
            DataRow dr = dt.NewRow();
            dr["HospitalName"] = lookUpEditorHospital.Text;
            dr["HospitalCode"] = lookUpEditorHospital.CodeValue;
            dr["DepartmentName"] = lookUpEditorDepartment.Text;
            dr["DepartmentCode"] = lookUpEditorDepartment.CodeValue;
            dr["EmployeeLevelName"] = lookUpEditorLevel.Text;
            dr["EmployeeLevelID"] = lookUpEditorLevel.CodeValue;
            dr["DeleteButton"] = "ɾ��";
            dr["EmployeeName"] = lookUpEditorDoctor.Text;
            dr["EmployeeNameStr"] = lookUpEditorDoctor.CodeValue == "" ? "" : lookUpEditorDoctor.CodeValue + "_" + lookUpEditorDoctor.Text;
            dr["EmployeeID"] = lookUpEditorDoctor.CodeValue;
            dt.Rows.Add(dr);
        }

        /// <summary>
        /// ��ʼ��GridControl����Դ
        /// </summary>
        private void InitGridControlDataSource()
        {
            DataTable dt = new DataTable("Department");
            dt.Columns.Add("HospitalName");
            dt.Columns.Add("HospitalCode");
            dt.Columns.Add("DepartmentName");
            dt.Columns.Add("DepartmentCode");
            dt.Columns.Add("EmployeeLevelName");
            dt.Columns.Add("EmployeeLevelID");
            dt.Columns.Add("DeleteButton");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EmployeeNameStr");
            dt.Columns.Add("EmployeeID");
            gridControlDepartment.DataSource = dt;

            m_App.PublicMethod.ConvertGridDataSourceUpper(gridViewDept);
        }

        public void ReadOnlyControl()
        {
            checkEditNormal.Properties.ReadOnly = true;
            checkEditEmergency.Properties.ReadOnly = true;

            memoEditAbstract.Properties.ReadOnly = true;
            memoEditPurpose.Properties.ReadOnly = true;
            lookUpEditorHospital.ReadOnly = true;
            lookUpEditorDepartment.ReadOnly = true;
            lookUpEditorLevel.ReadOnly = true;
            lookUpEditorDoctor.ReadOnly = true;
            dateEditConsultationDate.Properties.ReadOnly = true;
            timeEditConsultationTime.Properties.ReadOnly = true;
            textEditLocation.Properties.ReadOnly = true;
            dateEditApplyDate.Properties.ReadOnly = true;
            timeEditApplyTime.Properties.ReadOnly = true;
            lookUpEditorApplyEmployee.ReadOnly = true;
            lookUpEditorDirector.ReadOnly = true;

            //checkEditNormal.Enabled = false;
            //checkEditEmergency.Enabled = false;
            //lookUpEditorHospital.ReadOnly = true;
            //lookUpEditorDepartment.ReadOnly = true;
            //lookUpEditorLevel.ReadOnly = true;
            //gridControlDepartment.Enabled = false;
            //lookUpEditorApplyEmployee.ReadOnly = true;
            //lookUpEditorDirector.ReadOnly = true;
            //textEditLocation.Enabled = false;
            //memoEditAbstract.Enabled = false;
            //memoEditPurpose.Enabled = false;
            //dateEditApplyDate.Enabled = false;
            //timeEditApplyTime.Enabled = false;
            //dateEditConsultationDate.Enabled = false;
            //timeEditConsultationTime.Enabled = false;
            simpleButtonNew.Visible = false;
            DeleteButton.Visible = false;
        }

        ////////////////////////////

        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {
            if (CheckData())
            {
                try
                {
                    m_App.SqlHelper.BeginTransaction();
                    string consultApplySn = string.Empty;

                    if (m_ConsultApplySN == string.Empty)//����
                    {
                        consultApplySn = SaveConsultationApply(SaveType.Insert, "");

                        SaveConsultationApplyDept(consultApplySn);
                        m_ConsultApplySN = consultApplySn;
                        m_App.SqlHelper.CommitTransaction();
                    }
                    else//�޸�
                    {
                        SaveConsultationApply(SaveType.Modify, m_ConsultApplySN);
                        SaveConsultationApplyDept(m_ConsultApplySN);
                        m_App.SqlHelper.CommitTransaction();
                    }

                    m_App.CustomMessageBox.MessageShow("����ɹ�!", CustomMessageBoxKind.InformationOk);
                }
                catch (Exception ex)
                {
                    m_App.SqlHelper.RollbackTransaction();
                }
            }
        }


        /// <summary>
        /// ���ؼ����������
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (checkEditEmergency.Checked == false && checkEditNormal.Checked == false)
            {
                m_App.CustomMessageBox.MessageShow("��ѡ�������!", CustomMessageBoxKind.WarningOk);
                checkEditNormal.Focus();
                return false;
            }

            if (memoEditAbstract.Text.Trim() == "")
            {
                m_App.CustomMessageBox.MessageShow("�����벡��ժҪ!", CustomMessageBoxKind.WarningOk);
                memoEditAbstract.Focus();
                return false;
            }

            if (memoEditAbstract.Text.Trim().Length>3000)
            {
                m_App.CustomMessageBox.MessageShow("����ժҪ���ȴ���3000!", CustomMessageBoxKind.WarningOk);
                memoEditAbstract.Focus();
                return false;
            }

            if (memoEditPurpose.Text.Trim() == "")
            {
                m_App.CustomMessageBox.MessageShow("���������Ŀ�ĺ�Ҫ��!", CustomMessageBoxKind.WarningOk);
                memoEditPurpose.Focus();
                return false;
            }

            if (memoEditPurpose.Text.Trim().Length > 3000)
            {
                m_App.CustomMessageBox.MessageShow("����Ŀ�ĺ�Ҫ�󳤶ȴ���3000!", CustomMessageBoxKind.WarningOk);
                memoEditPurpose.Focus();
                return false;
            }

            //if (lookUpEditorHospital.CodeValue == "")
            //{
            //    m_App.CustomMessageBox.MessageShow("��ѡ������ҽԺ!", CustomMessageBoxKind.WarningOk);
            //    lookUpEditorHospital.Focus();
            //    return false;
            //}

            //if (lookUpEditorDepartment.CodeValue == "")
            //{
            //    m_App.CustomMessageBox.MessageShow("��ѡ����������!", CustomMessageBoxKind.WarningOk);
            //    lookUpEditorDepartment.Focus();
            //    return false;
            //}

            //if (lookUpEditorLevel.CodeValue == "")
            //{
            //    m_App.CustomMessageBox.MessageShow("��ѡ������ҽʦ����!", CustomMessageBoxKind.WarningOk);
            //    lookUpEditorLevel.Focus();
            //    return false;
            //}

            DataTable dt = gridControlDepartment.DataSource as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                {
                    m_App.CustomMessageBox.MessageShow("����������ҽʦ���Ҽ���!", CustomMessageBoxKind.WarningOk);
                    simpleButtonNew.Focus();
                    return false;
                }
            }

            if (dateEditConsultationDate.Text.Trim() == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ�����������!", CustomMessageBoxKind.WarningOk);
                dateEditConsultationDate.Focus();
                return false;
            }

            if (timeEditConsultationTime.Text.Trim() == "0:00:00")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ�������ʱ��!", CustomMessageBoxKind.WarningOk);
                timeEditConsultationTime.Focus();
                return false;
            }

            if (textEditLocation.Text.Trim() == "")
            {
                m_App.CustomMessageBox.MessageShow("���������ص�!", CustomMessageBoxKind.WarningOk);
                textEditLocation.Focus();
                return false;
            }

            if (dateEditApplyDate.Text.Trim() == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ����������!", CustomMessageBoxKind.WarningOk);
                dateEditApplyDate.Focus();
                return false;
            }

            if (timeEditApplyTime.Text.Trim() == "0:00:00")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ������ʱ��!", CustomMessageBoxKind.WarningOk);
                timeEditApplyTime.Focus();
                return false;
            }

            if (lookUpEditorApplyEmployee.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ������ҽʦ!", CustomMessageBoxKind.WarningOk);
                lookUpEditorApplyEmployee.Text = "";
                lookUpEditorApplyEmployee.Focus();
                return false;
            }

            return true;
        }

        private string SaveConsultationApply(SaveType saveType, string consultApplySN)
        {
            string typeID = Convert.ToString((int)saveType);
            string urgencyTypeID = string.Empty;
            if (checkEditNormal.Checked == true)
            {
                urgencyTypeID = Convert.ToString((int)UrgencyType.Normal);
            }
            else if (checkEditEmergency.Checked == true)
            {
                urgencyTypeID = Convert.ToString((int)UrgencyType.Urgency);
            }

            //string consultTypeID = Convert.ToString((int)ConsultType.More);
            string consultTypeID = "";
            string abstractContent = memoEditAbstract.Text;
            string purpose = memoEditPurpose.Text;
            string applyUser = lookUpEditorApplyEmployee.CodeValue;
            string applyTime = dateEditApplyDate.Text + " " + timeEditApplyTime.Text;
            string director = lookUpEditorDirector.CodeValue;
            string consultTime = dateEditConsultationDate.Text + " " + timeEditConsultationTime.Text;
            string consultLocation = textEditLocation.Text;
            string stateID = Convert.ToString((int)ConsultStatus.WaitApprove);
            string createUser = m_App.User.Id;
            string createTime = System.DateTime.Now.ToString();

            return DataAccess.InsertConsultationApply(typeID, consultApplySN, m_NoOfFirstPage, urgencyTypeID, consultTypeID, abstractContent, purpose,
                applyUser, applyTime, director, consultTime, consultLocation, stateID, createUser, createTime,m_App.User.CurrentDeptId);
        }

        private void SaveConsultationApplyDept(string consultApplySn)
        {
            string sql = string.Format("");
            DataTable dt = gridControlDepartment.DataSource as DataTable;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orderValue = Convert.ToString(i + 1);
                //string hospitalCode = lookUpEditorHospital.CodeValue;
                //string departmentCode = lookUpEditorDepartment.CodeValue;
                string hospitalCode = dt.Rows[i]["HospitalCode"].ToString();
                string departmentCode = dt.Rows[i]["DepartmentCode"].ToString();
                string departmentName = dt.Rows[i]["DepartmentName"].ToString();
                string employeeCode = dt.Rows[i]["EmployeeID"].ToString();
                string employeeName = dt.Rows[i]["EmployeeName"].ToString();
                //string employeeLevelID = lookUpEditorLevel.CodeValue;
                string employeeLevelID = dt.Rows[i]["employeeLevelID"].ToString();
                string createUser = m_App.User.Id;
                string createTime = System.DateTime.Now.ToString();

                DataAccess.InsertConsultationApplyDept(consultApplySn, orderValue, hospitalCode, departmentCode, departmentName,
                    employeeCode, employeeName, employeeLevelID, createUser, createTime);
            }
        }

        private void GetDoctor()
        {
            string deptid = lookUpEditorDepartment.CodeValue;
            string Level = lookUpEditorLevel.CodeValue;
            string usersid = lookUpEditorDoctor.CodeValue;
            if (deptid == "" || usersid != "")
                return;
            else
            {
                string sql = "";
                if (Level == "")
                    sql = string.Format(@"select * from users a where a.valid = 1 and  a.deptid = '{0}' and a.grade is not null and a.grade <> '2004'", deptid);
                else
                    sql = string.Format(@"select * from users a where a.valid = 1 and a.grade = '{0}' and a.deptid = '{1}' and a.grade is not null and a.grade <> '2004'", Level, deptid);

                lookUpWindowDoctor.SqlHelper = m_App.SqlHelper;

                DataTable Dept = m_App.SqlHelper.ExecuteDataTable(sql);

                Dept.Columns["ID"].Caption = "ҽ������";
                Dept.Columns["NAME"].Caption = "ҽ������";

                Dictionary<string, int> cols = new Dictionary<string, int>();

                cols.Add("ID", 70);
                cols.Add("NAME", 80);

                SqlWordbook deptWordBook = new SqlWordbook("querybook", Dept, "ID", "NAME", cols, "ID//NAME//py//wb");
                lookUpEditorDoctor.SqlWordbook = deptWordBook;

            }
        }

        /// <summary>
        /// ѡ�����ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditorDepartment_CodeValueChanged(object sender, EventArgs e)
        {
            lookUpEditorDoctor.CodeValue = "";
            lookUpEditorLevel.CodeValue = "";
            GetDoctor();
        }

        /// <summary>
        /// ѡ��ҽ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditorLevel_CodeValueChanged(object sender, EventArgs e)
        {
            lookUpEditorDoctor.CodeValue = "";
            GetDoctor();
        }

        private void lookUpEditorDoctor_CodeValueChanged(object sender, EventArgs e)
        {
            string userid = lookUpEditorDoctor.CodeValue;

            string sql = string.Format(@"select a.grade from users a where a.id = '{0}'", userid);

            DataTable dt = m_App.SqlHelper.ExecuteDataTable(sql);
            if (dt == null || dt.Rows.Count == 0)
                return;
            if (lookUpEditorLevel.CodeValue == dt.Rows[0][0].ToString())
                return;
            lookUpEditorDoctor.CodeValue = "";
            lookUpEditorLevel.CodeValue = dt.Rows[0][0].ToString();
            lookUpEditorDoctor.CodeValue = userid;
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (char)13)
                {
                    SendKeys.Send("{Tab}");
                    SendKeys.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void gridViewDept_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.gridViewDept.FocusedRowHandle == -1)
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
