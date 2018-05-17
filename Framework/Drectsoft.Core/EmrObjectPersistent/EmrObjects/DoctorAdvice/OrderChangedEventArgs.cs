using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Common.Eop
{
   ///// <summary>
   ///// ҽ���ı��¼�ί��
   ///// </summary>
   ///// <param name="sender"></param>
   ///// <param name="e"></param>
   //public delegate void OrderChangedEventHandler(object sender, OrderChangedEventArgs e);

   /// <summary>
   /// �ṩҽ���ı��¼�(OrderChanged)�Ĳ���
   /// </summary>
   public class OrderChangedEventArgs : EventArgs
   {
      /// <summary>
      /// ���º��ҽ����Ӧ��ҽ�����
      /// </summary>
      public decimal NewSerialNo
      {
         get { return _newSerialNo; }
      }
      private decimal _newSerialNo;

      /// <summary>
      /// �����µ�ҽ��ԭʼ��ҽ����ţ����˸���ҽ����ţ���������¶�Ϊ-1��
      /// </summary>
      public decimal OldSerialNo
      {
         get { return _oldSerialNo; }
      }
      private decimal _oldSerialNo;

      /// <summary>
      /// ҽ���ı��¼�����
      /// </summary>
      /// <param name="newSerialNo">���º��ҽ����Ӧ��ҽ�����</param>
      public OrderChangedEventArgs(decimal newSerialNo)
      {
         _newSerialNo = newSerialNo;
         _oldSerialNo = -1;
      }

      /// <summary>
      /// ҽ���ı��¼�����
      /// </summary>
      /// <param name="newSerialNo">���º��ҽ����Ӧ��ҽ�����</param>
      /// <param name="oldSerialNo">�����µ�ҽ��ԭʼ��ҽ�����</param>
      public OrderChangedEventArgs(decimal newSerialNo, decimal oldSerialNo)
      {
         _newSerialNo = newSerialNo;
         _oldSerialNo = oldSerialNo;
      }
   }
}
