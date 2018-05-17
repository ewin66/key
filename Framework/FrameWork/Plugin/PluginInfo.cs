using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DrectSoft.FrameWork.Plugin
{
    /// <summary>
    /// �����Ϣ�࣬���в����Ϣ����
    /// ���в�������£�
    /// ҵ����
    /// �ⲿ���    
    /// </summary>
    public abstract class PluginInfo
    {
        private string _assemblyName;        //���
        private string _assemblyFullName;    //

        /// <summary>
        /// ��ʼ�������Ϣ��
        /// </summary>
        public PluginInfo()
        {
        }

        /// <summary>
        /// ��ȡ�����ò���ĳ����ļ�����
        /// </summary>
        /// <value></value>
        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        /// <summary>
        /// ��ȡ�����ò���ĳ���ȫ��
        /// </summary>
        public string AssemblyFullName
        {
            get { return _assemblyFullName; }
            set { _assemblyFullName = value; }
        }
    }
}
