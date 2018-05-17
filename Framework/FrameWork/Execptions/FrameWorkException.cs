using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Execptions
{
   /// <summary>
   /// ���Exception����
   /// </summary>
   public abstract class FrameWorkException : Exception
   {
      /// <summary>
      /// ����ȼ�
      /// </summary>
      private ExceptionLevel _level= ExceptionLevel.Low;

      /// <summary>
      /// ��ʼ��һ���쳣
      /// </summary>
      public FrameWorkException()
      {
      }

      /// <summary>
      /// ��ʼ��һ���쳣
      /// </summary>
      /// <param name="message">������Ϣ</param>
      public FrameWorkException(string message)
         : base(message)
      { }

      /// <summary>
      /// ��ʼ��һ���쳣
      /// </summary>
      /// <param name="message">������Ϣ</param>
      /// <param name="exception">�ڲ��쳣</param>
      public FrameWorkException(string message, Exception exception)
         : base(message, exception)
      { }

      /// <summary>
      /// ���û��ȡ����ȼ�
      /// </summary>
      public ExceptionLevel Level
      {
         get { return _level; }
      }
   }
}
