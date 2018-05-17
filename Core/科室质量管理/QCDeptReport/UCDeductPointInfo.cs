using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.Data;
using DrectSoft.Core;
using DrectSoft.FrameWork;
using DrectSoft.FrameWork.WinForm;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;
using DrectSoft.Common.Report;


namespace DrectSoft.Core.QCDeptReport
{
    public partial class UCDeductPointInfo : DevExpress.XtraEditors.XtraUserControl
    {
        #region ����

        /// <summary>
        /// ���������Ӧ�ó���ӿ�
        /// </summary>
        private IEmrHost _app;
        /// <summary>
        /// ���������Ӧ�ó���ӿ�
        /// </summary>
        public IEmrHost App
        {
            get { return _app; }
            set { _app = value; }
        }

        /// <summary>
        /// ����ʧ�����
        /// </summary>
        public String PointID = "";

        /// <summary>
        /// �������ݿ��SqlHelper
        /// </summary>
        private IDataAccess m_DataAccessEmrly;

        /// <summary>
        /// �ؼ��п�ʼʱ��
        /// </summary>
        public DateTime m_Begintime = DateTime.Now.AddMonths(-1);

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime m_Endtime = DateTime.Now;

        public FormDeductPointInfo m_form;

        /// <summary>
        /// ҳ������ �����ӡ
        /// </summary>
        private DataTable m_pageSouce;

        #endregion

        public UCDeductPointInfo(IEmrHost application)
        {
            InitializeComponent();

            InitApp(application);
        }

        private void UserControlFail_Load(object sender, EventArgs e)
        {
            this.dateBegin.DateTime = m_Begintime;
            this.dateEnd.DateTime = m_Endtime;
            txtDeptName.Text = App.User.CurrentDeptName;
            InitSqlWorkBook();
        }

        #region ����
        

        protected virtual void InitApp(IEmrHost application)
        {
            try
            {
                App = application;
                m_DataAccessEmrly = App.SqlHelper;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ʧ������
        /// </summary>
        private void InitSqlWorkBook()
        {
            lookUpWindowtype.SqlHelper = _app.SqlHelper;

            DataTable duducttype = GetDeductPointSouce(m_Begintime.ToString("yyyy-MM-dd"), m_Endtime.ToString("yyyy-MM-dd"), 3, "");
            duducttype.Columns["ID"].Caption = "����";
            duducttype.Columns["NAME"].Caption = "ʧ������";
            
            Dictionary<string, int> cols = new Dictionary<string, int>();

            cols.Add("ID", 50);
            cols.Add("NAME", 150);

            SqlWordbook deductpoint = new SqlWordbook("querybook", duducttype, "ID", "NAME", cols);

            lookUpEditortype.SqlWordbook = deductpoint;

            lookUpEditortype.CodeValue = PointID;
        }


        /// <summary>
        /// ������ѯʱ��
        /// </summary>
        public void Query_Click()
        {
            dateBegin.DateTime = m_Begintime;
            dateEnd.DateTime = m_Endtime;

            btnQuery_Click(null, null);
        }

        /// <summary>
        /// ���ݿ�ʼʱ�������ʱ���ȡ����ʧ���������
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        private DataTable GetDeductPointSouce(string BeginTime, string EndTime, int type, string pointID)
        {
            SqlParameter[] sqlParams = new SqlParameter[] { 
                  new SqlParameter("@DeptCode", SqlDbType.VarChar,10),
                  new SqlParameter("@PointID", SqlDbType.VarChar, 10),
                  new SqlParameter("@DateTimeBegin", SqlDbType.VarChar, 10),
                  new SqlParameter("@DateTimeEnd",SqlDbType.VarChar, 10),
                  new SqlParameter("@QCStatType",SqlDbType.Int)
                  };

            sqlParams[0].Value = App.User.CurrentDeptId == null ? "" : App.User.CurrentDeptId;
            //sqlParams[0].Value = "";
            sqlParams[1].Value = pointID;
            sqlParams[2].Value = BeginTime;
            sqlParams[3].Value = EndTime;
            sqlParams[4].Value = type;
            DataSet ds = m_DataAccessEmrly.ExecuteDataSet("usp_GetDeptDeductPoint", sqlParams, CommandType.StoredProcedure);
            return ds.Tables[0];


        }

        private decimal GetCurrentPat()
        {

            if (gridViewInpatientFail.FocusedRowHandle < 0)
                return -1;
            else
            {

                DataRow dataRow = gridViewInpatientFail.GetDataRow(gridViewInpatientFail.FocusedRowHandle);
                if (dataRow == null) return -1;

                return Convert.ToDecimal(dataRow["NoOfInpat"]);
            }

        }

        #endregion

        #region �¼�


        private void btnQuery_Click(object sender, EventArgs e)
        {
            string begintime = dateBegin.DateTime.ToString("yyyy-MM-dd");
            string endtime = dateEnd.DateTime.ToString("yyyy-MM-dd");
            string PointID = lookUpEditortype.CodeValue == null ? "" : lookUpEditortype.CodeValue;
            m_pageSouce = GetDeductPointSouce(begintime, endtime, 2, PointID);


            gridInpatientFail.DataSource = m_pageSouce;

            _app.PublicMethod.ConvertGridDataSourceUpper(gridViewInpatientFail);
            if (m_pageSouce.Rows.Count == 0)
            {
                _app.CustomMessageBox.MessageShow("û�з�������������");
            }

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            decimal syxh = GetCurrentPat();
            if (syxh < 0) return;
            //�ر�ģ̬����
            if (m_form != null)
            {
                m_form.Close();
            }
            App.ChoosePatient(syxh);
            App.LoadPlugIn("DrectSoft.Core.RecordsInput.FormMain,DrectSoft.Core.RecordsInput");
        }

        private void gridInpatientFail_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (m_pageSouce == null)
                return;
            XReport xreport = new XReport(m_pageSouce.Copy(), @"ReportDeductpiontInfo.repx");

            xreport.ShowPreview();
        }

        #endregion

        private void simpleButtonReset_Click(object sender, EventArgs e)
        {
            this.dateBegin.DateTime = m_Begintime;
            this.dateEnd.DateTime = m_Endtime;
            txtDeptName.Text = App.User.CurrentDeptName;
            lookUpEditortype.CodeValue = PointID;
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (char)13)
                {
                    SendKeys.Send("{Tab}");
                    SendKeys.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
