using DrectSoft.Common;
using DrectSoft.Common.Ctrs.DLG;
using DrectSoft.Common.Ctrs.FORM;
using DrectSoft.Common.Eop;
using DrectSoft.FrameWork.WinForm.Plugin;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrectSoft.Core.Consultation
{
    /// <summary>
    /// Ժ�ڻ���ϵͳ������
    /// </summary>
    public partial class FormMainConsultation : DevBaseForm
    {
        private IEmrHost m_App;
        private UCWaitApprove ucWaitApprove;
        private UCList ucList;
        private UCRecord ucRecord;
        private UCAudioDept ucAudioDept;

        #region ��ط���

        public FormMainConsultation()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ����
        /// MOdify by xlb
        /// Add try catch
        /// </summary>
        /// <param name="app"></param>
        public FormMainConsultation(IEmrHost app)
            : this()
        {
            try
            {
                m_App = app;

                #region ������˽���
                ucWaitApprove = new UCWaitApprove(m_App);
                ucWaitApprove.Location = new Point(-3, -2);//���Ͻ�����
                ucWaitApprove.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                xtraTabPageApproveList.Controls.Add(ucWaitApprove);
                ucWaitApprove.Dock = DockStyle.Fill;
                #endregion

                #region �����嵥����
                ucList = new UCList(m_App);
                ucList.Location = new Point(0, -2);
                ucList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                xtraTabPageConsultationList.Controls.Add(ucList);
                ucList.Dock = DockStyle.Fill;
                #endregion

                #region �����¼����
                ucRecord = new UCRecord(m_App);
                ucRecord.Location = new Point(0, -2);
                ucRecord.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                xtraTabPage3.Controls.Add(ucRecord);
                ucRecord.Dock = DockStyle.Fill;
                #endregion

                //�����¼�������Σ�����ʾ������嵥
                Employee emp = new Employee(m_App.User.Id);
                emp.ReInitializeProperties();
                //������Ϊ���쳣��Ϣ edit by ywk 
                if (string.IsNullOrEmpty(emp.Grade))
                {
                    emp.Grade = "0";
                }

                DoctorGrade grade = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), emp.Grade);
                if (grade == DoctorGrade.Chief/*����ҽʦ*/ || grade == DoctorGrade.AssociateChief/*f������ҽʦ*/)
                {
                    xtraTabPageApproveList.PageVisible = true;
                    XtraConfigAudio.PageVisible = true;//���û��������Ҳ��ʾ����  added by ywk 2012��3��14��18:23:05
                    #region ��������˽���
                    ucAudioDept = new UCAudioDept(m_App);
                    ucAudioDept.Location = new Point(0, -2);
                    ucAudioDept.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                    ucAudioDept.Dock = DockStyle.Fill;
                    XtraConfigAudio.Controls.Add(ucAudioDept);
                    #endregion
                }
                else
                {
                    XtraConfigAudio.PageVisible = false;//���μ���͸����μ�����������������б��򿪷�������
                    xtraTabPageApproveList.PageVisible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ���ƴ�����嵥����ʾ���
        /// </summary>
        /// <returns></returns>
        private bool SetXTabApproveList()
        {
            try
            {
                bool cansee = false;

                Employee emp = new Employee(m_App.User.Id);
                emp.ReInitializeProperties();
                DoctorGrade grade = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), emp.Grade);

                AuditLogic audiLogic = new AuditLogic(m_App, m_App.User.Id);
                string fuzenid = audiLogic.GetUser(m_App.User.Id);
                //if (!string.IsNullOrEmpty(fuzenid))//���������
                //{
                if (audiLogic.CanAudioConsult(m_App.User.Id, emp))//�����Ȩ��
                {
                    //if (m_App.User.Id == fuzenid)
                    //{
                    cansee = true;
                    //}
                }
                else
                {
                    cansee = false;
                }
                return cansee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ����¼�

        /// <summary>
        ///ע��������¼�
        /// Modify by xlb
        /// Add try catch
        /// 2013-03-20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMainConsultation_Load(object sender, EventArgs e)
        {
            try
            {
                #region ע��by xlb ��˽��濪�������� �������ҽʦ�����������ȼ�
                //xtraTabPageApproveList.PageVisible = false;
                //if (SetXTabApproveList())
                //{
                //    xtraTabPageApproveList.PageVisible = true;
                //}
                //else
                //{
                //    xtraTabPageApproveList.PageVisible = false;
                //}
                #endregion
                this.ActiveControl = ucList;
                //���ù��ڻ�����˵�Tabҳ�Ƿ���ʾ edit by tj 2012-10-26
                //δ�����������������б���������ҽʦ����
                if (!CommonObjects.IsNeedVerifyInConsultation)
                {
                    xtraTabPageApproveList.PageVisible = false;
                    XtraConfigAudio.PageVisible = false;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        private void xtraTabControlRecordList_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// tabҳ�л�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControlRecordList_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                this.xtraTabControlRecordList.SelectedTabPage.Controls[0].Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        #endregion
    }
}