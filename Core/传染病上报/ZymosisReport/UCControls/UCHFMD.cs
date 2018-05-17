﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace DrectSoft.Core.ZymosisReport.UCControls
{
    public partial class UCHFMD : DevExpress.XtraEditors.XtraUserControl,IFuCade
    {
        /// <summary>
        /// add by ck 2013-8-20 手足口病
        /// </summary>
        string m_reportID;
        DataRow m_dataRow;
        public UCHFMD(string reportID)
        {
            try
            {
                InitializeComponent();
                if (!string.IsNullOrEmpty(reportID) && reportID != "0")
                {
                    this.m_reportID = reportID;
                    m_dataRow = GetDataRow(reportID);
                }
                InitData();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public DataRow GetDataRow(string reportID)
        {
            try
            {
                //数据库中查询出DataRow
                string sql = @"select * from zymosis_hfmd t where t.vaild='1' and t.reportid=" + reportID;
                DataTable dt = DrectSoft.DSSqlHelper.DS_SqlHelper.ExecuteDataTable(sql);
                DataRow dr = null;
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                }
                //m_dataRow = dr;
                return dr;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InitData()
        {
            try
            {
                foreach (var item in this.Controls)
                {
                    IZymosisReport iZymosisReport = item as IZymosisReport;
                    XtraUserControl xtraUserControl = item as XtraUserControl;
                    if (iZymosisReport != null && xtraUserControl != null)
                    {
                        //通过控件tag的名称来匹配数据库字段
                        string ColName = xtraUserControl.Tag.ToString();
                        if (m_dataRow != null)
                        {
                            string ValueStr = m_dataRow[ColName].ToString();
                            iZymosisReport.InitValue(ValueStr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SaveData()
        {
            try
            {
                #region 定义参数

                List<SqlParameter> sps = new List<SqlParameter>();
                SqlParameter sqlp1 = new SqlParameter("@HFMDID", SqlDbType.VarChar);
                SqlParameter sqlp2 = new SqlParameter("@REPORTID", SqlDbType.Int);
                SqlParameter sqlp3 = new SqlParameter("@LABRESULT", SqlDbType.VarChar);
                SqlParameter sqlp4 = new SqlParameter("@ISSEVERE", SqlDbType.VarChar);
                SqlParameter sqlp5 = new SqlParameter("@VAILD", SqlDbType.VarChar);
                SqlParameter sqlp6 = new SqlParameter("@CREATOR", SqlDbType.VarChar);
                SqlParameter sqlp7 = new SqlParameter("@CREATEDATE", SqlDbType.VarChar);
                SqlParameter sqlp8 = new SqlParameter("@MENDER", SqlDbType.VarChar);
                SqlParameter sqlp9 = new SqlParameter("@ALTERDATE", SqlDbType.VarChar);
                sps.Add(sqlp1);
                sps.Add(sqlp2);
                sps.Add(sqlp3);
                sps.Add(sqlp4);
                sps.Add(sqlp5);
                sps.Add(sqlp6);
                sps.Add(sqlp7);
                sps.Add(sqlp8);
                sps.Add(sqlp9);
                #endregion

                if (m_dataRow != null && !string.IsNullOrEmpty(m_dataRow["HFMDID"].ToString()))
                {
                    sqlp1.Value = m_dataRow["HFMDID"].ToString();
                }
                else
                {
                    sqlp1.Value = Guid.NewGuid().ToString();
                }

                sqlp2.Value = m_reportID;
                sqlp6.Value = DrectSoft.Common.DS_Common.currentUser.DoctorId;
                sqlp7.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sqlp8.Value = DrectSoft.Common.DS_Common.currentUser.DoctorId;
                sqlp9.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                foreach (var item in this.Controls)
                {
                    IZymosisReport iZymosisReport = item as IZymosisReport;
                    XtraUserControl xtraUserControl = item as XtraUserControl;
                    if (iZymosisReport != null && xtraUserControl != null)
                    {
                        string ColName = "@" + xtraUserControl.Tag.ToString();
                        foreach (SqlParameter itemsql in sps)
                        {
                            if (itemsql.ParameterName == ColName)
                            {
                                itemsql.Value = iZymosisReport.GetValue();
                                break;
                            }
                        }
                    }
                }
                foreach (var item in sps)
                {

                    if (item.Value == null && item.SqlDbType == SqlDbType.VarChar)
                        item.Value = "";
                }
                DrectSoft.DSSqlHelper.DS_SqlHelper.ExecuteNonQuery("EMR_ZYMOSIS_REPORT.usp_AddOrModHFMDReport", sps.ToArray(), CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region IFuCade 成员

        public bool ValideAndSave(string reportId)
        {
            try
            {
                m_reportID = reportId;
                SaveData();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}
