using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace YidanSoft.Core.Symbol
{
	/// <summary>
	/// Emr�����������
	/// </summary>
	 public class EmrSymbolEngine
	 {

		 #region static methods
		 /// <summary>
		 /// PrintImage,��RichTextת��Ϊͼ��
		 /// </summary>
		 /// <param name="textbox"></param>
		 /// <returns></returns>
		 public static Image PrintRTFImage(RichTextBox textbox)
		 {
			 return PrintRTFImage(textbox.Rtf,textbox.Size);
		 }
		 /// <summary>
		 /// PrintImage,��RichTextStringת��Ϊͼ��
		 /// </summary>
		 /// <param name="rtf"></param>
		 /// <returns></returns>
		 public static Image PrintRTFImage(string rtf,Size size) 
		 {
			 try
			 {
				 return m_sIConvertor.DoConvert(
					 new StrImgConvertWrap(rtf, size));
			 }
			 catch { return null; }
		 }
		 /// <summary>
		 /// EjectorString,���RichText������Ϣ
		 /// </summary>
		 /// <param name="textbox"></param>
		 /// <returns></returns>
		 public static string EjectorRTFString(RichTextBox textbox)
		 {
			 return EjectorRTFString(textbox.Rtf);
		 }
		 /// <summary>
		 ///  EjectorString,���RichTextString������Ϣ
		 /// </summary>
		 /// <param name="rtf"></param>
		 /// <returns></returns>
		 public static string EjectorRTFString(string rtf) 
		 {
			 try
			 {
				 return m_sEjector.DoConvert(rtf);
			 }
			 catch { return string.Empty; }
		 }
		 #endregion 

		 #region fields
		 private static StrImgConvertor m_sIConvertor = new StrImgConvertor(new Rtf2ImgStrategyA());
		 private static StringEjector m_sEjector = new StringEjector();
		 #endregion 
	 }
}