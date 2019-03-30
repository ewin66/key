using System;
using System.Data;


namespace DrectSoft.Common.Eop
{
    /// <summary>
    /// ���˸��˻�����Ϣ
    /// </summary>
    public class OutPatientBasicInfo1 : EPBaseObject
    {
        #region properties
        private decimal _NoOfInpatClinic;
        /// <summary>
        /// ��ҳ���
        /// </summary>
        public decimal NoOfInpatClinic
        {
            get { return _NoOfInpatClinic; }
            set { _NoOfInpatClinic = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string PatientName
        {
            get { return _patientName; }
            set
            {
                _patientName = value;
                // ��Ҫ��������ƴ������ʴ������
            }
        }
        private string _patientName;

        /// <summary>
        /// �����Ա�
        /// </summary>
        public CommonBaseCode Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        private CommonBaseCode _sex;

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set { _birthday = value; }
        }
        internal string InternalBirthday
        {
            get { return _birthday.ToString("yyyy-MM-dd"); }
            set { _birthday = Convert.ToDateTime(value); }
        }
        private DateTime _birthday;

        private DateTime _inHosDate = DateTime.Now;
        public DateTime InHosDate
        {
            get
            {
                return _inHosDate;
            }
            set { _inHosDate = value; }
        }

        /// <summary>
        /// ���˵�ǰ����,��λ����
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public int CurrentAge
        {
            get
            {
                TimeSpan diff = (DateTime.Now.Date - Birthday);
                return diff.Days / 365;
            }
        }

        /// <summary>
        /// ���˵�ǰ���䣨��ʾ�ã�
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public string CurrentDisplayAge
        {
            get { return CalcDisplayAge(Birthday, _inHosDate); }
        }

        /// <summary>
        /// ��ʾ����(��Ժʱ���䣬����ʵ�������ʾ������,��XXX��,XX��XX��,XX��)
        /// </summary>
        public string DisplayAge
        {
            get { return _displayAge; }
            internal set { _displayAge = value; }
        }
        private string _displayAge;

        /// <summary>
        /// ���֤��
        /// </summary>
        public string IdentificationNo
        {
            get { return _identificationNo; }
            set { _identificationNo = value; }
        }
        private string _identificationNo;

        /// <summary>
        /// �籣����
        /// </summary>
        public string SocialInsuranceNo
        {
            get { return _socialInsuranceNo; }
            set { _socialInsuranceNo = value; }
        }
        private string _socialInsuranceNo;

        /// <summary>
        /// ���տ���
        /// </summary>
        public string InsuranceNo
        {
            get { return _insuranceNo; }
            set { _insuranceNo = value; }
        }
        private string _insuranceNo;

        /// <summary>
        /// ��������
        /// </summary>
        public string OtherCardNo
        {
            get { return _otherCardNo; }
            set { _otherCardNo = value; }
        }
        private string _otherCardNo;


        private int _babySequence;
        /// <summary>
        /// Ӥ�����(��1��ʼ��0��ʾ����Ӥ��)
        /// </summary>
        public int BabySequence
        {
            get { return _babySequence; }
            set { _babySequence = value; }
        }

        private decimal _motherFirstPageNo;
        /// <summary>
        /// ĸ����ҳ���
        /// </summary>
        public decimal MotherFirstPageNo
        {
            get { return _motherFirstPageNo; }
            set { _motherFirstPageNo = value; }
        }

        /// <summary>
        /// �Ļ��̶� 
        /// </summary>
        public CommonBaseCode EducationLevel
        {
            get { return _educationLevel; }
            set { _educationLevel = value; }
        }
        private CommonBaseCode _educationLevel;

        /// <summary>
        /// ����״�� 
        /// </summary>
        public CommonBaseCode MarriageCondition
        {
            get { return _marriageCondition; }
            set { _marriageCondition = value; }
        }
        private CommonBaseCode _marriageCondition;

        /// <summary>
        /// ������� 
        /// </summary>
        public CommonBaseCode Nation
        {
            get { return _nation; }
            set { _nation = value; }
        }
        private CommonBaseCode _nation;

        /// <summary>
        /// ������
        /// </summary>
        public Address Homeplace
        {
            get { return _homeplace; }
            set { _homeplace = value; }
        }
        private Address _homeplace;

        /// <summary>
        /// ������λ
        /// </summary>
        public WorkDepartment DepartmentOfWork
        {
            get { return _departmentOfWork; }
            set { _departmentOfWork = value; }
        }
        private WorkDepartment _departmentOfWork;

        /// <summary>
        /// �����й���Ϣ
        /// </summary>
        public Address DomiciliaryInfo
        {
            get { return _domiciliaryInfo; }
            set { _domiciliaryInfo = value; }
        }
        private Address _domiciliaryInfo;

        /// <summary>
        /// ����
        /// </summary>
        public Address NativePlace
        {
            get { return _nativePlace; }
            set { _nativePlace = value; }
        }
        private Address _nativePlace;

        /// <summary>
        /// ��ϵ��Ϣ
        /// </summary>
        public LinkMan LinkManInfo
        {
            get { return _linkManInfo; }
            set { _linkManInfo = value; }
        }
        private LinkMan _linkManInfo;

        /// <summary>
        /// �ܽ�������
        /// ����Ϊ��λ
        /// </summary>
        public decimal YearsOfEducation
        {
            get { return _yearOfEducation; }
            set { _yearOfEducation = value; }
        }
        private decimal _yearOfEducation;

        /// <summary>
        /// �ڽ�����
        /// </summary>
        public string Faith
        {
            get { return _faith; }
            set { _faith = value; }
        }
        private string _faith;

        /// <summary>
        /// Ŀǰסַ
        /// </summary>
        public string CurrentAddress
        {
            get { return _currentAddress; }
            set { _currentAddress = value; }
        }
        private string _currentAddress;

        #endregion

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

        #region ctors
        /// <summary>
        /// 
        /// </summary>
        public OutPatientBasicInfo1()
            : base()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceRow"></param>
        public OutPatientBasicInfo1(DataRow sourceRow)
            : base(sourceRow)
        { }

        #endregion

        /// <summary>
        /// ��ʼ�����е����ԣ������������͵������Լ�������
        /// </summary>
        public override void ReInitializeAllProperties()
        {
            if (Sex != null)
                Sex.ReInitializeAllProperties();
            if (EducationLevel != null)
                EducationLevel.ReInitializeAllProperties();
            if (MarriageCondition != null)
                MarriageCondition.ReInitializeAllProperties();
            if (Nation != null)
                Nation.ReInitializeAllProperties();
            if (Homeplace != null)
                Homeplace.ReInitializeAllProperties();
            if (DepartmentOfWork != null)
                DepartmentOfWork.ReInitializeAllProperties();
            if (DomiciliaryInfo != null)
                DomiciliaryInfo.ReInitializeAllProperties();
            if (NativePlace != null)
                NativePlace.ReInitializeAllProperties();
            if (LinkManInfo != null)
                LinkManInfo.ReInitializeAllProperties();
        }

        /// <summary>
        /// ˢ�²�����������
        /// </summary>
        /// <param name="inWardTime">����ʱ��</param>
        public void RefreshAge(DateTime inWardTime)
        {
            DisplayAge = CalcDisplayAge(Birthday, inWardTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="birthday"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static string CalcDisplayAge(DateTime birthday, DateTime endDate)
        {
            string displayAge;
            int accAge;
            CalculateAge(birthday, endDate, out displayAge, out accAge);

            return displayAge;
        }

        /// <summary>
        /// TODO���ղ������䴦���߼����㲡�˽�ֹ��ָ�����ڵ����䣨��ʾ�ã�//edit by wyt 2012-11-1
        /// </summary>
        /// <param name="birthday">���˳�������</param>
        /// <param name="endDate">��ֹ����</param>
        /// <param name="displayAge">��ʾ����</param>
        /// <param name="accurateAge">��ȷ������ֵ</param>
        public static void CalculateAge(DateTime birthday, DateTime endDate, out string displayAge, out int accurateAge)
        {
            #region ����ǰ̨�������߼����ĳɵ��ô洢����
            accurateAge = 0;
            displayAge = string.Empty;
            if (birthday > endDate)
                return;

            // ��ȷ������ = ���� + ���� + ���� ����

            TimeSpan ts = endDate - birthday;
            //long P_BirthDay = DateAndTime.DateDiff(DateInterval.Year,
            //dateTimePicker1.Value, DateTime.Now,
            //FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1); //��������
            //MessageBox.Show(P_BirthDay.ToString());


            //TimeSpan dateDiff;
            int yearPart;
            int monthPart;
            int dayPart;
            int hourPart;
            int minPart;
            //int decMonth = 0;
            //int decYear = 0;

            // �Ƚ����������������֣����µĵڼ��죩�����Ƿ����¡�
            dayPart = endDate.Day - birthday.Day;
            if (dayPart < 0)
            {
                endDate = endDate.AddMonths(-1);
                dayPart += DateTime.DaysInMonth(endDate.Year, endDate.Month);
                //decMonth = 1; // �����£�����Ҫ�۳�1
            }
            // ��������
            monthPart = endDate.Month - birthday.Month;
            if (monthPart < 0)
            {
                monthPart += 12;
                endDate = endDate.AddYears(-1);
            }
            // ��������
            yearPart = endDate.Year - birthday.Year;
            // ��ʽ���������
            if (yearPart >= 14)//14�꼰���Ͽ����������
            {
                displayAge = yearPart.ToString() + "��";
            }
            else if (yearPart >= 1)//14������
            {
                if (monthPart >= 1) // 14������1�������Ͽ����������
                {
                    displayAge = yearPart.ToString() + "��"/* + monthPart.ToString() + "����"*/;
                }
                else
                {
                    displayAge = yearPart.ToString() + "��";
                }
            }
            else if (monthPart > 0)  // 1��������
            {
                displayAge = monthPart.ToString() + "����";
            }
            else if (dayPart > 0)  // 1�������¿����������
            {
                displayAge = dayPart.ToString() + "��";
            }
            else if (dayPart == 0)  // update by Ukey 2016-11-13 17:24 1���������Сʱ�ͷ�����                         
            {
                hourPart = endDate.Hour - birthday.Hour;
                minPart = endDate.Minute - birthday.Minute;
                if (hourPart > 0)
                {
                    if (minPart < 0 && hourPart > 1)
                    {
                        displayAge = (hourPart - 1).ToString() + "Сʱ" + (minPart + 60).ToString() + "��";
                    }
                    else if (minPart < 0 && hourPart == 1)
                    {
                        displayAge = (minPart + 60).ToString() + "��";
                    }
                    else if (minPart == 0)
                    {
                        displayAge = hourPart.ToString() + "Сʱ";
                    }
                    else
                    {
                        displayAge = hourPart.ToString() + "Сʱ" + minPart.ToString() + "��";
                    }
                }
                else
                {
                    displayAge = minPart.ToString() + "��";
                }
            }
            #endregion
        }

    }
}
