﻿using DrectSoft.Common.Eop;
using DrectSoft.FrameWork.WinForm;
using DrectSoft.FrameWork.WinForm.Plugin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace DrectSoft.Core.BirthProcess
{
    public partial class MainForm : DevExpress.XtraEditors.XtraUserControl, IEMREditor
    {
        private string Noofinpat;
        private IEmrHost m_app;
        internal IEmrHost App
        {
            get { return m_app; }
        }

        private Inpatient _currentPat = null;
        public Inpatient CurrentNoofinpat
        {
            get
            {
                if (_currentPat == null)
                {
                    _currentPat = m_app.CurrentPatientInfo;
                }
                return _currentPat;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(IEmrHost app)
        {
            m_app = app;
        }

        public MainForm(string noofinpat)
            : this()
        {
            Noofinpat = noofinpat;
            //Init();
        }


        private void Init()
        {

            timeEditCheckTime.Time = DateTime.Now;
            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@Noofinpat", CurrentNoofinpat.NoOfFirstPage.ToString()) };
            //sqlParams[0].Value = "100000";//CurrentNoofinpat.NoOfFirstPage.ToString();
            //sqlParams[1].Direction = ParameterDirection.Output;
            DataSet ds = m_app.SqlHelper.ExecuteDataSet("BIRTHPROCESS.usp_GetBIRTHPROCESS_UTERINE", sqlParams, CommandType.StoredProcedure);
            DataTable dt = ds.Tables[0];

            List<recoder1> list = new List<recoder1>();

            foreach (DataRow row in dt.Rows)
            {
                recoder1 rec = new recoder1();
                Type t = rec.GetType();
                PropertyInfo[] PropertyList = t.GetProperties();
                foreach (PropertyInfo info in PropertyList)
                {

                    info.SetValue(rec, row[info.Name].ToString(), null);
                }
                list.Add(rec);
            }
            gridControl1.DataSource = list;
        }
        private void simpleButtonShowImage_Click(object sender, EventArgs e)
        {
            BirthProcessImage birthProcessImage = new BirthProcessImage();
            birthProcessImage.ShowDialog();
        }

        #region IEMREditor 接口实现

        public Control DesignUI
        {
            get { return this; }
        }

        public new void Load(IEmrHost app)
        {
            try
            {
                if (app == null) return;
                m_app = app;
                Init();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }

        public void Print()
        {
        }

        public void Save()
        {
        }

        string IEMREditor.CurrentNoofinpat
        {
            get
            {
                return "";
            }
            set
            { }
        }

        public bool ReadOnlyControl
        {
            get
            {
                return false;
            }
            set
            { }
        }

        public string Title
        {
            get { return ""; }
        }
        #endregion

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            FrmEdit frm = new FrmEdit();
            frm.ShowDialog();
            frm.Dispose();
        }
        private void DevButtonAdd_Click(object sender, EventArgs e)
        {
            List<recoder1> list = new List<recoder1>();
            list = gridControl1.DataSource == null ? list : gridControl1.DataSource as List<recoder1>;
            recoder1 rec = new recoder1();
            rec.Noofinpat = CurrentNoofinpat.NoOfFirstPage.ToString();
            rec.UterineRaws = Guid.NewGuid().ToString();
            rec.CheckTime = DateTime.Now.ToString();
            list.Add(rec);
            gridControl1.DataSource = list;
            gridView1.RefreshData();
        }
        private void Save1()
        {
            List<recoder1> list = new List<recoder1>();
            list = gridControl1.DataSource as List<recoder1>;
            foreach (recoder1 rec in list)
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@Noofinpat",SqlDbType.VarChar,9),
                            new SqlParameter("@UterineRaws",SqlDbType.VarChar,50),
                            new SqlParameter("@CheckTime",SqlDbType.VarChar,20),
                            new SqlParameter("@PalaceMouth",SqlDbType.VarChar,2),
                            new SqlParameter("@FetalHeadDrop",SqlDbType.VarChar,2),
                            new SqlParameter("@InspectionMode",SqlDbType.VarChar,20),
                            new SqlParameter("@Delivery",SqlDbType.VarChar,1),
                            new SqlParameter("@SignaturesDoctor",SqlDbType.VarChar,10),
                            new SqlParameter("@InputPerson",SqlDbType.VarChar,10),
                            new SqlParameter("@Del",SqlDbType.VarChar,1)
                        };
                sqlParams[0].Value = rec.Noofinpat != null ? rec.Noofinpat : "";
                sqlParams[1].Value = rec.UterineRaws != null ? rec.UterineRaws : "";
                sqlParams[2].Value = rec.CheckTime != null ? rec.CheckTime : "";
                sqlParams[3].Value = rec.PalaceMouth != null ? rec.PalaceMouth : "";
                sqlParams[4].Value = rec.FetalHeadDrop != null ? rec.FetalHeadDrop : "";
                sqlParams[5].Value = rec.InspectionMode != null ? rec.InspectionMode : "";
                sqlParams[6].Value = rec.Delivery != null ? rec.Delivery : "";
                sqlParams[7].Value = rec.SignaturesDoctor != null ? rec.SignaturesDoctor : "";
                sqlParams[8].Value = rec.InputPerson != null ? rec.InputPerson : "";
                sqlParams[9].Value = rec.Del != null ? rec.Del : "";
                m_app.SqlHelper.ExecuteNoneQuery("BIRTHPROCESS.usp_SaveBIRTHPROCESS_UTERINE", sqlParams, CommandType.StoredProcedure);
            }
        }

        private void DevButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save1();
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(5, ex.Message);
            }

        }
    }
    class recoder1
    {
        public string Noofinpat { get; set; }
        public string UterineRaws { get; set; }
        public string CheckTime { get; set; }
        public string PalaceMouth { get; set; }
        public string FetalHeadDrop { get; set; }
        public string InspectionMode { get; set; }
        public string Delivery { get; set; }
        public string SignaturesDoctor { get; set; }
        public string InputPerson { get; set; }
        public string Del { get; set; }

    }
    class recoder2
    {
        public string Noofinpat { get; set; }
        public string UterineRaws { get; set; }
        public string CheckTime { get; set; }
        public string BloodPressure { get; set; }
        public string FetalAzimuth { get; set; }
        public string FetalHeart { get; set; }
        public string CervicalStrength { get; set; }
        public string CervicalContinuity { get; set; }
        public string CervicalIntermission { get; set; }
        public string FetalMembrane { get; set; }
        public string Remarks { get; set; }
    }
}