using System;
using System.Drawing;

namespace DrectSoft.Core.Printing
{
   /// <summary>
   /// ͼ�ζ����̶ȶ�����
   /// </summary>
   public class GraphicsDpis
   {


      /// <summary>
      /// 1/75 Ӣ��
      /// </summary>
      public static readonly float Display = 75f;
      /// <summary>
      /// �ĵ���λ��1/300 Ӣ�磩
      /// </summary>
      public static readonly float Document = 300f;
      /// <summary>
      /// �ٷ�֮һӢ��
      /// </summary>
      public static readonly float HundredthsOfAnInch = 100f;
      /// <summary>
      /// Ӣ��
      /// </summary>
      public static readonly float Inch = 1f;
      /// <summary>
      /// ����
      /// </summary>
      public static readonly float Millimeter = 25.4f;
      /// <summary>
      /// �豸����
      /// </summary>
      public static readonly float Pixel = 96f;
      /// <summary>
      /// ��ӡ����
      /// </summary>
      public static readonly float Point = 72f;
      /// <summary>
      /// ʮ��֮һ����
      /// </summary>
      public static readonly float TenthsOfAMillimeter = 254f;
      /// <summary>
      /// Twips
      /// </summary>
      public static readonly float Twips = 1440f;
      /// <summary>
      /// ����
      /// </summary>
      public static readonly float Centimeter = 2.54f;

      static GraphicsDpis()
      {
         Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
         Pixel = graphics.DpiX;
         graphics.Dispose();
      }

      /// <summary>
      /// ���ݶ�����λ�̶ȣ����GraphicsUnitö��ֵ
      /// </summary>
      /// <param name="dpi"></param>
      /// <returns></returns>
      public static GraphicsUnit DpiToUnit(float dpi)
      {
         if (dpi.Equals(Display))
         {
            return GraphicsUnit.Display;
         }
         if (dpi.Equals(Inch))
         {
            return GraphicsUnit.Inch;
         }
         if (dpi.Equals(Document))
         {
            return GraphicsUnit.Document;
         }
         if (dpi.Equals(Millimeter))
         {
            return GraphicsUnit.Millimeter;
         }
         if (dpi.Equals(Pixel))
         {
            return GraphicsUnit.Pixel;
         }
         if (!dpi.Equals(Point))
         {
            throw new ArgumentException("dpi");
         }
         return GraphicsUnit.Point;
      }

      /// <summary>
      /// ��ȡGraphicsUnit��Ӧ�Ķ����̶�
      /// </summary>
      /// <param name="unit"></param>
      /// <returns></returns>
      public static float UnitToDpi(GraphicsUnit unit)
      {
         switch (unit)
         {
            case GraphicsUnit.Display:
               return Display;

            case GraphicsUnit.Pixel:
               return Pixel;

            case GraphicsUnit.Point:
               return Point;

            case GraphicsUnit.Inch:
               return Inch;

            case GraphicsUnit.Document:
               return Document;

            case GraphicsUnit.Millimeter:
               return Millimeter;
         }
         throw new ArgumentException("unit");
      }
   }
}

