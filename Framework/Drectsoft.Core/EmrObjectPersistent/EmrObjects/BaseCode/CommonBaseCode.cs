using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;


namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// 通用字典对象，用来处理字典分类明细表（YY_ZDFLMXK）
   /// </summary>
   public class CommonBaseCode : EPBaseObject
   {
      #region properties
      /// <summary>
      /// 所属分类代码
      /// </summary>
      public string CatalogCode
      {
         get { return _catalogCode; }
         set { _catalogCode = value; }
      }
      private string _catalogCode;

      /// <summary>
      /// 与实体类匹配的、读取DB中数据的SQL语句
      /// </summary>
      public override string InitializeSentence
      {
         get { return GetQuerySentenceFromXml("SelectCommonBook"); }
      }

      /// <summary>
      /// 在DataTable中按主键值过滤记录的条件
      /// </summary>
      public override string FilterCondition
      {
         get
         {
            if (String.IsNullOrEmpty(CatalogCode))
               return "1=2";
            else
                return FormatFilterString("DetailID", Code) + " and CategoryID = '" + CatalogCode + "'"; 
         }
      }
      #endregion

      #region ctor
      /// <summary>
      /// 
      /// </summary>
      public CommonBaseCode()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      public CommonBaseCode(string code)
         : base(code)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      public CommonBaseCode(string code, string name)
         : base(code, name)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      /// <param name="catalogCode"></param>
      public CommonBaseCode(string code, string name, string catalogCode)
         : this(code, name)
      {
         _catalogCode = catalogCode;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public CommonBaseCode(DataRow sourceRow)
         : base(sourceRow)
      { }

      #endregion

      #region public method
      /// <summary>
      /// 确定两个对象是否具有相同的值
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         if (obj == null)
            return false;
         CommonBaseCode aimObj = (CommonBaseCode)obj;

         if (aimObj != null)
         {
            return ((aimObj.Code == Code) && (aimObj.CatalogCode == CatalogCode));
         }
         return false;
      }

      /// <summary>
      /// 返回该实例的哈希代码
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         string total = Code + Name + CatalogCode;
         return total.GetHashCode();
      }

      /// <summary>
      /// 初始化所有的属性，包括引用类型的属性自己的属性
      /// </summary>
      public override void ReInitializeAllProperties()
      {
         ReInitializeProperties();
      }
      #endregion
   }
}
