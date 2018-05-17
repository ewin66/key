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
   /// ��������ӳ��ʣ����ֶΣ�����ͬ�ķ�ʽ��������ӳ���ϵ������Ĳ��ᵥ��ʹ�ã�
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   public partial class ManyToSubClassType : PropertyWithClassComplexType
   {
      /// <summary>
      /// ���ƶ���
      /// </summary>
      /// <returns></returns>
      public new ManyToSubClassType Clone()
      {
         ManyToSubClassType obj = new ManyToSubClassType();
         obj.Property = Property;
         obj.ClassName = ClassName;
         obj.KindColumn = KindColumn;
         return obj;
      }
   }
}
