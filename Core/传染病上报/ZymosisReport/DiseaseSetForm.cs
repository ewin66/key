﻿using DrectSoft.Common;
using DrectSoft.Common.Ctrs.DLG;
using DrectSoft.Common.Ctrs.FORM;
using DrectSoft.FrameWork.WinForm.Plugin;
using System;
using System.Data;
using System.Windows.Forms;

namespace DrectSoft.Core.ZymosisReport
{
    public partial class DiseaseSetForm : DevBaseForm
    {
        SqlHelper m_SqlHelper;
        IEmrHost m_Host;
        public DiseaseSetForm(IEmrHost host, SqlHelper sqlHelper)
        {
            InitializeComponent();
            m_SqlHelper = sqlHelper;
            m_Host = host;
        }
        /// <summary>
        /// 项令波
        /// 2012-12-6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiseaseSetForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDiagnosisSource();
                LoadDiagnosisDest();
                LoadDiseaseLevel();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex); ;
            }
        }

        /// <summary>
        /// 加载诊断源
        /// </summary>
        private void LoadDiagnosisSource()
        {
            try
            {
                DataTable dt = m_SqlHelper.GetDiagnosis();
                gridControlDiagnisisSource.DataSource = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 加载病种
        /// </summary>
        private void LoadDiagnosisDest()
        {
            try
            {
                DataTable dt = m_SqlHelper.GetDisease2();
                gridControlDiagnosisDestination.DataSource = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 加载传染病等级
        /// </summary>
        private void LoadDiseaseLevel()
        {
            try
            {
                DataTable dt = m_SqlHelper.GetDiseaseLevel();
                dt.Columns["ID"].Caption = "编号";
                dt.Columns["NAME"].Caption = "病种名称";
                lookUpEditLevel.Properties.DataSource = dt;
                lookUpEditLevel.Properties.DisplayMember = "NAME";
                lookUpEditLevel.Properties.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void textEditDiagnosisSource_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = gridControlDiagnisisSource.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    string value = textEditDiagnosisSource.Text.Trim().Replace("'", "''");
                    if (value.Contains("["))
                    {
                        MyMessageBox.Show("请不要输入特殊符号");
                        return;
                    }
                    dv.RowFilter = " icd like '%" + value + "%' or name like '%" + value + "%' or py like '%" + value + "%' or wb like '%" + value + "%' ";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        private void textEditDiagnosisDestination_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = gridControlDiagnosisDestination.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    string value = textEditDiagnosisDestination.Text.Trim().Replace("'", "''");
                    dv.RowFilter = " icd like '%" + value + "%' or name like '%" + value + "%' or py like '%" + value + "%' or wb like '%" + value + "%' ";
                }
                SetFocusdRowInfo();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }
        private void simpleButtonMove_Click(object sender, EventArgs e)
        {
            try
            {
                textEditDiagnosisDestination.Text = "";
                DataRow drFocused = gridViewDiagnosisSource.GetFocusedDataRow();

                if (drFocused != null)
                {
                    DataTable dt = gridControlDiagnosisDestination.DataSource as DataTable;
                    var dtSelected = dt.AsEnumerable().Where<DataRow>(dr1 => dr1["ICD"].ToString() == drFocused["ICD"].ToString());
                    int count = 0;
                    foreach (DataRow dr in dtSelected)
                    {
                        count++;
                    }
                    if (count == 0)
                    {
                        //保存
                        m_SqlHelper.SaveZymosisDiagnosis(drFocused);
                        LoadDiagnosisDest();
                        gridViewDiagnosisDestination.RefreshData();
                        drFocused.Delete();
                        //((DataTable)gridControlDiagnisisSource.DataSource).AcceptChanges();
                        ((DataView)gridViewDiagnosisSource.DataSource).Table.AcceptChanges();

                        //LoadDiagnosisSource();
                        return;
                    }

                    //在其中定位已经存在的记录
                    dt = gridControlDiagnosisDestination.DataSource as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRowView drDiagnosisDestination = gridViewDiagnosisDestination.GetRow(i) as DataRowView;
                        if (drDiagnosisDestination["ICD"].ToString() == drFocused["ICD"].ToString())
                        {
                            gridViewDiagnosisDestination.Focus();
                            gridViewDiagnosisDestination.FocusedRowHandle = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }
        private void gridViewDiagnosisDestination_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                SetFocusdRowInfo();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        private void SetFocusdRowInfo()
        {
            try
            {
                DataRow dr = gridViewDiagnosisDestination.GetFocusedDataRow();
                if (dr != null)
                {
                    string icd = dr["ICD"].ToString();
                    string name = dr["NAME"].ToString();
                    string level = dr["LEVEL_ID"].ToString();
                    string namestr = dr["NAMESTR"].ToString();
                    string upcount = dr["upcount"].ToString();
                    textEditDiseaseICD.Text = icd;
                    textEditDiseaseName.Text = name;
                    lookUpEditLevel.EditValue = level;
                    txtDiseaseNamestr.Text = namestr == "" ? name : namestr;
                    txtFukaType.Text = dr["fukatype"].ToString();
                    this.txtUpCount.Value = Convert.ToDecimal(upcount);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 保存事件
        /// edit by Yanqiao.Cai 2012-11-05
        /// 1、add try ... catch
        /// 2、添加提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDiagnosisDestination.FocusedRowHandle < 0)
                {
                    MyMessageBox.Show("请选择一条病种记录");
                    return;
                }
                DataRow dr = gridViewDiagnosisDestination.GetFocusedDataRow();
                dr["LEVEL_ID"] = lookUpEditLevel.EditValue;
                dr["level_name"] = lookUpEditLevel.Text;
                dr["NAMESTR"] = txtDiseaseNamestr.Text.Trim();
                dr["UPCOUNT"] = this.txtUpCount.Value.ToString();
                dr["FUKATYPE"] = txtFukaType.Text.ToString();
                m_SqlHelper.SaveZymosisDiagnosis(dr);
                MyMessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// 作废事件
        /// edit by Yanqiao.Cai 2012-11-05
        /// 1、add try ... catch
        /// 2、添加提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //add by Yanqiao.Cai 2012-11-05
                if (gridViewDiagnosisSource.FocusedRowHandle < 0)
                {
                    MyMessageBox.Show("请选择一条记录");
                    return;
                }
                if (MyMessageBox.Show("您确定要作废该病种吗？", "作废病种", MyMessageBoxButtons.OkCancel) == DialogResult.Cancel)
                {
                    return;
                }

                DataRow dr = gridViewDiagnosisDestination.GetFocusedDataRow();
                if (dr["valid"].ToString() == "0" || dr["valid_name"].ToString() == "无效")
                {
                    MyMessageBox.Show("该病种已经是无效状态，不可重复作废");
                    return;
                }
                dr["valid"] = "0";
                dr["valid_name"] = "无效";
                m_SqlHelper.SaveZymosisDiagnosis(dr);
                MyMessageBox.Show("作废成功");
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// 删除事件
        /// edit by Yanqiao.Cai 2012-11-05
        /// 1、add try ... catch
        /// 2、添加提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectrow = gridViewDiagnosisSource.FocusedRowHandle;
                //add by Yanqiao.Cai 2012-11-05
                if (selectrow < 0)
                {
                    MyMessageBox.Show("请选择一条记录");
                    return;
                }
                if (MyMessageBox.Show("您确定要删除该病种吗？", "删除病种", MyMessageBoxButtons.OkCancel) == DialogResult.Cancel)
                {
                    return;
                }

                DataRow dr = gridViewDiagnosisDestination.GetFocusedDataRow();
                string icd = dr["ICD"].ToString();
                m_SqlHelper.DeleteZymosisDiagnosis(icd);
                gridViewDiagnosisDestination.DeleteRow(gridViewDiagnosisDestination.FocusedRowHandle);
                m_Host.CustomMessageBox.MessageShow("删除成功", CustomMessageBoxKind.InformationOk);

                ((DataView)gridViewDiagnosisDestination.DataSource).Table.AcceptChanges();

                LoadDiagnosisSource();
                gridViewDiagnosisSource.SelectRow(selectrow);
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// 序号 --- 诊断列表
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-05</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewDiagnosisSource_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                DS_Common.AutoIndex(e);
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }


        /// <summary>
        /// 序号 --- 病种设置
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-06</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewDiagnosisDestination_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                DS_Common.AutoIndex(e);
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }
    }
}