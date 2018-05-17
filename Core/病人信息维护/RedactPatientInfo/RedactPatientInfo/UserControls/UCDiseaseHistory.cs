using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DrectSoft.Core.RedactPatientInfo.PublicSet;
using DrectSoft.Wordbook;

namespace DrectSoft.Core.RedactPatientInfo.UserControls
{
    public partial class UCDiseaseHistory : DevExpress.XtraEditors.XtraUserControl
    {
        string m_NoOfInpat;
        DataTable m_Table;

        public UCDiseaseHistory(string NoOfInpat)
        {
            InitializeComponent();
            m_NoOfInpat = NoOfInpat;

            
        }

        #region ��ʼ������
        private void InitForm()
        {
           //��ʼ����������
            GetDiacrisis();

            //��ȡ����ʷ��¼
            GetOperationHistory();

            //��/���ò��ֿؼ�
            EditControlEnabled(false);
            btnCancel.Enabled = false;

        }
               

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
        //��ȡ������¼
        private void GetOperationHistory()
        {

            m_Table = SqlUtil.App.SqlHelper.ExecuteDataTable("usp_RedactPatientInfoFrm",
                 new SqlParameter[] { new SqlParameter("@FrmType", "27"), new SqlParameter("@NoOfInpat", m_NoOfInpat) }, CommandType.StoredProcedure);
            gridViewDiseaseHistory.SelectAll();
            gridViewDiseaseHistory.DeleteSelectedRows();
            gridControlDiseaseHistory.DataSource = m_Table;

            SqlUtil.App.PublicMethod.ConvertGridDataSourceUpper(gridViewDiseaseHistory);
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
            lookUpEditorDiagnosisID.Enabled = bolEnabled;
            memoEditDiscuss.Enabled = bolEnabled;
            dateEditDate.Enabled = bolEnabled;
            radioGroupCure.Enabled = bolEnabled;

            gridControlDiseaseHistory.Enabled = !bolEnabled;

        }
        #endregion

        #region ������ӡ��޸ġ�ɾ������
        public void SaveUCDiseaseHistoryInfo(bool isSave)
        {
            if (lookUpEditorDiagnosisID.CodeValue == null ||
                dateEditDate.Text.ToString().Trim()=="" ||
                radioGroupCure.SelectedIndex ==-1 )
            {
                SqlUtil.App.CustomMessageBox.MessageShow("�����������⣬����������д!");
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
                        SaveDiseaseHistory();
                        InitForm();
                    }

                }
                //�޸�
                else if (btnModify.Enabled && btnCancel.Enabled && isSave)
                {
                    if (SqlUtil.App.CustomMessageBox.MessageShow("ȷ���޸ĵ�ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes)
                    {
                        strTemp = "�޸�";
                        ModifyDiseaseHistory();
                        InitForm();
                    }
                }
                //ɾ��
                else if (btnDel.Enabled && !btnCancel.Enabled && !isSave)
                {
                    int fouceRowIndex = gridViewDiseaseHistory.FocusedRowHandle;
                    if (fouceRowIndex < 0)
                    {
                        SqlUtil.App.CustomMessageBox.MessageShow("��ѡ����Ҫɾ���ļ�¼! ");
                        return;
                    }
                    DataRow foucesRow = gridViewDiseaseHistory.GetDataRow(fouceRowIndex);

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

                        SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditIllnessHistoryInfo", sqlParam, CommandType.StoredProcedure);
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

        #region ���漲��ʷ��Ϣ
        /// <summary>
        /// ���漲��ʷ��Ϣ
        /// </summary>
        private void SaveDiseaseHistory()
        {
            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                        new SqlParameter("@DiagnosisICD", SqlDbType.VarChar),
                        new SqlParameter("@Discuss", SqlDbType.VarChar),
                        new SqlParameter("@DiseaseTime", SqlDbType.VarChar),
                        new SqlParameter("@Cure", SqlDbType.VarChar)
                    };

            sqlParam[0].Value = "1";
            sqlParam[1].Value = "-1";
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value =lookUpEditorDiagnosisID.CodeValue.ToString().Trim();
            sqlParam[4].Value = memoEditDiscuss.Text.ToString().Trim();
            sqlParam[5].Value = dateEditDate.DateTime.Date.ToString("yyyy-MM-dd");
            sqlParam[6].Value = radioGroupCure.SelectedIndex.ToString();

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditIllnessHistoryInfo", sqlParam, CommandType.StoredProcedure);
            SqlUtil.App.CustomMessageBox.MessageShow("��ӳɹ�!");

        }
        #endregion

        #region �޸ļ���ʷ��Ϣ
        /// <summary>
        /// �޸ļ���ʷ��Ϣ
        /// </summary>
        /// <param name="foucesRow">һ����¼</param>
        private void ModifyDiseaseHistory()
        {

            int fouceRowIndex = gridViewDiseaseHistory.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridViewDiseaseHistory.GetDataRow(fouceRowIndex);

            SqlParameter[] sqlParam = new SqlParameter[] 
                    { 
                         new SqlParameter("@EditType", SqlDbType.VarChar),//�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@NoOfInpat", SqlDbType.VarChar),
                        new SqlParameter("@DiagnosisICD", SqlDbType.VarChar),
                        new SqlParameter("@Discuss", SqlDbType.VarChar),
                        new SqlParameter("@DiseaseTime", SqlDbType.VarChar),
                        new SqlParameter("@Cure", SqlDbType.VarChar)
                    };

            sqlParam[0].Value = "2";
            sqlParam[1].Value = foucesRow["ID"].ToString();
            sqlParam[2].Value = string.IsNullOrEmpty(m_NoOfInpat) ? "0" : m_NoOfInpat;
            sqlParam[3].Value = lookUpEditorDiagnosisID.CodeValue.ToString().Trim();
            sqlParam[4].Value = memoEditDiscuss.Text.ToString().Trim();
            sqlParam[5].Value = dateEditDate.DateTime.Date.ToString("yyyy-MM-dd");
            sqlParam[6].Value = radioGroupCure.SelectedIndex.ToString();

            SqlUtil.App.SqlHelper.ExecuteNoneQuery("usp_EditIllnessHistoryInfo", sqlParam, CommandType.StoredProcedure);
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
            int fouceRowIndex = gridViewDiseaseHistory.FocusedRowHandle;
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
            SaveUCDiseaseHistoryInfo(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            btnAdd.Enabled = true;
            btnCancel.Enabled = false;
            EditControlEnabled(false);
        }

        private void gridViewDiseaseHistory_Click(object sender, EventArgs e)
        {
            int fouceRowIndex = gridViewDiseaseHistory.FocusedRowHandle;
            if (fouceRowIndex < 0) return;
            DataRow foucesRow = gridViewDiseaseHistory.GetDataRow(fouceRowIndex);

            string ID = foucesRow["ID"].ToString().Trim();

            foreach (DataRow dr in m_Table.Rows)
            {
                if (dr["ID"].ToString().Equals(ID))
                {
                    memoEditDiscuss.Text = dr["Discuss"].ToString();
                    lookUpEditorDiagnosisID.CodeValue = dr["DiagnosisICD"].ToString();
                    dateEditDate.Text = dr["DiseaseTime"].ToString().Trim();
                    radioGroupCure.SelectedIndex = int.Parse(dr["Cure"].ToString().Trim() == "" ? "-1" : dr["Cure"].ToString());

                    break;
                }
            }
        }


    }
}
