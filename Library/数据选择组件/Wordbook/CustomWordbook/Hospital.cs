/*****************************************************************************\
**             Yindansoft & DrectSoft Software Co. Ltd.                          **
**                                                                           **
**             ҽԺ�������õ��ֵ��ඨ��                                       **
**                                                                           **
**                                                                           **
\*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using DrectSoft.Common.Eop;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// Ժ�ڿ����ֵ�
   /// </summary>
   public sealed class DepartmentBook : BaseWordbook
   {
      /// <summary>
      /// ����Ժ�ڿ����ֵ����
      /// </summary>
      public DepartmentBook()
         : base("Hospital.DepartmentBook")
      { }

      /// <summary>
      /// ����Ժ�ڿ����ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public DepartmentBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.DepartmentBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ Department ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new Department(CurrentRow);
      }
   }

   /// <summary>
   /// �����ֵ�
   /// </summary>
   public sealed class WardBook : BaseWordbook
   {
      /// <summary>
      /// ���������ֵ����
      /// </summary>
      public WardBook()
         : base("Hospital.WardBook")
      { }

      /// <summary>
      /// ���������ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public WardBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.WardBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ Ward ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new Ward(CurrentRow);
      }
   }

   /// <summary>
   /// ������
   /// </summary>
   public sealed class OpDepartmentBook : BaseWordbook
   {
      /// <summary>
      /// �����������ֵ����
      /// </summary>
      public OpDepartmentBook()
         : base("Hospital.OpDepartmentBook")
      { }

      /// <summary>
      /// �����������ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public OpDepartmentBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.OpDepartmentBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ Department ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new Department(CurrentRow);
      }
   }

   /// <summary>
   /// Ժ�����������ֵ�
   /// </summary>
   public sealed class CustomOperationBook : BaseWordbook
   {
      /// <summary>
      /// ���� Ժ���������� �ֵ����
      /// </summary>
      public CustomOperationBook()
         : base("Hospital.CustomOperationBook")
      { }

      /// <summary>
      /// ���� Ժ���������� �ֵ����
      /// </summary>
      public CustomOperationBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.CustomOperationBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ Ժ���������� ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         throw new NotImplementedException();
      }
   }

   /// <summary>
   /// ְ����
   /// </summary>
   public sealed class EmployeeBook : BaseWordbook
   {
      /// <summary>
      /// ����ְ�����ֵ����
      /// </summary>
      public EmployeeBook()
         : base("Hospital.EmployeeBook")
      { }

      /// <summary>
      /// ����ְ�����ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public EmployeeBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.EmployeeBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ Employee ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new Employee(CurrentRow);
      }
   }

   /// <summary>
   /// �շ���Ŀ
   /// </summary>
   public sealed class ChargeItemBook : BaseWordbook
   {
      /// <summary>
      /// �����շ���Ŀ�ֵ����
      /// </summary>
      public ChargeItemBook()
         : base("Hospital.ChargeItemBook")
      { }

      /// <summary>
      /// �����շ���Ŀ�ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public ChargeItemBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.ChargeItemBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ OrderItem ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          //return OrderItemFactory.CreateOrderItem(CurrentRow, null);
          return new ChargeItem(CurrentRow); ;
      }
   }

   /// <summary>
   /// �ٴ��շ���Ŀ
   /// </summary>
   public sealed class ClinicItemBook : BaseWordbook
   {
      /// <summary>
      /// �����ٴ��շ���Ŀ�ֵ����
      /// </summary>
      public ClinicItemBook()
         : base("Hospital.ClinicItemBook")
      { }

      /// <summary>
      /// �����ٴ��շ���Ŀ�ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public ClinicItemBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.ClinicItemBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ OrderItem ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          //return OrderItemFactory.CreateOrderItem(CurrentRow, null);
          return new ClinicItem(CurrentRow);
      }
   }

   /// <summary>
   /// ҩƷ�ֵ�
   /// </summary>
   public sealed class DruggeryBook : BaseWordbook
   {
      /// <summary>
      /// ���� ҩƷ�ֵ� �ֵ����
      /// </summary>
      public DruggeryBook()
         : base("Hospital.DruggeryBook")
      { }

      /// <summary>
      /// ���� ҩƷ�ֵ� �ֵ����
      /// </summary>
      public DruggeryBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.DruggeryBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ Druggery ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new Druggery(CurrentRow);
      }
   }

   /// <summary>
   /// ҩƷ����
   /// </summary>
   public sealed class DruggeryCatalogBook : BaseWordbook
   {
      /// <summary>
      /// ���� ҩƷ���� �ֵ����
      /// </summary>
      public DruggeryCatalogBook()
         : base("Hospital.DruggeryCatalogBook")
      { }

      /// <summary>
      /// ���� ҩƷ���� �ֵ����
      /// </summary>
      public DruggeryCatalogBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.DruggeryCatalogBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ DruggeryCatalog ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new DruggeryCatalog(CurrentRow);
      }
   }

   /// <summary>
   /// ҩƷ����
   /// </summary>
   public sealed class DruggeryFormBook : BaseWordbook
   {
      /// <summary>
      /// ���� ҩƷ���� �ֵ����
      /// </summary>
      public DruggeryFormBook()
         : base("Hospital.DruggeryFormBook")
      { }

      /// <summary>
      /// ���� ҩƷ���� �ֵ����
      /// </summary>
      public DruggeryFormBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.DruggeryFormBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ DruggeryForm ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new DruggeryForm(CurrentRow);
      }
   }

   /// <summary>
   /// ҩƷ��Դ
   /// </summary>
   public sealed class DruggerySourceBook : BaseWordbook
   {
      /// <summary>
      /// ���� ҩƷ��Դ �ֵ����
      /// </summary>
      public DruggerySourceBook()
         : base("Hospital.DruggerySourceBook")
      { }

      /// <summary>
      /// ���� ҩƷ��Դ �ֵ����
      /// </summary>
      public DruggerySourceBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.DruggerySourceBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ DruggerySource ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new DruggerySource(CurrentRow);
      }
   }

   /// <summary>
   /// ҽ���÷�
   /// </summary>
   public sealed class OrderUsageBook : BaseWordbook
   {
      /// <summary>
      /// ���� ҽ���÷� �ֵ����
      /// </summary>
      public OrderUsageBook()
         : base("Hospital.OrderUsageBook")
      { }

      /// <summary>
      /// ���� ҽ���÷� �ֵ����
      /// </summary>
      public OrderUsageBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.OrderUsageBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ OrderUsage ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new OrderUsage(CurrentRow);
      }
   }

   /// <summary>
   /// ҽ��Ƶ��
   /// </summary>
   public sealed class OrderFrequencyBook : BaseWordbook
   {
      /// <summary>
      /// ���� ҽ��Ƶ�� �ֵ����
      /// </summary>
      public OrderFrequencyBook()
         : base("Hospital.OrderFrequencyBook")
      { }

      /// <summary>
      /// ���� ҽ��Ƶ�� �ֵ����
      /// </summary>
      public OrderFrequencyBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Hospital.OrderFrequencyBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ OrderFrequency ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new OrderFrequency(CurrentRow);
      }
   }
}
