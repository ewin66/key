using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

using DrectSoft.Common.Eop;
using DrectSoft.Core;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// ͨ��SQL�����������ݼ���̬�����ֵ䣬���������ⳡ�ϵ�ʹ��
   /// </summary>
   [TypeConverter(typeof(SqlWordbookConverter))]
   public sealed class SqlWordbook : BaseWordbook
   {
      #region properties
      /// <summary>
      /// ����ֵ
      /// </summary>
      public string CodeValue
      {
         get { return _codeValue; }
      }
      private string _codeValue;

      /// <summary>
      /// ����ֵ
      /// </summary>
      public string NameValue
      {
         get { return _nameValue; }
      }
      private string _nameValue;

      /// <summary>
      /// �Ƿ���Ҫʹ�ò�ѯ����������ݼ�
      /// </summary>
      public bool UseSqlStatement
      {
         get { return _useSqlStatement; }
      }
      private bool _useSqlStatement;

      /// <summary>
      /// �����ֵ�����DataTable�������Ը�ֵ��Ͳ���Ҫ�����ȥ�������ݼ�
      /// </summary>
      public DataTable BookData
      {
         get { return _bookData; }
         set 
         { 
            if ((value != null) && (value.TableName == _bookData.TableName))
            _bookData = value; 
         }
      }
      private DataTable _bookData;

      /// <summary>
      /// Sql�ֵ�Ĭ�ϵ���ʾ��ʽ
      /// </summary>
      public GridColumnStyleCollection DefaultGridStyle
      {
         get
         {
            if (ShowStyles.Count == 0)
               return new GridColumnStyleCollection();
            else
               return ShowStyles[0];
         }
      }
      #endregion

      private bool m_AutoAddShortCode;

      #region ctors
      /// <summary>
      /// ��ָ����SQL������Ϣ����SQL�ֵ���ʵ����
      /// �ֶε���ʾ���ơ���ʾ��Щ�С��п����Ϣ��columnStyles��ָ��
      /// </summary>
      /// <param name="uniqueName">�ֲ�Ψһ������</param>
      /// <param name="query">��ѯ���</param>
      /// <param name="codeField">�����ֶ���</param>
      /// <param name="nameField">�����ֶ���</param>
      /// <param name="columnStyles">�ֶε���ʾ���ơ���ʾ��Щ�С��п����Ϣ</param>
      public SqlWordbook(string uniqueName, string query, string codeField, string nameField, GridColumnStyleCollection columnStyles)
         : base()
      {
         Debug.Assert(uniqueName != null, "��������ΪNULL");
         Debug.Assert(uniqueName.Length > 0, "��������Ϊ��");
         WordbookName = uniqueName;
         Caption = WordbookName;
         ExtraCondition = "";

         if (query.Length == 0)
            throw new ArgumentNullException("����ָ����ѯ���");
         QuerySentence = query;

         if (codeField.Length == 0)
            throw new ArgumentNullException("����ָ�������ֶ�");
         CodeField = codeField;
         QueryCodeField = CodeField;

         if (nameField.Length == 0)
            throw new ArgumentNullException("����ָ�������ֶ�");
         NameField = nameField;

         DefaultFilterFields = new Collection<string>();
         // ʹ��Ĭ�ϵĴ����к������н��й���
         DefaultFilterFields.Add(codeField);
         DefaultFilterFields.Add(nameField);

         CurrentMatchFields = new Collection<string>();
         foreach(string field in DefaultFilterFields)
            CurrentMatchFields.Add(field);

         Parameters = new FilterParameterCollection();

         ShowStyles = new Collection<GridColumnStyleCollection>();
         ShowStyles.Add(columnStyles);
         SelectedStyleIndex = 0;

         // Sql�ֵ�Ĭ��Ϊ����������
         CacheTime = -1;

         _useSqlStatement = true;
         //_bookData = null;
         //m_DrawConditions = new Collection<CustomDrawSetting>();
      }

      /// <summary>
      /// ��ָ����SQL������Ϣ����SQL�ֵ���ʵ����
      /// �ֶε���ʾ���ơ���ʾ��Щ�С��п����Ϣ��columnStyles��ָ��
      /// </summary>
      /// <param name="uniqueName">�ֲ�Ψһ������</param>
      /// <param name="query">��ѯ���</param>
      /// <param name="codeField">�����ֶ���</param>
      /// <param name="nameField">�����ֶ���</param>
      /// <param name="columnStyles">�ֶε���ʾ���ơ���ʾ��Щ�С��п����Ϣ</param>
      /// <param name="matchFieldComb">ָ������ƥ���������ݵ��ֶ��������ʱ�á�//����,���Դ����</param>
      public SqlWordbook(string uniqueName, string query, string codeField, string nameField, GridColumnStyleCollection columnStyles, string matchFieldComb)
         : this(uniqueName, query, codeField, nameField, columnStyles)
      {
         string[] separator = new string[] { SeparatorSign.ListSeparator };
         string[] values = matchFieldComb.Split(separator, StringSplitOptions.RemoveEmptyEntries);

         if (values.Length > 0)
         {
            DefaultFilterFields.Clear();
            CurrentMatchFields.Clear();
            for (int i = 0; i < values.Length; i++)
            {
               DefaultFilterFields.Add(values[i]);
               CurrentMatchFields.Add(values[i]);
            }
         }
      }
      
      /// <summary>
      /// ��ָ����SQL������Ϣ����SQL�ֵ���ʵ����
      /// �ֶε���ʾ���ơ���ʾ��Щ�С��п����Ϣ��columnStyles��ָ��
      /// </summary>
      /// <param name="uniqueName">�ֲ�Ψһ������</param>
      /// <param name="query">��ѯ���</param>
      /// <param name="codeField">�����ֶ���</param>
      /// <param name="nameField">�����ֶ���</param>
      /// <param name="columnStyles">�ֶε���ʾ���ơ���ʾ��Щ�С��п����Ϣ</param>
      /// <param name="autoAddShortCode">�Զ����ƴ���������</param>
      public SqlWordbook(string uniqueName, string query, string codeField, string nameField, GridColumnStyleCollection columnStyles, bool autoAddShortCode)
         : this(uniqueName, query, codeField, nameField, columnStyles)
      {
         if (autoAddShortCode)
         {
            DefaultFilterFields.Add("py");
            DefaultFilterFields.Add("wb");
            CurrentMatchFields.Add("py");
            CurrentMatchFields.Add("wb");
            m_AutoAddShortCode = true;
         }
      }

      /// <summary>
      /// ��ָ����SQL������Ϣ����SQL�ֵ���ʵ����
      /// �ֶε���ʾ���ơ���ʾ��Щ�С��п����Ϣ��columnStylesString��ָ��
      /// </summary>
      /// <param name="uniqueName">�ֲ�Ψһ������</param>
      /// <param name="query">��ѯ���</param>
      /// <param name="codeField">�����ֶ���</param>
      /// <param name="nameField">�����ֶ���</param>
      /// <param name="columnStylesComb">�ֶε���ʾ���ơ���ʾ��Щ�С��п����Ϣ���ԡ�//���͡�///���ָ�</param>
      /// <param name="matchFieldComb">ָ������ƥ���������ݵ��ֶ��������ʱ�á�//����,���Դ����</param>
      public SqlWordbook(string uniqueName, string query, string codeField, string nameField, string columnStylesComb, string matchFieldComb)
         : this(uniqueName, query, codeField, nameField, CreateColumnStyleCollectionByString(columnStylesComb), matchFieldComb)
      { }

      /// <summary>
      /// ��ָ����DataTable����Ϣ����SQL�ֵ���ʵ����
      /// �е���ʾ����DataColumn��Caption���棬��Ҫ��ʾ���м����п�ͨ��columnWidthes����
      /// </summary>
      /// <param name="uniqueName">�ֲ�Ψһ���ֵ���</param>
      /// <param name="sourceData">���ݼ�</param>
      /// <param name="codeField">�����ֶ���</param>
      /// <param name="nameField">�����ֶ���</param>
      /// <param name="columnWidthes">��Ҫ��ʾ�����������п�</param>
      public SqlWordbook(string uniqueName, DataTable sourceData, string codeField, string nameField, Dictionary<string, int> columnWidthes)
         : base()
      {
         Debug.Assert(uniqueName != null, "��������ΪNULL");
         Debug.Assert(uniqueName.Length > 0, "��������Ϊ��");
         WordbookName = uniqueName;
         Caption = WordbookName;

         if (sourceData == null)
            throw new ArgumentNullException("���ݼ�����Ϊ��");
         //if (sourceData.Rows.Count == 0)
         //   throw new ArgumentNullException("���ݼ���δ��������");
         _bookData = sourceData;

         // DataTable��Ĭ��RowFilter��Ϊ��չ��������
         ExtraCondition = _bookData.DefaultView.RowFilter;

         QuerySentence = "";

         if (codeField.Length == 0)
            throw new ArgumentNullException("����ָ�������ֶ�");
         if (!sourceData.Columns.Contains(codeField))
            throw new ArgumentOutOfRangeException("ָ���Ĵ����ֶ������ݼ��в�����");

         CodeField = codeField;
         QueryCodeField = CodeField;

         if (nameField.Length == 0)
            throw new ArgumentNullException("����ָ�������ֶ�");
         if (!sourceData.Columns.Contains(nameField))
            throw new ArgumentOutOfRangeException("ָ���������ֶ������ݼ��в�����");
         NameField = nameField;

         DefaultFilterFields = new Collection<string>();
         // ʹ��Ĭ�ϵĴ����к������н��й���
         DefaultFilterFields.Add(codeField);
         DefaultFilterFields.Add(nameField);

         CurrentMatchFields = new Collection<string>();
         foreach (string field in DefaultFilterFields)
            CurrentMatchFields.Add(field);

         Parameters = new FilterParameterCollection();

         ShowStyles = new Collection<GridColumnStyleCollection>();
         GridColumnStyleCollection styleCollection = new GridColumnStyleCollection();
         GridColumnStyle style;
         foreach (KeyValuePair<string, int> keyValue in columnWidthes)
         {
            style = new GridColumnStyle(keyValue.Key
               , sourceData.Columns[keyValue.Key].Caption, keyValue.Value);
            styleCollection.Add(style);
         }
         if (styleCollection.Count == 0)
            throw new ArgumentException("���������ֵ����ʾ����Ϣ");
         ShowStyles.Add(styleCollection);
         SelectedStyleIndex = 0;

         // Sql�ֵ�Ĭ��Ϊ����������
         CacheTime = -1;

         //_useSqlStatement = false;
         //_autoAddShortCode = false;
         //_shortCodeAdded = true;

         //m_DrawConditions = new Collection<CustomDrawSetting>();
      }

      /// <summary>
      /// ��ָ����DataTable����Ϣ����SQL�ֵ���ʵ����
      /// �е���ʾ����DataColumn��Caption���棬��Ҫ��ʾ���м����п�ͨ��columnWidthes����
      /// </summary>
      /// <param name="uniqueName">�ֲ�Ψһ���ֵ���</param>
      /// <param name="sourceData">���ݼ�</param>
      /// <param name="codeField">�����ֶ���</param>
      /// <param name="nameField">�����ֶ���</param>
      /// <param name="columnWidthes">��Ҫ��ʾ�����������п�</param>
      /// <param name="autoAddShortCode">�Զ����ƴ���������</param>
      public SqlWordbook(string uniqueName, DataTable sourceData, string codeField, string nameField, Dictionary<string, int> columnWidthes, bool autoAddShortCode)
         : this(uniqueName, sourceData, codeField, nameField, columnWidthes)
      {
         if (autoAddShortCode)
         {
            DefaultFilterFields.Add(GenerateShortCode.FieldPy);
            DefaultFilterFields.Add(GenerateShortCode.FieldWb);
            CurrentMatchFields.Add(GenerateShortCode.FieldPy);
            CurrentMatchFields.Add(GenerateShortCode.FieldWb);
            m_AutoAddShortCode = true;
         }
      }

      /// <summary>
      /// ��ָ����DataTable����Ϣ����SQL�ֵ���ʵ����
      /// �е���ʾ����DataColumn��Caption���棬��Ҫ��ʾ���м����п�ͨ��columnWidthes����
      /// </summary>
      /// <param name="uniqueName">�ֲ�Ψһ���ֵ���</param>
      /// <param name="sourceData">���ݼ�</param>
      /// <param name="codeField">�����ֶ���</param>
      /// <param name="nameField">�����ֶ���</param>
      /// <param name="columnWidthes">��Ҫ��ʾ�����������п�</param>
      /// <param name="matchFieldComb">ָ������ƥ���������ݵ��ֶ��������ʱ�á�//����,���Դ����</param>
      public SqlWordbook(string uniqueName, DataTable sourceData, string codeField, string nameField, Dictionary<string, int> columnWidthes, string matchFieldComb)
         : this(uniqueName, sourceData, codeField, nameField, columnWidthes)
      {
         string[] separator = new string[] { SeparatorSign.ListSeparator };
         string[] values = matchFieldComb.Split(separator, StringSplitOptions.RemoveEmptyEntries);

         if (values.Length > 0)
         {
            DefaultFilterFields.Clear();
            CurrentMatchFields.Clear();
            for (int i = 0; i < values.Length; i++)
            {
               if (!sourceData.Columns.Contains(values[i]))
                  throw new ArgumentOutOfRangeException("ָ���Ĳ�ѯ�ֶ������ݼ��в�����");
               DefaultFilterFields.Add(values[i]);
               CurrentMatchFields.Add(values[i]);
            }
         }
      }
      #endregion

      #region public methods
      /// <summary>
      /// ȷ�����ݼ���������ͳһ����SQL��Table�������͵�����Դ�������������ƴ������ֶΣ�
      /// </summary>
      /// <param name="sqlHelper">ִ��SQLʱ��Ҫ</param>
      /// <param name="shortCodeHelper">�Զ�����ƴ����ʴ���ʱ��Ҫ</param>
      public void EnsureBookData(IDataAccess sqlHelper, GenerateShortCode shortCodeHelper)
      {
         if (UseSqlStatement)
         {
            if (sqlHelper == null)
               throw new ArgumentNullException("���ݷ��ʶ���Ϊ��");
            _bookData = sqlHelper.ExecuteDataTable(QuerySentence, CommandType.Text);
         }
         if (m_AutoAddShortCode)
         {
            // ����Ѵ��ڡ�py����'wb'���򲻴����Խ�ʡʱ��
            if (BookData.Columns.Contains(GenerateShortCode.FieldPy)
               && BookData.Columns.Contains(GenerateShortCode.FieldWb))
               return;

            if (shortCodeHelper == null)
               throw new ArgumentNullException("ƴ����ʴ���������Ϊ��");
            shortCodeHelper.AutoAddShortCode(BookData, NameField);
         }
      }

      /// <summary>
      /// ��ȡ����� Expression��������ڵĻ�����
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         if (UseSqlStatement)
            return String.Format(CultureInfo.CurrentCulture
               , "{0}; {1}; {2}; {3}; {4}; {5}"
               , WordbookName
               , QuerySentence
               , CodeField
               , NameField
               , MatchFieldComb
               , ShowStyles[0]);
         else
            return String.Format(CultureInfo.CurrentCulture
               , "{0}; {1}; {2}; {3}; {4}; {5}"
               , WordbookName
               , BookData.TableName
               , CodeField
               , NameField
               , MatchFieldComb
               , ShowStyles[0]);
      }

      ///// <summary>
      ///// �������ݼ�����Grid���е���ʾ��ʽ
      ///// </summary>
      ///// <param name="sourceTable"></param>
      //public void GenerateShowStyle(DataTable sourceTable)
      //{
      //   _showStyles.Clear();
      //   SelectedStyleIndex = -1;
      //   if (sourceTable == null)
      //      return;

      //   DataColumnCollection cols = sourceTable.Columns;
      //   _showStyles.Add(new GridColumnStyleCollection());

      //   int width;
      //   // ���δ������ʾ���ƣ���ֱ����ʾԭ�ֶ��������»��߿�ͷ���ֶ�����ʾ���в���ʾ��
      //   if ((_columnNames != null) && (_columnNames.Count > 0))
      //   {
      //      foreach (KeyValuePair<string, string> name in _columnNames)
      //      {
      //         if (cols.Contains(name.Key))
      //         {
      //            if (cols[name.Key].DataType == Type.GetType("System.String"))
      //            {
      //               width = 150;
      //            }
      //            else
      //               width = GetStringLength(name.Value) * 15 / 2;
      //            _showStyles[0].Add(new GridColumnStyle(name.Key, name.Value, width));
      //         }
      //      }
      //   }
      //   else
      //   {
      //      foreach (DataColumn col in cols)
      //      {
      //         if (col.ColumnName.StartsWith("_"))
      //            continue;
      //         width = GetStringLength(col.Caption) * 15 / 2;
      //         if (col.DataType != Type.GetType("System.String"))
      //         {
      //            width = Math.Min(width, 100);
      //         }
      //         _showStyles[0].Add(new GridColumnStyle(col.ColumnName, col.Caption, width));
      //      }
      //   }
      //   SelectedStyleIndex = 0;
      //}

      /// <summary>
      /// ������Ե�ֵ
      /// </summary>
      public void ClearValueFields()
      {
         _codeValue = "";
         _nameValue = "";
      }

      /// <summary>
      /// ��ѡ�еļ�¼��ʼ������ֵ
      /// </summary>
      /// <param name="sourceRow"></param>
      public void InitValueFields(DataRow sourceRow)
      {
         ClearValueFields();

         if (sourceRow != null)
         {
            DataColumnCollection cols = sourceRow.Table.Columns;
            if (cols.Contains(CodeField))
            {
               _codeValue = sourceRow[CodeField].ToString();
               if ((cols[CodeField].DataType == Type.GetType("System.String"))
                  || (cols[CodeField].DataType == Type.GetType("System.Char")))
                  CodeFieldIsString = true;
               else
                  CodeFieldIsString = false;
            }
            if (cols.Contains(NameField))
               _nameValue = sourceRow[NameField].ToString();
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

      /// <summary>
      /// ��������ַ���ת��ΪGridColumnStyleCollection����
      /// </summary>
      /// <param name="stylesComb">����ʽ�ַ������</param>
      /// <returns></returns>
      public static GridColumnStyleCollection CreateColumnStyleCollectionByString(string stylesComb)
      {
         GridColumnStyleCollection styleCollection = new GridColumnStyleCollection();
         if (String.IsNullOrEmpty(stylesComb))
            return styleCollection;
         // �ȷֽ������������ʾ��ʽ
         string[] sepColumn = new string[] { SeparatorSign.OtherSeparator };
         string[] styles = stylesComb.Split(sepColumn, StringSplitOptions.None);
         string[] sepValue = new string[] { SeparatorSign.ListSeparator };
         string[] values;
         foreach (string styleString in styles)
         {
            values = styleString.Split(sepValue, StringSplitOptions.None);
            styleCollection.Add(new GridColumnStyle(values[0], values[1]
               , Convert.ToInt32(values[2], CultureInfo.CurrentCulture)));
         }
         return styleCollection;
      }
      #endregion
   }
}
