using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DrectSoft.FrameWork.Config {
    /// <summary>
    /// �������Ϣ
    /// </summary>
    public class PackageInfoSaver {
        /// <summary>
        /// �������ļ�װ�ز����Ϣ
        /// </summary>
        /// <param name="info">���������Ϣ</param>
        /// <param name="elem">װ�صĲ��Ԫ��</param>
        /// <returns></returns>
        public static void SavePackageInfo(ServicePackageInfo info, XmlElement elem) {
            elem.RemoveAll();
            elem.SetAttribute("Name", info.Name);
            elem.SetAttribute("ImpleAssembly", info.ImpleAssemblyName);
            elem.SetAttribute("InfAssembly", info.InfAssemblyName);
            foreach (ServiceInfo s in info.Services) {
                XmlElement childelem = elem.OwnerDocument.CreateElement("service");
                SaveServiceInfo(s, childelem);
                elem.AppendChild(childelem);
            }
        }

        private static void SaveServiceInfo(ServiceInfo srvinfo, XmlElement serviceelem) {
            serviceelem.SetAttribute("key", srvinfo.Key);
            serviceelem.SetAttribute("imple", srvinfo.ImpleTypeName);
            serviceelem.SetAttribute("inf", srvinfo.ServiceTypeName);
            serviceelem.SetAttribute("isdefault", srvinfo.IsDefault.ToString());
        }
    }
}
