/*****************************************************************************\
**             DrectSoft Software Co. Ltd.                          **
**                                                                           **
**  �ֵ���Ļ���.                                                             **
**  �ṩĬ�ϵ��ֵ������Ժ͹�������,��������Ĺ��캯����Ϊ���Խ��г�ʼ��.         ** 
**                                                                           **
\*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraGrid.Columns;
using System.Xml.Serialization;
using System.Diagnostics;
using DrectSoft.Common.Eop;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// �����ֵ����
   /// </summary>  
   [TypeConverter(typeof(NormalWordbookConverter))]
   public abstract class BaseWordbook
   {
      #region readonly property
      /// <summary>
      /// �ֵ����ƣ���ʾ���ƣ�
      /// </summary>
      [Description("�ֵ���ʾ����"), ReadOnly(true)]
      public string Caption
      {
         get { return _caption; }
         internal set { _caption = value; }
      }
      private string _caption;

      /// <summary>
      /// �ֵ����ƣ��ϼ������ռ�+������������Ϊ���ݼ������ƣ�
      /// </summary>
      [Description("�ֵ�����"), ReadOnly(true)]
      public string WordbookName
      {
         get { return _wordbookName; }
         internal set { _wordbookName = value; }
      }
      private string _wordbookName;

      /// <summary>
      /// Ĭ�ϴ����ֶ�������ͨ�����뷴������ʱ��Ҫ
      /// </summary>
      [Description("Ĭ�ϴ����ֶ�����"), ReadOnly(true)]
      public string CodeField
      {
         get { return _codeField; }
         internal set { _codeField = value; }
      }
      private string _codeField;

      /// <summary>
      /// Ĭ�ϴ����ֶ�������ͨ�����뷴������ʱ��Ҫ
      /// </summary>
      [Description("�����ֶ����������Ƿ����ַ���"), ReadOnly(true)]
      public bool CodeFieldIsString
      {
         get { return _codeFieldIsString; }
         internal set { _codeFieldIsString = value; }
      }
      private bool _codeFieldIsString;

      /// <summary>
      /// Ĭ����ʾ�ֶ����������е�������ʾ�ڱ༭���С�
      /// </summary>
      [Description("Ĭ����ʾ�ֶ�����"), ReadOnly(true)]
      public string NameField
      {
         get { return _nameField; }
         internal set { _nameField = value; }
      }
      private string _nameField;

      /// <summary>
      /// Ĭ�ϲ�ѯ�����ֶ���������Щ����������Ҫ֧�ֱ����������ڲ�������Ҫ��Code����λ��¼����QueryCode��Ϊ��ѯ��������
      /// </summary>
      [Description("Ĭ�ϲ�ѯ�����ֶ�����"), ReadOnly(true)]
      public string QueryCodeField
      {
         get
         {
            if (String.IsNullOrEmpty(_queryCodeField))
               return _codeField;
            else
               return _queryCodeField;
         }
         internal set { _queryCodeField = value; }
      }
      private string _queryCodeField;

      /// <summary>
      /// ��ѯ���ݵ���䡣
      /// ��ͨ�Ĳ�ѯ��䣬��������ǰһ������ "py like :py" �����԰���������䡣
      /// �����Ҫ�ⲿ����Ĳ���������Where�����Ԥ��������λ�ã���ʽ��"@"+"������"�����ܲ����Ǻ����ͣ�����Ҫ�����š�
      /// </summary>
      [Browsable(false)]
      public string QuerySentence
      {
         get { return _querySentence; }
         internal set { _querySentence = value; }
      }
      private string _querySentence;

      /// <summary>
      /// ��ǰʹ�õ�ѡ�����ֶ������ϣ�������Ҫ����������
      /// </summary>
      [Browsable(false)]
      public Collection<string> CurrentMatchFields
      {
         get { return _currentMatchFields; }
         internal set { _currentMatchFields = value; }
      }
      private Collection<string> _currentMatchFields;

      /// <summary>
      /// ��ѡ�����ʾ��������
      /// </summary>
      [Browsable(false)]
      public Collection<GridColumnStyleCollection> ShowStyles
      {
         get { return _showStyles; }
         internal set { _showStyles = value; }
      }
      private Collection<GridColumnStyleCollection> _showStyles;

      /// <summary>
      /// ���˲����ļ��ϡ��������ò���ʱ�����á����Ӳ������޸Ĳ���ֵ
      /// </summary>
      [Browsable(false)]
      public FilterParameterCollection Parameters
      {
         get { return _parameters; }
         internal set { _parameters = value; }
      }
      private FilterParameterCollection _parameters;
      #endregion

      #region property
      /// <summary>
      /// ����ƥ�����ݵĵ��ֶ�������ϣ�����ֶ�ʱ��"//"����������ǰ������ȼ���
      /// </summary>
      [Browsable(false), DesignOnly(true), Description(@"����ƥ�����ݵĵ��ֶ��������,����ֶ�ʱ��'//'����,����ǰ������ȼ���")]
      public string MatchFieldComb
      {
         get
         {
            if (_currentMatchFields.Count > 0)
            {
               StringBuilder values = new StringBuilder(_currentMatchFields[0]);
               for (int index = 1; index < _currentMatchFields.Count; index++)
               {
                  values.Append(SeparatorSign.ListSeparator);
                  values.Append(_currentMatchFields[index]);
               }
               return values.ToString();
            }
            else
               return "";
         }
         set
         {
            if ((value == null) || (value.Length == 0))
               throw new ArgumentNullException("����ָ��������");

            string[] separator = new string[] { SeparatorSign.ListSeparator };
            string[] values = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length > _defaultFilterFields.Count)
               throw new ArgumentOutOfRangeException("ָ���Ĵ�������������Ĭ�ϴ���Ķ���");

            // _filterField = value;
            _currentMatchFields.Clear();
            for (int i = 0; i < values.Length; i++)
            {
               _currentMatchFields.Add(values[i]);
            }
         }
      }

      /// <summary>
      /// Ĭ�ϵ���Ϊ�����е��ֶ���
      /// </summary>
      internal Collection<string> DefaultFilterFields
      {
         get { return _defaultFilterFields; }
         set { _defaultFilterFields = value; }
      }
      private Collection<string> _defaultFilterFields;

      /// <summary>
      /// ��ǰѡ�е���ʾ�������
      /// </summary>
      [Description("Ĭ�ϵ���ʾ�������")]
      public int SelectedStyleIndex
      {
         get { return _selectedStyleIndex; }
         set
         {
            // ����ʾ����ʱ���ԭ�ȵ�Column����
            if (_selectedStyleIndex != value)
            {
               ClearGridColumns();
            }

            if (value < 0)
               _selectedStyleIndex = 0;
            else if (value >= _showStyles.Count)
               _selectedStyleIndex = _showStyles.Count - 1;
            else
               _selectedStyleIndex = value;
         }
      }
      private int _selectedStyleIndex;

      /// <summary>
      /// ���˲�����Ĭ��ֵ��ϡ��ԡ�������//ֵ������ʽ���
      /// </summary>
      [Browsable(false), DesignOnly(true), Description(@"���˲�����Ĭ��ֵ��ϡ��ԡ�������//ֵ������ʽ���")]
      public string ParameterValueComb
      {
         get
         {
            if ((_parameters == null) || (_parameters.Count == 0))
               return "";
            else
            {
               StringBuilder values = new StringBuilder();
               for (int index = 0; index < _parameters.Count; index++)
               {
                  if (!_parameters[index].Enabled)
                     continue;
                  if (values.Length > 0)
                     values.Append(SeparatorSign.ListSeparator);
                  values.Append(_parameters[index].Caption);
                  values.Append(SeparatorSign.ListSeparator);
                  values.Append(_parameters[index].Value);
               }
               return values.ToString();
            }
         }
         set
         {
            if (value == null)
               throw new ArgumentNullException(MessageStringManager.GetString("NullParameter"));
            if ((_parameters != null) && (_parameters.Count > 0))
            {
               foreach (FilterParameter para in _parameters)
                  para.Enabled = false;
               if ((value == null) || (value.Length == 0))
               {
                  return;
               }
               string[] separator = new string[] { SeparatorSign.ListSeparator };
               string[] values = value.Split(separator, StringSplitOptions.None);

               int indexP;
               string temp;
               for (int i = 0; i < values.Length; i++)
               {
                  indexP = _parameters.IndexOf(values[i++]);
                  if (indexP >= 0)
                  {
                     _parameters[indexP].Enabled = true;
                     // ���ڲ��ܲ����Ǻ������ͣ�����ֵ�����üӵ�����
                     // ��Ϊ���е��������ַ��͵Ĳ���ֵ���Ѽ������ţ������������ô���ȥ������
                     if (_parameters[indexP].IsString)
                     {
                        temp = values[i];
                        if ((temp.Length > 1)
                           && (temp[0] == '\'') && (temp[temp.Length - 1] == '\''))
                           _parameters[indexP].Value = temp.Substring(1, temp.Length - 2);
                        else
                           _parameters[indexP].Value = temp;
                     }
                     else
                        _parameters[indexP].Value = values[i];
                  }
               }
            }
         }
      }

      /// <summary>
      /// �������ݵĸ���SQL����
      /// </summary>
      [Browsable(false), Description("�������ݵĸ���SQL����(����DataTable����������)")]
      public string ExtraCondition
      {
         get { return _extraCondition; }
         set { _extraCondition = value; }
      }
      private string _extraCondition;

      /// <summary>
      /// �ֵ�������ShowList�����еĻ���ʱ�䣬��λ�룬0��ʾ�����ơ�
      /// </summary>
      [
        Description("�ֵ�������ShowList�����еĻ���ʱ��(��λ:��)��-1:������, 0: �����ơ�"),
        DefaultValue(0)
      ]
      public int CacheTime
      {
         get { return _cacheTime; }
         set
         {
            if (value < -1)
               _cacheTime = -1;
            else
               _cacheTime = value;
         }
      }
      private int _cacheTime;

      /// <summary>
      /// ���ݵ�ǰѡ�е��ֵ��¼�������Ӧ�Ķ���ʵ��.
      /// ������������ж����ݿ�Ĳ���,Ҫ�ǵø������SqlHelper���Ը�ֵ
      /// </summary>
      [Browsable(false)]
      public EPBaseObject PersistentObject
      {
         get
         {
            if (_persistentObject == null)
            {
               // ��������
               return CreatePersistentWordbook();
            }
            else
               return _persistentObject;
         }
      }
      private EPBaseObject _persistentObject;

      /// <summary>
      /// ������ʼ����ǰѡ���ֵ��¼�����DataRow
      /// </summary>
      [Browsable(false), Description("������ʼ����ǰѡ���ֵ��¼�����DataRow")]
      public DataRow CurrentRow
      {
         get { return _currentRow; }
         set
         {
            if (value == null)
               _currentRow = null;
            else // ����DataRow��ֵ�ͽṹ
            {
               _currentRow = value.Table.NewRow();
               _currentRow.ItemArray = value.ItemArray;
            }
            _persistentObject = null;
         }
      }
      private DataRow _currentRow;
      #endregion

      #region internal variable
      /// <summary>
      /// �뵱ǰ��ʾ����һ�µ�DataGridViewColumn����
      /// </summary>
      internal DataGridViewColumn[] m_GridColumns;
      internal GridColumn[] m_DevGridColumns;
      ///// <summary>
      ///// CustomDraw���ü���
      ///// </summary>
      //internal Collection<CustomDrawSetting> m_DrawConditions;
      #endregion

      #region private methods
      /// <summary>
      /// ����Ѵ�����GridColumn
      /// </summary>
      private void ClearGridColumns()
      {
         if (m_GridColumns != null)
         {
            for (int i = 0; i < m_GridColumns.Length; i++)
               m_GridColumns[i].Dispose();
         }
         m_GridColumns = null;
      }

      private void ClearDxGridColumns()
      {
         if (m_DevGridColumns != null)
         {
            for (int i = 0; i < m_DevGridColumns.Length; i++)
               m_DevGridColumns[i].Dispose();
         }
         m_DevGridColumns = null;
      }

      /// <summary>
      /// �ֽ������ΪIn�Ĺ��˲�����������ϺõĹ����������ʽ
      /// </summary>
      /// <param name="para"></param>
      private static string GenerateConditionFromInParameter(FilterParameter para)
      {
         string formatString = " {0} {1} {2}";
         StringBuilder expressions = new StringBuilder();
         // ����','��Ϊ�ָ�������ֳ�����������
         string[] separator1 = new string[] { "," };
         string[] separator2 = new string[] { "��" };
         string[] values = para.ParameterValue.ToString().Split(separator1, StringSplitOptions.None);
         string[] rangs;
         StringBuilder inValues = new StringBuilder(); // ����In�����ĵ���ֵ

         foreach (string condition in values)
         {
            if (condition.Contains("��")) // ��Χ��ת��Ϊ">=" �� "<="��������
            {
               if (expressions.Length > 0)
                  expressions.Append(" OR ");
               rangs = condition.Split(separator2, StringSplitOptions.None);
               expressions.AppendFormat(CultureInfo.CurrentCulture
                  , " ({0} >= {1} AND {0} <= {2})"
                  , para.FieldName
                  , rangs[0], rangs[1]);
            }
            else if (condition.Contains("%")) // ����"%"��ת��Ϊ"like"����
            {
               if (expressions.Length > 0)
                  expressions.Append(" OR ");
               expressions.Append(String.Format(CultureInfo.CurrentCulture
                  , formatString
                  , para.FieldName
                  , CommonOperation.GetOperatorSign(CompareOperator.Like)
                  , condition));
            }
            else //�����ĵ���ֵ����������ΪIn������ֵ,���ͳһ��
            {
               if (inValues.Length > 0)
                  inValues.Append(',');
               inValues.Append(condition);
            }
         }
         if (inValues.Length > 0)
         {
            if (expressions.Length > 0)
               expressions.Append(" OR ");
            expressions.Append(String.Format(CultureInfo.CurrentCulture
               , "{0} in ({1})"
               , para.FieldName
               , inValues.ToString()));
         }
         return "(" + expressions.ToString() + ")";
      }

      /// <summary>
      /// ���ݵ�ǰѡ�е�DataRow����PersistentWordbookʵ��
      /// </summary>
      /// <returns></returns>
      protected abstract EPBaseObject CreatePersistentWordbook();
      #endregion

      #region ctors
      /// <summary>
      /// �����ֵ���ʵ��
      /// </summary>
      protected BaseWordbook()
      { }

      /// <summary>
      /// ��DrectSoftWordbooks.XML�и���ָ�����ֵ��������������ݣ������ֵ�ʵ��
      /// XML�ļ�������Ҫ����Wordbook.XSD�Ķ��塣����Ĵ��������ǰ�XSD�Ķ�����еġ�
      /// </summary>
      /// <param name="name">�ֵ�����</param>
      protected BaseWordbook(string name)
      {
         Schema.Wordbook source = WordbookStaticHandle.GetSourceWordbookByName(name);

         if (String.IsNullOrEmpty(source.WordbookName))
            throw new ArgumentException("û����ȷ��ʼ���ֵ䣬�����������");

         // ��ʼ�����ֵ������  
         _wordbookName = source.WordbookName;
         _caption = source.Caption;
         _querySentence = PersistentObjectFactory.GetQuerySentenceByName(source.QuerySentence);
         _codeField = source.CodeField;
         _nameField = source.NameField;
         _queryCodeField = source.QueryCodeField;
         _codeFieldIsString = source.CodeFieldIsString;

         // ���˲���
         _parameters = new FilterParameterCollection();
         _showStyles = new Collection<GridColumnStyleCollection>();
         _defaultFilterFields = new Collection<string>();

         if (source.FilterFieldCollection != null)
            foreach (string field in source.FilterFieldCollection)
               _defaultFilterFields.Add(field);

         if (source.ParameterCollection != null)
            _parameters.AddRange(source.ParameterCollection);

         GridColumnStyleCollection columnStyle;
         foreach (GridColumnStyle[] style in source.ViewStyleCollection)
         {
            columnStyle = new GridColumnStyleCollection();
            columnStyle.AddRange(style);
            _showStyles.Add(columnStyle);
         }

         // ��������ʹ��Ĭ��ֵ
         SelectedStyleIndex = 0;
         ExtraCondition = "";

         _currentMatchFields = new Collection<string>();
         foreach (string field in _defaultFilterFields)
            _currentMatchFields.Add(field);
      }

      /// <summary>
      /// �����ֵ�������ͬʱ����ʼ���������õ�����
      /// </summary>
      /// <param name="name">Ҫ�������ֵ������</param>param 
      /// <param name="filters">������Ϊ�����е��ֶ���,����ֶ�ʱ��"\n"����������ǰ������ȼ���</param>
      /// <param name="gridStyleIndex">Ĭ��ѡ�е�Grid��ʾ����</param>
      /// <param name="filterComb">���������Ĭ��ֵ,�������ʱ��"\n"����</param>
      /// <param name="extraCondition">�����Ĳ�ѯ����</param>
      /// <param name="cacheTime">����ʱ��</param>
      protected BaseWordbook(string name, string filters, int gridStyleIndex, string filterComb, string extraCondition, int cacheTime)
         : this(name)
      {
         this.MatchFieldComb = filters;
         this.ParameterValueComb = filterComb;
         this.SelectedStyleIndex = gridStyleIndex;
         this.ExtraCondition = extraCondition;
         this.CacheTime = cacheTime;
      }
      #endregion

      #region public methods
      /// <summary>
      /// ��ȡ�ֵ����� Expression��������ڵĻ���
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         return this._wordbookName;
      }

      /// <summary>
      /// ��ȡ��ǰ�ֵ���Ĭ�ϵĹ�������
      /// </summary>
      /// <returns></returns>
      public string GenerateFilterExpression()
      {
         string formatString = " {0} {1} {2}"; // �ֶ��� ������ ֵ
         StringBuilder expressions = new StringBuilder();
         // ���Ӹ�������
         if ((ExtraCondition != null) && (ExtraCondition.Length > 0))
            expressions.Append(ExtraCondition);

         // ���Ӳ�������
         foreach (FilterParameter para in _parameters)
         {
            if (!para.Enabled)
               continue;
            if (expressions.Length > 0)
               expressions.Append(" AND ");
            // �����������IN�������⴦��(����ֵ�����Ƿ�Χ�͵�)
            if (para.Operator == CompareOperator.In)
               expressions.Append(GenerateConditionFromInParameter(para));
            else
               expressions.Append(String.Format(CultureInfo.CurrentCulture
                  , formatString
                  , para.FieldName
                  , CommonOperation.GetOperatorSign(para.Operator)
                  , CommonOperation.TransferCondition(para.ParameterValue, para.Operator)));
         }

         return expressions.ToString();
      }

      /// <summary>
      /// ���ݵ�ǰ��ʾ����������DataGridViewColumn����
      /// </summary>
      /// <returns></returns>      
      public DataGridViewColumn[] GenerateGridColumnCollection()
      {
         if (m_GridColumns == null)
         {
            m_GridColumns = new DataGridViewColumn[_showStyles[_selectedStyleIndex].Count];

            DataGridViewCellStyle styleNormal = new DataGridViewCellStyle();
            styleNormal.NullValue = "";

            DataGridViewTextBoxColumn newColumn;
            for (int i = 0; i < m_GridColumns.Length; i++)
            {
               newColumn = new DataGridViewTextBoxColumn();

               newColumn.DataPropertyName = _showStyles[_selectedStyleIndex][i].FieldName;
               newColumn.DefaultCellStyle = styleNormal;
               newColumn.HeaderText = _showStyles[_selectedStyleIndex][i].Caption;
               newColumn.Name = "Col" + _showStyles[_selectedStyleIndex][i].FieldName;
               newColumn.Width = _showStyles[_selectedStyleIndex][i].Width;

               m_GridColumns[i] = newColumn;
            }
         }

         return m_GridColumns;
      }

      /// <summary>
      /// ���ݵ�ǰ��ʾ����������DevXtraGridColumn����
      /// </summary>
      /// <returns></returns>      
      public GridColumn[] GenerateDevGridColumnCollection()
      {
         if (m_DevGridColumns == null)
         {
            m_DevGridColumns = new GridColumn[_showStyles[_selectedStyleIndex].Count];

            GridColumn newColumn;
            for (int i = 0; i < m_DevGridColumns.Length; i++)
            {
               newColumn = new GridColumn();

               newColumn.Caption = _showStyles[_selectedStyleIndex][i].Caption;
               newColumn.FieldName = _showStyles[_selectedStyleIndex][i].FieldName;
               newColumn.Name = "Col" + _showStyles[_selectedStyleIndex][i].FieldName;
               newColumn.Visible = true;
               newColumn.VisibleIndex = i;
               newColumn.Width = _showStyles[_selectedStyleIndex][i].Width;

               m_DevGridColumns[i] = newColumn;
            }
         }
         else // ȥ��Column�����������
         {
            foreach (GridColumn col in m_DevGridColumns)
            {
               col.ClearFilter();
               col.SortIndex = -1;
               col.SortOrder = DevExpress.Data.ColumnSortOrder.None;
            }
         }

         return m_DevGridColumns;
      }
      #endregion
   }
}
