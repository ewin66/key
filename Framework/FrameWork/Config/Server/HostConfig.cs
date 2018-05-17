using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DrectSoft.FrameWork.Config {
    /// <summary>
    /// ��������
    /// </summary>
    public class HostConfig : IHostConfig {
        #region IHostConfig Members
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public string GetConfigString() {
            XmlDocument doc = new XmlDocument();
            doc.Load("DrectSoft.FrameWork.config");
            return doc.OuterXml;
        }

        #endregion
    }
}
