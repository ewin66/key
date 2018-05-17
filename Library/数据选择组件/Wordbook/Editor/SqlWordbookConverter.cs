using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// ����String��SQL�ֵ���֮��Ļ�������PropertyGrid������SqlWordbook���͵�����ʱ��Ҫ
   /// </summary>
   public class SqlWordbookConverter : ExpandableObjectConverter
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
         // �����Stringת��BaseWordbook����
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
         // ��Stringת��BaseWordbook��
         string propertyList = (string)value;

         if (propertyList != null)
         {
            if (propertyList.Length == 0)
               return null;
            // ����BaseWordbookʵ��
            try
            {
               string[] properties = propertyList.Split(';');

               return new SqlWordbook(properties[0].Trim()
                  , properties[1].Trim()
                  , properties[2].Trim()
                  , properties[3].Trim()
                  , properties[5].Trim()
                  , properties[4].Trim());
            }
            catch
            {
               throw new ArgumentException();
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
         // ��SqlWordbook��ת��String��InstanceDescriptor
         SqlWordbook sqlBook = value as SqlWordbook;
         if (sqlBook != null)
         {
            // ת��String
            if ((destinationType == typeof(string)))
            {
               return sqlBook.ToString();
            }
            // ת��InstanceDescriptor������ƽ��汣�浽Designer�ļ�ʱ��Ҫ��
            if ((destinationType == typeof(InstanceDescriptor)))
            {
               object[] properties = new object[6];
               Type[] types = new Type[6];

               types[0] = typeof(string);
               properties[0] = sqlBook.WordbookName;

               types[1] = typeof(string);
               properties[1] = sqlBook.QuerySentence;

               types[2] = typeof(string);
               properties[2] = sqlBook.CodeField;

               types[3] = typeof(string);
               properties[3] = sqlBook.NameField;

               types[4] = typeof(string);
               properties[4] = sqlBook.DefaultGridStyle.ToString();

               types[5] = typeof(string);
               properties[5] = sqlBook.MatchFieldComb;

               // �õ����캯����Ϣ
               ConstructorInfo ci = sqlBook.GetType().GetConstructor(types);
               return new InstanceDescriptor(ci, properties);
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
         return new SqlWordbook(propertyValues["WordbookName"].ToString()
                                , propertyValues["QuerySentence"].ToString()
                                , propertyValues["DefaultCodeField"].ToString()
                                , propertyValues["DefaultDisplayField"].ToString()
                                , propertyValues["DefaultGridStyle"].ToString()
                                , propertyValues["MatchFieldComb"].ToString());
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
