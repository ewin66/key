using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DrectSoft.Common.Eop;
using Eop = DrectSoft.Common.Eop;
using DrectSoft.Common;
using System.Globalization;
using DrectSoft.Resources;
using DrectSoft.Wordbook;
using DevExpress.Utils;

namespace DrectSoft.Core.DoctorAdvice {
    /// <summary>
    /// ҽ�����ݱ༭��
    /// </summary>
    internal partial class OrderContentEditor : XtraUserControl {
        #region const
        /// <summary>
        /// ����ģʽ�µ���С���
        /// </summary>
        private const int MinCompactWidth = 570;
        /// <summary>
        /// ����ģʽ�µ���С���
        /// </summary>
        private const int MinNormalWidth = 770;
        #endregion

        #region properties
        /// <summary>
        /// ��ǰ�༭��ҽ�����ݶ���
        /// </summary>
        public OrderContent NewOrderContent {
            get {
                _tempContent = OrderContentFactory.CreateOrderContent(ContentKind, null);

                _tempContent.BeginInit();
                // �󶨴���������ݵ�ί��
                _tempContent.ProcessCreateOutputeInfo =
                   new OrderContent.GenerateOutputInfo(CustomDrawOperation.CreateOutputeInfo);

                // ��Ҫ����ҽ�����ݵ����ͱ�����Ӧ����
                _tempContent.Item = Item;
                _tempContent.ItemFrequency = Frequency;
                _tempContent.ItemUsage = Usage;
                _tempContent.Amount = Amount;
                _tempContent.CurrentUnit = Unit;

                // ��Ժ��ҩ�ĸ�����Ϣ
                if (OutDrugContent != null) {
                    OutDrugContent.ExecuteDays = ExecuteDays;
                    //OutDrugContent.TotalAmount = TotalAmount;
                    OutDrugContent.ReCalcTotalAmount();
                }
                else if (OperationContent != null) // ����ʱ��
            {
                    OperationContent.OperationTime = ExtraDatetime;
                    if (AnesthesiaOperation != null)
                        OperationContent.AnesthesiaOperation = AnesthesiaOperation;
                }
                else if (ShiftDeptContent != null) // ת�����ҺͲ���
            {
                    ShiftDeptContent.ShiftDept = ShiftDept;
                    ShiftDeptContent.ShiftWard = ShiftWard;
                }
                else if (LeaveHospitalContent != null) // ��Ժʱ��
            {
                    LeaveHospitalContent.LeaveHospitalTime = ExtraDatetime;
                }
                else {
                    _tempContent.EntrustContent = EntrustContent;
                }
                _tempContent.EndInit();

                if (ckSelfProvide.Visible) {
                    if (ckSelfProvide.Checked)
                        _tempContent.Attributes |= OrderAttributeFlag.Provide4Oneself;
                    else
                        _tempContent.Attributes &= (~OrderAttributeFlag.Provide4Oneself);
                }
                return _tempContent;
            }
        }

        /// <summary>
        /// ��Ժ��ҩִ����������ҽ��ִ������
        /// </summary>
        public int ExecuteDays {
            get {

                if (panelDays.Visible)
                    return Convert.ToInt32(edtDays.Value);
                else
                    return 0;
            }
            set {
                edtDays.Value = value;
            }
        }

        /// <summary>
        /// ҽ����ʼʱ��
        /// </summary>
        public DateTime StartDateTime {
            get {
                return dateEditStart.DateTime.Date + timeEditStart.Time.TimeOfDay;
            }
            set {
                dateEditStart.DateTime = value;
                timeEditStart.Time = value;
            }
        }

        /// <summary>
        /// ���ʵĳߴ�
        /// </summary>
        public Size SuitablySize {
            get {
                //int width;
                //int height;
                //if (m_CompactMode)
                //{
                //   //if (_useRadioCatalogInputStyle)
                //      width = panelItem.Width; // panelAmount.Width + panelUsage.Width + panelFrequency.Width
                //   //else
                //   //   width = panelCatalogInput.Width + panelItem.Width;
                //   height = panelTop.Height + panelClient.Height + panelClient2.Height + panelBottom.Height;
                //}
                //else
                //{
                //   width = panelClient.Width;
                //   height = panelTop.Height + panelClient.Height + panelBottom.Height;
                //}
                //return new Size(width, height);

                return new Size(SuitablyWidth, SuitablyHeight);
            }
        }

        /// <summary>
        /// ���ʵĿ��
        /// </summary>
        public int SuitablyWidth {
            get {
                if (m_CompactMode)
                    return MinCompactWidth; // panelCatalogInput.Width + panelItem.Width;
                else
                    return MinNormalWidth; // panelTop.Width;
            }
        }

        /// <summary>
        /// ���ʵĸ߶�
        /// </summary>
        public int SuitablyHeight {
            get {
                if (m_CompactMode)
                    return panelTop.Height + panelClient.Height + panelClient2.Height + panelBottom.Height;
                else
                    return panelTop.Height + panelClient.Height + panelBottom.Height;
            }
        }

        /// <summary>
        /// ��ǵ�ǰ�����޸����ݻ�������ҽ��
        /// </summary>
        public bool IsModifyData {
            get { return _isModifyData; }
            set {
                _isModifyData = value;
                btnInsert.Enabled = !value;
                if (value)
                    FireStartEdit(new DataCommitArgs(DataCommitType.Modify));
                else
                    FireStartEdit(new DataCommitArgs(DataCommitType.Add));

                //SetShowFormImmediatelyProperty(value);
            }
        }
        private bool _isModifyData;

        /// <summary>
        /// ҽ���������ģʽ��Fasle: ʹ��LookUpEditor  True: ʹ�õ�ѡ��ģʽ
        /// </summary>
        public bool UseRadioCatalogInputStyle {
            get { return _useRadioCatalogInputStyle; }
            set {
                if (_useRadioCatalogInputStyle != value) {
                    _useRadioCatalogInputStyle = value;
                    ChangeCatalogInputStyle();
                    Size = SuitablySize;
                }
            }
        }
        private bool _useRadioCatalogInputStyle;
        #endregion

        #region private properties
        /// <summary>
        /// ��Ժ��ҩҽ�����ݶ���(�ṩ�����ԣ��������ݷ���)
        /// </summary>
        private OutDruggeryContent OutDrugContent {
            get { return _tempContent as OutDruggeryContent; }
        }

        /// <summary>
        /// ����ҽ�����ݶ���(�ṩ�����ԣ��������ݷ���)
        /// </summary>
        private OperationOrderContent OperationContent {
            get { return _tempContent as OperationOrderContent; }
        }

        /// <summary>
        /// ת��ҽ�����ݶ���(�ṩ�����ԣ��������ݷ���)
        /// </summary>
        private ShiftDeptOrderContent ShiftDeptContent {
            get { return _tempContent as ShiftDeptOrderContent; }
        }

        /// <summary>
        /// ��Ժҽ�����ݶ���(�ṩ�����ԣ��������ݷ���)
        /// </summary>
        private LeaveHospitalOrderContent LeaveHospitalContent {
            get { return _tempContent as LeaveHospitalOrderContent; }
        }

        /// <summary>
        /// ҽ�����
        /// </summary>
        private OrderContentKind ContentKind {
            get {
                if (UseRadioCatalogInputStyle) {
                    if (ckEdtDruggery.Checked)
                        return OrderContentKind.Druggery;
                    else if (ckEdtNormalItem.Checked)
                        return OrderContentKind.ChargeItem;
                    else if (ckEdtGeneralItem.Checked)
                        return OrderContentKind.GeneralItem;
                    else if (ckEdtOperate.Checked)
                        return OrderContentKind.Operation;
                    else if (ckEdtText.Checked)
                        return OrderContentKind.TextNormal;
                    else if (ckEdtShiftDept.Checked)
                        return OrderContentKind.TextShiftDept;
                    else if (ckEdtLeaveHosp.Checked)
                        return OrderContentKind.TextLeaveHospital;
                    else if (ckEdtOutDruggery.Checked)
                        return OrderContentKind.OutDruggery;
                }
                else {
                    if (catalogInput.HadGetValue)
                        return (OrderContentKind)Convert.ToInt32(catalogInput.CodeValue);
                }

                return OrderContentKind.Druggery; //Ĭ��ΪҩƷ���Ա��ں��水ҩƷҽ�����д���
            }
            set {
                if (catalogInput.SqlWordbook != null) {
                    catalogInput.CodeValue = value.ToString("D");
                    switch (value) {
                        case OrderContentKind.Druggery:
                            ckEdtDruggery.Checked = true;
                            break;
                        case OrderContentKind.ChargeItem:
                            ckEdtNormalItem.Checked = true;
                            break;
                        case OrderContentKind.GeneralItem:
                            ckEdtGeneralItem.Checked = true;
                            break;
                        case OrderContentKind.Operation:
                            ckEdtOperate.Checked = true;
                            break;
                        case OrderContentKind.TextNormal:
                            ckEdtText.Checked = true;
                            break;
                        case OrderContentKind.TextShiftDept:
                            ckEdtShiftDept.Checked = true;
                            break;
                        case OrderContentKind.TextLeaveHospital:
                            ckEdtLeaveHosp.Checked = true;
                            break;
                        case OrderContentKind.OutDruggery:
                            ckEdtOutDruggery.Checked = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// ҽ����Ŀ
        /// </summary>
        private ItemBase Item {
            get {
                if (((m_EnableFlags & EditorEnableFlag.CanChoiceItem) > 0)
                   && itemInput.HadGetValue && (itemInput.PersistentObjects.Count > 0))
                    return itemInput.PersistentObjects[0] as ItemBase;
                else
                    return null;
            }
        }

        /// <summary>
        /// ҽ��Ƶ��
        /// </summary>
        private OrderFrequency Frequency {
            get {
                // ��ʱҽ���г���Ժ��ҩ��Ƶ�ζ���Ĭ��ΪST
                if ((m_UILogic.IsTempOrder && (ContentKind != OrderContentKind.OutDruggery))
                      || ((m_EnableFlags & EditorEnableFlag.CanChoiceFrequency) > 0))
                    return frequencyInputor.Frequency;
                else
                    return null;
            }
        }

        /// <summary>
        /// ҽ���÷�
        /// </summary>
        private OrderUsage Usage {
            get {
                if (((m_EnableFlags & EditorEnableFlag.CanChoiceUsage) > 0)
                   && usageInput.HadGetValue && (usageInput.PersistentObjects.Count > 0))
                    return usageInput.PersistentObjects[0] as OrderUsage;
                else
                    return null;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private decimal Amount {
            get {
                return Convert.ToDecimal(edtAmount.Value);
            }
        }

        /// <summary>
        /// ������λ
        /// </summary>
        private ItemUnit Unit {
            get {
                if (selectUnit.SelectedItem != null)
                    return (ItemUnit)selectUnit.SelectedItem;
                else
                    return new ItemUnit();
            }
        }

        ///// <summary>
        ///// ��Ժ��ҩ��������
        ///// </summary>
        //private decimal TotalAmount
        //{
        //   get
        //   {
        //      return Amount * Frequency.ExecuteTimesPerDay * ExecuteDays;
        //   }
        //}

        /// <summary>
        /// ��ȡ�����ĸ���ʱ����Ϣ�������������Ժʱ�䣩
        /// </summary>
        private DateTime ExtraDatetime {
            get {
                if (((m_EnableFlags & EditorEnableFlag.NeedOperationInfo) > 0)
                   || ((m_EnableFlags & EditorEnableFlag.NeedLeaveHospitalTime) > 0))
                    return dateEditExtra.DateTime.Date + timeEditExtra.Time.TimeOfDay;
                else
                    return DateTime.MinValue;
            }
        }

        /// <summary>
        /// ת�ƿ���
        /// </summary>
        private Eop.Department ShiftDept {
            get {
                if (((m_EnableFlags & EditorEnableFlag.NeedShiftDeptInfo) > 0)
                   && deptInput.HadGetValue && (deptInput.PersistentObjects.Count > 0))
                    return deptInput.PersistentObjects[0] as Eop.Department;
                else
                    return null;
            }
        }

        /// <summary>
        /// ת�Ʋ���
        /// </summary>
        private Eop.Ward ShiftWard {
            get {
                if (((m_EnableFlags & EditorEnableFlag.NeedShiftDeptInfo) > 0)
                   && wardInput.HadGetValue && (wardInput.PersistentObjects.Count > 0))
                    return wardInput.PersistentObjects[0] as Eop.Ward;
                else
                    return null;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        private Eop.Anesthesia AnesthesiaOperation {
            get {
                if (((m_EnableFlags & EditorEnableFlag.NeedOperationInfo) > 0)
                   && anesthesiaInput.HadGetValue && (anesthesiaInput.PersistentObjects.Count > 0))
                    return anesthesiaInput.PersistentObjects[0] as Eop.Anesthesia;
                else
                    return null;
            }
        }

        /// <summary>
        /// ���У�������������Ժ��ת����Ч��
        /// </summary>
        private string EntrustContent {
            get {
                if ((m_EnableFlags & EditorEnableFlag.CanInputEntrust) > 0)
                    return edtEntrustContent.Text;
                else
                    return "";
            }
        }
        #endregion

        #region private variables
        private UILogic m_UILogic;
        /// <summary>
        /// �༭���Ƿ���ñ�־
        /// </summary>
        private EditorEnableFlag m_EnableFlags;

        private IDataAccess m_SqlExecutor;

        /// <summary>
        /// �������ⲿ�����ҽ�����ݶ���
        /// </summary>
        private OrderContent _tempContent;

        private Druggery m_AllergicDrug;

        private SkinTestInputForm m_SkinTestForm;

        /// <summary>
        /// ����Ψһ�ɹ�ѡ���Ƶ�Σ�Ϊ�����ʾ�ж��Ƶ�οɹ�ѡ��
        /// </summary>
        private string m_OnlyFrequency;

        /// <summary>
        /// ����ģʽ
        /// </summary>
        private bool m_CompactMode;
        #endregion

        #region ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="useCompactMode">�Ƿ�ʹ�ý���ģʽ</param>
        public OrderContentEditor(bool useCompactMode) {
            InitializeComponent();


            //btnNew.Image = ResourceManager.GetSmallIcon(ResourceNames.NewDocument, IconType.Normal);
            //btnOk.Image = ResourceManager.GetSmallIcon(ResourceNames.Ok, IconType.Normal);
            //btnInsert.Image = ResourceManager.GetSmallIcon(ResourceNames.AddToList, IconType.Normal);

            m_SkinTestForm = new SkinTestInputForm();
            m_SkinTestForm.HadMadeChoice += new EventHandler(DoAfterChoiceSkinTestResult);

            itemInput.EnterMoveNextControl = false; // ѡ����Ŀ���پ����Ƿ��Զ���ת����һ�ؼ�

            m_CompactMode = useCompactMode;
            InitializePanelsOfClient();

            btnCommit.Size = new Size(0, 0);
            Size = SuitablySize;
        }
        #endregion

        #region interface methods
        /// <summary>
        /// ��ʼ��ҽ�����ݱ༭��
        /// </summary>
        /// <param name="logicHandle">�߼��������</param>
        /// <param name="sqlExecutor">���ݷ��ʶ���</param>
        public void InitializeEditor(UILogic logicHandle, IDataAccess sqlExecutor) {
            if (logicHandle == null)
                throw new ArgumentNullException(String.Format(ConstMessages.ExceptionFormatNullObject, ConstNames.OrderUILogic));
            m_UILogic = logicHandle;
            m_SqlExecutor = sqlExecutor;
            showListWindow1.SqlHelper = sqlExecutor;

            this.usageInput.NormalWordbook = new DrectSoft.Wordbook.OrderUsageBook("ID//Py//Wb", 0, "��¼״̬//1", "", 0);
            this.wardInput.NormalWordbook = new DrectSoft.Wordbook.WardBook("ID//Py//Wb", 0, "��¼״̬//1", "", 0);
            this.deptInput.NormalWordbook = new DrectSoft.Wordbook.DepartmentBook("ID//Py//Wb", 0, "��¼״̬//1//�������//101", "", 0);
            this.anesthesiaInput.NormalWordbook = new DrectSoft.Wordbook.AnesthesiaBook("ID//Py//Wb//Name", 0, "��¼״̬//1", "", 0);

            deptInput.NormalWordbook.Parameters[ConstSchemaNames.DeptWardMapColExceptDept].Enabled = true;
            deptInput.NormalWordbook.Parameters[ConstSchemaNames.DeptWardMapColExceptDept].Value = logicHandle.DeptCode;

            wardInput.NormalWordbook.Parameters[ConstSchemaNames.DeptWardMapColExceptWard].Enabled = true;
            wardInput.NormalWordbook.Parameters[ConstSchemaNames.DeptWardMapColExceptWard].Value = logicHandle.WardCode;
        }

        /// <summary>
        /// ����ҽ������ѡ���Ĭ��Ƶ��ѡ������ݡ�
        /// �ڶ��ڵ�ҽ������Դ�����仯������Ҫ���������߼�(�����Ժ��ҩ)ʱ����
        /// </summary>
        public void ResetDataOfWordbook() {
            // ����ҽ���������ѡ���б���ֵ���
            catalogInput.SqlWordbook = m_UILogic.CatalogWordbook;
            ResetCatalogChoiceGroup();

            // ��Ϊҽ��Ƶ����ҽ�������йأ��������߼���������ṩ
            frequencyInputor.FrequencyBook = m_UILogic.FrequencyWordbook;
            if (m_UILogic.IsTempOrder) {
                string[] defFrequency = showListWindow1.ValidateWordbookHasOneRecord(frequencyInputor.FrequencyBook, WordbookKind.Normal);
                if (defFrequency != null)
                    m_OnlyFrequency = defFrequency[0];
                else
                    m_OnlyFrequency = "";
            }
            else
                m_OnlyFrequency = "";
        }

        /// <summary>
        /// �ô���ı�����ʼ��ҽ�����ݱ༭����
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="content"></param>
        public void InitializeDefaultValue(DateTime startDateTime, OrderContent content) {
            if (startDateTime == DateTime.MinValue)
                StartDateTime = m_UILogic.DefaultStartDateTime;
            else
                StartDateTime = startDateTime;

            _tempContent = content;
            if (content == null) {
                if (catalogInput.HadGetValue) // ԭ�����з�������
            {
                    string catalogCode = catalogInput.CodeValue.ToString();
                    ClearComponentsValue();
                    //catalogInput.CodeValue = catalogCode;
                    ContentKind = (OrderContentKind)Convert.ToInt32(catalogCode);
                    if (itemInput.CanFocus)
                        itemInput.Focus();
                }
                else {
                    //catalogInput.CodeValue = OrderContentKind.Druggery.ToString("D");
                    ContentKind = OrderContentKind.Druggery;
                    if (catalogInput.CanFocus)
                        catalogInput.Focus();
                }
            }
            else {
                // ��ҽ���������Ӧ���Ը�ֵ
                //catalogInput.CodeValue = content.OrderKind.ToString("D");
                ContentKind = content.OrderKind;

                if (content.Item != null)
                    itemInput.CodeValue = content.Item.KeyValue;
                else
                    itemInput.CodeValue = null;

                edtAmount.Value = content.Amount;
                if (content.CurrentUnit.Name != null)
                    selectUnit.SelectedIndex = selectUnit.Properties.Items.IndexOf(
                       content.CurrentUnit);
                else
                    selectUnit.SelectedIndex = -1;

                if (OutDrugContent != null) // ��Ժ��ҩҪ�������� 
                    edtDays.Value = OutDrugContent.ExecuteDays;
                else if (OperationContent != null) {
                    DateTime opDateTime = OperationContent.OperationTime;
                    dateEditExtra.DateTime = opDateTime.Date;
                    timeEditExtra.Time = opDateTime.Date;
                    anesthesiaInput.CodeValue = OperationContent.AnesthesiaOperation.Code;
                }
                else if (LeaveHospitalContent != null) {
                    DateTime leaveTime = LeaveHospitalContent.LeaveHospitalTime;
                    dateEditExtra.DateTime = leaveTime.Date;
                    timeEditExtra.Time = leaveTime.Date;
                }
                else if (ShiftDeptContent != null) // ת��Ҫ����ת���Ŀ��ҡ�����
            {
                    deptInput.CodeValue = ShiftDeptContent.ShiftDept.Code;
                    wardInput.CodeValue = ShiftDeptContent.ShiftWard.Code;
                }

                edtEntrustContent.Text = content.EntrustContent;
                // �÷���Ƶ��һ��Ҫ����Ŀ��ֵ����У����ⱻ��Ŀ��Ĭ�����ó��
                if (content.ItemUsage != null)
                    usageInput.CodeValue = content.ItemUsage.Code;
                else
                    usageInput.CodeValue = "";

                frequencyInputor.Frequency = content.ItemFrequency;
                //_tempContent.ItemFrequency = content.ItemFrequency.Clone();

                ckSelfProvide.Checked = ((content.Attributes & OrderAttributeFlag.Provide4Oneself) > 0);

                itemInput.Focus();
            }
            IsModifyData = false;
            SetShowFormImmediatelyProperty(false);
        }

        /// <summary>
        /// �༭ָ��ҽ�������ݻ���ָ��ҽ������Ϊ�����༭����ҽ��������
        /// </summary>
        /// <param name="startDateTime">��ʼʱ��</param>
        /// <param name="content">ҽ������</param>
        public void EditContent(DateTime startDateTime, OrderContent content) {
            InitializeDefaultValue(startDateTime, content);
            IsModifyData = true;
            SetShowFormImmediatelyProperty(true);
        }

        /// <summary>
        /// �༭ָ��ҽ�������ݻ���ָ��ҽ������Ϊ�����༭����ҽ��������
        /// </summary>
        /// <param name="order">ҽ������ʵ��</param>
        /// <param name="isTempOrder">ture:��ʱҽ����false:����ҽ��</param>
        public void EditContent(Order order, bool isTempOrder) {
            InitializeDefaultValue(order.StartDateTime, order.Content);
            if (!isTempOrder) {
                LongOrder longOrder = order as LongOrder;
                if ((longOrder.CeaseInfo != null) && (longOrder.CeaseInfo.HadInitialized)) {
                    TimeSpan days = longOrder.CeaseInfo.ExecuteTime - longOrder.StartDateTime;
                    ExecuteDays = (int)days.TotalDays;
                }
            }
            IsModifyData = true;
            SetShowFormImmediatelyProperty(true);
        }

        /// <summary>
        /// ��ҽ�����ݱ༭�������Form�ػ�Ĺ��ܼ����Դ����ض����¼�
        /// </summary>
        /// <param name="e"></param>
        public void AcceptFunctionKeyPress(KeyEventArgs e) {
            if ((!e.Control) && (!e.Shift)) {
                if (e.KeyCode == Keys.F5)
                    btnOk_Click(this, new EventArgs());
                else if (e.KeyCode == Keys.F6) {
                    if (btnInsert.Enabled)
                        btnInsert_Click(this, new EventArgs());
                }
            }
        }

        //public void ResetControlFont(Font newFont)
        //{
        //   Font = newFont;
        //   styleController.Appearance.Font = newFont;
        //   styleController.AppearanceDisabled.Font = newFont;
        //   styleController.AppearanceDropDown.Font = newFont;
        //   styleController.AppearanceDropDownHeader.Font = newFont;
        //   styleController.AppearanceFocused.Font = newFont;
        //   groupControl1.Appearance.Font = newFont;
        //   groupControl1.AppearanceCaption.Font = newFont;
        //   groupControl2.Appearance.Font = newFont;
        //   groupControl2.AppearanceCaption.Font = newFont;
        //   catalogInput.Font = newFont;
        //   itemInput.Font = newFont;
        //   usageInput.Font = newFont;
        //   frequencyInput.Font = newFont;
        //   deptInput.Font = newFont;
        //   wardInput.Font = newFont;
        //   anesthesiaInput.Font = newFont;

        //   Width = panelItem.Width;
        //}
        #endregion

        #region custom event handler
        /// <summary>
        /// ���ݱ༭��ʼ�¼�
        /// </summary>
        public event EventHandler<DataCommitArgs> StartEdit {
            add {
                onStartEdit = (EventHandler<DataCommitArgs>)Delegate.Combine(onStartEdit, value);
            }
            remove {
                onStartEdit = (EventHandler<DataCommitArgs>)Delegate.Remove(onStartEdit, value);
            }
        }
        private EventHandler<DataCommitArgs> onStartEdit;

        private void FireStartEdit(DataCommitArgs e) {
            if (onStartEdit != null)
                onStartEdit(this, e);
        }

        /// <summary>
        /// ���ݱ༭����¼�
        /// </summary>
        public event EventHandler<DataCommitArgs> EditFinished {
            add {
                onEditFinished = (EventHandler<DataCommitArgs>)Delegate.Combine(onEditFinished, value);
            }
            remove {
                onEditFinished = (EventHandler<DataCommitArgs>)Delegate.Remove(onEditFinished, value);
            }
        }
        private EventHandler<DataCommitArgs> onEditFinished;

        private void FireDataEditFinished(DataCommitArgs e) {
            FireSelectedItemChanged(false);
            if (onEditFinished != null)
                onEditFinished(this, e);
        }

        /// <summary>
        /// ѡ�е���Ŀ�����ı��¼�
        /// </summary>
        public event EventHandler<OrderItemArgs> SelectedItemChanged {
            add {
                onSelectedItemChanged = (EventHandler<OrderItemArgs>)Delegate.Combine(onSelectedItemChanged, value);
            }
            remove {
                onSelectedItemChanged = (EventHandler<OrderItemArgs>)Delegate.Remove(onSelectedItemChanged, value);
            }
        }
        private EventHandler<OrderItemArgs> onSelectedItemChanged;

        private void FireSelectedItemChanged(bool selectedItem) {
            if ((onSelectedItemChanged != null) && (Item != null)) {
                string usage;
                if (Usage != null)
                    usage = Usage.Name;
                else
                    usage = "";

                onSelectedItemChanged(this, new OrderItemArgs(selectedItem, Item.Kind, Item.KeyValue, Item.Name, Unit.Name, usage));
            }
        }
        #endregion

        #region private methods
        private void InitializePanelsOfClient() {
            // ����Panel������
            panelTop.Height = panelAmount.Height;
            panelClient.Height = panelTop.Height;
            panelClient2.Height = panelClient.Height;

            panelAmount.Dock = DockStyle.Left;
            panelUsage.Dock = DockStyle.Left;
            panelFrequency.Dock = DockStyle.Left;
            panelDays.Dock = DockStyle.Left;
            panelExtraTime.Dock = DockStyle.Left;
            panelShiftDept.Dock = DockStyle.Left;
            panelOperation.Dock = DockStyle.Left;
            panelEntrust.Dock = DockStyle.Fill;

            //panelTop.Location = new Point(0, 0);
            //panelClient.Location = new Point(0, panelTop.Height);
            //panelClient2.Location = new Point(0, panelTop.Height * 2);

            panelCatalogChoice.Location = new Point(0, 0);
            panelCatalogChoice.Width = panelTop.Width - panelButton.Width;

            if (m_CompactMode) {
                panelStartTime.Visible = false;
                panelClient2.Controls.Add(panelEntrust);
                panelClient2.Controls.Add(panelDays);
                panelBottom.Height = panelCatalogChoice.Height + panelButton.Height;
                panelButton.Location = new Point(MinCompactWidth - panelButton.Width //panelCatalogInput.Width + panelItem.Width - panelButton.Width
                   , panelCatalogChoice.Height);
            }
            else {
                panelBottom.Height = panelButton.Height;
                panelButton.Location = new Point(panelCatalogChoice.Width, 0);
                panelClient2.Visible = false;
            }
        }

        /// <summary>
        /// ��ս���ؼ���ֵ(��ҽ���������ѡ���)
        /// </summary>
        private void ClearComponentsValue() {
            itemInput.CodeValue = null;
            // --����������������itemInput�ĸı��¼��лᴦ��
            //edtPrice.Text = "";
            //edtSpecification.Text = "";
            //edtAmount.Value = 1;
            //selectUnit.Properties.Items.Clear();
            //usageInput.CodeValue = null;
            //frequencyInput.CodeValue = null;
            //edtDays.Value = 1;

            //edtTotal.Text = ""; // --�����������ݻ��Զ�����

            deptInput.CodeValue = "";
            wardInput.CodeValue = "";
            dateEditExtra.DateTime = DateTime.Now;
            timeEditExtra.Time = DateTime.Now;

            edtEntrustContent.Text = "";
            ckSelfProvide.Checked = false;
        }

        private void ChangeCatalogInputStyle() {
            panelCatalogChoice.Visible = UseRadioCatalogInputStyle;
            panelCatalogInput.Visible = !UseRadioCatalogInputStyle;

            // ��Ҫ�ֹ�������Ŀ��Ŀ��
            int plugWidth;

            if (UseRadioCatalogInputStyle)
                plugWidth = panelCatalogInput.Width;
            else
                plugWidth = -panelCatalogInput.Width;
            itemInput.Width += plugWidth;
            edtItemMemo.Location = new Point(edtItemMemo.Location.X + plugWidth, edtItemMemo.Location.Y);
        }

        private void ResetCatalogChoiceGroup() {
            DataTable catalogTable = catalogInput.SqlWordbook.BookData;
            catalogTable.DefaultView.RowFilter = catalogInput.SqlWordbook.ExtraCondition;

            foreach (Control ctrl in panelCatalogChoice.Controls)
                ctrl.Visible = false;

            OrderContentKind kind;
            foreach (DataRowView view in catalogTable.DefaultView) {
                kind = (OrderContentKind)Convert.ToInt32(view[ConstSchemaNames.ContentCatalogColId]);
                switch (kind) {
                    case OrderContentKind.Druggery:
                        ckEdtDruggery.Visible = true;
                        break;
                    case OrderContentKind.ChargeItem:
                        ckEdtNormalItem.Visible = true;
                        break;
                    case OrderContentKind.GeneralItem:
                        ckEdtGeneralItem.Visible = true;
                        break;
                    case OrderContentKind.Operation:
                        ckEdtOperate.Visible = true;
                        break;
                    case OrderContentKind.TextNormal:
                        ckEdtText.Visible = true;
                        break;
                    case OrderContentKind.TextShiftDept:
                        ckEdtShiftDept.Visible = true;
                        break;
                    case OrderContentKind.TextLeaveHospital:
                        ckEdtLeaveHosp.Visible = true;
                        break;
                    case OrderContentKind.OutDruggery:
                        ckEdtOutDruggery.Visible = true;
                        break;
                }
            }

            // Ĭ��ѡ�е�һ�����
            (panelCatalogChoice.Controls[0] as CheckEdit).Checked = true;
        }

        private void SetVisiableAndEnable() {
            panelItem.Visible = ((m_EnableFlags & EditorEnableFlag.CanChoiceItem) > 0);
            panelAmount.Visible = ((m_EnableFlags & EditorEnableFlag.NeedInputAmount) > 0);
            panelUsage.Visible = ((m_EnableFlags & EditorEnableFlag.CanChoiceUsage) > 0);
            panelFrequency.Visible = ((m_EnableFlags & EditorEnableFlag.CanChoiceFrequency) > 0);
            if (panelFrequency.Visible)
                panelFrequency.Enabled = (String.IsNullOrEmpty(m_OnlyFrequency));

            panelDays.Visible = (((m_EnableFlags & EditorEnableFlag.CanSetExecuteDays) > 0)
               && ((!m_UILogic.IsTempOrder) || ((m_EnableFlags & EditorEnableFlag.NeedOutDruggeryInfo) > 0)));

            if ((m_EnableFlags & EditorEnableFlag.NeedOutDruggeryInfo) > 0) {
                panelTotal.Visible = true;

            }
            else {
                panelTotal.Visible = false;
            }

            panelExtraTime.Visible = false;
            if ((m_EnableFlags & EditorEnableFlag.NeedShiftDeptInfo) > 0) {
                panelShiftDept.Visible = true;
                labelExtraTime.Text = ConstNames.TimeOfShiftDept;
            }
            else
                panelShiftDept.Visible = false;
            if ((m_EnableFlags & EditorEnableFlag.NeedOperationInfo) > 0) {
                panelOperation.Visible = true;
                panelExtraTime.Visible = true;
                labelExtraTime.Text = ConstNames.TimeOfOperation;
            }
            else
                panelOperation.Visible = false;
            if ((m_EnableFlags & EditorEnableFlag.NeedLeaveHospitalTime) > 0) {
                panelExtraTime.Visible = true;
                labelExtraTime.Text = ConstNames.TimeOfOutHospital;
            }

            panelEntrust.Visible = ((m_EnableFlags & EditorEnableFlag.CanInputEntrust) > 0);

            ckSelfProvide.Visible = (((ContentKind == OrderContentKind.Druggery) || (ContentKind == OrderContentKind.OutDruggery))
               && (!m_CompactMode));
            ckSelfProvide.Checked = false;
        }

        private void SetShowFormImmediatelyProperty(bool isEdit) {
            //catalogInput.ShowFormImmediately = !isEdit;
            //itemInput.ShowFormImmediately = !isEdit;
            usageInput.ShowFormImmediately = !isEdit;
            frequencyInputor.ShowFormImmediately = !isEdit;
            deptInput.ShowFormImmediately = !isEdit;
            wardInput.ShowFormImmediately = !isEdit;
        }

        /// <summary>
        /// ҽ���������ı��Ĵ�����
        /// </summary>
        private void SetValueAfterCatalogChanged() {
            // ���ı��Ҫ����������飺
            //    ������Ŀѡ��ؼ����ֵ���
            //    ������ʾ����
            //    �ʵ������ÿؼ�Ĭ��ֵ
            itemInput.NormalWordbook = m_UILogic.GetItemWordbook(ContentKind);
            m_EnableFlags = UILogic.GetContentEditorEnableStatus(ContentKind);

            SetVisiableAndEnable();

            ClearComponentsValue();
            if (ContentKind == OrderContentKind.TextLeaveHospital) {
                dateEditExtra.DateTime = m_UILogic.DefaultLeaveHospitalTime;
                timeEditExtra.Time = m_UILogic.DefaultLeaveHospitalTime;
            }
            // ��������ҽ��ʱ��һ��Ҫѡ��Ŀ
            if (ContentKind == OrderContentKind.TextNormal)
                itemInput.MinCount = 0;
            else
                itemInput.MinCount = 1;

            if (ContentKind == OrderContentKind.OutDruggery)
                edtDays.Value = 1;
            else
                edtDays.Value = 0; // ���ִ������
        }

        /// <summary>
        /// ѡ����Ŀ�ı��������ؿؼ���ֵ
        /// </summary>
        private void SetValueAfterItemChanged() {
            // ѡ����Ŀ�������¹�����
            //    ��ʼ����񡢵��ۡ���λ�б�(��С��λ�������ҩƷ�����ù���סԺ��λ)
            //    ҩƷ��һ����Ŀ���ٴ���Ŀ������һ�ε�Ƶ�Σ������ҩƷ�������÷�
            //    ����ҩƷ�������Ĭ���÷�/Ƶ��/����/��λ/��������ֵ����Ӧ�ؼ�(�����滻���е�Ƶ�Ρ��÷�)
            //    ����ҩƷҪ���ÿ�ѡ���÷���Χ

            // ��Ҫ��������
            edtAmount.Value = 1; // ����Ĭ��Ϊ1
            // edtDays.Value = 1; ִ����������һ������һ��
            selectUnit.Properties.Items.Clear();
            selectUnit2.Properties.Items.Clear();

            switch (ContentKind) {
                case OrderContentKind.Druggery:
                case OrderContentKind.OutDruggery:
                    break;
                case OrderContentKind.ChargeItem:
                case OrderContentKind.ClinicItem:
                    usageInput.CodeValue = null;
                    break;
                default:
                    usageInput.CodeValue = null;
                    frequencyInputor.Frequency = null;
                    break;
            }

            if (!String.IsNullOrEmpty(m_OnlyFrequency))
                frequencyInputor.SetFrequency(m_OnlyFrequency);
            else if ((m_UILogic.IsTempOrder) && (ContentKind != OrderContentKind.OutDruggery))
                frequencyInputor.SetFrequency(CoreBusinessLogic.BusinessLogic.TempOrderFrequencyCode);

            // δѡ����Ŀ����������ۺ͹�����ʾ����
            if (Item == null) {
                edtItemMemo.Text = "";
                FireSelectedItemChanged(false);
            }
            else {
                // ������ѡ����Ŀ��Ĵ���
                edtItemMemo.Text = Item.Price + " " + Item.Specification;

                // ��ӵ�λ�б�
                // �����ҩƷ��Ŀ�����������������
                Druggery drug = Item as Druggery;
                if (drug == null) {
                    selectUnit.Properties.Items.Add(Item.BaseUnit);
                    // ������÷�������
                    usageInput.NormalWordbook.ExtraCondition = "";
                }
                else {
                    // ����Ҫȡ��ҩƷ��Ĭ���÷�����Ϣ����Ϊ���ڵ�ģʽ�ǲ��Զ���DB�л�ȡδ��ֵ�����ݣ�
                    drug.InitializeDefaultUsageRange(m_SqlExecutor);

                    // ������λ,��סԺ��λΪ�գ��򲻽�����뵽��λ�б���
                    //modified by zhouhui  �޸�ĳЩ�����û��Ĭ�ϵ�λ��bug
                    if (!string.IsNullOrEmpty(drug.SpecUnit.Name))
                        selectUnit.Properties.Items.Add(drug.SpecUnit);

                    if (!string.IsNullOrEmpty(drug.WardUnit.Name)) {
                        selectUnit.Properties.Items.Add(drug.WardUnit);
                        selectUnit2.Properties.Items.Add(drug.WardUnit);
                        selectUnit2.SelectedIndex = 0;
                    }

                    // ���ݼ����÷���Ӧ�����÷���ѡ��Χ
                    usageInput.NormalWordbook.ExtraCondition =
                       drug.DefaultUsageRangeCondition;

                    // ��鵱ǰ�������÷��Ƿ����÷�ѡ��Χ�ڣ�ͨ�����¸��������Ը�ֵ�ķ���ʵ�֣�
                    usageInput.CodeValue = usageInput.CodeValue;

                    // �������Ĭ�ϵ��÷���Ƶ�Ρ������ȣ���û�б������÷���Ƶ�Σ�����Ĭ��ֵ���ؼ�
                    // ����Ĭ��ȡסԺϵͳ��Ĭ�����ã��Ժ�Ҫ���ݵ�ǰʹ�õ������ﻹ��סԺ����������
                    DruggeryOrderContent defaultContent = drug.GetDefaultUsageFrequency(
                       SystemApplyRange.InpatientDept, m_SqlExecutor);

                    if (defaultContent.Amount > 0) // ��Ĭ���������ж��Ƿ���Ĭ�ϵ��÷���Ӧ����
               {
                        // Ĭ�������ĵ�λ����С��λ��Ҫ���任��ɵ�ǰ��λ������
                        edtAmount.Value = ((ItemUnit)selectUnit.Properties.Items[0]).Convert2CurrentUnit(
                           defaultContent.Amount);

                        // ����Ĭ���÷�
                        if (!usageInput.HadGetValue)
                            usageInput.CodeValue = defaultContent.ItemUsage.Code;
                        // ����Ĭ��Ƶ��
                        if (!frequencyInputor.HadGetValue)
                            frequencyInputor.SetFrequency(defaultContent.ItemFrequency.Code);
                    }
                }
                selectUnit.SelectedIndex = 0; // Ĭ����ʾ��һ��λ��ҩƷ��Ĭ�ϵ�λ��Ҫ�������� ������
                FireSelectedItemChanged(true);
            }
        }

        ///// <summary>
        ///// Ƶ�θı��������ؿؼ�����
        ///// </summary>
        //private void SetValueAfterFrequencyChanged()
        //{
        //   // ѡ��Ƶ�κ󣬳�ʼ����ҩʱ��ѡ���
        //   SetFrequencyDetail();
        //   // ��Ժ��ҩʱ���������/Ƶ��/�����и��£���Ҫˢ��ƽ��ÿ������
        //   ResetOutDruggeryTotalAmout();
        //   // ��Ժ��ҩʱ���������/Ƶ��/�����и��£���Ҫˢ�´�ҩ����
        //   ResetOutDrugTotalAmount();
        //}

        /// <summary>
        /// �����Ժ��ҩƽ��ÿ����������ʾ
        /// </summary>
        private void ResetOutDruggeryTotalAmout() {
            if (ContentKind == OrderContentKind.OutDruggery) {
                Druggery drug = Item as Druggery;
                if (drug != null) {
                    // ���ƽ��ÿ��Ҫ�Ե�������������ﵥλ��
                    decimal total = CalcTotalAmountOfAvgDay();
                    total = drug.PoliclinicUnit.Convert2CurrentUnit(Unit.Convert2BaseUnit(total));
                    int newTotal; // ����ֵ����ȡ�����Ƿ�Ҫ��������������������
                    newTotal = Decimal.ToInt32(total);
                    if (newTotal < total)
                        newTotal += 1;
                    edtTotal.Text = newTotal.ToString(CultureInfo.CurrentCulture) + Unit.Name;
                }
                else
                    edtTotal.Text = "";
            }
        }

        /// <summary>
        /// �����Ժ��ҩƽ��ÿ������(����ڵ�ǰѡ��ļ�����λ)
        /// </summary>
        private decimal CalcTotalAmountOfAvgDay() {
            // ��������*Ƶ�μ���õ����Ժ��ټ����ҩ�����Ĵ���������
            return Amount * Frequency.ExecuteTimesPerDay;
        }

        private void CheckNeedSkinTestAfterSelectItem() {
            // ���ѡ�����ҩƷ�����������Ϣ
            //   �Ƿ��ǹ���ҩƷ
            //   �������HIS�����������¼���Ƿ�����Ӧ��¼�����ޣ�����ʾҪ��Ƥ�ԣ���������ʾ��ȡ�����룬����������ͨ��
            //   �������HIS������ʾ���������Ϣ��ֻ��ѡ�����Բ�ȷ�ϲ�����ͨ��
            if (Item != null)
            //&& ((Item.Kind == ItemKind.WesternMedicine) || (Item.Kind == ItemKind.PatentMedicine) || (Item.Kind == ItemKind.HerbalMedicine)))
         {
                Druggery drug = Item as Druggery;
                if ((drug != null)
                   && ((drug.Attributes == DruggeryAttributeFlag.SkinTestLimited) || (drug.Attributes == DruggeryAttributeFlag.SkinTestUnlimited))) {
                    DataRow[] rows = m_UILogic.SkinTestResultTable.Select(ConstSchemaNames.SkinTestColSpecSerialNo + " = " + drug.SpecSerialNo);
                    if ((rows == null) || (rows.Length == 0) || (rows[0][ConstSchemaNames.SkinTestColFlag].ToString() == ConstNames.LightDemanding)) {
                        m_AllergicDrug = drug;
                        if (CoreBusinessLogic.BusinessLogic.ConnectToHis) {
                            m_SkinTestForm.ShowMessageOnly = true;
                            m_SkinTestForm.Message = ConstMessages.MsgNotFindSkinTestResult;
                        }
                        else if ((rows != null) && (rows.Length > 0) && (rows[0]["yxbz"].ToString() == ConstNames.LightDemanding)) {
                            m_SkinTestForm.ShowMessageOnly = true;
                            m_SkinTestForm.Message = ConstMessages.MsgSkinRestResultIsPlus;
                        }
                        else {
                            m_SkinTestForm.ShowMessageOnly = false;
                        }
                        // �ڵ���Ƥ�Խ������ؼ�ǰ���¼�����ʹ�ý�����ȡ���¼�
                        itemInput.EnterMoveNextControl = false;
                        m_SkinTestForm.ShowDialog();
                        return;
                    }
                }
            }
            itemInput.EnterMoveNextControl = true;
        }

        private void DoAfterChoiceSkinTestResult(object sender, EventArgs e) {
            if (m_SkinTestForm.HadChoiceOne) {
                m_UILogic.SaveSkinTestResult(m_AllergicDrug.SpecSerialNo, m_AllergicDrug.Name, m_SkinTestForm.SkinTestResult);

            }
            if ((!m_SkinTestForm.HadChoiceOne)
               || (m_SkinTestForm.SkinTestResult == SkinTestResultKind.Plus)) {
                itemInput.CodeValue = null;
                itemInput.Focus();
                itemInput.EnterMoveNextControl = true;
            }
        }

        private decimal CalcTotalAmount() {
            //סԺ��λ������
            decimal count = edtTotalAccount.Value;
            Druggery drug = Item as Druggery;
            if (drug == null)
                return 0;
            //סԺ��λϵ�� 
            decimal quctiety = drug.SpecUnit.Quotiety;

            // ���� /���ϵ�� 
            return count / quctiety;
        }

        private void ResetOutDrugTotalAmount() {
            if (!panelTotal.Visible)
                return;

            //���������õ���Ժ��ҩ������
            decimal totalOfDay;
            //ÿ�������
            totalOfDay = CalcTotalAmountOfAvgDay();

            //����/ÿ������
            decimal day = CalcTotalAmount() / totalOfDay;
            edtDays.Value = Convert.ToInt32(day);
        }
        #endregion

        #region events
        private void catalogInput_CodeValueChanged(object sender, EventArgs e) {
            SetValueAfterCatalogChanged();
        }

        private void itemInput_CodeValueChanged(object sender, EventArgs e) {
            SetValueAfterItemChanged();
            CheckNeedSkinTestAfterSelectItem();
        }

        private void usageInput_CodeValueChanged(object sender, EventArgs e) {
            //FireSelectedItemChanged(true);
        }

        private void edtAmount_EditValueChanged(object sender, EventArgs e) {
            // ��Ժ��ҩʱ���������/Ƶ��/�����и��£���Ҫˢ��ƽ��ÿ������
            ResetOutDruggeryTotalAmout();
            // ��Ժ��ҩʱ���������/Ƶ��/�����и��£���Ҫˢ�³�Ժ��ҩ����
            ResetOutDrugTotalAmount();
        }

        private void edtAmount_EditValueChanging(object sender, ChangingEventArgs e) {
            decimal newValue;

            //if ((e.NewValue.ToString() == "") || (e.NewValue.ToString() == "."))
            //   return;

            try {
                newValue = Convert.ToDecimal(e.NewValue);
                if ((newValue > edtAmount.Properties.MaxValue) || (newValue < edtAmount.Properties.MinValue))
                    edtAmount.ErrorText = String.Format(CultureInfo.CurrentCulture
                       , ConstMessages.FormatRangOfCount, edtAmount.Properties.MinValue, edtAmount.Properties.MaxValue);
                else
                    edtAmount.ErrorText = "";
            }
            catch { }
        }

        private void selectUnit_SelectedIndexChanged(object sender, EventArgs e) { }

        private void selectUnit_Enter(object sender, EventArgs e) {
            //selectUnit.ShowPopup();
        }

        private void selectUnit_CloseUp(object sender, CloseUpEventArgs e) {
            if (selectUnit.EnterMoveNextControl)
                SendKeys.Send("{ENTER}");
        }

        private void edtDays_EditValueChanged(object sender, EventArgs e) {
            // ��Ժ��ҩʱ���������/Ƶ��/�����и��£���Ҫˢ��ƽ��ÿ������
            ResetOutDruggeryTotalAmout();
            // ��Ժ��ҩʱ���������/Ƶ��/�����и��£���Ҫˢ�´�ҩ����
            ResetOutDrugTotalAmount();
        }

        private void deptInput_CodeValueChanged(object sender, EventArgs e) {
            if (ShiftDept != null) {
                ShiftDept.InitializeCorrespondingWards(m_SqlExecutor);
                // ֻ��ʾ���Ҷ�Ӧ�Ĳ���(Ҫ���財����������)
                wardInput.CurrentWordbook.ExtraCondition = ShiftDept.CorrespondingWardsCondition;
                if (wardInput.HadGetValue)
                    wardInput.CodeValue = wardInput.CodeValue;
            }
            else {
                wardInput.CodeValue = "";
            }
        }

        private void wardInput_CodeValueChanged(object sender, EventArgs e) { }

        private void edtEntrustContent_EditValueChanged(object sender, EventArgs e) { }

        private void dateEditExtra_EditValueChanged(object sender, EventArgs e) {
        }

        private void timeEditExtra_EditValueChanged(object sender, EventArgs e) {
        }

        private void ckSelfProvide_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)13) {
                ProcessDialogKey(Keys.Tab);
                //SendKeys.Send("TAB");
                //e.Handled = true;
            }
        }

        private void ckTomorrowUsed_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)13) {
                ProcessDialogKey(Keys.Tab);
                //SendKeys.Send("TAB");
                //e.Handled = true;
            }
        }

        private void btnCommit_Enter(object sender, EventArgs e) {
            btnOk.Focus();
            //FireDataEditFinished(new EventArgs());
        }

        private void btnNew_Click(object sender, EventArgs e) {
            IsModifyData = false;
            InitializeDefaultValue(DateTime.MinValue, null);

        }

        private void btnOk_Click(object sender, EventArgs e) {
            try
            {
                if (IsModifyData)
                {
                    FireDataEditFinished(new DataCommitArgs(DataCommitType.Modify));
                }
                else
                {
                    FireDataEditFinished(new DataCommitArgs(DataCommitType.Add));
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e) {
            FireDataEditFinished(new DataCommitArgs(DataCommitType.Insert));
        }

        private void OrderContentEditor_Enter(object sender, EventArgs e) {
            if (IsModifyData)
                FireStartEdit(new DataCommitArgs(DataCommitType.Modify));
            else
                FireStartEdit(new DataCommitArgs(DataCommitType.Add));
        }

        private void ckEdtDruggery_CheckedChanged(object sender, EventArgs e) {
            try
            {
                if (ckEdtDruggery.Checked)
                {
                    ContentKind = OrderContentKind.Druggery;
                    this.itemInput.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void ckEdtNormalItem_CheckedChanged(object sender, EventArgs e) {
            try
            {
                if (ckEdtNormalItem.Checked)
                {
                    ContentKind = OrderContentKind.ChargeItem;
                    this.itemInput.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void ckEdtGeneralItem_CheckedChanged(object sender, EventArgs e) {
            try
            {
                if (ckEdtGeneralItem.Checked)
                {
                    ContentKind = OrderContentKind.GeneralItem;
                    this.itemInput.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void ckEdtOperate_CheckedChanged(object sender, EventArgs e) { 
            try
            {
                if (ckEdtOperate.Checked)
                {
                    ContentKind = OrderContentKind.Operation;
                    this.itemInput.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void ckEdtText_CheckedChanged(object sender, EventArgs e) {
            try
            {
                if (ckEdtText.Checked)
                {
                    ContentKind = OrderContentKind.TextNormal;
                    this.itemInput.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void ckEdtShiftDept_CheckedChanged(object sender, EventArgs e) {
            try
            {
                if (ckEdtShiftDept.Checked)
                {
                    ContentKind = OrderContentKind.TextShiftDept;
                    this.itemInput.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void ckEdtLeaveHosp_CheckedChanged(object sender, EventArgs e) {   
            try
            {
                if (ckEdtLeaveHosp.Checked)
                {
                    ContentKind = OrderContentKind.TextLeaveHospital;
                    this.itemInput.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void ckEdtOutDruggery_CheckedChanged(object sender, EventArgs e) {   
            try
            {
                if (ckEdtOutDruggery.Checked)
                {
                    ContentKind = OrderContentKind.OutDruggery;
                    this.itemInput.Focus();
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }

        private void ckSupToTemp_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)13) {
                ProcessDialogKey(Keys.Tab);
                //SendKeys.Send("TAB");
                //e.Handled = true;
            }
        }

        void edtTotalAccount_ValueChanged(object sender, System.EventArgs e) {
            ResetOutDrugTotalAmount();
        }

        private void selectUnit2_CloseUp(object sender, EventArgs e) {
            if (selectUnit.EnterMoveNextControl)
                SendKeys.Send("{ENTER}");
        }

        /// <summary>
        /// ��ѡ��س��¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-25</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == 13)
                {
                    DS_Common.cbx_KeyPress(sender);
                }
            }
            catch (Exception ex)
            {
                DrectSoft.Common.Ctrs.DLG.MyMessageBox.Show(ex.Message);
            }
        }
        #endregion

    }
}
