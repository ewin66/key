using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ָʾ�����Ƿ���Ĭ�ϵĳ�ʼ�����ݵķ��������ɼ̳�
   /// </summary>
   [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, Inherited=false)]
   public sealed class SpecialMethodAttribute : Attribute
   {
      /// <summary>
      /// ������������
      /// </summary>
      public MethodSpecialKind Flag
      {
         get { return _flag; }
      }
      private MethodSpecialKind _flag;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="flag"></param>
      public SpecialMethodAttribute(MethodSpecialKind flag)
      {
         _flag = flag;
      }
   }

   /// <summary>
   /// ����෽���͹��캯�����������ԡ����÷��䷽ʽ���á���ȡ�����Ե�ֵʱҪ�á�
   /// </summary>
   public enum MethodSpecialKind
   { 
      /// <summary>
      /// Ĭ�ϵĹ��캯��
      /// </summary>
      DefaultCtor,
      /// <summary>
      /// ״̬��ĳ�ʼ������
      /// </summary>
      StateInitValueMethod,
      /// <summary>
      /// ״̬��ȡֵ�ķ���
      /// </summary>
      StateGetValueMethod
   }
}
