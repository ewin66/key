using Consultation.NEW;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DrectSoft.Common;
using DrectSoft.Common.Ctrs.DLG;
using DrectSoft.Common.Eop;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DrectSoft.Core.Consultation
{
    public partial class UCWaitApprove : DevExpress.XtraEditors.XtraUserControl
    {
        private IEmrHost m_App;
        public UCWaitApprove()
        {
            InitializeComponent();
        }

        public UCWaitApprove(IEmrHost app)
            : this()
        {
            m_App = app;
            BindLookUpEditorData();
            InitGridControl();
            InitDateEdit();
            //Search();
            textEditName.Focus();
        }

        private void InitGridControl()
        {
            gridViewWaitApprove.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridViewWaitApprove.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewWaitApprove.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewWaitApprove.DoubleClick += new EventHandler(gridViewWaitApprove_DoubleClick);
        }

        private void InitDateEdit()
        {
            dateEditConsultDateBegin.EditValue = System.DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            dateEditConsultDateEnd.EditValue = System.DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void BindLookUpEditorData()
        {
            BindUrgency();
            //BindBed();
        }

        private DataTable GetConsultationData(string typeID, string param)
        {
            if (Dal.DataAccess.App == null)
            {
                Dal.DataAccess.App = m_App;
            }
            DataTable dataTableConsultationData = new DataTable();
            dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, param);
            return dataTableConsultationData;
        }

        /// <summary>
        /// У�鷽��
        /// Add by xlb 2013-03-13
        /// </summary>
        private bool ValidateDate(ref string message)
        {
            try
            {
                DateTime dateBegin = dateEditConsultDateBegin.DateTime.Date;
                DateTime dateEnd = dateEditConsultDateEnd.DateTime.Date;
                if (dateBegin > dateEnd)
                {
                    message = "��ʼʱ�䲻�ܴ��ڽ���ʱ��";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region �󶨻�������̶�
        private void BindUrgency()
        {
            lookUpEditorUrgency.Kind = WordbookKind.Sql;
            lookUpEditorUrgency.ListWindow = lookUpWindowUrgency;
            BindUrgencyWordBook(GetConsultationData("6", "66"));
        }

        private void BindUrgencyWordBook(DataTable dataTableData)
        {
            for (int i = 0; i < dataTableData.Columns.Count; i++)
            {
                if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "name")
                {
                    dataTableData.Columns[i].Caption = "������";
                }
            }

            Dictionary<string, int> colWidths = new Dictionary<string, int>();
            colWidths.Add("NAME", 100);
            SqlWordbook wordBook = new SqlWordbook("Urgency", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorUrgency.SqlWordbook = wordBook;
        }
        #endregion

        #region �󶨵�ǰ�����Ĵ�λ
        //private void BindBed()
        //{
        //    lookUpEditorBed.Kind = WordbookKind.Sql;
        //    lookUpEditorBed.ListWindow = lookUpWindowBed;
        //    BindBedWordBook(GetConsultationData("7", m_App.User.CurrentWardId));
        //}

        //private void BindBedWordBook(DataTable dataTableData)
        //{
        //    for (int i = 0; i < dataTableData.Columns.Count; i++)
        //    {
        //        if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "id")
        //        {
        //            dataTableData.Columns[i].Caption = "��λ��";
        //        }
        //    }

        //    Dictionary<string, int> colWidths = new Dictionary<string, int>();
        //    colWidths.Add("ID", 60);
        //    SqlWordbook wordBook = new SqlWordbook("Bed", dataTableData, "ID", "ID", colWidths, "ID");
        //    lookUpEditorBed.SqlWordbook = wordBook;
        //}
        #endregion


        /// <summary>
        /// ��ѯ�¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";

                if (!ValidateDate(ref message))
                {
                    MessageBox.Show(message);
                    return;
                }
                Search();
                DataTable dt = gridControlWaitApprove.DataSource as DataTable;
                if (null == dt || dt.Rows.Count == 0)
                {
                    MessageBox.Show("δ�ҵ���Ӧ��¼");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ��ѯ����
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        private void Search()
        {
            try
            {
                /* modify by wwj 2013-02-27
                string consultTimeBegin = dateEditConsultDateBegin.Text;
                string consultTimeEnd = dateEditConsultDateEnd.Text;
                string consultType = "";
                string urgency = lookUpEditorUrgency.CodeValue;
                string name = textEditName.Text;
                string patientSN = textEditPatientSN.Text;

                DataTable dt = Dal.DataAccess.GetConsultationData("21", consultTimeBegin, consultTimeEnd, consultType, urgency, name, patientSN, "", m_App.User.CurrentDeptId);
                //gridControlWaitApprove.DataSource = dt;

                //������ܿ��� δ���
                //ȡ�������
                Employee emp = new Employee(m_App.User.Id);
                emp.ReInitializeProperties();
                DoctorGrade grade = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), emp.Grade);
                AuditLogic audiLogic = new AuditLogic(m_App, m_App.User.Id);

                DataTable DtFilter = dt.Clone();

                if (audiLogic.CanAudioConsult(m_App.User.Id, emp))
                {
                    //if (m_App.User.Id == fuzenid)
                    //{
                    DataRow[] Lookrow = dt.Select(string.Format("  audituserid='{0}'", m_App.User.Id));
                    for (int i = 0; i < Lookrow.Length; i++)
                    {
                        DtFilter.ImportRow(Lookrow[i]);
                    }
                    this.gridControlWaitApprove.DataSource = DtFilter;
                    //}
                }
                //ֻ�����κ͸������ܿ���������嵥
                //string fuzenid = audiLogic.GetUser(m_App.User.Id);
                //if (!string.IsNullOrEmpty(fuzenid))//�������úõ������
                //{

                //}
                else//����Ϊ�գ�����Ҫȡ����ҽʦ
                {
                    //����ҽʦ��������ҽʦ
                    if (grade == DoctorGrade.Chief && grade == DoctorGrade.AssociateChief)
                    {
                        DataRow[] Lookrow = dt.Select(string.Format(" audituserid='{0}'", m_App.User.Id));
                        for (int i = 0; i < Lookrow.Length; i++)
                        {
                            DtFilter.ImportRow(Lookrow[i]);
                        }
                        this.gridControlWaitApprove.DataSource = DtFilter;
                    }
                }
                */

                DateTime consultTimeBegin = dateEditConsultDateBegin.DateTime;
                DateTime consultTimeEnd = dateEditConsultDateEnd.DateTime;
                string urgency = lookUpEditorUrgency.CodeValue;
                string name = textEditName.Text;
                string patientSN = textEditPatientSN.Text;

                //ֻ��������ܿ���δ��˵Ļ��������¼
                Employee emp = new Employee(m_App.User.Id);
                emp.ReInitializeProperties();
                DoctorGrade grade = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), emp.Grade);

                //��ȡ��ǰ��¼�˴���˵����л������뵥 Add By wwj 2013-02-27
                DataTable dt = Dal.DataAccess.GetUnAuditConsult(
                    consultTimeBegin, consultTimeEnd,
                    name, patientSN, urgency, m_App.User.Id, emp.Grade);

                this.gridControlWaitApprove.DataSource = dt;
                m_App.PublicMethod.ConvertGridDataSourceUpper(gridViewWaitApprove);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ˫���б��¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// edit by xlb 2013-03-08
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridViewWaitApprove_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //edit by Yanqiao.Cai 2012-11-05 ˫�������¼�(ֱ�ӷ���)
                GridHitInfo hitInfo = gridViewWaitApprove.CalcHitInfo(gridControlWaitApprove.PointToClient(Cursor.Position));
                if (hitInfo.RowHandle < 0)
                {
                    return;
                }

                if (gridViewWaitApprove.FocusedRowHandle >= 0)
                {
                    DataRow dr = gridViewWaitApprove.GetDataRow(gridViewWaitApprove.FocusedRowHandle);
                    if (dr != null)
                    {
                        string noOfFirstPage = dr["NoOfInpat"].ToString();
                        string consultTypeID = dr["ConsultTypeID"].ToString();
                        string consultApplySn = dr["ConsultApplySn"].ToString();

                        //if (consultTypeID == Convert.ToString((int)ConsultType.One))
                        //{
                        //    FormApproveForOne formApprove = new FormApproveForOne(noOfFirstPage, m_App, consultApplySn);
                        //    formApprove.StartPosition = FormStartPosition.CenterParent;
                        //    formApprove.ShowDialog();
                        //}
                        //else
                        //{
                        //FormApproveForMultiply formApprove = new FormApproveForMultiply(noOfFirstPage, m_App, consultApplySn);
                        //formApprove.StartPosition = FormStartPosition.CenterParent;
                        //formApprove.ApplyInfoReadOnly();
                        //formApprove.ShowDialog();
                        //}
                        FrmConsultForReview frmReview = new FrmConsultForReview(noOfFirstPage, m_App, consultApplySn, false);
                        if (frmReview == null)
                        {
                            return;
                        }
                        frmReview.StartPosition = FormStartPosition.CenterParent;
                        frmReview.ShowDialog();
                        Search();
                    }
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-22</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewWaitApprove_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                DS_Common.AutoIndex(e);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// �����¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-14</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
                Reset();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
        /// <summary>
        /// ���÷���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-14</date>
        /// </summary>
        private void Reset()
        {
            try
            {
                InitDateEdit();
                textEditName.Text = string.Empty;
                textEditPatientSN.Text = string.Empty;
                lookUpEditorUrgency.CodeValue = string.Empty;
                textEditName.Focus();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
