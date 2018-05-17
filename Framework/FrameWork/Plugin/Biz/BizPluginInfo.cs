using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Plugin {
    /// <summary>
    /// ҵ���߼����
    /// </summary>
    public class BizPluginInfo : PluginInfo {
        BizPluginType type;
        string bizName;
        bool isCore;

        /// <summary>
        /// ���캯��
        /// </summary>
        public BizPluginInfo() { }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="bizname">ҵ���߼��������</param>
        /// <param name="type">ҵ���߼��������</param>
        /// <param name="iscore">�Ƿ�Ϊ���Ĳ��</param>
        public BizPluginInfo(string bizname, BizPluginType type, bool iscore) {
            this.bizName = bizname;
            this.type = type;
        }

        /// <summary>
        /// ���û��ȡҵ���߼�����
        /// </summary>
        public string BizName {
            get { return bizName; }
            set { bizName = value; }
        }

        /// <summary>
        /// ���û��ȡҵ���߼�����
        /// </summary>
        public BizPluginType BizType {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// ���û��ȡ�Ƿ��Ǻ��Ĳ��
        /// </summary>
        public bool IsCore {
            get { return isCore; }
            set { isCore = value; }
        }
    }

    /// <summary>
    /// ҵ�������
    /// </summary>
    public enum BizPluginType {
        /// <summary>
        /// �ӿ�
        /// </summary>
        Interface = 0,
        /// <summary>
        /// ʵ��
        /// </summary>
        Implement = 1
    }
}
