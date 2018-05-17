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
   /// Ԥ����ѡ����伯��
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   [XmlRootAttribute(Namespace = "http://www.DrectSoft.com.cn/orm", IsNullable = false)]
   public partial class PreDefineSqlCollection
   {
      private Collection<SelectSentence> _sentences;

      /// <summary>
      /// Ԥ����ѡ����伯��
      /// </summary>
      [XmlElementAttribute("SelectSentence")]
      public Collection<SelectSentence> Sentences
      {
         get
         {
            return _sentences;
         }
         set
         {
            _sentences = value;
         }
      }
   }

   /// <summary>
   /// Ԥ����ѡ�����
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   public partial class SelectSentence
   {
      private string _querySentence;
      private string _name;

      /// <summary>
      /// ����ʶ��
      /// </summary>
      [XmlAttributeAttribute("Name")]
      public string Name
      {
         get
         {
            return _name;
         }
         set
         {
            _name = value;
         }
      }

      /// <summary>
      /// ���������Ĳ�ѯ���
      /// </summary>
      [XmlElementAttribute("QuerySentence")]
      public string QuerySentence
      {
         get
         {
            return _querySentence;
         }
         set
         {
            _querySentence = value;
         }
      }
   }
}
