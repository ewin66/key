using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.DoctorAdvice
{
   internal static class ConstMessages
   {
      #region hint
      public const string OpHintAddNewOrder = "����������ҽ������";
      public const string OpHintModifyData = "�����޸�ҽ������";
      //private const string OpHintDonotAllowEdit = "{0}�����ܱ༭ҽ��";
      public const string HintInitData = "��ʼ���������ݡ���";
      public const string HintInitSkinTestResult = "��ȡƤ����Ϣ����";
      public const string HintInitMedicom = "��ʼ���������ģ�顭��";
      public const string HintReadOrderData = "��ȡ���˵�ҽ�����ݡ���";
      #endregion

      #region exception title
      public const string ExceptionTitleOrder = "ҽ��";
      public const string ExceptionTitleOrderTable = "ҽ���б�";
      #endregion

      #region normal exception
      public const string ExceptionNotOrderEdit = "��ҽ��ģʽ�����ܵ��ô˷���";
      public const string ExceptionNotOrderSuitEdit = "�ǳ���ҽ��ά��ģʽ�����ܵ��ô˷���";
      public const string ExceptionCantInsertOrder = "������ǰλ�ò���ҽ����������ˢ������";
      public const string ExceptionFailedSendDataToMedicom = "�������ҩģ�鴫��ҽ�����ݳ���";
      public const string ExceptionGroupSerialNoError = "��������д���";
      public const string ExceptionGroupStateError = "����״̬����ȷ";
      public const string ExceptionNullOrderObject = "�յ�ҽ������";
      public const string ExceptionNullOrderTable = "�յ�ҽ�������";
      public const string ExceptionOrderIndexNotFind = "ָ����ҽ����Ų�������";
      public const string ExceptionOrderNotFind = "δ�ҵ�ָ��ҽ��";
      public const string ExceptionOrginalOrderNotFind = "δ�ҵ�ԭʼ��¼";
      public const string ExceptionOutOfListRange = "�ƶ���λ�ó������б�ķ�Χ";
      public const string ExceptionHaveManyMatchRows = "���ڶ���ƥ�����";
      public const string ExceptionNotAllowAddNew = "�������������";
      public const string ExceptionNotAllowDeleteOrder = "������ɾ��ҽ������";
      public const string ExceptionAddError = "���ʧ��";
      public const string ExceptionInsertError = "����ʧ��";
      public const string ExceptionCallRemoting = "���ýӿڷ�������쳣";
      public const string ExceptionCantReadRecipteRules = "��ȡ�����������ó����쳣";
      #endregion

      #region format exception
      public const string ExceptionFormatNotFindBand = "δ�ҵ�ָ���ı�������{0}";
      public const string ExceptionFormatNotFindColumn = "δ�ҵ�ָ������: {0}";
      public const string ExceptionFormatNullObject = "�ն���{0}";
      public const string ExceptionFormatNoValue = "{0}δ��ֵ";
      #endregion

      #region normal message
      public const string MsgPromptingSaveData = "�������޸ģ��Ƿ񱣴棿";
      public const string MsgPromptingSendData = "��һλ������δ���͵�ҽ�����ݣ������Ƿ��ͣ�";
      public const string MsgSaveDataAfterModified = "���޸�ҽ�������±�������";
      public const string MsgSuccessSendData = "�����ɹ�������޸ĵ������ѷ��͵���ʿվ";
      public const string MsgNotFindSkinTestResult = "δ�ҵ�Ƥ����Ϣ������ϵ��ʿ��Ƥ��";
      public const string MsgSkinRestResultIsPlus = "Ƥ�Խ�������ԣ���������";
      public const string MsgCanntGetRecipeRuleData = "���ܶ�ȡ�����������ݣ��������޷����ӵ�HIS��";
      
      public const string FailedSaveData = "ҽ������ʧ��";
      public const string FailedSaveSuiteDetail = "����ҽ����ϸ���ݱ���ʧ��";
      public const string FailedSendDataToHis = "ҽ������ʧ��";
      public const string FailedInitMedicom = "��ʼ���������ģ������������ģ�齫������";

      public const string CheckSelectedTooManyExecuteDate = "ѡ�еġ�ִ�����ڡ�����̫�࣬����Ľ���ȥ��";
      public const string CheckSelectedTooFewExecuteDate = "ѡ�еġ�ִ�����ڡ�����̫�٣����ָ���Ĭ��ֵ";
      public const string CheckSelectedTooManyExecuteTime = "ѡ�еġ�ִ��ʱ�䡱����̫�࣬����Ľ���ȥ��";
      public const string CheckSelectedTooFewExecuteTime = "ѡ�еġ�ִ��ʱ�䡱����̫�٣����ָ���Ĭ��ֵ";
      public const string CheckStartDateNull = "��ʼ���ڲ���Ϊ��";
      public const string CheckStartDateBeforPreRow = "��ʼ���ڲ�������һ������ҽ��֮ǰ";
      public const string CheckCeaseDateBeforeStartDate = "ֹͣʱ�䲻���ڵ�ǰʱ���ҽ����ʼʱ��֮ǰ";
      public const string CheckCeaseDateBeforeNow = "ֹͣʱ�䲻��С�ڵ�ǰ����";
      public const string CheckOnlyAllowDruggery = "��Ժҽ���ѿ���ֻ�����ҩƷ";
      public const string CheckCatalogNotSupport = "��ǰҽ�����֧�ִ����͵�����";
      public const string CheckNotAllowMixCatalogInSuite = "������һ��������ͬʱ��ӳ�Ժ��ҩ����������ҽ��";
      public const string CheckItemRepeatedInNew = "��ҽ���������ҽ���д�����ͬ��Ŀ��ҽ��";
      public const string CheckItemRepeatedInExecuting = "��ǰ��Ч�ĳ���ҽ�����Ѵ�����ͬ��Ŀ��ҽ��";
      public const string CheckAllIsNew = "ȫ��������ҽ��";
      public const string CheckDelAllOutOrder = "����Ժҽ�����͡���Ժ��ҩ��ҽ��Ҫͬʱɾ��";
      public const string CheckOnlyOneRowInGroup = "һ��ҽ������Ҫ����";
      public const string CheckNewInGroup = "ȫ��������ҩƷҽ�����ҿ�ʼʱ�䡢�÷���Ƶ�ζ�һ��";
      public const string CheckSerialInGroup = "��������ҽ�����������ѷ���ļ�¼";
      public const string CheckCancelGroup = "ֻ�ܸı�����ҽ���ķ���״̬";
      public const string CheckAudit = "ͬ���ҽ�����ֿܷ����";
      public const string CheckAllIsAudited = "ȫ���������ҽ��";
      public const string CheckCancel = "ͬ���ҽ������һ��ȡ��";
      public const string CheckCeaseTimeIsNull = "ֻ�ܶ�û��ֹͣʱ��ĳ���ҽ������ֹͣʱ��";
      public const string CheckInsertRowInGroup = "�������ѳ����ҽ���в�������";
      public const string CheckPropertyInGroup = "�����в���������俪ʼʱ�䡢Ƶ�Ρ��÷�����һ��";
      public const string CheckNotAllowInsertFerbInGroup = "���������в����ҩ����";
      public const string CheckOnlyAllowInsertFerbInGroup = "ֻ�������в����ҩ����";
      public const string CheckOnlyAllowDruggeryInGroup = "ֻ�������в���ҩƷ����";
      public const string CheckOrderStateBeforeSave = "ҽ�������Ѿ������仯����ˢ�����ݣ�ˢ�º�����������޸Ľ���ʧ��\r\n"
                        + "    ���ݸı��ԭ������ǻ�ʿվ�Ѿ���˻�ִ������ҽ��\r\n"
                        + "    ��������վ�޸��˸Ĳ��˵����뵥����";
      public const string CheckNumberOfSynchedOrder = "Ҫͬ����ŵ�ҽ��������ƥ��";
      public const string CheckEditableOfOrderCatalog = "��ǰҽ���������༭";
      public const string CheckCanInsertOrderAtSpecialState = "ѡ�����ض�״̬��ҽ�������ܲ�����ҽ��";
      public const string CheckCantAddNewAfterHasShiftDeptOrder = "ת��ҽ���ѿ������ܲ�����ҽ��";
      public const string CheckOnlyAllowDruggeryAfterHasOutHospitalOrder = "��Ժҽ���ѿ���ֻ������ӳ�Ժ��ҩ";
      public const string CheckCanInsertOrderAfterCurrent = "��ǰҽ���ĺ��治�������ҽ��";
      public const string CheckCanInsertOrderBeforeOutHospitalOrder = "�������ڡ���Ժҽ����ǰ�����ҽ��";
      public const string CheckRecipeRuleDoctorLevel = "����ҩƷ����Ŀ��ʹ��Ȩ��ְ�����ƣ�����ʹ�ã�";
      public const string CheckRecipeRuleDiagnose = "����ҩƷ����Ŀ��ʹ��Ȩ�в���������ƣ�����ʹ�ã�";
      public const string CheckRecipeRuleDept = "������û��Ȩ��ʹ�ø���ҩƷ����Ŀ��";
      public const string CheckRecipeRuleMedicalCare = "����ҩƷ����Ŀ��ʹ��Ȩ��ҽ���������ƣ�����ʹ�ã�";
      public const string CheckOrderSelectionRange = "ҽ����Χ����ȷ";
      public const string CheckSelectedRangWithDataRow = "ѡ�е�����ʵ�ʼ�¼��ƥ��";

      public const string CheckSuiteData = "����������Ϊ�����Ŀ��ҩƷ�����Ƿ���ȷ��\r\n����ı���ͬʱ����";

      public const string ConditionMoveUp = "ȫ��������ҽ�����Ҽ�¼������\r\n��һ��Ҳ������ҽ��";
      public const string ConditionMoveDown = "ȫ��������ҽ�����Ҽ�¼������\r\n��һ��Ҳ������ҽ��";
      #endregion

      #region format message
      public const string FormatMedicomCheckNotPass = "������ҩģ���� {0} δͨ����ȷ��Ҫ����������";
      public const string FormatOrderSaveWarning = "{0}�д����������⣺\r\n {1} \r\n�Ƿ������";
      public const string FormatRangOfCount = "�����ķ�Χ�� {0} �� {1}";

      public const string FormatOpError = "���ܶ�ѡ���н���{0}����������������������{0}��\r\n{1}";
      public const string FormatStartDateMustAfter = "��ʼ���ڱ�����{0:yyyy-M-d HH:mm}֮��";
      public const string FormatStartDateMustBefore = "��ʼ������{0:yyyy-M-d HH:mm}֮ǰ";

      public const string FormatMoreThanControlLine = "��ǰ����������������(������:{0} {1})";
      public const string FormatMoreThanWarningLine = "��ǰ����������������(������:{0} {1})";

      public const string FormatItemAgeWarning = "���� {0} �����µĲ��˲�����ʹ�ô���Ŀ";
      #endregion
   }
}
