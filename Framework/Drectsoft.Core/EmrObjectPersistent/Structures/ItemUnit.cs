using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Globalization;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ��Ŀ��λ�ṹ��
   /// </summary>
   public struct ItemUnit
   {
      #region properties
      /// <summary>
      /// ��λ����
      /// </summary>
      public string Name
      {
         get 
         {
            if (_name == null)
               return "";
            return _name; 
         }
      }
      private string _name;

      /// <summary>
      /// ��λϵ��(����/��ϵ��=�������С��λ������)
      /// </summary>
      public decimal Quotiety
      {
         get { return _quotiety; }
      }
      private decimal _quotiety;

      /// <summary>
      /// ��λ���
      /// </summary>
      public DruggeryUnitKind Kind
      {
         get { return _kind; }
      }
      private DruggeryUnitKind _kind;

      /// <summary>
      /// ��ǵ�λ�ṹ���Ƿ���ȷ��ʼ��
      /// </summary>
      public bool IsEmpty
      {
         get 
         {
            if ((_name == null) || (_name.Length == 0)
               || (_quotiety <= 0))
               return true;
            else
               return false;
         }
      }
      #endregion

      #region ctor
      /// <summary>
      /// �ô���Ĵ�������Ƴ�ʼ��CodeItem
      /// </summary>
      /// <param name="name">��λ����</param>
      /// <param name="quotiety">��λϵ��(�������С��λ)</param>
      /// <param name="kind">��λ���</param>
      public ItemUnit(string name, decimal quotiety, DruggeryUnitKind kind)
      {
         if (String.IsNullOrEmpty(name))
            name = "";
            // throw new ArgumentNullException("name", MessageStringManager.GetString("CommonParameterIsNull", "��λ����"));
         _name = name.TrimEnd();

         if (kind == DruggeryUnitKind.Specification) // ���λ����ϵ���ĵ���
            _quotiety = 1 / quotiety;
         else
            _quotiety = quotiety;

         _kind = kind;
      }

      /// <summary>
      /// �ô���Ĵ�������ƶ����ʼ��CodeItem
      /// </summary>
      /// <param name="name">��λ����</param>
      /// <param name="quotiety">��λϵ��(�������С��λ)</param>
      /// <param name="kind">��λ����</param>
      [SpecialMethod(MethodSpecialKind.DefaultCtor)]
      public ItemUnit(object name, object quotiety, object kind)
      {
         if ((name == null) || (String.IsNullOrEmpty(_name = name.ToString().Trim())))
            name = "";

         if (kind == null)
            kind = DruggeryUnitKind.Min;

         _name = name.ToString().TrimEnd();

         _kind = (DruggeryUnitKind)Enum.Parse(typeof(DruggeryUnitKind), kind.ToString());
         _quotiety = Convert.ToDecimal(quotiety, CultureInfo.CurrentCulture);
         if (_kind == DruggeryUnitKind.Specification) // ���λ����ϵ���ĵ���
         {
            //hrr:���ĳЩ�����_quotietyΪ0ʱ�쳣������
            if (_quotiety != 0)
               _quotiety = 1 / _quotiety;
            else
               _quotiety = decimal.MaxValue;
         }
      }

      /// <summary>
      /// �ô����ItemUnit�����µ�ItemUnit
      /// </summary>
      /// <param name="source"></param>
      public ItemUnit(ItemUnit source) : this(source.Name, source.Quotiety, source.Kind)
      { }
      #endregion

      #region public method
      /// <summary>
      /// ���ԡ���ǰ��λ��Ϊ��λ������ת�����ԡ���С��λ��Ϊ��λ������
      /// </summary>
      /// <param name="quantity">����ڵ�ǰ��λ������</param>
      /// <returns>�������С��λ������</returns>
      public decimal Convert2BaseUnit(decimal quantity)
      { 
         return (quantity * _quotiety);
      }

      /// <summary>
      /// ���ԡ���С��λ��Ϊ��λ������ת�����ԡ���ǰ��λ��Ϊ��λ������
      /// </summary>
      /// <param name="quantity">�������С��λ������</param>
      /// <returns>����ڵ�ǰ��λ������</returns>
      public decimal Convert2CurrentUnit(decimal quantity)
      {
         return (quantity / _quotiety);
      }

      /// <summary>
      /// ȷ�����������Ƿ������ͬ��ֵ
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         if (obj is ItemUnit)
         {
            ItemUnit aimObj = (ItemUnit)obj;
            return (aimObj.Name == Name);
         }
         return false;
      }

      /// <summary>
      /// ���ظ�ʵ���Ĺ�ϣ����
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return Name.GetHashCode();
      }

      /// <summary>
      /// ��ȡ����� Expression��������ڵĻ���
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         return Name;
         //return String.Format(CultureInfo.CurrentCulture
         //   , "{0}({1:D}, {2:#.##})", Name, Kind, Quotiety) ;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="unit1"></param>
      /// <param name="unit2"></param>
      /// <returns></returns>
      public static bool operator ==(ItemUnit unit1, ItemUnit unit2)
      {
         return unit1.Equals(unit2);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="unit1"></param>
      /// <param name="unit2"></param>
      /// <returns></returns>
      public static bool operator !=(ItemUnit unit1, ItemUnit unit2)
      {
         return !(unit1 == unit2);
      }

      #endregion
   }
}
