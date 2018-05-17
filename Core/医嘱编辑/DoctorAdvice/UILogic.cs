using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Text;
using DevExpress.XtraEditors.Controls;
using DrectSoft.Common.Eop;
using DrectSoft.Wordbook;
using System.Xml.Serialization;
using DrectSoft.FrameWork.WinForm.Plugin;
using Eop = DrectSoft.Common.Eop;
namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// ���洦���߼��ࡣ�ṩ�������ʾ���ؼ����Կ����йص��㷨���¼�����ȡ�
   /// </summary>
   internal class UILogic
   {
      #region const & readonly
      /// <summary>
      /// ��ʾ����ʽ�е��к�
      /// </summary>
      public readonly int InvalidRowHandle;
      /// <summary>
      /// ��ʾ��������״̬���е��к�
      /// </summary>
      public readonly int NewItemRowHandle;
      #endregion

      #region static methods
      /// <summary>
      /// ͳһ��ʽ�������ַ�������ʾ��ʽ
      /// </summary>
      /// <param name="sourceDate"></param>
      /// <returns>Ĭ�ϸ�ʽ�����ڴ�"��-��-��"</returns>
      public static string ConvertToDateString(DateTime sourceDate)
      {
         return sourceDate.ToString(ConstFormat.ShortDateWithCentry, CultureInfo.CurrentCulture);
      }

      /// <summary>
      /// ͳһ��ʽ��ʱ���ַ�������ʾ��ʽ
      /// </summary>
      /// <param name="sourceTime"></param>
      /// <returns>Ĭ�ϸ�ʽ��ʱ�䴮"ʱ:��"</returns>
      public static string ConvertToTimeString(TimeSpan sourceTime)
      {
         if (sourceTime == TimeSpan.MinValue)
            return "00:00";

         DateTime temp = new DateTime(sourceTime.Ticks);
         return temp.ToString(ConstFormat.ShortTime, CultureInfo.CurrentCulture);
      }
      #endregion

      #region public properties
      /// <summary>
      /// ����ѡ��ҽ�����������ֵ���ʵ��
      /// </summary>
      public SqlWordbook CatalogWordbook
      {
         get
         {
            if (_catalogWordbook == null)
            {
               Dictionary<string, int> columnWidthes = new Dictionary<string, int>();
               columnWidthes.Add("Name", 150);
               _catalogWordbook =
                  new SqlWordbook(ConstSchemaNames.ContentCatalogTableName, m_CoreLogic.OrderContentCatalog
                  , ConstSchemaNames.ContentCatalogColId, ConstSchemaNames.ContentCatalogColName, columnWidthes, true);
            }
            _catalogWordbook.ExtraCondition = m_CoreLogic.GetOrderContentCatalogRowFilter(IsTempOrder);

            return _catalogWordbook;
         }
      }
      private SqlWordbook _catalogWordbook;

      /// <summary>
      /// ����ѡ��Ƶ�ε��ֵ���ʵ��
      /// </summary>
      public OrderFrequencyBook FrequencyWordbook
      {
         get
         {
            if (_frequencyWordbook == null)
               _frequencyWordbook = new OrderFrequencyBook();

            if ((IsTempOrder) && (!m_CoreLogic.HasOutHospitalOrder)) // ����ǳ�Ժ��ҩ����ʹ�ó���ҽ����Ƶ��
               _frequencyWordbook.ExtraCondition = String.Format(CultureInfo.CurrentCulture
                  , ConstSqlSentences.FormatFrequencyFilter
                  , OrderManagerKind.Normal, OrderManagerKind.ForTemp);
            else
               _frequencyWordbook.ExtraCondition = String.Format(CultureInfo.CurrentCulture
                  , ConstSqlSentences.FormatFrequencyFilter
                  , OrderManagerKind.Normal, OrderManagerKind.ForLong);
            return _frequencyWordbook;
         }
      }
      private OrderFrequencyBook _frequencyWordbook;

      /// <summary>
      /// ��ǰ������ҽ�������ʵ��
      /// </summary>
      public OrderTable CurrentOrderTable
      {
         get { return m_CoreLogic.GetCurrentOrderTable(IsTempOrder); }
      }

      /// <summary>
      /// ��ǵ�ǰ���������ʱ���ǳ���ҽ��
      /// </summary>
      public bool IsTempOrder
      {
         get { return _isTempOrder; }
         set
         {
            OrderState state = OrderStateFilter; // �л�������ʱʱ����״̬���˲���
            _isTempOrder = value;

            OrderStateFilter = state;
            //ResetAllowNew();

            //FireContentBaseDataChanged(this, new EventArgs());
            FireAfterSwitchOrderTable(new EventArgs());
         }
      }
      private bool _isTempOrder;

      /// <summary>
      /// ��ǰ�鿴��ҽ�����ҽ��״̬:ȫ������������Ч
      /// </summary>
      public OrderState OrderStateFilter
      {
         get 
         {
            if (CurrentView.State == OrderState.Audited && IsTempOrder)
               return OrderState.Executed;
            else
               return CurrentView.State; 
         }
         set
         {
            if ((value == OrderState.Executed) && IsTempOrder)
               CurrentView.State = OrderState.Audited;
            else
               CurrentView.State = value;
            ResetAllowNew();
         }
      }

      /// <summary>
      /// ��ǰ�༭��ҽ�����Ӧ��Grid������Ϣ
      /// </summary>
      public TypeGridBand[] CurrentBandSettings
      {
         get
         {
            if (IsTempOrder)
               return CustomDrawOperation.GridSetting.TempOrderSetting;
            else
               return CustomDrawOperation.GridSetting.LongOrderSetting;
         }
      }

      /// <summary>
      /// ����ҽ����������ʼʱ��(��ȷ������)
      /// </summary>
      public DateTime MinStartDateTime
      {
         get { return m_CoreLogic.MinStartDateTime; }
      }

      /// <summary>
      /// ����ҽ��Ĭ�Ͽ�ʼʱ�䣨��ȷ�����ӣ�
      /// </summary>
      public DateTime DefaultStartDateTime
      {
         get { return m_CoreLogic.GetDefaultStartDateTime(IsTempOrder); }
      }

      /// <summary>
      /// Ĭ�ϵĳ�Ժʱ��
      /// </summary>
      public DateTime DefaultLeaveHospitalTime
      {
         get { return m_CoreLogic.DefaultLeaveHospitalTime; }
      }

      /// <summary>
      /// Ĭ�ϵ�ֹͣҽ��ʱ��
      /// </summary>
      public DateTime DefaultCeaseOrderTime
      {
         get
         {
            if (DateTime.Now.Hour > 12)
               return DateTime.Today.AddDays(1) + new TimeSpan(8, 0, 0);
            else
               return DateTime.Today + new TimeSpan(16, 0, 0);
         }
      }

      /// <summary>
      /// ��ǵ�ǰҽ���������Ƿ��ѱ��޸�
      /// </summary>
      public bool HadChanged
      {
         get { return m_CoreLogic.HadChanged; }
      }

      /// <summary>
      /// ����Ƿ���δ���͵�����
      /// </summary>
      public bool HasNotSendData
      {
         get { return m_CoreLogic.HasNotSendData; }
      }

      /// <summary>
      /// ��ǰ���õ�ҽ������
      /// </summary>
      public List<string> OrderCatalogs
      {
         get { return _orderCatalogs; }
      }
      private List<string> _orderCatalogs;

      /// <summary>
      /// ��ǰҽ�������¿��õ�״̬����
      /// </summary>
      public List<ImageComboBoxItem> StatusFilters
      {
         get { return _statusFilters; }
      }
      private List<ImageComboBoxItem> _statusFilters;

      /// <summary>
      /// ��ǰҽ�������ͼ
      /// </summary>
      public OrderTableView CurrentView
      {
         get { return CurrentOrderTable.DefaultView; }
      }

      /// <summary>
      /// ��ǰҽ�����Ƿ�����༭
      /// </summary>
      public bool AllowEdit
      {
         get { return CurrentView.AllowEdit; }
      }

      /// <summary>
      /// ��ǰҽ�����Ƿ����������ҽ��
      /// </summary>
      public bool AllowAddNew
      {
         get { return ((m_CallModel != EditorCallModel.Query) && (_allowAddNew || (m_CallModel == EditorCallModel.EditSuite))); }
      }
      private bool _allowAddNew;

      /// <summary>
      /// ���Ƥ�Խ���ı�
      /// </summary>
      public DataTable SkinTestResultTable
      {
         get
         {
            if (_skinTestResultTable == null)
               GetSkinTestResultData();

            return _skinTestResultTable;
         }
      }
      private DataTable _skinTestResultTable;

      /// <summary>
      /// ͳһ����Ϣ�����
      /// </summary>
      public ICustomMessageBox CustomMessageBox
      {
         get { return _customMessageBox; }
      }
      private ICustomMessageBox _customMessageBox;

      /// <summary>
      /// ��ǰ����Ĳ���
      /// </summary>
      public Inpatient CurrentPatient
      {
         get { return m_CoreLogic.CurrentPatient; }
         set
         {
            FireProcessStarting(ConstMessages.HintReadOrderData);
            m_CoreLogic.CurrentPatient = value;
            _skinTestResultTable = null;
            DoAfterPatientChanged();
         }
      }

      public SuiteOrderHandle SuiteHelper
      {
         get { return m_CoreLogic.SuiteHelper; }
      }

      /// <summary>
      /// ��ǰ���Ҵ���
      /// </summary>
      public string DeptCode
      {
         get { return _deptCode; }
      }
      private string _deptCode;
      /// <summary>
      /// ��ǰ��������
      /// </summary>
      public string WardCode
      {
         get { return _wardCode; }
      }
      private string _wardCode;

      /// <summary>
      /// ��ǰ���˵�ҽ�����Ƿ������Ч�ġ���Ժҽ������û�б�ȡ���ģ�
      /// </summary>
      public bool HasOutHospitalOrder
      {
         get { return m_CoreLogic.HasOutHospitalOrder; }
      }
      #endregion

      #region private variables
      private CoreBusinessLogic m_CoreLogic;
      /// <summary>
      /// ������Ŀѡ���ֵ����Hash��������ҽ�����������
      /// </summary>
      private Dictionary<OrderContentKind, BaseWordbook> m_ItemWordbook;
      /// <summary>
      /// ��ǰҽ������
      /// </summary>
      private string m_DoctorCode;
      /// <summary>
      /// ������������ַ
      /// </summary>
      private string m_MacAddress;
      private RecipeRuleChecker m_RepRuleChecker;
      private EditorCallModel m_CallModel;
      #endregion

      #region ctors
      /// <summary>
      /// �����߼�����ʵ��
      /// </summary>
      /// <param name="app">��ܶ���������ʼ������</param>
      /// <param name="invalidRowHandle">��ʾ����ʽ�е��к�</param>
      /// <param name="newItemRowHandle">��ʾ��������״̬���к�</param>
      /// <param name="callModel">����ģʽ(�༭ҽ�����༭����)</param>
      public UILogic(IEmrHost app, int invalidRowHandle, int newItemRowHandle, EditorCallModel callModel)
      {
         //if (callModel != EditorCallModel.EditSuite)
         //   m_CoreLogic = new CoreBusinessLogic(app.SqlHelper);
         //else
         m_CoreLogic = new CoreBusinessLogic(app, callModel);

         m_CoreLogic.OutHospitalOrderChanged += new EventHandler(DoAfterOutHospitalOrderChanged);
         m_CoreLogic.PatientOrderDataChanged += new EventHandler(DoAfterPatientOrderDataChanged);

         InvalidRowHandle = invalidRowHandle;
         NewItemRowHandle = newItemRowHandle;

         m_DoctorCode = app.User.DoctorId;
         _deptCode = app.User.CurrentDeptId;
         _wardCode = app.User.CurrentWardId;
         m_MacAddress = app.MacAddress;
         _customMessageBox = app.CustomMessageBox;
         m_CallModel = callModel;

         _orderCatalogs = new List<string>();
         _orderCatalogs.Add(ConstNames.LongOrder);
         _orderCatalogs.Add(ConstNames.TempOrder);

         _statusFilters = new List<ImageComboBoxItem>();
         _statusFilters.Add(new ImageComboBoxItem(ConstNames.OrderStateAll, OrderState.All, -1));
         _statusFilters.Add(new ImageComboBoxItem(ConstNames.OrderStateNew, OrderState.New, -1));
         _statusFilters.Add(new ImageComboBoxItem(ConstNames.OrderStateAudited, OrderState.Audited, 11));
         _statusFilters.Add(new ImageComboBoxItem(ConstNames.OrderStateExecuted, OrderState.Executed, 13));
         _statusFilters.Add(new ImageComboBoxItem(ConstNames.OrderStateCeased, OrderState.Ceased, 7));
         _statusFilters.Add(new ImageComboBoxItem(ConstNames.OrderStateCancelled, OrderState.Cancellation, 6));

         CreateWordbookDictionary();

         if (CoreBusinessLogic.BusinessLogic.EnableOrderRules)
         {
            try
            {
               object techTitle = app.SqlHelper.ExecuteScalar(String.Format(ConstSqlSentences.FormatSelectEmployee, m_DoctorCode));
               //if (techTitle == null)
               //   m_RepRuleChecker = new RecipeRuleChecker(ExchangeInfoHelper.ExchangeInfoServer, _deptCode, "");
               //else
               //   m_RepRuleChecker = new RecipeRuleChecker(ExchangeInfoHelper.ExchangeInfoServer, _deptCode, techTitle.ToString());
               if (techTitle == null)
                  m_RepRuleChecker = new RecipeRuleChecker(app.SqlHelper, _deptCode, "");
               else
                  m_RepRuleChecker = new RecipeRuleChecker(app.SqlHelper, _deptCode, techTitle.ToString());
            }
            catch
            {
               CoreBusinessLogic.BusinessLogic.EnableOrderRules = false;
               //throw new CallRemotingException(ConstMessages.MsgCanntGetRecipeRuleData);
            }
         }
      }
      #endregion

      #region custom event handler
      /// <summary>
      /// �л���ǰ�����ҽ�����¼�(�л������)
      /// </summary>
      public event EventHandler AfterSwitchOrderTable
      {
         add
         {
            onAfterSwitchOrderTable = (EventHandler)Delegate.Combine(onAfterSwitchOrderTable, value);
         }
         remove
         {
            onAfterSwitchOrderTable = (EventHandler)Delegate.Remove(onAfterSwitchOrderTable, value);
         }
      }
      private EventHandler onAfterSwitchOrderTable;

      /// <summary>
      /// ��ǰ���������Դ�����ı�󴥷�
      /// </summary>
      /// <param name="e"></param>
      /// <param name="actualIndex"></param>
      protected void FireAfterSwitchOrderTable(EventArgs e)
      {
         if (onAfterSwitchOrderTable != null)
            onAfterSwitchOrderTable(this, e);
      }

      /// <summary>
      /// ��ǰ�༭��ҽ������������־�ı��¼���
      /// �༭�������ڴ��¼��д����Ƿ���ʾ���С��Ƿ���Ҫ����ҽ�����ݱ༭���Ļ�������
      /// </summary>
      public event EventHandler AllowNewChanged
      {
         add
         {
            onAllowNewChanged = (EventHandler)Delegate.Combine(onAllowNewChanged, value);
         }
         remove
         {
            onAllowNewChanged = (EventHandler)Delegate.Remove(onAllowNewChanged, value);
         }
      }
      private EventHandler onAllowNewChanged;

      protected void FireAllowNewChanged(object sender, EventArgs e)
      {
         if (onAllowNewChanged != null)
            onAllowNewChanged(sender, e);
      }

      /// <summary>
      /// �༭ҽ����������Ļ������ݱ��ı��¼���
      /// �༭���������ڴ��¼�������ҽ�����ݱ༭���Ļ�������
      /// </summary>
      public event EventHandler ContentBaseDataChanged
      {
         add
         {
            onContentBaseDataChanged = (EventHandler)Delegate.Combine(onContentBaseDataChanged, value);
         }
         remove
         {
            onContentBaseDataChanged = (EventHandler)Delegate.Remove(onContentBaseDataChanged, value);
         }
      }
      private EventHandler onContentBaseDataChanged;

      private void FireContentBaseDataChanged(object sender, EventArgs e)
      {
         if (onContentBaseDataChanged != null)
            onContentBaseDataChanged(sender, e);

         ResetAllowNew();
      }

      /// <summary>
      /// ����ÿ���߼�����ǰ���¼���������ʾ��ǰִ�еĲ�����Ϣ
      /// </summary>
      public event EventHandler<ProcessHintArgs> ProcessStarting
      {
         add { onProcessStarting = (EventHandler<ProcessHintArgs>)Delegate.Combine(onProcessStarting, value); }
         remove { onProcessStarting = (EventHandler<ProcessHintArgs>)Delegate.Combine(onProcessStarting, value); }
      }
      private EventHandler<ProcessHintArgs> onProcessStarting;

      private void FireProcessStarting(string processHint)
      {
         if (onProcessStarting != null)
         {
            if (processHint == null)
               processHint = "";
            onProcessStarting(this, new ProcessHintArgs(processHint));
         }
      }
      #endregion

      #region event handle
      private void DoAfterOutHospitalOrderChanged(object sender, EventArgs e)
      {
         FireContentBaseDataChanged(sender, e);
      }

      protected void DoAfterPatientOrderDataChanged(object sender, EventArgs e)
      {
         ResetAllowNew();
      }
      #endregion

      #region private methods
      /// <summary>
      /// ������Ŀѡ���ֵ����Hash��ʵ��
      /// </summary>
      private void CreateWordbookDictionary()
      {
         m_ItemWordbook = new Dictionary<OrderContentKind, BaseWordbook>();
         BaseWordbook wordbook;

         // ҩƷ
         wordbook = new DruggeryBook();
         m_ItemWordbook.Add(OrderContentKind.Druggery, wordbook);

         // �շ���Ŀ��������Ŀ�ȶ�ʹ��ͬһ���ֵ��ֻ࣬��Ҫͨ����������ʾ�ض�����
         // ��ͨ��Ŀ
         wordbook = new ChargeItemBook();
         wordbook.ExtraCondition = String.Format(CultureInfo.CurrentCulture
                  , ConstSqlSentences.FormatChargeItemFilterNormal
            , ItemKind.Cure
            , ItemKind.Transfusion
            , ItemKind.Examination
            , ItemKind.Assay
            , ItemKind.Infusion
            , ItemKind.Meterial
            , ItemKind.Diagnosis
            , ItemKind.Other
            , ItemKind.Sugar);
         m_ItemWordbook.Add(OrderContentKind.ChargeItem, wordbook);

         // ������Ŀ
         wordbook = new ChargeItemBook();
         wordbook.ExtraCondition = ConstSqlSentences.ChargeItemFilterGeneral;
         m_ItemWordbook.Add(OrderContentKind.GeneralItem, wordbook);

         // �ٴ���Ŀ(δʵ�֣�����)

         // ��Ժ��ҩ
         wordbook = new DruggeryBook();
         m_ItemWordbook.Add(OrderContentKind.OutDruggery, wordbook);

         // ����
         wordbook = new ChargeItemBook();
         wordbook.ExtraCondition = String.Format(CultureInfo.CurrentCulture
                  , ConstSqlSentences.FormatChargeItemFilterOperation, ItemKind.Operation);
         m_ItemWordbook.Add(OrderContentKind.Operation, wordbook);

         // ����ҽ��
         wordbook = new ChargeItemBook();
         wordbook.ExtraCondition = ConstSqlSentences.ChargeItemFilterText;
         m_ItemWordbook.Add(OrderContentKind.TextNormal, wordbook);
         // ת��ҽ��������ҽ������Ժҽ����ʱ����Ҫ��Ӧ��Ŀ������         
      }

      private void GetSkinTestResultData()
      {
         try
         {
            _skinTestResultTable = m_CoreLogic.GetSkinTestResultData();
         }
         catch
         {
            _skinTestResultTable = null;
         }
         if (_skinTestResultTable == null)
         {
            _skinTestResultTable = new DataTable();
            _skinTestResultTable.Locale = CultureInfo.CurrentCulture;
            DataColumn col;
            col = new DataColumn(ConstSchemaNames.SkinTestColDruggeryName, typeof(string));
            _skinTestResultTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.SkinTestColBeginDate, typeof(string));
            _skinTestResultTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.SkinTestColEndDate, typeof(string));
            _skinTestResultTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.SkinTestColFlag, typeof(string));
            _skinTestResultTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.SkinTestColSpecSerialNo, typeof(string));
            _skinTestResultTable.Columns.Add(col);
         }
      }

      private void DoAfterPatientChanged()
      {
         if ((CoreBusinessLogic.BusinessLogic.EnableOrderRules) && (CurrentPatient != null) && (m_RepRuleChecker != null))
         {
            m_RepRuleChecker.MedCareCode = CurrentPatient.Medicare.Code;
            m_RepRuleChecker.WarrantCode = CurrentPatient.Medicare.VoucherCode;// ƾ֤����
            if ((CurrentPatient.InfoOfAdmission.AdmitInfo.Diagnosis != null) && (CurrentPatient.InfoOfAdmission.AdmitInfo.Diagnosis.Code != null))
               m_RepRuleChecker.DiagnoseCode = CurrentPatient.InfoOfAdmission.AdmitInfo.Diagnosis.Code; // ��Ժ���
            else
               m_RepRuleChecker.DiagnoseCode = "";
         }
         //// ��������Դ�ı��¼�
         //FireAfterSwitchOrderTable(new EventArgs());
      }

      /// <summary>
      /// ��ָ����ҽ���������ͨ���кŻ�ȡ��Ӧ��ҽ������
      /// </summary>
      /// <param name="currentView">����ͼ�в��ҵ�ҽ������</param>
      /// <param name="rowHandle">ָ�����к�</param>
      /// <returns>��Ӧ��ҽ������</returns>
      private Order GetOrderByRowHandle(OrderTableView currentView, int rowHandle)
      {
         if (!CheckRowHandleIsValidate(rowHandle, true))
            return null;

         if (rowHandle == NewItemRowHandle)
         {
            if (currentView.Count > 0)
               rowHandle = currentView.Count - 1;
            else
               return null;
         }

         if (rowHandle > (currentView.Count - 1))
            return null;

         Order mapOrder = currentView[rowHandle].OrderCache;
         if (mapOrder == null)
            throw new IndexOutOfRangeException(ConstMessages.ExceptionOrderIndexNotFind);
         else
            return mapOrder;
      }

      /// <summary>
      /// ���ѡ�е��м��Ƿ���Ч����Чʱ�����쳣
      /// </summary>
      /// <param name="selectedHandles"></param>
      private void CheckSelectedRange(int[] selectedHandles)
      {
         if ((selectedHandles == null) || (selectedHandles.Length == 0))
            throw new ArgumentException(String.Format(ConstMessages.ExceptionFormatNoValue, "ѡ��Χ"));

         int startHandle = selectedHandles[0];
         int endHandle = selectedHandles[selectedHandles.Length - 1];

         // Ӧ���ڵ�ǰ��ʾ�ļ�¼��Χ��
         if ((startHandle < 0)
            || (endHandle > CurrentView.Count))
            throw new DataCheckException(ConstMessages.CheckOrderSelectionRange, "ѡ����");
      }

      private Order[] GetSlectedOrders(int[] selectedHandles)
      {
         CheckSelectedRange(selectedHandles);

         Collection<Order> selectedOrders = new Collection<Order>();
         foreach (int rowHandle in selectedHandles)
         {
            if (CheckRowHandleIsValidate(rowHandle))
               selectedOrders.Add(GetOrderByRowHandle(CurrentView, rowHandle));
         }

         Order[] result = new Order[selectedOrders.Count];
         selectedOrders.CopyTo(result, 0);
         return result;
      }

      /// <summary>
      /// �����ѡ�е��м�����ִ�е�ҽ������
      /// </summary>
      /// <param name="rowHandles"></param>
      /// <returns></returns>
      private EditProcessFlag CalcEditProcessFlag(int[] selectedHandles)
      {
         Order[] selectedOrders;
         try
         {
            selectedOrders = GetSlectedOrders(selectedHandles);
         }
         catch
         {
            return 0;
         }

         EditProcessFlag result = m_CoreLogic.CalcEditProcessFlag(CurrentOrderTable, selectedOrders);

         // ����--�����޸ģ�����Ҫ��������
         if (HadChanged)
            result |= EditProcessFlag.Save;

         return result;
      }

      /// <summary>
      /// ���ѡ�еļ�¼�Ƿ���������ƶ�
      /// </summary>
      /// <param name="selectedHandles">ѡ�еļ�¼�к�</param>
      /// <returns>������ʱ����ԭ��</returns>
      private string CheckCanBeMoveDown(int[] selectedHandles)
      {
         CheckSelectedRange(selectedHandles);

         EditProcessFlag flag = CalcEditProcessFlag(selectedHandles);
         if ((flag & EditProcessFlag.MoveDown) > 0)
            return "";

         return CoreBusinessLogic.FormatProcessErrorMessage(ConstNames.OpMoveDown, ConstMessages.ConditionMoveDown);
      }

      /// <summary>
      /// ���ѡ�еļ�¼�Ƿ���������ƶ�
      /// </summary>
      /// <param name="selectedHandles">ѡ�еļ�¼�к�</param>
      /// <returns>������ʱ����ԭ��</returns>
      private string CheckCanBeMoveUp(int[] selectedHandles)
      {
         CheckSelectedRange(selectedHandles);

         EditProcessFlag flag = CalcEditProcessFlag(selectedHandles);
         if ((flag & EditProcessFlag.MoveUp) > 0)
            return "";

         return CoreBusinessLogic.FormatProcessErrorMessage(ConstNames.OpMoveUp, ConstMessages.ConditionMoveUp);
      }

      /// <summary>
      /// �ɽ��������ҽ���Ŀ�ʼʱ��ʱ�����ݹ���ͬ������ҽ���Ŀ�ʼʱ��
      /// </summary>
      /// <param name="rowHandle">��ǰ���µ�ҽ�����</param>
      /// <param name="startDateTime">���º��ҽ��ʱ��</param>
      private void SynchStartDateAfterOrderValueChanged(int rowHandle, DateTime startDateTime)
      {
         Order aimOrder = GetOrderByRowHandle(CurrentView, rowHandle);
         Order temp;
         // ���ҽ���ѷ��飬��ͬ��ͬ��ҽ����ʱ��
         // �����ʼʱ����ں�һ��ҽ���Ŀ�ʼʱ�䣬��ͬʱ�ı����ҽ����ʱ�䣬ֱ�����м�¼�Ŀ�ʼʱ�䶼���Ϲ���
         for (int index = CurrentOrderTable.Orders.Count - 1; index >= 0; index--)
         {
            temp = CurrentOrderTable.Orders[index];
            // �ߵ�����һ�����ڷ����ǰ��ʱֱ���˳�
            // (��Ϊ�ڵ��ô˷���ǰ�Ѿ����߼���飬��֤�����Ŀ�ʼʱ�䲻��С��ǰ��ҽ����ʱ��)
            if (temp.GroupSerialNo < aimOrder.GroupSerialNo)
               break;
            if (temp.StartDateTime < startDateTime)
               temp.StartDateTime = startDateTime;
         }
      }

      /// <summary>
      /// ���ָ��ҽ�����÷���Ƶ����ͬ�������ҽ����һ�£������ָ��ҽ������Ϣ����ͬ��
      /// </summary>
      /// <param name="aimOrder"></param>
      private void SynchPublicInfoOfGroupedOrders(Order aimOrder)
      {
         Collection<Order> orderList = CurrentOrderTable.GetOtherOrdersOfSameGroup(aimOrder);
         CurrentView.BeginInit();
         foreach (Order temp in orderList)
         {
            //if (temp.Content.ItemUsage.Equals(aimOrder.Content.ItemUsage)
            //   && (temp.Content.ItemFrequency.Equals(aimOrder.Content.ItemFrequency)))
            //   break; // ͬ���ҽ��ֻҪ��һ����ָ��ҽ���÷���Ƶ����ͬ����˵������ҽ��Ҳ��ͬ
            temp.Content.ItemFrequency = aimOrder.Content.ItemFrequency.Clone();
            temp.Content.ItemUsage = aimOrder.Content.ItemUsage.Clone();
         }
         CurrentView.EndInit();
      }

      private bool JudgeCellOfOriginalRowCanEdit(string columnName, int rowHandle)
      {
         // �������ݽ��б༭
         Order temp = GetOrderByRowHandle(CurrentView, rowHandle);

         // ����ҽ���еġ�ת��ҽ�������ܱ��༭
         if (!IsTempOrder && (temp.Content != null)
            && (temp.Content.OrderKind == OrderContentKind.TextShiftDept))
            return false;

         // ��ʱҽ���еġ����뵥ҽ�������ܱ��༭
         if (IsTempOrder)
         {
            if (((TempOrder)temp).ApplySerialNo != 0)
               return false;
         }

         // ��ǰҽ��ֻ��
         if (temp.ReadOnly)
            return false;

         // ����ֻ�ſ���ҽ���Ŀ�ʼ���ں�ʱ����޸�
         switch (columnName)
         {
            case OrderView.UNStartDate:
            case OrderView.UNStartTime:
               return (temp.State == OrderState.New);
            default:
               return false;
         }

         //// 1����ʼ���ڡ���ʼʱ�䡢ҽ������
         ////       ֻ���ڵ�ǰҽ��״̬������ʱ���Ա༭
         //// 2��ֹͣ���ڡ�ֹͣʱ��
         ////       ֻ���ڵ�ǰҽ���ǳ���ҽ��
         ////       �ҿ���ֹͣ��ҩƷ����Ŀ��ҽ����
         ////       ��ȡ��״̬
         ////       ֹͣ��δ����˿��Ա༭
         //// 3�������ж�������ֱ�ӱ༭
         //switch (columnName)
         //{
         //   case OrderView.UNStartDate:
         //   case OrderView.UNStartTime:
         //   //case OrderView.UNContent:
         //   //   if (temp.State == OrderState.New)
         //   //      return true;
         //   //   else
         //   //      return false;
         //   //case OrderView.UNCeaseDate:
         //   //case OrderView.UNCeaseTime:
         //   //   LongOrder longOrder = temp as LongOrder;
         //   //   if ((longOrder != null)
         //   //      && (longOrder.Content.CanCeased)
         //   //      && ((longOrder.State == OrderState.New) 
         //   //         || (longOrder.State == OrderState.Executed)))
         //   //      return true;
         //   //   else
         //   //      return false;
         //   default:
         //      return false;
         //}
      }

      private static bool JudgeCellOfNewRowCanEdit(string columnName)
      {
         switch (columnName)
         {
            case OrderView.UNStartDate:
            case OrderView.UNStartTime:
               //case OrderView.UNContent:
               //case OrderView.UNCeaseDate:
               //case OrderView.UNCeaseTime:
               return true;
            default:
               return false;
         }
      }

      private void ResetAllowNew()
      {
         bool allowNew;
         if (IsTempOrder)
            allowNew = m_CoreLogic.AllowAddTemp;
         else
            allowNew = m_CoreLogic.AllowAddLong;
         if (_allowAddNew != allowNew)
         {
            _allowAddNew = allowNew;
            FireAllowNewChanged(this, new EventArgs());
         }
      }

      private int GetCountOfBrothers(int startPosition, bool downwards)
      {
         int result = 0;
         int borderIndex;
         int moveStep;
         if (downwards) // ���²���
         {
            borderIndex = CurrentView.Count - 1;
            moveStep = 1;
         }
         else  // ���ϲ���
         {
            borderIndex = 0;
            moveStep = -1;
         }
         bool needCheckLinkToApply = (CurrentView.Table.IsTempOrder
            && (CurrentView[startPosition].ApplySerialNo != 0));
         int nextIndex = startPosition;
         // ����һ��Ļ����ͬһ�����뵥��ŵ�ҽ������һ��
         while (nextIndex != borderIndex)
         {
            nextIndex = nextIndex + moveStep;
            if (CurrentView[nextIndex].GroupSerialNo != CurrentView[startPosition].GroupSerialNo)
            {
               if (needCheckLinkToApply)
               {
                  if (CurrentView[nextIndex].ApplySerialNo == CurrentView[startPosition].ApplySerialNo)
                     result++;
                  else
                     return result;
               }
               else
                  return result;
            }
            else
            {
               needCheckLinkToApply = false; // ҽ�������ܼ������ֹ������뵥
               result++;
            }
         }
         return result;
      }

      private void DoSaveOrderData(bool isTempOrder, bool skipWarnning)
      {
         OrderTable currentTable = GetCurrentOrderTable(isTempOrder);
         if (currentTable.HadChanged)
         {
            Order[] changedOrders = currentTable.GetNewAndChangedOrders();
            if ((changedOrders != null) && (changedOrders.Length > 0))
            {
               FireProcessStarting(GetProcessHint(ConstNames.OpCheck, isTempOrder));

               try
               {
                  m_CoreLogic.CheckOrderData(currentTable, changedOrders, true, skipWarnning);
               }
               catch (DataCheckException err)
               {
                  if ((err.WarnningLevel == 1) || (!skipWarnning))
                     throw err;
               }

               FireProcessStarting(GetProcessHint(ConstNames.OpSave, isTempOrder));
               currentTable.DefaultView.BeginInit();
               m_CoreLogic.SaveChangedOrderDataInEditor(currentTable, changedOrders, m_DoctorCode, m_MacAddress);
               currentTable.DefaultView.EndInit();
            }
         }
      }

      private string GetProcessHint(string opName, bool isTempOrder)
      {
         if (isTempOrder)
            return String.Format(CultureInfo.CurrentCulture, "{0} {1} ���ݡ���", opName, ConstNames.TempOrder);
         else
            return String.Format(CultureInfo.CurrentCulture, "{0} {1} ���ݡ���", opName, ConstNames.LongOrder);
      }
      #endregion

      #region public methods
      /// <summary>
      /// ���ָ�����к��Ƿ�����Ч��
      /// </summary>
      /// <param name="rowHandle"></param>
      /// <returns>true����  false����</returns>
      public bool CheckRowHandleIsValidate(int rowHandle)
      {
         return CheckRowHandleIsValidate(rowHandle, false);
      }

      /// <summary>
      /// ���ָ�����к��Ƿ�����Ч��
      /// </summary>
      /// <param name="rowHandle"></param>
      /// <param name="includeNew">�Ƿ���԰�������</param>
      /// <returns></returns>
      public bool CheckRowHandleIsValidate(int rowHandle, bool includeNew)
      {
         if (rowHandle == InvalidRowHandle)
            return false;
         else if (rowHandle == NewItemRowHandle)
            return includeNew;
         else if (rowHandle < 0)
            return false;
         else
            return true;
      }

      /// <summary>
      /// �ж����кź�����ָ���ĵ�Ԫ���Ƿ�ɽ���༭״̬
      /// </summary>
      /// <param name="columnName">ָ��������</param>
      /// <param name="rowHandle">ָ�����к�</param>
      /// <returns></returns>
      public bool JudgeCellCanEdit(string columnName, int rowHandle)
      {
         // ҽ����ֻ��������ʽ�к�
         if ((m_CoreLogic.CurrentPatient == null)
            || (!CurrentView.AllowEdit) || (rowHandle == InvalidRowHandle))
            return false;

         // ����
         if (rowHandle == NewItemRowHandle)
            return JudgeCellOfNewRowCanEdit(columnName);
         else
            return JudgeCellOfOriginalRowCanEdit(columnName, rowHandle);
      }

      /// <summary>
      /// ����ҽ�����ݵ�����ţ����ض�Ӧ���ֵ���ʵ��
      /// </summary>
      /// <param name="catalogNo"></param>
      /// <returns>�޶�Ӧ�ֵ���ʵ���򷵻�NULL</returns>
      public BaseWordbook GetItemWordbook(OrderContentKind orderCatalog)
      {
         if (m_ItemWordbook.ContainsKey(orderCatalog))
         {
            BaseWordbook wordbook = m_ItemWordbook[orderCatalog];
            // ����������ڳ���ҽ���������ҩ������Ҫ����ҩƷ�ֵ�
            if (orderCatalog == OrderContentKind.Druggery)
            {
               if ((!IsTempOrder) && (!CoreBusinessLogic.BusinessLogic.AllowLongHerbOrder))
                  wordbook.ExtraCondition = String.Format(ConstSqlSentences.FormatDruggeryKindFilter, (int)ItemKind.HerbalMedicine);
               else
                  wordbook.ExtraCondition = "";
            }

            return wordbook;
         }
         else
            return null;
      }

      /// <summary>
      /// ����Ĭ�ϵ�ҽ������ʵ��(��ǰһ��ҽ��Ϊ׼���༭����ҽ��������ʱ����)
      /// </summary>
      /// <param name="currentRowHandle">��ǰGrid��Focused���к�</param>
      /// <returns></returns>
      public OrderContent CreateDefaultContent(int currentRowHandle)
      {
         int preRowHandle;
         if (CheckRowHandleIsValidate(currentRowHandle))
            preRowHandle = currentRowHandle - 1;
         else if (currentRowHandle == NewItemRowHandle)//(CurrentView.Count > 0)
            preRowHandle = CurrentView.Count - 2; // ��ʱ������List�����һ����¼
         else
            preRowHandle = CurrentView.Count - 1; // ��������

         Order preOrder = GetOrderByRowHandle(CurrentView, preRowHandle);
         OrderContent defaultContent;

         if ((preOrder != null) && (preOrder.State == OrderState.New))// ����ҽ����������һ��ҽ������һ��
            defaultContent = OrderContentFactory.CreateOrderContent(
               preOrder.Content.OrderKind, null);
         else // Ĭ��ΪҩƷҽ��
            defaultContent = OrderContentFactory.CreateOrderContent(
               OrderContentKind.Druggery, null);
         return defaultContent;
      }

      /// <summary>
      /// Grid�в������к󣬳�ʼ����Ӧ������ҽ��������״̬����δ��ʼ����������
      /// </summary>
      /// <param name="newOrder">����ҽ����Ӧ��ʵ��</param>
      /// <param name="tempOrder">�����ʼ�����ݵ�ҽ��ʵ��</param>
      public void InitNewOrderValue(Order newOrder, Order tempOrder)
      {
         if (newOrder == null)
            throw new ArgumentNullException(ConstMessages.ExceptionNullOrderObject);

         newOrder.BeginInit();

         newOrder.PatientId = m_CoreLogic.CurrentPatientNo;

         // ����ҽ����Ĭ��Ϊ�����飬�����������ڱ༭ʱû�����ã����浽���ݿ�ʱ�����
         // ��ɸ��е����ݱ༭���ٸ����Ƿ��Զ�������д�������

         newOrder.OriginalWard = new Eop.Ward(_wardCode);
         newOrder.OriginalDepartment = new Eop.Department(_deptCode);

         if (newOrder.CreateInfo == null)
            newOrder.CreateInfo = new OrderOperateInfo(m_DoctorCode, DateTime.Now);
         else
            newOrder.CreateInfo.SetPropertyValue(m_DoctorCode, DateTime.Now);

         // �����ʼʱ��δ��ֵ����ʹ��Ĭ��ʱ��
         if (tempOrder.StartDateTime.Date == DateTime.MinValue.Date)
            newOrder.StartDateTime = DefaultStartDateTime;
         else if (tempOrder.StartDateTime.TimeOfDay == TimeSpan.Zero)
            newOrder.StartDateTime = tempOrder.StartDateTime.Date + DefaultStartDateTime.TimeOfDay;
         else
            newOrder.StartDateTime = tempOrder.StartDateTime;

         if (tempOrder.Content != null)
            newOrder.Content = tempOrder.Content;

         if ((newOrder as LongOrder) != null)
         {
            OrderOperateInfo ceaseInfo = (tempOrder as LongOrder).CeaseInfo;
            if (ceaseInfo != null)
               (newOrder as LongOrder).CeaseOrder(ceaseInfo.Executor.Code, ceaseInfo.ExecuteTime, OrderCeaseReason.Natural);
         }

         newOrder.EndInit();
      }

      /// <summary>
      /// �ڽ��༭���е����ݸ�ֵ��ҽ������ʱ��֤ҽ����ʼʱ���ҽ�������Ƿ���ȷ
      /// </summary>
      /// <param name="targetIndex">��ǰ�����ҽ���к�(����ڵ�ǰ��ͼ���к�)(��������������ҽ�������кſ��ܲ���List��������)</param>
      /// <param name="orderTemp">�����У��ֵ��ҽ����ʱ����</param>
      /// <param name="updateFalg">�����Щ�����ѱ�����</param>
      public void CheckOrderValueBeforeSet(int targetIndex, Order orderTemp, UpdateContentFlag updateFalg)
      {
         int trueIndex; // ��ͼ�е��к�ת��Table�е��к�
         if (targetIndex < 0)
            trueIndex = targetIndex;
         else
         {
            Order temp = GetOrderByRowHandle(CurrentView, targetIndex);
            if (temp != null)
               trueIndex = CurrentOrderTable.Orders.IndexOf(temp.SerialNo);
            else
               trueIndex = -1;
         }
         m_CoreLogic.CheckOrderValueBeforeSet(CurrentOrderTable
            , orderTemp
            , trueIndex
            , updateFalg);
      }

      /// <summary>
      /// ����ҽ������Ŀ�ʼʱ���ҽ������
      /// </summary>
      /// <param name="rowHandle">��Ҫ�����ҽ���к�</param>
      /// <param name="orderTemp">������ֵ��ҽ����ʱ����</param>
      /// <param name="updateFlag">�����Щ�����ѱ�����</param>
      public void SetNewOrderElementValue(int rowHandle, Order orderTemp, UpdateContentFlag updateFlag)
      {
         if (orderTemp == null)
            throw new ArgumentNullException(String.Format(ConstMessages.ExceptionFormatNullObject, "ҽ����ʱ����"));
         // ����ǰ�Ƿ���Ҫ����һ�����ݼ�飿����
         Order aimOrder = GetOrderByRowHandle(CurrentView, rowHandle);
         if (aimOrder == null)
            throw new IndexOutOfRangeException(ConstMessages.ExceptionOrderIndexNotFind);

         if ((updateFlag & UpdateContentFlag.StartDate) > 0)
         {
            aimOrder.StartDateTime = orderTemp.StartDateTime;
            SynchStartDateAfterOrderValueChanged(rowHandle, orderTemp.StartDateTime);
         }
         if ((updateFlag & UpdateContentFlag.Content) > 0)
         {
            aimOrder.Content = PersistentObjectFactory.CloneEopBaseObject(orderTemp.Content) as OrderContent;
            // ���ҽ���ѷ��飬��ͬ��ͬ��ҽ�����÷���Ƶ��
            if (aimOrder.GroupPosFlag != GroupPositionKind.SingleOrder)
               SynchPublicInfoOfGroupedOrders(aimOrder);
         }
         if ((updateFlag & UpdateContentFlag.CeaseDate) > 0)
         {
            OrderOperateInfo ceaseInfo = (orderTemp as LongOrder).CeaseInfo;
            (aimOrder as LongOrder).CeaseOrder(ceaseInfo.Executor.Code, ceaseInfo.ExecuteTime, OrderCeaseReason.Natural);
         }
         if (updateFlag != 0)
         {
            // ���������޸ĺ�Ҫ���´�������Ϣ
            aimOrder.CreateInfo.Executor.Code = m_DoctorCode;
         }
      }

      /// <summary>
      /// ��ǰҽ�����ָ��λ�ò���ҽ��
      /// </summary>
      /// <param name="rowHandle"></param>
      /// <param name="order"></param>
      public void InsertOrder(int rowHandle, Order order)
      {
         // ����ǰ���м��
         Order focusedOrder = null;
         if ((rowHandle >= 0) && (rowHandle < CurrentView.Count))
            focusedOrder = CurrentView[rowHandle].OrderCache;

         m_CoreLogic.CheckCanInsertOrder(focusedOrder, IsTempOrder, new Order[] { order });

         CurrentOrderTable.InsertOrderAt(order, CurrentView.GetOriginalIndex(rowHandle));
      }

      /// <summary>
      /// ɾ��ҽ�������в�����״̬��ҽ����Ŀǰ��ɾ����ʼʱ���ҽ������Ϊ�յ���ҽ��
      /// </summary>
      public void DeleteAbnormalNewOrders()
      {
         Order temp;
         for (int index = CurrentOrderTable.Orders.Count - 1; index >= 0; index--)
         {
            temp = CurrentOrderTable.Orders[index];
            if (temp.State == OrderState.New)
            {
               if ((temp.Content == null) || (temp.Content.ToString().Length == 0))
                  CurrentOrderTable.RemoveOrderAt(index);
            }
         }
      }

      /// <summary>
      /// ɾ��ָ������ҽ��
      /// </summary>
      /// <param name="selectedHandles"></param>
      public void DeleteNewOrder(int[] selectedHandles)
      {
         Order[] selectedOrders = GetSlectedOrders(selectedHandles);

         m_CoreLogic.DeleteNewOrder(selectedOrders, IsTempOrder);
      }

      /// <summary>
      /// ��ָ����Χ��ҽ����Ϊһ��
      /// </summary>
      /// <param name="selectedHandles">��ǰѡ�е�ҽ���к�</param>
      public void SetOrderGroup(int[] selectedHandles)
      {
         Order[] selectedOrders = GetSlectedOrders(selectedHandles);

         m_CoreLogic.SetOrderGroup(selectedOrders, IsTempOrder);
      }

      /// <summary>
      /// �Զ�����ҽ�����з���
      /// </summary>
      /// <returns>������ļ�¼���к�</returns>
      public int[] AutoSetNewOrderGrouped()
      {
         if (AllowAddNew)
         {
            return m_CoreLogic.AutoSetNewOrderGrouped(IsTempOrder);
         }
         return null;
      }

      /// <summary>
      /// ȡ��������Ϣ
      /// </summary>
      /// <param name="selectedHandles">��ǰѡ�е�ҽ���к�</param>
      public void CancelOrderGroup(int[] selectedHandles)
      {
         Order[] selectedOrders = GetSlectedOrders(selectedHandles);

         m_CoreLogic.CancelOrderGroup(selectedOrders, IsTempOrder);
      }

      /// <summary>
      /// ���ָ����Χ�ڵ�ҽ��
      /// </summary>
      /// <param name="selectedHandles">��ǰѡ�е�ҽ���к�</param>
      public void AuditOrder(int[] selectedHandles)
      {
         Order[] selectedOrders;
         selectedOrders = GetSlectedOrders(selectedHandles);
         if (selectedHandles.Length != selectedHandles.Length)
            throw new ArgumentException(ConstMessages.CheckSelectedRangWithDataRow);

         m_CoreLogic.AuditOrder(selectedOrders, m_DoctorCode, DateTime.Now, IsTempOrder);
      }

      /// <summary>
      /// ���ݵ�ǰѡ�м�¼��������ù�������ť��״̬
      /// </summary>
      /// <param name="rowHandles">ѡ�еļ�¼�к�(Ӧ�뵱ǰ��ͼһ��)</param>
      /// <returns>��Ӧ��ť��״̬����</returns>
      public EditProcessFlag GetBarItemStatus(int[] rowHandles)
      {
         if ((!AllowEdit) || (rowHandles == null) || (rowHandles.Length == 0))
         {
            if (HadChanged)
               return EditProcessFlag.Save;
            else
               return 0;
         }
         return CalcEditProcessFlag(rowHandles);
      }

      /// <summary>
      /// ����ָ����ҽ�����ͣ���ȡ��Ӧ��ҽ�����ݸ��༭���Ƿ���ñ�־
      /// </summary>
      /// <param name="orderKind">ҽ������</param>
      /// <returns></returns>
      public static EditorEnableFlag GetContentEditorEnableStatus(OrderContentKind orderKind)
      {
         EditorEnableFlag result = 0;
         // ���������������ҽ���ⶼ��Ҫѡ��Ŀ
         if (!((orderKind == OrderContentKind.TextAfterOperation)
            || (orderKind == OrderContentKind.TextShiftDept)
            || (orderKind == OrderContentKind.TextLeaveHospital)))
            result |= EditorEnableFlag.CanChoiceItem;
         // ҩƷ����ͨ��Ŀ��Ҫ������������������ִ������(���ڻ��Ժ��ҩʱ��Ч)
         if ((orderKind == OrderContentKind.Druggery)
            || (orderKind == OrderContentKind.ChargeItem)
            || (orderKind == OrderContentKind.OutDruggery))
         {
            result |= EditorEnableFlag.NeedInputAmount | EditorEnableFlag.CanSetExecuteDays;
         }
         // ֻ��ҩƷ��Ҫ�����÷�
         if ((orderKind == OrderContentKind.Druggery)
            || (orderKind == OrderContentKind.OutDruggery))
            result |= EditorEnableFlag.CanChoiceUsage;
         // ֻ��ҩƷ����ͨ��Ŀ��Ҫ����Ƶ��
         if ((orderKind == OrderContentKind.Druggery)
            || (orderKind == OrderContentKind.ChargeItem)
            || (orderKind == OrderContentKind.OutDruggery))
            result |= EditorEnableFlag.CanChoiceFrequency;
         // ��Ժ��ҩ�ĸ�����Ϣ
         if (orderKind == OrderContentKind.OutDruggery)
            result |= EditorEnableFlag.NeedOutDruggeryInfo;
         // ת��ҽ���ĸ�����Ϣ
         if (orderKind == OrderContentKind.TextShiftDept)
            result |= EditorEnableFlag.NeedShiftDeptInfo;
         // ����ҽ���ͳ�Ժҽ���ĸ�����Ϣ
         if (orderKind == OrderContentKind.Operation)
            result |= EditorEnableFlag.NeedOperationInfo;
         if (orderKind == OrderContentKind.TextLeaveHospital)
            result |= EditorEnableFlag.NeedLeaveHospitalTime;
         // ת�ơ���Ժ��������ʱ������������(��ΪҪ�������ֶδ���������)
         if ((orderKind != OrderContentKind.TextLeaveHospital)
            && (orderKind != OrderContentKind.TextShiftDept)
            && (orderKind != OrderContentKind.TextAfterOperation))
            result |= EditorEnableFlag.CanInputEntrust;
         return result;
      }

      /// <summary>
      /// ���ݴ����ҽ�����������ҽ��״̬���л���ǰ�༭��ҽ������
      /// </summary>
      /// <param name="catalog">ҽ�����</param>
      /// <param name="status">ҽ��״̬</param>
      public void SwitchOrderTable(bool isTempOrder, OrderState state)
      {
         IsTempOrder = isTempOrder;
         FilterOrderByState(state); // ͨ�����ô˷����������¼�
         //FireAfterSwitchOrderTable(new EventArgs());
      }

      /// <summary>
      /// ���ݴ����ҽ��״̬���˵�ǰ�༭��ҽ����
      /// </summary>
      /// <param name="status"></param>
      public void FilterOrderByState(OrderState state)
      {
         CurrentView.State = state;
         //ResetAllowNew();
      }

      /// <summary>
      /// ��ѡ�е���ҽ������һ��
      /// </summary>
      /// <param name="selectedHandles">��ǰѡ�е�ҽ���к�</param>
      /// <returns>ʵ�����Ƶĸ���</returns>
      public int MoveNewOrderDown(int[] selectedHandles)
      {
         // ���ȼ���Ƿ��������
         string errMsg = CheckCanBeMoveDown(selectedHandles);
         if ((errMsg == null) || (errMsg.Length == 0))
         {
            CurrentView.BeginInit();

            // ͨ����ѡ�м�¼����һ��ҽ������nλʵ�����Ƶ�Ч��
            // (�����һ��ҽ�����ڷ������������뵥����ͬ���Ҫһ������)
            int nextIndex = selectedHandles[selectedHandles.Length - 1] + 1;
            int newIndex = nextIndex - selectedHandles.Length;
            int brothersCount = GetCountOfBrothers(nextIndex, true);
            for (int increment = 0; increment <= brothersCount; increment++)
            {
               CurrentView.Move(nextIndex + increment, newIndex + increment);
               // �ƶ�ҽ�����Զ�����ʱ�䣨��������ҽ���Ŀ�ʼʱ�䲻�ܴ�����һ����
               if (CurrentView[nextIndex + increment].OrderCache.StartDateTime > CurrentView[newIndex + increment + 1].OrderCache.StartDateTime)
                  CurrentView[nextIndex + increment].OrderCache.StartDateTime = CurrentView[newIndex + increment + 1].OrderCache.StartDateTime;
            }
            CurrentView.EndInit();
            return brothersCount + 1;
         }
         else
            throw new DataCheckException(errMsg, "ѡ����");
      }

      /// <summary>
      /// ��ѡ�е���ҽ������һ��
      /// </summary>
      /// <param name="selectedHandles">��ǰѡ�е�ҽ���к�</param>
      /// <return>ʵ�����Ƶĸ���</return>
      public int MoveNewOrderUp(int[] selectedHandles)
      {
         // ���ȼ���Ƿ��������
         string errMsg = CheckCanBeMoveUp(selectedHandles);
         if ((errMsg == null) || (errMsg.Length == 0))
         {
            CurrentView.BeginInit();

            // ͨ����ѡ�м�¼��ǰһ��ҽ������nλʵ�����Ƶ�Ч��
            // (�����һ��ҽ�����ڷ������������뵥����ͬ���Ҫһ������)
            int nextIndex = selectedHandles[0] - 1;
            int newIndex = nextIndex + selectedHandles.Length;
            int brothersCount = GetCountOfBrothers(nextIndex, false);
            for (int increment = 0; increment <= brothersCount; increment++)
            {
               CurrentView.Move(nextIndex - increment, newIndex - increment);
               // �ƶ�ҽ�����Զ�����ʱ�䣨��������ҽ���Ŀ�ʼʱ�䲻��С����һ����
               if (CurrentView[newIndex - increment].OrderCache.StartDateTime < CurrentView[newIndex - increment - 1].OrderCache.StartDateTime)
                  CurrentView[newIndex - increment].OrderCache.StartDateTime = CurrentView[newIndex - increment - 1].OrderCache.StartDateTime;
            }
            CurrentView.EndInit();
            return brothersCount + 1;
         }
         else
            throw new DataCheckException(errMsg, "ѡ����");
      }

      /// <summary>
      /// ȡ�������ҽ��
      /// </summary>
      /// <param name="rowHandles"></param>
      public void CancelOrder(int[] selectedHandles)
      {
         Order[] selectedOrders = GetSlectedOrders(selectedHandles);

         CurrentView.BeginInit();
         m_CoreLogic.CancelOrder(selectedOrders, m_DoctorCode, DateTime.Now, IsTempOrder);
         CurrentView.EndInit();
      }

      /// <summary>
      /// ���ó���ҽ����ֹͣ����
      /// </summary>
      /// <param name="selectedHandles"></param>
      public void SetLongOrderCeaseInfo(int[] selectedHandles, DateTime ceaseTime)
      {
         Order[] selectedOrders = GetSlectedOrders(selectedHandles);

         CurrentView.BeginInit();
         m_CoreLogic.SetLongOrderCeaseInfo(selectedOrders, m_DoctorCode, ceaseTime);
         CurrentView.EndInit();
      }

      /// <summary>
      /// ����Ƥ����Ϣ
      /// </summary>
      /// <param name="specSerialNo"></param>
      /// <param name="druggeryName"></param>
      /// <param name="skinTestResultKind"></param>
      public void SaveSkinTestResult(decimal specSerialNo, string druggeryName, SkinTestResultKind skinTestResultKind)
      {
         DataTable newResult =
            m_CoreLogic.SaveSkinTestResult(m_DoctorCode, specSerialNo, druggeryName, skinTestResultKind);
         _skinTestResultTable.Merge(newResult);
      }

      /// <summary>
      /// ��ȡָ��ҽ��Ϊ��ǰ������ָ��ʱ��֮���¿���ҽ��
      /// </summary>
      /// <param name="doctorCode">ҽ������</param>
      /// <param name="startTime">ָ����ʱ��</param>
      /// <param name="needDrug">��ȡҩƷ��������Ŀҽ����־��true��ҩƷ</param>
      /// <returns></returns>
      public DataTable GetNewOrder(string doctorCode, DateTime startTime, bool needDrug)
      {
         return m_CoreLogic.GetNewOrder(doctorCode, startTime, needDrug);
      }

      /// <summary>
      /// ����������޸ĵ�ҽ�����浽���ݿ���
      /// </summary>
      /// <param name="skipWarnning">�Ƿ�����������Ϣ</param>
      public void SaveOrderTableData(bool skipWarnning)
      {
         if (!HadChanged)
            return;

         try
         {
            DoSaveOrderData(IsTempOrder, skipWarnning); // �ȱ��浱ǰ���������

            DoSaveOrderData(!IsTempOrder, skipWarnning);
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// �ֹ�ͬ��ָ�������͵�ҽ�������ݵ�HIS�У��ڵ���ǰҪ�ֹ�ִ�����ݱ��淽����
      /// </summary>
      public void ManualSynchDataToHIS()
      {
         FireProcessStarting(GetProcessHint(ConstNames.OpSend, IsTempOrder));
         OrderTable currentTable = m_CoreLogic.GetCurrentOrderTable(IsTempOrder);
         m_CoreLogic.SynchDataToHIS(currentTable, m_DoctorCode, m_MacAddress);

         FireProcessStarting(GetProcessHint(ConstNames.OpSend, !IsTempOrder));
         currentTable = m_CoreLogic.GetCurrentOrderTable(!IsTempOrder);
         m_CoreLogic.SynchDataToHIS(currentTable, m_DoctorCode, m_MacAddress);
      }

      public string GetHintOfEditOperation(int focusedRowHandle)
      {
         // �����ѳ�Ժ�����ܱ༭ҽ��
         if (!AllowEdit)
            return "�����ѳ������ѳ�Ժ�����ܱ༭ҽ��";

         Order focusedOrder = GetOrderByRowHandle(CurrentView, focusedRowHandle);
         OrderState orderstate = OrderState.None;
         bool link2Request = false;
         if (focusedOrder != null)
         {
            orderstate = focusedOrder.State;
            TempOrder temp = focusedOrder as TempOrder;
            if (temp != null)
               link2Request = (temp.ApplySerialNo != 0);
         }

         if (m_CoreLogic.HasOutHospitalOrder)
            return "�ѿ���Ժҽ������ȡ��ǰֻ�ܱ༭��Ժ��ҩҽ��";
         else if (m_CoreLogic.HasShiftDeptOrder)
            return "�ѿ�ת��ҽ������ȡ��ǰ�����޸�ҽ��";

         switch (orderstate)
         {
            case OrderState.Ceased:
               return "��ҽ����ֹͣ�������޸�";
            case OrderState.Cancellation:
               return "��ҽ����ȡ���������޸�";
            case OrderState.Executed:
               return "��ҽ����ִ��";
            case OrderState.Audited:
               if (link2Request)
                  return "��ҽ�������뵥��������Ҫ�޸Ļ�ɾ�����������뵥ģ�����";
               else
                  return "��ҽ�������(����ȡ��)";
            case OrderState.New:
               if (link2Request)
                  return "��ҽ�������뵥��������Ҫ�޸Ļ�ɾ�����������뵥ģ�����";
               else
                  return "˫������ҽ��������ҽ���༭��������޸�";
            default:
               return "";
         }
      }

      /// <summary>
      /// ��ҽ���б��м���ѡ�е�ҽ��
      /// </summary>
      /// <param name="selectedHandles"></param>
      /// <returns>���г�����ҽ������</returns>
      public Order[] CutOrdersFromList(int[] selectedHandles)
      {
         Order[] selectedOrders = GetSlectedOrders(selectedHandles);

         // ��ѡ��ҽ����ҽ�������Ƴ�
         m_CoreLogic.DeleteNewOrder(selectedOrders, IsTempOrder);
         return selectedOrders;
      }

      /// <summary>
      /// ��ҽ���б��и���ѡ�е�ҽ��
      /// </summary>
      /// <param name="selectedHandles"></param>
      /// <returns></returns>
      public Order[] CopyOrdersFromList(int[] selectedHandles)
      {
         Order[] selectedOrders = GetSlectedOrders(selectedHandles);

         Order[] cloneOrders;
         cloneOrders = new Order[selectedOrders.Length];
         for (int index = 0; index < selectedOrders.Length; index++)
         {
            if (IsTempOrder)
               cloneOrders[index] = PersistentObjectFactory.CloneEopBaseObject(selectedOrders[index]) as TempOrder;
            else
               cloneOrders[index] = PersistentObjectFactory.CloneEopBaseObject(selectedOrders[index]) as LongOrder;
         }

         return cloneOrders;
      }

      /// <summary>
      /// ����Ƿ�������ǰҽ��Table�ĵ�ǰλ�ò���ҽ��
      /// </summary>
      /// <returns>true:���� false:������, ���쳣�б��治�ܲ����ԭ��</returns>
      public void CheckAllowInsertOrder(int[] selectedHandles)
      {
         if ((selectedHandles != null) && (selectedHandles.Length > 1))
            throw new ArgumentException("ѡ�ж���ҽ��������²��ܲ���ҽ��");

         Order focusedOrder = null;
         if ((selectedHandles != null) && (selectedHandles.Length == 1))
            focusedOrder = CurrentOrderTable.Orders[selectedHandles[0]];

         m_CoreLogic.CheckCanInsertOrder(focusedOrder, IsTempOrder, null);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="contents"></param>
      public void InsertSuiteOrder(object[,] contents, Order focusedOrder)
      {
         CurrentView.BeginInit();
         int insertedNum = m_CoreLogic.InsertSuiteOrder(CurrentOrderTable, m_DoctorCode, contents, focusedOrder);
         int remainNum = contents.GetUpperBound(0) - insertedNum + 1;
         if (remainNum != 0)
            CustomMessageBox.MessageShow("��" + remainNum.ToString() + "����¼��Ϊ������У�����û�в���", CustomMessageBoxKind.InformationOk);
         CurrentView.EndInit();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="currentOrderTable"></param>
      /// <param name="contents"></param>
      /// <param name="fousedOrder"></param>
      public void InsertSuiteOrder(OrderTable currentOrderTable, object[,] contents, Order fousedOrder)
      {
         int insertedNum = m_CoreLogic.InsertSuiteOrder(currentOrderTable, m_DoctorCode, contents, fousedOrder);
         int remainNum = contents.GetUpperBound(0) - insertedNum + 1;
         if (remainNum != 0)
            CustomMessageBox.MessageShow("��" + remainNum.ToString() + "����¼��Ϊ������У�����û�в���", CustomMessageBoxKind.InformationOk);
      }

      /// <summary>
      /// ���ݱ�־���ض�Ӧҽ����
      /// </summary>
      /// <param name="isTempOrder"></param>
      /// <returns></returns>
      public OrderTable GetCurrentOrderTable(bool isTempOrder)
      {
         return m_CoreLogic.GetCurrentOrderTable(isTempOrder);
      }

      /// <summary>
      /// ��鵱ǰ��ҽ�������е���Ŀ�Ƿ����㴦������
      /// </summary>
      /// <param name="content">ҽ�����ݶ���</param>
      public void CheckRecipeRule(OrderContent content)
      {
         if ((content == null) || (content.Item == null) || (!content.Item.KeyInitialized))
            return;

         if (CoreBusinessLogic.BusinessLogic.EnableOrderRules && (m_RepRuleChecker != null))
         {
            switch (content.OrderKind)
            {
               case OrderContentKind.Druggery:
               case OrderContentKind.OutDruggery:
                  m_RepRuleChecker.CheckDruggery((content.Item as Druggery), content.Amount, content.CurrentUnit);
                  break;
               case OrderContentKind.ChargeItem:
                  m_RepRuleChecker.CheckItem((content.Item as ChargeItem), content.Amount, content.CurrentUnit);
                  break;
            }
         }
         if (CoreBusinessLogic.BusinessLogic.EnableItemAgeWarning && (content.OrderKind == OrderContentKind.ChargeItem))
         {
            //modified by zhouhui �˴��ı�����Ŀ����Ϊ��
            if (CoreBusinessLogic.BusinessLogic.WaringItem == null)
               return;

            int pos = CoreBusinessLogic.BusinessLogic.WaringItem.IndexOf(content.Item.Code);
            if ((pos >= 0)
               && ((CoreBusinessLogic.BusinessLogic.WaringItem.Length <= (pos + content.Item.Code.Length))
                  || (CoreBusinessLogic.BusinessLogic.WaringItem[pos + content.Item.Code.Length] == ',')))
            {
               if (CurrentPatient.PersonalInformation.CurrentAge <= CoreBusinessLogic.BusinessLogic.MaxWarningAge)
                  throw new DataCheckException(String.Format(ConstMessages.FormatOrderSaveWarning, CoreBusinessLogic.BusinessLogic.MaxWarningAge), ConstNames.ContentChargeItem, 1);
            }
         }
      }

      /// <summary>
      /// ���浱ǰ�༭�ĳ��׵���ϸ����
      /// </summary>
      public void SaveCurrentSuiteDetailData()
      {
         m_CoreLogic.SaveCurrentSuiteDetailData();
      }
      #endregion
   }
}
