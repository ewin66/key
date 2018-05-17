using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Plugin
{
   /// <summary>
   /// ҵ���߼��������
   /// </summary>
   [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
   public class BizPluginAttribute : PluginAttribute
   {
      BizPluginType type;
      bool isCore;

       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="name"></param>
       /// <param name="type"></param>
      public BizPluginAttribute(string name, BizPluginType type)
      {
         this.type = type;
         this.isCore = false;
      }

      /// <summary>
      /// ���캯��
      /// </summary>
      /// <param name="name">�������</param>
      /// <param name="type">ҵ���߼��������</param>
      /// <param name="iscore">�Ƿ��Ǻ��Ĳ��</param>
      public BizPluginAttribute(string name, BizPluginType type,bool iscore)
         : base(name)
      {
         this.type = type;
         this.isCore = iscore;
      }

      /// <summary>
      /// ��ȡ�������
      /// </summary>
      public override PluginType PluginType
      {
         get { return PluginType.Biz; }
      }

      /// <summary>
      /// ���ҵ���߼��������
      /// </summary>
      public BizPluginType BizPluginType
      {
         get { return type; }
      }

      /// <summary>
      /// ���û��ȡ�Ƿ��Ǻ��Ĳ��
      /// </summary>
      public bool IsCore
      {
         get { return isCore; }
         set { isCore = value; }
      }
   }
}
