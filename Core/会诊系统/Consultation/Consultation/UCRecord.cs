using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;
using DrectSoft.Common;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DrectSoft.Common.Eop;
using DrectSoft.Core.Consultation.NEW;
using DrectSoft.Common.Ctrs.DLG;

namespace DrectSoft.Core.Consultation
{
    public partial class UCRecord : DevExpress.XtraEditors.XtraUserControl
    {
        private IEmrHost m_App;
        public UCRecord()
        {
            InitializeComponent();
        }

        public UCRecord(IEmrHost app)
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
            gridViewList.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridViewList.OptionsSelection.EnableAppearanceFocusedCell = false; ;
            gridViewList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewList.DoubleClick += new EventHandler(gridViewList_DoubleClick);
        }

        private void InitDateEdit()
        {
            dateEditConsultDateBegin.EditValue = System.DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            dateEditConsultDateEnd.EditValue = System.DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void Search()
        {
            //string consultTimeBegin = dateEditConsultDateBegin.Text;
            //string consultTimeEnd = dateEditConsultDateEnd.Text;
            //string consultType = "";
            //string urgency = lookUpEditorUrgency.CodeValue;
            //string name = textEditName.Text;
            //string patientSN = textEditPatientSN.Text;
            //string bedCode = lookUpEditorBed.CodeValue;

            //DataTable dt = Dal.DataAccess.GetConsultationData("23", consultTimeBegin, consultTimeEnd, consultType, urgency, name, patientSN, bedCode, m_App.User.CurrentDeptId);
            //gridControlList.DataSource = dt;

            //m_App.PublicMethod.ConvertGridDataSourceUpper(gridViewList);
            SearchRecord();
        }

        /// <summary>
        /// ��ѯ��¼  ��ȡ��ϵͳ��¼����صĴ���������¼����ļ�¼  Ȼ���ڵ����ʾ�Ļ����¼ʱ�жϻ����������д��� Add by wwj 2013-02-27
        /// </summary>
        private void SearchRecord()
        {
            try
            {
                DateTime consultTimeBegin = dateEditConsultDateBegin.DateTime;
                DateTime consultTimeEnd = dateEditConsultDateEnd.DateTime;
                string urgency = lookUpEditorUrgency.CodeValue;
                string name = textEditName.Text;
                string patientSN = textEditPatientSN.Text;
                string bedCode = lookUpEditorBed.CodeValue;

                Employee emp = new Employee(m_App.User.Id);
                emp.ReInitializeProperties();
                DoctorGrade grade = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), emp.Grade);

                DataTable dt = Dal.DataAccess.GetWaitConsult(
                   consultTimeBegin,consultTimeEnd,
                    name, patientSN, urgency, bedCode, m_App.User.Id, emp.Grade);

                gridControlList.DataSource = dt;
                m_App.PublicMethod.ConvertGridDataSourceUpper(gridViewList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindLookUpEditorData()
        {
            BindUrgency();
            BindBed();
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
            colWidths.Add("NAME", 120);
            SqlWordbook wordBook = new SqlWordbook("Urgency", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
            lookUpEditorUrgency.SqlWordbook = wordBook;
        }
        #endregion

        #region �󶨵�ǰ�����Ĵ�λ
        private void BindBed()
        {
            lookUpEditorBed.Kind = WordbookKind.Sql;
            lookUpEditorBed.ListWindow = lookUpWindowBed;
            BindBedWordBook(GetConsultationData("7", m_App.User.CurrentWardId));
        }

        private void BindBedWordBook(DataTable dataTableData)
        {
            for (int i = 0; i < dataTableData.Columns.Count; i++)
            {
                if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "id")
                {
                    dataTableData.Columns[i].Caption = "��λ��";
                }
            }

            Dictionary<string, int> colWidths = new Dictionary<string, int>();
            colWidths.Add("ID", 120);
            SqlWordbook wordBook = new SqlWordbook("Bed", dataTableData, "ID", "ID", colWidths, "ID");
            lookUpEditorBed.SqlWordbook = wordBook;
        }
        #endregion

        /// <summary>
        /// У�鷽��
        /// Add by  xlb 2013-03-13
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateDate(ref string message)
        {
            try
            {
                DateTime dateBegin = dateEditConsultDateBegin.DateTime.Date;
                DateTime dateEnd = dateEditConsultDateEnd.DateTime.Date;
                if (dateBegin > dateEnd)//������ʼʱ����ڽ���ʱ��
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
        /// ��ѯ�¼�
        /// Edit by xlb 2013-03-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonSearch_Click(object sender, EventArgs e)
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
                if (((DataTable)gridControlList.DataSource).Rows.Count == 0 
                    || gridControlList.DataSource == null)
                {
                    MessageBox.Show("û�з�������������");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
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
        void gridViewList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //edit by Yanqiao.Cai 2012-11-05 ˫�������¼�(ֱ�ӷ���)
                GridHitInfo hitInfo = gridViewList.CalcHitInfo(gridControlList.PointToClient(Cursor.Position));
                if (hitInfo.RowHandle < 0)
                {
                    return;
                }

                if (gridViewList.FocusedRowHandle >= 0)
                {
                    DataRow dr = gridViewList.GetDataRow(gridViewList.FocusedRowHandle);
                    if (dr != null)
                    {
                        string noOfFirstPage = dr["NoOfInpat"].ToString();
                        string consultTypeID = dr["ConsultTypeID"].ToString();
                        string consultApplySn = dr["ConsultApplySn"].ToString();

                        //if (consultTypeID == Convert.ToString((int)ConsultType.One))
                        //{
                        //    FormRecordForOne formRecordForOne = new FormRecordForOne(noOfFirstPage, m_App, consultApplySn);
                        //    formRecordForOne.StartPosition = FormStartPosition.CenterParent;
                        //    formRecordForOne.ShowDialog();
                        //}
                        //else
                        //{
                        //FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                        //formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
                        //if (dr["APPLYUSER"].ToString() != m_App.User.Id)
                        //    formRecrodForMultiply.ReadOnlyControl();
                        //formRecrodForMultiply.ShowDialog();
                        //}
                        ProcessClickConsultatonListLogic processConsult = new ProcessClickConsultatonListLogic(m_App, noOfFirstPage);
                        processConsult.ProcessLogic(m_App.User.Id, consultApplySn);
                        Search();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-25</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
        /// <date>2011-11-13</date>
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
        /// <date>2011-11-13</date>
        /// </summary>
        private void Reset()
        {
            try
            {
                InitDateEdit();
                textEditName.Text = string.Empty;
                textEditPatientSN.Text = string.Empty;
                lookUpEditorUrgency.CodeValue = string.Empty;
                lookUpEditorBed.CodeValue = string.Empty;
                textEditName.Focus();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
