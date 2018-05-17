using DrectSoft.FrameWork.util;
using System;
using System.Collections.Generic;
using System.Xml;

namespace DrectSoft.FrameWork.Config.ConfigFile
{
    /// <summary>
    /// ����װ����
    /// </summary>
    public class ConfigLoader
    {
        const string defaultfilename = "DrectSoft.FrameWork.Config";

        /// <summary>
        /// ��ȡ������б�
        /// </summary>
        /// <param name="rootelem"></param>
        /// <returns></returns>
        public static List<ServicePackageInfo> GetPackageList(XmlElement rootelem)
        {
            List<ServicePackageInfo> packages = new List<ServicePackageInfo>();
            foreach (XmlNode childnode in rootelem.ChildNodes)
            {
                if (childnode is XmlElement && childnode.Name == "ServicePackage")
                {
                    ServicePackageInfo info = PackageInfoLoader.GetPackageInfo(childnode as XmlElement);
                    if (info != null)
                        packages.Add(info);
                }
            }
            return packages;
        }

        /// <summary>
        /// ��ȡ������б�
        /// </summary>
        /// <returns></returns>
        public static List<ServicePackageInfo> GetPackageList()
        {
            string bindir = AppDomain.CurrentDomain.BaseDirectory;

            string cfgfile = System.IO.Path.Combine(bindir, string.IsNullOrEmpty(Global.ConfigFileName) ? defaultfilename : Global.ConfigFileName);

            XmlDocument doc = new XmlDocument();

            doc.Load(cfgfile);

            //if (string.IsNullOrEmpty(Global.ConfigFileName))
            //   doc.Load(defaultfilename);
            //else
            //   doc.Load(Global.ConfigFileName);

            return GetPackageList(doc.DocumentElement);
        }

        /// <summary>
        /// ���������ļ���ȡ������б�
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ServicePackageListInfo GetPackageListInfo(string filename)
        {

            string bindir = AppDomain.CurrentDomain.BaseDirectory;

            string cfgfile = System.IO.Path.Combine(bindir, string.IsNullOrEmpty(filename) ? defaultfilename : filename);

            XmlDocument doc = new XmlDocument();

            doc.Load(cfgfile);
            List<ServicePackageInfo> packages = GetPackageList(doc.DocumentElement);
            ServicePackageListInfo packagesinfo = new ServicePackageListInfo();
            packagesinfo.Infos.AddRange(packages);
            packagesinfo.Uri = XmlUtil.GetAttributeValue(doc.DocumentElement, "URI");
            return packagesinfo;
        }

        /// <summary>
        /// ���������ļ���ȡ������б�
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ServicePackageListInfo GetPackageListInfo(XmlDocument doc)
        {

            List<ServicePackageInfo> packages = GetPackageList(doc.DocumentElement);
            ServicePackageListInfo packagesinfo = new ServicePackageListInfo();
            packagesinfo.Infos.AddRange(packages);
            packagesinfo.Uri = XmlUtil.GetAttributeValue(doc.DocumentElement, "URI");
            return packagesinfo;
        }

        /// <summary>
        /// ���������б���XML�ĵ���
        /// </summary>
        /// <param name="rootelem"></param>
        /// <param name="packages"></param>
        public static void SavePackageList(XmlElement rootelem, IList<ServicePackageInfo> packages)
        {
            rootelem.RemoveAll();
            foreach (ServicePackageInfo info in packages)
            {
                if (info != null)
                {
                    XmlElement childelem = rootelem.OwnerDocument.CreateElement("ServicePackage");
                    PackageInfoSaver.SavePackageInfo(info, childelem);
                    rootelem.AppendChild(childelem);
                }
            }
        }
    }
}
