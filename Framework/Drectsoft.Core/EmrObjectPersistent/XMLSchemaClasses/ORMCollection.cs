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
   /// ��Ͷ���ӳ���ϵ����
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   [XmlRootAttribute(Namespace = "http://www.DrectSoft.com.cn/orm", IsNullable = false)]   
   public partial class ORMCollection
   {
      private Collection<ORMapping> _oRMappings;
      private Collection<ORMapping> _parentORMappings;

      /// <summary>
      /// ��Ͷ���ӳ���ϵ����
      /// </summary>
      [XmlElementAttribute("ORMapping")]
      public Collection<ORMapping> ORMappings
      {
         get
         {
            return _oRMappings;
         }
         set
         {
            _oRMappings = value;
         }
      }
      
      /// <summary>
      /// ����ĸ���ͱ��ӳ���ϵ����
      /// </summary>
      [XmlElementAttribute("ParentORMapping")]
      public Collection<ORMapping> ParentORMappings
      {
         get
         {
            return _parentORMappings;
         }
         set
         {
            _parentORMappings = value;
         }
      }
   }
}
