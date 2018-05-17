using System;
using System.Windows.Forms ;
using System.Collections ;
using System.Drawing ;

namespace XWriterDemo
{
	
	/// <summary>
	/// ��ɫѡ��˵���Ŀ����������¼��
    /// add by ywk 2012��12��12��15:43:48 ��
	/// </summary>
	public class ColorMenuItem : System.Windows.Forms.MenuItem
	{
		/// <summary>
		/// ����16�ֱ�׼��ɫ�˵���Ŀ�б�
		/// </summary>
		/// <param name="handler">�˵��¼�����</param>
		/// <returns>�����Ĳ˵�����</returns>
		public static ColorMenuItem[] CreateStandItems( System.EventHandler handler )
		{
			System.Collections.ArrayList list = new ArrayList();
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0,   0,   0) , "Black" , handler )); // Black
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(128,   0,   0) , "Bark Red" , handler )); // Dark Red
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0, 128,   0) , "Dark Green" , handler )); // Dark Green
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(128, 128,   0) , "Pea Green" , handler )); // Pea Green
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0,   0, 128) , "Dark Blue" , handler )); // Dark Blue
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(128,   0, 128) , "Lavender" , handler )); // Lavender
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0, 128, 128) , "Slate" , handler )); // Slate
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(192, 192, 192) , "Light Gray" , handler )); // Light Gray
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(128, 128, 128) , "Dark Gray" , handler )); // Dark Gray
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(255,   0,   0) , "Bright Red" , handler )); // Bright Red
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0, 255,   0) , "Bright Green" , handler )); // Bright Green
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(255, 255,   0) , "Yellow" , handler )); // Yellow
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0,   0, 255) , "Bright Blue" , handler )); // Bright Blue
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(255,   0, 255) , "Magenta" , handler )); // Magenta
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0, 255, 255) , "Cyan" , handler )); // Cyan
			list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(255, 255, 255) , "White" , handler )); //  1 - white
			//
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0,   0,   0) , "��ɫ" , handler )); // Black
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(128,   0,   0) , "���ɫ" , handler )); // Dark Red
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0, 128,   0) , "����ɫ" , handler )); // Dark Green
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(128, 128,   0) , "���ɫ" , handler )); // Pea Green
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0,   0, 128) , "����ɫ" , handler )); // Dark Blue
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(128,   0, 128) , "��ɫ" , handler )); // Lavender
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0, 128, 128) , "����ɫ" , handler )); // Slate
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(192, 192, 192) , "��ɫ" , handler )); // Light Gray
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(128, 128, 128) , "���ɫ" , handler )); // Dark Gray
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(255,   0,   0) , "��ɫ" , handler )); // Bright Red
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0, 255,   0) , "��ɫ" , handler )); // Bright Green
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(255, 255,   0) , "��ɫ" , handler )); // Yellow
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0,   0, 255) , "��ɫ" , handler )); // Bright Blue
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(255,   0, 255) , "���ɫ" , handler )); // Magenta
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(  0, 255, 255) , "��ɫ" , handler )); // Cyan
			//				list.Add( new ColorMenuItem( System.Drawing.Color.FromArgb(255, 255, 255) , "��ɫ" , handler )); //  1 - white
			return ( ColorMenuItem[] ) list.ToArray( typeof( ColorMenuItem ));
		}

		/// <summary>
		/// �������
		/// </summary>
		private static System.Drawing.Font myFont = 
			System.Windows.Forms.SystemInformation.MenuFont ;
		/// <summary>
		/// ��ʼ������
		/// </summary>
		public ColorMenuItem()
		{
			this.OwnerDraw = true ;
		}
		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <param name="c">��ɫֵ</param>
		/// <param name="text">�˵��ı�</param>
		public ColorMenuItem( System.Drawing.Color c , string text )
		{
			this.OwnerDraw = true ;
			intColor = c ;
			this.Text = text ;
		}

		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <param name="c">��ɫֵ</param>
		/// <param name="text">�˵��ı�</param>
		/// <param name="handler">�˵�����¼�����</param>
		public ColorMenuItem(
			System.Drawing.Color c ,
			string text , 
			System.EventHandler handler )
		{
			this.OwnerDraw = true ;
			intColor = c ;
			this.Text = text ;
			this.Click += handler ;
		}
		private bool bolDialogSelectColor = false;
		/// <summary>
		/// ʹ�öԻ�����ѡ����ɫ
		/// </summary>
		public bool DialogSelectColor
		{
			get{ return bolDialogSelectColor ;}
			set{ bolDialogSelectColor = value;}
		}

		private System.Drawing.Color intColor = System.Drawing.Color.Black ;
		/// <summary>
		/// ��ɫֵ
		/// </summary>
		public System.Drawing.Color Color
		{
			get{ return intColor ;}
			set{ intColor = value;}
		}

		/// <summary>
		/// ����˵�����¼�
		/// </summary>
		/// <param name="e">�¼�����</param>
		protected override void OnClick(EventArgs e)
		{
			if( this.bolDialogSelectColor )
			{
				using( System.Windows.Forms.ColorDialog dlg = new ColorDialog())
				{
					dlg.Color = intColor ;
					if( dlg.ShowDialog( ) == System.Windows.Forms.DialogResult.OK )
					{
						intColor = dlg.Color ;
						base.OnClick( e );
					}
				}
			}
			else
				base.OnClick (e);
		}

		/// <summary>
		/// �������˵���С�¼�
		/// </summary>
		/// <param name="e">�¼�����</param>
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			base.OnMeasureItem (e);
			//e.ItemWidth += 20 ;
			e.ItemHeight = System.Windows.Forms.SystemInformation.MenuHeight ;
			string txt = this.Text ;
			using( System.Drawing.StringFormat f = new StringFormat(
					   System.Drawing.StringFormat.GenericDefault ))
			{
				f.FormatFlags = System.Drawing.StringFormatFlags.NoWrap ;
				System.Drawing.SizeF size = e.Graphics.MeasureString(
					this.Text , myFont , 1000 , f );
				e.ItemWidth = 20 + ( int) Math.Ceiling( size.Width );
			}
		}
		/// <summary>
		/// ������Ʋ˵��¼�
		/// </summary>
		/// <param name="e">�¼�����</param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			base.OnDrawItem (e);
			e.DrawBackground();
			System.Drawing.Rectangle rect = new Rectangle( 
				e.Bounds.Left + 4 , 
				e.Bounds.Top + ( e.Bounds.Height - 13 ) / 2 ,
				13 , 
				13 );
			using( System.Drawing.SolidBrush b = new SolidBrush( this.intColor ))
			{
				e.Graphics.FillRectangle( b , rect );
				e.Graphics.DrawRectangle( System.Drawing.Pens.Black , rect );
			}
			using( System.Drawing.StringFormat f = new StringFormat())
			{
				f.FormatFlags = System.Drawing.StringFormatFlags.NoWrap ;
				f.Alignment = System.Drawing.StringAlignment.Near ;
				f.LineAlignment = System.Drawing.StringAlignment.Center ;
				System.Drawing.RectangleF rect2 = new RectangleF(
					rect.Right + 2 , 
					e.Bounds.Top ,
					e.Bounds.Right - rect.Right - 2 ,
					e.Bounds.Height );
				using( SolidBrush b2 = new SolidBrush( e.ForeColor ))
				{
					e.Graphics.DrawString( this.Text , e.Font , b2 , rect2 , f );
				}
			}
		}
	}//public class ColorMenuItem : System.Windows.Forms.MenuItem
}
