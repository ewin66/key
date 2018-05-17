using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DrectSoft.Core.NursingDocuments.UserControls;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Core.NursingDocuments.PublicSet;
using DevExpress.XtraBars;
using DrectSoft.Wordbook;

namespace DrectSoft.Core.NursingDocuments
{
    public partial class NursingRecord : DevExpress.XtraEditors.XtraForm
    {

        string[] m_DaysAfterSurgery;


        /// <summary>
        /// NursingDocuments���̵��øù��캯��
        /// </summary>
        public NursingRecord()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��NursingDocuments���̵��øô��壬�øù��캯��
        /// </summary>
        /// <param name="App">Ӧ�ó������ӿ�</param>
        public NursingRecord(IEmrHost App)
        {
            InitializeComponent();
            m_DaysAfterSurgery = null;
            MethodSet.App = App;
        }

        public DialogResult ShowNursingRecord()
        {
            return ShowDialog();

        }

        /// <summary>
        /// ģ̬���ô��ڣ���
        /// </summary>
        /// <param name="DaysAfterSurgery"></param>
        /// <returns></returns>
        public DialogResult ShowNursingRecord(string[] DaysAfterSurgery)
        {
            m_DaysAfterSurgery = DaysAfterSurgery;
            return ShowDialog();

        }

        /// <summary>
        /// ��ʼ�����˻�����Ϣ
        /// </summary>
        private void InitInpatInfo()
        {
            DataTable dt = null;
            if (IsChagedPat)//����ת���� 
            {
                dt = MethodSet.GetRedactPatientInfoFrm("14", "", NoOfInpat);
            }
            else
            {
                dt = MethodSet.GetRedactPatientInfoFrm("14", "", MethodSet.CurrentInPatient.NoOfFirstPage.ToString());
            }
            if (dt.Rows.Count == 1)
            {
                MethodSet.AdmitDate = dt.Rows[0]["AdmitDate"].ToString().Trim();
                MethodSet.PatName = dt.Rows[0]["Name"].ToString().Trim();//������
                MethodSet.OutHosDate = dt.Rows[0]["OutHosDate"].ToString().Trim();
                MethodSet.PatID = dt.Rows[0]["PatID"].ToString();
                MethodSet.OutHosDate = dt.Rows[0]["OutWardDate"].ToString().Trim();
                MethodSet.BedID = dt.Rows[0]["OutBed"].ToString().Trim();//������
            }
        }

        private void NursingRecord_Load(object sender, EventArgs e)
        {
            MethodSet.DaysAfterSurgery = m_DaysAfterSurgery;

            if (MethodSet.CurrentInPatient != null)
            {
                InitInpatInfo();
                txtPatID.Text = MethodSet.PatID;
                //txtInpatName.Text = MethodSet.CurrentInPatient.Name;
                txtInpatName.Text = MethodSet.PatName;
                //�����Ĵ�����ʾ
                txtBedID.Text = MethodSet.BedID;
            }
            else
            {
                btnSave.Enabled = false;
            }

            ucNursingRecordTable1.InitForm();

            if (IsChagedPat)//����ת����
            {
                dateEdit.Text = InputDate;
            }
            else
            {
                dateEdit.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }
            //��������������
            string inputdate = dateEdit.Text;
            ucNursingRecordTable1.SetDaysAfterSurgery(inputdate, MethodSet.PatID);
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ucNursingRecordTable1.SaveUCNursingRecordTable(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"),NoOfInpat);
        }


        private void dateEdit_DateTimeChanged(object sender, EventArgs e)
        {

            if (MethodSet.CurrentInPatient != null)
            {
                //btnSave.Enabled = true;

                string CurrDateTime = MethodSet.GetCurrServerDateTime.Date.ToString("yyyy-MM-dd 00:00:01");

                int Day = Convert.ToDateTime(CurrDateTime).
                    Subtract(Convert.ToDateTime(dateEdit.DateTime.Date.ToString("yyyy-MM-dd 00:00:01"))).Days + 1;

                btnSave.Enabled = (Day <= ucNursingRecordTable1.DayOfModify || ucNursingRecordTable1.DayOfModify == -1) ? true : false;

                //�жϷ�������ǰ�����Ƿ�С����Ժ����
                if (!string.IsNullOrEmpty(MethodSet.AdmitDate))
                {
                    DateTime m_AdmitDate = Convert.ToDateTime(MethodSet.AdmitDate);
                    if (Convert.ToDateTime(m_AdmitDate.Date.ToString("yyyy-MM-dd 00:00:00"))
                        > Convert.ToDateTime(dateEdit.DateTime.Date.ToString("yyyy-MM-dd 00:00:00")))
                    {
                        btnSave.Enabled = false;
                    }
                }

                ////�ж�ѡ�������Ƿ񳬳���������2012��5��9��22:50:00 �����޸�
                //if (!string.IsNullOrEmpty(MethodSet.OutHosDate))
                //{
                //    DateTime m_OutWardDate = Convert.ToDateTime(MethodSet.OutWardDate);
                //    if (Convert.ToDateTime(m_OutWardDate.Date.ToString("yyyy-MM-dd 00:00:00"))
                //        < Convert.ToDateTime(dateEdit.DateTime.Date.ToString("yyyy-MM-dd 00:00:00")))
                //    {
                //        btnSave.Enabled = false;
                //    }
                //}

                //�ж�ѡ�������Ƿ񳬳���������ǰ��ǰ
                if (Convert.ToDateTime(CurrDateTime) < Convert.ToDateTime(dateEdit.DateTime.Date.ToString("yyyy-MM-dd 00:00:00")))
                {
                    btnSave.Enabled = false;
                }

                //�˴�Ϊ���ز��˵�����¼����Ϣ�����ڲ�����ҳ���Ҫ�ж����Ƿ��Ѿ��л��˲��ˣ�
                ucNursingRecordTable1.GetNotesOfNursingInfo(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"), NoOfInpat);
                //ucNursingRecordTable1.OperateLookUp(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"));
                //סԺ����Ҳ��ʱ��ı� edit by ywk 2012��5��18�� 10:16:03
                string inputdate = string.Format("{0:yyyy-MM-dd}", dateEdit.DateTime.Date.ToString("yyyy-MM-dd"));

                ucNursingRecordTable1.SetDaysAfterSurgery(inputdate,MethodSet.PatID);
                ucNursingRecordTable1.SetDayInHospital(inputdate);
            }
        }

        private void barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string temp = e.Item.Caption.Trim();
            ucNursingRecordTable1.ActivateTextEditFocus();
            try
            {
                SendKeys.Send(temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ����״̬��Ϣά��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PaientStatus paientS = new PaientStatus(MethodSet.PatID);
            paientS.StartPosition = FormStartPosition.CenterParent;//���������ڸ������м� 
            paientS.ShowDialog();
        }

        public bool IsChagedPat = false;//�����ж��Ƿ��ѡ���˴�����ת
        public string NoOfInpat = string.Empty;//���ڴ�Ų�����ҳ���
        public string InputDate = string.Empty;//���ڴ����תǰ������
        public string MyPatID = string.Empty;//���ڴ�PATID
        /// <summary>
        /// �л�����
        /// edit by ywk 2012��5��29�� 12:06:54
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkchangePat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePatient chageP = new ChangePatient(MethodSet.App, MethodSet.App.User);
            chageP.StartPosition = FormStartPosition.CenterParent;//���������ڸ������м� 
            InputDate = dateEdit.DateTime.Date.ToString("yyyy-MM-dd");
            if (chageP.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IsChagedPat = true;
                NoOfInpat = chageP.NOOfINPAT;
                //MessageBox.Show(chageP.NOOfINPAT);
                NursingRecord_Load(null, null);
                dateEdit_DateTimeChanged(null, null);
            }
        }


    }
}