using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Config
{
    /// <summary>
    /// �쳣����
    /// </summary>
   public class ErrMessages
   {
       /// <summary>
       /// �����쳣��Ϣ
       /// </summary>
       /// <param name="info"></param>
       /// <returns></returns>
      public static string ErrAssemblyMessage(ErrAssemblyInfo info)
      {
         if (!info.HasAssembly)
            return "����δ���ҵ�";

         if (!info.AssemblyHasAttribute)
            return "����δ��ע����";

         return "";
      }
   }
}
