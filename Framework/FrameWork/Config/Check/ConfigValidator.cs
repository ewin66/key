using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.FrameWork.BizBus.Service;

namespace DrectSoft.FrameWork.Config.Check
{
    /// <summary>
    /// ����У����
    /// </summary>
   public class ConfigValidator
   {

       /// <summary>
       /// У�����
       /// </summary>
       /// <param name="desc"></param>
      public static void ValidateServiceDesc(ServiceDesc desc)
      {
         if (desc == null)
            throw new ArgumentNullException("�����������Ϊ��");

         if (desc.ServiceType == null)
            throw new ArgumentNullException("serviceType", "��������Ϊ��");
         if (desc.ImpleType == null)
            throw new ArgumentNullException("impleType", "ʵ������Ϊ��");

         if (!TypeUtil.ConfirmInherit(desc.ServiceType, typeof(IService)))
            throw new ServiceInvalidException(desc.ServiceType.ToString(), "δʵ�ַ���ӿ�");

         if (!TypeUtil.ConfirmInherit(desc.ImpleType, desc.ServiceType))
            throw new ServiceInvalidException(desc.ServiceType.ToString(), "ʵ������δʵ�ַ���ӿ�");
      }
   }
}
