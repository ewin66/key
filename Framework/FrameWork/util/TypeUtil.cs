using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork
{
    /// <summary>
    /// ���͸�����
    /// </summary>
   public class TypeUtil
   {
      /// <summary>
      /// ��֤���ͼ̳й�ϵ
      /// </summary>
      /// <param name="type">������</param>
      /// <param name="baseType">������</param>
      /// <returns></returns>
      public static bool ConfirmInherit(Type type, Type baseType)
      {         
         if (type == null)
            throw new ArgumentNullException("����������Ϊ��");
         if (baseType == null)
            throw new ArgumentNullException("���븸����Ϊ��");

         //����Ϊ�ӿڣ�ֻ��Ҫ��ȡ������֤
         if(baseType.IsInterface)
         {
            Type[] types = type.GetInterfaces();
            foreach (Type t in types)
            {
               if (t.FullName == baseType.FullName)
                  return true;
            }
            return false;
         }

         //���ǽӿ���һ��������
         Type btype = type;
         while (btype!=null)
         {
            btype = btype.BaseType;
            Console.Write(btype.ToString());
            if (baseType==btype)
               return true;
         }
         
         return false;
      }         
   }
}
