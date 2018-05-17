using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using YidanSoft.Common.Object2Editor.Encoding;

namespace YidanSoft.Core.Symbol
{
    /// <summary>
    /// Rtf2Imageת������-API����
    /// </summary>
    public class Rtf2ImgStrategyA : StrImgCStrategy
    {
        #region const and fields
        private const float MinFontSize = 6;

        private RichTextBox tempRichBox;

        #endregion

        #region ctor
        /// <summary>
        /// Rtf2ImageCore������
        /// </summary>
        public Rtf2ImgStrategyA()
        {
            tempRichBox = new RichTextBox();
        }
        #endregion

        #region override methods
        /// <summary>
        /// ʵ��StrImgCStrategy�е�DoStrImgConvert
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected override Bitmap DoStrImgConvert(StrImgConvertWrap wrap)
        {
            //����RTF��ͼ
            if (wrap.str.StartsWith(RtfEncoding.RtfFlag))
                tempRichBox.Rtf = wrap.str;
            else
                tempRichBox.SelectedText = wrap.str;
            //����ͼ��
            return Format2Image(tempRichBox, wrap.size);
        }

        #endregion

        #region private method
        /// <summary>
        /// RTF�ı���ת�ƶ���Сͼ��
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="renderSize">�������������ߴ硣��λ��pixel</param>
        /// <returns></returns>
        private Bitmap Format2Image(RichTextBox rtb, Size renderSize)
        {
            Bitmap map = new Bitmap(renderSize.Width, renderSize.Height);
            //ʵ�ֻ���
            using (Graphics graph = Graphics.FromImage(map))
            {
                //����׼��
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                //���Ʋ�����������������Ӧ��Ⱦ����
                Rectangle zRenderRect = new Rectangle(new Point(0, 0),
                    renderSize);
                float currentFontSize = zRenderRect.Height / 2;
                do
                {
                    tempRichBox.SelectAll();
                    //RtfPrintNativeMethods.SetSelectionSize(tempRichBox, currentFontSize);
                    //����ʵ����Ⱦ�����С
                    zRenderRect.Height = tempRichBox.SelectionFont.Height;
                    if (FormatRangeCore(tempRichBox, true, graph, zRenderRect) ||
                        currentFontSize < MinFontSize)
                        break;
                    // ��С�������
                    currentFontSize -= 0.5f;
                } while (true);
                //ʵ�ʻ���
                zRenderRect.Offset(
                    0, (renderSize.Height - tempRichBox.SelectionFont.Height) / 2);//����ʵ����Ⱦλ��
                FormatRangeCore(rtb, false, graph, zRenderRect);
            }
            //͸��������
            map.MakeTransparent(Color.White);
            return map;
        }
        #endregion

        protected Rectangle DoScaleZoom(Rectangle zoomTarget, float zoomFactor)
        {
            return base.DoRenderZoom(zoomTarget, zoomFactor, zoomFactor);
        }
    }
}