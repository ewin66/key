using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using DrectSoft.Common.Eop;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ��ϵ��
   /// </summary>
   public class LinkMan : EPBaseObject
   {
      #region properties
      /// <summary>
      /// ��ϵ��ϵ
      /// </summary>
      public CommonBaseCode Relation
      {
         get { return _relation; }
         set { _relation = value; }
      }
      private CommonBaseCode _relation;

      /// <summary>
      /// ��ϵ��ַ
      /// </summary>
      public Address ContactAddress
      {
         get { return _contactAddress; }
         set { _contactAddress = value; }
      }
      private Address _contactAddress;

      /// <summary>
      /// ��ϵ��λ
      /// </summary>
      public WorkDepartment ContactDepartment
      {
         get { return _contactDepartment; }
         set { _contactDepartment = value; }
      }
      private WorkDepartment _contactDepartment;

      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
      /// </summary>
      public override string FilterCondition
      {
         get { throw new NotImplementedException(); }
      }
      #endregion

      #region ctors
      /// <summary>
      /// ��ʼ����ʵ��
      /// </summary>
      public LinkMan()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public LinkMan(DataRow sourceRow)
         : base(sourceRow)
      { }
      #endregion

      /// <summary>
      /// ��ʼ�����е����ԣ������������͵������Լ�������
      /// </summary>
      public override void ReInitializeAllProperties()
      {
         if (Relation != null)
            Relation.ReInitializeAllProperties();
         if (ContactAddress != null)
            ContactAddress.ReInitializeAllProperties();
         if (ContactDepartment != null)
            ContactDepartment.ReInitializeAllProperties();
      }
   }
}
