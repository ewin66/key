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
using DrectSoft.Common.Eop;
using DrectSoft.Service;
using DrectSoft.Common.Ctrs.FORM;

namespace DrectSoft.Core.NursingDocuments
{
    public partial class NursingRecord : DevBaseForm
    {

        string[] m_DaysAfterSurgery;
        Inpatient m_currInpatient;
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
        public NursingRecord(IEmrHost App, Inpatient currInpatient )
        {
            m_currInpatient = currInpatient;
            InitializeComponent();
            ucNursingRecordTable1.currInpatient = m_currInpatient;
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
        private void InitInpatInfo(Inpatient currInpatient)
        {
            DataTable dt = null;
            //�˴���DataTable���ݵ���ȡ�������ֿ��Ƿ����л����˺������
            if (IsChagedPat)//����ת���� 
            {
                dt = MethodSet.GetRedactPatientInfoFrm("14", "", NoOfInpat);
            }
            else
            {
                NoOfInpat = currInpatient.NoOfFirstPage.ToString();
                dt = MethodSet.GetRedactPatientInfoFrm("14", "", currInpatient.NoOfFirstPage.ToString());
            }
            //DataTable dt = MethodSet.GetRedactPatientInfoFrm("14", "", MethodSet.CurrentInPatient.NoOfFirstPage.ToString());
            if (dt.Rows.Count == 1)
            {
                //������ʱ��Ϊ�գ����ȡ��Ժʱ�䣬���������ʱ��Ϊ׼
                MethodSet.AdmitDate = string.IsNullOrEmpty(dt.Rows[0]["inwarddate"].ToString().Trim()) ?
                    dt.Rows[0]["AdmitDate"].ToString().Trim() : dt.Rows[0]["inwarddate"].ToString().Trim();
                //PatientInfo.IsBaby
                //if (PatientInfo.IsBaby == "1")//�����Ӥ�� add by ywk 2012��11��22��19:59:18 
                //{
                //    MethodSet.PatID = PublicSet.MethodSet.GetPatData(PatientInfo.Mother).Rows[0]["Patid"].ToString();
                //}
                //else
                //{
                    MethodSet.PatID = dt.Rows[0]["PatID"].ToString();
                //}

                MethodSet.OutHosDate = dt.Rows[0]["status"].ToString().Trim() == "1503" ? dt.Rows[0]["OutHosDate"].ToString().Trim() : "";
                //MethodSet.PatID = dt.Rows[0]["PatID"].ToString();
                MethodSet.OutWardDate = dt.Rows[0]["status"].ToString().Trim() == "1502" ? dt.Rows[0]["OutWardDate"].ToString().Trim() : "";
                MethodSet.PatName = dt.Rows[0]["Name"].ToString().Trim();//������
                MethodSet.BedID = dt.Rows[0]["OutBed"].ToString().Trim();//������
                MethodSet.Age = dt.Rows[0]["AGESTR"].ToString().Trim();//������
                //add by ywk ����һ�������¶�ʮ���� 15:21:51  
                MethodSet.NoOfinPat = dt.Rows[0]["NOOFINPAT"].ToString().Trim();//�����Ĳ��˵���ҳ���

                MethodSet.RecordNoofinpat = dt.Rows[0]["NOOFRECORD"].ToString().Trim();//�����Ĳ��˵���ҳ���
            }
        }

        public void NursingRecord_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshDate(m_currInpatient);
                dateEdit_DateTimeChanged(null, null);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex.Message);
            }
        }

        public void RefreshDate(Inpatient currInpatient)
        {
            try
            {
                DataTable dt = MethodSet.GetRedactPatientInfoFrm("14", "", m_currInpatient.NoOfFirstPage.ToString());
                MethodSet.DaysAfterSurgery = m_DaysAfterSurgery;
                if (currInpatient != null)
                {
                    InitInpatInfo(currInpatient);
                    if (dt.Rows[0]["isbaby"].ToString().Equals("1"))//�����Ӥ��
                    {
                        txtPatID.Text = PublicSet.MethodSet.GetPatData(dt.Rows[0]["mother"].ToString()).Rows[0]["Patid"].ToString();
                    }
                    else
                    {
                        txtPatID.Text = MethodSet.PatID;
                    }

                    //txtPatID.Text = MethodSet.PatID;
                    txtInpatName.Text = MethodSet.PatName;
                    //�����Ĵ�����ʾ
                    txtBedID.Text = MethodSet.BedID;
                    //txtInpatName.Text = MethodSet.CurrentInPatient.Name;
                    //txtAge.Text = PatientInfo.Age;
                    txtAge.Text = MethodSet.Age;//wyt

                    #region ��ע��
                //MethodSet.DaysAfterSurgery = m_DaysAfterSurgery;
                //if (currInpatient != null)
                //{
                //    InitInpatInfo(currInpatient);
                //    if (patientInfo.IsBaby == "1")//�����Ӥ�� add by ywk 2012��11��22��19:59:18 
                //    {
                //        txtPatID.Text = PublicSet.MethodSet.GetPatData(patientInfo.Mother).Rows[0]["Patid"].ToString();
                //    }
                //    else
                //    {
                //        txtPatID.Text = MethodSet.PatID;
                //    }

                //    //txtPatID.Text = MethodSet.PatID;
                //    txtInpatName.Text = MethodSet.PatName;
                //    //�����Ĵ�����ʾ
                //    txtBedID.Text = MethodSet.BedID;
                //    //txtInpatName.Text = MethodSet.CurrentInPatient.Name;
                //    //txtAge.Text = PatientInfo.Age;
                    //    txtAge.Text = MethodSet.Age;//wyt
                    #endregion
                }
                else
                {
                    btnSave.Enabled = false;
                }

                ucNursingRecordTable1.InitForm();

                //add by cyq 2013-03-05
                dateEdit.DateTimeChanged -= new EventHandler(dateEdit_DateTimeChanged);
                if (IsChagedPat)//����ת����
                {
                    dateEdit.Text = InputDate;
                }
                else
                {//add by cyq 2013-03-05
                    DataTable inps = DS_SqlService.GetInpatientByID((int)currInpatient.NoOfFirstPage);
                    if (null != inps && inps.Rows.Count == 1)
                    {
                        if (inps.Rows[0]["status"].ToString() == "1502" || inps.Rows[0]["status"].ToString() == "1503")
                        {
                            dateEdit.Text = DateTime.Parse(inps.Rows[0]["outhosdate"].ToString()).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            dateEdit.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        }
                    }
                    else
                    {
                        dateEdit.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    }
                }
                //add by cyq 2013-03-05
                dateEdit.DateTimeChanged += new EventHandler(dateEdit_DateTimeChanged);

                //dateEdit.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                //����¼�����ս���ʱ�������Զ���ؼ����м���Ҫ��ʾ������������  2012��5��15�� 09:44:02
                //��������������
                string inputdate = dateEdit.Text;
                //���±��Ѿ�ȡ�õ���ʵ����ҳ��� add by ywk 2013-4-8 16:10:04 
                ucNursingRecordTable1.SetDaysAfterSurgery(inputdate, currInpatient.NoOfFirstPage.ToString());
                ucNursingRecordTable1.CurrentOperTime = DateTime.Parse(inputdate);//add by wyt

                //ucNursingRecordTable1.SetDaysAfterSurgery(m_DaysAfterSurgery);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
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
            try
            {
                if (string.IsNullOrEmpty(this.dateEdit.Text))
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("¼�����ڲ���Ϊ��");
                    this.dateEdit.Focus();
                    return;
                }
                if (ucNursingRecordTable1.SaveUCNursingRecordTable(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"), NoOfInpat))
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ʱ��仯�¼�
        /// edit by Yanqiao.Cai 2012-11-14
        /// 1��add try ... catch
        /// 2���Ż���ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dateEdit_DateTimeChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_currInpatient != null)
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
                            DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("¼�����ڲ���С����Ժ����");
                            //btnSave.Enabled = false;
                            //add by cyq 2013-03-05
                            dateEdit.DateTimeChanged -= new EventHandler(dateEdit_DateTimeChanged);
                            dateEdit.DateTime = m_AdmitDate;
                            //add by cyq 2013-03-05
                            dateEdit.DateTimeChanged += new EventHandler(dateEdit_DateTimeChanged);
                            dateEdit.Focus();
                            return;
                        }

                        if (!string.IsNullOrEmpty(MethodSet.OutHosDate))
                        {
                            DateTime outHosDate = Convert.ToDateTime(MethodSet.OutHosDate);
                            if (Convert.ToDateTime(outHosDate.Date.ToString("yyyy-MM-dd 00:00:00"))
                                 < Convert.ToDateTime(dateEdit.DateTime.Date.ToString("yyyy-MM-dd 00:00:00")))
                            {
                                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("¼�����ڲ��ܴ��ڳ�Ժ����");
                                //add by cyq 2013-03-05
                                dateEdit.DateTimeChanged -= new EventHandler(dateEdit_DateTimeChanged);
                                dateEdit.DateTime = outHosDate;
                                //add by cyq 2013-03-05
                                dateEdit.DateTimeChanged += new EventHandler(dateEdit_DateTimeChanged);
                                dateEdit.Focus();
                                return;
                            }
                        }
                    }

                    #region ��ע��
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
                    #endregion

                    //�ж�ѡ�������Ƿ񳬳���������ǰ��ǰ
                    if (Convert.ToDateTime(CurrDateTime) < Convert.ToDateTime(dateEdit.DateTime.Date.ToString("yyyy-MM-dd 00:00:00")))
                    {
                        DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("¼�����ڲ��ܴ��ڵ�ǰ����");
                        //btnSave.Enabled = false;
                        //add by cyq 2013-03-05
                        dateEdit.DateTimeChanged -= new EventHandler(dateEdit_DateTimeChanged);
                        dateEdit.DateTime = Convert.ToDateTime(CurrDateTime);
                        //add by cyq 2013-03-05
                        dateEdit.DateTimeChanged += new EventHandler(dateEdit_DateTimeChanged);
                        dateEdit.Focus();
                        return;
                    }


                    ucNursingRecordTable1.GetNotesOfNursingInfo(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"), NoOfInpat,m_currInpatient);
                    //ucNursingRecordTable1.OperateLookUp(dateEdit.DateTime.Date.ToString("yyyy-MM-dd"));


                    //����ѡ��������������������� edit by ywk 2012��5��15�� 09:43:56
                    string inputdate = string.Format("{0:yyyy-MM-dd}", dateEdit.DateTime.Date.ToString("yyyy-MM-dd"));
                    //���±��Ѿ�ȡ�õ���ʵ����ҳ��� add by ywk 2013-4-8 16:10:04 
                    ucNursingRecordTable1.SetDaysAfterSurgery(inputdate, m_currInpatient.NoOfFirstPage.ToString());
                    ucNursingRecordTable1.CurrentOperTime = DateTime.Parse(inputdate);  //add by wyt
                    //סԺ����Ҳ��ʱ��ı� edit by ywk ����һ��������ʮ���� 20:48:22
                    ucNursingRecordTable1.SetDayInHospital(inputdate);
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        private void barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string temp = e.Item.Caption.Trim();
            //add by ywk 2012��11��8��10:48:36
            UCTextGroupBox ucText = new UCTextGroupBox();
            ucText.ActivateTextEditFocus();
            if (!ucText.ISFocused)
            {
                ucNursingRecordTable1.ActivateTextEditFocus();
            }


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
            //PaientStatus paientS = new PaientStatus(m_currInpatient);
            PaientStatus paientS = new PaientStatus(MethodSet.NoOfinPat,MethodSet.RecordNoofinpat);
            paientS.StartPosition = FormStartPosition.CenterParent;//���������ڸ������м� 
            paientS.TopMost = true;
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
            chageP.TopMost = true;
            if (chageP.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IsChagedPat = true;
                NoOfInpat = chageP.NOOfINPAT;
                //MessageBox.Show(chageP.NOOfINPAT);
                NursingRecord_Load(null, null);
                dateEdit_DateTimeChanged(null, null);
            }
        }

        /// <summary>
        /// �л�������ʾ
        /// </summary>
        /// <param name="hasVisible"></param>
        public void SetQieHuanInpatVisible(bool hasVisible)
        {
            linkchangePat.Visible = hasVisible;
        }
    }
}