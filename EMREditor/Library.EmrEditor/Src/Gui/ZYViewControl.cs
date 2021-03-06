﻿using DrectSoft.Library.EmrEditor.Src.Common;
using DrectSoft.Library.EmrEditor.Src.Document;
using DrectSoft.Library.EmrEditor.Src.Print;
using System;
///////////////////////序列化需要的引用
using Windows32;

namespace DrectSoft.Library.EmrEditor.Src.Gui
{
    /// <summary>
    /// 使用锁定鼠标拖拉来修改矩形时的回调委托
    /// </summary>
    /// <param name="SourceRect" type="System.Drawing.Rectangle" >原始矩形</param>
    /// <param name="CurrentRect" type="ref_System.Drawing.Rectangle">当前矩形</param>
    /// <param name="DragStyle" type="int">拖拉方式</param>
    /// <returns>是否继续拖拉</returns>
    /// <seealso>ZYCommon.ZYViewControl.CaptureDragRect</seealso>
    public delegate bool CaptureDragRectangleHandler(System.Drawing.Rectangle SourceRect, ref System.Drawing.Rectangle CurrentRect, int DragStyle);

    /// <summary>
    /// 状态文本改变事件处理
    /// </summary>
    /// <param name="sender" type="object">发送者</param>
    /// <param name="Text" type="string">状态文本</param>
    public delegate void StatusTextChangeHandler(object sender, string Text);

    /// <summary>
    /// 用于绘制文档的视图界面的控件,该控件带有滚动条
    /// 本对象配套使用了 IEMRViewDocument 接口
    /// </summary>
    /// <seealso>ZYCommon.IEMRViewDocument</seealso>
    /// <seealso>ZYCommon.ZYPopupList</seealso>
    /// 
    [Serializable]
    public class ZYViewControl : TextPageViewControl  //,System.Windows.Forms.IMessageFilter 
    {

        public ActiveEditArea ActiveEditArea = null;
        /// <summary>
        /// 在只读状态下，设置当前文档的只读区域,这个范围需要根据编辑区域的变化而变化
        /// </summary>
        /// <param name="ele"></param>
        /// <returns></returns>
        public bool IsInActiveEditArea(ZYTextElement ele)
        {
            if (this.Document.OwnerControl.ActiveEditArea != null)
            {
                if (ActiveEditArea.Top + ActiveEditArea.TopElement.Height <= ele.RealTop && ele.RealTop + ele.Height <= ActiveEditArea.End)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ZYViewControl()
        {
            //设置控件可接受焦点
            this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, true);
            //控件将自行绘制
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            //设置控件的背景色
            this.BackColor = System.Drawing.SystemColors.Window;
            //设置自动滚动
            this.AutoScroll = true;



            //接受拖放到控件上的数据
            this.AllowDrop = true;
        }

        /// <summary>
        /// 本控件包含的文档对象
        /// </summary>
        protected ZYTextDocument myDocument = null;
        /// <summary>
        /// 是否自动换行
        /// </summary>
        protected bool bolWordWrap = false;
        /// <summary>
        /// 是否允许切换插入-修改模式
        /// </summary>
        protected bool bolEnableInsertMode = true;

        /// <summary>
        /// 控件是否锁定鼠标
        /// </summary>
        protected bool bolCaptureMouse = false;
        /// <summary>
        /// 鼠标开始锁定时鼠标在文档视图区域中的X坐标
        /// </summary>
        protected int intStartCaptureX = 0;
        /// <summary>
        /// 鼠标开始锁定时鼠标在文档视图区域中的Y坐标
        /// </summary>
        protected int intStartCaptureY = 0;
        /// <summary>
        /// 上一次鼠标移动事件中的鼠标光标在文档视图区域中的X坐标
        /// </summary>
        protected int intLastMouseX = 0;
        /// <summary>
        /// 上一次鼠标移动事件中的鼠标光标在文档视图区域中的Y坐标
        /// </summary>
        protected int intLastMouseY = 0;
        /// <summary>
        /// 鼠标当前位置和上次位置的横向距离
        /// </summary>
        protected int intLastMouseDX = 0;
        /// <summary>
        /// 鼠标当前位置和上次位置的纵向距离
        /// </summary>
        protected int intLastMouseDY = 0;

        /// <summary>
        /// 编辑器状态标志
        /// </summary>
        public int EditorFlag = 0;

        /// <summary>
        /// 状态文本改变事件对象
        /// </summary>
        public event StatusTextChangeHandler StatusTextChange;

        /// <summary>
        /// 最后一个鼠标和键盘消息处理时的时间戳
        /// </summary>
        protected int LastMessageTick = 0;

        //protected System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        ///// <summary>
        ///// 静态的提示文本，若设置该标志位时移动鼠标将不会隐藏提示文本
        ///// </summary>
        //public bool StaticToolTip = false;



        //
        //		/// <summary>
        //		/// 用于记录用户输入的所有字符的记录器
        //		/// </summary>
        //		protected System.Text.StringBuilder myCharRecorder = null;
        // 
        //		/// <summary>
        //		/// 开始记录用户输入的字符
        //		/// </summary>
        //		public void BeginRecordChar()
        //		{
        //			myCharRecorder = new System.Text.StringBuilder();
        //		}
        //		/// <summary>
        //		/// 停止记录用户输入的字符
        //		/// </summary>
        //		/// <returns></returns>
        //		public string EndRecordChar()
        //		{
        //			string strText = myCharRecorder.ToString();
        //			myCharRecorder = null;
        //			if( strText == null || strText.Length == 0)
        //				return null;
        //			else
        //				return strText;
        //		}



        /// <summary>
        /// 已重写:销毁对象，释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            #region 注释内容
            //if (myPopupList != null)
            //{
            //    myPopupList.Dispose();
            //    myPopupList = null;
            //}
            //if (myToolTipCtl != null)
            //{
            //    myToolTipCtl.Dispose();
            //    myToolTipCtl = null;
            //}
            //if (myTimer != null)
            //{
            //    myTimer.Dispose();
            //    myTimer = null;
            //}
            #endregion
            base.Dispose(disposing);
        }





        #region 获得鼠标信息的属性群 **************************************************************
        /// <summary>
        /// 设置鼠标光标
        /// </summary>
        /// <param name="vCursor"></param>
        public void SetCursor(System.Windows.Forms.Cursor vCursor)
        {
            //if (this.Cursor.Equals(vCursor) == false)
            this.Cursor = vCursor;
        }

        /// <summary>
        /// 用户控件是否捕获鼠标
        /// (鼠标按键按下开始捕获,松开则结束捕获)
        /// <para>
        /// OnViewMouseDown中设为true, OnViewMouseUp则设为false. 默认为false
        /// </para>
        /// </summary>
        public bool CaptureMouse
        {
            get { return bolCaptureMouse; }
            set { bolCaptureMouse = value; }
        }
        /// <summary>
        /// 鼠标开始锁定时鼠标在文档视图区域中的X坐标
        /// </summary>
        public int StartCaptureX
        {
            get { return intStartCaptureX; }
        }
        /// <summary>
        /// 鼠标开始锁定时鼠标在文档视图区域中的Y坐标
        /// </summary>
        public int StartCaptureY
        {
            get { return intStartCaptureY; }
        }
        /// <summary>
        /// 鼠标当前位置和上次位置的横向距离
        /// (好像没用mfb)
        /// </summary>
        public int LastMouseDX
        {
            get { return intLastMouseDX; }
        }
        /// <summary>
        /// 鼠标当前位置和上次位置的纵向距离
        /// (好像没用mfb)
        /// </summary>
        public int LastMouseDY
        {
            get { return intLastMouseDY; }
        }
        /// <summary>
        /// 上一次鼠标移动事件中的鼠标光标在文档视图区域中的X坐标
        /// </summary>
        public int LastMouseX
        {
            get { return intLastMouseX; }
        }
        /// <summary>
        /// 上一次鼠标移动事件中的鼠标光标在文档视图区域中的Y坐标
        /// </summary>
        public int LastMouseY
        {
            get { return intLastMouseY; }
        }

        #endregion

        /// <summary>
        /// 是否允许执行插入修改模式的切换
        /// </summary>
        public bool EnableInsertMode
        {
            get { return bolEnableInsertMode; }
            set { bolEnableInsertMode = value; }
        }

        /// <summary>
        /// 控件编辑是否处于插入模式( True 插入模式 , False 改写模式)
        /// </summary>
        public override bool InsertMode
        {
            get { return bolInsertMode; }
            set
            {
                bolInsertMode = value;
                if (myDocument != null)
                    myDocument.ViewInsertModeChange();
            }
        }

        /// <summary>
        /// 对文档是否进行自动换行,此时视图区域的宽度不超过控件客户区的宽度
        /// </summary>
        public bool WordWrap
        {
            get { return bolWordWrap; }
            set
            {
                bolWordWrap = value;
                this.HScroll = !value;
            }
        }

        /// <summary>
        /// 本控件使用的文档对象
        /// </summary>
        public ZYTextDocument Document
        {
            get { return myDocument; }
            set { myDocument = value; }
        }

        //		/// <summary>
        //		/// 设置用户控件显示的视图区域的大小
        //		/// </summary>
        //		/// <param name="width"></param>
        //		/// <param name="height"></param>
        //		/// <returns>本函数是否导致刷新</returns>
        //		public bool SetViewSize(int width , int height)
        //		{
        //			if( intUpdateLevel > 0 ) return false ;
        //			System.Drawing.Size mySize = System.Drawing.Size.Empty ;
        //			if( bolWordWrap && width > this.ClientSize.Width)
        //				width = this.ClientSize.Width ;
        //			try
        //			{
        //				mySize = new System.Drawing.Size(width ,height);
        //				if(this.AutoScrollMinSize.Equals(mySize)==false)
        //				{
        //					this.AutoScrollMinSize = mySize;
        //					this.Refresh();
        //					return true;
        //				}
        //				return false;
        //			}
        //			catch(Exception ext)
        //			{
        //				System.Windows.Forms.MessageBox.Show(ext.ToString() + "\r\nSize:" + mySize.ToString());
        //			}
        //			return false;
        //		}

        /// <summary>
        /// 获得视图区域的大小(获得自动滚动的最小尺寸)
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Size GetViewSize()
        {
            return base.AutoScrollMinSize;
        }

        /// <summary>
        /// 更新视图区域的一部分
        /// </summary>
        /// <param name="vRect">更新的区域</param>
        public void UpdateView(System.Drawing.Rectangle vRect)
        {
            System.Drawing.Point myPoint = this.ViewPointToClient(0, 0);
            vRect.Offset(myPoint.X, myPoint.Y);
            this.Invalidate(vRect);
        }



        /// <summary>
        /// 设置状态文本
        /// </summary>
        /// <param name="strText"></param>
        protected virtual void OnStatusTextChange(string strText)
        {
            if (StatusTextChange != null)
            {
                StatusTextChange(this, strText);
            }
        }

        #region 对旧函数的重写 ********************************************************************

        /// <summary>
        /// 已重载:刷新用户界面
        /// </summary>
        public override void Refresh()
        {

            if (this.Updating == false)
            {
                myInvalidateRect.Clear();
                base.Refresh();
            }
        }

        /// <summary>
        /// 测试客户区指定位置坐标所在的页面对象
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public PrintPage HitTestPage(int x, int y)
        {
            x -= this.AutoScrollPosition.X;
            y -= this.AutoScrollPosition.Y;
            foreach (PrintPage myPage in myPages)
            {
                if (myPage.ClientBounds.Contains(x, y))
                    return myPage;
            }
            return null;
        }


        #endregion

        #region 鼠标键盘及其他事件处理 ************************************************************
        /// <summary>
        /// 查看 Shift 功能键是否正在按下
        /// </summary>
        /// <returns></returns>
        public bool HasShiftPressing()
        {
            return ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Shift) != 0);
        }

        /// <summary>
        /// 判断 Control 功能键是否按下
        /// </summary>
        /// <returns></returns>
        public bool HasControlPressing()
        {
            return ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control) != 0);
        }

        /// <summary>
        /// 判断 Alt 功能键是否按下
        /// </summary>
        /// <returns></returns>
        public bool HasAltPressing()
        {
            return ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Alt) != 0);
        }

        #region 键盘事件处理

        #endregion bwy :


        #region 鼠标事件处理
        private void mcp_Draw(object sender, CaptureMouseMoveEventArgs e)
        {
            RectangleMouseCapturer mcp = (RectangleMouseCapturer)sender;
            int dx = mcp.CurrentPosition.X - mcp.StartPosition.X;
            int dy = mcp.CurrentPosition.Y - mcp.StartPosition.Y;
            System.Drawing.Size size = this.Transform.TransformSize(dx, dy);
            System.Drawing.Rectangle rect = mcp.UpdateRectangle(mcp.SourceRectangle, size.Width, size.Height);
            this.ReversibleViewDrawRect(rect);
        }
        private void mcp_MouseMove(object sender, CaptureMouseMoveEventArgs e)
        {
            RectangleMouseCapturer mcp = (RectangleMouseCapturer)sender;
            int dx = mcp.CurrentPosition.X - mcp.StartPosition.X;
            int dy = mcp.CurrentPosition.Y - mcp.StartPosition.Y;
            System.Drawing.Size size = this.Transform.TransformSize(dx, dy);
            mcp.DescRectangle = mcp.UpdateRectangle(mcp.SourceRectangle, size.Width, size.Height);
        }
        /// <summary>
        /// 锁定鼠标并进行矩形的拖拉操作
        /// </summary>
        /// <remarks>在文档编辑或其他设计器中,经常会出现需要使用鼠标拖拽对象四周边缘上的8个控制点
        /// 的方式来改变对象的位置和大小,本函数则专门用于支持该操作。
        /// 本函数会锁定用户的所有的鼠标和键盘消息,并根据鼠标的移动来修改
        /// 指定的矩形,直到用户松开鼠标或者回调函数取消本次操作
        /// DragStyle 参数为拖拽点的编号，其有效范围为－1至7，其意义为
        /// -1 正在拖拽矩形本身，此时移动鼠标将整体移动矩形
        /// 0  拖拽矩形左上角的控制点，修改矩形的左上角位置，矩形的右下角位置不变,会导致矩形的位置和大小的改变
        /// 1  拖拽矩形上边缘中间的控制点，修改矩形的上边缘位置，其他3个边缘的位置不变，会导致矩形的顶端位置和高度的改变
        /// 2  拖拽矩形右上角的控制点，修改矩形的右上角位置，其左下角的位置不变，会导致矩形的顶端位置和宽度的改变
        /// 3  拖拽矩形右边缘中间的控制点，修改矩形的右边缘的位置，其他边缘位置不变，会导致矩形的宽度的改变
        /// 4  拖拽矩形右下角的控制点，修改矩形的右下角的位置，左上角的位置不变，会导致矩形的大小的改变
        /// 5  拖拽矩形下边缘中间的控制点，修改矩形的下边缘位置，其他边缘不变，会导致矩形的高度的改变
        /// 6  拖拽矩形左下角的控制点，修改矩形的左下角位置，其右上角位置不变，会导致矩形的左端位置和高度的改变
        /// 7  拖拽矩形左边缘中间的控制点，修改矩形左边缘的位置，其他边缘不变，会导致矩形的左端位置和宽度的改变
        /// 关于8个拖拽控制点请参见<link>ZYCommon.DocumentView.GetDragRects</link></remarks>
        /// <param name="SourceRect">原始矩形,坐标为视图区域中的坐标</param>
        /// <param name="DragStyle">拖拽控制点的编号</param>
        /// <param name="DrawFocusRect">拖拉时是否绘制可逆转矩形,若设为true 则会拖拽时会自动绘制当前矩形的可逆矩形边框</param>
        /// <param name="WidthHeightRate">拖动时边框的宽度和高度的比例，若小于等于0.1则不作该设置</param>
        /// <param name="ShowSizeInfo" >拖动时是否显示大小信息</param>
        /// <param name="CallBack">回调函数的委托</param>
        /// <returns>原始矩形进行拖拉操作后修改后的矩形,若用户未改变原始矩形的大小或取消操作则返回空矩形,坐标为视图区域中的坐标</returns>
        /// <seealso>ZYCommon.CaptureDragRectangleHandler</seealso>
        public System.Drawing.Rectangle CaptureDragRect(System.Drawing.Rectangle SourceRect, int DragStyle, bool DrawFocusRect, double WidthHeightRate, bool ShowSizeInfo, CaptureDragRectangleHandler CallBack)
        {
            RectangleMouseCapturer mcp = new RectangleMouseCapturer(this);
            mcp.SourceRectangle = SourceRect;
            mcp.CustomAction = true;
            mcp.DragStyle = DragStyle;
            mcp.Draw += new CaptureMouseMoveEventHandler(mcp_Draw);
            mcp.MouseMove += new CaptureMouseMoveEventHandler(mcp_MouseMove);
            if (mcp.CaptureMouseMove())
            {
                bolCaptureMouse = false;
                return mcp.DescRectangle;
            }
            bolCaptureMouse = false;
            return System.Drawing.Rectangle.Empty;
        }



        /// <summary>
        /// 开始进行拖拽处理,若不需要进行拖拽处理则返回false
        /// </summary>
        /// <returns>操作是否成功</returns>
        protected virtual bool OnStartDrag(int x, int y)
        {
            return false;
        }

        /// <summary>
        /// 重载鼠标按键按下事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnViewMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnViewMouseDown(e);
            LastMessageTick = System.Environment.TickCount;
            System.Drawing.Point myPoint = new System.Drawing.Point(e.X, e.Y);
            if (this.ContextMenu != null && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (myDocument != null)
                {
                    myDocument.ViewMouseDown(myPoint.X, myPoint.Y, e.Button);
                }
                return;
            }
            bolCaptureMouse = true;
            intStartCaptureX = myPoint.X;
            intStartCaptureY = myPoint.Y;
            intLastMouseX = intStartCaptureX;
            intLastMouseY = intStartCaptureY;
            if (myDocument != null)
            {
                if (myPoint.X != -1 || myPoint.Y != -1)
                {
                    if (myDocument.ViewMouseDown(intStartCaptureX, intStartCaptureY, e.Button))
                    {
                        return;
                    }
                }
            }

        }
        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnViewMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            LastMessageTick = System.Environment.TickCount;
            if (bolCaptureMouse == false)
            {
                this.SetCursor(System.Windows.Forms.Cursors.IBeam);
            }
            // 进行坐标转换
            System.Drawing.Point myPoint = new System.Drawing.Point(e.X, e.Y);

            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.AllowDrop)
            {
                RectangleObject myRect = new RectangleObject();
                myRect.SetRect(intStartCaptureX, intStartCaptureY, myPoint.X, myPoint.Y);
                if (myRect.Width > System.Windows.Forms.SystemInformation.DragSize.Width
                    || myRect.Height > System.Windows.Forms.SystemInformation.DragSize.Height)
                {
                    // 开始进行拖拽处理
                    if (OnStartDrag(myPoint.X, myPoint.Y))
                        return;
                }
            }

            // 计算移动步长
            intLastMouseDX = myPoint.X - intLastMouseX;
            intLastMouseDY = myPoint.Y - intLastMouseY;

            if (myDocument != null)
            {
                if (myDocument.ViewMouseMove(myPoint.X, myPoint.Y, e.Button))
                {
                    intLastMouseX = myPoint.X;
                    intLastMouseY = myPoint.Y;
                    return;
                }
            }
            intLastMouseX = myPoint.X;
            intLastMouseY = myPoint.Y;
            base.OnViewMouseMove(e);
        }
        /// <summary>
        /// 重载鼠标按键松开事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnViewMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            LastMessageTick = System.Environment.TickCount;
            // 用户控件结束鼠标捕获
            bolCaptureMouse = false;
            System.Drawing.Point myPoint = new System.Drawing.Point(e.X, e.Y);
            intLastMouseDX = myPoint.X - intLastMouseX;
            intLastMouseDY = myPoint.Y - intLastMouseY;
            if (myDocument != null)
            {
                if (myPoint.X != -1 || myPoint.Y != -1)
                {
                    if (myDocument.ViewMouseUp(myPoint.X, myPoint.Y, e.Button))
                    {
                        intLastMouseX = myPoint.X;
                        intLastMouseY = myPoint.Y;
                        return;
                    }
                }
            }
            intLastMouseX = myPoint.X;
            intLastMouseY = myPoint.Y;
            base.OnViewMouseUp(e);
        }
        /// <summary>
        /// 重载鼠标移开处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (myDocument != null)
            {
                if (myDocument.ViewMouseLeave()) return;
            }
            base.OnMouseLeave(e);
        }
        protected override void OnViewClick(System.Windows.Forms.MouseEventArgs e)
        {

            if (bolLockingUI && !IsInActiveEditArea(this.Document.Content.CurrentElement)) return;
            if (myDocument != null)
            {
                System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
                if (p.X != -1 || p.Y != -1)
                {
                    if (myDocument.ViewMouseClick(p.X, p.Y, System.Windows.Forms.Control.MouseButtons))
                    {

                    }
                }
            }
            base.OnViewClick(e);
        }

        protected override void OnViewDoubleClick(System.Windows.Forms.MouseEventArgs e)
        {
            ZYTextElement curChar = myDocument.GetElementByPos(e.X, e.Y);
            if (ModifyTipForm.ShowModifyTipFormLogic(curChar, e.X, e.Y)) return;

            if (bolLockingUI && !IsInActiveEditArea(this.Document.Content.CurrentElement)) return;
            if (myDocument != null)
            {
                System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
                if (p.X != -1 || p.Y != -1)
                    myDocument.ViewMouseDoubleClick(p.X, p.Y, System.Windows.Forms.Control.MouseButtons);
            }
            base.OnViewDoubleClick(e);
        }
        #endregion

        #endregion


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ZYViewControl
            // 
            this.AllowDrop = true;

            this.ResumeLayout(false);

        }

    }// ZYViewControl
}
