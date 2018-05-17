using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ҽ���༭״̬ö��
   /// </summary>
   [Flags]
   public enum OrderEditState
   {
      /// <summary>
      /// �ѱ��ӵ�ҽ���б��У���δ�浽���ݿ�
      /// </summary>
      Added = 4, 
      /// <summary>
      /// �Ѵ�ҽ���б���ɾ������δ�浽���ݿ�
      /// </summary>
      Deleted = 8, 
      /// <summary>
      /// ҽ���Ѵ��������������κ�ҽ������
      /// </summary>
      Detached = 1, 
      /// <summary>
      /// �ѱ��޸ģ���δ�浽���ݿ�
      /// </summary>
      Modified = 16, 
      /// <summary>
      /// δ���κ��޸�
      /// </summary>
      Unchanged = 2 
   }

   /// <summary>
   /// ����������ͱ�־(�����������)
   /// </summary>
   public enum OrderOutputTextType
   {
      /// <summary>
      /// ��ͨ�ı�
      /// </summary>
      NormalText = 99, 
      /// <summary>
      /// ��Ŀ����(�����)
      /// </summary>
      ItemName = 0, 
      /// <summary>
      /// ��Ŀ����(����λ)
      /// </summary>
      ItemAmount = 1, 
      /// <summary>
      /// ��Ŀ�÷�����
      /// </summary>
      ItemUsage = 2, 
      /// <summary>
      /// ��ĿƵ������
      /// </summary>
      ItemFrequency = 3, 
      /// <summary>
      /// ��Ŀ����
      /// </summary>
      ItemDays = 4, 
      /// <summary>
      /// ��������
      /// </summary>
      EntrustContent = 5, 
      /// <summary>
      /// ȡ����Ϣ(��ȡ��������)
      /// </summary>
      CancelInfo = 6, 
      /// <summary>
      /// ���鿪ʼ���
      /// </summary>
      GroupStart = 7, 
      /// <summary>
      /// �����м���
      /// </summary>
      GroupMiddle = 8, 
      /// <summary>
      /// ����������
      /// </summary>
      GroupEnd = 9,
      /// <summary>
      /// �Ա�
      /// </summary>
      SelfProvide = 10
   }
}
