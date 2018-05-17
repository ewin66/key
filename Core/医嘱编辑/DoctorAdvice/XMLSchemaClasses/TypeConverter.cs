using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Collections;
using System.Drawing;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// ����String��TypeXmlFont��ת��
   /// </summary>
   public class TypeXmlFontConvert : ExpandableObjectConverter
   {
      /// <summary>
      /// ���ش�ת�����Ƿ�ɽ��ö���ת��Ϊָ��������
      /// </summary>
      /// <param name="context"></param>
      /// <param name="destinationType"></param>
      /// <returns></returns>
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
         if (destinationType == typeof(InstanceDescriptor))
            return true;
         return base.CanConvertTo(context, destinationType);
      }

      /// <summary>
      /// ���ظ�ת�����Ƿ���Խ�һ�����͵Ķ���ת��Ϊ��ת����������
      /// </summary>
      /// <param name="context"></param>
      /// <param name="sourceType"></param>
      /// <returns></returns>
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
         // �����String
         if (sourceType == typeof(string))
         {
            return true;
         }
         return base.CanConvertFrom(context, sourceType);
      }

      /// <summary>
      /// ������ֵת��Ϊ��ת����������
      /// </summary>
      /// <param name="context"></param>
      /// <param name="culture"></param>
      /// <param name="value"></param>
      /// <returns></returns>
      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
      {
         // ��Stringת��
         string fontValue = (string)value;
         if (fontValue != null)
         {
            try
            {
               TypeXmlFont newFont = new TypeXmlFont();
               string[] fontParts = fontValue.Split(new char[] { ',' });
               newFont.FontFamily = fontParts[0].Trim();
               newFont.Size = (float)Convert.ToDouble(fontParts[1].Trim());
               newFont.Style = (FontStyle)Enum.Parse(typeof(FontStyle), fontParts[2].Trim());
               return newFont;
            }
            catch
            {
               throw; 
            }

         }
         return base.ConvertFrom(context, culture, value);
      }

      /// <summary>
      /// ������ֵ����ת��Ϊָ��������
      /// </summary>
      /// <param name="context"></param>
      /// <param name="culture"></param>
      /// <param name="value"></param>
      /// <param name="destinationType"></param>
      /// <returns></returns>
      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
      {
         TypeXmlFont font = value as TypeXmlFont;
         if (font != null)
         {
            // ת��String
            if ((destinationType == typeof(string)))
            {
               return font.ToString();
            }
         }
         // �����ConvertTo()�������
         return base.ConvertTo(context, culture, value, destinationType);
      }

      /// <summary>
      /// �ڸ��� Object ��һ������ֵ����������´����ö���
      /// </summary>
      /// <param name="context"></param>
      /// <param name="propertyValues"></param>
      /// <returns></returns>
      public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
      {
         if (propertyValues == null)
            throw new ArgumentNullException("NullParameter");

         TypeXmlFont font = new TypeXmlFont();
         font.FontFamily = propertyValues["FontFamily"].ToString();
         font.Size = (float)propertyValues["Size"];
         font.Style = (FontStyle)Enum.Parse(typeof(FontStyle), propertyValues["Style"].ToString());
         return font;
      }

      /// <summary>
      /// ���ظ��Ĵ˶����ֵ�Ƿ���Ҫ���� CreateInstance ��������ֵ��
      /// </summary>
      /// <param name="context"></param>
      /// <returns></returns>
      public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
      {
         return true;
      }
   }

   /// <summary>
   /// ����String��TypeColorPair��ת��
   /// </summary>
   public class TypeColorPairConvert : ExpandableObjectConverter
   {
      /// <summary>
      /// ���ش�ת�����Ƿ�ɽ��ö���ת��Ϊָ��������
      /// </summary>
      /// <param name="context"></param>
      /// <param name="destinationType"></param>
      /// <returns></returns>
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
         if (destinationType == typeof(InstanceDescriptor))
            return true;
         return base.CanConvertTo(context, destinationType);
      }

      /// <summary>
      /// ���ظ�ת�����Ƿ���Խ�һ�����͵Ķ���ת��Ϊ��ת����������
      /// </summary>
      /// <param name="context"></param>
      /// <param name="sourceType"></param>
      /// <returns></returns>
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
         // �����String
         if (sourceType == typeof(string))
         {
            return true;
         }
         return base.CanConvertFrom(context, sourceType);
      }

      /// <summary>
      /// ������ֵת��Ϊ��ת����������
      /// </summary>
      /// <param name="context"></param>
      /// <param name="culture"></param>
      /// <param name="value"></param>
      /// <returns></returns>
      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
      {
         // ��Stringת��
         string colorValue = (string)value;
         if (colorValue != null)
         {
            try
            {
               TypeColorPair colorPair = new TypeColorPair();
               string[] colorParts = colorValue.Split(new char[] { '|' });
               colorPair.XmlForeColor = colorParts[0].Trim();
               colorPair.XmlBackColor = colorParts[1].Trim();
               return colorPair;
            }
            catch
            {
               throw;
            }

         }
         return base.ConvertFrom(context, culture, value);
      }

      /// <summary>
      /// ������ֵ����ת��Ϊָ��������
      /// </summary>
      /// <param name="context"></param>
      /// <param name="culture"></param>
      /// <param name="value"></param>
      /// <param name="destinationType"></param>
      /// <returns></returns>
      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
      {
         TypeColorPair color = value as TypeColorPair;
         if (color != null)
         {
            // ת��String
            if ((destinationType == typeof(string)))
            {
               return color.ToString();
            }
         }
         // �����ConvertTo()�������
         return base.ConvertTo(context, culture, value, destinationType);
      }

      /// <summary>
      /// �ڸ��� Object ��һ������ֵ����������´����ö���
      /// </summary>
      /// <param name="context"></param>
      /// <param name="propertyValues"></param>
      /// <returns></returns>
      public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
      {
         if (propertyValues == null)
            throw new ArgumentNullException("NullParameter");

         TypeColorPair colorPair = new TypeColorPair();
         colorPair.ForeColor = (Color)propertyValues["ForeColor"];
         colorPair.BackColor = (Color)propertyValues["BackColor"];
         return colorPair;
      }

      /// <summary>
      /// ���ظ��Ĵ˶����ֵ�Ƿ���Ҫ���� CreateInstance ��������ֵ��
      /// </summary>
      /// <param name="context"></param>
      /// <returns></returns>
      public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
      {
         return true;
      }
   }
}
