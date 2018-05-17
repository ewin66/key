using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DrectSoft.FrameWork.BizBus.Service
{
   /// <summary>
   /// ����Ƿ�Exception
   /// </summary>
   [Serializable]
   public class ServiceInvalidException:ApplicationException
   {
      string m_servicename;

       /// <summary>
      /// ����Ƿ�Exception
       /// </summary>
       /// <param name="info"></param>
       /// <param name="context"></param>
       public ServiceInvalidException(SerializationInfo info, StreamingContext context)
           : base(info, context)
       {
       }

      /// <summary>
      /// �����쳣
      /// </summary>
      /// <param name="message"></param>
      public ServiceInvalidException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// ��ʼ������Ƿ�Exception
      /// </summary>
      /// <param name="message">������Ϣ</param>
      /// <param name="servicename">��������</param>
      public ServiceInvalidException(string servicename, string message)
         : base(message)
      {
         this.m_servicename = servicename;
      }

      /// <summary>
      /// ��ȡ������Ϣ
      /// </summary>
      public override string Message
      {
         get
         {
            string message = base.Message;
            if (!string.IsNullOrEmpty(m_servicename))
            {               
               return (message + Environment.NewLine +  this.m_servicename );
            }
            return message;

         }
      }

      /// <summary>
      /// ��ȡ��������
      /// </summary>
      public virtual string ServiceName
      {
         get { return m_servicename; }
      }
   }
}
