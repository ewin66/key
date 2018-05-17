using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using DrectSoft.Common.Eop;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// סԺ������Ϣ��
   /// </summary>
   public class AdmissionBasicInfo : EPBaseObject
   {
      #region properties
      /// <summary>
      /// ����
      /// </summary>
      public Department CurrentDepartment
      {
         get { return _currentDepartment; }
         set { _currentDepartment = value; }
      }
      private Department _currentDepartment;

      /// <summary>
      /// ����
      /// </summary>
      public Ward CurrentWard
      {
         get { return _currentWard; }
         set { _currentWard = value; }
      }
      private Ward _currentWard;

      /// <summary>
      /// ����
      /// </summary>
      public string BedNo
      {
         get { return _bedNo; }
         set { _bedNo = value; }
      }
      private string _bedNo;

      /// <summary>
      /// �����һ�׶ε�ʱ��(��Ժ�����)
      /// </summary>
      public DateTime StepOneDate
      {
         get { return _stepOneDate; }
         set { _stepOneDate = value; }
      }
      private DateTime _stepOneDate;

      /// <summary>
      /// ����ڶ��׶ε�ʱ��(�������Ժ)
      /// </summary>
      public DateTime StepTwoDate
      {
         get { return _stepTwoDate; }
         set { _stepTwoDate = value; }
      }
      private DateTime _stepTwoDate;

      /// <summary>
      /// ���
      /// </summary>
      public ICDDiagnosis Diagnosis
      {
         get { return _diagnosis; }
         set { _diagnosis = value; }
      }
      private ICDDiagnosis _diagnosis;

      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get { throw new NotImplementedException(); }
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
      /// </summary>
      public override string FilterCondition
      {
         get { throw new NotImplementedException(); }
      }
      #endregion

      #region ctors
      /// <summary>
      /// ��ʼ����ʵ��
      /// </summary>
      public AdmissionBasicInfo()
         : base()
      { }

      /// <summary>
      /// ��DataRow��ʼ��ʵ��
      /// </summary>
      /// <param name="sourceRow"></param>
      public AdmissionBasicInfo(DataRow sourceRow)
         : base(sourceRow)
      { }
      #endregion

      /// <summary>
      /// ��ʼ�����е����ԣ������������͵������Լ�������
      /// </summary>
      public override void ReInitializeAllProperties()
      {
          if (CurrentDepartment != null)
          {
              CurrentDepartment = new Department();
              CurrentDepartment.ReInitializeAllProperties();
          }
         if (CurrentWard != null)
            CurrentWard.ReInitializeAllProperties();
         if (Diagnosis != null)
            Diagnosis.ReInitializeAllProperties();
      }
   }
}
