using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.FrameWork.Plugin.Manager;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DrectSoft.FrameWork.WinForm;
using System.Windows.Forms;
using DrectSoft.Core;

namespace DrectSoft.FrameWork.Plugin
{
    /// <summary>
    /// ���������
    /// </summary>
    public class PluginManager
    {
        private Collection<PlugInConfiguration> pluginMenu;
        IPluginMenuRegister menuregister = null;
        const string _startupInterface = "DrectSoft.FrameWork.IStartup";
        DrectSoftLog _log ;
        PluginRunner _runner;
        Account _currentAccount;

        /// <summary>
        /// ��Ȩ�޵�MENU����
        /// </summary>
        private Collection<PlugInConfiguration> m_PrivilegeMenu;
        /// <summary>
        /// ��Ȩ�޵�MENU��Ϣ����
        /// </summary>
        public Collection<PlugInConfiguration> PrivilegeMenu
        {
            get { return m_PrivilegeMenu; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainApp"></param>
        /// <param name="log"></param>
        public PluginManager(Form mainApp, DrectSoftLog log)
        {

            _runner = new PluginRunner(mainApp);
            _log = log;
        }

        #region ע����

        /// <summary>
        /// ͨ���������ļ����ؿؼ��˵�
        /// </summary>
        /// <param name="appPath"></param>
        /// <param name="menufile"></param>
        public void RegisterPlugins(string appPath, params string[] menufile)
        {
            //װ�ز���ļ��������в����ע��ɲ���˵�
            this.pluginMenu = PlugInLoadHelper.LoadAllPlugIns(appPath, _startupInterface, menufile);
            //this.RegisterMenuPlugins();

            _log.Info("Menu Reader Over");
        }

        /// <summary>
        /// ע��˵����
        /// </summary>
        public void RegisterMenuPlugins(Account currentAccount)
        {
            m_PrivilegeMenu = new Collection<PlugInConfiguration>();
            for (int i = 0; i < this.pluginMenu.Count; i++)
            {
                PlugInConfiguration pluginconfig = this.pluginMenu[i];
                if (!pluginconfig.Visible) continue;
                if (currentAccount.IsAdministrator() || (currentAccount.HasPermission(pluginconfig.AssemblyName, pluginconfig.AssemblyStartupClass)))
                    if (menuregister != null)
                    {
                        //menuregister.Register(pluginconfig);
                        m_PrivilegeMenu.Add(pluginconfig);
                    }
            }
        }


        #endregion

        #region ע�����

        /// <summary>
        /// ж�����в���˵�
        /// </summary>
        public void UnRegisterAllPlugins()
        {
            if (this.pluginMenu == null) return;
            for (int i = 0; i < this.pluginMenu.Count; i++)
            {
                PlugInConfiguration pluginconfig = this.pluginMenu[i];
                if (menuregister != null)
                {
                    m_PrivilegeMenu.Remove(pluginconfig);
                    //menuregister.UnRegister(pluginconfig);
                }
            }
        }

        #endregion

        /// <summary>
        /// �˵�ע����
        /// </summary>
        public IPluginMenuRegister Menuregister
        {
            get { return menuregister; }
        }

        /// <summary>
        /// ����ע����
        /// </summary>
        /// <param name="register"></param>
        public void SetMenuRegister(IPluginMenuRegister register)
        {
            menuregister = register;
            menuregister.Manager = this;
        }

        /// <summary>
        /// ���������
        /// </summary>
        public PluginRunner Runner
        {
            get { return _runner; }
        }
    }

    /// <summary>
    /// �˵�ע��ӿ�
    /// </summary>
    public interface IPluginMenuRegister
    {
        /// <summary>
        /// 
        /// </summary>
        PluginManager Manager { get; set; }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="pluginconfig"></param>
        void Register(PlugInConfiguration pluginconfig);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginconfig"></param>
        void UnRegister(PlugInConfiguration pluginconfig);
    }
}
