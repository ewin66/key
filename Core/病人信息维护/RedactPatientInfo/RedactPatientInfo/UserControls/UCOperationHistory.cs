using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.Wordbook;
using DrectSoft.Core.RedactPatientInfo.PublicSet;
using System.Data.SqlClient;

namespace DrectSoft.Core.RedactPatientInfo.UserControls
{
    public partial class UCOperationHistory : DevExpress.XtraEditors.XtraUserControl
    {
        string m_NoOfInpat;
        DataTable m_Table;

        public UCOperationHistory(string NoOfInpat)
        {
            InitializeComponent();
            m_NoOfInpat = NoOfInpat;

            
        }

        #region ��ʼ������
        private void InitForm()
        {
            //��ʼ����������
            GetSurgery();
            //��ʼ����������
            //GetDiacrisis();

            //��ȡ����ʷ��¼
            GetOperationHistory();

            //��/���ò��ֿؼ�
            EditControlEnabled(false);
            btnCancel.Enabled = false;

        }

        #region ��ʼ����������
        //��ʼ����������
        private void GetSurgery()
        {
            lookUpWindowSurgeryID.SqlHelper = SqlUtil.App.SqlHelper;

            DataTable Surgery = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_RedactPatientInfoFrm",
                 new SqlParameter[] { new SqlParameter("@FrmType", "25") }, CommandType.StoredProcedure);

            Surgery.Columns["ID"].Caption = "��������";
            Surgery.Columns["Name"].Caption = "��������";

            Dictionary<string, int> cols = new Dictionary<string, int>();

            cols.Add("ID", 95);
            cols.Add("NAME", 150);

            SqlWordbook deptWordBook = new SqlWordbook("querybook", Surgery, "ID", "NAME", cols);

            //MethodSet.App.PublicMethod.ConvertSqlWordbookDataSourceUpper(deptWordBook);

            lookUpEditorSurgeryID.SqlWordbook = deptWordBook;

        }
        #endregion

        #region ��ʼ����������
        //��ʼ����������
        private void GetDiacrisis()
        {
            lookUpWindowDiagnosisID.SqlHelper = SqlUtil.App.SqlHelper;

            DataTable Diacrisis = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_RedactPatientInfoFrm",
                 new SqlParameter[] { new SqlParameter("@FrmType", "16") }, CommandType.StoredProcedure);

            Diacrisis.Columns["ICD"].Caption = "��ϴ���";
            Diacrisis.Columns["NAME"].Caption = "��������";

            Dictionary<string, int> cols = new Dictionary<string, int>();

            cols.Add("ICD", 89);
            cols.Add("NAME", 120);

            SqlWordbook deptWordBook = new SqlWordbook("querybook", Diacrisis, "ICD", "NAME", cols);

            //MethodSet.App.PublicMethod.ConvertSqlWordbookDataSourceUpper(deptWordBook);

            lookUpEditorDiagnosisID.SqlWordbook = deptWordBook;

        }
        #endregion

        #region ��ȡ����ʷ��¼
        //��ȡ����ʷ��¼
        private void GetOperationHistory()
        {

            m_Table = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_RedactPatientInfoFrm",
                 new SqlParameter[] { new SqlParameter("@FrmType", "26"), new SqlParameter("@NoOfInpat", m_NoOfInpat) }, CommandType.StoredProcedure);
            gridViewSurgeryHistory.SelectAll();
            gridViewSurgeryHistory.DeleteSelectedRows();
            gridControlSurgeryHistory.DataSource = m_Table;

            SqlUtil.App.PublicMethod.ConvertGridDataSourceUpper(gridViewSurgeryHistory);

        }
        #endregion

        private void UCOperationHistory_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        #endregion

        #region �༭�ؼ�����/��ֹ
        /// <summary>
        /// �༭�ؼ�����/��ֹ
        /// </summary>
        /// <param name="bolEnabled"></param>
        private void EditControlEnabled(bool bolEnabled)
        {
            txtDoctor.Enabled = bolEnabled;
            memoEditDiscuss.Enabled = bolEnabled;
            lookUpEditorDiagnosisID.Enabled = bolEnabled;
            lookUpEditorSurgeryID.Enabled = bolEnabled;

            gridControlSurgeryHistory.Enabled = !bolEnabled;

        }
        #endregion

        #region ������ӡ��޸ġ�ɾ������
        public void SaveUCOperationHistoryInfo(bool isSave)
        {
            if (lookUpEditorDiagnosisID.CodeValue == null || lookUpEditorSurgeryID.CodeValue == null)
            {
                SqlUtil.App.CustomMessageBox.MessageShow("�������ƺͲ������Ʊ�����д!");
                return;
            }

            string strTemp = "����";
            try
            {

                //���
                if (btnAdd.Enabled && btnCancel.Enabled && isSave)
                {
                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ����ӵ�ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "���";
                        SaveOperationHistory();
                        InitForm();
                    }

                }
                //�޸�
                else if (btnModify.Enabled && btnCancel.Enabled && isSave)
                {
                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ���޸ĵ�ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "�޸�";
                        ModifyOperationHistory();
                        InitForm();
                    }
                }
                //ɾ��
                else if (btnDel.Enabled && !btnCancel.Enabled && !isSave)
                {
                    int fouceRowIndex = gridViewSurgeryHistory.FocusedRowHandle;
                    if (fouceRowIndex < 0)
                    {
                        SqlUtil.App.CustomMessageBox.MessageShow("��ѡ����Ҫɾ���ļ�¼! ");
                        return;
                    }
                    DataRow foucesRow = gridViewSurgeryHistory.GetDataRow(fouceRowIndex);

                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ��ɾ����ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "ɾ��";
                        SqlParameter[] sqlParam = new SqlParameter[] 
                        { 
                            new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                            new SqlParameter("@ID", SqlDbType.VarChar)
                        };

                        sqlParam[0].Value = "3";
                        sqlParam[1].Value = foucesRow["ID"].ToString();

                        SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditSurgeryHistoryInfo", sqlParam, CommandType.StoredProcedure);
                        SqlUtil.App.CustomMessageBox.MessageShow("ɾ���ɹ�!");
                        InitForm();

                    }

                }

            }
            catch (Exception ex)
            {
                SqlUtil.App.CustomMessageBox.MessageShow(strTemp + "ʧ��!\n��ϸ����" + ex.Message);
            }
            finally
            {
                //���ֿؼ�����/��ֹ
                btnCancel_Click(this, null);
            }
        }

        #region ��������ʷ��Ϣ
        /// <summary>
        /// ��������ʷ��Ϣ
        /// </summary>
        private void SaveOperationHistory()
        {
            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                        new SqlParameter("@SurgeryID", SqlDbType.VarChar),
                        new SqlParameter("@DiagnosisID", SqlDbType.VarChar),
                        new SqlParameter("@Discuss", SqlDbType.VarChar),
                        new SqlParameter("@Doctor", SqlDbType.VarChar)
                    };

            sqlParam[0].Value = "1";
            sqlParam[1].Value = "-1";
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = lookUpWindowSurgeryID.CodeValue.ToString().Trim();
            sqlParam[4].Value = lookUpWindowDiagnosisID.CodeValue.ToString().Trim();
            sqlParam[5].Value = memoEditDiscuss.Text.ToString().Trim();
            sqlParam[6].Value = txtDoctor.Text.ToString().Trim();

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditSurgeryHistoryInfo", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("��ӳɹ�!");

        }
        #endregion

        #region �޸�����ʷ��Ϣ
        /// <summary>
        /// �޸�����ʷ��Ϣ
        /// </summary>
        /// <param name="foucesRow">һ����¼</param>
        private void ModifyOperationHistory()
        {

            int fouceRowIndex = gridViewSurgeryHistory.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridViewSurgeryHistory.GetDataRow(fouceRowIndex);

            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                         new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                        new SqlParameter("@SurgeryID", SqlDbType.VarChar),
                        new SqlParameter("@DiagnosisID", SqlDbType.VarChar),
                        new SqlParameter("@Discuss", SqlDbType.VarChar),
                        new SqlParameter("@Doctor", SqlDbType.VarChar)
                    };

            sqlParam[0].Value = "2";
            sqlParam[1].Value = foucesRow["ID"].ToString();
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = lookUpWindowSurgeryID.CodeValue.ToString().Trim();
            sqlParam[4].Value = lookUpWindowDiagnosisID.CodeValue.ToString().Trim();
            sqlParam[5].Value = memoEditDiscuss.Text.ToString().Trim();
            sqlParam[6].Value = txtDoctor.Text.ToString().Trim();

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditSurgeryHistoryInfo", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("�޸ĳɹ�!");

        }

        #endregion
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnModify.Enabled = false;
            btnDel.Enabled = false;
            btnCancel.Enabled = true;
            EditControlEnabled(true);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridViewSurgeryHistory.FocusedRowHandle;
            if (fouceRowIndex < 0)
            {
                SqlUtil.App.CustomMessageBox.MessageShow("��ѡ����Ҫ�޸ĵļ�¼! ");
                return;
            }

            btnAdd.Enabled = false;
            btnDel.Enabled = false;
            btnCancel.Enabled = true;
            EditControlEnabled(true);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            SaveUCOperationHistoryInfo(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            btnAdd.Enabled = true;
            btnCancel.Enabled = false;
            EditControlEnabled(false);
        }

        private void gridViewSurgeryHistory_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridViewSurgeryHistory.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridViewSurgeryHistory.GetDataRow(fouceRowIndex);

            string ID = foucesRow["ID"].ToString().Trim();

            foreach (DataRow dr in m_Table.Rows)
            {
                if (dr["ID"].ToString().Equals(ID))
                {
                    txtDoctor.Text = dr["Doctor"].ToString();
                    memoEditDiscuss.Text = dr["Discuss"].ToString();
                    lookUpEditorDiagnosisID.CodeValue = dr["DiagnosisID"].ToString().Trim();
                    lookUpEditorSurgeryID.CodeValue = dr["SurgeryID"].ToString().Trim();
                    
                    break;
                }
            }
        }

    }
}
