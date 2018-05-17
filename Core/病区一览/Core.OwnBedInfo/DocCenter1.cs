using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Card.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System.Xml.Serialization;
using YidanSoft.FrameWork.WinForm;
using YidanSoft.FrameWork.WinForm.Plugin;
using YidanSoft.Common.Eop;
using YidanSoft.Resources;
using YidanSoft.Core.RedactPatientInfo.UserControls;
using YidanSoft.Core.QCDeptReport;
using YidanSoft.Core.RedactPatientInfo;
using YidanSoft.Core.MedicalRecordQuery;
using YidanSoft.Core.Consultation;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using YidanSoft.Core.OwnBedInfo.Helper;

namespace YidanSoft.Core.OwnBedInfo
{
    public partial class DocCenter : DevExpress.XtraEditors.XtraForm
    {
        private IYidanEmrHost m_App;
        /// <summary>
        /// �����б�
        /// </summary>
        private UserControlAllListBedInfo m_UserControlAllListIno;

        Dictionary<decimal, Inpatient> m_PatsList;

        private DataTable myPats;

        private DataTable OutPats;

        private DataManager m_DataManager;

        /// <summary>
        /// �Ƿ����������ҵĻ���
        /// </summary>
        public bool NeedUndoMyInpatient { get; set; }
        /// <summary>
        /// ���Ҵ���
        /// </summary>
        private string m_DeptId;
        /// <summary>
        /// ��������
        /// </summary>
        private string m_WardId;

        private ImageList m_ImageListzifei;

        private Employee m_Employee;

        private WaitDialogForm m_WaitDialog;
        #region constructor
        public DocCenter()
            : this(null)
        {
        }

        public DocCenter(IYidanEmrHost app)
        {
            InitializeComponent();

            monthCalendar1.DateTime = DateTime.Now;
            monthCalendar1.TodayButton.Text = "����";

            m_WaitDialog = new WaitDialogForm("�����û����桭��", "���Եȡ�");
            m_App = app;
            m_DeptId = m_App.User.CurrentDeptId;
            m_WardId = m_App.User.CurrentWardId;

            
            m_PatsList = new Dictionary<decimal, Inpatient>();
            monthCalendar1.EditDateModified += new EventHandler(monthCalendar1_EditDateModified);
            gridViewConsultation.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gridViewConsultation_CustomDrawCell);

        }
        #endregion


        /// <summary>
        /// �����Ҳ�Ļ�����Ϣ���б�������״̬��ʾ��ɫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridViewConsultation_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue == null) return;
            DataRowView drv = gridViewConsultation.GetRow(e.RowHandle) as DataRowView;
            string value = drv["urgencytype"].ToString().Trim();
            if (value == "����")
            {
                e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
                e.Appearance.ForeColor = Color.White;//ǰ��ɫΪ��ɫ
            }
        }
        private void FormUserControlShow_Load(object sender, EventArgs e)
        {
            SetWaitDialogCaption("���ڶ�ȡ��������...");
            userCtrlTimeQcInfo1.App = m_App;
            dateEditFrom.DateTime = DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = DateTime.Now;
            this.cmbQueryType.SelectedIndex = 0;
            m_DataManager = new DataManager(m_App, m_DeptId, m_WardId);
            InitializeImage();
            InitAllPat(1);
            InitUndoMyInpatient();
            HideWaitDialog();
        }



        /// <summary>
        /// �жϡ������ҵĲ��ˡ����ܵ���ʾ���
        /// </summary>
        private void InitUndoMyInpatient()
        {
            string config = m_App.SqlHelper.ExecuteScalar("select value from appcfg where configkey = 'IsOpenSetMyInpatient'", CommandType.Text).ToString();
            NeedUndoMyInpatient = !config.Equals("0");
            barButtonItemUndoMyInpatient.Visibility = NeedUndoMyInpatient.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        public void SetWaitDialogCaption(string caption)
        {
            if (m_WaitDialog != null)
            {
                if (!m_WaitDialog.Visible)
                    m_WaitDialog.Visible = true;
                m_WaitDialog.Caption = caption;
            }

        }

        public void HideWaitDialog()
        {
            if (m_WaitDialog != null)
                m_WaitDialog.Hide();
        }

        #region methods

        /// <summary>
        /// ��ʼ�� �����б� usercontrol
        /// </summary>
        private void InitAllListView()
        {
            if (m_UserControlAllListIno == null)
            {
                m_UserControlAllListIno = new UserControlAllListBedInfo(m_App);
                m_UserControlAllListIno.Dock = DockStyle.Fill;
                m_UserControlAllListIno.NeedUndoMyInpatient = NeedUndoMyInpatient;
                this.AllInpTabPage.Controls.Add(m_UserControlAllListIno);
            }
        }

        /// <summary>
        /// ���ˢ��ʱ,��������ʾ��usercontrol�ؼ�ˢ��
        /// </summary>
        private void ControlRefresh()
        {
            if (this.xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĲ���
            {
                InitAllPat(1);
            }
            else if (this.xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
            {
                if (AllInpTabPage.Controls.Count > 1)
                    AllInpTabPage.Controls.Clear();
                InitAllListView();
            }
            else if (this.xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//��ʷ���˲�ѯ
            {
                btnQuery_Click(null, null);
            }
            else if (this.xtraTabControl1.SelectedTabPage == FailTabPage)//��Ժδ�鵵
            {
                if (FailTabPage.Controls.Count > 1)
                    return;
                UCFail m_UCFail = new UCFail(m_App);
                m_UCFail.Dock = DockStyle.Fill;
                FailTabPage.Controls.Add(m_UCFail);
            }
            else if (this.xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
            {
                BindEmrPoint();
            }
            else if (this.xtraTabControl1.SelectedTabPage == Replenish)//��д����
            {
                if (Replenish.Controls.Count > 1) return;
                ReplenishPatRec repl = new ReplenishPatRec();
                repl.Dock = DockStyle.Fill;
                Replenish.Controls.Add(repl);
                repl.LoadData(m_App);
            }

            BindTaskInfo();
        }

        /// <summary>
        /// ��ʼ��ͼƬ
        /// </summary>
        private void InitializeImage()
        {
            try
            {
                ImageHelper.GetImageListIllness();
                imageListCustomwzjb = ImageHelper.GetImageListIllness();
                repItemImageComboBoxwzjb.SmallImages = imageListCustomwzjb;
                DataTable dt = null;
                if ((dt == null) || (dt.Rows.Count <= 0))
                {
                    ImageComboBoxItem item1 = new ImageComboBoxItem("һ�㲡��", "0", 0);
                    ImageComboBoxItem item2 = new ImageComboBoxItem("Σ�ز���", "1", 1);
                    ImageComboBoxItem item3 = new ImageComboBoxItem("���ز���", "2", 2);
                    repItemImageComboBoxwzjb.Items.AddRange(new ImageComboBoxItem[] { item1, item2, item3 });
                }
                else
                {
                    ImageComboBoxItem[] imageCombo = new ImageComboBoxItem[dt.Rows.Count];
                    for (int index = 0; index < imageCombo.Length; index++)
                    {
                        ImageComboBoxItem item = new ImageComboBoxItem(dt.Rows[index]["name"].ToString().Trim(), dt.Rows[index]["mxdm"].ToString().Trim(), Convert.ToInt16(dt.Rows[index]["mxdm"].ToString().Trim()));
                        imageCombo[index] = item;
                    }
                    repItemImageComboBoxwzjb.Items.AddRange(imageCombo);
                }
                imageListcwdm = ImageHelper.GetImageListBedNum();
                m_ImageListzifei = ImageHelper.GetImageListPay();
                imageListBrxb = ImageHelper.GetImageListBrxb();
                repItemImageComboBoxBrxb.SmallImages = imageListBrxb;
                ImageComboBoxItem ImageComboItemMale = new ImageComboBoxItem("��", "1", 1);
                ImageComboBoxItem ImageComboItemFemale = new ImageComboBoxItem("Ů", "2", 0);
                repItemImageComboBoxBrxb.Items.Add(ImageComboItemMale);
                repItemImageComboBoxBrxb.Items.Add(ImageComboItemFemale);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region �ڶ���MENU

        /// <summary>
        /// ҽ��һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_App.LoadPlugIn("YidanSoft.Core.DoctorTasks.dll", "YidanSoft.Core.DoctorTasks.FormInpatientOrder");
        }

        /// <summary>
        /// ����һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemWriteView_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadRecordInput();
        }

        /// <summary>
        /// ������д
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemWrite_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadRecordInput();
        }

        /// <summary>
        /// ��������¼��
        /// </summary>
        private void LoadRecordInput()
        {
            m_App.LoadPlugIn("Yidansoft.Core.MainEmrPad.dll", "Yidansoft.Core.MainEmrPad.MainForm");
        }
        #endregion


        #region ��������

        /// <summary>
        /// �����������ݼ���
        /// </summary>
        private DataSet m_DataTask;

        /// <summary>
        /// �󶨽�������
        /// </summary>
        private void BindTaskInfo()
        {
            m_DataTask = GetTaskToday();

            if (m_DataTask != null)
            {
                for (int i = 0; i < m_DataTask.Tables.Count; i++)
                {
                    if (i == (int)TaskType.Consultation)
                    {
                        GetConsultionData();
                        InitConsultConfirm();
                    }
                    else if (i == (int)TaskType.ThreeLevelCheck)
                    {
                        DataTable dt = m_DataTask.Tables[i];
                        GetThreeLevelCheckEmrDoc(dt);
                        this.gridControlThreeLevelCheck.DataSource = dt;
                    }
                }
            }
            //��ʱ����Ϣ
            userCtrlTimeQcInfo1.CheckDoctorTime(m_App.User.DoctorId, true);
            //�����Ƿ���ʾ��˽���
            if ((m_Employee == null) || (!m_Employee.Code.Equals(m_App.User.Id)))
            {
                m_Employee = new Employee(m_App.User.Id);
                m_Employee.ReInitializeProperties();
            }
            if (m_Employee.DoctorGradeNumber < 1)
            {
                xtraTabControl1.TabPages.Remove(xtraTabPageCheck);
            }
            else
            {
                if (!xtraTabControl1.TabPages.Contains(xtraTabPageCheck))
                {
                    xtraTabControl1.TabPages.Add(xtraTabPageCheck);
                }
            }

        }


        private void GetThreeLevelCheckEmrDoc(DataTable dataTable)
        {
            Employee emp = new Employee(m_App.User.Id);
            emp.ReInitializeProperties();

            DoctorGrade grade = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), emp.Grade);
            if (grade == DoctorGrade.Attending) //����ҽʦ
            {
                string filter = GetEmrCanAudit(grade);
                dataTable.DefaultView.RowFilter = " hassubmit = '4601' " + filter; //�ύ��δ���
            }
            else if (grade == DoctorGrade.Chief || grade == DoctorGrade.AssociateChief)//���κ͸�����
            {
                string filter = GetEmrCanAudit(grade);
                dataTable.DefaultView.RowFilter = " hassubmit in ('4601', '4602') " + filter; //�ύ��δ���,�������
            }
        }

        const string c_GetThreeLevelCheck = @"select count(1) from THREE_LEVEL_CHECK ";
        const string c_Resident = " where resident_id = '{0}' ";
        const string c_Attend = " where resident_id = '{0}' and attend_id = '{1}' ";
        const string c_Chief = " where resident_id = '{0}' and chief_id = '{1}' ";

        /// <summary>
        /// �ж��Ƿ�����˵�Ȩ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true�������Ȩ�� false��û�����Ȩ��</returns>
        private bool GetThreeLevelCheck(string hassubmit, string owner, DoctorGrade grade)
        {
            bool result = true;
            string num = m_App.SqlHelper.ExecuteScalar(c_GetThreeLevelCheck + string.Format(c_Resident, owner), System.Data.CommandType.Text).ToString();
            if (num != "0") //������ָ����Ա����������
            {
                switch (grade)
                {
                    case DoctorGrade.Attending:
                        num = m_App.SqlHelper.ExecuteScalar(c_GetThreeLevelCheck + string.Format(c_Attend, owner, m_App.User.Id), System.Data.CommandType.Text).ToString();
                        if (num == "0")
                        {
                            result = false;
                        }
                        break;
                    case DoctorGrade.AssociateChief:
                    case DoctorGrade.Chief:
                        num = m_App.SqlHelper.ExecuteScalar(c_GetThreeLevelCheck + string.Format(c_Chief, owner, m_App.User.Id), System.Data.CommandType.Text).ToString();
                        if (num == "0")
                        {
                            result = false;
                        }
                        break;
                }
            }
            return result;
        }

        private string GetEmrCanAudit(DoctorGrade grade)
        {
            DataTable dt = gridControlThreeLevelCheck.DataSource as DataTable;
            string id = string.Empty;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string hassubmit = dr["hassubmit"].ToString();
                    string owner = dr["owner"].ToString();
                    if (!GetThreeLevelCheck(hassubmit, owner, grade))
                    {
                        id += dr["id"].ToString() + ",";
                    }
                }
            }
            if (id != string.Empty)
            {
                id = " and id not in (" + id.Trim(',') + ")";
            }
            return id;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        private DataSet GetTaskToday()
        {
            try
            {
                DataSet dataSet = new DataSet();
                SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("@Wardid", SqlDbType.VarChar, 12),
                new SqlParameter("@Deptids", SqlDbType.VarChar, 255),
                new SqlParameter("@UserID", SqlDbType.VarChar,4),
                new SqlParameter("@Time", SqlDbType.VarChar,10)
            };
                sqlParams[0].Value = m_App.User.CurrentWardId.Trim(); // ��������
                sqlParams[1].Value = m_App.User.CurrentDeptId.Trim(); // ���Ҵ���
                sqlParams[2].Value = m_App.User.Id.Trim(); // USERID
                sqlParams[3].Value = this.monthCalendar1.SelectionEnd.ToString("yyyy-MM-dd").Trim(); // ѡ������

                dataSet = m_App.SqlHelper.ExecuteDataSet("usp_GetDoctorTaskInfo", sqlParams, CommandType.StoredProcedure);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private enum TaskType
        {
            /// <summary>
            /// ����
            /// </summary>
            Consultation = 3,
            /// <summary>
            /// ���
            /// </summary>
            ThreeLevelCheck = 4
        }

        #endregion

        /// <summary>
        /// �л�ʱ����ð�ҽ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_EditDateModified(object sender, EventArgs e)
        {
            BindTaskInfo();
        }

        /// <summary>
        /// �����¼Ϊ����ʱ����ʾ��ɫ��������ɫǰ��
        /// </summary>
        private void SetGridControlConsultationColor()
        {
            StyleFormatCondition cn;
            cn = new StyleFormatCondition(FormatConditionEnum.Equal, gridViewConsultation.Columns[m_App.PublicMethod.ConvertProperty("UnitPrice")], null, "����");
            cn.Appearance.BackColor = Color.Red;
            cn.Appearance.ForeColor = Color.White;
            gridViewConsultation.FormatConditions.Add(cn);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            ControlRefresh();
        }

        /// <summary>
        /// �󶨲������ֱ�
        /// </summary>
        private void BindEmrPoint()
        {
            DataTable dt = m_DataManager.GetEmrPoint(m_App.User.Id);
            gridControlPoint.DataSource = dt;
        }


        /// <summary>
        /// ��ȡ��Ժĩ����ҽʦ�����б�
        /// </summary>
        private void GetHistoryInPat()
        {
            string deptID = string.Empty;
            string wardID = string.Empty;
            deptID = m_App.User.CurrentDeptId;
            wardID = m_App.User.CurrentWardId;
            DataTable dt = this.GetHistoryPat(1, deptID, wardID);
            this.gridHistoryInp.DataSource = dt;
        }

        /// <summary>
        /// ��ѯ��Ժȫ������
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="deptID"></param>
        /// <param name="wardID"></param>
        /// <returns></returns>
        private DataTable GetHistoryPat(int queryType, string deptID, string wardID)
        {
            SqlParameter[] sqlParams = new SqlParameter[] { 
                  new SqlParameter("@Wardid", SqlDbType.VarChar, 8),
                  new SqlParameter("@Deptids",SqlDbType.VarChar, 8),
                  new SqlParameter("@TimeFrom",SqlDbType.VarChar,10),
                  new SqlParameter("@TimeTo",SqlDbType.VarChar,10),
                  new SqlParameter("@PatientSN",SqlDbType.VarChar,32),
                  new SqlParameter("@Name",SqlDbType.VarChar,32),
                  new SqlParameter("@QueryType", SqlDbType.Int)
                  };
            sqlParams[0].Value = wardID;
            sqlParams[1].Value = deptID;
            sqlParams[2].Value = this.dateEditFrom.DateTime.ToString("yyyy-MM-dd");
            sqlParams[3].Value = this.dateEditTo.DateTime.ToString("yyyy-MM-dd");
            sqlParams[4].Value = this.textEditPatientSN.Text.Trim();
            sqlParams[5].Value = this.textEditPatientName.Text.Trim();
            sqlParams[6].Value = queryType;
            DataTable dataTable = m_App.SqlHelper.ExecuteDataTable("usp_QueryQuitPatientNoDoctor", sqlParams, CommandType.StoredProcedure);
            return dataTable;
        }

        /// <summary>
        /// ��ȡ�ҷֹܵĲ���
        /// <param name="queryType">QueryType = 0:ȫ������ QueryType = 1:�ֹܻ���</param>
        /// </summary>
        public void InitAllPat(int queryType)
        {

            if (queryType == 1)
            {
                this.myPats = m_DataManager.GetCurrentBedInfos(m_DeptId, m_WardId, QueryType.OWN);
                this.cmbQueryType.SelectedIndex = 0;
            }
            else if (queryType == 0)
            {
                this.myPats = m_DataManager.GetCurrentBedInfos(m_DeptId, m_WardId, QueryType.ALL);
            }

            this.gridMain.DataSource = this.myPats;
            //this.myPats.DefaultView.RowFilter = AddFilter();
            //BindLabText();
        }

        /// <summary>
        /// �����lable��Ϣ
        /// </summary>
        private void BindLabText()
        {

            string sRowFilter = AddFilter();

            if (sRowFilter == "")
                sRowFilter = "1=1 ";

            this.myPats.DefaultView.RowFilter = sRowFilter;
            this.labAllCnt.Text = gridMain.MainView.DataRowCount + " ��";

            this.myPats.DefaultView.RowFilter = sRowFilter + " and HLJB = 'һ������'";
            this.labOneCnt.Text = gridMain.MainView.DataRowCount + " ��";

            this.myPats.DefaultView.RowFilter = sRowFilter + " and HLJB = '��������'";
            this.labTwoCnt.Text = gridMain.MainView.DataRowCount + " ��";

            this.myPats.DefaultView.RowFilter = sRowFilter + " and HLJB = '��������'";
            this.labThreeCnt.Text = gridMain.MainView.DataRowCount + " ��";

            this.myPats.DefaultView.RowFilter = sRowFilter;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetHistoryInPat();
        }

        private void cmbQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbQueryType.SelectedText == "����")
            {
                checkEditShowShift.Checked = false;
                checkEditShowOutHos.Checked = false;
                checkEditOnlyShowShift.Checked = false;
                checkEditOnlyShowOutHos.Checked = false;

                checkEditShowShift.Enabled = true;
                checkEditShowOutHos.Enabled = true;
                checkEditOnlyShowShift.Enabled = true;
                checkEditOnlyShowOutHos.Enabled = true;
                InitAllPat(1);
            }
            else if (this.cmbQueryType.SelectedText == "ȫ��")
            {
                checkEditShowShift.Checked = false;
                checkEditShowOutHos.Checked = false;
                checkEditOnlyShowShift.Checked = false;
                checkEditOnlyShowOutHos.Checked = false;

                checkEditShowShift.Enabled = false;
                checkEditShowOutHos.Enabled = false;
                checkEditOnlyShowShift.Enabled = false;
                checkEditOnlyShowOutHos.Enabled = false;
                InitAllPat(0);
            }
        }

        private string GetSelectedGridView()
        {
            if (gridViewGridWardPat.FocusedRowHandle < 0) return "";

            DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
            if (xtraTabControl1.SelectedTabPage == MyInpTabPage)
            {
                dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
            }
            else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)
            {
                dataRow = m_UserControlAllListIno.GetSelectedGridViewRow();
            }
            else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)
            {
                dataRow = gridViewHistoryInfo.GetDataRow(gridViewHistoryInfo.FocusedRowHandle);
            }
            else if (xtraTabControl1.SelectedTabPage == FailTabPage)
            {
                if (FailTabPage.Controls.Count > 0)
                {
                    UCFail fail = FailTabPage.Controls[0] as UCFail;
                    dataRow = fail.GetSelectedGridViewRow();
                }
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)
            {
                dataRow = gridViewThreeLevelCheck.GetDataRow(gridViewThreeLevelCheck.FocusedRowHandle);
            }
            if (dataRow == null) return "";
            return dataRow["NoOfInpat"].ToString();
        }

        private void gridViewGridWardPat_DoubleClick(object sender, EventArgs e)
        {
            decimal syxh = GetCurrentPat(gridViewGridWardPat);
            if (syxh < 0) return;

            m_App.ChoosePatient(syxh);
            m_App.LoadPlugIn("Yidansoft.Core.MainEmrPad.dll", "Yidansoft.Core.MainEmrPad.MainForm");

        }

        /// <summary>
        /// ��ȡ��ǰ����
        /// </summary>
        /// <returns></returns>
        private decimal GetCurrentPat()
        {
            if (gridViewGridWardPat.FocusedRowHandle < 0)
                return -1;
            else
            {
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                if (dataRow == null) return -1;

                return Convert.ToDecimal(dataRow["NoOfInpat"]);
            }

        }

        private void barButtonItem_PersonalInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
            if (dataRow == null || string.IsNullOrEmpty(dataRow["NoOfInpat"].ToString())) return;
            //to do ���ò���������Ϣ����
            //BasePatientInfo info = new BasePatientInfo(m_App);
            //info.ShowCurrentPatInfo(dataRow["NoOfInpat"].ToString());
            XtraFormPatientInfo patientInfo = new XtraFormPatientInfo(m_App, dataRow["NoOfInpat"].ToString());
            patientInfo.ShowDialog();
        }

        private void barButtonItem_Record_ItemClick(object sender, ItemClickEventArgs e)
        {
            decimal syxh = GetCurrentPat();
            if (syxh < 0) return;
            m_App.ChoosePatient(syxh);
            m_App.LoadPlugIn("Yidansoft.Core.MainEmrPad.dll", "Yidansoft.Core.MainEmrPad.MainForm");
        }

        /// <summary>
        /// ��ʾһ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labOneCnt_Click(object sender, EventArgs e)
        {
            //this.gridMain.DataSource = DataTableSelect(myPats, "HLJB = 'һ������'");
            //this.myPats.DefaultView.RowFilter = AddFilter();

            string sRowFilter = AddFilter();

            if (sRowFilter == "")
                sRowFilter = "1=1 ";

            this.myPats.DefaultView.RowFilter = sRowFilter + " and HLJB = 'һ������'";
            //BindLabText();
        }

        private void labTwoCnt_Click(object sender, EventArgs e)
        {
            string sRowFilter = AddFilter();

            if (sRowFilter == "")
                sRowFilter = "1=1 ";

            this.myPats.DefaultView.RowFilter = sRowFilter + " and HLJB = '��������'";

            //BindLabText();
        }

        private void labThreeCnt_Click(object sender, EventArgs e)
        {
            string sRowFilter = AddFilter();

            if (sRowFilter == "")
                sRowFilter = "1=1 ";
            this.myPats.DefaultView.RowFilter = sRowFilter + " and HLJB = '��������'";

            //BindLabText();
        }

        private void labAllCnt_Click(object sender, EventArgs e)
        {
            //this.gridMain.DataSource = myPats;
            string sRowFilter = AddFilter();

            if (sRowFilter == "")
                sRowFilter = "1=1 ";
            this.myPats.DefaultView.RowFilter = sRowFilter;
        }

        private DataTable DataTableSelect(DataTable pats, string expression)
        {
            DataTable dt = pats.Clone();
            DataRow[] rows = pats.Select(expression);
            foreach (DataRow dr in rows)
            {
                dt.Rows.Add(dr.ItemArray);
            }
            return dt;
        }

        private void popupMenu1_BeforePopup(object sender, CancelEventArgs e)
        {

        }

        private void barManager1_QueryShowPopupMenu(object sender, QueryShowPopupMenuEventArgs e)
        {
            if (e.Control == this.gridMain)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void barButtonItem7_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            decimal syxh = GetCurrentPat();
            if (syxh < 0) return;
            m_App.ChoosePatient(syxh);
            m_App.LoadPlugIn("YidanSoft.Core.DoctorTasks.dll", "YidanSoft.Core.DoctorTasks.InpatientPathForm");
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AppConsult_ItemClick(object sender, ItemClickEventArgs e)
        {
            decimal syxh = GetCurrentPat();
            if (syxh < 0) return;
            m_App.ChoosePatient(syxh);

            FormConsultationApply formApply = new FormConsultationApply(syxh.ToString(), m_App, true);
            formApply.StartPosition = FormStartPosition.CenterParent;
            formApply.ShowDialog();
        }

        #region ��ʱ��

        private void timerLoadData_Tick(object sender, EventArgs e)
        {
        }
        #endregion

        #region �Ի����б�Ŀ���
        /// <summary>
        /// ��ʼ������ǩ���б�
        /// </summary>
        private void InitConsultConfirm()
        {
            //Employee emp = new Employee(m_App.User.Id);
            //emp.ReInitializeProperties();
            //DoctorGrade grade = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), emp.Grade);

            int num1 = 0;
            string info = string.Empty;
            DataTable dt = this.gridControlConsultation.DataSource as DataTable;

            if (dt != null)
            {

                //ֻ�����κ͸����β��ܿ���������嵥
                //if (grade != DoctorGrade.Chief && grade != DoctorGrade.AssociateChief)
                //{
                //    if (dt != null)
                //    {
                //        //dt.DefaultView.RowFilter = " stateid <> '6720' ";
                //    }
                //}
                //else
                //{
                num1 = dt.Select(" consultstatus = '�����' ").Length;
                info += "�����:" + num1;
                //}

                int num2 = dt.Select(" consultstatus = '������' ").Length;
                if (info.Length > 0)
                {
                    info += "  ������:" + num2;
                }
                else
                {
                    info += "������:" + num2;
                }
                navBarGroupConsultation.Caption = "������Ϣ��" + info + "��";
            }

        }

        /// <summary>
        /// ��ȡ����״̬�Ļ�����Ϣ
        /// </summary>
        private void GetConsultionData()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("@Deptids", SqlDbType.VarChar, 255),
                 new SqlParameter("@userid", SqlDbType.VarChar, 255)
                };
                sqlParams[0].Value = m_App.User.CurrentDeptId.Trim(); // ���Ҵ���
                sqlParams[1].Value = m_App.User.Id.Trim(); // ��ǰ��¼�˱���
                dt = m_App.SqlHelper.ExecuteDataTable("usp_GetMyConsultion", sqlParams, CommandType.StoredProcedure);

                DataTable DtFilter = dt.Clone();
                if (dt.Rows.Count > 0)
                {
                    //���ڼ�¼������е����뵥�ţ����ظ��ļ�¼���й���
                    List<string> applySnList = new List<string>();

                    #region �������ܿ��� �����������������
                    DataRow[] Lookrow = dt.Select(
                        string.Format(" stateid in ('6730','6750','6741') and applyuser='{0}' and applydept='{1}'", m_App.User.Id, m_App.User.CurrentDeptId)
                    );
                    for (int i = 0; i < Lookrow.Length; i++)
                    {
                        string applySn = Lookrow[i]["consultapplysn"].ToString();
                        if (!applySnList.Contains(applySn))
                        {
                            applySnList.Add(applySn);
                            DtFilter.ImportRow(Lookrow[i]);
                        }
                    }
                    #endregion

                    #region �����

                    #region ���ﵥ��ָ���������
                    Lookrow = dt.Select(string.Format(" stateid in ('6720') and audituserid='{0}' and applydept='{1}' ", m_App.User.Id, m_App.User.CurrentDeptId));
                    for (int i = 0; i < Lookrow.Length; i++)
                    {
                        string applySn = Lookrow[i]["consultapplysn"].ToString();
                        if (!applySnList.Contains(applySn))
                        {
                            applySnList.Add(applySn);
                            DtFilter.ImportRow(Lookrow[i]);
                        }
                    }
                    DtFilter.AcceptChanges();
                    #endregion

                    #region ���ﵥ��û��ָ�������ʱ�����ݼ�������ж�
                    Employee emp = new Employee(m_App.User.Id);
                    emp.ReInitializeProperties();
                    DoctorGrade grade = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), emp.Grade);
                    AuditLogic audiLogic = new AuditLogic(m_App, m_App.User.Id);

                    //����ҽʦ��������ҽʦ
                    if (grade == DoctorGrade.Chief && grade == DoctorGrade.AssociateChief)
                    {
                        Lookrow = dt.Select(string.Format(" stateid in ('6720') and audituserid is null and applydept='{0}'", m_App.User.CurrentDeptId));
                        for (int i = 0; i < Lookrow.Length; i++)
                        {
                            string applySn = Lookrow[i]["consultapplysn"].ToString();
                            if (!applySnList.Contains(applySn))
                            {
                                applySnList.Add(applySn);
                                DtFilter.ImportRow(Lookrow[i]);
                            }
                        }
                        DtFilter.AcceptChanges();
                    }
                    #endregion

                    #endregion

                    #region �������ܿ��� ������������

                    #region ���ﵥ��ָ����������
                    Lookrow = dt.Select(string.Format(" stateid in ('6730','6741') and EmployeeCode='{0}' and departmentcode='{1}' ", m_App.User.Id, m_App.User.CurrentDeptId));
                    for (int i = 0; i < Lookrow.Length; i++)
                    {
                        string applySn = Lookrow[i]["consultapplysn"].ToString();
                        if (!applySnList.Contains(applySn))
                        {
                            applySnList.Add(applySn);
                            DtFilter.ImportRow(Lookrow[i]);
                        }
                    }
                    #endregion

                    #region ���ﵥ��û��ָ��������ʱ�����ݼ�������ж�

                    Lookrow = dt.Select(string.Format(" stateid in ('6730','6741') and EmployeeCode is null and departmentcode='{0}' and EmployeeLevelID = '{1}'", m_App.User.CurrentDeptId, emp.Grade));
                    for (int i = 0; i < Lookrow.Length; i++)
                    {
                        string applySn = Lookrow[i]["consultapplysn"].ToString();
                        if (!applySnList.Contains(applySn))
                        {
                            applySnList.Add(applySn);
                            DtFilter.ImportRow(Lookrow[i]);
                        }
                    }

                    #endregion

                    #endregion

                    this.gridControlConsultation.DataSource = DtFilter;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #region ԭ����SQL��� ���ڲ���
            //�Ȳ�ѯ��Ҫ���������״̬�Ļ�����Ϣ
            //            string sql = string.Format(@"select A.* from ( SELECT ip.NAME AS inpatientname,
            //                         TO_CHAR(TO_DATE(ca.consulttime, 'yyyy-mm-dd hh24:mi:ss'),
            //                                 'yyyy-mm-dd hh24:mi:ss') AS consulttime,
            //                         cd.NAME AS consultstatus,
            //                         cd1.NAME AS urgencytype,
            //                         ip.NAME || '_' || cd1.NAME || '_' || ca.consulttime AS inpatientinfo,
            //                         ca.stateid,
            //                         ca.noofinpat,
            //                          ca.finishtime,
            //                         ca.consulttypeid,
            //                         ca.applyuser,
            //                         ca.consultapplysn
            //                        FROM consultapply ca
            //                        LEFT OUTER JOIN users u ON u.ID = ca.applyuser
            //                                               AND u.valid = '1'
            //                        LEFT OUTER JOIN inpatient ip ON ip.noofinpat = ca.noofinpat
            //                        LEFT OUTER JOIN categorydetail cd ON cd.categoryid = '67'
            //                                                         AND ca.stateid = cd.ID
            //                        LEFT OUTER JOIN categorydetail cd1 ON cd1.categoryid = '66'
            //                                                          AND cd1.ID = ca.urgencytypeid
            //                       WHERE ca.valid = '1'
            //                         AND ip.status IN ('1501', '1502', '1504', '1505', '1506', '1507')
            //                         and ((ip.outhosdept = '{0}' AND ca.stateid IN ('6720', '6730')) or
            //                         ((INSTR('{0}', u.deptid) > 0 AND
            //                         ca.stateid in ('6770','6720','6730','6750','6741')
            //                         AND EXISTS
            //                       (SELECT *  FROM consultapplydepartment cad
            //                       WHERE cad.consultapplysn = ca.consultapplysn
            //                       AND INSTR('{0}', cad.departmentcode) > 0)))))A  where 
            //                       A.stateid='6730'
            //                        or A.stateid='6770'
            //                         or A.stateid='6720'
            //                         or A.stateid='6750'
            //                         or A.stateid='6741' and to_date(A.finishtime,'yyyy-mm-dd hh24:mi:ss')>trunc(sysdate)-3", m_App.User.CurrentDeptId);
            //            DataTable dtconsult = m_App.SqlHelper.ExecuteDataTable(sql);

            //DataTable dtnew = new DataTable();//���������Ļ�����Ϣ����
            ////������---�������6730 ���ѷ����6750��������ɣ�6741
            //DataRow[] applyuserrows = dtconsult.Select(string.Format(" applyuser='{0}' and stateid in ('6730','6750','6741')", m_App.User.Id));//�������ܿ�����
            #endregion
        }

        private void gridViewConsultation_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo =
                gridViewConsultation.CalcHitInfo(gridControlConsultation.PointToClient(Cursor.Position));
            if (hitInfo.RowHandle >= 0)
            {
                DataRow dr = gridViewConsultation.GetDataRow(hitInfo.RowHandle);
                if (dr != null)
                {
                    string stateid = dr["stateid"].ToString();
                    if (stateid == Convert.ToString((int)ConsultStatus.WaitApprove))//�����
                    {
                        ConsultionConfirm(dr);
                        GetConsultionData();
                        InitConsultConfirm();
                    }
                    else if (stateid == Convert.ToString((int)ConsultStatus.WaitConsultation))//������
                    {
                        WaitConsultaion(dr);
                        GetConsultionData();
                        InitConsultConfirm();
                    }
                    else if (stateid == Convert.ToString((int)ConsultStatus.CancelConsultion))//��ȡ��
                    {
                        CancelConsultion(dr);
                        GetConsultionData();
                        InitConsultConfirm();
                    }
                    else if (stateid == Convert.ToString((int)ConsultStatus.Reject))//�ѷ��
                    {
                        RejectConsultion(dr);
                        GetConsultionData();
                        InitConsultConfirm();
                    }
                    else if (stateid == Convert.ToString((int)ConsultStatus.RecordeComplete))//�������
                    {
                        CompleteConsultion(dr);
                        GetConsultionData();
                        InitConsultConfirm();
                    }
                }
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="dr"></param>
        private void CompleteConsultion(DataRow dr)
        {
            string noOfFirstPage = dr["NoOfInpat"].ToString();
            string consultTypeID = dr["ConsultTypeID"].ToString();
            string consultApplySn = dr["ConsultApplySn"].ToString();

            FormRecordForMultiply formRecord = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
            //���������д������Ϣ�����Ƴ����ڶ�Ӧ�Ļ����¼�����˵��嵥ģ��
            formRecord.StartPosition = FormStartPosition.CenterParent;
            formRecord.ShowDialog();

        }
        /// <summary>
        /// �ѷ��
        /// </summary>
        /// <param name="dr"></param>
        private void RejectConsultion(DataRow dr)
        {
            ////�ѷ����������ҳ��
            //string noOfFirstPage = dr["NoOfInpat"].ToString();
            //string consultTypeID = dr["ConsultTypeID"].ToString();
            //string consultApplySn = dr["ConsultApplySn"].ToString();

            //FormApproveForMultiply formApprove = new FormApproveForMultiply(noOfFirstPage, m_App, consultApplySn);
            ////formApprove.ReadOnlyControl();
            //formApprove.StartPosition = FormStartPosition.CenterParent;
            //formApprove.ShowDialog();
            //decimal syxh = GetCurrentPat();
            //if (syxh < 0) return;
            ////m_App.ChoosePatient(syxh);

            //FormConsultationApply formApply = new FormConsultationApply(syxh.ToString(), m_App, true);
            //formApply.StartPosition = FormStartPosition.CenterParent;
            //formApply.ShowDialog();

            string noOfFirstPage = dr["NoOfInpat"].ToString();
            string consultTypeID = dr["ConsultTypeID"].ToString();
            string consultApplySn = dr["ConsultApplySn"].ToString();

            FormConsultationApply formApply = new FormConsultationApply(noOfFirstPage, m_App, consultApplySn);
            formApply.StartPosition = FormStartPosition.CenterParent;
            formApply.ShowApprove(consultApplySn);
            formApply.ShowDialog();
        }
        /// <summary>
        /// ��ȡ��
        /// </summary>
        /// <param name="dr"></param>
        private void CancelConsultion(DataRow dr)
        {
            string noOfFirstPage = dr["NoOfInpat"].ToString();
            string consultTypeID = dr["ConsultTypeID"].ToString();
            string consultApplySn = dr["ConsultApplySn"].ToString();

            FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
            formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
            formRecrodForMultiply.ShowDialog();
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="dr"></param>
        private void ConsultionConfirm(DataRow dr)
        {
            string noOfFirstPage = dr["NoOfInpat"].ToString();
            string consultTypeID = dr["ConsultTypeID"].ToString();
            string consultApplySn = dr["ConsultApplySn"].ToString();

            FormApproveForMultiply formApprove = new FormApproveForMultiply(noOfFirstPage, m_App, consultApplySn);
            formApprove.StartPosition = FormStartPosition.CenterParent;
            formApprove.ShowDialog();
            //BindTaskInfo();???
        }

        /// <summary>
        /// ������(����˫��ֱ����д������Ϣ)
        /// </summary>
        /// <param name="dr"></param>
        private void WaitConsultaion(DataRow dr)
        {
            string noOfFirstPage = dr["NoOfInpat"].ToString();
            string consultTypeID = dr["ConsultTypeID"].ToString();
            string consultApplySn = dr["ConsultApplySn"].ToString();

            if (dr["applyuser"].ToString() == m_App.User.Id)// ��ǰ��¼���������ˣ����Խ�����˲��� 
            {
                FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
                formRecrodForMultiply.ShowDialog();
            }
            else
            {
                FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
                formRecrodForMultiply.ReadOnlyControl();
                formRecrodForMultiply.ShowDialog();
            }


        }
        #endregion

        /// <summary>
        /// ������������˫�����벡���༭ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewHistory_DoubleClick(object sender, EventArgs e)
        {
            //if (gridViewHistory.FocusedRowHandle < 0)
            //    return;

            //DataRow dataRow = gridViewHistory.GetDataRow(gridViewHistory.FocusedRowHandle);
            //if (dataRow == null)
            //    return;

            //decimal syxh = Convert.ToDecimal(dataRow["noofinpat"]);
            //if (syxh < 0) return;
            //m_App.ChoosePatient(syxh);
            //m_App.LoadPlugIn("Yidansoft.Core.MainEmrPad.dll", "Yidansoft.Core.MainEmrPad.MainForm");

        }

        private void monthCalendar1_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            if (e.Date.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
                e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.White, e.Bounds);
                e.Handled = true;
            }

            else if (e.Date.ToString("yyyy-MM-dd") == monthCalendar1.DateTime.ToString("yyyy-MM-dd"))
            {
                e.Graphics.FillRectangle(Brushes.Green, e.Bounds);
                e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.White, e.Bounds);
                e.Handled = true;
            }
        }

        /// <summary>
        /// ˫������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlThreeLevelCheck_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gridViewThreeLevelCheck.FocusedRowHandle < 0)
                return;

            DataRow dataRow = gridViewThreeLevelCheck.GetDataRow(gridViewThreeLevelCheck.FocusedRowHandle);
            if (dataRow == null)
                return;

            decimal syxh = Convert.ToDecimal(dataRow["noofinpat"]);
            if (syxh < 0) return;
            m_App.CurrentSelectedEmrID = dataRow["id"].ToString();
            m_App.ChoosePatient(syxh);
            m_App.LoadPlugIn("Yidansoft.Core.MainEmrPad.dll", "Yidansoft.Core.MainEmrPad.MainForm");

        }

        #region CheckedChanged�¼�

        private void checkEditShowShift_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditShowShift.Checked)
            {
                checkEditOnlyShowShift.Checked = false;
                checkEditOnlyShowOutHos.Checked = false;
            }
            this.myPats.DefaultView.RowFilter = AddFilter();
            BindLabText();
        }

        private void checkEditShowOutHos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditShowOutHos.Checked)
            {
                checkEditOnlyShowShift.Checked = false;
                checkEditOnlyShowOutHos.Checked = false;
            }
            this.myPats.DefaultView.RowFilter = AddFilter();
            BindLabText();
        }

        private void checkEditOnlyShowShift_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditOnlyShowShift.Checked)
            {
                checkEditShowShift.Checked = false;
                checkEditShowOutHos.Checked = false;
                checkEditOnlyShowOutHos.Checked = false;
            }
            this.myPats.DefaultView.RowFilter = AddFilter();
            BindLabText();
        }

        private void checkEditOnlyShowOutHos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditOnlyShowOutHos.Checked)
            {
                checkEditShowShift.Checked = false;
                checkEditShowOutHos.Checked = false;
                checkEditOnlyShowShift.Checked = false;
            }
            this.myPats.DefaultView.RowFilter = AddFilter();
            BindLabText();
        }

        /// <summary>
        /// ����ɸѡ����
        /// </summary>
        /// <returns></returns>
        private string AddFilter()
        {
            ((DataView)gridMain.MainView.DataSource).RowFilter = "";
            string filter1 = "";
            string filter2 = "";
            string filter = "";
            if (!checkEditShowShift.Checked)
            {
                filter1 = " extra <> '������������' ";
            }
            if (!checkEditShowOutHos.Checked)
            {
                filter2 = " brzt IN (1501, 1504, 1505, 1506, 1507) "; //��Ժ����
            }
            if (checkEditOnlyShowShift.Checked)
            {
                filter1 = " extra = '������������' ";
            }
            if (checkEditOnlyShowOutHos.Checked)
            {
                filter2 = " brzt NOT IN (1500, 1501, 1504, 1505, 1506, 1507) "; //��Ժ����
            }

            if (filter1.Trim() == "" && filter2.Trim() != "")
            {
                filter = filter2;
            }
            if (filter1.Trim() != "" && filter2.Trim() == "")
            {
                filter = filter1;
            }
            if (filter1.Trim() != "" && filter2.Trim() != "")
            {
                filter = filter1 + " AND " + filter2;
            }
            return filter;
        }

        #endregion

        private void gridHistoryInp_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewHistoryInfo.FocusedRowHandle < 0) return;

            DataRowView drv = gridViewHistoryInfo.GetRow(gridViewHistoryInfo.FocusedRowHandle) as DataRowView;
            if (drv != null)
            {
                decimal syxh = GetCurrentPat(gridViewHistoryInfo);
                if (syxh < 0) return;

                if (IsLock(syxh))
                {
                    //�ѹ鵵����˫�����벡������
                    ApplyExamine frm = new ApplyExamine(m_App);
                    frm.SetPatID(GetCurrentPatID(gridViewHistoryInfo));
                    frm.ShowDialog(this.Owner);
                }
                else
                {
                    //δ�鵵����˫�����벡���༭
                    m_App.ChoosePatient(syxh);
                    m_App.LoadPlugIn("Yidansoft.Core.MainEmrPad.dll", "Yidansoft.Core.MainEmrPad.MainForm");
                }
            }
        }

        private decimal GetCurrentPat(GridView gridview)
        {
            if (gridview.FocusedRowHandle < 0)
                return -1;
            else
            {
                DataRow dataRow = gridview.GetDataRow(gridview.FocusedRowHandle);
                if (dataRow == null) return -1;

                return Convert.ToDecimal(dataRow["NoOfInpat"]);
            }

        }

        private string GetCurrentPatID(GridView gridview)
        {
            if (gridview.FocusedRowHandle < 0)
                return "";
            else
            {
                DataRow dataRow = gridview.GetDataRow(gridview.FocusedRowHandle);
                if (dataRow == null) return "";

                return dataRow["PatID"].ToString();
            }
        }

        private bool IsLock(decimal noOfInpat)
        {
            return m_DataManager.EmrDocIsLock(noOfInpat);
        }

        private void textEditPATID_EditValueChanged(object sender, EventArgs e)
        {
            refreshGridView();
        }

        private void textEditPATNAME_EditValueChanged(object sender, EventArgs e)
        {
            refreshGridView();
        }

        private void textEditPATBEDNO_EditValueChanged(object sender, EventArgs e)
        {
            refreshGridView();
        }

        private void refreshGridView()
        {
            string filter = " PATID like '%{0}%' and PatName like '%{1}%' and BedID like '%{2}%' ";
            DataTable dt = gridMain.DataSource as DataTable;
            if (dt != null)
            {
                string rowFilter = AddFilter();
                if (rowFilter != "")
                {
                    dt.DefaultView.RowFilter = rowFilter + " and " + string.Format(filter, textEditPATID.Text.Trim(), textEditPATNAME.Text.Trim(), textEditPATBEDNO.Text.Trim());
                }
                else
                {
                    dt.DefaultView.RowFilter = string.Format(filter, textEditPATID.Text.Trim(), textEditPATNAME.Text.Trim(), textEditPATBEDNO.Text.Trim());
                }
            }
        }

        private void gridMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(labelControlBedNO.Text, labelControlBedNO.Font, new SolidBrush(Color.Black), labelControlBedNO.Location);
            e.Graphics.DrawString(labelControlName.Text, labelControlName.Font, new SolidBrush(Color.Black), labelControlName.Location);
            e.Graphics.DrawString(labelControlPatientSN.Text, labelControlPatientSN.Font, new SolidBrush(Color.Black), labelControlPatientSN.Location);
        }

        /// <summary>
        /// �����ҵĲ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemUndoMyInpatient_ItemClick(object sender, ItemClickEventArgs e)
        {
            decimal syxh = GetCurrentPat();
            if (syxh < 0) return;
            string sqlUndoMyInpatient = " update Doctor_AssignPatient set valid = '0' where valid = '1' and noofinpat = '{0}' ";
            m_App.SqlHelper.ExecuteNoneQuery(string.Format(sqlUndoMyInpatient, syxh), CommandType.Text);
            DataView dt = gridViewGridWardPat.DataSource as DataView;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Table.Rows)
                {
                    if (dr["noofinpat"].ToString() == syxh.ToString())
                    {
                        dt.Table.Rows.Remove(dr);
                        break;
                    }
                }
                dt.Table.AcceptChanges();
            }

            //ˢ��ȫ������
            RefreshAllPatientList();
        }

        private void RefreshAllPatientList()
        {
            if (AllInpTabPage.Controls.Count > 1)
                AllInpTabPage.Controls.Clear();
            InitAllListView();
        }

        private void barButtonItemReportCard_ItemClick(object sender, ItemClickEventArgs e)
        {
            string noofinpat = GetCurrentPat().ToString();
            XtraForm form = (XtraForm)Activator.CreateInstance(
                Type.GetType("YidanSoft.Core.ZymosisReport.ReportCardApply,YidanSoft.Core.ZymosisReport"),
                new object[] { m_App, noofinpat });
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barLargeButtonItemPatientInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            string noOfInpat = GetSelectedGridView();
            if (!string.IsNullOrEmpty(noOfInpat))
            {
                //to do ���ò���������Ϣ����
                //BasePatientInfo info = new BasePatientInfo(m_App);
                //info.ShowCurrentPatInfo(dataRow["NoOfInpat"].ToString());
                XtraFormPatientInfo patientInfo = new XtraFormPatientInfo(m_App, noOfInpat);
                patientInfo.ShowDialog();
            }
        }

        private void barLargeButtonItemAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            if (AllInpTabPage.Controls.Count > 1)
                return;
            InitAllListView();
        }

        private void barLargeButtonItemConsultation_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_App.LoadPlugIn("YidanSoft.Core.Consultation.dll", "YidanSoft.Core.Consultation.ConsultationFormStartUp");
        }

        private void barLargeButtonItemEmrApply_ItemClick(object sender, ItemClickEventArgs e)
        {
            ApplyExamine frm = new ApplyExamine(m_App);
            frm.ShowDialog(this.Owner);
        }

        private void barLargeButtonItemReportCard_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_App.LoadPlugIn("YidanSoft.Core.ZymosisReport.dll", "YidanSoft.Core.ZymosisReport.MainForm");
        }

        private void barLargeButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SetWaitDialogCaption("����ˢ�£����Ժ�");
                checkEditShowShift.Checked = false;
                checkEditShowOutHos.Checked = false;
                checkEditOnlyShowShift.Checked = false;
                checkEditOnlyShowOutHos.Checked = false;

                checkEditShowShift.Enabled = true;
                checkEditShowOutHos.Enabled = true;
                checkEditOnlyShowShift.Enabled = true;
                checkEditOnlyShowOutHos.Enabled = true;
                ControlRefresh();
            }
            catch (Exception ex)
            { }
            finally
            {
                HideWaitDialog();
            }
        }

        private void gridControlPoint_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gridViewEmrPoint.FocusedRowHandle < 0)
                return;

            DataRow dataRow = gridViewEmrPoint.GetDataRow(gridViewEmrPoint.FocusedRowHandle);
            if (dataRow == null)
                return;

            decimal syxh = Convert.ToDecimal(dataRow["noofinpat"]);
            if (syxh < 0) return;
            m_App.CurrentSelectedEmrID = dataRow["recorddetailid"].ToString().Trim();
            m_App.ChoosePatient(syxh);
            m_App.LoadPlugIn("Yidansoft.Core.MainEmrPad.dll", "Yidansoft.Core.MainEmrPad.MainForm");
        }
    }
}
