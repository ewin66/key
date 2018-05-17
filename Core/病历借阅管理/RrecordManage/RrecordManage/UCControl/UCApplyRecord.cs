using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.Common;
using DrectSoft.Core.RecordManage.PublicSet;
using System.Data.SqlClient;
using DrectSoft.Wordbook;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace DrectSoft.Core.RecordManage.UCControl
{
    public partial class UCApplyRecord : DevExpress.XtraEditors.XtraUserControl
    {
        public UCApplyRecord()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
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

                cols.Add("ID", 80);
                cols.Add("NAME", 150);

                SqlWordbook deptWordBook = new SqlWordbook("querybook", Dept, "ID", "NAME", cols, "ID//NAME//PY//WB");
                lookUpEditorDepartment.SqlWordbook = deptWordBook;
                lookUpEditorDepartment.CodeValue = "0000";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region ������ by cyq 2012-12-06
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        //private void InitName()
        //{
        //    try
        //    {
        //        this.lookUpWindowName.SqlHelper = SqlUtil.App.SqlHelper;

        //        DataTable Name = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_GetMedicalRrecordViewFrm",
        //             new SqlParameter[] { new SqlParameter("@GetType", "4") }, CommandType.StoredProcedure);

        //        Name.Columns["ID"].Caption = "���˱��";
        //        Name.Columns["NAME"].Caption = "��������";

        //        Dictionary<string, int> cols = new Dictionary<string, int>();

        //        cols.Add("ID", 60);
        //        cols.Add("NAME", 90);

        //        SqlWordbook nameWordBook = new SqlWordbook("queryname", Name, "ID", "NAME", cols, "ID//NAME//PY//WB");
        //        this.txt_patiName.SqlWordbook = nameWordBook;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        #endregion

        /// <summary>
        /// ��������¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// 1��add try ... catch
        /// 2����װ��ʼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCApplyRecord_Load(object sender, EventArgs e)
        {
            try
            {
                lblAuditTip.Text = "����ˣ�" + SqlUtil.App.User.Name;

                InitDepartment();
                //InitName(); //edit by cyq 2012-12-06

                Reset();
                this.ActiveControl = txt_patiName;
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ��ѯ�¼�
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

                DataTable table = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_GetApplyRecordNew"
                   , new SqlParameter[] 
                { 
                    new SqlParameter("@DateBegin", dateBegin.DateTime.Date.ToString("yyyy-MM-dd")), 
                    new SqlParameter("@DateEnd", string.IsNullOrEmpty(dateEnd.Text.Trim())?DateTime.Now.ToString("yyyy-MM-dd"):dateEnd.DateTime.Date.ToString("yyyy-MM-dd")),
                    new SqlParameter("@PatientName", this.txt_patiName.Text.Trim()),
                    new SqlParameter("@DocName", this.txt_applyDoc.Text.Trim()),
                    new SqlParameter("@OutHosDept", lookUpEditorDepartment.CodeValue.ToString())
                }
                   , CommandType.StoredProcedure);

                //         DataTable table = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_GetApplyRecord"
                //, new SqlParameter[] 
                //             { 
                //                 new SqlParameter("@DateBegin", dateBegin.DateTime.Date.ToString("yyyy-MM-dd")), 
                //                 new SqlParameter("@DateEnd", dateEnd.DateTime.Date.ToString("yyyy-MM-dd")),
                //                 new SqlParameter("@OutHosDept", lookUpEditorDepartment.CodeValue.ToString())
                //             }
                //, CommandType.StoredProcedure);

                gridViewApplyRecord.SelectAll();
                gridViewApplyRecord.DeleteSelectedRows();
                gridControlApplyRecord.DataSource = table;

                lblTip.Text = "��" + table.Rows.Count.ToString() + "����¼";

                if (table.Rows.Count <= 0)
                {
                    SqlUtil.App.CustomMessageBox.MessageShow("û�����������ļ�¼");
                }
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
        /// <returns></returns>
        private string CheckItem()
        {
            try
            {
                if (string.IsNullOrEmpty(lookUpEditorDepartment.CodeValue))
                {
                    lookUpEditorDepartment.Focus();
                    return "��ѡ���Ժ����";
                }
                else if (dateBegin.DateTime.Date > dateEnd.DateTime.Date)
                {
                    dateBegin.Focus();
                    return "��Ժ��ʼ���ڲ��ܴ��ڳ�Ժ��������";
                }
                //else if (dateEditInBegin.DateTime.Date > dateEditInEnd.DateTime.Date)
                //{
                //    dateEditInBegin.Focus();
                //    return "��Ժ��ʼ���ڲ��ܴ�����Ժ��������";
                //}

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BrowserMedicalRecord(bool bolMessage)
        {
            try
            {
                string noOfInpat; //סԺ��

                int fouceRowIndex = gridViewApplyRecord.FocusedRowHandle;
                if (fouceRowIndex < 0)
                {
                    if (bolMessage) SqlUtil.App.CustomMessageBox.MessageShow("��ѡ��һ�����˼�¼");
                    return;
                }
                DataRow foucesRow = gridViewApplyRecord.GetDataRow(fouceRowIndex);

                //edit by wyt 2012-11-09 �½�������ʾ����
                string noOfFirstPage = foucesRow["NOOFINPAT"].ToString();
                EmrBrowser frm = new EmrBrowser(noOfFirstPage, SqlUtil.App);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
                #region ȡ�����ز����ʽ��ʾ���� edit by wyt 2012-11-09
                //SqlUtil.App.ChoosePatient(Convert.ToDecimal(noOfInpat));
                //SqlUtil.App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", "DrectSoft.Core.MainEmrPad.MainForm");
                #endregion

                //HistoryRecordBrowser frmHistoryRecordBrowser = new HistoryRecordBrowser(PatID);
                //frmHistoryRecordBrowser.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ˫���¼�
        /// edit by Yanqiao.Cai 2012-11-12
        /// 1��add try ... catch
        /// 2��˫��С����Ӧ�޲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewApplyRecord_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridHitInfo hitInfo = gridViewApplyRecord.CalcHitInfo(gridControlApplyRecord.PointToClient(Cursor.Position));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AuditType">�������</param>
        /// <param name="flag">��ʶ��0-������1-����������2-����</param>
        private void AuditApplayRecord(string AuditType,int flag)
        {
            string tipStr = flag == 0 ? "����" : (flag == 1 ? "��������" : "����");
            try
            {
                int fouceRowIndex = gridViewApplyRecord.FocusedRowHandle;
                if (fouceRowIndex < 0)
                {
                    SqlUtil.App.CustomMessageBox.MessageShow("��ѡ��һ���������������¼");
                    return;
                }
                DataRow foucesRow = gridViewApplyRecord.GetDataRow(fouceRowIndex);

                if (SqlUtil.App.CustomMessageBox.MessageShow("��ȷ��Ҫ" + tipStr + "�ò������������¼��"
                    , CustomMessageBoxKind.QuestionYesNo) == DialogResult.No)
                {
                    return;
                }

                SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_AuditApplyRecord"
                  , new SqlParameter[] 
                { 
                    new SqlParameter("@AuditType", AuditType), 
                    new SqlParameter("@ManID",SqlUtil.App.User.Id),
                    new SqlParameter("@ID", foucesRow["ID"].ToString())
                }
                  , CommandType.StoredProcedure);

                SqlUtil.App.CustomMessageBox.MessageShow(tipStr + "�ɹ�");
                gridViewApplyRecord.DeleteRow(gridViewApplyRecord.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(tipStr+"ʧ�ܣ�\n����ԭ��:" + ex.Message);
            }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                AuditApplayRecord("5202",0);
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
        private void btnNoAudit_Click(object sender, EventArgs e)
        {
            try
            {
                AuditApplayRecord("5203",1);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                AuditApplayRecord("5204",2);
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
                dateBegin.Text = DateTime.Now.AddMonths(-6).ToShortDateString();
                dateEnd.Text = DateTime.Now.ToShortDateString();
                //dateEditInBegin.Text = DateTime.Now.AddMonths(-6).ToShortDateString();
                //dateEditInEnd.Text = DateTime.Now.ToShortDateString();
                this.txt_patiName.Text = string.Empty;
                this.txt_applyDoc.Text = string.Empty;
                this.lookUpEditorDepartment.CodeValue = "0000";
                this.txt_patiName.Focus();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-26</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewApplyRecord_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
        /// �б��¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-14</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewApplyRecord_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                DataRow dr = gridViewApplyRecord.GetDataRow(e.RowHandle) as DataRow;
                if (null == dr)
                {
                    return;
                }
                if (dr["STATUS"].ToString() == "5201")
                {//������
                    e.Appearance.ForeColor = Color.Black;
                }
                if (dr["STATUS"].ToString() == "5202")
                {//����ͨ��
                    e.Appearance.ForeColor = Color.YellowGreen;
                }
                else if (dr["STATUS"].ToString() == "5203")
                {//����δͨ��
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (dr["STATUS"].ToString() == "5204")
                {//��������
                    e.Appearance.ForeColor = Color.Orange;
                }
                else if (dr["STATUS"].ToString() == "5205")
                {//���ĵ���
                    e.Appearance.ForeColor = Color.MediumAquamarine;
                }
                else if (dr["STATUS"].ToString() == "5206")
                {//��ǩ��
                    e.Appearance.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ��հ�ť
        /// <auth>��ҵ��</auth>
        /// <date>2012-12-17</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txt_patiName.Text = string.Empty;
                lookUpEditorDepartment.CodeValue = "0000";
                panelControl1.Text = string.Empty;
                dateBegin.Text = string.Empty;
                dateEnd.Text = string.Empty;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

        
    }
}
