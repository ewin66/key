using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using DrectSoft.Core;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ���������
   /// </summary>
   public enum DepartmentKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// �ٴ�
      /// </summary>
      Clinic = 101,
      /// <summary>
      /// ҽ��
      /// </summary>
      Technic = 102,
      /// <summary>
      /// ҩ��
      /// </summary>
      Druggery = 103,
      /// <summary>
      /// ����
      /// </summary>
      Service = 104,
      /// <summary>
      /// ����
      /// </summary>
      Other = 105
   }

   /// <summary>
   /// ���ұ�־(����ٴ�����)
   /// </summary>
   public enum DepartmentClinicKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��ͨ
      /// </summary>
      Normal = 201,
      /// <summary>
      /// ������
      /// </summary>
      Theater = 202,
      /// <summary>
      /// ����
      /// </summary>
      Delivery = 203,
      /// <summary>
      /// ICU|CCU
      /// </summary>
      ICU = 204,
      /// <summary>
      /// ����
      /// </summary>
      Pediatrics = 205
   }

   /// <summary>
   /// ������־
   /// </summary>
   public enum WardKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��ͨ
      /// </summary>
      Normal = 300,
      /// <summary>
      /// ����
      /// </summary>
      Emergency = 301,
      /// <summary>
      /// ����
      /// </summary>
      Delivery = 302,
      /// <summary>
      /// ICU|CCU
      /// </summary>
      ICU = 303
   }

   /// <summary>
   /// ְ�������
   /// </summary>
   public enum EmployeeKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��ͨҽ��
      /// </summary>
      Doctor = 400,
      /// <summary>
      /// ר��ҽ��
      /// </summary>
      Specialist = 401,
      /// <summary>
      /// ��ʿ
      /// </summary>
      Nurse = 402,
      /// <summary>
      /// ����ʦ
      /// </summary>
      Anaesthetist = 403,
      /// <summary>
      /// ����
      /// </summary>
      Others = 404
   }

   /// <summary>
   /// ҽԺ�ȼ����
   /// </summary>
   public enum HospitalGrade
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// һ��
      /// </summary>
      Level1 = 501,
      /// <summary>
      /// ����
      /// </summary>
      Level2 = 502,
      /// <summary>
      /// ����
      /// </summary>
      Level3 = 503
   }

   /// <summary>
   /// ҽԺ�����
   /// </summary>
   public enum HospitalKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��ǰϵͳʹ��ҽԺ
      /// </summary>
      Current = 600,
      /// <summary>
      /// Э��ҽԺ
      /// </summary>
      Cooperation = 601
   }

   /// <summary>
   /// ���������(���ڲ��ַ������)
   /// </summary>
   public enum DiseaseKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ������
      /// </summary>
      Single = 700,
      /// <summary>
      /// ��������
      /// </summary>
      Disease = 701,
      /// <summary>
      /// Ժ�ڷ���
      /// </summary>
      Hospital = 702
   }

   /// <summary>
   /// ����������
   /// </summary>
   public enum OperationGrade
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// �ش�
      /// </summary>
      Super = 800,
      /// <summary>
      /// ��
      /// </summary>
      Large = 801,
      /// <summary>
      /// ��
      /// </summary>
      Middle = 802,
      /// <summary>
      /// С
      /// </summary>
      Small = 803
   }

   ///// <summary>
   ///// �������������(��ǲ�������,��������)
   ///// </summary>
   //public enum DiseaseTag
   //{
   //   None = 0,
   //   /// <summary>
   //   /// ���Գۻ�����Բ�
   //   /// </summary>
   //   Paralytic = 901,
   //   /// <summary>
   //   /// ��������Ⱦ����
   //   /// </summary>
   //   Baby = 902,
   //   /// <summary>
   //   /// �����ڿƺϲ�֢
   //   /// </summary>
   //   Gestation = 903
   //}

   /// <summary>
   /// ����������
   /// </summary>
   public enum ZoneGrade
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ʡ��ֱϽ��
      /// </summary>
      Province = 1000,
      /// <summary>
      /// ����
      /// </summary>
      Country = 1001
   }

   /// <summary>
   /// ��λ���ͱ��
   /// </summary>
   public enum BedType
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��
      /// </summary>
      Male = 1100,
      /// <summary>
      /// Ů
      /// </summary>
      Female = 1101,
      /// <summary>
      /// ��
      /// </summary>
      Mix = 1102
   }

   /// <summary>
   /// ��λ�������ͱ��
   /// </summary>
   public enum BedKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// �ڱ�
      /// </summary>
      Normal = 1200,
      /// <summary>
      /// �Ǳ�
      /// </summary>
      Extra = 1201,
      /// <summary>
      /// �Ӵ�
      /// </summary>
      AddIn = 1202
   }

   /// <summary>
   /// ��λʹ��״̬
   /// </summary>
   public enum BedState
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// �մ�
      /// </summary>
      No = 1300,
      /// <summary>
      /// ռ��
      /// </summary>
      Yes = 1301,
      /// <summary>
      /// ����
      /// </summary>
      Wrapped = 1302
   }

   /// <summary>
   /// ϵͳ��־(ָ�����������õ�ϵͳ��Χ)
   /// </summary>
   public enum SystemApplyRange
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ���סԺ
      /// </summary>
      All = 1400,
      /// <summary>
      /// ���ﲿ��
      /// </summary>
      OutpatientDept = 1401,
      /// <summary>
      /// סԺ����
      /// </summary>
      InpatientDept = 1402
   }

   /// <summary>
   /// סԺ����״̬���
   /// </summary>
   public enum InpatientState
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��Ժ�Ǽ�
      /// </summary>
      New = 1500,
      /// <summary>
      /// �����ִ�
      /// </summary>
      InWard = 1501,
      /// <summary>
      /// ������Ժ
      /// </summary>
      OutWard = 1502,
      /// <summary>
      /// ���˳�Ժ
      /// </summary>
      Balanced = 1503,
      /// <summary>
      /// ȡ������
      /// </summary>
      CancleBalanced = 1504,
      /// <summary>
      /// ����ICU
      /// </summary>
      InICU = 1505,
      /// <summary>
      /// �������
      /// </summary>
      InDeliveryRoom = 1506,
      /// <summary>
      /// ת��״̬(��ת��,�Է���û�н���)
      /// </summary>
      ShiftDept = 1507,
      /// <summary>
      /// ����ת��(������Ǩ�Ƶ���ʷ����)
      /// </summary>
      DataDumped = 1508,
      /// <summary>
      /// ���ϼ�¼
      /// </summary>
      Outdated = 1509
   }

   ///// <summary>
   ///// ҽ���Ը�������־���(�ⲿ�ֱ����EMR��Ӧ���ò���,ֻ��Ϊ�˵���ҽ����������ʹ��)
   ///// </summary>
   //public enum InsurancePayRateFlags
   //{
   //   //[Description("ȱʡ�Էѱ���")]
   //   // = 1700,
   //   //[Description("ȱʡ�Żݱ���")]
   //   // = 1701,
   //   //[Description("�����Էѱ���")]
   //   // = 1702,
   //   //[Description("�����Żݱ���")]
   //   // = 1703,
   //   //[Description("ȫ�ԷѲ��˱�־")]
   //   // = 1704,
   //}

   ///// <summary>
   ///// �ʻ���־(�ⲿ�ֱ����EMR��Ӧ���ò���,ֻ��Ϊ�˵���ҽ����������ʹ��)
   ///// </summary>
   //public enum PatientAccountFlags
   //{
   //   /// <summary>
   //   /// ��Ӧ���������
   //   /// </summary>
   //   Catalog = 18,
   //   //[Description("�޸����ʻ�")]
   //   // = 1800,
   //   //[Description("�и����˻�")]
   //   // = 1801,
   //   //[Description("����Ƿ��")]
   //   // = 1802,
   //}

   ///// <summary>
   ///// ҽ�����㷽ʽ���(�ⲿ�ֱ����EMR��Ӧ���ò���,ֻ��Ϊ�˵���ҽ����������ʹ��)
   ///// </summary>
   //public enum InsuranceCalcFlags
   //{
   //   /// <summary>
   //   /// ��Ӧ���������
   //   /// </summary>
   //   Catalog = 19,
   //   //[Description("��ҽ���ܶ����")]
   //   // = 1900,
   //   //[Description("��ҽ��֧������")]
   //   // = 1901,
   //}

   /// <summary>
   /// ҽ��������
   /// </summary>
   public enum DoctorGrade
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ����ҽʦ
      /// </summary>
      Chief = 2000,
      /// <summary>
      /// ������ҽʦ
      /// </summary>
      AssociateChief = 2001,
      /// <summary>
      /// ����ҽʦ
      /// </summary>
      Attending = 2002,
      /// <summary>
      /// סԺҽʦ
      /// </summary>
      Resident = 2003,
      /// <summary>
      ///��ʿ 
      /// </summary>
      Nurse = 2004
   }

   /// <summary>
   /// SNOMED����״̬���
   /// </summary>
   public enum SnomedConceptState
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ����-��Ч
      /// </summary>
      Natural = 2100,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Retired = 2101,
      /// <summary>
      /// �ظ�-ͣ��
      /// </summary>
      Repeated = 2102,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Outdated = 2103,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Multivocal = 2104,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Wrong = 2105,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Limited = 2106,
      /// <summary>
      /// ��ת��-ͣ��
      /// </summary>
      Transferred = 2110,
      /// <summary>
      /// ���ƶ�-��Ч
      /// </summary>
      Transferring = 2111,
      /// <summary>
      /// �Զ���-��Ч
      /// </summary>
      Custom = 2150,
      /// <summary>
      /// �����Զ�����
      /// </summary>
      Cancellation = 2151
   }

   /// <summary>
   /// SNOMED����״̬���
   /// </summary>
   public enum SnomedAliasState
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ����-��Ч
      /// </summary>
      Natural = 2200,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Retired = 2201,
      /// <summary>
      /// �ظ�-ͣ��
      /// </summary>
      Repeated = 2202,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Outdated = 2203,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Wrong = 2205,
      /// <summary>
      /// ����-ͣ��
      /// </summary>
      Limited = 2206,
      /// <summary>
      /// ������-ͣ��
      /// </summary>
      Unseemliness = 2207,
      /// <summary>
      /// ������ͣ��
      /// </summary>
      ConceptCeased = 2208,
      /// <summary>
      /// ��ת��-ͣ��
      /// </summary>
      Transferred = 2210,
      /// <summary>
      /// ���ƶ�-��Ч
      /// </summary>
      Transferring = 2211
   }

   /// <summary>
   /// SNOMED�������ͱ��
   /// </summary>
   public enum SnomedTermKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// δָ��
      /// </summary>
      Unspecified = 2300,
      /// <summary>
      /// ��ѡ
      /// </summary>
      Preferred = 2301,
      /// <summary>
      /// ͬ���
      /// </summary>
      Synonymous = 2302,
      /// <summary>
      /// ȫ��
      /// </summary>
      FullName = 2303
   }

   /// <summary>
   /// ��Ŀ�����
   /// </summary>
   public enum ItemKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��ҩ
      /// </summary>
      WesternMedicine = 2401,
      /// <summary>
      /// ��ҩ
      /// </summary>
      PatentMedicine = 2402,
      /// <summary>
      /// ��ҩ
      /// </summary>
      HerbalMedicine = 2403,
      /// <summary>
      /// ����
      /// </summary>
      Cure = 2404,
      /// <summary>
      /// ����
      /// </summary>
      Operation = 2405,
      /// <summary>
      /// ����
      /// </summary>
      Anesthesia = 2406,
      /// <summary>
      /// ��ʳ
      /// </summary>
      Meal = 2407,
      /// <summary>
      /// ��Ѫ
      /// </summary>
      Transfusion = 2408,
      /// <summary>
      /// ����
      /// </summary>
      Care = 2409,
      /// <summary>
      /// ��λ
      /// </summary>
      BedFee = 2410,
      /// <summary>
      /// ���
      /// </summary>
      Examination = 2411,
      /// <summary>
      /// ����
      /// </summary>
      Assay = 2412,
      /// <summary>
      /// ��Һ
      /// </summary>
      Infusion = 2413,
      /// <summary>
      /// �Һ�
      /// </summary>
      Registration = 2414,
      /// <summary>
      /// ����
      /// </summary>
      Meterial = 2415,
      /// <summary>
      /// ����(ע�������Ƶ�����)
      /// </summary>
      Diagnosis = 2416,
      /// <summary>
      /// ����
      /// </summary>
      Other = 2417,
      /// <summary>
      /// ��
      /// </summary>
      Sugar = 2420,
      /// <summary>
      /// Σ�ؼ���
      /// </summary>
      DangerLevel = 2421,
      /// <summary>
      /// ��������
      /// </summary>
      IsolationCatalog = 2422,
      /// <summary>
      /// ��λ
      /// </summary>
      BodyPosition = 2423,
      /// <summary>
      /// �ٴ���Ŀ
      /// </summary>
      ClinicItem = 2430
   }

   /// <summary>
   /// �������
   /// </summary>
   public enum SubmitAccountKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ȫ��
      /// </summary>
      PayFull = 2500,
      /// <summary>
      /// ���ֱ�
      /// </summary>
      PayPart = 2501,
      /// <summary>
      /// �Է�
      /// </summary>
      PaySelf = 2502,
      /// <summary>
      /// ����
      /// </summary>
      Other = 2503
   }

   /// <summary>
   /// ҩƷ���
   /// </summary>
   public enum DruggeryKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��ͨ
      /// </summary>
      Normal = 2600,
      /// <summary>
      /// ����
      /// </summary>
      Anesthetics = 2601,
      /// <summary>
      /// ����
      /// </summary>
      Psychosis = 2602,
      /// <summary>
      /// �綾
      /// </summary>
      Virulent = 2603,
      /// <summary>
      /// Σ��
      /// </summary>
      Danger = 2604,
      /// <summary>
      /// ����
      /// </summary>
      Reagent = 2605,
      /// <summary>
      /// �ȵ���
      /// </summary>
      Insulin = 2606,
      /// <summary>
      /// ������
      /// </summary>
      Antibiotics = 2609
   }

   /// <summary>
   /// ҽ�������־
   /// </summary>
   public enum OrderManagerKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��ͨ
      /// </summary>
      Normal = 2700,
      /// <summary>
      /// ������ҽ��
      /// </summary>
      NotUse = 2701,
      /// <summary>
      /// ֻ������ʱҽ��
      /// </summary>
      ForTemp = 2702,
      /// <summary>
      /// ֻ���ڳ���ҽ��
      /// </summary>
      ForLong = 2703
   }

   /// <summary>
   /// ��Ŀʹ�÷�Χ���
   /// </summary>
   public enum ItemApplyRange
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ������
      /// </summary>
      Unlimited = 2800,
      /// <summary>
      /// �Է�ʹ��
      /// </summary>
      ForNormal = 2801,
      /// <summary>
      /// ҽ��ʹ��
      /// </summary>
      ForInsurance = 2802
   }

   /// <summary>
   /// ʹ�÷�Χ���Ʊ��(���ݵ�Ӧ�÷�Χ)
   /// </summary>
   public enum DataApplyRange
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ȫԺ
      /// </summary>
      All = 2900,
      /// <summary>
      /// ����
      /// </summary>
      Department = 2901,
      /// <summary>
      /// ����
      /// </summary>
      Ward = 2902,
      /// <summary>
      /// ����
      /// </summary>
      Individual = 2903
   }

   /// <summary>
   /// ҩƷ��λ���
   /// </summary>
   public enum DruggeryUnitKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ������λ
      /// </summary>
      Dosage = 3000,
      /// <summary>
      /// ҩ�ⵥλ
      /// </summary>
      Depot = 3001,
      /// <summary>
      /// ���ﵥλ
      /// </summary>
      Policlinic = 3002,
      /// <summary>
      /// סԺ��λ
      /// </summary>
      Ward = 3003,
      /// <summary>
      /// ������λ
      /// </summary>
      Stock = 3004,
      /// <summary>
      /// ���Ƶ�λ
      /// </summary>
      Paediatrics = 3005,
      /// <summary>
      /// ���λ
      /// </summary>
      Specification = 3006,
      /// <summary>
      /// ��С��λ
      /// </summary>
      Min = 3007
   }

   /// <summary>
   /// ҽ�������
   /// </summary>
   [TypeConverter(typeof(LocalizedEnumConverter))]
   public enum OrderContentKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ҩƷҽ��
      /// </summary>
      Druggery = 3100,
      /// <summary>
      /// �շ���Ŀҽ��
      /// </summary>
      ChargeItem = 3101,
      /// <summary>
      /// ����ҽ��
      /// </summary>
      GeneralItem = 3102,
      /// <summary>
      /// �ٴ���Ŀҽ��
      /// </summary>
      ClinicItem = 3103,
      /// <summary>
      /// ��Ժ��ҩ
      /// </summary>
      OutDruggery = 3104,
      /// <summary>
      /// ����ҽ��
      /// </summary>
      Operation = 3105,
      /// <summary>
      /// ͣ����ҽ��
      /// </summary>
      CeaseLong = 3109,
      /// <summary>
      /// ��ҽ��(��ͨ)
      /// </summary>
      TextNormal = 3110,
      /// <summary>
      /// ��ҽ��(ת��)
      /// </summary>
      TextShiftDept = 3111,
      /// <summary>
      /// ��ҽ��(����)
      /// </summary>
      TextAfterOperation = 3112,
      /// <summary>
      /// ��ҽ��(��Ժ)
      /// </summary>
      TextLeaveHospital = 3113
   }

   /// <summary>
   /// ҽ��״̬���
   /// </summary>
   public enum OrderState
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ȫ��
      /// </summary>
      All = 9999,
      /// <summary>
      /// ¼��
      /// </summary>
      New = 3200,
      /// <summary>
      /// �����
      /// </summary>
      Audited = 3201,
      /// <summary>
      /// ��ִ��
      /// </summary>
      Executed = 3202,
      /// <summary>
      /// ��ȡ��
      /// </summary>
      Cancellation = 3203,
      /// <summary>
      /// ��ֹͣ
      /// </summary>
      Ceased = 3204
   }

   /// <summary>
   /// ����ҽ��ֹͣԭ����
   /// </summary>
   public enum OrderCeaseReason
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ����ͣ
      /// </summary>
      Natural = 3300,
      /// <summary>
      /// ����
      /// </summary>
      AfterOperation = 3301,
      /// <summary>
      /// ����
      /// </summary>
      AfterDelivery = 3302,
      /// <summary>
      /// ת��
      /// </summary>
      ShiftDept = 3303,
      /// <summary>
      /// �³���
      /// </summary>
      NewGeneral = 3305,
      /// <summary>
      /// ��Ժ
      /// </summary>
      LeaveHospital = 3306
   }

   /// <summary>
   /// ҽ��ִ������(��λ)���
   /// </summary>
   public enum OrderExecPeriodUnitKind
   {
      /// <summary>
      /// δ֪
      /// </summary>
      None = 0,
      /// <summary>
      /// ��
      /// </summary>
      Week = 3400,
      /// <summary>
      /// ��
      /// </summary>
      Day = 3401,
      /// <summary>
      /// Сʱ
      /// </summary>
      Hour = 3402,
      /// <summary>
      /// ����
      /// </summary>
      Minute = 3403
   }

   /// <summary>
   /// ����λ������
   /// </summary>
   public enum GroupPositionKind
   {
      /// <summary>
      /// �޷�ʶ��
      /// </summary>
      None = 0,
      /// <summary>
      /// ����ҽ��
      /// </summary>
      SingleOrder = 3500,
      /// <summary>
      /// �鿪ʼ
      /// </summary>
      GroupStart = 3501,
      /// <summary>
      /// ���м�
      /// </summary>
      GroupMiddle = 3502,
      /// <summary>
      /// �����
      /// </summary>
      GroupEnd = 3599
   }

   /// <summary>
   /// ��ӡ״̬
   /// </summary>
   public enum PrintState
   {
      /// <summary>
      /// �޷�ʶ��
      /// </summary>
      None = 0,
      /// <summary>
      /// δ��ӡ
      /// </summary>
      IsNotPrinted = 3600,
      /// <summary>
      /// �Ѵ�ӡ
      /// </summary>
      HadPrinted = 3601,
      /// <summary>
      /// ��ӡ���޸�
      /// </summary>
      ChangedAfterPrint = 3602
   }

   /// <summary>
   /// ��Ϣ״̬
   /// </summary>
   public enum MessageState
   {
      /// <summary>
      /// �޷�ʶ��
      /// </summary>
      None = 0,
      /// <summary>
      /// ��ʼ������
      /// </summary>
      Waiting = 3700,
      /// <summary>
      /// ������
      /// </summary>
      Handling = 3701,
      /// <summary>
      /// �������
      /// </summary>
      Handled = 3702
   }

   /// <summary>
   /// ����ģ������
   /// </summary>
   public enum ModelKind
   {
      /// <summary>
      /// ԭ������
      /// </summary>
      Atom = 3800,
      /// <summary>
      /// Ԫ����
      /// </summary>
      MetaData = 3801,
      /// <summary>
      /// ����ģ��
      /// </summary>
      BaseModel = 3802,
      /// <summary>
      /// �ļ��ṹ
      /// </summary>
      FileStructure = 3803,
      /// <summary>
      /// �����ļ�
      /// </summary>
      File = 3804,
      /// <summary>
      /// ����ģ��
      /// </summary>
      FullModel = 3805,
      /// <summary>
      /// ���
      /// </summary>
      Grid = 3806,
      /// <summary>
      /// ͼ��
      /// </summary>
      Picture = 3807,
      /// <summary>
      /// �����ļ���
      /// </summary>
      Folder = 3808,
      /// <summary>
      /// ����ģ��
      /// </summary>
      PersonalModel = 3809
   }

   /// <summary>
   /// ҽ��ȷ��״̬
   /// </summary>
   public enum TechAffirmState
   {
      /// <summary>
      /// ����
      /// </summary>
      New = 4100,
      /// <summary>
      /// ���
      /// </summary>
      Audited = 4101,
      /// <summary>
      /// ִ��
      /// </summary>
      Executed = 4102,
      /// <summary>
      /// ҽ��ȷ��
      /// </summary>
      Affirmed = 4103,
      /// <summary>
      /// ҽ������
      /// </summary>
      Abolished = 4104,
      /// <summary>
      /// ҽ���ܾ�
      /// </summary>
      Rejected = 4105,
      /// <summary>
      /// ��˺�ȡ��
      /// </summary>
      Cancelled = 4106
   }

   /// <summary>
   /// ��ӡԭ��
   /// </summary>
   public enum PrintReason
   {
      /// <summary>
      /// ��
      /// </summary>
      None = 0,
      /// <summary>
      /// ����
      /// </summary>
      Continue = 4200,
      /// <summary>
      /// ������ӡ
      /// </summary>
      All = 4201,
      /// <summary>
      /// ��һ�δ�ӡ
      /// </summary>
      FirstTime = 4202,
      /// <summary>
      /// ֽ����
      /// </summary>
      PageDamaged = 4203,
      /// <summary>
      /// �Ѵ�ӡ���ֱ��޸�
      /// </summary>
      PrintedChanged = 4204,
      /// <summary>
      /// Ԥ����ӡ
      /// </summary>
      PreviewPrint = 4205,
      /// <summary>
      /// ��������
      /// </summary>
      Other = 4206
   }

   /// <summary>
   /// ���ݲ�������
   /// </summary>
   public enum DataOperator
   {
      /// <summary>
      /// ����
      /// </summary>
      New = 4301,
      /// <summary>
      /// �޸�
      /// </summary>
      Modiffy = 4302,
      /// <summary>
      /// ɾ��
      /// </summary>
      Delete = 4303,
      /// <summary>
      /// �ύ
      /// </summary>
      Submit = 4304,
      /// <summary>
      /// ���
      /// </summary>
      Audit = 4305,
      /// <summary>
      /// �����ύ
      /// </summary>
      DischargeSubmit = 4306
   }

   /// <summary>
   /// �����������
   /// </summary>
   public enum PhysicalSignKind
   {
      /// <summary>
      /// ����
      /// </summary>
      Temperature = 4401,
      /// <summary>
      /// ����
      /// </summary>
      Sphygmus = 4402,
      /// <summary>
      /// ����
      /// </summary>
      HeartRate = 4403,
      /// <summary>
      /// Ѫѹ
      /// </summary>
      BloodPressure = 4404,
      /// <summary>
      /// ��������
      /// </summary>
      Breathe = 4405,
      /// <summary>
      /// ������
      /// </summary>
      Stool = 4406,
      /// <summary>
      /// ���
      /// </summary>
      Height = 4407,
      /// <summary>
      /// ����
      /// </summary>
      Weight = 4408,
      /// <summary>
      /// ��Χ
      /// </summary>
      GirthOfPaunch = 4409,
      /// <summary>
      /// �ܳ���
      /// </summary>
      TotalOutput = 4410,
      /// <summary>
      /// ����
      /// </summary>
      UrinaQuantity = 4411,
      /// <summary>
      /// ̵��
      /// </summary>
      SputumQuantity = 4412,
      /// <summary>
      /// Ż����
      /// </summary>
      VomitQuantity = 4413,
      /// <summary>
      /// ������
      /// </summary>
      ConductionQuantity = 4414,
      /// <summary>
      /// ������
      /// </summary>
      TotalInput = 4419,
      /// <summary>
      /// ����
      /// </summary>
      TongueMark = 4430,
      /// <summary>
      /// ����
      /// </summary>
      SphygmusMark = 4431
   }

   /// <summary>
   /// ҩƷ�÷����
   /// </summary>
   public enum DragUsageKind
   {
      /// <summary>
      /// ��ͨ
      /// </summary>
      Normal = 4500,
      /// <summary>
      /// �ڷ�
      /// </summary>
      PO = 4501,
      /// <summary>
      /// ��Һ
      /// </summary>
      Transfusion = 4502,
      /// <summary>
      /// ���
      /// </summary>
      Ampule = 4503
   }

   /// <summary>
   /// ����״̬
   /// </summary>
   public enum ExamineState
   {
      /// <summary>
      /// δ�ύ,
      /// </summary>
      NotSubmit = 4600,
      /// <summary>
      /// �ύ��δ���,
      /// </summary>
      SubmitButNotExamine = 4601,
      /// <summary>
      /// �������,
      /// </summary>
      FirstExamine = 4602,
      /// <summary>
      /// �������
      /// </summary>
      SecondExamine = 4603,
      /// <summary>
      /// ��ɾ��
      /// </summary>
      Deleted = 4604,
      /// <summary>
      /// ���������
      /// </summary>
      ThirdExamine = 4610,
      /// <summary>
      /// ���Ĵ����
      /// </summary>
      FouthExamine = 4611,
      /// <summary>
      /// ��������,
      /// </summary>
      FifthExamine = 4612,
      /// <summary>
      /// �������
      /// </summary>
      Final = 4613
   }
}
