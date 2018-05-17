using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.Common.Eop;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Core.RedactPatientInfo;

namespace DrectSoft.Core.Consultation
{
    public partial class UCPatientInfo : DevExpress.XtraEditors.XtraUserControl
    {
        private string m_NoOfFirstPage;
        private IEmrHost m_Host;

        public UCPatientInfo()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.Paint += new PaintEventHandler(UCPatientInfo_Paint);
            }
        }

        void UCPatientInfo_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString("������Ϣ" + this.GetType().Namespace + "." + this.GetType().Name, this.Font, Brushes.Black, new RectangleF(0f, 0f, this.Width, this.Height), sf);
            }
        }

        public void Init(string noOfFirstPage, IEmrHost host)
        {
            m_NoOfFirstPage = noOfFirstPage;
            m_Host = host;
            Dal.DataAccess.App = m_Host;
            InitInner();
        }

        private void InitInner()
        {
            DataTable dt = Dal.DataAccess.GetRedactPatientInfoFrm("14", m_NoOfFirstPage);

            if (dt.Rows.Count > 0)
            {
                textEditName.Text = dt.Rows[0]["NAME"].ToString().Trim();
                textEditPatientSN.Text = dt.Rows[0]["PatID"].ToString().Trim().Substring(4);//���ε�ǰ4��0
                textEditGender.Text = dt.Rows[0]["Gender"].ToString().Trim();
                textEditAge.Text = dt.Rows[0]["AgeStr"].ToString().Trim();
                textEditBedCode.Text = dt.Rows[0]["OutBed"].ToString().Trim();
                textEditDepartment.Text = dt.Rows[0]["OutHosDeptName"].ToString().Trim();
                textEditMarriage.Text = dt.Rows[0]["Marriage"].ToString().Trim();
                textEditJob.Text = dt.Rows[0]["JobName"].ToString().Trim();
            }
        }

        private void simpleButtonInpatientInfo_Click(object sender, EventArgs e)
        {
            //���ò�����Ϣ����ά������
            if (m_NoOfFirstPage == null || m_NoOfFirstPage=="") 
                return;
            //to do ���ò���������Ϣ����
            //BasePatientInfo info = new BasePatientInfo(m_Host);
            //info.ShowCurrentPatInfo(m_NoOfFirstPage);
            XtraFormPatientInfo patientInfo = new XtraFormPatientInfo(m_Host, m_NoOfFirstPage);
            patientInfo.ShowDialog();
        }

        public void ReadOnlyControl()
        {
            textEditName.Enabled = false;
            textEditPatientSN.Enabled = false;
            textEditGender.Enabled = false;
            textEditAge.Enabled = false;
            textEditBedCode.Enabled = false;
            textEditDepartment.Enabled = false;
            textEditMarriage.Enabled = false;
            textEditJob.Enabled = false;
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
                throw new Exception(ex.Message);
            }
        }
    }
}
