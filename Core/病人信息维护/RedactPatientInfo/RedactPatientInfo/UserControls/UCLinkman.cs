using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DrectSoft.Core.RedactPatientInfo.PublicSet;

namespace DrectSoft.Core.RedactPatientInfo.UserControls
{
    public partial class UCLinkman : DevExpress.XtraEditors.XtraUserControl
    {
        string m_NoOfInpat;
        DataTable m_Table;


        public UCLinkman(string NoOfInpat)
        {
            InitializeComponent();

            m_NoOfInpat = NoOfInpat;


        }

        #region ��ʼ������ؼ�
        private void InitForm()
        {
            //��ϵ
            SqlUtil.GetDictionarydetail(lookUpRelation, "13", "44");
            //�Ա�
            SqlUtil.GetDictionarydetail(lookUpSex, "13", "3");

            //��ȡ��һ��ϵ����Ϣ
            SetPatientContacts();

            EditControlEnabled(false);

            btnCancel.Enabled = false;

        }
        #endregion

        #region ��ȡ��һ��ϵ����Ϣ
        /// <summary>
        /// ��ȡ��һ��ϵ����Ϣ
        /// </summary>
        private void SetPatientContacts()
        {
            m_Table = SqlUtil.GetRedactPatientInfoFrm("15", "", m_NoOfInpat);
            gridControlLinkman.DataSource = m_Table;

            SqlUtil.App.PublicMethod.ConvertGridDataSourceUpper(gridviewLinkman);

        }
        #endregion

        #region �༭�ؼ�����/��ֹ
        /// <summary>
        /// �༭�ؼ�����/��ֹ
        /// </summary>
        /// <param name="bolEnabled"></param>
        private void EditControlEnabled(bool bolEnabled)
        {
            txtName.Enabled = bolEnabled;
            txtHomeAddress.Enabled = bolEnabled;
            txtHomeTel.Enabled = bolEnabled;
            txtOfficeTel.Enabled = bolEnabled;
            txtPostalCode.Enabled = bolEnabled;
            txtWorkUnit.Enabled = bolEnabled;
            lookUpRelation.Enabled = bolEnabled;
            lookUpSex.Enabled = bolEnabled;
            chkFirstLinkman.Enabled = bolEnabled;

            gridControlLinkman.Enabled = !bolEnabled;

        }
        #endregion

        #region ������ӡ��޸ġ�ɾ������
        public void SaveUCLinkmanInfo(bool isSave)
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
                        SaveLinkmanInfo();
                        InitForm();
                    }

                }
                //�޸�
                else if (btnModify.Enabled && btnCancel.Enabled && isSave)
                {
                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ���޸ĵ�ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "�޸�";
                        ModifyLinkmanInfo();
                        InitForm();
                    }
                }
                //ɾ��
                else if (btnDel.Enabled && !btnCancel.Enabled && !isSave)
                {
                    int fouceRowIndex = gridviewLinkman.FocusedRowHandle;
                    if (fouceRowIndex < 0)
                    {
                        SqlUtil.App.CustomMessageBox.MessageShow("��ѡ����Ҫɾ���ļ�¼! ");
                        return;
                    }
                    DataRow foucesRow = gridviewLinkman.GetDataRow(fouceRowIndex);

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

                        SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditPatientContacts", sqlParam, CommandType.StoredProcedure);
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

        #region �����һ��ϵ����Ϣ
        /// <summary>
        /// �����һ��ϵ����Ϣ
        /// </summary>
        private void SaveLinkmanInfo()
        {
            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                        new SqlParameter("@Name", SqlDbType.VarChar),
                        new SqlParameter("@Sex", SqlDbType.VarChar),
                        new SqlParameter("@Relation", SqlDbType.VarChar),
                        new SqlParameter("@Address", SqlDbType.VarChar),
                        new SqlParameter("@WorkUnit", SqlDbType.VarChar),
                        new SqlParameter("@HomeTel", SqlDbType.VarChar),
                        new SqlParameter("@WorkTel", SqlDbType.VarChar),
                        new SqlParameter("@PostalCode", SqlDbType.VarChar),
                        new SqlParameter("@Tag", SqlDbType.VarChar)

                    };

            sqlParam[0].Value = "1";
            sqlParam[1].Value = "0";
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = txtName.Text.ToString().Trim();
            sqlParam[4].Value = lookUpSex.EditValue.ToString();
            sqlParam[5].Value = lookUpRelation.EditValue.ToString();
            sqlParam[6].Value = txtHomeAddress.Text.ToString().Trim();
            sqlParam[7].Value = txtWorkUnit.Text.ToString().Trim();
            sqlParam[8].Value = txtHomeTel.Text.ToString().Trim();
            sqlParam[9].Value = txtOfficeTel.Text.ToString().Trim();
            sqlParam[10].Value = txtPostalCode.Text.ToString().Trim();
            sqlParam[11].Value = chkFirstLinkman.Checked ? "1" : "0";

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditPatientContacts", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("��ӳɹ�!");

        }
        #endregion

        #region �޸ĵ�һ��ϵ����Ϣ
        /// <summary>
        /// �޸ĵ�һ��ϵ����Ϣ
        /// </summary>
        /// <param name="foucesRow">һ����¼</param>
        private void ModifyLinkmanInfo()
        {

            int fouceRowIndex = gridviewLinkman.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridviewLinkman.GetDataRow(fouceRowIndex);

            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                        new SqlParameter("@Name", SqlDbType.VarChar),
                        new SqlParameter("@Sex", SqlDbType.VarChar),
                        new SqlParameter("@Relation", SqlDbType.VarChar),
                        new SqlParameter("@Address", SqlDbType.VarChar),
                        new SqlParameter("@WorkUnit", SqlDbType.VarChar),
                        new SqlParameter("@HomeTel", SqlDbType.VarChar),
                        new SqlParameter("@WorkTel", SqlDbType.VarChar),
                        new SqlParameter("@PostalCode", SqlDbType.VarChar),
                        new SqlParameter("@Tag", SqlDbType.VarChar)

                    };

            sqlParam[0].Value = "2";
            sqlParam[1].Value = foucesRow["ID"].ToString();
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = txtName.Text.ToString().Trim();
            sqlParam[4].Value = lookUpSex.EditValue.ToString();
            sqlParam[5].Value = lookUpRelation.EditValue.ToString();
            sqlParam[6].Value = txtHomeAddress.Text.ToString().Trim();
            sqlParam[7].Value = txtWorkUnit.Text.ToString().Trim();
            sqlParam[8].Value = txtHomeTel.Text.ToString().Trim();
            sqlParam[9].Value = txtOfficeTel.Text.ToString().Trim();
            sqlParam[10].Value = txtPostalCode.Text.ToString().Trim();
            sqlParam[11].Value = chkFirstLinkman.Checked ? "1" : "0";

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditPatientContacts", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("�޸ĳɹ�!");

        }

        #endregion
        #endregion

        private void gridviewLinkman_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridviewLinkman.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridviewLinkman.GetDataRow(fouceRowIndex);

            string ID = foucesRow["ID"].ToString().Trim();

            foreach (DataRow dr in m_Table.Rows)
            {
                if (dr["ID"].ToString().Equals(ID))
                {
                    txtName.Text = dr["Name"].ToString();
                    txtHomeAddress.Text = dr["Address"].ToString();
                    txtHomeTel.Text = dr["HomeTel"].ToString();
                    txtOfficeTel.Text = dr["WorkTel"].ToString();
                    txtPostalCode.Text = dr["PostalCode"].ToString();
                    txtWorkUnit.Text = dr["WorkUnit"].ToString();
                    lookUpRelation.EditValue = dr["Relation"].ToString().Trim();
                    lookUpSex.EditValue = dr["Sex"].ToString().Trim();
                    chkFirstLinkman.Checked = dr["Tag"].ToString().Equals("1") ? true : false;

                    break;
                }
            }
        }

        private void UCLinkman_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnModify.Enabled = false;
            btnDel.Enabled = false;
            btnCancel.Enabled = true;
            EditControlEnabled(true);
            return;
            //txtName.Text = "";
            //txtHomeAddress.Text = "";
            //txtHomeTel.Text = "";
            //txtOfficeTel.Text = "";
            //txtPostalCode.Text = "";
            //txtWorkUnit.Text = "";
            //lookUpRelation.EditValue = "";
            //lookUpSex.EditValue = "";
            //chkFirstLinkman.Checked = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridviewLinkman.FocusedRowHandle;
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
            SaveUCLinkmanInfo(false);

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
