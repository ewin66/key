using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// �������ַ�������
   /// </summary>
   internal class ConstNames
   {
      #region normal
      public const string OpSave = "����";
      public const string OpCheck = "���";
      public const string OpSend = "����";

      public const string LongOrder = "����ҽ��";
      public const string TempOrder = "��ʱҽ��";

      public const string DoctorId = "ҽ������";
      public const string Inpatient = "����";

      public const string LightDemanding = "����";
      public const string Amount = "����";

      public const string DoctorLevel = "ְ��";
      public const string Diagnose = "���";
      public const string Dept = "����";
      public const string MedicalCare = "ҽ������";

      public const string OrderUILogic = "���洦���߼�";
      public const string DataAccess = "���ݷ������";

      public const string TimeOfShiftDept = "ת��ʱ��";
      public const string TimeOfOperation = "����ʱ��";
      public const string TimeOfOutHospital = "��Ժʱ��";
      public const string TimeOfCease = "ֹͣʱ��";
      public const string TimeOfCancel = "ȡ��ʱ��";
      #endregion

      #region Order State
      public const string OrderStateAll = "<ȫ��>����";
      public const string OrderStateNew = "��ҽ��";
      public const string OrderStateAudited = "�����ҽ��";
      public const string OrderStateExecuted = "��ִ��ҽ��";
      public const string OrderStateCeased = "��ֹͣҽ��";
      public const string OrderStateCancelled = "��ȡ��ҽ��";
      #endregion

      #region Order Operation
      public const string OpDelete = "ɾ��";
      public const string OpSetGroup = "����";
      public const string OpCancelGroup = "ȡ������";
      public const string OpAudit = "���";
      public const string OpCancel = "ȡ��";
      public const string OpCease = "ֹͣҽ��";
      public const string OpMoveUp = "����";
      public const string OpMoveDown = "����";
      #endregion

      #region name of content kind
      public const string ContentDruggery = "ҩƷ";
      public const string ContentChargeItem = "��ͨ��Ŀ";
      public const string ContentGeneralItem = "������Ŀ";
      public const string ContentClinicItem = "�ٴ���Ŀ";
      public const string ContentOutDruggery = "��Ժ��ҩ";
      public const string ContentOperation = "����";
      public const string ContentTextNormal = "��ҽ��";
      public const string ContentTextShiftDept = "ת��";
      public const string ContentTextAfterOperation = "����ҽ��";
      public const string ContentTextLeaveHospital = "��Ժ";
      #endregion

      #region name of content catalog table column
      public const string ContentCatalogId = "����";
      public const string ContentCatalogName = "��������";
      public const string ContentCatalogFlag = "ҽ�������־";
      #endregion

      #region name of order output table column
      public const string OrderOutputProductSerialNo = "ҩƷ���";
      public const string OrderOutputDruggeryName = "ҩƷ����";
      public const string OrderOutputAmount = "ҩƷ����";
      public const string OrderOutputUnit = "��λ����";
      public const string OrderOutputUsageCode = "�÷�����";
      public const string OrderOutputUsageName = "�÷�����";
      public const string OrderOutputFrequencyCode = "Ƶ�δ���";
      public const string OrderOutputFrequencyName = "Ƶ������";
      public const string OrderOutPutDruggeryCode = "ҩƷ����";
      #endregion
   }
}
