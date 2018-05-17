using System.Xml.Serialization;
using System;
using System.Diagnostics;
using DrectSoft.Wordbook;


namespace DrectSoft.Wordbook.Schema
{
   /// <summary>
   /// �������е��ֵ��ඨ��
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   [XmlRootAttribute(Namespace = "http://www.DrectSoft.com.cn/Wordbook", IsNullable = false)]
   public class Wordbooks
   {
      /// <summary>
      /// �ֵ����������
      /// </summary>
      [XmlElementAttribute("WordbookCatalog")]
      public WordbookCatalog[] Catalogs
      {
         get { return _catalogs; }
         set { _catalogs = value; }
      }
      private WordbookCatalog[] _catalogs;
   }
}
