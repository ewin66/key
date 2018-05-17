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
   /// �ṹ������ӳ���ϵ
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   public partial class StructPropertyMap : OneToOneType
   {
      private string _defaultValue;
      private string _propertyType;
      private string _actualProperty;

      /// <remarks/>
      [XmlAttributeAttribute()]
      public string DefaultValue
      {
         get
         {
            return _defaultValue;
         }
         set
         {
            _defaultValue = value;
         }
      }

      /// <remarks/>
      [XmlAttributeAttribute()]
      public string PropertyType
      {
         get
         {
            return _propertyType;
         }
         set
         {
            _propertyType = value;
         }
      }

      /// <remarks/>
      [XmlAttributeAttribute()]
      public string ActualProperty
      {
         get
         {
            return _actualProperty;
         }
         set
         {
            _actualProperty = value;
         }
      }

      /// <summary>
      /// ���ƶ���
      /// </summary>
      /// <returns></returns>
      public new StructPropertyMap Clone()
      {
         StructPropertyMap obj = new StructPropertyMap();
         obj.Property = Property;
         obj.Column = Column;
         obj.ActualProperty = ActualProperty;
         obj.DefaultValue = DefaultValue;
         obj.PropertyType = PropertyType;
         return obj;
      }
   }
}
