using DrectSoft.Core.EMR_NursingDocument.NursingDocuments;
using DrectSoft.Core.EMR_NursingDocument.PublicSet;
using DrectSoft.Core.EMR_NursingDocument.UserControls;
using DrectSoft.FrameWork.WinForm.Plugin;
using System;
using System.Data;
using System.Windows.Forms;

namespace DrectSoft.Core.EMR_NursingDocument
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
            //�˴���DataTable���ݵ���ȡ�������ֿ��Ƿ����л����˺������
            if (IsChagedPat)//����ת���� 
            {
                dt = MethodSet.GetRedactPatientInfoFrm("14", "", NoOfInpat);
            }
            else
            {
                dt = MethodSet.GetRedactPatientInfoFrm("14", "", MethodSet.CurrentInPatient.NoOfFirstPage.ToString());
            }
            //DataTable dt = MethodSet.GetRedactPatientInfoFrm("14", "", MethodSet.CurrentInPatient.NoOfFirstPage.ToString());
            if (dt.Rows.Count == 1)
            {
                //������ʱ��Ϊ�գ����ȡ��Ժʱ�䣬���������ʱ��Ϊ׼
                MethodSet.AdmitDate = string.IsNullOrEmpty(dt.Rows[0]["inwarddate"].ToString().Trim()) ?
                    dt.Rows[0]["AdmitDate"].ToString().Trim() : dt.Rows[0]["inwarddate"].ToString().Trim();

                MethodSet.OutHosDate = dt.Rows[0]["status"].ToString().Trim() == "1503" ? dt.Rows[0]["OutHosDate"].ToString().Trim() : "";
                MethodSet.PatID = dt.Rows[0]["PatID"].ToString();
                MethodSet.OutWardDate = dt.Rows[0]["status"].ToString().Trim() == "1502" ? dt.Rows[0]["OutWardDate"].ToString().Trim() : "";
                MethodSet.PatName = dt.Rows[0]["Name"].ToString().Trim();//������
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
                txtInpatName.Text = MethodSet.PatName;
                //�����Ĵ�����ʾ
                txtBedID.Text = MethodSet.BedID;
                //txtInpatName.Text = MethodSet.CurrentInPatient.Name;
                txtAge.Text = PatientInfo.Age;
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

            //dateEdit.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //����¼�����ս���ʱ�������Զ���ؼ����м���Ҫ��ʾ������������  2012��5��15�� 09:44:02
            //��������������
            string inputdate = dateEdit.Text;
            ucNursingRecordTable1.SetDaysAfterSurgery(inputdate, MethodSet.PatID);
            //ucNursingRecordTable1.SetDaysAfterSurgery(m_DaysAfterSurgery);
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
            ucNursingRecordTable1.SaveUCNursingRecordTable(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"), NoOfInpat);
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
                        MethodSet.App.CustomMessageBox.MessageShow("ѡ�����ڲ���С����Ժ���ڣ�������ѡ�����ڣ�", CustomMessageBoxKind.InformationOk);
                        //btnSave.Enabled = false;
                        dateEdit.DateTime = m_AdmitDate;
                        dateEdit.Focus();
                        return;
                    }

                    if (!string.IsNullOrEmpty(MethodSet.OutHosDate))
                    {
                        DateTime outHosDate = Convert.ToDateTime(MethodSet.OutHosDate);
                        if (Convert.ToDateTime(outHosDate.Date.ToString("yyyy-MM-dd 00:00:00"))
                             < Convert.ToDateTime(dateEdit.DateTime.Date.ToString("yyyy-MM-dd 00:00:00")))
                        {
                            MethodSet.App.CustomMessageBox.MessageShow("ѡ�����ڲ��ܴ��ڳ�Ժ���ڣ�������ѡ�����ڣ�", CustomMessageBoxKind.InformationOk);
                            dateEdit.DateTime = outHosDate;
                            dateEdit.Focus();
                            return;
                        }
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
                    MethodSet.App.CustomMessageBox.MessageShow("������ǰ��д��������ѡ�����ڣ�", CustomMessageBoxKind.InformationOk);
                    //btnSave.Enabled = false;
                    dateEdit.DateTime = Convert.ToDateTime(CurrDateTime);
                    dateEdit.Focus();
                    return;
                }


                ucNursingRecordTable1.GetNotesOfNursingInfo(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"), NoOfInpat);
                //ucNursingRecordTable1.OperateLookUp(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"));


                //����ѡ��������������������� edit by ywk 2012��5��15�� 09:43:56
                string inputdate = string.Format("{0:yyyy-MM-dd}", dateEdit.DateTime.Date.ToString("yyyy-MM-dd"));

                ucNursingRecordTable1.SetDaysAfterSurgery(inputdate, MethodSet.PatID);
                //סԺ����Ҳ��ʱ��ı� edit by ywk ����һ��������ʮ���� 20:48:22
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
        /// edit by ywk 2012��5��29�� 13:22:03
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