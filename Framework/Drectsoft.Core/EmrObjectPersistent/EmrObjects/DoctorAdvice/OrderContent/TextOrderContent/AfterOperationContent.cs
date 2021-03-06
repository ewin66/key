using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// 术后医嘱内容
   /// </summary>
   public sealed class AfterOperationContent : TextOrderContent
   {
      #region properties
      /// <summary>
      /// 隐藏嘱托字段，避免外部直接访问
      /// </summary>
      internal new string EntrustContent
      {
         get
         {
            if (_entrustContent != null)
               return _entrustContent;
            else
               return "";
         }
         set { _entrustContent = value; }
      }
      private string _entrustContent;
      #endregion

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      public AfterOperationContent()
         : base()
      {
         InnerOrderKind = OrderContentKind.TextAfterOperation;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public AfterOperationContent(DataRow sourceRow)
         : base(sourceRow)
      {
         InnerOrderKind = OrderContentKind.TextAfterOperation;
      }
      #endregion

      /// <summary>
      /// 重设当前可显示的内容,为各项内容间加上空格
      /// </summary>
      protected override void ResetDisplayTexts()
      {
         InitBaseDisplayTexts();

         Texts.Insert(0, new OutputInfoStruct("术后医嘱", OrderOutputTextType.ItemName));
      }

      /// <summary>
      /// 校验属性值
      /// </summary>
      /// <returns>返回字符串不为空表示有属性的值不正确</returns>
      public override string CheckProperties()
      {         
         return "";
      }
   }
}