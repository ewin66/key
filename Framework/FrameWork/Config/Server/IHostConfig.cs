using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace DrectSoft.FrameWork.Config
{
   /// <summary>
   /// ������������
   /// </summary>
   [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
   public interface IHostConfig
   {
       /// <summary>
       /// ��ȡ��������������Ϣ
       /// </summary>
       /// <returns></returns>
      [OperationContract]
      string GetConfigString();
   }
}
