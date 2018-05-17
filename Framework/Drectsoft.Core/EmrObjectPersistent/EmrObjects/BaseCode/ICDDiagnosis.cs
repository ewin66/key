using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.Common.Eop;
using System.Data;
using System.Globalization;
namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ҽԺ��Ͽ���
   /// </summary>
   public class ICDDiagnosis : EPBaseObject
   {
      #region properties
      /// <summary>
      /// ICD10����
      /// </summary>
      public string IcdCode
      {
         get { return _icdCode; }
         set { _icdCode = value; }
      }
      private string _icdCode;

      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get { return GetQuerySentenceFromXml("SelectDiagnoseICD10"); }
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
      /// </summary>
      public override string FilterCondition
      {
          get { return FormatFilterString("MarkId", IcdCode); }
      }
      #endregion

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      public ICDDiagnosis()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      public ICDDiagnosis(string code)
         : base(code)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      public ICDDiagnosis(string code, string name)
         : base(code, name)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public ICDDiagnosis(DataRow sourceRow)
         : base(sourceRow)
      { }

      #endregion

      /// <summary>
      /// ��ʼ�����е����ԣ������������͵������Լ�������
      /// </summary>
      public override void ReInitializeAllProperties()
      {
         ReInitializeProperties();
      }
   }
}
