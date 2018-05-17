using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.Common.Eop;
using System.Data;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ��������--ʡ��ֱϽ��
   /// TODO: �йص�������Ĵ�����Ľ����
   /// </summary>
   public class ZoneProvince : EPBaseObject
   {
      #region properties
      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get { return GetQuerySentenceFromXml("SelectZone"); }
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
      /// </summary>
      public override string FilterCondition
      {
         get
         {
             return String.Format("{0} and Category = {1}", FormatFilterString("AreaID", Code), (int)ZoneGrade.Province); 
         }
      }
      #endregion

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      public ZoneProvince()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      public ZoneProvince(string code)
         : base(code)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      public ZoneProvince(string code, string name)
         : base(code, name)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public ZoneProvince(DataRow sourceRow)
         : base(sourceRow)
      { }
      #endregion

      #region public methods

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
