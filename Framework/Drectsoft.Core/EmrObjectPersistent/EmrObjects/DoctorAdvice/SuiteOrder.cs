using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ����ҽ������¼��
   /// </summary>
   public class SuiteOrder : EPBaseObject
   {
      #region properties
      /// <summary>
      /// ����ҽ�����
      /// </summary>
      [ReadOnly(true), DisplayName("(���)")]
      public decimal SerialNo
      {
         get { return _serialNo; }
         set { _serialNo = value; }
      }
      private decimal _serialNo;

      /// <summary>
      /// ����ҽ������
      /// </summary>
      [DisplayName("����ҽ������")]
      public new string Name
      {
         get { return _name; }
         set
         {
            if (String.IsNullOrEmpty(_name) || (!_name.Equals(value)))
            {
               _name = value;
               FireValueChanged();
            }
         }
      }
      private string _name;

      /// <summary>
      /// ƴ������
      /// </summary>
      [ReadOnly(true), DisplayName("ƴ������")]
      public string Py
      {
         get { return _py; }
         set
         {
            if (String.IsNullOrEmpty(_py) || (!_py.Equals(value)))
            {
               _py = value;
               FireValueChanged();
            }
         }
      }
      private string _py;

      /// <summary>
      /// ��ʴ���
      /// </summary>
      [ReadOnly(true), DisplayName("��ʴ���")]
      public string Wb
      {
         get { return _wb; }
         set
         {
            if (String.IsNullOrEmpty(_wb) || (!_wb.Equals(value)))
            {
               _wb = value;
               FireValueChanged();
            }
         }
      }
      private string _wb;

      /// <summary>
      /// �������Ҵ���
      /// </summary>
      [Browsable(false)]
      public string DeptCode
      {
         get { return _deptCode; }
         set
         {
            if (String.IsNullOrEmpty(_deptCode) || (_deptCode.Equals(value)))
            {
               _deptCode = value;
               FireValueChanged();
            }
         }
      }
      private string _deptCode;

      /// <summary>
      /// ������������
      /// </summary>
      [Browsable(false)]
      public string WardCode
      {
         get { return _wardCode; }
         set
         {
            if (String.IsNullOrEmpty(_wardCode) || (!_wardCode.Equals(value)))
            {
               _wardCode = value;
               FireValueChanged();
            }
         }
      }
      private string _wardCode;

      /// <summary>
      /// ����ҽ������
      /// </summary>
      [Browsable(false)]
      public string DoctorCode
      {
         get { return _doctorCode; }
         set
         {
            if (String.IsNullOrEmpty(_doctorCode) || (!_doctorCode.Equals(value)))
            {
               _doctorCode = value;
               FireValueChanged();
            }
         }
      }
      private string _doctorCode;

      /// <summary>
      /// Ӧ�÷�Χ(ȫԺ/����/����/����)
      /// </summary>
      [Browsable(false)]
      public DataApplyRange ApplyRange
      {
         get { return _applyRange; }
         set
         {
            if (_applyRange != value)
            {
               _applyRange = value;
               FireValueChanged();
            }
         }
      }
      private DataApplyRange _applyRange;

      /// <summary>
      /// ��ע
      /// </summary>
      [DisplayName("��ע")]
      public new string Memo
      {
         get { return _memo; }
         set
         {
            if (String.IsNullOrEmpty(_memo) || (!_memo.Equals(value)))
            {
               _memo = value;
               FireValueChanged();
            }
         }
      }
      private string _memo;

      /// <summary>
      /// 
      /// </summary>
      public override string InitializeSentence
      {
          get { return "select * from AdviceGroup"; }
      }

      /// <summary>
      /// 
      /// </summary>
      public override string FilterCondition
      {
          get { return "ID = " + SerialNo; }
      }

      /// <summary>
      /// ������������Ƿ��ѳ�ʼ��
      /// </summary>
      public override bool KeyInitialized
      {
         get { return (SerialNo > 0); }
      }
      #endregion

      private bool m_IsEditing;

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      public SuiteOrder()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public SuiteOrder(DataRow sourceRow)
         : base(sourceRow)
      { }
      #endregion

      #region custom event handler
      /// <summary>
      /// ����ֵ�ı��¼�
      /// </summary>
      public event EventHandler ValueChanged
      {
         add { onValueChanged = (EventHandler)Delegate.Combine(onValueChanged, value); }
         remove { onValueChanged = (EventHandler)Delegate.Remove(onValueChanged, value); }
      }
      private EventHandler onValueChanged;

      private void FireValueChanged()
      {
         if ((!m_IsEditing) && (onValueChanged != null))
            onValueChanged(this, new EventArgs());
      }
      #endregion

      /// <summary>
      /// ��ʼ�����е����ԣ������������͵������Լ�������
      /// </summary>
      public override void ReInitializeAllProperties()
      {
         
      }

      #region ISupportInitialize Members
      /// <summary>
      /// 
      /// </summary>
      public override void BeginInit()
      {
         m_IsEditing = true;
      }

      /// <summary>
      /// 
      /// </summary>
      public override void EndInit()
      {
         m_IsEditing = false;
         FireValueChanged();
      }
      #endregion
   }
}
