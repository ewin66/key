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
   /// ���Ժ���������ֵ�ԣ������ø��ӵĶ�Ӧ��ϵʱ��Ҫ
   /// </summary>
   [XmlIncludeAttribute(typeof(PropertyWithClassComplexType))]
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(Namespace = "http://www.DrectSoft.com.cn/orm")]
   public partial class PropertyWithClassType
   {
      private string _property;

      private string _className;

      /// <summary>
      /// ������
      /// </summary>
      [XmlAttributeAttribute()]
      public string Property
      {
         get
         {
            return _property;
         }
         set
         {
            _property = value;
         }
      }

      /// <summary>
      /// ������������
      /// </summary>
      [XmlAttributeAttribute("Class")]
      public string ClassName
      {
         get
         {
            return _className;
         }
         set
         {
            _className = value;
         }
      }

      /// <summary>
      /// ���ƶ���
      /// </summary>
      /// <returns></returns>
      public PropertyWithClassType Clone()
      {
         PropertyWithClassType obj = new PropertyWithClassType();
         obj.Property = Property;
         obj.ClassName = ClassName;
         return obj;
      }
   }
}
