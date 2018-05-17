using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DrectSoft.FrameWork
{
   /// <summary>
   /// ί�У���ȡ�����ļ�
   /// </summary>
   /// <param name="xmlNode"></param>
   /// <returns></returns>
   public delegate object DelegateReadConfiguration(System.Xml.XmlNode xmlNode);

   /// <summary>
   /// DrectSoftConfigurationSectionHandler
   /// </summary>
   public class DrectSoftConfigurationSectionHandler : IConfigurationSectionHandler
   {      
      private static DelegateReadConfiguration delegateFunction;

      /// <summary>
      /// ����ί�к���
      /// </summary>
      public static void SetConfigurationDelegate(DelegateReadConfiguration function)
      {
         delegateFunction = function;
      }

      /// <summary>
      /// ʵ��IConfigurationSectionHandler.Create
      /// </summary>
      /// <param name="parent"></param>
      /// <param name="configContext"></param>
      /// <param name="section"></param>
      /// <returns></returns>
      public object Create(object parent, object configContext, System.Xml.XmlNode section)
      {
         if (delegateFunction != null)
         {
            return delegateFunction(section);
         }
         else
         {
            return null;
         }
      }
   }
}
