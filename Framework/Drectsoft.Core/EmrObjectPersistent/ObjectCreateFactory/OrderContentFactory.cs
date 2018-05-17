using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ����ҽ�����ݵĹ�����
   /// </summary>
   public sealed class OrderContentFactory
   {
       private static string ColumnNameOrderKind = "OrderType";
      private OrderContentFactory()
      { }

      /// <summary>
      /// ����DataRow���ƶ��е�ֵȷ�����ʵ�ҽ����������
      /// </summary>
      /// <param name="kindValue"></param>
      /// <returns></returns>
      public static string GetOrderContentClassName(object kindValue)
      {
         if (kindValue == null)
            throw new ArgumentNullException("kindValue"
               , MessageStringManager.GetString("CommonParameterIsNull", "ҽ������"));
         
         OrderContentKind orderKind =
            (OrderContentKind)Enum.Parse(typeof(OrderContentKind), kindValue.ToString());

         switch (orderKind)
         {
            case OrderContentKind.Druggery: //ҩƷҽ��
               return  "DruggeryOrderContent";
            case OrderContentKind.ChargeItem://��ͨ��Ŀҽ��
               return "ChargeItemOrderContent";
            case OrderContentKind.GeneralItem://����ҽ��
               return "GeneralOrderContent";
            case OrderContentKind.ClinicItem://�ٴ���Ŀҽ��
               return "ClinicItemOrderContent";
            case OrderContentKind.OutDruggery://��Ժ��ҩ
               return "OutDruggeryContent";
            case OrderContentKind.Operation://����ҽ��
               return "OperationOrderContent";
            //case OrderKindFlags.CeaseLong://ͣ����ҽ��
            //   break;
            case OrderContentKind.TextNormal://��ҽ��(��ͨ)
               return "TextOrderContent";
            case OrderContentKind.TextShiftDept://��ҽ��(ת��)
               return "ShiftDeptOrderContent";
            case OrderContentKind.TextAfterOperation://��ҽ��(����)
               return "AfterOperationContent";
            case OrderContentKind.TextLeaveHospital://��ҽ��(��Ժ)
               return "LeaveHospitalOrderContent";
            default:
               throw new ArgumentException(MessageStringManager.GetString("ClsssNotImplement"));
         }
      }
      
      /// <summary>
      /// ����ָ����DataRow����ҽ�����ݶ������Ƚ���DataRow����ȡҽ�����
      /// </summary>
      /// <param name="sourceRow">������ʼ���ݵ�ҽ��DataRow</param>
      /// <returns>���ʵ�ҽ������</returns>
      public static OrderContent CreateOrderContent(DataRow sourceRow)
      {
         if (sourceRow == null)
            return null;

         OrderContentKind orderKind;

         if ((sourceRow != null)
            && (sourceRow.Table.Columns.Contains(ColumnNameOrderKind)))
            orderKind = (OrderContentKind)Convert.ToInt32(sourceRow[ColumnNameOrderKind]);
         else
            orderKind = OrderContentKind.Druggery; // -1; Ĭ�ϴ���ҩƷҽ��
         return CreateOrderContent(orderKind, sourceRow);
      }

      /// <summary>
      /// ����ָ����ҽ�����ͺ�DataRow����ҽ�����ݶ���
      /// </summary>
      /// <param name="orderKind">ҽ�����</param>
      /// <param name="sourceRow">������ʼ���ݵ�ҽ��DataRow</param>
      /// <returns>���ʵ�ҽ������</returns>
      public static OrderContent CreateOrderContent(OrderContentKind orderKind, DataRow sourceRow)
      {
         OrderContent content;
         // ����ҽ�����+��Ŀ��𴴽�ҽ����Ŀ
         switch (orderKind)
         {
            case OrderContentKind.Druggery: //ҩƷҽ��
               content = new DruggeryOrderContent(sourceRow);
               break;
            case OrderContentKind.ChargeItem://��ͨ��Ŀҽ��
               content = new ChargeItemOrderContent(sourceRow);
               break;
            case OrderContentKind.GeneralItem://����ҽ��
               content = new GeneralOrderContent(sourceRow);
               break;
            case OrderContentKind.ClinicItem://�ٴ���Ŀҽ��
               content = new ClinicItemOrderContent(sourceRow);
               break;
            case OrderContentKind.OutDruggery://��Ժ��ҩ
               content = new OutDruggeryContent(sourceRow);
               break;
            case OrderContentKind.Operation://����ҽ��
               content = new OperationOrderContent(sourceRow);
               break;
            //case OrderKindFlags.CeaseLong://ͣ����ҽ��
            //   break;
            case OrderContentKind.TextNormal://��ҽ��(��ͨ)
               content = new TextOrderContent(sourceRow);
               break;
            case OrderContentKind.TextShiftDept://��ҽ��(ת��)
               content = new ShiftDeptOrderContent(sourceRow);
               break;
            case OrderContentKind.TextAfterOperation://��ҽ��(����)
               content = new AfterOperationContent(sourceRow);
               break;
            case OrderContentKind.TextLeaveHospital://��ҽ��(��Ժ)
               content = new LeaveHospitalOrderContent(sourceRow);
               break;
            default:
               throw new ArgumentException(MessageStringManager.GetString("ClsssNotImplement"));
         }
         content.InnerOrderKind = orderKind;
         return content;
      }
   }
}
