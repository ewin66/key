using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.FrameWork.BizBus.Service;

namespace DrectSoft.FrameWork.BizBus
{
   internal class PackageListSearcher
   {
      Dictionary<ServiceKey, ServiceDesc> dicPackage;
      Dictionary<Type, ServiceDesc> dicDefault;

      /// <summary>
      /// ������б�����
      /// </summary>
      public PackageListSearcher()
      {
         dicPackage = new Dictionary<ServiceKey, ServiceDesc>();
         dicDefault = new Dictionary<Type, ServiceDesc>();         
      }

      /// <summary>
      /// ���ҷ����,���ݹؼ��ֲ���
      /// </summary>
      /// <param name="key">����ؼ���</param>
      /// <returns></returns>
      public ServiceDesc SearchPackage(ServiceKey key)
      {
         if (dicPackage.ContainsKey(key))
            return dicPackage[key];
         return null;
      }

      /// <summary>
      /// ����ȱʡ����
      /// </summary>
      /// <param name="type">��������</param>
      /// <returns></returns>
      public ServiceDesc SearchDefault(Type type)
      {
         if (dicDefault.ContainsKey(type))
            return dicDefault[type];
         return null;
      }

      /// <summary>
      /// ��������б�
      /// </summary>
      /// <param name="packages"></param>
      public void IndexPackageList(ServicePackageList packages)
      {
         dicPackage.Clear();
         dicDefault.Clear();

         foreach (IServicePackage package in packages)
         {
            foreach (ServiceDesc sd in package.ReadOnlyServices)
            {
               ServiceKey key = new ServiceKey(sd);
               dicPackage.Add(key, sd);
            }
            foreach (ServiceKey key in package.ReadOnlyDefaultServices)
            {
               dicDefault.Add(key.ServiceType, dicPackage[key]);
            }
         }
      }
   }
}
