using System;
using DrectSoft.Common.Eop;
using DrectSoft.Emr.Util;


namespace DrectSoft.Core.EMR_NursingDocument.EMRInput.Table
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IEmrModelPermision
    {
        bool CanDo(EmrModel model);
        bool CanDo(EmrModel model, out string msg);
    }

    /// <summary>
    /// ����Ȩ��
    /// </summary>
    internal class CreateModelPermision
        : IEmrModelPermision
    {

        private Employee _employee;

        private bool _readOnlyMode;

        public CreateModelPermision(
            Employee employee, bool readOnlyMode)
        {
            _employee = employee;

            _readOnlyMode = readOnlyMode;
        }

        #region IEmrModelPermision Members

        public bool CanDo(EmrModel model)
        {
            string msg;
            return CanCreateCore(model, out msg);
        }

        public bool CanDo(EmrModel model, out string msg)
        {
            return CanCreateCore(model, out msg);
        }

        #endregion

        private bool CanCreateCore(EmrModel model, out string msg)
        {
            msg = string.Empty;
            if (_readOnlyMode)
                return false;
            int grade = _employee.DoctorGradeNumber;
            if (grade == -1)
            {
                msg = "���޴�������Ȩ�ޡ�";
                return false;
            }
            return true;
        }

    }

    /// <summary>
    /// �༭Ȩ��
    /// </summary>
    internal class EditModelPermision
        : IEmrModelPermision
    {

        private Employee _employee;

        private bool _readOnlyMode;

        public EditModelPermision(
            Employee employee, bool readOnlyMode)
        {
            _employee = employee;
            _readOnlyMode = readOnlyMode;
        }

        #region IEmrModelPermision Members

        public bool CanDo(EmrModel model)
        {
            string msg;
            return CanEditCore(model, out msg);
        }

        public bool CanDo(EmrModel model, out string msg)
        {
            return CanEditCore(model, out msg);
        }

        #endregion

        private bool CanEditCore(EmrModel model, out string msg)
        {
            bool result = false;
            msg = string.Empty;
            if (_readOnlyMode)
                return false;
            int grade = _employee.DoctorGradeNumber;
            if (grade == -1)
            {
                //msg = "�����޸Ĳ���Ȩ�ޡ�";
                //return false;
            }
            // Check State

            if (_employee.Grade.Trim() == "") return false;
            DoctorGrade gradeEnum = (DoctorGrade)Enum.Parse(typeof(DoctorGrade), _employee.Grade);

            if (gradeEnum == DoctorGrade.Nurse)
            {
                //�����ĵ� AJ�������¼ AI�����������¼ AK
                if (model.ModelCatalog == "AJ" || model.ModelCatalog == "AJ" || model.ModelCatalog == "AJ")
                {
                    return true;
                }
            }

            if (gradeEnum == DoctorGrade.None) return false;

            switch (model.State)
            {
                case ExamineState.Deleted:
                    result = model.CreatorXH == _employee.Code;
                    if (!result)
                    {
                        if (_employee.DoctorGradeNumber <= 0)
                            msg = "�����޸Ĵ˲����ļ���Ȩ�ޡ�";
                        else
                            msg = "�˲����ļ���ɾ����";
                    }
                    break;
                case ExamineState.NotSubmit:
                    result = model.CreatorXH == _employee.Code;
                    if (!result)
                    {
                        if (_employee.DoctorGradeNumber <= 0)
                            msg = "�����޸Ĵ˲����ļ���Ȩ�ޡ�";
                        else
                            msg = "�˲����ļ�δ�ύ��";
                    }
                    break;
                case ExamineState.SubmitButNotExamine:
                    result = (_employee.DoctorGradeNumber == 0);
                    if (!result)
                    {
                        if (model.CreatorXH == _employee.Code)
                            msg = "�˲����ļ����ύ��";
                        else
                            msg = "�����޸Ĵ˲����ļ���Ȩ�ޡ�";
                    }
                    break;
                case ExamineState.FirstExamine:

                    result = (_employee.DoctorGradeNumber > 0);
                    if (!result)
                    {
                        if (model.CreatorXH == _employee.Code)
                            msg = "�˲����ļ����ύ��";
                        else
                            msg = "�����޸Ĵ˲����ļ���Ȩ�ޡ�";
                    }
                    break;
                case ExamineState.SecondExamine:
                default:
                    result = (_employee.DoctorGradeNumber > 1);
                    if (!result)
                        msg = "�����޸Ĵ˲����ļ���Ȩ�ޡ�";
                    break;
            }
            return result;
        }

    }

    /// <summary>
    /// ɾ��Ȩ��
    /// </summary>
    internal class DeleteModelPermision
        : IEmrModelPermision
    {

        private Employee _employee;

        private bool _readOnlyMode;

        public DeleteModelPermision(
            Employee employee, bool readOnlyMode)
        {
            _employee = employee;
            _readOnlyMode = readOnlyMode;
        }

        #region IEmrModelPermision Members

        public bool CanDo(EmrModel model)
        {
            string msg;
            return CanDoCore(model, out msg);
        }

        public bool CanDo(EmrModel model, out string msg)
        {
            return CanDoCore(model, out msg);
        }

        #endregion

        private bool CanDoCore(EmrModel model, out string msg)
        {
            bool result;
            msg = string.Empty;
            if (_readOnlyMode)
                return false;

            int grade = _employee.DoctorGradeNumber;
            if (grade == -1)
            {
                //msg = "�����޸Ĳ���Ȩ�ޡ�";
                //return false;
            }

            if (model.State != ExamineState.NotSubmit)
            {
                result = false;
                msg = "�˲����ļ��޷�ɾ������Ϊ�����ļ����ύ��";
            }

            else if (model.CreatorXH != _employee.Code)
            {
                result = false;
                msg = "�˲����ļ��޷�ɾ������Ϊֻ�д����˲ſ���ɾ�������ļ���";
            }
            else
            {
                result = true;
                msg = string.Empty;
            }
            return result;
        }

    }

    /// <summary>
    /// �ύ���
    /// </summary>
    internal class SubmitModelPermision
        : IEmrModelPermision
    {

        private Employee _employee;
        private bool _readOnlyMode;

        public SubmitModelPermision(
            Employee employee, bool readOnlyModel)
        {
            _employee = employee;
            _readOnlyMode = readOnlyModel;
        }

        #region IEmrModelPermision Members

        public bool CanDo(EmrModel model)
        {
            return CanSubmitCore(model);
        }

        public bool CanDo(EmrModel model, out string msg)
        {
            msg = string.Empty;
            return CanSubmitCore(model);
        }

        #endregion

        private bool CanSubmitCore(EmrModel model)
        {
            if (_readOnlyMode)
                return false;
            bool result;
            int grade = _employee.DoctorGradeNumber;
            if (grade == -1)
            {
                return false;
            }
            // Check State
            switch (model.State)
            {
                case ExamineState.NotSubmit:
                    result = model.CreatorXH == _employee.Code;
                    break;
                case ExamineState.Deleted:
                    result = false;
                    break;
                case ExamineState.SubmitButNotExamine:
                    result = (_employee.DoctorGradeNumber > 0);
                    break;
                case ExamineState.FirstExamine:
                    result = _employee.DoctorGradeNumber > 1;
                    break;
                case ExamineState.SecondExamine:
                default:
                    result = _employee.DoctorGradeNumber == 2;
                    break;
            }
            return result;
        }

    }

    //��˲�����Ȩ�� 
    internal class AuditModelPermision
        : IEmrModelPermision
    {
        private Employee _employee;
        private bool _readOnlyMode;

        public AuditModelPermision(
          Employee employee, bool readOnlyModel)
        {
            _employee = employee;
            _readOnlyMode = readOnlyModel;
        }
        #region IEmrModelPermision ��Ա

        public bool CanDo(EmrModel model)
        {
            return CanAuditModeCore(model);
        }

        public bool CanDo(EmrModel model, out string msg)
        {
            msg = string.Empty;
            return CanAuditModeCore(model);
        }
        private bool CanAuditModeCore(EmrModel model)
        {
            if (_readOnlyMode)
                return false;
            bool result;

            if (_employee.DoctorGradeNumber == -1)
            {
                return false;
            }
            switch (model.State)
            {
                //δ�ύ���������
                case ExamineState.NotSubmit:
                case ExamineState.Deleted:
                    result = false;
                    break;
                case ExamineState.SubmitButNotExamine:// �������ϲ������
                    result = _employee.DoctorGradeNumber > 0;
                    if (result) result = GetThreeLevelCheck(model, _employee);
                    break;
                case ExamineState.FirstExamine:
                    result = _employee.DoctorGradeNumber >= 1;
                    if (result) result = GetThreeLevelCheck(model, _employee);
                    break;
                case ExamineState.SecondExamine:
                default:
                    result = _employee.DoctorGradeNumber >= 2;
                    break;
            }
            return result;
        }
        #endregion

        const string c_GetThreeLevelCheck = @"select count(1) from THREE_LEVEL_CHECK ";
        const string c_Resident = " where resident_id = '{0}' ";
        const string c_Attend = " where resident_id = '{0}' and attend_id = '{1}' ";
        const string c_Chief = " where resident_id = '{0}' and chief_id = '{1}' ";

        /// <summary>
        /// �ж��Ƿ�����˵�Ȩ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true�������Ȩ�� false��û�����Ȩ��</returns>
        private bool GetThreeLevelCheck(EmrModel model, Employee employee)
        {
            IDataAccess sqlDataAccess = DataAccessFactory.DefaultDataAccess;
            bool result = true;
            string num = sqlDataAccess.ExecuteScalar(c_GetThreeLevelCheck + string.Format(c_Resident, model.CreatorXH), System.Data.CommandType.Text).ToString();
            if (num != "0") //������ָ����Ա����������
            {
                switch (employee.DoctorGradeNumber)
                {
                    case 1: //����ҽʦ
                        num = sqlDataAccess.ExecuteScalar(c_GetThreeLevelCheck + string.Format(c_Attend, model.CreatorXH, _employee.Code), System.Data.CommandType.Text).ToString();
                        if (num == "0")
                        {
                            result = false;
                        }
                        break;
                    case 2: //����ҽʦ ������ҽʦ
                        num = sqlDataAccess.ExecuteScalar(c_GetThreeLevelCheck + string.Format(c_Chief, model.CreatorXH, _employee.Code), System.Data.CommandType.Text).ToString();
                        if (num == "0")
                        {
                            result = false;
                        }
                        break;
                }
            }
            return result;
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    internal class WithdrawSubmissionModelPermision
        : IEmrModelPermision
    {

        private Employee _employee;

        private bool _readOnlyMode;

        public WithdrawSubmissionModelPermision(
            Employee employee, bool readOnlyMode)
        {
            _employee = employee;
            _readOnlyMode = readOnlyMode;
        }

        #region IEmrModelPermision Members

        public bool CanDo(EmrModel model)
        {
            return CanWithdrawSubmissionCore(model);
        }

        public bool CanDo(EmrModel model, out string msg)
        {
            msg = string.Empty;
            return CanWithdrawSubmissionCore(model);
        }

        #endregion

        private bool CanWithdrawSubmissionCore(EmrModel model)
        {
            if (_readOnlyMode)
                return false;
            int grade = _employee.DoctorGradeNumber;
            if (grade == -1)
                return false;
            // Check State
            if ((model.State == ExamineState.NotSubmit) || (model.State == ExamineState.Deleted))
                return false;
            if (model.State == ExamineState.SubmitButNotExamine) // ���ύδ���
            {
                //��������γ���������ǰ�����ǵ�ǰҽ��������ǰҽʦ��סԺҽʦ�������ϵ�ҽʦ����������

                return _employee.DoctorGradeNumber > 1;
            }
            else if (model.State == ExamineState.FirstExamine) // ���������״̬
                return _employee.DoctorGradeNumber > 1; // ��ʱ������������
            else // ����״̬�£�ֻ�������γ���
                return _employee.DoctorGradeNumber > 2;
        }

    }

    internal static class ModelPermisionFactroy
    {

        internal static IEmrModelPermision Create(
            ModelPermisionType type, Employee employee)
        {
            return Create(type, employee, false);
        }

        internal static IEmrModelPermision Create(
            ModelPermisionType type, Employee employee, bool readOnlyMode)
        {
            switch (type)
            {
                case ModelPermisionType.Create:
                    return new CreateModelPermision(
                        employee, readOnlyMode);
                case ModelPermisionType.Edit:
                    return new EditModelPermision(
                        employee, readOnlyMode);
                case ModelPermisionType.Delete:
                    return new DeleteModelPermision(
                        employee, readOnlyMode);
                case ModelPermisionType.Submit:
                    return new SubmitModelPermision(
                        employee, readOnlyMode);
                case ModelPermisionType.Audit:
                    return new AuditModelPermision(
                        employee, readOnlyMode);
                case ModelPermisionType.WithdrawSubmission:
                    return new WithdrawSubmissionModelPermision(
                        employee, readOnlyMode);
                default:
                    return null;
            }
        }

    }

    /// <summary>
    /// ����ģ�Ͳ����б�
    /// </summary>
    internal enum ModelPermisionType
    {
        /// <summary>
        /// ����
        /// </summary>
        Create,
        /// <summary>
        /// �༭
        /// </summary>
        Edit,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete,
        /// <summary>
        /// �滻
        /// </summary>
        Replace,
        /// <summary>
        /// �ύ
        /// </summary>
        Submit,
        /// <summary>
        /// ���
        /// </summary>
        Audit,
        /// <summary>
        /// �������
        /// </summary>
        WithdrawSubmission,
    }

}
