using DrectSoft.FrameWork.BizBus.Service;
using DrectSoft.FrameWork.Config;
using DrectSoft.FrameWork.Config.ConfigFile;
using System.Collections.Generic;
using System.ServiceModel;
using System.Xml;

namespace DrectSoft.FrameWork.BizBus
{
    /// <summary>
    /// ���߹���
    /// </summary>
    public class BusFactory
    {
        static readonly object padlock = new object();
        static IBizBus bizbus = null;

        /// <summary>
        /// ��ȡ�򴴽�ҵ������
        /// </summary>
        /// <returns></returns>
        public static IBizBus GetBus()
        {
            if (bizbus == null)
            {
                lock (padlock)
                {
                    if (bizbus == null)
                    {
                        CallMode callmode = AppSettingConfig.GetCallMode();
                        if (callmode == CallMode.Local)
                            InitializeClientBus();
                        else if (callmode == CallMode.Remote)
                            InitializeRemoteBus();
                    }
                }
            }
            return bizbus;
        }

        /// <summary>
        /// ��ʼ����������(���ص��ò���WCF����)
        /// </summary>
        public static void InitializeClientBus()
        {
            if (bizbus == null)
                bizbus = new ClientBizBus();

            ClientBizBus cbizbus = bizbus as ClientBizBus;

            List<ServicePackageInfo> packageinfos = ConfigLoader.GetPackageList();
            ServicePackageLoader loader = new ServicePackageLoader();
            ServicePackageList packages = new ServicePackageList();

            foreach (ServicePackageInfo info in packageinfos)
            {
                IServicePackage package = PackageConverter.Convert2Package(info);
                if (package == null)
                    continue;
                packages.Add(package);
            }
            cbizbus.packages = packages;
            cbizbus.IndexPackage();
        }

        /// <summary>
        /// ��ʼ����������(ʹ��WCF����)
        /// </summary>
        public static void InitializeRemoteBus()
        {
            bizbus = new RemoteBizBus();
            RemoteBizBus rbizbus = bizbus as RemoteBizBus;
            string configuri = AppSettingConfig.getRemoteUriPath() + "Config";
            string configPath = AppSettingConfig.getLocalConfigPath();
            BasicHttpBinding bhb = new BasicHttpBinding("BasicHttpBinding_Service");
            EndpointAddress epa = new EndpointAddress(configuri);
            IHostConfig hostconfig = ChannelFactory<IHostConfig>.CreateChannel(bhb, epa);
            XmlDocument doc = new XmlDocument();
            if (string.IsNullOrEmpty(configPath))
            {
                string configstring = hostconfig.GetConfigString();
                doc.LoadXml(configstring);
            }
            //else
            //{
            //    string file = AppDomain.CurrentDomain.BaseDirectory + @"\namespace DrectSoft.FrameWork.Config";
            //    doc.Load(file);
            //}

            List<ServicePackageInfo> packages = ConfigLoader.GetPackageList(doc.DocumentElement);
            rbizbus.packages = packages;
        }

        /// <summary>
        /// ��ӷ����
        /// </summary>
        /// <param name="package"></param>
        public static void AddPackage(IServicePackage package)
        {
            ClientBizBus cbizbus = bizbus as ClientBizBus;
            cbizbus.packages.Add(package);
            cbizbus.IndexPackage();
        }

        /// <summary>
        /// �Ƴ�����
        /// </summary>
        /// <param name="package"></param>
        public static void RemovePackage(IServicePackage package)
        {
            ClientBizBus cbizbus = bizbus as ClientBizBus;
            cbizbus.packages.Remove(package);
            cbizbus.IndexPackage();
        }
    }
}
