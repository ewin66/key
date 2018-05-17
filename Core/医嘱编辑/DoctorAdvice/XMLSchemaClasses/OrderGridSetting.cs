using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// ҽ��Grid����ʽ
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(AnonymousType = true)]
   [XmlRootAttribute(Namespace = "http://medical.DrectSoft.com", IsNullable = false)]
   public partial class OrderGridSetting
   {
      private TypeXmlFont _gridFont;
      private int _rowHeight;
      private bool _showBand;
      private GridSettingColumnBasic[] _columns;
      private TypeGridBand[] _longOrderSetting;
      private TypeGridBand[] _tempOrderSetting;

      /// <summary>
      /// Grid��Ĭ������
      /// </summary>
      [Category("����"), DisplayName("Ĭ������"), Description("ҽ������Ĭ������")]
      public TypeXmlFont GridFont
      {
         get { return _gridFont; }
         set { _gridFont = value; }
      }

      /// <summary>
      /// Grid��Ĭ���и�
      /// </summary>
      [Category("��ʾ"), DisplayName("Ĭ���и�"), Description("ҽ������Ĭ���и�,������Ϊ��λ")]
      public int RowHeight
      {
         get { return _rowHeight; }
         set { _rowHeight = value; }
      }

      /// <summary>
      /// �Ƿ���ʾGrid��Band
      /// </summary>
      [Category("��ʾ"), DisplayName("��ʾ���ķ���"), Description("��ҽ��������Ƿ���ʾ������ͷ")]
      public bool ShowBand
      {
         get { return _showBand; }
         set { _showBand = value; }
      }

      /// <summary>
      /// Grid�е�������
      /// </summary>
      [Category("��ʾ"), DisplayName("������ʾ����"), Description("������ʾ��ҽ������е���"), Browsable(false)]
      [XmlArrayItemAttribute("ColumnBasic", IsNullable = false)]
      public GridSettingColumnBasic[] Columns
      {
         get { return _columns; }
         set { _columns = value; }
      }

      /// <summary>
      /// ����ҽ������
      /// </summary>
      [Category("��ʾ"), DisplayName("����ҽ����ͷ"), Description("����ҽ�������Ҫ��ʾ���м���˳��"), Browsable(false)]
      [XmlArrayItemAttribute("Band", IsNullable = false)]
      public TypeGridBand[] LongOrderSetting
      {
         get { return _longOrderSetting; }
         set { _longOrderSetting = value; }
      }

      /// <summary>
      /// ��ʱҽ������
      /// </summary>
      [Category("��ʾ"), DisplayName("��ʱҽ����ͷ"), Description("��ʱҽ�������Ҫ��ʾ���м���˳��"), Browsable(false)]
      [XmlArrayItemAttribute("Band", IsNullable = false)]
      public TypeGridBand[] TempOrderSetting
      {
         get { return _tempOrderSetting; }
         set { _tempOrderSetting = value; }
      }

      /// <summary>
      /// ҽ�����ݵ�Ԫ��Ŀ��
      /// </summary>
      [Category("��ʾ"), DisplayName("ҽ�������еĿ��"), Description("������Ϊ��λ"), Browsable(false)]
      public int WidthOfContentCell
      {
         get 
         {
            foreach (GridSettingColumnBasic col in _columns)
               if (col.Name == "UNContent")
                  return col.Width;
            return 250;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="colName"></param>
      /// <returns></returns>
      public int GetColumnWidth(string colName)
      {
         foreach (GridSettingColumnBasic col in Columns)
            if (col.Name == colName)
               return col.Width;
         return -1;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="colName"></param>
      /// <returns></returns>
      public string GetColumnCaption(string colName)
      {
         foreach (GridSettingColumnBasic col in Columns)
            if (col.Name == colName)
               return col.Caption;
         return "";
      }
   }

   /// <summary>
   /// �л�����Ϣ
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(AnonymousType = true)]
   public partial class GridSettingColumnBasic
   {
      private string _name;
      private string _caption;
      private int _width;
      private string _memo;

      /// <summary>
      /// ������
      /// </summary>
      [XmlAttributeAttribute()]
      [DisplayName("����"), Description("")]
      public string Name
      {
         get { return _name; }
         set { _name = value; }
      }

      /// <summary>
      /// ����ʾ����
      /// </summary>
      [XmlAttributeAttribute()]
      [DisplayName("��ʾ����"), Description("")]
      public string Caption
      {
         get { return _caption; }
         set { _caption = value; }
      }      

      /// <summary>
      /// ��Ĭ�Ͽ��
      /// </summary>
      [XmlAttributeAttribute()]
      [DisplayName("���"), Description("")]
      public int Width
      {
         get { return _width; }
         set { _width = value; }
      }

      /// <summary>
      /// ��˵��
      /// </summary>
      [XmlAttributeAttribute()]
      [DisplayName("˵��"), Description("")]
      public string Memo
      {
         get { return _memo; }
         set { _memo = value; }
      }
   }
}