﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.FrameWork;
using DrectSoft.FrameWork.WinForm;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Emr.Util;
using DevExpress.XtraTreeList.Nodes;
using DrectSoft.Common.Ctrs.FORM;

namespace DrectSoft.Core.MainEmrPad
{
    public partial class MainForm : DevBaseForm, IStartPlugIn
    {
        private IEmrHost m_app;
        RecordDal m_RecordDal;
        UCEmrInput m_UCEmrInput;

        public MainForm()
        {
            try
            {
                InitializeComponent();
                AddUcEmrInput();
                FormClosing += new FormClosingEventHandler(UCEmrInput_FormClosing);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex.Message);
            }
        }

        private void AddUcEmrInput()
        {
            try
            {
                m_UCEmrInput = new UCEmrInput();
                //m_UCEmrInput.HideBar();
                m_UCEmrInput.Dock = DockStyle.Fill;
                this.Controls.Add(m_UCEmrInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// edit by Yanqiao.Cai 2013-01-17
        /// 1、add try ... catch
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCEmrInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_UCEmrInput.CloseAllTabPages();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        #region IStartPlugIn 成员

        public IPlugIn Run(DrectSoft.FrameWork.WinForm.Plugin.IEmrHost host)
        {
            try
            {
                PlugIn plg = new PlugIn(this.GetType().ToString(), this);
                plg.PatientChanging += new PatientChangingHandler(plg_PatientChanging);
                plg.PatientChanged += new PatientChangedHandler(plg_PatientChanged);
                m_app = host;
                m_RecordDal = new RecordDal(m_app.SqlHelper);
                m_UCEmrInput.SetInnerVar(m_app, m_RecordDal);
                m_UCEmrInput.CurrentInpatient = m_app.CurrentPatientInfo;
                return plg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void plg_PatientChanging(object sender, CancelEventArgs arg)
        {
            try
            {
                m_UCEmrInput.PatientChanging();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        void plg_PatientChanged(object Sender, PatientArgs arg)
        {
            try
            {
                m_UCEmrInput.PatientChanged(m_app.CurrentPatientInfo);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}