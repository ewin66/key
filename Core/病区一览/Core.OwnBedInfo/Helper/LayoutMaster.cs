using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraGrid.Views.Card;
using System.Collections.ObjectModel;
using DevExpress.XtraGrid.Views.Base;

namespace DrectSoft.Core.OwnBedInfo
{
    /// <summary>
    /// ��Ļ�ֱ���
    /// </summary>
    public enum ScreenSize
    {
        /// <summary>
        /// С����Ļ(640*480)
        /// </summary>
        SmallScreen = 2,
        /// <summary>
        /// �е���Ļ(800*600)
        /// </summary>
        MiddleScreen = 3,
        /// <summary>
        /// ��׼��Ļ(1024*768)
        /// </summary>
        StandardScreen = 4,
        /// <summary>
        /// ����(1440*900)
        /// </summary>
        WideScreen = 5,
        /// <summary>
        /// �ϴ���Ļ(1280*960)
        /// </summary>
        SuperiorScreen = 6,
        /// <summary>
        /// ������Ļ(1280*1024)
        /// </summary>
        BigScreen = 7,
        /// <summary>
        /// ������Ļ(1600*1200������)
        /// </summary>
        SupramaxilmalScreen = 8
    }
    /// <summary>
    /// ���ֹ�����
    /// </summary>
    public static class LayoutMaster
    {
        private static Dictionary<string, Font> m_CardFont;

        /// <summary>
        /// �Զ�����cardView��Ӧ����
        /// </summary>
        /// <param name="cardView"></param>
        /// <returns></returns>
        internal static Dictionary<string, Font> GetGridFont(BaseView cardView)
        {
            m_CardFont = new Dictionary<string, Font>();
            if (cardView == null)
            {
                return null;
            }
            m_CardFont.Add("FieldCaption", new Font("Tahoma", 10F));
            m_CardFont.Add("FieldValue", new Font("Tahoma", 10F));
            m_CardFont.Add("CardCaption", new Font("����", 15F));
            m_CardFont.Add("FocusedCardCaption", new Font("Tahoma", 15F));
            m_CardFont.Add("SelectedCardCaption", new Font("Tahoma", 15F));
            return m_CardFont;
        }
        private const int OneRowHeight = 19;//19
        private const int OneCardHeight = 105;//105;//xjt
        private const int OneCaptionHeight = 28;
        private const int OnePageRowCount = 5;
        //����һ���������߶�
        private const int PatientBar = 28;
        //��������λ����������ĳ����ϲ��ռ�ĸ߶�
        private const int SolidRegion = 85;
        //ҳ�����߶�
        private const int ToolBarHeight = 27;
        private const int RegionAbovePageBarAndCard = 114;
        private const int RegionBottom = 32;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowCount">��Ƭ����</param>
        /// <param name="bedsCount">��λ����</param>
        /// <param name="isObstetricWard"></param>
        /// <param name="expandedRowsCount"></param>
        /// <returns></returns>
        internal static Action CalcHeight(int rowCount, int bedsCount, bool isObstetricWard, out int expandedRowsCount)
        {
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            int height = Screen.PrimaryScreen.WorkingArea.Height;
            int pages = (bedsCount > (rowCount * 10)) ? 2 : 1;
            int extraHeight = 0;
            expandedRowsCount = 0;
            int extraRowHeight = 0;
            int oneCardHeight = isObstetricWard ? (OneCardHeight + OneRowHeight) : OneCardHeight;

            if (pages == 1)
            {
                //һҳģʽ�������������ù������߼�ȥ�̶����ڵ��ϲ��ռ估�²��ռ�Ϳ�Ƭ��������ÿ����Ƭ�еĿ��ø߶ȣ�3Ϊ����ֵ
                //extraHeight = height - 6 * (rowCount + 1) - PatientBar - ToolBarHeight - SolidRegion - oneCardHeight * rowCount - 3;
                extraHeight = height - 6 * 2 - (rowCount - 1) * 5 - RegionAbovePageBarAndCard - RegionBottom - oneCardHeight * rowCount - 9;
                extraRowHeight = extraHeight / rowCount;
                if (extraRowHeight > OneRowHeight)
                {
                    //������չ������
                    expandedRowsCount = extraRowHeight / OneRowHeight;
                    return Action.ExpandRow;
                }
                if (extraRowHeight < 0)
                    return Action.NotEnoughRegion;
                return Action.NoAction;
            }
            else
            {
                //��ҳ������ģʽ����������ʹ�ù������߼�ȥ�̶����ڵ��ϲ��ռ估�²��ռ�Ϳ�Ƭ�����ҳ���������ĸߣ����ÿ����Ƭ�Ŀ��ø߶�,3Ϊ����ֵ
                //extraHeight = height - 6 * (rowCount + 1) - PatientBar - SolidRegion - ToolBarHeight - ToolBarHeight - oneCardHeight * rowCount - 3;
                extraHeight = height - 6 * 2 - (rowCount - 1) * 5 - RegionAbovePageBarAndCard - RegionBottom - ToolBarHeight - oneCardHeight * rowCount - 3;
                if (extraHeight > 0)
                {
                    if (extraHeight >= oneCardHeight)
                        return Action.NextRowCount;
                    else
                    {
                        extraRowHeight = extraHeight / rowCount;
                        if (extraRowHeight >= OneRowHeight)
                        {
                            //������չ������
                            expandedRowsCount = extraRowHeight / OneRowHeight;
                            return Action.ExpandRow;
                        }
                        return Action.NoAction;
                    }
                }
                else
                    return Action.NotEnoughRegion;
            }
        }

        internal enum Action
        {
            /// <summary>
            /// �޶���
            /// </summary>
            NoAction = 0,
            /// <summary>
            /// ��չ�и�
            /// </summary>
            ExpandRow = 1,
            /// <summary>
            /// ������һ���������ж�
            /// </summary>
            NextRowCount = 2,
            /// <summary>
            /// ����ռ���ʾ
            /// </summary>
            NotEnoughRegion = 3
        }

        private static int ColCount
        {
            get { return _colCount; }
        }
        private static int _colCount;

        /// <summary>
        /// ������
        /// </summary>
        public static int ContainerWidth
        {
            set
            {
                m_ContainerWidth = value;
                if (m_ContainerWidth == 0)
                    m_ContainerWidth = Screen.PrimaryScreen.Bounds.Size.Width;
            }
        }
        private static int m_ContainerWidth;

        /// <summary>
        /// ���ÿ�Ƭ�������Ϳ��
        /// </summary>
        internal static Dictionary<string, int> GetScreenAutoSize(int bedsCount, bool isObstetricWard)
        {
            int rowCardView, colCardView, cardInterval, cardWidth, rowWholeNumber;
            cardInterval = 4;
            rowCardView = 4;
            cardWidth = 170;// (m_PanelWidth / 8) + 1;// ((Screen.PrimaryScreen.Bounds.Size.Width - 10 - (colCardView + 6) * cardInterval) / colCardView) - 1;
            colCardView = (m_ContainerWidth) / cardWidth;
            _colCount = colCardView;
            int expandedRowCount;
            for (int i = OnePageRowCount; ; i++)
            {
                Action action = CalcHeight(i, bedsCount, isObstetricWard, out expandedRowCount);
                if (action == Action.NextRowCount)
                    continue;
                else
                {
                    switch (action)
                    {
                        case Action.NoAction:
                            rowCardView = i;
                            break;
                        case Action.ExpandRow:
                            rowCardView = i;
                            break;
                        case Action.NextRowCount:
                            break;
                        case Action.NotEnoughRegion:
                            rowCardView = i - 1;
                            CalcHeight(rowCardView, bedsCount, isObstetricWard, out expandedRowCount);
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }
            rowWholeNumber = rowCardView * colCardView;
            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("rowCardView", rowCardView);
            result.Add("colCardView", colCardView);
            result.Add("rowWholeNumber", rowWholeNumber);
            result.Add("cardInterval", cardInterval);
            result.Add("cardWidth", cardWidth);
            result.Add("expandedRows", expandedRowCount);
            return result;
        }
    }
}
