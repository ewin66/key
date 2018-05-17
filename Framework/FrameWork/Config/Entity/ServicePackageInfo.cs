using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DrectSoft.FrameWork.BizBus.Service;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace DrectSoft.FrameWork.Config
{
   /// <summary>
   /// �������Ϣ�࣬һ��һ�������������һ���ӿ�Assembly��һ��ʵ��Assembly��һ��������Ϣ�б�
   /// </summary>
   [DataContract]
   public class ServicePackageInfo
   {
      string name;
      String infAssemblyName;      //�ӿ�assembly
      String impleAssemblyName;    //ʵ��assembly
      List<ServiceInfo> services;   

      /// <summary>
      /// ���캯��
      /// </summary>
      public ServicePackageInfo()
      {         
         services = new List<ServiceInfo>();
      }

      /// <summary>
      /// ���û��ȡ���������
      /// </summary>
       [DisplayName("���������")]
       public string Name
      {
         get { return name; }
         set { name = value; }
      }

       /// <summary>
      /// �ӿ��������
       /// </summary>
       [DisplayName("�ӿ��������"), ReadOnly(true)]
       public String InfAssemblyName
      {
         get { return infAssemblyName; }
         set { infAssemblyName = value; }
      }

       /// <summary>
      /// ʵ���������
       /// </summary>
       [DisplayName("ʵ���������"), ReadOnly(true)]
       public String ImpleAssemblyName
      {
         get { return impleAssemblyName; }
         set { impleAssemblyName = value; }
      }

      /// <summary>
      /// ��ȡ����
      /// </summary>
      public List<ServiceInfo> Services
      {
         get
         {
            return services;
         }
      }
   }
}
