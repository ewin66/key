using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.Common.Eop;
using System.Data;
using System.Globalization;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ��ҽ��Ͽ���
   /// </summary>
   public class ChineseDiagnosis : EPBaseObject 
   {
      #region properties
      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get { return GetQuerySentenceFromXml("SelectChineseDiagnose"); }
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
      /// </summary>
      public override string FilterCondition
      {
          get { return FormatFilterString("ChDiagnoseID", Code); }
      }
      #endregion

      #region ctors
      /// <summary>
      /// 
      /// </summary>
     public ChineseDiagnosis()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      public ChineseDiagnosis(string code)
         : base(code)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      public ChineseDiagnosis(string code, string name)      
         : base(code, name)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public ChineseDiagnosis(DataRow sourceRow)
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
