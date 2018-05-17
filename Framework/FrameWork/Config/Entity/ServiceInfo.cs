using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DrectSoft.FrameWork.Config
{
   /// <summary>
   /// ������Ϣ��
   /// </summary>
   public class ServiceInfo
   {
      String serviceTypeName = "";    //������������
      String key = "";                //����ؼ���
      String impleTypeName = "";      //ʵ����������
      bool isdefault=false;

      /// <summary>
      /// ������������
      /// </summary>
       [DisplayName("������������"), ReadOnly(true)]
      public String ServiceTypeName
      {
         get { return serviceTypeName; }
         set { serviceTypeName = value; }
      }

      /// <summary>
      /// ����ؼ���
      /// </summary>
       [DisplayName("����ؼ���")]
      public String Key
      {
         get { return key; }
         set { key = value; }
      }

      /// <summary>
      /// ����ʵ������
      /// </summary>
       [DisplayName("����ʵ������"), ReadOnly(true)]
       public String ImpleTypeName
      {
         get { return impleTypeName; }
         set { impleTypeName = value; }
      }

      /// <summary>
      /// ���û��ȡ�Ƿ�Ϊȱʡ����
      /// </summary>
       [DisplayName("�Ƿ�Ϊȱʡ����")]
       public bool IsDefault
      {
         get { return isdefault; }
         set { isdefault = value; }
      }

      /// <summary>
      /// �ж����
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         if (obj == null || (!(obj is ServiceInfo)))
            return false;

         ServiceInfo info = obj as ServiceInfo;
         if ((this.serviceTypeName != info.serviceTypeName) ||
            (this.impleTypeName != info.impleTypeName) ||
            (this.key != info.key))
            return false;

         if (isdefault != info.isdefault)
            return false;

         return true;
      }

      /// <summary>
      /// ��ȡ��ϣ��
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return this.serviceTypeName.GetHashCode()
            + this.impleTypeName.GetHashCode()
            + this.key.GetHashCode();
      }
   }
}