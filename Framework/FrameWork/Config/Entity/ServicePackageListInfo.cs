using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Config {
    /// <summary>
    /// �������Ϣ�б�
    /// </summary>
    public class ServicePackageListInfo {
        List<ServicePackageInfo> infos;
        string uri;

        /// <summary>
        /// ctor
        /// </summary>
        public ServicePackageListInfo() {
            infos = new List<ServicePackageInfo>();
        }


        /// <summary>
        /// ���������б�(������Ϣ)
        /// </summary>
        public List<ServicePackageInfo> Infos {
            get { return infos; }
        }

        /// <summary>
        /// �������uri
        /// </summary>
        public string Uri {
            get { return uri; }
            set { uri = value; }
        }
    }
}
