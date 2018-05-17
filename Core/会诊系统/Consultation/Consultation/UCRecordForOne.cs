using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using YidanSoft.FrameWork.WinForm.Plugin;

namespace YidanSoft.Core.Consultation
{
    public partial class UCRecordForOne : DevExpress.XtraEditors.XtraUserControl
    {
        private string m_NoOfFirstPage = string.Empty;
        private IYidanEmrHost m_Host;
        private string m_ConsultApplySn = string.Empty;

        public UCRecordForOne()
        {
            InitializeComponent();
        }

        public UCRecordForOne(string noOfFirstPage, IYidanEmrHost host, string consultApplySn)
            : this()
        {
            m_NoOfFirstPage = noOfFirstPage;
            m_Host = host;
            m_ConsultApplySn = consultApplySn;
            InitInner(false);
            RegisterEvent();
        }

        private void InitInner(bool isNew)
        {
            ucRecordResultForOne.Init(m_NoOfFirstPage, m_Host, isNew, false, m_ConsultApplySn);
            ucPatientInfoForOne.Init(m_NoOfFirstPage, m_Host);
            ucApplyInfoForOne.Init(m_NoOfFirstPage, m_Host, isNew, true, m_ConsultApplySn);

            InitBtnState();
        }

        private void RegisterEvent()
        {
            simpleButtonSave.Click += new EventHandler(simpleButtonSave_Click);
            simpleButtonComplete.Click += new EventHandler(simpleButtonComplete_Click);
        }

        void simpleButtonComplete_Click(object sender, EventArgs e)
        {
            ucRecordResultForOne.Save(ConsultStatus.RecordeComplete);
        }

        void simpleButtonSave_Click(object sender, EventArgs e)
        {
            if (!simpleButtonSave.Enabled)
                return;
            ucRecordResultForOne.Save(ConsultStatus.RecordeSave);
        }

        public void ReadOnlyControl()
        {
            simpleButtonSave.Visible = false;
            simpleButtonComplete.Visible = false;
            ucPatientInfoForOne.Location = new Point(ucRecordResultForOne.Location.X, ucRecordResultForOne.Location.Y + ucRecordResultForOne.Height + 8);
            ucApplyInfoForOne.Location   = new Point(ucPatientInfoForOne.Location.X, ucPatientInfoForOne.Location.Y + ucPatientInfoForOne.Height + 8);
            this.Height = ucApplyInfoForOne.Location.Y + ucApplyInfoForOne.Height + 10;
        }

        /// <summary>
        /// �жϻ��ﱣ������������ť״̬
        /// </summary>
        private void InitBtnState()
        {
            if (m_ConsultApplySn.Trim() == "")
                return;

            string applyDeptID = string.Empty;
            string consultationDeptID = string.Empty;

            //���������ŵ����ݿ��л�ȡ���ﵥ����������Լ�����������Ϣ
            string sql = string.Format(@"select apply.consultapplysn,
                                                    apply.noofinpat,
                                                    apply.applydept ApplyDeptID,
                                                    applydept.departmentcode ConsultDeptID,
                                                    recorddept.hospitalcode,
                                                    recorddept.departmentcode ConsultDeptID2,
                                                    apply.finishtime ConsultTime,
                                                    apply.stateid
                                            from consultapply apply
                                            left join ConsultApplyDepartment applydept on apply.consultapplysn =
                                                                                            applydept.consultapplysn
                                                                                        and applydept.valid = 1
                                            left join consultrecorddepartment recordDept on recordDept.Consultapplysn =
                                                                                            apply.consultapplysn
                                                                                        and recordDept.Valid = 1
                                            where apply.consultapplysn = '{0}'
                                                and apply.valid = 1
                                                and apply.consulttypeid = '6501';
                                    ",m_ConsultApplySn);

            DataTable dt = m_Host.SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            if (dt.Rows.Count == 0)
                return;

            applyDeptID = dt.Rows[0]["ApplyDeptID"].ToString();
            //���ʵ�ʻ������δ����ѡ����������   ���ʵ�ʻ��������ֵ ��������Ϊʵ�ʿ���
            if(dt.Rows[0]["ConsultDeptID2"].ToString()=="")
                consultationDeptID = dt.Rows[0]["ConsultDeptID"].ToString();
            else
                consultationDeptID = dt.Rows[0]["ConsultDeptID2"].ToString();

            InitBtnState(applyDeptID, consultationDeptID);
        }

        /// <summary>
        /// �жϻ��ﱣ������������ť״̬
        /// </summary>
        /// <param name="applyDeptID">�������</param>
        /// <param name="consultationDeptID">��������</param>
        private void InitBtnState(string applyDeptID, string consultationDeptID)
        {
            //��ȡϵͳ���������õĵ��ƻ������
            string config = m_Host.SqlHelper.ExecuteScalar("select value from appcfg where configkey = 'ConsultationConfig'", CommandType.Text).ToString();

            string[] configs = config.Split(',');
            //if (m_ConsultationEntity.StateID != Convert.ToString((int)ConsultStatus.WaitConsultation) && m_ConsultationEntity.StateID != Convert.ToString((int)ConsultStatus.RecordeSave))
            //    return;

            //���뷽�б���Ȩ��
            if (configs[0].ToString() == "1")
            {
                if (m_Host.User.CurrentDeptId == applyDeptID)
                    this.simpleButtonSave.Enabled = true;
            }
            //�������б���Ȩ��
            else if (configs[0].ToString() == "2")
            {
                if (m_Host.User.CurrentDeptId == consultationDeptID)
                    this.simpleButtonSave.Enabled = true;
            }
            //˫�����б���Ȩ��
            else if (configs[0].ToString() == "3")
            {
                if (m_Host.User.CurrentDeptId == applyDeptID || m_Host.User.CurrentDeptId == consultationDeptID)
                    this.simpleButtonSave.Enabled = true;
            }

            //���뷽�����Ȩ��
            if (configs[1].ToString() == "1")
            {
                if (m_Host.User.CurrentDeptId == applyDeptID)
                    this.simpleButtonComplete.Enabled = true;
            }
            //�����������Ȩ��
            else if (configs[1].ToString() == "2")
            {
                if (m_Host.User.CurrentDeptId == consultationDeptID)
                    this.simpleButtonComplete.Enabled = true;
            }
            //˫���������Ȩ��
            else if (configs[1].ToString() == "3")
            {
                if (m_Host.User.CurrentDeptId == applyDeptID || m_Host.User.CurrentDeptId == consultationDeptID)
                    this.simpleButtonComplete.Enabled = true;
            }

            //���������޸İ�ť����������ؼ�������
            if (!simpleButtonComplete.Enabled && !simpleButtonSave.Enabled)
            {
                ReadOnlyControl();
            }
        }

    }
}
