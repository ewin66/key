using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace DrectSoft.Core.Printing
{
   /// <summary>
   /// ͼ�ε�λ�����࣬ǿ�Ʊ�֤ת���󲻻���Ϊ�����������⵼��ת������С��ת��ǰ����
   /// </summary>
   public class GraphicsUnitTransformer
   {
      private static float scale = 1f;
      //private static Hashtable alignHT;
      //private static Hashtable valignHT;

      //static GraphicsUnitConverter()
      //{
      //   alignHT = new Hashtable();
      //   InitializeHash(alignHT, StringAlignment.Near, new object[] { TextAlignment.TopLeft, TextAlignment.MiddleLeft, TextAlignment.BottomLeft, TextAlignment.TopJustify, TextAlignment.MiddleJustify, TextAlignment.BottomJustify });
      //   InitializeHash(alignHT, StringAlignment.Center, new object[] { TextAlignment.TopCenter, TextAlignment.MiddleCenter, TextAlignment.BottomCenter });
      //   InitializeHash(alignHT, StringAlignment.Far, new object[] { TextAlignment.TopRight, TextAlignment.MiddleRight, TextAlignment.BottomRight });
      //   valignHT = new Hashtable();
      //   InitializeHash(valignHT, StringAlignment.Near, new object[] { TextAlignment.TopLeft, TextAlignment.TopCenter, TextAlignment.TopRight, TextAlignment.TopJustify });
      //   InitializeHash(valignHT, StringAlignment.Center, new object[] { TextAlignment.MiddleLeft, TextAlignment.MiddleCenter, TextAlignment.MiddleRight, TextAlignment.MiddleJustify });
      //   InitializeHash(valignHT, StringAlignment.Far, new object[] { TextAlignment.BottomLeft, TextAlignment.BottomCenter, TextAlignment.BottomRight, TextAlignment.BottomJustify });
      //}

      #region public convert methods
      /// <summary>
      /// ת����
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromUnit"></param>
      /// <param name="toUnit"></param>
      /// <returns></returns>
      public static Point Convert(Point val, GraphicsUnit fromUnit, GraphicsUnit toUnit)
      {
         return Convert(val, GraphicsDpis.UnitToDpi(fromUnit), GraphicsDpis.UnitToDpi(toUnit));
      }

      /// <summary>
      /// ת����
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static Point Convert(Point val, float fromDpi, float toDpi)
      {
         SetScale(fromDpi, toDpi);
         return new Point(Conv(val.X), Conv(val.Y));
      }

      /// <summary>
      /// ת����
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromUnit"></param>
      /// <param name="toUnit"></param>
      /// <returns></returns>
      public static PointF Convert(PointF val, GraphicsUnit fromUnit, GraphicsUnit toUnit)
      {
         return Convert(val, GraphicsDpis.UnitToDpi(fromUnit), GraphicsDpis.UnitToDpi(toUnit));
      }

      /// <summary>
      /// ת����
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static PointF Convert(PointF val, float fromDpi, float toDpi)
      {
         SetScale(fromDpi, toDpi);
         return new PointF(ConvF(val.X), ConvF(val.Y));
      }

      /// <summary>
      /// ת������
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromUnit"></param>
      /// <param name="toUnit"></param>
      /// <returns></returns>
      public static Rectangle Convert(Rectangle val, GraphicsUnit fromUnit, GraphicsUnit toUnit)
      {
         return Convert(val, GraphicsDpis.UnitToDpi(fromUnit), GraphicsDpis.UnitToDpi(toUnit));
      }

      /// <summary>
      /// ת������
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static Rectangle Convert(Rectangle val, float fromDpi, float toDpi)
      {
         return Round(Convert((RectangleF)val, fromDpi, toDpi));
      }

      /// <summary>
      /// ת������
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromUnit"></param>
      /// <param name="toUnit"></param>
      /// <returns></returns>
      public static RectangleF Convert(RectangleF val, GraphicsUnit fromUnit, GraphicsUnit toUnit)
      {
         return Convert(val, GraphicsDpis.UnitToDpi(fromUnit), GraphicsDpis.UnitToDpi(toUnit));
      }

      /// <summary>
      /// ת������
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static RectangleF Convert(RectangleF val, float fromDpi, float toDpi)
      {
         SetScale(fromDpi, toDpi);
         return RectangleF.FromLTRB(ConvF(val.Left), ConvF(val.Top), ConvF(val.Right), ConvF(val.Bottom));
      }

      /// <summary>
      /// ת���ߴ�
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromUnit"></param>
      /// <param name="toUnit"></param>
      /// <returns></returns>
      public static Size Convert(Size val, GraphicsUnit fromUnit, GraphicsUnit toUnit)
      {
         return Convert(val, GraphicsDpis.UnitToDpi(fromUnit), GraphicsDpis.UnitToDpi(toUnit));
      }

      /// <summary>
      /// ת���ߴ�
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static Size Convert(Size val, float fromDpi, float toDpi)
      {
         SetScale(fromDpi, toDpi);
         return new Size(Conv(val.Width), Conv(val.Height));
      }

      /// <summary>
      /// ת���ߴ�
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromUnit"></param>
      /// <param name="toUnit"></param>
      /// <returns></returns>
      public static SizeF Convert(SizeF val, GraphicsUnit fromUnit, GraphicsUnit toUnit)
      {
         return Convert(val, GraphicsDpis.UnitToDpi(fromUnit), GraphicsDpis.UnitToDpi(toUnit));
      }

      /// <summary>
      /// ת���ߴ�
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static SizeF Convert(SizeF val, float fromDpi, float toDpi)
      {
         SetScale(fromDpi, toDpi);
         return new SizeF(ConvF(val.Width), ConvF(val.Height));
      }

      /// <summary>
      /// ת������
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static int Convert(int val, float fromDpi, float toDpi)
      {
         SetScale(fromDpi, toDpi);
         return Conv(val);
      }

      /// <summary>
      /// ת��������
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromUnit"></param>
      /// <param name="toUnit"></param>
      /// <returns></returns>
      public static float Convert(float val, GraphicsUnit fromUnit, GraphicsUnit toUnit)
      {
         return Convert(val, GraphicsDpis.UnitToDpi(fromUnit), GraphicsDpis.UnitToDpi(toUnit));
      }

      /// <summary>
      /// ת��������
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static float Convert(float val, float fromDpi, float toDpi)
      {
         SetScale(fromDpi, toDpi);
         return ConvF(val);
      }

      /// <summary>
      /// ת���߾�
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromUnit"></param>
      /// <param name="toUnit"></param>
      /// <returns></returns>
      public static Margins Convert(Margins val, GraphicsUnit fromUnit, GraphicsUnit toUnit)
      {
         return Convert(val, GraphicsDpis.UnitToDpi(fromUnit), GraphicsDpis.UnitToDpi(toUnit));
      }

      /// <summary>
      /// ת���߾�
      /// </summary>
      /// <param name="val"></param>
      /// <param name="fromDpi">�����ڵĶ����̶�</param>
      /// <param name="toDpi">Ҫת�����Ķ����̶�</param>
      /// <returns></returns>
      public static Margins Convert(Margins val, float fromDpi, float toDpi)
      {
         SetScale(fromDpi, toDpi);
         return new Margins(Conv(val.Left), Conv(val.Right), Conv(val.Top), Conv(val.Bottom));
      }

      /// <summary>
      /// �ĵ���λ�ĵ�ת�����ص�λ
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static PointF DocToPixel(PointF val)
      {
         return Convert(val, GraphicsDpis.Document, GraphicsDpis.Pixel);
      }

      /// <summary>
      /// �ĵ���λ�ľ���ת�����ص�λ
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static RectangleF DocToPixel(RectangleF val)
      {
         return Convert(val, GraphicsDpis.Document, GraphicsDpis.Pixel);
      }

      /// <summary>
      /// �ĵ���λ�ĳߴ�ת�����ص�λ
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static SizeF DocToPixel(SizeF val)
      {
         return Convert(val, GraphicsDpis.Document, GraphicsDpis.Pixel);
      }

      /// <summary>
      /// �ĵ���λ��ֵת�����ص�λ
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static float DocToPixel(float val)
      {
         return Convert(val, GraphicsDpis.Document, GraphicsDpis.Pixel);
      }

      /// <summary>
      /// �㣺�豸���ص��ĵ���λ
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static PointF PixelToDoc(PointF val)
      {
         return Convert(val, GraphicsDpis.Pixel, GraphicsDpis.Document);
      }

      /// <summary>
      /// ���Σ��豸���ص��ĵ���λ
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static RectangleF PixelToDoc(RectangleF val)
      {
         return Convert(val, GraphicsDpis.Pixel, GraphicsDpis.Document);
      }

      /// <summary>
      /// �ߴ磺�豸���ص��ĵ���λ
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static SizeF PixelToDoc(SizeF val)
      {
         return Convert(val, GraphicsDpis.Pixel, GraphicsDpis.Document);
      }

      /// <summary>
      /// ����
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static float PixelToDoc(float val)
      {
         return Convert(val, GraphicsDpis.Pixel, GraphicsDpis.Document);
      }

      /// <summary>
      /// ����Pointȡ��
      /// </summary>
      /// <param name="point"></param>
      /// <returns></returns>
      public static Point Round(PointF point)
      {
         return new Point(Round(point.X), Round(point.Y));
      }

      /// <summary>
      /// �������ȡ��
      /// </summary>
      /// <param name="rect"></param>
      /// <returns></returns>
      public static Rectangle Round(RectangleF rect)
      {
         return Rectangle.FromLTRB(Round(rect.Left), Round(rect.Top), Round(rect.Right), Round(rect.Bottom));
      }

      /// <summary>
      /// �Ը�����ȡ��
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static int Round(float val)
      {
         //return (int)Math.Ceiling(val);
          return System.Convert.ToInt32(val);
      }

      //public static StringAlignment ToHorzStringAlignment(TextAlignment align)
      //{
      //   return (StringAlignment)alignHT[align];
      //}

      //public static StringAlignment ToVertStringAlignment(TextAlignment align)
      //{
      //   return (StringAlignment)valignHT[align];
      //}

      //public static ImageSizeMode ToImageSizeMode(PictureBoxSizeMode sizeMode)
      //{
      //   return (ImageSizeMode)sizeMode;
      //}

      //public static PictureBoxSizeMode ToPictureBoxSizeMode(ImageSizeMode sizeMode)
      //{
      //   if (sizeMode != ImageSizeMode.ZoomImage)
      //   {
      //      return (PictureBoxSizeMode)sizeMode;
      //   }
      //   return PictureBoxSizeMode.StretchImage;
      //}
      #endregion

      #region private methods
      private static int Conv(int val)
      {
         return Round((float)val * scale);
      }

      private static float ConvF(float val)
      {
         return (val * scale);
      }

      //private static void InitializeHash(Hashtable ht, object[] values, object[] keys)
      //{
      //   for (int i = 0; i < keys.Length; i++)
      //   {
      //      ht.Add(keys[i], values[i]);
      //   }
      //}

      //private static void InitializeHash(Hashtable ht, object value, object[] keys)
      //{
      //   for (int i = 0; i < keys.Length; i++)
      //   {
      //      ht.Add(keys[i], value);
      //   }
      //}

      private static void SetScale(float fromDpi, float toDpi)
      {
         scale = toDpi/ fromDpi;
      }
      #endregion
   }
}

