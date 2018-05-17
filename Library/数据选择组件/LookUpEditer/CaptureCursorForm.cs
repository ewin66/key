using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace DrectSoft.Common.Library
{
   /// <summary>
   /// �Զ��رմ���
   /// </summary>
   public class CaptureCursorForm : DevExpress.XtraEditors.XtraForm// Form//, IDisposable
   {
      private Timer timerCheckCursor;

      /// <summary>
      /// ������������Ϣ��ֵ
      /// </summary>
      private const int WM_LBUTTONUP = 0x202;

      #region WndAPI
      [DllImport("user32")]
      internal static extern bool GetCursorPos(out Point lpPoint);
      [DllImport("user32")]
      internal static extern int SetCapture(IntPtr hwnd);
      [DllImport("user32")]
      internal static extern int ReleaseCapture();
      [DllImport("user32")]
      internal static extern IntPtr GetCapture();
      #endregion

      /// <summary>
      /// ���ü�ʱ���Ƿ�����
      /// </summary>
      public bool TimerActived
      {
         get { return _timerActived; }
         set
         {
            _timerActived = value;
            timerCheckCursor.Enabled = _timerActived;
         }
      }
      private bool _timerActived;

      /// <summary>
      /// 
      /// </summary>
      public CaptureCursorForm()
      {
         timerCheckCursor = new Timer();
         timerCheckCursor.Interval = 100;
         timerCheckCursor.Tick += new System.EventHandler(this.timerCheckCursor_Tick);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="disposing"></param>
      protected override void Dispose(bool disposing)
      {
         TimerActived = false;
         timerCheckCursor.Dispose();
         base.Dispose(disposing);
      }

      /// <summary>
      /// ��дWndProc��Ϣ
      /// </summary>
      /// <param name="m"></param>
      protected override void WndProc(ref Message m)
      {
         switch (m.Msg)
         {
            case WM_LBUTTONUP: // ������λ�ò��ڴ��ڷ�Χ�ڣ���رմ���               
               Point p;

               GetCursorPos(out p);

               if (!this.Bounds.Contains(p))
               {
                  //MessageBox.Show(p.ToString() + "\r\n" + PointToClient(p).ToString() + "\r\n" + this.Bounds.ToString());
                  this.Close();
                  return;
               }
               break;
         }
         base.WndProc(ref m);
      }

      private void SetCapture2Form()
      {
         SetCapture(this.Handle);
         this.Cursor = Cursors.Default;

      }

      private void timerCheckCursor_Tick(object sender, EventArgs e)
      {
         IntPtr hwnd = GetCapture();
         //if (this.Bounds.Contains(MousePosition))
         if (CheckMousePositionIsInForm())
         {
            if (hwnd == this.Handle)
            {
               ReleaseCapture();
            }
         }
         else
         {
            if (hwnd != this.Handle)
            {
               SetCapture2Form();
            }
         }
      }

      private bool CheckMousePositionIsInForm()
      {
         if (this.Bounds.Contains(MousePosition))
            return true;
         else
            return CheckMousePositionIsInPopupForm();
      }

      private bool CheckMousePositionIsInPopupForm()
      {
         //PopupBaseForm popForm;
         foreach (Form form in this.OwnedForms)
         {
            if (form.Visible)
            {
               if (form.Bounds.Contains(MousePosition))
                  return true;
            }
            //popForm = form as PopupBaseForm;
            //if ((popForm != null) && (popForm.OwnerEdit.IsPopupOpen))
            //{
            //   if (popForm.Bounds.Contains(MousePosition))
            //      return true;
            //}
         }
         return false;
      }

      private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureCursorForm));
            this.SuspendLayout();
            // 
            // CaptureCursorForm
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CaptureCursorForm";
            this.ResumeLayout(false);

      }
   }
}
