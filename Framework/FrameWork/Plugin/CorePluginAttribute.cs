using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Plugin
{
   /// <summary>
    /// ���Ĳ�����ԣ��������ҵ��
   /// </summary>
   [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
   public class CorePluginAttribute : PluginAttribute
   {
       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="name"></param>
      public CorePluginAttribute(string name):base(name)
      { 

      }

      /// <summary>
      /// ������
      /// </summary>
      public override PluginType PluginType
      {
         get { return PluginType.Biz; }
      }
   }
}
