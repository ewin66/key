using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Config
{
    /// <summary>
    /// �����쳣��Ϣ
    /// </summary>
   public class ErrServiceInfo
   {
      string servicekey;
      int errCode;

       /// <summary>
       /// ��������
       /// </summary>
      public string ServiceKey
      {
         get { return servicekey; }
         set { servicekey = value; }
      }

       /// <summary>
       /// �������
       /// </summary>
      public int ErrCode
      {
         get { return errCode; }
         set { errCode = value; }
      }
   }
}
