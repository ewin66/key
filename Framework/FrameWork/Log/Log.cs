using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Log
{
   /// <summary>
   /// ��־
   /// </summary>
   public class Log:ILog
   {
      /// <summary>
      /// д��־
      /// </summary>
      /// <param name="message"></param>
      public void Write(string message)
      {
         Console.WriteLine("Log: "+message);
      }
   }

   /// <summary>
   /// ��־����������ILog�ӿ�
   /// </summary>
   public class LogFactory
   {
      /// <summary>
      /// ����ILog�ӿ�
      /// </summary>
      /// <returns></returns>
      public static ILog Create()
      {
         return new Log();
      }
   }
}
