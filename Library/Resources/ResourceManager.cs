using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.IO;
using System.Globalization;

namespace DrectSoft.Resources
{
   /// <summary>
   /// ͼ������ö��
   /// </summary>
   public enum IconType
   {
      /// <summary>
      /// ����״̬
      /// </summary>
      Normal,
      /// <summary>
      /// ����״̬
      /// </summary>
      Disable,
      /// <summary>
      /// ����
      /// </summary>
      Highlight
   }

   /// <summary>
   /// �ṩ������Դ�ľ�̬����
   /// </summary>
   public static class ResourceManager
   {
      private static Assembly Resources
      {
         get
         {
            if (_resources == null)
               _resources = Assembly.GetExecutingAssembly();//.GetAssembly(typeof(DrectSoftResourceManager));

            return _resources;
         }
      }
      private static Assembly _resources;

      private static string Namespace
      {
         get
         {
            if (String.IsNullOrEmpty(_namespace))
               _namespace = typeof(ResourceManager).Namespace;
            return _namespace;
         }
      }
      private static string _namespace;

      #region const
      private const string ButtonIconFileType = "png";

      private const string SmallSize = "16";
      private const string MiddleSize = "24";

      private const string FlagDisable = "d";
      private const string FlagHighlight = "h";

      private const string FormatSmallNormal = "{0}_" + SmallSize;
      private const string FormatSmallDisable = "{0}_" + SmallSize + "_" + FlagDisable;
      private const string FormatSmallHigh = "{0}_" + SmallSize + "_" + FlagHighlight;

      private const string FormatMiddleNormal = "{0}_" + MiddleSize;
      private const string FormatMiddleDisable = "{0}_" + MiddleSize + "_" + FlagDisable;
      private const string FormatMiddleHigh = "{0}_" + MiddleSize + "_" + FlagHighlight;

      #endregion

      #region properties
      /// <summary>
      /// Ĭ�ϵĹ�˾Logo
      /// </summary>
      public static Icon DrectSoftLogo
      {
         get
         {
            Stream mapStream = GetStreamFromResources("Icon", ResourceNames.IconDrectSoftLogoLarge, "ico");

            return new Icon(mapStream);
         }
      }

      /// <summary>
      ///��ȡָ��ϵͳlogo
      /// </summary>
      /// <param name="shortKey"></param>
      /// <returns></returns>
      public static Icon GetDrectSoftLogo(string shortKey)
      {
         Stream mapStream = GetStreamFromResources("Icon", ResourceNames.IconDrectSoftLogoLarge+shortKey, "ico");
         return new Icon(mapStream);
      }





      #endregion

      #region private methods
      private static string FormatResourceName(string catalog, string resourceName, string fileType)
      {
         // ��ʽ����Դ���ƣ�Namespace + ���� + ��Դ���� + �ļ����� 
         if (String.IsNullOrEmpty(fileType))
            return String.Format("{0}.{1}.{2}", Namespace, catalog, resourceName);
         else
            return String.Format("{0}.{1}.{2}.{3}", Namespace, catalog, resourceName, fileType);
      }

      private static Image GetNormalIconFromResources(string iconName)
      {
         Stream mapStream = GetStreamFromResources("Icon", iconName, "ico");

         return Image.FromStream(mapStream);
      }

      private static Image GetButtonIconFromResources(string iconName)
      {
         Stream mapStream = GetStreamFromResources("Button", iconName, ButtonIconFileType);

         return Image.FromStream(mapStream);
      }

      private static Image GetImageFromResources(string imageName)
      {
         Stream mapStream = GetStreamFromResources("Images", imageName, String.Empty);

         return Image.FromStream(mapStream);
      }

      private static Stream GetStreamFromResources(string catalog, string resourceName, string fileType)
      {
         try
         {
            return Resources.GetManifestResourceStream(FormatResourceName(catalog, resourceName, fileType));
         }
         catch (FileNotFoundException)
         {
            throw new ArgumentException("ָ������Դ������", resourceName);
         }
         catch
         {
            throw;
         }
      }

      private static void CheckName(string imageName)
      {
         if (String.IsNullOrEmpty(imageName))
            throw new ArgumentNullException("��Դ����Ϊ��");
      }
      #endregion

      #region public methods
      /// <summary>
      /// ��ȡһ���Ե�ͼƬ��bmp��gif��
      /// </summary>
      /// <param name="imageName">ͼƬ����(������׺)</param>
      /// <returns></returns>
      public static Image GetImage(string imageName)
      {
         CheckName(imageName);
         return GetImageFromResources(imageName);
      }

      /// <summary>
      /// ��ȡ��ͨ��ͼ�꣨��ico��β�ģ�
      /// </summary>
      /// <param name="iconName">ͼ�����ƣ���������׺��</param>
      /// <returns></returns>
      public static Image GetNormalIcon(string iconName)
      {
         CheckName(iconName);
         return GetNormalIconFromResources(iconName);
      }

      /// <summary>
      /// ��ȡ��ťС�ߴ�ͼ�꣨16��16��
      /// </summary>
      /// <param name="iconName">ͼ�����ƣ�����Ҫָ���ߴ硢���ͺͺ�׺</param>
      /// <param name="iconType">ͼ������</param>
      /// <returns></returns>
      public static Image GetSmallIcon(string iconName, IconType iconType)
      {
         CheckName(iconName);
         switch (iconType)
         {
            case IconType.Normal:
               return GetButtonIconFromResources(String.Format(CultureInfo.CurrentCulture
                  , FormatSmallNormal, iconName));
            case IconType.Disable:
               return GetButtonIconFromResources(String.Format(CultureInfo.CurrentCulture
                  , FormatSmallDisable, iconName));
            default:
               return GetButtonIconFromResources(String.Format(CultureInfo.CurrentCulture
                  , FormatSmallHigh, iconName));
         }
      }

      /// <summary>
      /// ��ȡ��ť�еȳߴ�ͼ�꣨24��24��
      /// </summary>
      /// <param name="iconName">ͼ�����ƣ�����Ҫָ���ߴ硢���ͺͺ�׺</param>
      /// <param name="iconType">ͼ������</param>
      /// <returns></returns>
      public static Image GetMiddleIcon(string iconName, IconType iconType)
      {
         CheckName(iconName);
         switch (iconType)
         {
            case IconType.Normal:
               return GetButtonIconFromResources(String.Format(CultureInfo.CurrentCulture
                  , FormatMiddleNormal, iconName));
            case IconType.Disable:
               return GetButtonIconFromResources(String.Format(CultureInfo.CurrentCulture
                  , FormatMiddleDisable, iconName));
            default:
               return GetButtonIconFromResources(String.Format(CultureInfo.CurrentCulture
                  , FormatMiddleHigh, iconName));
         }
      }

      /// <summary>
      /// ������Զ�Ӧ����Դ����
      /// </summary>
      /// <param name="fieldName"></param>
      /// <returns></returns>
      public static String GetSourceName(string fieldName)
      {
         FieldInfo field = typeof(ResourceNames).GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
         if (field != null)
            return field.GetValue(null).ToString();
         else
            return String.Empty;
      }
      #endregion
   }
}
