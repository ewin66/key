using DrectSoft.FrameWork.BizBus.Service;
using DrectSoft.FrameWork.ObjectBuilder;
using Microsoft.Practices.ObjectBuilder;

namespace DrectSoft.FrameWork.BizBus
{
    /// <summary>
    /// ҵ�����ߣ�����������е�
    /// </summary>
    public class ClientBizBus : IBizBus
    {
        private DrectSoft.FrameWork.ObjectBuilder.Builder builder;             //������
        internal ServicePackageList packages = new ServicePackageList();      //�����
        private DrectLocator locator;                                      //��λ��

        //Helper
        private PolicyListBuilder policylistbuilder = new PolicyListBuilder();
        private PackageListSearcher packagesearcher = new PackageListSearcher();

        /// <summary>
        /// ���캯��
        /// </summary>
        internal ClientBizBus()
        {
            InitializeBuilder();
        }

        private void InitializeBuilder()
        {
            builder = new DrectSoft.FrameWork.ObjectBuilder.Builder();
            locator = new DrectLocator();
            LifetimeContainer lifetime = new LifetimeContainer();
            locator.Add(typeof(ILifetimeContainer), lifetime);
        }

        #region IBizBus Members

        /// <summary>
        /// ��������T�����ݷ���ؼ��ִ���
        /// </summary>
        /// <typeparam name="T">�贴��������</typeparam>
        /// <param name="key">����ؼ���</param>
        /// <param name="parameters">����</param>
        /// <returns></returns>
        public T BuildUp<T>(string key, params object[] parameters)
        {
            ServiceKey servicekey = new ServiceKey(typeof(T), key);
            ServiceDesc desc = this.packagesearcher.SearchPackage(servicekey);
            return InternalBuildUp<T>(desc, false, parameters);
        }

        /// <summary>
        /// ͨ��ȱʡ����ʵ�֣�����ȱʡ���񣬷��ؿ�
        /// </summary>
        /// <typeparam name="T">����������</typeparam>
        /// <returns></returns>
        public T BuildUp<T>(params object[] parameters)
        {
            ServiceDesc desc = this.packagesearcher.SearchDefault(typeof(T));
            if (desc == null)
                return default(T);
            return InternalBuildUp<T>(desc, false, parameters);
        }

        /// <summary>
        /// �������񣬱��������������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T BuildUpAndSaveObject<T>(string key, params object[] parameters)
        {
            ServiceKey servicekey = new ServiceKey(typeof(T), key);
            ServiceDesc desc = this.packagesearcher.SearchPackage(servicekey);
            return InternalBuildUp<T>(desc, true, parameters);
        }

        /// <summary>
        /// �������񣬱��������������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T BuildUpAndSaveObject<T>(params object[] parameters)
        {
            ServiceDesc desc = this.packagesearcher.SearchDefault(typeof(T));
            if (desc == null)
                return default(T);
            return InternalBuildUp<T>(desc, true, parameters);
        }

        /// <summary>
        /// ��λ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Locate<T>(string id)
        {
            DependencyResolutionLocatorKey locatorkey = new DependencyResolutionLocatorKey(typeof(T), id);
            return locator.Get<T>(locatorkey);
        }

        /// <summary>
        /// ��λ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Locate<T>()
        {
            ServiceDesc desc = packagesearcher.SearchDefault(typeof(T));
            if (desc == null)
                return default(T);
            ServiceKey key = new ServiceKey(desc);
            //���춨λ���ؼ���
            DependencyResolutionLocatorKey locatorkey = new DependencyResolutionLocatorKey(desc.ImpleType, key.ID);
            if (this.locator.Contains(locatorkey))
                return this.locator.Get<T>(locatorkey);
            return default(T);
        }

        private T InternalBuildUp<T>(ServiceDesc desc, bool issave, params object[] parameters)
        {
            PolicyList[] pls = policylistbuilder.BuildPolicy(desc, issave, parameters);
            if (issave)
                return builder.BuildUp<T>(locator, desc.Key, null, pls);
            else
                return builder.BuildUp<T>(null, desc.Key, null, pls);
        }

        /// <summary>
        /// �Է�����б��������
        /// </summary>
        internal void IndexPackage()
        {
            packagesearcher.IndexPackageList(this.packages);
        }

        /// <summary>
        /// /����ָ��ҵ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void SaveObject<T>(string key, T obj)
        {
            DependencyResolutionLocatorKey dkey = new DependencyResolutionLocatorKey(typeof(T), key);
            locator.Add(dkey, obj);
        }

        /// <summary>
        /// ����ָ��ҵ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T LoadObject<T>(string key)
        {
            DependencyResolutionLocatorKey dkey = new DependencyResolutionLocatorKey(typeof(T), key);
            return locator.Get<T>(dkey);
        }

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