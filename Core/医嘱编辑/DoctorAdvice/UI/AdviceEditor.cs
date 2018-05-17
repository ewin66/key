using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DrectSoft.Common;
using DrectSoft.Common.Eop;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Resources;
using DrectSoft.Wordbook;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DrectSoft.Core.DoctorAdvice
{
    /// <summary>
    /// ҽ���༭�ؼ�
    /// </summary>
    public partial class AdviceEditor : XtraUserControl, ISupportInitialize
    {
        #region public properties
        /// <summary>
        /// ���ҽ�������Ƿ�ı��
        /// </summary>
        [Browsable(false)]
        public bool HadChanged
        {
            get
            {
                if (m_UILogic == null)
                    return false;
                return m_UILogic.HadChanged;
            }
        }

        /// <summary>
        /// ��ǵ�ǰ�������Ǻ������͵�ҽ��
        /// </summary>
        [Browsable(false)]
        public bool IsTempOrder
        {
            get { return m_UILogic.IsTempOrder; }
            set
            {
                m_UILogic.IsTempOrder = value;
                barItemLongOrder.Checked = !value;
                barItemTempOrder.Checked = value;
            }
        }

        /// <summary>
        /// ��ǰ�鿴��ҽ��״̬(Ŀǰ�ṩȫ������Ч����������ѡ��)
        /// ��ʱҽ������˵Ŀ�����Ч������ҽ����ִ�еĿ�����Ч
        /// </summary>
        [Browsable(false)]
        public OrderState OrderState
        {
            get { return m_UILogic.OrderStateFilter; }
            set
            {
                m_UILogic.OrderStateFilter = value;
                barItemStateAll.Checked = (value == OrderState.All);
                barItemStateAvailably.Checked = (value == OrderState.Executed);
                barItemStateNew.Checked = (value == OrderState.New);
                advGridView.MoveLast();
            }
        }

        /// <summary>
        /// �Ƿ����������ҽ��
        /// </summary>
        public bool AllowAddNew
        {
            get
            {
                if (m_UILogic != null)
                    return m_UILogic.AllowAddNew;
                else
                    return false;
            }
        }

        /// <summary>
        /// ���������ڵ�ǰ��ʾ��ҽ������Ƶ�ε��ֵ���
        /// </summary>
        [CLSCompliantAttribute(false)]
        public OrderFrequencyBook FrequencyWordbook
        {
            get { return m_UILogic.FrequencyWordbook; }
        }

        /// <summary>
        /// ��ǰ���˵�ҽ�����Ƿ������Ч�ġ���Ժҽ������û�б�ȡ���ģ�
        /// </summary>
        public bool HasOutHospitalOrder
        {
            get { return m_UILogic.HasOutHospitalOrder; }
        }

        /// <summary>
        /// ����ҽ���߼��������
        /// </summary>
        public SuiteOrderHandle SuiteHelper
        {
            get
            {
                if (m_UILogic == null)
                    InitializeUILogicObject();
                return m_UILogic.SuiteHelper;
            }
        }
        #endregion

        #region private variables
        private UILogic m_UILogic;
        private OrderContentEditor contentEditor;
        /// <summary>
        /// ����������ҩ���
        /// </summary>
        //private PassComponent m_MedicomPass;
        private DateTimeInputForm m_TimeInputForm;
        private WaitDialogForm m_WaitDialog;

        private int m_FocusedRowHandle;
        private IEmrHost m_App;

        /// <summary>
        /// ����Ƿ�����ESC��(��������ESCȡ����ǰ�༭���ݵ����)
        /// </summary>
        private bool m_HadPressESC;
        /// <summary>
        /// �������У������˴��󣨺�HasPressESC���ʹ�ã�
        /// </summary>
        private bool m_HadError;
        /// <summary>
        /// ��Ǳ��޸ĵ���������
        /// </summary>
        private UpdateContentFlag m_UpdateFlag;
        private bool m_FocusInGrid;
        private EditorCallModel m_CallModel; // �Ƿ񱻱༭�������
        #endregion

        #region private properties
        private Inpatient CurrentPatient
        {
            get
            {
                if (m_UILogic == null)
                    return null;

                return m_UILogic.CurrentPatient;
            }
            set
            {
                if (value == null)
                {
                    ClearSurface();

                    orderToolBar.Visible = false;
                    statusBar.Visible = false;
                }
                else if ((CurrentPatient == null) || (CurrentPatient.NoOfFirstPage != value.NoOfFirstPage))
                {
                    InitializePatientData(value);
                }
            }
        }

        private void InitializePatientData(Inpatient value)
        {
            try
            {
                ClearSurface();

                if (m_UILogic == null)
                    InitializeUILogicObject();

                bool isFirstPat = (m_UILogic.CurrentPatient == null);
                m_UILogic.CurrentPatient = value;

                InitializeToolbar(m_CallModel);

                //IsTempOrder = false;
                //OrderState = OrderState.All;
                if (isFirstPat)
                {
                    // �״���ʾʱ���༭ģʽ��Ĭ����ʾ��ҽ������ѯģʽĬ����ʾ��Чҽ��
                    if (m_CallModel == EditorCallModel.EditOrder)
                        OrderState = OrderState.New;
                    else if (m_CallModel == EditorCallModel.Query)
                        OrderState = OrderState.Executed;
                    else
                        OrderState = OrderState.All;
                }
                else
                    OrderState = OrderState;
                IsTempOrder = IsTempOrder;

                //barItemOrderCatalog.EditValue = selectOrderCatalog.Items[0];
                //barItemFilterStatus.EditValue = OrderState.All;

                // ��ʼ��������ҩ����еĲ�����Ϣ
                /*
                if (CoreBusinessLogic.BusinessLogic.UseMedicomPlug)
                    SetMedicomPassPatient();
                 */

                // ����ˢ�¹������İ�ť״̬
                ResetToolBarItemState();

                InitializeSkinTestResult();

                Enabled = true;
                orderToolBar.Visible = true;
                statusBar.Visible = (m_CallModel != EditorCallModel.EditSuite);
            }
            finally
            {
                HideWaitDialog();
            }
        }

        /// <summary>
        /// �������¿�ʼ���ں�ʱ�䣨��ȷ�����ӣ�
        /// </summary>
        private DateTime NewStartDateTime
        {
            get
            {
                if (OrderTemp.StartDateTime == DateTime.MinValue)
                    return m_UILogic.DefaultStartDateTime;

                if (OrderTemp.StartDateTime.TimeOfDay == TimeSpan.Zero)
                    return OrderTemp.StartDateTime.Date + m_UILogic.DefaultStartDateTime.TimeOfDay;
                else
                    return OrderTemp.StartDateTime.Date + new TimeSpan(
                       OrderTemp.StartDateTime.Hour, OrderTemp.StartDateTime.Minute, 0);
                //if ((m_TempStartDate == DateTime.MinValue)
                //   && (m_TempStartTime == TimeSpan.MinValue))
                //   return m_UILogic.DefaultStartDateTime;

                //if (m_TempStartTime == TimeSpan.MinValue)
                //   return m_TempStartDate.Date + m_UILogic.DefaultStartDateTime.TimeOfDay;
                //else
                //   return m_TempStartDate.Date
                //      + new TimeSpan(m_TempStartTime.Hours, m_TempStartTime.Minutes, 0);
            }
        }

        /// <summary>
        /// ��ǰFocused���ж�Ӧ��ҽ������
        /// </summary>
        private Order FocusedOrder
        {
            get
            {
                if (m_FocusedRowHandle >= 0)
                    return m_UILogic.CurrentView[m_FocusedRowHandle].OrderCache;
                else
                    return null;
            }
        }

        /// <summary>
        /// ���ص�ǰ�༭��ҽ�����ʹ�õ���ʱ����
        /// </summary>
        private Order OrderTemp
        {
            get
            {
                if (m_UILogic.IsTempOrder)
                    return _tempOrder;
                else
                    return _longOrder;
            }
        }
        private LongOrder _longOrder;
        private TempOrder _tempOrder;

        /// <summary>
        /// ҽ��������(��ֵ��ճ��ҽ��ʱ���õ�ҽ�����ݺͷ�������)
        /// </summary>
        private Collection<Order> OrderClipboard
        {
            get
            {
                if (_orderClipboard == null)
                    _orderClipboard = new Collection<Order>();
                return _orderClipboard;
            }
        }
        private Collection<Order> _orderClipboard;

        private OrderSuiteEditForm SuiteEditForm
        {
            get
            {
                if (_suiteEditForm == null)
                {
                    _suiteEditForm = new OrderSuiteEditForm();
                    _suiteEditForm.InitializeProperty(m_App.SqlHelper, m_App.CustomMessageBox);
                }
                return _suiteEditForm;
            }
        }
        private OrderSuiteEditForm _suiteEditForm;

        /// <summary>
        /// �Թ������в����п�ݼ��İ�ť�����ƣ��Ա�ֻ֤�ڶ�Grid�����ݽ��в���ʱ����Ч������Ӱ�쵽�������еı༭����
        /// </summary>
        private bool EnableShortCut
        {
            get { return _enableShortCut; }
            set
            {
                _enableShortCut = value;
                ResetToolBarItemState();
                //BindOrUnbindItemShortcut(value);
                //if (!value)
                //   advGridView.ClearSelection();
            }
        }
        private bool _enableShortCut;


        /*
        private List<MediIntfDrugInfo> MedicomDrugInfos
        {
            get
            {
                if (_medicomDrugInfos == null)
                    _medicomDrugInfos = new List<MediIntfDrugInfo>();
                return _medicomDrugInfos;
            }
        }
        private List<MediIntfDrugInfo> _medicomDrugInfos;
        */
        private bool IsInEditing
        {
            get
            {
                if (m_CallModel == EditorCallModel.EditOrder)
                    return (CurrentPatient != null);
                else if (m_CallModel == EditorCallModel.EditSuite)
                    return ((SuiteHelper != null) && (SuiteHelper.CurrentSuiteNo > 0));
                else
                    return true;
            }
        }
        #endregion

        #region ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="callModel">ָ������ģʽ</param>
        public AdviceEditor(IEmrHost app, EditorCallModel callModel)
        {
            try
            {
                m_WaitDialog = new WaitDialogForm("�����������", "����ִ�в��������Եȡ�");
                InitializeComponent();

                m_CallModel = callModel;
                CustomInitialize(app);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region public call methods
        /// <summary>
        /// �༭���ѯָ�����˵�ҽ������
        /// </summary>
        /// <param name="inpatient"></param>
        public void CallShowPatientOrder(Inpatient inpatient)
        {
            if (m_CallModel == EditorCallModel.EditSuite)
                throw new ApplicationException(ConstMessages.ExceptionNotOrderEdit);

            Cursor originalCursor = Cursor;
            Cursor = Cursors.WaitCursor;

            CurrentPatient = inpatient;

            Cursor = originalCursor;
            HideWaitDialog();
        }

        /// <summary>
        /// �༭����ҽ������
        /// </summary>
        /// <param name="suiteSerialNo">Ҫ�༭�ĳ���ҽ�����</param>
        public void CallEditSuiteOrder(decimal suiteSerialNo)
        {
            if (m_CallModel != EditorCallModel.EditSuite)
                throw new ApplicationException(ConstMessages.ExceptionNotOrderSuitEdit);

            Cursor originalCursor = Cursor;
            Cursor = Cursors.WaitCursor;

            SuiteHelper.CurrentSuiteNo = suiteSerialNo;

            Cursor = originalCursor;
            HideWaitDialog();
        }
        #endregion

        #region public methods
        /// <summary>
        /// �����ڹر�ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnParentFormClosing(object sender, FormClosingEventArgs e)
        {
            m_FocusInGrid = false;
            e.Cancel = !CheckCanExitOrSwitch(null);
        }

        /// <summary>
        /// ���˳�ǰ���л�����ʱ��������л�
        /// </summary>
        /// <param name="newPatient">�л�����²���,�˳�ʱ��null</param>
        /// <returns></returns>
        public bool CheckCanExitOrSwitch(Inpatient newPatient)
        {
            if ((IsDisposed) || (m_UILogic == null) || (CurrentPatient == null) || (m_CallModel == EditorCallModel.Query)
               || ((newPatient != null) && (CurrentPatient.NoOfFirstPage == newPatient.NoOfFirstPage)))
                return true;

            DialogResult result;
            if (HadChanged)
            {
                result = m_App.CustomMessageBox.MessageShow(ConstMessages.MsgPromptingSaveData
                   , CustomMessageBoxKind.QuestionYesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        DoSaveData();
                        if (!HadChanged)
                            return false;
                        break;
                    case DialogResult.No:
                        return true;
                    default:
                        return false;
                }
            }
            if ((m_CallModel == EditorCallModel.EditOrder) && (m_UILogic.HasNotSendData))
            {
                result = m_App.CustomMessageBox.MessageShow(ConstMessages.MsgPromptingSendData
                   , CustomMessageBoxKind.QuestionYesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        DoSendChangedDataToHIS();
                        if (!m_UILogic.HasNotSendData)
                            return false;
                        break;
                    case DialogResult.No:
                        return true;
                    default:
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �������ҽ��
        /// </summary>
        /// <param name="selectedContents">����ҽ�����ݺͷ������������</param>
        public void InsertSuiteOrder(object[,] selectedContents)
        {
            try
            {
                m_UILogic.CheckAllowInsertOrder(null);

                // ����Ĭ��Ϊ���
                m_UILogic.InsertSuiteOrder(selectedContents, null);
                MoveFocusedRowByHand(false);
            }
            catch (Exception e)
            {
                ShowErrorMessage(e.Message, CustomMessageBoxKind.ErrorOk);
            }
        }

        /// <summary>
        /// ��ҽ�����ݱ༭�������Form�ػ�Ĺ��ܼ����Դ����ض����¼�
        /// </summary>
        /// <param name="e"></param>
        public void AcceptFunctionKeyPress(KeyEventArgs e)
        {
            if ((contentEditor.Visible) && (m_UILogic.AllowEdit))
                contentEditor.AcceptFunctionKeyPress(e);
        }
        #endregion

        #region custom event handler
        /// <summary>
        /// ����µ�����ҽ���¼�
        /// </summary>
        public event EventHandler OperationOrderAdded
        {
            add { onOperationOrderAdded = (EventHandler)Delegate.Combine(onOperationOrderAdded, value); }
            remove { onOperationOrderAdded = (EventHandler)Delegate.Remove(onOperationOrderAdded, value); }
        }
        private EventHandler onOperationOrderAdded;

        public event EventHandler AfterSwitchOrderTable
        {
            add { onAfterSwitchOrderTable = (EventHandler)Delegate.Combine(onAfterSwitchOrderTable, value); }
            remove { onAfterSwitchOrderTable = (EventHandler)Delegate.Remove(onAfterSwitchOrderTable, value); }
        }
        private EventHandler onAfterSwitchOrderTable;

        private void FireAfterSwitchOrderTable(object sender, EventArgs e)
        {
            if (onAfterSwitchOrderTable != null)
                onAfterSwitchOrderTable(this, new EventArgs());
        }

        /// <summary>
        /// ҽ����ӡί��
        /// </summary>
        /// <param name="patient">Ҫ��ӡҽ���Ĳ���</param>
        /// <returns></returns>
        public delegate void PrintOrder(Inpatient patient);

        /// <summary>
        /// ���������Ϣ����
        /// </summary>
        public PrintOrder PrintCurrentPatientOrder
        {
            get { return _printCurrentPatientOrder; }
            set
            {
                _printCurrentPatientOrder = value;
                barItemPrint.Enabled = (value != null);
            }
        }
        private PrintOrder _printCurrentPatientOrder;
        #endregion

        #region private methods of initialize data
        private void CustomInitialize(IEmrHost app)
        {
            if (app == null)
                throw new ArgumentNullException();

            m_App = app;
            _longOrder = new LongOrder(); // ��ʱҽ��������Ҫ�ظ�����
            _tempOrder = new TempOrder();

            // �󶨿ؼ����¼�
            BindingEvents2Controls();

            BindUniformImageToControl();

            repItemDateEdit.EditValueChangedFiringMode = EditValueChangedFiringMode.Buffered;
            repItemTimeEdit.EditValueChangedFiringMode = EditValueChangedFiringMode.Buffered;

            // ���ڿؼ���Ҫ��NullDate���Ϳ����õ�ǰ���ڳ�ΪĬ������
            //itemDateEdit.NullDate = m_UILogic.DefaultStartDateTime.Date;

            //m_MedicomPass = null;
            CurrentPatient = null;
            CreateOrderEditControl();

            if (m_CallModel == EditorCallModel.EditSuite) // ά������ʱֻ��Ҫ��ʼ��һ��Grid��ʽ
                ResetGridStyle();

            HideWaitDialog();
        }

        private void ClearSurface()
        {
            if (advGridView.SelectedRowsCount > 0)
                advGridView.ClearSelection();
            //MedicomDrugInfos.Clear();
            Enabled = false;
            EnableShortCut = false;
        }

        private void InitializeUILogicObject()
        {
            if (m_UILogic == null)
            {
                SetWaitDialogCaption(ConstMessages.HintInitData);
                bool enabledOrderRules = CoreBusinessLogic.BusinessLogic.EnableOrderRules;
                //try
                //{
                //�����߼��������ʵ��
                m_UILogic = new UILogic(m_App, GridControl.InvalidRowHandle, GridControl.NewItemRowHandle, m_CallModel);
                //}
                //catch (CallRemotingException err)
                //{
                //   HideWaitDialog();
                //   m_App.CustomMessageBox.MessageShow(err.Message, CustomMessageBoxKind.ErrorYes);
                //}
                //catch
                //{
                //   throw;
                //}
                if (enabledOrderRules != CoreBusinessLogic.BusinessLogic.EnableOrderRules)
                {
                    HideWaitDialog();
                    m_App.CustomMessageBox.MessageShow(ConstMessages.MsgCanntGetRecipeRuleData, CustomMessageBoxKind.ErrorYes);
                }
                m_UILogic.AfterSwitchOrderTable += new EventHandler(DoAfterSwitchOrderTable);
                m_UILogic.AllowNewChanged += new EventHandler(AfterCurrentTableAllowNewChanged);
                m_UILogic.ContentBaseDataChanged += new EventHandler(AfterContentBaseDataChanged);
                m_UILogic.ProcessStarting += new EventHandler<ProcessHintArgs>(DoUILogicProcessStaring);
                if (m_CallModel == EditorCallModel.EditSuite)
                {
                    // �����ڴ���UILogic����¼����Ա�֤��ִ��CoreLogic�а󶨵��¼���
                    SuiteHelper.AfterSwitchSuite += new EventHandler(DoAfterSwitchSuite);
                }

                contentEditor.InitializeEditor(m_UILogic, m_App.SqlHelper);
                HideWaitDialog();
            }
        }

        private void SetUnboundColumnFieldName()
        {
            gridColStartDate.FieldName = OrderView.UNStartDate;
            gridColStartTime.FieldName = OrderView.UNStartTime;
            gridColContent.FieldName = OrderView.UNContent;
            gridColCeaseDate.FieldName = OrderView.UNCeaseDate;
            gridColCeaseTime.FieldName = OrderView.UNCeaseTime;
        }

        private void BindingEvents2Controls()
        {
            gridCtrl.DataSourceChanged += new EventHandler(GridDataSourceChanged);
            gridCtrl.MouseDown += new MouseEventHandler(DoAfterGridCtrlMouseDown);
            gridCtrl.KeyPress += new KeyPressEventHandler(GridKeyPress);
            advGridView.CustomUnboundColumnData += new CustomColumnDataEventHandler(GridCustomUnboundColumnData);
            advGridView.ValidateRow += new ValidateRowEventHandler(GridValidateRow);
            advGridView.InitNewRow += new InitNewRowEventHandler(GridInitNewRow);
            advGridView.FocusedColumnChanged += new FocusedColumnChangedEventHandler(GridFocusedColumnChanged);
            advGridView.FocusedRowChanged += new FocusedRowChangedEventHandler(GridFocusedRowChanged);
            advGridView.InvalidRowException += new InvalidRowExceptionEventHandler(SetInvalidRowExceptionMode);
            advGridView.SelectionChanged += new SelectionChangedEventHandler(SelectionChanged);
            advGridView.GotFocus += new EventHandler(GridViewGotFocus);
            advGridView.LostFocus += new EventHandler(GridViewLostFocus);
            advGridView.CustomDrawCell += new RowCellCustomDrawEventHandler(CustomDrawOrderGridCell);
            advGridView.DoubleClick += new EventHandler(GridRowDoubleClick);

            repItemContentEdit.QueryPopUp += new CancelEventHandler(GridContentEditPopup);
            repItemContentEdit.CloseUp += new CloseUpEventHandler(GridContentEditClose);

            repItemDateEdit.EditValueChanging += new ChangingEventHandler(StartDateEditValueChanging);
            repItemTimeEdit.EditValueChanging += new ChangingEventHandler(StartTimeEditValueChanging);

            barItemOrderCatalog.EditValueChanged += new EventHandler(ItemFilterOrderCatalogClick);
            barItemFilterStatus.EditValueChanged += new EventHandler(ItemFilterOrderStateClick);

            barItemSave.ItemClick += new ItemClickEventHandler(ItemSaveClick);
            barItemDelete.ItemClick += new ItemClickEventHandler(ItemDeleteNewOrderClick);
            barItemSetGroup.ItemClick += new ItemClickEventHandler(ItemSetGroupClick);
            barItemCancelGroup.ItemClick += new ItemClickEventHandler(ItemCancelGroupClick);
            barItemCancel.ItemClick += new ItemClickEventHandler(ItemCancelClick);
            barItemCease.ItemClick += new ItemClickEventHandler(ItemCeaseClick);
            barItemAudit.ItemClick += new ItemClickEventHandler(ItemAuditClick);
            barItemUp.ItemClick += new ItemClickEventHandler(ItemMoveNewOrderUpClick);
            barItemDown.ItemClick += new ItemClickEventHandler(ItemMoveNewOrderDownClick);
            barItemCheckOrder.ItemClick += new ItemClickEventHandler(DoCheckOrders);
            barItemDrugManual.ItemClick += new ItemClickEventHandler(DoShowPlugDrugInfoMenu);
            barItemSubmit.ItemClick += new ItemClickEventHandler(ItemSubmitClick);
            barItemLongOrder.CheckedChanged += new ItemClickEventHandler(ItemLongOrderClick);
            barItemTempOrder.CheckedChanged += new ItemClickEventHandler(ItemTempOrderClick);
            barItemStateAll.CheckedChanged += new ItemClickEventHandler(ItemStateAllClick);
            barItemStateNew.CheckedChanged += new ItemClickEventHandler(ItemStateNewClick);
            barItemStateAvailably.CheckedChanged += new ItemClickEventHandler(ItemStateAvailablyClick);
            barItemCut.ItemClick += new ItemClickEventHandler(ItemCutClick);
            barItemCopy.ItemClick += new ItemClickEventHandler(ItemCopyClick);
            barItemPaste.ItemClick += new ItemClickEventHandler(ItemPasteClick);
            barItemRefresh.ItemClick += new ItemClickEventHandler(ItemRefreshClick);
            barItemDrugInfo.ItemClick += new ItemClickEventHandler(DoShowDrugInfo);
            barItemPrint.ItemClick += new ItemClickEventHandler(ItemPrintClick);
            barItemExpandHerbDetail.ItemClick += new ItemClickEventHandler(ItemExpandHerbDetailClick);
            barItemExpandAllHerb.ItemClick += new ItemClickEventHandler(ItemExpandAllHerbClick);
            barItemCollapseHerbDetail.ItemClick += new ItemClickEventHandler(ItemCollapseHerbDetailClick);
            barItemCollapseAllHerb.ItemClick += new ItemClickEventHandler(ItemCollapseAllHerbClick);
            barItemAutoGroup.ItemClick += new ItemClickEventHandler(ItemAutoGroupClick);

            this.Leave += new EventHandler(LeaveAdviceEditor);
        }

        private void InitializeSkinTestResult()
        {
            SetWaitDialogCaption(ConstMessages.HintInitSkinTestResult);
            gridAllergic.DataSource = m_UILogic.SkinTestResultTable;
        }
        #endregion

        #region private methods of UI process
        private void CreateOrderEditControl()
        {
            if (m_CallModel != EditorCallModel.EditSuite)
                contentEditor = new OrderContentEditor(false);
            else
                contentEditor = new OrderContentEditor(true);
            contentEditor.UseRadioCatalogInputStyle = CoreBusinessLogic.BusinessLogic.UseRadioCatalogInputStyle;

            MinimumSize = new Size(contentEditor.SuitablyWidth, 400);
            Size = MinimumSize;
            panelContentEditor.Height = contentEditor.SuitablyHeight;
            panelContentEditor.Controls.Add(contentEditor);

            contentEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            contentEditor.Font = new Font("����", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            contentEditor.Location = new Point(0, 0);
            contentEditor.Margin = new Padding(0);
            contentEditor.Name = "contentEditor";
            //contentEditor.Size = new Size(734, 102);
            contentEditor.StartEdit += new EventHandler<DataCommitArgs>(BeforeContentEditStart);
            contentEditor.EditFinished += new EventHandler<DataCommitArgs>(AfterContentEditFinished);
            contentEditor.SelectedItemChanged += new EventHandler<OrderItemArgs>(DoAfterContentEditorSelectedItemChanged);
        }

        private void InitializeToolbar(EditorCallModel callModel)
        {
            // �����˳�����˰�ť
            barItemExit.Visibility = BarItemVisibility.Never;
            barItemAudit.Visibility = BarItemVisibility.Never;
            barItemPrint.Enabled = (PrintCurrentPatientOrder != null);

            if (callModel == EditorCallModel.EditOrder)
            {
                if ((CoreBusinessLogic.BusinessLogic.ConnectToHis)
                   && (!CoreBusinessLogic.BusinessLogic.AutoSyncData))
                    barItemSubmit.Visibility = BarItemVisibility.Always;
                else
                    barItemSubmit.Visibility = BarItemVisibility.Never;

                // �󶨺�����ҩ
                /*
                if ((CoreBusinessLogic.BusinessLogic.UseMedicomPlug)
                   && InitializeMedicomPass())
                {
                    barItemCheckOrder.Visibility = BarItemVisibility.Always;
                    barItemDrugManual.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    //m_MedicomPass = null;
                    barItemCheckOrder.Visibility = BarItemVisibility.Never;
                    barItemDrugManual.Visibility = BarItemVisibility.Never;
                }
                 */
            }
            else if (callModel == EditorCallModel.Query) // ��ѯģʽ��Ҫ���ñ༭��ť
            {
                barItemSave.Visibility = BarItemVisibility.Never;
                barItemSubmit.Visibility = BarItemVisibility.Never;
                barItemCut.Visibility = BarItemVisibility.Never;
                barItemCopy.Visibility = BarItemVisibility.Never;
                barItemPaste.Visibility = BarItemVisibility.Never;
                barItemDelete.Visibility = BarItemVisibility.Never;
                barItemUp.Visibility = BarItemVisibility.Never;
                barItemDown.Visibility = BarItemVisibility.Never;
                barItemCancel.Visibility = BarItemVisibility.Never;
                barItemCease.Visibility = BarItemVisibility.Never;
                barItemSetGroup.Visibility = BarItemVisibility.Never;
                barItemAutoGroup.Visibility = BarItemVisibility.Never;
                barItemCancelGroup.Visibility = BarItemVisibility.Never;
                barItemCheckOrder.Visibility = BarItemVisibility.Never;
                barItemDrugManual.Visibility = BarItemVisibility.Never;
            }
            else if (callModel == EditorCallModel.EditSuite) // 
            {
                barItemPrint.Visibility = BarItemVisibility.Never;
                barItemRefresh.Visibility = BarItemVisibility.Never;
                barItemSubmit.Visibility = BarItemVisibility.Never;
                barItemCancel.Visibility = BarItemVisibility.Never;
                barItemCease.Visibility = BarItemVisibility.Never;
                barItemCheckOrder.Visibility = BarItemVisibility.Never;
                barItemDrugManual.Visibility = BarItemVisibility.Never;
                barItemStateAll.Visibility = BarItemVisibility.Never;
                barItemStateAvailably.Visibility = BarItemVisibility.Never;
                barItemStateNew.Visibility = BarItemVisibility.Never;
                barItemSkinTestInfo.Visibility = BarItemVisibility.Never;
                barItemExpandHerbDetail.Visibility = BarItemVisibility.Never;
                barItemCollapseHerbDetail.Visibility = BarItemVisibility.Never;
                barItemExpandAllHerb.Visibility = BarItemVisibility.Never;
                barItemCollapseAllHerb.Visibility = BarItemVisibility.Never;
            }
        }

        private void BindUniformImageToControl()
        {
            InitializeImageList();

            barItemSave.ImageIndex = 0;
            barItemSubmit.ImageIndex = 2;
            barItemPrint.ImageIndex = 4;
            barItemCut.ImageIndex = 6;
            barItemCopy.ImageIndex = 8;
            barItemPaste.ImageIndex = 10;
            barItemDelete.ImageIndex = 12;
            barItemUp.ImageIndex = 14;
            barItemDown.ImageIndex = 16;
            barItemCancel.ImageIndex = 18;
            barItemCease.ImageIndex = 20;
            barItemSetGroup.ImageIndex = 22;
            barItemCancelGroup.ImageIndex = 24;
            barItemCheckOrder.ImageIndex = 26;
            barItemDrugManual.ImageIndex = 28;
            barItemExit.ImageIndex = 30;
            barItemRefresh.ImageIndex = 32;
            barItemAutoGroup.ImageIndex = 34;

            barItemSave.ImageIndexDisabled = 1;
            barItemSubmit.ImageIndexDisabled = 3;
            barItemPrint.ImageIndexDisabled = 5;
            barItemCut.ImageIndexDisabled = 7;
            barItemCopy.ImageIndexDisabled = 9;
            barItemPaste.ImageIndexDisabled = 11;
            barItemDelete.ImageIndexDisabled = 13;
            barItemUp.ImageIndexDisabled = 15;
            barItemDown.ImageIndexDisabled = 17;
            barItemCancel.ImageIndexDisabled = 19;
            barItemCease.ImageIndexDisabled = 21;
            barItemSetGroup.ImageIndexDisabled = 23;
            barItemCancelGroup.ImageIndexDisabled = 25;
            barItemCheckOrder.ImageIndexDisabled = 27;
            barItemDrugManual.ImageIndexDisabled = 29;
            barItemExit.ImageIndexDisabled = 31;
            barItemRefresh.ImageIndexDisabled = 33;
            barItemAutoGroup.ImageIndexDisabled = 35;

            barItemLegendNew.Glyph = GetStaticItemGlyph(OrderState.New, true, barItemLegendNew.Caption);
            barItemLegendAudit.Glyph = GetStaticItemGlyph(OrderState.Audited, true, barItemLegendAudit.Caption);
            barItemLegendCancel.Glyph = GetStaticItemGlyph(OrderState.Cancellation, true, barItemLegendCancel.Caption);
            barItemLegendExecuted.Glyph = GetStaticItemGlyph(OrderState.Executed, true, barItemLegendExecuted.Caption);
            barItemLegendCeased.Glyph = GetStaticItemGlyph(OrderState.Ceased, true, barItemLegendCeased.Caption);
            barItemLegendNotSynch.Glyph = GetStaticItemGlyph(OrderState.All, false, barItemLegendNotSynch.Caption);
        }

        private Image GetStaticItemGlyph(OrderState orderState, bool hadSynched, string staticText)
        {
            return CustomDrawOperation.CreateColorLegend(
               CustomDrawOperation.GetBackColorByState(orderState, false, hadSynched)
               , CustomDrawOperation.GetForeColorByState(orderState, false, hadSynched)
               , staticText);
        }

        private void InitializeImageList()
        {
            imageListSmall.ColorDepth = ColorDepth.Depth24Bit;
            imageListSmall.ImageSize = new Size(16, 16);
            imageListSmall.TransparentColor = Color.Magenta;
            imageListSmall.Images.AddRange(new Image[] {
              ResourceManager.GetSmallIcon(ResourceNames.Save, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Save, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.TransferData, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.TransferData, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Print, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Print, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Cut, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Cut, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Copy, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Copy, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Paste, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Paste, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Delete, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Delete, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.ArrowUp, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.ArrowUp, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.ArrowDown, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.ArrowDown, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Cancel, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Cancel, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Stop, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Stop, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.SetGroup, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.SetGroup, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.CancelGroup, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.CancelGroup, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.SpellCheck, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.SpellCheck, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Help, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Help, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Exit, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Exit, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.Refresh, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.Refresh, IconType.Disable)
            , ResourceManager.GetSmallIcon(ResourceNames.AutoGroup, IconType.Normal)
            , ResourceManager.GetSmallIcon(ResourceNames.AutoGroup, IconType.Disable)
         });
        }

        private void ResetToolBarItemState()
        {
            if (m_CallModel != EditorCallModel.Query)
            {
                EditProcessFlag flags = 0;
                try
                {
                    if (m_UILogic != null)
                        flags = m_UILogic.GetBarItemStatus(advGridView.GetSelectedRows());
                }
                catch
                { }
                barItemDelete.Enabled = (((flags & EditProcessFlag.Delete) > 0) && EnableShortCut);
                barItemCancel.Enabled = ((flags & EditProcessFlag.Cancel) > 0);
                barItemCease.Enabled = ((flags & EditProcessFlag.Cease) > 0);
                barItemUp.Enabled = ((flags & EditProcessFlag.MoveUp) > 0);
                barItemDown.Enabled = ((flags & EditProcessFlag.MoveDown) > 0);
                barItemSetGroup.Enabled = ((flags & EditProcessFlag.SetGroup) > 0);
                barItemAutoGroup.Enabled = AllowAddNew;
                barItemCancelGroup.Enabled = ((flags & EditProcessFlag.CancelGroup) > 0);
                if ((m_UILogic != null) && (m_FocusedRowHandle == m_UILogic.NewItemRowHandle))
                    barItemSave.Enabled = m_UILogic.HadChanged;
                else
                    barItemSave.Enabled = ((flags & EditProcessFlag.Save) > 0);

                barItemSubmit.Enabled = ((m_UILogic != null) && m_UILogic.HasNotSendData);

                barItemAudit.Enabled = ((flags & EditProcessFlag.Audit) > 0);

                barItemCut.Enabled = (((flags & EditProcessFlag.Cut) > 0) && EnableShortCut);
                barItemCopy.Enabled = (((flags & EditProcessFlag.Copy) > 0) && EnableShortCut);
                // ճ��--��ճ����ʱ�����ж��Ƿ��ִ�в���������ճ��ʱҪ������ʾ��(�ѿ���Ժҽ�������ֹճ��)
                barItemPaste.Enabled = ((OrderClipboard.Count > 0) && EnableShortCut && (!HasOutHospitalOrder));

                barItemCheckOrder.Enabled = ((m_UILogic != null) && AllowAddNew /*&& (m_MedicomPass != null)*/);
                barItemDrugInfo.Enabled = (/*(m_MedicomPass != null) && */(advGridView.SelectedRowsCount == 1)
                   && (OrderTemp.Content != null)
                   && ((OrderTemp.Content.OrderKind == OrderContentKind.Druggery) || (OrderTemp.Content.OrderKind == OrderContentKind.OutDruggery)));

                // ���Ʋ�ҩ��ϸչ��������ز˵�
                barItemExpandHerbDetail.Enabled = ((flags & EditProcessFlag.IsHerbSummary) > 0);
                barItemCollapseHerbDetail.Enabled = ((flags & EditProcessFlag.IsHerbDetail) > 0);
            }
        }

        private void ResetGridStyle()
        {
            //-- advGridView.BeginUpdate(); // ���ⲿ��������
            ResetControlFont();

            advGridView.RowHeight = CustomDrawOperation.GridSetting.RowHeight;

            ResetGridViewBandAndColumns();

            //advGridView.EndDataUpdate();
        }

        private void ResetGridViewBandAndColumns()
        {
            // ����Դ�ı�ʱ����Grid��������ʾ���м���ߴ�
            GridBand[] bands;
            BandedGridColumn col;

            advGridView.Bands.Clear();
            if (m_CallModel != EditorCallModel.EditSuite)
            {
                TypeGridBand[] bandSetting = m_UILogic.CurrentBandSettings;
                bands = new GridBand[bandSetting.Length];
                for (int index = 0; index < bands.Length; index++)
                {
                    switch (bandSetting[index].BandName)
                    {
                        case OrderGridBandName.bandBeginInfo:
                            bands[index] = bandBeginInfo;
                            break;
                        case OrderGridBandName.bandAuditInfo:
                            bands[index] = bandAuditInfo;
                            break;
                        case OrderGridBandName.bandExecuteInfo:
                            bands[index] = bandExecuteInfo;
                            break;
                        case OrderGridBandName.bandCeaseInfo:
                            bands[index] = bandCeaseInfo;
                            break;
                        default:
                            throw new IndexOutOfRangeException(String.Format(ConstMessages.ExceptionFormatNotFindBand,
                               bandSetting[index].BandName));
                    }

                    bands[index].Columns.Clear();
                    foreach (string colName in bandSetting[index].ColumnNames)
                    {
                        col = advGridView.Columns[colName];
                        if (col == null)
                            throw new IndexOutOfRangeException(String.Format(ConstMessages.ExceptionFormatNotFindColumn,
                               colName));
                        col.Visible = true;
                        col.Width = CustomDrawOperation.GridSetting.GetColumnWidth(colName);
                        col.Caption = CustomDrawOperation.GridSetting.GetColumnCaption(colName);
                        bands[index].Columns.Add(col);
                    }
                }

                // ֱ����Ӻ�����ҩ�������
                if ((m_CallModel == EditorCallModel.EditOrder) && CoreBusinessLogic.BusinessLogic.UseMedicomPlug)
                    bands[0].Columns.Insert(0, gridColCheckResult);
            }
            else
            {
                // ά������ҽ��ʱֻ��Ҫҽ��������
                bands = new GridBand[] { bandBeginInfo };
                bandBeginInfo.Columns.Clear();
                bandBeginInfo.Columns.Add(gridColContent);
            }

            advGridView.Bands.AddRange(bands);
            advGridView.OptionsView.ShowBands = CustomDrawOperation.GridSetting.ShowBand;
        }

        private void ResetControlFont()
        {
            gridCtrl.Font = CustomDrawOperation.GridSetting.GridFont.Font;

            foreach (AppearanceObject ap in advGridView.Appearance)
                ap.Font = gridCtrl.Font;

            //contentEditor.ResetControlFont(gridCtrl.Font);
        }

        /// <summary>
        /// ���ݵ�ǰѡ�е��к����ж�Grid�Ƿ�ɱ༭
        /// </summary>
        /// <param name="columnName">ѡ�е�����</param>
        /// <param name="rowHandle">ѡ�е��к�</param>
        private void SetGridEditable(string columnName, int rowHandle)
        {
            advGridView.OptionsBehavior.Editable = ((m_UILogic != null)
                       && m_UILogic.JudgeCellCanEdit(columnName, rowHandle));

            if (advGridView.OptionsBehavior.Editable)
            {
                advGridView.FocusRectStyle = DrawFocusRectStyle.CellFocus;
                advGridView.OptionsSelection.EnableAppearanceFocusedCell = true;
                advGridView.OptionsSelection.EnableAppearanceFocusedRow = true;
            }
            else
            {
                advGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                advGridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            }
        }

        private void ChangeVisibleOfEditRegion(bool visible)
        {
            if (panelContentEditor.Visible != visible)
            {
                panelContentEditor.Visible = visible;
                barItemEditRegion.Checked = visible;
                if (visible)
                {
                    //btnAdd.Focus();
                    PrepareEditNewOrder();
                }
            }
        }

        private void ShowOperationHint(string hintMessage)
        {
            barItemHint.Caption = "��ʾ��" + hintMessage;
        }

        /// <summary>
        /// �ֹ��ƶ�Focused ����
        /// </summary>
        /// <param name="toLast">True��Focused���һ�� False��Focused��һ��</param>
        private void MoveFocusedRowByHand(bool toLast)
        {
            if (toLast)
            {
                advGridView.MoveLast();
                advGridView.TopRowIndex = advGridView.FocusedRowHandle;
            }
            else
                advGridView.MoveNext();
        }
        #endregion

        #region private methods of WaitDialog
        private void SetWaitDialogCaption(string caption)
        {
            if (m_WaitDialog != null)
            {
                if (!m_WaitDialog.Visible)
                    m_WaitDialog.Visible = true;
                m_WaitDialog.Caption = caption;
            }
        }

        private void HideWaitDialog()
        {
            if (m_WaitDialog != null)
                m_WaitDialog.Hide();
        }
        #endregion

        #region private methods of normal
        /// <summary>
        /// ��ָ���и���ʱ������ֵ
        /// </summary>
        /// <param name="rowHandle"></param>
        private void SetTempVarialbesFromRow(int rowHandle)
        {
            if (m_UILogic.CheckRowHandleIsValidate(rowHandle))
            {
                OrderTemp.StartDateTime = m_UILogic.CurrentView[rowHandle].OrderCache.StartDateTime;
                OrderTemp.Content = PersistentObjectFactory.CloneEopBaseObject(
                   m_UILogic.CurrentView[rowHandle].OrderCache.Content) as OrderContent;
            }
            else
            {
                OrderTemp.StartDateTime = DateTime.MinValue;
                OrderTemp.Content = null;
            }
        }

        /// <summary>
        /// ΪUnbound�л�ȡָ���С�ָ���е�ֵ
        /// </summary>
        /// <param name="rowIndex">ָ�����к�</param>
        /// <param name="fieldName">ָ��������</param>
        /// <returns>ֵ</returns>
        private object GetCustomColumnData(int rowIndex, string fieldName)
        {
            //// ���ڲ���Grid��ֱ��������У����Կ���ֱ�Ӵ����ݼ���ȡ����
            ////return m_UILogic.CurrentOrderTable.GetCustomColumnData(rowIndex, fieldName);

            // ����ǶԵ�ǰ�༭�н���ȡֵ������ͨ��У����ǰʹ����ʱ�����е�ֵ���и�ֵ��
            // �ڰ�ESCȡ���Ե�ǰ�б༭ʱ��ʹ��ԭʼ��¼��ֵ�����ҳ�ʼ����ʱ����
            bool indexValidated = m_UILogic.CheckRowHandleIsValidate(rowIndex);
            if ((!indexValidated) || (rowIndex == m_FocusedRowHandle))
            {
                bool useOriginal = (m_HadError && m_HadPressESC && (indexValidated)); // ����Ƿ�ʹ��ԭʼ����
                switch (fieldName)
                {
                    case OrderView.UNStartDate:
                        if (useOriginal) // ���������ʱ������ֵ�����ǵ���ͳһ�ĸ�ֵ��������ԭ���Ǳ����ε��ø�ֵ����
                            OrderTemp.StartDateTime = m_UILogic.CurrentView[rowIndex].OrderCache.StartDateTime;
                        else if (OrderTemp.StartDateTime.Date == DateTime.MinValue)
                            OrderTemp.StartDateTime = m_UILogic.DefaultStartDateTime;
                        return UILogic.ConvertToDateString(OrderTemp.StartDateTime);
                    case OrderView.UNStartTime:
                        if (useOriginal)
                            OrderTemp.StartDateTime = m_UILogic.CurrentView[rowIndex].OrderCache.StartDateTime;
                        else if (OrderTemp.StartDateTime.TimeOfDay == TimeSpan.Zero)
                            OrderTemp.StartDateTime = m_UILogic.DefaultStartDateTime;
                        return UILogic.ConvertToTimeString(OrderTemp.StartDateTime.TimeOfDay);
                }
            }

            return m_UILogic.CurrentOrderTable.GetCustomColumnData(rowIndex, fieldName);
        }

        /// <summary>
        /// ��ͳһ�ķ�ʽ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ����</param>
        /// <param name="errorKind">��������</param>
        private void ShowErrorMessage(string message, CustomMessageBoxKind errorKind)
        {
            m_UILogic.CustomMessageBox.MessageShow(message, errorKind);
        }

        /// <summary>
        /// ���ָ�����к��Ƿ�ѡ��
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        private bool CheckRowIsInSelection(int rowHandle)
        {
            if (advGridView.SelectedRowsCount == 0)
                return false;

            foreach (int handle in advGridView.GetSelectedRows())
                if (handle == rowHandle)
                    return true;

            return false;
        }

        private void PrepareEditNewOrder()
        {
            contentEditor.InitializeDefaultValue(DateTime.MinValue, null);
        }

        private int GetAimPositionOfNewOrder(DataCommitType commitType)
        {
            switch (commitType)
            {
                case DataCommitType.Add:
                    return advGridView.RowCount;
                case DataCommitType.Modify:
                    return m_FocusedRowHandle;
                case DataCommitType.Insert:
                    if (m_FocusedRowHandle < 0)
                        return 0;
                    else
                        return m_FocusedRowHandle + 1;
                default:
                    return -1;
            }
        }

        private object[,] ConvertOrderToObjectArray(Collection<Order> orders)
        {
            object[,] result = new object[orders.Count, 2];
            for (int index = 0; index < orders.Count; index++)
            {
                result[index, 0] = orders[index].Content;
                result[index, 1] = orders[index].GroupPosFlag;
            }
            return result;
        }

        #endregion

        #region private methods of operation process
        private void CommitOrderEdit(DataCommitType commitType)
        {
            try
            {
                UpdateContentFlag updateFlag = UpdateContentFlag.StartDate
                   | UpdateContentFlag.Content;
                OrderTemp.StartDateTime = contentEditor.StartDateTime;
                OrderTemp.Content = contentEditor.NewOrderContent;
                if (!IsTempOrder) // ����ҽ��Ҫ����ֹͣʱ��
                {
                    DateTime ceaseDate = OrderTemp.StartDateTime.AddDays(contentEditor.ExecuteDays);

                    if (contentEditor.ExecuteDays > 0)
                    {
                        _longOrder.CeaseOrder(m_App.User.DoctorId, ceaseDate, OrderCeaseReason.Natural);
                        updateFlag |= UpdateContentFlag.CeaseDate;
                    }
                    else if ((_longOrder.CeaseInfo != null) && (_longOrder.CeaseInfo.HadInitialized))
                    {
                        // ���ֹͣ����
                        _longOrder.CancelInfo.SetPropertyValue(null, DateTime.MinValue);
                        updateFlag |= UpdateContentFlag.CeaseDate;
                    }
                }
                // �����ҽ��Ҫ�����λ�û�Ҫ�޸ĵ�ҽ�����ڵ�λ��
                int rowIndex = GetAimPositionOfNewOrder(commitType);

                if (rowIndex != -1)
                {
                    try
                    {
                        m_UILogic.CheckOrderValueBeforeSet(rowIndex, OrderTemp, updateFlag);
                        if (m_CallModel == EditorCallModel.EditOrder)
                            m_UILogic.CheckRecipeRule(OrderTemp.Content);
                    }
                    catch (DataCheckException err)
                    {
                        if (err.WarnningLevel == 0)
                        {
                            if (m_App.CustomMessageBox.MessageShow(err.Message + "\r\n" + "�Ƿ������", CustomMessageBoxKind.QuestionYesNo)
                               == DialogResult.No)
                                return;
                        }
                        else
                            throw err;
                    }
                    // ���¶�Ӧ��������
                    if (commitType == DataCommitType.Modify)
                        m_UILogic.SetNewOrderElementValue(rowIndex, OrderTemp, updateFlag);
                    else
                    {
                        // ������ҽ��������ҽ����
                        Order newOrder = m_UILogic.CurrentOrderTable.NewOrder();
                        m_UILogic.InitNewOrderValue(newOrder, OrderTemp);
                        m_UILogic.InsertOrder(rowIndex, newOrder);
                        //����ˢ��һ��ҽ������
                        newOrder.Content.ResetContentOutputText();
                        // modified by zhouhui ��ˢ����gridctrl����Դ���ٶ�λ����
                        gridCtrl.RefreshDataSource();
                        advGridView.ClearSelection();
                        advGridView.FocusedRowHandle = rowIndex;
                    }

                    // ���蹤������ť��״̬
                    ResetToolBarItemState();

                    PrepareEditNewOrder();
                    return;
                }
                else
                {
                    throw new DataCheckException(ConstMessages.ExceptionCantInsertOrder, ConstMessages.ExceptionTitleOrder);
                }
            }
            catch (Exception err)
            {
                m_App.CustomMessageBox.MessageShow(err.Message, CustomMessageBoxKind.ErrorOk);
            }

        }

        private void DoSaveData()
        {
            if (m_CallModel == EditorCallModel.EditOrder)
            {
                try
                {
                    ProcessSaveData();
                    HideWaitDialog();
                    ResetToolBarItemState();
                }
                catch (ArgumentNullException err)
                {
                    HideWaitDialog();
                    ShowErrorMessage(err.Message, CustomMessageBoxKind.ErrorOk);
                }
                catch (DataCheckException err)
                {
                    HideWaitDialog();
                    ShowErrorMessage(err.Message, CustomMessageBoxKind.ErrorOk);
                    // ��λ��������С���

                }
                catch
                {
                    HideWaitDialog();
                    ShowErrorMessage(ConstMessages.FailedSaveData, CustomMessageBoxKind.ErrorOk);
                }
            }
            else
            {
                try
                {
                    m_UILogic.SaveCurrentSuiteDetailData();
                }
                catch
                {
                    ShowErrorMessage(ConstMessages.FailedSaveSuiteDetail, CustomMessageBoxKind.ErrorOk);
                }
            }
        }

        private bool ProcessSaveData()
        {
            string caption;
            try
            {
                /* if (m_MedicomPass != null)
                 {
                     CheckNewOrderUseMedicom();
                     if (MedicomDrugInfos.Count > 0)
                     {
                         foreach (MediIntfDrugInfo drugInfo in MedicomDrugInfos)
                         {
                             switch (drugInfo.Warn)
                             {
                                 case PassWarnType.Lower:
                                 case PassWarnType.Higher:
                                 case PassWarnType.Normal:
                                 case PassWarnType.Critical:
                                     if (drugInfo.OrderType == "1")
                                         caption = ConstNames.TempOrder;
                                     else
                                         caption = ConstNames.LongOrder;

                                     if (m_App.CustomMessageBox.MessageShow(String.Format(ConstMessages.FormatMedicomCheckNotPass, caption)
                                        , CustomMessageBoxKind.QuestionYesNo) == DialogResult.No)
                                         throw new DataCheckException(ConstMessages.MsgSaveDataAfterModified, caption);
                                     else
                                         break;
                             }
                         }
                     }
                 }
                 */
                m_UILogic.SaveOrderTableData(false);
                HideWaitDialog();
                //SetToolBarItemStatus();
                return true;
            }
            catch (DataCheckException err)
            {
                HideWaitDialog();
                if (err.WarnningLevel == 0)
                {
                    if (Convert.ToBoolean(err.DataName))
                        caption = ConstNames.TempOrder;
                    else
                        caption = ConstNames.LongOrder;
                    if (m_App.CustomMessageBox.MessageShow(String.Format(ConstMessages.FormatOrderSaveWarning, caption, err.Message), CustomMessageBoxKind.QuestionYesNo)
                       == DialogResult.No)
                        return false;
                    else
                    {
                        m_UILogic.SaveOrderTableData(true);
                        //SetToolBarItemStatus();
                        return true;
                    }
                }
                else
                    throw err;
            }
            catch
            {
                HideWaitDialog();
                throw;
            }
        }

        private void DoSendChangedDataToHIS()
        {
            try
            {
                if (ProcessSaveData())
                {
                    m_UILogic.ManualSynchDataToHIS();
                    HideWaitDialog();
                    ResetToolBarItemState();
                    ShowErrorMessage(ConstMessages.MsgSuccessSendData, CustomMessageBoxKind.InformationOk);
                }
            }
            catch (ArgumentNullException err)
            {
                HideWaitDialog();
                ShowErrorMessage(err.Message, CustomMessageBoxKind.ErrorOk);
            }
            catch (DataCheckException err)
            {
                HideWaitDialog();
                ShowErrorMessage(err.Message, CustomMessageBoxKind.ErrorOk);
            }
            catch
            {
                HideWaitDialog();
                ShowErrorMessage(ConstMessages.FailedSendDataToHis, CustomMessageBoxKind.ErrorOk);
            }
        }
        #endregion

        #region event handle
        private void DoAfterSwitchOrderTable(object sender, EventArgs e)
        {
            if (IsInEditing && (gridCtrl.MainView != null))
            {
                contentEditor.ResetDataOfWordbook();
                PrepareEditNewOrder();
                gridCtrl.DataSource = null;
                gridCtrl.DataSource = m_UILogic.CurrentOrderTable;
                //�˴�����������Ϊǿ���õ�ǰ��grid��ȡ����
                //�Դ���gridviewgotfoucs�¼�
                contentEditor.Focus();
                gridCtrl.Focus();
                m_FocusInGrid = true;

                if (m_FocusedRowHandle < 0)
                    ShowOperationHint(m_UILogic.GetHintOfEditOperation(-1));
                // ��λ�����һ��
                advGridView.MoveLast();

                FireAfterSwitchOrderTable(sender, e);
            }
        }

        /// <summary>
        /// ҽ�����ݵĶ�Ӧ�Ļ������ݷ����仯�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterContentBaseDataChanged(object sender, EventArgs e)
        {
            // ����ҽ�����ݱ༭���Ļ�������
            contentEditor.ResetDataOfWordbook();
        }

        /// <summary>
        /// ��ǰ�༭��ҽ�����Ƿ�����������־�����仯�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterCurrentTableAllowNewChanged(object sender, EventArgs e)
        {
            ChangeVisibleOfEditRegion(AllowAddNew);
            FireAfterSwitchOrderTable(this, new EventArgs());
        }

        private void AfterContentEditFinished(object sender, DataCommitArgs e)
        {
            CommitOrderEdit(e.CommitType);
            e.Handled = true;
        }

        private void BeforeContentEditStart(object sender, DataCommitArgs e)
        {
            switch (e.CommitType)
            {
                case DataCommitType.Add:
                    ShowOperationHint(ConstMessages.OpHintAddNewOrder);
                    break;
                case DataCommitType.Modify:
                    ShowOperationHint(ConstMessages.OpHintModifyData);
                    break;
                default:
                    ShowOperationHint("");
                    break;
            }
            e.Handled = true;
            EnableShortCut = false;
        }

        private void DoUILogicProcessStaring(object sender, ProcessHintArgs e)
        {
            SetWaitDialogCaption(e.ProcessHint);
        }

        private void LeaveAdviceEditor(object sender, EventArgs e)
        {
            EnableShortCut = false;
        }

        private void DoAfterSwitchSuite(object sender, EventArgs e)
        {
            if (SuiteHelper.CurrentSuiteNo <= 0)
            {
                Enabled = false;
                EnableShortCut = false;
                orderToolBar.Visible = false;
                statusBar.Visible = false;
                ChangeVisibleOfEditRegion(false);
            }
            else
            {
                Enabled = true;
                orderToolBar.Visible = true;
                statusBar.Visible = (m_CallModel == EditorCallModel.EditOrder);

                IsTempOrder = IsTempOrder;

                InitializeToolbar(m_CallModel);
                barItemLongOrder.Checked = true;

                // ����ˢ�¹������İ�ť״̬
                ResetToolBarItemState();

                ChangeVisibleOfEditRegion(true);

                HideWaitDialog();
            }
        }
        #endregion

        #region event handle of order grid
        private void GridDataSourceChanged(object sender, EventArgs e)
        {
            if (gridCtrl.DataSource != null)
            {
                // �ڽ������洦��ǰһ��Ҫ�����ؼ��������ʹ���������ʱ��������ˢ��
                advGridView.BeginUpdate();

                SetUnboundColumnFieldName();// ����GridView��Unbound�ж�Ӧ���ֶ���
                MoveFocusedRowByHand(true);// �������������л����һ��ҽ��
                m_FocusedRowHandle = advGridView.FocusedRowHandle;// Ԥ�ȳ�ʼ��������ʱ����
                // �л�����Դʱ��������ں���ʱ�ļ�¼��һ���࣬�򲻻ᴥ��FocusedRow�ı��¼���
                // Ҳ�Ͳ��ܸ���ʱ������ֵ������������ǿ�Ƶ�����ʱ������ֵ����
                SetTempVarialbesFromRow(m_FocusedRowHandle);
                m_UpdateFlag = 0;
                if (m_CallModel != EditorCallModel.EditSuite)
                    ResetGridStyle();// ����Grid����ʽ

                advGridView.EndUpdate();

                ChangeVisibleOfEditRegion(AllowAddNew); // �л��༭�����Visible״̬
            }
        }

        private void GridInitNewRow(object sender, InitNewRowEventArgs e)
        {
            //// ��ʼ������ҽ��
            //Order newOrder = (advGridView.GetRow(e.RowHandle) as OrderView).OrderCache;

            //m_UILogic.InitNewOrderValue(newOrder, NewStartDateTime, OrderTemp.Content);

            //// ͬ����ʼʱ�����
            //OrderTemp.StartDateTime = newOrder.StartDateTime;

            //m_UpdateFlag = UpdateContentFlag.StartDate | UpdateContentFlag.Content;
        }

        private void GridCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            // ע�⣺Unbound�е�FieldName���ܺ����ݼ��е��ֶ����ظ���
            // Ŀǰ��ȡUnbound�е�ֵʱ��ͨ��OrderView����������ģ�
            // ����Unbound�е�FieldNameҲ��������ָ���ģ�Ҫ����OrderView�еĶ��塣
            if (e.IsGetData)
            {
                if (e.Column == gridColCheckResult)
                    e.Value = GetDrugWarnPicture(e.ListSourceRowIndex);
                else
                    e.Value = GetCustomColumnData(e.ListSourceRowIndex, e.Column.FieldName);
            }
        }

        private void GridFocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            // ���ݾ۽��ĵ�Ԫ���ж�Grid�Ƿ�ɱ༭
            SetGridEditable(e.FocusedColumn.FieldName, advGridView.FocusedRowHandle);
            gridCtrl.Refresh();
        }

        private void GridFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            // ���ݾ۽��ĵ�Ԫ���ж�Grid�Ƿ�ɱ༭
            SetGridEditable(advGridView.FocusedColumn.FieldName, e.FocusedRowHandle);

            //// ��Ϊ�����е����ݵ�һ�α༭��ɺ�ᴥ�������иı��¼���
            //// Ϊ�˲������ʱ����,�����������Ҫ��������Ĵ���
            //if (((e.FocusedRowHandle == m_UILogic.InvalidRowHandle)
            //      && (e.PrevFocusedRowHandle == m_UILogic.NewItemRowHandle))
            //   || ((e.PrevFocusedRowHandle == m_UILogic.InvalidRowHandle)
            //      && (e.FocusedRowHandle == m_UILogic.NewItemRowHandle)))
            //   return;

            // ��ʼ����ʱ����
            m_FocusedRowHandle = e.FocusedRowHandle;
            m_UpdateFlag = 0;

            // �Ѵ��ڵ�ҽ����¼���򽫿ɱ༭��ֵ���浽��ʱ���������������ʱ����������ʱ�����ݣ�
            // ������InitNewRowǰ�ᴥ������FocusedRowChanged�¼���
            SetTempVarialbesFromRow(e.FocusedRowHandle);
            if (panelContentEditor.Visible && contentEditor.IsModifyData)
                PrepareEditNewOrder();

            //// ɾ��ҽ�������п�ʼʱ�䡢ҽ������Ϊ�յļ�¼��Ŀ���Ƿ�ֹ���ֶ���û��������������ҽ��
            //if ((e.PrevFocusedRowHandle > 0) && (e.PrevFocusedRowHandle != e.FocusedRowHandle))
            //{
            //   advGridView.FocusedRowChanged -= new FocusedRowChangedEventHandler(DoFocusedRowChanged);
            //   advGridView.BeginInit();
            //   m_UILogic.DeleteAbnormalNewOrders();
            //   advGridView.EndInit();
            //   advGridView.FocusedRowChanged += new FocusedRowChangedEventHandler(DoFocusedRowChanged);
            //}

            ShowOperationHint(m_UILogic.GetHintOfEditOperation(m_FocusedRowHandle));
            CloseDrugInfoForm();
            // ���蹤������ť��״̬
            ResetToolBarItemState();
        }

        private void GridValidateRow(object sender, ValidateRowEventArgs e)
        {
            // �����ݽ��м���У��
            try
            {
                m_UILogic.CheckOrderValueBeforeSet(e.RowHandle, OrderTemp, m_UpdateFlag);
                e.Valid = true;
                //// (����ֻ������Grid�б༭��ʼʱ�䣬ʱ���޸ĺ�ֱ�Ӹ���Row�����Բ���Ҫ��ȥ��������)
                // ���¶�Ӧ��������
                m_UILogic.SetNewOrderElementValue(e.RowHandle, OrderTemp, m_UpdateFlag);
            }
            catch (DataCheckException err)
            {
                if (m_FocusInGrid)
                {
                    // Ҫ����ϴε���ʾ��Ϣ
                    if (advGridView.HasColumnErrors)
                        advGridView.ClearColumnErrors();

                    advGridView.SetColumnError(advGridView.Columns[err.DataName], err.Message);
                }
                else
                {
                    advGridView.DeleteRow(e.RowHandle);
                }
                e.Valid = false;
            }
            m_HadError = !e.Valid;
            m_HadPressESC = false;
        }

        private void GridRowDoubleClick(object sender, EventArgs e)
        {
            if ((m_UILogic.AllowEdit) && (FocusedOrder != null) && (FocusedOrder.State == OrderState.New)
               && AllowAddNew) // ���뵥��Ҫ�������뵥�༭,�������뵥ҽ������ֱ��ɾ��,
            // ����copy�������������䲢ȥ���˶�DeleteItem��Enabled�ж�
            {
                TempOrder tmp = FocusedOrder as TempOrder;
                //if (tmp != null && tmp.ApplySerialNo != 0)
                //{
                //   NetWorkStudio.Logic.RequestOrder.CallWebWindowHelper callWebWindowHelper = new NetWorkStudio.Logic.RequestOrder.CallWebWindowHelper();
                //   if (tmp.ApplySerialNo < 0) //���
                //      // NetWorkStudio.Logic.RequestOrderInterface.CallWebWindow.ShowCheckListEditFrm((-tmp.ApplySerialNo).ToString(), m_App);
                //      callWebWindowHelper.ShowCheckListEditFrm((-tmp.ApplySerialNo).ToString(), m_App);
                //   else
                //      //NetWorkStudio.Logic.RequestOrderInterface.CallWebWindow.ShowInspectionEditFrm(tmp.ApplySerialNo.ToString(), m_App);
                //      callWebWindowHelper.ShowInspectionEditFrm(tmp.ApplySerialNo.ToString(), m_App);
                //   return;
                //}
            }

            if ((m_UILogic.AllowEdit) && (FocusedOrder != null) && (FocusedOrder.State == OrderState.New)
               && AllowAddNew && barItemDelete.Enabled) // ��ɾ����ʾ�ܱ༭
            {
                ChangeVisibleOfEditRegion(true);

                SetTempVarialbesFromRow(m_FocusedRowHandle);
                contentEditor.EditContent(OrderTemp, IsTempOrder);
                //contentEditor.EditContent(FocusedOrder.StartDateTime, FocusedOrder.Content);
                //if ((!IsTempOrder) && (_longOrder.CeaseInfo != null) && (_longOrder.CeaseInfo.HadInitialized))
                //{
                //   TimeSpan days = _longOrder.CeaseInfo.ExecuteTime - _longOrder.StartDateTime;
                //   contentEditor.ExecuteDays = (int)days.TotalDays;
                //}
            }
        }

        private void GridKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
                m_HadPressESC = true;
        }

        private void SetInvalidRowExceptionMode(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ���蹤������ť��״̬
            ResetToolBarItemState();
        }

        private void GridContentEditPopup(object sender, CancelEventArgs e)
        {
            //// �ڵ����༭����ǰ�����ʵ��ĳ�ʼ��
            //if ((advGridView.FocusedRowHandle == m_UILogic.NewItemRowHandle)
            //   || (advGridView.FocusedRowHandle == m_UILogic.InvalidRowHandle))
            //{
            //   if (OrderTemp.Content == null) // ����ҽ����������һ��ҽ������һ��
            //   {
            //      if (advGridView.RowCount == m_UILogic.CurrentView.Count)
            //         OrderTemp.Content = m_UILogic.CreateDefaultContent(advGridView.FocusedRowHandle);
            //      else // FocusedRowHandleΪ����ʱ���п���û����DataSource������У�����Ҫ���⴦��
            //         OrderTemp.Content = m_UILogic.CreateDefaultContent(-1);
            //   }
            //   contentEditor.EditContent(OrderTemp.Content, false);
            //}
            //else
            //{
            //   contentEditor.EditContent(OrderTemp.Content, true);
            //}
        }

        private void GridContentEditClose(object sender, CloseUpEventArgs e)
        {
            //m_FocusInGrid = true;
            //OrderTemp.Content = contentEditor.NewOrderContent;
            //e.Value = contentEditor.NewOrderContent.ToString();
            //m_UpdateFlag |= UpdateContentFlag.Content;

            //// �༭����ʱ�����ҽ�������Ѿ������ݣ����Զ�������
            //if (((m_FocusedRowHandle == m_UILogic.InvalidRowHandle)
            //   || (m_FocusedRowHandle == m_UILogic.NewItemRowHandle))
            //   && (OrderTemp.Content != null))
            //{
            //   SendKeys.Send("{DOWN}");
            //   SendKeys.Send("{RIGHT}");
            //   SendKeys.Send("{LEFT}");
            //}
        }

        private void StartTimeEditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue.ToString().Length > 0)
            {
                OrderTemp.StartDateTime = OrderTemp.StartDateTime.Date
                   + Convert.ToDateTime(e.NewValue, CultureInfo.CurrentCulture).TimeOfDay;
                //FocusedOrder.StartDateTime = FocusedOrder.StartDateTime.Date
                //   + Convert.ToDateTime(e.NewValue, CultureInfo.CurrentCulture).TimeOfDay;
                m_UpdateFlag |= UpdateContentFlag.StartDate;

            }
        }

        private void StartDateEditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue.ToString().Length > 0)
            {
                OrderTemp.StartDateTime = Convert.ToDateTime(e.NewValue, CultureInfo.CurrentCulture).Date
                   + OrderTemp.StartDateTime.TimeOfDay;
                //FocusedOrder.StartDateTime = Convert.ToDateTime(e.NewValue, CultureInfo.CurrentCulture).Date
                //   + FocusedOrder.StartDateTime.TimeOfDay;
                m_UpdateFlag |= UpdateContentFlag.StartDate;
            }
        }

        private void GridViewGotFocus(object sender, EventArgs e)
        {
            m_FocusInGrid = true;
            EnableShortCut = true;
        }

        private void GridViewLostFocus(object sender, EventArgs e)
        {
            m_FocusInGrid = false;
        }
        #endregion

        #region event handle & methods by customdraw
        private void CustomDrawOrderGridCell(object sender, RowCellCustomDrawEventArgs e)
        {
            // ���С������Ǿ۽����У����ʾ�����У���û������
            if ((e.RowHandle == m_UILogic.InvalidRowHandle)
               || ((e.RowHandle == m_UILogic.NewItemRowHandle)
               && (e.RowHandle != m_FocusedRowHandle)))
                return;

            switch (e.Column.FieldName)
            {
                case OrderView.UNContent:// ��ҽ�����ݽ����ػ�
                    DoCustomDrawOrderGridContent(e);
                    break;
                default:
                    if (!CustomDrawOperation.DrawSetting.ShowRepeatInfo)
                        DoCustomDrawOrderGridRepeatRowInfo(e);
                    break;
            }
        }

        private void DoCustomDrawOrderGridRepeatRowInfo(RowCellCustomDrawEventArgs e)
        {
            if ((e.RowHandle > 0) && (e.RowHandle != advGridView.FocusedRowHandle))
            {
                OrderView preView = advGridView.GetRow(e.RowHandle - 1) as OrderView;
                OrderView curView = advGridView.GetRow(e.RowHandle) as OrderView;

                // ���Դ����������ݣ�
                // ��ʼ���ڡ������ߡ�������ں�ʱ�䡢����ߡ�ִ�����ں�ʱ�䡢ִ���ߡ�
                // ֹͣ���ں�ʱ�䡢ֹͣ�ߡ�ֹͣ�����

                switch (e.Column.FieldName)
                {
                    case OrderView.UNStartDate:
                        if ((curView.Creator == preView.Creator) && (curView.StartDate == preView.StartDate))
                            e.DisplayText = CustomDrawOperation.DrawSetting.ReplaceOfRepeatInfo;
                        break;
                    case OrderView.UNStartTime:
                        if ((curView.Creator == preView.Creator) && (curView.StartDate == preView.StartDate) && (curView.StartTime == preView.StartTime))
                            e.DisplayText = CustomDrawOperation.DrawSetting.ReplaceOfRepeatInfo;
                        break;
                    case OrderView.ColCreator:
                        if ((curView.Creator == preView.Creator) && (curView.StartDate == preView.StartDate))
                            e.DisplayText = CustomDrawOperation.DrawSetting.ReplaceOfRepeatInfo;
                        break;
                    case OrderView.UNCeaseDate:
                        if ((!String.IsNullOrEmpty(curView.Ceasor)) && (curView.Ceasor == preView.Ceasor)
                           && (curView.CeaseDate == preView.CeaseDate))
                            e.DisplayText = CustomDrawOperation.DrawSetting.ReplaceOfRepeatInfo;
                        break;
                    case OrderView.UNCeaseTime:
                        if ((!String.IsNullOrEmpty(curView.Ceasor)) && (curView.Ceasor == preView.Ceasor)
                           && (curView.CeaseDate == preView.CeaseDate) && (curView.CeaseTime == preView.CeaseTime))
                            e.DisplayText = CustomDrawOperation.DrawSetting.ReplaceOfRepeatInfo;
                        break;
                    case OrderView.ColCeasor:
                        if ((!String.IsNullOrEmpty(curView.Ceasor)) && (curView.Ceasor == preView.Ceasor)
                           && (curView.CeaseDate == preView.CeaseDate))
                            e.DisplayText = CustomDrawOperation.DrawSetting.ReplaceOfRepeatInfo;
                        break;
                }
            }

        }

        private void DoCustomDrawOrderGridContent(RowCellCustomDrawEventArgs e)
        {
            SolidBrush foreBrushNormal, foreBrushCancel, foreBrushGroup, backBrush;
            //// 1 ��ҽ����������
            ////    ��3�������1)δѡ�� 2)��ѡ�У���������Focused�� 3)��ѡ�У��Ҵ���Focused��
            //bool isFocused = ((e.RowHandle == advGridView.FocusedRowHandle)
            //   && (e.Column == advGridView.FocusedColumn)); // �Ƿ��Ǿ۽��ĵ�Ԫ��

            //// ���ú��ʵ�ǰ��ɫ(����ѡ��ʱ��Ҫ�ж��Ƿ��ڿɱ༭��״̬)
            //if (CheckRowIsInSelection(e.RowHandle)
            //   && (((!advGridView.Editable) && (isFocused))
            //         || (!isFocused)))
            //{
            //   foreBrushNormal = new SolidBrush(CustomDrawOperation.DrawSetting.ForeColor.ForeColor);
            //   foreBrushCancel = new SolidBrush(CustomDrawOperation.DrawSetting.CancelledColor.ForeColor);
            //   foreBrushGroup = new SolidBrush(CustomDrawOperation.DrawSetting.GroupFlagColor.ForeColor);
            //}
            //else
            //{
            //   foreBrushNormal = new SolidBrush(CustomDrawOperation.DrawSetting.ForeColor.BackColor);
            //   foreBrushCancel = new SolidBrush(CustomDrawOperation.DrawSetting.CancelledColor.BackColor);
            //   foreBrushGroup = new SolidBrush(CustomDrawOperation.DrawSetting.GroupFlagColor.BackColor);
            //}
            //// Focused��ʱ��Ҫ�ı�Ĭ�ϵı���ɫ
            //if (advGridView.Editable && isFocused)
            //{
            //   backBrush = new SolidBrush(CustomDrawOperation.DrawSetting.BackColor.BackColor);
            //   e.Graphics.FillRectangle(backBrush, e.Bounds);
            //}

            //// ����ǵ�ǰ�༭���У�������ʱ������ֵ
            //OrderContent content;
            //if (e.RowHandle == m_FocusedRowHandle)
            //   content = OrderTemp.Content;
            //else
            //   content = m_UILogic.CurrentView[e.RowHandle].OrderCache.Content;

            // ���ڲ����Ƿ�ѡ�л�Focused����ʹ�ù̶�����ɫ
            OrderView currentRow = m_UILogic.CurrentView[e.RowHandle];
            OrderState state = currentRow.State;
            if (state == OrderState.Cancellation)
                state = OrderState.Audited;
            foreBrushNormal = new SolidBrush(CustomDrawOperation.GetForeColorByState(state, false, currentRow.HadSynch));
            foreBrushCancel = new SolidBrush(CustomDrawOperation.GetForeColorByState(OrderState.Cancellation, false, currentRow.HadSynch));
            foreBrushGroup = new SolidBrush(CustomDrawOperation.DrawSetting.GroupFlagColor.BackColor);

            backBrush = new SolidBrush(CustomDrawOperation.GetBackColorByState(state, false, currentRow.HadSynch));
            e.Graphics.FillRectangle(backBrush, e.Bounds);

            // ���ڲ���������ֱ�ӱ༭���ݣ�����ֱ��ʹ�����ݼ��е�����
            OrderContent content = currentRow.OrderCache.Content;
            if (content == null)
                return;

            foreach (OutputInfoStruct output in content.Outputs)
            {
                if (output.OutputType == OrderOutputTextType.NormalText)
                    e.Appearance.DrawString(e.Cache, output.Text
                       , CellBoundsToGrid(e.Bounds, output.Bounds)
                       , output.Font, foreBrushNormal, new StringFormat());
                else if (output.OutputType == OrderOutputTextType.CancelInfo)
                    e.Appearance.DrawString(e.Cache, output.Text
                       , CellBoundsToGrid(e.Bounds, output.Bounds)
                       , output.Font, foreBrushCancel, new StringFormat());
                else
                    e.Graphics.FillRectangle(foreBrushGroup
                       , CellBoundsToGrid(e.Bounds, output.Bounds));
            }
            e.Handled = true;
        }

        private static Rectangle CellBoundsToGrid(Rectangle bounds, Rectangle outputBounds)
        {
            return new Rectangle(bounds.X + outputBounds.X
               , bounds.Y + outputBounds.Y
               , outputBounds.Width
               , outputBounds.Height);
        }

        private void CustomAllergicGridCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if ((e.RowHandle != GridControl.InvalidRowHandle)
               && (e.RowHandle != GridControl.NewItemRowHandle))
            {
                DataRow row = gridViewAllergic.GetDataRow(e.RowHandle);
                if ((row != null) && (row[ConstSchemaNames.SkinTestColFlag].ToString() == ConstNames.LightDemanding))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region event handle of barItems
        private void ItemDeleteNewOrderClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            try
            {
                int[] rowHandles = advGridView.GetSelectedRows();
                if (rowHandles == null)
                    return;
                advGridView.BeginDataUpdate();
                m_UILogic.DeleteNewOrder(rowHandles);
                advGridView.EndDataUpdate();
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemSetGroupClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            // ���ȼ���Ƿ���Գ��飬Ȼ��������Ϊ��
            try
            {
                int[] rowHandles = advGridView.GetSelectedRows();
                if ((rowHandles == null) || (rowHandles.Length == 1))
                    return;
                m_UILogic.SetOrderGroup(rowHandles);
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemAutoGroupClick(object sender, ItemClickEventArgs e)
        {
            // ���ڱ༭״̬
            if (AllowAddNew)
            {
                advGridView.ClearSelection();
                int[] rowHandles = m_UILogic.AutoSetNewOrderGrouped();
                if ((rowHandles != null) && (rowHandles.Length > 0))
                    advGridView.SelectRange(rowHandles[0], rowHandles[rowHandles.Length - 1]);
                else
                    MoveFocusedRowByHand(true);
            }
        }

        private void ItemAuditClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            try
            {
                int[] rowHandles = advGridView.GetSelectedRows();
                if (rowHandles == null)
                    return;
                m_UILogic.AuditOrder(rowHandles);
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemMoveNewOrderUpClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            try
            {
                int[] rowHandles = advGridView.GetSelectedRows();
                if (rowHandles == null)
                    return;

                int acturalSteps = m_UILogic.MoveNewOrderUp(rowHandles);
                // ����ѡ�б��ƶ��ļ�¼�����嵹�� acturalSteps ����
                advGridView.BeginSelection();
                advGridView.ClearSelection();

                advGridView.SelectRange(rowHandles[0] - acturalSteps, rowHandles[0] + rowHandles.Length - 1 - acturalSteps);

                advGridView.FocusedRowHandle = advGridView.GetPrevVisibleRow(advGridView.FocusedRowHandle);
                advGridView.EndSelection();
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemMoveNewOrderDownClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            try
            {
                int[] rowHandles = advGridView.GetSelectedRows();
                if (rowHandles == null)
                    return;

                int acturalSteps = m_UILogic.MoveNewOrderDown(rowHandles);
                // ����ѡ���ƶ��ļ�¼������ǰ�� acturalSteps ����
                advGridView.BeginSelection();
                advGridView.ClearSelection();

                advGridView.SelectRange(rowHandles[0] + acturalSteps, rowHandles[0] + rowHandles.Length - 1 + acturalSteps);

                advGridView.FocusedRowHandle = advGridView.GetNextVisibleRow(advGridView.FocusedRowHandle);
                advGridView.EndSelection();
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemCancelGroupClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            try
            {
                int[] rowHandles = advGridView.GetSelectedRows();
                if (rowHandles == null)
                    return;
                m_UILogic.CancelOrderGroup(rowHandles);
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemCeaseClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            if (m_TimeInputForm == null)
            {
                m_TimeInputForm = new DateTimeInputForm();
                m_TimeInputForm.App = m_App;
            }
            if (m_TimeInputForm.InputDateTime < DateTime.Now)
                m_TimeInputForm.InputDateTime = m_UILogic.DefaultCeaseOrderTime;
            if (m_TimeInputForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int[] rowHandles = advGridView.GetSelectedRows();
                    if (rowHandles == null)
                        return;
                    m_UILogic.SetLongOrderCeaseInfo(rowHandles, m_TimeInputForm.InputDateTime);
                }
                catch (Exception err)
                {
                    ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
                }
            }
        }

        private void ItemCancelClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            try
            {
                int[] rowHandles = advGridView.GetSelectedRows();
                if (rowHandles == null)
                    return;
                m_UILogic.CancelOrder(rowHandles);
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemFilterOrderCatalogClick(object sender, EventArgs e)
        {
            // �ı�Grid������ҽ�����ݼ�
            if (barItemFilterStatus.EditValue == null)
                m_UILogic.SwitchOrderTable((barItemOrderCatalog.EditValue.ToString() == ConstNames.TempOrder)
                   , OrderState.All);
            else
                m_UILogic.SwitchOrderTable((barItemOrderCatalog.EditValue.ToString() == ConstNames.TempOrder)
                   , (OrderState)barItemFilterStatus.EditValue);
        }

        private void ItemFilterOrderStateClick(object sender, EventArgs e)
        {
            // ͬ��OrderTableView�е�״̬
            m_UILogic.FilterOrderByState((OrderState)barItemFilterStatus.EditValue);
        }

        private void ItemSaveClick(object sender, ItemClickEventArgs e)
        {
            DoSaveData();
        }

        private void ItemSubmitClick(object sender, ItemClickEventArgs e)
        {
            DoSendChangedDataToHIS();
        }

        private void ItemStateAvailablyClick(object sender, ItemClickEventArgs e)
        {
            if (OrderState != OrderState.Executed)
                OrderState = OrderState.Executed;
        }

        private void ItemStateNewClick(object sender, ItemClickEventArgs e)
        {
            if (OrderState != OrderState.New)
                OrderState = OrderState.New;
        }

        private void ItemStateAllClick(object sender, ItemClickEventArgs e)
        {
            if (OrderState != OrderState.All)
                OrderState = OrderState.All;
        }

        private void ItemTempOrderClick(object sender, ItemClickEventArgs e)
        {
            if (!IsTempOrder)
                IsTempOrder = true;
        }

        private void ItemLongOrderClick(object sender, ItemClickEventArgs e)
        {
            if (IsTempOrder)
                IsTempOrder = false;
        }

        private void ItemCutClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            // ��ѡ��ҽ����ҽ�������Ƴ������浽������
            try
            {
                OrderClipboard.Clear();
                int[] rowHandles = advGridView.GetSelectedRows();
                if (rowHandles == null)
                    return;
                Order[] orders = m_UILogic.CutOrdersFromList(rowHandles);
                if (orders != null)
                {
                    foreach (Order order in orders)
                        OrderClipboard.Add(order);
                    barItemPaste.Enabled = (OrderClipboard.Count > 0);
                }
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemCopyClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            // ��¡ѡ�е�ҽ�������浽������
            try
            {
                OrderClipboard.Clear();
                int[] rowHandles = advGridView.GetSelectedRows();
                if (rowHandles == null)
                    return;
                Order[] orders = m_UILogic.CopyOrdersFromList(rowHandles);
                if (orders != null)
                {
                    foreach (Order order in orders)
                        OrderClipboard.Add(order);
                    barItemPaste.Enabled = (OrderClipboard.Count > 0);
                }
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemPasteClick(object sender, ItemClickEventArgs e)
        {
            //if (!EnableShortCut)
            //   return;
            if (OrderClipboard.Count == 0)
                return;

            try
            {
                int[] rowHandles = advGridView.GetSelectedRows();
                m_UILogic.CheckAllowInsertOrder(rowHandles);

                // �����޸Ľ��棬��ɲ��빤�������뵽��ǰ�е���һ�У�
                if (SuiteEditForm.CallOrderSuiteEditForm(true, ConvertOrderToObjectArray(OrderClipboard)
                   , m_UILogic.FrequencyWordbook) == DialogResult.OK)
                {
                    object[,] contents = SuiteEditForm.SelectedContents;
                    Order focusedOrder;
                    if ((rowHandles != null) && (rowHandles.Length > 0))
                        focusedOrder = m_UILogic.CurrentOrderTable.Orders[rowHandles[rowHandles.Length - 1]];
                    else
                        focusedOrder = null;

                    m_UILogic.InsertSuiteOrder(contents, focusedOrder);
                    MoveFocusedRowByHand(false);
                }
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void ItemRefreshClick(object sender, ItemClickEventArgs e)
        {
            //CurrentPatient = null; //����ղ����ٵ���ʼ��
            //CallShowPatientOrder(CurrentPatient);
            InitializePatientData(CurrentPatient);
        }

        private void ItemPrintClick(object sender, ItemClickEventArgs e)
        {
            if (PrintCurrentPatientOrder != null)
                PrintCurrentPatientOrder(CurrentPatient);
        }

        private void ItemExpandHerbDetailClick(object sender, ItemClickEventArgs e)
        {
            m_UILogic.CurrentView.ExpandHerbDetail(m_FocusedRowHandle);
        }

        private void ItemExpandAllHerbClick(object sender, ItemClickEventArgs e)
        {
            m_UILogic.CurrentView.ExpandAllHerbDetail();
        }

        private void ItemCollapseHerbDetailClick(object sender, ItemClickEventArgs e)
        {
            m_UILogic.CurrentView.CollapseHerbDetail(m_FocusedRowHandle);
        }

        private void ItemCollapseAllHerbClick(object sender, ItemClickEventArgs e)
        {
            m_UILogic.CurrentView.CollapseAllHerbDetail();
        }
        #endregion

        #region private methods of medicompass
        /*
        private bool InitializeMedicomPass()
        {
            if (m_MedicomPass != null)
                return true;

            SetWaitDialogCaption(ConstMessages.HintInitMedicom);
            try
            {
                if (m_MedicomPass == null)
                    m_MedicomPass = new PassComponent();

                return m_MedicomPass.InitializePassIntf(m_App.User.CurrentDeptId
                   , m_App.User.CurrentDeptName
                   , m_App.User.DoctorId
                   , m_App.User.DoctorName);
            }
            catch
            {
                HideWaitDialog();
                m_App.CustomMessageBox.MessageShow(ConstMessages.FailedInitMedicom, CustomMessageBoxKind.ErrorOk);
                return false;
            }
        }
        
        private void SetMedicomPassPatient()
        {
            if (m_MedicomPass != null)
                m_MedicomPass.PassCheckHelper.PassSetPatient(ConvertPatientToStruct(CurrentPatient));
        }
        
        private static MediIntfPatientInfo ConvertPatientToStruct(Inpatient patient)
        {
            MediIntfPatientInfo result = new MediIntfPatientInfo();
            result.PatientID = patient.NoOfFirstPage.ToString();
            result.Birthday = patient.PersonalInformation.Birthday.ToString(ConstFormat.FullDate, CultureInfo.CurrentCulture);
            result.DeptName = patient.InfoOfAdmission.DischargeInfo.CurrentDepartment.Name;
            //result.Doctor = patient.InHos.

            return result;
        }
        */
        private void DoAfterGridCtrlMouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = advGridView.CalcHitInfo(e.X, e.Y);
            if (hitInfo.InRowCell && (hitInfo.Column == gridColCheckResult))
            {
                /*
                if ((MedicomDrugInfos.Count > 0)
                   && (advGridView.GetRowCellValue(hitInfo.RowHandle, hitInfo.Column) != null))
                {
                    string serialNo;
                    if (IsTempOrder)
                        serialNo = m_UILogic.CurrentView[hitInfo.RowHandle].SerialNo.ToString();
                    else
                        serialNo = (-m_UILogic.CurrentView[hitInfo.RowHandle].SerialNo).ToString();

                   
                    foreach (MediIntfDrugInfo drugInfo in MedicomDrugInfos)
                    {
                        if (drugInfo.OrderUniqueCode == serialNo)
                            m_MedicomPass.PassCheckHelper.PassSetWarnDrug(serialNo);
                    }
                    
                }
                */
            }
        }

        private void DoShowPlugDrugInfoMenu(object sender, ItemClickEventArgs e)
        {
            /* 
            if ((FocusedOrder.Content != null)
               && ((FocusedOrder.Content.OrderKind == OrderContentKind.Druggery)
                  || (FocusedOrder.Content.OrderKind == OrderContentKind.OutDruggery)))
            {
                
                if ((FocusedOrder.Content.Item != null) && (FocusedOrder.Content.Item.KeyInitialized))
                    m_MedicomPass.PassCheckHelper.CurrentDrugIndex = FocusedOrder.Content.Item.KeyValue;
                else
                    m_MedicomPass.PassCheckHelper.CurrentDrugIndex = "";
            }
            //���û��ѡ��ҩƷ�������ĳЩ�˵������

            m_MedicomPass.PassContextMenu.Show(Cursor.Position);
             */
        }

        private void DoCheckOrders(object sender, ItemClickEventArgs e)
        {
            try
            {
                CheckNewOrderUseMedicom();
            }
            catch (Exception err)
            {
                ShowErrorMessage(err.Message, CustomMessageBoxKind.WarningOk);
            }
        }

        private void CheckNewOrderUseMedicom()
        {
            CreateMedicomDrugInfoData();
            /*
            if (MedicomDrugInfos.Count > 0)
            {
                if (!m_MedicomPass.PassCheckHelper.PassSetRecipeInfos(MedicomDrugInfos))
                    throw new ArgumentNullException(ConstMessages.ExceptionFailedSendDataToMedicom);
                m_MedicomPass.PassCheckHelper.DoPassCheck(PassCheckType.HospSubmitAuto);
                advGridView.BeginUpdate(); // ͨ��ˢ�µķ�ʽ�ı�������ͼƬ
                advGridView.EndUpdate();
            }
             */
        }

        private void DoShowDrugInfo(object sender, ItemClickEventArgs e)
        {
            /*
            if ((m_FocusedRowHandle > 0) && (m_MedicomPass != null))
            {
                OrderContent content = m_UILogic.CurrentView[m_FocusedRowHandle].OrderCache.Content;
                if ((content.OrderKind == OrderContentKind.Druggery) || (content.OrderKind == OrderContentKind.OutDruggery))
                {
                    // ����ҩƷ��Ϣ
                    // ���ø�������λ��
                    // ��ʾ��Ϣ
                    Rectangle cellRange = CustomDrawOperation.GetGridCellRect(advGridView, m_FocusedRowHandle, gridColContent);
                    m_MedicomPass.PassCheckHelper.PassQueryDrugInfo(content.Item.KeyValue, content.Item.Name
                       , content.CurrentUnit.Name, content.ItemUsage.Name
                       , new Point(cellRange.X + cellRange.Width, cellRange.Y)
                       , new Point(cellRange.X + cellRange.Width + 200, cellRange.Y + 150));
                }
            }
             */
        }

        private void DoAfterContentEditorSelectedItemChanged(object sender, OrderItemArgs e)
        {
            /*
            if (m_MedicomPass != null)
            {
                if (e.HadData)
                {
                    switch (e.Kind)
                    {
                        case ItemKind.WesternMedicine:
                        case ItemKind.PatentMedicine:
                        case ItemKind.HerbalMedicine:
                            Rectangle client = ClientRectangle;
                            Point rightBottom = new Point(client.Width, client.Height - 30 - panelContentEditor.Height);
                            Point leftTop = new Point(rightBottom.X - 200, rightBottom.Y - 150);
                            m_MedicomPass.PassCheckHelper.PassQueryDrugInfo(e.ItemCode, e.ItemName, e.DoseUnit, e.Usage
                               , PointToScreen(leftTop), PointToScreen(rightBottom));
                            //, new Point(500, 300), new Point(700, 450));
                            break;
                        default:
                            CloseDrugInfoForm();
                            break;
                    }
                }
                else
                    CloseDrugInfoForm();
            }
             */
        }

        private Image GetDrugWarnPicture(int rowHandle)
        {
            /*
            if ((MedicomDrugInfos.Count > 0) && (rowHandle >= 0))
            {
                decimal serialNo = m_UILogic.CurrentView[rowHandle].SerialNo;
                if (!IsTempOrder)
                    serialNo = -serialNo;

                foreach (MediIntfDrugInfo drugInfo in MedicomDrugInfos)
                {
                    if (drugInfo.OrderUniqueCode == serialNo.ToString())
                        return m_MedicomPass.PassCheckHelper.GetWarnBmp(drugInfo.Warn);
                }
            }
             */

            return null;

            //// �л����ݼ���Ҫ���ô˷���
            //if (MedicomDrugInfos.Count > 0)
            //{
            //   decimal serialNo;
            //   int rowHandle;
            //   foreach (MediIntfDrugInfo drugInfo in MedicomDrugInfos)
            //   {
            //      if ((drugInfo.OrderType == "1") == IsTempOrder)
            //      {
            //         serialNo = Convert.ToDecimal(drugInfo.OrderUniqueCode);
            //         if (serialNo < 0)
            //            serialNo = -serialNo;
            //         rowHandle = m_UILogic.CurrentView.IndexOf(serialNo);                  
            //         if (rowHandle > 0)
            //            advGridView.SetRowCellValue(rowHandle, gridColCheckResult
            //               , m_MedicomPass.PassCheckHelper.GetWarnBmp(drugInfo.Warn));
            //      }
            //   }
            //}
        }

        private void CloseDrugInfoForm()
        {
            /*
            if ((m_MedicomPass != null) && (m_MedicomPass.PassCheckHelper.CurrentDrugIndex != null))
                m_MedicomPass.PassCheckHelper.DoCommand(402);
             */
        }

        private void CreateMedicomDrugInfoData()
        {
            //MedicomDrugInfos.Clear();
            Order temp;
            // ��������ʱҽ��
            OrderTable table = m_UILogic.GetCurrentOrderTable(true);
            for (int index = table.Orders.Count - 1; index >= 0; index--)
            {
                temp = table.Orders[index];
                if (temp.State != OrderState.New)
                    break;
                /*
                if ((temp.Content.OrderKind == OrderContentKind.Druggery)
                   || (temp.Content.OrderKind == OrderContentKind.OutDruggery))
                    MedicomDrugInfos.Insert(0, ConvertTempOrderToMediIntfDrugInfo(temp));
                 */
            }
            // �����³���ҽ��
            table = m_UILogic.GetCurrentOrderTable(false);
            for (int index = table.Orders.Count - 1; index >= 0; index--)
            {
                temp = table.Orders[index];
                if (temp.State != OrderState.New)
                    break;
                /*
                if (temp.Content.OrderKind == OrderContentKind.Druggery)
                    MedicomDrugInfos.Insert(0, ConvertLongOrderToMediIntfDrugInfo((temp as LongOrder)));
                 */
            }
        }
        /*
        private MediIntfDrugInfo ConvertOrderToMediIntfDrugInfo(Order temp, bool isTemp)
        {
            MediIntfDrugInfo drugInfo = new MediIntfDrugInfo();
            if (isTemp)
                drugInfo.OrderUniqueCode = temp.SerialNo.ToString();//ҽ��Ψһ�루���봫ֵ��(���ڡ���ʱ�����ظ�)
            else
                drugInfo.OrderUniqueCode = (-temp.SerialNo).ToString(); // �ø�����ʾ����ҽ�������
            drugInfo.DrugCode = temp.Content.Item.KeyValue; //ҩƷ���� �����봫ֵ�� 
            drugInfo.DrugName = temp.Content.Item.Name; //ҩƷ���� �����봫ֵ��
            drugInfo.SingleDose = temp.Content.Amount.ToString(); //ÿ������ �����봫ֵ��
            drugInfo.DoseUnit = temp.Content.CurrentUnit.Name; // ������λ �����봫ֵ��
            drugInfo.Frequency = temp.Content.ItemFrequency.ConvertToMedicomFrequency(); //��ҩƵ��(��/��)�����봫ֵ�� 
            drugInfo.StartDate = temp.StartDateTime.ToString(ConstFormat.FullDate); //��ҩ��ʼ���ڣ���ʽ��yyyy-mm-dd �����봫ֵ�� 
            drugInfo.RouteName = temp.Content.ItemUsage.Name; // ��ҩ;���������� �����봫ֵ�� 
            drugInfo.GroupTag = temp.GroupSerialNo.ToString(); // ����ҽ����־ �����봫ֵ��
            drugInfo.OrderDoctorId = temp.CreateInfo.Executor.Code;// ����ҽ��ID/����ҽ������ �����봫ֵ��
            drugInfo.OrderDoctorName = temp.CreateInfo.Executor.Name;
            return drugInfo;
        }

        private MediIntfDrugInfo ConvertTempOrderToMediIntfDrugInfo(Order order)
        {
            MediIntfDrugInfo drugInfo = ConvertOrderToMediIntfDrugInfo(order, true);
            drugInfo.OrderType = "1";// �Ƿ�Ϊ��ʱҽ�� 1-����ʱҽ�� 0��� ����ҽ�� �����봫ֵ��
            return drugInfo;
        }

        private MediIntfDrugInfo ConvertLongOrderToMediIntfDrugInfo(LongOrder longOrder)
        {
            MediIntfDrugInfo drugInfo = ConvertOrderToMediIntfDrugInfo(longOrder, false);
            if ((longOrder.CeaseInfo != null) && (longOrder.CeaseInfo.HadInitialized))
                drugInfo.EndDate = longOrder.CeaseInfo.ExecuteTime.ToString(ConstFormat.FullDate); //��ҩ�������ڣ���ʽ��yyyy-mm-dd �����Բ���ֵ����Ĭ��ֵΪ���� 
            drugInfo.OrderType = "0";// �Ƿ�Ϊ��ʱҽ�� 1-����ʱҽ�� 0��� ����ҽ�� �����봫ֵ��
            return drugInfo;
        }
        */
        #endregion

        #region ISupportInitialize Members

        public void BeginInit()
        { }

        public void EndInit()
        { }

        #endregion

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-22</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advGridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                DS_Common.AutoIndex(e);
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(1, ex);
            }
        }
    }
}