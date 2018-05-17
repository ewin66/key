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
   /// ʹ�ù����ഴ��ʵ��ʱ���ھ����ʵ����һ�����õ����е��У�����Ҫ����ָ��Ĭ��ֵ
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(Namespace = "http://www.DrectSoft.com.cn/orm")]
   public partial class DefaultValueType
   {
      private string _column;

      private string _value;

      /// <summary>
      /// ����
      /// </summary>
      [XmlAttributeAttribute()]
      public string Column
      {
         get
         {
            return _column;
         }
         set
         {
            _column = value;
         }
      }

      /// <summary>
      /// Ĭ��ֵ
      /// </summary>
      [XmlAttributeAttribute()]
      public string Value
      {
         get
         {
            return _value;
         }
         set
         {
            _value = value;
         }
      }

      /// <summary>
      /// ���ƶ���
      /// </summary>
      /// <returns></returns>
      public DefaultValueType Clone()
      {
         DefaultValueType obj = new DefaultValueType();
         obj.Column = Column;
         obj.Value = Value;
         return obj;
      }
   }
}
