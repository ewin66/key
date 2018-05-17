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
   /// ��ǰ�����ֶ�����������ֶεĶ�Ӧ��ϵ
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   public partial class ColumnToColumn
   {
      private string _sourceColumn;
      private string _targetColumn;
      private string _defaultValue;

      /// <summary>
      /// ��ǰ��������
      /// </summary>
      [XmlAttributeAttribute()]
      public string SourceColumn
      {
         get
         {
            return _sourceColumn;
         }
         set
         {
            _sourceColumn = value;
         }
      }

      /// <summary>
      /// ��������ԭʼ����
      /// </summary>
      [XmlAttributeAttribute()]
      public string TargetColumn
      {
         get
         {
            return _targetColumn;
         }
         set
         {
            _targetColumn = value;
         }
      }

      /// <summary>
      /// ���������е�ȱʡֵ(��ǰ��������Ϊ��ʱ��Ч)
      /// </summary>
      [XmlAttributeAttribute()]
      public string DefaultValue
      {
         get
         {
            if (_defaultValue == null)
               _defaultValue = "";
            return _defaultValue;
         }
         set
         {
            _defaultValue = value;
         }
      }

      /// <summary>
      /// ���ƶ���
      /// </summary>
      /// <returns></returns>
      public ColumnToColumn Clone()
      {
         ColumnToColumn obj = new ColumnToColumn();
         obj.SourceColumn = SourceColumn;
         obj.TargetColumn = TargetColumn;
         obj.DefaultValue = DefaultValue;
         return obj;
      }
   }
}
