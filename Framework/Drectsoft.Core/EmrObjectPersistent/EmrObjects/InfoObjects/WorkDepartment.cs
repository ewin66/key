using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using DrectSoft.Common.Eop;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ������λ
   /// </summary>
   public class WorkDepartment : EPBaseObject
   {
      #region properties
      /// <summary>
      /// ��˾����(������λ)
      /// </summary>
      public string CompanyName
      {
         get { return _companyName; }
         set { _companyName = value; }
      }
      private string _companyName;

      /// <summary>
      /// ��λ��ַ
      /// </summary>    
      public Address CompanyAddress
      {
         get { return _companyAddress; }
         set { _companyAddress = value; }
      }
      private Address _companyAddress;

      /// <summary>
      /// ְҵ���� Ĭ�ϳ�ʼ���ֵ�
      /// </summary>
      public CommonBaseCode Occupation
      {
         get { return _occupation; }
         set { _occupation = value; }
      }
      private CommonBaseCode _occupation;

      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      public override string InitializeSentence
      {
         get { throw new NotImplementedException(); }
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
      /// 
      /// </summary>
      public WorkDepartment()
         : base()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      public WorkDepartment(DataRow sourceRow)
         : base(sourceRow)
      { }
      #endregion

      /// <summary>
      /// ��ʼ�����е����ԣ������������͵������Լ�������
      /// </summary>
      public override void ReInitializeAllProperties()
      {
         if (CompanyAddress != null)
            CompanyAddress.ReInitializeAllProperties();
         if (Occupation != null)
            Occupation.ReInitializeAllProperties();
      }
   }
}
