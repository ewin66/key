using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DrectSoft.Core.RedactPatientInfo.PublicSet;
using System.Data.SqlClient;
using DrectSoft.Wordbook;

namespace DrectSoft.Core.RedactPatientInfo.UserControls
{
    public partial class UCFamilyHistory : DevExpress.XtraEditors.XtraUserControl
    {
        string m_NoOfInpat;
        DataTable m_Table;

        public UCFamilyHistory( string  NoOfInpat)
        {
            InitializeComponent();
            m_NoOfInpat= NoOfInpat;

        }

        #region ��ʼ������
        private void InitForm()
        {  
            //�����ϵ
            SqlUtil.GetDictionarydetail(lookUpRelation, "21","62");
            //�Ƿ�
            SqlUtil.GetDictionarydetail(lookUpBreathing, "21", "0");
            //���ز���
            GetDiacrisis();
            //���ؼ���ʷ�б�
            gridviewFamilyHistory.SelectAll();
            gridviewFamilyHistory.DeleteSelectedRows();
            m_Table= SqlUtil.App.SqlHelper.ExecuteDataTable("usp_RedactPatientInfoFrm"
                         , new SqlParameter[] 
                        { 
                            new SqlParameter("@NoOfInpat", m_NoOfInpat),
                            new SqlParameter("@FrmType", "22")
                        }
                         , CommandType.StoredProcedure);
            gridControlFamilyHistory.DataSource =m_Table;
            SqlUtil.App.PublicMethod.ConvertGridDataSourceUpper(gridviewFamilyHistory);

            //��/���ò��ֿؼ�
            EditControlEnabled(false);
            btnCancel.Enabled = false;

        }

        #region ��ʼ����������

        //��ʼ����������
        private void GetDiacrisis()
        {
            lookUpWindowDiacrisis.SqlHelper = SqlUtil.App.SqlHelper;

            DataTable Diacrisis = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_RedactPatientInfoFrm",
                 new SqlParameter[] { new SqlParameter("@FrmType", "16") }, CommandType.StoredProcedure);

            Diacrisis.Columns["ICD"].Caption = "��ϴ���";
            Diacrisis.Columns["NAME"].Caption = "��������";

            Dictionary<string, int> cols = new Dictionary<string, int>();

            cols.Add("ICD", 86);
            cols.Add("NAME", 160);

            SqlWordbook deptWordBook = new SqlWordbook("querybook", Diacrisis, "ICD", "NAME", cols);

            //MethodSet.App.PublicMethod.ConvertSqlWordbookDataSourceUpper(deptWordBook);

            lookUpEditorDiacrisis.SqlWordbook = deptWordBook;

        }
        #endregion
        
        #endregion

        #region �༭�ؼ�����/��ֹ
        /// <summary>
        /// �༭�ؼ�����/��ֹ
        /// </summary>
        /// <param name="bolEnabled"></param>
        private void EditControlEnabled(bool bolEnabled)
        {
            memoEditCause.Enabled = bolEnabled;
            memoEditMemo.Enabled = bolEnabled;
            lookUpBreathing.Enabled = bolEnabled;
            lookUpRelation.Enabled = bolEnabled;
            lookUpEditorDiacrisis.Enabled = bolEnabled;
            dateEditBirthday.Enabled = bolEnabled;

            gridControlFamilyHistory.Enabled = !bolEnabled;

        }
        #endregion

        #region ������ӡ��޸ġ�ɾ������
        public void SaveUCFamilyHistoryInfo(bool isSave)
        {
            string strTemp = "����";
            try
            {

                //���
                if (btnAdd.Enabled && btnCancel.Enabled && isSave)
                {
                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ����ӵ�ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "���";
                        SaveFamilyHistory();
                        InitForm();
                    }

                }
                //�޸�
                else if (btnModify.Enabled && btnCancel.Enabled && isSave)
                {
                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ���޸ĵ�ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "�޸�";
                        ModifyFamilyHistory();
                        InitForm();
                    }
                }
                //ɾ��
                else if (btnDel.Enabled && !btnCancel.Enabled && !isSave)
                {
                    int fouceRowIndex = gridviewFamilyHistory.FocusedRowHandle;
                    if (fouceRowIndex < 0)
                    {
                        SqlUtil.App.CustomMessageBox.MessageShow("��ѡ����Ҫɾ���ļ�¼! ");
                        return;
                    }
                    DataRow foucesRow = gridviewFamilyHistory.GetDataRow(fouceRowIndex);

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

                        SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditFamilyHistoryInfo", sqlParam, CommandType.StoredProcedure);
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

        #region �������ʷ��Ϣ
        /// <summary>
        /// �������ʷ��Ϣ
        /// </summary>
        private void SaveFamilyHistory()
        {
            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                        new SqlParameter("@Relation", SqlDbType.VarChar),
                        new SqlParameter("@Birthday", SqlDbType.VarChar),
                        new SqlParameter("@DiseaseID", SqlDbType.VarChar),
                        new SqlParameter("@Breathing", SqlDbType.VarChar),
                        new SqlParameter("@Cause", SqlDbType.VarChar),
                        new SqlParameter("@Memo", SqlDbType.VarChar)
                    };

            sqlParam[0].Value = "1";
            sqlParam[1].Value = "0";
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = lookUpRelation.EditValue.ToString().Trim();
            sqlParam[4].Value = dateEditBirthday.DateTime.Date.ToString("yyyy-MM-dd").Trim();
            sqlParam[5].Value = lookUpEditorDiacrisis.CodeValue.ToString().Trim();
            sqlParam[6].Value = lookUpBreathing.EditValue.ToString().Trim();
            sqlParam[7].Value = memoEditCause.Text.ToString().Trim();
            sqlParam[8].Value = memoEditMemo.Text.ToString().Trim();

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditFamilyHistoryInfo", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("��ӳɹ�!");

        }
        #endregion

        #region �޸ļ���ʷ��Ϣ
        /// <summary>
        /// �޸ļ���ʷ��Ϣ
        /// </summary>
        /// <param name="foucesRow">һ����¼</param>
        private void ModifyFamilyHistory()
        {

            int fouceRowIndex = gridviewFamilyHistory.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridviewFamilyHistory.GetDataRow(fouceRowIndex);

            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                        new SqlParameter("@Relation", SqlDbType.VarChar),
                        new SqlParameter("@Birthday", SqlDbType.VarChar),
                        new SqlParameter("@DiseaseID", SqlDbType.VarChar),
                        new SqlParameter("@Breathing", SqlDbType.VarChar),
                        new SqlParameter("@Cause", SqlDbType.VarChar),
                        new SqlParameter("@Memo", SqlDbType.VarChar)
                    };

            sqlParam[0].Value = "2";
            sqlParam[1].Value = foucesRow["ID"].ToString(); 
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = lookUpRelation.EditValue.ToString().Trim();
            sqlParam[4].Value = dateEditBirthday.DateTime.Date.ToString("yyyy-MM-dd").Trim();
            sqlParam[5].Value = lookUpEditorDiacrisis.CodeValue.ToString().Trim();
            sqlParam[6].Value = lookUpBreathing.EditValue.ToString().Trim();
            sqlParam[7].Value = memoEditCause.Text.ToString().Trim();
            sqlParam[8].Value = memoEditMemo.Text.ToString().Trim();

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditFamilyHistoryInfo", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("�޸ĳɹ�!");

        }

        #endregion
        #endregion

        private void UCFamilyHistory_Load(object sender, EventArgs e)
        {        
            InitForm();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnModify.Enabled = false;
            btnDel.Enabled = false;
            btnCancel.Enabled = true;
            EditControlEnabled(true);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridviewFamilyHistory.FocusedRowHandle;
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
            SaveUCFamilyHistoryInfo(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            btnAdd.Enabled = true;
            btnCancel.Enabled = false;
            EditControlEnabled(false);
        }

        private void gridviewFamilyHistory_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridviewFamilyHistory.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridviewFamilyHistory.GetDataRow(fouceRowIndex);

            string ID= foucesRow["ID"].ToString().Trim();

            foreach (DataRow dr in m_Table.Rows)
            {
                if (dr["ID"].ToString().Equals(ID))
                {                                         
                    memoEditCause.Text=  dr["Cause"].ToString();
                    memoEditMemo.Text = dr["Memo"].ToString();
                    lookUpBreathing.EditValue =  dr["Breathing"].ToString();
                    lookUpRelation.EditValue = dr["Relation"].ToString();
                    lookUpEditorDiacrisis.CodeValue =  dr["DiseaseID"].ToString();
                    dateEditBirthday.Text  = dr["Birthday"].ToString();

                    break;
                }
            }
        }

      
    }
}


