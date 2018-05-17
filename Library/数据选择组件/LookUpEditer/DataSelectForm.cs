using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using DrectSoft.Core;
using DrectSoft.Wordbook;

namespace DrectSoft.Common.Library
{
    internal partial class DataSelectForm : CaptureCursorForm
    {
        #region readonly value defines
        /// <summary>
        /// �����Ĭ�ϱ���ɫ��������Ϊ��System.Controlһ������ɫ
        /// </summary>
        private readonly Color ColorFormBack = Color.Silver;
        /// <summary>
        /// �������ӿؼ���Ĭ�ϱ���ɫ
        /// </summary>
        private readonly Color ColorChildBack = Color.WhiteSmoke;// .Gainsboro;
        /// <summary>
        /// �������ӿؼ���Ĭ��ǰ��ɫ
        /// </summary>
        private readonly Color ColorChildFore = SystemColors.ControlText;
        /// <summary>
        /// ��Ӱ���ֵı���ɫ
        /// </summary>
        private readonly Color ColorShadowBack = SystemColors.ButtonShadow;
        /// <summary>
        /// Ĭ�ϵ���Ӱ����ƫ����
        /// </summary>
        private readonly int DefaultShadowOffset = 5;
        /// <summary>
        /// Ĭ���������ֶ�ѡ�ַ����ķָ���
        /// </summary>
        private readonly string[] MultiStringSeparators = new string[] { "��", ",", "��" };
        #endregion

        #region properties
        /// <summary>
        /// �Ƿ���ʾ�ֵ��б���б��⡣Ĭ����ʾ
        /// </summary>
        public bool ShowColumnTitle
        {
            get { return _showColumnTitle; }
            set { _showColumnTitle = value; }
        }
        private bool _showColumnTitle;

        /// <summary>
        /// �Ƿ���ʾ�ֵ��б��е�����Ĭ����ʾ
        /// </summary>
        public bool ShowGridline
        {
            get { return _showGridline; }
            set { _showGridline = value; }
        }
        private bool _showGridline;

        /// <summary>
        /// �Ƿ�ϰ��ʹ����ʴ���
        /// </summary>
        public bool UseWB
        {
            get { return _useWB; }
            set { _useWB = value; }
        }
        private bool _useWB;

        /// <summary>
        /// ��̬��ѯ
        /// </summary>
        public bool IsDynamic
        {
            get { return _isDynamic; }
            set
            {
                _isDynamic = value;
                if (DesignMode)
                    return;
                SynchDynamicToControl();
            }
        }
        private bool _isDynamic;

        /// <summary>
        /// ��ѯƥ��ģʽ
        /// </summary>
        public ShowListMatchType MatchType
        {
            get { return _matchType; }
            set
            {
                ShowListMatchType tempType = _matchType;
                _matchType = value;
                if (DesignMode)
                    return;

                if (tempType != _matchType)
                {
                    if (Visible) // ���ڴ򿪵�����µ���
                    {
                        SynchMatchTypeToControl();
                        SearchMatchRow(m_SearchText, _matchType, false);
                    }
                    else
                        m_NeedSynchMatchType = true;
                }
            }
        }
        private ShowListMatchType _matchType;

        /// <summary>
        /// ShowList���ڵ���ʾģʽ
        /// </summary>
        public ShowListFormMode FormMode
        {
            get { return _formMode; }
            set
            {
                _formMode = value;
                if (value == ShowListFormMode.Full)
                    panelBottom.Height = m_DefBottomRegionHeight;
                else
                    panelBottom.Height = 0;
            }
        }
        private ShowListFormMode _formMode;

        ///// <summary>
        ///// �Ƿ���ʾ��ȷ�ϡ�����ȡ������ť
        ///// </summary>
        //[
        //  Browsable(false),
        //  Category("ShowListConfig"),
        //  Description("�Ƿ���ʾ��ȷ�ϡ�����ȡ������ť"),
        //  ReadOnly(true)
        //]
        //public bool ShowFormButton
        //{
        //   get { return _showFormButton; }
        //   //set { _showFormButton = value; }
        //}
        //private bool _showFormButton;

        /// <summary>
        /// ��ǰShowList���ڴ�����ֵ���ʵ��
        /// </summary>
        public BaseWordbook Wordbook
        {
            get { return _wordbook; }
            //set { _wordbook = value; }
        }
        private BaseWordbook _wordbook;

        /// <summary>
        /// ��ǰShowList���ڴ�����ֵ�������
        /// </summary>
        public WordbookKind BookKind
        {
            get { return _bookKind; }
            //set { _wordbookKind = value; }
        }
        private WordbookKind _bookKind;

        /// <summary>
        /// ShowList�����Ƿ���ʾ��Ӱ
        /// </summary>
        public bool ShowShadow
        {
            get { return _showShadow; }
            set
            {
                _showShadow = value;
                if (_showShadow)
                    m_ShadowOffset = DefaultShadowOffset;
                else
                    m_ShadowOffset = 0;
            }
        }
        private bool _showShadow;

        /// <summary>
        /// ��ʾ�ڱ༭���е�ֵ���������ʱ�á�,������
        /// </summary>
        public string DisplayValue
        {
            get { return _displayValue; }
            //set { _displayValue = value; }
        }
        private string _displayValue;

        /// <summary>
        /// �༭���е�ֵ��Ӧ�Ĵ��롣�������ʱ�á�,������
        /// </summary>
        public string CodeValue
        {
            get { return _codeValue; }
        }
        private string _codeValue;

        /// <summary>
        /// �༭���е�ֵ��Ӧ�Ĵ��롣�������ʱ�á�,������
        /// </summary>
        public string QueryValue
        {
            get { return _queryValue; }
        }
        private string _queryValue;

        /// <summary>
        /// ѡ�е����ݼ�¼�м���
        /// </summary>
        public List<DataRow> ResultRows
        {
            get
            {
                if (_resultRows == null)
                    _resultRows = new List<DataRow>();
                return _resultRows;
            }
        }
        private List<DataRow> _resultRows;

        /// <summary>
        /// �Ƿ�������ʾShowList����(Ĭ��ֻ��һ��ƥ���¼ʱ����ʾ����)
        /// </summary>
        public bool AlwaysShowWindow
        {
            get { return _alwaysShowWindow; }
            set { _alwaysShowWindow = value; }
        }
        private bool _alwaysShowWindow;

        /// <summary>
        /// ִ��SQL����DataAccess��������ʹ��ǰ��ʼ��������
        /// </summary>
        public IDataAccess SqlHelper
        {
            get { return _sqlHelper; }
            set { _sqlHelper = value; }
        }
        private IDataAccess _sqlHelper;

        /// <summary>
        /// ����ƴ���������д�Ķ������û�г�ʼ����������_sqlHelper�����¶���
        /// </summary>
        public GenerateShortCode GenShortCode
        {
            get
            {
                if (DesignMode)
                    return null;

                if (_genShortCode == null)
                {
                    if (_sqlHelper != null)
                        _genShortCode = new GenerateShortCode(_sqlHelper);
                    //else
                    //   throw new ArgumentNullException("����ƴ���������д�Ķ��󲻴���");

                    return _genShortCode;
                }
                else
                    return _genShortCode;
            }
            set { _genShortCode = value; }
        }
        private GenerateShortCode _genShortCode;

        /// <summary>
        /// �����ѡ
        /// </summary>
        public bool MultiSelect
        {
            get { return _multiSelect; }
            set
            {
                if (_multiSelect != value)
                {
                    _multiSelect = value;
                    // �����Ƿ��ѡ���ý���

                    if (value)
                        panelMultiSelect.Visible = true;
                    else
                        panelMultiSelect.Visible = false;
                }
            }
        }
        private bool _multiSelect;

        /// <summary>
        /// ����ѡ������ټ�¼��,��СΪ0
        /// </summary>
        public int MinCount
        {
            get { return _minCount; }
            set
            {
                if (value >= 0)
                    _minCount = value;
                else
                    _minCount = 0;
            }
        }
        private int _minCount;

        /// <summary>
        /// ����ѡ�������¼��
        /// </summary>
        public int MaxCount
        {
            get { return _maxCount; }
            set
            {
                if (value < MinCount)
                    _maxCount = MinCount;
                else
                    _maxCount = value;

                panelMultiSelect.Visible = (_maxCount > 1);
            }
        }
        private int _maxCount;

        /// <summary>
        /// ��ȡ��������Ŀ�ȣ�������Ӱ���ֵĿ�ȣ�
        /// </summary>
        private int InputRegionWidth
        {
            get { return panelTop.Width + m_ShadowOffset; }
        }

        /// <summary>
        /// ������Ч����ĳߴ磨����Ӱ���֣�
        /// </summary>
        private Size ValiableRegionSize
        {
            get
            {
                int wMax = Math.Max(DataRegionWidth, InputRegionWidth);
                // ���ڳߴ�Ҫ�������ܵ���Ӱ���ֿ��
                return new Size(wMax + m_ShadowOffset * 2
                   , panelTop.Height + panelData.Height + panelBottom.Height + m_ShadowOffset * 2);
            }
        }

        //private EditorButton MenuButton
        //{
        //   get { return textInputor.Properties.Buttons[0]; }
        //}

        /// <summary>
        /// ����ѡ������Ŀ�ȣ�grid+��ѡ�����,������Ӱ
        /// </summary>
        private int DataRegionWidth
        {
            get
            {
                int regionWidth = SystemInformation.VerticalScrollBarWidth + 4; // Ĭ�ϱ����������Ŀ��
                // ���Grid��������(�����п�ȵļ���)
                foreach (GridColumn col in gridViewbook.Columns)
                    regionWidth += col.Width;
                // ��֧�ֶ�ѡ�����Ի�Ҫ���Ǽ��϶�ѡ�������Ŀ��
                if (_maxCount > 1)
                    regionWidth += panelMultiSelect.Width;

                int base2Right = m_ScreenSize.Width - m_InitPosition.X; //����ؼ���߽絽�ұ���Ļ�ľ���
                int base2Left = m_InitPosition.X + panelTop.Width; // ����ؼ��ұ߽絽�����Ļ�ľ���

                // ��������Ŀ��Ҫ������������
                if (regionWidth > Math.Max(base2Left, base2Right))
                    regionWidth = Math.Max(base2Left, base2Right);
                return regionWidth;
            }
        }
        #endregion

        #region �Ϳؼ����֡��ߴ��йص�˽�б���
        /// <summary>
        /// ShowListForm ��ʾʱ�ĳ�ʼλ�ã���Ļ���꣩
        /// </summary>
        private Point m_InitPosition;
        /// <summary>
        /// ShowListForm �����ĳߴ�
        /// </summary>
        private Size m_InputControlSize;
        /// <summary>
        /// ����ShowListForm�Ĵ���������Ļ�ĳߴ�
        /// </summary>
        private Rectangle m_ScreenSize;
        /// <summary>
        /// ��Ǵ����Ƿ���������пؼ�
        /// </summary>
        private bool m_Left2Right;
        /// <summary>
        /// ��Ǵ����Ƿ���ϵ������пؼ�
        /// </summary>
        private bool m_Top2Down;
        /// <summary>
        /// ��Ӱ���ֵ�ƫ����
        /// </summary>
        private int m_ShadowOffset;
        /// <summary>
        /// ����ѡ�񲿷ֵ�Ĭ�ϸ߶ȣ�������ť�����Ĭ�ϸ߶ȣ�
        /// </summary>
        private int m_DefDataRegionHeight;
        /// <summary>
        /// ��ť�����Ĭ�ϸ߶�
        /// </summary>
        private int m_DefBottomRegionHeight;
        #endregion

        #region //popupmenu
        //private BarDockControl barDockControlTop;
        //private BarDockControl barDockControlBottom;
        //private BarDockControl barDockControlLeft;
        //private BarDockControl barDockControlRight;
        //private BarCheckItem barItemIsDynamic;
        //private BarCheckItem barItemMatchFull;
        //private BarCheckItem barItemMatchBegin;
        //private BarCheckItem barItemMatchAny;
        //private BarManagerCategory[] barManagerCategory;
        //private PopupMenu popupSettingMenu;

        //private void InitializeBasicPopupmenuComponets()
        //{
        //   barItemIsDynamic = new BarCheckItem();
        //   barItemMatchFull = new BarCheckItem();
        //   barItemMatchBegin = new BarCheckItem();
        //   barItemMatchAny = new BarCheckItem();
        //   popupSettingMenu = new PopupMenu();
        //   barManagerCategory = new BarManagerCategory[] {
        //      new BarManagerCategory("��ѯ��ʽ", new Guid("4c9d2035-2e11-479e-a0c5-4fc93d06733d")),
        //      new BarManagerCategory("ƥ�䷽ʽ", new Guid("07c5978d-2571-4c99-878e-6fd268d262d2"))};

        //   barItemIsDynamic.Caption = "��̬";
        //   barItemIsDynamic.CategoryGuid = new Guid("4c9d2035-2e11-479e-a0c5-4fc93d06733d");
        //   barItemIsDynamic.Checked = true;
        //   barItemIsDynamic.Id = 0;
        //   barItemIsDynamic.Name = "barItemIsDynamic";
        //   barItemIsDynamic.CheckedChanged += new ItemClickEventHandler(barItemIsDynamic_CheckedChanged);

        //   barItemMatchFull.Caption = "��ȫƥ��";
        //   barItemMatchFull.CategoryGuid = new Guid("07c5978d-2571-4c99-878e-6fd268d262d2");
        //   barItemMatchFull.GroupIndex = 1;
        //   barItemMatchFull.Id = 1;
        //   barItemMatchFull.Name = "barItemMatchFull";
        //   barItemMatchFull.CheckedChanged += new ItemClickEventHandler(barItemMatchFull_CheckedChanged);

        //   barItemMatchBegin.Caption = "ǰ������";
        //   barItemMatchBegin.CategoryGuid = new Guid("07c5978d-2571-4c99-878e-6fd268d262d2");
        //   barItemMatchBegin.Checked = true;
        //   barItemMatchBegin.GroupIndex = 1;
        //   barItemMatchBegin.Id = 2;
        //   barItemMatchBegin.Name = "barItemMatchBegin";
        //   barItemMatchBegin.CheckedChanged += new ItemClickEventHandler(barItemMatchBegin_CheckedChanged);

        //   barItemMatchAny.Caption = "���ְ���";
        //   barItemMatchAny.CategoryGuid = new System.Guid("07c5978d-2571-4c99-878e-6fd268d262d2");
        //   barItemMatchAny.GroupIndex = 1;
        //   barItemMatchAny.Id = 6;
        //   barItemMatchAny.Name = "barItemMatchAny";
        //   barItemMatchAny.CheckedChanged += new ItemClickEventHandler(barItemMatchAny_CheckedChanged);

        //   popupSettingMenu.Name = "popupSettingMenu";
        //}

        //private void InitializePopupMenuBeforCall(BarManager newBarManager)
        //{
        //   newBarManager.BeginInit();

        //   newBarManager.Categories.AddRange(barManagerCategory);
        //   newBarManager.Form = this;
        //   newBarManager.Items.AddRange(new BarItem[] {
        //      barItemIsDynamic,
        //      barItemMatchFull,
        //      barItemMatchBegin,
        //      barItemMatchAny});
        //   newBarManager.MaxItemId = 7;

        //   popupSettingMenu.BeginInit();
        //   popupSettingMenu.ClearLinks();
        //   popupSettingMenu.Manager = newBarManager;
        //   popupSettingMenu.LinksPersistInfo.AddRange(new LinkPersistInfo[] {
        //      new LinkPersistInfo(BarLinkUserDefines.PaintStyle, barItemIsDynamic, BarItemPaintStyle.CaptionGlyph),
        //      new LinkPersistInfo(barItemMatchFull, true),
        //      new LinkPersistInfo(barItemMatchBegin),
        //      new LinkPersistInfo(barItemMatchAny)});
        //   popupSettingMenu.EndInit();

        //   newBarManager.ForceLinkCreate();
        //   newBarManager.EndInit();
        //}
        #endregion

        #region private variables
        /// <summary>
        /// ��������ʹ�ù����ֵ�����
        /// </summary>
        private DataSet m_CacheDataSet;
        /// <summary>
        /// ��ǰʹ�õ��ֵ��DataTable
        /// </summary>
        private DataTable m_DefaultTable;
        /// <summary>
        /// ��ǰDataTable��Ĭ��DataView
        /// </summary>
        private DataView m_DefaultView;
        private string m_DefaultRowFilter;
        /// <summary>
        /// ���������е��ı��ı���Ƿ���й���ѯ���Ծ����س����¼���δ���
        /// </summary>
        private bool m_HaveSearch;
        /// <summary>
        /// �������ֵ������н��в�ѯ���ı�
        /// </summary>
        private string m_SearchText;

        /// <summary>
        /// ����DataGridView��ѡ�е��е���ţ������ѡʱ��Ҫ��
        /// </summary>
        private int m_SelectedRowIndex;
        /// <summary>
        /// ��Ҫ��Grid��ʾʱ��λ���к�
        /// </summary>
        private int m_LocateIndex;
        /// <summary>
        /// ����Ƿ��ڳ�ʼ��״̬
        /// </summary>
        private bool m_Initializing;
        private bool m_ParentAutoClose;
        private bool m_NeedSynchMatchType; // ����Ƿ���Ҫͬ��ƥ��ģʽ��������
        #endregion

        #region ctor
        public DataSelectForm()
        {
            InitializeComponent();
            InitializeProperties();
            InitializePrivateVariable();
        }
        #endregion

        #region public methods
        /// <summary>
        /// ���ô���ѡ�񴰿ڡ�
        /// ��ʼ��ShowList���ڵĲ������ԡ���Щ������Ϊ����Ϊ����ʾЧ�����йأ�������Ҫͳһ���á�
        /// <param name="wordbook">Ĭ�ϵ��ֵ���</param>
        /// <param name="kind">�ֵ��������</param>
        /// <param name="initText">��ѯ������ʼֵ</param>
        /// <param name="formMode">ShowList������ʾģʽ</param>
        /// <param name="initPosition">ShowListFormĬ����ʾλ��(��Ļ����)</param>
        /// <param name="inputSize">ShowListForm�����ĳߴ�</param>
        /// <param name="screenSize">����ShowListForm�Ĵ���������Ļ�ĳߴ�</param>
        /// <param name="callType">����ģʽ</param>
        /// </summary>
        public void CallShowListWindow(BaseWordbook wordbook, WordbookKind kind, string initText, ShowListFormMode formMode, Point initPosition, Size inputSize, Rectangle screenSize, ShowListCallType callType)
        {
            //StringBuilder timeLog = new StringBuilder();
            //timeLog.AppendLine(wordbook.Caption + " " + initText);
            //timeLog.AppendLine(String.Format("{0,20} {1}", "begin", DateTime.Now.ToString("mm:ss fff")));

            // ��ʼ��ǰҪ����ϴεĲ�ѯ���
            ClearLastData();
            m_Initializing = true;

            // ��ʼ��ʱ���û�д����ʼ������ֱ���˳�
            if ((callType == ShowListCallType.Initialize) && (String.IsNullOrEmpty(initText)))
                return;

            if (wordbook == null)
                throw new ArgumentNullException("δ��ʼ���ֵ���");

            if (m_NeedSynchMatchType)
            {
                m_NeedSynchMatchType = false;
                SynchMatchTypeToControl();
            }

            _wordbook = wordbook;
            _bookKind = kind;

            // ShowList���ڵ���ʾģʽ(ʹ��List�ֵ�ʱ��Ĭ��ʹ�ü�ര��)
            if (_bookKind == WordbookKind.List)
                FormMode = ShowListFormMode.Concision;
            else if (_maxCount > 1)
                FormMode = ShowListFormMode.Full;
            else
                FormMode = formMode;

            // ���ȼ�����������Ƿ���ȷ
            string errMsgs = CheckFormProperties(true);
            if (errMsgs.Length > 0)
            {
                CancleSelectAction();
                throw new ArgumentException(errMsgs);
            }

            //timeLog.AppendLine(String.Format("{0,20} {1}", "begininitidata", DateTime.Now.ToString("mm:ss fff")));

            // ��ʼ���ֵ�����
            InitializeWordbookData();

            //timeLog.AppendLine(String.Format("{0,20} {1}", "endinitdata", DateTime.Now.ToString("mm:ss fff")));

            m_InitPosition = initPosition;
            m_InputControlSize = inputSize;
            m_ScreenSize = screenSize;
            m_SearchText = initText.Trim();

            // �ȳ�ʼ��DataGridView����Ϊ���ڵĿ����DataGrid���еĿ���йأ�
            InitializeDataGridView();

            //timeLog.AppendLine(String.Format("{0,20} {1}", "dosearch", DateTime.Now.ToString("mm:ss fff")));

            if (_maxCount > 1)
                DoMultiSelectInitialize(m_SearchText, callType);
            else
                DoSingleSelectInitialize(m_SearchText, callType);

            //timeLog.AppendLine(String.Format("{0,20} {1}", "end", DateTime.Now.ToString("mm:ss fff")));
            //Trace.WriteLine(timeLog.ToString());
        }

        public string[] ValidateWordbookHasOneRecord(BaseWordbook wordbook, WordbookKind kind)
        {
            if (wordbook == null)
                return null;

            _wordbook = wordbook;
            _bookKind = kind;
            InitializeWordbookData();
            if (m_DefaultView.Count == 1)
                return new string[] { m_DefaultView[0][_wordbook.CodeField].ToString()
               , m_DefaultView[0][_wordbook.NameField].ToString()};
            else
                return null;
        }
        #endregion

        #region private common methods
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitializeProperties()
        {
            _showColumnTitle = true;
            _showGridline = true;
            //_useWb = false;
            _isDynamic = true;
            panelSetting.Hide();
            _matchType = ShowListMatchType.Any;
            _formMode = ShowListFormMode.Full;
            ShowShadow = false;
            //_showFormButton = false;
            _bookKind = WordbookKind.Normal;

            _displayValue = "";
            _codeValue = "";
            _queryValue = "";
            _sqlHelper = null;
            _genShortCode = null;
            MinCount = 0;
            MaxCount = 1;

            BackColor = ColorFormBack;
            panelTop.BackColor = ColorChildBack;
            panelData.BackColor = ColorChildBack;
            panelTop.ForeColor = ColorChildFore;
            panelData.ForeColor = ColorChildFore;
            panelTopShadow.BackColor = ColorShadowBack;
            panelDataShadow.BackColor = ColorShadowBack;

            textInputor.ContextMenu = new ContextMenu();
            textInputor.MouseWheel += new MouseEventHandler(inputBox_MouseWheel);

            //InitializeBasicPopupmenuComponets();
        }

        /// <summary>
        /// ��ʼ��˽�б���
        /// </summary>
        private void InitializePrivateVariable()
        {
            m_CacheDataSet = new DataSet();
            m_CacheDataSet.Locale = CultureInfo.CurrentCulture;
            m_HaveSearch = false;
            m_SearchText = "";


            //m_MatchStringFormat = "{0} like '{1}%'";
            //m_MatchNumericFormat = "{0} = {1}";
            m_SelectedRowIndex = -1;
            m_Left2Right = true;
            m_Top2Down = true;
            m_DefDataRegionHeight = 238;
            m_DefBottomRegionHeight = 30;
            m_LocateIndex = -1;
            m_NeedSynchMatchType = false;
        }

        /// <summary>
        /// ��鴰�ڵ����������Ƿ���ȷ
        /// </summary>
        /// <param name="fullCheck">�Ƿ������������־</param>
        /// <returns>���ش�����Ϣ��Ϊ�ձ�ʾû�д���</returns>
        private string CheckFormProperties(bool fullCheck)
        {
            StringBuilder msgs = new StringBuilder();

            if (_wordbook == null)
            {
                msgs.Append("δָ���ֵ���\r\n");
            }
            if ((_wordbook is ListWordbook) && (_bookKind != WordbookKind.List))
            {
                msgs.Append("�ֵ����ʵ���������������Բ�ƥ��\r\n");
            }
            if ((_wordbook is SqlWordbook) && (_bookKind != WordbookKind.Sql))
            {
                msgs.Append("�ֵ����ʵ���������������Բ�ƥ��\r\n");
            }
            if (!fullCheck)
                return msgs.ToString();

            if ((_bookKind == WordbookKind.List) && (_formMode != ShowListFormMode.Concision))
            {
                msgs.Append("ʹ��List���ֵ�ʱû��Ӧ�ü��ģʽ����");
            }

            return msgs.ToString();
        }

        /// <summary>
        /// ��ʼ��DataGridView
        /// </summary>
        private void InitializeDataGridView()
        {
            gridControl1.BeginUpdate();
            gridViewbook.BeginUpdate();
            // Grid�Ƿ���ʾLine����
            gridViewbook.OptionsView.ShowVertLines = _showGridline;
            gridViewbook.OptionsView.ShowVertLines = _showGridline;

            // ��������Դ
            gridControl1.DataSource = null;
            // �����У�������ε���һ����������
            GridColumn[] newCols = _wordbook.GenerateDevGridColumnCollection();
            bool containsAll = (gridViewbook.Columns.Count == newCols.Length);
            if (containsAll)
            {
                foreach (GridColumn col in newCols)
                {
                    if (!gridViewbook.Columns.Contains(col))
                    {
                        containsAll = false;
                        break;
                    }
                }
            }
            if (!containsAll)
            {
                gridViewbook.Columns.Clear();
                ////gridViewbook.Columns.AddRange(_wordbook.GenerateDevGridColumnCollection());

            }

            gridControl1.DataSource = m_DefaultTable;
            //// ֱ�Ӷ�λ��ָ����
            //if (m_LocateIndex > -1)
            //{
            //   gridViewbook.FocusedRowHandle = m_LocateIndex;
            //   // ���þ۽����к�GridViewû���Զ�����ԭ������������������������ķ������۽�������ʾ����Ļ��
            //   gridViewbook.TopRowIndex = m_LocateIndex - 3;
            //   m_LocateIndex = -1;
            //}
            gridViewbook.EndUpdate();
            gridControl1.EndUpdate();
        }
        #endregion

        #region private methods of handle select
        private void DoMultiSelectInitialize(string searchText, ShowListCallType callType)
        {
            // ����Ƕ�ѡ
            //    �ȷֽ⴫����ַ�����ƥ�䵽��Ӧ��¼����ӵ�List��
            //    ���ǳ�ʼ��ShowListBoxʱ��������ʾForm������ǿ����ʾForm
            string[] texts = SplitSearchText(searchText);
            foreach (string t in texts)
            {
                if (String.IsNullOrEmpty(t))
                    continue;

                if (SearchMatchRow(t, ShowListMatchType.Full, (callType == ShowListCallType.Initialize), true) > 0)
                {
                    if (m_LocateIndex >= 0)
                        DoAfterRowHadBeenSelected(); // ��ѡʱ�Ĵ����ǲ�ͬ��
                }
            }
            if (callType == ShowListCallType.Initialize)
                CommitSelectAction();
            else
            {
                textInputor.Text = "";
                //inputBox_TextChanged(this, new EventArgs());
                m_Initializing = false;
                gridViewbook.MoveFirst();
                if (gridViewbook.FocusedRowHandle >= 0)
                    m_SelectedRowIndex = gridViewbook.FocusedRowHandle;
                else
                    m_SelectedRowIndex = -1;
                //if (Visible)
                //   Visible = false;
                //try
                {
                    ShowDialog();
                }
                //catch
                //{
                //   Show();
                //}
            }
        }

        private string[] SplitSearchText(string searchText)
        {
            if (String.IsNullOrEmpty(searchText))
                return new string[] { };

            if (searchText.Contains("��"))
                searchText = searchText.Replace("��", ","); // ȫ�ǵĶ���Ҫ�滻�ɰ�ǵ�

            string[] separators = MultiStringSeparators;
            return searchText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        private void DoSingleSelectInitialize(string searchText, ShowListCallType callType)
        {
            // ����ǵ�ѡ
            //    ��ʼ��ʱ���ã������ƥ���¼������ʾForm
            //    ǿ����ʾFormʱ��ֻ��λ��¼��������������
            //    ��������£��������ݣ����ֻ��һ��ƥ�䣬����ʾForm
            if (callType == ShowListCallType.Initialize)
            {
                // ��ѡ�ĳ�ʼ�����̣�ֱ��ƥ���¼������ж��Ƿ��н��
                if (SearchMatchRow(searchText, ShowListMatchType.Full, true) > 0)
                    DoAfterRowHadBeenSelected();
            }
            else
            {
                int rowCount;
                if (callType == ShowListCallType.Normal)
                    rowCount = SearchMatchRow(searchText, MatchType, false);
                else// ǿ����ʾ����ʱ��ֻ���ҡ�����������
                    rowCount = SearchMatchRow(searchText, MatchType, false, true);

                if ((!AlwaysShowWindow) && (rowCount == 1))
                {
                    DoAfterRowHadBeenSelected();
                }
                else
                {
                    textInputor.TextChanged -= new EventHandler(inputBox_TextChanged);
                    //if (rowCount > 0)
                    //   textInputor.Text = "";
                    //else
                    textInputor.Text = searchText.Trim();
                    textInputor.TextChanged += new EventHandler(inputBox_TextChanged);
                    m_Initializing = false;
                    //if (Visible)
                    //   Visible = false;
                    //try
                    {
                        ShowDialog();
                    }
                    //catch
                    //{
                    //   Show();
                    //}
                }
            }
        }

        /// <summary>
        /// ����ϴβ�ѯ���
        /// </summary>
        private void ClearLastData()
        {
            m_HaveSearch = false;
            //m_SelectedRowIndexes.Clear();
            if (_resultRows == null)
                _resultRows = new List<DataRow>();
            else
                _resultRows.Clear();
            _codeValue = "";
            _displayValue = "";
            _queryValue = "";
            m_SearchText = "";
            lbSelectedRecords.Items.Clear();
        }

        /// <summary>
        /// ��ʼ������Ҫʹ�õ��ֵ�����
        /// </summary>
        private void InitializeWordbookData()
        {
            // --���������ݻ����м��, û�еĻ��ٴ����ݿ��ȡ
            // ���ݻ��������Ƶ�����У��ɿ�ܾ������ȡ����
            bool cached = (_wordbook.CacheTime != -1); // �����ֵ�Ļ���ʱ�����þ����Ƿ�ʹ�û�������
            m_DefaultRowFilter = _wordbook.GenerateFilterExpression();

            switch (_bookKind)
            {
                case WordbookKind.Normal:
                    if (cached)
                    {
                        m_DefaultTable = _sqlHelper.ExecuteDataTable(_wordbook.QuerySentence
                           , m_DefaultRowFilter);
                        m_DefaultRowFilter = "";
                    }
                    else
                    {
                        m_DefaultTable = _sqlHelper.ExecuteDataTable(_wordbook.QuerySentence
                           , cached, CommandType.Text);
                    }
                    break;
                case WordbookKind.List:
                    // ����List���͵��ֵ䣬��ͨ�����ȡ���ݣ���Ȼʹ���Լ��Ļ���
                    if (m_CacheDataSet.Tables.Contains(_wordbook.WordbookName))
                        m_DefaultTable = m_CacheDataSet.Tables[_wordbook.WordbookName];
                    else
                        m_DefaultTable = ConvertStringList2DataTable(_wordbook.WordbookName
                                                 , (_wordbook as ListWordbook).Items);
                    break;
                case WordbookKind.Sql:
                    SqlWordbook sqlBook = _wordbook as SqlWordbook;
                    sqlBook.EnsureBookData(SqlHelper, GenShortCode);
                    m_DefaultTable = sqlBook.BookData;
                    //// �ж��Ƿ���Ҫʹ��Sql��䴴�����ݼ�
                    //if (sqlBook.UseSqlStatement)
                    //   m_DefaultTable = _sqlHelper.ExecuteDataTable(_wordbook.QuerySentence
                    //      , cached, CommandType.Text);
                    //else
                    //   m_DefaultTable = ResetSqlbookData(sqlBook);
                    break;
                default:
                    throw new ArgumentException("δ������ֵ�����");
            }

            m_DefaultView = m_DefaultTable.DefaultView;
            m_DefaultView.RowFilter = m_DefaultRowFilter;// _wordbook.GenerateFilterExpression(); // ȡ�ֵ��Ĭ�Ϲ�������
        }

        /// <summary>
        /// ���ݴ����StringList����DataTable���Զ������š�ƴ���������
        /// </summary>
        /// <param name="tableName">ָ����DataTable����</param>
        /// <param name="source">��������DataTable��StringList</param>
        /// <returns></returns>
        private DataTable ConvertStringList2DataTable(string tableName, Collection<string> source)
        {
            DataTable newTable = new DataTable(tableName);
            newTable.Locale = CultureInfo.CurrentCulture;
            newTable.Columns.AddRange(new DataColumn[] { 
              new DataColumn("xh", Type.GetType("System.Int32"))
            , new DataColumn("name", Type.GetType("System.String"))
            , new DataColumn("py", Type.GetType("System.String"))
            , new DataColumn("wb", Type.GetType("System.String")) });

            DataRow newRow;
            string[] shortCode;
            for (int i = 0; i < source.Count; i++)
            {
                newRow = newTable.NewRow();
                newRow["xh"] = i;
                newRow["name"] = source[i];

                shortCode = GenShortCode.GenerateStringShortCode(source[i]);
                newRow["py"] = shortCode[0];
                newRow["wb"] = shortCode[1];
                newTable.Rows.Add(newRow);
            }

            return newTable;
        }

        /// <summary>
        /// ���ݵ�ǰ�Ĳ�ѯ�ı��������ݣ����������ݣ�
        /// </summary>
        /// <param name="searchText">��ѯ���ݵ�����ֵ</param>
        /// <param name="matchType">����ƥ��ģʽ</param>
        /// <param name="useCodeFieldOnly">�Ƿ�ֱ��ʹ��Ĭ�ϵĴ����н���ƥ��</param>
        /// <returns>����ƥ��ļ�¼����</returns>
        private int SearchMatchRow(string searchText, ShowListMatchType matchType, bool useCodeFieldOnly)
        {
            int rowCount = SearchMatchRow(searchText, matchType, useCodeFieldOnly, false);
            gridViewbook.MoveFirst(); // 
            return rowCount;
        }

        /// <summary>
        /// ���ݵ�ǰ�Ĳ�ѯ�ı��������ݣ����������ݣ�
        /// </summary>
        /// <param name="searchText">��ѯ���ݵ�����ֵ</param>
        /// <param name="matchType">����ƥ��ģʽ</param>
        /// <param name="useCodeFieldOnly">�Ƿ�ֱ��ʹ��Ĭ�ϵĴ����н���ƥ��</param>
        /// <param name="findOnly">�Ƿ�ֻ�������ݣ�������</param>
        /// <returns>����ƥ��ļ�¼����</returns>
        private int SearchMatchRow(string searchText, ShowListMatchType matchType, bool useCodeFieldOnly, bool findOnly)
        {
            m_HaveSearch = true;
            m_LocateIndex = -1;

            // ���ȸ����������ݵ�����������ѡ�Ĳ�ѯ����
            StringType inputType = CommonOperation.GetStringType(searchText);
            if (inputType == StringType.Numeric)
            {
                StringBuilder newString = new StringBuilder();
                for (int index = 0; index < searchText.Length; index++)
                {
                    if (searchText[index] >= 65296)
                        newString.Append((char)(searchText[index] - 65248));
                    else
                        newString.Append(searchText[index]);
                }
                searchText = newString.ToString();
            }

            string firstField;

            if (useCodeFieldOnly)
                firstField = _wordbook.CodeField;
            else
                firstField = GetFirstMatchField(inputType);

            if (findOnly)
                m_DefaultView.RowFilter = m_DefaultRowFilter;

            // �������ѡ����û���ҵ��������δ��������н��в��ҡ�
            int i = -1;
            string field; // ���浱ǰ��ѯ�����ı���
            //string defaultExpression = _wordbook.GenerateFilterExpression(); // �ֵ���Ĭ�ϵĹ����������ʽ
            string expression; // ������ݲ�ѯ���ݺͲ�ѯ�����ɵĲ�ѯ�������ʽ
            do
            {
                i++;

                if (i == 0)
                    field = firstField;
                else
                {
                    field = _wordbook.CurrentMatchFields[i - 1];
                    if (field == firstField)
                        continue;
                }

                if (findOnly)
                {
                    //m_DefaultView.RowFilter = defaultExpression;
                    // ��ʼ����Ϊ�գ�����Ҫ����
                    if (searchText.Length == 0)
                        break;
                    // ����ֶε���������ֵ�ͣ����ѯ�ı�������Ҳ��������ֵ�ͣ�������Find
                    if ((m_DefaultTable.Columns[field].DataType != typeof(string))
                       && (inputType != StringType.Numeric))
                        break;

                    m_DefaultView.Sort = field;
                    m_LocateIndex = m_DefaultView.Find(searchText);
                    if (m_LocateIndex >= 0)
                        break;
                }
                else
                {
                    if (inputType == StringType.Empty)
                        expression = "";
                    else if (m_DefaultTable.Columns[field].DataType == typeof(string))
                        expression = String.Format(CultureInfo.CurrentCulture
                           , GetMatchStringFormatByMatchType(matchType), field
                           , CommonOperation.TransferCondition(searchText, GetFilterOperatorByMatchType(matchType), true));
                    else if (inputType == StringType.Numeric)
                        expression = String.Format(CultureInfo.CurrentCulture
                           , GetMatchNumericFormatByMatchType(matchType), field, searchText);
                    else
                        expression = " 1 = 2 ";

                    if (m_DefaultRowFilter.Length == 0)
                        m_DefaultView.RowFilter = expression;
                    else if (expression.Length == 0)
                        m_DefaultView.RowFilter = m_DefaultRowFilter;
                    else
                        m_DefaultView.RowFilter = m_DefaultRowFilter + " AND " + expression;

                    // �ҵ���¼���˳��������ֻʹ�ô����н��в��ң������Ƿ��ҵ���¼���˳���
                    if ((m_DefaultView.Count > 0) || (useCodeFieldOnly))
                        break;
                }
            }
            while (i < _wordbook.CurrentMatchFields.Count);

            // ÿ�ι������ݺ��ʼ��ѡ�е��м���
            if (findOnly)
            {
                m_SelectedRowIndex = -1;

                // ֻ��λ���ܶ�λ����¼ʱ�Ž��ü�¼���浽��ѡ�м����У����򱣴��һ�У�������ڵĻ���
                if (m_LocateIndex >= 0)
                    m_SelectedRowIndex = m_LocateIndex;
                else if (m_DefaultView.Count > 0)
                    m_SelectedRowIndex = 0;
            }

            return m_DefaultView.Count;
        }

        private string GetMatchNumericFormatByMatchType(ShowListMatchType matchType)
        {
            switch (matchType)
            {
                case ShowListMatchType.Begin:
                    return "{0} >= {1}";
                case ShowListMatchType.Full:
                    return "{0} = {1}";
                default:
                    return "{0} >= {1}";
            }
        }

        private string GetMatchStringFormatByMatchType(ShowListMatchType matchType)
        {
            switch (matchType)
            {
                case ShowListMatchType.Begin:
                    return "{0} like '{1}%'";
                case ShowListMatchType.Full:
                    return "{0} = '{1}'";
                default:
                    return "{0} like '%{1}%'";
            }
        }

        /// <summary>
        /// ȡ�õ�ǰ����ʹ�õĲ�����
        /// </summary>
        private CompareOperator GetFilterOperatorByMatchType(ShowListMatchType matchType)
        {
            if (matchType == ShowListMatchType.Full)
                return CompareOperator.Equal;
            else
                return CompareOperator.Like;
        }

        /// <summary>
        /// �����ַ������ͣ���ȡ��ѡ�Ĳ�ѯ�ֶ���
        /// </summary>
        /// <param name="inputType">�ַ�������</param>
        /// <returns></returns>
        private string GetFirstMatchField(StringType inputType)
        {
            string field = "";
            switch (inputType)
            {
                case StringType.EnglishChar: // ȫӢ����ĸ���ַ���Ĭ����py/wb�ֶ�ƥ��
                    if (_wordbook.CurrentMatchFields.Contains("py") && (!_useWB))
                        field = "py";
                    else if (_wordbook.CurrentMatchFields.Contains("wb") && _useWB)
                        field = "wb";
                    break;
                case StringType.Char: // ASCII�ַ�
                case StringType.Other: // �����ַ�����Ĭ���������ֶ�ƥ��
                    //if (_wordbook.CurrentFilterFields.Contains("name"))
                    field = _wordbook.NameField;
                    break;
                case StringType.Empty: // �մ�ʱ��ʹ��Ĭ�ϵĵ�һ���ֶ�
                    field = _wordbook.CurrentMatchFields[0];
                    break;
                // ����/�����ֵ��ַ����������Ǵ����ֶΣ����������ǣ���Ĭ��ȡ��py/wb/name�ĵ�һ���ֶ�
            }
            if (field.Length == 0)
            {
                foreach (string colName in _wordbook.CurrentMatchFields)
                {
                    if ((colName == "py") || (colName == "wb") || (colName == "name"))
                        continue;
                    field = colName;
                    break;
                }
            }
            if (String.IsNullOrEmpty(field))
                field = _wordbook.CurrentMatchFields[0];
            return field;
        }

        /// <summary>
        /// ��ѡ�м�¼��������ݴ�����
        /// </summary>
        private void SaveSelectedRow()
        {
            if ((m_DefaultView.Count == 0) || (ResultRows.Count == MaxCount))
                return;

            // ����ѡ�е���.���ݼ���ֻ��һ��ƥ���¼ʱֱ�Ӵ������ݼ�
            DataRow selectedRow;
            if (m_DefaultView.Count == 1)
            {
                selectedRow = m_DefaultView[0].Row;
            }
            else
            {
                // �����Grid�������������ʱ��RowHandle����ʵ�ʵ������кţ�����Ҫͨ��GetDataRow�������õ�ʵ�ʵ�������
                // ���Ǵ�����ʾǰ����Ϊ����г�ʼ����������ʱ��Ȼ���������ݼ������ǻ�û�и���Grid������Դ�����Բ�����GetDataRow����
                // ��������ԭ���������Ƿ��ڳ�ʼ����־
                if (m_Initializing)
                    selectedRow = m_DefaultView[m_SelectedRowIndex].Row;
                else
                    selectedRow = gridViewbook.GetDataRow(m_SelectedRowIndex);
            }

            // ���ѡ�еļ�¼�Ƿ��Ѿ�����
            foreach (DataRow row in ResultRows)
            {
                if (row[Wordbook.QueryCodeField].ToString() == selectedRow[Wordbook.QueryCodeField].ToString())
                    return;
            }

            ResultRows.Add(CloneDataRow(selectedRow));

            if (_maxCount > 1)
                lbSelectedRecords.Items.Add(ResultRows[ResultRows.Count - 1][Wordbook.NameField]);
        }

        /// <summary>
        /// �ô����DataRow����һ����¼
        /// </summary>
        /// <param name="sourceRow"></param>
        /// <returns></returns>
        private static DataRow CloneDataRow(DataRow sourceRow)
        {
            DataRow cloneRow = sourceRow.Table.NewRow();
            for (int i = 0; i < sourceRow.Table.Columns.Count; i++)
                cloneRow[i] = sourceRow[i];
            return cloneRow;
        }

        /// <summary>
        /// �������ѡ�����
        /// </summary>
        private void CommitSelectAction()
        {
            if (ResultRows.Count > 0)
            {
                string queryCodeField;
                if (String.IsNullOrEmpty(Wordbook.QueryCodeField))
                    queryCodeField = Wordbook.CodeField;
                else
                    queryCodeField = Wordbook.QueryCodeField;

                // ����DisplayValue��CodeValue
                StringBuilder tempCode = new StringBuilder(ResultRows[0][Wordbook.CodeField].ToString().TrimEnd());
                StringBuilder tempDisplay = new StringBuilder(ResultRows[0][Wordbook.NameField].ToString().TrimEnd());
                StringBuilder tempQueryCode = new StringBuilder(ResultRows[0][queryCodeField].ToString().TrimEnd());
                for (int i = 1; i < ResultRows.Count; i++)
                {
                    tempCode.Append(MultiStringSeparators[1]);
                    tempCode.Append(ResultRows[i][Wordbook.CodeField].ToString().TrimEnd());
                    tempDisplay.Append(MultiStringSeparators[0]);
                    tempDisplay.Append(ResultRows[i][Wordbook.NameField].ToString().TrimEnd());
                    tempQueryCode.Append(MultiStringSeparators[1]);
                    tempQueryCode.Append(ResultRows[i][queryCodeField].ToString().TrimEnd());
                }
                _codeValue = tempCode.ToString();
                _displayValue = tempDisplay.ToString();
                _queryValue = tempQueryCode.ToString();

                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// ȡ������ѡ����
        /// </summary>
        private void CancleSelectAction()
        {
            ClearLastData();
            DialogResult = DialogResult.Cancel;
            //Close();
        }

        /// <summary>
        /// ִ��ѡ���к�Ĳ���
        /// </summary>
        private void DoAfterRowHadBeenSelected()
        {
            if (m_SelectedRowIndex >= 0)
            {
                // �ڶ�ѡģʽ�£�������ͬһ����¼�ϰ��س�ʱ���˳��Ĵ���
                // ͨ���ȽϽ������¼���ĸı�������ж��Ƿ�����ͬһ����¼�ϰ��Ļس�
                int preResultCount = ResultRows.Count;

                SaveSelectedRow();

                if ((_maxCount == 1) // ����ǵ�ѡ�����������ύ
                   || (preResultCount == ResultRows.Count))
                    CommitSelectAction();
                else
                    textInputor.SelectAll();
            }
        }
        #endregion

        #region private UI methods
        /// <summary>
        /// ����ShowListForm����ʾ��ʽ
        /// </summary>
        private void SetFormStyle()
        {
            SuspendLayout();
            Opacity = 1;

            gridControl1.SuspendLayout();

            // ����Ҫȷ��Form����ؼ���λ�úͳߴ�
            SetFormAndControlsSize();
            SetFormAndControlsPosition();

            // ���ý����񣨼�ࡢ������
            // ��ര�ڣ������Grid����չ��ť��
            // �������ڣ�������ʾ��ѯ���á�ȷ�ϡ�ȡ����ť��         
            gridControl1.ResumeLayout(false);
            gridControl1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

            // ���ڳߴ���С����ʱ��û�д����ػ��¼��������ڴ�ǿ���ػ�
            mainForm_Paint(this, new PaintEventArgs(Graphics.FromHwnd(Handle), ClientRectangle));
        }

        ///// <summary>
        ///// ����BottomPanel�ĸ߶�(�øı�߶ȵķ�ʽ�ﵽ�ı�Panel�Ƿ���ӵ�Ч��)
        ///// </summary>
        ///// <param name="visible">�Ƿ����</param>
        //private void SetBottomPanelHeight(bool visible)
        //{
        //   if (visible)
        //      panelBottom.Height = m_DefBottomRegionHeight;
        //   else
        //      panelBottom.Height = 0;
        //}

        /// <summary>
        /// ���ô����пؼ��ĳߴ�
        /// </summary>
        private void SetFormAndControlsSize()
        {
            // ���Ƚ������ĳߴ�����ÿؼ�����ͬ��
            textInputor.Size = m_InputControlSize;

            panelTop.Size = new Size(m_InputControlSize.Width, m_InputControlSize.Height);

            // ��ѡʱ���������ֶεĿ�����ö�ѡ�б�Ŀ��
            if (_maxCount > 1)
            {
                int listWidth = 150;
                if (gridViewbook.Columns[Wordbook.NameField] != null)
                    listWidth = gridViewbook.Columns[Wordbook.NameField].Width;
                panelMultiSelect.Width = listWidth + panelSelRecord.Width
                   + SystemInformation.VerticalScrollBarWidth;
            }

            // �жϴ��ڲ������ͣ������Grid��������
            JudgeFormLayoutType();

            // �������ڼ����ݲ��ֵĳߴ�
            panelData.Size = new Size(DataRegionWidth, m_DefDataRegionHeight);
            Size = ValiableRegionSize;
        }

        private void SetFormAndControlsPosition()
        {
            // �����ʼλ�õ�������(-1, -1)�� ��ĳɾ�����ʾ
            if (m_InitPosition == new Point(-1, -1))
                m_InitPosition = new Point((m_ScreenSize.Width - Width) / 2
                   , (m_ScreenSize.Height - Height) / 2);

            // ���㴰�ڡ��������벿�֡�����ѡ�񲿷ֵ�����
            int pFX; // Form��X����
            int pFY; // Form��Y����
            int pTX; // ���������Panel��X����
            int pTY; // ���������Panel��Y����
            int pDX; // Grid����Panel�ĵ�X����
            int pDY; // Grid����Panel�ĵ�Y����

            // ���ô��ڲ���
            if (m_Left2Right)
            {
                pFX = m_InitPosition.X - m_ShadowOffset;
                pTX = m_ShadowOffset;
                pDX = m_ShadowOffset;
                panelSetting.Dock = DockStyle.Right;
            }
            else
            {
                pFX = m_InitPosition.X + m_InputControlSize.Width + m_ShadowOffset - Width;
                pTX = Width - panelTop.Width - m_ShadowOffset;
                pDX = Width - panelData.Width - m_ShadowOffset;
                panelSetting.Dock = DockStyle.Left;
            }

            if (m_Top2Down)
            {
                pFY = m_InitPosition.Y - m_ShadowOffset;
                pTY = m_ShadowOffset;
                pDY = m_ShadowOffset + panelTop.Height;
            }
            else
            {
                pFY = m_InitPosition.Y + panelTop.Height + m_ShadowOffset - Height;
                pTY = m_ShadowOffset + panelData.Height;
                pDY = m_ShadowOffset;
            }

            // ��ѡʱ�������Ҳ��֣��ı��ѡ�б��λ��
            if (_maxCount > 1)
                SetMultiSelectPanelPosition(m_Left2Right);

            Location = new Point(pFX, pFY);
            panelTop.Location = new Point(pTX, pTY);
            panelData.Location = new Point(pDX, pDY);
        }

        private void SetMultiSelectPanelPosition(bool left2Right)
        {
            if (left2Right)
            {
                panelMultiSelect.Dock = DockStyle.Right;
                panelSelRecord.Dock = DockStyle.Left;
                btnSelectAll.Text = ">>";
                btnSelectOne.Text = ">";
                btnDeleteAll.Text = "<<";
                btnDeleteOne.Text = "<";
            }
            else
            {
                panelMultiSelect.Dock = DockStyle.Left;
                panelSelRecord.Dock = DockStyle.Right;
                btnSelectAll.Text = "<<";
                btnSelectOne.Text = "<";
                btnDeleteAll.Text = ">>";
                btnDeleteOne.Text = ">";
            }
        }

        /// <summary>
        /// �жϴ��ڲ������ͣ������Grid��������
        /// </summary>
        private void JudgeFormLayoutType()
        {
            // 1���жϴ����Ǵ��ϵ��²��ֻ��Ǵ��µ��ϲ���
            if ((m_ScreenSize.Height - m_InitPosition.Y) >= ValiableRegionSize.Height)
                m_Top2Down = true;
            else
                m_Top2Down = false;

            int base2Right = m_ScreenSize.Width - m_InitPosition.X;

            // 2���жϴ����Ǵ�������ʾ���Ǵ��ҵ�����ʾ
            m_Left2Right = (base2Right >= Math.Max(DataRegionWidth, InputRegionWidth));
        }

        private void SynchDynamicToControl()
        {
            ckEditDynamic.Checked = IsDynamic;
            //barItemIsDynamic.Checked = _isDynamic;
        }

        private void SynchMatchTypeToControl()
        {
            switch (_matchType)
            {
                case ShowListMatchType.Begin:
                    if (!ckEditBegin.Checked)
                        ckEditBegin.Checked = true;
                    ckEditAny.Checked = false;
                    ckEditFull.Checked = false;
                    //if (!barItemMatchBegin.Checked)
                    //   barItemMatchBegin.Checked = true;
                    //barItemMatchFull.Checked = false;
                    //barItemMatchAny.Checked = false;
                    break;
                case ShowListMatchType.Full:
                    if (!ckEditFull.Checked)
                        ckEditFull.Checked = true;
                    ckEditAny.Checked = false;
                    ckEditBegin.Checked = false;
                    //if (!barItemMatchFull.Checked)
                    //   barItemMatchFull.Checked = true;
                    //barItemMatchBegin.Checked = false;
                    //barItemMatchAny.Checked = false;
                    break;
                default:
                    if (!ckEditAny.Checked)
                        ckEditAny.Checked = true;
                    ckEditBegin.Checked = false;
                    ckEditFull.Checked = false;
                    //if (!barItemMatchAny.Checked)
                    //   barItemMatchAny.Checked = true;
                    //barItemMatchFull.Checked = false;
                    //barItemMatchBegin.Checked = false;
                    break;
            }
        }

        private void ResetControlFont(Font font)
        {
            Font = font;
            //styleController1.Appearance.Font = font;
            //styleController1.AppearanceDisabled.Font = font;
            //styleController1.AppearanceDropDown.Font = font;
            //styleController1.AppearanceDropDownHeader.Font = font;
            //styleController1.AppearanceFocused.Font = font;

            textInputor.Font = font;
            btnOk.Font = font;
            btnCancel.Font = font;
            btnSelectAll.Font = font;
            btnSelectOne.Font = font;
            btnDeleteAll.Font = font;
            btnDeleteOne.Font = font;
            //lbSelectedRecords.Font = font;

            foreach (AppearanceObject ap in gridViewbook.Appearance)
                ap.Font = font;
        }

        private void ChangeFormOpacity(bool toTransparency)
        {
            if (toTransparency)
                Opacity = 0.2;
            else
                Opacity = 1;
        }
        #endregion

        #region events
        private void mainForm_Load(object sender, EventArgs e)
        {
            ImeMode = ImeMode.Off; // ǿ�ƹر����뷨
            // �����������ô�����ʽ
            SetFormStyle();
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            m_ParentAutoClose = false;
            if (Owner != null)
            {
                CaptureCursorForm cursorForm = Owner as CaptureCursorForm;
                if ((cursorForm != null) && (cursorForm.TimerActived))
                {
                    cursorForm.TimerActived = false;
                    m_ParentAutoClose = true;
                }
            }

            // ֱ�Ӷ�λ��ָ����
            if (m_LocateIndex >= 0)
            {
                gridViewbook.FocusedRowHandle = m_LocateIndex;
                // ���þ۽����к�GridViewû���Զ�����ԭ������������������������ķ������۽�������ʾ����Ļ��
                gridViewbook.TopRowIndex = m_LocateIndex - 3;
                m_LocateIndex = -1;
            }

            if (textInputor.CanFocus)
                textInputor.Focus();
            else
                throw new ApplicationException("���ܶ�λ����ؼ�");
            TimerActived = true;
        }

        private void mainForm_Paint(object sender, PaintEventArgs e)
        {
            // ��ʾ�����򴰿�
            Rectangle topRectangle = new Rectangle(panelTopShadow.Location, panelTopShadow.Size);
            Rectangle dataRectangle = new Rectangle(panelDataShadow.Location, panelDataShadow.Size);

            Region newRegion = new Region(topRectangle);
            newRegion.Union(dataRectangle);
            Region = newRegion;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TimerActived = false;
            //textInputor.Text = "";

            if (Owner != null)
            {
                CaptureCursorForm cursorForm = Owner as CaptureCursorForm;
                if ((cursorForm != null) && m_ParentAutoClose)
                    cursorForm.TimerActived = true;
            }
            m_ParentAutoClose = false;
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 17) // Control ��
                ChangeFormOpacity(true);
        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            //if (!e.Control)
            ChangeFormOpacity(false);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_maxCount > 1)
                CommitSelectAction();
            else
                DoAfterRowHadBeenSelected();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            CancleSelectAction();
        }

        private void inputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                // �жϰ��س��Ƿ����ֱ���˳�
                if (m_HaveSearch || IsDynamic)
                    //CommitSelectAction();
                    DoAfterRowHadBeenSelected();
                else if (!IsDynamic)
                    SearchMatchRow(m_SearchText, MatchType, false);
            }
        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            m_HaveSearch = false;
            m_SearchText = textInputor.Text.Trim();

            // ��̬��ѯʱ��ÿ���ı��ı䶼���в�ѯ
            if (IsDynamic)
                SearchMatchRow(m_SearchText, MatchType, false);
        }

        private void inputBox_Leave(object sender, EventArgs e)
        {
            if (!gridControl1.Focused || (m_DefaultView.Count <= 1))
                textInputor.Focus();
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if ((_maxCount > 1) && e.Control && (e.KeyCode == Keys.Enter))
                CommitSelectAction(); // ��ѡʱ��Ctrl + Enter�����˳�

            // �����¼�����ҳ������Ҫ���͵�Grid��
            switch (e.KeyCode)
            {
                case Keys.Down:
                    gridViewbook.MoveNext();
                    break;
                case Keys.Up:
                    gridViewbook.MovePrev();
                    break;
                case Keys.PageDown:
                    gridViewbook.MoveNextPage();
                    break;
                case Keys.PageUp:
                    gridViewbook.MovePrevPage();
                    break;
                //e.Handled = SendKey2DataGridView(e.KeyCode);               
                default:
                    e.Handled = false;
                    break;
            }
        }

        private void inputBox_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //if (e.Button == MenuButton)
            //{
            //   // ÿ���ֹ������˵��������ΪBarManager�ڴ��ڹر�ʱ���Զ��ͷţ�
            //   BarManager newBarManager = new BarManager();
            //   InitializePopupMenuBeforCall(newBarManager);

            //   popupSettingMenu.ShowPopup(textInputor.PointToScreen(new Point(textInputor.Width - 150, textInputor.Height)));
            //}
        }

        private void inputBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                gridViewbook.MoveBy(-3);
            else
                gridViewbook.MoveBy(3);
        }

        private void panelBottom_SizeChanged(object sender, EventArgs e)
        {
            // ���¼��㰴ť��λ��
            if (panelBottom.Width >= 250)
            {
                btnOk.Location = new Point(panelBottom.Width / 2 - 125, 3);
                btnCancel.Location = new Point(panelBottom.Width / 2 + 50, 3);
            }
            else
            {
                btnOk.Location = new Point(panelBottom.Width / 2 - 75, 3);
                btnCancel.Location = new Point(panelBottom.Width / 2 + 1, 3);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            // ˫����¼ʱ��ʾֱ���˳�
            if (gridViewbook.FocusedRowHandle >= 0)
                //CommitSelectAction();
                DoAfterRowHadBeenSelected();
            textInputor.Focus();
        }

        private void gridViewbook_SelectionChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridViewbook.FocusedRowHandle == -1)
                m_SelectedRowIndex = -1;
            else
            {
                m_SelectedRowIndex = e.FocusedRowHandle;
            }
            // �����û������
            textInputor.Focus();
        }

        private void gridViewbook_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            textInputor.Focus();
        }

        private void panelTop_SizeChanged(object sender, EventArgs e)
        {
            panelTopShadow.Size = panelTop.Size
               + new Size(m_ShadowOffset * 2, m_ShadowOffset * 2);
        }

        private void panelData_SizeChanged(object sender, EventArgs e)
        {
            panelDataShadow.Size = panelData.Size
               + new Size(m_ShadowOffset * 2, m_ShadowOffset * 2);
        }

        private void panelTop_LocationChanged(object sender, EventArgs e)
        {
            // ������Ӱ��������
            Size baseOffset = new Size(m_ShadowOffset, m_ShadowOffset);
            panelTopShadow.Location = panelTop.Location - baseOffset;
        }

        private void panelData_LocationChanged(object sender, EventArgs e)
        {
            // ������Ӱ��������
            Size baseOffset = new Size(m_ShadowOffset, m_ShadowOffset);
            panelDataShadow.Location = panelData.Location - baseOffset;
        }

        #region //removed
        //private void inputBox_MouseUp(object sender, MouseEventArgs e)
        //{
        //   if ((e.Button & MouseButtons.Right) != 0
        //      && textInputor.ClientRectangle.Contains(e.X, e.Y))
        //   {
        //      // ÿ���ֹ������˵��������ΪBarManager�ڴ��ڹر�ʱ���Զ��ͷţ�
        //      BarManager newBarManager = new BarManager();
        //      InitializePopupMenuBeforCall(newBarManager);

        //      popupSettingMenu.ShowPopup(Control.MousePosition);
        //   }
        //}

        //private void barItemIsDynamic_CheckedChanged(object sender, ItemClickEventArgs e)
        //{
        //   IsDynamic = barItemIsDynamic.Checked;
        //}

        //private void barItemMatchBegin_CheckedChanged(object sender, ItemClickEventArgs e)
        //{
        //   if (barItemMatchBegin.Checked)
        //      MatchType = ShowListMatchType.Begin;
        //}

        //private void barItemMatchFull_CheckedChanged(object sender, ItemClickEventArgs e)
        //{
        //   if (barItemMatchFull.Checked)
        //      MatchType = ShowListMatchType.Full;
        //}

        //private void barItemMatchAny_CheckedChanged(object sender, ItemClickEventArgs e)
        //{
        //   if (barItemMatchAny.Checked)
        //      MatchType = ShowListMatchType.Any;
        //}
        #endregion

        private void ckEditBegin_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEditBegin.Checked)
                MatchType = ShowListMatchType.Begin;
        }

        private void ckEditAny_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEditAny.Checked)
                MatchType = ShowListMatchType.Any;
        }

        private void ckEditFull_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEditFull.Checked)
                MatchType = ShowListMatchType.Full;
        }

        private void ckEditDynamic_CheckedChanged(object sender, EventArgs e)
        {
            IsDynamic = ckEditDynamic.Checked;
        }

        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            DoAfterRowHadBeenSelected();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int rowHandle = 0; rowHandle < gridViewbook.RowCount; rowHandle++)
            {
                m_SelectedRowIndex = rowHandle;
                DoAfterRowHadBeenSelected();
            }
        }

        private void btnDeleteOne_Click(object sender, EventArgs e)
        {
            if (lbSelectedRecords.SelectedIndex >= 0)
            {
                ResultRows.RemoveAt(lbSelectedRecords.SelectedIndex);
                lbSelectedRecords.Items.RemoveAt(lbSelectedRecords.SelectedIndex);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            ClearLastData();
        }

        private void DataSelectForm_FontChanged(object sender, EventArgs e)
        {
            ResetControlFont(Font);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lbSelectedRecords.SelectedIndex <= 0)
                return;

            int pos = lbSelectedRecords.SelectedIndex;
            // ���ƶ�result�еļ�¼
            DataRow selectedRow = ResultRows[pos];
            ResultRows.RemoveAt(pos);
            ResultRows.Insert(pos - 1, selectedRow);
            // ���ƶ��ؼ��еļ�¼
            object selectedObj = lbSelectedRecords.Items[pos];
            lbSelectedRecords.Items.RemoveAt(pos);
            lbSelectedRecords.Items.Insert(pos - 1, selectedObj);

            lbSelectedRecords.SelectedIndex = pos - 1;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lbSelectedRecords.SelectedIndex < 0)
                return;
            if (lbSelectedRecords.SelectedIndex == (lbSelectedRecords.ItemCount - 1))
                return;

            int pos = lbSelectedRecords.SelectedIndex;
            // ���ƶ�result�еļ�¼
            DataRow selectedRow = ResultRows[pos];
            ResultRows.RemoveAt(pos);
            ResultRows.Insert(pos + 1, selectedRow);
            // ���ƶ��ؼ��еļ�¼
            object selectedObj = lbSelectedRecords.Items[pos];
            lbSelectedRecords.Items.RemoveAt(pos);
            lbSelectedRecords.Items.Insert(pos + 1, selectedObj);

            lbSelectedRecords.SelectedIndex = pos + 1;
        }
        #endregion
    }
}

