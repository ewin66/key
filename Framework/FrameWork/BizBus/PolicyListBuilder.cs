using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using DrectSoft.FrameWork.BizBus.Service;

namespace DrectSoft.FrameWork.BizBus
{
   /// <summary>
   /// ���Թ�����
   /// </summary>
   internal class PolicyListBuilder
   {
      public PolicyListBuilder()
      { }

      /// <summary>
      /// ��������
      /// </summary>
      /// <param name="servicedesc">��������</param>
      /// <param name="issave">�Ƿ񱣴����</param>
      /// <param name="parameters">���������</param>
      /// <returns></returns>
      public PolicyList[] BuildPolicy(ServiceDesc servicedesc, bool issave, object[] parameters)
      {
         PolicyList[] policies = new PolicyList[1];
         PolicyList policylist = new PolicyList();
         policies[0] = policylist;

         //����Ӱ�����
         TypeMappingPolicy typepolicy = new TypeMappingPolicy(servicedesc.ImpleType, servicedesc.Key);
         policylist.Set<ITypeMappingPolicy>(typepolicy, servicedesc.ServiceType, servicedesc.Key);

         //������һ�������ԣ�ֻ�д����˵�һ�������Բ��ܹ�Locate��
         if (issave)
         {
            SingletonPolicy singletonpolicy = new SingletonPolicy(true);
            policylist.Set<ISingletonPolicy>(singletonpolicy, servicedesc.ImpleType, servicedesc.Key);
         }

         //�����������ԣ�������Ӳ���
         if (parameters != null && parameters.Length != 0)
         {
            ConstructorPolicy constuctorpolicy = new ConstructorPolicy();
            for (int i = 0; i < parameters.Length; i++)
            {
               constuctorpolicy.AddParameter(new ValueParameter(parameters[i].GetType(), parameters[i]));
            }
            policylist.Set<ICreationPolicy>(constuctorpolicy, servicedesc.ImpleType, servicedesc.Key);
         }
         return policies;
      }
   }
}
