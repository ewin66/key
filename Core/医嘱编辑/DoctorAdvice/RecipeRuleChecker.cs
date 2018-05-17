using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.IO;
using DrectSoft.Common.Eop;


namespace DrectSoft.Core.DoctorAdvice {
    /// <summary>
    /// ����������
    /// </summary>
    public class RecipeRuleChecker {
        #region public properites
        /// <summary>
        /// ��ϴ���
        /// </summary>
        public string DiagnoseCode {
            get {
                if (_diagnoseCode == null)
                    _diagnoseCode = "";
                return _diagnoseCode;
            }
            set { _diagnoseCode = value; }
        }
        private string _diagnoseCode;

        /// <summary>
        /// ҽ�����ʹ���
        /// </summary>
        public string MedCareCode {
            get {
                if (_medCareCode == null)
                    _medCareCode = "";
                return _medCareCode;
            }
            set { _medCareCode = value; }
        }
        private string _medCareCode;

        /// <summary>
        /// ƾ֤���ʹ���
        /// </summary>
        public string WarrantCode {
            get {
                if (_warrantCode == null)
                    _warrantCode = "";
                return _warrantCode;
            }
            set { _warrantCode = value; }
        }
        private string _warrantCode;
        #endregion

        #region private variables
        /// <summary>
        /// ְ��ҩƷ��Ӧ��(YY_ZCYPDYK)
        /// </summary>
        private DataTable m_TechTitleMap;
        /// <summary>
        /// ���ҩƷ��Ӧ��(YY_ZDYPDYK)
        /// </summary>
        private DataTable m_DiagnoseMap;
        /// <summary>
        /// ����ҩƷ��Ӧ��(YY_KSYPDYK)
        /// </summary>
        private DataTable m_DeptMap;
        /// <summary>
        /// ҽ��(����)ҩƷ��Ӧ��(YY_YBYPDYK)
        /// </summary>
        private DataTable m_MedCareMap;
        /// <summary>
        /// ҩƷ��Ŀ���Ʊ�(ZY_YS_YPXMXZK)
        /// </summary>
        private DataTable m_ItemLimited;
        /// <summary>
        /// ��ϸҩƷ��Ӧ��(YY_MXYPDYK)
        /// </summary>
        private DataTable m_ItemDetailMap;
        //private IExchangeInfoServer m_InfoServer;
        private bool m_HadInitialized;
        private string m_DeptCode;
        /// <summary>
        /// ��ϸ��������(������������ģʽ: false:���ò�������ϸ true:����������ϸ)
        /// </summary>
        private bool m_SetLimitedDetail;
        /// <summary>
        /// ҽ����ְ��
        /// </summary>
        private string m_TechTitle;
        private string m_Filter;
        private IDataAccess m_SqlHelper;
        #endregion

        public RecipeRuleChecker( string deptCode, string techTitle) {
            //m_InfoServer = infoServer;
            m_DeptCode = deptCode;
            m_TechTitle = techTitle;

            m_HadInitialized = false;
            //if (m_InfoServer != null)
            GenerateRulesData();
        }

        /// <summary>
        /// ���������������ݷ�ʽ�任Ϊ���ӱ������ݶ�ȡ
        /// </summary>
        /// <param name="app"></param>
        /// <param name="deptCode"></param>
        /// <param name="techTitle"></param>
        public RecipeRuleChecker(IDataAccess sqlHelper, string deptCode, string techTitle) {
            m_SqlHelper = sqlHelper;
            m_DeptCode = deptCode;
            m_TechTitle = techTitle;

            m_HadInitialized = false;
            if (m_SqlHelper != null)
                GenerateRulesData();
        }

        #region private methods
        private void GenerateRulesData() {
            m_HadInitialized = false;
            //��ҽ���Ĵ�����������ݶ�ȡ��ʽ��Ϊ�ӱ������ݿ��ж�ȡ

            //string[,] parameters = new string[3, 0];

            //string sExio;
            //sExio = m_InfoServer.BuildExchangeInfoString(ExchangeInfoOrderConst.MsgGetRecipeRules, "", ExchangeInfoOrderConst.HisSystemName, parameters);

            //Encoding currentEncoding = Encoding.GetEncoding(ExchangeInfoOrderConst.EncodingName);
            //string outMsg;
            //if (m_InfoServer.AddSyncExchangeInfo(sExio, currentEncoding, out outMsg) != ResponseFlag.Complete)
            //   throw new ApplicationException(outMsg);
            //// ͨ���жϷ������ݼ����������Ƿ�ɹ�
            //if (String.IsNullOrEmpty(outMsg))
            //   throw new ApplicationException(ConstMessages.ExceptionCantReadRecipteRules);
            //else
            //{
            //   DataSet resultDS = new DataSet();
            //   resultDS.Locale = CultureInfo.CurrentCulture;

            //   // ���ݼ�ת����byte����
            //   MemoryStream source = new MemoryStream();
            //   byte[] data = currentEncoding.GetBytes(outMsg);
            //   source.Write(data, 0, data.Length);
            //   source.Seek(0, SeekOrigin.Begin);
            //   resultDS.ReadXml(source);

            //   m_TechTitleMap = resultDS.Tables[0];
            //   m_DiagnoseMap = resultDS.Tables[1];
            //   m_DeptMap = resultDS.Tables[2];
            //   m_MedCareMap = resultDS.Tables[3];
            //   m_ItemLimited = resultDS.Tables[4];
            //   m_ItemDetailMap = resultDS.Tables[5];
            //   m_SetLimitedDetail = (resultDS.Tables[6].Rows[0][0].ToString() == "1");
            m_TechTitleMap = m_SqlHelper.ExecuteDataTable(String.Format(ConstSqlSentences.FormatRecipeRuleCFYP, ConstSchemaNames.RecipeRuleDefRangeLevel));
            m_DiagnoseMap = m_SqlHelper.ExecuteDataTable(String.Format(ConstSqlSentences.FormatRecipeRuleCFYP, ConstSchemaNames.RecipeRuleDefRangeDiag));
            m_DeptMap = m_SqlHelper.ExecuteDataTable(String.Format(ConstSqlSentences.FormatRecipeRuleCFYP, ConstSchemaNames.RecipeRuleDefRangeDept));
            m_MedCareMap = m_SqlHelper.ExecuteDataTable(String.Format(ConstSqlSentences.FormatRecipeRuleCFYP, ConstSchemaNames.RecipeRuleDefRangeMedicalCare));
            m_ItemLimited = m_SqlHelper.ExecuteDataTable(ConstSqlSentences.FormatRecipeRuleJLXZ);
            m_ItemDetailMap = m_SqlHelper.ExecuteDataTable(ConstSqlSentences.FormatRecipeRuleTSYP);
            m_SetLimitedDetail = CoreBusinessLogic.BusinessLogic.SetLimitedDetail;
            m_HadInitialized = true;
        }

        private bool CheckHasMatchRows(DataTable table, string filter) {
            DataRow[] matchRows = table.Select(filter);
            return ((matchRows != null) && (matchRows.Length > 0));
        }

        private bool CheckDetailLimited(string type, string typeValue, string catalogCode, decimal productSerialNo, string itemCode) {
            m_Filter = String.Format(CultureInfo.CurrentCulture
                    , ConstSqlSentences.FormatRecipeRuleDetailFilter
                    , type, typeValue.Trim(), catalogCode.Trim(), productSerialNo, itemCode.Trim());
            if (CheckHasMatchRows(m_ItemDetailMap, m_Filter)) {
                if (m_SetLimitedDetail)
                    return false;
            }
            else {
                if (!m_SetLimitedDetail)
                    return false;
            }
            return true;
        }

        private bool CheckRule(DataTable ruleTable, string type, string typeField, string typeValue, bool isDruggery, string catalogCode, decimal productSerialNo, string itemCode) {
            int drugFlag;
            if (isDruggery)
                drugFlag = 1;
            else
                drugFlag = 0;

            m_Filter = String.Format(CultureInfo.CurrentCulture, ConstSqlSentences.FormatRecipeRuleFilter
                , drugFlag, catalogCode.Trim(), typeField, typeValue.Trim());
            if (CheckHasMatchRows(ruleTable, m_Filter))
                return CheckDetailLimited(type, typeValue, catalogCode, productSerialNo, itemCode);
            else
                return true;
        }

        private bool CheckTechTitleRule(bool isDruggery, string catalogCode, decimal productSerialNo, string itemCode) {
            //modified by zhouhui
            //return CheckRule(m_TechTitleMap, ConstSchemaNames.RecipeRuleDefRangeLevel, ConstSchemaNames.RecipeRuleColDoctorLevel, m_TechTitle, isDruggery, catalogCode, productSerialNo, itemCode);
            return CheckRule(m_TechTitleMap, ConstSchemaNames.RecipeRuleDefRangeLevel, ConstSchemaNames.RecipeRuleColName, m_TechTitle, isDruggery, catalogCode, productSerialNo, itemCode);
        }

        private bool CheckDiagnoseRule(bool isDruggery, string catalogCode, decimal productSerialNo, string itemCode) {
            //modified by zhouhui
            //return CheckRule(m_DiagnoseMap, ConstSchemaNames.RecipeRuleDefRangeDiag, ConstSchemaNames.RecipeRuleColDiagnose, DiagnoseCode, isDruggery, catalogCode, productSerialNo, itemCode);
            return CheckRule(m_DiagnoseMap, ConstSchemaNames.RecipeRuleDefRangeDiag, ConstSchemaNames.RecipeRuleColName, DiagnoseCode, isDruggery, catalogCode, productSerialNo, itemCode);

        }

        private bool CheckDepartmentRule(bool isDruggery, string catalogCode, decimal productSerialNo, string itemCode) {
            return CheckRule(m_DeptMap, ConstSchemaNames.RecipeRuleDefRangeDept, ConstSchemaNames.RecipeRuleColName, m_DeptCode, isDruggery, catalogCode, productSerialNo, itemCode);
            //return CheckRule(m_DeptMap, ConstSchemaNames.RecipeRuleDefRangeDept, ConstSchemaNames.RecipeRuleColDept, m_DeptCode, isDruggery, catalogCode, productSerialNo, itemCode);
        }

        private bool CheckMedCareRule(bool isDruggery, string catalogCode, decimal productSerialNo, string itemCode) {
            //modified by zhouhui
            //return CheckRule(m_MedCareMap, ConstSchemaNames.RecipeRuleDefRangeMedicalCare, ConstSchemaNames.RecipeRuleColMedicalCare, MedCareCode, isDruggery, catalogCode, productSerialNo, itemCode);
            return CheckRule(m_MedCareMap, ConstSchemaNames.RecipeRuleDefRangeMedicalCare, ConstSchemaNames.RecipeRuleColName, MedCareCode, isDruggery, catalogCode, productSerialNo, itemCode);

        }

        private void CheckItemOrDruggery(bool isDruggery, string catalogCode, decimal productSerialNo, string itemCode) {
            if (!CheckTechTitleRule(isDruggery, catalogCode, productSerialNo, itemCode))
                throw new DataCheckException(ConstMessages.CheckRecipeRuleDoctorLevel, ConstNames.DoctorLevel);
            if (!CheckDiagnoseRule(isDruggery, catalogCode, productSerialNo, itemCode))
                throw new DataCheckException(ConstMessages.CheckRecipeRuleDiagnose, ConstNames.Diagnose);
            if (!CheckDepartmentRule(isDruggery, catalogCode, productSerialNo, itemCode))
                throw new DataCheckException(ConstMessages.CheckRecipeRuleDept, ConstNames.Dept);
            if (!CheckMedCareRule(isDruggery, catalogCode, productSerialNo, itemCode))
                throw new DataCheckException(ConstMessages.CheckRecipeRuleMedicalCare, ConstNames.MedicalCare);
        }

        private void CheckItemAmount(decimal productSerialNo, string itemCode, decimal amount, ItemUnit unit, ItemUnit dosageUnit, ItemUnit wardUnit) {
            m_Filter = String.Format(CultureInfo.CurrentCulture, ConstSqlSentences.FormatRecipeRuleItemAmountFilter
               , WarrantCode, productSerialNo, itemCode);
            DataRow[] matchRows = m_ItemLimited.Select(m_Filter);
            if ((matchRows != null) && (matchRows.Length > 0)) {
                ItemUnit usedUnit;
                decimal warningAmount = Convert.ToDecimal(matchRows[0][ConstSchemaNames.RecipeRuleColWarningLine]);
                decimal maxAmount = Convert.ToDecimal(matchRows[0][ConstSchemaNames.RecipeRuleColControlLine]);
                decimal baseWarningAmount;
                decimal baseMaxAmount;
                if (Convert.ToInt16(matchRows[0][ConstSchemaNames.RecipeRuleColUnitKind]) == (int)DruggeryUnitKind.Ward) // ʹ��סԺ��λ
                    usedUnit = wardUnit;
                else
                    usedUnit = dosageUnit;

                baseWarningAmount = usedUnit.Convert2BaseUnit(warningAmount);
                baseMaxAmount = usedUnit.Convert2BaseUnit(maxAmount);

                decimal trueAmount = unit.Convert2BaseUnit(amount);

                if (trueAmount > baseMaxAmount)
                    throw new DataCheckException(String.Format(ConstMessages.FormatMoreThanControlLine, maxAmount, usedUnit.Name), ConstNames.Amount, 1);
                else if (trueAmount > baseWarningAmount)
                    throw new DataCheckException(String.Format(ConstMessages.FormatMoreThanWarningLine, warningAmount, usedUnit.Name), ConstNames.Amount, 0);
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// ��鵱ǰ�������Ŀ�Ƿ����㴦������
        /// </summary>
        /// <param name="item">��Ŀ����</param>
        /// <param name="amount">����</param>
        /// <param name="unit">��ǰ����ʹ�õĵ�λ</param>
        public void CheckItem(ChargeItem item, decimal amount, ItemUnit unit) {
            if (m_HadInitialized && (item != null) && item.KeyInitialized) {
                string itemKind = Convert.ToInt32(item.Kind).ToString();
                CheckItemOrDruggery(false, itemKind, 0, item.Code);
                CheckItemAmount(0, item.Code, amount, unit, item.BaseUnit, item.BaseUnit);
            }
        }

        /// <summary>
        /// ��鵱ǰ�����ҩƷ�Ƿ����㴦������
        /// </summary>
        /// <param name="druggery">ҩƷ����</param>
        /// <param name="amount">����</param>
        /// <param name="unit">��ǰ����ʹ�õĵ�λ</param>
        public void CheckDruggery(Druggery druggery, decimal amount, ItemUnit unit) {
            if (m_HadInitialized && (druggery != null) && druggery.KeyInitialized) {
                CheckItemOrDruggery(true, druggery.Catalog.Code, druggery.ProductSerialNo, druggery.Code);
                CheckItemAmount(druggery.ProductSerialNo, druggery.Code, amount, unit, druggery.SpecUnit, druggery.WardUnit);
            }
        }
        #endregion
    }

    ///// <summary>
    ///// �й���Ŀ��������Ľṹ��
    ///// </summary>
    //public struct ItemAmountRule
    //{
    //   /// <summary>
    //   /// ��λ���
    //   /// </summary>
    //   public DruggeryUnitKind UnitKind
    //   {
    //      get { return _unitKind; }
    //   }
    //   private DruggeryUnitKind _unitKind;

    //   /// <summary>
    //   /// Ĭ�ϼ���
    //   /// </summary>
    //   public decimal DefaultAmount
    //   {
    //      get { return _defaultAmount; }
    //   }
    //   private decimal _defaultAmount;

    //   /// <summary>
    //   /// �������
    //   /// </summary>
    //   public decimal WarningAmount
    //   {
    //      get { return _warningAmount; }
    //   }
    //   private decimal _warningAmount;

    //   /// <summary>
    //   /// ���Ƽ���
    //   /// </summary>
    //   public decimal MaxAmount
    //   {
    //      get { return _maxAmount; }
    //   }
    //   private decimal _maxAmount;

    //   /// <summary>
    //   /// Ĭ������
    //   /// </summary>
    //   public decimal DefaultCount
    //   {
    //      get { return _defaultCount; }
    //   }
    //   private decimal _defaultCount;

    //   /// <summary>
    //   /// ��������
    //   /// </summary>
    //   public decimal WarningCount
    //   {
    //      get { return _warningCount; }
    //   }
    //   private decimal _warningCount;

    //   /// <summary>
    //   /// ��������
    //   /// </summary>
    //   public decimal MaxCount
    //   {
    //      get { return _maxCount; }
    //   }
    //   private decimal _maxCount;

    //   /// <summary>
    //   /// ��ע
    //   /// </summary>
    //   public string Memo
    //   {
    //      get { return _memo; }
    //   }
    //   private string _memo;

    //   public ItemAmountRule(DruggeryUnitKind unitKind, decimal defaultAmount, decimal warningAmount, decimal maxAmount, decimal defaultCount, decimal warningCount, decimal maxCount, string memo)
    //   {
    //      _unitKind = unitKind;
    //      _defaultAmount = defaultAmount;
    //      _warningAmount = warningAmount;
    //      _maxAmount = maxAmount;
    //      _defaultCount = defaultCount;
    //      _warningCount = warningCount;
    //      _maxCount = maxCount;
    //      _memo = memo;
    //   }
    //}
}
