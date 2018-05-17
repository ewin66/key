using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.FrameWork.BizBus.Service;

namespace DrectSoft.FrameWork.BizBus {
    /// <summary>
    /// ҵ�����߽ӿ�
    /// </summary>
    public interface IBizBus {
        /// <summary>
        /// �������󣬰��շ���ؼ��ֹ���
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="servicekey">�ؼ���</param>
        /// <param name="parameters">����</param>
        /// <returns></returns>
        T BuildUp<T>(string servicekey, params object[] parameters);

        ///// <summary>
        ///// ��������
        ///// </summary>
        ///// <typeparam name="T">��������</typeparam>
        ///// <param name="type">ʵ������</param>
        ///// <returns></returns>
        //T BuildUp<T>(Type impletype, params object[] parameters);

        /// <summary>
        /// ��������,Ĭ�Ϲ���ȱʡ����
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <returns></returns>
        T BuildUp<T>(params object[] parameters);

        /// <summary>
        /// ��������
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="servicekey">�ؼ���</param>
        /// <param name="parameters">����</param>
        /// <returns></returns>
        T BuildUpAndSaveObject<T>(string servicekey, params object[] parameters);

        ///// <summary>
        ///// ��������
        ///// </summary>
        ///// <typeparam name="T">��������</typeparam>
        ///// <param name="type">ʵ������</param>
        ///// <returns></returns>
        //T BuildUpAndSaveObject<T>(Type impletype,  params object[] parameters);

        /// <summary>
        /// ��������,Ĭ�Ϲ���ȱʡ����
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <returns></returns>
        T BuildUpAndSaveObject<T>(params object[] parameters);

        /// <summary>
        /// ��λ����(ָ��ҵ�����)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="servicekey"></param>
        /// <returns></returns>
        T Locate<T>(string servicekey);

        /// <summary>
        /// ��λ����(Ĭ��ҵ�����)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Locate<T>();

        /// <summary>
        /// ����������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        void SaveObject<T>(string key, T obj);

        /// <summary>
        /// ���»���������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        void UpdateObject<T>(string key, T obj);
    }
}