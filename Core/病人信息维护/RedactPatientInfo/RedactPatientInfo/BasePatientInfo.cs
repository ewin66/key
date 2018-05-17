using DevExpress.Utils;
using System;
using System.Data;
using System.Windows.Forms;
using DrectSoft.Core.RedactPatientInfo.PublicSet;
using DrectSoft.Core.RedactPatientInfo.UserControls;
using DrectSoft.FrameWork.WinForm.Plugin;

namespace DrectSoft.Core.RedactPatientInfo
{
    public partial class BasePatientInfo : DevExpress.XtraEditors.XtraForm
    {
        IEmrHost m_app;
        IDataAccess sql_Helper;

        UCBaseInfo m_UCBaseInfo;
        UCLinkman m_UCLinkman;
        UCDiacrisis m_UCDiacrisis;

        //UCFamilyHistory m_UCFamilyHistory;
        //UCPersonalHistory m_UCPersonalHistory;
        //UCAllergicHistory m_UCAllergicHistory;
        //UCOperationHistory m_UCOperationHistory;
        //UCDiseaseHistory m_UCDiseaseHistory;


        string m_NoOfInpat;
        DataTable m_Table;

        public BasePatientInfo(IEmrHost app)
        {
            InitializeComponent();
            m_app = app;
            //m_WaitDialog = new WaitDialogForm("�����û����桭��", "���Եȡ�");
        }

        #region IStartPlugIn ��Ա

        #endregion

        private WaitDialogForm m_WaitDialog;

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

        /// <summary>
        /// ������ҳ���
        /// </summary>
        public DialogResult ShowCurrentPatInfo()
        {
            m_NoOfInpat = m_app.CurrentPatientInfo.NoOfFirstPage.ToString();
            return ShowDialog();

        }

        /// <summary>
        /// ������ҳ���
        /// </summary>
        /// <param name="NoOfInpat">��ҳ���</param>
        public DialogResult ShowCurrentPatInfo(string NoOfInpat)
        {
            m_NoOfInpat = NoOfInpat;
            return ShowDialog();

        }
        private void BasePatientInfo_Load(object sender, EventArgs e)
        {
            SqlUtil.App = m_app;

            SetWaitDialogCaption("���ڶ�ȡ���߻�����Ϣ");

            if (string.IsNullOrEmpty(m_NoOfInpat))
            {
                SqlUtil.App.CustomMessageBox.MessageShow("�޷��ҵ����˻�����Ϣ��");
                btnSave.Enabled = false;
                //return;
            }
            else
            {
                //��ȡ���˻�����Ϣ
                m_Table = SqlUtil.GetRedactPatientInfoFrm("14", "", m_NoOfInpat);

                if (m_Table.Rows.Count <= 0)
                {
                    SqlUtil.App.CustomMessageBox.MessageShow("����û�л�����Ϣ��");
                    btnSave.Enabled = false;
                    //return;
                }
            }

            //���ػ�����Ϣ
            m_UCBaseInfo = new UCBaseInfo(m_Table, m_NoOfInpat);
            m_UCBaseInfo.Dock = DockStyle.Fill;
            tabPageBaseInfo.Controls.Add(m_UCBaseInfo);

            //���ص�һ��ϵ��
            m_UCLinkman = new UCLinkman(m_NoOfInpat);
            m_UCLinkman.Dock = DockStyle.Fill;
            tabPageLinkman.Controls.Add(m_UCLinkman);

            // SetWaitDialogCaption("���ڶ�ȡ���߾�����Ϣ");
            //���ؾ�����Ϣ
            m_UCDiacrisis = new UCDiacrisis(m_Table, m_NoOfInpat);
            m_UCDiacrisis.Dock = DockStyle.Fill;
            tabPageDiacrisis.Controls.Add(m_UCDiacrisis);

            //SetWaitDialogCaption("���ڶ�ȡ���߼���ʷ");
            ////���ؼ���ʷ
            //m_UCFamilyHistory = new UCFamilyHistory(m_NoOfInpat);
            //m_UCFamilyHistory.Dock = DockStyle.Fill;
            //this.tabPageFamilyHistory.Controls.Add(m_UCFamilyHistory);

            ////���ظ���ʷ
            //m_UCPersonalHistory = new UCPersonalHistory(m_NoOfInpat);
            //m_UCPersonalHistory.Dock = DockStyle.Fill;
            //this.tabPagePersonalHistory.Controls.Add(m_UCPersonalHistory);

            ////���ع���ʷ
            //m_UCAllergicHistory = new UCAllergicHistory(m_NoOfInpat);
            //m_UCAllergicHistory.Dock = DockStyle.Fill;
            //this.tabPageAllergicHistory.Controls.Add(m_UCAllergicHistory);

            ////��������ʷ
            //m_UCOperationHistory = new UCOperationHistory(m_NoOfInpat);
            //m_UCOperationHistory.Dock = DockStyle.Fill;
            //tabPageOperationHistory.Controls.Add(m_UCOperationHistory);

            ////���ؼ���ʷ
            //m_UCDiseaseHistory = new UCDiseaseHistory(m_NoOfInpat);
            //m_UCDiseaseHistory.Dock = DockStyle.Fill;
            //tabPageDiseaseHistory.Controls.Add(m_UCDiseaseHistory);
            //HideWaitDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            if (tabPatientInfo.TabPages.TabControl.SelectedTabPage == tabPageBaseInfo)
            {
                //���������Ϣ
                m_UCBaseInfo.SaveBaseInfo();
            }
            else if (tabPatientInfo.TabPages.TabControl.SelectedTabPage == tabPageLinkman)
            {
                //�����һ��ϵ����Ϣ
                m_UCLinkman.SaveUCLinkmanInfo(true);
            }
            else if (tabPatientInfo.TabPages.TabControl.SelectedTabPage == tabPageDiacrisis)
            {
                //���������Ϣ
                m_UCDiacrisis.SaveUCDiacrisisInfo();
            }
            //else if (tabPatientInfo.TabPages.TabControl.SelectedTabPage == tabPageFamilyHistory)
            //{
            //    //�������ʷ��Ϣ
            //    m_UCFamilyHistory.SaveUCFamilyHistoryInfo(true);
            //}
            //else if (tabPatientInfo.TabPages.TabControl.SelectedTabPage == tabPagePersonalHistory)
            //{
            //    //�������ʷ��Ϣ
            //    m_UCPersonalHistory.SaveUCPersonalHistory();
            //}
            //else if (tabPatientInfo.TabPages.TabControl.SelectedTabPage == tabPageAllergicHistory)
            //{
            //    //�������ʷ��Ϣ
            //    m_UCAllergicHistory.SaveUCAllergicHistoryInfo(true);
            //}
            //else if (tabPatientInfo.TabPages.TabControl.SelectedTabPage == tabPageOperationHistory)
            //{
            //    //��������ʷ��Ϣ
            //    m_UCOperationHistory.SaveUCOperationHistoryInfo(true);
            //}
            //else if (tabPatientInfo.TabPages.TabControl.SelectedTabPage == tabPageDiseaseHistory)
            //{
            //    //���漲��ʷ��Ϣ
            //    m_UCDiseaseHistory.SaveUCDiseaseHistoryInfo(true);
            //}

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }









    }
}