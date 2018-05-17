using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DrectSoft.FrameWork.util
{
    /// <summary>
    /// xml ������
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// ��ȡָ���ڵ�����ֵ
        /// </summary>
        /// <param name="node">ָ���ڵ�</param>
        /// <param name="attributename">��������</param>
        /// <returns></returns>
        public static string GetAttributeValue(XmlNode node, string attributename)
        {
            if (node.Attributes[attributename] != null)
                return node.Attributes[attributename].Value;
            return null;
        }

        /// <summary>
        /// ��ȡָ������ֵ
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributename"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string GetAttributeValue(XmlNode node, string attributename,bool ignoreCase)
        {
            string attrName=string.Empty;
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Name.Equals(attributename, StringComparison.CurrentCultureIgnoreCase))
                    attrName = attr.Name;
            }            
            if (!string.IsNullOrEmpty(attrName))
                return node.Attributes[attrName].Value;
            return null;
        }
    }
}
