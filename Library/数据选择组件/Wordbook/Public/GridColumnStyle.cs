using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// Grid��Column��������Ϣ(����ҪColumnName����ʾ���ơ���ȵ�)
   /// </summary>
   [CLSCompliantAttribute(true)]
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   public class GridColumnStyle
   {
      /// <summary>
      /// Grid��Column��ColumnName
      /// </summary>
      public string FieldName
      {
         get { return _fieldName; }
         set { _fieldName = value; }
      }
      private string _fieldName;

      /// <summary>
      /// Grid��Column����ʾ����
      /// </summary>
      public string Caption
      {
         get { return _caption; }
         set { _caption = value; }
      }
      private string _caption;

      /// <summary>
      /// Grid��Column�ĳ�ʼ���
      /// </summary>
      public int Width
      {
         get { return _width; }
         set { _width = value; }
      }
      private int _width;

      /// <summary>
      /// ����Grid������ʾ��ʽ����
      /// </summary>
      public GridColumnStyle()
      { }

      /// <summary>
      /// ����Grid������ʾ��ʽ����
      /// </summary>
      /// <param name="column"></param>
      /// <param name="title"></param>
      /// <param name="colWidth"></param>
      public GridColumnStyle(string column, string title, int colWidth)
      {
         _fieldName = column;
         _caption = title;
         _width = colWidth;
      }
   }
}
