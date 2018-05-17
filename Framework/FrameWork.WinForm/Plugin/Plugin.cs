using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Drawing;
using DrectSoft.Resources;
using System.ComponentModel;

namespace DrectSoft.FrameWork.WinForm
{
    /// <summary>
    /// ʵ�ֽӿ�IPlugin
    /// </summary>
    public class PlugIn : MarshalByRefObject, IPlugIn, ICloneable
    {
        private Form m_MainForm;
        private Boolean m_IsMdiChild;
        private Boolean m_IsShowModel;
        private Collection<IPluginOwnerMenu> m_AddInMenuItems;
        private Collection<IPluginOwnerToolBar> m_AddInToolBar;
        private string m_AssemblyFileName;
        private Collection<IPlugIn> _assistPlugIn;
        private Collection<IPlugIn> _byLoadPlugIn;
        private string m_startClassType;
        private Image m_icon;

        /// <summary>
        /// ����ί�ж���
        /// </summary>
        /// <returns></returns>
        public delegate bool NeedSaveDataDelegate();
        /// <summary>
        /// ��������ί�з���
        /// </summary>
        public NeedSaveDataDelegate NeedSaveData;

        /// <summary>
        /// ����ί�ж���
        /// </summary>
        /// <returns></returns>
        public delegate bool SaveDataDelegate();
        /// <summary>
        /// ��������ί�з���
        /// </summary>
        public SaveDataDelegate SaveData;

        #region Plugin���캯��

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="startClassType"></param>
        public PlugIn(string assemblyName, string startClassType)
            : this(startClassType, null, true, false)
        {
            m_AssemblyFileName = assemblyName;
            m_startClassType = startClassType;
            //m_AddInMenuItems = new Collection<ToolStripMenuItem>();
            //m_AddInToolStrips = new Collection<ToolStrip>();
            //m_AddInToolBar = new Collection<Bar>();
            m_AddInMenuItems = new Collection<IPluginOwnerMenu>();
            m_AddInToolBar = new Collection<IPluginOwnerToolBar>();
        }

        //#region Obsolete

        //[Obsolete("�ϳ������� PlugIn(string startClassType,Form mainForm, Boolean isMdiChild, Boolean isShowModel"
        //    +", Collection<ToolStripMenuItem> addInMenuItems"
        //    +", Collection<ToolStrip> addInToolStrips"
        //    +", Collection<IDockingForm> addInIDockingForms"
        //    +", Collection<IPlugIn> assistPlugIn)")]
        //public PlugIn(Form mainForm, Boolean isMdiChild, Boolean isShowModel
        //    , Collection<ToolStripMenuItem> addInMenuItems
        //    , Collection<ToolStrip> addInToolStrips
        //    , Collection<IDockingForm> addInIDockingForms
        //    , Collection<IPlugIn> assistPlugIn)
        //    : this( mainForm, isMdiChild, isShowModel, addInMenuItems, addInToolStrips, addInIDockingForms)
        //{
        //    if (assistPlugIn == null)
        //    {
        //        _assistPlugIn = new Collection<IPlugIn>();
        //    }
        //    else
        //    {
        //        _assistPlugIn = assistPlugIn;
        //    }
        //}

        ////[Obsolete("PlugIn(string startClassType, Form mainForm, Boolean isMdiChild, Boolean isShowModel"
        ////    +", Collection<ToolStripMenuItem> addInMenuItems"
        ////    +", Collection<ToolStrip> addInToolStrips"
        ////    +", Collection<IDockingForm> addInIDockingForms"
        ////    +")")]
        ////public PlugIn(Form mainForm, Boolean isMdiChild, Boolean isShowModel
        ////    , Collection<ToolStripMenuItem> addInMenuItems
        ////    , Collection<ToolStrip> addInToolStrips
        ////    , Collection<IDockingForm> addInIDockingForms
        ////    )
        ////{
        ////    m_MainForm = mainForm;
        ////    m_IsMdiChild = isMdiChild;
        ////    m_IsShowModel = isShowModel;
        ////    m_AddInMenuItems = addInMenuItems;
        ////    m_AddInToolStrips = addInToolStrips;
        ////    m_AddInDockingForms = addInIDockingForms;

        ////    _assistPlugIn = new Collection<IPlugIn>();
        ////    if (mainForm != null)
        ////    {
        ////        m_startClassType = mainForm.GetType().ToString();
        ////        m_AssemblyFileName = mainForm.GetType().Module.Name;
        ////    }
        ////    else
        ////    {
        ////        //���û��ֱ����ʾ�Ĵ���,ֻ����Dock����ʱ,ȡDock���ڵ�Assembly
        ////        SetAssemblyFrom1stDockForm(addInIDockingForms);
        ////    }

        ////    _byLoadPlugIn = new Collection<IPlugIn>();
        ////}

        //[Obsolete("�Ѿ��ϳ�����ʹ��PlugIn(string startClassType,Form mainForm, Boolean isMdiChild, Boolean isShowModel)")]
        //public PlugIn(Form mainForm, Boolean isMdiChild, Boolean isShowModel)
        //    : this( mainForm, isMdiChild, isShowModel, new Collection<ToolStripMenuItem>(),
        //    new Collection<ToolStrip>(), new Collection<IDockingForm>())
        //{
        //}

        //[Obsolete("�Ѿ��ϳ�����ʹ��PlugIn(string startClassType, Form mainForm)")]
        //public PlugIn( Form mainForm)
        //    : this( mainForm, true, false)
        //{
        //}

        //#endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="startClassType">��������</param>
        /// <param name="mainForm"></param>
        /// <param name="isMdiChild"></param>
        /// <param name="isShowModel"></param>
        /// <param name="addInMenuItems"></param>
        /// <param name="addInToolStrips"></param>
        /// <param name="addInIDockingForms"></param>
        /// <param name="assistPlugIn"></param>
        public PlugIn(string startClassType, Form mainForm, Boolean isMdiChild, Boolean isShowModel
            //, Collection<ToolStripMenuItem> addInMenuItems
            //, Collection<ToolStrip> addInToolStrips
            , Collection<IPluginOwnerMenu> addInMenuItems
            , Collection<IPluginOwnerToolBar> addInToolStrips
            , Collection<IPlugIn> assistPlugIn)
            : this(startClassType, mainForm, isMdiChild, isShowModel, addInMenuItems, addInToolStrips)
        {
            if (assistPlugIn == null)
            {
                _assistPlugIn = new Collection<IPlugIn>();
            }
            else
            {
                _assistPlugIn = assistPlugIn;
            }
        }

        /// <summary>
        /// Plugin���캯��
        /// </summary>
        /// <param name="startClassType">��������</param>
        /// <param name="mainForm">������Form</param>
        /// <param name="isMdiChild">�Ƿ�ΪMdiChild����</param>
        /// <param name="isShowModel">�Ƿ�ΪShowModel����</param>
        /// <param name="addInMenuItems">����Ĳ˵�����</param>
        /// <param name="addInToolStrips">����Ĺ���������</param>
        /// <param name="addInIDockingForms">�����Docking��������</param>
        public PlugIn(string startClassType, Form mainForm, Boolean isMdiChild, Boolean isShowModel
            //, Collection<ToolStripMenuItem> addInMenuItems
            //, Collection<ToolStrip> addInToolStrips
            , Collection<IPluginOwnerMenu> addInMenuItems
            , Collection<IPluginOwnerToolBar> addInToolStrips
            )
        {
            m_MainForm = mainForm;
            m_IsMdiChild = isMdiChild;
            m_IsShowModel = isShowModel;
            m_AddInMenuItems = addInMenuItems;
            //m_AddInToolStrips = addInToolStrips;
            m_AddInToolBar = addInToolStrips;

            _assistPlugIn = new Collection<IPlugIn>();
            if (mainForm != null)
            {
                m_AssemblyFileName = mainForm.GetType().Module.Name;//Assembly.FullName;//.GetName().Name;//.FullName;
            }
            else
            {
                //���û��ֱ����ʾ�Ĵ���,ֻ����Dock����ʱ,ȡDock���ڵ�Assembly
                //SetAssemblyFrom1stDockForm(addInIDockingForms);
            }

            _byLoadPlugIn = new Collection<IPlugIn>();
        }

        /// <summary>
        /// Plugin���캯��
        /// </summary>
        /// <param name="startClassType">��������</param>
        /// <param name="mainForm">������</param>
        /// <param name="isMdiChild">�Ƿ�ΪMdiChild����</param>
        /// <param name="isShowModel">�Ƿ�ΪShowModel����</param>
        public PlugIn(string startClassType, Form mainForm, Boolean isMdiChild, Boolean isShowModel)
            : this(startClassType, mainForm, isMdiChild, isShowModel
                      , new Collection<IPluginOwnerMenu>()
            , new Collection<IPluginOwnerToolBar>()
                //, new Collection<ToolStrip>()
                // , new Collection<IPluginOwnerToolBar>()
             )
        {
        }

        /// <summary>
        /// Plugin���캯��
        /// </summary>
        /// <param name="startClassType">��������</param>
        /// <param name="mainForm">������Form</param>
        public PlugIn(string startClassType, Form mainForm)
            : this(startClassType, mainForm, true, false)
        {
            m_startClassType = startClassType;
        }
        #endregion

        #region IPlugin ��Ա
        /// <summary>
        /// ��ȡ�����ò����������
        /// </summary>
        /// <value>�����������</value>
        public Form MainForm
        {
            get { return m_MainForm; }
            set { m_MainForm = value; }
        }

        /// <summary>
        ///��ȡ�����ñ�������Form�Ƿ���MdiChild��ʽ���� 
        /// </summary>
        /// <value></value>
        public Boolean IsMdiChild
        {
            get { return m_IsMdiChild; }
            set { m_IsMdiChild = value; }
        }

        /// <summary>
        /// ��ȡ�����ñ�������Form�Ƿ���ShowModel��ʽ����
        /// </summary>
        /// <value></value>
        public Boolean IsShowModel
        {
            get { return m_IsShowModel; }
            set { m_IsShowModel = value; }
        }

        /// <summary>
        /// ��ȡ��������Ҫ��ӵĲ˵�����
        /// </summary>
        /// <value>�˵�����</value>
        //public Collection<ToolStripMenuItem> AddInMenuItems
        //{
        //    get { return m_AddInMenuItems; }
        //}
        public Collection<IPluginOwnerMenu> AddInMenuItems
        {
            get { return m_AddInMenuItems; }
        }

        /// <summary>
        /// ��ȡ��������Ҫ��ӵĹ���������
        /// </summary>
        /// <value>����������</value>
        //public Collection<ToolStrip> AddInToolStrips
        //{
        //    get { return m_AddInToolStrips; }
        //}
        public Collection<IPluginOwnerToolBar> AddInToolStrips
        {
            get { return m_AddInToolBar; }
        }

        /// <summary>
        /// Assembly���ļ�����
        /// </summary>
        /// <value></value>
        public string AssemblyFileName
        {
            get { return m_AssemblyFileName; }
            set { m_AssemblyFileName = value; }
        }

        /// <summary>
        /// �����Ĳ��
        /// </summary>
        /// <value></value>
        public Collection<IPlugIn> AssistPlugIn
        {
            get { return _assistPlugIn; }
        }

        /// <summary>
        /// ���������ַ���ֵ
        /// </summary>
        /// <value></value>
        public string StartClassType
        {
            get { return m_startClassType; }
            set { m_startClassType = value; }
        }

        /// <summary>
        /// ���ص�ǰPlugin��Plugin
        /// </summary>
        /// <value></value>
        public Collection<IPlugIn> ByLoadPlugIn
        {
            get { return _byLoadPlugIn; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public System.Drawing.Image GetImage(string imageName)
        {
            return ResourceManager.GetImage(ResourceManager.GetSourceName(imageName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iconName"></param>
        /// <returns></returns>
        public System.Drawing.Image GetNormalIcon(string iconName)
        {
            return ResourceManager.GetNormalIcon(ResourceManager.GetSourceName(iconName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        public System.Drawing.Image GetSmallIcon(string imageName, IconType imageType)
        {
            return ResourceManager.GetSmallIcon(ResourceManager.GetSourceName(imageName), imageType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        public System.Drawing.Image GetMiddleIcon(string imageName, IconType imageType)
        {
            return ResourceManager.GetMiddleIcon(ResourceManager.GetSourceName(imageName), imageType);
        }

        /// <summary>
        /// ��û�����ͼ��
        /// </summary>
        public Image Icon
        {
            get { return m_icon; }
            set { m_icon = value; }
        }

        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        public bool NeedSave()
        {
            if (NeedSaveData == null) return true;
            return NeedSaveData();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (SaveData == null) return true;
            return SaveData();
        }
        #endregion

        #region ICloneable Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        /// <summary>
        /// ����Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;

            PlugIn other = (PlugIn)obj;
            if (!IsMdiChild.Equals(other.IsMdiChild)) return false;
            if (!IsShowModel.Equals(other.IsShowModel)) return false;
            if (!string.Equals(m_AssemblyFileName, other.m_AssemblyFileName, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(m_startClassType, other.m_startClassType, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!object.Equals(m_MainForm, other.m_MainForm)) return false;
            if (!object.Equals(m_AddInMenuItems, other.m_AddInMenuItems)) return false;
            //if (!object.Equals(m_AddInToolStrips, other.m_AddInToolStrips)) return false;
            if (!object.Equals(m_AddInToolBar, other.m_AddInToolBar)) return false;
            //if (!object.Equals(m_AddInDockingForms, other.m_AddInDockingForms)) return false;
            if (!object.Equals(_assistPlugIn, other._assistPlugIn)) return false;
            //            if (!object.Equals(_byLoadPlugin, other._byLoadPlugin)) return false;

            return true;
        }

        /// <summary>
        /// ���ز�����=
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static Boolean operator ==(PlugIn o1, PlugIn o2)
        {
            return object.Equals(o1, o2);
        }

        /// <summary>
        /// ���ز�����!=
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static Boolean operator !=(PlugIn o1, PlugIn o2)
        {
            return !(o1 == o2);
        }

        /// <summary>
        /// ����GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ((string)(m_AssemblyFileName + m_startClassType)).GetHashCode();
        }



        /// <summary>
        /// ��λ�Ÿı��¼�
        /// </summary>
        public event PatientChangedHandler PatientChanged;

        /// <summary>
        /// �������ڸı��¼�
        /// </summary>
        public event PatientChangingHandler PatientChanging;

        /// <summary>
        /// ִ�в��˸ı��¼�
        /// </summary>
        /// <param name="e">����</param>
        public void ExecutePatientChangeEvent(PatientArgs e)
        {
            if (PatientChanged != null)
                PatientChanged(null, e);
        }

        /// <summary>
        /// ִ�в������ڸı��¼�
        /// </summary>
        /// <param name="e">����</param>
        public void ExecutePatientChangingEvent(CancelEventArgs e)
        {
            if (PatientChanging != null)
                PatientChanging(null, e);
        }

        #region IPlugIn ��Ա

        /// <summary>
        /// �û��ı��¼�
        /// </summary>
        public event UsersChangedHandeler UserChanged;
        /// <summary>
        /// �û��ı�
        /// </summary>
        /// <param name="e"></param>
        public void ExecuteUsersChangeEvent(UserArgs e)
        {
            if (UserChanged != null)
            {
                UserChanged(null, e);
            }
        }

        #endregion
    }
}
