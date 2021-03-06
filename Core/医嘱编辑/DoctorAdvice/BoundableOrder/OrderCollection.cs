using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using DrectSoft.Common.Eop;
using System.Collections.ObjectModel;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// 表示医嘱对象的集合
   /// </summary> 
   public class OrderCollection : InternalDataCollectionBase, ICollection, IEnumerable<Order>
   {
      #region internal & private fields
      /// <summary>
      /// 医嘱列表(所有状态的医嘱对象)
      /// </summary>
      internal List<Order> OrderList
      {
         get { return _orderList; }
      }
      private List<Order> _orderList;

      /// <summary>
      /// 原始医嘱集合中的医嘱对应的医嘱视图对象列表
      /// </summary>
      internal List<OrderView> OrderViewList
      {
         get { return _orderViewList; }
      }
      private List<OrderView> _orderViewList;

      /// <summary>
      /// 当前最大的医嘱序号
      /// </summary>
      internal decimal MaxSerialNo
      {
         get { return _maxSerialNo; }
         set { _maxSerialNo = value; }
      }
      private decimal _maxSerialNo;

      /// <summary>
      /// 关联的医嘱对象表
      /// </summary>
      private readonly OrderTable m_Table;
      #endregion

      #region properties
      public override int Count
      {
         get { return OrderList.Count; }
      }

      public Order this[int index]
      {
         get { return GetOrder(index); }
      }
      #endregion

      #region ctors
      /// <summary>
      /// 根据传入的医嘱对象表创建医嘱对象集合
      /// </summary>
      /// <param name="table"></param>
      internal OrderCollection(OrderTable table)
      {
         _maxSerialNo = 0;
         m_Table = table;

         _orderList = new List<Order>();
         Order newOrder;
         List<Order> newOrders = new List<Order>();
         // 先处理其它状态的医嘱，将新医嘱插在医嘱表的最后
         foreach (DataRow row in m_Table.OrderDataTable.Rows)
         {
            if (m_Table.IsTempOrder)
               newOrder = new TempOrder(row);
            else
               newOrder = new LongOrder(row);
            if (newOrder.State != OrderState.New)
               _orderList.Add(newOrder);
            else
               newOrders.Add(newOrder);
         }
         foreach (Order tempOrde in newOrders)
            _orderList.Add(tempOrde);

         //// 先处理分组信息，然后再绑定事件
         //ResetOrderGroupInfo();

         foreach (Order temp in _orderList)
         {
            // 绑定创建输出内容的委托
            temp.Content.ProcessCreateOutputeInfo =
               new OrderContent.GenerateOutputInfo(CustomDrawOperation.CreateOutputeInfo);
            if (temp.Content.Item != null)
            {
               temp.BeginInit();
               temp.Content.Item.ReInitializeProperties();
               temp.Content.EndInit();
               temp.EndInit();
            }
            temp.OrderChanged += new EventHandler<OrderChangedEventArgs>(m_Table.AfterOrderChanged);
         }

         ResetViewList();
      }
      #endregion

      #region internal events
      /// <summary>
      /// 在原始的医嘱对象列表发生改变后调用此事件，以同步医嘱视图列表
      /// </summary>
      /// <param name="e"></param>
      /// <param name="actualIndex"></param>
      internal void AfterListChanged(ListChangedEventArgs e)
      {
         //// 重新生成满足当前设置的医嘱对象列表
         //ResetInnerList();
         // --e中的Index是基于InnerList的，所以在处理原始列表时要进行转换处理
         //int actualIndex;
         // 同步医嘱视图列表
         switch (e.ListChangedType)
         {
            case ListChangedType.ItemAdded:
               OrderList[e.NewIndex].Added(); // 需要手工设置新加入医嘱对象的状态
               _maxSerialNo++;
               OrderViewList.Insert(e.NewIndex, new OrderView(OrderList[e.NewIndex]));
               break;
            case ListChangedType.ItemChanged:
               // 更新原位置对应的医嘱视图内容
               OrderViewList[e.NewIndex].ResetProperties();
               break;
            case ListChangedType.ItemDeleted:
               for (int index = OrderViewList.Count - 1; index >= 0; index--)
               {
                  // 视图对应的医嘱在医嘱列表中不存在，则删除
                  if (OrderList.IndexOf(OrderViewList[index].OrderCache) < 0)
                     OrderViewList.RemoveAt(index);
               }
               break;
            case ListChangedType.ItemMoved:
            case ListChangedType.Reset:
               ResetViewList();
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }
      }

      ///// <summary>
      ///// 医嘱对象列表或视图设置的医嘱状态发生变化后，同步InnerList
      ///// （为了让OrderTableView对象调用，方法设为internal）
      ///// </summary>
      //internal void ResetInnerList()
      //{
      //   if (_innerList == null)
      //      _innerList = new List<Order>();
      //   else
      //      _innerList.Clear();

      //   foreach (Order order in _orderList)
      //   {
      //      if ((order.EditState != OrderEditState.Deleted)
      //         && (order.EditState != OrderEditState.Detached))
      //      {
      //         if ((m_Table.DefaultView.OrderStatus == -1)
      //            || (order.Status == m_Table.DefaultView.OrderStatus))
      //            _innerList.Add(order);
      //      }
      //   }
      //}
      #endregion

      #region public methods
      public void Add(Order order)
      {
         m_Table.AddOrder(order);
      }

      public void Clear()
      {
         m_Table.Clear();
      }

      /// <summary>
      /// 医嘱列表中是否包含指定医嘱序号的医嘱
      /// </summary>
      /// <param name="serialNo"></param>
      /// <returns></returns>
      public bool Contains(decimal serialNo)
      {
         return (-1 != IndexOf(serialNo));
      }

      public void CopyTo(Order[] array, int index)
      {
         OrderList.CopyTo(array, index);
      }

      //public override IEnumerator GetEnumerator()
      //{
      //   return OrderList.GetEnumerator();
      //}

      public new IEnumerator<Order> GetEnumerator()
      {
         foreach (Order order in OrderList)
         {
            yield return order;
         }
      }

      /// <summary>
      /// 指定医嘱序号的医嘱在医嘱List中的位置
      /// </summary>
      /// <param name="serialNo"></param>
      /// <returns></returns>
      public int IndexOf(decimal serialNo)
      {
         for (int i = 0; i < OrderList.Count; i++)
         {
            if (OrderList[i].SerialNo == serialNo)
               return i;
         }
         return -1;
      }

      public void InsertAt(Order order, int pos)
      {
         m_Table.InsertOrderAt(order, pos);
      }

      /// <summary>
      /// 将指定医嘱序号的医嘱从医嘱列表中移除
      /// </summary>
      /// <param name="serialNo"></param>
      public void Remove(int serialNo)
      {
         m_Table.RemoveOrder(serialNo);
      }

      /// <summary>
      /// 从医嘱列表中移除指定的医嘱对象
      /// </summary>
      /// <param name="order">要移除的医嘱对象</param>
      public void Remove(Order order)
      {
         m_Table.RemoveOrder(order);
      }

      /// <summary>
      /// 从医嘱列表中移除指定位置的医嘱
      /// </summary>
      /// <param name="index"></param>
      public void RemoveAt(int index)
      {
         m_Table.RemoveOrderAt(index);
      }

      ///// <summary>
      ///// 用传入的DataRow更新指定序号对应的医嘱
      ///// </summary>
      ///// <param name="serialNo"></param>
      ///// <param name="sourceRow"></param>
      //public void UpdateOrder(int serialNo, DataRow sourceRow)
      //{
      //   int index = IndexOf(serialNo);
      //   Order order = this[index];
      //   if (order == null)
      //      throw new IndexOutOfRangeException("指定的医嘱序号不不存在");

      //   order.Initialize(sourceRow);
      //}
      #endregion

      #region private methods
      /// <summary>
      /// 在创建完医嘱对象列表后，处理医嘱的分组信息（设置分组标志）
      /// </summary>
      private void ResetOrderGroupInfo()
      {
         Order temp;
         for (int index = 0; index < _orderList.Count; index++)
         {
            temp = _orderList[index];
            temp.BeginInit();
            // 医嘱序号和分组序号一致时先默认医嘱未分组（单条）
            if (temp.SerialNo == temp.GroupSerialNo)
               temp.GroupPosFlag = GroupPositionKind.SingleOrder;
            else
            {
               // 下面针对分组序号与自己的医嘱序号不一致进行处理

               // 此时，分组序号应该与上一条医嘱的序号保持一致
               if ((index == 0)
                  || (temp.GroupSerialNo != _orderList[index - 1].GroupSerialNo))
                  throw new ArgumentException(ConstMessages.ExceptionGroupSerialNoError);
               _orderList[index - 1].BeginInit();
               // 将上一条由单条改为组开始（或由组结束改为组中间），本条置为组结束
               switch (_orderList[index - 1].GroupPosFlag)
               {
                  case GroupPositionKind.SingleOrder:
                     _orderList[index - 1].GroupPosFlag = GroupPositionKind.GroupStart;
                     break;
                  case GroupPositionKind.GroupEnd:
                     _orderList[index - 1].GroupPosFlag = GroupPositionKind.GroupMiddle;
                     break;
                  default:
                     throw new ArgumentException(ConstMessages.ExceptionGroupStateError);
               }
               _orderList[index - 1].EndInit();
               temp.GroupPosFlag = GroupPositionKind.GroupEnd;
            }
            temp.EndInit();
         }
      }

      /// <summary>
      /// 医嘱列表重置后，更新对应的视图列表(同时会更新最大序号的值)
      /// </summary>
      private void ResetViewList()
      {
         if (_orderViewList == null)
            _orderViewList = new List<OrderView>();
         else
            _orderViewList.Clear();

         _maxSerialNo = 0;
         foreach (Order order in OrderList)
         {
            if (order.SerialNo > _maxSerialNo)
               _maxSerialNo = order.SerialNo;
            _orderViewList.Add(new OrderView(order));
         }
      }

      /// <summary>
      /// 获取指定索引位置的医嘱对象
      /// </summary>
      /// <param name="index"></param>
      /// <returns></returns>
      private Order GetOrder(int index)
      {
         RangeCheck(index);
         //return InnerList[index];
         return OrderList[index];
      }

      private void RangeCheck(int index)
      {
         if ((index < 0) || (Count <= index))
         {
            throw new IndexOutOfRangeException(index.ToString(CultureInfo.CurrentCulture));
         }
      }
      #endregion
   }
}
