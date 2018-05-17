using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DrectSoft.FrameWork.BizBus;

namespace DrectSoft.FrameWork.Config
{
    /// <summary>
    /// Ӧ�ó�������
    /// </summary>
    public class AppSettingConfig
    {
        /// <summary>
        /// ��ȡԶ�̷���URL
        /// </summary>
        /// <returns></returns>
        public static string getRemoteUriPath()
        {
            return ConfigurationManager.AppSettings["BaseUriPath"];
        }

        /// <summary>
        /// ���ʱ��������ļ�
        /// </summary>
        /// <returns></returns>
        public static string getLocalConfigPath()
        {
            return ConfigurationManager.AppSettings["BizConfigPath"];
        }
        /// <summary>
        /// ��ȡ��ܼ���ģʽ
        /// </summary>
        /// <returns></returns>
        public static CallMode GetCallMode()
        {
            string callmode = ConfigurationManager.AppSettings["CallMode"];
            if (("Remote").Equals(callmode, StringComparison.CurrentCultureIgnoreCase))
                return CallMode.Remote;
            return CallMode.Local;
        }
    }
}
