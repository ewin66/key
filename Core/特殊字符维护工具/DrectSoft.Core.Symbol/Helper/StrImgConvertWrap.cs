using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace YidanSoft.Core.Symbol
{
	/// <summary>
	/// stringתͼ��Ĳ�������
	/// </summary>
	public struct StrImgConvertWrap
	{
		public string str;
		public Size size;

		/// <summary>
		/// �ṹ������
		/// </summary>
		/// <param name="str"></param>
		/// <param name="size"></param>
		public StrImgConvertWrap(string str,Size size)
		{
			this.str = str;
			this.size = size;
		}
	}
}
