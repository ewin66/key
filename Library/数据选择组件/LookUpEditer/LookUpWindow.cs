using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DrectSoft.Core;
using DrectSoft.Wordbook;

namespace DrectSoft.Common.Library
{
    /// <summary>
    /// ���������ʾ��ѡ������
    /// </summary>
    [ToolboxBitmapAttribute(typeof(DrectSoft.Common.Library.LookUpWindow), "Images.ShowListWindow.ico")]
    public partial class LookUpWindow : Component, ISupportInitialize
    {
        #region properties
        /// <summary>
        /// �Ƿ���ʾ�ֵ��б���б��⡣Ĭ����ʾ
        /// </summary>
        [
          Category("ShowListConfig"),
          Description("�Ƿ���ʾ�ֵ��б���б��⡣Ĭ����ʾ"),
          DefaultValue(true)
        ]
        public bool ShowColumnTitle
        {
            get { return m_SelForm.ShowColumnTitle; }
            set { m_SelForm.ShowColumnTitle = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ�ֵ��б��е�����Ĭ����ʾ
        /// </summary>
        [
          Category("ShowListConfig"),
          Description("�Ƿ���ʾ�ֵ��б��е�����Ĭ����ʾ"),
          DefaultValue(true)
        ]
        public bool ShowGridline
        {
            get { return m_SelForm.ShowGridline; }
            set { m_SelForm.ShowGridline = value; }
        }

        /// <summary>
        /// �Ƿ�ϰ��ʹ����ʴ���
        /// </summary>
        [
          Category("ShowListConfig"),
          Description("�Ƿ�ϰ��ʹ����ʴ���"),
          DefaultValue(false)
        ]
        public bool UseWB
        {
            get { return m_SelForm.UseWB; }
            set { m_SelForm.UseWB = value; }
        }

        /// <summary>
        /// ��̬��ѯ
        /// </summary>
        [
          Category("ShowListConfig"),
          Description("��̬��ѯ"),
          DefaultValue(true)
        ]
        public bool IsDynamic
        {
            get { return m_SelForm.IsDynamic; }
            set { m_SelForm.IsDynamic = value; }
        }

        /// <summary>
        /// ��ѯƥ��ģʽ
        /// </summary>
        [
          Category("ShowListConfig"),
          Description("��ѯƥ��ģʽ"),
          DefaultValue(ShowListMatchType.Begin)
        ]
        public ShowListMatchType MatchType
        {
            get { return m_SelForm.MatchType; }
            set { m_SelForm.MatchType = value; }
        }

        /// <summary>
        /// ShowList���ڵ���ʾģʽ
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public ShowListFormMode FormMode
        {
            get { return m_SelForm.FormMode; }
        }

        /// <summary>
        /// ��ǰShowList���ڴ�����ֵ���ʵ��
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public BaseWordbook Wordbook
        {
            get { return m_SelForm.Wordbook; }
        }

        /// <summary>
        /// ��ǰShowList���ڴ�����ֵ�������
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public WordbookKind BookKind
        {
            get { return m_SelForm.BookKind; }
        }

        /// <summary>
        /// ShowList�����Ƿ���ʾ��Ӱ
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        public bool ShowShadow
        {
            get { return m_SelForm.ShowShadow; }
            set { m_SelForm.ShowShadow = value; }
        }

        /// <summary>
        /// ��ʾ�ڱ༭���е�ֵ���������ʱ�á�,������
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public string DisplayValue
        {
            get { return m_SelForm.DisplayValue; }
        }

        /// <summary>
        /// �༭���е�ֵ��Ӧ�Ĵ��롣�������ʱ�á�,������
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public string CodeValue
        {
            get { return m_SelForm.CodeValue; }
        }

        /// <summary>
        /// �༭���е�ֵ��Ӧ�Ĵ��롣�������ʱ�á�,������
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public string QueryValue
        {
            get { return m_SelForm.QueryValue; }
        }

        /// <summary>
        /// ѡ�е����ݼ�¼�м���
        /// </summary>
        [
          Browsable(false),
          ReadOnly(true)
        ]
        public List<DataRow> ResultRows
        {
            get { return m_SelForm.ResultRows; }
        }

        /// <summary>
        /// ����Ƿ��Ѿ�ѡ����ֵ
        /// </summary>
        [Browsable(false)]
        public bool HadGetValue
        {
            get { return !String.IsNullOrEmpty(CodeValue); }
        }

        /// <summary>
        /// �Ƿ�������ʾShowList����(Ĭ��ֻ��һ��ƥ���¼ʱ����ʾ����)
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        public bool AlwaysShowWindow
        {
            get { return m_SelForm.AlwaysShowWindow; }
            set { m_SelForm.AlwaysShowWindow = value; }
        }

        /// <summary>
        /// ִ��SQL����DataAccess��������ʹ��ǰ��ʼ��������
        /// </summary>
        [
          Browsable(false)
        ]
        public IDataAccess SqlHelper
        {
            get { return m_SelForm.SqlHelper; }
            set { m_SelForm.SqlHelper = value; }
        }

        /// <summary>
        /// ����ƴ���������д�Ķ������û�г�ʼ����������_sqlHelper�����¶���
        /// </summary>
        [Browsable(false)]
        public GenerateShortCode GenShortCode
        {
            get { return m_SelForm.GenShortCode; }
            set { m_SelForm.GenShortCode = value; }
        }

        /// <summary>
        /// ����ѡ������ټ�¼��,��СΪ0
        /// </summary>
        [Browsable(false), DefaultValue(0)]
        public int MinCount
        {
            get { return m_SelForm.MinCount; }
            set { m_SelForm.MinCount = value; }
        }

        /// <summary>
        /// ����ѡ�������¼��
        /// </summary>
        [Browsable(false), DefaultValue(1)]
        public int MaxCount
        {
            get { return m_SelForm.MaxCount; }
            set { m_SelForm.MaxCount = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Font Font
        {
            get { return m_SelForm.Font; }
            set { m_SelForm.Font = value; }
        }

        /// <summary>
        /// ��ȡ������ӵ�д˴���Ĵ���
        /// </summary>
        [Browsable(false)]
        public Form Owner
        {
            get { return m_SelForm.Owner; }
            set { m_SelForm.Owner = value; }
        }
        #endregion

        #region private variables
        private DataSelectForm m_SelForm;
        #endregion

        #region ctors
        /// <summary>
        /// 
        /// </summary>
        public LookUpWindow()
        {
            if (!this.DesignMode)
            {
                m_SelForm = new DataSelectForm();
                InitializeComponent();
                Font = new Font("����", 10.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public LookUpWindow(IContainer container)
            : this()
        {
            if (container == null)
                throw new ArgumentNullException();
            container.Add(this);
        }
        #endregion

        #region interface method
        /// <summary>
        /// ��ʼ��ShowList�ؼ�ʱ���ô˺���
        /// </summary>
        /// <param name="wordbook">��Ҫʹ�õ��ֵ���</param>
        /// <param name="kind">�ֵ�������</param>
        /// <param name="codeValue">��ʼ���Ĵ���ֵ</param>
        public void CallLookUpWindow(BaseWordbook wordbook, WordbookKind kind, string codeValue)
        {
            CallLookUpWindow(wordbook, kind, codeValue
              , ShowListFormMode.Concision
              , new Point(0, 0)
              , new Size(100, 100)
              , new Rectangle(0, 0, 1000, 600)
              , ShowListCallType.Initialize);
        }

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
        /// </summary>
        public void CallLookUpWindow(BaseWordbook wordbook, WordbookKind kind, string initText, ShowListFormMode formMode, Point initPosition, Size inputSize, Rectangle screenSize)
        {
            CallLookUpWindow(wordbook, kind, initText, formMode, initPosition, inputSize, screenSize
              , ShowListCallType.Normal);
        }

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
        public void CallLookUpWindow(BaseWordbook wordbook, WordbookKind kind, string initText, ShowListFormMode formMode, Point initPosition, Size inputSize, Rectangle screenSize, ShowListCallType callType)
        {
            //m_SelForm.ClearTempData();

            //// ��ʼ��ʱ���û�д����ʼ������ֱ���˳�
            //if ((callType == ShowListCallType.Initialize) && (String.IsNullOrEmpty(initText)))
            //   return;

            m_SelForm.CallShowListWindow(wordbook, kind, initText, formMode
               , initPosition, inputSize, screenSize, callType);
        }

        /// <summary>
        /// ���ָ�����ֵ��Ƿ�ֻ����һ����¼��
        /// </summary>
        /// <param name="wordbook"></param>
        /// <param name="kind"></param>
        /// <returns>����ǵĻ��򷵻�{����,����}�ԣ����򷵻ؿ�</returns>
        public string[] ValidateWordbookHasOneRecord(BaseWordbook wordbook, WordbookKind kind)
        {
            return m_SelForm.ValidateWordbookHasOneRecord(wordbook, kind);
        }
        #endregion

        #region ISupportInitialize ��Ա
        /// <summary>
        /// 
        /// </summary>
        public void BeginInit()
        { }

        /// <summary>
        /// 
        /// </summary>
        public void EndInit()
        { }

        #endregion
    }
}
