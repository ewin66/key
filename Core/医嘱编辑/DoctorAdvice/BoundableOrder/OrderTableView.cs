using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DrectSoft.Common.Eop;
using System.Globalization;
using System.Collections.ObjectModel;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// ҽ����������ͼ
   /// (OrderTableView��OrderCollection��OrderTable��ƻ����Ǻܺã���Щ�ط�����������������)
   /// </summary>
   public sealed class OrderTableView : IBindingList, IList, ICollection, IEnumerable, ISupportInitialize
   {
      #region private fields
      /// <summary>
      /// ��Чҽ������ͼ�б�
      /// </summary>
      private List<OrderView> InnerList
      {
         get
         {
            if (_innerList == null)
            {
               ResetInnerList();
            }

            return _innerList;
         }
      }
      private List<OrderView> _innerList;
      #endregion

      #region custom public properties
      /// <summary>
      /// ������ҽ�������
      /// </summary>
      public OrderTable Table
      {
         get { return _table; }
         //set { ��δʵ�ָ�����ӦTable�Ĺ���}
      }
      private OrderTable _table;

      /// <summary>
      /// ָ����ͼ��ʾ����״̬��ҽ��(-1��ʾȫ��ʾ)
      /// </summary>
      public OrderState State
      {
         get { return _state; }
         set
         {
            if (_state != value)
            {
               // ״̬�ı�ʱ���ҽ������
               _state = value;

               // ���ݵ�ǰ���õ�״̬����AllowNew����
               AllowNew = ((value == OrderState.All) || (value == OrderState.New));
               AllowRemove = AllowNew;

               FireViewListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
         }
      }
      private OrderState _state;

      public OrderView this[int index]
      {
         get
         {
            return GetOrderView(index);
         }
      }
      #endregion

      #region private variables
      /// <summary>
      /// ����Ƿ����ڸ�������(��ֹƵ�����ø����¼�)
      /// </summary>
      private bool m_IsEditing;
      /// <summary>
      /// ����Ƿ�չ��ȫ����ҩ��ϸ��
      /// true: ȫչ
      /// false��ȫ�գ���Ҫ������һ�������ܾ����Ƿ�Ҫ�۵���ϸ��
      /// </summary>
      private bool m_ExpandAllHerbDetail;
      /// <summary>
      /// ��¼����չ���Ĳ�ҩ��ϸ�ķ������
      /// </summary>
      private Collection<decimal> m_GroupSerialNoOfExpandedHerbs;
      #endregion

      #region IBindingList properties
      /// <summary>
      /// ����༭
      /// </summary>
      public bool AllowEdit
      {
         get { return _allowEdit; }
         set { _allowEdit = value; }
      }
      private bool _allowEdit;

      /// <summary>
      /// �������ҽ��
      /// </summary>
      public bool AllowNew
      {
         get { return _allowNew; }
         set { _allowNew = value; }
      }
      private bool _allowNew;

      /// <summary>
      /// ����ɾ��ҽ��
      /// </summary>
      public bool AllowRemove
      {
         get { return _allowRemove; }
         set { _allowRemove = value; }
      }
      private bool _allowRemove;

      public bool IsSorted
      {
         get { return true; }
      }

      public ListSortDirection SortDirection
      {
         get { return ListSortDirection.Ascending; }
      }

      public PropertyDescriptor SortProperty
      {
         get { return null; }
      }

      public bool SupportsSorting
      {
         get { return false; }
      }

      public bool SupportsChangeNotification
      {
         get { return true; }
      }

      public bool SupportsSearching
      {
         get { return true; }
      }
      #endregion

      #region IList properties
      public bool IsFixedSize
      {
         get { return false; }
      }

      public bool IsReadOnly
      {
         get { return false; }
      }

      object IList.this[int index]
      {
         get { return GetOrderView(index); }
         set
         {
            throw new NotSupportedException();
         }
      }
      #endregion

      #region ICollection propteties
      public int Count
      {
         get { return InnerList.Count; }
      }

      public bool IsSynchronized
      {
         get { return false; }
      }

      public object SyncRoot
      {
         get { return this; }
      }
      #endregion

      #region ctors
      /// <summary>
      /// ���ݴ����ҽ�������ݴ�����ʱ����ҽ����OrderView����
      /// </summary>
      /// <param name="orderTable">ҽ�������</param>
      public OrderTableView(OrderTable orderTable)
      {
         if (orderTable == null)
            throw new ArgumentNullException(ConstMessages.ExceptionNullOrderTable);

         _table = orderTable;
         _state = OrderState.All;
         _allowNew = true;
         _allowEdit = true;
         _allowRemove = true;

         m_ExpandAllHerbDetail = !CoreBusinessLogic.BusinessLogic.AutoHideHerbDetail;
         m_GroupSerialNoOfExpandedHerbs = new Collection<decimal>();

         Table.ListChanged += new ListChangedEventHandler(DoAfterOrderListChanged);
      }
      #endregion

      #region custom event handler
      /// <summary>
      /// ҽ������ͼ��List�ı��¼�
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

      private void FireViewListChanged(ListChangedEventArgs e)
      {
         // ͬ��InnerList(���˲���������ǰ��ִ�У��Ա�View��list��ʵ�ʵ�list��ʱ����һ��)
         ResetInnerList();

         if (m_IsEditing)
            return;

         if (onListChanged != null)
            onListChanged(this, e);
      }
      #endregion

      #region public custom Methods
      /// <summary>
      /// �������
      /// </summary>
      /// <returns></returns>
      public OrderView AddNew()
      {
         OrderView view;
         Order newOrder;
         try
         {
            if (!AllowNew)
            {
               throw new ArgumentException(ConstMessages.ExceptionNotAllowAddNew);
            }

            newOrder = Table.NewOrder();
            Table.AddOrder(newOrder);

            FireViewListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, Count - 1));
            view = this[Count - 1];
         }
         finally
         {
         }
         return view;
      }

      /// <summary>
      /// ɾ��ָ��λ�õ�ҽ��������ͼ
      /// </summary>
      /// <param name="index"></param>
      public void Delete(int index)
      {
         int actualIndex = Table.Orders.IndexOf(InnerList[index].SerialNo);
         // ����ɾ�����������ҽ��
         //if ((m_AddNewOrder != null) && (index == Count))
         if (InnerList[index].OrderCache.EditState == OrderEditState.Added)
         {
            Table.Orders.RemoveAt(actualIndex);
         }
         else
         {
            if (!AllowRemove)
            {
               throw new ArgumentException(ConstMessages.ExceptionNotAllowDeleteOrder);
            }
            Table.Orders.RemoveAt(actualIndex);
         }
         FireViewListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
      }

      /// <summary>
      /// ����ָ����ŵ�ҽ�����б��е�λ��
      /// </summary>
      /// <param name="serialNo"></param>
      /// <returns></returns>
      public int IndexOf(decimal serialNo)
      {
         for (int index = 0; index < Count; index++)
            if (InnerList[index].SerialNo == serialNo)
               return index;

         return -1;
      }

      /// <summary>
      /// ��ȡ��ͼ��ָ����ŵĶ�����ԭʼ�����б��е�λ��
      /// </summary>
      /// <param name="index">��ͼ�ж����λ��</param>
      /// <returns>��ԭʼ�����е�λ��</returns>
      public int GetOriginalIndex(int index)
      {
         if (index < 0)
            return -1;
         if (Count <= index)
            return Table.Orders.Count;

         return Table.Orders.IndexOf(InnerList[index].SerialNo);
      }

      /// <summary>
      /// ��ָ��λ�õļ�¼�ƶ�����λ��
      /// </summary>
      /// <param name="oldIndex">��¼ԭ�ȵ�λ��</param>
      /// <param name="newIndex">��¼Ҫ�ƶ�����λ��</param>
      public void Move(int oldIndex, int newIndex)
      {
         if ((oldIndex < 0) || (oldIndex >= Count))
            throw new ArgumentOutOfRangeException();
         if ((newIndex < -1) || (newIndex > Count))
            throw new ArgumentOutOfRangeException();

         if (oldIndex == newIndex)
            return;
         // ��View�е�����λ��ת����ʵ�ʵ�����λ�ã�Ȼ��ʹ��Table�ķ���������λ
         Table.MoveOrder(GetOriginalIndex(oldIndex), GetOriginalIndex(newIndex));
      }

      /// <summary>
      /// չ��ָ��λ��ҽ�������Ĳ�ҩ��ϸ
      /// </summary>
      /// <param name="index"></param>
      public void ExpandHerbDetail(int index)
      {
         // Ӧ����������Χ��
         if ((index < 0) || (index >= Count))
            throw new IndexOutOfRangeException();

         TextOrderContent textContent = this[index].OrderCache.Content as TextOrderContent;
         // Ӧ���ǲ�ҩ�Ļ�����Ϣ
         // ��������Ų������ʾ���б���������list
         if ((textContent != null) && (textContent.IsSummaryOfHerbDetail))
         {
            if (!m_GroupSerialNoOfExpandedHerbs.Contains(textContent.GroupSerialNoOfLinkedHerbs))
               m_GroupSerialNoOfExpandedHerbs.Add(textContent.GroupSerialNoOfLinkedHerbs);
            FireViewListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
         }
      }

      /// <summary>
      /// չ�����в�ҩ��ϸ
      /// </summary>
      public void ExpandAllHerbDetail()
      {
         m_ExpandAllHerbDetail = true;
         FireViewListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
      }

      /// <summary>
      /// �۵�ָ��λ�õĲ�ҩ��ϸ
      /// </summary>
      /// <param name="index"></param>
      public void CollapseHerbDetail(int index)
      {
         // Ӧ����������Χ��
         if ((index < 0) || (index >= Count))
            throw new IndexOutOfRangeException();

         // Ӧ���ǲ�ҩ����ϸ��Ϣ
         // ��������Ŵӿ���ʾ���б����Ƴ�����������list
         if ((this[index].OrderCache.Content.Item.Kind == ItemKind.HerbalMedicine)
            && (this[index].GroupPosFlag != GroupPositionKind.SingleOrder))
         {
            if (m_GroupSerialNoOfExpandedHerbs.Contains(this[index].GroupSerialNo))
               m_GroupSerialNoOfExpandedHerbs.Remove(this[index].GroupSerialNo);
            FireViewListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
         }
      }

      /// <summary>
      /// �۵����в�ҩ��ϸ
      /// </summary>
      public void CollapseAllHerbDetail()
      {
         m_ExpandAllHerbDetail = false;
         m_GroupSerialNoOfExpandedHerbs.Clear();
         FireViewListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
      }
      #endregion

      #region IBindingList Members
      void IBindingList.AddIndex(PropertyDescriptor property)
      {
         throw new NotSupportedException();
      }

      object IBindingList.AddNew()
      {
         return AddNew();
      }

      void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
      {
         throw new NotSupportedException();
      }

      int IBindingList.Find(PropertyDescriptor property, object key)
      {
         if (property != null)
         {
            throw new NotSupportedException();
         }
         return -1;
      }

      void IBindingList.RemoveIndex(PropertyDescriptor property)
      {
         throw new NotSupportedException();
      }

      void IBindingList.RemoveSort()
      {
         throw new NotSupportedException();
      }

      #endregion

      #region IList Members

      int IList.Add(object value)
      {
         if (value == null)
         {
            AddNew();
            return (Count - 1);
         }
         throw new ArgumentException(ConstMessages.ExceptionInsertError);
      }

      void IList.Clear()
      {
         throw new NotImplementedException();
      }

      bool IList.Contains(object value)
      {
         //Order order = value as Order;
         //if (value != null)
         //{
         //   return Table.Orders.Contains(order.SerialNo);
         //}
         return false;
      }

      int IList.IndexOf(object value)
      {
         OrderView view = value as OrderView;
         if (value != null)
         {
            return InnerList.IndexOf(view);
         }
         return -1;
      }

      void IList.Insert(int index, object value)
      {
         throw new ArgumentException(ConstMessages.ExceptionInsertError);
      }

      void IList.Remove(object value)
      {
         ((IList)this).RemoveAt(((IList)this).IndexOf(value));
      }

      void IList.RemoveAt(int index)
      {
         Delete(index);
      }

      #endregion

      #region ICollection Members
      void ICollection.CopyTo(Array array, int index)
      {
         throw new NotSupportedException();
         //for (int num1 = 0; num1 < Count; num1++)
         //{
         //   // ��δʵ�����鸴�ƹ��ܣ�����
         //   array.SetValue(new Order(m_ViewOrders[num1].  this, num1), (int)(num1 + index));
         //}
      }
      #endregion

      #region IEnumerable Members
      public IEnumerator GetEnumerator()
      {
         return InnerList.GetEnumerator();
      }
      #endregion

      #region private methods
      /// <summary>
      /// ҽ���б�ı�ʱ��Ҫͬ�����Ӧ��ҽ����ͼ�б�
      /// </summary>
      /// <param name="Sender"></param>
      /// <param name="e"></param>
      private void DoAfterOrderListChanged(object Sender, ListChangedEventArgs e)
      {
         FireViewListChanged(e);
      }

      /// <summary>
      /// ���յ�ǰ�趨��ҽ��״̬����InnerList
      /// </summary>
      private void ResetInnerList()
      {
         if (_innerList == null)
            _innerList = new List<OrderView>();
         else
            _innerList.Clear();

         bool isHerbDetail; // ����Ƿ��ǲ�ҩ��ϸ
         //bool isHerbSummary; // ����Ƿ��ǲ�ҩ����
         TextOrderContent textContent;

         foreach (OrderView view in Table.Orders.OrderViewList)
         {
            if ((view.OrderCache.EditState == OrderEditState.Deleted)
               || (view.OrderCache.EditState == OrderEditState.Detached)
               || ((State != OrderState.All) && (view.OrderCache.State != State)))
               continue;

            isHerbDetail = (view.OrderCache.Content.Item != null)
               && (view.OrderCache.Content.Item.Kind == ItemKind.HerbalMedicine)
               && (view.OrderCache.GroupPosFlag != GroupPositionKind.SingleOrder);// ������¼�����ڻ�����Ϣ������ֱ����ʾ

            // ���ڲ�ҩ��¼Ҫ�����⴦��
            //   ȫչʱ����ʾ���ܼ�¼��ǿ��������
            //   ȫ��ʱ����ʾ��ϸ��¼(�����Ѿ�չ������ϸ)
            if (isHerbDetail)
            {
               if (m_ExpandAllHerbDetail)
               {
                  _innerList.Add(view);
                  if (!m_GroupSerialNoOfExpandedHerbs.Contains(view.GroupSerialNo))
                     m_GroupSerialNoOfExpandedHerbs.Add(view.GroupSerialNo);
               }
               else
               {
                  if (m_GroupSerialNoOfExpandedHerbs.Contains(view.GroupSerialNo))
                     _innerList.Add(view);
               }
            }
            else
            {
               textContent = view.OrderCache.Content as TextOrderContent;
               if ((textContent != null) && (textContent.IsSummaryOfHerbDetail))
               {
                  if ((!m_ExpandAllHerbDetail) && (!m_GroupSerialNoOfExpandedHerbs.Contains(textContent.GroupSerialNoOfLinkedHerbs)))
                     _innerList.Add(view);
               }
               else
                  _innerList.Add(view);
            }
         }
         m_ExpandAllHerbDetail = false; // ǿ�����false,�Ա�������ˢ��listʱ������ϸ���۵���չ��״̬
      }

      private OrderView GetOrderView(int index)
      {
         if ((index < 0) || (Count <= index))
            throw new IndexOutOfRangeException(index.ToString(CultureInfo.CurrentCulture));

         return InnerList[index];
      }
      #endregion

      #region ISupportInitialize Members

      public void BeginInit()
      {
         m_IsEditing = true;
      }

      public void EndInit()
      {
         m_IsEditing = false;
         FireViewListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
      }

      #endregion
   }
}
