/*****************************************************************************\
**            DrectSoft Software Co. Ltd.                          **
**                                                                           **
**             ���������ֵ��ඨ��                                           **
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
   /// ������λ
   /// </summary>
   public sealed class MeasureUnit : BaseWordbook
   {
      /// <summary>
      /// ���� ������λ �ֵ����
      /// </summary>
      public MeasureUnit()
         : base("Other.MeasureUnit")
      { }

      /// <summary>
      /// ���� ������λ �ֵ����
      /// </summary>
      public MeasureUnit(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Other.MeasureUnit", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
   /// ������λ���
   /// </summary>
   public sealed class MeasureUnitCatalog : BaseWordbook
   {
      /// <summary>
      /// ���� ������λ��� �ֵ����
      /// </summary>
      public MeasureUnitCatalog()
         : base("Other.MeasureUnitCatalog")
      { }

      /// <summary>
      /// ���� ������λ��� �ֵ����
      /// </summary>
      public MeasureUnitCatalog(string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : base("Other.MeasureUnitCatalog", filters, gridStyleIndex, filterComb, extraCondition, cacheTime)
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
}
