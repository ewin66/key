using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Common.MediComIntf
{
   /// <summary>
   /// �����ӿڲ�����Ϣ��
   /// </summary>
   public struct MediIntfPatientInfo
   {
      /// <summary>
      ///����ID
      /// </summary>
      public string PatientID;                 
      /// <summary>
      ///סԺ����,�����''����ϵͳ��Ϊ��'1'
      /// </summary>
      public string VisitID;                     
      /// <summary>
      ///��������
      /// </summary>
      public string PatientName;            
      /// <summary>
      ///�����Ա�,��ʽΪ"��"��"Ů"��"����"�ȡ�
      /// </summary>
      public string Sex;                          
      /// <summary>
      ///���˳�������,��ʽΪ"yyyy-mm-dd"
      /// </summary>
      public string Birthday;                   
      /// <summary>
      ///����,��ʾ�����Թ���Ϊ��λ������ֵ
      /// </summary>
      public string Weight;                     
      /// <summary>
      ///���,��ʾ����������Ϊ��λ�����ֵ
      /// </summary>
      public string Height;                      
      /// <summary>
      ///��������,��ʾ���˵�ǰ���ڿ��һ�������
      /// </summary>
      public string DeptName;                 
      /// <summary>
      ///ҽ������
      /// </summary>
      public string Doctor;                      
      /// <summary>
      ///�������� "0"סԺ,"1"���"2"�������Ļ�����
      /// </summary>
      public string PatientType;              
      /// <summary>
      ///��ʾ������ҩ������ڣ���ʽΪ"yyyy-mm-dd"������ʱ��ϵͳĬ��Ϊ��ǰ����
      /// </summary>
      public string UseDate;                   
   }
}
