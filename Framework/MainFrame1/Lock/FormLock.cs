using DrectSoft.Common.Ctrs.FORM;
using DrectSoft.Core;
using DrectSoft.Resources;
using DrectSoft.Wordbook;
using MainFrame;
/*********************************************************************************
** File Name	:   FormLock.cs
** User		    :	xjt
** Date	        :	2010-05-25
** Description	:	LOCK SCREEN
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DrectSoft.MainFrame
{
    public partial class FormLock : DevBaseForm
    {
        IDataAccess m_SqlHelper;
        DrectSoftLog m_Logger;

        IUser m_CurrentUser;
        Account m_Account;
        DataTable m_DataTable;

        public FormLock()
        {
            InitializeComponent();
        }

        private void FormLock_Load(object sender, EventArgs e)
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex);
            }
        }

        /// <summary>
        /// ��ֹʹ��ALT+F4
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;
            }

        }

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLock_FormClosing(object sender, FormClosingEventArgs e)
        {
            //OK��ע��ǰ�û���¼��Retry��ע��ǰ�û���¼��Cancel��ȡ��
            if (this.DialogResult == DialogResult.OK)
            {
                //do something
            }
            else if (this.DialogResult == DialogResult.Retry)
            {
                //do something
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void simpleButtonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lookUpEditorLock.CodeValue != string.Empty)
                    Confirm();
                else
                    FormMain.Instance.MessageShow("��ѡ���û�!", CustomMessageBoxKind.InformationOk);
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex);
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void lookUpEditorLock_CodeValueChanged(object sender, EventArgs e)
        {
            try
            {
                string codeValue = lookUpEditorLock.CodeValue;
                this.textEditPassWord.Text = string.Empty;
                GetGridSource(codeValue);
                if (m_CurrentUser.Id == codeValue)
                {
                    this.labelControlShow.Visible = false;
                    this.labelControlPassWord.Visible = true;
                    this.textEditPassWord.Visible = true;

                }
                else
                {
                    this.labelControlShow.Visible = true;
                    this.labelControlPassWord.Visible = false;
                    this.textEditPassWord.Visible = false;
                }
                this.cardView1.SelectRow(0);
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex);
            }
        }


        private void textEditPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Confirm();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Init
        /// </summary>
        private void Init()
        {
            try
            {


                //// ����������
                //IntPtr trayHwnd = FindWindow("Shell_TrayWnd", null);
                //ShowWindow(trayHwnd, 0);


                //// ����windows�ȼ�
                //intLLKey = SetWindowsHookEx(13, DisableHotkeys, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);


                this.FormBorderStyle = FormBorderStyle.FixedDialog;//no title
                m_SqlHelper = DataAccessFactory.DefaultDataAccess;

                m_Logger = new DrectSoftLog("ϵͳ����ģ�����");
                m_CurrentUser = FormMain.Instance.User;
                m_Account = new Account();

                InitControls();

                // �������������
                //RegistryKey regkey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                //regkey.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
                //regkey.Close();
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex);
            }

        }

        /// <summary>
        /// ��ʼ��lookupeditor & Grid
        /// </summary>
        private void InitControls()
        {
            try
            {
                this.lookUpWindowLock.SqlHelper = m_SqlHelper;
                //m_DataTable = m_SqlHelper.ExecuteDataTable(" SELECT ID , ID + '_' + Name as �û���,Images  FROM Users  WHERE Valid = 1 order by ID");
                m_DataTable = m_SqlHelper.ExecuteDataTable("SELECT ID , ID || '_' || Name as �û���  FROM Users  WHERE Valid = 1 order by ID");


                Dictionary<string, int> columnwidth = new Dictionary<string, int>();
                columnwidth.Add("�û���", 165);
                SqlWordbook sqlWordBook = new SqlWordbook("ID", m_DataTable, "ID", "�û���", columnwidth, true);

                lookUpEditorLock.SqlWordbook = sqlWordBook;
                lookUpEditorLock.ListWindow = this.lookUpWindowLock;
                this.lookUpEditorLock.CodeValue = m_CurrentUser.Id;
                this.textEditPassWord.Focus();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                m_Logger.Error(ex);
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex);
            }
        }

        /// <summary>
        /// ����ԭʼ�û���¼,ֱ����֤��Ϣ
        /// </summary>
        /// <returns></returns>
        private bool LoginDb()
        {
            try
            {
                IUser user = m_Account.Login(this.lookUpEditorLock.CodeValue, textEditPassWord.Text, 0);
                m_Logger.Info("�û�:" + user.Name + "��" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "�ɹ���¼ϵͳ");
            }
            catch (Exception ex)
            {
                //todo
                textEditPassWord.SelectAll();
                textEditPassWord.Focus();
                return false;
            }

            return true;
        }

        private void Confirm()
        {
            try
            {
                if (this.lookUpEditorLock.CodeValue != string.Empty)
                {
                    if (lookUpEditorLock.CodeValue == m_CurrentUser.Id)
                    {
                        if (LoginDb())
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                            MessageBox.Show("�������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string codeVale = this.lookUpEditorLock.CodeValue;
                        Program.m_StrUserId = codeVale;
                        //m_Account.InsertUserLogIn(m_CurrentUser.ID, this.GetType().ToString(), FormMain.Instance.MacAddress, FormMain.Instance.Ip, "1", codeVale);
                        this.DialogResult = DialogResult.Retry;
                    }
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex);
            }
        }

        /// <summary>
        /// ͣ��
        /// </summary>
        /// <param name="dataTable"></param>
        private void GetDbImages(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!dataRow.IsNull("Images"))
                {
                    MemoryStream ms = new MemoryStream((byte[])dataRow["Images"]);
                    cardView1.SetRowCellValue(0, cardView1.Columns["Images"], ms.ToArray());
                    //dataRow["Images"] = Image.FromStream(ms);
                }
            }
        }

        /// <summary>
        /// �ı��û�ʱ,�õ�grid����Դ
        /// </summary>
        /// <param name="codeValue"></param>
        private void GetGridSource(string codeValue)
        {
            try
            {
                DataTable dataTable = m_DataTable.Copy();
                dataTable.Clear();
                DataRow[] dataRows = m_DataTable.Select("ID='" + codeValue + "'");
                if (dataRows.Length > 0)
                {
                    dataTable.ImportRow(dataRows[0]);
                    dataTable.AcceptChanges();
                }
                MemoryStream ms = new MemoryStream();
                Image image = ResourceManager.GetImage("NoImage.bmp");
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0].IsNull("Images"))
                    {
                        //cardView1.SetRowCellValue(0, "Images", ms.ToArray());
                        dataTable.Rows[0]["Images"] = ms.ToArray();
                    }
                }
                dataTable.AcceptChanges();
                this.groupControl1.SuspendLayout();
                this.gridControlLock.DataSource = dataTable;
                this.groupControl1.ResumeLayout();
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex);
            }
        }
        #endregion


    }
}