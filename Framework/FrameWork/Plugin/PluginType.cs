using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Plugin {
    /// <summary>
    /// �������
    /// </summary>
    public enum PluginType {
        /// <summary>
        /// ��ܲ��������������ܽ���
        /// </summary>
        Frame,

        /// <summary>
        /// ���Ĳ�����������ҵ��
        /// </summary>
        Core,

        /// <summary>
        /// ҵ���߼����
        /// </summary>
        Biz,

        /// <summary>
        /// �ⲿ�������������ҵ��
        /// </summary>
        External
    }

    /// <summary>
    /// ��������ж�
    /// </summary>
    public class CPluginType {
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PluginType GetPluginType(string name) {

            switch (name.ToUpper(System.Globalization.CultureInfo.CurrentCulture)) {
                case "COREPLUGIN":
                    return PluginType.Core;
                case "BIZPLUGIN":
                    return PluginType.Biz;
                case "FRAMEPLUGIN":
                    return PluginType.Frame;
                case "EXTERNALPLUGIN":
                    return PluginType.External;
                default:
                    return PluginType.Biz;
            }
        }
    }
}