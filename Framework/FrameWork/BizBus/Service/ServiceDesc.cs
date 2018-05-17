using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.BizBus.Service
{
   /// <summary>
   /// ��������
   /// </summary>
   public class ServiceDesc
   {
      Type serviceType;    //��������
      string key;          //����ؼ���
      Type impleType;      //ʵ������
      bool isDefault;      //�Ƿ�Ϊȱʡ����


      /// <summary>
      /// ���캯��
      /// </summary>
      /// <param name="servicetype">��������</param>
      /// <param name="key">����ؼ���</param>
      /// <param name="impletype">����ʵ������</param>
       /// <param name="isdefault"></param>
      public ServiceDesc(Type servicetype, string key, Type impletype,bool isdefault)
      {
         this.serviceType = servicetype;
         this.key = key;
         this.impleType = impletype;
         this.isDefault = isdefault;
      }

      /// <summary>
      /// ���û��ȡ��������
      /// </summary>
      public Type ServiceType
      {
         get { return serviceType; }
         set { serviceType = value; }
      }

      /// <summary>
      /// ���û��ȡ����ؼ���
      /// </summary>
      public string Key
      {
         get { return key; }
         set { key = value; }
      }

      /// <summary>
      /// ���û��ȡ����ʵ������
      /// </summary>
      public Type ImpleType
      {
         get { return impleType; }
         set { impleType = value; }
      }
   }
}
