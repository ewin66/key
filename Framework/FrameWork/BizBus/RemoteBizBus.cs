using DrectSoft.FrameWork.Config;
using DrectSoft.FrameWork.ObjectBuilder;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DrectSoft.FrameWork.BizBus
{
    /// <summary>
    /// Զ��ҵ������
    /// </summary>
    public class RemoteBizBus : IBizBus
    {
        internal List<ServicePackageInfo> packages = new List<ServicePackageInfo>();
        DrectLocator locator = new DrectLocator();
        //internal ServicePackageList packages = new ServicePackageList();      //�����

        private string getUri(Type t, string key)
        {
            string baseuri = AppSettingConfig.getRemoteUriPath();
            if (string.IsNullOrEmpty(baseuri))
                return "";
            string typename = t.FullName;
            foreach (ServicePackageInfo package in packages)
            {
                foreach (ServiceInfo si in package.Services)
                {
                    if (si.ServiceTypeName == typename && si.Key == key)
                    {
                        return UriUtil.GetServiceUri(baseuri, package.Name, key);
                    }
                }
            }
            return "";
        }

        private string getdefault(Type t)
        {
            string typename = t.FullName;
            foreach (ServicePackageInfo package in packages)
            {
                foreach (ServiceInfo si in package.Services)
                {
                    if (typename == si.ServiceTypeName && si.IsDefault)
                        return si.Key;
                }
            }
            return "";
        }

        #region IBizBus Members

        /// <summary>
        /// ����ָ��ҵ��
        /// </summary>
        /// <typeparam name="T">ҵ���������</typeparam>
        /// <param name="servicekey">��������</param>
        /// <param name="parameters">ָ������</param>
        /// <returns></returns>
        public T BuildUp<T>(string servicekey, params object[] parameters)
        {
            string uri = getUri(typeof(T), servicekey);
            BasicHttpBinding bhb = new BasicHttpBinding("BasicHttpBinding_Service");
            EndpointAddress epa = new EndpointAddress(uri);
            return ChannelFactory<T>.CreateChannel(bhb, epa);
        }

        /// <summary>
        /// ����ָ��ҵ��
        /// </summary>
        /// <typeparam name="T">ҵ���������</typeparam>
        /// <param name="parameters">ָ������</param>
        /// <returns></returns>
        public T BuildUp<T>(params object[] parameters)
        {
            string key = getdefault(typeof(T));
            return BuildUp<T>(key);
        }

        /// <summary>
        /// ����ָ��ҵ����󲢻���
        /// </summary>
        /// <typeparam name="T">ҵ���������</typeparam>
        /// <param name="servicekey">��������</param>
        /// <param name="parameters">����</param>
        /// <returns></returns>
        public T BuildUpAndSaveObject<T>(string servicekey, params object[] parameters)
        {
            T result = this.BuildUp<T>(servicekey);
            DependencyResolutionLocatorKey locatorkey = new DependencyResolutionLocatorKey(typeof(T), servicekey);
            locator.Add(locatorkey, result);
            return result;
        }

        /// <summary>
        /// ����ָ��ҵ����󲢻���
        /// </summary>
        /// <typeparam name="T">ҵ���������</typeparam>
        /// <param name="parameters">����</param>
        /// <returns></returns>
        public T BuildUpAndSaveObject<T>(params object[] parameters)
        {
            T result = this.BuildUp<T>();
            string key = getdefault(typeof(T));
            DependencyResolutionLocatorKey locatorkey = new DependencyResolutionLocatorKey(typeof(T), key);
            locator.Add(locatorkey, result);
            return result;
        }

        /// <summary>
        /// ��λָ���������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="servicekey"></param>
        /// <returns></returns>
        public T Locate<T>(string servicekey)
        {
            DependencyResolutionLocatorKey locatorkey = new DependencyResolutionLocatorKey(typeof(T), servicekey);
            return locator.Get<T>(locatorkey);
        }

        /// <summary>
        /// ��λָ���������(��ָ������)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Locate<T>()
        {
            string key = getdefault(typeof(T));
            DependencyResolutionLocatorKey locatorkey = new DependencyResolutionLocatorKey(typeof(T), key);
            return locator.Get<T>(locatorkey);
        }

        /// <summary>
        /// ����ҵ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void SaveObject<T>(string key, T obj)
        {
            DependencyResolutionLocatorKey dkey = new DependencyResolutionLocatorKey(typeof(T), key);
            locator.Add(dkey, obj);
        }

        //public T LoadObject<T>(string key)
        //{
        //   DependencyResolutionLocatorKey dkey = new DependencyResolutionLocatorKey(typeof(T), key);
        //   return locator.Get<T>(dkey);
        //}

        /// <summary>
        /// ����ҵ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void UpdateObject<T>(string key, T obj)
        {
            DependencyResolutionLocatorKey dkey = new DependencyResolutionLocatorKey(typeof(T), key);
            if (locator.Contains(dkey)) locator.Remove(dkey);
            locator.Add(dkey, obj);
        }

        #endregion
    }
}
