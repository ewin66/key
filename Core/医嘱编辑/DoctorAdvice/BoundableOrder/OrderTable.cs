using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using DrectSoft.Common.Eop;
using System.Globalization;
using System.Collections.ObjectModel;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// 医嘱表类(仿照DataTable的功能构造)，用来封装长期和临时医嘱表
   /// </summary>
   public class OrderTable : IListSource
   {
      #region custom properties
      /// <summary>
      /// IDataAccess实例
      /// </summary>
      public IDataAccess SqlExecutor
      {
         get { return _sqlExecutor; }
         set
         {
            _sqlExecutor = value;
            // 要给类型为持久类的属性传递SqlExecutor
            // SetChildrenExecutor();
         }
      }
      private IDataAccess _sqlExecutor;

      /// <summary>
      /// 保存对应医嘱数据的DataTable
      /// </summary>
      public DataTable OrderDataTable
      {
         get { return _orderDataTable; }
      }
      private DataTable _orderDataTable;

      /// <summary>
      /// 标记当前对象保存的是临时还是长期医嘱数据，true表示是临时
      /// </summary>
      public bool IsTempOrder
      {
         get { return _isTempOrder; }
      }
      private bool _isTempOrder;

      /// <summary>
      /// 医嘱对象集合
      /// </summary>
      public OrderCollection Orders
      {
         get { return _orders; }
      }
      private OrderCollection _orders;

      /// <summary>
      /// 医嘱对象表的缺省视图
      /// </summary>
      public OrderTableView DefaultView
      {
         get
         {
            if (_defaultView == null)
               _defaultView = new OrderTableView(this);
            return _defaultView;
         }
      }
      private OrderTableView _defaultView;

      /// <summary>
      /// 是否有新增的医嘱
      /// </summary>
      public bool HasNewAddedOrder
      {
         get
         {
            for (int index = Orders.Count - 1; index >= 0; index--)
               if (Orders[index].State == OrderState.New)
                  return true;
            return false;
         }
      }

      /// <summary>
      /// 标记医嘱中是否包含有效的“出院医嘱”（没有被取消的）
      /// 只会在临时医嘱中出现
      /// </summary>
      public bool HasOutHospitalOrder
      {
         get { return _hasOutHospitalOrder; }
      }
      private bool _hasOutHospitalOrder;

      /// <summary>
      /// 标记医嘱中是否包含有效的“转科医嘱”(新的、还未执行，可能已审核)
      /// 长期、临时医嘱中都会出现
      /// </summary>
      public bool HasShiftDeptOrder
      {
         get { return _hasShiftDeptOrder; }
      }
      private bool _hasShiftDeptOrder;

      /// <summary>
      /// 标记数据是否发生改变
      /// </summary>
      public bool HadChanged
      {
         get { return _hadChanged; }
      }
      private bool _hadChanged;

      /// <summary>
      /// 标记是否还有未发送的数据
      /// </summary>
      public bool HasNotSendData
      {
         get { return _hasNotSendData; }
      }
      private bool _hasNotSendData;
      #endregion

      #region private variables
      /// <summary>
      /// 标记是否正在处理对象数据
      /// </summary>
      private bool m_IsEditing;

      /// <summary>
      /// 收容所有被Remove或delete的医嘱
      /// </summary>
      private List<Order> m_RemovedCollection;
      #endregion

      #region custom event handlers
      /// <summary>
      /// 医嘱列表改变事件
      /// </summary>
      public event ListChangedEventHandler ListChanged
      {
         add
         {
            onListChanged = (ListChangedEventHandler)Delegate.Combine(onListChanged, value);
         }
         remove
         {
            onListChanged = (ListChangedEventHandler)Delegate.Remove(onListChanged, value);
         }
      }
      private ListChangedEventHandler onListChanged;

      protected void FireListChanged(ListChangedEventArgs e)
      {
         // 如果正在编辑数据，则不触发改变事件
         if (m_IsEditing)
            return;

         _hadChanged = true;
         _hasNotSendData = true;
         // 调用医嘱对象集合的列表更新处理函数（此时原始的医嘱对象列表已经更新）
         Orders.AfterListChanged(e);

         if (onListChanged != null)
            onListChanged(this, e);
      }

      /// <summary>
      /// 出院医嘱改变事件(增加或被删除、取消)。
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

      private void FireOutHospitalOrderChanged(EventArgs e)
      {
         if (onOutHospitalOrderChanged != null)
            onOutHospitalOrderChanged(this, e);
      }
      #endregion

      #region ctors
      /// <summary>
      /// 根据传入的医嘱表数据创建临时或长期医嘱的OrderTable对象
      /// </summary>
      /// <param name="orderData">医嘱数据</param>
      /// <param name="isTempOrder">临时、长期标志,true 表示是临时</param>
      public OrderTable(DataTable orderData, bool isTempOrder, IDataAccess sqlExecutor)
      {
         _orderDataTable = orderData;
         _isTempOrder = isTempOrder;
         _sqlExecutor = sqlExecutor;
         _orders = new OrderCollection(this);
         // orderData.RowChanged += new DataRowChangeEventHandler(RowChanged);
         // 初始化“出院医嘱”和“转科医嘱”的存在标志         
         _hasOutHospitalOrder = SearchOutHospitalOrder();
         _hasShiftDeptOrder = SearchShiftDeptOrder();

         m_RemovedCollection = new List<Order>();
         _hadChanged = false;
         _hasNotSendData = false;
         foreach (Order order in _orders)
         {
            if (!order.HadSync)
            {
               _hasNotSendData = true;
               break;
            }
         }
      }
      #endregion

      #region internal methods
      /// <summary>
      /// 添加医嘱对象
      /// </summary>
      /// <param name="order"></param>
      internal void AddOrder(Order order)
      {
         Validate(-1, order);
         order.OrderChanged += new EventHandler<OrderChangedEventArgs>(AfterOrderChanged);
         Orders.OrderList.Add(order);
         ResetSpecialFlag(order, ListChangedType.ItemAdded);

         FireListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, Orders.Count - 1));
      }

      /// <summary>
      /// 清空医嘱对象
      /// </summary>
      internal void Clear()
      {
         m_IsEditing = true;
         for (int index = Orders.Count - 1; index >= 0; index--)
         {
            RemoveOrder(Orders[index]);
         }
         m_IsEditing = false;
         _hasOutHospitalOrder = false;
         _hasShiftDeptOrder = false;

         FireOutHospitalOrderChanged(new EventArgs());
         FireListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
      }

      internal void InsertOrderAt(Order order, int pos)
      {
         if (pos < 0)
         {
            throw new IndexOutOfRangeException(pos.ToString(CultureInfo.CurrentCulture));
         }
         Validate(-1, order);

         if (pos >= Orders.Count) // 在列表末尾插入
         {
            AddOrder(order);
         }
         else
         {
            order.OrderChanged -= new EventHandler<OrderChangedEventArgs>(AfterOrderChanged);
            order.OrderChanged += new EventHandler<OrderChangedEventArgs>(AfterOrderChanged);
            Orders.OrderList.Insert(pos, order);// .InsertAt(order, pos);
            ResetSpecialFlag(order, ListChangedType.ItemAdded);
            FireListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, pos));
         }
      }

      internal void RemoveOrder(decimal serialNo)
      {
         int index = Orders.IndexOf(serialNo);
         if (index != -1)
            RemoveOrder(Orders[index]);
         else
            throw new ArgumentOutOfRangeException();
      }

      internal void RemoveOrder(Order order)
      {
         if (order == null)
            throw new ArgumentNullException(ConstMessages.ExceptionNullOrderObject);
         int index = Orders.IndexOf(order.SerialNo);
         if (index == -1)
            throw new ArgumentOutOfRangeException(ConstMessages.ExceptionOrderIndexNotFind);

         //首先检查医嘱是否可以移除
         if (order.CanRemove)
         {
            order.OrderChanged -= new EventHandler<OrderChangedEventArgs>(AfterOrderChanged);
            ResetSpecialFlag(order, ListChangedType.ItemDeleted);
            Orders.OrderList.Remove(order);
            if ((order.State == OrderState.New) && (order.EditState != OrderEditState.Added))
            {
               order.Delete(); // 强制编辑状态为删除
               m_RemovedCollection.Add(order);
            }
            //// 此处处理方式不是很好，应该用统一的方法处理（参考DataRowCollection类）
            //// 目前只会删除新增状态的医嘱医嘱，因为新医嘱保存时都是删掉再增加
            //// ，所以可以直接从列表中移除对象
            ////if (order.EditState == OrderEditState.Added) // 新医嘱，还未保存到数据库中
            ////{// .Remove(order);
            ////}
            ////else // 其它情况下用医嘱对象自己的删除方法将其标记为删除
            ////{
            ////   order.Delete();
            ////}
            FireListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
         }
      }

      internal void RemoveOrderAt(int index)
      {
         RemoveOrder(Orders[index]);
      }

      /// <summary>
      /// 将医嘱从老位置移动到新位置
      /// </summary>
      /// <param name="oldIndex"></param>
      /// <param name="newIndex"></param>
      internal void MoveOrder(int oldIndex, int newIndex)
      {
         if ((oldIndex < 0) || (oldIndex >= Orders.Count))
            throw new ArgumentOutOfRangeException(ConstMessages.ExceptionOrginalOrderNotFind);
         if ((newIndex < -1) || (newIndex > Orders.Count))
            throw new ArgumentOutOfRangeException(ConstMessages.ExceptionOutOfListRange);

         if (oldIndex == newIndex)
            return;
         // 移动的方法是先从原列表中移除记录，然后插入到新位置
         // 向下移动时，检查新位置是否是草药汇总信息，如果是的话，则要跳过，以保证草药明细和汇总信息在一起
         if (oldIndex < newIndex)
         {
            TextOrderContent textContent = Orders[newIndex].Content as TextOrderContent;
            if ((textContent != null) && (textContent.IsSummaryOfHerbDetail))
               newIndex++;
         }
         Order temp = Orders[oldIndex];
         m_IsEditing = true;
         Orders.OrderList.Remove(temp);
         if (newIndex >= Orders.Count) // 在列表末尾插入
            AddOrder(temp);
         else
            Orders.OrderList.Insert(newIndex, temp);
         temp.HadSync = false;
         m_IsEditing = false;
         FireListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
      }

      /// <summary>
      /// 医嘱对象内容改变后触发事件
      /// </summary>
      /// <param name="Sender"></param>
      /// <param name="e"></param>
      internal void AfterOrderChanged(object Sender, OrderChangedEventArgs e)
      {
         int index = Orders.IndexOf(e.NewSerialNo);
         if (index == -1)
            throw new ArgumentOutOfRangeException();
         
         ResetSpecialFlag(Orders[index], ListChangedType.ItemChanged);
         FireListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
      }
      #endregion

      #region public method
      /// <summary>
      /// 生成新医嘱行
      /// </summary>
      /// <returns></returns>
      public Order NewOrder()
      {
         Order newOrder;
         if (IsTempOrder)
            newOrder = new TempOrder();
         else
            newOrder = new LongOrder();

         //只设置基本属性，其它属性的初始设置由外部设置
         newOrder.SerialNo = Orders.MaxSerialNo + 1; // 最大序号
         newOrder.GroupPosFlag = GroupPositionKind.SingleOrder;
         newOrder.GroupSerialNo = newOrder.SerialNo;

         return newOrder;
      }

      /// <summary>
      /// 为GridView中指定FieldName的列从医嘱中获取数据
      /// </summary>
      /// <param name="rowIndex">提供数据的医嘱在医嘱列表中的行号</param>
      /// <param name="fieldName">需要数据的列的FieldName</param>
      /// <returns></returns>
      public object GetCustomColumnData(int rowIndex, string fieldName)
      {
         if ((rowIndex > -1) && (rowIndex < Orders.Count))
            return DefaultView[rowIndex][fieldName];
         else
            return null;
      }

      /// <summary>
      /// 获取指定医嘱序号的医嘱
      /// </summary>
      /// <param name="serialNo"></param>
      /// <returns></returns>
      public Order GetOrderBySerialNo(int serialNo)
      {
         int index = DefaultView.IndexOf(serialNo);
         if (index > -1)
            return DefaultView[index].OrderCache;
         else
            return null;
      }

      /// <summary>
      /// 获取指定序号的医嘱对应的医嘱视图
      /// </summary>
      /// <param name="serialNo"></param>
      /// <returns></returns>
      public OrderView GetOrderViewBySerialNo(int serialNo)
      {
         int index = DefaultView.IndexOf(serialNo);
         if (index > -1)
            return DefaultView[index];
         else
            return null;
      }

      /// <summary>
      /// 接受自上次保存以来的修改
      /// </summary>
      public void AcceptChanges()
      {
         m_IsEditing = true;
         for (int index = Orders.Count - 1; index >= 0; index--)
         {
            if ((Orders[index].EditState == OrderEditState.Deleted)
               || (Orders[index].EditState == OrderEditState.Detached))
               Orders.RemoveAt(index);
            else
               Orders[index].AcceptChanges();
         }
         OrderDataTable.AcceptChanges();

         m_IsEditing = false;
         m_RemovedCollection.Clear();
         _hadChanged = false;
      }

      /// <summary>
      /// 在医嘱的DataTable中找到指定医嘱对象对应的行，并返回
      /// </summary>
      /// <param name="order"></param>
      /// <returns>医嘱所在行，不存在则返回null</returns>
      public DataRow GetOrderRow(Order order)
      {
         if (order == null)
            throw new ArgumentNullException();

         if ((order.EditState == OrderEditState.Added)
            || (order.EditState == OrderEditState.Detached))
            return null;

         // 通过医嘱序号来定位
         DataRow[] rows = OrderDataTable.Select(String.Format(CultureInfo.CurrentCulture
            , "{0} = {1}"
            , CoreBusinessLogic.GetSerialNoField(IsTempOrder)
            , order.SerialNo));
         if (rows.Length == 1)
            return rows[0];
         else if (rows.Length > 1)
            throw new ArgumentException(ConstMessages.ExceptionHaveManyMatchRows);
         else
            throw new ArgumentOutOfRangeException();
      }

      /// <summary>
      /// 获得所有新医嘱和做过修改的医嘱
      /// </summary>
      /// <returns>做过修改的医嘱对象集合</returns>
      public Order[] GetNewAndChangedOrders()
      {
         List<Order> changedOrders = new List<Order>();
         for (int index = 0; index < Orders.OrderList.Count; index++)
         {
            // 新医嘱或改变过的医嘱都算
            if ((Orders[index].State == OrderState.New)
               || ((Orders[index].EditState != OrderEditState.Unchanged)
               && (Orders[index].EditState != OrderEditState.Detached)))
               changedOrders.Add(Orders[index]);
         }
         foreach (Order order in m_RemovedCollection)
         {
            changedOrders.Add(order);
         }
         Order[] array = new Order[changedOrders.Count];
         changedOrders.CopyTo(array);
         return array;
      }

      /// <summary>
      /// 获取所有做过修改的医嘱
      /// </summary>
      /// <returns></returns>
      public Order[] GetChangedOrders()
      {
         List<Order> changedOrders = new List<Order>();
         for (int index = 0; index < Orders.OrderList.Count; index++)
         {
            if ((Orders[index].EditState != OrderEditState.Unchanged)
               && (Orders[index].EditState != OrderEditState.Detached))
               changedOrders.Add(Orders[index]);
         }
         foreach (Order order in m_RemovedCollection)
         {
            changedOrders.Add(order);
         }
         Order[] array = new Order[changedOrders.Count];
         changedOrders.CopyTo(array);
         return array;
      }

      /// <summary>
      /// 获取所有新增或修改过的关联到申请单的医嘱
      /// </summary>
      /// <returns></returns>
      public Order[] GetNewAndChangedRequestOrder()
      {
         if (!IsTempOrder)
            return null;

         TempOrder temp;
         List<Order> changedOrders = new List<Order>();
         for (int index = 0; index < Orders.OrderList.Count; index++)
         {
            temp = Orders[index] as TempOrder;
            if ((temp.EditState != OrderEditState.Unchanged)
               && (temp.EditState != OrderEditState.Detached)
               && (temp.ApplySerialNo != 0))
               changedOrders.Add(Orders[index]);
         }
         foreach (Order order in m_RemovedCollection)
         {
            temp = order as TempOrder;
            if (temp.ApplySerialNo != 0)
               changedOrders.Add(order);
         }
         Order[] array = new Order[changedOrders.Count];
         changedOrders.CopyTo(array);
         return array;
      }

      /// <summary>
      /// 同步医嘱对象集合和DataTable的数据
      /// </summary>
      /// <param name="changedOrders"></param>
      /// <param name="autoDeleteNewOrder">是否主动删除所有新增状态的医嘱</param>
      /// <returns>返回DataTable的更新内容</returns>
      public DataTable SyncObjectData2Table(Order[] changedOrders, bool autoDeleteNewOrder)
      {
         // 新增和修改过的医嘱，统一将更新标志设为0

         // 首先同步修改过的医嘱，取出新医嘱稍后处理
         StringBuilder deletedSerialNos = new StringBuilder("0, ");
         Collection<Order> newOrders = new Collection<Order>();
         if ((changedOrders != null) && (changedOrders.Length > 0))
         {
            DataRow row;
            foreach (Order order in changedOrders)
            {
               if (order.EditState == OrderEditState.Deleted)
                  deletedSerialNos.Append(order.SerialNo.ToString() + ",");
               else if (order.State == OrderState.New)
                  newOrders.Add(order);
               else
               {
                  row = GetOrderRow(order);
                  PersistentObjectFactory.SetDataRowValueFromObject(row, order);
                  row[ConstSchemaNames.OrderColSynchFlag] = 0;
               }
            }
         }
         deletedSerialNos.Append("0");

         // 如果自动删除新医嘱，则将DataTable中的新医嘱全部删除, 否则只删除标记为删除的医嘱
         string filter;
         if (autoDeleteNewOrder)
            filter = String.Format(CultureInfo.CurrentCulture, "{1} = {0:D}", OrderState.New, ConstSchemaNames.OrderColState);
         else
            filter = String.Format(CultureInfo.CurrentCulture, "{0} in ({1})"
               , CoreBusinessLogic.GetSerialNoField(IsTempOrder), deletedSerialNos.ToString());
         DataRow[] deletedRow = OrderDataTable.Select(filter);
         if (deletedRow != null)
         {
            foreach (DataRow row in deletedRow)
               row.Delete();
         }

         foreach (Order order in newOrders) // 插入新医嘱
         {
            DataRow row;
            decimal groupSerialNo;

            groupSerialNo = order.GroupSerialNo;// 新医嘱的分组序号通过触发器来更新,在这里先将其分组序号与分组标志设为一致
            order.GroupSerialNo = (int)order.GroupPosFlag;
            row = OrderDataTable.NewRow();
            PersistentObjectFactory.SetDataRowValueFromObject(row, order);
            row[ConstSchemaNames.OrderColSynchFlag] = 0;
            OrderDataTable.Rows.Add(row);
            order.GroupSerialNo = groupSerialNo; // 恢复原先的分组序号   
         }

         DataTable changedTable = OrderDataTable.GetChanges();
         if ((changedTable != null) && (changedTable.Rows.Count > 0))
         {
            return changedTable.Copy();
         }
         else
         {
            return null;
         }
      }

      /// <summary>
      /// 获取与指定医嘱同组的所有其它医嘱
      /// </summary>
      /// <param name="order">指定的医嘱</param>
      /// <returns>同组的所有其它医嘱列表</returns>
      public Collection<Order> GetOtherOrdersOfSameGroup(Order order)
      {
         if ((order == null) || (!Orders.Contains(order.SerialNo)))
            throw new ArgumentOutOfRangeException(ConstMessages.ExceptionOrderNotFind);

         Collection<Order> orderList = new Collection<Order>();
         if (order.GroupPosFlag != GroupPositionKind.SingleOrder)
         {
            foreach (Order temp in Orders)
            {
               if ((temp.GroupSerialNo != order.GroupSerialNo)
                  || (temp.SerialNo == order.SerialNo))
                  continue;

               orderList.Add(temp);
            }
         }
         return orderList;
      }

      /// <summary>
      /// 接受数据发送
      /// </summary>
      public void AcceptDataSended()
      {
         foreach (Order order in Orders)
            order.HadSync = true;
         _hasNotSendData = false;
      }
      #endregion

      #region private methods

      private void Validate(int index, object value)
      {
         if (value == null)
         {
            throw new ArgumentNullException();
         }
         if ((index != -1) && (index > Orders.Count))
         {
            throw new IndexOutOfRangeException();
         }
      }

      /// <summary>
      /// 查找医嘱中是否含有有效的"出院医嘱"
      /// </summary>
      /// <returns>true表示有</returns>
      private bool SearchOutHospitalOrder()
      {
         // 只在临时医嘱列表中查找，新增和已审核、已执行的都认为是有效
         if (!IsTempOrder)
            return false;

         for (int index = Orders.Count - 1; index >= 0; index--)
            if ((Orders[index].Content.OrderKind == OrderContentKind.TextLeaveHospital)
               && (Orders[index].State != OrderState.Cancellation))
               return true;
         return false;
      }

      /// <summary>
      /// 查找医嘱中是否含有有效的“转科医嘱”
      /// </summary>
      /// <returns></returns>
      private bool SearchShiftDeptOrder()
      {
         // 新的、或已审核的（在处理逻辑中应该保证有效的“转科医嘱”只可能出现在病人转科前）
         for (int index = Orders.Count - 1; index >= 0; index--)
            if ((Orders[index].Content.OrderKind == OrderContentKind.TextShiftDept)
               && ((Orders[index].State == OrderState.New)
                  || (Orders[index].State == OrderState.Audited)))
               return true;
         return false;
      }

      /// <summary>
      /// 根据当前处理的医嘱对象及处理的动作决定如何设置出院医嘱和转科医嘱标志
      /// (在增、改操作后调用此方法，在删操作前调用此方法)
      /// </summary>
      /// <param name="order">当前处理的医嘱对象</param>
      /// <param name="changedType">增、删、改中的一种</param>
      private void ResetSpecialFlag(Order order, ListChangedType changedType)
      {
         if ((!IsTempOrder) || (order == null) || (order.Content == null))
            return;

         bool isDeleted = (changedType == ListChangedType.ItemDeleted);
         bool isCancled = (order.State == OrderState.Cancellation);
         bool canBeTrue;

         if (order.Content.OrderKind == OrderContentKind.TextLeaveHospital)
         {
            if (isDeleted || isCancled)
               canBeTrue = false;
            else
               canBeTrue = true;
            if (_hasOutHospitalOrder != canBeTrue)
            {
               _hasOutHospitalOrder = canBeTrue;
               FireOutHospitalOrderChanged(new EventArgs());
            }
         }
         else if (order.Content.OrderKind == OrderContentKind.TextShiftDept)
         {
            if (isDeleted || isCancled
               || ((order.State != OrderState.New) && (order.State != OrderState.Audited)))
               _hasShiftDeptOrder = false;
            else
               _hasShiftDeptOrder = true;
         }
      }
      #endregion

      #region IListSource Members

      public bool ContainsListCollection
      {
         get { return false; }
      }

      public IList GetList()
      {
         //return OrderDataTable.DefaultView;
         return DefaultView;
      }

      #endregion
   }
}
