using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DrectSoft.Core;
using DrectSoft.Wordbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DrectSoft.Common.Library
{
    /// <summary>
    /// ѡ������Ԥ�����ֵ�Ĵ���
    /// </summary>
    [ToolboxBitmapAttribute(typeof(DrectSoft.Common.Library.WordbookChooseForm), "Images.WordbookChooseForm.ico")]
    public partial class WordbookChooseForm : Form
    {
        #region properties
        /// <summary>
        /// ��ǰѡ����ֵ���
        /// </summary>
        public BaseWordbook Wordbook
        {
            get { return _wordbook; }
            set
            {
                _wordbook = value;
                // ��ѡ�е��ֵ��ʼ������
                InitializeEditBox();
            }
        }
        private BaseWordbook _wordbook;

        /// <summary>
        /// �������ݿ�Ķ���
        /// </summary>
        public IDataAccess SqlExecutor
        {
            get { return _sqlExecutor; }
            set
            {
                _sqlExecutor = value;
                btnPreviewData.Enabled = (_sqlExecutor != null);
            }
        }
        private IDataAccess _sqlExecutor;
        #endregion

        #region private variable
        private DataTable m_PreviewTable;
        private DataTable m_ParameterTable;
        private DataTable m_DataCatalogTable;
        private Dictionary<int, RepositoryItemLookUpEdit> m_RepItemSelDataCatalogs;
        #endregion

        /// <summary>
        /// �����ֵ�ѡ�񴰿�ʵ��
        /// </summary>
        public WordbookChooseForm()
        {
            InitializeComponent();

            // �˴�Ҫ�Ľ�������
            // ���ʱ���ã���ҪԤ�����ݡ���֤SQL���ʱ�������ȳ������������򵼣��ṩ���ַ�ʽ��
            //    1��ָ�������ļ���ѡ��Database
            //    2������ADO���Ӵ� 

            //splitContainer1.Panel1.MinSize = 170;
            //splitContainer1.Panel2.MinSize = 420;
            btnPreviewData.Enabled = false;
            // ��ʱ�ڴ����ڲ������������ӣ��Ժ�Ҫ�ĳɴ��ⲿ���룡����
            SqlExecutor = new SqlDataAccess("EMRDB");
            if (SqlExecutor != null)
            {
                string command = "select ID, Name, CategoryID, Py, Wb, Memo from CategoryDetail order by CategoryID, ID";

                m_DataCatalogTable = SqlExecutor.ExecuteDataTable(command);

            }
        }

        #region public method
        /// <summary>
        /// ��Ĭ�Ϸ�ʽ����Ԥ�����ֵ�ѡ�񴰿�
        /// </summary>
        /// <returns>ѡ���ֵ�Ĺؼ���Ϣ��ɵ��ַ���</returns>
        public string ShowForm()
        {
            return CallSelectForm();
        }

        /// <summary>
        /// �ô�����ֵ�ؼ���Ϣ��ʼ��ѡ���ֵ䣬������Ԥ�����ֵ�ѡ�񴰿�
        /// </summary>
        /// <param name="keyInfo">�ֵ�Ĺؼ���Ϣ��ɵ��ַ���</param>
        /// <returns>ѡ���ֵ�Ĺؼ���Ϣ��ɵ��ַ���</returns>
        public string ShowForm(string keyInfo)
        {
            _wordbook = WordbookStaticHandle.GetWordbookByString(keyInfo);
            return CallSelectForm();
        }

        #endregion

        #region private methods
        /// <summary>
        /// ��ʾԤ�����ֵ�ѡ�񴰿�
        /// </summary>
        /// <returns>ѡ���ֵ�Ĺؼ���Ϣ��ɵ��ַ���</returns>
        private string CallSelectForm()
        {
            InitializeEditBox();
            if ((this.ShowDialog() == DialogResult.OK) && (_wordbook != null))
            {
                return _wordbook.Caption + SeparatorSign.OtherSeparator
                   + _wordbook.WordbookName + SeparatorSign.OtherSeparator
                   + _wordbook.ParameterValueComb;
            }
            else
            {
                _wordbook = null;
                return "";
            }
        }

        /// <summary>
        /// �õ�ǰѡ�е��ֵ������Ϣ��ʼ���༭����
        /// </summary>
        private void InitializeEditBox()
        {
            gridViewData.Columns.Clear();
            gridCtrlData.DataSource = null;
            if (_wordbook == null)
            {
                // ������Ϣ
                tBoxName.Text = "";
                gridCtrlPara.DataSource = null;
                m_ParameterTable = null;
                m_PreviewTable = null;
            }
            else
            {
                // ������Ϣ
                tBoxName.Text = _wordbook.Caption;

                m_ParameterTable = _wordbook.Parameters.Convert2DataTable();
                m_ParameterTable.DefaultView.RowFilter = "AllowUserEdit = 1";

                gridCtrlPara.DataSource = m_ParameterTable;
                // ����Ԥ������
                if (_sqlExecutor != null)
                {
                    string commandText = _wordbook.QuerySentence; // .GenerateSqlSentence();
                    m_PreviewTable = SqlExecutor.ExecuteDataTable(commandText, CommandType.Text);
                    m_PreviewTable.DefaultView.RowFilter = _wordbook.GenerateFilterExpression();

                    gridViewData.Columns.Clear();
                    // gridViewData.Columns.AddRange(_wordbook.GenerateDevGridColumnCollection());

                    gridCtrlData.DataSource = m_PreviewTable;
                }
            }
        }
        #endregion

        #region events

        private void wordbookTree_TreeDoubleClick(object sender, EventArgs e)
        {
            if (WordbookTree.CurrentBookInfo.TypeName != null)
            {
                _wordbook = WordbookStaticHandle.GetWordbook(WordbookTree.CurrentBookInfo.TypeName);
                // ��ѡ�е��ֵ��ʼ������
                InitializeEditBox();
            }
        }

        private void btnPreviewData_Click(object sender, EventArgs e)
        {
            m_PreviewTable.DefaultView.RowFilter = _wordbook.GenerateFilterExpression();
        }
        #endregion

        private void gridCtrlPara_Leave(object sender, EventArgs e)
        {
            // �����û��޸Ĺ��Ĳ���ֵ
            if (m_ParameterTable == null)
                return;

            foreach (DataRow row in m_ParameterTable.Rows)
            {
                if (Convert.ToBoolean(row["Enable"], CultureInfo.CurrentCulture))
                {
                    _wordbook.Parameters[row["Name"].ToString().Trim()].Enabled = true;
                    _wordbook.Parameters[row["Name"].ToString().Trim()].Value = row["Value"].ToString();
                }
                else
                {
                    _wordbook.Parameters[row["Name"].ToString().Trim()].Enabled = false;
                }
            }
        }

        private int GetDataCatalogOfRow(int rowHandle)
        {
            if (rowHandle < 0)
                return -1;
            DataRow sourceRow = gridViewPara.GetDataRow(rowHandle);
            if ((Convert.ToBoolean(sourceRow["IsString"], CultureInfo.CurrentCulture))
               || (sourceRow["DataSort"].ToString().Length == 0))
                return -1;

            return Convert.ToInt32(sourceRow["DataSort"], CultureInfo.CurrentCulture);
        }

        private void gridViewPara_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Value")
            {
                int dataSort = GetDataCatalogOfRow(e.RowHandle);
                if (dataSort >= 0)
                {
                    if (m_DataCatalogTable != null)
                        e.RepositoryItem = GetRepositoryItem(dataSort);
                }
            }
        }

        private RepositoryItem GetRepositoryItem(int dataSort)
        {
            if (dataSort < 0)
                return null;

            if (m_DataCatalogTable == null)
                return null;

            if (m_RepItemSelDataCatalogs == null)
                m_RepItemSelDataCatalogs = new Dictionary<int, RepositoryItemLookUpEdit>();

            if (m_RepItemSelDataCatalogs.ContainsKey(dataSort))
                return m_RepItemSelDataCatalogs[dataSort];

            m_DataCatalogTable.DefaultView.RowFilter = "CategoryID = " + dataSort.ToString();

            RepositoryItemLookUpEdit newRepItem = new RepositoryItemLookUpEdit();
            newRepItem.BeginInit();

            newRepItem.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            newRepItem.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            newRepItem.DisplayMember = "Name";
            newRepItem.DataSource = m_DataCatalogTable.DefaultView.ToTable();
            newRepItem.ValueMember = "ID";
            gridCtrlPara.RepositoryItems.Add(newRepItem);

            newRepItem.EndInit();
            m_RepItemSelDataCatalogs.Add(dataSort, newRepItem);
            return newRepItem;
        }
    }
}