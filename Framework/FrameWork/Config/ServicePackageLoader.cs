using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using DrectSoft.FrameWork.util;
using DrectSoft.FrameWork.BizBus.Service;

namespace DrectSoft.FrameWork.Config {
    /// <summary>
    /// �����װ����
    /// </summary>
    [Serializable]
    public class ServicePackageLoader {
        /// <summary>
        /// application·��
        /// </summary>
        public static string ApplicationPath = "";

        /// <summary>
        /// �������ļ�װ�ط������Ϣ����ͨ��Assembly��֤
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public ServicePackageInfo GetPackageInfoAssemblyValidate(XmlElement elem) {
            ServicePackageInfo bizinfo = PackageInfoLoader.GetPackageInfo(elem);
            ParseAssembly pa = new ParseAssembly();
            string infAssemblyPath = Global.defaultBizFolder + bizinfo.InfAssemblyName;
            string impleAssemblyPath = Global.defaultBizFolder + bizinfo.ImpleAssemblyName;
            ServicePackageInfo assemblyinfo = pa.GetAssemblyInfo(infAssemblyPath, impleAssemblyPath);

            ServicePackageInfo result = new ServicePackageInfo();
            result.ImpleAssemblyName = bizinfo.ImpleAssemblyName;
            result.InfAssemblyName = bizinfo.InfAssemblyName;
            foreach (ServiceInfo service in bizinfo.Services) {
                if (FindService(assemblyinfo.Services, service) != null)
                    result.Services.Add(service);
            }
            return result;
        }

        /// <summary>
        /// Ѱ��ָ������
        /// </summary>
        /// <param name="services"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public ServiceInfo FindService(List<ServiceInfo> services, ServiceInfo service) {
            foreach (ServiceInfo info in services) {
                if (info.ServiceTypeName == service.ServiceTypeName &&
                   info.ImpleTypeName == service.ImpleTypeName)
                    return info;
            }
            return null;
        }

        //public IServicePackage GetServicePackage(XmlElement elem)
        //{
        //   ServicePackageInfo info = PackageInfoLoader.GetPackageInfo(elem);
        //   return Convert2Package(info);
        //}

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public static ServicePackageLoader Create() {
            return new ServicePackageLoader();
        }
    }
}