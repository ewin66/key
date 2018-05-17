using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DrectSoft.FrameWork;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.FrameWork.WinForm;
using DrectSoft.Core.NursingDocuments.PublicSet;
using DrectSoft.Common.Eop;
using DevExpress.Utils;

namespace DrectSoft.Core.NursingDocuments
{
    public partial class MainNursingMeasure : DevExpress.XtraEditors.XtraUserControl, IEMREditor
    {
        public MainNursingMeasure(IEmrHost app)
        {
            m_app = app;
            PublicSet.MethodSet.App = app;
            InitForm();

        }

        public MainNursingMeasure(string noofinpat) : this()
        {
            CurrentNoofinpat = noofinpat;
        }

        public MainNursingMeasure()
        {
            InitializeComponent();
        }

        private WaitDialogForm m_WaitDialog;

        public Inpatient CurrentPat
        {
            get
            {
                if (_currentPat == null)
                {
                    _currentPat = m_app.CurrentPatientInfo;
                } 
                return _currentPat;
            }
            set
            {
                //if (!object.ReferenceEquals(_currentPat, value))
                {
                    _currentPat = value;
                    InitMeasureTableData();

                }
            }
        }
        private Inpatient _currentPat = null;

        internal IEmrHost App
        {
            get { return m_app; }
        }
        private IEmrHost m_app;

        #region �¼�&&����

        private void SetWaitDialogCaption(string caption)
        {
            if (m_WaitDialog == null)
                m_WaitDialog = new WaitDialogForm("���ڼ�������......", "�����Ժ�");
            if (!m_WaitDialog.Visible)
                m_WaitDialog.Visible = true;
            m_WaitDialog.Caption = caption;


        }

        private void HideWaitDialog()
        {
            if (m_WaitDialog != null)
                m_WaitDialog.Hide();
        }


        private void InitForm()
        {
            AddEvents();
            this.Text = "�����";
            SetNewButtonEnable();
        }

        private void AddEvents()
        {
            this.simpleButtonNew.Click += new EventHandler(simpleButtonNew_Click);//����
            this.simpleButtonFirstWeek.Click += new EventHandler(simpleButtonFirstWeek_Click);//��һ��
            this.simpleButtonLastWeek.Click += new EventHandler(simpleButtonLastWeek_Click);//��һ��
            this.simpleButtonNextWeek.Click += new EventHandler(simpleButtonNextWeek_Click);//��һ��
            this.simpleButtonThisWeek.Click += new EventHandler(simpleButtonThisWeek_Click);//����
            this.simpleButtonRefresh.Click += new EventHandler(simpleButtonRefresh_Click);//����
            this.simpleButtonPrint.Click += new EventHandler(simpleButtonPrint_Click);//��ӡ
            this.simpleButtonPrintBat.Click += new EventHandler(simpleButtonPrintBat_Click);//������ӡ
        }

        void simpleButtonPrintBat_Click(object sender, EventArgs e)
        {
            ucThreeMeasureTable.PrintAllDocument();
        }

        /// <summary>
        /// ��ʼ��������е�����
        /// </summary>
        private void InitMeasureTableData()
        {
            if (CurrentPat != null)
            {
                SetWaitDialogCaption("���ڶ�ȡ������Ϣ");

                MethodSet.CurrentInPatient = CurrentPat;
                DataTable patientInfo = PublicSet.MethodSet.GetPatientInfoForThreeMeasureTable(MethodSet.CurrentInPatient.NoOfFirstPage);
                SetWaitDialogCaption("���ڻ������ⵥ");
                this.ucThreeMeasureTable.SetPatientInfo(patientInfo);
                this.ucThreeMeasureTable.LoadData();
                HideWaitDialog();
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonRefresh_Click(object sender, EventArgs e)
        {
            this.ucThreeMeasureTable.LoadData();
            ucThreeMeasureTable.Refresh();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonThisWeek_Click(object sender, EventArgs e)
        {
            this.ucThreeMeasureTable.SetAllocateDateTime(DateTime.Now);
            SetButtonEnable();

            ucThreeMeasureTable.LoadData();
            ucThreeMeasureTable.Refresh();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonNextWeek_Click(object sender, EventArgs e)
        {
            this.ucThreeMeasureTable.SetAllocateDateTime(7);
            this.simpleButtonLastWeek.Enabled = true;

            this.ucThreeMeasureTable.LoadData();
            ucThreeMeasureTable.Refresh();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonLastWeek_Click(object sender, EventArgs e)
        {
            this.ucThreeMeasureTable.SetAllocateDateTime(-7);
            SetButtonEnable();

            this.ucThreeMeasureTable.LoadData();
            ucThreeMeasureTable.Refresh();
        }

        /// <summary>
        /// ��һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonFirstWeek_Click(object sender, EventArgs e)
        {
            this.ucThreeMeasureTable.SetAllocateDateTime();//��Ժ����һ�ܣ�����һ��
            this.simpleButtonLastWeek.Enabled = false;//ѡ���һ�ܵ�ʱ����һ�ܡ��İ�ť������

            this.ucThreeMeasureTable.LoadData();
            ucThreeMeasureTable.Refresh();
        }

        /// <summary>
        /// д������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonNew_Click(object sender, EventArgs e)
        {
            NursingRecord nursingRecord = new NursingRecord();
            nursingRecord.ShowDialog();

            this.ucThreeMeasureTable.LoadData();
            this.ucThreeMeasureTable.Refresh();
        }


        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            ucThreeMeasureTable.PrintDocument();
        }

        private void SetButtonEnable()
        {
            this.simpleButtonLastWeek.Enabled = this.ucThreeMeasureTable.DateTimeLogicForLastWeek();
        }

        #endregion

        #region IEMREditor ��Ա

        public Control DesignUI
        {
            get { return this; }
        }

        public new void Load(IEmrHost app)
        {
            //m_app = app;
            //PublicSet.MethodSet.App = app;
            //CurrentPat = m_app.CurrentPatientInfo;
            //InitForm();
            m_app = app;
            PublicSet.MethodSet.App = app;
            //edit by ���� 2012-11-06 
            if (!string.IsNullOrEmpty(CurrentNoofinpat))
            {
                CurrentPat = new Common.Eop.Inpatient(Convert.ToDecimal(CurrentNoofinpat));
            }
            else if (m_app.CurrentPatientInfo != null)
            {
                CurrentPat = m_app.CurrentPatientInfo;
            }
            else
            {
                return;
            }
            //CurrentPat = m_app.CurrentPatientInfo; 

            InitForm();
        }

        public void Save()
        {

        }

        public string Title
        {
            get { return "���ⵥ����"; }
        }

        public bool ReadOnlyControl
        {
            get { return m_ReadOnlyControl; }
            set { m_ReadOnlyControl = value; }
        }
        private bool m_ReadOnlyControl = false;
        #endregion

        private void MainNursingMeasure_SizeChanged(object sender, EventArgs e)
        {
            ReSetUCLocaton();
        }

        /// <summary>
        /// �ı�UserControl��λ��
        /// </summary>
        private void ReSetUCLocaton()
        {
            Int32 pointX = (this.Width - this.ucThreeMeasureTable.Width) / 2;
            Int32 pointY = ucThreeMeasureTable.Location.Y;
            this.ucThreeMeasureTable.Location = new Point(pointX, pointY);
        }

        private void SetNewButtonEnable()
        {
            Employee emp = new Employee(m_app.User.Id);
            emp.ReInitializeProperties();
            if (emp.Kind == EmployeeKind.Nurse)//��ǰ��¼���ǻ�ʿ
            {
                simpleButtonNew.Enabled = true;
            }
            else
            {
                simpleButtonNew.Enabled = false;
            }
        }


        #region IEMREditor ��Ա

        public void Print()
        {
            ucThreeMeasureTable.PrintDocument();
        }

        public string CurrentNoofinpat
        {
            get;
            set;
        }

        #endregion

       
    }
}