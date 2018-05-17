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

namespace DrectSoft.Core.RedactPatientInfo.UserControls
{
    public partial class UCAllergicHistory : DevExpress.XtraEditors.XtraUserControl
    {
        string m_NoOfInpat;
        DataTable m_Table;


        public UCAllergicHistory(string NoOfInpat)
        {
            InitializeComponent();
            m_NoOfInpat = NoOfInpat;

        }

        #region ��ʼ������
        private void InitForm()
        {
            //��������
            SqlUtil.GetDictionarydetail(lookUpAllergyID, "21", "60");
            //�����ȼ�
            SqlUtil.GetDictionarydetail(lookUpAllergyLevel, "21", "61");

            //��ȡ����ʷ��¼
            GetAllergicHistory();

            //��/���ò��ֿؼ�
            EditControlEnabled(false);
            btnCancel.Enabled = false;

        }

        #region ��ȡ����ʷ��¼
        //��ȡ����ʷ��¼
        private void GetAllergicHistory()
        {

            m_Table = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_RedactPatientInfoFrm",
                 new SqlParameter[] { new SqlParameter("@FrmType", "24"), new SqlParameter("@NoOfInpat", m_NoOfInpat) }, CommandType.StoredProcedure);
            gridViewAllergicHistory.SelectAll();
            gridViewAllergicHistory.DeleteSelectedRows();
            gridControlAllergicHistory.DataSource = m_Table;

            SqlUtil.App.PublicMethod.ConvertGridDataSourceUpper(gridViewAllergicHistory);
           
        }
        #endregion

        private void UCAllergicHistory_Load(object sender, EventArgs e)
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
            lookUpAllergyLevel.Enabled = bolEnabled;
            lookUpAllergyID.Enabled = bolEnabled;
            txtAllergyPart.Enabled = bolEnabled;
            txtDoctor.Enabled = bolEnabled;
            txtReactionType.Enabled = bolEnabled;

            gridControlAllergicHistory.Enabled = !bolEnabled;

        }
        #endregion

        #region ������ӡ��޸ġ�ɾ������
        public void SaveUCAllergicHistoryInfo(bool isSave)
        {
            if (lookUpAllergyID.EditValue == null || lookUpAllergyLevel.EditValue == null)
            {
                SqlUtil.App.CustomMessageBox.MessageShow("�������ͺ͹����̶ȱ�����д!");
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
                        SaveAllergicHistory();
                        InitForm();
                    }

                }
                //�޸�
                else if (btnModify.Enabled && btnCancel.Enabled && isSave)
                {
                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ���޸ĵ�ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "�޸�";
                        ModifyAllergicHistory();
                        InitForm();
                    }
                }
                //ɾ��
                else if (btnDel.Enabled && !btnCancel.Enabled && !isSave)
                {
                    int fouceRowIndex = gridViewAllergicHistory.FocusedRowHandle;
                    if (fouceRowIndex < 0)
                    {
                        SqlUtil.App.CustomMessageBox.MessageShow("��ѡ����Ҫɾ���ļ�¼! ");
                        return;
                    }
                    DataRow foucesRow = gridViewAllergicHistory.GetDataRow(fouceRowIndex);

                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ��ɾ����ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "ɾ��";
                        SqlParameter[] sqlParam = new SqlParameter[] 
                        { 
                            new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                            new SqlParameter("@ID", SqlDbType.Int)
                        };

                        sqlParam[0].Value = "3";
                        sqlParam[1].Value = foucesRow["ID"].ToString();

                        SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditAllergyHistoryInfo", sqlParam, CommandType.StoredProcedure);
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
        private void SaveAllergicHistory()
        {
            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.Int),
                        new SqlParameter("@NoOfInpat", SqlDbType.Int),
                        new SqlParameter("@AllergyPart", SqlDbType.VarChar),
                        new SqlParameter("@Doctor", SqlDbType.VarChar),
                        new SqlParameter("@AllergyLevel", SqlDbType.Int),
                        new SqlParameter("@AllergyID", SqlDbType.Int),
                        new SqlParameter("@ReactionType", SqlDbType.VarChar)
                    };

            sqlParam[0].Value = "1";
            sqlParam[1].Value = "-1";
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = txtAllergyPart.Text.ToString().Trim();
            sqlParam[4].Value = txtDoctor.Text.ToString().Trim();
            sqlParam[5].Value = lookUpAllergyLevel.EditValue.ToString().Trim();
            sqlParam[6].Value = lookUpAllergyID.EditValue.ToString().Trim();
            sqlParam[7].Value = txtReactionType.Text.ToString().Trim();

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditAllergyHistoryInfo", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("��ӳɹ�!");

        }
        #endregion

        #region �޸Ĺ���ʷ��Ϣ
        /// <summary>
        /// �޸Ĺ���ʷ��Ϣ
        /// </summary>
        /// <param name="foucesRow">һ����¼</param>
        private void ModifyAllergicHistory()
        {

            int fouceRowIndex = gridViewAllergicHistory.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridViewAllergicHistory.GetDataRow(fouceRowIndex);

            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.Int),
                        new SqlParameter("@NoOfInpat", SqlDbType.Int),
                        new SqlParameter("@AllergyPart", SqlDbType.VarChar),
                        new SqlParameter("@Doctor", SqlDbType.VarChar),
                        new SqlParameter("@AllergyLevel", SqlDbType.Int),
                        new SqlParameter("@AllergyID", SqlDbType.Int),
                        new SqlParameter("@ReactionType", SqlDbType.VarChar)
                    };

            sqlParam[0].Value = "2";
            sqlParam[1].Value = foucesRow["ID"].ToString();
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = txtAllergyPart.Text.ToString().Trim();
            sqlParam[4].Value = txtDoctor.Text.ToString().Trim();
            sqlParam[5].Value = lookUpAllergyLevel.EditValue.ToString().Trim();
            sqlParam[6].Value =lookUpAllergyID.EditValue.ToString().Trim();
            sqlParam[7].Value = txtReactionType.Text.ToString().Trim();

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditAllergyHistoryInfo", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("�޸ĳɹ�!");

        }

        #endregion
        #endregion

        private void gridViewAllergicHistory_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridViewAllergicHistory.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridViewAllergicHistory.GetDataRow(fouceRowIndex);

            string ID = foucesRow["ID"].ToString().Trim();

            foreach (DataRow dr in m_Table.Rows)
            {
                if (dr["ID"].ToString().Equals(ID))
                {
                    txtAllergyPart.Text = dr["AllergyPart"].ToString();
                    txtDoctor.Text = dr["Doctor"].ToString();
                    lookUpAllergyLevel.EditValue = dr["AllergyLevel"].ToString().Trim();
                    lookUpAllergyID.EditValue = dr["AllergyID"].ToString().Trim();
                    txtReactionType.Text = dr["ReactionType"].ToString();

                    break;
                }
            }
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
            int fouceRowIndex = gridViewAllergicHistory.FocusedRowHandle;
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
            SaveUCAllergicHistoryInfo(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            btnAdd.Enabled = true;
            btnCancel.Enabled = false;
            EditControlEnabled(false);
        }

        

    
    }
}
