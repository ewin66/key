using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DrectSoft.Wordbook;
using DrectSoft.Core.RecordManage.PublicSet;
using DrectSoft.Common;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace DrectSoft.Core.RecordManage.UCControl
{
    public partial class UCSingInRecord : DevExpress.XtraEditors.XtraUserControl
    {
        public UCSingInRecord()
        {
            InitializeComponent();
        }

        //��ʼ������
        private void InitDepartment()
        {
            try
            {
                lookUpWindowDepartment.SqlHelper = SqlUtil.App.SqlHelper;

                DataTable Dept = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_GetMedicalRrecordViewFrm",
                     new SqlParameter[] { new SqlParameter("@GetType", "1") }, CommandType.StoredProcedure);

                Dept.Columns["ID"].Caption = "���ұ���";
                Dept.Columns["NAME"].Caption = "��������";

                Dictionary<string, int> cols = new Dictionary<string, int>();

                cols.Add("ID", 60);
                cols.Add("NAME", 90);

                SqlWordbook deptWordBook = new SqlWordbook("querybook", Dept, "ID", "NAME", cols, "ID//NAME//PY//WB");
                lookUpEditorDepartment.SqlWordbook = deptWordBook;
                lookUpEditorDepartment.CodeValue = "0000";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void InitName()
        {
            try
            {
                this.lookUpWindowName.SqlHelper = SqlUtil.App.SqlHelper;

                DataTable Name = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_GetMedicalRrecordViewFrm",
                     new SqlParameter[] { new SqlParameter("@GetType", "4") }, CommandType.StoredProcedure);

                Name.Columns["ID"].Caption = "���˱��";
                Name.Columns["NAME"].Caption = "��������";

                Dictionary<string, int> cols = new Dictionary<string, int>();

                cols.Add("ID", 60);
                cols.Add("NAME", 90);

                SqlWordbook nameWordBook = new SqlWordbook("queryname", Name, "ID", "NAME", cols, "ID//NAME//PY//WB");
                this.lookUpEditorName.SqlWordbook = nameWordBook;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��������¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// 1��add try ... catch
        /// 2����װ��ʼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCSingInRecord_Load(object sender, EventArgs e)
        {
            try
            {
                lblSingInTip.Text = "ǩ���ˣ�" + SqlUtil.App.User.Name;

                //���µ��ڵĽ��Ĳ���
                SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_UpDateDueApplyRecord",
                     new SqlParameter[] { new SqlParameter("@ApplyDoctor", SqlUtil.App.User.Id) }, CommandType.StoredProcedure);

                InitDepartment();
                InitName();

                Reset();
                this.ActiveControl = lookUpEditorName;
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ��ѯ�¼�
        /// edit by Yanqiao.Cai 2012-11-16
        /// 1��add try ... catch
        /// 2�������Ա�ͼƬ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string errorStr = CheckItem();
                if (!string.IsNullOrEmpty(errorStr))
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(errorStr);
                    return;
                }
                //�����Ա�ͼƬ
                DS_Common.InitializeImage_XB(repositoryItemImageXB, imageListXB);

                DataTable table = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_GetSignInRecordNew"
                   , new SqlParameter[] 
                { 
                    new SqlParameter("@DateBegin", dateEditBegin.DateTime.Date.ToString("yyyy-MM-dd")), 
                    new SqlParameter("@DateEnd", string.IsNullOrEmpty(dateEnd.Text.Trim())? DateTime.Now.ToString("yyyy-MM-dd"):dateEnd.DateTime.Date.ToString("yyyy-MM-dd")),
                    new SqlParameter("@DateInBegin", dateEditInBegin.DateTime.Date.ToString("yyyy-MM-dd")), 
                    new SqlParameter("@DateInEnd", string.IsNullOrEmpty(dateEditInEnd.Text.Trim())?DateTime.Now.ToString("yyyy-MM-dd"):dateEditInEnd.DateTime.Date.ToString("yyyy-MM-dd")),
                    new SqlParameter("@PatientName", this.lookUpEditorName.Text.Trim()),
                    new SqlParameter("@OutHosDept", lookUpEditorDepartment.CodeValue.ToString())
                }
                   , CommandType.StoredProcedure);

                gridViewSingInRecord.SelectAll();
                gridViewSingInRecord.DeleteSelectedRows();
                gridControlSingInRecord.DataSource = table;

                lblTip.Text = "��" + table.Rows.Count.ToString() + "����¼";

                if (table.Rows.Count <= 0)
                    SqlUtil.App.CustomMessageBox.MessageShow("û�з��������ļ�¼");
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ������֤
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-26</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string CheckItem()
        {
            try
            {
                if (string.IsNullOrEmpty(lookUpEditorDepartment.CodeValue))
                {
                    lookUpEditorDepartment.Focus();
                    return "��ѡ���Ժ����";
                }
                else if (dateEditBegin.DateTime > dateEnd.DateTime)
                {
                    dateEditBegin.Focus();
                    return "���뿪ʼ���ڲ��ܴ��������������";
                }
                else if (dateEditInBegin.DateTime > dateEditInEnd.DateTime)
                {
                    dateEditInBegin.Focus();
                    return "��Ժ��ʼ���ڲ��ܴ�����Ժ��������";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return string.Empty;
        }

        private void BrowserMedicalRecord(bool bolMessage)
        {
            try
            {
                #region ע�� by cyq 2012-12-06 �޸�Ȩ������ ��������ģ�鲻�ɱ༭����
                //string noOfInpat; //סԺ��

                //int fouceRowIndex = gridViewSingInRecord.FocusedRowHandle;
                //if (fouceRowIndex < 0)
                //{
                //    if (bolMessage) SqlUtil.App.CustomMessageBox.MessageShow("��ѡ����Ҫ�����Ĳ�����¼");
                //    return;
                //}
                //DataRow foucesRow = gridViewSingInRecord.GetDataRow(fouceRowIndex);
                //noOfInpat = foucesRow["noofinpat"].ToString();

                //SqlUtil.App.ChoosePatient(Convert.ToDecimal(noOfInpat));
                //SqlUtil.App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", "DrectSoft.Core.MainEmrPad.MainForm");
                ////HistoryRecordBrowser frmHistoryRecordBrowser = new HistoryRecordBrowser(PatID);
                ////frmHistoryRecordBrowser.ShowDialog();
                #endregion

                int fouceRowIndex = gridViewSingInRecord.FocusedRowHandle;
                if (fouceRowIndex < 0)
                {
                    if (bolMessage)
                    {
                        DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("��ѡ��һ�����˼�¼");
                    }
                    return;
                }
                DataRow foucesRow = gridViewSingInRecord.GetDataRow(fouceRowIndex);

                string noOfFirstPage = foucesRow["NOOFINPAT"].ToString();
                EmrBrowser frm = new EmrBrowser(noOfFirstPage, SqlUtil.App);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// �����黹��¼˫���¼�
        /// edit by Yanqiao.Cai 2012-11-12
        /// 1��add try ... catch
        /// 2��˫��С����Ӧ�޲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewSingInRecord_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridHitInfo hitInfo = gridViewSingInRecord.CalcHitInfo(gridControlSingInRecord.PointToClient(Cursor.Position));
                if (hitInfo.RowHandle < 0)
                {
                    return;
                }
                BrowserMedicalRecord(false);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                BrowserMedicalRecord(true);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        private void AuditApplayRecord(string AuditType)
        {
            try
            {
                int fouceRowIndex = gridViewSingInRecord.FocusedRowHandle;
                if (fouceRowIndex < 0)
                {
                    SqlUtil.App.CustomMessageBox.MessageShow("��ѡ����Ҫǩ�յĲ������������¼");
                    return;
                }
                DataRow foucesRow = gridViewSingInRecord.GetDataRow(fouceRowIndex);

                if (SqlUtil.App.CustomMessageBox.MessageShow("��ȷ��Ҫǩ�ոò������������¼��"
                    , CustomMessageBoxKind.QuestionYesNo) == DialogResult.No)
                    return;

                SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_AuditApplyRecord"
                  , new SqlParameter[] 
                { 
                    new SqlParameter("@AuditType", AuditType), 
                    new SqlParameter("@ManID",SqlUtil.App.User.Id),
                    new SqlParameter("@ID", foucesRow["ID"].ToString())
                }
                  , CommandType.StoredProcedure);

                SqlUtil.App.CustomMessageBox.MessageShow("ǩ�ճɹ�");
                gridViewSingInRecord.DeleteRow(gridViewSingInRecord.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("ǩ��ʧ�ܣ�\n����ԭ��:" + ex.Message);
            }
        }

        /// <summary>
        /// ǩ���¼�
        /// </summary>
        /// <param name="AuditType"></param>
        private void btnSingIn_Click(object sender, EventArgs e)
        {
            try
            {
                AuditApplayRecord("5206");
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-26</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewSingInRecord_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
        /// <date>2011-11-05</date>
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
        /// �����¼�����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-05</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset()
        {
            try
            {
                dateEditBegin.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                dateEnd.Text = DateTime.Now.ToShortDateString();
                dateEditInBegin.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                dateEditInEnd.Text = DateTime.Now.ToShortDateString();

                this.lookUpEditorName.CodeValue = string.Empty;
                this.lookUpEditorDepartment.CodeValue = "0000";
                this.lookUpEditorName.Focus();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��տؼ��е�ֵ
        /// <auth>��ҵ��</auth>
        /// <date>2012-12-17</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lookUpEditorName.CodeValue = string.Empty;
                dateEditBegin.Text = string.Empty;
                dateEnd.Text = string.Empty;
                lookUpEditorDepartment.CodeValue = "0000";
                dateEditInBegin.Text = string.Empty;
                dateEditInEnd.Text = string.Empty;
            }
            catch (Exception ex)
            {

                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

    }
}
