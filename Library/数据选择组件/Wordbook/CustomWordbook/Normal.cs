/*****************************************************************************\
**             DrectSoft Software Co. Ltd.                          **
**                                                                           **
**             һ�㳣�õ��ֵ��ඨ��                                           **
**                                                                           **
**                                                                           **
\*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.Common.Eop;
using DrectSoft.Core;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// ͨ���ֵ䣨�ֵ������ϸ��
   /// </summary>
   public sealed class CommonBook : BaseWordbook
   {
      /// <summary>
      /// ����ͨ���ֵ����
      /// </summary>
      public CommonBook()
         : base("Normal.CommonBook")
      { }

      /// <summary>
      /// ����ͨ���ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public CommonBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.CommonBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// �Ա�
   /// </summary>
   public sealed class SexBook : BaseWordbook
   {
      /// <summary>
      /// �����Ա��ֵ����
      /// </summary>
      public SexBook()
         : base("Normal.SexBook")
      { }

      /// <summary>
      /// �����Ա��ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public SexBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.SexBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// ְҵ
   /// </summary>
   public sealed class MetierBook : BaseWordbook
   {
      /// <summary>
      /// ����ְҵ�ֵ����
      /// </summary>
      public MetierBook()
         : base("Normal.MetierBook")
      { }

      /// <summary>
      /// ����ְҵ�ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public MetierBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.MetierBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// ְ��
   /// </summary>
   public sealed class TechnicalTitleBook : BaseWordbook
   {
      /// <summary>
      /// ����ְ���ֵ����
      /// </summary>
      public TechnicalTitleBook()
         : base("Normal.TechnicalTitleBook")
      { }

      /// <summary>
      /// ����ְ���ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public TechnicalTitleBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.TechnicalTitleBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// ��ϵ�˹�ϵ
   /// </summary>
   public sealed class RelationBook : BaseWordbook
   {
      /// <summary>
      /// ������ϵ�˹�ϵ�ֵ����
      /// </summary>
      public RelationBook()
         : base("Normal.RelationBook")
      { }

      /// <summary>
      /// ������ϵ�˹�ϵ�ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public RelationBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.RelationBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// ���ƽ��
   /// </summary>
   public sealed class TreatResultBook : BaseWordbook
   {
      /// <summary>
      /// �������ƽ���ֵ����
      /// </summary>
      public TreatResultBook()
         : base("Normal.TreatResultBook")
      { }

      /// <summary>
      /// �������ƽ���ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public TreatResultBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.TreatResultBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// �пڵȼ�
   /// </summary>
   public sealed class IncisionLevelBook : BaseWordbook
   {
      /// <summary>
      /// �����пڵǼ��ֵ����
      /// </summary>
      public IncisionLevelBook()
         : base("Normal.IncisionLevelBook")
      { }

      /// <summary>
      /// �����пڵȼ��ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public IncisionLevelBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.IncisionLevelBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// �������
   /// </summary>
   public sealed class CicatrizationKindBook : BaseWordbook
   {
      /// <summary>
      /// ������������ֵ����
      /// </summary>
      public CicatrizationKindBook()
         : base("Normal.CicatrizationKindBook")
      { }

      /// <summary>
      /// ������������ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public CicatrizationKindBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.CicatrizationKindBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// �п����ϵȼ�(���пں���������������ϵõ�)
   /// </summary>
   public sealed class IncisionCicatrizationKindBook : BaseWordbook
   {
      /// <summary>
      /// ������������ֵ����
      /// </summary>
      public IncisionCicatrizationKindBook()
         : base("Normal.IncisionCicatrizationKindBook")
      { }

      /// <summary>
      /// ������������ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public IncisionCicatrizationKindBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.IncisionCicatrizationKindBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         return null;
      }
   }

   /// <summary>
   /// ����״��
   /// </summary>
   public sealed class MarriageStateBook : BaseWordbook
   {
      /// <summary>
      /// ����MarriageStateBook�ֵ����
      /// </summary>
      public MarriageStateBook()
         : base("Normal.MarriageStateBook")
      { }

      /// <summary>
      /// ����MarriageStateBook�ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public MarriageStateBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.MarriageStateBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }

   /// <summary>
   /// ����״��
   /// </summary>
   public sealed class BearStateBook : BaseWordbook
   {
      /// <summary>
      /// ����BearStateBook�ֵ����
      /// </summary>
      public BearStateBook()
         : base("Normal.BearStateBook")
      { }

      /// <summary>
      /// ����BearStateBook�ֵ����
      /// </summary>
      /// <param name="filters"></param>
      /// <param name="gridStyleIndex"></param>
      /// <param name="filterComb"></param>
      /// <param name="extraCondition"></param>
      /// <param name="cacheTime"></param>
      public BearStateBook(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Normal.BearStateBook", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
      { }

      /// <summary>
      /// ���ֵ�ʵ������CommonBaseCode���ͳ־ö���
      /// </summary>
      /// <returns></returns>
      protected override EPBaseObject CreatePersistentWordbook()
      {
         if (CurrentRow == null)
            return null;
         return new BasicDictionnary(CurrentRow);
      }
   }
}
