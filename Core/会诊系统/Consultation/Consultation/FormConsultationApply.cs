/*
 * 
 * ���ܣ������������
 * 
 * ���ã�ҽʦ����վ
 * 
 */
using System;
using System.Windows.Forms;
using DrectSoft.Common;
using DrectSoft.Service;
using DrectSoft.Core.MainEmrPad;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Emr.Util;

namespace DrectSoft.Core.Consultation
{
    public partial class FormConsultationApply : DevExpress.XtraEditors.XtraForm
    {
        private string m_ApplySN = string.Empty;
        private string m_NoOfFirstPage;
        private IEmrHost m_Host;

        public FormConsultationApply()
        {
            InitializeComponent();
        }

        public FormConsultationApply(string noOfFirstPage, IEmrHost host, bool isNew)
            : this()
        {
            m_NoOfFirstPage = noOfFirstPage;
            m_Host = host;
            InitInner(isNew);
        }

        public FormConsultationApply(string noOfFirstPage, IEmrHost host, string applySN)
            : this()
        {
            m_NoOfFirstPage = noOfFirstPage;
            m_Host = host;
            m_ApplySN = applySN;
            InitInner(false);
        }

        private void InitInner(bool isNew)
        {
            ApplyForMultiply.Init(m_NoOfFirstPage, m_Host, isNew, false, m_ApplySN);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPageEmrContent)
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
            m_UCEmrInput = new UCEmrInput();
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

        private void FormConsultationApply_Load(object sender, EventArgs e)
        {
            ApplyForMultiply.InitFocus();
            ApplyForMultiply.ActiveControl = ApplyForMultiply.Controls["UCApplyInfoForMultiple"];
        }

        /// <summary>
        /// Ĭ������²���ʾ�����Ϣ  ֻ�з����������ʾ�����Ϣ
        /// </summary>
        public void ShowApprove(string consultApplySn)
        {
            //this.Height = 700;
            ApplyForMultiply.ShowApprove(consultApplySn);
        }

        /// <summary>
        /// ֻ��
        /// </summary>
        public void ReadOnlyControl()
        {
            ApplyForMultiply.ReadOnlyControl();
        }
    }
}