using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DrectSoft.FrameWork.Config
{
   /// <summary>
   /// ���򼯰�����
   /// </summary>
   public class AssemblyHelper
   {
      /// <summary>
      /// װ�س���
      /// </summary>
       /// <param name="assemblypath"></param>
      /// <returns></returns>
      public static Assembly LoadAssembly(string assemblypath)
      {
         string name = GetAssemblyName(assemblypath);

         Assembly impleAssembly;
         try
         {
            impleAssembly = AppDomain.CurrentDomain.Load(name);
         }
         catch
         {
            impleAssembly = CheckConfig.GetAssembly(ConfigUtil.GetBizPluginFullPath(assemblypath));
         }

         return impleAssembly;
      }

      private static string GetAssemblyName(string assemblypath)
      {
         //��ȡdll����
         string[] strs = assemblypath.Split('\\');
         string names = (strs[strs.Length - 1]);

         //��ȡAssemblyName
         string[] arrname = (names.Split('.'));
         int i = 1;
         string name = arrname[0];
         while (i < arrname.Length - 1)
         {
            name += "." + arrname[i];
            i++;
         }
         return name;
      }
   }
}
