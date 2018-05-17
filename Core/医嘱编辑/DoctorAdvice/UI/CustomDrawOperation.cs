using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DrectSoft.Common.Eop;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Reflection;
using DevExpress.XtraGrid.Columns;

namespace DrectSoft.Core.DoctorAdvice
{
   public static class CustomDrawOperation
   {
      #region properties
      /// <summary>
      /// �й�CustomDraw������
      /// </summary>
      public static GridCustomDrawSetting DrawSetting
      {
         get { return CoreBusinessLogic.CustomSetting.CustomDrawSetting; }
      }

      /// <summary>
      /// �й�Grid������
      /// </summary>
      public static OrderGridSetting GridSetting
      {
         get { return CoreBusinessLogic.CustomSetting.GridSetting; }
      }
      #endregion

      #region private methods
      /// <summary>
      /// stringListת��Ϊ�ַ���
      /// </summary>
      /// <param name="list"></param>
      /// <param name="startIndex"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      private static string ListToString(Collection<string> list, int startIndex, int length)
      {
         StringBuilder result = new StringBuilder();
         for (int index = startIndex; index < (startIndex + length); index++)
            result.Append(list[index]);
         return result.ToString();
      }

      /// <summary>
      /// ���ݴ������Ϣ���������Ϣ�ṹ��(����ͬһ�е����ݽ��ϲ���һ��)
      /// </summary>
      /// <param name="texts"></param>
      /// <param name="widthes"></param>
      /// <param name="font"></param>
      /// <param name="count">��ʾ��Ԫ�ظ���</param>
      /// <param name="startIndexOfLine2"></param>
      /// <param name="outputWidth"></param>
      /// <returns></returns>
      private static Collection<OutputInfoStruct> CreateOutputList(Collection<string> texts, Font font, int count, int startIndexOfLine2, int outputWidth)
      {
         // ͬһ�е����ݺϲ���һ�����,�������������ڵ�Ԫ��ĳߴ�
         Collection<OutputInfoStruct> results = new Collection<OutputInfoStruct>();

         if (startIndexOfLine2 == 0) // ֻ��һ������, ������ʾ
         {
            int fontHeight = Convert.ToInt32(font.GetHeight());
            results.Add(new OutputInfoStruct(
                            ListToString(texts, 0, count)
                          , new Rectangle(DrawSetting.Margin.Left
                                         , DrawSetting.Margin.Top
                                          + ((DrawSetting.OutputSizeOfContent.Height - fontHeight) / 2)
                                         , outputWidth
                                         , fontHeight)
                          , font
                          , OrderOutputTextType.NormalText));
         }
         else
         {
            // 0 �� (startIndexOfLine2 - 1) ��������ʾ���ϰ벿
            results.Add(new OutputInfoStruct(
                            ListToString(texts, 0, startIndexOfLine2)
                          , new Rectangle(DrawSetting.Margin.Left
                                         , DrawSetting.Margin.Top
                                         , outputWidth
                                         , DrawSetting.OutputSizeOfContent.Height / 2)
                          , font
                          , OrderOutputTextType.NormalText));
            // startIndexOfLine2 �� ��� ��������ʾ���°벿
            results.Add(new OutputInfoStruct(
                            ListToString(texts, startIndexOfLine2
                                        , count - startIndexOfLine2)
                          , new Rectangle(DrawSetting.Margin.Left
                                         , DrawSetting.Margin.Top
                                          + (DrawSetting.OutputSizeOfContent.Height / 2)
                                         , outputWidth
                                         , DrawSetting.OutputSizeOfContent.Height / 2)
                          , font
                          , OrderOutputTextType.NormalText));
         }

         return results;
      }

      /// <summary>
      /// ��������ı���ָ�������µĿ��(������Ϊ��λ)
      /// </summary>
      /// <param name="widthes"></param>
      /// <param name="texts"></param>
      /// <param name="font"></param>
      private static void CalcTextsWidth(List<int> widthes, Collection<string> texts, Font font)
      {
         widthes.Clear();
         for (int index = 0; index < texts.Count; index++)
            widthes.Add(CalcStringWidth(texts[index], font));
      }

      /// <summary>
      /// �����ַ����ڸ��������µĿ��
      /// </summary>
      /// <param name="text"></param>
      /// <param name="font"></param>
      private static int CalcStringWidth(string text, Font font)
      {
         return TextRenderer.MeasureText(text, font).Width;
      }

      /// <summary>
      /// ��ȡָ�����ͷ����ǵ�������ݽṹ��
      /// </summary>
      /// <param name="groupType">�����ǵ�����</param>
      /// <param name="groupTextWidth">���鹫����Ϣ�Ŀ��</param>
      /// <returns></returns>
      private static OutputInfoStruct GetGroupFlagOutput(OrderOutputTextType groupType, int groupTextWidth)
      {
         Rectangle bounds = new Rectangle();
         int startPosition = GridSetting.WidthOfContentCell
            - DrawSetting.Margin.Right - groupTextWidth - DrawSetting.GroupFlagWidth + 1;
         int flagWidth = DrawSetting.GroupFlagWidth - 3;
         switch (groupType)
         {
            case OrderOutputTextType.GroupStart:
               bounds = new Rectangle(startPosition
                  , GridSetting.RowHeight / 2
                  , flagWidth
                  , GridSetting.RowHeight / 2);
               break;
            case OrderOutputTextType.GroupMiddle:
               bounds = new Rectangle(startPosition, 0
                  , flagWidth, GridSetting.RowHeight);
               break;
            case OrderOutputTextType.GroupEnd:
               bounds = new Rectangle(startPosition, 0
                  , flagWidth, GridSetting.RowHeight / 2);
               break;
         }

         return new OutputInfoStruct("", bounds, DrawSetting.DefaultFont.Font, groupType);
      }

      /// <summary>
      /// ��ȡ���鹫����Ϣ��������ݽṹ��
      /// </summary>
      /// <param name="groupText">���鹫����Ϣ</param>
      /// <param name="groupTextWidth">���鹫����Ϣ�Ŀ��</param>
      /// <returns></returns>
      private static OutputInfoStruct GetGroupPublicOutput(string groupText, int groupTextWidth)
      {
         // �����������ֱ����
         int fontHeight = Convert.ToInt32(DrawSetting.DefaultFont.Font.GetHeight());
         return new OutputInfoStruct(groupText
            , new Rectangle(GridSetting.WidthOfContentCell - DrawSetting.Margin.Right - groupTextWidth
                           , (GridSetting.RowHeight - fontHeight) / 2
                           , CalcStringWidth(groupText, DrawSetting.DefaultFont.Font)
                           , fontHeight)
            , DrawSetting.DefaultFont.Font
            , OrderOutputTextType.NormalText);
         //// �÷���Ƶ�ν����ŷ����־����ֱ����
         //int fontHeight = Convert.ToInt32(DefaultFont.GetHeight());
         //return new OutputInfoStruct(groupText
         //   , new Rectangle(groupTextWidth
         //                  , (CellSize.Height - fontHeight) / 2
         //                  , CalcStringWidth(groupText, DefaultFont)
         //                  , fontHeight)
         //   , DefaultFont
         //   , OrderOutputTextType.NormalText);
      }

      private static string[] ProcessFirstElement(string text, Font font, int maxWidth)
      {
         int moreWidth = CalcStringWidth(text, font) - maxWidth;
         int index = text.Length - 1;
         StringBuilder first = new StringBuilder();
         StringBuilder second = new StringBuilder();

         if (moreWidth > 0)
         {
            for (; index >= 0; index--)
            {
               second.Insert(0, text[index]);
               if (CalcStringWidth(second.ToString(), font) >= moreWidth)
                  break;
            }
            index--;
         }
         for (int start = 0; start <= index; start++)
            first.Append(text[start]);

         return new string[2] { first.ToString(), second.ToString() };
      }

      /// <summary>
      /// ����һ����������
      /// </summary>
      /// <param name="outputsTexts"></param>
      /// <param name="maxWidth"></param>
      /// <returns></returns>
      private static Collection<OutputInfoStruct> CreateNormalOutputInfos(Collection<string> outputTexts, int maxWidth)
      {
         Font font = DrawSetting.DefaultFont.Font;// �������ʱʹ�õ����壬��ʼ��Ĭ�����忪ʼ
         float fontSize = font.Size;            // ���������ֺţ���ʼ��Ĭ���ֺſ�ʼ
         List<int> widthes = new List<int>();   // ������ݵ��ı����
         int totalWidth;                        // �����Ⱥ͵���ʱ����
         int lineNum;                           // ��¼�ڵ�ǰ�ֺ��¿�����ʾ�������ݣ�������У�
         int startIndexOfLine2 = 0;             // �ڶ������ݵ�һ��Ԫ�ص�������
         int nextIndex;                         // ��Ϊѭ������������ʱ����
         int maxIndex;                          // �ڴ�����֮ǰ�����ݿ�����ʾ�����������
         bool hadSplited = false;               // ��ǵ�һ��Ԫ���Ƿ񱻷ֳ�����
         string[] firstElements;

         do
         {
            // ���ȶԵ�һ��Ԫ�ؽ��д���, ����ڵ�ǰ�����²�����ȫ���ڵ�һ�У������µ����ݷŵ���һ��
            if (hadSplited)
            {
               firstElements = ProcessFirstElement(outputTexts[0] + outputTexts[1]
                  , font, maxWidth); // �Ѳ�֣�����ǰ����Ԫ����϶���
               outputTexts.RemoveAt(1);
               hadSplited = false;
            }
            else
               firstElements = ProcessFirstElement(outputTexts[0], font, maxWidth);
            outputTexts[0] = firstElements[0];
            if (firstElements[1].Length > 0)
            {
               outputTexts.Insert(1, firstElements[1]);
               hadSplited = true;
            }

            // �Ե�ǰ�������ÿ���ı��Ŀ��
            CalcTextsWidth(widthes, outputTexts, font);

            // �ж��Ƿ����۳�������ʾ
            if (DrawSetting.OutputSizeOfContent.Height >= (font.GetHeight() * 2))
               lineNum = 2;
            else
               lineNum = 1;
            // �������������Ԫ�أ�ֱ���ﵽ�еļ���
            nextIndex = 0;
            maxIndex = 0;
            for (int lineHandle = 0; lineHandle < lineNum; lineHandle++) // �������
            {
               totalWidth = 0;
               startIndexOfLine2 = nextIndex; // ��¼ÿ�е���ʼ������
               for (; nextIndex < widthes.Count; nextIndex++)
               {
                  totalWidth += widthes[nextIndex];
                  if (totalWidth > maxWidth) // ���뵱ǰԪ��ʱ��������һ�еĿ��
                  {
                     // ÿ������Ҫ��ʾһ�����ݣ��Ա����һ��Ԫ��̫�����Ӷ�������ʾ����Ϣ̫��
                     if (nextIndex != startIndexOfLine2)
                        break;
                  }
               }
               maxIndex = nextIndex;
            }
            // �ж������Ƿ���ʣ��
            if (maxIndex == widthes.Count)
               break;

            // �����������û���£����Ѿ��ﵽ��С���壬������һ�������滻��"��"
            // ������С������壬�ٴν��м���
            if (fontSize == DrawSetting.MinFontSize)
            {
               if (maxIndex > 1) // �����滻��һ��Ԫ��
                  outputTexts[maxIndex - 1] = "��";
               break;
            }

            fontSize -= 0.5F;
            font = new Font(DrawSetting.DefaultFont.FontFamily, fontSize);
         } while (fontSize >= DrawSetting.MinFontSize); // Ӧ���ڴ�����δ����ǰ�����˳�ѭ��

         return CreateOutputList(outputTexts, font, maxIndex, startIndexOfLine2, maxWidth);
      }

      private static GridViewInfo GetGridViewInfo(GridView view)
      {
         FieldInfo fi;
         fi = typeof(GridView).GetField("fViewInfo", BindingFlags.NonPublic | BindingFlags.Instance);
         return fi.GetValue(view) as GridViewInfo;
      }
      #endregion

      #region public methods
      /// <summary>
      /// ���ݴ�����ı���Ϣ�����������Ϣ�б�(����)
      /// </summary>
      /// <param name="texts"></param>
      /// <returns></returns>
      public static Collection<OutputInfoStruct> CreateOutputeInfo(Collection<OutputInfoStruct> texts)
      {
         if ((texts == null) || (texts.Count == 0))
            return new Collection<OutputInfoStruct>();

         int maxWidth = DrawSetting.OutputSizeOfContent.Width; // ��һ����Ϣ����������(�з�����޷���ʱ�ǲ�һ����)
         Collection<string> outputTexts = new Collection<string>();  // ʵ����Ҫ������ı�
         OrderOutputTextType groupType = OrderOutputTextType.NormalText; // �������ͣ�NormalText��ʾ�Ƿ����¼
         string usageText = "";                          // �÷���Ϣ
         string frequencyText = "";                      // Ƶ����Ϣ
         string groupText = "";                          // ����ʱ����Ĺ�����Ϣ 
         int groupTextWidth = 0;                         // ������Ϣ��Ĭ�������µĿ��
         int insertPos = -1;                             // δ����ʱ���������List�в����÷���Ƶ��
         string cancelText = "";                         // ȡ����Ϣ
         OutputInfoStruct info;

         for (int index = 0; index < texts.Count; index++)
         {
            info = texts[index];
            switch (info.OutputType)
            {
               case OrderOutputTextType.GroupStart:
               case OrderOutputTextType.GroupMiddle:
               case OrderOutputTextType.GroupEnd:
                  groupType = info.OutputType;
                  break;
               case OrderOutputTextType.CancelInfo:
                  cancelText = info.Text;
                  break;
               case OrderOutputTextType.ItemUsage:
                  insertPos = index; // �÷��϶���Ƶ��ǰ��
                  usageText = info.Text;
                  break;
               case OrderOutputTextType.ItemFrequency:
                  frequencyText = info.Text;
                  break;
               default:
                  outputTexts.Add(info.Text);
                  break;
            }
         }

         // �ж��Ƿ��Ƿ���ļ�¼(ͬ����÷���Ƶ��ֻ��ʾһ��), Ȼ����ȡ��Ҫ������ı�
         if (groupType != OrderOutputTextType.NormalText)
         {
            groupText = usageText + frequencyText.Trim();
            groupTextWidth = CalcStringWidth(groupText, DrawSetting.DefaultFont.Font);
            // ���¼���һ����Ϣ�������������ȣ��ڴ�����һ����Ϣ���ٴ��������Ϣ�����
            maxWidth = maxWidth - groupTextWidth - DrawSetting.GroupFlagWidth;
         }
         else
         {
            if (insertPos > 0)
            {
               outputTexts.Insert(insertPos, usageText);
               outputTexts.Insert(insertPos + 1, frequencyText);
            }
            else
            {
               if (usageText.Length > 0)
                  outputTexts.Add(usageText);
               if (frequencyText.Length > 0)
                  outputTexts.Add(frequencyText);
            }
         }

         // ��������һ����Ϣ�����
         Collection<OutputInfoStruct> results = CreateNormalOutputInfos(outputTexts, maxWidth);

         // ���������Ϣ
         if (groupType != OrderOutputTextType.NormalText)
         {
            // ԭ�ȵ��뷨���÷�����Ϣ������ǰ�����Ϣ��������Ϊÿһ����¼�ǵ�������
            // ͬһ��ļ�¼������־��λ�ÿ��ܴ�λ����˸ĳɴ��ҵ�����ʾ������Ϣ

            // �ȴ�������Ϣ(ֻ����ĵ�һ����¼����Ҫ����Ĺ�����Ϣ)
            if (groupType == OrderOutputTextType.GroupStart)
               results.Add(GetGroupPublicOutput(groupText, groupTextWidth));
            // �ٻ�������
            results.Add(GetGroupFlagOutput(groupType, groupTextWidth));

            //maxWidth = 0;
            //foreach (OutputInfoStruct text in results)
            //   if (maxWidth < text.Bounds.Width)
            //      maxWidth = text.Bounds.Width;

            //// �����־�������������
            //results.Add(GetGroupFlagOutput(groupType, maxWidth));
            //// ֻ����ĵ�һ����¼����Ҫ����Ĺ�����Ϣ
            //if (groupType == OrderOutputTextType.GroupStart)
            //   results.Add(GetGroupPublicOutput(groupText, maxWidth + GroupFlagWidth));
         }
         // ����ȡ����Ϣ
         if (cancelText.Length > 0)
            results.Add(new OutputInfoStruct(cancelText, DrawSetting.BoundsOfCancel
               , DrawSetting.FontOfCancel.Font
               , OrderOutputTextType.CancelInfo));

         return results;
      }

      /// <summary>
      /// ����ҽ��״̬���Ƿ�ѡ�е���Ϣ��ȡǰ��ɫ
      /// </summary>
      /// <param name="orderState">ҽ��״̬</param>
      /// <param name="isSelected">�Ƿ�ѡ��</param>
      /// <param name="hadSynched">ҽ�������Ƿ���ͬ��(δͬ��ʱ�е�������ɫ����)</param>
      /// <returns></returns>
      public static Color GetForeColorByState(OrderState orderState, bool isSelected, bool hadSynched)
      {
         if (hadSynched)
         {
            switch (orderState)
            {
               case OrderState.New:
                  if (isSelected)
                     return DrawSetting.NewOrderColor.SelectedColor.ForeColor;
                  else
                     return DrawSetting.NewOrderColor.NormalColor.ForeColor;
               case OrderState.Audited:
                  if (isSelected)
                     return DrawSetting.AuditedColor.SelectedColor.ForeColor;
                  else
                     return DrawSetting.AuditedColor.NormalColor.ForeColor;
               case OrderState.Cancellation:
                  if (isSelected)
                     return DrawSetting.CancelledColor.SelectedColor.ForeColor;
                  else
                     return DrawSetting.CancelledColor.NormalColor.ForeColor;
               case OrderState.Executed:
                  if (isSelected)
                     return DrawSetting.ExecutedColor.SelectedColor.ForeColor;
                  else
                     return DrawSetting.ExecutedColor.NormalColor.ForeColor;
               case OrderState.Ceased:
                  if (isSelected)
                     return DrawSetting.CeasedColor.SelectedColor.ForeColor;
                  else
                     return DrawSetting.CeasedColor.NormalColor.ForeColor;
               default:
                  if (isSelected)
                     return DrawSetting.DefaultColor.SelectedColor.ForeColor;
                  else
                     return DrawSetting.DefaultColor.NormalColor.ForeColor;
            }
         }
         else
         {
            if (isSelected)
               return DrawSetting.NotSynchColor.SelectedColor.ForeColor;
            else
               return DrawSetting.NotSynchColor.NormalColor.ForeColor;
         }
      }

      /// <summary>
      /// ����ҽ��״̬���Ƿ�ѡ�е���Ϣ��ȡ����ɫ
      /// </summary>
      /// <param name="orderState">ҽ��״̬</param>
      /// <param name="isSelected">�Ƿ�ѡ��</param>
      /// <param name="hadSynched">ҽ�������Ƿ���ͬ��(δͬ��ʱ�е�������ɫ����)</param>
      /// <returns></returns>
      public static Color GetBackColorByState(OrderState orderState, bool isSelected, bool hadSynched)
      {
         if (hadSynched)
         {
            switch (orderState)
            {
               case OrderState.New:
                  if (isSelected)
                     return DrawSetting.NewOrderColor.SelectedColor.BackColor;
                  else
                     return DrawSetting.NewOrderColor.NormalColor.BackColor;
               case OrderState.Audited:
                  if (isSelected)
                     return DrawSetting.AuditedColor.SelectedColor.BackColor;
                  else
                     return DrawSetting.AuditedColor.NormalColor.BackColor;
               case OrderState.Cancellation:
                  if (isSelected)
                     return DrawSetting.CancelledColor.SelectedColor.BackColor;
                  else
                     return DrawSetting.CancelledColor.NormalColor.BackColor;
               case OrderState.Executed:
                  if (isSelected)
                     return DrawSetting.ExecutedColor.SelectedColor.BackColor;
                  else
                     return DrawSetting.ExecutedColor.NormalColor.BackColor;
               case OrderState.Ceased:
                  if (isSelected)
                     return DrawSetting.CeasedColor.SelectedColor.BackColor;
                  else
                     return DrawSetting.CeasedColor.NormalColor.BackColor;
               default:
                  if (isSelected)
                     return DrawSetting.DefaultColor.SelectedColor.BackColor;
                  else
                     return DrawSetting.DefaultColor.NormalColor.BackColor;
            }
         }
         else
         {
            if (isSelected)
               return DrawSetting.NotSynchColor.SelectedColor.BackColor;
            else
               return DrawSetting.NotSynchColor.NormalColor.BackColor;
         }
      }

      public static Image CreateColorLegend(Color backColor, Color foreColor, string text)
      {
         Font font = new Font("SimSun", 9f); 
         int textWidth = CalcStringWidth(text, font);
         Bitmap legend = new Bitmap(textWidth, 16);
         
         Graphics gp = Graphics.FromImage(legend);
         // �Ȼ�����ɫ�Ŀ������(��ΪDxBarItem���Զ���͸��ɫ������ͼ���Ϳ�������)
         Color penColor = Color.Beige; // Color.FromArgb(backColor.A, 255 - backColor.R, 255 - backColor.G, 255 - backColor.B);
         gp.DrawRectangle(new Pen(penColor, 1), 0, 0, textWidth, 16); 
         gp.FillRectangle(new SolidBrush(backColor), 1, 1, textWidth - 1, 15);
         gp.DrawString(text, font, new SolidBrush(foreColor), 2, 2);

         return legend;
      }

      public static Rectangle GetGridCellRect(GridView view, int rowHandle, GridColumn column)
      {
         GridViewInfo info = GetGridViewInfo(view);
         GridCellInfo cell = info.GetGridCellInfo(rowHandle, column);
         if (cell != null)
            return cell.Bounds;
         return Rectangle.Empty;
      }

      #endregion
   }
}
