using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core
{
   /// <summary>
   /// Ȩ��ģ�鹦���б�ӿ�
   /// </summary>
   public interface IRBSFunction
   {
      /// <summary>
      /// ����Id
      /// </summary>
      string FunctionId { get;}

      /// <summary>
      /// ��������
      /// </summary>
      string FunctionName { get;}

      /// <summary>
      /// ��������
      /// </summary>
      string FunctionDescription { get;}
   }

   /// <summary>
   /// Ȩ��ģ���б�ӿ�
   /// </summary>
   public interface IRBSModule
   {
      /// <summary>
      /// ģ��Id
      /// </summary>
      string ModuleId { get;}

      /// <summary>
      /// ģ������
      /// </summary>
      string ModuleName { get;}

      /// <summary>
      /// ģ������
      /// </summary>
      string ModuleDescription { get;}

      
      /// <summary>
      /// ģ�鹦���б�
      /// </summary>
      Collection<IRBSFunction> FunctionList { get;}

      /// <summary>
      /// ����һ����Ȩ����
      /// </summary>
      /// <param name="function"></param>
      void AddAFunction(IRBSFunction function);

      /// <summary>
      /// ����һ����Ȩ���ܲ���������
      /// </summary>
      /// <param name="functionId"></param>
      /// <param name="functionName"></param>
      /// <param name="functionDescription"></param>
      /// <returns></returns>
      int AddAFunction(string functionId, string functionName, string functionDescription);
   }

   /// <summary>
   /// ����Ȩ�޽ӿ�
   /// </summary>
   public interface IPermission
   {
      /// <summary>
      /// ��ɫId
      /// </summary>
      string RoleId { get; }

      /// <summary>
      /// ��ɫ����
      /// </summary>
      string RoleName { get; set; }
      
      
      /// <summary>
      /// ģ���б�
      /// </summary>
      Collection<IRBSModule> ModuleList { get;}

      /// <summary>
      /// ����ģ��Ȩ��
      /// </summary>
      /// <param name="module"></param>
      void AddAModule(IRBSModule module);

      /// <summary>
      /// �����µ�ģ��Ȩ��
      /// </summary>
      /// <param name="moduleId"></param>
      /// <param name="moduleName"></param>
      /// <param name="moduleDescription"></param>
      /// <param name="system"></param>
      /// <returns></returns>
      int AddAModule(string moduleId, string moduleName, string moduleDescription);

      /// <summary>
      /// ��ɫId�ı��¼�
      /// </summary>
      event EventHandler<RoleIdChangedEventArgs> RoleIdChanged;

      /// <summary>
      /// ��ɫName�ı��¼�
      /// </summary>
      event EventHandler<RoleNameChangedEventArgs> RoleNameChanged;


      /// <summary>
      /// �ж�һ����λ�Ƿ�Ϊ����ԱȨ��,ĿǰĬ��00
      /// </summary>
      bool IsAdministrators { get;}
   }

   /// <summary>
   /// RoleIdChanged,��ɫId�ı��¼�������
   /// </summary>
   public class RoleIdChangedEventArgs : EventArgs
   {
      private string _oldId;
      private string _newId;

      /// <summary>
      /// ԭId
      /// </summary>
      public string OldId
      {
         get { return _oldId; }
         set { _oldId = value; }
      }

      /// <summary>
      /// ��Id
      /// </summary>
      public string NewId
      {
         get { return _newId; }
         set { _newId = value; }
      }

      /// <summary>
      /// ctor,��������Id
      /// </summary>
      /// <param name="oldId"></param>
      /// <param name="newId"></param>
      public RoleIdChangedEventArgs(string oldId, string newId)
      {
         _oldId = oldId;
         _newId = newId;
      }
   }

   /// <summary>
   /// RoleNameChanged,��ɫName�ı��¼�������
   /// </summary>
   public class RoleNameChangedEventArgs : EventArgs
   {
      private string _oldName;
      private string _newName;

      /// <summary>
      /// ԭName
      /// </summary>
      public string OldName
      {
         get { return _oldName; }
      }

      /// <summary>
      /// ��Name
      /// </summary>
      public string NewName
      {
         get { return _newName; }
      }

      /// <summary>
      /// ctor,��������Name
      /// </summary>
      /// <param name="oldName"></param>
      /// <param name="newName"></param>
      public RoleNameChangedEventArgs(string oldName, string newName)
      {
         _oldName = oldName;
         _newName = newName;
      }
   }  
   
}

