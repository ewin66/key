﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrectSoft.Core.EMR_NursingDocument.EMRInput.Table
{
    public partial class TableInsert : Form
    {
        public TableInsert()
        {
            InitializeComponent();
        }
        #region 公有属性
        /// <summary>
        /// 列数
        /// </summary>
        public int Columns
        {
            get
            {
                return Convert.ToInt32(this.nudColumn.Value);
            }
            set
            {
                this.nudColumn.Value = value;
            }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public int Rows
        {
            get
            {
                return Convert.ToInt32(this.nudRow.Value);
            }
            set
            {
                this.nudRow.Value = value;
            }
        }
        /// <summary>
        /// 表格名称
        /// </summary>
        public string Header
        {
            get
            {
                return this.tbHeader.Text;
            }
            set
            {
                this.tbHeader.Text = value;
            }
        }

        /// <summary>
        /// 列宽(厘米)
        /// </summary>
        public decimal ColumnWidth
        {
            get
            {
                if (true == rbAuto.Checked)
                {
                    return 0;
                }
                return nudWidth.Value;
                
            }
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbFix_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFix.Checked == true)
            {
                nudWidth.Enabled = true;
            }
        }

        private void rbAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAuto.Checked == true)
            {
                nudWidth.Enabled = false;
            }
        }
    }
}
