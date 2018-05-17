using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using DrectSoft.FrameWork.Plugin.Manager;

namespace DrectSoft.FrameWork.WinForm
{
   /// <summary>
   /// ������Զ���˵��ӿ�
   /// </summary>
   public interface IPluginOwnerMenu
   {
      /// <summary>
      /// ��ʾ�ı�
      /// </summary>
      string Text { get;}

      /// <summary>
      /// �Ӳ˵�
      /// </summary>
      Collection<IPluginOwnerMenu> subMenus { get;}

      /// <summary>
      /// �����¼�
      /// </summary>
      void Click();
   }

   /// <summary>
   /// �Զ��幤�����ӿ�
   /// </summary>
   public interface IPluginOwnerToolBar
   {
      /// <summary>
      /// ��������Ŀ
      /// </summary>
      Collection<IPluginOwnerMenu> Items { get;}

      /// <summary>
      /// ����(ͬһplugin�б���Ψһ)
      /// </summary>
      string Name { get;}
   }

   /// <summary>
   /// �Զ��嵯���˵��ӿ�
   /// </summary>
   public interface IPluginContextMenu
   {
      /// <summary>
      /// �˵���ʽ
      /// </summary>
      PluginContextMenuType MenuType { get;}

      /// <summary>
      /// �Ӳ˵�
      /// </summary>
      ReadOnlyCollection<IPlugInMenuInfo> Items { get;}

      /// <summary>
      /// ����һ��
      /// </summary>
      /// <param name="menu"></param>
      void AddItem(IPlugInMenuInfo menu);

      /// <summary>
      /// ָ��λ������һ��
      /// </summary>
      /// <param name="index"></param>
      /// <param name="menu"></param>
      void InsertAt(int index, IPlugInMenuInfo menu);

      /// <summary>
      /// �Ƴ�һ��
      /// </summary>
      /// <param name="menu"></param>
      void RemoveItem(IPlugInMenuInfo menu);

      /// <summary>
      /// �Ƴ�һ��ָ��λ��
      /// </summary>
      /// <param name="index"></param>
      void RemoveAt(int index);
   }

   /// <summary>
   /// �����˵�����
   /// </summary>
   public enum PluginContextMenuType
   {
      /// <summary>
      /// .Net = ContextMenu
      /// </summary>
      DotNet = 1,

      /// <summary>
      /// Devexpress = PopupMenu
      /// </summary>
      DevExpress = 2,
   }

   /// <summary>
   /// ����˵��ӿ�
   /// </summary>
   public interface IPlugInMenuInfo
   {
      /// <summary>
      /// �����Ϣ
      /// </summary>
      PlugInConfiguration MenuInfo { get;}

      /// <summary>
      /// �Ӳ˵�
      /// </summary>
      Collection<IPlugInMenuInfo> SubItems { get;}

      /// <summary>
      /// ��ʾ
      /// </summary>
      string Text { get;set;}

      /// <summary>
      /// ����(��λ)
      /// </summary>
      string Name { get;}
   }
}
