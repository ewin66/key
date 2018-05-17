using DevExpress.XtraEditors;
using DrectSoft.Common.Ctrs.FORM;
using DrectSoft.Core;
using DrectSoft.FrameWork.WinForm;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrectSoft.MainFrame
{
    /// <summary>
    /// �Զ������Ϣ����
    /// </summary>
    public partial class DSMessageBox : DevBaseForm
    {
        #region const
        private const int InitTop = 10;
        private const int InitLeft = 10;
        private const int InitGap = 5;
        const string s_OkCaption = "ȷ��(&O)";
        const string s_CancelCaption = "ȡ��(&C)";
        const string s_YesCaption = "��(&Y)";
        const string s_NoCaption = "��(&N)";

        const int s_MinBoxWidth = 260;
        const int s_MinBoxHeight = 150;
        const int s_DefaultPad = 3;
        #endregion

        #region fields
        private int m_MaxWidth;
        private int m_MaxHeight;
        private int m_MaxLayoutWidth;
        private int m_MaxLayoutHeight;
        private int m_MinLayoutWidth;
        private int m_MinLayoutHeight;
        #endregion

        #region ctors
        public DSMessageBox()
        {
            InitializeComponent();
            this.Text = "��Ϣ";
            this.KeyDown += new KeyEventHandler(FormMessageBox_KeyDown);

            SetContainer();
        }

        public DSMessageBox(Control parent)
            : this()
        {
            this.Parent = parent;

            SetContainer();
        }
        #endregion

        void FormMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(labelMessage.Text);
            }
        }

        #region set message info
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="messages"></param>
        public void SetMessageInfo(Collection<string> messages)
        {
            if (messages == null)
            {
                labelMessage.Text = String.Empty;
            }
            else
            {
                StringBuilder allMsg = new StringBuilder();
                foreach (string msg in messages)
                    allMsg.Append(msg);
                labelMessage.Text = allMsg.ToString();
            }

            // ������Ϣ��ĺ��ʳߴ�
            ResetBoxMaxSize();

            ResetMessageSizeRange();

            Font messageFont = new Font("SimSun", 11);
            Graphics g = Graphics.FromHwnd(panelMessageInfo.Handle);
            SizeF stringSize = g.MeasureString(labelMessage.Text, messageFont, m_MaxLayoutWidth);

            ResetBoxSize(stringSize);
            labelMessage.Text = labelMessage.Text.Trim();
            // ֻ��һ��ʱ������ʾ
            SizeF singleSize = g.MeasureString("��", messageFont);
            if (stringSize.Height <= singleSize.Height)
                labelMessage.TextAlign = ContentAlignment.MiddleCenter;
            else
                labelMessage.TextAlign = ContentAlignment.MiddleLeft;

        }

        private void ResetBoxSize(SizeF stringSize)
        {
            int newHeight = EnsureSizeInRange((int)stringSize.Height, m_MinLayoutHeight, m_MaxLayoutHeight);
            int newWidth = EnsureSizeInRange((int)stringSize.Width + 30, m_MinLayoutWidth, m_MaxLayoutWidth);

            this.Height = newHeight + captionBarAnother1.Height + panelButtonInfo.Height + s_DefaultPad;
            this.Width = newWidth + panelIconInfo.Width + s_DefaultPad;
        }

        private int EnsureSizeInRange(int value, int minValue, int maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// ������Ϣ���ֵ������С�ߴ�
        /// </summary>
        private void ResetMessageSizeRange()
        {
            // �߶� = �Ի���߶� - caption�߶� - ��ť����߶� - Ԥ���߾�
            // ��� = �Ի����� - icon������ - Ԥ���߾�

            int heightMargin = captionBarAnother1.Height + panelButtonInfo.Height + s_DefaultPad;
            int widthMargin = panelIconInfo.Width + s_DefaultPad;
            // ��С�ߴ�
            m_MinLayoutHeight = s_MinBoxHeight - heightMargin;
            m_MinLayoutWidth = s_MinBoxWidth - widthMargin;
            // ���ĳߴ�
            m_MaxLayoutHeight = m_MaxHeight - heightMargin;
            m_MaxLayoutWidth = m_MaxWidth - widthMargin;
        }

        /// <summary>
        /// ÿ�ε���ʱ��ȡ��Ϣ����������ʾ�ĳߴ�(�͵�ǰ��Ļ�й�)
        /// </summary>
        private void ResetBoxMaxSize()
        {
            Rectangle rect;
            if (Parent == null)
                rect = SystemInformation.WorkingArea;
            else
                rect = Screen.GetWorkingArea(this.Parent);
            m_MaxHeight = (int)(rect.Height * 0.90);
            m_MaxWidth = (int)(rect.Width * 0.60);
        }
        #endregion

        #region set ButtonInfo
        public void SetButtonInfo(Collection<SimpleButton> controls)
        {
            this.SuspendLayout();
            panelButtonInfo.Controls.Clear();

            if (controls == null) return;

            int controlCount = controls.Count;
            if (controlCount < 0) return;

            int allControlWidth = 0;
            int addInitLeft = 0;
            for (int i = 0; i < controlCount; i++)
            {
                allControlWidth += controls[i].Width;
            }
            allControlWidth += (controlCount - 1) * InitGap;
            if (allControlWidth < panelButtonInfo.Width - 2 * InitLeft)
            {
                addInitLeft = (panelButtonInfo.Width - 2 * InitLeft - allControlWidth) / 2;
            }

            int controlLeft = InitLeft + addInitLeft;
            for (int i = 0; i < controlCount; i++)
            {
                controls[i].AutoSize = true;
                panelButtonInfo.Controls.Add(controls[i]);
                controls[i].Top = InitTop;
                controls[i].Left = controlLeft;
                controlLeft = controlLeft + controls[i].Width + InitGap;
            }

            this.ResumeLayout();
        }

        public void SetButtonInfo(CustomMessageBoxKind kind)
        {
            Collection<SimpleButton> buttonInfoControls = new Collection<SimpleButton>();
            switch (kind)
            {
                case CustomMessageBoxKind.WarningOk:
                case CustomMessageBoxKind.ErrorOk:
                case CustomMessageBoxKind.InformationOk:
                    buttonInfoControls.Add(CreateButton(s_OkCaption, DialogResult.OK));
                    break;
                case CustomMessageBoxKind.WarningOkCancel:
                case CustomMessageBoxKind.QuestionOkCancel:
                case CustomMessageBoxKind.ErrorOkCancel:
                    buttonInfoControls.Add(CreateButton(s_OkCaption, DialogResult.OK));
                    buttonInfoControls.Add(CreateButton(s_CancelCaption, DialogResult.Cancel));
                    break;
                case CustomMessageBoxKind.QuestionYesNo:
                case CustomMessageBoxKind.ErrorYesNo:
                    buttonInfoControls.Add(CreateButton(s_YesCaption, DialogResult.Yes));
                    buttonInfoControls.Add(CreateButton(s_NoCaption, DialogResult.No));
                    break;
                case CustomMessageBoxKind.QuestionYesNoCancel:
                case CustomMessageBoxKind.ErrorYesNoCancel:
                    buttonInfoControls.Add(CreateButton(s_YesCaption, DialogResult.Yes));
                    buttonInfoControls.Add(CreateButton(s_NoCaption, DialogResult.No, false));
                    buttonInfoControls.Add(CreateButton(s_CancelCaption, DialogResult.Cancel));
                    break;
                case CustomMessageBoxKind.ErrorYes:
                    buttonInfoControls.Add(CreateButton(s_YesCaption, DialogResult.Yes));
                    break;
                default:
                    break;
            }
            SetButtonInfo(buttonInfoControls);
        }

        private SimpleButton CreateButton(string text, DialogResult dialogResult)
        {
            return CreateButton(text, dialogResult, true);
        }

        private SimpleButton CreateButton(string text, DialogResult dialogResult, bool setAcceptCancelButton)
        {
            SimpleButton b = new SimpleButton();
            b.Text = text;
            b.DialogResult = dialogResult;
            if (setAcceptCancelButton)
            {
                if (dialogResult == DialogResult.Yes || dialogResult == DialogResult.OK)
                    this.AcceptButton = b;
                if (dialogResult == DialogResult.No || dialogResult == DialogResult.Cancel)
                    this.CancelButton = b;
            }
            return b;
        }
        #endregion

        #region set icon info
        public void SetIconInfo(CustomMessageBoxIconType iconType)
        {
            Image icon = null;
            switch (iconType)
            {
                case CustomMessageBoxIconType.ErrorIcon:
                    //icon = MainInfo.GetIcon(ResourceNames.Error);
                    icon = Resources.ResourceManager.GetImage(Resources.ResourceNames.MessageBoxError);
                    break;
                case CustomMessageBoxIconType.InformationIcon:
                    //icon = MainInfo.GetIcon(ResourceNames.Information);
                    icon = Resources.ResourceManager.GetImage(Resources.ResourceNames.MessageBoxMessage);
                    break;
                case CustomMessageBoxIconType.QuestionIcon:
                    //icon = MainInfo.GetIcon(ResourceNames.Question);
                    icon = Resources.ResourceManager.GetImage(Resources.ResourceNames.MessageBoxPrompt);
                    break;
                case CustomMessageBoxIconType.WarningIcon:
                    //icon = MainInfo.GetIcon(ResourceNames.Warning);
                    icon = Resources.ResourceManager.GetImage(Resources.ResourceNames.MessageBoxWarning);
                    break;
                default:
                    break;
            }
            if (icon == null) return;
            Bitmap bmp = new Bitmap(icon);
            if (bmp != null)
            {
                bmp.MakeTransparent(Color.Magenta);
                //pictureBoxIcon.Image = bmp;
                pictureBoxIcon.BackgroundImage = bmp;
                pictureBoxIcon.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
                pictureBoxIcon.Image = icon;
        }

        public void SetIconInfo(CustomMessageBoxKind kind)
        {
            CustomMessageBoxIconType iconType = CustomMessageBoxIconType.None;
            switch (kind)
            {
                case CustomMessageBoxKind.WarningOk:
                case CustomMessageBoxKind.WarningOkCancel:
                    iconType = CustomMessageBoxIconType.WarningIcon;
                    break;
                case CustomMessageBoxKind.ErrorYes:
                case CustomMessageBoxKind.ErrorYesNo:
                case CustomMessageBoxKind.ErrorOk:
                case CustomMessageBoxKind.ErrorOkCancel:
                case CustomMessageBoxKind.ErrorYesNoCancel:
                    iconType = CustomMessageBoxIconType.ErrorIcon;
                    break;
                case CustomMessageBoxKind.QuestionOkCancel:
                case CustomMessageBoxKind.QuestionYesNo:
                case CustomMessageBoxKind.QuestionYesNoCancel:
                    iconType = CustomMessageBoxIconType.QuestionIcon;
                    break;
                case CustomMessageBoxKind.InformationOk:
                    iconType = CustomMessageBoxIconType.InformationIcon;
                    break;
                default:
                    break;
            }
            SetIconInfo(iconType);
        }
        #endregion

        private void SetContainer()
        {
            GroupControl groupControlContainer = new GroupControl();
            groupControlContainer.Location = new System.Drawing.Point(0, 0);
            groupControlContainer.Name = "groupControlContainer";
            groupControlContainer.Text = "��ʾ";
            groupControlContainer.Dock = DockStyle.Fill;
            groupControlContainer.MouseDown += new MouseEventHandler(groupControlContainer_MouseDown);
            this.Controls.Add(groupControlContainer);

            foreach (Control control in this.Controls)
            {
                groupControlContainer.Controls.Add(control);
            }

            captionBarAnother1.Visible = false;
        }

        /// <summary>
        /// ����϶�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void groupControlContainer_MouseDown(object sender, MouseEventArgs e)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage((IntPtr)this.FindForm().Handle, NativeMethods.WMNclbuttonDown, (IntPtr)NativeMethods.HTCaption, IntPtr.Zero);
        }
    }

    public enum CustomMessageBoxIconType
    {
        None = 0,
        WarningIcon = 1,
        ErrorIcon = 2,
        QuestionIcon = 3,
        InformationIcon = 4
    }
}