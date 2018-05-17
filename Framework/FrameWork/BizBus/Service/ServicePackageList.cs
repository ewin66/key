using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DrectSoft.FrameWork.BizBus.Service
{
   /// <summary>
   /// ������б�
   /// </summary>
   public class ServicePackageList:ICollection<IServicePackage>
   {
      private List<IServicePackage> packages;

      /// <summary>
      /// ���캯��
      /// </summary>
      public ServicePackageList()
      {
         packages = new List<IServicePackage>();
      }

      #region ICollection<IServicePackage> Members

      /// <summary>
      /// ��ӷ����
      /// </summary>
      /// <param name="item"></param>
      public void Add(IServicePackage item)
      {
         packages.Add(item);
      }

      /// <summary>
      /// ���������б�
      /// </summary>
      public void Clear()
      {
         packages.Clear();
      }

      /// <summary>
      /// ��ѯ�Ƿ���������
      /// </summary>
      /// <param name="item"></param>
      /// <returns></returns>
      public bool Contains(IServicePackage item)
      {
         return packages.Contains(item);
      }

      /// <summary>
      /// ���������
      /// </summary>
      /// <param name="array"></param>
      /// <param name="arrayIndex"></param>
      public void CopyTo(IServicePackage[] array, int arrayIndex)
      {
         packages.CopyTo(array, arrayIndex);
      }

      /// <summary>
      /// ��ȡ���������
      /// </summary>
      public int Count
      {
         get { return packages.Count; }
      }

      /// <summary>
      /// ��ȡ�б��Ƿ�Ϊֻ��
      /// </summary>
      public bool IsReadOnly
      {
         get { return false; }
      }

      /// <summary>
      /// �Ƴ������
      /// </summary>
      /// <param name="item"></param>
      /// <returns></returns>
      public bool Remove(IServicePackage item)
      {
         return packages.Remove(item);
      }

      #endregion

      #region IEnumerable<IServicePackage> Members

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
      public IEnumerator<IServicePackage> GetEnumerator()
      {
         return packages.GetEnumerator();
      }

      #endregion

      #region IEnumerable Members

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
         return packages.GetEnumerator();
      }

      #endregion
   }
}
