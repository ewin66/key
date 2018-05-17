using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Common;

namespace DrectSoft.Core.Consultation
{
    public partial class UCConsultationApplyForMultiply : DevExpress.XtraEditors.XtraUserControl
    {
        private string m_NoOfFirstPage;
        private IEmrHost m_Host;
        private string m_ConsultApplySn = string.Empty;

        public UCConsultationApplyForMultiply()
        {
            try
            {
                InitializeComponent();
                memoEditSuggestion.Enabled = false;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Init(string noOfFirstPage, IEmrHost host, bool isNew, bool readOnly, string consultApplySn)
        {
            //this.Height = 520;
            m_NoOfFirstPage = noOfFirstPage;
            m_Host = host;
            m_ConsultApplySn = consultApplySn;
            InitInner(isNew, readOnly);
        }

        /// <summary>
        /// ���ó�ʼ������
        /// </summary>
        public void InitFocus()
        {
            UCApplyInfoForMultiple.InitFocus();
        }

        private void InitInner(bool isNew, bool readOnly)
        {
            UCPatientInfoForMultiple.Init(m_NoOfFirstPage, m_Host);
            UCApplyInfoForMultiple.Init(m_NoOfFirstPage, m_Host, isNew, readOnly, m_ConsultApplySn);
        }

        private void simpleButtonClear_Click(object sender, EventArgs e)
        {
            if (m_Host.CustomMessageBox.MessageShow("��ȷ��Ҫ�������������", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
            {
                UCApplyInfoForMultiple.Clear();
            }
        }

        /// <summary>
        /// �����¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                UCApplyInfoForMultiple.Save();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            if (m_Host.CustomMessageBox.MessageShow("��ȷ��Ҫ�˳���", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
            {
                Form form = this.FindForm();
                if (form != null)
                {
                    form.Close();
                }
            }
        }

        public void ReadOnlyControl()
        {
            UCApplyInfoForMultiple.ReadOnlyControl();

            panelControl1.Enabled = false;
            simpleButtonClear.Enabled = false;
            simpleButtonConfirm.Enabled = false;
            simpleButtonExit.Enabled = false;
        }

        /// <summary>
        /// Ĭ������²���ʾ�����Ϣ  ֻ�з����������ʾ�����Ϣ
        /// </summary>
        public void ShowApprove(string consultApplySn)
        {
            groupControlApprove.Visible = true;
            //this.Height = 640;
            InitApprove(consultApplySn);
            simpleButtonClear.Enabled = false;

            //��������,������˻����������� edit by tj 2012-10-29
            if (!CommonObjects.IsNeedVerifyInConsultation)
            {
                groupControlApprove.Visible = false;
            }
        }

        private void InitApprove(string consultApplySn)
        {
            DataSet ds = Dal.DataAccess.GetConsultationDataSet(consultApplySn, "20");//, Convert.ToString((int)ConsultType.More));
            DataTable dtConsultApply = ds.Tables[0];
            memoEditSuggestion.Text = dtConsultApply.Rows[0]["RejectReason"].ToString().Trim();
             
        }

        private void UCApplyInfoForMultiple_Load(object sender, EventArgs e)
        {
            //��������,������˻����������� edit by tj 2012-10-29
            if (!CommonObjects.IsNeedVerifyInConsultation)
            {
                groupControlApprove.Visible = false;
            }
        }
    }
}
