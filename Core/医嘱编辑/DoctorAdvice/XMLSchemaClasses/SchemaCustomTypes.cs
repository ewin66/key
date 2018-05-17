using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;
using System.Globalization;
using System.Drawing.Design;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// �������á�Size�Դ�ӡ���㣨1/72 Ӣ�磩ָ��Ϊ������λ���ַ���Ĭ��Ϊ����
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(Namespace = "http://medical.DrectSoft.com")]
   [TypeConverter(typeof(TypeXmlFontConvert))]
   public partial class TypeXmlFont
   {
      private string _fontFamily;
      private float _size;
      private FontStyle _style;

      /// <remarks/>
      [DisplayName("����")]
      [TypeConverter(typeof(FontConverter.FontNameConverter)), Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
      [XmlAttribute()]
      public string FontFamily
      {
         get
         {
            return _fontFamily;
         }
         set
         {
            _fontFamily = value;
         }
      }

      /// <remarks/>
      [DisplayName("�ֺ�")]
      [XmlAttribute()]
      public float Size
      {
         get
         {
            return _size;
         }
         set
         {
            _size = value;
         }
      }

      /// <remarks/>
      [DisplayName("��ʽ")]
      [XmlAttribute("FontStyle")]
      public FontStyle Style
      {
         get
         {
            return _style;
         }
         set
         {
            _style = value;
         }
      }

      //public TypeXmlFont(Font f)
      //{
      //   FontFamily = f.FontFamily.Name;
      //   Size = f.Size;
      //   Style = f.Style;
      //}

      [XmlIgnore(), Browsable(false)]
      public Font Font
      {
         get
         {
            return new Font(FontFamily, Size, Style, GraphicsUnit.Point, ((byte)(134)));
         }
      }

      public override string ToString()
      {
         return FontFamily + "," + Size.ToString() + "," + Style.ToString();
      }
   }

   /// <summary>
   /// ��ɫ�ԣ�ǰ���ͱ���ɫ��
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(Namespace = "http://medical.DrectSoft.com")]
   [TypeConverter(typeof(TypeColorPairConvert))]
   public partial class TypeColorPair
   {
      private Color _backColor;
      private Color _foreColor;

      /// <summary>
      /// ����ɫ
      /// </summary>
      [XmlIgnore(), DisplayName("����ɫ")]
      public Color BackColor
      {
         get
         {
            return _backColor;
         }
         set
         {
            _backColor = value;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      [XmlElement("BackColor"), Browsable(false)]
      public string XmlBackColor
      {
         get
         {
            return TypeColorPair.SerializeColor(_backColor);
         }
         set
         {
            _backColor = TypeColorPair.DeserializeColor(value);
         }
      }

      /// <summary>
      /// ǰ��ɫ
      /// </summary>
      [XmlIgnore(), DisplayName("ǰ��ɫ")]
      public Color ForeColor
      {
         get
         {
            return _foreColor;
         }
         set
         {
            _foreColor = value;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      [XmlElement("ForeColor"), Browsable(false)]
      public string XmlForeColor
      {
         get
         {
            return TypeColorPair.SerializeColor(_foreColor);
         }
         set
         {
            _foreColor = TypeColorPair.DeserializeColor(value);
         }
      }

      /// <summary>
      /// ����ɫ���л����ַ���������ֻ֧��NamedColor��ARGB��ʽ��
      /// </summary>
      /// <param name="color"></param>
      /// <returns></returns>
      public static string SerializeColor(Color color)
      {
         if (color.IsNamedColor)
            return color.Name;
         else
            return String.Format(CultureInfo.CurrentCulture
                  , "{0},{1},{2},{3}", color.A, color.R, color.G, color.B);
      }

      /// <summary>
      /// ���ַ��������л���Color
      /// </summary>
      /// <param name="color"></param>
      /// <returns></returns>
      public static Color DeserializeColor(string color)
      {
         if (String.IsNullOrEmpty(color))
            throw new ArgumentNullException();

         string[] pieces = color.Split(new char[] { ',' });

         if (pieces.Length == 1)
         {
            return Color.FromName(pieces[0]);
         }
         else
         {
            byte a, r, g, b;
            a = byte.Parse(pieces[0], CultureInfo.CurrentCulture);
            r = byte.Parse(pieces[1], CultureInfo.CurrentCulture);
            g = byte.Parse(pieces[2], CultureInfo.CurrentCulture);
            b = byte.Parse(pieces[3], CultureInfo.CurrentCulture);

            return Color.FromArgb(a, r, g, b);
         }
      }

      public override string ToString()
      {
         return XmlForeColor + " | " + XmlBackColor;
      }
   }

   /// <summary>
   /// ��Ԫ����ɫ���ã���������ѡ�����������
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(Namespace = "http://medical.DrectSoft.com")]
   public partial class TypeCellColor
   {
      private TypeColorPair _normalColor;
      private TypeColorPair _selectedColor;

      /// <summary>
      /// �����������ɫ
      /// </summary>
      public TypeColorPair NormalColor
      {
         get { return _normalColor; }
         set { _normalColor = value; }
      }

      /// <summary>
      /// ѡ��״̬�µ���ɫ
      /// </summary>
      public TypeColorPair SelectedColor
      {
         get { return _selectedColor; }
         set { _selectedColor = value; }
      }
   }

   /// <summary>
   /// ��Grid��Band������
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(Namespace = "http://medical.DrectSoft.com")]
   public partial class TypeGridBand
   {
      private string[] _columnNames;
      private OrderGridBandName _bandName;

      /// <summary>
      /// Band�а�������
      /// </summary>
      [XmlElementAttribute("ColumnName")]
      public string[] ColumnNames
      {
         get
         {
            return _columnNames;
         }
         set
         {
            _columnNames = value;
         }
      }

      /// <remarks/>
      [XmlAttributeAttribute()]
      public OrderGridBandName BandName
      {
         get
         {
            return _bandName;
         }
         set
         {
            _bandName = value;
         }
      }
   }

   /// <summary>
   /// ��ʾ���û����� (UI) Ԫ�ع����Ŀհ׻�߾���Ϣ
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(Namespace = "http://medical.DrectSoft.com")]
   public partial class TypePadding
   {
      private int _all;
      private int _top;
      private int _bottom;
      private int _left;
      private int _right;

      /// <remarks/>
      public int All
      {
         get { return _all; }
         set { _all = value; }
      }

      /// <remarks/>
      public int Top
      {
         get
         {
            return _top;
         }
         set
         {
            _top = value;
         }
      }

      /// <remarks/>
      public int Bottom
      {
         get
         {
            return _bottom;
         }
         set
         {
            _bottom = value;
         }
      }

      /// <remarks/>
      public int Left
      {
         get
         {
            return _left;
         }
         set
         {
            _left = value;
         }
      }

      /// <remarks/>
      public int Right
      {
         get
         {
            return _right;
         }
         set
         {
            _right = value;
         }
      }
   }

}
