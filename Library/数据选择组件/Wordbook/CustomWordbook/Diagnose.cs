/*****************************************************************************\
**             Yindansoft & DrectSoft Software Co. Ltd.                          **
**                                                                           **
**             ������ʵ��ֵ��ඨ��                                           **
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
   /// �������ICD-10
   /// </summary>
   public sealed class ICD10 : BaseWordbook
   {
      /// <summary>
      /// ������������ֵ����
      /// </summary>
      public ICD10() 
         : base("Diagnose.ICD10")
      { }

      /// <summary>
      /// ������������ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public ICD10(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Diagnose.ICD10", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ Diagnose ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         throw new NotImplementedException();
      }
   }

   /// <summary>
   /// ������
   /// </summary>
   public sealed class TumourBook : BaseWordbook
   {
      /// <summary>
      /// ���� ������ �ֵ����
      /// </summary>
      public TumourBook()
         : base("Diagnose.TumourBook")
      { }

      /// <summary>
      /// ���� ������ �ֵ����
      /// </summary>
      public TumourBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Diagnose.TumourBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         throw new NotImplementedException();
      }
   }

   /// <summary>
   /// �����ж���
   /// </summary>
   public sealed class DamnificationBook : BaseWordbook
   {
      /// <summary>
      /// ���� �����ж��� �ֵ����
      /// </summary>
      public DamnificationBook()
         : base("Diagnose.DamnificationBook")
      { }

      /// <summary>
      /// ���� �����ж��� �ֵ����
      /// </summary>
      public DamnificationBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Diagnose.DamnificationBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         throw new NotImplementedException();
      }
   }

   /// <summary>
   /// �������
   /// </summary>
   public sealed class OperationBook : BaseWordbook
   {
      /// <summary>
      /// ���� ������� �ֵ����
      /// </summary>
      public OperationBook()
         : base("Diagnose.OperationBook")
      { }

      /// <summary>
      /// ���� ������� �ֵ����
      /// </summary>
      public OperationBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Diagnose.OperationBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������  ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         throw new NotImplementedException();
      }
   }

   /// <summary>
   /// �����ֵ�
   /// </summary>
   public sealed class AnesthesiaBook : BaseWordbook
   {
      /// <summary>
      /// ���� �����ֵ� �ֵ����
      /// </summary>
      public AnesthesiaBook()
         : base("Diagnose.AnesthesiaBook")
      { }

      /// <summary>
      /// ���� �����ֵ� �ֵ����
      /// </summary>
      public AnesthesiaBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Diagnose.AnesthesiaBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new Anesthesia(CurrentRow);
          
      }
   }

   /// <summary>
   /// ��ҽ����ֵ�
   /// </summary>
   public sealed class ChineseDiagnosisBook : BaseWordbook
   {
      /// <summary>
      /// ���� �����ֵ� �ֵ����
      /// </summary>
      public ChineseDiagnosisBook()
         : base("Diagnose.ChineseDiagnosisBook")
      { }

      /// <summary>
      /// ���� �����ֵ� �ֵ����
      /// </summary>
      public ChineseDiagnosisBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Diagnose.ChineseDiagnosisBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������ ���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
          if (CurrentRow == null)
              return null;
          return new ChineseDiagnosis(CurrentRow);
      }
   }
}
