using DrectSoft.Common.Eop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DrectSoft.Core.DoctorAdvice
{
    /// <summary>
    /// ҽ���ӿڴ����ࡣ�ṩҽ����ˡ����ݱ���ȴ���ӿ�
    /// </summary>
    public class OrderInterfaceLogic
    {
        #region public properties
        /// <summary>
        /// ��ǰ����Ĳ��ˡ��ڵ��ô�����ǰҪ�������Ը�ֵ
        /// </summary>
        public Inpatient CurrentPatient
        {
            get { return _currentPatient; }
            set
            {
                _currentPatient = value;
                if (value != null)
                {
                    if (m_CoreLogic == null)
                        IniializeCoreLogic(value);
                    else
                        m_CoreLogic.CurrentPatient = value;
                }
            }
        }
        private Inpatient _currentPatient;

        /// <summary>
        /// ������HIS�е���ҳ��š�����ֱ�Ӹ������Ը�ֵ���ﵽ�л����˵�Ŀ��
        /// </summary>
        public string FirstPageNoOfHis
        {
            get
            {
                if (CurrentPatient != null)
                    return CurrentPatient.NoOfHisFirstPage;
                else
                    return "-1";
            }
            set
            {
                CurrentPatient = CreatePatientByFirstpageNoOfHis(value);
            }
        }

        /// <summary>
        /// �����ߵ�������ַ
        /// </summary>
        public string MacAddress
        {
            get { return _macAddress; }
            set { _macAddress = value; }
        }
        private string _macAddress;
        #endregion

        #region private variables & properties
        private CoreBusinessLogic m_CoreLogic;
        private IDataAccess m_SqlExecutor;

        private bool HadInitialized
        {
            get { return (CurrentPatient != null); }
        }
        #endregion

        #region ctors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlExecutor"></param>
        public OrderInterfaceLogic(IDataAccess sqlExecutor, string macAddress)
        {
            if (sqlExecutor == null)
                throw new ArgumentNullException("���ݷ������Ϊ��");
            m_SqlExecutor = sqlExecutor;
            _macAddress = macAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlExecutor"></param>
        /// <param name="inpatient"></param>
        public OrderInterfaceLogic(IDataAccess sqlExecutor, string macAddress, Inpatient inpatient)
            : this(sqlExecutor, macAddress)
        {
            if (inpatient == null)
                throw new ArgumentNullException("�ղ���");

            CurrentPatient = inpatient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlExecutor"></param>
        /// <param name="firstpageNoOfHis">������HIS�е���ҳ���</param>
        public OrderInterfaceLogic(IDataAccess sqlExecutor, string macAddress, string firstpageNoOfHis)
            : this(sqlExecutor, macAddress)
        {
            if (firstpageNoOfHis == "-1")
                throw new ArgumentNullException("�ղ���");

            FirstPageNoOfHis = firstpageNoOfHis;
        }
        #endregion

        #region private methods
        private static int CompareSerialNoOfOrder(Order x, Order y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (y == null)
                    return 1;
                else
                    return x.SerialNo.CompareTo(y.SerialNo);
            }
        }

        private static Order[] GetOrdesBySerialNo(OrderTable table, decimal[] serialNums)
        {
            if ((serialNums == null) || (serialNums.Length == 0))
                return new Order[] { };

            List<Order> orders = new List<Order>();
            Order order;
            foreach (decimal serialNo in serialNums)
            {
                order = table.Orders[table.Orders.IndexOf(serialNo)];
                if (order != null)
                    orders.Add(order);
            }
            // ������Ž�������
            orders.Sort(CompareSerialNoOfOrder);
            Order[] result = new Order[orders.Count];
            orders.CopyTo(result);
            return result;
        }

        private Inpatient CreatePatientByFirstpageNoOfHis(string firstpageNoOfHis)
        {
            decimal firstpageNo = (decimal)m_SqlExecutor.ExecuteScalar(
               String.Format("select syxh from BL_BRSYK where hissyxh = {0}", firstpageNoOfHis));

            Inpatient inpatient = new Inpatient(firstpageNo);
            inpatient.NoOfHisFirstPage = firstpageNoOfHis;

            return inpatient;
        }

        private void IniializeCoreLogic(Inpatient patient)
        {
            if (patient != null)
            {
                m_CoreLogic = new CoreBusinessLogic(m_SqlExecutor, EditorCallModel.EditOrder);
                m_CoreLogic.CurrentPatient = patient;
            }
            else
                throw new ArgumentNullException("����δ��ֵ");
        }

        private string CombineSerialNo(DataTable dataTable, string serialNoField)
        {
            StringBuilder serialNoLine = new StringBuilder("0");
            foreach (DataRow row in dataTable.Rows)
                serialNoLine.Append("," + row[serialNoField].ToString());
            return serialNoLine.ToString();
        }

        private void SynchLongOrderExecResultToEmr(DataTable longOrderTable)
        {
            if ((longOrderTable != null) && (longOrderTable.Rows.Count > 0))
            {
                OrderTable currentTable = m_CoreLogic.GetCurrentOrderTable(false);
                if (currentTable.Orders.Count == 0)
                {
                    if (CoreBusinessLogic.BusinessLogic.UsedForAllPatient)
                        throw new ArgumentOutOfRangeException("����ĳ���ҽ�������ڵ��Ӳ���ϵͳ�в�����");
                    else
                        return;
                }
                currentTable.OrderDataTable.Merge(longOrderTable);
                string changedSerialNo = CombineSerialNo(longOrderTable, "cqyzxh");
                DataRow[] changedRows = currentTable.OrderDataTable.Select("cqyzxh in (" + changedSerialNo + ")");
                Dictionary<decimal, DataRow> changedHerbSummaries = new Dictionary<decimal, DataRow>();

                foreach (DataRow row in changedRows)
                {
                    if (row["memo"].ToString().StartsWith(Order.HerbSummaryFlag))
                        changedHerbSummaries.Add(Convert.ToDecimal(row["memo"].ToString().Substring(Order.HerbSummaryFlag.Length - 1)), row);
                }
                // ͬ�������Ĳ�ҩ��ϸ��״̬
                if (changedHerbSummaries.Count > 0)
                {
                    decimal groupSerialNo;

                    foreach (DataRow row in currentTable.OrderDataTable.Rows)
                    {
                        groupSerialNo = Convert.ToDecimal(row["fzxh"]);
                        if (changedHerbSummaries.ContainsKey(groupSerialNo))
                        {
                            row["yzzt"] = changedHerbSummaries[groupSerialNo]["yzzt"];
                            row["zxczy"] = changedHerbSummaries[groupSerialNo]["zxczy"];
                            row["zxrq"] = changedHerbSummaries[groupSerialNo]["zxrq"];
                        }
                    }
                }

                DataTable changedTable = currentTable.OrderDataTable.GetChanges();
                m_SqlExecutor.UpdateTable(currentTable.OrderDataTable, CoreBusinessLogic.GetOrderTableName(currentTable.IsTempOrder), false);
                //currentTable.OrderDataTable.AcceptChanges();
                // ��������������Ϣ
                m_CoreLogic.HandleQcMessageAfterSynchExecute(changedTable, false);
            }
        }

        private void SynchTempOrderExecResultToEmr(DataTable tempOrderTable)
        {
            // ֱ�Ӷ����ݼ����кϲ���Ȼ�󱣴浽EMR���ݿ�(�����ᵼ��OrderTable�����ݼ����ݺͶ������Բ�һ��)
            if ((tempOrderTable != null) && (tempOrderTable.Rows.Count > 0))
            {
                OrderTable currentTable = m_CoreLogic.GetCurrentOrderTable(true);
                if (currentTable.Orders.Count == 0)
                {
                    if (CoreBusinessLogic.BusinessLogic.UsedForAllPatient)
                        throw new ArgumentOutOfRangeException("�������ʱҽ�������ڵ��Ӳ���ϵͳ�в�����");
                    else
                        return;
                }
                currentTable.OrderDataTable.Merge(tempOrderTable);

                string changedSerialNo = CombineSerialNo(tempOrderTable, "lsyzxh");
                DataRow[] changedRows = currentTable.OrderDataTable.Select("lsyzxh in (" + changedSerialNo + ")");

                Dictionary<decimal, DataRow> changedHerbSummaries = new Dictionary<decimal, DataRow>();
                StringBuilder reqSerialNos = new StringBuilder("0");
                foreach (DataRow row in changedRows)
                {
                    if ((row["sqdxh"].ToString() != "") && (Convert.ToDecimal(row["sqdxh"]) != 0))
                        reqSerialNos.Append("," + row["sqdxh"].ToString());
                    else if (row["memo"].ToString().StartsWith(Order.HerbSummaryFlag))
                        changedHerbSummaries.Add(Convert.ToDecimal(row["memo"].ToString().Substring(Order.HerbSummaryFlag.Length - 1)), row);
                }
                // ͬ�������Ĳ�ҩ��ϸ��״̬
                if (changedHerbSummaries.Count > 0)
                {
                    decimal groupSerialNo;
                    foreach (DataRow row in currentTable.OrderDataTable.Rows)
                    {
                        groupSerialNo = Convert.ToDecimal(row["fzxh"]);
                        if (changedHerbSummaries.ContainsKey(groupSerialNo))
                        {
                            row["yzzt"] = changedHerbSummaries[groupSerialNo]["yzzt"];
                            row["zxczy"] = changedHerbSummaries[groupSerialNo]["zxczy"];
                            row["zxrq"] = changedHerbSummaries[groupSerialNo]["zxrq"];
                        }
                    }
                }

                DataTable changedTable = currentTable.OrderDataTable.GetChanges();
                m_SqlExecutor.UpdateTable(currentTable.OrderDataTable, CoreBusinessLogic.GetOrderTableName(currentTable.IsTempOrder), false);
                //currentTable.OrderDataTable.AcceptChanges();
                // �����������뵥״̬�ĳ���ִ�� 
                if (reqSerialNos.Length > 1)
                    m_SqlExecutor.ExecuteNoneQuery("update BL_SQDK set qrbz = 2 where xh in (" + reqSerialNos.ToString() + ")");

                // ��������������Ϣ
                m_CoreLogic.HandleQcMessageAfterSynchExecute(changedTable, true);
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// EMRҽ����˽ӿ�,���ص��쳣�а�������˲�ͨ����ԭ��
        /// ����ĵ����߹��ź�������ַ�ڵ���HIS�ӿ���Ҫ�õ���Ҫ�����ͻ��
        /// </summary>
        /// <param name="executorCode">������˹��̵Ĳ���Ա����</param>
        /// <param name="macAddress">������ַ</param>
        /// <param name="selectedLongs">Ҫ��˵ĳ���ҽ����ż���</param>
        /// <param name="selectedTemps">Ҫ��˵���ʱҽ����ż���</param>
        /// <param name="auditor">����߹���</param>
        /// <param name="auditTime">���ʱ��</param>
        public void AuditOrder(string executorCode, string macAddress, decimal[] selectedLongs, decimal[] selectedTemps, string auditor, DateTime auditTime)
        {
            if (!CoreBusinessLogic.BusinessLogic.EnableEmrOrderModul)
                return;

            if (!HadInitialized)
                throw new ArgumentException(String.Format(ConstMessages.ExceptionFormatNullObject, ConstNames.Inpatient));

            // ������������飬Ȼ��ִ����˵Ĺ��̣���󱣴���º�����ݣ��浽���أ���ͬ����HIS��
            try
            {
                OrderTable currentTable;
                Order[] selectedOrders = null;
                if (selectedTemps.Length > 0)
                {
                    currentTable = m_CoreLogic.GetCurrentOrderTable(true);
                    if (currentTable.Orders.Count == 0)
                    {
                        if (CoreBusinessLogic.BusinessLogic.UsedForAllPatient)
                            throw new ArgumentOutOfRangeException("�������ʱҽ�������ڵ��Ӳ���ϵͳ�в�����");
                        else
                            return;
                    }
                    //currentTable.DefaultView.BeginInit();
                    selectedOrders = GetOrdesBySerialNo(currentTable, selectedTemps);
                    m_CoreLogic.AuditOrder(selectedOrders, auditor, auditTime, currentTable.IsTempOrder);
                    //currentTable.DefaultView.EndInit();
                }
                if (selectedLongs.Length > 0)
                {
                    currentTable = m_CoreLogic.GetCurrentOrderTable(false);
                    if (currentTable.Orders.Count == 0)
                    {
                        if (CoreBusinessLogic.BusinessLogic.UsedForAllPatient)
                            throw new ArgumentOutOfRangeException("����ĳ���ҽ�������ڵ��Ӳ���ϵͳ�в�����");
                        else
                            return;
                    }
                    //currentTable.DefaultView.BeginInit();
                    m_CoreLogic.AuditOrder(GetOrdesBySerialNo(currentTable, selectedLongs), auditor, auditTime, currentTable.IsTempOrder);
                    //currentTable.DefaultView.EndInit();
                }
                // ��������
                m_CoreLogic.SaveAllChangedOrderData(executorCode, macAddress, true);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ͬ��ҽ��ִ�н���ӿ�(��HIS����)
        /// ��������ݼ��а���
        ///       cqyzxh  utXh12      --����ҽ����� ������ lsyzxh��
        /// 	zxczy   utMc16      --ִ�в���Ա(����)
        /// 	zxrq    utDatetime  --ִ������
        ///       yzzt    utBz        --ҽ��״̬(EMR�ж����״̬)
        /// </summary>
        /// <param name="longOrderTable">��ִ�еĳ���ҽ�����ݼ�</param>
        /// <param name="tempOrderTable">��ִ�е���ʱҽ�����ݼ�</param>
        public void SynchExecuteResultToEmr(DataTable longOrderTable, DataTable tempOrderTable)
        {
            if (!CoreBusinessLogic.BusinessLogic.EnableEmrOrderModul)
                return;

            SynchTempOrderExecResultToEmr(tempOrderTable);

            SynchLongOrderExecResultToEmr(longOrderTable);
        }

        /// <summary>
        /// �������뵥����(����)
        /// </summary>
        /// <param name="executorCode">���뵥�����ߴ���</param>
        /// <param name="applySerialNo">���뵥���</param>
        /// <param name="executeDept">���뵥ִ�п���</param>
        /// <param name="itemCodeArray">���뵥��������Ŀ����</param>
        /// <param name="starTime">���뵥��ʼʱ��</param>
        /// <param name="operateTime">���뵥����ʱ��</param>
        /// <param name="operateType">����ά����������</param>
        [Obsolete(@"�����·���SaveRequestFormData(string executorCode, decimal applySerialNo, RequestFormCategory applyCategory,  
         string executeDept, IList<RequestFormItem> itemArray, DateTime starTime, DateTime operateTime, RecordState operateType)")]
        public void SaveRequestFormData(string executorCode, decimal applySerialNo, string executeDept, string[] itemCodeArray, DateTime starTime, DateTime operateTime, RecordState operateType)
        {
            if (!CoreBusinessLogic.BusinessLogic.EnableEmrOrderModul)
                return;
            m_CoreLogic.SaveRequestFormData(executorCode, MacAddress, applySerialNo, executeDept, itemCodeArray, starTime, operateTime, operateType);
        }

        /// <summary>
        /// �������뵥����(֧���ٴ���Ŀ)
        /// </summary>
        /// <param name="executorCode">���뵥�����ߴ���</param>
        /// <param name="applySerialNo">���뵥���</param>
        /// <param name="executeDept">���뵥ִ�п���</param>
        /// <param name="itemArray">���뵥��������Ŀ����</param>
        /// <param name="starTime">���뵥��ʼʱ��</param>
        /// <param name="operateTime">���뵥����ʱ��</param>
        /// <param name="operateType">����ά����������</param>
        public void SaveRequestFormData(string executorCode, decimal applySerialNo, RequestFormCategory applyCategory,
           string executeDept, IList<RequestFormItem> itemArray, DateTime starTime, DateTime operateTime, RecordState operateType)
        {
            if (!CoreBusinessLogic.BusinessLogic.EnableEmrOrderModul)
                return;

            m_CoreLogic.SaveRequestFormData(executorCode, MacAddress, applySerialNo, applyCategory, executeDept, itemArray, starTime, operateTime, operateType);
        }

        public static decimal GetSend2HisApplySerialNo(decimal applySerialNo, OrderInterfaceLogic.RequestFormCategory applyCategory)
        {
            switch (applyCategory)
            {
                case OrderInterfaceLogic.RequestFormCategory.CL:
                    return -applySerialNo;
                default:
                    break;
            }

            return applySerialNo;
        }

        /// <summary>
        /// ��ȡָ��ҽ��Ϊ��ǰ������ָ��ʱ��֮���¿���ҩƷ��������Ŀҽ��
        /// </summary>
        /// <param name="doctorCode">ҽ������</param>
        /// <param name="startTime">ָ����ʱ��</param>
        /// <returns></returns>
        public DataTable GetNewOrder(string doctorCode, DateTime startTime)
        {
            return m_CoreLogic.GetNewOrder(doctorCode, startTime);
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
        /// �Զ�������ҽ����״̬���ڲ���״̬���ڲ������ת�ƻ�������Լ�����״̬�ı������ʱ����
        /// </summary>
        /// <param name="ceaeReason">ͣҽ����ԭ��</param>
        /// <param name="isRollback">����Ƿ��ǳ�������״̬�ı����ʱ����</param>
        public void AutoHandleLongOrderState(string executorCode, OrderCeaseReason ceaseReason, bool isRollback)
        {
            if (!CoreBusinessLogic.BusinessLogic.EnableEmrOrderModul)
                return;

            LongOrder ltemp;
            DateTime rollbackTime = DateTime.Now;
            foreach (Order order in m_CoreLogic.GetCurrentOrderTable(false).Orders)
            {
                if ((order.State == OrderState.Audited) || (order.State == OrderState.New))
                    break;
                ltemp = order as LongOrder;
                if ((ltemp.State == OrderState.Executed) && (!isRollback))
                {
                    // ��ִ��״ֱ̬�Ӹĳ�ֹͣ״̬
                    ltemp.SetStateToCeased();
                }
                else if ((order.State == OrderState.Ceased) && (isRollback))
                {
                    // ���ֹͣԭ��ʹ���ԭ��һ�£�����ֹͣʱ���ֹͣʱ�����ڵ�ǰ���ڣ���ֹͣ״̬�ĳ�ִ��״̬
                    ltemp.RollbackStateOfCeasedOrder(ceaseReason, rollbackTime);
                }
            }

            // ����ı������(�Զ�ͬ����HIS��)
            m_CoreLogic.SaveAllChangedOrderData(executorCode, MacAddress, true);
        }

        /// <summary>
        /// ˢ�µ�ǰ���˵�ҽ������
        /// </summary>
        public void RefreshPatientData()
        {
            CurrentPatient = CurrentPatient;
        }

        /// <summary>
        /// ����ͬ��δ���͵�ҽ����HIS��
        /// TODO������ͬ���Ĵ������Ǻ��ϸ񡣻�����ϸ�о���α�֤EMR��HIS֮�������һ����
        /// </summary>
        /// <param name="executorCode"></param>
        /// <param name="macAddress"></param>
        public void ResendSynchRecords(string executorCode, string macAddress)
        {
            if (!CoreBusinessLogic.BusinessLogic.EnableEmrOrderModul)
                return;
            m_CoreLogic.ResendSynchRecordsToHis(executorCode, macAddress);
        }

        /// <summary>
        /// ��ָ���ĳ���ҽ����Ϊ��ҽ������
        /// </summary>
        /// <param name="suiteSerialNo"></param>
        public void InsertSuiteOrderAsNewOrder(string doctor, decimal suiteSerialNo)
        {
            if (suiteSerialNo <= 0)
                return;
            DataTable detailTable = m_SqlExecutor.ExecuteDataTable("select * from YY_CTYZMXK where ctyzxh = " + suiteSerialNo.ToString()
               , CommandType.Text);

            // �ȴ�����ʱҽ��
            DataRow[] selectedRows = detailTable.Select(ConstSchemaNames.SuiteDetailColAmount + " > 0 and yzbz in (2700, 2702)");
            InsertTempSuiteOrderIntoTable(doctor, selectedRows, true);
            selectedRows = detailTable.Select(ConstSchemaNames.SuiteDetailColAmount + " > 0 and yzbz in (2700, 2703)");
            InsertTempSuiteOrderIntoTable(doctor, selectedRows, false);
            m_CoreLogic.SaveAllChangedOrderData(doctor, MacAddress, true);

        }

        private void InsertTempSuiteOrderIntoTable(string doctor, DataRow[] selectedRows, bool isTempOrder)
        {
            if (selectedRows.Length == 0)
                return;

            object[,] selectedContents = new object[selectedRows.Length, 2];

            // �ݲ������Ժ��ҩbool needCalcTotalAmount = false ; 
            string checkMsg;
            DataRow row;
            OrderContent content;
            try
            {
                for (int index = 0; index < selectedRows.Length; index++)
                {
                    row = selectedRows[index];
                    selectedContents[index, 1] = (GroupPositionKind)Convert.ToInt32(row[ConstSchemaNames.SuiteDetailColGroupFlag]);
                    content = PersistentObjectFactory.CreateAndIntializeObject(
                       OrderContentFactory.GetOrderContentClassName(row[ConstSchemaNames.SuiteDetailColOrderCatalog]), row) as OrderContent;
                    content.ProcessCreateOutputeInfo = new OrderContent.GenerateOutputInfo(CustomDrawOperation.CreateOutputeInfo);

                    //checkMsg = content.CheckProperties();
                    //if (!String.IsNullOrEmpty(checkMsg))
                    //   m_MessageBox.MessageShow(checkMsg, CustomMessageBoxKind.InformationOk);

                    selectedContents[index, 0] = content;
                }
                //// TODO: ��ʱ����ҽ�����
                m_CoreLogic.CheckCanInsertOrder(null, isTempOrder, null);
                m_CoreLogic.InsertSuiteOrder(m_CoreLogic.GetCurrentOrderTable(isTempOrder), doctor, selectedContents, null);
            }
            catch { }
        }
        #endregion

        #region Class for RequestForm FeeItems Save

        /// <summary>
        /// ���Ա�������뵥��Ӧ���շ���Ŀ��Ϣ
        /// </summary>
        public class RequestFormItem
        {
            string _itemCode;
            int _itemKind;
            private string _memo;
            private string _specimen;
            private string _specimenId;
            private int _jzbz;
            private string _execDept;

            /// <summary>
            /// ��Ŀ����
            /// </summary>
            public string Code { get { return _itemCode; } }

            /// <summary>
            /// ��Ŀ���
            /// </summary>
            public int Kind { get { return _itemKind; } }

            /// <summary>
            /// ���뵥��ע
            /// </summary>
            public string Memo { get { return _memo; } }

            /// <summary>
            /// �걾
            /// </summary>
            public string Specimen { get { return _specimen; } }

            /// <summary>
            /// �걾
            /// </summary>
            public string SpecimenId { get { return _specimenId; } }

            /// <summary>
            /// �Ӽ���־
            /// </summary>
            public int Urgent { get { return _jzbz; } }

            /// <summary>
            /// ִ�п���
            /// </summary>
            public string ExecDept { get { return _execDept; } }

            /// <summary>
            /// ����һ�����뵥��Ӧ���շ���Ŀ
            /// </summary>
            /// <param name="code">��Ŀ����</param>
            /// <param name="kind">0 �շ�С��Ŀ 1 �ٴ���Ŀ</param>
            public RequestFormItem(string code, int kind)
            {
                _itemCode = code;
                _itemKind = kind;
            }

            public RequestFormItem(string code, int kind, string specimenId, string specimen, int urgent, string memo)
            {
                this._itemCode = code;
                this._itemKind = kind;
                this._specimenId = specimenId;
                this._specimen = specimen;
                this._memo = memo;
                this._jzbz = urgent;
            }

            public RequestFormItem(string code, int kind, string specimenId, string specimen, int urgent, string memo, string execDept)
            {
                this._itemCode = code;
                this._itemKind = kind;
                this._specimenId = specimenId;
                this._specimen = specimen;
                this._memo = memo;
                this._jzbz = urgent;
                this._execDept = execDept;
            }
        }

        public enum RequestFormCategory
        {
            /// <summary>
            /// ����
            /// </summary>
            TF = 0,

            /// <summary>
            /// ���
            /// </summary>
            CL = 1,
        }
        #endregion
    }
}
