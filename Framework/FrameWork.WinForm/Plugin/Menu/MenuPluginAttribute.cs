using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Plugin
{
    /// <summary>
    /// �ⲿ�������
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ClientPluginAttribute:PluginAttribute
    {
        private string _menuNameSubsystem;
        private string _menuNameParent;
        private string _menuNameAssembly;
        private Type _startupClassType;
        private bool _visible = true;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="menuNameSubsystem">��������ϵͳ����</param>
        /// <param name="menuNameParent">�ϼ��˵�����</param>
        /// <param name="menuNameAssembly">ģ��˵�����</param>
        /// <param name="startupClassType">������</param>
       public ClientPluginAttribute(string menuNameSubsystem, string menuNameParent, string menuNameAssembly, Type startupClassType)
        {
            _menuNameSubsystem = menuNameSubsystem;
            _menuNameParent = menuNameParent;
            _menuNameAssembly = menuNameAssembly;
            _startupClassType = startupClassType;
        }

        /// <summary>
        /// ��������ϵͳ����
        /// </summary>
        /// <value></value>
        public string MenuNameSubsystem
        {
            get { return _menuNameSubsystem; }
        }

        /// <summary>
        /// �ϼ��˵�������
        /// </summary>
        /// <value></value>
        public string MenuNameParent
        {
            get { return _menuNameParent; }
        }

        /// <summary>
        /// ����Assembly�Ĳ˵�����,ģ��˵�����
        /// </summary>
        /// <value></value>
        public string MenuNameAssembly
        {
            get { return _menuNameAssembly; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <value></value>
        public Type StartupClassType
        {
            get { return _startupClassType; }
        }

        /// <summary>
        /// ��ò������
        /// </summary>
       public override PluginType PluginType
        {
            get { return PluginType.External; }
        }

       /// <summary>
       /// ���ͼ������
       /// </summary>
       public string IconName
       {
           get { return _iconName; }
       }
       private string _iconName;


       /// <summary>
       /// ����˵��Ƿ�ɼ�
       /// </summary>
       public bool Visible
       {
           get { return _visible; }
           set { _visible = value; }
       }
    }
}
