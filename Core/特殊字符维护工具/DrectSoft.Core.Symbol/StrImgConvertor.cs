using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace YidanSoft.Core.Symbol
{
	/// <summary>
	/// StrImgConvertor,�ַ���תͼ�������
	/// </summary>
	public  class StrImgConvertor :ConvertController<StrImgConvertWrap,Image>
	{
		#region fields

		private StrImgCStrategy m_convertStrategy;

		#endregion

		#region ctor
		/// <summary>
		/// ImageConvertor������
		/// </summary>
		/// <param name="convertCore"></param>
		public StrImgConvertor(StrImgCStrategy cStrategy) 
		{
			this.m_convertStrategy = cStrategy;
		}
		#endregion

		#region overrid method
		/// <summary>
		/// ʵ��ConvertController<S,T>�е�DoSTConvert���������Ԥ���ͼ���С���ַ�������ת��Ϊͼ��
		/// </summary>
		/// <param name="origianl"></param>
		/// <returns></returns>
		protected override Image DoSTConvert(StrImgConvertWrap origianl)
		{
			if (m_convertStrategy != null)
				return m_convertStrategy.DoConvert(origianl);
			return null;
		}
		#endregion
	}
}