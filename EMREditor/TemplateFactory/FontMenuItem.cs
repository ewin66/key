using System;
using System.Windows.Forms  ;
using System.Collections ;
using System.Drawing ;

namespace XWriterDemo
{
	/// <summary>
	/// ֧��������ʽ�Ĳ˵���Ŀ
	/// </summary>
	public class FontMenuItem : System.Windows.Forms.MenuItem
	{
		/// <summary>
		/// Ĭ������
		/// </summary>
		private static System.Drawing.Font DefaultFont = System.Windows.Forms.Control.DefaultFont ;
			 
		/// <summary>
		/// ��ʼ������
		/// </summary>
		public FontMenuItem()
		{
			this.OwnerDraw = true ;
		}
		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <param name="fontName">��������</param>
		public FontMenuItem( string fontName )
		{
			this.OwnerDraw = true ;
			strFontName = fontName ;
			this.Text = strFontName ;
		}
		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <param name="f">�������</param>
		public FontMenuItem ( System.Drawing.Font f )
		{
			this.OwnerDraw = true ;
			this.Font = f ;
			this.Text = f.Name ;
		}
		/// <summary>
		/// ����
		/// </summary>
		public System.Drawing.Font Font
		{
			get
			{
				return new System.Drawing.Font( strFontName , fFontSize , intFontStyle );
			}
			set
			{
				if( value != null )
				{
					strFontName = value.Name ;
					fFontSize = value.Size ;
					intFontStyle = value.Style ;
				}
			}
		}
		private string strFontName = DefaultFont.Name ;
		/// <summary>
		/// ��������
		/// </summary>
		public string FontName
		{
			get{ return strFontName ;}
			set{ strFontName = value;}
		}

		private float fFontSize = DefaultFont.Size ;
		/// <summary>
		/// �����С
		/// </summary>
		public float FontSize
		{
			get{ return fFontSize ;}
			set{ fFontSize = value;}
		}

		private System.Drawing.FontStyle intFontStyle = System.Drawing.FontStyle.Regular ;
		/// <summary>
		/// ������ʽ
		/// </summary>
		public System.Drawing.FontStyle FontStyle
		{
			get{ return intFontStyle ;}
			set{ intFontStyle = value;}
		}

		private System.Drawing.Color intColor = System.Drawing.Color.Black ;
		/// <summary>
		/// �ı���ɫ
		/// </summary>
		public System.Drawing.Color Color
		{
			get{ return intColor ;}
			set{ intColor = value;}
		}

		private bool bolFontNameOnly = true ;
		/// <summary>
		/// ֻ����������������Ч
		/// </summary>
		public bool FontNameOnly
		{
			get{ return bolFontNameOnly ;}
			set{ bolFontNameOnly = value;}
		}

		private bool bolDialogSelectFont = false;
		/// <summary>
		/// ʹ�öԻ�����ѡ������
		/// </summary>
		public bool DialogSelectFont
		{
			get{ return bolDialogSelectFont ;}
			set{ bolDialogSelectFont = value;}
		}

		/// <summary>
		/// ����˵�����¼�
		/// </summary>
		/// <param name="e">�¼�����</param>
		protected override void OnClick(EventArgs e)
		{
			if( this.bolDialogSelectFont )
			{
				this.bolFontNameOnly = false;
				using( System.Windows.Forms.FontDialog dlg = new FontDialog())
				{
					dlg.Color = this.intColor ;
					dlg.Font = new Font( strFontName , fFontSize , intFontStyle );
					dlg.ShowColor = true ;
					if( dlg.ShowDialog( ) == System.Windows.Forms.DialogResult.OK )
					{
						intColor = dlg.Color ;
						this.Font = dlg.Font ;
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
			using( System.Drawing.Font font = new Font( this.strFontName , fFontSize , intFontStyle ))
			{
				using( System.Drawing.StringFormat f = new StringFormat(
						   System.Drawing.StringFormat.GenericDefault ))
				{
					f.FormatFlags = System.Drawing.StringFormatFlags.NoWrap ;
					System.Drawing.SizeF size = e.Graphics.MeasureString(
						this.Text , font , 1000 , f );
					e.ItemHeight = ( int ) Math.Ceiling( font.GetHeight( e.Graphics )) + 4 ;
					e.ItemWidth = 20 + ( int) Math.Ceiling( size.Width );
				}
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
			using( System.Drawing.Font font = new Font( strFontName , fFontSize , intFontStyle ))
			{
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
					System.Drawing.Color c = intColor ;
					if( e.ForeColor.ToArgb() == System.Drawing.SystemColors.HighlightText.ToArgb())
						c = e.ForeColor ;
					using( SolidBrush b2 = new SolidBrush( c ))
					{
						e.Graphics.DrawString( this.Text , font , b2 , rect2 , f );
					}
				}
			}
		}
	}
}