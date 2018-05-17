using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace DrectSoft.Common.MediComIntf
{
   /// <summary>
   /// ������ɸ�����(����)
   /// </summary>
   public class PassCheckHelper
   {
      //����վ����: 10 ҽ������վ, 20 ҩѧ����վ
      const int CstWorkStationType = 10;

      string _deptId;
      string _deptName;
      string _operatorId;
      string _operatorName;
      string _errorMessage = string.Empty;
      MediIntfPatientInfo _currentPatientInfo;
      IList<MediIntfDrugInfo> _currentDrugInfos = new List<MediIntfDrugInfo>();
      string _currentDrugIndex;

      /// <summary>
      /// ��ǰ��ҩƷ����(Ĭ�ϲ����к�)
      /// </summary>
      public string CurrentDrugIndex
      {
         get { return _currentDrugIndex; }
         set { _currentDrugIndex = value; }
      }

      /// <summary>
      /// 
      /// </summary>
      public IList<MediIntfDrugInfo> CurrentDrugInfos
      {
         get { return _currentDrugInfos; }
      }

      /// <summary>
      /// 
      /// </summary>
      public string ErrorMessage
      {
         get { return _errorMessage; }
      }

      /// <summary>
      /// 
      /// </summary>
      public MediIntfPatientInfo CurrentPatientInfo
      {
         get { return _currentPatientInfo; }
      }

      /// <summary>
      /// ����,����ʼ��
      /// </summary>
      public PassCheckHelper(string deptId, string deptName, string operatorId, string operatorName)
      {
         _deptId = deptId;
         _deptName = deptName;
         _operatorId = operatorId;
         _operatorName = operatorName;
      }

      /// <summary>
      /// �Ƿ��Ѿ������˲�����Ϣ
      /// </summary>
      /// <returns></returns>
      public bool HasPatient()
      {
         return !string.IsNullOrEmpty(_currentPatientInfo.PatientID);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public bool PassRegisterAndInit()
      {
         int ret = -1;
         ret = NativeMethods.RegisterServer();
         if (ret != 0)
         {
            _errorMessage = "����ģʽע��ʧ��!";
            Trace.WriteLine(_errorMessage);
            return false;
         }
         ret = NativeMethods.PassInit(_deptId + "/" + _deptName, _operatorId + "/" + _operatorName, CstWorkStationType);
         if (ret == 0)
         {
            _errorMessage = "��ʼ��Passʧ��!";
            Trace.WriteLine(_errorMessage);
            return false;
         }
         if (PassEnable())
            NativeMethods.PassSetControlParam(1, 2, 0, 2, 1);
         return true;
      }

      /// <summary>
      /// Pass�����Ƿ����
      /// </summary>
      /// <returns></returns>
      public bool PassEnable()
      {
         return (NativeMethods.PassGetState("PassEnable") != 0);
      }

      /// <summary>
      ///
      /// </summary>
      public bool PassPopMenuEnable(string menuCode)
      {
         return (NativeMethods.PassGetState(menuCode) != 0);
      }

      /// <summary>
      ///
      /// </summary>
      public void PassSetPatient(MediIntfPatientInfo patient)
      {
         if (!PassEnable())
         {
            _errorMessage = "�ӿ����Ӳ�����!";
            Trace.WriteLine(_errorMessage);
            return;
         }

         NativeMethods.PassSetPatientInfo(patient.PatientID,
             patient.VisitID, patient.PatientName, patient.Sex,
             patient.Birthday, patient.Weight, patient.Height,
             patient.DeptName, patient.Doctor, "");
         _currentPatientInfo = patient;
      }

      /// <summary>
      ///
      /// </summary>
      public bool PassSetRecipeInfos(IList<MediIntfDrugInfo> drugs)
      {
         bool ret = true;
         _currentDrugInfos.Clear();
         for (int i = 0; i < drugs.Count; i++)
         {
            ret = ret & PassSetRecipeInfo(i.ToString(), drugs[i]);
            _currentDrugInfos.Add(drugs[i]);
         }
         return ret;
      }

      /// <summary>
      ///
      /// </summary>
      public bool PassSetRecipeInfo(string index, MediIntfDrugInfo drug)
      {
         if (!PassEnable())
         {
            _errorMessage = "�ӿ����Ӳ�����!";
            Trace.WriteLine(_errorMessage);
            return false;
         }
         if (string.IsNullOrEmpty(_currentPatientInfo.PatientID))
         {
            _errorMessage = "��Ҫ���벡����Ϣ!";
            Trace.WriteLine(_errorMessage);
            return false;
         }
         int ret = NativeMethods.PassSetRecipeInfo(index,
             drug.DrugCode, drug.DrugName, drug.SingleDose,
             drug.DoseUnit, drug.Frequency, drug.StartDate, drug.EndDate,
             drug.RouteName, drug.GroupTag, drug.OrderType,
             drug.OrderDoctorId + "/" + drug.OrderDoctorName);
         _currentDrugIndex = index;
         return true;
      }

      /// <summary>
      /// �����ӿڼ��,��ǰ�Ĳ�����Ϣ��ҩƷ��Ϣ
      /// </summary>
      public void DoPassCheck(PassCheckType checkType)
      {
         if (!PassEnable())
         {
            _errorMessage = "�ӿ����Ӳ�����!";
            Trace.WriteLine(_errorMessage);
            return;
         }

         if (!HasPatient())
         {
            _errorMessage = "��Ҫ���벡����Ϣ!";
            Trace.WriteLine(_errorMessage);
            return;
         }

         if (_currentDrugInfos == null || _currentDrugInfos.Count == 0)
         {
            _errorMessage = "��Ҫ����ҩƷ��Ϣ/����ҩƷ��Ϣ���!";
            Trace.WriteLine(_errorMessage);
            return;
         }

         int commandId = (int)checkType;

         NativeMethods.PassDoCommand(commandId);

         for (int i = 0; i < _currentDrugInfos.Count; i++)
         {
            int ret = NativeMethods.PassGetWarn(i.ToString());
            MediIntfDrugInfo drug = _currentDrugInfos[i];
            drug.Warn = (PassWarnType)ret;
         }
      }

      /// <summary>
      ///
      /// </summary>
      public void DoCommand(int commandId)
      {
         if (!PassEnable())
         {
            _errorMessage = "�ӿ����Ӳ�����!";
            Trace.WriteLine(_errorMessage);
            return;
         }
         NativeMethods.PassDoCommand(commandId);
      }

      /// <summary>
      ///
      /// </summary>
      public void PassSetWarnDrug(string index)
      {
         if (!PassEnable())
         {
            _errorMessage = "�ӿ����Ӳ�����!";
            Trace.WriteLine(_errorMessage);
            return;
         }
         NativeMethods.PassSetWarnDrug(index);
      }

      /// <summary>
      ///
      /// </summary>
      public System.Drawing.Bitmap GetWarnBmp(PassWarnType warnType)
      {
         switch (warnType)
         {
            case PassWarnType.Error:
            case PassWarnType.Filter:
            case PassWarnType.NoWatch:
               return null;
            case PassWarnType.Pass:
               return Properties.Resources._0;
            case PassWarnType.Lower:
               return Properties.Resources._1;
            case PassWarnType.Normal:
               return Properties.Resources._4;
            case PassWarnType.Higher:
               return Properties.Resources._2;
            case PassWarnType.Critical:
               return Properties.Resources._3;
            default:
               return null;
         }
      }

      /// <summary>
      /// ��ѯ��ҩ��Ϣ
      /// </summary>
      /// <param name="drugCode">ҩƷ����</param>
      /// <param name="drugName">ҩƷ����</param>
      /// <param name="doseUnit">������λ</param>
      /// <param name="routeName">�÷�����</param>
      /// <param name="leftTop"></param>
      /// <param name="rightBottom"></param>
      public void PassQueryDrugInfo(string drugCode, string drugName,
          string doseUnit, string routeName,
          Point leftTop, Point rightBottom)
      {
         if (!PassEnable())
         {
            _errorMessage = "�ӿ����Ӳ�����!";
            Trace.WriteLine(_errorMessage);
            return;
         }
         int ret;
         ret = NativeMethods.PassDoCommand(402);
         ret = NativeMethods.PassSetQueryDrug(drugCode, drugName, doseUnit, routeName);
         ret = NativeMethods.PassSetFloatWinPos(leftTop.X, leftTop.Y, rightBottom.X, rightBottom.Y);
         ret = NativeMethods.PassDoCommand(401);
      }
   }

   /// <summary>
   /// �����ӿڷ��صľ��漶��,ע������˳�������ֲ���ȫһ��
   /// </summary>
   public enum PassWarnType
   {
      /// <summary>
      /// 
      /// </summary>
      Error = -3,
      /// <summary>
      /// 
      /// </summary>
      Filter = -2,
      /// <summary>
      /// 
      /// </summary>
      NoWatch = -1,
      /// <summary>
      /// 
      /// </summary>
      Pass = 0,
      /// <summary>
      /// 
      /// </summary>
      Lower = 1,
      /// <summary>
      /// 
      /// </summary>
      Normal = 4,
      /// <summary>
      /// 
      /// </summary>
      Higher = 2,
      /// <summary>
      /// 
      /// </summary>
      Critical = 3,
   }

   /// <summary>
   /// �������
   /// </summary>
   public enum PassCheckType
   {
      /// <summary>
      /// סԺҽ��վ�����Զ����
      /// </summary>
      HospSaveAuto = 1,
      /// <summary>
      /// סԺҽ��վ�ύ�Զ����
      /// </summary>
      HospSubmitAuto = 2,
      /// <summary>
      /// �ֹ����
      /// </summary>
      ManulCheck = 3,
      /// <summary>
      /// ����ҽ��վ�����Զ����
      /// </summary>
      RecipeSaveAuto = 33,
      /// <summary>
      /// ����ҽ��վ�ύ�Զ����
      /// </summary>
      RecipeSubmitAuto = 44,
   }
}
