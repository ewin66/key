using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DrectSoft.FrameWork.BizBus.Service
{
   /// <summary>
   /// ������ӿڣ����غͱ��������Ϣ
   /// ����ӷ����ͷŷ��񣬻�ȡ�������÷����ȱʡʵ�֣���ȡ�����б�ȹ���
   /// </summary>
   public interface IServicePackage
   {
      /// <summary>
      /// ��ӷ���
      /// </summary>
      /// <param name="servicedesc">��������</param>
      void AddService(ServiceDesc servicedesc);

      /// <summary>
      /// �ͷŷ���
      /// </summary>
      /// <param name="key">����ؼ���</param>
      bool ReleaseService(ServiceKey key);

      /// <summary>
      /// ��ȡ����
      /// </summary>
      /// <param name="key">����ؼ���</param>
      /// <returns></returns>
      ServiceDesc GetService(ServiceKey key);

      /// <summary>
      /// ����ȱʡ����
      /// </summary>
      /// <param name="key">����ؼ���</param>
      void SetDefaultService(ServiceKey key);

      /// <summary>
      /// ��ȡ����
      /// </summary>
      /// <param name="serviceType">��������</param>
      /// <returns></returns>
      ServiceDesc GetDefaultService(Type serviceType);

      /// <summary>
      /// ��ȡ��������
      /// </summary>
      int ServiceCount { get;}

      /// <summary>
      /// ������з���
      /// </summary>
      /// <returns></returns>
      void Clear();

      /// <summary>
      /// ���ֻ�������б�
      /// </summary>
      ReadOnlyCollection<ServiceDesc> ReadOnlyServices { get;}

      /// <summary>
      /// ��ȡֻ��ȱʡ����
      /// </summary>
      ReadOnlyCollection<ServiceKey> ReadOnlyDefaultServices { get;}

      //#region by impleType

      ///// <summary>
      ///// ��ȡ����
      ///// </summary>
      ///// <param name="impleType"></param>
      ///// <param name="serviceType"></param>
      ///// <returns></returns>
      //ServiceDesc GetService(Type impleType, Type serviceType);

      ///// <summary>
      ///// ��ӷ���
      ///// </summary>
      ///// <param name="serviceType">��������</param>
      ///// <param name="impleType">ʵ������</param>
      //void AddService(Type serviceType, Type impleType);

      ///// <summary>
      ///// ����ȱʡ����
      ///// </summary>
      ///// <param name="serviceType"></param>
      ///// <param name="impleType"></param>
      //void SetDefaultService(Type serviceType, Type impleType);

      //#endregion

      ///// <summary>
      ///// ��ȡ�����б�
      ///// </summary>
      
   }
}