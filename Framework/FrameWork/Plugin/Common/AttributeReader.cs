using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DrectSoft.FrameWork.Plugin
{
   /// <summary>
   /// ��ȡ����
   /// </summary>
   public class AttributesReader
   {
      private Assembly _assembly;

      /// <summary>
      /// ���캯��
      /// </summary>
      /// <param name="assemblyName">��������</param>
      public AttributesReader(string assemblyName)
      {
         try
         {
            _assembly = Assembly.LoadFrom(assemblyName);
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// ��ָ��Ӧ������صĹ��캯��
      /// </summary>
      /// <param name="domain">Ӧ�ó�����</param>
      /// <param name="assemblyName">��������</param>
      public AttributesReader(AppDomain domain, string assemblyName)
      {
         try
         {
            string assemblyNameWithExt = System.IO.Path.GetFileNameWithoutExtension(assemblyName);
            if (domain != null)
               _assembly = domain.Load(assemblyNameWithExt);
            else
               _assembly = AppDomain.CurrentDomain.Load(assemblyNameWithExt);
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// ���캯��
      /// </summary>
      /// <param name="assembly">����</param>
      public AttributesReader(Assembly assembly)
      {
         _assembly = assembly;
      }

      /// <summary>
      /// ȡ�ò����������Ϣ
      /// </summary>
      /// <returns>�������������</returns>
      public PluginAttribute[] GetPlugInMenuInfoAttribute()
      {
         if (_assembly != null)
         {
            try
            {
               return (PluginAttribute[])_assembly.GetCustomAttributes(typeof(PluginAttribute), true);
            }
            catch (Exception e)
            {
               throw e;
            }
         }
         else
         {
            return null;
         }
      }
   }
}
