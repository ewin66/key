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
using DrectSoft.Common;
using DrectSoft.DSSqlHelper;

namespace DrectSoft.Core.Consultation
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
            InitFocus();
        }

        public void Init(string noOfFirstPage, IEmrHost app, bool isNew/*�Ƿ�����������*/, bool readOnly/*�Ƿ�ֻ��*/, string consultApplySN)
        {
            m_App = app;
            DS_SqlHelper.CreateSqlHelper();
            m_NoOfFirstPage = noOfFirstPage;
            m_ReadOnly = readOnly;
            m_ConsultApplySN = consultApplySN;

            gridViewDept.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridViewDept.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewDept.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            RegisterEvent();
            InitInner(isNew);
            InitFocus();
        }

        /// <summary>
        /// ���ó�ʼ������
        /// </summary>
        public void InitFocus()
        {
            this.ActiveControl = memoEditAbstract;
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
                simpleButtonNew.Enabled = false;
                btn_reset.Enabled = false;
            }
        }


        /// <summary>
        /// ��ȡԭ����д�����ݣ���Ϊ�ؼ���ֵ
        /// </summary>
        private void SetData()
        {
            //BindApplyEmployee();

            DataSet ds = Dal.DataAccess.GetConsultationDataSet(m_ConsultApplySN, "20");//, Convert.ToString((int)ConsultType.More));
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
                //timeEditApplyTime.Text = dtConsultApply.Rows[0]["ApplyTime"].ToString().Split(' ')[1];
                timeEditApplyTime.EditValue = dtConsultApply.Rows[0]["ApplyTime"].ToString().Split(' ')[1];   //

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

        /// <summary>
        /// ɾ���¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridViewDept_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // edit by Yanqiao.Cai 2012-11-05
                // С�������������¼�
                GridHitInfo hit = gridViewDept.CalcHitInfo(e.X, e.Y);
                if (hit.RowHandle < 0)
                {
                    return;
                }

                DataTable dt = gridControlDepartment.DataSource as DataTable;
                if (null == dt || dt.Rows.Count == 0)
                {
                    return;
                }
                else if (gridViewDept.FocusedRowHandle < 0)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("��ѡ��һ����¼");
                    return;
                }



                if (hit.Column != null)
                {
                    if (hit.Column.Name == "DeleteButton")
                    {
                        if (m_App.CustomMessageBox.MessageShow(string.Format("��ȷ��Ҫɾ���û��������¼��Ϣ��"), CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
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
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
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
            colWidths.Add("ID", 60);
            colWidths.Add("NAME", 90);

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
            colWidths.Add("ID", 60);
            colWidths.Add("NAME", 90);
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
            colWidths.Add("ID", 60);
            colWidths.Add("NAME", 90);
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
            colWidths.Add("ID", 60);
            colWidths.Add("NAME", 90);
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

            cols.Add("ID", 60);
            cols.Add("NAME", 90);

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
            if (Dal.DataAccess.App == null)
            {
                Dal.DataAccess.App = m_App;
            }
            DataTable dataTableConsultationData = new DataTable();

            if (typeID == "1")//ҽԺ
            {
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, "");
            }
            else if (typeID == "2")//��������
            {
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, lookUpEditorHospital.CodeValue.Trim());
            }
            else if (typeID == "3")//����ҽ��
            {
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, lookUpEditorDepartment.CodeValue.Trim());
            }
            else if (typeID == "4")//����ҽʦ
            {
                string wardID = m_App.User.CurrentWardId.ToString();
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, wardID);
            }
            else if (typeID == "5")//����ҽʦ������
            {
                string wardID = m_App.User.CurrentWardId.ToString();
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, wardID);
            }
            else if (typeID == "6")//CategoryDetail
            {
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, "20");
            }
            else if (typeID == "9")//ҽʦ����
            {
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, "20");
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
            //if (dateEditApplyDate.Text.Trim() == "" || timeEditApplyTime.Text.Trim() == "")
            //{
            dateEditApplyDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            timeEditApplyTime.EditValue = System.DateTime.Now.ToString().Split(' ')[1];
            //}

            //if (dateEditApplyDate.Text.Trim() == "" || timeEditApplyTime.Text.Trim() == "")
            //{
            dateEditConsultationDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            timeEditConsultationTime.EditValue = System.DateTime.Now.ToString().Split(' ')[1];
            //}

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

        /// <summary>
        /// �����¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonNew_Click(object sender, EventArgs e)
        {
            try
            {
                Insert();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// �����¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        private void Insert()
        {
            try
            {
                if (CheckDataDept())
                {
                    InsertData();
                    lookUpEditorHospital.Focus();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool CheckDataDept()
        {
            if (lookUpEditorHospital.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ������ҽԺ", CustomMessageBoxKind.WarningOk);
                lookUpEditorHospital.Focus();
                return false;
            }
            else if (lookUpEditorDepartment.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ����������", CustomMessageBoxKind.WarningOk);
                lookUpEditorDepartment.Focus();
                return false;
            }
            else if (lookUpEditorLevel.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ������ҽʦ����", CustomMessageBoxKind.WarningOk);
                lookUpEditorLevel.Focus();
                return false;
            }

            DataTable dt = gridControlDepartment.DataSource as DataTable;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //�ݲ����Ŷ�ƻ���  edit b yywk  2012��11��10��10:19:32
                if (!dt.Rows[i]["DepartmentCode"].Equals(lookUpEditorDepartment.CodeValue))
                {
                    m_App.CustomMessageBox.MessageShow("��ƻ��﹦����δ����", CustomMessageBoxKind.WarningOk);
                    gridViewDept.FocusedRowHandle = i;
                    return false;
                }
                if (dt.Rows[i]["EmployeeLevelID"].Equals(lookUpEditorLevel.CodeValue) && lookUpEditorDoctor.CodeValue == "")
                {
                    m_App.CustomMessageBox.MessageShow("�Ѿ�������ü���ҽ��", CustomMessageBoxKind.WarningOk);
                    gridViewDept.FocusedRowHandle = i;
                    return false;
                }
                if (lookUpEditorDoctor.CodeValue == "")
                    continue;
                if (dt.Rows[i]["EmployeeID"].Equals(lookUpEditorDoctor.CodeValue))
                {
                    m_App.CustomMessageBox.MessageShow("�ü�¼�Ѿ�����", CustomMessageBoxKind.WarningOk);
                    gridViewDept.FocusedRowHandle = i;
                    return false;
                }


            }
            //            string sql = string.Format(@"select id from consultapplydepartment a ,consultapply b  where a.DEPARTMENTCODE='{0}'
            //             and a.EMPLOYEECODE='{1}' and a.EMPLOYEELEVELID='{2}' and a.valid='1' and b.stateid='6730' and b.noofinpat='{3}'", lookUpEditorDepartment.CodeValue
            //                                                                           , lookUpEditorDoctor.CodeValue,
            //                                                                           lookUpEditorLevel.CodeValue,m_App.CurrentPatientInfo.NoOfFirstPage);

            //����߼��жϲ�Ӧ�ü�  ��ΰ��ע�͵� 2013��1��14��12:10:04
            //            string sql = string.Format(@"select A.Consultapplysn,B.Consultapplysn from (
            //        (select Consultapplysn from consultapplydepartment where DEPARTMENTCODE='{0}'
            //             and EMPLOYEECODE='{1}' and EMPLOYEELEVELID='{2}' and valid='1' )A) 
            //inner join consultapply B on A.Consultapplysn=B.Consultapplysn and B.Stateid='6730' and B.Noofinpat='{3}'", lookUpEditorDepartment.CodeValue
            //                                                                           , lookUpEditorDoctor.CodeValue,
            //                                                                           lookUpEditorLevel.CodeValue, m_App.CurrentPatientInfo.NoOfFirstPage);

            //            if (m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text).Rows.Count > 0)
            //            {
            //                m_App.CustomMessageBox.MessageShow("�˲����������������ظ��ύ��", CustomMessageBoxKind.WarningOk);
            //                //lookUpEditorHospital.Focus();
            //                return false;
            //            }

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
            try
            {
                //edit by cyq 2012-10-19
                //checkEditNormal.Properties.ReadOnly = true;
                //checkEditEmergency.Properties.ReadOnly = true;
                //memoEditAbstract.Properties.ReadOnly = true;
                //memoEditPurpose.Properties.ReadOnly = true;
                //lookUpEditorHospital.ReadOnly = true;
                //lookUpEditorDepartment.ReadOnly = true;
                //lookUpEditorLevel.ReadOnly = true;
                //lookUpEditorDoctor.ReadOnly = true;
                //dateEditConsultationDate.Properties.ReadOnly = true;
                //timeEditConsultationTime.Properties.ReadOnly = true;
                //textEditLocation.Properties.ReadOnly = true;
                //dateEditApplyDate.Properties.ReadOnly = true;
                //timeEditApplyTime.Properties.ReadOnly = true;
                //lookUpEditorApplyEmployee.ReadOnly = true;
                //lookUpEditorDirector.ReadOnly = true;

                checkEditNormal.Enabled = false;
                checkEditEmergency.Enabled = false;
                memoEditAbstract.Enabled = false;
                memoEditPurpose.Enabled = false;
                lookUpEditorHospital.Enabled = false;
                lookUpEditorDepartment.Enabled = false;
                lookUpEditorLevel.Enabled = false;
                lookUpEditorDoctor.Enabled = false;
                dateEditConsultationDate.Enabled = false;
                timeEditConsultationTime.Enabled = false;
                textEditLocation.Enabled = false;
                dateEditApplyDate.Enabled = false;
                timeEditApplyTime.Enabled = false;
                //gridControlDepartment.Enabled = false;
                lookUpEditorApplyEmployee.Enabled = false;
                lookUpEditorDirector.Enabled = false;

                simpleButtonNew.Enabled = false;
                btn_reset.Enabled = false;
                DeleteButton.Visible = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ����
        /// edit by wangji 2013 1.6
        /// </summary>
        public void Save()
        {
            if (CheckData())
            {
                try
                {
                    //m_App.SqlHelper.BeginTransaction();
                    DS_SqlHelper.BeginTransaction();
                    string consultApplySn = string.Empty;

                    if (m_ConsultApplySN == string.Empty)//����
                    {
                        consultApplySn = SaveConsultationApply(SaveType.Insert, "");
                        SaveConsultationApplyDept(consultApplySn);
                        #region ע��
                        //                        DataTable dt = gridControlDepartment.DataSource as DataTable;

                        //                        for (int i = 0; i < dt.Rows.Count; i++)
                        //                        {
                        //                            //string orderValue = Convert.ToString(i + 1);
                        //                            //string hospitalCode = lookUpEditorHospital.CodeValue;
                        //                            //string departmentCode = lookUpEditorDepartment.CodeValue;
                        //                            string hospitalCode = dt.Rows[i]["HospitalCode"].ToString();
                        //                            string departmentCode = dt.Rows[i]["DepartmentCode"].ToString();
                        //                            string departmentName = dt.Rows[i]["DepartmentName"].ToString();
                        //                            string employeeCode = dt.Rows[i]["EmployeeID"].ToString();
                        //                            string employeeName = dt.Rows[i]["EmployeeName"].ToString();
                        //                            //string employeeLevelID = lookUpEditorLevel.CodeValue;
                        //                            string employeeLevelID = dt.Rows[i]["employeeLevelID"].ToString();

                        //                            string sql = string.Format(@"select  ID from 
                        //consultapplydepartment where CONSULTAPPLYSN='{0}' and DEPARTMENTCODE='{1}' 
                        //and EMPLOYEECODE='{2}' and EMPLOYEELEVELID='{3}'", consultApplySn, departmentCode, employeeCode, employeeLevelID);
                        //                            if (m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text).Rows.Count > 0)
                        //                            {
                        //                                m_App.CustomMessageBox.MessageShow("�˲����������������ظ��ύ��");

                        //                                string updatesql = string.Format("update consultapplydepartment set valid='0' where CONSULTAPPLYSN='{0}'",consultApplySn);
                        //                                m_App.SqlHelper.ExecuteDataTable(updatesql,CommandType.Text);
                        //                                string updatesql1 = string.Format("update consultapply set valid='0' where CONSULTAPPLYSN='{0}'", consultApplySn);
                        //                                m_App.SqlHelper.ExecuteDataTable(updatesql1, CommandType.Text);
                        //                                string updatesql2 = string.Format("update consultrecorddepartment set valid='0' where CONSULTAPPLYSN='{0}'", consultApplySn);
                        //                                m_App.SqlHelper.ExecuteDataTable(updatesql2, CommandType.Text);
                        //                                gridControlDepartment.DataSource = null;
                        //                            }

                        //}
                        #endregion
                        m_ConsultApplySN = consultApplySn;

                        //m_App.SqlHelper.CommitTransaction();

                    }
                    else//�޸�
                    {
                        SaveConsultationApply(SaveType.Modify, m_ConsultApplySN);
                        SaveConsultationApplyDept(m_ConsultApplySN);
                        //m_App.SqlHelper.CommitTransaction();
                        //DS_SqlHelper.CommitTransaction();
                    }
                    DS_SqlHelper.CommitTransaction();
                    m_App.CustomMessageBox.MessageShow("����ɹ�", CustomMessageBoxKind.InformationOk);
                }
                catch (Exception ex)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
                    //m_App.SqlHelper.RollbackTransaction();
                }
            }
        }


        /// <summary>
        /// ���ؼ����������
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            try
            {
                DataTable dt = gridControlDepartment.DataSource as DataTable;
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        m_App.CustomMessageBox.MessageShow("�����������������¼", CustomMessageBoxKind.WarningOk);
                        simpleButtonNew.Focus();
                        return false;
                    }
                }

                if (checkEditEmergency.Checked == false && checkEditNormal.Checked == false)
                {
                    m_App.CustomMessageBox.MessageShow("��ѡ�������", CustomMessageBoxKind.WarningOk);
                    checkEditNormal.Focus();
                    return false;
                }

                if (memoEditAbstract.Text.Trim() == "")
                {
                    m_App.CustomMessageBox.MessageShow("����ժҪ����Ϊ��", CustomMessageBoxKind.WarningOk);
                    memoEditAbstract.Focus();
                    return false;
                }

                if (memoEditAbstract.Text.Trim().Length > 3000)
                {
                    m_App.CustomMessageBox.MessageShow("����ժҪ���Ȳ��ܴ���3000", CustomMessageBoxKind.WarningOk);
                    memoEditAbstract.Focus();
                    return false;
                }

                if (memoEditPurpose.Text.Trim() == "")
                {
                    m_App.CustomMessageBox.MessageShow("Ŀ��Ҫ����Ϊ��", CustomMessageBoxKind.WarningOk);
                    memoEditPurpose.Focus();
                    return false;
                }

                if (memoEditPurpose.Text.Trim().Length > 3000)
                {
                    m_App.CustomMessageBox.MessageShow("Ŀ��Ҫ�󳤶Ȳ��ܴ���3000", CustomMessageBoxKind.WarningOk);
                    memoEditPurpose.Focus();
                    return false;
                }

                //if (lookUpEditorHospital.CodeValue == "")
                //{
                //    m_App.CustomMessageBox.MessageShow("��ѡ������ҽԺ", CustomMessageBoxKind.WarningOk);
                //    lookUpEditorHospital.Focus();
                //    return false;
                //}

                //if (lookUpEditorDepartment.CodeValue == "")
                //{
                //    m_App.CustomMessageBox.MessageShow("��ѡ����������", CustomMessageBoxKind.WarningOk);
                //    lookUpEditorDepartment.Focus();
                //    return false;
                //}

                //if (lookUpEditorLevel.CodeValue == "")
                //{
                //    m_App.CustomMessageBox.MessageShow("��ѡ������ҽʦ����", CustomMessageBoxKind.WarningOk);
                //    lookUpEditorLevel.Focus();
                //    return false;
                //}

                if (dateEditConsultationDate.Text.Trim() == "")
                {
                    m_App.CustomMessageBox.MessageShow("��ѡ�����������", CustomMessageBoxKind.WarningOk);
                    dateEditConsultationDate.Focus();
                    return false;
                }

                if (timeEditConsultationTime.Text.Trim() == "0:00:00")
                {
                    m_App.CustomMessageBox.MessageShow("��ѡ�������ʱ��", CustomMessageBoxKind.WarningOk);
                    timeEditConsultationTime.Focus();
                    return false;
                }

                if (DateTime.Parse(dateEditApplyDate.Text + " " + timeEditApplyTime.Text) >= DateTime.Parse(dateEditConsultationDate.Text + " " + timeEditConsultationTime.Text))
                {
                    m_App.CustomMessageBox.MessageShow("�����ʱ�䲻��С�ڻ��������ʱ��", CustomMessageBoxKind.WarningOk);
                    timeEditConsultationTime.Focus();
                    return false;
                }

                if (textEditLocation.Text.Trim() == "")
                {
                    m_App.CustomMessageBox.MessageShow("����ص㲻��Ϊ��", CustomMessageBoxKind.WarningOk);
                    textEditLocation.Focus();
                    return false;
                }

                if (dateEditApplyDate.Text.Trim() == "")
                {
                    m_App.CustomMessageBox.MessageShow("��ѡ����������", CustomMessageBoxKind.WarningOk);
                    dateEditApplyDate.Focus();
                    return false;
                }

                if (timeEditApplyTime.Text.Trim() == "0:00:00")
                {
                    m_App.CustomMessageBox.MessageShow("��ѡ������ʱ��", CustomMessageBoxKind.WarningOk);
                    timeEditApplyTime.Focus();
                    return false;
                }

                if (lookUpEditorApplyEmployee.CodeValue == "")
                {
                    m_App.CustomMessageBox.MessageShow("��ѡ������ҽʦ", CustomMessageBoxKind.WarningOk);
                    lookUpEditorApplyEmployee.Text = "";
                    lookUpEditorApplyEmployee.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string SaveConsultationApply(SaveType saveType, string consultApplySN)
        {
            try
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
                //edit by tj 2012-10-26  ����ǲ���Ҫ���������� ����������������ʱֱ�ӽ�״̬��Ϊ�����������������Ϊ������ˡ�
                string stateID = string.Empty;
                if (CommonObjects.IsNeedVerifyInConsultation)
                {
                    stateID = Convert.ToString((int)ConsultStatus.WaitApprove);
                }
                else
                {
                    stateID = Convert.ToString((int)ConsultStatus.WaitConsultation);
                }
                string createUser = m_App.User.Id;
                string createTime = System.DateTime.Now.ToString();

                return Dal.DataAccess.InsertConsultationApply(typeID, consultApplySN, m_NoOfFirstPage, urgencyTypeID, consultTypeID, abstractContent, purpose,
                    applyUser, applyTime, director, consultTime, consultLocation, stateID, createUser, createTime, m_App.User.CurrentDeptId,lookUpEditorApplyEmployee.CodeValue,"");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveConsultationApplyDept(string consultApplySn)
        {
            try
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

                    Dal.DataAccess.InsertConsultationApplyDept(consultApplySn, orderValue, hospitalCode, departmentCode, departmentName,
                        employeeCode, employeeName, employeeLevelID, createUser, createTime);
                }
            }
            catch (Exception)
            {
                throw;
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
                try
                {
                    string sql = "";
                    if (Level == "")
                        sql = string.Format(@"select ID,NAME,PY,WB from users a where a.deptid = '{0}' and a.grade is not null and a.grade <> '2004'", deptid);
                    else
                        sql = string.Format(@"select ID,NAME,PY,WB from users a where a.grade = '{0}' and a.deptid = '{1}' and a.grade is not null and a.grade <> '2004'", Level, deptid);

                    lookUpWindowDoctor.SqlHelper = m_App.SqlHelper;

                    DataTable Dept = m_App.SqlHelper.ExecuteDataTable(sql);
                    //add by ywk  2012��12��3��10:21:34
                    //�����ҽ����Ӧ���������һ��߶���������Ӧ���ټ��� (������ѡ�Ŀ���)
                    string sqlsearch = string.Format(@"select  USERID,DEPTID,WARDID  from USER2DEPT where deptid='{0}' ", deptid);
                    DataTable dtuser2Dept = m_App.SqlHelper.ExecuteDataTable(sqlsearch, CommandType.Text);
                    DataTable dtResultTemp = Dept.Clone();
                    string splitersql = string.Empty;
                    string sql1 = string.Empty;
                    if (dtuser2Dept.Rows.Count > 0 && dtuser2Dept != null)
                    {

                        for (int i = 0; i < dtuser2Dept.Rows.Count; i++)
                        {
                            splitersql += dtuser2Dept.Rows[i]["USERID"].ToString() + ",";
                        }
                        splitersql = splitersql.Remove(splitersql.Length - 1);
                        sql1 = string.Format("select ID,NAME,PY,WB from users where id in ({0}) and grade='{1}'", splitersql, Level);
                        dtResultTemp = m_App.SqlHelper.ExecuteDataTable(sql1, CommandType.Text);
                    }

                    if (dtResultTemp != null && dtResultTemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResultTemp.Rows.Count; i++)
                        {
                            DataRow dr = Dept.NewRow();
                            dr["ID"] = dtResultTemp.Rows[i]["ID"].ToString();
                            dr["NAME"] = dtResultTemp.Rows[i]["NAME"].ToString();
                            dr["PY"] = dtResultTemp.Rows[i]["PY"].ToString();
                            dr["WB"] = dtResultTemp.Rows[i]["WB"].ToString();
                            Dept.Rows.Add(dr);
                        }
                    }


                    Dept.Columns["ID"].Caption = "ҽ������";
                    Dept.Columns["NAME"].Caption = "ҽ������";

                    Dictionary<string, int> cols = new Dictionary<string, int>();

                    cols.Add("ID", 65);
                    cols.Add("NAME", 160);

                    DataView dv = new DataView(Dept);//������ͼ edit by ywk 
                    DataTable dtresu = dv.ToTable(true, "ID", "NAME", "PY", "WB");

                    SqlWordbook deptWordBook = new SqlWordbook("querybook", dtresu, "ID", "NAME", cols, "ID//NAME//py//wb");
                    lookUpEditorDoctor.SqlWordbook = deptWordBook;

                }
                catch (Exception ex)
                {
                    m_App.CustomMessageBox.MessageShow(ex.Message);
                    return;
                }


            }
        }

        /// <summary>
        /// ѡ������¼�
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

        /// <summary>
        /// ��ѡ��س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == 13)
                {
                    DS_Common.cbx_KeyPress(sender);
                }
            }
            catch (Exception ex)
            {
                m_App.CustomMessageBox.MessageShow(ex.Message);
            }
        }

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-25</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewDept_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                DS_Common.AutoIndex(e);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// �����¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-14</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
                Reset();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
        /// <summary>
        /// ���÷���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-14</date>
        /// </summary>
        private void Reset()
        {
            try
            {
                lookUpEditorHospital.CodeValue = string.Empty;
                lookUpEditorDepartment.CodeValue = string.Empty;
                lookUpEditorLevel.CodeValue = string.Empty;
                lookUpEditorDoctor.CodeValue = string.Empty;
                lookUpEditorHospital.Focus();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
