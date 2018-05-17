using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// �����Ϣ�ṹ�壬����������ı��������Լ�����
   /// </summary>
   public struct OutputInfoStruct
   {
      #region properties
      /// <summary>
      /// ����ı�(��������Ƿ����ǣ�������ı�Ϊ��)
      /// </summary>
      public string Text
      {
         get 
         {
            if (_text == null)
               return "";
            return _text; 
         }
         set { _text = value; }
      }
      private string _text;

      /// <summary>
      /// �����Χ(�����Ƿ����Ǿ��ο�ķ�Χ)
      /// </summary>
      public Rectangle Bounds
      {
         get { return _bounds; }
         set { _bounds = value; }
      }
      private Rectangle _bounds;

      /// <summary>
      /// ���ʹ�õ�����
      /// </summary>
      public Font Font
      {
         get { return _font; }
         set { _font = value; }
      } 
      private Font _font;

      /// <summary>
      /// �����������
      /// </summary>
      public OrderOutputTextType OutputType
      {
         get { return _outputType; }
         set { _outputType = value; }
      }
      private OrderOutputTextType _outputType;
      #endregion

      #region ctor
      /// <summary>
      /// 
      /// </summary>
      /// <param name="text"></param>
      /// <param name="bounds"></param>
      /// <param name="font"></param>
      /// <param name="outputType"></param>
      public OutputInfoStruct(string text, Rectangle bounds, Font font, OrderOutputTextType outputType)
      {
         _text = text;
         _bounds = bounds;
         _font = font;
         _outputType = outputType;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="text"></param>
      /// <param name="outputType"></param>
      public OutputInfoStruct(string text, OrderOutputTextType outputType)
      {
         _text = text;
         _outputType = outputType;
         _bounds = new Rectangle();
         _font = new Font("SimSun", 10.5F, FontStyle.Regular, GraphicsUnit.Point
            , ((byte)(134)));
      }
      #endregion

      #region public method
      /// <summary>
      /// ȷ�����������Ƿ������ͬ��ֵ
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         if (obj is OutputInfoStruct)
         {
            OutputInfoStruct aimObj = (OutputInfoStruct)obj;
            return ((aimObj.Text == Text) && (aimObj.Bounds == Bounds)
               && (aimObj.Font == Font)
               && (aimObj.OutputType == OutputType));
         }
         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="output1"></param>
      /// <param name="output2"></param>
      /// <returns></returns>
      public static bool operator ==(OutputInfoStruct output1, OutputInfoStruct output2)
      {
         return output1.Equals(output2);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="output1"></param>
      /// <param name="output2"></param>
      /// <returns></returns>
      public static bool operator !=(OutputInfoStruct output1, OutputInfoStruct output2)
      {
         return !(output1 == output2);
      }

      /// <summary>
      /// ���ظ�ʵ���Ĺ�ϣ����
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return Text.GetHashCode();
      }

      /// <summary>
      /// ��ȡ����� Expression��������ڵĻ���
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         return Text;
      }
      #endregion
   }
}
