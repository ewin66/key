using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using YidanSoft.Common.Controls;

namespace YidanSoft.Core.Symbol
{
	/// <summary>
	/// Rtf�ַ�������ͼ��ת��������ĵĳ�����
	/// </summary>
	public abstract class StrImgCStrategy :ConvertController<StrImgConvertWrap,Bitmap>
	{
		/// <summary>
		/// ����Ԥ��ͼ���С��string��image��ת������
		/// </summary>
		/// <param name="origianl"></param>
		/// <returns></returns>
		protected override Bitmap DoSTConvert(StrImgConvertWrap origianl)
		{
			return DoStrImgConvert(origianl);
		}
		/// <summary>
		/// ����Ԥ��ͼ���С��string��image��ת������
		/// </summary>
		/// <param name="origianl"></param>
		/// <returns></returns>
		protected abstract Bitmap DoStrImgConvert(StrImgConvertWrap origianl);


		#region assist method
		/// <summary>
		/// Calculate or render the contents of our RichTextBox for printing
		/// </summary>
		/// <param name="rtb"></param>
		/// <param name="measureOnly"></param>
		/// <param name="graph"></param>
		/// <param name="renderRect">������򣬵�λ��pixel</param>
		/// <returns>RTF�Ƿ��Ѿ��������</returns>
		protected  bool FormatRangeCore(RichTextBox rtb, bool measureOnly, Graphics graph, Rectangle renderRect)
		{
			int charindex = 0;
			charindex = RtfPrintNativeMethods.FormatRange(rtb, measureOnly, graph, 
				renderRect, renderRect, 0, rtb.TextLength);
			if (charindex < rtb.TextLength)
				return false;
			return true;
		}


		/// <summary>
		/// DoRenderZoom
		/// </summary>
		/// <param name="zoomTarget"></param>
		/// <param name="zoomFactorW"></param>
		/// <param name="zoomFactorH"></param>
		/// <returns></returns>
		protected  Rectangle DoRenderZoom(Rectangle zoomTarget, float zoomFactorW,float zoomFactorH)
		{
			int incW = (int)(zoomTarget.Width * 
				(zoomFactorW - 1));
			int incH = (int)(zoomTarget.Height *
				(zoomFactorH - 1));
			return Rectangle.Inflate(zoomTarget, incW, incH);
		}
		#endregion
	}
}
