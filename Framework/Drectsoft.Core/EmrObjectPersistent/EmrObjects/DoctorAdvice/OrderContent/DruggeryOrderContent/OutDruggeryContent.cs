using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


using System.Globalization;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ��Ժ��ҩҽ������.�̳���DruggerOrderContent
   /// </summary>
   public class OutDruggeryContent : DruggeryOrderContent
   {
      #region properties
      /// <summary>
      /// ����Ƿ�����ֹͣ
      /// </summary>
      public override bool CanCeased
      {
         get { return false; }
      }

      /// <summary>
      /// ִ������
      /// </summary>
      public int ExecuteDays
      {
         get { return _executeDays; }
         set
         {
            if (value <= 0)
               throw new ArgumentException(MessageStringManager.GetString("CommonValueIsLess", 0));

            _executeDays = value;
            FireOrderContentChanged(new EventArgs());
         }
      }
      private int _executeDays;

      /// <summary>
      /// ҩƷ������(Ϊ��Ժ��ҩ����,ʹ�ü�����λ)
      /// </summary>
      public decimal TotalAmount
      {
         get { return _totalAmount; }
         set { _totalAmount = value; }
      }
      private decimal _totalAmount;
      #endregion

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      public OutDruggeryContent()
         : base()
      {
         InnerOrderKind = OrderContentKind.OutDruggery;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public OutDruggeryContent(DataRow sourceRow)
         : base(sourceRow)
      {
         InnerOrderKind = OrderContentKind.OutDruggery;
      }
      #endregion

      /// <summary>
      /// ���赱ǰ����ʾ������,Ϊ�������ݼ���Ͽո�
      /// </summary>
      protected override void ResetDisplayTexts()
      {
         InitBaseDisplayTexts();

         // ˳��Ϊ����Ŀ ������λ [�÷�] [Ƶ��] ���� [����]
         if (Item == null) // ���û����Ŀ����Ĭ��Ϊ��ʾ����Ϊ��
            return;

         string tail = ""; // ���ȴ���1��ʾ�Ѿ��н�β�ˣ��ѱ����ڸ������ݼ����ո�
         if ((Attributes & OrderAttributeFlag.Provide4Oneself) > 0)
         {
            Texts.Insert(0, new OutputInfoStruct("�Ա�", OrderOutputTextType.SelfProvide));
            tail = " ";
         }
         if (!String.IsNullOrEmpty(EntrustContent))
         {
            Texts.Insert(0, new OutputInfoStruct(EntrustContent.Trim(), OrderOutputTextType.EntrustContent));
            tail = " ";
         }
         Texts.Insert(0, new OutputInfoStruct(ExecuteDays.ToString("#'��'", CultureInfo.CurrentCulture), OrderOutputTextType.ItemDays));
         if (tail.Length == 0)
            tail = " ";

         if ((ItemFrequency != null) && (ItemFrequency.KeyInitialized))
            Texts.Insert(0, new OutputInfoStruct(ItemFrequency.ToString().Trim() + tail, OrderOutputTextType.ItemFrequency));
         if ((ItemUsage != null) && (ItemUsage.KeyInitialized))
            Texts.Insert(0, new OutputInfoStruct(ItemUsage.ToString().Trim() + tail, OrderOutputTextType.ItemUsage));

         Texts.Insert(0, new OutputInfoStruct(Amount.ToString("#.##", CultureInfo.CurrentCulture) + CurrentUnit.Name.Trim() + tail, OrderOutputTextType.ItemAmount));

         Texts.Insert(0, new OutputInfoStruct(Item.ToString().Trim() + tail, OrderOutputTextType.ItemName));
      }

      /// <summary>
      /// У������ֵ
      /// </summary>
      /// <returns>�����ַ�����Ϊ�ձ�ʾ�����Ե�ֵ����ȷ</returns>
      public override string CheckProperties()
      {
         StringBuilder errMsg = new StringBuilder();
         if ((Item == null) || (String.IsNullOrEmpty(Item.KeyValue)))
            errMsg.AppendLine("����ѡ��ҩƷ");
         if ((Amount <= 0) || (Amount > 1000))
            errMsg.AppendLine("ҩƷ����Ӧ��0��1000��Χ��");
         if (CurrentUnit.IsEmpty)
            errMsg.AppendLine("����ѡ��λ");
         if (ItemUsage == null)
            errMsg.AppendLine("����ѡ���÷�");
         if (ItemFrequency == null)
            errMsg.AppendLine("����ѡ��Ƶ��");
         if ((ExecuteDays <= 0) || (ExecuteDays > 30))
            errMsg.AppendLine("ִ������Ӧ��1��30֮��");

         return errMsg.ToString();
      }

      /// <summary>
      /// ���¼�������������ֵ
      /// </summary>
      public void ReCalcTotalAmount()
      {
         TotalAmount = Amount * ItemFrequency.ExecuteTimesPerDay * ExecuteDays;
      }
   }
}
