using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DrectSoft.Common;
using DrectSoft.FrameWork;
using DrectSoft.FrameWork.WinForm;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;
namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ʱ��������ѯ����
    /// </summary>
    public partial class FormTimeQCUnLock : Form, IStartPlugIn
    {
        #region fields

        IEmrHost app;
        DoctorManagerPatientInfoDal _dmpDal;

        #endregion

        #region "������"
        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        private void InitDept()
        {
            lookUpWindowDept.SqlHelper = app.SqlHelper;
            DataTable depts = app.SqlHelper.ExecuteDataTable("select distinct a.ID,a.NAME,a.PY,a.WB from DEPARTMENT  a ,Dept2ward b where a.ID=b.deptid");

            depts.Columns["ID"].Caption = "���ұ��";
            depts.Columns["NAME"].Caption = "��������";

            Dictionary<string, int> cols = new Dictionary<string, int>();
            cols.Add("ID", 60);
            cols.Add("NAME", 90);

            SqlWordbook deptWorkBook = new SqlWordbook("querybook", depts, "ID", "NAME", cols, "ID//NAME//PY//WB");
            lookUpEditorDept.SqlWordbook = deptWorkBook;
        }

        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        private void InitDoc(string deptCode)
        {
            lookUpWindowDoc.SqlHelper = app.SqlHelper;
            DataTable docs = app.SqlHelper.ExecuteDataTable("select ID,NAME,PY,WB from users where valid != 0 and grade != '2004' and deptid='" + deptCode + "' order by name ");

            docs.Columns["ID"].Caption = "ҽ������";
            docs.Columns["NAME"].Caption = "ҽ������";
            Dictionary<string, int> cols = new Dictionary<string, int>();
            cols.Add("ID", 60);
            cols.Add("NAME", 90);

            SqlWordbook docWorkBook = new SqlWordbook("querybook", docs, "ID", "NAME", cols, "ID//NAME//PY//WB");
            lookUpEditorDoc.SqlWordbook = docWorkBook;
        }

        //����-ҽ�� ����
        private void lookUpEditorDept_CodeValueChanged(object sender, EventArgs e)
        {
            InitDoc(lookUpEditorDept.CodeValue);
        }
        #endregion
        CGridCheckMarksSelection selection;
        /// <summary>
        /// ����
        /// </summary>
        public FormTimeQCUnLock()
        {
            InitializeComponent();

            _dmpDal = new DoctorManagerPatientInfoDal();
            //userCtrlTimeQcInfo.ContextMenuStrip = contextMenuStrip1;

            //ToolStripMenuItemRefresh.Click += new EventHandler(ToolStripMenuItemRefresh_Click);
            //this.Load += new EventHandler(FormTimeQCInfo_Load);
        }

        public void FormTimeQCInfo_Load(object sender, EventArgs e)
        {
            //userCtrlTimeQcInfo.App = a                     pp;
            //userCtrlTimeQcInfo.CheckDoctorTime(app.User.DoctorId, true);

            //��ʼ�����ҡ�ҽ������������
            InitDept();
            InitDoc(lookUpEditorDept.CodeValue);

            //�ʿ�ʱ����Ϣ
            string userIDs = GetConfigValueByKey("TimeLimitUsers");
            List<string> userIdList = userIDs.Split(',').ToList();
            if (!string.IsNullOrEmpty(userIDs) && (userIdList.Contains(app.User.Id) || (app.User.Id.Length >= 6 && userIdList.Contains(app.User.Id.Substring(app.User.Id.Length - 4, 4)))))
            {
                xtraTabPage2.PageVisible = true;
            }

            Reset();

            selection = new CGridCheckMarksSelection(this.gridViewTimeLimit);//�Ѷ�ѡ��󶨵���ָ����grid
            selection.CheckMarkColumn.VisibleIndex = 0;//ʹ��ѡ���ŵ�һ��
        }

        void ToolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            //userCtrlTimeQcInfo.CheckDoctorTime(app.User.DoctorId, true);

        }

        #region IStartup Members

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public IPlugIn Run(IEmrHost application)
        {

            app = application;
            PlugIn plg = new PlugIn(this.GetType().ToString(), this);
            return plg;
        }

        #endregion

        /// <summary>
        /// ��ѯ�¼�
        /// edit by Yanqiao.Cai 2012-11-16
        /// 1��add try ... catch
        /// 2�������Ա�ͼƬ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_query_Click(object sender, EventArgs e)
        {
            try
            {
                if (date_inpStart.DateTime > date_inpEnd.DateTime)
                {
                    app.CustomMessageBox.MessageShow("��ʼ���ڲ��ܴ��ڽ�������");
                    return;
                }
                //�����Ա�ͼƬ
                DS_Common.InitializeImage_XB(repositoryItemImageXB, imageListXB);
                gridTimeLimit.DataSource = GetGridViewData(CreateDataTable(), GetGridViewBaseData());
            }
            catch (Exception ex)
            {
                app.CustomMessageBox.MessageShow(ex.Message);
            }
        }

        /// <summary>
        /// ��ȡ�������ݼ�
        /// </summary>
        /// <returns></returns>
        public DataTable GetGridViewBaseData()
        {
            try
            {
                string sqlStr = @"select a.ID,a.ConditionTime,a.FoulState,REALCONDITIONTIME,a.ResultTime,a.Reminder,a.FoulMessage,b.Name as PatName,b.sexid,case b.sexid when '1' then '��' when '2' then 'Ů' else 'δ֪' end as Sex,b.Age,b.NoOfInpat,b.NoOfRecord,b.PatID,b.Outbed,b.inwarddate as InTime,c.id UserID,c.Name UserName,b.admitdept DeptID,p.name as DeptName,d.TimeLimit 
                              from QCRecord a left join InPatient b on a.NoOfInpat=b.NoOfInpat left join QCRule d on a.RuleCode=d.RuleCode  left join Users c on b.resident = c.ID left join department p on b.admitdept=p.id 
                              where a.Valid!=0 and a.FoulState in (1) and d.RuleCode is not null ";
                if (!string.IsNullOrEmpty(lookUpEditorDept.CodeValue))
                {
                    sqlStr += " and b.admitdept = '" + lookUpEditorDept.CodeValue + "'";
                }
                if (!string.IsNullOrEmpty(lookUpEditorDoc.CodeValue))
                {
                    sqlStr += " and c.id = '" + lookUpEditorDoc.CodeValue + "'";
                }
                if (!string.IsNullOrEmpty(text_patientSN.Text))
                {
                    sqlStr += " and b.NoOfRecord = '" + text_patientSN.Text + "'";
                }
                if (null != date_inpStart.DateTime)
                {
                    sqlStr += " and b.inwarddate >= '" + date_inpStart.DateTime.ToString("yyyy-MM-dd 00:00:00") + "'";
                }
                if (null != date_inpEnd.DateTime)
                {
                    sqlStr += " and b.inwarddate <= '" + date_inpEnd.DateTime.ToString("yyyy-MM-dd 23:59:59") + "'";
                }
                if (radioButton_locked.Checked)
                {
                    sqlStr += "  and nvl(a.islock,0)<>1 ";

                }
                if (radioButton_unchecked.Checked)
                {
                    sqlStr += "  and nvl(a.islock,0)=1 ";

                }
                if (textEdit_name.Text.Trim() != "")
                {
                    sqlStr += "  and b.Name like '%" + textEdit_name.Text.Trim() + "%' ";
                }
                sqlStr += " order by c.Name asc,b.Name,a.RuleCode asc";

                return app.SqlHelper.ExecuteDataTable(sqlStr, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��ȡ���յ����ݼ�
        /// Edit by xlb �µĲ���ʱ����Ϣ����ʱ�����ж� ��REALCONDITIONTIME
        /// 2013-02-26
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="dtTimeLimit"></param>
        public DataTable GetGridViewData(DataTable infos, DataTable dtTimeLimit)
        {
            //if (null == dtTimeLimit)
            //{
            //    return infos;
            //}
            try
            {
                for (int i = 0; i < dtTimeLimit.Rows.Count; i++)
                {
                    DataRow row = dtTimeLimit.Rows[i];
                    RuleRecordState recordstate = (RuleRecordState)int.Parse(row["FoulState"].ToString());
                    DateTime conditionTime = DateTime.Parse(row["ConditionTime"].ToString());
                    if (recordstate == RuleRecordState.None || recordstate == RuleRecordState.DoIntime || conditionTime > DateTime.Now)
                    {
                        continue;
                    }
                    DataRow newRow = infos.NewRow();
                    newRow["UserName"] = row["UserName"].ToString().Trim(); //+ "(" + row["UserID"].ToString().Trim() + ")";
                    newRow["PatName"] = row["PatName"].ToString().Trim();// + "(" + row["Patid"].ToString().Trim() + ")";
                    newRow["DeptName"] = row["DeptName"];
                    newRow["Age"] = null == row["Age"] ? "" : (row["Age"].ToString() + "��");
                    newRow["SexID"] = row["SexID"];
                    newRow["Sex"] = row["Sex"];
                    newRow["NoOfRecord"] = row["NoOfRecord"];
                    newRow["InTime"] = row["InTime"];
                    newRow["ID"] = row["ID"];

                    //�����ɹ�ʱ�� ������ʱ����ӳ�ʱ��
                    DateTime dateConditionTime = DateTime.Parse(row["REALCONDITIONTIME"].ToString());
                    //����ʱ��
                    DateTime dateNow = DateTime.Now;
                    //ʱ������ ����Ϊ��λ
                    double timeLimit = double.Parse(row["TIMELIMIT"].ToString());

                    TimeSpan tsLimit = (TimeSpan)(dateNow - dateConditionTime);
                    //Υ��״̬
                    string foulState = row["FOULSTATE"].ToString().Trim();
                    //ʱ������ת��Ϊָ����ʽ
                    TimeSpan lTimeLimit = new TimeSpan(0, 0, 0, Convert.ToInt32(timeLimit));
                    switch (foulState)
                    {
                        case "1"://1��ʾΥ��
                            newRow["TimeLimit"] = "����" + DS_Common.TimeSpanToLocal(tsLimit - lTimeLimit);
                            newRow["TipWarnInfo"] = row["FOULMESSAGE"];
                            infos.Rows.Add(newRow);
                            break;
                        case "0":
                            if (tsLimit > lTimeLimit)
                            {
                                newRow["TipWarnInfo"] = row["FOULMESSAGE"].ToString();
                                newRow["TimeLimit"] = "����" + DS_Common.TimeSpanToLocal(tsLimit - lTimeLimit);
                                infos.Rows.Add(newRow);
                            }
                            else
                            {
                                newRow["TipWarnInfo"] = row["REMINDER"].ToString();
                                newRow["TimeLimit"] = "��ʣ" + DS_Common.TimeSpanToLocal(lTimeLimit - tsLimit);
                                infos.Rows.Add(newRow);
                            }
                            break;
                        default:
                            break;
                    }
                    #region ��ע�� by xlb 2013-02-26
                    //Ԥ������յĲ��
                    //DateTime dtResultTime = DateTime.Parse(row["ResultTime"].ToString());
                    //DateTime dtConditionTime = DateTime.Parse(row["ConditionTime"].ToString());
                    //TimeSpan tsTimeLimit = (TimeSpan)(dtResultTime - dtConditionTime);
                    //double dblTimeLimit = double.Parse(row["TimeLimit"].ToString());
                    //TimeSpan tsDefTimeLimit = new TimeSpan(0, 0, 0, Convert.ToInt32(dblTimeLimit));
                    //switch (recordstate)
                    //{
                    //case RuleRecordState.UndoIntime:
                    //tsTimeLimit = DateTime.Now - dtConditionTime;
                    //if (tsTimeLimit > tsDefTimeLimit)
                    //{
                    // recordstate = RuleRecordState.UndoOuttime;
                    //}
                    //break;
                    //case RuleRecordState.DoIntime:
                    // break;
                    //case RuleRecordState.UndoOuttime:
                    //break;
                    //case RuleRecordState.DoOuttime:
                    //break;
                    //default:
                    //break;
                    //}
                    //newRow["TimeLimit"] = tsTimeLimit > tsDefTimeLimit ? (ConstRes.cstOverTime + TimeSpan2Chn(tsTimeLimit - tsDefTimeLimit)) : (ConstRes.cstOnTime + TimeSpan2Chn(tsDefTimeLimit - tsTimeLimit));

                    //if (recordstate == RuleRecordState.UndoIntime)
                    //{
                    //    newRow["TipWarnInfo"] = row["Reminder"];
                    //}
                    //else if (recordstate == RuleRecordState.DoOuttime || recordstate == RuleRecordState.UndoOuttime)
                    //{
                    //    newRow["TipWarnInfo"] = row["FoulMessage"];
                    //}
                    //infos.Rows.Add(newRow);
                    #endregion
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return infos;
        }

        string TimeSpan2Chn(TimeSpan span)
        {
            int day = span.Days;
            int hour = span.Hours;
            int min = span.Minutes;
            string chnspan = string.Empty;
            if (day > 0) chnspan += day.ToString() + ConstRes.cstDayChn;
            if (hour > 0) chnspan += hour.ToString() + ConstRes.cstHourChn;
            if (min > 0) chnspan += min.ToString() + ConstRes.cstMinuteChn;
            return chnspan;
        }

        /// <summary>
        /// ����DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable CreateDataTable()
        {
            try
            {
                DataTable newTable = new DataTable();
                newTable.Columns.Add(new DataColumn("UserName", typeof(string)));
                newTable.Columns.Add(new DataColumn("PatName", typeof(string)));
                newTable.Columns.Add(new DataColumn("DeptName", typeof(string)));
                newTable.Columns.Add(new DataColumn("Age", typeof(string)));
                newTable.Columns.Add(new DataColumn("SexID", typeof(string)));
                newTable.Columns.Add(new DataColumn("Sex", typeof(string)));
                newTable.Columns.Add(new DataColumn("NoOfRecord", typeof(string)));
                newTable.Columns.Add(new DataColumn("InTime", typeof(string)));
                newTable.Columns.Add(new DataColumn("TimeLimit", typeof(string)));
                newTable.Columns.Add(new DataColumn("TipWarnInfo", typeof(string)));
                newTable.Columns.Add(new DataColumn("ID", typeof(string)));

                return newTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// �õ�������Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfigValueByKey(string key)
        {
            try
            {
                if (app == null) return "";
                string sql1 = " select * from appcfg where configkey = '" + key + "'  ";
                DataTable dt = app.SqlHelper.ExecuteDataTable(sql1, CommandType.Text);
                string config = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    config = dt.Rows[0]["value"].ToString();
                }
                return config;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region "����-չ�� by cyq 2012-09-21"
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_contract_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == gridViewTimeLimit.GetDataRow(gridViewTimeLimit.FocusedRowHandle) || gridViewTimeLimit.FocusedRowHandle > 0)
                {
                    gridViewTimeLimit.CollapseAllGroups();
                }
                else
                {
                    gridViewTimeLimit.CollapseGroupRow(gridViewTimeLimit.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
        /// <summary>
        /// չ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_expansion_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == gridViewTimeLimit.GetDataRow(gridViewTimeLimit.FocusedRowHandle) || gridViewTimeLimit.FocusedRowHandle > 0)
                {
                    gridViewTimeLimit.ExpandAllGroups();
                }
                else
                {
                    gridViewTimeLimit.ExpandGroupRow(gridViewTimeLimit.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
        #endregion

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
                this.lookUpEditorDept.CodeValue = string.Empty;
                this.lookUpEditorDoc.CodeValue = string.Empty;
                this.text_patientSN.Text = string.Empty;
                //��ʼ����ʼʱ�䡢����ʱ��
                date_inpStart.DateTime = DateTime.Now.AddMonths(-1);
                date_inpEnd.DateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// tabҳ�л��¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-29</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {

                if (this.xtraTabControl1.SelectedTabPage == xtraTabPage2)
                {
                    this.ActiveControl = lookUpEditorDept;
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        private void radioButton_locked_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_locked.Checked)
            {
                btn_query_Click(sender, e);
                this.simpleButton_UnLocked.Visible = true;
                selection.CheckMarkColumn.VisibleIndex = 0;
            }
        }

        private void radioButton_unchecked_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_unchecked.Checked)
            {
                btn_query_Click(sender, e);
                this.simpleButton_UnLocked.Visible = false;
                selection.CheckMarkColumn.VisibleIndex = -1;
            }

        }
        internal CGridCheckMarksSelection Selection
        {
            get
            {
                return selection;
            }
        }
        private void simpleButton_UnLocked_Click(object sender, EventArgs e)
        {
            try
            {
                if (selection.SelectedCount <= 0)
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("δѡ���κ��У�");
                    return;
                }
                ArrayList al = new System.Collections.ArrayList();
                for (int i = 0; i < selection.SelectedCount; i++)
                {
                    DataRowView dataRow = (DataRowView)Selection.selection[i];
                    al.Add("update QCRecord set islock='1' where id='" + dataRow["ID"].ToString() + "'");
                }
                string returnValue = app.SqlHelper.ExecuteSqlTran2(al);
                if (returnValue == "OK")
                {
                    DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show("�����ɹ���");
                }
                btn_query_Click(sender, e);
            }
            catch (Exception ex)
            { }
        }
    }
}