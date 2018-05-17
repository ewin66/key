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
        //xll 2012-10-10 ¼���¼� ����¼��ʱʹ��
        public EventHandler eventHandlerDaoRu;
        public MainNursingMeasure(IEmrHost app)
        {
            m_app = app;
            PublicSet.MethodSet.App = app;
            InitForm();
            this.ActiveControl = this.simpleButtonNew;
        }

        public MainNursingMeasure(string noofinpat)
        {
            CurrentNoofinpat = noofinpat;
            InitializeComponent();
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
                return _currentPat;
            }
            set
            {
                    _currentPat = value;
                    InitMeasureTableData();
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
        public void InitMeasureTableData()
        {
            if (CurrentPat != null)
            {
                SetWaitDialogCaption("���ڶ�ȡ������Ϣ");

                //MethodSet.CurrentInPatient = CurrentPat;
                DataTable patientInfo = PublicSet.MethodSet.GetPatientInfoForThreeMeasureTable(CurrentPat.NoOfFirstPage);
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
            //xll 2012-10-10 ���ⵥ����¼��ʱ���� 
            if (eventHandlerDaoRu != null)
            {
                eventHandlerDaoRu(sender, e);
            }
            else
            {
                NursingRecord nursingRecord = new NursingRecord(m_app, CurrentPat);
                nursingRecord.ShowDialog();
                this.ucThreeMeasureTable.LoadData();
                this.ucThreeMeasureTable.Refresh();
            }
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
            try
            {
                m_app = app;
                PublicSet.MethodSet.App = app;
                CurrentPat = m_app.CurrentPatientInfo;
                InitForm();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }

        }


        /// <summary>
        /// xll 2013-01-10
        /// </summary>
        /// <param name="app"></param>
        /// <param name="CurrentPatientInfo"></param>
        public  void Load(IEmrHost app, Inpatient CurrentPatientInfo)
        {
            try
            {
                m_app = app;
                PublicSet.MethodSet.App = app;
                CurrentPat = CurrentPatientInfo;
                InitForm();
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }

        }

        public new void Load(IEmrHost app, bool firstPageEditFlag)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
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