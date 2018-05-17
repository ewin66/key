using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DrectSoft.FrameWork.util;

namespace DrectSoft.FrameWork.Config
{
   /// <summary>
   /// �������Ϣװ��
   /// </summary>
   public class PackageInfoLoader
   {
      /// <summary>
      /// �������ļ�װ�ز����Ϣ
      /// </summary>
      /// <param name="elem">װ�صĲ��Ԫ��</param>
      /// <returns></returns>
      public static ServicePackageInfo GetPackageInfo(XmlElement elem)
      {
         ServicePackageInfo ba = new ServicePackageInfo();
         ba.Name = XmlUtil.GetAttributeValue(elem, "Name");
         ba.ImpleAssemblyName = XmlUtil.GetAttributeValue(elem, "ImpleAssembly");
         ba.InfAssemblyName = XmlUtil.GetAttributeValue(elem, "InfAssembly");
         foreach (XmlNode node in elem.ChildNodes)
         {
            if (!(node is XmlElement))
               continue;

            XmlElement serviceelem = node as XmlElement;
            ServiceInfo srvinfo = ParseServiceInfo(serviceelem);
            ba.Services.Add(srvinfo);
         }
         return ba;
      }

      private static ServiceInfo ParseServiceInfo(XmlElement serviceelem)
      {
         ServiceInfo srvinfo = new ServiceInfo();
         srvinfo.Key = XmlUtil.GetAttributeValue(serviceelem, "key");
          //ʵ�ַ�������
         srvinfo.ImpleTypeName = XmlUtil.GetAttributeValue(serviceelem, "imple");
          //����
         srvinfo.ServiceTypeName = XmlUtil.GetAttributeValue(serviceelem, "inf");
         string isdefault = XmlUtil.GetAttributeValue(serviceelem, "isdefault");
         if (isdefault.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            srvinfo.IsDefault = true;
         else
            srvinfo.IsDefault = false;
         return srvinfo;
      }
   }
}
