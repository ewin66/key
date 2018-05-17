using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Plugin
{
   /// <summary>
   /// �����Ϣ����
   /// </summary>
   [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
   public abstract class PluginAttribute : Attribute
   {
      string m_name;

      /// <summary>
      /// ���캯��
      /// </summary>
      public PluginAttribute() { }

      /// <summary>
      /// ���캯��
      /// </summary>
      /// <param name="name">�������</param>
      public PluginAttribute(string name)
      {
         m_name = name;
      }

      /// <summary>
      /// ��ȡ�����ò������
      /// </summary>
      public string Name
      {
         get { return m_name; }
         set { m_name = value; }
      }

      /// <summary>
      /// ��ȡ�������
      /// </summary>
      public abstract PluginType PluginType { get;}
   }
}