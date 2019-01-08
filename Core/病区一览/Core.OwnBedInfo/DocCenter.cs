using Consultation.NEW;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DrectSoft.Common;
using DrectSoft.Common.Ctrs.DLG;
using DrectSoft.Common.Eop;
using DrectSoft.Core.Consultation;
using DrectSoft.Core.QCDeptReport;
using DrectSoft.DSSqlHelper;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Service;
using MedicalRecordManage.UCControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace DrectSoft.Core.OwnBedInfo
{
    /// <summary>
    /// ҽ������վ����
    /// 
    /// </summary>
    public partial class DocCenter : DevExpress.XtraEditors.XtraForm
    {
        public delegate void del_ShowVigals(string noofinpat);
        public event del_ShowVigals ShowVigalsHandle;
        public void OnShowVigals(string noofinpat)
        {
            if (ShowVigalsHandle != null)
            {
                ShowVigalsHandle(noofinpat);
            }
        }
        NursingRecordForm nursingRecordForm;

        private IEmrHost m_App;
        /// <summary>
        /// �����б�
        /// </summary>
        private UserControlAllListBedInfo m_UserControlAllListIno;

        private UCFail m_UCFail;
        private UCTran m_UCTran;
        private MyMedicalRecordList mymedicalrecordlist;
        private DrectSoft.Emr.QcManager.QualityMedicalRecord qualityDept;
        private ReplenishPatRec repl;

        private DataTable myPats;

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

        private UCConsultationForDocCenter m_UCConsultationForDocCenter;

        #region constructor
        public DocCenter()
            : this(null)
        {
            if (!this.DesignMode)
            {
                DS_SqlHelper.CreateSqlHelper();
            }
        }

        public DocCenter(IEmrHost app)
        {
            m_WaitDialog = new WaitDialogForm("���ڴ����û�����...", "���Ժ�");

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;//����������ؼ��첽���������
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            DS_SqlHelper.CreateSqlHelper();
            monthCalendar1.DateTime = DateTime.Now;
            monthCalendar1.TodayButton.Text = "����";
            m_App = app;
            m_DeptId = m_App.User.CurrentDeptId;
            m_WardId = m_App.User.CurrentWardId;
            m_DataManager = new DataManager(m_App, m_DeptId, m_WardId);
            AddUCConsultationForDocCenter();
            monthCalendar1.EditDateModified += new EventHandler(monthCalendar1_EditDateModified);
            gridViewGridWardPat.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(gridViewGridWardPat_CustomDrawRowIndicator);
            BindTaskInfo();//������ؾͼ��س�Ҫ�����Ļ�����Ϣ add 2012��6��14�� 09:52:02

            //edit by Yanqiao.Cai 2012-11-08
            //CopntrolModPatient();


            //Application.AddMessageFilter(new DrectSoft.FrameWork.Filter.KeyMessageFilter());
            textEditPATID.Focus();


        }
        /// <summary>
        /// ��ʾ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridViewGridWardPat_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            DS_Common.AutoIndex(e);
        }
        #endregion

        private void FormUserControlShow_Load(object sender, EventArgs e)
        {
            try
            {

                DS_Common.SetWaitDialogCaption(m_WaitDialog, "���ڶ�ȡ��������...");
                ucTimeQcInfo1.App = m_App;//ʹ�����µĲ������ѹ���xlb2013-01-11
                BtnReset();
                this.cmbQueryType.SelectedIndex = 0;
                InitConfig();
                SetButtonState();
                InitializeImage();
                InitMyInpatient();
                InitUndoMyInpatient();
                SetPageCheckVisible();
                DS_Common.HideWaitDialog(m_WaitDialog);
                groupQCTiXing.Visible = false;
                MedicalRecordManage.UI.MedicalRecordManageForm fm = new MedicalRecordManage.UI.MedicalRecordManageForm();
                fm.Run(m_App);
                if (!isdeptManager(m_App.User.CurrentDeptId, m_App.User.Id))
                {
                    DeptQc.PageVisible = false;
                }
                //if (m_App.CurrentPatientInfo != null)
                //{
                //    m_App.ChoosePatient(decimal.Parse(m_App.CurrentPatientInfo.NoOfFirstPage.ToString()));
                //    m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private string m_ConfigKeyBase = string.Empty;
        private string m_ConfigKeyHis = string.Empty;
        private string m_ConfigKeySimple = string.Empty;
        private string m_ConfigClinical = string.Empty;

        private void InitConfig()
        {
            try
            {
                ///������Ϣά�����ܣ��Ƿ��ֶ�ά����1-�ǣ�0-��
                m_ConfigKeyBase = GetConfigValueByKey("ManualMaintainBasicInfo");
                ///��his�鲡����Ϣ���ܣ�1-�ǣ�0-��
                m_ConfigKeyHis = GetConfigValueByKey("GetInpatientForHis");
                ///��湤��վ
                m_ConfigKeySimple = GetConfigValueByKey("SimpleDoctorCentor");
                m_ConfigClinical = GetConfigValueByKey("IsShowClinicalButton");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ������Ժ����Ϊ��Ժʱ�仹��������ڣ��������ã�
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2013-03-14</date>
        public void SetInHosOrInWardDate()
        {
            try
            {
                //��ȡ��Ժ���ڣ����ã� edit by cyq 2013-03-13
                string config = DS_SqlService.GetConfigValueByKey("EmrInputConfig");
                if (!string.IsNullOrEmpty(config))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(config);
                    XmlNodeList nodeList = doc.GetElementsByTagName("InHosTimeType");
                    if (null != nodeList && nodeList.Count > 0)
                    {
                        string cfgValue = null == nodeList[0].InnerText ? "" : nodeList[0].InnerText.Trim();
                        if (cfgValue == "1")
                        {//���
                            gridViewGridWardPat.Columns[9].FieldName = "INWARDDATE";//�ҵĲ���
                            //gridViewHistoryInfo.Columns[6].FieldName = "INWARDDATE";//������ʷ����  xll ������ʷ����ȥ��Ժʱ�� 20130820
                        }
                        else
                        {//��Ժ
                            gridViewGridWardPat.Columns[9].FieldName = "ADMITDATE";//�ҵĲ���
                            //gridViewHistoryInfo.Columns[6].FieldName = "ADMITDATE";//������ʷ���� xll ������ʷ����ȥ��Ժʱ�� 20130820
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add by wwj 2013-03-07 �����Ҳ������б�
        /// </summary>
        private void AddUCConsultationForDocCenter()
        {
            try
            {
                m_UCConsultationForDocCenter = new UCConsultationForDocCenter(m_App, navBarGroupConsultation);
                navBarGroupControlContainer4.Controls.Add(m_UCConsultationForDocCenter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// �õ�������Ϣ  wyt 2012��8��27��
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfigValueByKey(string key)
        {

            string sql1 = " select * from appcfg where configkey = '" + key + "'  ";
            DataTable dt = m_App.SqlHelper.ExecuteDataTable(sql1, CommandType.Text);
            string config = string.Empty;
            if (dt != null && dt.Rows.Count > 0)//add by xlb 2012-12-25
            {
                config = dt.Rows[0]["value"].ToString();
            }
            return config;
        }

        /// <summary>
        /// �ʿ����ѵ�����
        /// </summary>
        private void GetQCTiXong()
        {
            //DataTable dtQC = m_DataManager.GetQCTiXong();
            //gridControlQCTiXing.DataSource = dtQC;��ʽ�����ʺ͵���ʹ�� add by ywk 2012��8��14�� 13:28:54
        }

        /// <summary>
        /// �жϡ������ҵĲ��ˡ����ܵ���ʾ���
        /// edit by xlb 2012-12-25
        /// ���÷����õ�������Ϣ
        /// </summary>
        private void InitUndoMyInpatient()
        {
            //string config = m_App.SqlHelper.ExecuteScalar("select value from appcfg where configkey = 'IsOpenSetMyInpatient'", CommandType.Text).ToString();
            //string IsOpenSetMyInpatient = "IsOpenSetMyInpatient";
            string config = GetConfigValueByKey("IsOpenSetMyInpatient");
            NeedUndoMyInpatient = !config.Equals("0");
            barButtonItemUndoMyInpatient.Visibility = NeedUndoMyInpatient.Equals(true) ? BarItemVisibility.Always : BarItemVisibility.Never;
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
            else
            {
                m_UserControlAllListIno.RefreshInpatientList();
            }
        }

        /// <summary>
        /// ���ˢ��ʱ,��������ʾ��usercontrol�ؼ�ˢ��
        /// </summary>
        public void ControlRefresh()
        {
            if (this.xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĲ���
            {
                InitMyInpatient();
            }
            else if (this.xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
            {
                if (AllInpTabPage.Controls.Count > 1)
                    AllInpTabPage.Controls.Clear();
                InitAllListView();
            }
            else if (this.xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//��ʷ���˲�ѯ
            {
                Reset();
                btnQuery_Click(null, null);
            }
            else if (this.xtraTabControl1.SelectedTabPage == FailTabPage)//��Ժδ�鵵
            {
                if (FailTabPage.Controls.Count > 0)
                {
                    Reset();
                    m_UCFail.refreshQuery();
                    return;
                }
                m_UCFail = new QCDeptReport.UCFail(m_App);
                m_UCFail.Dock = DockStyle.Fill;
                FailTabPage.Controls.Add(m_UCFail);
            }
            else if (this.xtraTabControl1.SelectedTabPage == FailTabPage)// ת�Ʋ��˲鿴
            {

                if (paintTran.Controls.Count > 0)
                {
                    Reset();
                    m_UCTran.refreshQuery();
                    return;
                }
                m_UCTran = new QCDeptReport.UCTran(m_App);
                m_UCTran.Dock = DockStyle.Fill;
                paintTran.Controls.Add(m_UCTran);
            }
            else if (this.xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
            {
                BindEmrPoint();
            }
            else if (this.xtraTabControl1.SelectedTabPage == Replenish)//��д����
            {
                //Replenish.Controls.Clear();
                if (Replenish.Controls.Count == 0)
                {
                    repl = new ReplenishPatRec();
                    repl.Dock = DockStyle.Fill;
                    Replenish.Controls.Add(repl);
                }
                repl.LoadData(m_App);
            }
            else if (this.xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
            {
                DataTable dt = m_DataManager.GetThreeLevelCheckList(m_App.User.CurrentDeptId);
                this.gridControlThreeLevelCheck.DataSource = dt;
                GetThreeLevelCheckEmrDoc(dt);
            }
            else if (this.xtraTabControl1.SelectedTabPage == myinplist)//����
            {
                mymedicalrecordlist = new MyMedicalRecordList(m_App);
                mymedicalrecordlist.Dock = DockStyle.Fill;
                myinplist.Controls.Add(mymedicalrecordlist);
            }
            else if (this.xtraTabControl1.SelectedTabPage == DeptQc)//�����ʿ�
            {
                qualityDept = new DrectSoft.Emr.QcManager.QualityMedicalRecord(m_App);
                qualityDept.IsDeptQc = true;
                qualityDept.Dock = DockStyle.Fill;
                DeptQc.Controls.Add(qualityDept);
            }
            BindTaskInfo();
        }

        bool m_IsLoadMyInpTabPage = false;
        bool m_IsLoadAllInpTabPage = false;
        bool m_IsLoadHistoryInpTabPage = false;
        bool m_IsLoadFailTabPage = false;
        bool m_IsLoadXtraTabPagePoint = false;
        bool m_IsLoadReplenish = false;
        bool m_IsLoadXtraTabPageCheck = false;
        bool m_myinplist = false;
        bool m_DeptQc = false;
        bool m_IsLoadTranPage = false;

        /// <summary>
        /// ���ˢ��ʱ,��������ʾ��usercontrol�ؼ�ˢ��
        /// </summary>
        public void ControlRefreshForSelectedPageChanged()
        {
            try
            {
                if (!m_IsLoadMyInpTabPage && this.xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĲ���
                {
                    m_IsLoadMyInpTabPage = true;
                    InitMyInpatient();
                }
                else if (!m_IsLoadAllInpTabPage && this.xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    m_IsLoadAllInpTabPage = true;
                    if (AllInpTabPage.Controls.Count > 1)
                        AllInpTabPage.Controls.Clear();
                    InitAllListView();
                }
                else if (!m_IsLoadHistoryInpTabPage && this.xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//��ʷ���˲�ѯ
                {
                    m_IsLoadHistoryInpTabPage = true;
                    Reset();
                    btnQuery_Click(null, null);
                }

                else if (!m_IsLoadFailTabPage && this.xtraTabControl1.SelectedTabPage == FailTabPage)//��Ժδ�鵵
                {
                    m_IsLoadFailTabPage = true;
                    if (FailTabPage.Controls.Count > 0)
                    {
                        Reset();
                        m_UCFail.refreshQuery();
                        return;
                    }
                    m_UCFail = new QCDeptReport.UCFail(m_App);
                    m_UCFail.Dock = DockStyle.Fill;
                    FailTabPage.Controls.Add(m_UCFail);
                }
                else if (!m_IsLoadTranPage && this.xtraTabControl1.SelectedTabPage == paintTran)//��Ժδ�鵵
                {
                    m_IsLoadTranPage = true;
                    if (paintTran.Controls.Count > 0)
                    {
                        Reset();
                        m_UCTran.refreshQuery();
                        return;
                    }
                    m_UCTran = new QCDeptReport.UCTran(m_App);
                    m_UCTran.Dock = DockStyle.Fill;
                    paintTran.Controls.Add(m_UCTran);
                }
                else if (!m_IsLoadXtraTabPagePoint && this.xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                {
                    m_IsLoadXtraTabPagePoint = true;
                    BindEmrPoint();
                }
                else if (!m_IsLoadReplenish && this.xtraTabControl1.SelectedTabPage == Replenish)//��д����
                {
                    m_IsLoadReplenish = true;
                    if (Replenish.Controls.Count == 0)
                    {
                        repl = new ReplenishPatRec();
                        repl.Dock = DockStyle.Fill;
                        Replenish.Controls.Add(repl);
                    }
                    repl.LoadData(m_App);
                }
                else if (!m_IsLoadXtraTabPageCheck && this.xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                {
                    m_IsLoadXtraTabPageCheck = true;
                    DataTable dt = m_DataManager.GetThreeLevelCheckList(m_App.User.CurrentDeptId);
                    this.gridControlThreeLevelCheck.DataSource = dt;
                    GetThreeLevelCheckEmrDoc(dt);
                }
                else if (!m_myinplist && this.xtraTabControl1.SelectedTabPage == myinplist)//������ѯ
                {
                    m_myinplist = true;
                    if (myinplist.Controls.Count > 0)
                    {
                        Reset();
                        mymedicalrecordlist.refreshQuery();
                        return;
                    }
                    mymedicalrecordlist = new MyMedicalRecordList(m_App);
                    mymedicalrecordlist.Dock = DockStyle.Fill;
                    myinplist.Controls.Add(mymedicalrecordlist);
                }
                else if (!m_DeptQc && this.xtraTabControl1.SelectedTabPage == DeptQc)//�����ʿ�
                {
                    m_DeptQc = true;
                    if (DeptQc.Controls.Count > 0)
                    {
                        Reset();
                        qualityDept.refreshQuery();
                        return;
                    }
                    qualityDept = new DrectSoft.Emr.QcManager.QualityMedicalRecord(m_App);
                    qualityDept.IsDeptQc = true;
                    qualityDept.Dock = DockStyle.Fill;
                    DeptQc.Controls.Add(qualityDept);
                }
            }
            catch (Exception)
            {
                throw;
            }
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
                ImageComboBoxItem ImageComboItemUnknow = new ImageComboBoxItem("δ֪", "3", 1);
                repItemImageComboBoxBrxb.Items.Add(ImageComboItemMale);
                repItemImageComboBoxBrxb.Items.Add(ImageComboItemFemale);
                repItemImageComboBoxBrxb.Items.Add(ImageComboItemUnknow);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ʼ��ͼƬ
        /// </summary>
        private void InitializeImage2()
        {
            try
            {
                ImageHelper.GetImageListIllness();
                imageListCustomwzjb = ImageHelper.GetImageListIllness();
                repositoryItemImageWZJB.SmallImages = imageListCustomwzjb;
                DataTable dt = null;
                if ((dt == null) || (dt.Rows.Count <= 0))
                {
                    ImageComboBoxItem item1 = new ImageComboBoxItem("һ�㲡��", "0", 0);
                    ImageComboBoxItem item2 = new ImageComboBoxItem("Σ�ز���", "1", 1);
                    ImageComboBoxItem item3 = new ImageComboBoxItem("���ز���", "2", 2);
                    repositoryItemImageWZJB.Items.AddRange(new ImageComboBoxItem[] { item1, item2, item3 });
                }
                else
                {
                    ImageComboBoxItem[] imageCombo = new ImageComboBoxItem[dt.Rows.Count];
                    for (int index = 0; index < imageCombo.Length; index++)
                    {
                        ImageComboBoxItem item = new ImageComboBoxItem(dt.Rows[index]["name"].ToString().Trim(), dt.Rows[index]["mxdm"].ToString().Trim(), Convert.ToInt16(dt.Rows[index]["mxdm"].ToString().Trim()));
                        imageCombo[index] = item;
                    }
                    repositoryItemImageWZJB.Items.AddRange(imageCombo);
                }
                imageListBrxb = ImageHelper.GetImageListBrxb();
                repositoryItemImageXB.SmallImages = imageListBrxb;
                ImageComboBoxItem ImageComboItemMale = new ImageComboBoxItem("��", "1", 1);
                ImageComboBoxItem ImageComboItemFemale = new ImageComboBoxItem("Ů", "2", 0);
                ImageComboBoxItem ImageComboItemUnknow = new ImageComboBoxItem("δ֪", "3", 1);
                repItemImageComboBoxBrxb.Items.Add(ImageComboItemMale);
                repItemImageComboBoxBrxb.Items.Add(ImageComboItemFemale);
                repItemImageComboBoxBrxb.Items.Add(ImageComboItemUnknow);
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
            m_App.LoadPlugIn("DrectSoft.Core.DoctorTasks.dll", "DrectSoft.Core.DoctorTasks.FormInpatientOrder");
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
            m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
        }
        #endregion


        #region ��������

        /// <summary>
        /// ���Ҳ๤�����е�����
        /// edit by xlb 2013-01-11
        /// ʹ����QCTimeLimit��ģ��
        /// </summary>
        private void BindTaskInfo()
        {
            try//������ݲ�ͣˢ�£�ҪTRY   add by ywk 2012��9��27�� 11:19:55 
            {
                //�󶨻�����Ϣ
                m_UCConsultationForDocCenter.BindConsultationThread();

                //��ʱ����Ϣ
                ucTimeQcInfo1.App = m_App;
                ucTimeQcInfo1.CheckDoctorLimitThread(m_App.User.DoctorId);
            }
            catch (Exception ex)
            {
                //throw ex;
                //MyMessageBox.Show(1, ex);
            }

            //GetQCTiXong();
        }

        private void SetPageCheckVisible()
        {
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

            if (emp.Grade.Trim() != "")
            {
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
            else
            {
                xtraTabPageCheck.PageVisible = false;
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
            Consultation = 0
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
        /// tabҳ�л��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                //add by Yanqiao.Cai 2012-11-08
                SetButtonState();

                ControlRefreshForSelectedPageChanged();//Add by wwj 2013-06-04 ��֤���л�Pageʱ�����ظ���ȡ����
                Reset();
                FocusControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FocusControl()
        {
            if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
            {
                this.textEditPATBEDNO.Focus();
            }
            else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
            {
                m_UserControlAllListIno.FocusFirstControl();
            }
            else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
            {
                textEditPatientSN.Focus();
            }
            else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
            {
                m_UCFail.FocusFirstControl();
                FailTabPage.Controls[0].Focus();
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
            { }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
            { }
            else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
            {
                //repl.FocusFirstControl();
                Replenish.Controls[0].Focus();
            }
        }

        /// <summary>
        /// ���ù�����ͼƬ��ť���Ҽ���ť����ʾ������
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-08</date>
        /// </summary>
        private void SetButtonState()
        {
            try
            {
                string configKeyBase = m_ConfigKeyBase;
                string configKeyHis = m_ConfigKeyHis;
                string configKeySimple = m_ConfigKeySimple;
                string configClinical = m_ConfigClinical;

                #region ������Ϣά�� &&��His��ѯ������Ϣ
                if (configKeyBase == "1")
                {///�ֶ�ά��
                    //������Ϣ - ������
                    this.barLargeButtonItemPatientInfo.Visibility = BarItemVisibility.Always;
                    //������Ϣ - �Ҽ�
                    this.barButtonItem_PersonalInfo.Visibility = BarItemVisibility.Always;
                    //��Ժ�Ǽ� - ������ edit by Ukey 2016-11-14 9:54 ��ʾ��Ժ�Ǽ�
                    this.barLargeButtonItemInHosLogin.Visibility = BarItemVisibility.Always;
                    //��Ժ�Ǽ� - �Ҽ�
                    this.barButtonItem_inHosLogin.Visibility = BarItemVisibility.Always;
                    //�༭������Ϣ - ������
                    this.barLargeButtonItemPatiEdit.Visibility = BarItemVisibility.Always;
                    //�༭������Ϣ - �Ҽ�
                    this.barButtonItemChange.Visibility = BarItemVisibility.Always;
                    //���˳�Ժ - ������ edit by Ukey 2016-11-14 9:54 ��ʾ���˳�Ժ
                    this.barLargeButtonItem2.Visibility = BarItemVisibility.Always;
                    //���˳�Ժ - �Ҽ�
                    this.barButtonItemOut.Visibility = BarItemVisibility.Always;
                }
                else
                {///���ֶ�ά��
                    if (configKeyHis == "1" && configKeySimple != "1")
                    {///��His��ѯ������Ϣ && ���Ǽ�湤��վ
                        //������Ϣ - ������
                        //this.barLargeButtonItemPatientInfo.Visibility = BarItemVisibility.Always;
                        //������Ϣ - �Ҽ�
                        this.barButtonItem_PersonalInfo.Visibility = BarItemVisibility.Always;
                    }
                    else
                    {
                        //������Ϣ - ������
                        //this.barLargeButtonItemPatientInfo.Visibility = BarItemVisibility.Never;
                        //������Ϣ - �Ҽ�
                        this.barButtonItem_PersonalInfo.Visibility = BarItemVisibility.Never;
                    }
                    //��Ժ�Ǽ� - ������
                    this.barLargeButtonItemInHosLogin.Visibility = BarItemVisibility.Always;
                    //��Ժ�Ǽ� - �Ҽ�
                    this.barButtonItem_inHosLogin.Visibility = BarItemVisibility.Never;
                    //�༭������Ϣ - ������
                    this.barLargeButtonItemPatiEdit.Visibility = BarItemVisibility.Never;
                    //�༭������Ϣ - �Ҽ�
                    this.barButtonItemChange.Visibility = BarItemVisibility.Never;
                    //���˳�Ժ - ������
                    this.barLargeButtonItem2.Visibility = BarItemVisibility.Never;
                    //���˳�Ժ - �Ҽ�
                    this.barButtonItemOut.Visibility = BarItemVisibility.Never;
                }
                #endregion

                #region ��湤��վ
                if (configKeySimple == "1")
                {///��湤��վ
                    //�������� - ������
                    this.barLargeButtonItemConsultationApply.Visibility = BarItemVisibility.Never;
                    //�������� - �Ҽ�
                    this.btn_AppConsult.Visibility = BarItemVisibility.Never;
                    //Ժ�ڻ��� - ������
                    this.barLargeButtonItemConsultation.Visibility = BarItemVisibility.Never;
                    //Ժ�ڻ��� - �Ҽ�
                    this.barButtonItem_consultation.Visibility = BarItemVisibility.Never;
                    //�������� - ������
                    this.barLargeButtonItemEmrApply.Visibility = BarItemVisibility.Never;
                    //�������� - �Ҽ�
                    this.barButtonItem_emrApply.Visibility = BarItemVisibility.Never;
                    //��Ⱦ���ϱ� - ������
                    this.barLargeButtonItemReportCard.Visibility = BarItemVisibility.Never;
                    //��Ⱦ���ϱ� - �Ҽ�
                    this.barButtonItemReportCard.Visibility = BarItemVisibility.Never;
                    //�ٴ�·�� - ������
                    this.barLargeButtonItemClinical.Visibility = BarItemVisibility.Never;
                    //�ٴ�·�� - �Ҽ�
                    this.barButtonItem7.Visibility = BarItemVisibility.Never;
                    //�������� - �Ҳ�ģ��
                    this.dockPanel1.Visibility = DockVisibility.Hidden;
                    //������� - tabҳ
                    this.xtraTabPageCheck.PageVisible = false;
                    //��������- tabҳ
                    this.xtraTabPagePoint.PageVisible = false;
                }
                else
                {
                    //�������� - ������
                    this.barLargeButtonItemConsultationApply.Visibility = BarItemVisibility.Always;
                    //�������� - �Ҽ�
                    this.btn_AppConsult.Visibility = BarItemVisibility.Always;
                    //Ժ�ڻ��� - ������
                    this.barLargeButtonItemConsultation.Visibility = BarItemVisibility.Always;
                    //Ժ�ڻ��� - �Ҽ�
                    this.barButtonItem_consultation.Visibility = BarItemVisibility.Always;
                    //�������� - ������
                    this.barLargeButtonItemEmrApply.Visibility = BarItemVisibility.Always;
                    //�������� - �Ҽ�
                    this.barButtonItem_emrApply.Visibility = BarItemVisibility.Always;
                    //��Ⱦ���ϱ� - ������
                    this.barLargeButtonItemReportCard.Visibility = BarItemVisibility.Always;
                    //��Ⱦ���ϱ� - �Ҽ�
                    this.barButtonItemReportCard.Visibility = BarItemVisibility.Always;
                    //�ٴ�·�� - ������
                    this.barLargeButtonItemClinical.Visibility = BarItemVisibility.Always;
                    //�ٴ�·�� - �Ҽ�
                    this.barButtonItem7.Visibility = BarItemVisibility.Always;
                    //�������� - �Ҳ�ģ��
                    this.dockPanel1.Visibility = DockVisibility.Visible;
                    //������� - tabҳ
                    this.xtraTabPageCheck.PageVisible = true;
                    //��������- tabҳ
                    this.xtraTabPagePoint.PageVisible = true;
                }
                #endregion

                #region tabҳ�л��Ĳ�ͬ��ʾ
                //�ر�ע�����Ҽ���ťΪ �ҵĻ���Tabҳר�ã�����Tabҳ������ʾ
                if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                {
                    //�����ҵĲ��� - ������
                    this.barLargeButtonItemCancelMyPati.Visibility = BarItemVisibility.Always;
                    //�����ҵĲ��� - �Ҽ�
                    this.barButtonItemUndoMyInpatient.Visibility = BarItemVisibility.Always;
                    //��Ϊ�ҵĲ��� - ������
                    this.barLargeButtonItemSetMyPati.Visibility = BarItemVisibility.Never;
                    //��Ϊ�ҵĲ��� - �Ҽ� ---> ��ȫ������ҳ�����
                    //�趨Ӥ�� - ������
                    this.barLargeButtonItemSetBaby.Visibility = BarItemVisibility.Always;
                    //�趨Ӥ�� - �Ҽ�
                    this.barButtonItemBaby.Visibility = BarItemVisibility.Always;
                    //����¼��- ������
                    this.barLargeButtonItemDocumentWrite.Visibility = BarItemVisibility.Always;
                    //����¼��- �Ҽ�
                    this.barButtonItem_Record.Visibility = BarItemVisibility.Always;
                }
                else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    //�����ҵĲ��� - ������
                    this.barLargeButtonItemCancelMyPati.Visibility = BarItemVisibility.Never;
                    //�����ҵĲ��� - �Ҽ�
                    this.barButtonItemUndoMyInpatient.Visibility = BarItemVisibility.Never;
                    //�����������������Ƿ���ʾΪ����������Ϊ�ҵĲ��˰�ť Add by xlb 2013-06-09
                    if (DS_SqlService.GetConfigValueByKey("IsOpenSetMyInpatient").Equals("1"))
                    {
                        //��Ϊ�ҵĲ��� - ������
                        this.barLargeButtonItemSetMyPati.Visibility = BarItemVisibility.Always;
                    }
                    else
                    {
                        this.barLargeButtonItemSetMyPati.Visibility = BarItemVisibility.Never;
                    }
                    //��Ϊ�ҵĲ��� - �Ҽ� ---> ��ȫ������ҳ�����
                    //�趨Ӥ�� - ������
                    this.barLargeButtonItemSetBaby.Visibility = BarItemVisibility.Always;
                    //�趨Ӥ�� - �Ҽ�
                    this.barButtonItemBaby.Visibility = BarItemVisibility.Always;
                    //����¼��- ������
                    this.barLargeButtonItemDocumentWrite.Visibility = BarItemVisibility.Always;
                    //����¼��- �Ҽ�
                    this.barButtonItem_Record.Visibility = BarItemVisibility.Never;
                }
                else if (xtraTabControl1.SelectedTabPage == FailTabPage) //�ѳ�Ժδ��ɲ�ѯ
                {
                    //�༭������Ϣ - ������
                    this.barLargeButtonItemPatiEdit.Visibility = BarItemVisibility.Always;
                    //�༭������Ϣ - �Ҽ�
                    this.barButtonItemChange.Visibility = BarItemVisibility.Always;
                    //���˳�Ժ - ������
                    this.barLargeButtonItem2.Visibility = BarItemVisibility.Never;
                    //���˳�Ժ - �Ҽ�
                    this.barButtonItemOut.Visibility = BarItemVisibility.Never;
                    //�����ҵĲ��� - ������
                    this.barLargeButtonItemCancelMyPati.Visibility = BarItemVisibility.Never;
                    //�����ҵĲ��� - �Ҽ�
                    this.barButtonItemUndoMyInpatient.Visibility = BarItemVisibility.Never;
                    //��Ϊ�ҵĲ��� - ������
                    this.barLargeButtonItemSetMyPati.Visibility = BarItemVisibility.Never;
                    //��Ϊ�ҵĲ��� - �Ҽ� ---> ��ȫ������ҳ�����
                    //�趨Ӥ�� - ������
                    this.barLargeButtonItemSetBaby.Visibility = BarItemVisibility.Never;
                    //�趨Ӥ�� - �Ҽ�
                    this.barButtonItemBaby.Visibility = BarItemVisibility.Never;
                    //����¼��- ������
                    this.barLargeButtonItemDocumentWrite.Visibility = BarItemVisibility.Never;
                    //����¼��- �Ҽ�
                    this.barButtonItem_Record.Visibility = BarItemVisibility.Never;
                    //��Ժ�Ǽ� - ������
                    this.barLargeButtonItemInHosLogin.Visibility = BarItemVisibility.Always;
                    //��Ժ�Ǽ� - �Ҽ�
                    this.barButtonItem_inHosLogin.Visibility = BarItemVisibility.Never;
                    //�������� - ������
                    this.barLargeButtonItemConsultationApply.Visibility = BarItemVisibility.Never;
                    //�������� - �Ҽ�
                    this.btn_AppConsult.Visibility = BarItemVisibility.Never;
                    //�ٴ�·�� - ������
                    this.barLargeButtonItemClinical.Visibility = BarItemVisibility.Never;
                    //�ٴ�·�� - �Ҽ�
                    this.barButtonItem7.Visibility = BarItemVisibility.Never;
                }
                else
                {
                    //�༭������Ϣ - ������
                    this.barLargeButtonItemPatiEdit.Visibility = BarItemVisibility.Never;
                    //�༭������Ϣ - �Ҽ�
                    this.barButtonItemChange.Visibility = BarItemVisibility.Never;
                    //���˳�Ժ - ������
                    this.barLargeButtonItem2.Visibility = BarItemVisibility.Never;
                    //���˳�Ժ - �Ҽ�
                    this.barButtonItemOut.Visibility = BarItemVisibility.Never;
                    //�����ҵĲ��� - ������
                    this.barLargeButtonItemCancelMyPati.Visibility = BarItemVisibility.Never;
                    //�����ҵĲ��� - �Ҽ�
                    this.barButtonItemUndoMyInpatient.Visibility = BarItemVisibility.Never;
                    //��Ϊ�ҵĲ��� - ������
                    this.barLargeButtonItemSetMyPati.Visibility = BarItemVisibility.Never;
                    //��Ϊ�ҵĲ��� - �Ҽ� ---> ��ȫ������ҳ�����
                    //�趨Ӥ�� - ������
                    this.barLargeButtonItemSetBaby.Visibility = BarItemVisibility.Never;
                    //�趨Ӥ�� - �Ҽ�
                    this.barButtonItemBaby.Visibility = BarItemVisibility.Never;
                    //����¼��- ������
                    this.barLargeButtonItemDocumentWrite.Visibility = BarItemVisibility.Never;
                    //����¼��- �Ҽ�
                    this.barButtonItem_Record.Visibility = BarItemVisibility.Never;
                    //��Ժ�Ǽ� - ������
                    this.barLargeButtonItemInHosLogin.Visibility = BarItemVisibility.Always;
                    //��Ժ�Ǽ� - �Ҽ�
                    this.barButtonItem_inHosLogin.Visibility = BarItemVisibility.Never;
                    //�������� - ������
                    this.barLargeButtonItemConsultationApply.Visibility = BarItemVisibility.Never;
                    //�������� - �Ҽ�
                    this.btn_AppConsult.Visibility = BarItemVisibility.Never;
                    //�ٴ�·�� - ������
                    this.barLargeButtonItemClinical.Visibility = BarItemVisibility.Never;
                    //�ٴ�·�� - �Ҽ�
                    this.barButtonItem7.Visibility = BarItemVisibility.Never;
                }
                #endregion
                if (null != configClinical && configClinical.Trim() == "0")
                {///Ĭ����ʾ
                    this.barLargeButtonItemClinical.Visibility = BarItemVisibility.Never;
                    this.barButtonItem7.Visibility = BarItemVisibility.Never;
                }
                //ˢ�� - ������
                this.barLargeButtonItemRefresh.Visibility = BarItemVisibility.Always;
                //�˳� - ������
                this.barLargeButtonItemExit.Visibility = BarItemVisibility.Always;

                bool hasHZXT = DrectSoft.Service.DS_BaseService.FlieHasKey("HZXT");  //����ϵͳģ���Ƿ����
                if (!hasHZXT)
                {
                    //2�����ﰴť����ʾ ͬʱ�ұ߻����б�Ҳ����ʾ
                    barLargeButtonItemConsultationApply.Visibility = BarItemVisibility.Never;
                    barLargeButtonItemConsultation.Visibility = BarItemVisibility.Never;
                    navBarGroupConsultation.Visible = false;
                    btn_AppConsult.Visibility = BarItemVisibility.Never;
                    barButtonItem_consultation.Visibility = BarItemVisibility.Never;

                }
                bool hasBLLL = DrectSoft.Service.DS_BaseService.FlieHasKey("BLLL");  //�������ģ���Ƿ����
                if (!hasBLLL)
                {
                    barLargeButtonItemEmrApply.Visibility = BarItemVisibility.Never;
                    barButtonItem_emrApply.Visibility = BarItemVisibility.Never;
                }

                bool hasCRBSB = DrectSoft.Service.DS_BaseService.FlieHasKey("CRBSB");  //��Ⱦ��ģ���Ƿ����
                if (!hasCRBSB)
                {
                    barLargeButtonItemReportCard.Visibility = BarItemVisibility.Never;
                    barButtonItemReportCard.Visibility = BarItemVisibility.Never;
                }
                //����DockPanel �Ƿ���ʾadd  by ywk 2013��6��13�� 10:16:50
                if (DS_BaseService.IsShowThisMD("IsShowDockPanel", "Docter"))//�Ϸ������ء����������Ϊ�Զ�����add by ywk 
                {
                    dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;

                }
                else
                {
                    dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;//�Զ�����

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ���õ�ǰTabҳ��ѯ����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-22</date>
        /// </summary>
        private void Reset()
        {
            try
            {
                if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                {
                    textEditPATID.Text = "";
                    textEditPATNAME.Text = "";
                    textEditPATBEDNO.Text = "";
                    textEditInwDia.Text = string.Empty;
                }
                else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    m_UserControlAllListIno.Reset();
                }
                else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
                {
                    BtnReset();
                }
                else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
                {
                    m_UCFail.Reset();//.ResetText();
                }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                { }
                else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
                {
                }
                else if (xtraTabControl1.SelectedTabPage == myinplist)
                {
                    mymedicalrecordlist.Reset();
                }
                else if (xtraTabControl1.SelectedTabPage == DeptQc)
                {
                    qualityDept.Reset();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// �󶨲������ֱ�
        /// </summary>
        private void BindEmrPoint()
        {
            try
            {
                DataTable dt = m_DataManager.GetEmrPoint(m_App.User.Id);
                //add by cyq 2013-01-31 ��������������ʱTabҳ����Ϊ��ɫ
                if (null != dt && dt.Rows.Count > 0)
                {
                    this.xtraTabPagePoint.Appearance.HeaderActive.ForeColor = Color.Red;
                }
                gridControlPoint.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// ��ȡ��Ժĩ����ҽʦ�����б�
        /// edit by Yanqiao.Cai 2012-11-14
        /// 1��add try ... catch
        /// 2������Ӥ����������ʾ
        /// </summary>
        private void GetHistoryInPat()
        {
            try
            {
                //������Ժ����Ϊ��Ժʱ�仹��������ڣ��������ã�add by cyq 2013-03-14
                SetInHosOrInWardDate();
                //��ʼ��ͼƬ
                //InitializeImage2();
                DS_Common.InitializeImage_XB(repositoryItemImageXB, imageListBrxb);
                DS_Common.InitializeImage_WZJB(repositoryItemImageWZJB, imageListCustomwzjb);

                string deptID = m_App.User.CurrentDeptId;
                string wardID = m_App.User.CurrentWardId;
                string dia = textEditHistory.Text.Trim().Replace("'", "''").Replace("*", "[*]").Replace("%", "[%]");

                //DataTable dt = this.GetHistoryPat(1, deptID, wardID);
                //�ٴ�ɸ����Ժ����add by ywk 2013��5��29�� 14:00:38
                DataTable dt = ToDataTable(this.GetHistoryPat(1, deptID, wardID).Select(" BRZT not in ('1500','1501')"));
                if (dia.Length > 0)
                {
                    dt = ToDataTable(dt.Select(string.Format(@"ZDMC like '%{0}%'", dia)));
                }
                if (dt == null || dt.Rows.Count == 0)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("û�з�������������");
                    this.gridHistoryInp.DataSource = new DataTable();
                    return;
                }
                string ResultName = string.Empty;//��������Ҫ���б���ʾ������������
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ResultName = DataManager.GetPatsBabyContent(m_App, dt.Rows[i]["noofinpat"].ToString());
                    dt.Rows[i]["PatName"] = ResultName;
                }
                this.gridHistoryInp.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ����DataRow[]ת���ɱ�DataTable
        /// ���� 2013 1 6
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable ToDataTable(DataRow[] rows)
        {
            try
            {
                if (rows == null || rows.Length == 0) return null;
                DataTable tmp = rows[0].Table.Clone();
                foreach (DataRow row in rows)
                    tmp.ImportRow(row);
                return tmp;
            }
            catch (Exception)
            {
                throw;
            }
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
        /// �����ҵĲ����б�
        /// </summary>
        public void InitMyInpatient()
        {
            Thread initMyInpatientThread = new Thread(new ThreadStart(InitMyInpatientInner));
            initMyInpatientThread.Start();
        }

        /// <summary>
        /// ��ȡ�ҷֹܵĲ���
        /// </summary>
        private void InitMyInpatientInner()
        {
            try
            {
                this.myPats = m_DataManager.GetCurrentBedInfos(m_DeptId, m_WardId, QueryType.OWN);
                InitMyInpatientInnerInvoke(myPats);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitMyInpatientInnerInvoke(DataTable dtPats)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(InitMyInpatientInner));
                }
                else
                {
                    //������Ժ����Ϊ��Ժʱ�仹��������ڣ��������ã�add by cyq 2013-03-14
                    SetInHosOrInWardDate();
                    this.cmbQueryType.SelectedIndex = 0;

                    //���⴦��󶨵�����Դ������Ӥ���Ĳ��� 2012��6��11�� 16:15:05
                    DataTable newDt = myPats;

                    string ResultName = string.Empty;//��������Ҫ���б���ʾ������������
                    for (int i = 0; i < newDt.Rows.Count; i++)
                    {
                        ResultName = DataManager.GetPatsBabyContent(m_App, newDt.Rows[i]["noofinpat"].ToString());
                        newDt.Rows[i]["PatName"] = ResultName;
                    }
                    this.gridMain.DataSource = newDt;
                }
            }
            catch (Exception)
            {
                throw;
            }
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

        /// <summary>
        /// ��ѯ�¼� - ������ʷ���˲�ѯ
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-14</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                GetHistoryInPat();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void cmbQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.cmbQueryType.SelectedText == "����")
            //{
            //    checkEditShowShift.Checked = false;
            //    checkEditShowOutHos.Checked = false;
            //    checkEditOnlyShowShift.Checked = false;
            //    checkEditOnlyShowOutHos.Checked = false;

            //    checkEditShowShift.Enabled = true;
            //    checkEditShowOutHos.Enabled = true;
            //    checkEditOnlyShowShift.Enabled = true;
            //    checkEditOnlyShowOutHos.Enabled = true;
            //    InitAllPat(1);
            //}
            //else if (this.cmbQueryType.SelectedText == "ȫ��")
            //{
            //    checkEditShowShift.Checked = false;
            //    checkEditShowOutHos.Checked = false;
            //    checkEditOnlyShowShift.Checked = false;
            //    checkEditOnlyShowOutHos.Checked = false;

            //    checkEditShowShift.Enabled = false;
            //    checkEditShowOutHos.Enabled = false;
            //    checkEditOnlyShowShift.Enabled = false;
            //    checkEditOnlyShowOutHos.Enabled = false;
            //    InitAllPat(0);
            //}
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
            else if (xtraTabControl1.SelectedTabPage == paintTran)
            {
                if (paintTran.Controls.Count > 0)
                {
                    UCTran fail = FailTabPage.Controls[0] as UCTran;
                    dataRow = fail.GetSelectedGridViewRow();
                }
            }
            if (dataRow == null) return "";
            return dataRow["NoOfInpat"].ToString();
        }

        private void gridViewGridWardPat_DoubleClick(object sender, EventArgs e)
        {
            GridHitInfo hitInfo = gridViewGridWardPat.CalcHitInfo(gridMain.PointToClient(Cursor.Position));
            if (hitInfo.RowHandle < 0) return;

            decimal syxh = GetCurrentPat(gridViewGridWardPat);
            if (syxh < 0) return;

            DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
            string noofinpat = dataRow["noofinpat"].ToString();
            if (DataManager.HasBaby(noofinpat))
            {
                ChoosePatOrBaby choosepat = new ChoosePatOrBaby(m_App, noofinpat);
                choosepat.StartPosition = FormStartPosition.CenterParent;
                if (choosepat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_App.ChoosePatient(decimal.Parse(choosepat.NOOfINPAT));
                    m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());

                }
            }
            else
            {
                m_App.ChoosePatient(syxh);
                m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
            }
        }

        /// <summary>
        /// ��ȡ��ǰ����
        /// </summary>
        /// <returns></returns>
        private decimal GetCurrentPat()
        {
            if (gridViewGridWardPat.FocusedRowHandle < 0)
            {
                return -1;
            }
            else
            {
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                if (dataRow == null)
                {
                    return -1;
                }
                return Convert.ToDecimal(dataRow["NoOfInpat"]);
            }

        }

        private void barButtonItem_PersonalInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                if (dataRow == null || string.IsNullOrEmpty(dataRow["NoOfInpat"].ToString()))
                {
                    return;
                }
                //to do ���ò���������Ϣ����
                Assembly AspatientInfo = Assembly.Load("DrectSoft.Core.RedactPatientInfo");
                Type TypatientInfo = AspatientInfo.GetType("DrectSoft.Core.RedactPatientInfo.XtraFormPatientInfo");
                DevExpress.XtraEditors.XtraForm patientInfo = (DevExpress.XtraEditors.XtraForm)Activator.CreateInstance(TypatientInfo, new object[] { m_App, dataRow["NoOfInpat"].ToString() });
                patientInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                //m_App.CustomMessageBox.MessageShow("�鿴������Ϣʧ�ܣ�");
                MessageBox.Show("�鿴������Ϣʧ��" + ex.Message);
            }
        }

        /// <summary>
        /// ����¼���¼�
        /// edit by Yanqiao.Cai 2012-11-09
        /// 1��add try ... catch
        /// 2��������װ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_Record_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                {
                    DocumentsWrite();
                }
                else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    m_UserControlAllListIno.DocumentsWrite();
                }
                else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
                { }
                else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                { }
                else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
                { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����¼�뷽�� --- �ҵĲ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        /// </summary>
        private void DocumentsWrite()
        {
            try
            {
                decimal syxh = GetCurrentPat();
                if (syxh < 0) return;
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                string noofinpat = dataRow["noofinpat"].ToString();
                //m_App.ChoosePatient(syxh);
                if (DataManager.HasBaby(noofinpat))
                {
                    ChoosePatOrBaby choosepat = new ChoosePatOrBaby(m_App, noofinpat);
                    choosepat.StartPosition = FormStartPosition.CenterParent;
                    if (choosepat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_App.ChoosePatient(decimal.Parse(choosepat.NOOfINPAT));
                        m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
                    }
                }
                else
                {
                    m_App.ChoosePatient(syxh);
                    m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        /// <summary>
        /// �Ҽ��˵�
        /// edit by Yanqiao.Cai 2012-11-12
        /// 1��add try ... catch
        /// 2���Ҽ�С�����޲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager1_QueryShowPopupMenu(object sender, QueryShowPopupMenuEventArgs e)
        {
            try
            {
                GridHitInfo hitInfo = gridViewGridWardPat.CalcHitInfo(gridMain.PointToClient(Cursor.Position));
                if (hitInfo.RowHandle < 0)
                {
                    e.Cancel = true;
                    return;
                }
                if (e.Control == this.gridMain)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �ٴ�·���¼�
        /// edit by Yanqiao.Cai 2012-11-09
        /// 1��add try ... catch
        /// 2��������װ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                {
                    ClinicalPath();
                }
                else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    m_UserControlAllListIno.ClinicalPath();
                }
                else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
                { }
                else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                { }
                else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
                { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �ٴ�·������
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        /// </summary>
        private void ClinicalPath()
        {
            try
            {
                decimal syxh = GetCurrentPat();
                if (syxh < 0) return;
                m_App.ChoosePatient(syxh);
                m_App.LoadPlugIn("DrectSoft.Core.DoctorTasks.dll", "DrectSoft.Core.DoctorTasks.InpatientPathForm");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ���������¼�
        /// edit by Yanqiao.Cai 2012-11-09
        /// 1��add try ... catch
        /// 2��������װ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AppConsult_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                {
                    ApplyConsult();
                }
                else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    m_UserControlAllListIno.ApplyConsult();
                }
                else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
                { }
                else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                { }
                else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
                { }

                //ˢ�»����б��е�����
                m_UCConsultationForDocCenter.BindConsultationThread();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �������뷽�� Modified by wwj 2013-03-01
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        /// </summary>
        private void ApplyConsult()
        {
            #region ��ע��
            //try
            //{
            //    decimal syxh = GetCurrentPat();
            //    if (syxh < 0) return;
            //    m_App.ChoosePatient(syxh);

            //    FormConsultationApply formApply = new FormConsultationApply(syxh.ToString(), m_App, true);
            //    formApply.StartPosition = FormStartPosition.CenterParent;
            //    formApply.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            #endregion
            try
            {
                decimal syxh = GetCurrentPat();
                if (syxh < 0)
                {
                    return;
                }
                //�����µĻ����������
                FormApplyForMultiply formApply = new FormApplyForMultiply(syxh.ToString(), m_App, "", false);
                formApply.StartPosition = FormStartPosition.CenterParent;
                formApply.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #region ��ʱ��

        private void timerLoadData_Tick(object sender, EventArgs e)
        {
            try
            {
                m_UCConsultationForDocCenter.BindConsultationThread();
                //GetQCTiXong();
            }
            catch (Exception ex)
            {
                m_App.CustomMessageBox.MessageShow("��ʱ�����ִ���" + ex.Message);
            }

        }
        #endregion

        #region �Ի����б�Ŀ���

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
            formApprove.StartPosition = FormStartPosition.CenterScreen;
            formApprove.ShowDialog();
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
            //m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());

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
            m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());

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

            //if (!checkEditShowShift.Checked)//add by ywk 2012��11��7��15:28:45 ��ʱ��Ҫ
            //{
            //    filter1 = " extra <> '������������' ";
            //}
            //if (checkEditOnlyShowShift.Checked)
            //{
            //    filter1 = " extra = '������������' ";
            //}

            if (!checkEditShowOutHos.Checked)
            {
                filter2 = " brzt IN (1501, 1504, 1505, 1506, 1507) "; //��Ժ����
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

        /// <summary>
        /// ˫���¼� - ������ʷ���˲�ѯ
        /// edit by Yanqiao.Cai 2012-11-14
        /// 1��add try ... catch
        /// 2�����ܵ���(˫������Ӥ��ѡ��ҳ��)
        /// 3��˫��С�����޲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridHistoryInp_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridHitInfo hitInfo = gridViewHistoryInfo.CalcHitInfo(gridHistoryInp.PointToClient(Cursor.Position));
                if (hitInfo.RowHandle < 0)
                {
                    return;
                }
                DataRow dataRow = gridViewHistoryInfo.GetDataRow(gridViewHistoryInfo.FocusedRowHandle);
                if (dataRow == null)//add by xlb 2012-12-25
                {
                    return;
                }
                string noofinpat = dataRow["noofinpat"].ToString();
                if (DataManager.HasBaby(noofinpat))
                {
                    ChoosePatOrBaby choosepat = new ChoosePatOrBaby(m_App, noofinpat);
                    choosepat.StartPosition = FormStartPosition.CenterParent;
                    if (choosepat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_App.ChoosePatient(decimal.Parse(choosepat.NOOfINPAT));
                        m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
                    }
                }
                else
                {
                    m_App.ChoosePatient(Convert.ToDecimal(noofinpat));
                    m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
                }

                #region by cyq 2012-11-14 ˫������Ӥ��ѡ��ҳ��
                //if (gridViewHistoryInfo.FocusedRowHandle < 0) return;

                //DataRowView drv = gridViewHistoryInfo.GetRow(gridViewHistoryInfo.FocusedRowHandle) as DataRowView;
                //if (drv != null)
                //{
                //    decimal syxh = GetCurrentPat(gridViewHistoryInfo);
                //    if (syxh < 0) return;

                //    if (IsLock(syxh))
                //    {
                //        //�ѹ鵵����˫�����벡������
                //        ApplyExamine frm = new ApplyExamine(m_App);
                //        frm.SetPatID(GetCurrentPatID(gridViewHistoryInfo));
                //        frm.ShowDialog(this.Owner);
                //    }
                //    else
                //    {
                //        //δ�鵵����˫�����벡���༭
                //        m_App.ChoosePatient(syxh);
                //        m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �б�����¼� - ������ʷ���˲�ѯ
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-14</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewHistoryInfo_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                s.Alignment = StringAlignment.Near;
                s.LineAlignment = StringAlignment.Center;
                if (e.CellValue == null)
                {
                    return;
                }
                DataRowView drv = gridViewHistoryInfo.GetRow(e.RowHandle) as DataRowView;
                //ȡ�ò�������
                string patname = drv["patname"].ToString().Trim();
                if (e.Column.FieldName == colname.FieldName)
                {
                    if (patname.Contains("Ӥ��"))
                    {
                        Region oldRegion = e.Graphics.Clip;
                        e.Graphics.Clip = new Region(e.Bounds);

                        e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height));
                        e.Graphics.DrawString(patname, e.Appearance.Font, Brushes.Red,
                            new RectangleF(e.Bounds.Location, new SizeF(300, e.Bounds.Height)), s);

                        e.Graphics.Clip = oldRegion;
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
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
            try
            {
                refreshGridView();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void textEditPATNAME_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                refreshGridView();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void textEditPATBEDNO_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                refreshGridView();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void refreshGridView()
        {
            string PATID = textEditPATID.Text.Trim().Replace("'", "''").Replace("*", "[*]").Replace("%", "[%]");
            string PATNAME = textEditPATNAME.Text.Trim().Replace("'", "''").Replace("*", "[*]").Replace("%", "[%]");
            string PATBEDNO = textEditPATBEDNO.Text.Trim().Replace("'", "''").Replace("*", "[*]").Replace("%", "[%]");
            string InwDia = textEditInwDia.Text.Trim().Replace("'", "''").Replace("*", "[*]").Replace("%", "[%]");

            string filter = " PATID like '%{0}%' and (PatName like '%{1}%' or PY like '%{1}%' or WB like '%{1}%') and (BedID like '%{2}%' or '{2}' is null or '{2}'='')  and (ZDMC like '%{3}%'or '{3}' is null or '{3}' = '') ";
            DataTable dt = gridMain.DataSource as DataTable;
            if (dt != null)
            {
                string rowFilter = AddFilter();
                if (rowFilter != "")
                {
                    dt.DefaultView.RowFilter = rowFilter + " and " + string.Format(filter, PATID, PATNAME, PATBEDNO, InwDia);
                }
                else
                {
                    dt.DefaultView.RowFilter = string.Format(filter, PATID, PATNAME, PATBEDNO, InwDia);
                }
            }
        }

        /// <summary>
        /// �����ҵĲ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemUndoMyInpatient_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                if (gridViewGridWardPat.FocusedRowHandle < 0)
                {
                    MessageBox.Show("��ѡ��һ�����˼�¼��");
                    return;
                }
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                if (dataRow == null)
                {
                    return;
                }

                if (MessageBox.Show("��ȷ��Ҫ�������� " + dataRow["PATNAME"].ToString() + " ��", "��ʾ��Ϣ", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //decimal syxh = GetCurrentPat();
                decimal syxh = Convert.ToDecimal(dataRow["NoOfInpat"]);
                if (syxh < 0) return;
                string sqlUndoMyInpatient = " update Doctor_AssignPatient set valid = '0' where valid = '1' and noofinpat = '{0}' ";
                m_App.SqlHelper.ExecuteNoneQuery(string.Format(sqlUndoMyInpatient, syxh), CommandType.Text);
                string sqlUndoMyInpatient2 = " update inpatient set resident = '' where noofinpat = '{0}' ";
                m_App.SqlHelper.ExecuteNoneQuery(string.Format(sqlUndoMyInpatient2, syxh), CommandType.Text);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                Type.GetType("DrectSoft.Core.ZymosisReport.ReportCardApply,DrectSoft.Core.ZymosisReport"),
                new object[] { m_App, noofinpat });
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        /// <summary>
        /// ������Ϣ�¼�
        /// edit by Yanqiao.Cai 2012-11-09
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItemPatientInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                {
                    ViewPatientInfo();
                }
                else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    m_UserControlAllListIno.ViewPatientInfo();
                }
                else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
                {

                }
                else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                { }
                else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
                { }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ������Ϣ����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        /// </summary>
        private void ViewPatientInfo()
        {
            try
            {
                string noOfInpat = GetSelectedGridView();
                if (!string.IsNullOrEmpty(noOfInpat))
                {
                    //to do ���ò���������Ϣ����
                    Assembly AspatientInfo = Assembly.Load("DrectSoft.Core.RedactPatientInfo");
                    Type TypatientInfo = AspatientInfo.GetType("DrectSoft.Core.RedactPatientInfo.XtraFormPatientInfo");
                    DevExpress.XtraEditors.XtraForm patientInfo = (DevExpress.XtraEditors.XtraForm)Activator.CreateInstance(TypatientInfo, new object[] { m_App, noOfInpat });
                    patientInfo.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void barLargeButtonItemAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            if (AllInpTabPage.Controls.Count > 1)
                return;
            InitAllListView();
        }

        /// <summary>
        /// �������� --- ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItemEmrApply_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                MedicalRecordManage.UI.MedicalRecordApply frm = new MedicalRecordManage.UI.MedicalRecordApply(m_App);
                frm.ShowDialog(this.Owner);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }

        }

        /// <summary>
        /// �������� --- �Ҽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_emrApply_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //ApplyExamine frm = new ApplyExamine(m_App);
                //frm.ShowDialog(this.Owner);
                MedicalRecordManage.UI.MedicalRecordApply frm = new MedicalRecordManage.UI.MedicalRecordApply(m_App);
                frm.ShowDialog(this.Owner);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void barLargeButtonItemReportCard_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_App.LoadPlugIn("DrectSoft.Core.ZymosisReport.dll", "DrectSoft.Core.ZymosisReport.MainForm");
        }

        private void barLargeButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            try
            {
                DS_Common.SetWaitDialogCaption(m_WaitDialog, "����ˢ�£����Ժ�...");
                checkEditShowShift.Checked = false;
                checkEditShowOutHos.Checked = false;
                checkEditOnlyShowShift.Checked = false;
                checkEditOnlyShowOutHos.Checked = false;

                checkEditShowShift.Enabled = true;
                checkEditShowOutHos.Enabled = true;
                checkEditOnlyShowShift.Enabled = true;
                checkEditOnlyShowOutHos.Enabled = true;
                ControlRefresh();
                Reset();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DS_Common.HideWaitDialog(m_WaitDialog);
            }
        }

        private void gridControlPoint_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gridViewEmrPoint.FocusedRowHandle < 0)
            {
                return;
            }
            DataRow dataRow = gridViewEmrPoint.GetDataRow(gridViewEmrPoint.FocusedRowHandle);
            if (dataRow == null)
            {
                return;
            }
            decimal syxh = Convert.ToDecimal(dataRow["noofinpat"]);
            if (syxh < 0) return;
            m_App.CurrentSelectedEmrID = dataRow["recorddetailid"].ToString().Trim();
            m_App.ChoosePatient(syxh);
            m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
        }
        StringFormat s = new StringFormat();
        /// <summary>
        /// ���ƴ�Ӥ����Ϣ����ʾ��
        /// add by ywk 2012��6��8�� 10:32:09
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewGridWardPat_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            s.Alignment = StringAlignment.Near;
            s.LineAlignment = StringAlignment.Center;
            if (e.CellValue == null) return;
            DataRowView drv = gridViewGridWardPat.GetRow(e.RowHandle) as DataRowView;
            //ȡ�ò�������
            string patname = drv["patname"].ToString().Trim();

            if (e.Column == colname)
            {
                if (patname.Contains("Ӥ��"))
                {
                    Region oldRegion = e.Graphics.Clip;
                    e.Graphics.Clip = new Region(e.Bounds);

                    e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height));
                    e.Graphics.DrawString(patname, e.Appearance.Font, Brushes.Red,
                        new RectangleF(e.Bounds.Location, new SizeF(300, e.Bounds.Height)), s);

                    e.Graphics.Clip = oldRegion;
                    e.Handled = true;
                }

            }
        }

        private void DocCenter_Activated(object sender, EventArgs e)
        {
            FocusControl();
        }

        int oldFocusRowHandle;
        /// <summary>
        /// �趨Ӥ��
        /// edit by Yanqiao.Cai 2012-11-09
        /// 1��add try ... catch
        /// 2��������װ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemBaby_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                try
                {
                    if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                    {
                        SetBabys();
                    }
                    else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                    {
                        m_UserControlAllListIno.SetBabys();
                    }
                    else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
                    { }
                    else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
                    { }
                    else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                    { }
                    else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                    { }
                    else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
                    { }
                }
                catch (Exception ex)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �趨Ӥ������
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        /// </summary>
        private void SetBabys()
        {
            try
            {
                if (gridViewGridWardPat.FocusedRowHandle < 0)
                    return;
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                string syxh = dataRow["NoOfInpat"].ToString();
                string patname = dataRow["PatName"].ToString();
                if ((!string.IsNullOrEmpty(syxh)) && (syxh != "-1"))
                {
                    oldFocusRowHandle = gridViewGridWardPat.FocusedRowHandle;
                    SetPatientsBaby setBaby = new SetPatientsBaby(syxh, m_App, patname, this);
                    setBaby.StartPosition = FormStartPosition.CenterScreen;//�����������м� 
                    setBaby.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void simpleButtonRefresh_Click(object sender, EventArgs e)
        {
            GetQCTiXong();
        }

        private void simpleButtonShouSuo_Click(object sender, EventArgs e)
        {
            gridView1.CollapseAllGroups();
        }

        private void simpleButtonZhanKai_Click(object sender, EventArgs e)
        {
            gridView1.ExpandAllGroups();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string noofinpat = dataRow["noofinpat"].ToString();
            if (DataManager.HasBaby(noofinpat))
            {
                ChoosePatOrBaby choosepat = new ChoosePatOrBaby(m_App, noofinpat);
                choosepat.StartPosition = FormStartPosition.CenterParent;
                if (choosepat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_App.ChoosePatient(decimal.Parse(choosepat.NOOfINPAT));
                    m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
                }
            }
            else
            {
                m_App.ChoosePatient(Convert.ToDecimal(noofinpat));
                m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
            }
        }

        private void gridView1_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {

        }

        private void btnChanged_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                string noofinpat = dataRow["noofinpat"].ToString();
                m_DataManager.SetRHQCHasXiuGai(noofinpat);
                m_App.CustomMessageBox.MessageShow("�޸ĳɹ���");
                GetQCTiXong();

            }
            catch (Exception ex)
            {

                m_App.CustomMessageBox.MessageShow(ex.Message);
            }
        }

        /// <summary>
        /// ��Ժ�Ǽ� --- ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItemInHosLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                InHosLogin();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��Ժ�Ǽ� --- �Ҽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_inHosLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                InHosLogin();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��Ժ�ǼǷ���
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        private void InHosLogin()
        {
            try
            {
                //string noOfInpat = GetSelectedGridView();
                //if (!string.IsNullOrEmpty(noOfInpat))
                // {
                //to do ���ò���������Ϣ����
                //BasePatientInfo info = new BasePatientInfo(m_App);
                //info.ShowCurrentPatInfo(dataRow["NoOfInpat"].ToString());
                XtraFormInHosLogin patientInfo = new XtraFormInHosLogin(m_App, null);
                patientInfo.ShowDialog();
                //add by cyq 2012-11-15 ��Ժ�ǼǺ�ˢ��
                if (patientInfo.refreashFlag)
                {
                    if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                    {
                        InitMyInpatient();
                    }
                    else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                    {
                        InitAllListView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �༭������Ϣ����������
        /// add by ywk 2012��9��4�� 09:01:58
        /// edit by Yanqiao.Cai 2012-11-09
        /// 1��add try ... catch
        /// 2��������װ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                {
                    EditPatientInfo();
                }
                else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    m_UserControlAllListIno.EditPatientInfo();
                }
                else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
                { }
                else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
                {
                    string noofinpat = m_UCFail.EditPatientInfo();
                    if (noofinpat != "")
                    {
                        XtraFormInHosLogin patientInfo = new XtraFormInHosLogin(m_App, noofinpat);
                        patientInfo.SetEnable(false);
                        patientInfo.Text = "�༭������Ϣ";
                        patientInfo.ShowDialog();
                    }
                }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                { }
                else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
                { }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �༭������Ϣ����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        /// </summary>
        private void EditPatientInfo()
        {
            try
            {
                if (gridViewGridWardPat.FocusedRowHandle < 0)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("��ѡ��һ����¼");
                    return;
                }
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                string syxh = dataRow["NoOfInpat"].ToString();

                XtraFormInHosLogin patientInfo = new XtraFormInHosLogin(m_App, syxh);
                patientInfo.SetEnable(false);
                patientInfo.Text = "�༭������Ϣ";
                patientInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ���˳�Ժ
        /// edit by Yanqiao.Cai 2012-11-09
        /// 1��add try ... catch
        /// 2��������װ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPage == MyInpTabPage)//�ҵĻ���
                {
                    SetPatientOutHos();
                }
                else if (xtraTabControl1.SelectedTabPage == AllInpTabPage)//ȫ������
                {
                    m_UserControlAllListIno.SetPatientOutHos();
                }
                else if (xtraTabControl1.SelectedTabPage == HistoryInpTabPage)//������ʷ����
                { }
                else if (xtraTabControl1.SelectedTabPage == FailTabPage)//�ѳ�Ժδ�鵵
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPageCheck)//�������
                { }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPagePoint)//��������
                { }
                else if (xtraTabControl1.SelectedTabPage == Replenish)//��д����
                { }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ���˳�Ժ����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        /// </summary>
        /// edit by xlb 2012-12-24
        private void SetPatientOutHos()
        {
            try
            {
                if (gridViewGridWardPat.FocusedRowHandle < 0)
                {
                    MessageBox.Show("��ѡ��һ�����˼�¼��");
                    return;
                }
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                if (dataRow == null)//add xlb 2012-12-24
                {
                    return;
                }
                string syxh = dataRow["noofinpat"].ToString();
                if ((!string.IsNullOrEmpty(syxh)))
                {
                    //DialogResult dResult = m_App.CustomMessageBox.MessageShow("ȷ���ò��˳�Ժ��", CustomMessageBoxKind.QuestionYesNo);
                    DialogResult dResult = MessageBox.Show("��ȷ���� " + dataRow["PATNAME"].ToString() + " ��Ժ��", "���˳�Ժ", MessageBoxButtons.YesNo);
                    if (dResult == DialogResult.Yes)
                    {
                        //string sql = string.Format("update inpatient i set i.status=1503 and i.outhosdept='{0}' and i.outhosward='{1}' and i.outwarddate='{2}' and i.outhosdate='{3}' where inpatient.noofinpat={4}",
                        // m_App.User.CurrentDeptId,
                        //m_App.User.CurrentWardId,
                        //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 
                        //Convert.ToInt32(syxh));
                        // m_App.SqlHelper.ExecuteNoneQuery(sql, CommandType.Text);
                        //xll ��ӵ��Ӳ�����Ժ��� 1  emrouthos
                        string sql = "update inpatient i set i.status=1503,i.outhosdept=@outhostdept, i.outhosward=@outhostward,i.outwarddate=@outwarddate,i.outhosdate=@outhostdate,i.emrouthos='1' where i.noofinpat=@noofinpat";
                        SqlParameter[] sps ={
                                               new SqlParameter("@outhostdept",m_App.User.CurrentDeptId),
                                               new SqlParameter("@outhostward",m_App.User.CurrentWardId),
                                               new SqlParameter("@outwarddate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                                               new SqlParameter("@outhostdate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                                               new SqlParameter("@noofinpat",Convert.ToInt32(syxh))
                                            };
                        DS_SqlHelper.ExecuteNonQuery(sql, sps, CommandType.Text);
                        RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Ժ�ڻ��� --- ������
        /// </summary>
        /// edit by Yanqiao.Cai 2012-11-09
        /// 1��add try ... catch
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItemConsultation_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                m_App.LoadPlugIn("DrectSoft.Core.Consultation.dll", "DrectSoft.Core.Consultation.ConsultationFormStartUp");
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Ժ�ڻ��� --- �Ҽ�
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-09</date>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_consultation_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                m_App.LoadPlugIn("DrectSoft.Core.Consultation.dll", "DrectSoft.Core.Consultation.ConsultationFormStartUp");
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����(���Ҳ�����ʷ��ѯ)
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-10-10</date>
        /// </summary>
        private void BtnReset()
        {
            textEditPatientSN.Text = "";
            textEditPatientName.Text = "";
            dateEditFrom.DateTime = DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = DateTime.Now;
            textEditHistory.Text = "";
            this.textEditPatientSN.Focus();
        }

        /// <summary>
        /// ����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-10-10</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reset_Click(object sender, EventArgs e)
        {
            BtnReset();
        }

        /// <summary>
        /// �س��л�����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-10-11</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == 13)
                {
                    SendKeys.Send("{Tab}");
                    SendKeys.Flush();
                }
            }
            catch (Exception ex)
            {
                //m_App.CustomMessageBox.MessageShow(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��Ϊ�ҵĲ����¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-08</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItemSetMyPati_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                m_UserControlAllListIno.SetMyPatient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �˳��¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-08</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MyMessageBox.Show("��ȷ��Ҫ�˳���", "�˳�", MyMessageBoxButtons.OkCancel) == DialogResult.Cancel)
                {
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-08</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            try
            {
                this.textEditPATBEDNO.Text = string.Empty;
                this.textEditPATID.Text = string.Empty;
                this.textEditPATNAME.Text = string.Empty;
                this.textEditInwDia.Text = string.Empty;
                this.textEditPATBEDNO.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textEditInwDia_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                refreshGridView();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void barButtonItemRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                string noofinpat = dataRow["noofinpat"] == null ? null : dataRow["noofinpat"].ToString();
                if (noofinpat == null)
                {
                    return;
                }
                nursingRecordForm = NursingRecordForm.CreateInstance();
                this.ShowVigalsHandle += new del_ShowVigals(nursingRecordForm.InitNursingRecord);
                OnShowVigals(noofinpat);
                nursingRecordForm.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void gridMain_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (nursingRecordForm != null && !nursingRecordForm.IsDisposed)
                {
                    DataRow dataRow = gridViewGridWardPat.GetDataRow(gridViewGridWardPat.FocusedRowHandle);
                    string noofinpat = dataRow["noofinpat"].ToString();
                    //nursingRecordForm = NursingRecordForm.CreateInstance();
                    //this.ShowVigalsHandle += new del_ShowVigals(nursingRecordForm.InitNursingRecord);
                    OnShowVigals(noofinpat);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ToExcelColumnForm toexcelcolumn = new ToExcelColumnForm(this.gridMain, this.gridViewGridWardPat);
                toexcelcolumn.ShowDialog();
                if (toexcelcolumn.m_iCommandFlag == 1)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Title = "����";
                    saveFileDialog.Filter = "Excel�ļ�(*.xls)|*.xls";
                    DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                    List<DrectSoft.Core.OwnBedInfo.ToExcelColumnForm.ListItem> list = toexcelcolumn.lists;
                    if (dialogResult == DialogResult.OK)
                    {
                        DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                        toexcelcolumn.dbgrid.ExportToXls(saveFileDialog.FileName.ToString(), true);

                        if (list.Count > 0)
                        {
                            foreach (GridColumn c in this.gridViewGridWardPat.Columns)
                            {
                                foreach (DrectSoft.Core.OwnBedInfo.ToExcelColumnForm.ListItem item in list)
                                {
                                    if (item.Value == c.Name)
                                    {
                                        c.Visible = true;
                                    }
                                }

                            }
                        }
                        m_App.CustomMessageBox.MessageShow("�����ɹ���");
                    }
                    else
                    {
                        if (list.Count > 0)
                        {
                            foreach (GridColumn c in this.gridViewGridWardPat.Columns)
                            {
                                foreach (DrectSoft.Core.OwnBedInfo.ToExcelColumnForm.ListItem item in list)
                                {
                                    if (item.Value == c.Name)
                                    {
                                        c.Visible = true;
                                    }
                                }

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

        private bool IsColumn(string name, List<DrectSoft.Core.OwnBedInfo.ToExcelColumnForm.ListItem> list)
        {
            foreach (DrectSoft.Core.OwnBedInfo.ToExcelColumnForm.ListItem item in list)
            {
                if (Name == item.Value)
                {
                    return false;

                }
            }
            return true;

        }

        private void barLargeButtonItemWriteUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                MedicalRecordManage.UI.MedicalRecordApplyBack frm = new MedicalRecordManage.UI.MedicalRecordApplyBack(m_App);
                frm.ShowDialog(this.Owner);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        private void gridControlPoint_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// �ж��ǲ��ǿ�������
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool isdeptManager(string deptid, string userid)
        {
            bool result = false;
            try
            {
                DataTable deptmanager = GetDirectorDoc(deptid);
                foreach (DataRow dr in deptmanager.Rows)
                {
                    if (dr["ID"].ToString() == userid)
                    {
                        result = true;

                    }
                }
                if (!result)//add Ukey zhang 2016-10-09 һ�����μ�˼�������
                {
                    DataTable usermanager = GetDirectorDocById(userid);
                    foreach (DataRow dr in usermanager.Rows)
                    {
                        if (dr["GRADE"].ToString() == "2000")
                        {
                            result = true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
                return result;
            }
            return result;
        }
        /// <summary>
        /// ���ݿ��ұ�Ż�ô˿����¶�Ӧ������
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataTable GetDirectorDoc(string deptid)
        {
            string sql = string.Format(@"  
               SELECT * FROM users u
         WHERE (u.wardid IS NOT NULL or u.wardid != '')
           AND u.valid = '1' AND u.grade = '2000'and u.deptid='{0}'  ORDER BY u.ID ", deptid);
            return m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
        }
        /// <summary>
        /// ������ԱID��ȡ��Ա��Ϣ
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetDirectorDocById(string userid)//add Ukey zhang 2016-10-09 һ�����μ�˼�������
        {
            string sql = string.Format(@"  
               SELECT * FROM users u
         WHERE (u.wardid IS NOT NULL or u.wardid != '')
           AND u.valid = '1' AND u.grade = '2000'and u.id='{0}'  ORDER BY u.ID ", userid);
            return m_App.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
        }
    }
}
