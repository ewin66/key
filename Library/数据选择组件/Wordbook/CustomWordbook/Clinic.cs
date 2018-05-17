/*****************************************************************************\
**             Yindansoft & DrectSoft Software Co. Ltd.                          **
**                                                                           **
**             �ٴ������ֵ��ඨ��                                           **
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
   /// �ֲ�����ѧ
   /// </summary>
   public sealed class Topography : BaseWordbook
   {
      /// <summary>
      /// ���� �ֲ�����ѧ �ֵ����
      /// </summary>
      public Topography()
         : base("Clinic.Topography")
      { }

      /// <summary>
      /// ���� �ֲ�����ѧ �ֵ����
      /// </summary>
      public Topography(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Clinic.Topography", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
   /// ��̬ѧ
   /// </summary>
   public sealed class Morphology : BaseWordbook
   {
      /// <summary>
      /// ���� ��̬ѧ �ֵ����
      /// </summary>
      public Morphology()
         : base("Clinic.Morphology")
      { }

      /// <summary>
      /// ���� ��̬ѧ �ֵ����
      /// </summary>
      public Morphology(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Clinic.Morphology", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
   /// ���﹦��
   /// </summary>
   public sealed class BiologyFunction : BaseWordbook
   {
      /// <summary>
      /// ���� ���﹦�� �ֵ����
      /// </summary>
      public BiologyFunction()
         : base("Clinic.BiologyFunction")
      { }

      /// <summary>
      /// ���� ���﹦�� �ֵ����
      /// </summary>
      public BiologyFunction(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Clinic.BiologyFunction", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
   /// ���л���
   /// </summary>
   public sealed class LivingOrganisms : BaseWordbook
   {
      /// <summary>
      /// ���� ���л��� �ֵ����
      /// </summary>
      public LivingOrganisms()
         : base("Clinic.LivingOrganisms")
      { }

      /// <summary>
      /// ���� ���л��� �ֵ����
      /// </summary>
      public LivingOrganisms(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Clinic.LivingOrganisms", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
   /// �������ء����ͻ
   /// </summary>
   public sealed class PhysicalAgents : BaseWordbook
   {
      /// <summary>
      /// ���� �������ء����ͻ �ֵ����
      /// </summary>
      public PhysicalAgents()
         : base("Clinic.PhysicalAgents")
      { }

      /// <summary>
      /// ���� �������ء����ͻ �ֵ����
      /// </summary>
      public PhysicalAgents(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Clinic.PhysicalAgents", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
   /// ��ỷ��
   /// </summary>
   public sealed class SocialContext : BaseWordbook
   {
      /// <summary>
      /// ���� ��ỷ�� �ֵ����
      /// </summary>
      public SocialContext()
         : base("Clinic.SocialContext")
      { }

      /// <summary>
      /// ���� ��ỷ�� �ֵ����
      /// </summary>
      public SocialContext(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Clinic.SocialContext", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
   /// ����
   /// </summary>
   public sealed class Procedure : BaseWordbook
   {
      /// <summary>
      /// ���� ���� �ֵ����
      /// </summary>
      public Procedure()
         : base("Clinic.Procedure")
      { }

      /// <summary>
      /// ���� ���� �ֵ����
      /// </summary>
      public Procedure(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Clinic.Procedure", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
   /// ������(���δ�)
   /// </summary>
   public sealed class GeneralLinkage : BaseWordbook
   {
      /// <summary>
      /// ���� ������ �ֵ����
      /// </summary>
      public GeneralLinkage()
         : base("Clinic.GeneralLinkage")
      { }

      /// <summary>
      /// ���� ������ �ֵ����
      /// </summary>
      public GeneralLinkage(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Clinic.GeneralLinkage", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
}
