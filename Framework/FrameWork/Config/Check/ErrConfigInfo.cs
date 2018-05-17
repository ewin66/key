using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DrectSoft.FrameWork.Config
{
    /// <summary>
    /// �쳣������Ϣ
    /// </summary>
   [DataContract]
   public class ErrPackageInfo
   {
      private List<ErrServiceInfo> errServiceInfos;
      private ErrAssemblyInfo errAssemblyInfo;

       /// <summary>
       /// ctor
       /// </summary>
      public ErrPackageInfo() 
      {
         errServiceInfos = new List<ErrServiceInfo>();
         errAssemblyInfo = new ErrAssemblyInfo();
      }

       /// <summary>
       /// �쳣�����б�
       /// </summary>
      public List<ErrServiceInfo> ErrServiceInfoList
      {
         get { return errServiceInfos; }
      }

      /// <summary>
      /// �������쳣��Ϣ
      /// </summary>
      public ErrAssemblyInfo ErrAssemblyInfo
      {
         get { return errAssemblyInfo; }
         set { errAssemblyInfo = value; }
      }
   }
}
