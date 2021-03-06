using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;



namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// 文字医嘱内容
   /// </summary>
   public class TextOrderContent : OrderContent
   {
      #region properties
      /// <summary>
      /// 标记是否允许停止
      /// </summary>
      public override bool CanCeased
      {
         get { return true; }
      }

      /// <summary>
      /// 与实体类匹配的、读取DB中数据的SQL语句
      /// </summary>
      public override string InitializeSentence
      {
         get { throw new NotImplementedException(); }
      }

      /// <summary>
      /// 在DataTable中按主键值过滤记录的条件
      /// </summary>
      public override string FilterCondition
      {
         get { throw new NotImplementedException(); }
      }

      /// <summary>
      /// 指明该条文字医嘱是否是草药医嘱的汇总信息
      /// </summary>
      public bool IsSummaryOfHerbDetail
      {
         get
         {
            if ((ParentOrder == null) || String.IsNullOrEmpty(ParentOrder.Memo))
               return false;
            else
               return ParentOrder.Memo.StartsWith(Order.HerbSummaryFlag);
         }
      }

      /// <summary>
      /// 关联的草药医嘱的分组序号(该条文字医嘱是草药医嘱的汇总信息时有效)
      /// </summary>
      public decimal GroupSerialNoOfLinkedHerbs
      {
         get
         {
            if (IsSummaryOfHerbDetail)
            {
               try
               {
                  return Convert.ToDecimal(ParentOrder.Memo.Substring(Order.HerbSummaryFlag.Length));
               }
               catch
               {
                  return -1;
               }
            }
            else
               return -1;
         }
      }
      #endregion

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      public TextOrderContent()
         : base()
      {
         InnerOrderKind = OrderContentKind.TextNormal;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public TextOrderContent(DataRow sourceRow)
         : base(sourceRow)
      {
         InnerOrderKind = OrderContentKind.TextNormal;
      }
      #endregion

      /// <summary>
      /// 重设当前可显示的内容,为各项内容间加上空格
      /// </summary>
      protected override void ResetDisplayTexts()
      {
         InitBaseDisplayTexts();

         // 顺序为：[项目名称] 嘱托
         string tail = ""; // 长度大于1表示已经有结尾了，已便于在各项内容间插入空格
         if (!String.IsNullOrEmpty(EntrustContent))
         {
            Texts.Insert(0, new OutputInfoStruct(EntrustContent.Trim()
               , OrderOutputTextType.EntrustContent));
            tail = " ";
         }
         if ((Item != null) && (Item.KeyInitialized))
            Texts.Insert(0, new OutputInfoStruct(Item.ToString().Trim() + tail
               , OrderOutputTextType.ItemName));
      }

      /// <summary>
      /// 校验属性值
      /// </summary>
      /// <returns>返回字符串不为空表示有属性的值不正确</returns>
      public override string CheckProperties()
      {
         StringBuilder errMsg = new StringBuilder();
         if (((Item == null) || (String.IsNullOrEmpty(Item.KeyValue)))
            && (String.IsNullOrEmpty(EntrustContent)))
            errMsg.AppendLine("必须选择文字医嘱项目或填写文字医嘱内容");

         return errMsg.ToString();
      }
   }
}
