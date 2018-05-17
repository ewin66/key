using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using DrectSoft.Common.Eop;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using DrectSoft.Core.TimeLimitQC;
using DrectSoft.FrameWork.WinForm.Plugin;
using Eop = DrectSoft.Common.Eop;

namespace DrectSoft.Core.DoctorAdvice
{
    /// <summary>
    /// �����й�ҽ���ĺ����߼�(��ȡ���ݡ�ORMת����ҵ���߼����ơ�����У���)
    /// ֧�ֶԲ���ҽ���ı༭�ͳ���ҽ�����ݵı༭
    /// </summary>
    internal class CoreBusinessLogic
    {
        #region const & readonly
        //private const string QCValShiftDept = "O:3111"; // ת��ҽ��
        //private const string QCValDanger = "O:3102Z";   // ����ҽ��
        //private const string QCValOperation = "O:3105"; // ����ҽ��
        //private const string QCValNormalPat = "P:1501N"; //����״̬����
        #endregion

        #region static common methods
        /// <summary>
        /// �õ�ָ�����͵�ҽ�������ݿ��еı���
        /// </summary>
        /// <param name="isTemp">ҽ�����ͣ����ڡ���ʱ��</param>
        /// <returns>ҽ������</returns>
        public static string GetOrderTableName(bool isTemp)
        {
            if (isTemp)
                return ConstSchemaNames.TempOrderTableName;
            else
                return ConstSchemaNames.LongOrderTableName;
        }

        /// <summary>
        /// ���ָ�����͵�ҽ����ҽ��ҽ����ŵ��ֶ���
        /// </summary>
        /// <param name="isTemp">ҽ�����ͣ����ڡ���ʱ��</param>
        /// <returns>ҽ����ŵ��ֶ���</returns>
        public static string GetSerialNoField(bool isTemp)
        {
            if (isTemp)
                return ConstSchemaNames.OrderTempColSerialNo;
            else
                return ConstSchemaNames.OrderLongColSerialNo;
        }

        private static void DeserializerDoctorAdviceSetting()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DoctorAdviceSetting));
            //FileStream fileStream = new FileStream(fileName, FileMode.Open);
            _customSetting = (DoctorAdviceSetting)serializer.Deserialize(BasicSettings.GetConfig("DoctorAdviceSetting"));
            //fileStream.Close();
            _customSetting.CalcOtherVariables();
        }

        /// <summary>
        /// �������ҽ���������������Ϣ���и�ʽ��
        /// </summary>
        /// <param name="processName">��������</param>
        /// <param name="messages">������Ϣ</param>
        /// <returns></returns>
        public static string FormatProcessErrorMessage(string processName, string messages)
        {
            return String.Format(CultureInfo.CurrentCulture
               , ConstMessages.FormatOpError, processName, messages);
        }
        #endregion

        #region public properties
        /// <summary>
        /// �й�ҽ�����Զ�������
        /// </summary>
        public static DoctorAdviceSetting CustomSetting
        {
            get
            {
                if (_customSetting == null)
                    DeserializerDoctorAdviceSetting();
                return _customSetting;
            }
        }
        private static DoctorAdviceSetting _customSetting;

        /// <summary>
        /// ҵ���߼�����
        /// </summary>
        public static BusinessLogicSetting BusinessLogic
        {
            get { return CustomSetting.BusinessLogic; }
        }

        /// <summary>
        /// ҽ������������ݱ�
        /// </summary>
        public DataTable OrderContentCatalog
        {
            get
            {
                if (_orderContentCatalog == null)
                    CreateOrderContentCatalogTable();
                return _orderContentCatalog;
            }
        }
        private DataTable _orderContentCatalog;

        /// <summary>
        /// ����ҽ����������ʼʱ��(����Ժʱ��Ϊ׼)
        /// </summary>
        public DateTime MinStartDateTime
        {
            get
            {
                if (CurrentPatient != null)
                    return CurrentPatient.InfoOfAdmission.AdmitInfo.StepOneDate;
                else
                    return DateTime.Now;
            }
        }

        /// <summary>
        /// ��ҽ���Ŀ�ʼʱ���ڵ�ǰʱ��֮ǰ�೤ʱ��ͽ�����ʾ����СʱΪ��λ
        /// </summary>
        public DateTime WarnStartDateTime
        {
            get
            {
                return DateTime.Now.AddHours(-BusinessLogic.StartDateWarningHours);
            }
        }

        /// <summary>
        /// Ĭ�ϵĳ�Ժʱ��
        /// </summary>
        public DateTime DefaultLeaveHospitalTime
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
        /// ��ǰ����Ĳ���
        /// </summary>
        public Inpatient CurrentPatient
        {
            get { return _currentPatient; }
            set
            {
                //if (_currentPatient != value)
                {
                    if ((value == null) || ((value.NoOfHisFirstPage == "-1") && (value.NoOfFirstPage == 0)))
                        _currentPatient = null;
                    else
                        _currentPatient = value;
                    DoAfterSwitchPatient();
                }
            }
        }
        private Inpatient _currentPatient;

        /// <summary>
        /// ��ǰ����Ĳ��˵���ҳ��ţ���ʼ����ҽ����������ʱ��Ҫ��
        /// �ṩ��������Ϊ�˼���ҽ���ͳ���ҽ���Ĵ���
        /// </summary>
        public decimal CurrentPatientNo
        {
            get
            {
                if (m_ProcessModel != EditorCallModel.EditSuite)
                {
                    if (CurrentPatient != null)
                        return CurrentPatient.NoOfFirstPage;
                    else
                        return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        /// <summary>
        /// ��ǰ���˵�ҽ�����Ƿ������Ч�ġ���Ժҽ������û�б�ȡ���ģ�
        /// </summary>
        public bool HasOutHospitalOrder
        {
            get
            {
                return (m_TempTable != null) && m_TempTable.HasOutHospitalOrder;
            }
        }

        /// <summary>
        /// ��ǰ���˵�ҽ�����Ƿ������Ч�ġ�ת��ҽ����(�µġ���δִ�У����������)
        /// </summary>
        public bool HasShiftDeptOrder
        {
            get
            {
                return (m_TempTable != null) && m_TempTable.HasShiftDeptOrder;
            }
        }

        /// <summary>
        /// �Ƿ���������³���ҽ��
        /// </summary>
        public bool AllowAddLong
        {
            get
            {
                // ����������³���ҽ�������:
                //    ��ǰҽ����ֻ��
                //    ��ʱҽ���а�����Ժҽ����������ӳ���ҽ��
                //    ������Ч��ת��ҽ��(��ҽ���༭���߼���Ҫ��֤��Ч��ת��ҽ��ֻ���ܳ����ڲ���ת��ǰ)
                return (m_LongTable != null) && (!HasOutHospitalOrder) && (!HasShiftDeptOrder)
                   && (m_LongTable.DefaultView.AllowEdit) && (m_LongTable.DefaultView.AllowNew);
            }
        }

        /// <summary>
        /// �Ƿ����������ʱҽ��
        /// </summary>
        public bool AllowAddTemp
        {
            get
            {
                // �������������ʱҽ�������:
                //    ��ǰҽ����ֻ��
                //    ������Ч��ת��ҽ��(��ҽ���༭���߼���Ҫ��֤��Ч��ת��ҽ��ֻ���ܳ����ڲ���ת��ǰ)
                return (m_TempTable != null) && (!HasShiftDeptOrder)
                   && (m_TempTable.DefaultView.AllowEdit) && (m_TempTable.DefaultView.AllowNew);
            }
        }

        /// <summary>
        /// ���ҽ���������Ƿ����ı�
        /// </summary>
        public bool HadChanged
        {
            get
            {
                return (m_LongTable != null) && (m_TempTable != null)
                   && (m_LongTable.HadChanged || m_TempTable.HadChanged);
            }
        }

        /// <summary>
        /// ����Ƿ���δ���͵�����
        /// </summary>
        public bool HasNotSendData
        {
            get
            {
                return (m_LongTable != null) && (m_TempTable != null)
                   && (m_LongTable.HasNotSendData || m_TempTable.HasNotSendData);
            }
        }

        /// <summary>
        /// ����ҽ���߼��������
        /// </summary>
        public SuiteOrderHandle SuiteHelper
        {
            get
            {
                return _suiteHelper;
            }
            set
            {
                _suiteHelper = value;
                DoAfterSwitchSuite(this, new EventArgs());
            }
        }
        private SuiteOrderHandle _suiteHelper;
        #endregion

        #region private variables
        private OrderTable m_LongTable;
        private OrderTable m_TempTable;
        private IDataAccess m_SqlExecutor;
        private DataTable m_OutputTable;
        /// <summary>
        /// �����Ǵ�����ҽ�����ǳ���ҽ������
        /// </summary>
        private EditorCallModel m_ProcessModel;
        private Qcsv m_Qcsv;
        //private LogSendRecord m_SynchLogHelper;
        #endregion

        #region private properties
        private DataTable TableOfSynchTempOrder
        {
            get
            {
                if (_tableOfSynchTempOrder == null)
                {
                    _tableOfSynchTempOrder = m_SqlExecutor.ExecuteDataTable(String.Format(CultureInfo.CurrentCulture
                       , ConstSqlSentences.FormatSelectOrderSchema, GetOrderTableName(true)));
                    m_SqlExecutor.ResetTableSchema(_tableOfSynchTempOrder, GetOrderTableName(true));
                }
                return _tableOfSynchTempOrder;
            }
        }
        private DataTable _tableOfSynchTempOrder;

        private DataTable TableOfSynchLongOrder
        {
            get
            {
                if (_tableOfSynchLongOrder == null)
                {
                    _tableOfSynchLongOrder = m_SqlExecutor.ExecuteDataTable(String.Format(CultureInfo.CurrentCulture
                       , ConstSqlSentences.FormatSelectOrderSchema, GetOrderTableName(false)));
                    m_SqlExecutor.ResetTableSchema(_tableOfSynchLongOrder, GetOrderTableName(false));
                }
                return _tableOfSynchLongOrder;
            }
        }
        private DataTable _tableOfSynchLongOrder;

        //private IExchangeInfoServer m_InfoServer {
        //    get {
        //        if (_infoServer == null)
        //            _infoServer = ExchangeInfoHelper.ExchangeInfoServer;
        //        return _infoServer;
        //    }
        //}
        //private IExchangeInfoServer _infoServer;
        #endregion

        #region ctors
        /// <summary>
        /// ����ҽ���߼��������
        /// </summary>
        /// <param name="app"></param>
        /// <param name="callModel">����ģʽ</param>
        public CoreBusinessLogic(IEmrHost app, EditorCallModel callModel)
            : this(app.SqlHelper)
        {
            PersistentObjectFactory.SqlExecutor = app.SqlHelper;
            m_SqlExecutor = app.SqlHelper;
            m_Qcsv = new Qcsv();

            if (callModel == EditorCallModel.EditSuite)
            {
                _suiteHelper = new SuiteOrderHandle(app, false);
                _suiteHelper.AfterSwitchSuite += new EventHandler(DoAfterSwitchSuite);
            }
            m_ProcessModel = callModel;
        }

        /// <summary>
        /// ������ѯģʽ���߼�����
        /// </summary>
        /// <param name="sqlHelper"></param>
        public CoreBusinessLogic(IDataAccess sqlHelper)
        {
            PersistentObjectFactory.SqlExecutor = sqlHelper;
            m_SqlExecutor = sqlHelper;
            m_ProcessModel = EditorCallModel.Query;
        }

        /// <summary>
        /// ��ʱ���
        /// </summary>
        /// <param name="sqlHelper"></param>
        /// <param name="callModel"></param>
        public CoreBusinessLogic(IDataAccess sqlHelper, EditorCallModel callModel)
            : this(sqlHelper)
        {
            m_ProcessModel = EditorCallModel.EditOrder;
        }
        #endregion

        #region custom event handler
        /// <summary>
        /// ��Ժҽ���ı��¼�(���ӻ�ɾ����ȡ��)��
        /// </summary>
        public event EventHandler OutHospitalOrderChanged
        {
            add
            {
                onOutHospitalOrderChanged = (EventHandler)Delegate.Combine(onOutHospitalOrderChanged, value);
            }
            remove
            {
                onOutHospitalOrderChanged = (EventHandler)Delegate.Remove(onOutHospitalOrderChanged, value);
            }
        }
        private EventHandler onOutHospitalOrderChanged;

        private void FireOutHospitalOrderChanged(object sender, EventArgs e)
        {
            if (onOutHospitalOrderChanged != null)
                onOutHospitalOrderChanged(sender, e);
        }

        /// <summary>
        /// ����ҽ�����ݷ����ı��¼���
        /// </summary>
        public event EventHandler PatientOrderDataChanged
        {
            add
            {
                onPatientOrderDataChanged = (EventHandler)Delegate.Combine(onPatientOrderDataChanged, value);
            }
            remove
            {
                onPatientOrderDataChanged = (EventHandler)Delegate.Remove(onPatientOrderDataChanged, value);
            }
        }
        private EventHandler onPatientOrderDataChanged;

        private void FirePatientOrderDataChanged(EventArgs e)
        {
            if (onPatientOrderDataChanged != null)
                onPatientOrderDataChanged(this, e);
        }
        #endregion

        #region event hadle
        private void DoAfterOutHospitalOrderChanged(object sender, EventArgs e)
        {
            FireOutHospitalOrderChanged(sender, e);
        }

        private void DoAfterSwitchSuite(object sender, EventArgs e)
        {
            //m_ProcessModel = EditorCallModel.EditSuite;

            if (m_TempTable != null)
            {
                m_TempTable.OutHospitalOrderChanged -= new EventHandler(DoAfterOutHospitalOrderChanged);
                m_TempTable.DefaultView.ListChanged -= new ListChangedEventHandler(DoOrderViewListChanged);
            }
            if (m_LongTable != null)
            {
                m_LongTable.DefaultView.ListChanged -= new ListChangedEventHandler(DoOrderViewListChanged);
            }

            if (SuiteHelper.CurrentSuiteNo <= 0)
            {
                m_TempTable = null;
                m_LongTable = null;
                return;
            }

            CreateTempOrderTable(SuiteHelper.TempOrderTable);
            CreateLongOrderTable(SuiteHelper.LongOrderTable);

            m_TempTable.DefaultView.AllowEdit = true;
            m_LongTable.DefaultView.AllowEdit = true;
        }
        #endregion

        #region private common methods
        private void CreateOrderContentCatalogTable()
        {
            if (_orderContentCatalog != null)
                return;

            _orderContentCatalog = new DataTable();
            _orderContentCatalog.Locale = CultureInfo.CurrentCulture;
            // ������ṹ
            _orderContentCatalog.Columns.AddRange(new DataColumn[] {
              new DataColumn(ConstSchemaNames.ContentCatalogColId, typeof(int))
            , new DataColumn(ConstSchemaNames.ContentCatalogColName, typeof(string))
            , new DataColumn(ConstSchemaNames.ContentCatalogColFlag, typeof(int))
            });
            _orderContentCatalog.Columns[ConstSchemaNames.ContentCatalogColId].Caption = ConstNames.ContentCatalogId; // ��ӦOrderContentKind�е�ֵ
            _orderContentCatalog.Columns[ConstSchemaNames.ContentCatalogColName].Caption = ConstNames.ContentCatalogName;
            _orderContentCatalog.Columns[ConstSchemaNames.ContentCatalogColFlag].Caption = ConstNames.ContentCatalogFlag; // ҽ�������־��˵���������õ�ҽ����𣬶�ӦOrderManagerKind�е�ֵ
            _orderContentCatalog.PrimaryKey = new DataColumn[] { _orderContentCatalog.Columns[ConstSchemaNames.ContentCatalogColId] };

            // ��������
            InsertOrderContentCatalogRow(OrderContentKind.Druggery, ConstNames.ContentDruggery, OrderManagerKind.Normal);
            InsertOrderContentCatalogRow(OrderContentKind.ChargeItem, ConstNames.ContentChargeItem, OrderManagerKind.Normal);
            InsertOrderContentCatalogRow(OrderContentKind.GeneralItem, ConstNames.ContentGeneralItem, OrderManagerKind.ForLong);
            InsertOrderContentCatalogRow(OrderContentKind.ClinicItem, ConstNames.ContentClinicItem, OrderManagerKind.Normal);
            InsertOrderContentCatalogRow(OrderContentKind.OutDruggery, ConstNames.ContentOutDruggery, OrderManagerKind.ForTemp);
            InsertOrderContentCatalogRow(OrderContentKind.Operation, ConstNames.ContentOperation, OrderManagerKind.ForTemp);
            InsertOrderContentCatalogRow(OrderContentKind.TextNormal, ConstNames.ContentTextNormal, OrderManagerKind.Normal);
            InsertOrderContentCatalogRow(OrderContentKind.TextShiftDept, ConstNames.ContentTextShiftDept, OrderManagerKind.ForTemp);
            //InsertOrderContentCatalogRow(OrderContentKind.TextAfterOperation, "����ҽ��", OrderManagerKind.ForLong);
            InsertOrderContentCatalogRow(OrderContentKind.TextLeaveHospital, ConstNames.ContentTextLeaveHospital, OrderManagerKind.ForTemp);

            //_orderContentCatalog.DefaultView.Sort = ConstSchemaNames.ContentCatalogColName;
        }

        private void InsertOrderContentCatalogRow(OrderContentKind contentKind, string name, OrderManagerKind managerKind)
        {
            DataRow row = _orderContentCatalog.NewRow();

            row[ConstSchemaNames.ContentCatalogColId] = (int)contentKind;
            row[ConstSchemaNames.ContentCatalogColName] = name;
            row[ConstSchemaNames.ContentCatalogColFlag] = (int)managerKind;

            _orderContentCatalog.Rows.Add(row);
        }

        private void DoAfterSwitchPatient()
        {
            if (m_TempTable != null)
            {
                m_TempTable.OutHospitalOrderChanged -= new EventHandler(DoAfterOutHospitalOrderChanged);
                m_TempTable.DefaultView.ListChanged -= new ListChangedEventHandler(DoOrderViewListChanged);
            }
            if (m_LongTable != null)
            {
                m_LongTable.DefaultView.ListChanged -= new ListChangedEventHandler(DoOrderViewListChanged);
            }

            if (CurrentPatient == null)
            {
                m_TempTable = null;
                m_LongTable = null;
                return;
            }

            CreateTempOrderTable(QueryOrderData(true));
            CreateLongOrderTable(QueryOrderData(false));

            // ��������ڲ���������༭
            bool allowEdit;
            switch (CurrentPatient.State)
            {
                case InpatientState.InWard:
                case InpatientState.InICU:
                case InpatientState.InDeliveryRoom:
                    allowEdit = true;
                    break;
                default:
                    allowEdit = false;
                    break;
            }

            m_TempTable.DefaultView.AllowEdit = allowEdit && (m_ProcessModel != EditorCallModel.Query);
            m_LongTable.DefaultView.AllowEdit = allowEdit && (m_ProcessModel != EditorCallModel.Query);

            // �����ִ�еĳ���ҽ���Ƿ��ѵ�ֹͣʱ�䣬����ѵ�������״̬��Ϊֹͣ
            AutoHandleExecutingLongOrder();

            //m_SynchLogHelper.CurrentPatient = CurrentPatient;
        }

        private void CreateTempOrderTable(DataTable orderData)
        {
            m_TempTable = new OrderTable(orderData, true, m_SqlExecutor);
            m_TempTable.OutHospitalOrderChanged += new EventHandler(DoAfterOutHospitalOrderChanged);
            m_TempTable.DefaultView.ListChanged += new ListChangedEventHandler(DoOrderViewListChanged);
        }

        private void CreateLongOrderTable(DataTable orderData)
        {
            m_LongTable = new OrderTable(orderData, false, m_SqlExecutor);
            m_LongTable.DefaultView.ListChanged += new ListChangedEventHandler(DoOrderViewListChanged);
        }

        /// <summary>
        /// ��ѯָ������ҽ������
        /// </summary>
        /// <param name="isTemp">ҽ�������ǣ����ڡ���ʱ��</param>
        /// <returns>ҽ�����ݱ�</returns>
        private DataTable QueryOrderData(bool isTemp)
        {
            DataTable table = null;
            if (BusinessLogic.EnableEmrOrderModul) // ����EMRҽ������ʱ����EMR�Լ���ҽ����
            {

                string tableName = GetOrderTableName(isTemp);

                table = m_SqlExecutor.ExecuteDataTable(
                   String.Format(CultureInfo.CurrentCulture
                      , ConstSqlSentences.FormatSelectOrderData, tableName, _currentPatient.NoOfFirstPage, GetSerialNoField(isTemp))
                      , CommandType.Text);
                //m_SqlExecutor.ResetTableSchema(table, tableName);
            }
            else // ͨ���ӿڶ�ȡHIS��ҽ������
            {
                //TODO ��ȡhisҽ�����ݼ�
                //string[,] parameters = new string[3, 6];

                //parameters[0, 0] = "queryType";
                //parameters[1, 0] = "int";
                //parameters[2, 0] = "1"; // �鵥������
                //parameters[0, 1] = "hisFirstPageNo";
                //parameters[1, 1] = "string";
                //parameters[2, 1] = CurrentPatient.NoOfHisFirstPage.ToString();
                //parameters[0, 2] = "wardCode";
                //parameters[1, 2] = "string";
                //parameters[2, 2] = "";
                //parameters[0, 3] = "deptCode";
                //parameters[1, 3] = "string";
                //parameters[2, 3] = "";
                //parameters[0, 4] = "orderType";
                //parameters[1, 4] = "int";
                //parameters[2, 4] = isTemp ? "2" : "1";
                //parameters[0, 5] = "orderState";
                //parameters[1, 5] = "int";
                //parameters[2, 5] = "0"; // ����ҽ��

                //string sExio;
                //sExio = m_InfoServer.BuildExchangeInfoString(ExchangeInfoOrderConst.MsgGetHisOrder
                //   , ExchangeInfoOrderConst.EmrSystemName
                //   , ExchangeInfoOrderConst.HisSystemName, parameters);

                //string outMsg;
                //if (m_InfoServer.AddSyncExchangeInfo(sExio, ExchangeInfoOrderConst.DefaultEncoding, out outMsg) != ResponseFlag.Complete)
                //    throw new ApplicationException(outMsg);
                //// ת�����ݼ�
                //DataSet resultDS = new DataSet();
                //if (!String.IsNullOrEmpty(outMsg)) {
                //    // ���ݼ�ת����byte����
                //    MemoryStream source = new MemoryStream();
                //    byte[] data = ExchangeInfoOrderConst.DefaultEncoding.GetBytes(outMsg);
                //    source.Write(data, 0, data.Length);
                //    source.Seek(0, SeekOrigin.Begin);
                //    resultDS.ReadXml(source);
                //}
                //else
                //    throw new ApplicationException(ConstMessages.ExceptionCallRemoting);
                //// ��ҳ��Ÿ��³�EMR�Լ���
                //table = resultDS.Tables[0];
                //// ���ص����ݼ���ֻ��HIS��ҳ��ţ�Ҫ�滻��EMR�Լ�����ҳ���
                //table.Columns.Remove("hissyxh");
                //DataColumn col = new DataColumn("syxh", typeof(decimal));
                //col.DefaultValue = CurrentPatient.NoOfFirstPage;
                ////foreach (DataRow row in table.Rows)
                ////   row["syxh"] = CurrentPatient.NoOfFirstPage;
                //table.AcceptChanges();
            }

            return table;
        }

        private void GenerateOutputTable()
        {
            m_OutputTable = new DataTable();
            m_OutputTable.Locale = CultureInfo.CurrentCulture;
            DataColumn col;
            col = new DataColumn(ConstSchemaNames.OrderOutputColProductSerialNo, Type.GetType("System.String"));
            col.Caption = ConstNames.OrderOutputProductSerialNo;
            m_OutputTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.OrderOutputColDruggeryName, Type.GetType("System.String"));
            col.Caption = ConstNames.OrderOutputDruggeryName;
            m_OutputTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.OrderOutputColAmount, Type.GetType("System.Double"));
            col.Caption = ConstNames.OrderOutputAmount;
            m_OutputTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.OrderOutputColUnit, Type.GetType("System.String"));
            col.Caption = ConstNames.OrderOutputUnit;
            m_OutputTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.OrderOutputColUsageCode, Type.GetType("System.String"));
            col.Caption = ConstNames.OrderOutputUsageCode;
            m_OutputTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.OrderOutputColUsageName, Type.GetType("System.String"));
            col.Caption = ConstNames.OrderOutputUsageName;
            m_OutputTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.OrderOutputColFrequencyCode, Type.GetType("System.String"));
            col.Caption = ConstNames.OrderOutputFrequencyCode;
            m_OutputTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.OrderOutputColFrequencyName, Type.GetType("System.String"));
            col.Caption = ConstNames.OrderOutputFrequencyName;
            m_OutputTable.Columns.Add(col);
            col = new DataColumn(ConstSchemaNames.OrderOutputColDruggeryCode, Type.GetType("System.String"));
            col.Caption = ConstNames.OrderOutPutDruggeryCode;
            m_OutputTable.Columns.Add(col);

        }

        /// <summary>
        /// ҽ������List�ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoOrderViewListChanged(object sender, ListChangedEventArgs e)
        {
            // ����Ժҽ��������ת��ҽ��������ӻ�ɾ����ȡ������Ӱ�쵽�Ƿ����������ҽ��
            //    Ϊ�Ż�������Ҫ������жϣ��Լ����¼������Ĵ���
            switch (e.ListChangedType)
            {
                case ListChangedType.Reset:
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                    FirePatientOrderDataChanged(new EventArgs());
                    break;
            }
        }

        private static void SetNewOrderTogether(OrderTable table)
        {
            Order temp;
            // ����ҽ���鵽һ�𣬷��������ҽ���ĺ���
            bool hasMeetAudit = false;
            int lastAuditIndex = -1;
            List<int> newList = new List<int>();
            for (int rowHandle = table.Orders.Count - 1; rowHandle >= 0; rowHandle--)
            {
                temp = table.Orders[rowHandle];
                if ((temp.State != OrderState.Audited) && (temp.State != OrderState.New))
                    break;
                // ������ң���һ����������˼�¼ʱ������λ��
                // ������ҵ�����˼�¼���ַ����¼�¼���������λ��
                if ((temp.State == OrderState.Audited) && (lastAuditIndex == -1))
                {
                    hasMeetAudit = true;
                    lastAuditIndex = rowHandle;
                }
                else if ((temp.State == OrderState.New) && hasMeetAudit)
                {
                    newList.Add(rowHandle);
                }
            }
            if ((newList.Count > 0) && (lastAuditIndex > -1))
            {
                // ���������һ�����ҽ��֮ǰ����ҽ�������䰴ԭ�ȵ�˳����뵽�������ҽ���ĺ���
                for (int index = newList.Count - 1; index >= 0; index--)
                {
                    table.MoveOrder(newList[index], lastAuditIndex + 1);
                    lastAuditIndex -= 1; // �ƶ������һ�����ҽ����λ�ý�ǰ��һ��
                }
            }
        }
        #endregion

        #region private methods of checking data
        private void CheckOrderStartDatetime(OrderTable currentTable, int targetIndex, DateTime startDateTime)
        {
            // ��鿪ʼ���ڣ����
            //    �������ڵ���ĳ��ʱ���(����״̬��ҽ��)
            //    ����������һ��ҽ���Ŀ�ʼʱ��
            if (startDateTime.Date == DateTime.MinValue.Date)
                throw new DataCheckException(ConstMessages.CheckStartDateNull, OrderView.UNStartDate);
            else if (startDateTime < MinStartDateTime)
                throw new DataCheckException(String.Format(CultureInfo.CurrentCulture, ConstMessages.FormatStartDateMustAfter, MinStartDateTime)
                   , OrderView.UNStartDate);
            else
            {
                int preIndex = targetIndex - 1;
                bool isFirstNew;
                Order preOrder = null;
                if ((preIndex < 0) || (preIndex >= currentTable.Orders.Count))
                    isFirstNew = true;
                else
                {
                    preOrder = currentTable.Orders[preIndex];
                    isFirstNew = (preOrder.State != OrderState.New);
                }

                if (!isFirstNew)
                {
                    DateTime preStartTime;
                    // ���ǰһ��ҽ�����鿪ʼ�����м䣬˵�����͵�ǰ�����ҽ������ͬһ�����飬��ȡ����ǰ������ҽ���Ŀ�ʼʱ��
                    if ((preOrder.GroupPosFlag != GroupPositionKind.SingleOrder)
                       && (preOrder.GroupPosFlag != GroupPositionKind.GroupEnd))
                    {
                        preStartTime = MinStartDateTime; // ����֮ǰ����û����ҽ��
                        for (int handle = preIndex - 1; handle >= 0; handle--)
                        {
                            if (currentTable.Orders[handle].State == OrderState.New)
                            {
                                if (currentTable.Orders[handle].GroupSerialNo != preOrder.GroupSerialNo)
                                {
                                    preStartTime = currentTable.Orders[handle].StartDateTime;
                                    break;
                                }
                            }
                            else
                                break;
                        }
                    }
                    else
                        preStartTime = preOrder.StartDateTime;

                    if (startDateTime < preStartTime)
                        throw new DataCheckException(ConstMessages.CheckStartDateBeforPreRow, OrderView.UNStartDate);
                }
                if (startDateTime < WarnStartDateTime)
                    throw new DataCheckException(String.Format(CultureInfo.CurrentCulture, ConstMessages.FormatStartDateMustBefore, WarnStartDateTime)
                       , currentTable.IsTempOrder.ToString(), 0);
            }
        }

        private void CheckCeaseTimeOfLongOrder(OrderTable currentTable, Order orderTemp)
        {
            LongOrder longOrder = orderTemp as LongOrder;
            if ((!currentTable.IsTempOrder) && (longOrder != null))
            {
                if (longOrder.CeaseInfo.ExecuteTime != DateTime.MinValue)
                {
                    // ���ֹͣ���ڣ�ֻ�Գ���ҽ����Ч������Ϊ�գ�
                    //    �������ڵ�ǰʱ��
                    //    ��������ҽ����ʼʱ�� 
                    if ((longOrder.CeaseInfo.ExecuteTime < DateTime.Now)
                       || (longOrder.CeaseInfo.ExecuteTime <= longOrder.StartDateTime))
                        throw new DataCheckException(ConstMessages.CheckCeaseDateBeforeStartDate, OrderView.UNCeaseDate);
                }
            }
        }

        private void CheckOrderContentData(OrderTable currentTable, OrderContent content, int targetIndex, GroupPositionKind currentGroupKind)
        {
            if (content == null)
                throw new DataCheckException(ConstMessages.ExceptionNullOrderObject, OrderView.UNContent);

            //    ҽ�����ݵ������Ƿ������ڵ�ǰҽ������г���
            if (HasOutHospitalOrder && (content.OrderKind != OrderContentKind.OutDruggery)
               && (content.OrderKind != OrderContentKind.TextLeaveHospital))
                throw new DataCheckException(ConstMessages.CheckOnlyAllowDruggery, OrderView.UNContent);

            if (!HasOutHospitalOrder) // ��Ժҽ���Ƚ����⣬ǰ����������������кܴ�ͬ�����Բ�������ļ��
            {
                OrderContentCatalog.DefaultView.RowFilter = GetOrderContentCatalogRowFilter(currentTable.IsTempOrder);
                OrderContentCatalog.DefaultView.Sort = ConstSchemaNames.ContentCatalogColId;
                int locateIndex = OrderContentCatalog.DefaultView.Find((int)content.OrderKind);
                if (locateIndex < 0)
                    throw new DataCheckException(ConstMessages.CheckCatalogNotSupport, OrderView.UNContent);
            }

            // ��������Ƿ�������
            string contentErr = content.CheckProperties();
            if (contentErr.Length > 0)
                throw new DataCheckException(contentErr, OrderView.UNContent);

            // ����Ƿ������ظ���ҩƷ����Ŀ(ֻ����ҽ��������ˡ�����ִ�е�ҽ���м�飬���������ҽ����Ŀǰ������ʱҽ�������)
            if (currentTable.IsTempOrder)
                return;

            //add by zhouhui ����ҽ����ҩƷ����Ŀ������ظ�
            if ((currentGroupKind != GroupPositionKind.None) && (currentGroupKind != GroupPositionKind.SingleOrder))
                return;

            bool isGeneral = ((content.OrderKind == OrderContentKind.GeneralItem) && (!currentTable.IsTempOrder));
            //bool isOutDruggery = (content.OrderKind == OrderContentKind.OutDruggery);
            switch (content.OrderKind)
            {
                case OrderContentKind.Druggery:
                case OrderContentKind.ChargeItem:
                case OrderContentKind.GeneralItem:
                case OrderContentKind.Operation:
                case OrderContentKind.OutDruggery:
                    Order order;
                    for (int index = currentTable.Orders.Count - 1; index >= 0; index--)
                    {
                        if (index == targetIndex)
                            continue;
                        order = currentTable.Orders[index];
                        if (order.Content.OrderKind != content.OrderKind)
                        {
                            if ((m_ProcessModel == EditorCallModel.EditSuite)
                               && ((content.OrderKind == OrderContentKind.OutDruggery) || (order.Content.OrderKind == OrderContentKind.OutDruggery)))
                                throw new DataCheckException(ConstMessages.CheckNotAllowMixCatalogInSuite, OrderView.UNContent);
                            else
                                continue;
                        }
                        if (order.GroupPosFlag != GroupPositionKind.SingleOrder)
                            continue;
                        if ((order.Content.Item.Kind == content.Item.Kind)
                           && (order.Content.Item.KeyValue == content.Item.KeyValue))
                        {
                            switch (order.State)
                            {
                                case OrderState.New:
                                case OrderState.Audited:
                                    throw new DataCheckException(ConstMessages.CheckItemRepeatedInNew, OrderView.UNContent);
                                case OrderState.Executed:
                                    if ((!currentTable.IsTempOrder) && (!isGeneral))
                                        throw new DataCheckException(ConstMessages.CheckItemRepeatedInExecuting, OrderView.UNContent);
                                    break;
                            }
                        }
                    }
                    break;
            }
            //// ����ҽ�����
            //if ((content.OrderKind == OrderContentKind.GeneralItem) && (!currentTable.IsTempOrder))
            //{
            //   LongOrder order;
            //   for (int index = currentTable.Orders.Count - 1; index >= 0; index--)
            //   {
            //      if (index == targetIndex)
            //         continue;
            //      order = currentTable.Orders[index] as LongOrder;
            //      if ((order.State == OrderState.Executed) || (order.State == OrderState.Ceased))
            //         break;

            //      if ((order.Content.OrderKind == OrderContentKind.GeneralItem)
            //         && (order.Content.Item.Kind == content.Item.Kind))
            //      {
            //         throw new DataCheckException("�����ظ������ͬ���͵ĳ���ҽ��", OrderView.UNContent);
            //      }
            //   }
            //}
        }

        private string CheckCanBeDeleted(OrderTable table, Order[] selectedOrders)
        {
            EditProcessFlag flag = CalcEditProcessFlag(table, selectedOrders);
            if ((flag & EditProcessFlag.Delete) > 0)
                return "";

            StringBuilder msgs = new StringBuilder();
            msgs.AppendLine(ConstMessages.CheckAllIsNew);
            msgs.AppendLine(ConstMessages.CheckDelAllOutOrder);

            return FormatProcessErrorMessage(ConstNames.OpDelete, msgs.ToString());
        }

        private string CheckCanSetGroup(OrderTable table, Order[] selectedOrders)
        {
            if (selectedOrders.Length == 1)
                throw new DataCheckException(ConstMessages.CheckOnlyOneRowInGroup, ConstMessages.ExceptionTitleOrder);

            EditProcessFlag flag = CalcEditProcessFlag(table, selectedOrders);
            if ((flag & EditProcessFlag.SetGroup) > 0)
                return "";

            StringBuilder msgs = new StringBuilder();
            msgs.AppendLine(ConstMessages.CheckNewInGroup);
            msgs.AppendLine(ConstMessages.CheckSerialInGroup);

            return FormatProcessErrorMessage(ConstNames.OpSetGroup, msgs.ToString());
        }

        private void AutoHandleExecutingLongOrder()
        {
            foreach (LongOrder temp in m_LongTable.Orders)
            {
                // ���ѵ�ֹͣʱ���ҽ���ĳ���ֹͣ״̬
                if ((temp.State == OrderState.Executed) && (temp.CeaseInfo != null) && (temp.CeaseInfo.HadInitialized))
                {
                    //if (temp.CeaseInfo.ExecuteTime > DateTime.Now)
                    //�˴��߼����� modified by zhouhui
                    if (temp.CeaseInfo.ExecuteTime < DateTime.Now)
                        temp.SetStateToCeased();
                }
            }
        }

        private static string CheckCanCancelGroup(Order[] selectedOrders)
        {
            // ֻ����Ƿ�������ҽ��
            if (CheckStateIsSame(selectedOrders) == OrderState.New)
                return "";

            return FormatProcessErrorMessage(ConstNames.OpCancelGroup, ConstMessages.CheckCancelGroup);
        }

        private string CheckCanBeAudited(OrderTable currentTable, Order[] selectedOrders)
        {
            EditProcessFlag flag = CalcEditProcessFlag(currentTable, selectedOrders);
            if ((flag & EditProcessFlag.Audit) > 0)
                return "";

            StringBuilder msgs = new StringBuilder();
            msgs.AppendLine(ConstMessages.CheckAllIsNew);
            msgs.AppendLine(ConstMessages.CheckAudit);

            return FormatProcessErrorMessage(ConstNames.OpAudit, msgs.ToString());
        }

        private string CheckCanBeCancelled(OrderTable currentTable, Order[] selectedOrders)
        {
            EditProcessFlag flag = CalcEditProcessFlag(currentTable, selectedOrders);
            if ((flag & EditProcessFlag.Cancel) > 0)
                return "";

            StringBuilder msgs = new StringBuilder();
            msgs.AppendLine(ConstMessages.CheckAllIsAudited);
            msgs.AppendLine(ConstMessages.CheckCancel);

            return FormatProcessErrorMessage(ConstNames.OpCancelGroup, msgs.ToString());
        }

        private string CheckCanSetCeaseTime(OrderTable currentTable, Order[] selectedOrders, DateTime ceaseTime)
        {
            StringBuilder msgs = new StringBuilder();
            EditProcessFlag flag = CalcEditProcessFlag(currentTable, selectedOrders);
            if ((flag & EditProcessFlag.Cease) > 0)
            {
                if (ceaseTime < DateTime.Now)
                    msgs.AppendLine(ConstMessages.CheckCeaseDateBeforeNow);
                else
                {
                    foreach (Order order in selectedOrders)
                        if (order.StartDateTime >= ceaseTime)
                        {
                            msgs.AppendLine(ConstMessages.CheckCeaseDateBeforeStartDate);
                            break;
                        }
                    return "";
                }
            }
            else
                msgs.AppendLine(ConstMessages.CheckCeaseTimeIsNull);

            return FormatProcessErrorMessage(ConstNames.OpCease, msgs.ToString());
        }

        private static void CheckCanInsertOrdersToGroup(OrderTable currentTable, Order focusedOrder, Order[] insertOrders)
        {
            if ((focusedOrder.GroupPosFlag == GroupPositionKind.GroupMiddle) || (focusedOrder.GroupPosFlag == GroupPositionKind.GroupEnd))
            {
                // ֻ����ʹ��ֱ����ӵķ�ʽ�����в�����ҽ����ճ��������¶�������
                if ((insertOrders == null) || (insertOrders.Length == 0))
                    throw new DataCheckException(ConstMessages.CheckInsertRowInGroup, ConstMessages.ExceptionTitleOrderTable);
                if ((!CheckCommonPropertiesIsSame(new Order[] { focusedOrder, insertOrders[0] }))
                   || (!CheckCommonPropertiesIsSame(insertOrders)))
                    throw new DataCheckException(ConstMessages.CheckPropertyInGroup, ConstMessages.ExceptionTitleOrderTable);
                AttributeOfSelectedFlag flag = GetAttributeOfSelectedOrder(currentTable, insertOrders);
                if ((flag & AttributeOfSelectedFlag.AllIsHerbDruggery) > 0)
                {
                    if (focusedOrder.Content.Item.Kind != ItemKind.HerbalMedicine)
                        throw new DataCheckException(ConstMessages.CheckNotAllowInsertFerbInGroup, ConstMessages.ExceptionTitleOrderTable);
                }
                else if ((flag & AttributeOfSelectedFlag.AllIsOtherDruggery) > 0)
                {
                    if (focusedOrder.Content.Item.Kind == ItemKind.HerbalMedicine)
                        throw new DataCheckException(ConstMessages.CheckOnlyAllowInsertFerbInGroup, ConstMessages.ExceptionTitleOrderTable);
                }
                else
                    throw new DataCheckException(ConstMessages.CheckOnlyAllowDruggeryInGroup, ConstMessages.ExceptionTitleOrderTable);
            }
        }
        #endregion

        #region private methods of process logic
        private static void DoDeleteNewOrder(OrderTable table, Order[] selectedOrders)
        {
            bool inGroup = false;
            foreach (Order order in selectedOrders)
            {
                if (order == null)
                    throw new IndexOutOfRangeException(ConstMessages.ExceptionOrderNotFind);

                if (order.GroupPosFlag != GroupPositionKind.SingleOrder)
                    inGroup = true;

                table.RemoveOrder(order);
            }

            // ���ɾ�����ѷ����ҽ�������������÷���
            if (inGroup)
                DoReformingGroup(table);
        }

        private static void DoSetGroup(OrderTable currentTable, Order[] selectedOrders)
        {
            Order order;
            decimal groupSerialNo = -1;
            int endIndex = selectedOrders.Length - 1;
            for (int index = 0; index <= endIndex; index++)
            {
                order = selectedOrders[index];
                if (order == null)
                    throw new IndexOutOfRangeException(ConstMessages.ExceptionOrderNotFind);
                if (index == 0)
                {
                    order.GroupPosFlag = GroupPositionKind.GroupStart;
                    groupSerialNo = order.SerialNo;
                }
                else if (index == endIndex)
                    order.GroupPosFlag = GroupPositionKind.GroupEnd;
                else
                    order.GroupPosFlag = GroupPositionKind.GroupMiddle;
                // ������źͱ���ĵ�һ������һ��
                order.GroupSerialNo = groupSerialNo;
            }
            // ����ǶԲ�ҩ���з��飬��Ҫ�Զ�����һ����ϸ����
            if (selectedOrders[endIndex].Content.Item.Kind == ItemKind.HerbalMedicine)
                HandleHerbSummary(currentTable, selectedOrders[endIndex], true);
        }

        private static void DoCancelGroup(OrderTable table, Order[] selectedOrders)
        {
            // ���ѡ�м�¼���ڷ��飬�����Ϊ������¼�����������֯����
            foreach (Order order in selectedOrders)
            {
                if (order == null)
                    throw new IndexOutOfRangeException(ConstMessages.ExceptionOrderNotFind);
                if (order.GroupPosFlag != GroupPositionKind.SingleOrder)
                {
                    order.GroupPosFlag = GroupPositionKind.SingleOrder;
                    order.GroupSerialNo = order.SerialNo;
                }
            }
            DoReformingGroup(table);
        }

        private static void DoReformingGroup(OrderTable table)
        {
            // ��Ϊɾ���Ȳ����������ƻ���ԭ�еķ����������ʱ�����ܰ���ԭ�з�������������·���
            //OrderTableView currentView = table.DefaultView;

            //// ���ֲ���ֻ�ᷢ������ҽ���Ĵ�����
            //if ((currentView.State != OrderState.All)
            //   && (currentView.State != OrderState.New))
            //   return;

            // �����ͷ����β�ļ�¼��ɾ��������������������ͷ��β
            // ���շ��������¸��·������
            // ��Ҫ�����ҩ������Ϣ��
            //    ɾ����¼��ȡ������ʱ���ܻ��ƻ���ҩ��ϸ�ͻ�����Ϣ�Ķ�Ӧ��ϵ
            //    ������ҩ������ϢҪ�жϺ�ǰһ����¼�Ƿ�ƥ��
            //    ���³���ʱ��Ҫ����������������Ϣ
            int endHandle = table.Orders.Count - 1;// currentView.Count - 1;
            decimal serialOld = 0;
            decimal serialNew = 0;
            Collection<Order> needDelSummaries = new Collection<Order>(); // �ȴ�ɾ���Ĳ�ҩ������Ϣ
            Collection<Order> needInsertedSummaries = new Collection<Order>(); // �ȴ������ҩ������Ϣ�ķ����¼���һ����¼
            TextOrderContent textContent;
            Order temp;
            Order next;
            for (int index = 0; index < table.Orders.Count; index++)
            {
                temp = table.Orders[index];//temp = currentView[index].OrderCache;
                if (temp.State != OrderState.New)
                    continue;

                // ��������ҽ������û�м����飬���ô���
                if ((temp.State != OrderState.New)
                   || (temp.GroupPosFlag == GroupPositionKind.SingleOrder))
                    continue;

                // �ȼ�������Ϣ
                textContent = temp.Content as TextOrderContent;
                if ((textContent != null) && (textContent.IsSummaryOfHerbDetail))
                {
                    if ((index == 0)
                       || (table.Orders[index - 1].GroupSerialNo != textContent.GroupSerialNoOfLinkedHerbs)
                       || (table.Orders[index - 1].GroupPosFlag == GroupPositionKind.SingleOrder)
                       || (table.Orders[index - 1].Content.Item == null)
                       || (table.Orders[index - 1].Content.Item.Kind != ItemKind.HerbalMedicine))
                        needDelSummaries.Add(temp);
                    continue; // 
                }

                // ���·���ʱ����Ҫ����ǰ���¼��������д���
                if (index < endHandle)
                    next = table.Orders[index + 1]; // currentView[index + 1].OrderCache;
                else
                    next = null;

                // ���´����������������������ж��Ƿ���Ҫ���������Ϣ
                if (temp.GroupPosFlag == GroupPositionKind.GroupStart)
                {
                    // ������ͷ���������Ƿ���ͬ��ļ�¼��û�����Ϊ����
                    if ((next == null) || (next.GroupSerialNo != temp.GroupSerialNo))
                    {
                        temp.GroupPosFlag = GroupPositionKind.SingleOrder;
                        serialOld = -1;
                    }
                    else
                    {
                        serialOld = temp.GroupSerialNo;
                    }
                    serialNew = serialOld;
                }
                else if (temp.GroupPosFlag == GroupPositionKind.GroupEnd)
                {
                    // ������β�����ǰ���Ƿ���ͬ��ļ�¼��û�����Ϊ����
                    if (temp.GroupSerialNo != serialOld) // ��ǰһ��������Ų�һ�£���ʾ�ǲ�ͬ��
                    {
                        temp.GroupPosFlag = GroupPositionKind.SingleOrder;
                        serialOld = -1;
                        serialNew = temp.SerialNo;
                    }
                    else if (temp.GroupSerialNo != serialNew) // ����������·�����Ų�һ�£���ʾҪ�ı����
                    {
                        needInsertedSummaries.Add(temp);
                    }
                    // ���������¼�ķ�������Ǳ����(��Ϊ�������ֻʣ���Լ���������ͷ��ɾ�������м�ļ�¼��Ϊ��ͷ)
                    temp.GroupSerialNo = serialNew;
                }
                else if (temp.GroupPosFlag == GroupPositionKind.GroupMiddle)
                {
                    // �������м䣬���ǰ���Ƿ���ͬ��ļ�¼
                    // ��ǰ��û�����Ϊͷ������û�����Ϊβ����û�����Ϊ����
                    if (temp.GroupSerialNo != serialOld) // ǰ��û��ͬ��ļ�¼
                    {
                        temp.GroupPosFlag = GroupPositionKind.GroupStart;
                        serialOld = temp.GroupSerialNo;
                        serialNew = temp.SerialNo;
                    }
                    temp.GroupSerialNo = serialNew;

                    if ((next == null) || (next.GroupSerialNo != serialOld)) // ����û��ͬ���
                    {
                        if (temp.GroupPosFlag == GroupPositionKind.GroupStart)
                            temp.GroupPosFlag = GroupPositionKind.SingleOrder;
                        else
                        {
                            temp.GroupPosFlag = GroupPositionKind.GroupEnd;
                            needInsertedSummaries.Add(temp);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// �����ҩ������Ϣ
        /// </summary>
        /// <param name="currentTable"></param>
        /// <param name="herbDetail">������ҽ����ϸ��¼(���봫��������һ����¼)</param>
        /// <param name="addSummary">true:��ӻ���, false:ɾ������</param>
        private static void HandleHerbSummary(OrderTable currentTable, Order herbDetail, bool addSummary)
        {
            int index = currentTable.Orders.IndexOf(herbDetail.SerialNo);
            if (addSummary)
            {
                Order newSummary = currentTable.NewOrder();
                newSummary.BeginInit();

                newSummary.PatientId = herbDetail.PatientId;

                newSummary.OriginalWard = new Eop.Ward(herbDetail.OriginalWard.Code);
                newSummary.OriginalDepartment = new Eop.Department(herbDetail.OriginalDepartment.Code);

                if (newSummary.CreateInfo == null)
                    newSummary.CreateInfo = new OrderOperateInfo();
                newSummary.CreateInfo.SetPropertyValue(herbDetail.CreateInfo.Executor.Code, herbDetail.CreateInfo.ExecuteTime);

                newSummary.StartDateTime = herbDetail.StartDateTime;
                newSummary.Content = new TextOrderContent();
                // 
                newSummary.Content.EntrustContent = String.Format(Order.HerbSummaryFormat, 1, herbDetail.Content.ItemUsage, herbDetail.Content.ItemFrequency);
                newSummary.Memo = Order.HerbSummaryFlag + herbDetail.GroupSerialNo.ToString();
                newSummary.EndInit();
                // �����ܼ�¼�������һ����ϸ����
                currentTable.InsertOrderAt(newSummary, index + 1);
                //add by zhouhui ������ϢҪ����������ͨҽ��һ������Ҫ���´����������Ϣ
                newSummary.Content.ProcessCreateOutputeInfo = new OrderContent.GenerateOutputInfo(CustomDrawOperation.CreateOutputeInfo);
            }
            else
            {
                TextOrderContent textContent = currentTable.Orders[index + 1].Content as TextOrderContent;
                if ((textContent != null) && (textContent.IsSummaryOfHerbDetail))
                    currentTable.RemoveOrderAt(index + 1);
            }
        }

        private void DoAutoCeaseLongOrder(string ceasor, DateTime ceaseTime, OrderCeaseReason ceaseReason)
        {
            if (ceaseTime == DateTime.MinValue)
                throw new DataCheckException(String.Format(ConstMessages.ExceptionFormatNoValue, ConstNames.TimeOfCease), OrderView.UNCeaseDate);

            foreach (LongOrder order in m_LongTable.Orders)
            {
                if ((order.State == OrderState.Audited) || (order.State == OrderState.Executed))
                {
                    // --ֻ��δ����ֹͣʱ���ǰֹͣʱ������Զ�ͣ��ʱ�������¸���
                    // --(��������ֹͣ��Ϣ��ҽ����Ӧ���޸�ֹͣʱ�䣬Ӧ����ҽ��ִ�е�ʱ��ֱ���޸�״̬������)
                    // �������ֹͣʱ�䣬��ֹͣʱ������ת��ִ��ʱ�䣬�������ڵ�ҽ������ģʽ�²��ܽ���Щҽ��ͣ��ת��ʱ��
                    // ���Զ���Щҽ����ֹͣʱ��ҲҪ�޸ģ���������ݿ��е�ֹͣʱ����Ѵ�ӡ��ʱ�䲻һ�£�
                    if ((order.CeaseInfo == null) || (!order.CeaseInfo.HadInitialized)
                       || (order.CeaseInfo.ExecuteTime > ceaseTime))
                    {
                        if ((order.CeaseInfo != null) && (order.CeaseInfo.HadInitialized))
                            order.Memo = order.CeaseInfo.ToString(); // ��ԭʼ��ֹͣ��Ϣ������Memo��
                        order.CeaseOrder(ceasor, ceaseTime, ceaseReason);
                    }
                }
            }
        }

        private void DoCeaseGeneralOrder(ItemKind generalKind, string ceasor, DateTime ceaseTime)
        {
            LongOrder order;
            for (int index = m_LongTable.Orders.Count - 1; index >= 0; index--)
            {
                order = m_LongTable.Orders[index] as LongOrder;
                if ((order.State == OrderState.Executed)
                   && (order.Content.OrderKind == OrderContentKind.GeneralItem)
                   && (order.Content.Item.Kind == generalKind))
                {
                    order.CeaseOrder(ceasor, ceaseTime, OrderCeaseReason.NewGeneral);
                    break; // ��ͬ���͵ĳ���ҽ�������ֻ����һ��������ִ��״̬
                }
            }
        }

        private void DoAuditTempOrder(OrderTable currentTable, Order[] selectedTemps, string auditor, DateTime auditTime)
        {
            TempOrder temp;
            TextOrderContent textContent;
            Collection<decimal> linkHerbDetails = new Collection<decimal>(); // �����Ĳ�ҩ��ϸ�ķ������
            foreach (Order order in selectedTemps)
            {
                temp = order as TempOrder;
                if (temp == null)
                    throw new IndexOutOfRangeException(ConstMessages.ExceptionOrderNotFind);
                if (order.State != OrderState.New) // ֻ�����״̬Ϊ������ҽ��
                    continue;

                switch (order.Content.OrderKind)
                {
                    // ���������ҽ������ֱ�ӽ�״̬��Ϊ��ִ��
                    case OrderContentKind.TextNormal:
                    case OrderContentKind.TextShiftDept:
                    case OrderContentKind.TextLeaveHospital:
                    case OrderContentKind.TextAfterOperation:
                        temp.ExecuteOrder(auditor, auditTime);
                        break;
                    default:
                        // ����ҽ�������ʱ�䡢����ˣ�������״̬
                        temp.AuditOrder(auditor, auditTime);
                        break;
                }
                // ����ǲ�ҩ������Ϣ����ͬʱ�����ص���ϸ��¼(ĿǰHIS��ҽ���в������ҩ��ϸ)
                textContent = temp.Content as TextOrderContent;
                if ((textContent != null) && (textContent.IsSummaryOfHerbDetail))
                    linkHerbDetails.Add(textContent.GroupSerialNoOfLinkedHerbs);
            }

            DoAutoAuditHerbDetailOrders(currentTable, linkHerbDetails, auditor, auditTime);
        }

        private void DoAuditLongOrder(OrderTable currentTable, Order[] selectedLongs, string auditor, DateTime auditTime)
        {
            // 1 ��������ֶ�
            DateTime autoCeaseTime = DateTime.MinValue;
            OrderCeaseReason ceaseReason = OrderCeaseReason.None;

            LongOrder temp;
            Order tendOrder = null; // ������
            Order dangerOrder = null; // Σ�ؼ���
            TextOrderContent textContent;
            Collection<decimal> linkHerbDetails = new Collection<decimal>(); // �����Ĳ�ҩ��ϸ�ķ������
            foreach (Order order in selectedLongs)
            {
                temp = order as LongOrder;
                if (temp == null)
                    throw new IndexOutOfRangeException(ConstMessages.ExceptionOrderNotFind);

                // ����ҽ�������ʱ�䡢����ˣ�������״̬
                if (order.State == OrderState.New) // ����
                    temp.AuditOrder(auditor, auditTime);
                else if (order.State == OrderState.Executed) // ��ִ��״̬�����Ӧ�������ֹͣ��Ϣ
                    temp.AuditCeaseOrder(auditor, auditTime);
                else
                    continue;

                // ͣ��Ӧ�ĳ���ҽ��
                if (temp.Content.OrderKind == OrderContentKind.GeneralItem)
                {
                    DoCeaseGeneralOrder(temp.Content.Item.Kind, auditor, temp.StartDateTime);
                    if (temp.Content.Item.Kind == ItemKind.DangerLevel)
                        dangerOrder = temp;
                    else if (temp.Content.Item.Kind == ItemKind.Care)
                        tendOrder = temp;
                }
                // ���󡢲���Ҫͣ��Ч�ĳ���ҽ��
                else if (temp.Content.OrderKind == OrderContentKind.TextAfterOperation)
                {
                    // ����ҽ��ͣ������Ŀ�ʼʱ��
                    autoCeaseTime = temp.StartDateTime;
                    ceaseReason = OrderCeaseReason.AfterOperation;
                }
                else
                {
                    // ����ǲ�ҩ������Ϣ����ͬʱ�����ص���ϸ��¼(ĿǰHIS��ҽ���в������ҩ��ϸ)
                    textContent = temp.Content as TextOrderContent;
                    if ((textContent != null) && (textContent.IsSummaryOfHerbDetail))
                        linkHerbDetails.Add(textContent.GroupSerialNoOfLinkedHerbs);
                }
            }
            // 2 ͣ����ҽ��
            // ���󡢲���Ҫͣ��Ч�ĳ���ҽ��(����˵�ҽ����ҲҪ)
            // ���ҽ��״̬�Ƿ����Ϊ��ֹͣ
            // �������˵�ҽ����ֹͣʱ���Ƿ񳬹���Ժ��ת�Ƶ�ʱ��
            if (autoCeaseTime > DateTime.MinValue)
                DoAutoCeaseLongOrder(auditor, autoCeaseTime, ceaseReason);

            DoAutoAuditHerbDetailOrders(currentTable, linkHerbDetails, auditor, auditTime);

            // 4 ִ�ж�����Զ���������
            if ((dangerOrder != null) || (tendOrder != null))
                SetCurrentPatientDangerAndTendLevel(dangerOrder, tendOrder);
        }

        private void DoAutoAuditHerbDetailOrders(OrderTable currentTable, Collection<decimal> linkHerbDetails, string auditor, DateTime auditTime)
        {
            if (linkHerbDetails.Count > 0)
            {
                for (int index = currentTable.Orders.Count - 1; index >= 0; index--)
                {
                    if ((currentTable.Orders[index].State == OrderState.Executed)
                       || (currentTable.Orders[index].State == OrderState.Ceased))
                        break;
                    if (currentTable.Orders[index].State != OrderState.New)
                        continue;

                    if (linkHerbDetails.Contains(currentTable.Orders[index].GroupSerialNo))
                        currentTable.Orders[index].AuditOrder(auditor, auditTime);
                }
            }
        }

        private void SetCurrentPatientDangerAndTendLevel(Order dangerOrder, Order tendOrder)
        {
            string updateCmd = null;
            ItemBase item;
            if (dangerOrder != null) // Σ�ؼ���ı�ʱҪ������Ӧ��Ϣ
            {

                item = dangerOrder.Content.Item;
                updateCmd = ConstSchemaNames.InpatientColDangerLevel + " = '" + item.Memo.Trim() + "'"; // ��ע�д�ŵ��Ƕ�Ӧ��Σ�ؼ������

                if (CurrentPatient.PatientCondition == null)
                    CurrentPatient.ReInitializeProperties();
                CurrentPatient.PatientCondition.Code = item.Memo.Trim();
                CurrentPatient.PatientCondition.Name = item.Name;
            }

            if (tendOrder != null)
            {
                item = tendOrder.Content.Item;
                if (String.IsNullOrEmpty(updateCmd))
                    updateCmd = ConstSchemaNames.InpatientColCareLevel + " = '" + item.Code.Trim() + "'";
                else
                    updateCmd += " ," + ConstSchemaNames.InpatientColCareLevel + " = '" + item.Code.Trim() + "'";

                if (CurrentPatient.PatientCondition == null)
                    CurrentPatient.ReInitializeProperties();
                CurrentPatient.InfoOfAdmission.CareLevel.Code = item.Code;
                CurrentPatient.InfoOfAdmission.CareLevel.Name = item.Name;
            }

            if (!String.IsNullOrEmpty(updateCmd))
            {
                // �ȸ���EMR�в��˵�Σ�غͻ�����
                m_SqlExecutor.ExecuteNoneQuery(String.Format(CultureInfo.CurrentCulture
                   , ConstSqlSentences.FormatUpdateInpatient, updateCmd, CurrentPatient.NoOfFirstPage));

                // TODO�ٸ���HIS�в��˵�Σ�غͻ�����
                //DataTable changedPatient = m_SqlExecutor.ExecuteDataTable(
                //   String.Format(CultureInfo.CurrentCulture, ConstSqlSentences.FormatSelectInpatient, CurrentPatient.NoOfFirstPage));
                //MemoryStream source = new MemoryStream();
                //changedPatient.WriteXml(source, XmlWriteMode.WriteSchema);
                //source.Seek(0, SeekOrigin.Begin);
                //byte[] data = new byte[source.Length];
                //source.Read(data, 0, (int)source.Length);

                //string[,] parameters = new string[3, 1];
                //parameters[0, 0] = "changeData";
                //parameters[1, 0] = "System.Byte[]";
                //parameters[2, 0] = Convert.ToBase64String(data);

                //string sExio = m_InfoServer.BuildExchangeInfoString(ExchangeInfoOrderConst.MsgUpdatePatient, ExchangeInfoOrderConst.EmrSystemName
                //   , ExchangeInfoOrderConst.HisSystemName,
                //    parameters);

                //string outMsg;
                //if (m_InfoServer.AddSyncExchangeInfo(sExio, out outMsg) != ResponseFlag.Complete)
                //    throw new ApplicationException(outMsg);
            }
        }

        private void DoCancelOrder(OrderTable currentTable, Order[] selectedOrders, string cancellor, DateTime cancelTime)
        {
            if (cancelTime == DateTime.MinValue)
                throw new DataCheckException(String.Format(ConstMessages.ExceptionFormatNoValue, ConstNames.TimeOfCancel), ConstNames.TimeOfCancel);

            TextOrderContent textContent;
            Collection<decimal> linkHerbDetails = new Collection<decimal>(); // �����Ĳ�ҩ��ϸ�ķ������

            foreach (Order order in selectedOrders)
            {
                if (order.State == OrderState.Audited)
                    order.CancelOrder(cancellor, cancelTime);

                // ����ǲ�ҩ������Ϣ����ͬʱ�����ص���ϸ��¼(ĿǰHIS��ҽ���в������ҩ��ϸ)
                textContent = order.Content as TextOrderContent;
                if ((textContent != null) && (textContent.IsSummaryOfHerbDetail))
                    linkHerbDetails.Add(textContent.GroupSerialNoOfLinkedHerbs);
            }

            if (linkHerbDetails.Count > 0)
            {
                for (int index = currentTable.Orders.Count - 1; index >= 0; index--)
                {
                    if (currentTable.Orders[index].State != OrderState.Audited)
                        continue;
                    else if (linkHerbDetails.Contains(currentTable.Orders[index].GroupSerialNo))
                        currentTable.Orders[index].CancelOrder(cancellor, cancelTime);
                }
            }
        }

        private void DoSetLongOrderCeaseInfo(Order[] selectedOrders, string ceasor, DateTime ceaseTime)
        {
            LongOrder longOrder;
            foreach (Order order in selectedOrders)
            {
                longOrder = order as LongOrder;
                //modified by zhouhui �����ǰ�ĳ���û�����֮ǰ���������޸�ֹͣʱ���
                //TODO:?�������������޸�ֹͣʱ���������ҽ����ӡ����
                //if ((longOrder != null) && ((longOrder.CeaseInfo == null) || (!longOrder.CeaseInfo.HadInitialized)))
                if ((longOrder != null) && (longOrder.State != OrderState.Ceased))
                {
                    if (ceaseTime < longOrder.StartDateTime)
                        continue;
                    longOrder.CeaseOrder(ceasor, ceaseTime, OrderCeaseReason.Natural);
                    // ͬ���һ��ֹͣ
                    if (longOrder.GroupPosFlag != GroupPositionKind.SingleOrder)
                    {
                        Collection<Order> groupOrders = m_LongTable.GetOtherOrdersOfSameGroup(order);
                        foreach (Order gpOrder in groupOrders)
                        {
                            longOrder = gpOrder as LongOrder;
                            if ((longOrder != null) && ((longOrder.CeaseInfo == null) || (!longOrder.CeaseInfo.HadInitialized)))
                                longOrder.CeaseOrder(ceasor, ceaseTime, OrderCeaseReason.Natural);
                        }
                    }
                }
            }
        }

        private void DoSaveChangedData(OrderTable currentTable, Order[] changedOrders, string executorCode, string macAddress, bool callByEditor, bool forceSend, bool autoDeleteNewOrder)
        {
            //return DoSaveChangedData(currentTable, changedOrders, executorCode, macAddress, callByEditor, forceSend, autoDeleteNewOrder, false, null, DateTime.MinValue);

            if ((changedOrders == null) || (changedOrders.Length == 0))
                return;

            if (callByEditor) // ��ҽ���༭���е���ʱ�Ÿ��´�����Ϣ
            {
                DateTime createTime = DateTime.Now;
                foreach (Order order in changedOrders)
                {
                    if ((order.State == OrderState.New) && (order.EditState != OrderEditState.Deleted))
                        order.CreateInfo.ExecuteTime = createTime;
                }
            }
            DataTable changedTable = currentTable.SyncObjectData2Table(changedOrders, autoDeleteNewOrder);

            DataTable currentChangedTable;
            try
            {
                currentChangedTable = DoSaveDataToEmr(currentTable, changedTable, autoDeleteNewOrder);
            }
            catch (Exception e)
            {
                currentTable.OrderDataTable.RejectChanges(); // ȡ����DataTable�������޸�
                throw e;
            }

            // ���ر���ɹ����������ڴ������ݵĸı��־
            currentTable.AcceptChanges();

            if (autoDeleteNewOrder) // ֻ������ɾ����ҽ��ʱǿ��ˢ����ҽ����ҽ����ţ������������(��Ϊ��֪����ζ�Ӧ����ҽ�������)
                SynchSerialNoOfNewOrder(currentTable, currentChangedTable);

            if (forceSend || ((CoreBusinessLogic.BusinessLogic.AutoSyncData) && (currentChangedTable != null)))
            {
                SendOrderDataToHis(currentTable, currentChangedTable, executorCode, macAddress);
            }
        }

        //private DataTable DoSaveChangedData(OrderTable currentTable, Order[] changedOrders, string executorCode, string macAddress, bool callByEditor, bool forceSend, bool autoDeleteNewOrder, bool saveSendLog, string fireCode, DateTime fireTime)
        //{
        //   if ((changedOrders == null) || (changedOrders.Length == 0))
        //      return;

        //   if (callByEditor) // ��ҽ���༭���е���ʱ�Ÿ��´�����Ϣ
        //   {
        //      DateTime createTime = DateTime.Now;
        //      foreach (Order order in changedOrders)
        //      {
        //         if ((order.State == OrderState.New) && (order.EditState != OrderEditState.Deleted))
        //            order.CreateInfo.ExecuteTime = createTime;
        //      }
        //   }
        //   DataTable changedTable = currentTable.SyncObjectData2Table(changedOrders, autoDeleteNewOrder);

        //   DataTable currentChangedTable;
        //   try
        //   {
        //      currentChangedTable = DoSaveDataToEmr(currentTable, changedTable, autoDeleteNewOrder);
        //   }
        //   catch (Exception e)
        //   {
        //      currentTable.OrderDataTable.RejectChanges(); // ȡ����DataTable�������޸�
        //      throw e;
        //   }

        //   // ���ر���ɹ����������ڴ������ݵĸı��־
        //   currentTable.AcceptChanges();

        //   if (autoDeleteNewOrder) // ֻ������ɾ����ҽ��ʱǿ��ˢ����ҽ����ҽ����ţ������������(��Ϊ��֪����ζ�Ӧ����ҽ�������)
        //      SynchSerialNoOfNewOrder(currentTable, currentChangedTable);

        //   if (forceSend || ((CoreBusinessLogic.BusinessLogic.AutoSyncData) && (currentChangedTable != null)))
        //   {
        //      //�ڷ��͵�HISǰ��¼��־
        //      if (saveSendLog)
        //         m_SynchLogHelper.SaveRecord(currentChangedTable, currentTable.IsTempOrder, fireCode, fireTime);

        //      SendOrderDataToHis(currentTable, currentChangedTable, executorCode, macAddress);
        //   }
        //   return currentChangedTable;
        //}

        //yxy
        private DataTable DoSaveDataToEmr(OrderTable currentTable, DataTable changedTable, bool autoDeleteNewOrder)
        {
            if ((changedTable != null) && (changedTable.Rows.Count > 0))
            {
                string tableName = GetOrderTableName(currentTable.IsTempOrder);

                // �����Ҫ����ɾ�����ݿ��������״̬��ҽ��(һ���Ǳ༭�����ñ��淽���Żᷢ��)���������⴦��
                if (autoDeleteNewOrder)
                {
                    // ����ǰ����ɾ�����ݿ�������״̬��ҽ��
                    //    ��Ϊ�����û��ͬ��ҽ����ţ������ٴα���ʱ���ڴ��е����ȥɾ�����ݻ��������
                    //    ���⣬�ڱ༭���б༭��ҽ��ʱ�����ܻ�ı�ԭʼ��˳������Ҫͨ�����²���ķ�ʽ��֤��ҽ�������˳�򲻻����

                    // ����ɾ������ǰ���ȼ�鱻ɾ������ҽ�������ݿ��е�״̬
                    //    ���������״̬Ϊ�����������ɾ���ٲ���
                    //    ���������״̬��Ϊ����������ɾ�������룬���ܱ��棬����ˢ������
                    //    �������Ų���ָ����Χ�ڵ�����ҽ����˵�����ܿ������뵥������ִ��ɾ������������ˢ������
                    OrderState state;
                    string serialNoField = GetSerialNoField(currentTable.IsTempOrder);

                    DataViewRowState oldRowState = changedTable.DefaultView.RowStateFilter;
                    // �����ɾ����ҽ������ţ���Ϊ��ѯ����
                    changedTable.DefaultView.RowStateFilter = DataViewRowState.Deleted;
                    StringBuilder deletedSerials = new StringBuilder("0, ");
                    DataRowView rowView;
                    for (int index = changedTable.DefaultView.Count - 1; index >= 0; index--)
                    {
                        rowView = changedTable.DefaultView[index];
                        state = (OrderState)Convert.ToInt32(rowView[ConstSchemaNames.OrderColState]);
                        if (state == OrderState.New)
                        {
                            deletedSerials.Append(rowView[serialNoField] + ",");
                            changedTable.Rows.Remove(rowView.Row);
                        }
                    }
                    deletedSerials.Append(" 0");

                    //object num = m_SqlExecutor.ExecuteScalar(String.Format(CultureInfo.CurrentCulture
                    //      , ConstSqlSentences.FormatSelectChangedOrderData
                    //      , tableName, CurrentPatient.NoOfFirstPage, OrderState.New, serialNoField, deletedSerials.ToString()));
                    //if (num != null) {
                    //    if (Convert.ToInt32(num) > 0)
                    //        throw new DataCheckException(ConstMessages.CheckOrderStateBeforeSave, ConstMessages.ExceptionTitleOrder);
                    //}

                    changedTable.DefaultView.RowStateFilter = oldRowState;
                    m_SqlExecutor.ExecuteNoneQuery(String.Format(CultureInfo.CurrentCulture
                          , ConstSqlSentences.FormatDeleteNewOrder
                          , tableName, CurrentPatient.NoOfFirstPage, OrderState.New));
                }

                //// ����ǰ�ȵ���HIS��ҽ�����
                //if (CoreBusinessLogic.BusinessLogic.ConnectToHis)
                //{
                //   // ���ݼ�ת����byte����
                //   MemoryStream source = new MemoryStream();
                //   changedTable.WriteXml(source, XmlWriteMode.WriteSchema);
                //   source.Seek(0, SeekOrigin.Begin);
                //   byte[] data = new byte[source.Length];
                //   source.Read(data, 0, (int)source.Length);

                //   CallDoctorAdviceService(ExchangeInfoOrderConst.MsgCheckData, data, executorCode, macAddress, currentTable.IsTempOrder);

                //}
                //yxy
                //m_SqlExecutor.UpdateTable(changedTable, tableName, false);
                DoUpdateOrder(changedTable, tableName, false);


                // �ֹ����ø��·�����ŵĴ洢����
                SqlParameter[] paras = new SqlParameter[]{
               new SqlParameter(ConstSchemaNames.ProcParaFirstpageNo, SqlDbType.Decimal)
               , new SqlParameter(ConstSchemaNames.ProcParaOrderKind, SqlDbType.Int)
               , new SqlParameter(ConstSchemaNames.ProcParaOnlyNew, SqlDbType.Int)
            };
                paras[0].Value = CurrentPatient.NoOfFirstPage;
                if (currentTable.IsTempOrder)
                    paras[1].Value = 0;
                else
                    paras[1].Value = 1;
                paras[2].Value = 1;
                m_SqlExecutor.ExecuteNoneQuery(ConstSchemaNames.ProcUpdateSerialNo, paras, CommandType.StoredProcedure);

                return changedTable;
            }
            else
                return null;
        }

        /// <summary>
        /// �����޸Ĺ���ҽ����Oracle���ݿ���
        /// </summary>
        /// <param name="changerTable"></param>
        /// <param name="tableName"></param>
        /// <param name="needUpdateSchema"></param>
        private void DoUpdateOrder(DataTable changerTable, string tableName, bool needUpdateSchema)
        {
            if (changerTable == null || changerTable.Rows.Count == 0)
                return;
            if (tableName == "Long_Order")
            {
                DoUpdateLong_Order(changerTable);
            }
            else if (tableName == "Temp_Order")
            {
                DoUpdateTemp_Order(changerTable);
            }
        }

        #region ��ҽ����Ϣ���浽���ݿ���
        /// <summary>
        /// ���ݴ���ĳ���ҽ��������ҽ����Ϣ���浽���ݿ���
        /// </summary>
        /// <param name="changerTable"></param>
        private void DoUpdateLong_Order(DataTable changerTable)
        {
            if (changerTable == null || changerTable.Rows.Count == 0)
                return;
            string editType;
            foreach (DataRow dr in changerTable.Rows)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    editType = "1";

                    SqlParameter[] sqlParam = new SqlParameter[] 
                {
                    new SqlParameter("@EditType",SqlDbType.VarChar),
                    new SqlParameter("@LONGID",SqlDbType.Int),
                    new SqlParameter("@NOOFINPAT",SqlDbType.Int),
                    new SqlParameter("@GROUPID",SqlDbType.Int),
                    new SqlParameter("@GROUPFLAG",SqlDbType.Int),
                    new SqlParameter("@WARDID",SqlDbType.VarChar),

                    new SqlParameter("@DEPTID",SqlDbType.VarChar),
                    new SqlParameter("@TYPEDOCTOR",SqlDbType.VarChar),
                    new SqlParameter("@TYPEDATE",SqlDbType.VarChar),
                    new SqlParameter("@AUDITOR",SqlDbType.VarChar),
                    new SqlParameter("@DATEOFAUDIT",SqlDbType.VarChar),

                    new SqlParameter("@EXECUTOR",SqlDbType.VarChar),
                    new SqlParameter("@EXECUTEDATE",SqlDbType.VarChar),
                    new SqlParameter("@CANCELDOCTOR",SqlDbType.VarChar),
                    new SqlParameter("@CANCELDATE",SqlDbType.VarChar),
                    new SqlParameter("@CEASEDCOCTOR",SqlDbType.VarChar),

                    new SqlParameter("@CEASEDATE",SqlDbType.VarChar),
                    new SqlParameter("@CEASENURSE",SqlDbType.VarChar),
                    new SqlParameter("@CEASEADUDITDATE",SqlDbType.VarChar),
                    new SqlParameter("@STARTDATE",SqlDbType.VarChar),
                    new SqlParameter("@TOMORROW",SqlDbType.Int),

                    new SqlParameter("@PRODUCTNO",SqlDbType.Int),
                    new SqlParameter("@NORMNO",SqlDbType.Int),
                    new SqlParameter("@MEDICINENO",SqlDbType.Int),
                    new SqlParameter("@DRUGNO",SqlDbType.VarChar),
                    new SqlParameter("@DRUGNAME",SqlDbType.VarChar),

                    new SqlParameter("@DRUGNORM",SqlDbType.VarChar),
                    new SqlParameter("@ITEMTYPE",SqlDbType.Int),
                    new SqlParameter("@MINUNIT",SqlDbType.VarChar),
                    new SqlParameter("@DRUGDOSE",SqlDbType.Int),
                    new SqlParameter("@DOSEUNIT",SqlDbType.VarChar),

                    new SqlParameter("@UNITRATE",SqlDbType.Int),
                    new SqlParameter("@UNITTYPE",SqlDbType.Int),
                    new SqlParameter("@DRUGUSE",SqlDbType.VarChar),
                    new SqlParameter("@BATCHNO",SqlDbType.VarChar),
                    new SqlParameter("@EXECUTECOUNT",SqlDbType.Int),

                    new SqlParameter("@EXECUTECYCLE",SqlDbType.Int),
                    new SqlParameter("@CYCLEUNIT",SqlDbType.Int),
                    new SqlParameter("@DATEOFWEEK",SqlDbType.VarChar),
                    new SqlParameter("@INNEREXECUTETIME",SqlDbType.VarChar),
                    new SqlParameter("@EXECUTEDEPT",SqlDbType.VarChar),

                    new SqlParameter("@ENTRUST",SqlDbType.VarChar),
                    new SqlParameter("@ORDERTYPE",SqlDbType.Int),
                    new SqlParameter("@ORDERSTATUS",SqlDbType.Int),
                    new SqlParameter("@SPECIALMARK",SqlDbType.Int),
                    new SqlParameter("@CEASEREASON",SqlDbType.Int),

                    new SqlParameter("@CURGERYID",SqlDbType.Int),
                    new SqlParameter("@CONTENT",SqlDbType.VarChar),
                    new SqlParameter("@SYNCH",SqlDbType.Int),
                    new SqlParameter("@MEMO",SqlDbType.VarChar),
                    new SqlParameter("@DJFL",SqlDbType.VarChar)
 
                };

                    sqlParam[0].Value = editType;
                    for (int i = 0; i < changerTable.Columns.Count; i++)
                    {
                        sqlParam[i + 1].Value = dr[i];
                    }

                    try
                    {
                        m_SqlExecutor.ExecuteDataSet("usp_EditEmrLONG_ORDER", sqlParam, CommandType.StoredProcedure).Tables[0].Rows[0][0].ToString();
                    }
                    catch (Exception e)
                    {
                        ;
                    }
                }
            }
        }

        private void DoUpdateTemp_Order(DataTable changerTable)
        {
            if (changerTable == null || changerTable.Rows.Count == 0)
                return;
            string editType;
            foreach (DataRow dr in changerTable.Rows)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    editType = "1";

                    SqlParameter[] sqlParam = new SqlParameter[] 
                {
                    new SqlParameter("@EditType",SqlDbType.VarChar),
                    new SqlParameter("@TEMPID",SqlDbType.Int),
                    new SqlParameter("@NOOFINPAT",SqlDbType.Int),
                    new SqlParameter("@GROUPID",SqlDbType.Int),
                    new SqlParameter("@GROUPFLAG",SqlDbType.Int),
                    new SqlParameter("@WARDID",SqlDbType.VarChar),

                    new SqlParameter("@DEPTID",SqlDbType.VarChar),
                    new SqlParameter("@TYPEDOCTOR",SqlDbType.VarChar),
                    new SqlParameter("@TYPEDATE",SqlDbType.VarChar),
                    new SqlParameter("@AUDITOR",SqlDbType.VarChar),
                    new SqlParameter("@DATEOFAUDIT",SqlDbType.VarChar),

                    new SqlParameter("@EXECUTOR",SqlDbType.VarChar),
                    new SqlParameter("@EXECUTEDATE",SqlDbType.VarChar),
                    new SqlParameter("@CANCELDOCTOR",SqlDbType.VarChar),
                    new SqlParameter("@CANCELDATE",SqlDbType.VarChar),
                    new SqlParameter("@STARTDATE",SqlDbType.VarChar),

                    new SqlParameter("@PRODUCTNO",SqlDbType.Int),
                    new SqlParameter("@NORMNO",SqlDbType.Int),
                    new SqlParameter("@MEDICINENO",SqlDbType.Int),
                    new SqlParameter("@DRUGNO",SqlDbType.VarChar),
                    new SqlParameter("@DRUGNAME",SqlDbType.VarChar),

                    new SqlParameter("@DRUGNORM",SqlDbType.VarChar),
                    new SqlParameter("@ITEMTYPE",SqlDbType.Int),
                    new SqlParameter("@MINUNIT",SqlDbType.VarChar),
                    new SqlParameter("@DRUGDOSE",SqlDbType.Int),
                    new SqlParameter("@DOSEUNIT",SqlDbType.VarChar),

                    new SqlParameter("@UNITRATE",SqlDbType.Int),
                    new SqlParameter("@UNITTYPE",SqlDbType.Int),
                    new SqlParameter("@DRUGUSE",SqlDbType.VarChar),
                    new SqlParameter("@BATCHNO",SqlDbType.VarChar),
                    new SqlParameter("@EXECUTECOUNT",SqlDbType.Int),

                    new SqlParameter("@EXECUTECYCLE",SqlDbType.Int),
                    new SqlParameter("@CYCLEUNIT",SqlDbType.Int),
                    new SqlParameter("@DATEOFWEEK",SqlDbType.VarChar),
                    new SqlParameter("@INNEREXECUTETIME",SqlDbType.VarChar),
                    new SqlParameter("@ZXTS",SqlDbType.Int),

                    new SqlParameter("@TOTALDOSE",SqlDbType.Int),
                    new SqlParameter("@ENTRUST",SqlDbType.VarChar),
                    new SqlParameter("@ORDERTYPE",SqlDbType.Int),
                    new SqlParameter("@ORDERSTATUS",SqlDbType.Int),
                    new SqlParameter("@SPECIALMARK",SqlDbType.Int),

                    new SqlParameter("@CEASEID",SqlDbType.Int),
                    new SqlParameter("@CEASEDATE",SqlDbType.VarChar),
                    new SqlParameter("@CONTENT",SqlDbType.VarChar),
                    new SqlParameter("@SYNCH",SqlDbType.Int),
                    new SqlParameter("@MEMO",SqlDbType.VarChar),

                    new SqlParameter("@FORMTYPE",SqlDbType.VarChar)
 
                };

                    sqlParam[0].Value = editType;
                    for (int i = 0; i < changerTable.Columns.Count; i++)
                    {
                        sqlParam[i + 1].Value = dr[i];
                    }

                    try
                    {
                        m_SqlExecutor.ExecuteDataSet("usp_EditEmrTEMP_ORDER", sqlParam, CommandType.StoredProcedure).Tables[0].Rows[0][0].ToString();
                    }
                    catch (Exception e)
                    {
                        ;
                    }
                }
            }
        }
        #endregion


        private void DoManualSynchDataToHis(OrderTable currentTable, string executorCode, string macAddress)
        {
            // ȡ��ͬ����־Ϊ0�����ݣ����浽HIS
            DataTable changedData = m_SqlExecutor.ExecuteDataTable(String.Format(CultureInfo.CurrentCulture
                     , ConstSqlSentences.FormatSelectNotSynchedOrderData
                     , GetOrderTableName(currentTable.IsTempOrder)
                     , CurrentPatient.NoOfFirstPage
                     , GetSerialNoField(currentTable.IsTempOrder)));
            // ���жϱ仯�ļ�¼������Ϊ����û�иĹ�ҽ����ֻ��ɾ������ҽ������ʱ��EMR�õ��ı�����ݼ��ǿյ�
            //if (changedData.Rows.Count > 0)
            //{
            DataTable table;
            if (currentTable.IsTempOrder)
                table = TableOfSynchTempOrder;
            else
                table = TableOfSynchLongOrder;
            table.Clear();
            table.Merge(changedData);
            SendOrderDataToHis(currentTable, table, executorCode, macAddress);
            //}
        }

        private void SendOrderDataToHis(OrderTable currentTable, DataTable changedTable, string executorCode, string macAddress)
        {
            if (CoreBusinessLogic.BusinessLogic.ConnectToHis)
            {
                // ���ݼ�ת����byte����
                MemoryStream source = new MemoryStream();
                changedTable.WriteXml(source, XmlWriteMode.WriteSchema);
                source.Seek(0, SeekOrigin.Begin);
                byte[] data = new byte[source.Length];
                source.Read(data, 0, (int)source.Length);

                // 3 ���ýӿڼ������
                CallDoctorAdviceService(ExchangeInfoOrderConst.MsgCheckData, data, executorCode, macAddress, currentTable.IsTempOrder);

                // 4 ���ýӿ�ͬ������
                CallDoctorAdviceService(ExchangeInfoOrderConst.MsgSaveData, data, executorCode, macAddress, currentTable.IsTempOrder);

                // ͬ���ɹ��������ͬ����־
                UpdateSynchFlagToTrue(currentTable, changedTable);

                currentTable.AcceptChanges();
            }
        }

        private void SynchSerialNoOfNewOrder(OrderTable currentTable, DataTable changedTable)
        {
            string oldRowFilter = changedTable.DefaultView.RowFilter;

            changedTable.DefaultView.RowFilter = String.Format("{1} = {0:D}", OrderState.New, ConstSchemaNames.OrderColState);// .RowStateFilter = DataViewRowState.CurrentRows;
            if (changedTable.DefaultView.Count == 0)
                return;

            // �����ͬ����ҽ������źͷ������
            string serialNoField = GetSerialNoField(currentTable.IsTempOrder);
            IDataReader serialNoReader = m_SqlExecutor.ExecuteReader(
               String.Format(CultureInfo.CurrentCulture
                  , ConstSqlSentences.FormatSelectSerialNosOfNewSynchedOrder
                  , serialNoField, GetOrderTableName(currentTable.IsTempOrder)
                  , CurrentPatient.NoOfFirstPage, OrderState.New));

            List<decimal> trueSerialNos = new List<decimal>();
            List<decimal> trueGroupNos = new List<decimal>();
            while (serialNoReader.Read())
            {
                trueSerialNos.Add(Convert.ToDecimal(serialNoReader[serialNoField]));
                trueGroupNos.Add(Convert.ToDecimal(serialNoReader[ConstSchemaNames.OrderColGroupSerialNo]));
            }
            serialNoReader.Close();

            if (trueSerialNos.Count != changedTable.DefaultView.Count)
                throw new DataCheckException(ConstMessages.CheckNumberOfSynchedOrder, "");

            // OrderTable�����ݱ��changedTable��Ҫ���£�������£�
            DataRow[] newRows = currentTable.OrderDataTable.Select(
               String.Format(CultureInfo.CurrentCulture, "{1} = {0:D}", OrderState.New, ConstSchemaNames.OrderColState));
            int changedIndex = changedTable.Rows.Count - 1;
            decimal oldSerialNo;
            decimal newSerialNo;
            decimal newGroupNo;
            Order temp;
            for (int index = trueSerialNos.Count - 1; index >= 0; index--)
            {
                oldSerialNo = Convert.ToDecimal(changedTable.DefaultView[index][serialNoField]);
                newSerialNo = trueSerialNos[index];
                newGroupNo = trueGroupNos[index];

                temp = currentTable.Orders[currentTable.Orders.IndexOf(oldSerialNo)];
                temp.SerialNo = newSerialNo;
                temp.GroupSerialNo = newGroupNo;
                if (currentTable.Orders.MaxSerialNo < temp.SerialNo)
                    currentTable.Orders.MaxSerialNo = temp.SerialNo;

                changedTable.DefaultView[index][serialNoField] = newSerialNo;
                changedTable.DefaultView[index][ConstSchemaNames.OrderColGroupSerialNo] = newGroupNo;

                newRows[index][serialNoField] = newSerialNo;
                newRows[index][ConstSchemaNames.OrderColGroupSerialNo] = newGroupNo;
            }
            changedTable.DefaultView.RowFilter = oldRowFilter;
            currentTable.AcceptChanges();
        }

        private void CallDoctorAdviceService(string messageName, byte[] data, string executorCode, string macAddress, bool isTempOrder)
        {
            string[,] parameters = new string[3, 6];

            //parameters[0, 0] = "wkdz";
            //parameters[1, 0] = "string";
            //parameters[2, 0] = macAddress;
            //parameters[0, 1] = "syxh";
            //parameters[1, 1] = "string";
            //parameters[2, 1] = CurrentPatient.NoOfHisFirstPage.ToString();
            //parameters[0, 2] = "xmlcqdata";
            //parameters[1, 2] = "base64string";
            //parameters[2, 2] = Convert.ToBase64String(data);
            //parameters[0, 3] = "iscqls";
            //parameters[1, 3] = "bool";
            //parameters[2, 3] = isTempOrder.ToString();
            //parameters[0, 4] = "encodingName";
            //parameters[1, 4] = "string";
            //parameters[2, 4] = ExchangeInfoOrderConst.EncodingName;
            //parameters[0, 5] = "lyjzsj";
            //parameters[1, 5] = "string";
            //parameters[2, 5] = string.Format("{0:D2}", CoreBusinessLogic.BusinessLogic.BlockingTimeOfTakeDrug);


            //string sExio;
            //sExio = m_InfoServer.BuildExchangeInfoString(messageName, ExchangeInfoOrderConst.EmrSystemName
            //   , ExchangeInfoOrderConst.HisSystemName, parameters);

            //string outMsg;
            //if (m_InfoServer.AddSyncExchangeInfo(sExio, ExchangeInfoOrderConst.DefaultEncoding, out outMsg) != ResponseFlag.Complete)
            //    throw new ApplicationException(outMsg);
            //// ͨ���жϷ������ݼ����������Ƿ�ɹ�
            //if (String.IsNullOrEmpty(outMsg))
            //    throw new DataCheckException(ConstMessages.ExceptionCallRemoting, ConstMessages.ExceptionTitleOrderTable);
            //else if (outMsg[0] != 'T') {
            //    if (outMsg[0] == 'F')
            //        throw new DataCheckException(outMsg.Substring(1), ConstMessages.ExceptionTitleOrderTable);
            //    else
            //        throw new DataCheckException(outMsg, ConstMessages.ExceptionTitleOrderTable);
            //}
        }

        private void UpdateSynchFlagToTrue(OrderTable currentTable, DataTable changedTable)
        {
            if (changedTable == null)// || (changedTable.Rows.Count == 0))
                return;

            changedTable.AcceptChanges();
            foreach (DataRow row in changedTable.Rows)
            {
                if (row[ConstSchemaNames.OrderColSynchFlag].ToString() == "0")
                    row[ConstSchemaNames.OrderColSynchFlag] = 1;

                // wxg , 2009-2-20, 6Ժ�޸�, ��������뵥ҽ����ֱ�����ñ�־Ϊ��ִ�У�����ҽԺ��Ҫ�ٿ��ǣ�
                //if (!string.IsNullOrEmpty(row[ConstSchemaNames.OrderRequestOrderNo].ToString())
                //    && (decimal.Parse(row[ConstSchemaNames.OrderRequestOrderNo].ToString()) > 0)) {
                //    row[ConstSchemaNames.OrderColState] = "3202";
                //}
            }

            m_SqlExecutor.UpdateTable(changedTable, GetOrderTableName(currentTable.IsTempOrder), false);
            currentTable.DefaultView.BeginInit();
            currentTable.AcceptDataSended();
            currentTable.DefaultView.EndInit();
        }

        private void AddOrderDataIntoOutputTable(string doctorCode, OrderTable orderTable, bool needDrug, DateTime startTime)
        {
            OrderContentKind contentKind;
            if (needDrug)
                contentKind = OrderContentKind.Druggery;
            else
                contentKind = OrderContentKind.ChargeItem;

            DataRow row;
            // ȡ���������ڵ���֮��ġ�����˵�ҩƷ��������Ŀҽ��(��������Ժ��ҩ)
            // TODO: ��Ϊ����ModelBuild���ܵ����ƣ������ܷ���Ľ����е����͵�ҽ��ת��ģ������
            foreach (Order order in orderTable.Orders)
            {
                if (((order.State == OrderState.Audited) || (order.State == OrderState.New))
                    && (order.CreateInfo.ExecuteTime >= startTime)
                    //&& (order.CreateInfo.Executor.Code.Trim() == doctorCode)
                    && (order.Content.OrderKind == contentKind))
                {
                    row = m_OutputTable.NewRow();
                    if (needDrug)
                        row[ConstSchemaNames.OrderOutputColProductSerialNo] = order.Content.Item.KeyValue;
                    else
                    {
                        // add by zhouhui ��ȷ��
                        //�������Ŀҽ������Ҫ������Ŀ��������ֶ�ypdm�У��Ա������Ŀ�ļ��У��
                        row[ConstSchemaNames.OrderOutputColProductSerialNo] = 0;
                        row[ConstSchemaNames.OrderOutputColDruggeryCode] = order.Content.Item.KeyValue;
                    }
                    row[ConstSchemaNames.OrderOutputColDruggeryName] = order.Content.Item.Name;
                    row[ConstSchemaNames.OrderOutputColAmount] = order.Content.Amount;
                    row[ConstSchemaNames.OrderOutputColUnit] = order.Content.CurrentUnit.Name;
                    if ((order.Content.ItemUsage != null) && order.Content.ItemUsage.KeyInitialized)
                    {
                        row[ConstSchemaNames.OrderOutputColUsageCode] = order.Content.ItemUsage.Code;
                        row[ConstSchemaNames.OrderOutputColUsageName] = order.Content.ItemUsage.Name;
                    }
                    else
                    {
                        row[ConstSchemaNames.OrderOutputColUsageCode] = "";
                        row[ConstSchemaNames.OrderOutputColUsageName] = "";
                    }
                    row[ConstSchemaNames.OrderOutputColFrequencyCode] = order.Content.ItemFrequency.Code;
                    row[ConstSchemaNames.OrderOutputColFrequencyName] = order.Content.ItemFrequency.Name;
                    m_OutputTable.Rows.Add(row);
                }
            }
        }

        private void CancelOrderLinkedToRequest(string executorCode, decimal applySerialNo, DateTime operateTime)
        {
            TempOrder order;
            for (int index = m_TempTable.Orders.Count - 1; index >= 0; index--)
            {
                order = m_TempTable.Orders[index] as TempOrder;
                if ((order.State == OrderState.Executed) || (order.State == OrderState.Ceased))
                    break;

                if ((order.State == OrderState.Audited) && (order.ApplySerialNo == applySerialNo))
                    order.CancelOrder(executorCode, operateTime);
            }
        }

        private void DeleteOrderLinkedToRequest(decimal applySerialNo)
        {
            TempOrder order;
            for (int index = m_TempTable.Orders.Count - 1; index >= 0; index--)
            {
                if (m_TempTable.Orders[index].State != OrderState.New)
                    break;
                order = m_TempTable.Orders[index] as TempOrder;
                if ((order.State == OrderState.New) && (order.ApplySerialNo == applySerialNo))
                    order.Delete();
            }
        }

        [Obsolete("����ʹ�ã�ԭ�������޷������ٴ���Ŀ��")]
        private void AddRequestItemIntoTempOrderTable(string executorCode, decimal applySerialNo, string executeDept, string[] itemCodeArray, DateTime starTime, DateTime operateTime)
        {
            TempOrder order;
            ChargeItemOrderContent content;

            foreach (string itemCode in itemCodeArray)
            {
                order = m_TempTable.NewOrder() as TempOrder;
                order.BeginInit();

                order.PatientId = Convert.ToDecimal(CurrentPatient.NoOfFirstPage, CultureInfo.CurrentCulture);
                order.OriginalDepartment = new Eop.Department(CurrentPatient.InfoOfAdmission.DischargeInfo.CurrentDepartment.Code);
                order.OriginalWard = new Eop.Ward(CurrentPatient.InfoOfAdmission.DischargeInfo.CurrentWard.Code);

                order.ApplySerialNo = applySerialNo;
                order.ExecuteDept = new Eop.Department(executeDept);
                order.CreateInfo = new OrderOperateInfo(executorCode, operateTime);
                order.StartDateTime = starTime;

                content = new ChargeItemOrderContent();
                content.BeginInit();
                content.Item = new ChargeItem(itemCode);
                content.Item.ReInitializeProperties();
                content.Amount = 1;
                content.CurrentUnit = content.Item.BaseUnit;
                content.ItemFrequency = new OrderFrequency(BusinessLogic.TempOrderFrequencyCode);
                content.EndInit();
                order.Content = content;

                order.EndInit();
                m_TempTable.AddOrder(order);
            }
        }

        private void AddRequestItemIntoTempOrderTable(string executorCode, decimal applySerialNo, string executeDept,
           IList<OrderInterfaceLogic.RequestFormItem> itemArray, DateTime starTime, DateTime operateTime)
        {
            TempOrder order;
            ChargeItemOrderContent content;
            ItemBase orderItem;

            foreach (OrderInterfaceLogic.RequestFormItem item in itemArray)
            {
                order = m_TempTable.NewOrder() as TempOrder;
                order.BeginInit();

                order.PatientId = Convert.ToDecimal(CurrentPatient.NoOfFirstPage, CultureInfo.CurrentCulture);
                order.OriginalDepartment = new Eop.Department(CurrentPatient.InfoOfAdmission.DischargeInfo.CurrentDepartment.Code);
                order.OriginalWard = new Eop.Ward(CurrentPatient.InfoOfAdmission.DischargeInfo.CurrentWard.Code);

                order.ApplySerialNo = applySerialNo;
                //order.ExecuteDept = new Department(executeDept);
                order.ExecuteDept = new Eop.Department(string.IsNullOrEmpty(item.ExecDept) ? executeDept : item.ExecDept);
                order.CreateInfo = new OrderOperateInfo(executorCode, operateTime);
                order.StartDateTime = starTime;
                order.Memo = string.Format("{0}`{1}`{2}`{3}", item.SpecimenId, item.Specimen, item.Memo, item.Urgent.ToString());

                // item.Kind  ( 0 �շ� 1 �ٴ� )��δʹ��ö��
                switch (item.Kind)
                {
                    case 0:
                        content = new ChargeItemOrderContent();
                        orderItem = new ChargeItem();
                        break;
                    case 1:
                        content = new ClinicItemOrderContent();
                        orderItem = new ClinicItem();
                        break;
                    default:
                        throw new NotSupportedException("����֧�ֵ���Ŀ���( 0 �շ� 1 �ٴ� )");
                }

                content.BeginInit();
                content.Item = orderItem;
                orderItem.Code = item.Code;
                content.Item.ReInitializeProperties();
                content.Amount = 1;
                content.CurrentUnit = content.Item.BaseUnit;
                content.ItemFrequency = new OrderFrequency(BusinessLogic.TempOrderFrequencyCode);
                content.EndInit();
                order.Content = content;

                order.EndInit();
                m_TempTable.AddOrder(order);
            }
        }

        private void SendQcMessage(string docCode, QCConditionType conditionType, object conditionObj, DateTime conditionTime)
        {
            // TODO: ����ʱ�޹������û������⣬������ͨ����ȡ������������ж��������������ڲ��˶����ҽ�������ж�ֻ��������״̬������һЩ��������Ҫ�ж�ǰ��״̬�ı仯
            m_Qcsv.AddRuleRecord(Convert.ToInt32(CurrentPatient.NoOfFirstPage), -1, docCode, conditionType, conditionObj, conditionTime);
        }
        #endregion

        #region private methods about edit flag calc
        private static bool CheckIsSpecialTextOrder(Order order)
        {
            bool isHerbSummary;
            return CheckIsSpecialTextOrder(order, out isHerbSummary);
        }

        private static bool CheckIsSpecialTextOrder(Order order, out bool isHerbSummary)
        {
            isHerbSummary = false;

            if ((order == null) || (order.Content == null))
                return false;

            switch (order.Content.OrderKind)
            {
                case OrderContentKind.TextAfterOperation:
                case OrderContentKind.TextShiftDept:
                case OrderContentKind.TextLeaveHospital:
                    return true;
                case OrderContentKind.TextNormal:
                    isHerbSummary = ((order.Content as TextOrderContent).IsSummaryOfHerbDetail);
                    return isHerbSummary;
                default:
                    return false;
            }
        }

        /// <summary>
        /// ��ȡָ��ҽ�������������ҽ����λ�ã����е���ҽ��Ӧ����������һ�飩
        /// ���ж�ѡ�е����Ƿ���������ƶ�ʱ��Ҫ�õ�
        /// </summary>
        /// <param name="currentTable">ҽ�����ڵ�ҽ�������</param>
        /// <param name="order">ָ����ҽ��</param>
        /// <returns>����GroupPositionKind����ʾ</returns>
        private static GroupPositionKind GetOrderPosInNewGroup(OrderTable currentTable, Order order)
        {
            // None����������ҽ��
            // Single��ֻ��һ����ҽ��
            // GroupStart����һ����ҽ��
            // GroupMiddle����ҽ�����м��¼
            // GroupEnd�����һ����ҽ��

            if (order == null)
                throw new ArgumentException(ConstMessages.ExceptionOrderIndexNotFind);

            // ��������״̬�����������ҽ����������Ϊ������ҽ��֮ǰ
            if (order.State != OrderState.New)
                return GroupPositionKind.None;

            if (CheckIsSpecialTextOrder(order))
                return GroupPositionKind.None;

            // "����ҽ��"��"ת��ҽ��"��"��Ժҽ��"���ܱ��ƶ�������Ҫ�ų�����ҽ��֮��
            // ������Ĵ����а��ռ򻯵�˼·����
            //    ��ǰҽ����ǰһ��������ҽ�����ա�������ҽ���������Ϳ�����ҽ���Ŀ�ʼ
            //    ��ǰҽ���ĺ�һ��������ҽ�����գ������Ϳ�����ҽ���Ľ���
            // ����ӡ�ɾ��ҽ���ĵط���֤��
            //    "����ҽ��"��"ת��ҽ��"ǰ�����������״̬��ҽ��
            //    "��Ժҽ��"����ֻ�����г�Ժ��ҩҽ��
            int index = currentTable.Orders.IndexOf(order.SerialNo);
            Order pre = null;
            Order next = null;
            bool isStart = true;
            bool isEnd = true;

            if (index > 0)
            {
                pre = currentTable.Orders[index - 1];
                if ((!CheckIsSpecialTextOrder(pre)) && (pre.State == OrderState.New))
                    isStart = false;
            }
            if (index < (currentTable.Orders.Count - 1))
            {
                next = currentTable.Orders[index + 1];
                if (!CheckIsSpecialTextOrder(next))
                    isEnd = false;
            }

            if (isStart && isEnd)
                return GroupPositionKind.SingleOrder;
            if (isStart)
                return GroupPositionKind.GroupStart;
            if (isEnd)
                return GroupPositionKind.GroupEnd;

            return GroupPositionKind.GroupMiddle;
        }

        private static bool CheckIsBeforeSecondNewOrder(OrderTable currentTable, Order order)
        {
            if (order == null)
                return false;

            // ����GroupPositionKind����ʾ���λ��
            GroupPositionKind pos = GetOrderPosInNewGroup(currentTable, order);

            return ((pos == GroupPositionKind.None) || (pos == GroupPositionKind.SingleOrder)
               || (pos == GroupPositionKind.GroupStart));
        }

        private static bool CheckIsLastNewOrder(OrderTable currentTable, Order order)
        {
            if (order == null)
                return false;

            GroupPositionKind pos = GetOrderPosInNewGroup(currentTable, order);

            return ((pos == GroupPositionKind.SingleOrder) || (pos == GroupPositionKind.GroupEnd));
        }

        private static OrderState CheckStateIsSame(Order[] selectedOrders)
        {
            OrderState state = OrderState.All; // ҽ��״̬����ʼΪAll��None��ʾ״̬��һ��

            foreach (Order order in selectedOrders)
            {
                if (order == null)
                    return OrderState.None;
                if (state != order.State)
                {
                    if (state == OrderState.All)
                        state = order.State;
                    else
                        return OrderState.None;
                }
            }
            return state;
        }

        private static int CalcCountOfOutDruggery(OrderTable tempTable)
        {
            if ((tempTable == null) || (!tempTable.IsTempOrder))
                return 0;

            int totalCountOfOutDruggery = 0; // ��ʱҽ���г�Ժ��ҩҽ��������
            for (int index = tempTable.Orders.Count - 1; index >= 0; index--)
            {
                if (tempTable.Orders[index].Content.OrderKind != OrderContentKind.OutDruggery)
                    break;
                totalCountOfOutDruggery++;
            }
            return totalCountOfOutDruggery;
        }

        private static decimal CheckHasPieceOfGroup(Order order, decimal lastGroupNo)
        {
            // -9999 ��ʾ����
            // -2 ��ʾ�����һ����¼
            // -1 ��ʾ������������
            // ����������ط������
            decimal errorNum = -9999;

            if (order == null)
                return errorNum;

            // ������ͷ�����ļ�¼����ʾ��ʼ�µķ��飬����ļ�¼�������Ӧ�ú���һ��
            // ��ֱ��������β�ļ�¼���������������Ϊ-1
            switch (order.GroupPosFlag)
            {
                case GroupPositionKind.GroupStart:
                    if ((lastGroupNo == -2) || (lastGroupNo == -1))
                        return order.GroupSerialNo;
                    else
                        return errorNum;
                case GroupPositionKind.GroupMiddle:
                    if (order.GroupSerialNo != lastGroupNo)
                        return errorNum;
                    return order.GroupSerialNo;
                case GroupPositionKind.GroupEnd:
                    if (order.GroupSerialNo != lastGroupNo)
                        return errorNum;
                    return -1; // ��ʾ������� 
                default:
                    if ((lastGroupNo == -2) || (lastGroupNo == -1))
                        return -1;
                    else
                        return errorNum;
            }
        }

        private static bool CheckCommonPropertiesIsSame(Order[] selectedOrders)
        {
            DateTime startTime = DateTime.Now;
            DateTime ceaseTime = DateTime.MinValue;
            string usageCode = String.Empty;
            OrderFrequency frequency = null;
            LongOrder longOrder;
            // ��鿪ʼʱ�䡢�÷���Ƶ�Ρ�ֹͣʱ�����Ϣ�Ƿ�һ�£��Ա��ж���Щҽ���Ƿ�����ͬһ��
            foreach (Order order in selectedOrders)
            {
                if (order != null)
                {
                    longOrder = order as LongOrder;
                    if (String.IsNullOrEmpty(usageCode)) //��һ��
                    {
                        startTime = order.StartDateTime;

                        if ((order.Content.ItemUsage != null)
                           && (order.Content.ItemUsage.KeyInitialized))
                            usageCode = order.Content.ItemUsage.Code;
                        else
                            usageCode = "";

                        if (order.Content.ItemFrequency.KeyInitialized)
                            frequency = order.Content.ItemFrequency;
                        else
                            frequency = null;

                        if ((longOrder != null) && (longOrder.CeaseInfo != null) && (longOrder.CeaseInfo.HadInitialized))
                            ceaseTime = longOrder.CeaseInfo.ExecuteTime;
                    }
                    else
                    {
                        if (order.StartDateTime != startTime)
                            return false;

                        if ((order.Content.ItemUsage == null)
                           || (!order.Content.ItemUsage.KeyInitialized)
                           || (order.Content.ItemUsage.Code != usageCode))
                            return false;

                        if ((!order.Content.ItemFrequency.KeyInitialized)
                           || (!order.Content.ItemFrequency.Equals(frequency)))
                            return false;

                        if (longOrder != null)
                        {
                            if ((longOrder.CeaseInfo != null) && (longOrder.CeaseInfo.HadInitialized))
                            {
                                if ((ceaseTime == DateTime.MinValue) || (ceaseTime != longOrder.CeaseInfo.ExecuteTime))
                                    return false;
                            }
                            else
                            {
                                if (ceaseTime > DateTime.MinValue)
                                    return false;
                            }
                        }
                    }
                }
                else
                    return false;
            }
            return true;
        }

        private static AttributeOfSelectedFlag GetAttributeOfSelectedOrder(OrderTable table, Order[] selectedOrders)
        {
            AttributeOfSelectedFlag result = AttributeOfSelectedFlag.NumIsSerial
               | AttributeOfSelectedFlag.InSameGroup;

            if (CheckIsBeforeSecondNewOrder(table, selectedOrders[0]))
                result |= AttributeOfSelectedFlag.HasFirstNew;

            if (CheckIsLastNewOrder(table, selectedOrders[selectedOrders.Length - 1]))
                result |= AttributeOfSelectedFlag.HasLastNew;

            bool checkSerial = true;
            bool checkSpecial = true;
            bool checkLeaveHospital = table.IsTempOrder;
            bool checkOutDruggery = false;
            int countOfOutDruggery = 0;
            ItemKind itemKind = ItemKind.None;
            bool sameItemKind = true;
            bool isHerbSummary;

            int orderIndex = -1;
            decimal lastGroupSerialNo = -2; // -1��ʾû�������ѷ���ļ�¼
            bool checkCeaseInfo = (!table.IsTempOrder);
            bool checkLinkToApply = table.IsTempOrder;
            LongOrder longOrder;
            foreach (Order order in selectedOrders)
            {
                if (order != null)
                {
                    // ����Ƿ�������״̬��ҽ��
                    result |= GetStateAttributeOfSelected(order);
                    // ���ҽ������Ƿ�����
                    if (checkSerial)
                    {
                        if (orderIndex == -1)
                            orderIndex = table.Orders.IndexOf(order.SerialNo);
                        else if (order.SerialNo != table.Orders[orderIndex].SerialNo)
                        {
                            result &= (~AttributeOfSelectedFlag.NumIsSerial);
                            checkSerial = false;
                        }
                        orderIndex++;
                    }
                    // �Ƿ�������ҽ��
                    if (checkSpecial && CheckIsSpecialTextOrder(order, out isHerbSummary))
                    {
                        result |= AttributeOfSelectedFlag.HasSpecial;
                        if (isHerbSummary)
                            result |= AttributeOfSelectedFlag.HasHerbSummary;
                        checkCeaseInfo = !isHerbSummary;
                    }
                    // ����Ժҽ��
                    if (checkLeaveHospital && (order.Content != null))
                    {
                        if (order.Content.OrderKind == OrderContentKind.TextLeaveHospital)
                        {
                            checkLeaveHospital = false;
                            checkOutDruggery = true;
                            result |= AttributeOfSelectedFlag.HasLeaveHospital;
                        }
                    }
                    // ���ҽ�����ݵ�����
                    if ((order.Content != null) && (order.Content.Item != null))
                    {
                        if (order.Content.OrderKind == OrderContentKind.OutDruggery)
                            countOfOutDruggery++;
                        if (sameItemKind && (itemKind != order.Content.Item.Kind))
                        {
                            if (itemKind == ItemKind.None)
                                itemKind = order.Content.Item.Kind;
                            else if ((itemKind != ItemKind.HerbalMedicine)
                               && ((itemKind == ItemKind.WesternMedicine) || (itemKind == ItemKind.PatentMedicine))
                               && ((order.Content.OrderKind == OrderContentKind.Druggery) || (order.Content.OrderKind == OrderContentKind.OutDruggery)))
                                itemKind = ItemKind.WesternMedicine; // ����ҩ��������ҩ�ͳ�ҩ
                            else
                                sameItemKind = false;
                        }
                    }
                    else
                        sameItemKind = false;

                    // �Է�����м��
                    if ((lastGroupSerialNo != -2) && (order.GroupSerialNo != lastGroupSerialNo)) // �����иı�
                        result &= (~AttributeOfSelectedFlag.InSameGroup);
                    if ((result & AttributeOfSelectedFlag.HasPieceOfGroup) == 0)
                        result |= GetGroupAttributeOfSelected(order, ref lastGroupSerialNo);

                    // ���ֹͣ��Ϣ
                    if (checkCeaseInfo)
                    {
                        longOrder = order as LongOrder;
                        if ((longOrder != null) && (longOrder.CeaseInfo != null) && longOrder.CeaseInfo.HadInitialized)
                        {
                            result |= AttributeOfSelectedFlag.HasCeaseInfo;
                            checkCeaseInfo = false;
                        }
                    }
                    if (checkLinkToApply)
                    {
                        if ((order as TempOrder).ApplySerialNo != 0)
                        {
                            result |= AttributeOfSelectedFlag.HasLinkToApply;
                            checkLinkToApply = false;
                        }
                    }
                }
                else
                {
                    result &= (~AttributeOfSelectedFlag.NumIsSerial);
                    checkSerial = false;
                    sameItemKind = false;
                }
            }
            // ������յķ�����Ų���-1�����ʾ���һ������ļ�¼û�б�ѡȫ
            if (lastGroupSerialNo != -1)
                result |= AttributeOfSelectedFlag.HasPieceOfGroup;
            // ����Ժ��ҩ�Ƿ�ȫ��ѡ��
            if ((!checkOutDruggery) || (CalcCountOfOutDruggery(table) == countOfOutDruggery))
                result |= AttributeOfSelectedFlag.SelectedAllOutDurg;
            // �ж��Ƿ�ȫ����ҩƷ����
            if (sameItemKind)
            {
                if (itemKind == ItemKind.HerbalMedicine)
                    result |= AttributeOfSelectedFlag.AllIsHerbDruggery;
                else if (itemKind == ItemKind.WesternMedicine)
                    result |= AttributeOfSelectedFlag.AllIsOtherDruggery;
            }
            return result;
        }

        private static AttributeOfSelectedFlag GetGroupAttributeOfSelected(Order order, ref decimal lastGroupSerialNo)
        {
            AttributeOfSelectedFlag result = 0;

            if (order.GroupPosFlag != GroupPositionKind.SingleOrder)
                result |= AttributeOfSelectedFlag.HasGrouped;

            lastGroupSerialNo = CheckHasPieceOfGroup(order, lastGroupSerialNo);
            if (lastGroupSerialNo == -9999)
                result |= AttributeOfSelectedFlag.HasPieceOfGroup;

            return result;
        }

        private static AttributeOfSelectedFlag GetStateAttributeOfSelected(Order order)
        {
            switch (order.State)
            {
                case OrderState.Cancellation:
                    return AttributeOfSelectedFlag.HasCancelled;
                case OrderState.Ceased:
                    return AttributeOfSelectedFlag.HasCeased;
                default:
                    return 0;
            }
        }

        private static EditProcessFlag CalcEditFlagAboutAllowNew(Order[] selectedOrders, OrderState state, bool noSpecialOrder, AttributeOfSelectedFlag flag)
        {
            bool isSerial = ((flag & AttributeOfSelectedFlag.NumIsSerial) > 0); // �к��Ƿ�����
            bool noGrouped = ((flag & AttributeOfSelectedFlag.HasGrouped) == 0); // �Ƿ��а����ڷ����е�
            bool notFirstNew = ((flag & AttributeOfSelectedFlag.HasFirstNew) == 0); // �Ƿ��д�������������¼�еĵ�һ���ģ��ڵ�һ������֮ǰҲ��Ϊtrue
            bool notLastNew = ((flag & AttributeOfSelectedFlag.HasLastNew) == 0); // �Ƿ��д�������������¼�е����һ����
            bool allIsDrug = (((flag & AttributeOfSelectedFlag.AllIsHerbDruggery) > 0)
               || ((flag & AttributeOfSelectedFlag.AllIsOtherDruggery) > 0)); // ����Ƿ���ҩƷ

            EditProcessFlag result = 0;
            if (state == OrderState.New) // ����Ķ����й���������ȫ��������
            {
                if (isSerial && noGrouped && noSpecialOrder)
                {
                    // ����--ȫ������������¼�������ģ������ڵ�һ������״̬�ļ�¼֮��û�д��ڷ����еģ�������ҽ��
                    if (notFirstNew)
                        result |= EditProcessFlag.MoveUp;
                    // ����--ȫ������������¼�������ģ�ĩβ�����һ������״̬�ļ�¼֮ǰ��û�д��ڷ����еģ�������ҽ��
                    if (notLastNew)
                        result |= EditProcessFlag.MoveDown;
                    // ����--ȫ������������¼�������ģ��ж�����û���ѷ���ģ�������ҽ��,�������Զ�һ�£�ȫ����ҩƷ
                    if ((selectedOrders.Length > 1) && allIsDrug && (CheckCommonPropertiesIsSame(selectedOrders)))
                        result |= EditProcessFlag.SetGroup;
                }
                // ȡ������--ȫ�����������з���
                if (!noGrouped)
                    result |= EditProcessFlag.CancelGroup;
            }
            if ((selectedOrders.Length == 1) && allIsDrug && noGrouped)
            {
                // �鿪ʼ--����������¼���������У��������һ��������¼
                if (notLastNew)
                    result |= EditProcessFlag.GroupStart;
                // �����--����������¼���������У����ǵ�һ��������¼
                if (notFirstNew)
                    result |= EditProcessFlag.GroupEnd;
            }
            return result;
        }
        #endregion

        #region public methods
        /// <summary>
        /// ����ҽ�����ͻ�ȡ��������ҽ��������ݱ������
        /// </summary>
        /// <param name="isTempOrder"></param>
        /// <returns></returns>
        public string GetOrderContentCatalogRowFilter(bool isTempOrder)
        {
            if (m_ProcessModel == EditorCallModel.EditOrder)
            {
                // ���յ�ǰά����ҽ�����ͣ����ÿ�ѡ��ҽ������
                if (isTempOrder)
                {
                    if (HasOutHospitalOrder) // �����¼���Ժҽ������ֻ�������Ժ��ҩ
                        return String.Format(CultureInfo.CurrentCulture
                           , "Flag in ({0:D}, {1:D}) and ID = {2:D}"
                           , OrderManagerKind.Normal, OrderManagerKind.ForTemp, OrderContentKind.OutDruggery);
                    else
                        return String.Format(CultureInfo.CurrentCulture
                           , "Flag in ({0:D}, {1:D}) and ID <> {2:D}"
                           , OrderManagerKind.Normal, OrderManagerKind.ForTemp, OrderContentKind.OutDruggery);
                }
                else
                    return String.Format(CultureInfo.CurrentCulture
                          , "Flag in ({0:D}, {1:D})"
                          , OrderManagerKind.Normal, OrderManagerKind.ForLong);
            }
            else if (m_ProcessModel == EditorCallModel.EditSuite)
            {
                if (isTempOrder)
                    return String.Format(CultureInfo.CurrentCulture
                          , "ID in ({0:D}, {1:D}, {2:D}, {3:D})"
                          , OrderContentKind.Druggery, OrderContentKind.ChargeItem, OrderContentKind.OutDruggery, OrderContentKind.TextNormal);
                else
                    return String.Format(CultureInfo.CurrentCulture
                          , "ID in ({0:D}, {1:D}, {2:D}, {3:D})"
                          , OrderContentKind.Druggery, OrderContentKind.ChargeItem, OrderContentKind.GeneralItem, OrderContentKind.TextNormal);
            }
            else
                return "1=2";
        }

        /// <summary>
        /// ���ݱ�־���ض�Ӧҽ����
        /// </summary>
        /// <param name="isTempOrder"></param>
        /// <returns></returns>
        public OrderTable GetCurrentOrderTable(bool isTempOrder)
        {
            if (isTempOrder)
                return m_TempTable;
            else
                return m_LongTable;
        }

        /// <summary>
        /// ����ҽ��Ĭ�Ͽ�ʼʱ�䣨��ȷ�����ӣ�
        /// </summary>
        public DateTime GetDefaultStartDateTime(bool isTempOrder)
        {
            OrderTable currentTable;
            if (isTempOrder)
                currentTable = m_TempTable;
            else
                currentTable = m_LongTable;

            // ��ʼʱ��Ĭ��ֵ�����ȼ���
            //      �ߣ���һ������ҽ���Ŀ�ʼʱ�䣨�������С��ʼʱ��֮��
            //      �У�������Լ����ʱ�䣨����ʱ�䣩
            //      �ͣ���ǰʱ�䣨Ĭ��ֵ��
            DateTime last = DateTime.MinValue;
            for (int index = currentTable.Orders.Count - 1; index >= 0; index--)
            {
                last = currentTable.Orders[index].StartDateTime;
                if (last > DateTime.MinValue) // ���һ��ҽ������������¼���ģ���û�г�ʼ����ʼ����
                    break;
            }

            if (last >= WarnStartDateTime)
                return last;
            else
            {
                // ����ҽ���ӵڶ���˵㿪ʼ����ʱҽ���ӵ�ǰʱ�俪ʼ
                if (isTempOrder)
                {
                    // Ĭ��Ϊ��ǰ���ڣ������԰�СʱΪ��λ��45��15����Ϊ��00����15��45����Ϊ��30����
                    int hour = DateTime.Now.Hour;
                    int minute = DateTime.Now.Minute;
                    if (minute <= 30)
                        minute = 30;
                    else
                    {
                        hour += 1;
                        minute = 0;
                    }
                    return DateTime.Today + new TimeSpan(hour, minute, 0);
                }
                else
                {
                    return DateTime.Now.AddDays(1).Date + new TimeSpan(8, 0, 0);
                }
            }
        }

        /// <summary>
        /// �����ѡ�е�ҽ������ִ�е�ҽ������
        /// </summary>
        /// <param name="table"></param>
        /// <param name="selectedOrders"></param>
        /// <returns></returns>
        public EditProcessFlag CalcEditProcessFlag(OrderTable table, Order[] selectedOrders)
        {
            if ((table == null) || (selectedOrders == null) || (selectedOrders.Length == 0))
                return 0;

            Type orderType;
            bool allowNew;
            if (table.IsTempOrder)
            {
                orderType = typeof(TempOrder);
                allowNew = AllowAddTemp;
            }
            else
            {
                orderType = typeof(LongOrder);
                allowNew = AllowAddLong;
            }

            foreach (Order order in selectedOrders)
                if (order.GetType() != orderType)
                    return 0;

            EditProcessFlag result = 0;
            OrderState state = CheckStateIsSame(selectedOrders); // ״̬�Ƿ�һ�£�һ��ʱ�Ǻ���״̬����None��ʾ״̬��һ��
            AttributeOfSelectedFlag flag = GetAttributeOfSelectedOrder(table, selectedOrders);

            bool noCancelled = ((flag & AttributeOfSelectedFlag.HasCancelled) == 0); // �Ƿ������ȡ��ҽ��
            bool noCeased = ((flag & AttributeOfSelectedFlag.HasCeased) == 0); // �Ƿ������ֹͣҽ��
            bool noCeaseInfo = ((flag & AttributeOfSelectedFlag.HasCeaseInfo) == 0); // �Ƿ������ֹͣ��Ϣ��ҽ��
            bool noSpecialOrder = ((flag & AttributeOfSelectedFlag.HasSpecial) == 0); // �Ƿ��������ҽ��(����ת�ơ���Ժ��),Ӱ�쵽ҽ���Ƿ�����λ
            bool noLeaveHospital = ((flag & AttributeOfSelectedFlag.HasLeaveHospital) == 0); // �Ƿ������Ժҽ��
            bool allOutDurgSelected = ((flag & AttributeOfSelectedFlag.SelectedAllOutDurg) > 0); // ��Ժ��ҩҽ���Ƿ�ȫ��ѡ����
            bool noPieceOfGroup = ((flag & AttributeOfSelectedFlag.HasPieceOfGroup) == 0); // ͬ��ļ�¼�Ƿ�ȫ��ѡ��
            bool linkToApply = ((flag & AttributeOfSelectedFlag.HasLinkToApply) > 0); // ����Ƿ�������뵥
            bool hasHerbSummary = ((flag & AttributeOfSelectedFlag.HasHerbSummary) > 0); // ����Ƿ��в�ҩ������Ϣ

            // �����Ǻ��Ƿ����������йص�
            if (allowNew)
            {
                if (linkToApply)
                    noSpecialOrder = false;
                result |= CalcEditFlagAboutAllowNew(selectedOrders, state, noSpecialOrder, flag);
            }
            // ɾ��--ȫ��������, ɾ������Ժҽ����ʱͬʱɾ�������С���Ժ��ҩ��ҽ�����������������뵥��ҽ���� ��������ҩ������Ϣ
            if ((state == OrderState.New) && (noLeaveHospital || allOutDurgSelected) && (!linkToApply) && (!hasHerbSummary))
                result |= EditProcessFlag.Delete;
            // ȡ��--ȫ������ˣ��������������뵥��ҽ��
            if ((state == OrderState.Audited) && (!linkToApply))// && noShiftDeptOrder)
                result |= EditProcessFlag.Cancel;
            // ֹͣ--���ڵģ���û�а�����ֹͣ����ȡ���ļ�¼��û�����ֹͣʱ���
            // modified by zhouhui ֹͣ--���ڵģ���û�а�����ֹͣ����ȡ���ļ�¼
            //TODO: Ŀǰ������ֹͣʱ���ǿ����޸ĵģ�Ҫ������ص�����
            //if ((!table.IsTempOrder) && noCancelled && noCeased && noCeaseInfo)
            if ((!table.IsTempOrder) && noCancelled && noCeased)
                result |= EditProcessFlag.Cease;
            // ���--ȫ��������,ͬ���ҽ�����ֿܷ����//���������������뵥��ҽ��
            if ((state == OrderState.New) && noPieceOfGroup)// && (!linkToApply))
                result |= EditProcessFlag.Audit;
            // ִ��--ȫ���������״̬(ִ�а�ťֻ����ʱʹ�ã�����)
            if (state == OrderState.Audited)
                result |= EditProcessFlag.Execute;
            //// ��ѡ--ѡ�е��ǵ���
            //if (selectedHandles.Length == 1)
            //   result |= EditProcessFlag.StartMultiSelect;

            // ����--ȫ������ҽ��������ҽ����һ��ѡ�У�����������ҽ��
            if (allowNew && (state == OrderState.New) && noPieceOfGroup && noSpecialOrder)
                result |= EditProcessFlag.Cut;
            // ����--����������ҽ��
            if (allowNew && noSpecialOrder)
                result |= EditProcessFlag.Copy;

            // �ǲ�ҩ������Ϣ--����������ҽ����������ҩ��ϸ
            if ((selectedOrders.Length == 1) && (hasHerbSummary))
                result |= EditProcessFlag.IsHerbSummary;

            // �ǲ�ҩ��ϸ��Ϣ--�������ѷ��飬�ǲ�ҩ��Ŀ
            if ((selectedOrders.Length == 1) && (selectedOrders[0].GroupPosFlag != GroupPositionKind.SingleOrder)
               && (selectedOrders[0].Content.Item != null) && (selectedOrders[0].Content.Item.Kind == ItemKind.HerbalMedicine))
                result |= EditProcessFlag.IsHerbDetail;

            return result;
        }

        /// <summary>
        /// ɾ��ָ������ҽ��
        /// </summary>
        /// <param name="selectedHandles"></param>
        public void DeleteNewOrder(Order[] selectedOrders, bool isTempOrder)
        {
            OrderTable currentTable = GetCurrentOrderTable(isTempOrder);

            string errMsg;
            try
            {
                errMsg = CheckCanBeDeleted(currentTable, selectedOrders);
            }
            catch
            {
                throw;
            }
            if (String.IsNullOrEmpty(errMsg))
            {
                currentTable.DefaultView.BeginInit();

                DoDeleteNewOrder(currentTable, selectedOrders);

                currentTable.DefaultView.EndInit();
            }
            else
                throw new DataCheckException(errMsg, ConstMessages.ExceptionTitleOrder);
        }

        /// <summary>
        /// ��ָ����Χ��ҽ����Ϊһ��
        /// </summary>
        /// <param name="selectedHandles">��ǰѡ�е�ҽ���к�</param>
        public void SetOrderGroup(Order[] selectedOrders, bool isTempOrder)
        {
            OrderTable currentTable = GetCurrentOrderTable(isTempOrder);

            string errMsg;
            try
            {
                errMsg = CheckCanSetGroup(currentTable, selectedOrders);
            }
            catch
            {
                throw;
            }
            if (String.IsNullOrEmpty(errMsg))
            {
                currentTable.DefaultView.BeginInit();

                DoSetGroup(currentTable, selectedOrders);

                currentTable.DefaultView.EndInit();
            }
            else
                throw new DataCheckException(errMsg, ConstMessages.ExceptionTitleOrder);
        }

        /// <summary>
        /// �Զ�����ҽ�����з���
        /// </summary>
        /// <returns>������ļ�¼���к�</returns>
        public int[] AutoSetNewOrderGrouped(bool isTempOrder)
        {
            OrderTable currentTable = GetCurrentOrderTable(isTempOrder);
            OrderTableView defaultView = currentTable.DefaultView;
            // �����һ������ҽ����ʼ��ǰ��飬�ҵ����Գ�������Χ
            // ��ѡ�з�Χ�ڵļ�¼��Ϊһ��
            int index = defaultView.Count - 1; // index����ָ���ҵ��ĵ�����һ�����ܼ������ļ�¼
            for (; index >= 0; index--)
            {
                // ��������״̬��δ����
                if ((defaultView[index].State != OrderState.New)
                   || (defaultView[index].GroupPosFlag != GroupPositionKind.SingleOrder))
                    break;
                if ((defaultView[index].OrderCache.Content.OrderKind != OrderContentKind.Druggery)
                   && (defaultView[index].OrderCache.Content.OrderKind != OrderContentKind.OutDruggery))
                    break;
                if (index == (defaultView.Count - 1))
                    continue;
                // ��������Ƿ���Գ��飬�õ����յ�indexλ��
                try
                {
                    if (!String.IsNullOrEmpty(CheckCanSetGroup(currentTable, new Order[] { defaultView[index].OrderCache, defaultView[index + 1].OrderCache })))
                        break;
                }
                catch
                {
                    break;
                }
            }

            index++;
            int maxGroupCount = defaultView.Count - index;

            // ѡ��ָ����¼����Ϊ�� 
            //if (index < (defaultView.Count - 1))
            if (maxGroupCount > 1)
            {
                Order[] selectedOrders = new Order[maxGroupCount];
                int[] rowHandles = new int[maxGroupCount];
                for (int index2 = 0; index2 < maxGroupCount; index2++)
                {
                    selectedOrders[index2] = defaultView[index + index2].OrderCache;
                    rowHandles[index2] = index + index2;
                }
                SetOrderGroup(selectedOrders, isTempOrder);
                return rowHandles;
            }
            else
                return null;
        }

        /// <summary>
        /// ȡ��ѡ��ҽ���ķ���
        /// </summary>
        /// <param name="selectedOrders">��ǰѡ�е�ҽ��</param>
        /// <param name="isTempOrder"></param>
        public void CancelOrderGroup(Order[] selectedOrders, bool isTempOrder)
        {
            OrderTable currentTable = GetCurrentOrderTable(isTempOrder);

            string errMsg;
            try
            {
                errMsg = CheckCanCancelGroup(selectedOrders);
            }
            catch
            {
                throw;
            }
            if (String.IsNullOrEmpty(errMsg))
            {
                currentTable.DefaultView.BeginInit();

                DoCancelGroup(currentTable, selectedOrders);

                currentTable.DefaultView.EndInit();
            }
            else
                throw new DataCheckException(errMsg, ConstMessages.ExceptionTitleOrder);
        }

        /// <summary>
        /// ���ѡ����ҽ��
        /// </summary>
        /// <param name="selectedOrders">��ǰѡ����ҽ��</param>
        /// <param name="auditor">����ߴ���</param>
        /// <param name="auditTime">���ʱ��</param>
        /// <param name="isTempOrder">����Ƿ�����ʱҽ��</param>
        public void AuditOrder(Order[] selectedOrders, string auditor, DateTime auditTime, bool isTempOrder)
        {
            OrderTable currentTable = GetCurrentOrderTable(isTempOrder);

            string errMsg;
            try
            {
                errMsg = CheckCanBeAudited(currentTable, selectedOrders);
            }
            catch
            {
                throw;
            }
            if (String.IsNullOrEmpty(errMsg))
            {
                currentTable.DefaultView.BeginInit();

                if (isTempOrder)
                    DoAuditTempOrder(currentTable, selectedOrders, auditor, auditTime);
                else
                    DoAuditLongOrder(currentTable, selectedOrders, auditor, auditTime);

                currentTable.DefaultView.EndInit();
            }
            else
                throw new DataCheckException(errMsg, ConstMessages.ExceptionTitleOrder);
        }

        /// <summary>
        /// ȡ��ѡ���������ҽ��
        /// </summary>
        /// <param name="selectedOrders">��ǰѡ����ҽ��</param>
        /// <param name="auditor">����ߴ���</param>
        /// <param name="auditTime">���ʱ��</param>
        public void CancelOrder(Order[] selectedOrders, string cancellor, DateTime cancelTime, bool isTempOrder)
        {
            OrderTable currentTable = GetCurrentOrderTable(isTempOrder);

            string errMsg;
            try
            {
                errMsg = CheckCanBeCancelled(currentTable, selectedOrders);
            }
            catch
            {
                throw;
            }
            if (String.IsNullOrEmpty(errMsg))
            {
                //currentTable.DefaultView.BeginInit();

                DoCancelOrder(currentTable, selectedOrders, cancellor, cancelTime);

                //currentTable.DefaultView.EndInit();
            }
            else
                throw new DataCheckException(errMsg, ConstMessages.ExceptionTitleOrder);
        }

        /// <summary>
        /// ���ó���ҽ����ֹͣʱ��
        /// </summary>
        /// <param name="selectedOrders"></param>
        /// <param name="m_DoctorCode"></param>
        /// <param name="ceaseTime"></param>
        public void SetLongOrderCeaseInfo(Order[] selectedOrders, string ceasor, DateTime ceaseTime)
        {
            string errMsg;
            try
            {
                errMsg = CheckCanSetCeaseTime(m_LongTable, selectedOrders, ceaseTime);
            }
            catch
            {
                throw;
            }
            if (String.IsNullOrEmpty(errMsg))
            {
                //m_LongTable.DefaultView.BeginInit();

                DoSetLongOrderCeaseInfo(selectedOrders, ceasor, ceaseTime);

                //m_LongTable.DefaultView.EndInit();
            }
            else
                throw new DataCheckException(errMsg, ConstMessages.ExceptionTitleOrder);
        }

        /// <summary>
        /// ��鴫���ҽ�������Ƿ����Ҫ��
        /// </summary>
        /// <param name="currentTable">��ָ����ҽ�����м��</param>
        /// <param name="orders">Ҫ����ҽ������</param>
        /// <param name="callByEditor">�Ƿ��Ǳ༭������(���ڱ༭�������򲻼�鿪ʼʱ��)</param>
        public void CheckOrderData(OrderTable currentTable, Order[] orders, bool callByEditor, bool skipWarnning)
        {
            if ((orders == null) || (orders.Length == 0))
                return;

            UpdateContentFlag updateFlag;
            if (callByEditor)
                updateFlag = UpdateContentFlag.StartDate | UpdateContentFlag.Content;
            else
                updateFlag = UpdateContentFlag.Content;

            Order order;
            for (int index = 0; index < orders.Length; index++)
            {
                order = orders[index];
                if (order.EditState == OrderEditState.Deleted)
                    continue;
                if (order.State == OrderState.Ceased) // ֹͣ״̬�Ŀ�������ʱ�䡢���ݼ��
                    continue;
                try
                {
                    CheckOrderValueBeforeSet(currentTable, order
                       , currentTable.Orders.IndexOf(order.SerialNo), updateFlag);
                }
                catch (DataCheckException e)
                {
                    if ((e.WarnningLevel == 1) || (!skipWarnning))
                    {
                        e.RowIndex = index;
                        e.OrderSerialNo = order.SerialNo;
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// ��ָ��ҽ��������������޸ĵ�ҽ�����浽���ݿ���(�ڵ���֮ǰҪ�ֹ��������ݼ�鷽��)
        /// ��Ҫ���༭������
        /// </summary>
        /// <param name="changedOrders"></param>
        /// <param name="currentTable"></param>
        /// <param name="executorCode">ִ�б��������ְ������</param>
        /// <param name="macAddress">���ö˵�������ַ</param>
        /// <param name="callByEditor">����Ƿ�����ҽ���༭���е���</param>
        public void SaveChangedOrderDataInEditor(OrderTable currentTable, Order[] changedOrders, string executorCode, string macAddress)
        {
            if ((String.IsNullOrEmpty(executorCode)) || (String.IsNullOrEmpty(macAddress)))
                throw new ArgumentNullException();

            DoSaveChangedData(currentTable, changedOrders, executorCode, macAddress, true, false, true);
        }

        /// <summary>
        /// ����������޸ĵ�ҽ�����浽���ݿ���
        /// ��Ҫ�����������߼���ֱ�ӵ���
        /// </summary>
        /// <param name="executorCode">ִ�б��������ְ������</param>
        /// <param name="macAddress">���ö˵�������ַ</param>
        /// <param name="forceSend">ǿ�Ʒ���</param>
        public void SaveAllChangedOrderData(string executorCode, string macAddress, bool forceSend)
        {
            //SaveAllChangedOrderData(executorCode, macAddress, forceSend, false, null, DateTime.MinValue);

            if ((String.IsNullOrEmpty(executorCode)) || (String.IsNullOrEmpty(macAddress)))
                throw new ArgumentNullException();

            Order[] changedOrders = m_TempTable.GetChangedOrders();

            if ((changedOrders != null) && (changedOrders.Length > 0))
            {
                CheckOrderData(m_TempTable, changedOrders, false, true);

                DoSaveChangedData(m_TempTable, changedOrders, executorCode, macAddress, false, forceSend, false);
            }

            changedOrders = m_LongTable.GetChangedOrders();
            if ((changedOrders != null) && (changedOrders.Length > 0))
            {
                CheckOrderData(m_LongTable, changedOrders, false, true);

                DoSaveChangedData(m_LongTable, changedOrders, executorCode, macAddress, false, forceSend, false);
            }
        }

        ///// <summary>
        ///// ����������޸ĵ�ҽ�����浽���ݿ���
        ///// ��Ҫ�����������߼���ֱ�ӵ���
        ///// </summary>
        ///// <param name="executorCode">ִ�б��������ְ������</param>
        ///// <param name="macAddress">���ö˵�������ַ</param>
        ///// <param name="forceSend">ǿ�Ʒ���</param>
        ///// <param name="saveSendLog">�Ƿ񱣴��б����ҽ�����ݵ���¼����</param>
        ///// <param name="fireCode">�������涯����ְ�����룬��ִ�б�������Ĳ�һ����ͬһ����</param>
        ///// <param name="fireTime">�������涯����ʱ��</param>
        //public void SaveAllChangedOrderData(string executorCode, string macAddress, bool forceSend, bool saveSendLog, string fireCode, DateTime fireTime)
        //{
        //   if ((String.IsNullOrEmpty(executorCode)) || (String.IsNullOrEmpty(macAddress)))
        //      throw new ArgumentNullException();
        //   if (saveSendLog && (String.IsNullOrEmpty(fireCode) || (fireTime == DateTime.MinValue)))
        //      throw new ArgumentNullException("�������涯����ְ������");

        //   Order[] changedOrders = m_TempTable.GetChangedOrders();

        //   if ((changedOrders != null) && (changedOrders.Length > 0))
        //   {
        //      CheckOrderData(m_TempTable, changedOrders, false, true);

        //      DoSaveChangedData(m_TempTable, changedOrders, executorCode, macAddress, false, forceSend, false, saveSendLog, fireCode, fireTime);
        //   }

        //   changedOrders = m_LongTable.GetChangedOrders();
        //   if ((changedOrders != null) && (changedOrders.Length > 0))
        //   {
        //      CheckOrderData(m_LongTable, changedOrders, false, true);

        //      DoSaveChangedData(m_LongTable, changedOrders, executorCode, macAddress, false, forceSend, false, saveSendLog, fireCode, fireTime);
        //   }
        //}

        /// <summary>
        /// ͬ�����˵ĳ��ڡ���ʱ���ݵ�HIS�У��ڵ���ǰҪ�ֹ�ִ�����ݱ��淽����
        /// </summary>
        /// <param name="currentTable"></param>
        /// <param name="executorCode"></param>
        /// <param name="macAddress"></param>
        /// <param name="callByEditor"></param>
        public void SynchDataToHIS(OrderTable currentTable, string executorCode, string macAddress)
        {
            if ((String.IsNullOrEmpty(executorCode)) || (String.IsNullOrEmpty(macAddress)))
                throw new ArgumentNullException();

            DoManualSynchDataToHis(currentTable, executorCode, macAddress);
        }

        /// <summary>
        /// ��֤ҽ����ʼʱ���ҽ�������Ƿ���ȷ
        /// </summary>
        /// <param name="currentTable">ָ����ҽ����</param>
        /// <param name="orderTemp">�����У��ֵ��ҽ����ʱ����</param>
        /// <param name="targetIndex">��ǰ�����ҽ���к�(�����Table���к�)(��������������ҽ�������кſ��ܲ���List��������)</param>
        /// <param name="updateFalg">�����Щ�����ѱ�����</param>
        /// <returns>����������ʹ�����Ϣ�б�����Grid������ʾ��Ϣ</returns>
        public void CheckOrderValueBeforeSet(OrderTable currentTable, Order orderTemp, int targetIndex, UpdateContentFlag updateFalg)
        {
            if (orderTemp == null)
                throw new ArgumentNullException(String.Format(ConstMessages.ExceptionFormatNoValue, ConstNames.TempOrder));

            if (m_ProcessModel == EditorCallModel.EditOrder) // ֻ�б༭ҽ��ʱ�Ź���ʱ����߼�
            {
                if (((updateFalg & UpdateContentFlag.StartDate) > 0)
                   && (orderTemp.State == OrderState.New)) // ��鿪ʼ����
                {
                    CheckOrderStartDatetime(currentTable, targetIndex, orderTemp.StartDateTime);
                    //����ǳ���ҽ���������޸��꿪ʼʱ�����ҪУ����ֹͣʱ��
                    if (!currentTable.IsTempOrder)
                    {
                        LongOrder orderLong = (LongOrder)orderTemp;
                        if ((orderLong != null) && (orderLong.CeaseInfo != null))
                            CheckCeaseTimeOfLongOrder(currentTable, orderLong);
                    }
                }
                if ((updateFalg & UpdateContentFlag.CeaseDate) > 0) // ��鳤��ҽ����ֹͣʱ��
                    CheckCeaseTimeOfLongOrder(currentTable, orderTemp);
            }

            if ((updateFalg & UpdateContentFlag.Content) > 0) // ���ҽ������
                CheckOrderContentData(currentTable, orderTemp.Content, targetIndex, GroupPositionKind.None);

            // ��������߼�
        }

        /// <summary>
        /// ��ȡָ��ҽ��Ϊ��ǰ������ָ��ʱ��֮���¿���ҩƷ��������Ŀҽ��
        /// </summary>
        /// <param name="doctorCode">ҽ������</param>
        /// <param name="startTime">ָ����ʱ��</param>
        /// <returns></returns>
        public DataTable GetNewOrder(string doctorCode, DateTime startTime)
        {
            if (String.IsNullOrEmpty(doctorCode))
                throw new ArgumentNullException(String.Format(ConstMessages.ExceptionFormatNoValue, ConstNames.DoctorId));

            if (m_OutputTable == null)
                GenerateOutputTable();
            else
                m_OutputTable.Clear();

            AddOrderDataIntoOutputTable(doctorCode, m_LongTable, true, startTime);
            AddOrderDataIntoOutputTable(doctorCode, m_TempTable, true, startTime);

            AddOrderDataIntoOutputTable(doctorCode, m_LongTable, false, startTime);
            AddOrderDataIntoOutputTable(doctorCode, m_TempTable, false, startTime);

            return m_OutputTable;
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
            if (String.IsNullOrEmpty(doctorCode))
                throw new ArgumentNullException(String.Format(ConstMessages.ExceptionFormatNoValue, ConstNames.DoctorId));

            if (m_OutputTable == null)
                GenerateOutputTable();
            else
                m_OutputTable.Clear();

            AddOrderDataIntoOutputTable(doctorCode, m_LongTable, needDrug, startTime);
            AddOrderDataIntoOutputTable(doctorCode, m_TempTable, needDrug, startTime);

            return m_OutputTable;
        }

        /// <summary>
        /// ��ȡƤ����ϢDataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetSkinTestResultData()
        {
            string commandText = String.Format(CultureInfo.CurrentCulture
               , ConstSqlSentences.FormatSelectSkinTestResult
               , CurrentPatient.NoOfFirstPage);
            return m_SqlExecutor.ExecuteDataTable(commandText);
        }

        /// <summary>
        /// ����Ƥ����Ϣ
        /// </summary>
        /// <param name="specSerialNo"></param>
        /// <param name="druggeryName"></param>
        /// <param name="skinTestResultKind"></param>
        public DataTable SaveSkinTestResult(string doctorCode, decimal specSerialNo, string druggeryName, SkinTestResultKind skinTestResultKind)
        {
            int plusFlag;
            if (skinTestResultKind == SkinTestResultKind.Plus)
                plusFlag = 1;
            else
                plusFlag = 0;

            string endDate;
            if (skinTestResultKind == SkinTestResultKind.MinusTreeDay)
                endDate = DateTime.Now.Date.AddDays(3).ToString(ConstFormat.FullDate);
            else
                endDate = "";

            string insertCommand = String.Format(CultureInfo.CurrentCulture
               , ConstSqlSentences.FormatInsertSkinTestResult
               , CurrentPatient.NoOfFirstPage, specSerialNo, DateTime.Now.ToString(ConstFormat.FullDate)
               , endDate, plusFlag, doctorCode, DateTime.Now.ToString(ConstFormat.FullDateTime));
            m_SqlExecutor.ExecuteNoneQuery(insertCommand);
            return GetSkinTestResultData();
        }

        /// <summary>
        /// ����Ƿ������ǰѡ��ҽ���ĺ������ҽ���������ǰѡ��ҽ��Ϊ�գ���ʾ�����ҽ��
        /// </summary>
        /// <param name="focusedOrder">��ǰѡ�е�ҽ��</param>
        /// <param name="isTempOrder">ָ�����Ǹ�ҽ�����������</param>
        /// <param name="insertOrders">Ҫ�����ҽ��</param>
        public void CheckCanInsertOrder(Order focusedOrder, bool isTempOrder, Order[] insertOrders)
        {
            // ��鵱ǰλ���Ƿ���Բ�����ҽ������ʾ���ܲ����ԭ��
            //    �Ƿ�����༭����ҽ��
            //    �Ƿ���ת��(����ҽ����Ӳ���)����Ժҽ��(����ҽ����ӡ��޸Ĳ������������ӳ�Ժ��ҩ)
            //    ��ǰҽ������Ƿ���������ҽ��
            //    ��ǰѡ���ҽ��״̬�Ƿ��������ҽ��
            //    ��ǰѡ���е���һ���Ƿ���Բ���ҽ�������ѡ��һ�У�
            //       ��һ��Ӧ������ҽ��(��Ϊ��)
            //       ��δѡ��ҽ��
            //    Ҫ�������λ���Ƿ��ڷ����У�����ǵĻ�����Ҫ����ܷ������飻�ܼ���Ļ������ڲ�������·��飬������ʾ
            OrderTable currentTable = GetCurrentOrderTable(isTempOrder);
            if (!currentTable.DefaultView.AllowEdit)
                throw new DataCheckException(ConstMessages.CheckEditableOfOrderCatalog, ConstMessages.ExceptionTitleOrderTable);
            if (!currentTable.DefaultView.AllowNew)
                throw new DataCheckException(ConstMessages.CheckCanInsertOrderAtSpecialState, ConstMessages.ExceptionTitleOrderTable);
            if (HasShiftDeptOrder)
                throw new DataCheckException(ConstMessages.CheckCantAddNewAfterHasShiftDeptOrder, ConstMessages.ExceptionTitleOrderTable);
            if ((HasOutHospitalOrder) && (!isTempOrder))
                throw new DataCheckException(ConstMessages.CheckOnlyAllowDruggeryAfterHasOutHospitalOrder, ConstMessages.ExceptionTitleOrderTable);
            if (focusedOrder != null)
            {
                int index = currentTable.Orders.IndexOf(focusedOrder.SerialNo);
                if ((index >= 0) && ((index + 1) < currentTable.Orders.Count))
                {
                    if (currentTable.Orders[index + 1].State != OrderState.New)
                        throw new DataCheckException(ConstMessages.CheckCanInsertOrderAfterCurrent, ConstMessages.ExceptionTitleOrderTable);
                    if (isTempOrder && HasOutHospitalOrder)
                    {
                        int indexOfOutHospital = currentTable.Orders.Count - 1;
                        for (int i = indexOfOutHospital; i >= 0; i--)
                        {
                            if (currentTable.Orders[i].Content.OrderKind == OrderContentKind.TextLeaveHospital)
                            {
                                indexOfOutHospital = i;
                                break;
                            }
                        }
                        if (index < indexOfOutHospital)
                            throw new DataCheckException(ConstMessages.CheckCanInsertOrderBeforeOutHospitalOrder, ConstMessages.ExceptionTitleOrderTable);
                    }
                }
                // ����Ƿ��ڷ�����
                CheckCanInsertOrdersToGroup(currentTable, focusedOrder, insertOrders);
            }
        }

        /// <summary>
        /// ������׻�Ҫճ����ҽ��
        /// </summary>
        /// <param name="currentTable"></param>
        /// <param name="docCode"></param>
        /// <param name="contents"></param>
        /// <param name="focusedOrder"></param>
        /// <returns>ʵ�ʲ���ļ�¼��</returns>
        public int InsertSuiteOrder(OrderTable currentTable, string docCode, object[,] contents, Order focusedOrder)
        {
            if ((contents == null) || (contents.GetUpperBound(0) < 0))
                return 0;

            Order order;
            decimal patientId = CurrentPatient.NoOfFirstPage;
            DateTime startTime = GetDefaultStartDateTime(currentTable.IsTempOrder);
            DateTime operateTime = DateTime.Now;
            OrderContent content;
            //bool skipOrder = false; // ����Ƿ�Ҫ����ͬ���ҽ��(��)
            int insertPos;

            if (focusedOrder == null)
                insertPos = currentTable.Orders.Count;
            else
                insertPos = currentTable.Orders.IndexOf(focusedOrder.SerialNo) + 1;

            //currentTable.DefaultView.BeginInit();
            int insertedNum = 0;

            for (int index = 0; index <= contents.GetUpperBound(0); index++)
            {
                content = contents[index, 0] as OrderContent;
                // �ȼ��ҽ�����ݣ���ͨ������������һ����������
                try
                {
                    CheckOrderContentData(currentTable, content, insertPos, (GroupPositionKind)contents[index, 1]);
                }
                catch
                {
                    continue;
                }

                order = currentTable.NewOrder();
                order.BeginInit();

                order.PatientId = patientId;
                order.OriginalDepartment = new Eop.Department(CurrentPatient.InfoOfAdmission.DischargeInfo.CurrentDepartment.Code);
                order.OriginalWard = new Eop.Ward(CurrentPatient.InfoOfAdmission.DischargeInfo.CurrentWard.Code);

                order.CreateInfo = new OrderOperateInfo(docCode, operateTime);
                order.StartDateTime = startTime;
                order.GroupPosFlag = (GroupPositionKind)Convert.ToInt32(contents[index, 1]);

                // ������飨û�д���һ��ҽ���в���ͨ���������������
                if ((order.GroupPosFlag == GroupPositionKind.SingleOrder)
                   || (order.GroupPosFlag == GroupPositionKind.GroupStart))
                    order.GroupSerialNo = order.SerialNo;
                else
                    order.GroupSerialNo = currentTable.Orders[currentTable.Orders.Count - 1].GroupSerialNo; // ����һ��һ��

                order.Content = content;

                order.EndInit();
                currentTable.InsertOrderAt(order, insertPos);

                insertPos++;
                insertedNum++;
            }

            return insertedNum;

            //currentTable.DefaultView.EndInit();
        }

        /// <summary>
        /// �������뵥����
        /// </summary>
        /// <param name="executorCode">���뵥�����ߴ���</param>
        /// <param name="macAddress">�����ߵ�������ַ</param>
        /// <param name="applySerialNo">���뵥���</param>
        /// <param name="executeDept">���뵥ִ�п���</param>
        /// <param name="itemCodeArray">���뵥��������Ŀ����</param>
        /// <param name="starTime">���뵥��ʼʱ��</param>
        /// <param name="operateTime">���뵥����ʱ��</param>
        /// <param name="operateType">����ά����������</param>
        [Obsolete(@"�����SaveRequestFormData(string executorCode, string macAddress, decimal applySerialNo, OrderInterfaceLogic.RequestFormCategory applyCategory,
         string executeDept, IList<OrderInterfaceLogic.RequestFormItem> itemArray, DateTime starTime, DateTime operateTime, RecordState operateType)")]
        public void SaveRequestFormData(string executorCode, string macAddress, decimal applySerialNo, string executeDept, string[] itemCodeArray, DateTime starTime, DateTime operateTime, RecordState operateType)
        {
            CheckCanInsertOrder(null, true, null);

            // ��������뵥ʱ��������Ŀ��������ʱҽ������������������뵥��
            // �޸����뵥ʱ�����ԭ�Ȳ��������ʱҽ�����������뵥���²�����ʱҽ��
            // ɾ�����뵥ʱ���������������ʱҽ��
            // ȡ�����뵥ʱ��ȡ����������ʱҽ��
            m_TempTable.DefaultView.BeginInit();
            if ((operateType == RecordState.Deleted) || (operateType == RecordState.Modified))
                DeleteOrderLinkedToRequest(applySerialNo);

            if ((operateType == RecordState.Added) || (operateType == RecordState.Modified))
                AddRequestItemIntoTempOrderTable(executorCode, applySerialNo, executeDept, itemCodeArray, starTime, operateTime);

            if (operateType == RecordState.Cancelled)
                CancelOrderLinkedToRequest(executorCode, applySerialNo, operateTime);
            m_TempTable.DefaultView.EndInit();

            // ������ʱҽ������
            // ��������Ƿ�����Լ��
            Order[] changedOrders = m_TempTable.GetNewAndChangedRequestOrder();
            if ((changedOrders == null) || (changedOrders.Length == 0))
                return;
            CheckOrderData(m_TempTable, changedOrders, false, true);
            // �ȴ浽���أ������Զ�����
            DoSaveChangedData(m_TempTable, changedOrders, executorCode, macAddress, false, false, false);
        }

        /// <summary>
        /// �������뵥����
        /// </summary>
        /// <param name="executorCode">���뵥�����ߴ���</param>
        /// <param name="macAddress">�����ߵ�������ַ</param>
        /// <param name="applySerialNo">���뵥���(���ּ���ͼ�����뵥������+-�ŵķ�ʽ������+�����-)</param>
        /// <param name="applyCategory">���뵥���</param>
        /// <param name="executeDept">���뵥ִ�п���</param>
        /// <param name="itemArray">���뵥��������Ŀ����</param>
        /// <param name="starTime">���뵥��ʼʱ��</param>
        /// <param name="operateTime">���뵥����ʱ��</param>
        /// <param name="operateType">����ά����������</param>
        public void SaveRequestFormData(string executorCode, string macAddress, decimal applySerialNo, OrderInterfaceLogic.RequestFormCategory applyCategory,
           string executeDept, IList<OrderInterfaceLogic.RequestFormItem> itemArray, DateTime starTime, DateTime operateTime, RecordState operateType)
        {
            CheckCanInsertOrder(null, true, null);

            // ��������뵥ʱ��������Ŀ��������ʱҽ������������������뵥��
            // �޸����뵥ʱ�����ԭ�Ȳ��������ʱҽ�����������뵥���²�����ʱҽ��
            // ɾ�����뵥ʱ���������������ʱҽ��
            // ȡ�����뵥ʱ��ȡ����������ʱҽ��
            m_TempTable.DefaultView.BeginInit();

            applySerialNo = OrderInterfaceLogic.GetSend2HisApplySerialNo(applySerialNo, applyCategory);

            if ((operateType == RecordState.Deleted) || (operateType == RecordState.Modified))
                DeleteOrderLinkedToRequest(applySerialNo);

            if ((operateType == RecordState.Added) || (operateType == RecordState.Modified))
                AddRequestItemIntoTempOrderTable(executorCode, applySerialNo, executeDept, itemArray, starTime, operateTime);

            if (operateType == RecordState.Cancelled)
                CancelOrderLinkedToRequest(executorCode, applySerialNo, operateTime);
            m_TempTable.DefaultView.EndInit();

            // ������ʱҽ������
            // ��������Ƿ�����Լ��
            Order[] changedOrders = m_TempTable.GetNewAndChangedRequestOrder();
            if ((changedOrders == null) || (changedOrders.Length == 0))
                return;
            CheckOrderData(m_TempTable, changedOrders, false, true);
            // �ȴ浽���أ������Զ�����
            // DoSaveChangedData(m_TempTable, changedOrders, executorCode, macAddress, false, false, false);
            // wxg -- 2009-02-13 �Զ���������, 6Ժ�ȿ� autoDeleteNewOrder = true
            DoSaveChangedData(m_TempTable, changedOrders, executorCode, macAddress, false, true, true);
        }

        /// <summary>
        /// ���浱ǰ�༭�ĳ��׵���ϸ����
        /// </summary>
        public void SaveCurrentSuiteDetailData()
        {
            // ������ı�ͬ����Table ��
            try
            {
                Order[] changedOrders = m_TempTable.GetNewAndChangedOrders();
                m_TempTable.SyncObjectData2Table(changedOrders, true);

                changedOrders = m_LongTable.GetNewAndChangedOrders();
                m_LongTable.SyncObjectData2Table(changedOrders, true);

                // ���� SuiteHelper �� SaveDetail ����
                SuiteHelper.SaveSuiteDetailData();

                m_TempTable.AcceptChanges();
                m_LongTable.AcceptChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ����ͬ��δ���͵�ҽ����HIS��,���ӿڷ�������
        /// </summary>
        /// <param name="executorCode"></param>
        /// <param name="macAddress"></param>
        public void ResendSynchRecordsToHis(string executorCode, string macAddress)
        {
            OrderTable currentTable = GetCurrentOrderTable(true);
            SynchDataToHIS(currentTable, executorCode, macAddress);

            currentTable = GetCurrentOrderTable(false);
            SynchDataToHIS(currentTable, executorCode, macAddress);
        }

        /// <summary>
        /// ��ͬ��ִ�н������ʱ����Ϣ
        /// </summary>
        /// <param name="changedData">��ִ�л���ֹͣ��ҽ��</param>
        /// <param name="isTemp">����Ƿ�����ʱҽ��</param>
        internal void HandleQcMessageAfterSynchExecute(DataTable changedData, bool isTemp)
        {
            // ֹͣΣ��ҽ����Ҫ���´���ʱ����Ϣ
            // ִ��Σ�ء�ת�ơ�����ҽ���󣬴���ʱ����Ϣ
            OrderTable table = new OrderTable(changedData, isTemp, m_SqlExecutor);
            OperationOrderContent opContent;

            if (isTemp)
            {
                foreach (Order order in table.Orders)
                {
                    switch (order.Content.OrderKind)
                    {
                        case OrderContentKind.TextShiftDept:
                            SendQcMessage(order.CreateInfo.Executor.Code, QCConditionType.AdviceChange, order, order.StartDateTime);
                            break;
                        case OrderContentKind.Operation:
                            opContent = order.Content as OperationOrderContent;
                            if (opContent != null)
                                SendQcMessage(order.CreateInfo.Executor.Code, QCConditionType.AdviceChange, order, opContent.OperationTime);
                            break;
                    }
                }
            }
            else
            {
                LongOrder longOrder;
                foreach (Order order in table.Orders)
                {
                    if ((order.Content.Item.Kind == ItemKind.DangerLevel)
                       && (order.Content.Item.Memo.Trim().Equals("1"))) // 1��ʾΣ��
                    {
                        if (order.State == OrderState.Executed)
                            SendQcMessage(order.CreateInfo.Executor.Code, QCConditionType.AdviceChange, order, order.StartDateTime);
                        else if (order.State == OrderState.Ceased)
                        {
                            longOrder = order as LongOrder;
                            SendQcMessage(longOrder.CeaseInfo.Executor.Code, QCConditionType.PatStateChange, CurrentPatient, longOrder.CeaseInfo.ExecuteTime);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
