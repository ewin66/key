using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using DrectSoft.Common.Eop;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// StringList�����ֵ��ࡣ��������StringList��̬�����ֵ䣬�Ա���ShowList�������ComboBox
   /// </summary>
   public sealed class ListWordbook : BaseWordbook
   {
      #region properties
      ///// <summary>
      ///// �ֵ�������
      ///// </summary>
      //public new string WordbookName
      //{
      //   get { return _wordbookName; }
      //   set 
      //   {
      //      Debug.Assert(value != null, "��������ΪNULL");
      //      Debug.Assert(value.Length > 0, "��������Ϊ��");
      //      _wordbookName = value; 
      //      // �ֵ��������������һ��
      //      _title = value;
      //   }
      //}
      //private new string _wordbookName;

      /// <summary>
      /// ��Ϊ����Դ��StringList
      /// </summary>
      public Collection<string> Items
      {
         get { return _items; }
      }
      private Collection<string> _items;
      #endregion 

      #region value properties
      /// <summary>
      /// ���к�
      /// </summary>
      public int SerialNo
      {
         get { return _serialNo; }
      }
      private int _serialNo;

      /// <summary>
      /// ����
      /// </summary>
      public string Name
      {
         get { return _name; }
      }
      private string _name;

      /// <summary>
      /// ƴ��
      /// </summary>
      public string PingYin
      {
         get { return _pingYin; }
      }
      private string _pingYin;

      /// <summary>
      /// ���
      /// </summary>
      public string FiveStrokes
      {
         get { return _fiveStrokes; }
      }
      private string _fiveStrokes;
      #endregion

      /// <summary>
      /// ��ָ�����ַ������ϴ���List���ֵ���ʵ��
      /// </summary>
      /// <param name="uniqueName">�ֲ�Ψһ������</param>
      /// <param name="valueList">������������ַ�������</param>
      public ListWordbook(string uniqueName, Collection<string> valueList)
         : base()
      {
         Debug.Assert(uniqueName != null, "��������ΪNULL");
         Debug.Assert(uniqueName.Length > 0, "��������Ϊ��");
         WordbookName = uniqueName;
         Caption = WordbookName;
         //_name = "List�ֵ�";
         if (valueList != null)
            _items = new Collection<string>(valueList);
         else
            _items = new Collection<string>(); 
         
         ExtraCondition = "";
         QuerySentence = "";
         // �������Ե����ò����޸ģ������ط�����ʱ���ܻ�Ĭ����Щ���Ե�ֵû�б��
         CodeField = "name";
         NameField = "name";
         QueryCodeField = CodeField;
         CodeFieldIsString = true;

         DefaultFilterFields = new Collection<string>();
         DefaultFilterFields.Add("xh");
         DefaultFilterFields.Add("py");
         DefaultFilterFields.Add("wb");
         DefaultFilterFields.Add("name");

         CurrentMatchFields = new Collection<string>();
         foreach (string field in DefaultFilterFields)
            CurrentMatchFields.Add(field);

         Parameters = new FilterParameterCollection();

         ShowStyles = new Collection<GridColumnStyleCollection>();
         ShowStyles.Add(new GridColumnStyleCollection());
         ShowStyles[0].AddRange(new GridColumnStyle[]{
             new GridColumnStyle("xh", "���", 40)
            ,new GridColumnStyle("name", "����", 80)
            ,new GridColumnStyle("py", "ƴ��", 70)
            ,new GridColumnStyle("wb", "���", 70)});
         SelectedStyleIndex = 0;

         //m_DrawConditions = new Collection<CustomDrawSetting>();
      }

      /// <summary>
      /// ��������ֶε�ֵ
      /// </summary>
      public void ClearValueFields()
      {
         _name = "";
         _pingYin = "";
         _fiveStrokes = "";
         _serialNo = -1;
      }

      /// <summary>
      /// ��ѡ�еļ�¼��ʼ�������ֶ�
      /// </summary>
      /// <param name="sourceRow"></param>
      public void InitValueFields(DataRow sourceRow)
      {
         ClearValueFields();

         if (sourceRow != null)
         { 
            DataColumnCollection cols = sourceRow.Table.Columns;
            if (cols.Contains("xh"))
               _serialNo = (int)sourceRow["xh"];
            if (cols.Contains("name"))
               _name = sourceRow["name"].ToString();
            if (cols.Contains("py"))
               _pingYin = sourceRow["py"].ToString();
            if (cols.Contains("wb"))
               _fiveStrokes = sourceRow["wb"].ToString();
         }
      }

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
