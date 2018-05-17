using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;
using DrectSoft.FrameWork;
using DrectSoft.FrameWork.WinForm;
using System.Data.SqlClient;
using DrectSoft.Common;
using DrectSoft.Common.Ctrs.FORM;

namespace DrectSoft.Core.MedicalRecordQuery
{
    public partial class ApplyExamine : DevBaseForm//, IStartPlugIn
    {
        IEmrHost m_app;
        IDataAccess sql_Helper;


        public ApplyExamine(IEmrHost App)
        {
            InitializeComponent();
            m_app = App;
            sql_Helper = m_app.SqlHelper;
            Search();
            SelectDefaultRow();
        }

        /// <summary>
        /// ���ò����ţ����ⲿ����
        /// </summary>
        /// <param name="patID"></param>
        public void SetPatID(string patID)
        {
            textEditPatID.Text = patID;
        }

        #region ��ʼ��������
        private void InitSqlWorkBook()
        {
            //����
            lookUpWindowDepartment.SqlHelper = sql_Helper;


            DataTable Dept = sql_Helper.ExecuteDataTable("usp_GetMedicalRrecordViewFrm",
                 new SqlParameter[] { new SqlParameter("@GetType", "1") }, CommandType.StoredProcedure);

            Dept.Columns["ID"].Caption = "���ұ���";
            Dept.Columns["NAME"].Caption = "��������";

            Dictionary<string, int> cols = new Dictionary<string, int>();

            cols.Add("ID", 80);
            cols.Add("NAME", 120);

            SqlWordbook deptWordBook = new SqlWordbook("querybook", Dept, "ID", "NAME", cols, "ID//NAME//PY//WB");
            lookUpEditorDepartment.SqlWordbook = deptWordBook;
            lookUpEditorDepartment.CodeValue = "0000";

            //����Ŀ��
            lookUpWindowPurpose.SqlHelper = sql_Helper;

            DataTable Purpose = sql_Helper.ExecuteDataTable("usp_GetMedicalRrecordViewFrm",
               new SqlParameter[] { new SqlParameter("@GetType", "2") }, CommandType.StoredProcedure);

            Purpose.Columns["NAME"].Caption = "����Ŀ��";

            Dictionary<string, int> PurposeCols = new Dictionary<string, int>();

            PurposeCols.Add("NAME", 150);

            SqlWordbook PurposeWordBook = new SqlWordbook("querybook", Purpose, "ID", "NAME", PurposeCols, "ID//NAME//PY//WB");
            lookUpEditorPurpose.SqlWordbook = PurposeWordBook;
            lookUpEditorPurpose.CodeValue = "5001";

            //��λ
            lookUpWindowUint.SqlHelper = sql_Helper;

            DataTable Uint = sql_Helper.ExecuteDataTable("usp_GetMedicalRrecordViewFrm",
                new SqlParameter[] { new SqlParameter("@GetType", "3") }, CommandType.StoredProcedure);

            Uint.Columns["NAME"].Caption = "��λ";

            Dictionary<string, int> UintCols = new Dictionary<string, int>();

            UintCols.Add("NAME", 60);

            SqlWordbook UintWordBook = new SqlWordbook("querybook", Uint, "ID", "NAME", UintCols,"ID//NAME//PY//WB");
            lookUpEditorUint.SqlWordbook = UintWordBook;
            lookUpEditorUint.CodeValue = "5101";

        }

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
                DataTable diagnosis = sql_Helper.ExecuteDataTable("select py, wb, name, icd from diagnosis  where valid='1' union select py, wb, name, icdid from diagnosisothername where valid='1'");
                foreach (DataRow row in diagnosis.Rows)
                {
                    DataRow displayRow = disease.NewRow();
                    displayRow["ICD"] = row["ICD"];
                    displayRow["NAME"] = row["NAME"];
                    displayRow["PY"] = row["PY"];
                    displayRow["WB"] = row["WB"];
                    disease.Rows.Add(displayRow);
                }

                this.lookUpWindowOutDiag.SqlHelper = sql_Helper;
                disease.Columns["ICD"].Caption = "��ϱ���";
                disease.Columns["NAME"].Caption = "�������";
                Dictionary<string, int> cols = new Dictionary<string, int>();
                cols.Add("ICD", 60);
                cols.Add("NAME", 140);
                SqlWordbook diagWordBook = new SqlWordbook("queryDiag", disease, "ICD", "NAME", cols, "ICD//NAME//PY//WB");
                this.lookUpEditorOutDiag.SqlWordbook = diagWordBook;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        private void ApplyExamine_Load(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString();
            txtDepartment.Text = m_app.User.CurrentDeptName;
            txtDoctor.Text = m_app.User.Name;

            InitSqlWorkBook();
            InitDiag();

            Reset();
        }

        //private void InitShowDisease()
        //{
        //    DataTable disease = new DataTable();
        //    disease.Columns.Add("ICD");
        //    disease.Columns.Add("NAME");
        //    disease.Columns.Add("PY");
        //    disease.Columns.Add("WB");
        //    DataTable diagnosis = sql_Helper.ExecuteDataTable("select * from diagnosis");
        //    foreach (DataRow row in diagnosis.Rows)
        //    {
        //        DataRow displayRow = disease.NewRow();
        //        displayRow["ICD"] = row["ICD"];
        //        displayRow["NAME"] = row["NAME"];
        //        displayRow["PY"] = row["PY"];
        //        displayRow["WB"] = row["WB"];
        //        disease.Rows.Add(displayRow);
        //    }
        //    //checkedListBoxControlDisease.DisplayMember = "ICD";
        //    //checkedListBoxControlDisease.DisplayMember = "NAME";
        //    //checkedListBoxControlDisease.ValueMember = "ICD";
        //    //checkedListBoxControlDisease.DataSource = disease;


        //    this.lookUpWindowDisease.SqlHelper = sql_Helper;
        //    disease.Columns["ICD"].Caption = "����";
        //    disease.Columns["NAME"].Caption = "����";
        //    Dictionary<string, int> cols = new Dictionary<string, int>();

        //    cols.Add("ICD", 80);
        //    cols.Add("NAME", 160);

        //    SqlWordbook diagWordBook = new SqlWordbook("queryDiag", disease, "ICD", "NAME", cols, "ICD//NAME//PY//WB");
        //    this.lookUpEditorDisease.SqlWordbook = diagWordBook;
        //}

        /// <summary>
        /// ��ѯ�¼�
        /// edit by Yanqiao.Cai 2012-11-15
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ��ѯ����
        /// edit by Yanqiao.Cai 2012-11-15
        /// 1��add try ... catch
        /// 2�������Ա�ͼƬ
        /// </summary>
        private void Search()
        {
            try
            {
                //�����Ա�ͼƬ
                DS_Common.InitializeImage_XB(repositoryItemImageXB, imageListXB);

                SqlParameter[] sqlParam = new SqlParameter[] 
                { 
                new SqlParameter("@DeptCode", SqlDbType.VarChar),
                new SqlParameter("@DateTimeBegin", SqlDbType.VarChar),
                new SqlParameter("@DateTimeEnd", SqlDbType.VarChar),
                new SqlParameter("@QCStatType", SqlDbType.VarChar),
                new SqlParameter("@PatientName", SqlDbType.VarChar),
                new SqlParameter("@RecordID", SqlDbType.VarChar),
                new SqlParameter("@PatID", SqlDbType.VarChar),
                new SqlParameter("@OutDiag", SqlDbType.VarChar)
                };
                sqlParam[0].Value = lookUpEditorDepartment.CodeValue == "0000" ? "" : lookUpEditorDepartment.CodeValue;
                sqlParam[1].Value = "";
                sqlParam[2].Value = "";
                sqlParam[3].Value = 2;
                sqlParam[4].Value = textEditName.Text.Trim();
                sqlParam[5].Value = "";
                sqlParam[6].Value = textEditPatID.Text.Trim();
                sqlParam[7].Value = this.lookUpEditorOutDiag.CodeValue;

                DataTable MedicalRrecordSet = sql_Helper.ExecuteDataTable("usp_GetMedicalRrecordView", sqlParam, CommandType.StoredProcedure);
                gridControlApplyRecord.DataSource = MedicalRrecordSet;
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }

        }

        private void gridViewApplyRecord_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridViewApplyRecord.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridViewApplyRecord.GetDataRow(fouceRowIndex);
            txtNoOfRecord.Text = foucesRow["RecordID"].ToString();
            txtPatientName.Text = foucesRow["Name"].ToString();
        }

        private void SelectDefaultRow()
        {
            DataTable dt = gridControlApplyRecord.DataSource as DataTable;
            if (dt.Rows.Count == 1)
            {
                DataRow foucesRow = gridViewApplyRecord.GetDataRow(0);
                txtNoOfRecord.Text = foucesRow["RecordID"].ToString();
                txtPatientName.Text = foucesRow["Name"].ToString();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Apply();
        }
        private void gridControlApplyRecord_DoubleClick(object sender, EventArgs e)
        {
            this.Apply();
        }
        //����
        private void Apply()
        {
            try
            {
                DataRow foucesRow = gridViewApplyRecord.GetDataRow(gridViewApplyRecord.FocusedRowHandle);
                if (gridViewApplyRecord.FocusedRowHandle < 0)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("��ѡ��һ�����˼�¼");
                    return;
                }

                string errorStr = CheckItems();
                if (!string.IsNullOrEmpty(errorStr))
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(errorStr);
                    return;
                }
                
                SqlParameter[] sqlParam = new SqlParameter[] 
                 { 
                    new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                    new SqlParameter("@ApplyDoctor", SqlDbType.VarChar),
                    new SqlParameter("@DeptID", SqlDbType.VarChar),
                    new SqlParameter("@ApplyAim", SqlDbType.VarChar),
                    new SqlParameter("@DueTime", SqlDbType.VarChar),
                    new SqlParameter("@Unit", SqlDbType.VarChar)
                    //new SqlParameter("@ApplyDate", SqlDbType.VarChar),
                    //new SqlParameter("@Status", SqlDbType.VarChar)
                };

                sqlParam[0].Value = foucesRow["NoOfInpat"].ToString();
                sqlParam[1].Value = m_app.User.Id;
                sqlParam[2].Value = m_app.User.CurrentDeptId;
                sqlParam[3].Value = lookUpEditorPurpose.CodeValue;
                sqlParam[4].Value = txtNumOfDate.Text.Trim();
                sqlParam[5].Value = lookUpEditorUint.CodeValue;
                //sqlParam[6].Value =txtDate.Text;
                //sqlParam[7].Value = "5201";

                sql_Helper.ExecuteNoneQuery("usp_SaveApplyRecord", sqlParam, CommandType.StoredProcedure);

                m_app.CustomMessageBox.MessageShow("����ɹ�");

            }
            catch (Exception ex)
            {
                m_app.CustomMessageBox.MessageShow("����ʧ�ܣ�\n��ϸ����" + ex.Message);
            }
        }

        /// <summary>
        /// ������
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-29</date>
        /// </summary>
        /// <returns></returns>
        private string CheckItems()
        {
            try
            {
                DataRow foucesRow = gridViewApplyRecord.GetDataRow(gridViewApplyRecord.FocusedRowHandle);
                //edit by cyq 2013-03-01
                //if (foucesRow["DeptID"].ToString() != m_app.User.CurrentDeptId)
                //{
                //    this.lookUpEditorDepartment.Focus();
                //    return "��ֻ��������ı����Ҳ���";
                //}
                //else 
                if (string.IsNullOrEmpty(lookUpEditorPurpose.CodeValue))
                {
                    lookUpEditorPurpose.Focus();
                    return "��ѡ�����Ŀ��";
                }
                else if (string.IsNullOrEmpty(txtNumOfDate.Text.Trim()))
                {
                    txtNumOfDate.Focus();
                    return "�������޲���Ϊ��";
                }
                else if (string.IsNullOrEmpty(lookUpEditorUint.CodeValue))
                {
                    lookUpEditorUint.Focus();
                    return "��ѡ��������޵ĵ�λ";
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-29</date>
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
        /// �ر��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevButtonClose1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-07</date>
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
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-07</date>
        /// <return></return>
        private void Reset()
        {
            try
            {
                this.textEditName.Text = string.Empty;
                this.textEditPatID.Text = string.Empty;
                this.lookUpEditorDepartment.CodeValue = m_app.User.CurrentDeptId;
                this.lookUpEditorOutDiag.CodeValue = string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}