using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.Common;
using DrectSoft.Wordbook;
using DrectSoft.Core.RecordManage.PublicSet;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DrectSoft.Service;

namespace DrectSoft.Core.RecordManage.UCControl
{
    public partial class UCRecordNoOnFile : DevExpress.XtraEditors.XtraUserControl
    {
        public UCRecordNoOnFile()
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

                Dept.Columns["ID"].Caption = "���Ҵ���";
                Dept.Columns["NAME"].Caption = "��������";

                Dictionary<string, int> cols = new Dictionary<string, int>();

                cols.Add("ID", 60);
                cols.Add("NAME", 90);

                SqlWordbook deptWordBook = new SqlWordbook("querybook", Dept, "ID", "NAME", cols, "ID//NAME//PY//WB");
                lookUpEditorDepartment.SqlWordbook = deptWordBook;
                lookUpEditorDepartment.CodeValue = DS_Common.currentUser.CurrentDeptId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region edit by cyq 2012-12-04 ������
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

        /// <summary>
        /// ��ʼ��סԺ��
        /// </summary>
        //private void IntiRecordID()
        //{
        //    try
        //    {
        //        this.lookUpWindowRecordID.SqlHelper = SqlUtil.App.SqlHelper;

        //        DataTable RecordID = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_GetMedicalRrecordViewFrm",
        //             new SqlParameter[] { new SqlParameter("@GetType", "5") }, CommandType.StoredProcedure);

        //        RecordID.Columns["ID"].Caption = "סԺ��";

        //        Dictionary<string, int> cols = new Dictionary<string, int>();

        //        cols.Add("ID", 120);

        //        SqlWordbook recordIDWordBook = new SqlWordbook("queryrecordid", RecordID, "ID", "ID", cols, "ID");
        //        this.txt_recordID.SqlWordbook = recordIDWordBook;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        #endregion

        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private void InitDiag()
        {
            try
            {
                DataTable disease = new DataTable();
                disease.Columns.Add("ICD");
                disease.Columns.Add("NAME");
                disease.Columns.Add("PY");
                disease.Columns.Add("WB");
                DataTable diagnosis = SqlUtil.App.SqlHelper.ExecuteDataTable("select py, wb, name, icd from diagnosis  where valid='1' union select py, wb, name, icdid from diagnosisothername where valid='1'");
                foreach (DataRow row in diagnosis.Rows)
                {
                    DataRow displayRow = disease.NewRow();
                    displayRow["ICD"] = row["ICD"];
                    displayRow["NAME"] = row["NAME"];
                    displayRow["PY"] = row["PY"];
                    displayRow["WB"] = row["WB"];
                    disease.Rows.Add(displayRow);
                }

                this.lookUpWindowInDiag.SqlHelper = SqlUtil.App.SqlHelper;
                this.lookUpWindowOutDiag.SqlHelper = SqlUtil.App.SqlHelper;
                disease.Columns["ICD"].Caption = "��ϱ���";
                disease.Columns["NAME"].Caption = "�������";
                Dictionary<string, int> cols = new Dictionary<string, int>();

                cols.Add("ICD", 60);
                cols.Add("NAME", 120);

                SqlWordbook diagWordBook = new SqlWordbook("queryDiag", disease, "ICD", "NAME", cols, "ICD//NAME//PY//WB");
                this.lookUpEditorInDiag.SqlWordbook = diagWordBook;
                this.lookUpEditorOutDiag.SqlWordbook = diagWordBook;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��ʼ��סԺҽʦ
        /// </summary>
        private void InitPhysician()
        {
            try
            {
                this.lookUpWindowPhysician.SqlHelper = SqlUtil.App.SqlHelper;
                string sql = "select ID,NAME,PY,WB from users";
                DataTable Name = SqlUtil.App.SqlHelper.ExecuteDataTable(sql);
                Name.Columns["ID"].Caption = "ҽʦ����";
                Name.Columns["NAME"].Caption = "ҽʦ����";

                Dictionary<string, int> cols = new Dictionary<string, int>();

                cols.Add("ID", 60);
                cols.Add("NAME", 60);

                SqlWordbook nameWordBook = new SqlWordbook("queryname", Name, "ID", "NAME", cols, "ID//NAME//PY//WB");
                this.lookUpEditorPhysician.SqlWordbook = nameWordBook;
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
        private void UCRecordNoOnFile_Load(object sender, EventArgs e)
        {
            try
            {
                //edit by cyq 2012-12-04
                //IntiRecordID();
                //InitName();
                InitDepartment();
                InitDiag();
                InitPhysician();
                //��ȡ����״̬
                SqlUtil.GetDictionarydetail(lookUpRecordStatus, "1", "47", "usp_GetRecordManageFrm");

                //add by cyq 2012-12-23 ��������Ա��ʾ�鵵��ť
                this.btn_reback.Visible = DS_BaseService.CheckIfQuatityControlPerson(DS_Common.currentUser.Id);

                Reset();
                this.ActiveControl = txt_recordID;
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        public void InitializeSqlUtil(DrectSoft.FrameWork.WinForm.Plugin.IEmrHost app)
        {
            if (SqlUtil.App == null)
            {
                SqlUtil.App = app;
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

                DataTable table = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_GetRecordNoOnFileWyt"
                    , new SqlParameter[] 
                { 
                    new SqlParameter("@DateOutHosBegin", dateEditBegin.DateTime.Date.ToString("yyyy-MM-dd")), 
                    new SqlParameter("@DateOutHosEnd", string.IsNullOrEmpty(this.dateEditEnd.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : this.dateEditEnd.DateTime.ToString("yyyy-MM-dd")),
                    new SqlParameter("@DateInHosBegin", "1900-01-01"), //dateEditInBegin.DateTime.Date.ToString("yyyy-MM-dd")
                    new SqlParameter("@DateInHosEnd",DateTime.Now.ToString("yyyy-MM-dd")),//string.IsNullOrEmpty(this.dateEditInEnd.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : this.dateEditInEnd.DateTime.ToString("yyyy-MM-dd")
                    new SqlParameter("@DeptID", lookUpEditorDepartment.CodeValue.ToString()),
                    new SqlParameter("@InDiag", lookUpEditorInDiag.CodeValue.ToString()),
                    new SqlParameter("@OutDiag", lookUpEditorOutDiag.CodeValue.ToString()),
                    new SqlParameter("@Status", lookUpRecordStatus.EditValue.ToString()) ,
                    new SqlParameter("@Patientname", this.txt_patiName.Text.Trim()),
                    new SqlParameter("@Recordid", this.txt_recordID.Text.Trim()),
                    new SqlParameter("@Physician", this.lookUpEditorPhysician.CodeValue.ToString()) 
                }
                    , CommandType.StoredProcedure);
                

                gridviewRecordNoOnFile.SelectAll();
                gridviewRecordNoOnFile.DeleteSelectedRows();
                gridControlRecordNoOnFile.DataSource = table;

                lblTip.Text = "��" + table.Rows.Count.ToString() + "��";

                if (table.Rows.Count <= 0)
                {
                    SqlUtil.App.CustomMessageBox.MessageShow("û�����������ļ�¼");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��������¼��ҳ��
        /// edit by Yanqiao.Cai 2012-11-23
        /// </summary>
        /// <param name="bolMessage"></param>
        private void BrowserMedicalRecord(bool bolMessage)
        {
            try
            {
                int fouceRowIndex = gridviewRecordNoOnFile.FocusedRowHandle;
                if (fouceRowIndex < 0)
                {
                    if (bolMessage) 
                    {
                        gridControlRecordNoOnFile.Focus();
                        SqlUtil.App.CustomMessageBox.MessageShow("��ѡ��һ�����˼�¼"); 
                    }
                    return;
                }
                DataRow foucesRow = gridviewRecordNoOnFile.GetDataRow(fouceRowIndex);

                //edit by wyt 2012-11-09 �½�������ʾ����
                string noOfFirstPage = foucesRow["NOOFINPAT"].ToString();
                //��������¼�����˵�ͼ��״̬
                FloderState floaderState = FloderState.None;
                if (DS_BaseService.CheckIfQuatityControlPerson(DS_Common.currentUser.Id))
                {
                    floaderState = FloderState.FirstPage;
                }
                EmrBrowser frm = new EmrBrowser(noOfFirstPage, SqlUtil.App, floaderState);
                frm.StartPosition = FormStartPosition.CenterParent;
                //��Ӵ���ر��¼� add by cyq 2012-12-06
                frm.FormClosed += new FormClosedEventHandler(EmrBrowser_FormClosed);
                frm.ShowDialog();
                //�Ƴ�����ر��¼� add by cyq 2012-12-06
                frm.FormClosed -= new FormClosedEventHandler(EmrBrowser_FormClosed);

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
        /// ��������ر��¼�
        /// 1������ǰ���༭������¼�ѹ鵵�����Ƴ��ü�¼
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-06</date>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmrBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (gridviewRecordNoOnFile.FocusedRowHandle < 0)
                {
                    return;
                }
                DataRow foucesRow = gridviewRecordNoOnFile.GetDataRow(gridviewRecordNoOnFile.FocusedRowHandle);
                if (null == foucesRow || null == foucesRow["NOOFINPAT"])
                {
                    return;
                }
                if (!DS_BaseService.CheckRecordRebacked(foucesRow["NOOFINPAT"].ToString()))
                {//����ǰ���༭������¼�ѹ鵵�����Ƴ��ü�¼
                    gridviewRecordNoOnFile.DeleteRow(gridviewRecordNoOnFile.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ˫�������¼�
        /// edit by Yanqiao.Cai 2012-11-12
        /// 1��add try ... catch
        /// 2��˫��С����Ӧ�޲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridviewRecordNoOnFile_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridHitInfo hitInfo = gridviewRecordNoOnFile.CalcHitInfo(gridControlRecordNoOnFile.PointToClient(Cursor.Position));
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
        /// ������
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-26</date>
        /// </summary>
        /// <returns></returns>
        private string CheckItem()
        {
            try
            {
                //if (dateEditInBegin.DateTime > dateEditInEnd.DateTime)
                //{
                //    dateEditInBegin.Focus();
                //    return "��Ժ��ʼ���ڲ��ܴ�����Ժ��������";
                //}
                //else 
                if (lookUpRecordStatus.EditValue == null)
                {
                    lookUpRecordStatus.Focus();
                    return "��ѡ����״̬";
                }
                else if (string.IsNullOrEmpty(lookUpEditorDepartment.CodeValue))
                {
                    lookUpEditorDepartment.Focus();
                    return "��ѡ���Ժ����";
                }
                else if (dateEditBegin.DateTime > dateEditEnd.DateTime)
                {
                    dateEditBegin.Focus();
                    return "��Ժ��ʼ���ڲ��ܴ��ڳ�Ժ��������";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return string.Empty;
        }

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-26</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridviewRecordNoOnFile_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
                dateEditBegin.Text = DateTime.Now.AddMonths(-6).ToShortDateString();
                dateEditEnd.Text = DateTime.Now.ToShortDateString();
                //dateEditInBegin.Text = DateTime.Now.AddMonths(-6).ToShortDateString();
                //dateEditInEnd.Text = DateTime.Now.ToShortDateString();

                this.txt_patiName.Text = string.Empty;
                this.txt_recordID.Text = string.Empty;
                //edit by cyq 2012-12-04
                //this.lookUpEditorDepartment.CodeValue = "0000";
                this.lookUpEditorDepartment.CodeValue = DS_Common.currentUser.CurrentDeptId;
                this.lookUpRecordStatus.EditValue = "4700";
                this.lookUpEditorPhysician.CodeValue = string.Empty;
                this.lookUpEditorInDiag.CodeValue = string.Empty;
                this.lookUpEditorOutDiag.CodeValue = string.Empty;
                this.txt_recordID.Focus();
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
                txt_patiName.Text = string.Empty;
                txt_recordID.Text = string.Empty;
                //dateEditInBegin.Text = string.Empty;
                //dateEditInEnd.Text = string.Empty;
                lookUpEditorInDiag.Text = string.Empty;
                lookUpEditorPhysician.Text = string.Empty;
                lookUpRecordStatus.SelectedText = "δ�鵵";
                lookUpEditorDepartment.CodeValue = "0000";
                dateEditBegin.Text = string.Empty;
                dateEditEnd.Text = string.Empty;
                panelControl2.Text = string.Empty;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// �����鵵�¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-05</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reback_Click(object sender, EventArgs e)
        {
            try
            {
                int fouceRowIndex = gridviewRecordNoOnFile.FocusedRowHandle;
                if (fouceRowIndex < 0)
                {
                    gridControlRecordNoOnFile.Focus();
                    SqlUtil.App.CustomMessageBox.MessageShow("��ѡ��һ�����˼�¼");
                    return;
                }
                DataRow foucesRow = gridviewRecordNoOnFile.GetDataRow(fouceRowIndex);
                if (null == foucesRow || null == foucesRow["NOOFINPAT"])
                {
                    return;
                }
                int noofinpat = int.Parse(foucesRow["NOOFINPAT"].ToString().Trim());

                DataTable dt = DS_SqlService.GetRecordsByNoofinpat(noofinpat);
                if (null == dt || dt.Rows.Count == 0)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(foucesRow["NAME"] + " û�в������޷��鵵��");
                    return;
                }
                if (DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("��ȷ��Ҫ�鵵 " + foucesRow["NAME"] + " �Ĳ�����", "�鵵����", DrectSoft.Common.Ctrs.DLG.MyMessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                if (DS_BaseService.CheckRecordRebacked(noofinpat.ToString()))
                {
                    int num = DS_SqlService.SetRecordsRebacked(noofinpat.ToString());
                    if (num > 0)
                    {
                        DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("�鵵�ɹ�");
                        gridviewRecordNoOnFile.DeleteRow(gridviewRecordNoOnFile.FocusedRowHandle);
                        return;
                    }
                }
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("�ò����ѹ鵵��");
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
    }
}
