using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.Common.Eop;
using System.Data;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ��������--����
   /// </summary>
   public class ZoneCountry : EPBaseObject
   {
      #region properties
      /// <summary>
      /// ��������
      /// </summary>
      public ZoneProvince ParentZone
      {
         get { return _parentZone; }
         set { _parentZone = value; }
      }
      private ZoneProvince _parentZone;

      /// <summary>
      /// ����
      /// </summary>
      public ZoneGrade Grade
      {
         get { return _grade; }
         set { _grade = value; }
      }
      private ZoneGrade _grade;

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
             return String.Format("{0} and Category = {1}", FormatFilterString("AreaID", Code), (int)ZoneGrade.Country); 
         }
      }
      #endregion

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      public ZoneCountry()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      public ZoneCountry(string code)
         : base(code)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      public ZoneCountry(string code, string name)
         : base(code, name)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public ZoneCountry(DataRow sourceRow)
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
