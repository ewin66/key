using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.DoctorAdvice
{
   internal static class ConstSchemaNames
   {
      #region SkinTestResult
       public const string SkinTestColFlag = "Masculine";
      public const string SkinTestColSpecSerialNo = "NormNo";
      public const string SkinTestColDruggeryName = "DrugName";
      public const string SkinTestColBeginDate = "StartDate";
      public const string SkinTestColEndDate = "EndDate";
      #endregion

      #region Order Table
      /// <summary>
      /// ��ʱҽ������
      /// </summary>
      public const string TempOrderTableName = "Temp_Order";
      /// <summary>
      /// ����ҽ������
      /// </summary>
      public const string LongOrderTableName = "Long_Order";
      /// <summary>
      /// ��ʱҽ������ֶ���
      /// </summary>
      public const string OrderTempColSerialNo = "TempID";
      /// <summary>
      /// ����ҽ������ֶ���
      /// </summary>
      public const string OrderLongColSerialNo = "LongID";

      public const string OrderColGroupSerialNo = "GroupID";
      //public const string OrderColCeaseFlag = "tzbz";
      public const string OrderColSynchFlag = "Synch";
      public const string OrderColState = "OrderStatus";
       //public const string OrderRequestOrderNo = "sqdxh";
      #endregion

      #region order output table
      public const string OrderOutputColProductSerialNo = "ProductNo";
      public const string OrderOutputColDruggeryName = "DrugName";
      public const string OrderOutputColAmount = "ypsl";
      public const string OrderOutputColUnit = "xmdw";
      public const string OrderOutputColUsageCode = "yfdm";
      public const string OrderOutputColUsageName = "yfmc";
      public const string OrderOutputColFrequencyCode = "pcdm";
      public const string OrderOutputColFrequencyName = "pcmc";
      public const string OrderOutputColDruggeryCode = "ypdm";
      #endregion

      #region DeptWardMapping
      public const string DeptWardMapColExceptDept = "�ų�����";
      public const string DeptWardMapColExceptWard = "�ų�����";
      #endregion

      #region Order Suite
      public const string SuiteTableName = "AdviceGroup";
      public const string SuiteColName = "Name";
      public const string SuiteColPy = "py";
      public const string SuiteColWb = "Wb";
      public const string SuiteColDeptCode = "DeptID";
      public const string SuiteColWardCode = "WardID";
      public const string SuiteColDoctorId = "DoctorID";
      public const string SuiteColApplyRange = "UseRange";
      public const string SuiteColMemo = "Memo";

      public const string SuiteDetailTableName = "AdviceGroupDetail";
      public const string SuiteDetailColSuiteSerialNo = "SuiteID";
      public const string SuiteDetailColOrderFlag = "Mark";
      public const string SuiteDetailColUsageCode = "UseageID";
      public const string SuiteDetailColFrequecyCode = "Frequency";

      /// <summary>
      /// ҽ�����
      /// 3100ҩƷҽ��,3101��ͨ��Ŀҽ��,3102����ҽ��,3103�ٴ���Ŀҽ��,3104��Ժ��ҩҽ��,3105����ҽ��,
      /// 3109ͣ����ҽ��,3110��ҽ��(��ͨ),3111��ҽ��(ת��),3112��ҽ��(����),3113��ҽ��(��Ժ)
      /// </summary>
      public const string SuiteDetailColOrderCatalog = "OrderType"; //"AdviceCategory" Modified By wwj 2011-06-28
      public const string SuiteDetailColGroupFlag = "SortMark";
      public const string SuiteDetailColAmount = "DrugDose";

      public const string SuiteDetailViewColUsageName = "UseageName";
      public const string SuiteDetailViewColFrequencyName = "FrequencyName";
      public const string SuiteDetailViewColOrderCatalogName = "AdviceCategoryName";
      public const string SuiteDetailViewColGroupSymbol = "fzfh";
      #endregion

      #region OrderContentCatalog
      public const string ContentCatalogTableName = "OrderContentCatalog";
      public const string ContentCatalogColId = "ID";
      public const string ContentCatalogColName = "Name";
      public const string ContentCatalogColFlag = "Flag";
      #endregion

      #region inpatient
      public const string InpatientColDangerLevel = "CriticalLevel";
      public const string InpatientColCareLevel = "AttendLevel";
      #endregion

      #region recipe rule
      // modified by zhouhui 
      public const string RecipeRuleColName = "Code";
      //public const string RecipeRuleColDoctorLevel = "zcdm";
      //public const string RecipeRuleColDiagnose = "zddm";
      //public const string RecipeRuleColDept = "ksdm";
      //public const string RecipeRuleColMedicalCare = "ybdm";
      public const string RecipeRuleColControlLine = "ControlDose";
      public const string RecipeRuleColWarningLine = "WarnDose";
      public const string RecipeRuleColUnitKind = "UnitCategory";

      public const string RecipeRuleDefRangeLevel = "ZC";
      public const string RecipeRuleDefRangeDiag = "ZD";
      public const string RecipeRuleDefRangeDept = "KS";
      public const string RecipeRuleDefRangeMedicalCare = "YB";
      #endregion

      #region procedure para
      public const string ProcUpdateSerialNo = "usp_Emr_SetOrderGroupSerialNo";
      public const string ProcParaFirstpageNo = "NoOfInpat";
      public const string ProcParaOrderKind = "OrderType";
      public const string ProcParaOnlyNew = "onlynew";

      public const string ProcQueryOrderSuite = "usp_Emr_QueryOrderSuites";
      #endregion 

   }
}
