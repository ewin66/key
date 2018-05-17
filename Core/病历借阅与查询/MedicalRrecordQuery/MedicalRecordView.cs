using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using DrectSoft.Common;
using DrectSoft.Common.Ctrs.DLG;
using DrectSoft.Common.Ctrs.FORM;
using DrectSoft.Service;
using DrectSoft.FrameWork;
using DrectSoft.FrameWork.WinForm;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;

namespace DrectSoft.Core.MedicalRecordQuery
{
    public partial class MedicalRecordView : DevBaseForm, IStartPlugIn
    {
        IEmrHost m_app;
        IDataAccess sql_Helper;

        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public MedicalRecordView()
        {
            InitializeComponent();
        }
        #endregion

        #region IStartPlugIn ��Ա

        public IPlugIn Run(IEmrHost host)
        {
            PlugIn plg = new PlugIn(this.GetType().ToString(), this);

            m_app = host;
            sql_Helper = m_app.SqlHelper;

            return plg;
        }

        #endregion

        #region Private

        /// <summary>
        /// ���ز���
        /// edit by Yanqiao.Cai 2012-11-05
        /// 1��add try ... catch
        /// 2�������ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadRecord()
        {
            try
            {
                int rowHandle = xTabPageNoOnFile.SelectedTabPage == xtraTabPageNoOnFile ? gridviewMedicalRecord.FocusedRowHandle : gridviewMedicalRecordOnFile.FocusedRowHandle;
                if (rowHandle < 0)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("��ѡ��һ����¼");
                    return;
                }
                DataRow dataRow;
                if (xTabPageNoOnFile.SelectedTabPage == xtraTabPageNoOnFile)
                {//δ�鵵������¼
                    dataRow = gridviewMedicalRecord.GetDataRow(gridviewMedicalRecord.FocusedRowHandle);
                }
                else
                {
                    dataRow = gridviewMedicalRecordOnFile.GetDataRow(gridviewMedicalRecordOnFile.FocusedRowHandle);
                }
                if (null == dataRow)
                {
                    return;
                }
                if (xTabPageNoOnFile.SelectedTabPage == xTabPageOnFile)
                {//�ѹ鵵������¼
                    if (dataRow["ApplyStatusID"].ToString() != Convert.ToInt32(BorrowStatus.ApplyPass).ToString())
                    {
                        DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("�ò���״̬Ϊ " + dataRow["ApplyStatus"] + "������Ȩ�鿴��");
                        return;
                    }
                }
                string deptid = Convert.ToString(dataRow["OutHosDept"]);
                if (m_app.User.CurrentDeptId != deptid && !m_app.User.RelateDeptWards.Any(pt => pt.DeptId.Equals(deptid)))
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("�ò��˲������Ĺ������ڣ�����Ȩ�鿴��");
                    return;
                }
                string noofinpat = dataRow["NOOFINPAT"].ToString();

                #region ��ע�� by cyq 2013-02-18
                //string msg;
                //string noofinpat = GetCurrentNOofinpat(out msg);
                //if (!string.IsNullOrEmpty(msg))
                //{
                //    Common.Ctrs.DLG.MessageBox.Show(msg);
                //    return;
                //}
                //if (string.IsNullOrEmpty(noofinpat))
                //{
                //    Common.Ctrs.DLG.MessageBox.Show("��ѡ��һ��������¼");
                //    return;
                //}
                #endregion

                //edit by wyt 2012-11-09 �½�������ʾ����
                EmrBrowser frm = new EmrBrowser(noofinpat, m_app);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();

                #region ȡ�����ز����ʽ��ʾ���� edit by wyt 2012-11-09
                //m_app.ChoosePatient(syxh);
                //m_app.LoadPlugIn("DrectSoft.Core.MainEmrPad.MainForm,DrectSoft.Core.MainEmrPad");
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetCurrentNOofinpat(out string msg)
        {
            try
            {
                msg = string.Empty;
                GridView view = gridviewMedicalRecordOnFile;
                if (xTabPageNoOnFile.SelectedTabPage != xTabPageOnFile)
                {
                    view = gridviewMedicalRecord;
                }


                if (view.FocusedRowHandle < 0)
                {
                    return "";
                }
                else
                {
                    DataRow dataRow = view.GetDataRow(view.FocusedRowHandle);
                    if (dataRow == null) return "";

                    if (xTabPageNoOnFile.SelectedTabPage == xTabPageOnFile)
                    {
                        if (dataRow["ApplyStatusID"].ToString() != Convert.ToInt32(BorrowStatus.ApplyPass).ToString())
                        {
                            return "";
                        }
                    }

                    string deptid = Convert.ToString(dataRow["OutHosDept"]);
                    if (m_app.User.RelateDeptWards.Where(pt => pt.DeptId.Equals(deptid)).Count() < 0)
                    {
                        msg = "�û��߲������Ĺ�������";
                        return "";
                    }

                    return dataRow["NOOFINPAT"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void InitSqlWorkBook()
        {
            try
            {
                lookUpWindowDepartment.SqlHelper = sql_Helper;
                //����
                DataTable Dept = sql_Helper.ExecuteDataTable("usp_GetMedicalRrecordViewFrm",
                     new SqlParameter[] { new SqlParameter("@GetType", "1") }, CommandType.StoredProcedure);
                Dept.Columns["ID"].Caption = "���ұ���";
                Dept.Columns["NAME"].Caption = "��������";
                Dictionary<string, int> cols = new Dictionary<string, int>();
                cols.Add("ID", 60);
                cols.Add("NAME", 120);
                SqlWordbook deptWordBook = new SqlWordbook("querybook", Dept, "ID", "NAME", cols, "ID//NAME//PY//WB");
                lookUpEditorDepartment.SqlWordbook = deptWordBook;
                lookUpEditorDepartment.CodeValue = DS_Common.currentUser.CurrentDeptId;

                //���
                this.lookUpWindowInDiag.SqlHelper = sql_Helper;
                this.lookUpWindowOutDiag.SqlHelper = sql_Helper;
                this.lookUpWindowCurrentDiag.SqlHelper = sql_Helper;
                Dictionary<string, int> col = new Dictionary<string, int>();
                //edit by cyq 2013-01-07 �°�&�ϰ�����ҽʦ(����)
                DataTable diagDt = new DataTable();
                string config = DS_SqlService.GetConfigValueByKey("ImportDiseasesGroupHistoryData");
                XmlDocument doc = new XmlDocument();
                if (!string.IsNullOrEmpty(config))
                {
                    doc.LoadXml(config);
                }
                XmlNodeList nodeList = doc.GetElementsByTagName("isnew");
                string isnew = string.Empty;
                if (null != nodeList && nodeList.Count > 0)
                {
                    isnew = nodeList[0].InnerText;
                }
                if (!string.IsNullOrEmpty(isnew) && isnew.Trim() == "1")
                {//�°�����ҽʦ
                    diagDt = DS_BaseService.GetDiagResourceByUserID(DS_Common.currentUser.Id);
                    if (null == diagDt || diagDt.Rows.Count == 0)
                    {
                        if (!diagDt.Columns.Contains("ICD"))
                        {
                            diagDt.Columns.Add("ICD");
                        }
                        if (!diagDt.Columns.Contains("NAME"))
                        {
                            diagDt.Columns.Add("NAME");
                        }
                        if (!diagDt.Columns.Contains("PY"))
                        {
                            diagDt.Columns.Add("PY");
                        }
                        if (!diagDt.Columns.Contains("WB"))
                        {
                            diagDt.Columns.Add("WB");
                        }
                    }
                    diagDt.Columns["ICD"].Caption = "��ϱ���";
                    diagDt.Columns["NAME"].Caption = "�������";
                    col.Add("ICD", 60);
                    col.Add("NAME", 120);
                    SqlWordbook inWordBook = new SqlWordbook("inDiag", diagDt, "ICD", "NAME", col, "ICD//NAME//PY//WB");
                    this.lookUpEditorInDiag.SqlWordbook = inWordBook;
                    SqlWordbook outWordBook = new SqlWordbook("outDiag", diagDt, "ICD", "NAME", col, "ICD//NAME//PY//WB");
                    this.lookUpEditorOutDiag.SqlWordbook = outWordBook;
                    SqlWordbook currentWordBook = new SqlWordbook("curDiag", diagDt, "ICD", "NAME", col, "ICD//NAME//PY//WB");
                    this.lookUpEditorCurrentDiag.SqlWordbook = currentWordBook;
                }
                else
                {//ԭ������ҽʦ
                    #region ԭ������ݼ�
                    DataTable diagofuser = new DataTable();
                    diagofuser.Columns.Add("ICD");
                    diagofuser.Columns.Add("DIAGNOSIS");
                    diagofuser.Columns.Add("PY");
                    diagofuser.Columns.Add("WB");
                    diagofuser.Columns["ICD"].Caption = "��ϱ���";
                    diagofuser.Columns["DIAGNOSIS"].Caption = "�������";
                    DataTable diag = sql_Helper.ExecuteDataTable("usp_getdiagbyattendphysician",
                         new SqlParameter[] { new SqlParameter("@USERID", m_app.User.Id) }, CommandType.StoredProcedure);
                    if (diag.Rows.Count != 0)
                    {
                        string diagnames = diag.Rows[0][0].ToString();
                        string[] name = diagnames.Split('��');
                        for (int i = 0; i < name.Length; i++)
                        {
                            if (name[i].Trim() != "")
                            {
                                if (name[i].Trim().Contains("$"))
                                {
                                    string[] str = name[i].Split('$');
                                    DataRow dr = diagofuser.NewRow();
                                    dr[0] = str[0];
                                    dr[1] = str[1];
                                    diagofuser.Rows.Add(dr);
                                }
                                else if (name[i].Trim().Contains("-"))
                                {
                                    string[] str = name[i].Split('-');
                                    DataRow dr = diagofuser.NewRow();
                                    dr[0] = str[0];
                                    dr[1] = str[1];
                                    diagofuser.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    #endregion

                    diagDt = diagofuser;
                    new GenerateShortCode(sql_Helper).AutoAddShortCode(diagDt, "DIAGNOSIS");
                    col.Add("ICD", 60);
                    col.Add("DIAGNOSIS", 120);
                    SqlWordbook inWordBook = new SqlWordbook("inDiag", diagDt, "ICD", "DIAGNOSIS", col, "ICD//DIAGNOSIS//PY//WB");
                    this.lookUpEditorInDiag.SqlWordbook = inWordBook;
                    SqlWordbook outWordBook = new SqlWordbook("outDiag", diagDt, "ICD", "DIAGNOSIS", col, "ICD//DIAGNOSIS//PY//WB");
                    this.lookUpEditorOutDiag.SqlWordbook = outWordBook;
                    SqlWordbook currentWordBook = new SqlWordbook("curDiag", diagDt, "ICD", "DIAGNOSIS", col, "ICD//DIAGNOSIS//PY//WB");
                    this.lookUpEditorCurrentDiag.SqlWordbook = currentWordBook;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��ʼ���������
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2013-01-10</date>
        /// </summary>
        private void InitDiseaseGroup()
        {
            try
            {
                this.lookUpWindowInDiagGroup.SqlHelper = sql_Helper;
                this.lookUpWindowOutDiagGroup.SqlHelper = sql_Helper;
                DataTable diseaseGroupDT = DS_BaseService.GetDiseaseGroupsByUserID(DS_Common.currentUser.Id);
                if (null == diseaseGroupDT || diseaseGroupDT.Rows.Count == 0)
                {
                    if (!diseaseGroupDT.Columns.Contains("ID"))
                    {
                        diseaseGroupDT.Columns.Add("ID");
                    }
                    if (!diseaseGroupDT.Columns.Contains("NAME"))
                    {
                        diseaseGroupDT.Columns.Add("NAME");
                    }
                    if (!diseaseGroupDT.Columns.Contains("PY"))
                    {
                        diseaseGroupDT.Columns.Add("PY");
                    }
                    if (!diseaseGroupDT.Columns.Contains("WB"))
                    {
                        diseaseGroupDT.Columns.Add("WB");
                    }
                }
                diseaseGroupDT.Columns["NAME"].Caption = "������";

                Dictionary<string, int> cols = new Dictionary<string, int>();
                cols.Add("NAME", lookUpEditorInDiagGroup.Width);

                SqlWordbook inGroupWordBook = new SqlWordbook("queryname", diseaseGroupDT, "ID", "NAME", cols, "NAME//PY//WB");
                SqlWordbook outGroupWordBook = new SqlWordbook("queryname", diseaseGroupDT, "ID", "NAME", cols, "NAME//PY//WB");
                this.lookUpEditorInDiagGroup.SqlWordbook = inGroupWordBook;
                this.lookUpEditorOutDiagGroup.SqlWordbook = outGroupWordBook;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Events

        //���ݲ���ICD���ز������� --add by wyt 2012-08-15
        private string GetDiagNameByICD(string icd)
        {
            string name = "";
            for (int i = 0; i < icd.Length; i++)
            {
                DataTable diagname = sql_Helper.ExecuteDataTable("usp_getdiagnamebyicd",
                     new SqlParameter[] { new SqlParameter("@ICD", icd) }, CommandType.StoredProcedure);
                if (diagname.Rows.Count != 0)
                {
                    name = diagname.Rows[0][0].ToString();
                }
            }
            return name;
        }

        //���ݲ���name���ز���icd --add by wyt 2012-08-15
        private string GetDiagICDByName(string name)
        {
            string icd = "";
            for (int i = 0; i < name.Length; i++)
            {
                DataTable diagicd = sql_Helper.ExecuteDataTable("usp_getdiagicdbyname",
                     new SqlParameter[] { new SqlParameter("@NAME", name) }, CommandType.StoredProcedure);
                if (diagicd.Rows.Count != 0)
                {
                    icd = diagicd.Rows[0][0].ToString();
                }
            }
            return icd;
        }

        private void btnApplyExamine_Click(object sender, EventArgs e)
        {
            ApplyExamine frm = new ApplyExamine(m_app);
            frm.ShowDialog(this.Owner);

        }

        /// <summary>
        /// ��������¼�
        /// edit by ywk 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MedicalRecordView_Load(object sender, EventArgs e)
        {
            //���µ��ڵĽ��Ĳ���
            sql_Helper.ExecuteNoneQuery("usp_UpDateDueApplyRecord",
                 new SqlParameter[] { new SqlParameter("@ApplyDoctor", m_app.User.Id) }, CommandType.StoredProcedure);

            InitSqlWorkBook();
            InitDiseaseGroup();
            Reset();
            this.dateEditInBegin.Focus();

            //Ŀǰ���ƹ���Ա�ɿ�����������ҽʦ��ť��Ȩ��
            string[] roles = GetConfigValueByKey("SetAttendPhysicianRole");
            if (m_app.User.GWCodes != "" && roles != null && roles.Length != 0)
            {
                string[] jobids = m_app.User.GWCodes.Split(',');
                foreach (string jobid in jobids)
                {
                    if (jobid.Trim() != "")
                        foreach (string role in roles)
                        {
                            if (jobid.Trim() == role.Trim())
                            {
                                btnSetAttenDoc.Visible = true;
                                return;
                            }
                        }
                }
            }
            btnSetAttenDoc.Visible = false;
        }

        /// <summary>
        /// �õ�������Ϣ  ywk 2012��5��21�� 11:29:14
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] GetConfigValueByKey(string key)
        {
            try
            {
                if (m_app == null) return null;
                string sql1 = " select * from appcfg where configkey = '" + key + "'  ";
                DataTable dt = m_app.SqlHelper.ExecuteDataTable(sql1, CommandType.Text);
                string config = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    config = dt.Rows[0]["value"].ToString();
                }
                if (config != "")
                {
                    string[] configs = config.Split(',');
                    return configs;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #region public methods of WaitDialog

        public void SetWaitDialogCaption(string caption)
        {
            if (m_WaitDialog != null)
            {
                if (!m_WaitDialog.Visible)
                    m_WaitDialog.Visible = true;
                m_WaitDialog.Caption = caption;
            }

        }

        public void HideWaitDialog()
        {
            if (m_WaitDialog != null)
                m_WaitDialog.Hide();
        }
        #endregion
        /// <summary>
        /// �����е��õĵȴ�����
        /// add by ywk 2012��12��13��16:04:41
        /// </summary>
        private WaitDialogForm m_WaitDialog;
        /// <summary>
        /// ��ѯ�¼�
        /// edit by Yanqiao.Cai 2012-11-15
        /// 1��add try ... catch
        /// 2���Ա����ͼƬ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryMedicalRecord_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] sqlParam;
                DataTable MedicalRrecordSet = new DataTable();
                if (xTabPageOnFile.TabControl.SelectedTabPage == xtraTabPageNoOnFile)
                {
                    //�Ա����ͼƬ
                    m_WaitDialog = new WaitDialogForm("���ڲ�ѯ����...", "���Ե�");
                    DS_Common.InitializeImage_XB(repositoryItemImageXB, imageListBrxb);
                    sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@DeptID", SqlDbType.VarChar),
                        new SqlParameter("@DateOutHosBegin", SqlDbType.VarChar),
                        new SqlParameter("@DateOutHosEnd", SqlDbType.VarChar),
                        new SqlParameter("@DateInHosBegin", SqlDbType.VarChar),
                        new SqlParameter("@DateInHosEnd", SqlDbType.VarChar),
                        new SqlParameter("@Status", SqlDbType.VarChar),
                        new SqlParameter("@PatientName", SqlDbType.VarChar),
                        new SqlParameter("@RecordID", SqlDbType.VarChar),
                        new SqlParameter("@InDiag", SqlDbType.VarChar),
                        new SqlParameter("@OutDiag", SqlDbType.VarChar),
                        new SqlParameter("@CurDiag", SqlDbType.VarChar),
                        new SqlParameter("@diaGroupStatus", SqlDbType.VarChar),
                        new SqlParameter("@PatientStatus", SqlDbType.VarChar),
                        new SqlParameter("@ingroupid", SqlDbType.VarChar),
                        new SqlParameter("@outgroupid", SqlDbType.VarChar)
                    };
                    sqlParam[0].Value = lookUpEditorDepartment.CodeValue == "0000" ? "" : lookUpEditorDepartment.CodeValue;
                    sqlParam[1].Value = dateEditStart.DateTime.Date.ToString("yyyy-MM-dd");
                    DateTime dteditEnd = DateTime.Now;
                    if (dateEditEnd.DateTime.Date.ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        dteditEnd = dateEditEnd.DateTime.Date;
                    }
                    sqlParam[2].Value = dteditEnd.ToString("yyyy-MM-dd");
                    sqlParam[3].Value = dateEditInBegin.DateTime.Date.ToString("yyyy-MM-dd");

                    DateTime dteditInEnd = DateTime.Now;
                    if (dateEditInEnd.DateTime.Date.ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        dteditInEnd = dateEditInEnd.DateTime.Date;
                    }

                    sqlParam[4].Value = dteditInEnd.ToString("yyyy-MM-dd");
                    sqlParam[5].Value = "4700"; //"'4700','4702','4703'";

                    if (cmbPatientInfo.SelectedIndex == 0)
                    {//������
                        sqlParam[6].Value = string.Empty;
                        sqlParam[7].Value = txtcmbPatientInfo.Text.Trim();
                    }
                    else
                    {//��������
                        sqlParam[6].Value = txtcmbPatientInfo.Text.Trim();
                        sqlParam[7].Value = string.Empty;
                    }
                    sqlParam[8].Value = this.lookUpEditorInDiag.CodeValue.Trim();
                    sqlParam[9].Value = this.lookUpEditorOutDiag.CodeValue.Trim();
                    sqlParam[10].Value = "";// this.lookUpEditorCurrentDiag.CodeValue.Trim();
                    sqlParam[11].Value = this.rdg_diag.SelectedIndex.ToString();
                    #region
                    //if (this.rdg_patient.SelectedIndex == 2)
                    //{
                    //    sqlParam[11].Value = "1";
                    //    sqlParam[12].Value = "2";
                    //}
                    //else
                    //{
                    //    sqlParam[11].Value = "1";
                    //    sqlParam[12].Value = "1";
                    //}
                    #endregion
                    sqlParam[12].Value = this.rdg_patient.SelectedIndex.ToString();
                    sqlParam[13].Value = "";
                    sqlParam[14].Value = "";
                    if (this.rdg_diag.SelectedIndex == 1)
                    {
                        sqlParam[8].Value = "";
                        sqlParam[9].Value = "";
                        //��Ժ���
                        if (!string.IsNullOrEmpty(this.lookUpEditorInDiagGroup.CodeValue))
                        {
                            #region
                            //int groupID = int.Parse(this.lookUpEditorInDiagGroup.CodeValue.Trim());
                            //List<string> diseaseIDs = DS_BaseService.GetDiseaseIDsByGroupID(groupID);
                            //string filterExpression = string.Empty;
                            //for (int i = 0; i < diseaseIDs.Count; i++)
                            //{
                            //    filterExpression += "'" + diseaseIDs[i] + "',";
                            //}
                            //filterExpression = filterExpression.Substring(0, filterExpression.Length - 1);
                            sqlParam[13].Value = "";//"admitdiagnosis in (" + filterExpression + ")";
                            #endregion
                            sqlParam[13].Value = this.lookUpEditorInDiagGroup.CodeValue.Trim();
                        }
                        //��Ժ���
                        if (!string.IsNullOrEmpty(this.lookUpEditorOutDiagGroup.CodeValue))
                        {
                            #region
                            //int groupID = int.Parse(this.lookUpEditorOutDiagGroup.CodeValue.Trim());
                            //List<string> diseaseIDs = DS_BaseService.GetDiseaseIDsByGroupID(groupID);
                            //string filterExpression = string.Empty;
                            //for (int i = 0; i < diseaseIDs.Count; i++)
                            //{
                            //    filterExpression += "'" + diseaseIDs[i] + "',";
                            //}
                            //filterExpression = filterExpression.Substring(0, filterExpression.Length - 1);
                            sqlParam[13].Value = "";//"diagnosis_code in (" + filterExpression + ")";
                            #endregion
                            sqlParam[14].Value = this.lookUpEditorOutDiagGroup.CodeValue.Trim();
                        }
                    }
                    MedicalRrecordSet = sql_Helper.ExecuteDataTable("usp_getattendrecordnoonfile", sqlParam, CommandType.StoredProcedure);
                    #region
                    ////edit by cyq 2013-01-10
                    //List<DataRow> fillterList = MedicalRrecordSet.Select(" 1=1 ").ToList();
                    //if (this.rdg_patient.SelectedIndex == 1)
                    //{//��Ժ����
                    //    fillterList = fillterList.Where(p => p["STATUS"].ToString() == "1500" || p["STATUS"].ToString() == "1501").ToList();
                    //}
                    //else if (this.rdg_patient.SelectedIndex == 2)
                    //{//��Ժ����
                    //    fillterList = fillterList.Where(p => p["STATUS"].ToString() == "1502" || p["STATUS"].ToString() == "1503").ToList();
                    //}
                    //if (null != fillterList && fillterList.Count() > 0)
                    //{
                    //    MedicalRrecordSet = fillterList.CopyToDataTable();
                    //}
                    //else
                    //{
                    //    MedicalRrecordSet = MedicalRrecordSet.Clone();
                    //}

                    //List<DataRow> fillterList1 = fillterList;

                    ////�����  zyx 2013-1-11
                    //if (this.rdg_diag.SelectedIndex == 1)
                    //{
                    //    //��Ժ���
                    //    if (!string.IsNullOrEmpty(this.lookUpEditorInDiagGroup.CodeValue))
                    //    {
                    //        int groupID = int.Parse(this.lookUpEditorInDiagGroup.CodeValue.Trim());
                    //        List<string> diseaseIDs = DS_BaseService.GetDiseaseIDsByGroupID(groupID);
                    //        string filterExpression = string.Empty;
                    //        for (int i = 0; i < diseaseIDs.Count; i++)
                    //        {
                    //            filterExpression += "'" + diseaseIDs[i] + "',";
                    //        }
                    //        filterExpression = filterExpression.Substring(0, filterExpression.Length - 1);
                    //        filterExpression = "admitdiagnosis in (" + filterExpression + ")";
                    //        if (MedicalRrecordSet.Select(filterExpression).Count() > 0)
                    //        {
                    //            MedicalRrecordSet = MedicalRrecordSet.Select(filterExpression).CopyToDataTable();
                    //        }
                    //        else
                    //        {
                    //            gridControlMedicalRecord.DataSource = null;
                    //            lbl_totalCount1.Text = "��0����¼";
                    //            m_app.CustomMessageBox.MessageShow("û�з��������ļ�¼");
                    //            HideWaitDialog();
                    //            return;
                    //        }
                    //    }
                    //    //��Ժ���
                    //    if (!string.IsNullOrEmpty(this.lookUpEditorOutDiagGroup.CodeValue))
                    //    {
                    //        int groupID = int.Parse(this.lookUpEditorOutDiagGroup.CodeValue.Trim());
                    //        List<string> diseaseIDs = DS_BaseService.GetDiseaseIDsByGroupID(groupID);
                    //        string filterExpression = string.Empty;
                    //        for (int i = 0; i < diseaseIDs.Count; i++)
                    //        {
                    //            filterExpression += "'" + diseaseIDs[i] + "',";
                    //        }
                    //        filterExpression.Substring(0, filterExpression.Length - 1);
                    //        filterExpression = "diagnosis_code in (" + filterExpression + ")";
                    //        if (MedicalRrecordSet.Select(filterExpression).Count() > 0)
                    //        {
                    //            MedicalRrecordSet = MedicalRrecordSet.Select(filterExpression).CopyToDataTable();
                    //        }
                    //        else
                    //        {
                    //            gridControlMedicalRecord.DataSource = null;
                    //            lbl_totalCount1.Text = "��0����¼";
                    //            m_app.CustomMessageBox.MessageShow("û�з��������ļ�¼");
                    //            HideWaitDialog();
                    //            return;
                    //        }
                    //    }
                    //}
                    #endregion
                    //MedicalRrecordSet = fillterList.CopyToDataTable();

                    lbl_totalCount1.Text = "��" + MedicalRrecordSet.Rows.Count + "����¼";
                    gridControlMedicalRecord.DataSource = MedicalRrecordSet;
                    m_app.PublicMethod.ConvertGridDataSourceUpper(gridviewMedicalRecord);
                    this.HideWaitDialog();
                }
                else
                {
                    m_WaitDialog = new WaitDialogForm("���ڲ�ѯ����...", "���Ե�");
                    //�Ա����ͼƬ
                    DS_Common.InitializeImage_XB(repositoryItemImageXB2, imageListBrxb);
                    sqlParam = new SqlParameter[] 
                    { 
                        new SqlParameter("@DeptCode", SqlDbType.VarChar),
                        new SqlParameter("@DateTimeBegin", SqlDbType.VarChar),
                        new SqlParameter("@DateTimeEnd", SqlDbType.VarChar),
                        new SqlParameter("@DateTimeInBegin", SqlDbType.VarChar),
                        new SqlParameter("@DateTimeInEnd", SqlDbType.VarChar),
                        new SqlParameter("@PatientName", SqlDbType.VarChar),
                        new SqlParameter("@RecordID", SqlDbType.VarChar),
                        new SqlParameter("@ApplyDoctor", SqlDbType.VarChar),
                        new SqlParameter("@QCStatType", SqlDbType.VarChar),
                        new SqlParameter("@Patid", SqlDbType.VarChar),
                        new SqlParameter("@indiag", SqlDbType.VarChar),
                        new SqlParameter("@outdiag", SqlDbType.VarChar),
                        new SqlParameter("@curdiag", SqlDbType.VarChar)//,
                        //   new SqlParameter("@PatientStatus", SqlDbType.VarChar)
                    };
                    sqlParam[0].Value = lookUpEditorDepartment.CodeValue == "0000" ? "" : lookUpEditorDepartment.CodeValue;
                    sqlParam[1].Value = dateEditStart.DateTime.Date.ToString("yyyy-MM-dd");
                    sqlParam[2].Value = dateEditEnd.DateTime.Date.ToString("yyyy-MM-dd");
                    sqlParam[3].Value = dateEditInBegin.DateTime.Date.ToString("yyyy-MM-dd");
                    sqlParam[4].Value = dateEditInEnd.DateTime.Date.ToString("yyyy-MM-dd");

                    if (cmbPatientInfo.SelectedIndex == 0)
                    {
                        sqlParam[5].Value = "";
                        sqlParam[6].Value = txtcmbPatientInfo.Text.Trim();
                        //sqlParam[3].Value = "";
                        //sqlParam[4].Value = txtcmbPatientInfo.Text.Trim();
                    }
                    else
                    {
                        sqlParam[5].Value = txtcmbPatientInfo.Text.Trim();
                        sqlParam[6].Value = "";
                        //sqlParam[3].Value = txtcmbPatientInfo.Text.Trim();
                        //sqlParam[4].Value = "";
                    }
                    //sqlParam[5].Value = m_app.User.Id;
                    //sqlParam[6].Value = 1;
                    //sqlParam[7].Value = "";
                    sqlParam[7].Value = m_app.User.Id;
                    sqlParam[8].Value = 1;
                    sqlParam[9].Value = "";
                    sqlParam[10].Value = this.lookUpEditorInDiag.CodeValue.Trim();
                    sqlParam[11].Value = this.lookUpEditorOutDiag.CodeValue.Trim();
                    sqlParam[12].Value = this.lookUpEditorCurrentDiag.CodeValue.Trim();
                    //if (this.rdg_patient.SelectedIndex == 2)
                    //{
                    //    sqlParam[13].Value = "2";
                    //}
                    //else
                    //{
                    //    sqlParam[13].Value = "1";
                    //}
                    MedicalRrecordSet = sql_Helper.ExecuteDataTable("usp_GetMedicalRrecordViewNew", sqlParam, CommandType.StoredProcedure);

                    List<DataRow> fillterList = MedicalRrecordSet.Select(" 1=1 ").ToList();
                    if (this.rdg_patient.SelectedIndex == 1)
                    {//��Ժ����
                        fillterList = fillterList.Where(p => p["STATUS"].ToString() == "1500" || p["STATUS"].ToString() == "1501").ToList();
                    }
                    else if (this.rdg_patient.SelectedIndex == 2)
                    {//��Ժ����
                        fillterList = fillterList.Where(p => p["STATUS"].ToString() == "1502" || p["STATUS"].ToString() == "1503").ToList();
                    }
                    if (null != fillterList && fillterList.Count() > 0)
                    {
                        MedicalRrecordSet = fillterList.CopyToDataTable();
                    }
                    else
                    {
                        MedicalRrecordSet = MedicalRrecordSet.Clone();
                    }

                    List<DataRow> fillterList1 = fillterList;
                    //�����  zyx 2013-1-11
                    if (this.rdg_diag.SelectedIndex == 1)
                    {
                        //��Ժ���
                        if (!string.IsNullOrEmpty(this.lookUpEditorInDiagGroup.CodeValue))
                        {
                            int groupID = int.Parse(this.lookUpEditorInDiagGroup.CodeValue.Trim());
                            List<string> diseaseIDs = DS_BaseService.GetDiseaseIDsByGroupID(groupID);
                            string filterExpression = string.Empty;
                            for (int i = 0; i < diseaseIDs.Count; i++)
                            {
                                filterExpression += "'" + diseaseIDs[i] + "',";
                            }
                            filterExpression.Substring(0, filterExpression.Length - 1);
                            filterExpression = "admitdiagnosis in (" + filterExpression + ")";
                            if (MedicalRrecordSet.Select(filterExpression).Count() > 0)
                            {
                                MedicalRrecordSet = MedicalRrecordSet.Select(filterExpression).CopyToDataTable();
                            }
                            else
                            {
                                gridControlMedicalRecord.DataSource = null;
                                lbl_totalCount2.Text = "��" + MedicalRrecordSet.Rows.Count + "����¼";
                                m_app.CustomMessageBox.MessageShow("û�з��������ļ�¼");
                                HideWaitDialog();
                                return;
                            }
                        }
                        //��Ժ���
                        if (!string.IsNullOrEmpty(this.lookUpEditorOutDiagGroup.CodeValue))
                        {
                            int groupID = int.Parse(this.lookUpEditorOutDiagGroup.CodeValue.Trim());
                            List<string> diseaseIDs = DS_BaseService.GetDiseaseIDsByGroupID(groupID);
                            string filterExpression = string.Empty;
                            for (int i = 0; i < diseaseIDs.Count; i++)
                            {
                                filterExpression += "'" + diseaseIDs[i] + "',";
                            }
                            filterExpression.Substring(0, filterExpression.Length - 1);
                            filterExpression = "diagnosis_code in (" + filterExpression + ")";
                            if (MedicalRrecordSet.Select(filterExpression).Count() > 0)
                            {
                                MedicalRrecordSet = MedicalRrecordSet.Select(filterExpression).CopyToDataTable();
                            }
                            else
                            {
                                gridControlMedicalRecord.DataSource = null;
                                lbl_totalCount2.Text = "��" + MedicalRrecordSet.Rows.Count + "����¼";
                                m_app.CustomMessageBox.MessageShow("û�з��������ļ�¼");
                                HideWaitDialog();
                                return;
                            }
                        }
                    }

                    gridControlMedicalRecordOnFile.DataSource = MedicalRrecordSet;
                    lbl_totalCount2.Text = "��" + MedicalRrecordSet.Rows.Count + "����¼";
                    m_app.PublicMethod.ConvertGridDataSourceUpper(gridviewMedicalRecordOnFile);
                    this.HideWaitDialog();
                }
                if (MedicalRrecordSet.Rows.Count <= 0)
                {
                    m_app.CustomMessageBox.MessageShow("û�з��������ļ�¼");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// �����¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-29</date>
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
        /// ����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-29</date>
        /// </summary>
        private void Reset()
        {
            try
            {
                dateEditStart.DateTime = DateTime.Now.AddMonths(-3);
                dateEditEnd.DateTime = DateTime.Now;
                dateEditInBegin.DateTime = DateTime.Now.AddMonths(-3);
                dateEditInEnd.DateTime = DateTime.Now;

                this.lookUpEditorInDiag.CodeValue = string.Empty;
                this.lookUpEditorOutDiag.CodeValue = string.Empty;
                this.lookUpEditorCurrentDiag.CodeValue = string.Empty;
                this.lookUpEditorDepartment.CodeValue = DS_Common.currentUser.CurrentDeptId;
                this.cmbPatientInfo.SelectedIndex = 0;
                this.txtcmbPatientInfo.Text = string.Empty;

                this.rdg_diag.SelectedIndex = 0;
                this.rdg_patient.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// ��ת����������ҽʦ����
        /// add by ywk 2012��8��9�� 17:21:47
        /// edit by Yanqiao.Cai 2012-12-28
        /// 1��add try ... catch
        /// 2������ҽʦ����ҳ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAttenDoc_Click(object sender, EventArgs e)
        {
            try
            {
                string config = DS_SqlService.GetConfigValueByKey("ImportDiseasesGroupHistoryData");
                XmlDocument doc = new XmlDocument();
                if (!string.IsNullOrEmpty(config))
                {
                    doc.LoadXml(config);
                }
                XmlNodeList nodeList = doc.GetElementsByTagName("isnew");
                string isnew = string.Empty;
                if (null != nodeList && nodeList.Count > 0)
                {
                    isnew = nodeList[0].InnerText;
                }
                if (!string.IsNullOrEmpty(isnew) && isnew.Trim() == "1")
                {
                    SetAttenDoc();
                }
                else
                {
                    SetAttendDoctor setDoc = new SetAttendDoctor(m_app, btnSetAttenDoc.Text);
                    setDoc.StartPosition = FormStartPosition.CenterParent;
                    setDoc.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ����ҽʦ���� --- �°�
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-12-28</date>
        private void SetAttenDoc()
        {
            try
            {
                SetAttendDoctorForm setDoc = new SetAttendDoctorForm(m_app);
                setDoc.StartPosition = FormStartPosition.CenterParent;
                setDoc.ShowDialog();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���������¼�
        /// edit by Yanqiao.Cai 2012-11-09
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseMedicalRecord_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRecord();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
        /// <summary>
        /// �б�˫���¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridviewMedicalRecord_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridHitInfo hitInfo = gridviewMedicalRecord.CalcHitInfo(gridControlMedicalRecord.PointToClient(Cursor.Position));
                if (hitInfo.RowHandle < 0)
                {
                    return;
                }
                LoadRecord();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
        /// <summary>
        /// �б�˫���¼�
        /// add by wyt 2012-11-09
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridviewMedicalRecordOnFile_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridHitInfo hitInfo = gridviewMedicalRecordOnFile.CalcHitInfo(gridControlMedicalRecordOnFile.PointToClient(Cursor.Position));
                if (hitInfo.RowHandle < 0)
                {
                    return;
                }
                LoadRecord();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
        /// <summary>
        /// �л���ǩҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xTabPageNoOnFile_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                if (xTabPageNoOnFile.SelectedTabPage == xTabPageOnFile)
                {
                    lookUpEditorDepartment.CodeValue = m_app.User.CurrentDeptId;
                    lookUpEditorDepartment.Enabled = false;
                }
                else
                {
                    lookUpEditorDepartment.Enabled = true;
                }
                this.lookUpEditorInDiag.Focus();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
        #endregion

        #region Enum

        /// <summary>
        /// ����״̬
        /// </summary>
        private enum BorrowStatus : int
        {
            /// <summary>
            /// ������
            /// </summary>
            UnderApprove = 5201,

            /// <summary>
            /// ����ͨ��
            /// </summary>
            ApplyPass = 5202,

            /// <summary>
            /// ����δͨ��
            /// </summary>
            ApplyUnPass = 5203,

            /// <summary>
            /// ��������
            /// </summary>
            ApplyInvalid = 5204,

            /// <summary>
            /// ���ĵ���
            /// </summary>
            Expire = 5205,

            /// <summary>
            /// ��ǩ��
            /// </summary>
            SignIn = 5206,
        }

        #endregion

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-29</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridviewMedicalRecord_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-29</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridviewMedicalRecordOnFile_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
        /// add by ��� 2012-12-09
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_export_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "����Excel";
                saveFileDialog.Filter = "Excel�ļ�(*.xls)|*.xls";
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.SheetName = "������Ϣ";
                    options.ShowGridLines = true;
                    if (xTabPageNoOnFile.SelectedTabPage == xtraTabPageNoOnFile)
                    {
                        string caption = gridControlMedicalRecord.MainView.ViewCaption;
                        gridControlMedicalRecord.MainView.ViewCaption = options.SheetName;
                        gridControlMedicalRecord.ExportToXls(saveFileDialog.FileName, options);
                        MessageBox.Show("�����ɹ�");
                        gridControlMedicalRecord.MainView.ViewCaption = caption;
                    }
                    else
                    {
                        string caption = gridControlMedicalRecordOnFile.MainView.ViewCaption;
                        gridControlMedicalRecordOnFile.MainView.ViewCaption = options.SheetName;
                        gridControlMedicalRecordOnFile.ExportToXls(saveFileDialog.FileName, options);
                        MessageBox.Show("�����ɹ�");
                        gridControlMedicalRecordOnFile.MainView.ViewCaption = caption;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ����ϡ�������л��¼�
        /// </summary>
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2013-01-10</date>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdg_diag_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rdg_diag.SelectedIndex == 0)
                {//�����
                    this.lookUpEditorInDiag.Enabled = true;
                    this.lookUpEditorOutDiag.Enabled = true;
                    this.lookUpEditorInDiagGroup.Enabled = false;
                    this.lookUpEditorOutDiagGroup.Enabled = false;
                    this.lookUpEditorInDiagGroup.CodeValue = string.Empty;
                    this.lookUpEditorOutDiagGroup.CodeValue = string.Empty;
                    this.lookUpEditorInDiag.Focus();
                }
                else if (this.rdg_diag.SelectedIndex == 1)
                {//�����
                    this.lookUpEditorInDiag.Enabled = false;
                    this.lookUpEditorOutDiag.Enabled = false;
                    this.lookUpEditorInDiagGroup.Enabled = true;
                    this.lookUpEditorOutDiagGroup.Enabled = true;
                    this.lookUpEditorInDiag.CodeValue = string.Empty;
                    this.lookUpEditorOutDiag.CodeValue = string.Empty;
                    this.lookUpEditorInDiagGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

    }
}
