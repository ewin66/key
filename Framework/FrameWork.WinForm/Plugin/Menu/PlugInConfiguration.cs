using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace DrectSoft.FrameWork.Plugin.Manager
{
   /// <summary>
   /// ���Plugin����������
   /// </summary>
   public class PlugInConfiguration
   {
      //private PlugInConfiguration _instance;
      private string _assemblyName;
      private string _assemblyStartupClass;
      private string _menuCaption;
      private string _menuParentCaption;
      private string _subSystem;
      private bool _visible = true;
      private Image _icon;
      private string _iconname = string.Empty;

      /// <summary>
      /// Ctor
      /// </summary>
      public PlugInConfiguration()
      {
         _menuCaption = "Default Menu Name";
      }

      /// <summary>
      /// �����ģ���ļ�����
      /// </summary>
      /// <value></value>
      [ReadOnly(true)]
      [DisplayName("ģ���ļ���")]
      public string AssemblyName
      {
         get { return _assemblyName; }
         set { _assemblyName = value; }
      }

      /// <summary>
      /// �������������
      /// </summary>
      /// <value></value>
      [Browsable(false)]
      public string AssemblyStartupClass
      {
         get { return _assemblyStartupClass; }
         set { _assemblyStartupClass = value; }
      }

      /// <summary>
      /// ����Ĳ˵�����
      /// </summary>
      /// <value></value>
      [DisplayName("�˵�����")]
      public string MenuCaption
      {
         get { return _menuCaption; }
         set { _menuCaption = value; }
      }

      /// <summary>
      /// ����˵����ص������Ĳ˵�����
      /// </summary>
      /// <value></value>
      [DisplayName("�ϼ��˵�����")]
      public string MenuParentCaption
      {
         get { return _menuParentCaption; }
         set { _menuParentCaption = value; }
      }

      /// <summary>
      /// �����������ϵͳ
      /// </summary>
      [DisplayName("������ϵͳ")]
      public string SubSystem
      {
         get { return _subSystem; }
         set { _subSystem = value; }
      }

      //[Browsable(false)]
      /// <summary>
      /// �˵��Ƿ�ɼ�
      /// </summary>
      public bool Visible
      {
         get { return _visible; }
         set { _visible = value; }
      }

      /// <summary>
      /// �õ������ò˵�ͼ��
      /// </summary>
      public Image Icon
      {
         get { return _icon; }
         set { _icon = value; }
      }

      /// <summary>
      /// �õ������ò˵�ͼ������
      /// </summary>
      public string IconName
      {
         get { return _iconname; }
         set { _iconname = value; }
      }

      /// <summary>
      /// ����1��
      /// </summary>
      /// <returns></returns>
      public PlugInConfiguration Clone()
      {
         PlugInConfiguration cfg = new PlugInConfiguration();
         cfg.AssemblyName = this.AssemblyName;
         cfg.AssemblyStartupClass = this.AssemblyStartupClass;
         cfg.MenuCaption = this.MenuCaption;
         cfg.MenuParentCaption = this.MenuParentCaption;
         cfg.SubSystem = this.SubSystem;
         cfg.Icon = this.Icon;
         cfg.IconName = this.IconName;
         cfg.Visible = this.Visible;
         return cfg;
      }
   }
}
