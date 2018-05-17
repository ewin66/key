using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace DrectSoft.Wordbook.Schema
{
   /// <summary>
   /// �ֵ������
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   public class WordbookCatalog
   {
      /// <summary>
      /// �ֵ�������
      /// </summary>
      [XmlElementAttribute("Wordbook")]
      public Wordbook[] Wordbooks
      {
         get { return _wordbooks; }
         set { _wordbooks = value; }
      }
      private Wordbook[] _wordbooks;

      /// <summary>
      /// ��������
      /// </summary>
      [XmlAttributeAttribute()]
      public string CatalogName
      {
         get { return _catalogName; }
         set { _catalogName = value; }
      }
      private string _catalogName;

      /// <summary>
      /// ������ʾ����
      /// </summary>
      [XmlAttributeAttribute()]
      public string Caption
      {
         get { return _caption; }
         set { _caption = value; }
      }
      private string _caption;
   }
}
