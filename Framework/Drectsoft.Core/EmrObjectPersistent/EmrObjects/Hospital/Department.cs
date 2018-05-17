using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.ObjectModel;

using System.Globalization;
using DrectSoft.Core;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// Ժ�ڿ���
   /// TODO: ��ȱ��������
   /// </summary>
   public class Department : EPBaseObject
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
      /// ����һ�����Ҵ���
      /// TODO: �貹��һ��������
      /// </summary>
      public string LevelADeptCode
      {
         get { return _levelADeptCode; }
         set { _levelADeptCode = value; }
      }
      private string _levelADeptCode;


      /// <summary>
      /// �����������Ҵ���
      /// TODO: �貹�����������
      /// </summary>
      public string LevelBDeptCode
      {
         get { return _levelBDeptCode; }
         set { _levelBDeptCode = value; }
      }
      private string _levelBDeptCode;

      /// <summary>
      /// �������
      /// </summary>
      public DepartmentKind Kind
      {
         get { return _kind; }
         set { _kind = value; }
      }
      private DepartmentKind _kind;

      /// <summary>
      /// ���ұ�־
      /// </summary>
      public DepartmentClinicKind DeptAttribute
      {
         get { return _deptAttribute; }
         set { _deptAttribute = value; }
      }
      private DepartmentClinicKind _deptAttribute;

      /// <summary>
      /// ��Ӧ���������б�
      /// </summary>
      public Collection<String> CorrespondingWards
      {
         get
         {
            if (_correspondingWards == null)
               _correspondingWards = new Collection<String>();

            return _correspondingWards;
         }
      }
      private Collection<string> _correspondingWards;

      /// <summary>
      /// ��Ӧ������������
      /// </summary>
      public string CorrespondingWardsCondition
      {
         get
         {
            StringBuilder result = new StringBuilder(" WardID in (");
            if (CorrespondingWards.Count == 0)
               result.Append("''");
            else
               foreach (string ward in CorrespondingWards)
               {
                  result.AppendFormat(CultureInfo.CurrentCulture, " '{0}'", ward.Trim());
                  if (result.Length > 0)
                     result.Append(',');
               }
            result.Append(')');
            return result.ToString();
         }
      }

      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get { return GetQuerySentenceFromXml("SelectDepartment"); }
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
      /// </summary>
      public override string FilterCondition
      {
          get { return FormatFilterString("DeptID", Code); }
      }
      #endregion

      #region ctor
      /// <summary>
      /// 
      /// </summary>
      public Department()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      public Department(string code)
         : base(code)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      public Department(string code, string name)
         : base(code, name)
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public Department(DataRow sourceRow)
         : base(sourceRow)
      { }

      #endregion

      #region public method
      /// <summary>
      /// ��ʼ�����Ҷ�Ӧ��������
      /// </summary>
      /// <param name="sqlExecutor"></param>
      public void InitializeCorrespondingWards(IDataAccess sqlExecutor)
      {
         if ((_correspondingWards == null) || (_correspondingWards.Count == 0))
         {
            _correspondingWards = new Collection<string>();
            // ���Ҷ�Ӧ����
            DataRow[] rows = sqlExecutor.GetRecords(
                 PersistentObjectFactory.GetQuerySentenceByName("SelectDepartmentWardMappings")
               , String.Format(CultureInfo.CurrentCulture, "DeptID = '{0}'", Code)
               , true);

            foreach (DataRow row in rows)
                _correspondingWards.Add(row["WardID"].ToString());
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
