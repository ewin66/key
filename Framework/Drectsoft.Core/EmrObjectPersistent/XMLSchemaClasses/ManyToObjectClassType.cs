using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Text;
using System.Collections.ObjectModel;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// �ֶ�ֵ������������ʱ�����ӵ���ص���
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   public partial class ManyToObjectClassType : PropertyWithClassComplexType
   {
      private Collection<LinkedObject> _linkedObjects;

      /// <remarks/>
      [XmlElementAttribute("LinkedObject")]
      public Collection<LinkedObject> LinkedObjects
      {
         get
         {
            return _linkedObjects;
         }
         set
         {
            _linkedObjects = value;
         }
      }

      /// <summary>
      /// ���ƶ���
      /// </summary>
      /// <returns></returns>
      public new ManyToObjectClassType Clone()
      {
         ManyToObjectClassType obj = new ManyToObjectClassType();
         obj.Property = Property;
         obj.ClassName = ClassName;
         obj.KindColumn = KindColumn;
         obj.LinkedObjects = new Collection<LinkedObject>();
         foreach (LinkedObject link in LinkedObjects)
            obj.LinkedObjects.Add(link.Clone());
         return obj;
      }
   }
}
