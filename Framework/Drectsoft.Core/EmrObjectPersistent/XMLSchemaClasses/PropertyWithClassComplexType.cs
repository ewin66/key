using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ���Ժ���������ֵ�ԣ��Լ�����Ϊ������ʱ��Ҫ����Ϣ
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(Namespace = "http://www.DrectSoft.com.cn/orm")]
   public partial class PropertyWithClassComplexType : PropertyWithClassType
   {
      private string _kindColumn;

      /// <summary>
      /// ��ʾ���������������Ե������ǳ�����ʱ�����������ʹ�ù����ഴ��ʵ��������������Բ��
      /// </summary>
      [XmlAttributeAttribute()]
      public string KindColumn
      {
         get
         {
            return _kindColumn;
         }
         set
         {
            _kindColumn = value;
         }
      }

      /// <summary>
      /// ���ƶ���
      /// </summary>
      /// <returns></returns>
      public new PropertyWithClassComplexType Clone()
      {
         PropertyWithClassComplexType obj = new PropertyWithClassComplexType();
         obj.Property = Property;
         obj.ClassName = ClassName;
         obj.KindColumn = KindColumn;
         return obj;
      }
   }
}

