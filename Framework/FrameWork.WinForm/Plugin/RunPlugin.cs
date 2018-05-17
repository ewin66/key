using DrectSoft.FrameWork.Plugin.Manager;
using DrectSoft.FrameWork.WinForm.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DrectSoft.FrameWork.WinForm
{
    /// <summary>
    /// 
    /// </summary>
    public class PluginRunner
    {
        const string _startupInterface = "DrectSoft.FrameWork.IStartPlugIn";
        const string _businessInterface = "DrectSoft.FrameWork.IBusiness";
        const string PlugInLoadConfigSectionName = "plugInLoadSettings";
        private List<IPlugIn> m_PluginsLoaded = new List<IPlugIn>();

        //ִ�й������޸Ĳ����Ĳ��
        private List<IPlugIn> _patientChangeExecuted = new List<IPlugIn>();
        private List<IPlugIn> _userChangeExecuted = new List<IPlugIn>();
        //�������
        private List<IPlugIn> _startPlugin = new List<IPlugIn>();
        private IPlugIn _activePlugIn;
        private Form m_owner;

        public PluginRunner(Form owner)
        {
            m_owner = owner;

            if (_patientChangeExecuted == null)
                _patientChangeExecuted = new List<IPlugIn>();

            _startPlugin = new List<IPlugIn>();
        }



        /// <summary>
        /// ��λ�Ѿ����õ�����
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="assemblyClassName"></param>
        /// <returns></returns>
        public int PluginIndexInLoaded(string assemblyName, string assemblyClassName)
        {
            return PlugInIndexInList(assemblyName, assemblyClassName, PluginsLoaded);
        }

        public int PlugInIndexInList(string assemblyName, string assemblyClassName, List<IPlugIn> plugins)
        {
            int foundIndex = -1;
            if (plugins == null) return foundIndex;

            for (int i = 0; i < plugins.Count; i++)
            {
                if (string.Compare(assemblyName, (plugins[i]).AssemblyFileName, true) == 0)
                {
                    if (assemblyClassName == (plugins[i]).StartClassType)
                    {
                        foundIndex = i;
                        break;
                    }
                    else
                    {
                        string[] temp = assemblyClassName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        if (temp != null && temp.Length > 0)
                        {
                            if (plugins[i].StartClassType.Contains(temp[temp.Length - 1]))
                            {
                                foundIndex = i;
                                break;
                            }
                        }
                    }
                }
            }

            return foundIndex;
        }

        /// <summary>
        /// ���в��(�˵���Ϣ)
        /// </summary>
        /// <param name="menuInfo"></param>
        /// <returns></returns>
        public IPlugIn LoadPlugIn(PlugInConfiguration menuInfo)
        {
            return LoadPlugIn(menuInfo.AssemblyName, menuInfo.AssemblyStartupClass, false);
        }

        /// <summary>
        /// ���в��(�˵�)
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public IPlugIn LoadPlugIn(IPlugInMenuInfo menuItem)
        {
            return LoadPlugIn(menuItem.MenuInfo.AssemblyName, menuItem.MenuInfo.AssemblyStartupClass, false);
        }

        /// <summary>
        /// �Ӹ����ĳ��򼯺��������������
        /// </summary>
        /// <param name="assemblyName">��������</param>
        /// <param name="startupClassName">��������</param>
        /// <returns> ���ؼ��سɹ��Ĳ��</returns>
        public IPlugIn LoadPlugIn(string assemblyName, string startupClassName, bool notShowMessage)
        {
            //У��
            Type startupType;
            if (!this.ValidatePlugin(assemblyName, startupClassName, notShowMessage, out startupType))
                return null;

            IPlugIn plugin;
            int foundindex = this.PluginIndexInLoaded(assemblyName, startupClassName);
            if (foundindex < 0)
            {
                plugin = BuildPlugin(assemblyName, startupClassName);
                RunPlugin(plugin);
            }
            else
            {
                plugin = (IPlugIn)this.PluginsLoaded[foundindex];
                FocusLoadedPlugIn(plugin);
            }
            return plugin;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="assemblyName">��������</param>
        /// <param name="startupClassName">������</param>
        /// <returns></returns>
        public IPlugIn BuildPlugin(string assemblyName, string startupClassName)
        {
            Type startupType;
            if (!this.ValidatePlugin(assemblyName, startupClassName, true, out startupType))
                return null;
            IPlugIn plugin;

            //IStartPlugIn startup = (IStartPlugIn)(PlugInDynamicProxy.CreateDynamicProxyClass(startupType));
            IStartPlugIn startup = (IStartPlugIn)Activator.CreateInstance(startupType);
            plugin = startup.Run(this.m_owner as IEmrHost);
            return plugin;
        }

        private bool ValidatePlugin(string assemblyName, string startupClassName, bool notShowMessage, out Type startupType)
        {
            startupType = null;
            Assembly assembly;
            try
            {
                assembly = PlugInLoadHelper.LoadPlugIn(Application.StartupPath, assemblyName);
            }
            catch (FileNotFoundException)
            {
                if (!notShowMessage)
                {
                    MessageBox.Show("�������ĳ��򼯲�����");
                }
                return false;
            }

            try
            {
                startupType = assembly.GetType(startupClassName, true, true);
            }
            catch (TypeLoadException)
            {
                if (!notShowMessage)
                {
                    MessageBox.Show("��������������[" + startupClassName + "]�����ڣ�");
                }
                return false;
            }

            //������������������Ƿ�ʵ����DrectSoft.Framework.IStartup�ӿ�
            Type startupInterfaceType = startupType.GetInterface(_startupInterface);
            if (startupInterfaceType == null)
            {
                if (!notShowMessage)
                {
                    MessageBox.Show("��������������û��ʵ��[" + _startupInterface + "]�ӿڣ�");
                }
                return false;
            }
            return true;
        }
        public void RunPlugin(IPlugIn plugin)
        {
            this.m_PluginsLoaded.Add(plugin);
            this._activePlugIn = plugin;
            //������������
            this.LoadPluginMainForm(plugin);
            //�ڴ����
            MemoryUtil.FlushMemory();
        }


        /// <summary>
        /// �۽�װ�ص�PlugIn
        /// </summary>
        /// <param name="plugin"></param>
        public void FocusLoadedPlugIn(IPlugIn plugin)
        {
            if (plugin.MainForm != null)
            {
                plugin.MainForm.BringToFront();
            }
        }


        /// <summary>
        /// ���������������
        /// </summary>
        /// <param name="plugin"></param>
        private void LoadPluginMainForm(IPlugIn plugin)
        {
            try
            {
                if (plugin.MainForm != null)
                {
                    Form mainForm = plugin.MainForm;
                    mainForm.ShowInTaskbar = false;
                    mainForm.Owner = this.m_owner;
                    mainForm.FormClosed += new FormClosedEventHandler(this.OnPluginFormClosed);
                    mainForm.Activated += new EventHandler(this.OnPlugInStartFormActived);
                    if (plugin.IsShowModel)
                    {
                        // CaptionBarAnother.SetFormCaption(mainForm);
                        mainForm.ShowDialog();
                    }
                    else
                    {
                        if (plugin.IsMdiChild)
                        {
                            mainForm.MdiParent = this.m_owner;
                        }
                        //mainForm.WindowState = FormWindowState.Normal;
                        mainForm.Show();
                    }
                }
                else
                {
                    //_dockingHelper.ShowDockingContent(plugin.AddInDockingForms[0].DockingWindows[0].DockingContents[0]);
                    // ToDo , ������������,����Ҫ����DockWindow�رմ���,��ʱ������
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnPluginFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender == null) throw new ArgumentNullException("sender", "δ����������");

            Form f = sender as Form;
            if (f == null) return;
            //�ڹرմ���ʱ���ͷ�һ��ϵͳ�ڴ�
            MemoryUtil.FlushMemory();
            //if (_parentMdiChildrenHandles.IndexOf(f.Handle) >= 0)
            //{
            //   _parentMdiChildrenHandles.Remove(f.Handle);
            //}
            for (int i = 0; i < this.PluginsLoaded.Count; i++)
            {
                IPlugIn plugin = (IPlugIn)((IPlugIn)this.PluginsLoaded[i] as PlugIn).Clone();
                if (plugin.MainForm == sender)
                {
                    this.ClosePlugin(plugin);
                    this.PluginsLoaded.Remove(plugin);
                    return;
                }
            }
        }

        private void OnPlugInStartFormActived(object sender, EventArgs e)
        {

            if (sender == null) throw new ArgumentNullException("sender", "δ����������");

            Form f = sender as Form;
            if (f != null)
            {
                //if (_parentMdiChildrenHandles.IndexOf(f.Handle) <= 0)
                //{
                //   _parentMdiChildrenHandles.Add(f.Handle);
                //}
                for (int i = 0; i < this.PluginsLoaded.Count; i++)
                {
                    IPlugIn plugin = (IPlugIn)((IPlugIn)this.PluginsLoaded[i] as PlugIn).Clone();
                    if (plugin.MainForm == sender)
                    {
                        if (this.ActivePlugIn == plugin) return;
                        // ActivePluginDockingForm(plugin);
                    }
                }
            }
        }

        /// <summary>
        /// �رղ��
        /// </summary>
        /// <param name="plugin">�������</param>
        public void ClosePlugin(IPlugIn plugin)
        {
            if (plugin == null) return;

            this.m_PluginsLoaded.Remove(plugin);
            //modified by zhouhui �˴������ȷ��
            this.ActivePlugIn = null;
        }

        /// <summary>
        /// �رղ��,�ʹ���
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="closeform"></param>
        public void ClosePlugin(IPlugIn plugin, bool closeform)
        {

            if (closeform && plugin.MainForm != null)
            {
                plugin.MainForm.Close();
            }

            ClosePlugin(plugin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void CloseAllPlugins()
        {
            for (int i = PluginsLoaded.Count - 1; i >= 0; i--)
            //for (int i = 0; i < PluginsLoaded.Count; i++)
            {
                IPlugIn plugin = (IPlugIn)PluginsLoaded[i];
                if (plugin.MainForm != null)
                {
                    plugin.MainForm.Close();
                }
                else
                {
                    ClosePlugin(plugin);
                    PluginsLoaded.RemoveAt(i);
                }
                if (PluginsLoaded.Contains(plugin))
                {
                    //todo
                    //�رղ�����ɹ��������ǲ���ڹر�ʱ����һЩ��������ֹ�ر�
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public List<IPlugIn> PluginsLoaded
        {
            get { return m_PluginsLoaded; }
            set { m_PluginsLoaded = value; }
        }

        public IPlugIn ActivePlugIn
        {
            get { return _activePlugIn; }
            set { _activePlugIn = value; }
        }


        public List<IPlugIn> PluginsWithMainFormLoaded
        {
            get
            {
                List<IPlugIn> plugins = new List<IPlugIn>();
                foreach (IPlugIn plg in PluginsLoaded)
                {
                    if (plg.MainForm != null)
                        plugins.Add(plg);
                }
                return plugins;
            }
        }

        /// <summary>
        /// �����Ѿ�ִ�й���λ�Ÿı���¼�
        /// </summary>
        public List<IPlugIn> PatientChangeExecuted
        {
            get { return _patientChangeExecuted; }
        }

        /// <summary>
        /// �����Ѿ�ִ�й��û��ı���¼�
        /// </summary>
        public List<IPlugIn> UserChangeExecuted
        {
            get { return _userChangeExecuted; }
        }


        public Form Owner
        {
            get { return m_owner; }
            set { m_owner = value; }
        }
        /// <summary>
        /// ����Plugin
        /// </summary>
        public List<IPlugIn> StartPlugins
        {
            get { return _startPlugin; }
            set { _startPlugin = value; }
        }
    }
}
