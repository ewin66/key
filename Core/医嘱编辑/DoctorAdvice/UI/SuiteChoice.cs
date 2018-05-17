using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Utils;
using DrectSoft.Common.Eop;
using DrectSoft.Wordbook;
using System.Globalization;
using DevExpress.XtraNavBar;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DrectSoft.Resources;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DrectSoft.FrameWork.WinForm.Plugin;

namespace DrectSoft.Core.DoctorAdvice {
    /// <summary>
    /// ����ҽ���ؼ�
    /// </summary>
    public partial class SuiteChoice : XtraUserControl {
        #region public properties
        /// <summary>
        /// �Ƿ���Ա༭
        /// </summary>
        public bool Editable {
            get { return _editable; }
            set {
                _editable = value;

                foreach (BarItem item in suitebarManager.Items)
                    if (value)
                        item.Visibility = BarItemVisibility.Always;
                    else
                        item.Visibility = BarItemVisibility.Never;
            }
        }
        private bool _editable;

        /// <summary>
        /// ��ǰѡ�еĳ���ҽ������¼���
        /// </summary>
        public decimal SelectedSuiteNo {
            get { return _selectedSuiteNo; }
        }
        private decimal _selectedSuiteNo;

        /// <summary>
        /// ��ǰѡ�еĳ�������¼
        /// </summary>
        public SuiteOrder SelectedSuiteObject {
            get { return _selectedSuiteObject; }
        }
        private SuiteOrder _selectedSuiteObject;

        /// <summary>
        /// ������ϸѡ���������ѡ���ļ�¼
        /// </summary>
        public object[,] SelectedContents {
            get { return _selectedContents; }
        }
        private object[,] _selectedContents;
        #endregion

        #region private variables & properties
        private IEmrHost _app;
        /// <summary>
        /// App
        /// </summary>
        public IEmrHost App {
            get { return _app; }
            set { _app = value; }
        }
        private IDataAccess m_SqlExecutor;
        private ICustomMessageBox m_MessageBox;
        private SqlWordbook m_SuiteBook;
        private OrderFrequencyBook m_FrequencyBook;
        private OrderSuiteEditForm m_InputForm;
        private bool m_IsTempOrder;
        private bool m_OutDruggeryOnly; // ֻ��ʾ��Ժ��ҩ�ĳ���

        private SuiteOrderHandle m_SuiteHelper;

        private DataApplyRange SuiteType {
            get {
                if (navCtrlOrderSuit.ActiveGroup == navGroupPersonal)
                    return DataApplyRange.Individual;
                else if (navCtrlOrderSuit.ActiveGroup == navGroupDept)
                    return DataApplyRange.Department;
                else
                    return DataApplyRange.All;
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public SuiteChoice() {
            InitializeComponent();

            this.navCtrlOrderSuit.View = new DevExpress.XtraNavBar.ViewInfo.StandardSkinNavigationPaneViewInfoRegistrator("Office 2010 Silver");
            

        }

        #region public methods
        /// <summary>
        /// ��ʼ��Form�������ڴ���ʱ����
        /// </summary>
        /// <param name="sqlHelper"></param>
        [CLSCompliant(false)]
        public void InitializeOrderSuiteChoiceForm(IDataAccess sqlHelper, ICustomMessageBox messageBox, SuiteOrderHandle suiteHelper, bool editable) {
            CustomInitialize(_app);
            m_SqlExecutor = sqlHelper;
            m_MessageBox = messageBox;
            
            m_SuiteHelper = suiteHelper;
            Editable = editable;
            navCtrlOrderSuit.Enabled = editable;
            

            

            if (Editable)
                navCtrlOrderSuit_ActiveGroupChanged(this, new NavBarGroupEventArgs(navGroupDept));

            BindImageToNavGroup();
        }



        /// <summary>
        /// ˢ�³���ҽ�����ݡ����ݵ�ǰҽ�������Һͱ༭��ҽ�����ͽ���
        /// </summary>
        /// <param name="deptCode">���Ҵ���</param>
        /// <param name="wardCode">��������</param>
        /// <param name="docCode">ҽ������</param>
        /// <param name="isTempOrder">���ڡ���ʱ��־</param>
        /// <param name="enabled">�Ƿ����</param>
        [CLSCompliantAttribute(false)]
        public void RefreshOrderSuiteData(bool isTempOrder, bool enabled, bool outDruggeryOnly, OrderFrequencyBook frequencyBook) {
            m_IsTempOrder = isTempOrder;
            m_FrequencyBook = frequencyBook;
            m_OutDruggeryOnly = outDruggeryOnly;
            navCtrlOrderSuit.Enabled = enabled;
            navCtrlOrderSuit_ActiveGroupChanged(this, new NavBarGroupEventArgs(navCtrlOrderSuit.ActiveGroup));
            
        }
        #endregion

        #region custom event handler
        /// <summary>
        /// ˫��ѡ�г��׼�¼�¼�
        /// </summary>
        public event EventHandler SelectedOrderSuite {
            add { onSelectedOrderSuite = (EventHandler)Delegate.Combine(onSelectedOrderSuite, value); }
            remove { onSelectedOrderSuite = (EventHandler)Delegate.Remove(onSelectedOrderSuite, value); }
        }
        private EventHandler onSelectedOrderSuite;

        private void FireHadSelectedOrderSuite() {
            if (onSelectedOrderSuite != null)
                onSelectedOrderSuite(this, new EventArgs());
        }

        /// <summary>
        /// ѡ���˳��ױ༭����
        /// </summary>
        [CLSCompliant(false)]
        public event EventHandler<DataCommitArgs> AfterChoicedEditModel {
            add { onAfterChoicedEditModel = (EventHandler<DataCommitArgs>)Delegate.Combine(onAfterChoicedEditModel, value); }
            remove { onAfterChoicedEditModel = (EventHandler<DataCommitArgs>)Delegate.Remove(onAfterChoicedEditModel, value); }
        }
        private EventHandler<DataCommitArgs> onAfterChoicedEditModel;

        private void FireAfterChoicedEditModel(DataCommitType editModel) {
            if (onAfterChoicedEditModel != null)
                onAfterChoicedEditModel(this, new DataCommitArgs(editModel));
        }
        #endregion

        #region private methodes
        private void BindImageToNavGroup() {
            //navGroupPersonal.SmallImage = ResourceManager.GetSmallIcon("Individual", IconType.Normal);
            //navGroupPersonal.LargeImage = ResourceManager.GetMiddleIcon("Individual", IconType.Normal);
            //navGroupDept.SmallImage = ResourceManager.GetSmallIcon("Department", IconType.Normal);
            //navGroupDept.LargeImage = ResourceManager.GetMiddleIcon("Department", IconType.Normal);
            //navGroupHospital.SmallImage = ResourceManager.GetSmallIcon("Hospital", IconType.Normal);
            //navGroupHospital.LargeImage = ResourceManager.GetMiddleIcon("Hospital", IconType.Normal);
        }

        private void CustomInitialize(IEmrHost application) {
            ResetControlFont();

            navCtrlOrderSuit.Enabled = false;
            navCtrlOrderSuit.ActiveGroupChanged += new NavBarGroupEventHandler(navCtrlOrderSuit_ActiveGroupChanged);
            navCtrlOrderSuit.ActiveGroup = navGroupDept;


            ResetSelectedSuiteObject(null);
        }

        private void ResetControlFont() {


            foreach (AppearanceObject ap in gridViewSuit.Appearance)
                ap.Font = Font;

            foreach (AppearanceObject ap in gridViewSuit.Appearance)
                ap.Font = Font;

            foreach (AppearanceObject ap in navCtrlOrderSuit.Appearance)
                ap.Font = Font;

            navGroupPersonal.Appearance.Font = Font;
            navGroupDept.Appearance.Font = Font;
            //navGroupHospital.Appearance.Font = Font;
        }

        private void FilterData() {
            if (!navCtrlOrderSuit.Enabled)
                return;

            OrderManagerKind orderKind;
            if (m_IsTempOrder)
                orderKind = OrderManagerKind.ForTemp;
            else
                orderKind = OrderManagerKind.ForLong;

            if (Editable)
                m_SuiteHelper.SuiteMasterTable.DefaultView.RowFilter = String.Format(CultureInfo.CurrentCulture
                    , "UseRange = {0:D}"
                    , SuiteType);
            else if (m_OutDruggeryOnly)
                m_SuiteHelper.SuiteMasterTable.DefaultView.RowFilter = String.Format(CultureInfo.CurrentCulture

                    //Modified By wwj 2011-06-28
                    //, "UseRange = {0:D} and Mark in ({1:D}, {2:D}) and AdviceCategory = {3:D}"
                    , "UseRange = {0:D} and Mark in ({1:D}, {2:D}) OrderType = {3:D}"

                    , SuiteType
                    , OrderManagerKind.Normal
                    , orderKind
                    , OrderContentKind.OutDruggery);
            else
                m_SuiteHelper.SuiteMasterTable.DefaultView.RowFilter = String.Format(CultureInfo.CurrentCulture

                    //Modified By wwj 2011-06-28
                    //, "UseRange = {0:D} and Mark in ({1:D}, {2:D}) and AdviceCategory <> {3:D}"
                    , "UseRange = {0:D} and Mark in ({1:D}, {2:D}) and OrderType <> {3:D}"

                    , SuiteType
                    , OrderManagerKind.Normal
                    , orderKind
                    , OrderContentKind.OutDruggery);

            m_SuiteBook.ExtraCondition = m_SuiteHelper.SuiteMasterTable.DefaultView.RowFilter;

            if (gridCtrlSuit.DataSource == null)
                gridCtrlSuit.DataSource = m_SuiteHelper.SuiteMasterTable;
        }

        private void DoAfterSelectedRecordValueChanged(object sender, EventArgs e) {
            _selectedSuiteObject.ValueChanged -= new EventHandler(DoAfterSelectedRecordValueChanged);
            m_SuiteHelper.SynchAndSaveMasterData(_selectedSuiteNo, _selectedSuiteObject);
            m_SuiteBook.BookData = m_SuiteHelper.SuiteMasterTable.Copy();
            _selectedSuiteObject.ValueChanged += new EventHandler(DoAfterSelectedRecordValueChanged);
        }

        private void ResetSelectedSuiteObject(DataRow row) {
            if (row == null) {
                _selectedSuiteNo = 0;
                _selectedSuiteObject = null;
            }
            else {
                _selectedSuiteNo = (decimal)row["SuiteID"];
                _selectedSuiteObject = new SuiteOrder(row);
                _selectedSuiteObject.ValueChanged += new EventHandler(DoAfterSelectedRecordValueChanged);
            }
        }
        #endregion

        #region events


        /// <summary>
        /// ���ø���
        /// </summary>
        public virtual void AddPersonal() {
            this.navCtrlOrderSuit.Groups.Add(navGroupPersonal);
        }
        /// <summary>
        /// ���ÿ���
        /// </summary>
        public virtual void AddDept() {
            this.navCtrlOrderSuit.Groups.Add(navGroupDept);
        }
        /// <summary>
        /// ����ȫԺ
        /// </summary>
        public virtual void AddHospital() {
            //this.navCtrlOrderSuit.Groups.Add(navGroupHospital);
        }


        private void navCtrlOrderSuit_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e) {
            if (SuiteType == DataApplyRange.Individual)
                navGroupPersonalContainer.Controls.Add(gridCtrlSuit);
            else if (SuiteType == DataApplyRange.Department)
                navGroupDeptContainer.Controls.Add(gridCtrlSuit);
            //else
            //    navGroupHospitalContainer.Controls.Add(gridCtrlSuit);

            gridCtrlSuit.BringToFront();

            FilterData();
        }

        private void gridCtrlSuit_DoubleClick(object sender, EventArgs e) {
            if (gridViewSuit.FocusedRowHandle >= 0) {
                if (m_InputForm == null) {
                    m_InputForm = new OrderSuiteEditForm();
                    m_InputForm.InitializeProperty(m_SqlExecutor, m_MessageBox);
                }
                DataRow row = gridViewSuit.GetDataRow(gridViewSuit.FocusedRowHandle);
                if (row == null)
                    return;

                ResetSelectedSuiteObject(row);

                if (Editable) {
                    FireAfterChoicedEditModel(DataCommitType.Modify);
                }
                else {
                    OrderManagerKind orderKind;
                    if (m_IsTempOrder)
                        orderKind = OrderManagerKind.ForTemp;
                    else
                        orderKind = OrderManagerKind.ForLong;

                    if (m_OutDruggeryOnly)
                        m_SuiteHelper.SuiteDetailTable.DefaultView.RowFilter = String.Format(CultureInfo.CurrentCulture
                            , "GroupID = {0} and Mark in ({1:D}, {2:D}) and AdviceCategory = {3:D}"
                            , _selectedSuiteNo
                            , OrderManagerKind.Normal
                            , orderKind
                            , OrderContentKind.OutDruggery);
                    else
                        m_SuiteHelper.SuiteDetailTable.DefaultView.RowFilter = String.Format(CultureInfo.CurrentCulture
                            , "GroupID = {0} and Mark in ({1:D}, {2:D}) and AdviceCategory <> {3:D}"
                            , _selectedSuiteNo
                            , OrderManagerKind.Normal
                            , orderKind
                            , OrderContentKind.OutDruggery);

                    if (m_InputForm.CallOrderSuiteEditForm(true, m_SuiteHelper.SuiteDetailTable.DefaultView.ToTable(), m_FrequencyBook) == DialogResult.OK) {
                        _selectedContents = m_InputForm.SelectedContents;
                        FireHadSelectedOrderSuite();
                    }
                }
            }
        }

        private void barItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            decimal newSerialNo = m_SuiteHelper.AddNewMasterRecord(SuiteType);

            // ��λ�����µļ�¼
            int locateRow = gridViewSuit.LocateByDisplayText(0, gridColSerialNo, newSerialNo.ToString());
            if (locateRow >= 0)
                gridViewSuit.FocusedRowHandle = locateRow;

            m_SuiteBook.BookData = m_SuiteHelper.SuiteMasterTable.Copy();
            // �����޸��¼�
            FireAfterChoicedEditModel(DataCommitType.Add);
        }

        private void baItemModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FireAfterChoicedEditModel(DataCommitType.Modify);
        }

        private void barItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            m_SuiteHelper.DeleteMasterRecord(_selectedSuiteNo);
            m_SuiteBook.BookData = m_SuiteHelper.SuiteMasterTable.Copy();

            FireAfterChoicedEditModel(DataCommitType.Modify);
        }

        private void gridViewSuit_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e) {
            DataRow row = gridViewSuit.GetDataRow(gridViewSuit.FocusedRowHandle);
            //ResetSelectedSuiteObject(row);
        }


        #endregion
    }
}