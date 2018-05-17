using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.FrameWork.Plugin;
using DrectSoft.FrameWork.Plugin.Manager;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;


namespace DrectSoft.FrameWork.WinForm
{
    /// <summary>
    /// ����˵�ע����
    /// </summary>
    public class PluginMenuRegister : IPluginMenuRegister
    {
        private PluginManager manager;
        private List<IPluginMenuRegister> childMenuRegister = new List<IPluginMenuRegister>();

        /// <summary>
        /// ctor
        /// </summary>
        public PluginMenuRegister()
        {

        }

        /// <summary>
        /// �Ӳ˵�ע��
        /// </summary>
        public ReadOnlyCollection<IPluginMenuRegister> ChildMenuRegister
        {
            get { return new ReadOnlyCollection<IPluginMenuRegister>(childMenuRegister); }
        }

        /// <summary>
        /// �����Ӳ˵�
        /// </summary>
        /// <param name="menuRegister"></param>
        public void AddChildRegister(IPluginMenuRegister menuRegister)
        {
            menuRegister.Manager = this.Manager;
            childMenuRegister.Add(menuRegister);
        }

        /// <summary>
        /// ����Ӳ˵���Ϣ
        /// </summary>
        public void ClearChildRegisters()
        {
            childMenuRegister.Clear();
        }

        #region IPluginMenuRegister Members

        /// <summary>
        /// ע��
        /// </summary>
        /// <param name="pluginconfig"></param>
        public void Register(PlugInConfiguration pluginconfig)
        {
            foreach (IPluginMenuRegister register in childMenuRegister)
            {
                register.Register(pluginconfig);
            }
        }

        /// <summary>
        /// ע��
        /// </summary>
        /// <param name="pluginconfig"></param>
        public void UnRegister(PlugInConfiguration pluginconfig)
        {
            foreach (IPluginMenuRegister register in childMenuRegister)
            {
                register.UnRegister(pluginconfig);
            }
        }

        /// <summary>
        /// ���������
        /// </summary>
        public PluginManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }
        #endregion
    }
}
