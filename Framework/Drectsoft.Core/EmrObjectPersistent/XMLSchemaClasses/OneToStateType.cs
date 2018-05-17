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
   /// ����״̬λ�������ݵ��ֶ����⴦��
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(Namespace = "http://www.DrectSoft.com.cn/orm")]
   public partial class OneToStateType : OneToOneType
   {
      private string _className;

      /// <summary>
      /// Attribute����
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
      public new OneToStateType Clone()
      {
         OneToStateType obj = new OneToStateType();
         obj.Property = Property;
         obj.Column = Column;
         obj.ClassName = ClassName;
         return obj;
      }
   }
}
