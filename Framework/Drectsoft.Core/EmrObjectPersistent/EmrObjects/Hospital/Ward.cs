using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using System.Globalization;
using DrectSoft.Core;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ����
   /// </summary>
   public class Ward : EPBaseObject
   {
      #region properties
      /// <summary>
      /// ����ҽԺ����
      /// TODO: ��Ҫ����ҽԺ��
      /// </summary>
      public string HospitalCode
      {
         get { return _hospitalCode; }
         set { _hospitalCode = value; }
      }
      private string _hospitalCode;

      /// <summary>
      /// ��λ��
      /// </summary>
      public int AmountOfBeds
      {
         get { return _amountOfBeds; }
         set { _amountOfBeds = value; }
      }
      private int _amountOfBeds;

      /// <summary>
      /// ������־()
      /// </summary>
      public WardKind Kind
      {
         get { return _kind; }
         set { _kind = value; }
      }
      private WardKind _kind;

      /// <summary>
      /// ��Ӧ����
      /// </summary>
      public Collection<string> CorrespondingDepts
      {
         get
         {
            if (_correspondingDepts == null)
               _correspondingDepts = new Collection<string>();

            return _correspondingDepts;
         }
      }
      private Collection<string> _correspondingDepts;

      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get { return GetQuerySentenceFromXml("SelectWard"); }
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
      /// </summary>
      public override string FilterCondition
      {
          get { return FormatFilterString("WardID", Code); }
      }
      #endregion

      #region ctor
      /// <summary>
      /// 
      /// </summary>
      public Ward()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      public Ward(string code)
         : base(code)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      public Ward(string code, string name)
         : base(code, name)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public Ward(DataRow sourceRow)
         : base(sourceRow)
      { }

      #endregion

      #region public method
      /// <summary>
      /// ��ʼ��������������
      /// </summary>
      /// <param name="sqlExecutor"></param>
      public void InitializeCorrespondingDepts(IDataAccess sqlExecutor)
      {
         if ((_correspondingDepts == null) || (_correspondingDepts.Count == 0))
         {
            _correspondingDepts = new Collection<string>();
            // ���Ҷ�Ӧ����
            DataRow[] rows = sqlExecutor.GetRecords(
                 PersistentObjectFactory.GetQuerySentenceByName("SelectDepartmentWardMappings")
               , String.Format(CultureInfo.CurrentCulture, "WardID = '{0}'", Code)
               , true);

            foreach (DataRow row in rows)
                _correspondingDepts.Add(row["DeptID"].ToString());
         }

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
