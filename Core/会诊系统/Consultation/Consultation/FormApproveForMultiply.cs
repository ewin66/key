using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Core.MainEmrPad;
using DrectSoft.Emr.Util;
using DrectSoft.Core.Consultation.Dal;
using DrectSoft.Common;
using DevExpress.Utils;
using DrectSoft.Service;

namespace DrectSoft.Core.Consultation
{
    public partial class FormApproveForMultiply : DevExpress.XtraEditors.XtraForm 
    {
        private string m_NoOfFirstPage;
        private IEmrHost m_Host;
        private string m_ConsultApplySn = string.Empty;

        public FormApproveForMultiply()
        {
            InitializeComponent();
        }

        public FormApproveForMultiply(string noOfFirstPage, IEmrHost host, string consultApplySn)
            : this()
        {
            m_NoOfFirstPage = noOfFirstPage;
            m_Host = host;
            m_ConsultApplySn = consultApplySn;
            InitInner(false);
            ApplyForMultiply.ReadOnlyControl();
            memoEditSuggestion.Focus();
        }

        private void InitInner(bool isNew)
        {
            ApplyForMultiply.Init(m_NoOfFirstPage, m_Host, isNew, true, m_ConsultApplySn);
        }

        /// <summary>
        /// ���ͨ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (CheckData(true))
            {
                Dal.DataAccess.ModifyConsultationData(m_ConsultApplySn, "1", Convert.ToString((int)ConsultStatus.WaitConsultation), memoEditSuggestion.Text.Trim());
                m_Host.CustomMessageBox.MessageShow("��˳ɹ�", CustomMessageBoxKind.InformationOk);
                this.Close();
            }
        }
        private bool CheckData(bool isOK)
        {
            if (string.IsNullOrEmpty(memoEditSuggestion.Text.Trim()) && !isOK)
            {
                m_Host.CustomMessageBox.MessageShow("���ԭ����Ϊ��", CustomMessageBoxKind.WarningOk);
                memoEditSuggestion.Focus();
                return false;
            }
            if(memoEditSuggestion.Text.Trim().Length>3000)
            {
                m_Host.CustomMessageBox.MessageShow("�������������ܴ���3000", CustomMessageBoxKind.WarningOk);
                memoEditSuggestion.Focus();
                return false;
            }
            return true;

        }

        /// <summary>
        /// ��˷��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonReject_Click(object sender, EventArgs e)
        {
            if (CheckData(false))
            { 
                Dal.DataAccess.ModifyConsultationData(m_ConsultApplySn, "1", Convert.ToString((int)ConsultStatus.Reject), memoEditSuggestion.Text.Trim());
                m_Host.CustomMessageBox.MessageShow("����ɹ�", CustomMessageBoxKind.InformationOk);
                this.Close();
            }
        }

        /// <summary>
        /// ��������������
        /// </summary>
        private void HideApproveArea()
        {
            groupControlApprove.Visible = false;
            ApplyForMultiply.Location = groupControlApprove.Location;
            this.Height = ApplyForMultiply.Height + 60;
            //add by cyq 2012-10-25 ȥ��������ռλ
            this.Width = this.Width - 15;
            this.Text = "������Ϣ";
        }

        public void ReadOnlyControl()
        {
            simpleButtonOK.Enabled = false;
            simpleButtonReject.Enabled = false;
            memoEditSuggestion.Properties.ReadOnly = true;

            ApplyForMultiply.ReadOnlyControl();

            DataSet ds = Dal.DataAccess.GetConsultationDataSet(m_ConsultApplySn, "20");//, Convert.ToString((int)ConsultType.More));
            DataTable dtConsultApply = ds.Tables[0];
            memoEditSuggestion.Text = dtConsultApply.Rows[0]["RejectReason"].ToString().Trim();
            string stateID = dtConsultApply.Rows[0]["stateid"].ToString().Trim();

            if (stateID == Convert.ToString((int)ConsultStatus.WaitApprove) )//δ������������Ϣ
            {
                HideApproveArea();
            }
 
        }

        /// <summary>
        /// ����������Ϣ�������޸�
        /// </summary>
        public void ApplyInfoReadOnly()
        {
            ApplyForMultiply.ReadOnlyControl();
        }

        private void xtraTabControlApprove_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControlApprove.SelectedTabPage == xtraTabPageEmrContent)
            {
                string config = DS_SqlService.GetConfigValueByKey("IsNewUcInput");
                if (null != config && config.Trim() == "1")
                {
                    AddEmrInputNew();
                }
                else
                {
                    AddEmrInput();
                    LoadEmrContent();
                }
            }
            if (this.xtraTabControlApprove.SelectedTabPage == xtraTabPage1)
            {
                this.ActiveControl = memoEditSuggestion;
            }
        }

        #region �������� - �ϰ�

        /// <summary>
        /// �������ݴ���
        /// </summary>
        UCEmrInput m_UCEmrInput;
        bool m_IsLoadedEmrContent = false;
        private void LoadEmrContent()
        {
            if (!string.IsNullOrEmpty(m_NoOfFirstPage) && !m_IsLoadedEmrContent)
            {
                m_UCEmrInput.PatientChangedByIEmrHost(Convert.ToDecimal(m_NoOfFirstPage));
                m_UCEmrInput.HideBar();
                m_IsLoadedEmrContent = true;
            }
        }

        private void AddEmrInput()
        {
            m_UCEmrInput = new UCEmrInput("���ڼ��ػ�����Ϣ");
            m_UCEmrInput.CurrentInpatient = null;
            m_UCEmrInput.HideBar();
            RecordDal m_RecordDal = new RecordDal(m_Host.SqlHelper);
            m_UCEmrInput.SetInnerVar(m_Host, m_RecordDal);
            xtraTabPageEmrContent.Controls.Add(m_UCEmrInput);
            m_UCEmrInput.Dock = DockStyle.Fill;
        }
        #endregion

        #region �������� - �°�
        /// <summary>
        /// �������ݴ���
        /// </summary>
        DrectSoft.Core.MainEmrPad.New.UCEmrInput m_UCEmrInputNew;
        bool m_IsLoadedEmrContentNew = false;

        /// <summary>
        /// ���ز���
        /// </summary>
        private void AddEmrInputNew()
        {
            try
            {
                if (string.IsNullOrEmpty(m_NoOfFirstPage) || m_IsLoadedEmrContentNew)
                {
                    return;
                }
                m_Host.ChoosePatient(Convert.ToDecimal(m_NoOfFirstPage), FloderState.None.ToString());//�л�����

                m_UCEmrInputNew = new DrectSoft.Core.MainEmrPad.New.UCEmrInput(m_Host.CurrentPatientInfo, m_Host, FloderState.None);
                m_UCEmrInputNew.SetVarData(m_Host);
                xtraTabPageEmrContent.Controls.Add(m_UCEmrInputNew);
                m_UCEmrInputNew.OnLoad();
                m_UCEmrInputNew.HideBar();
                m_UCEmrInputNew.Dock = DockStyle.Fill;
                m_IsLoadedEmrContentNew = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void FormApproveForMultiply_Load(object sender, EventArgs e)
        {
            CheckISApprove();

            this.ActiveControl = memoEditSuggestion;

            //���ù��ڻ�����˵�Tabҳ�Ƿ���ʾ edit by tj 2012-10-26
            if (!CommonObjects.IsNeedVerifyInConsultation)
            {
                groupControlApprove.Visible = false;
            }
        }

        private void CheckISApprove()
        {

            if (m_ConsultApplySn == "")
                ReadOnlyControl();

            string sql = string.Format("select a.stateid from consultapply a where a.consultapplysn = '{0}'",m_ConsultApplySn);

            string stateid = m_Host.SqlHelper.ExecuteDataTable(sql).Rows[0][0].ToString();

            if (stateid != Convert.ToString((int)ConsultStatus.WaitApprove))
            {
                ReadOnlyControl();
            }
                
        }
    }
}