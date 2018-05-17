using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using YidanSoft.FrameWork.WinForm.Plugin;
using YidanSoft.Wordbook;
using System.Data.SqlClient;

namespace YidanSoft.Core.Consultation
{
    public partial class UCConsultationInfoForOne : DevExpress.XtraEditors.XtraUserControl
    {
        private IYidanEmrHost m_App;
        private string m_NoOfFirstPage = string.Empty;
        private string m_ConsultApplySN = string.Empty;
        private bool m_ReadOnly = false;

        public UCConsultationInfoForOne()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���ⲿ���õķ������Դ˿ؼ����г�ʼ��
        /// </summary>
        /// <param name="noOfFirstPage">��ҳ���</param>
        /// <param name="app"></param>
        /// <param name="isNew">�Ƿ��¿������ﵥ</param>
        public void Init(string noOfFirstPage, IYidanEmrHost app, bool isNew, bool readOnly, string consultApplySn)
        {
            m_App = app;
            m_NoOfFirstPage = noOfFirstPage;
            Dal.DataAccess.App = app;
            m_ReadOnly = readOnly;
            m_ConsultApplySN = consultApplySn;

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
            }
        }

        /// <summary>
        /// ��ȡԭ����д�����ݣ���Ϊ�ؼ���ֵ
        /// </summary>
        private void SetData()
        {
            DataSet ds = Dal.DataAccess.GetConsultationDataSet(m_ConsultApplySN, "20", Convert.ToString((int)ConsultType.One));
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
                //����ҽԺ
                lookUpEditorHospital.CodeValue = dtConsultApplyDepartment.Rows[0]["HospitalCode"].ToString();

                //��������
                lookUpEditorDepartment.CodeValue = dtConsultApplyDepartment.Rows[0]["DepartmentCode"].ToString();

                //����ҽʦ
                lookUpEditorEmployee.CodeValue = dtConsultApplyDepartment.Rows[0]["EmployeeCode"].ToString();
            }
        }

        #region ע�ᡢע���¼�
        private void RegisterEvent()
        {
            checkEditNormal.CheckedChanged += new EventHandler(checkEditNormal_CheckedChanged);
            checkEditEmergency.CheckedChanged += new EventHandler(checkEditEmergency_CheckedChanged);
            lookUpEditorHospital.CodeValueChanged += new EventHandler(lookUpEditorHospital_CodeValueChanged);
            lookUpEditorDepartment.CodeValueChanged += new EventHandler(lookUpEditorDepartment_CodeValueChanged);
        }

        private void UnRegisterEvent()
        {
            checkEditNormal.CheckedChanged -= new EventHandler(checkEditNormal_CheckedChanged);
            checkEditEmergency.CheckedChanged -= new EventHandler(checkEditEmergency_CheckedChanged);
            lookUpEditorHospital.CodeValueChanged -= new EventHandler(lookUpEditorHospital_CodeValueChanged);
            lookUpEditorDepartment.CodeValueChanged -= new EventHandler(lookUpEditorDepartment_CodeValueChanged);
        }
        #endregion

        #region Event
        private void UCConsultationInfoForOne_Load(object sender, EventArgs e)
        {
        }

        void UCConsultationInfoForOne_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString("��д������Ϣ" + this.GetType().Namespace + "." + this.GetType().Name, this.Font, Brushes.Black, new RectangleF(0f, 0f, this.Width, this.Height), sf);
            }
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

        void lookUpEditorHospital_CodeValueChanged(object sender, EventArgs e)
        {
            //������ҽԺ��Ӱ��
            BindDepartment();
        }

        void lookUpEditorDepartment_CodeValueChanged(object sender, EventArgs e)
        {
            //ҽʦ�ܿ��ҵ�Ӱ��
            BindEmployee();
        }

        #endregion

        #region ��LookUpEditor����Դ

        private void BindData()
        {
            BindHospitalData();
            BindDepartment();
            BindEmployee();
            BindApplyEmployee();
            BindApplyDirector();
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
            colWidths.Add("NAME", 120);

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
            colWidths.Add("NAME", 120);
            SqlWordbook wordBook = new SqlWordbook("Department", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorDepartment.SqlWordbook = wordBook;
        }

        #endregion

        #region ������ҽ��
        /// <summary>
        /// ����������
        /// </summary>
        private void BindEmployee()
        {
            lookUpEditorEmployee.Kind = WordbookKind.Sql;
            lookUpEditorEmployee.ListWindow = lookUpWindowEmployee;
            BindEmployeeWordBook(GetConsultationData("3"));
        }

        private void BindEmployeeWordBook(DataTable dataTableData)
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
            colWidths.Add("NAME", 120);
            SqlWordbook wordBook = new SqlWordbook("Employee", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorEmployee.SqlWordbook = wordBook;
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
            colWidths.Add("NAME", 120);
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
            colWidths.Add("NAME", 120);
            SqlWordbook wordBook = new SqlWordbook("Director", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorDirector.SqlWordbook = wordBook;
        }

        #endregion

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
                string wardID = m_App.User.CurrentWardId.ToString() ;
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, wardID);
            }
            else if (typeID == "5")//����ҽʦ������
            {
                string wardID = m_App.User.CurrentWardId.ToString();
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, wardID);
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


        /// <summary>
        /// ��ս��棨���ص���ʼ״̬��
        /// </summary>
        public void Clear()
        {
            SetDefaultValue();
            checkEditNormal.Checked = false;
            checkEditEmergency.Checked = false;
            memoEditAbstract.Text = "";
            memoEditPurpose.Text = "";
            textEditLocation.Text = "";

            lookUpEditorDepartment.CodeValue = "";
            lookUpEditorEmployee.CodeValue = "";
            lookUpEditorDirector.CodeValue = "";
        }

        /// <summary>
        /// ���������пؼ�ֻ��
        /// </summary>
        public void ReadOnlyControl()
        {
            //checkEditNormal.Enabled = false;
            //checkEditEmergency.Enabled = false;
            //lookUpEditorHospital.ReadOnly = true;
            //lookUpEditorDepartment.ReadOnly = true;
            //lookUpEditorEmployee.ReadOnly = true;
            //lookUpEditorApplyEmployee.ReadOnly = true;
            //lookUpEditorDirector.ReadOnly = true;
            //textEditLocation.Enabled = false;
            //memoEditAbstract.Enabled = false;
            //memoEditPurpose.Enabled = false;
            //dateEditApplyDate.Enabled = false;
            //timeEditApplyTime.Enabled = false;
            //dateEditConsultationDate.Enabled = false;
            //timeEditConsultationTime.Enabled = false;
        }

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

            if (memoEditPurpose.Text.Trim() == "")
            {
                m_App.CustomMessageBox.MessageShow("���������Ŀ�ĺ�Ҫ��!", CustomMessageBoxKind.WarningOk);
                memoEditPurpose.Focus();
                return false;
            }

            if (lookUpEditorHospital.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ������ҽԺ!", CustomMessageBoxKind.WarningOk);
                lookUpEditorHospital.Text = "";
                lookUpEditorHospital.Focus();
                return false;
            }

            if (lookUpEditorDepartment.CodeValue == "")
            {
                m_App.CustomMessageBox.MessageShow("��ѡ����������!", CustomMessageBoxKind.WarningOk);
                lookUpEditorDepartment.Text = "";
                lookUpEditorDepartment.Focus();
                return false;
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

            string consultTypeID = Convert.ToString((int)ConsultType.One);
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
            string applydept = m_App.CurrentPatientInfo.InfoOfAdmission.AdmitInfo.CurrentDepartment.Code;
            string ward = m_App.CurrentPatientInfo.InfoOfAdmission.AdmitInfo.CurrentWard.Code;
            //string bed = m_App.CurrentPatientInfo.InfoOfAdmission.AdmitInfo.BedNo;

            return Dal.DataAccess.InsertConsultationApply(typeID, consultApplySN, m_NoOfFirstPage, urgencyTypeID, consultTypeID, abstractContent, purpose,
                applyUser, applyTime, director, consultTime, consultLocation, stateID, createUser, createTime);
        }

        private void SaveConsultationApplyDept(string consultApplySn)
        {
            string orderValue = "1";
            string hospitalCode = lookUpEditorHospital.CodeValue;
            string departmentCode = lookUpEditorDepartment.CodeValue;
            string departmentName = string.Empty;
            string employeeCode = lookUpEditorEmployee.CodeValue;
            string employeeName = string.Empty;
            string employeeLevelID = string.Empty;
            string createUser = m_App.User.Id;
            string createTime = System.DateTime.Now.ToString();

            Dal.DataAccess.InsertConsultationApplyDept(consultApplySn, orderValue, hospitalCode, departmentCode, departmentName,
                employeeCode, employeeName, employeeLevelID, createUser, createTime);
        }
    }
}
