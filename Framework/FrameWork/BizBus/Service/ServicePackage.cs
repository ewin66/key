using DrectSoft.FrameWork.Config.Check;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DrectSoft.FrameWork.BizBus.Service
{
    /// <summary>
    /// �����
    /// </summary>
    public class ServicePackage : IServicePackage
    {
        private Dictionary<ServiceKey, ServiceDesc> serviceDic;     //�����б�
        private Dictionary<Type, ServiceKey> defaultService;        //ȱʡ�����б�

        /// <summary>
        /// ��ʼ��ҵ������
        /// </summary>
        public ServicePackage()
        {
            serviceDic = new Dictionary<ServiceKey, ServiceDesc>();
            defaultService = new Dictionary<Type, ServiceKey>();
        }

        #region IServiceManager Members

        /// <summary>
        /// ��ӷ���
        /// </summary>
        /// <param name="desc"></param>
        public void AddService(ServiceDesc desc)
        {
            ConfigValidator.ValidateServiceDesc(desc);

            ServiceKey servicekey = new ServiceKey(desc);
            serviceDic.Add(servicekey, desc);
        }

        /// <summary>
        /// �ͷŷ���
        /// </summary>
        /// <param name="key">����ؼ���</param>
        public bool ReleaseService(ServiceKey key)
        {
            if (!serviceDic.Remove(key))
                return false;
            if (defaultService.ContainsValue(key))
                defaultService.Remove(key.ServiceType);

            return true;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="key">�ؼ���</param>
        /// <returns>��������</returns>
        public ServiceDesc GetService(ServiceKey key)
        {
            if (!serviceDic.ContainsKey(key))
                return null;
            return serviceDic[key];
        }

        /// <summary>
        /// ����ȱʡ����
        /// </summary>
        /// <param name="key"></param>
        public void SetDefaultService(ServiceKey key)
        {
            if (!serviceDic.ContainsKey(key))
                throw new ArgumentException("û�и÷�������");
            if (!defaultService.ContainsKey(key.ServiceType))
                defaultService.Add(key.ServiceType, key);
            else
                defaultService[key.ServiceType] = key;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public ServiceDesc GetDefaultService(Type serviceType)
        {
            if (defaultService.ContainsKey(serviceType))
                return serviceDic[defaultService[serviceType]];
            return null;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public int ServiceCount
        {
            get { return this.serviceDic.Count; }
        }

        /// <summary>
        /// ������з���
        /// </summary>
        public void Clear()
        {
            this.serviceDic.Clear();
        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        public ReadOnlyCollection<ServiceDesc> ReadOnlyServices
        {
            get
            {
                ServiceDesc[] sds = new ServiceDesc[serviceDic.Values.Count];
                serviceDic.Values.CopyTo(sds, 0);
                return new ReadOnlyCollection<ServiceDesc>(sds);
            }
        }

        /// <summary>
        /// ��ȡֻ����Ĭ�Ϸ��񼯺�
        /// </summary>
        public ReadOnlyCollection<ServiceKey> ReadOnlyDefaultServices
        {
            get
            {
                ServiceKey[] keys = new ServiceKey[defaultService.Values.Count];
                defaultService.Values.CopyTo(keys, 0);
                return new ReadOnlyCollection<ServiceKey>(keys);
            }
        }

        #endregion

        //#region IServiceManager Members �ⲿʵ�ְ���ʵ�����ͱ�Ƿ���

        //ServiceDesc IServicePackage.GetService(Type impleType, Type serviceType)
        //{
        //   return GetService(new ServiceKey(serviceType,impleType.FullName));
        //}

        //void IServicePackage.AddService(Type serviceType, Type impleType)
        //{
        //   String key = impleType.FullName;
        //   AddService(new ServiceDesc(serviceType,key,impleType,false));
        //}

        //void IServicePackage.SetDefaultService(Type serviceType, Type impleType)
        //{
        //   if (!serviceDic.ContainsKey(new ServiceKey(serviceType, impleType.FullName)))
        //      throw new ArgumentException("û�и÷�������");
        //   if (!defaultService.ContainsKey(serviceType))
        //      defaultService.Add(serviceType, new ServiceKey(serviceType, impleType.FullName));
        //   else
        //      defaultService[serviceType] = new ServiceKey(serviceType, impleType.FullName);
        //}

        //#endregion

    }
}