using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;


namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ͨ���ֵ�������������ֵ������ϸ��YY_ZDFLMXK��
   /// </summary>
   public class CommonBaseCode : EPBaseObject
   {
      #region properties
      /// <summary>
      /// �����������
      /// </summary>
      public string CatalogCode
      {
         get { return _catalogCode; }
         set { _catalogCode = value; }
      }
      private string _catalogCode;

      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get { return GetQuerySentenceFromXml("SelectCommonBook"); }
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
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
      /// ȷ�����������Ƿ������ͬ��ֵ
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
      /// ���ظ�ʵ���Ĺ�ϣ����
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         string total = Code + Name + CatalogCode;
         return total.GetHashCode();
      }

      /// <summary>
      /// ��ʼ�����е����ԣ������������͵������Լ�������
      /// </summary>
      public override void ReInitializeAllProperties()
      {
         ReInitializeProperties();
      }
      #endregion
   }
}
