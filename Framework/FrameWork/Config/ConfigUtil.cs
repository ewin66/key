using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Config {
    /// <summary>
    /// �����ļ�����
    /// </summary>
    public class ConfigUtil {
        /// <summary>
        /// ��ȡҵ����ȫ��·��
        /// </summary>
        /// <param name="assemblypath"></param>
        /// <returns></returns>
        public static string GetBizPluginFullPath(string assemblypath) {
            if (assemblypath.IndexOf("BizPlugin", 0, StringComparison.CurrentCultureIgnoreCase) == -1)
                return @"bizplugins\" + assemblypath;
            else
                return assemblypath;
        }

        /// <summary>
        /// ��ȡҵ����������
        /// </summary>
        /// <param name="assemblypath"></param>
        /// <returns></returns>
        public static string GetBizPluginAssemblyName(string assemblypath) {
            string[] strs = assemblypath.Split('\\');
            return strs[strs.Length - 1];
        }
    }
}
