using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.FrameWork.WinForm;
using DrectSoft.Common.Ctrs.FORM;

namespace DrectSoft.Core.QCDeptReport
{
    public partial class FormQCDeptReport : DevBaseForm, DrectSoft.FrameWork.IStartPlugIn
    {
        #region ����
        
        
        /// <summary>
        /// ���������Ӧ�ó���ӿ�
        /// </summary>
        private IEmrHost m_App;
        /// <summary>
        /// ���������Ӧ�ó���ӿ�
        /// </summary>
        public IEmrHost App
        {
            get { return m_App; }
            set { m_App = value; }
        }

        /// <summary>
        /// δ�鵵������Ϣ
        /// </summary>
        private UCFail m_UCFail;

        /// <summary>
        /// �鵵������Ϣ
        /// </summary>
        private UCFiling m_UCFiling;

        /// <summary>
        /// ���Ҳ���ʧ�ֵ�ͳ��
        /// </summary>
        private UCDeductPoint m_UCDeductPoint;

        /// <summary>
        /// ���ұ���ʧ�ֵ���ϸ
        /// </summary>
        private UCDeductPointInfo m_UCDeductPointInfo;

        #endregion

        public FormQCDeptReport()
        {
            InitializeComponent();

        }



        protected virtual void InitApp(IEmrHost application)
        {
            try
            {
                App = application;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IStartPlugIn ��Ա

        public DrectSoft.FrameWork.IPlugIn Run(IEmrHost host)
        {
            PlugIn plg = new PlugIn(this.GetType().ToString(), this);
            m_App = host;

            return plg;
        }

        #endregion

        private void FormQCDeptReport_Load(object sender, EventArgs e)
        {
            BindInpatientFail();
        }

        /// <summary>
        /// ��ʼ�� δ�鵵������Ϣ
        /// </summary>
        private void BindInpatientFail()
        {
            m_UCFail = new UCFail(m_App);
            m_UCFail.Dock = DockStyle.Fill;
            panelControlFail.Controls.Add(m_UCFail);
        }

        private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Name == "TabPageFail")
            {
                if (panelControlFail.Controls.Count > 1)
                    return;
                m_UCFail = new UCFail(m_App);
                m_UCFail.Dock = DockStyle.Fill;
                panelControlFail.Controls.Add(m_UCFail);
            
            }
            else if (e.Page.Name == "TabPageFiling")
            {
                if (panelControlFiling.Controls.Count > 1)
                    return;
                m_UCFiling = new UCFiling(m_App);
                m_UCFiling.Dock = DockStyle.Fill;
                panelControlFiling.Controls.Add(m_UCFiling);

            }

            else if (e.Page.Name == "TabPageDeductPoint")
            {
                if (panelControlDeductPoint.Controls.Count > 1)
                    return;
                m_UCDeductPoint = new UCDeductPoint(m_App);
                m_UCDeductPoint.Dock = DockStyle.Fill;
                panelControlDeductPoint.Controls.Add(m_UCDeductPoint);
            }
            else if (e.Page.Name == "TabPageDeductPointInfo")
            {
                if (panelControlDeductPointInfo.Controls.Count > 1)
                    return;
                m_UCDeductPointInfo = new UCDeductPointInfo(m_App);
                m_UCDeductPointInfo.Dock = DockStyle.Fill;
                panelControlDeductPointInfo.Controls.Add(m_UCDeductPointInfo);
            }
        }

    }
}