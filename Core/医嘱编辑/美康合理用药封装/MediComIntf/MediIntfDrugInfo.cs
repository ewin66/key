using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Common.MediComIntf
{
   /// <summary>
   /// �����ӿ�ҩƷ��Ϣ��
   /// </summary>
   public struct MediIntfDrugInfo
   {
      /// <summary>
      ///ҽ��Ψһ�룬Ҫ����Ψһ���ҽ����¼
      /// </summary>
      public string OrderUniqueCode;
      /// <summary>
      ///���봫��ҽ���е�ҩƷΨһ��
      /// </summary>
      public string DrugCode;
      /// <summary>
      ///���봫��ҽ���е�ҩƷ����
      /// </summary>
      public string DrugName;
      /// <summary>
      ///��������ʾÿ��ʹ�ü��������ֲ���
      /// </summary>
      public string SingleDose;
      /// <summary>
      ///������λ����ʾÿ�η��ü�����λ��Ҫ����ҩƷ��Լ�����λ��ȫһ�µ�λ��ȫһ�£����������ɼ�����鲻��ȷ��
      /// </summary>
      public string DoseUnit;
      /// <summary>
      ///Ƶ�� ����ʾҩƷ����Ƶ����Ϣ������Ҫ��n��m�Σ���"m/n"�����磺1��3�Σ���"3/1"��7��2�Σ���"2/7"��
      /// </summary>
      public string Frequency;
      /// <summary>
      ///�������ڣ���ʾ����ҽ�����ڡ���ʽΪ"yyyy-mm-dd"�����翪������Ϊ1999��3��12�գ���Ӧ����"1999-3-12"������ʱ��ϵͳĬ��Ϊ��ǰ���ڡ�
      /// </summary>
      public string StartDate;
      /// <summary>
      ///ͣ������ ��ʾͣ�����ڣ�����δͣҽ����Ӧ��Ϊ�������ڡ���ʽΪ"yyyy-mm-dd"������ͣ������Ϊ1999��3��12�գ���Ӧ����"1999-3-12"������ʱ��ϵͳĬ��Ϊ��ǰ���ڡ�
      /// </summary>
      public string EndDate;
      /// <summary>
      ///���봫����ʾ��ҩ;�����ƣ�����"�ڷ�"��"����"�ȡ�ע�⣬����PASSϵͳ������÷���ϵ���У��˲���������󣬽�ֱ�ӵ���������
      /// </summary>
      public string RouteName;
      /// <summary>
      ///������ҽ����ʾֵ(�ɲ�����ֵ����Ҫ���ڷ�����龯ʾֵ)
      /// </summary>
      public PassWarnType Warn;
      /// <summary>
      ///����ҽ����ǣ���ͬ������Ϊ��һ��ҽ��
      /// </summary>
      public string GroupTag;
      /// <summary>
      ///ҽ������1 ��ʱ 0 ���� 
      /// </summary>
      public string OrderType;
      /// <summary>
      ///ҽ��ҽ��Id
      /// </summary>
      public string OrderDoctorId;
      /// <summary>
      /// ҽ��ҽ������
      /// </summary>
      public string OrderDoctorName;
   }
}
