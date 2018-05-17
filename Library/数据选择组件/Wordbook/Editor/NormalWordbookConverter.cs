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
   /// ����String���ֵ���֮��Ļ�������PropertyGrid������BaseWordbook���͵�����ʱ��Ҫ
   /// </summary>
   public class NormalWordbookConverter : ExpandableObjectConverter
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
         string bookName = (string)value;
         if (bookName != null)
         {
            // ����BaseWordbookʵ��
            try
            {
               // ���þ�̬�����õ���ȷ���ֵ��ࡣ�޶�Ӧ���ƽ�����null
               return WordbookStaticHandle.GetWordbook(bookName);
            }
            catch
            {
               throw;// new ArgumentException("��������ȷ");
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
         // ��BaseWordbook��ת��String��InstanceDescriptor
         BaseWordbook wordbook = value as BaseWordbook;
         if (wordbook != null)
         {
            // ת��String
            if ((destinationType == typeof(string)))
            {
               return (wordbook).ToString();
            }
            // ת��InstanceDescriptor������ƽ��汣�浽Designer�ļ�ʱ��Ҫ��
            if ((destinationType == typeof(InstanceDescriptor)))
            {
               object[] properties = new object[5];
               Type[] types = new Type[5];
               // FilterFields
               types[0] = typeof(string);
               properties[0] = wordbook.MatchFieldComb;
               // SelectedStyleIndex
               types[1] = typeof(int);
               properties[1] = wordbook.SelectedStyleIndex;
               // ParameterValues
               types[2] = typeof(string);
               properties[2] = wordbook.ParameterValueComb;
               // ExtraCondition
               types[3] = typeof(string);
               properties[3] = wordbook.ExtraCondition;
               // CacheTime
               types[4] = typeof(int);
               properties[4] = wordbook.CacheTime;

               //object[] properties = new object[1];
               //Type[] types = new Type[1];
               ////// FilterFields
               //types[0] = typeof(Collection<string>);
               //Collection<string> filters = new Collection<string>();
               //filters.Add("aa");
               //properties[0] = new Collection<string>(filters);

               //object[] properties = new object[3];
               //Type[] types = new Type[3];
               ////// FilterFields
               //types[0] = typeof(Collection<string>);
               ////Collection<string> filters = new Collection<string>(wordbook.FilterFields);
               //properties[0] = new Collection<string>();
               //((Collection<string>)properties[0]).Add("czydm");
               //((Collection<string>)properties[0]).Add("wb");
               //((Collection<string>)properties[0]).Add("py");

               //// SelectedStyleIndex
               //types[1] = typeof(int);
               //properties[1] = wordbook.SelectedStyleIndex;
               //// ExtraCondition
               //types[2] = typeof(string);
               //properties[2] = wordbook.ExtraCondition;
               // �õ����캯����Ϣ
               ConstructorInfo ci = wordbook.GetType().GetConstructor(types);
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
         if ( propertyValues == null )
            throw new ArgumentNullException(MessageStringManager.GetString("NullParameter"));
         BaseWordbook wordbook = WordbookStaticHandle.GetWordbook((string)propertyValues["WordbookName"]);
         wordbook.ExtraCondition = propertyValues["ExtraCondition"].ToString();
         wordbook.SelectedStyleIndex = (int)propertyValues["SelectedStyleIndex"];
         wordbook.MatchFieldComb = propertyValues["MatchFieldComb"].ToString();
         wordbook.ParameterValueComb = propertyValues["ParameterValue"].ToString();
         wordbook.CacheTime = (int)propertyValues["SelectedStyleIndex"];
         return wordbook;
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
