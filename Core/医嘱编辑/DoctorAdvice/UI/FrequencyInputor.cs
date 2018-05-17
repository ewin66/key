using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DrectSoft.Common.Eop;
using DrectSoft.Common.Library;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DrectSoft.Wordbook;

namespace DrectSoft.Core.DoctorAdvice
{
   public partial class FrequencyInputor : XtraUserControl
   {
      #region properties
      /// <summary>
      /// ��ǰ�����ҽ��Ƶ��
      /// </summary> 
      [Browsable(false),
       ReadOnly(true)]
      public OrderFrequency Frequency
      {
         get 
         {
            if (_showListWindow != null)
            {
               if (frequencyInput.HadGetValue && (frequencyInput.PersistentObjects.Count > 0))
                  return frequencyInput.PersistentObjects[0] as OrderFrequency;
            }
            return null;
         }
         set
         {
            if (value != null)
               SetFrequency(value.Code);
            else
               SetFrequency("");
            //_currentFrequency = value;
         }
      }
      //private OrderFrequency _currentFrequency;

      /// <summary>
      /// 
      /// </summary>
      [Browsable(false),
       ReadOnly(true)]
      public bool HadGetValue
      {
         get { return frequencyInput.HadGetValue; }
      }

      /// <summary>
      /// Ƶ���ֵ�
      /// </summary>
      [Browsable(false),
       ReadOnly(true)]
      public BaseWordbook FrequencyBook
      {
         get { return frequencyInput.NormalWordbook; }
         set { frequencyInput.NormalWordbook = value; }
      }

      /// <summary>
      /// 
      /// </summary>
      [Browsable(false),
       ReadOnly(true)]
      public bool ShowFormImmediately
      {
         get { return frequencyInput.ShowFormImmediately; }
         set { frequencyInput.ShowFormImmediately = value; }
      }

      /// <summary>
      /// ����ѡ�񴰿�
      /// </summary>
      public LookUpWindow LookUpWindow
      {
         get { return _showListWindow; }
         set 
         { 
            _showListWindow = value;
            frequencyInput.ListWindow = value;
         }
      }
      private LookUpWindow _showListWindow;
      #endregion

      #region private variables
      private bool m_IsInit;
      #endregion

      #region ctor
      public FrequencyInputor()
      {
         InitializeComponent();
         if (!DesignMode)
            InitializeRuntimeUI();
      }
      #endregion

      #region public methods
      /// <summary>
      /// 
      /// </summary>
      /// <param name="frequencyCode"></param>
      public void SetFrequency(string frequencyCode)
      {
         if (!String.IsNullOrEmpty(frequencyCode))
            frequencyInput.CodeValue = frequencyCode;
         else
            frequencyInput.CodeValue = "";
         //_currentFrequency = value;
         ResetFrequencyDetailUI();
      }
      #endregion

      #region private methods
      private void InitializeRuntimeUI()
      {
         m_IsInit = true;
         // �����ؼ�λ�ã���������
         panelContainer.Location = new Point(0, 0);
         this.Size = panelContainer.Size;
         m_IsInit = false;
      }

      /// <summary>
      /// ���ݵ�ǰѡ�е�Ƶ�Σ�����Ƶ�ε���ϸ��Ϣ
      /// </summary>
      private void ResetFrequencyDetailUI()
      {
         // ִ�����ڣ�
         //    ֻ��"��"��Ч
         //    ���Ԥ���ִ�����ڲ���������(������,����������),��Ԥ������ƽ���ƶ�,�Զ�����������(������)
         //    �����ֹ�����,ѡ���������������ִ����������
         // ִ��ʱ��:
         //    ֻ��"��""��"��Ч
         //    �����ֹ�����,ѡ���ʱ�����������ִ�д�������
         //    --����"Сʱ",Ҫ����ִ�����ڿ���ʱ����

         // �����ÿؼ��ĳ�ʼ���ݣ������ý�������
         UncheckListBox(listBoxWeekDay);
         UncheckListBox(listBoxHour);

         listBoxWeekDay.Enabled = false;
         listBoxHour.Enabled = false;

         if (Frequency != null)
         {
            if (Frequency.PeriodUnitFlag == OrderExecPeriodUnitKind.Week)
            {
               ResetWeedDayListBox(Frequency.WeekDays);
               listBoxWeekDay.Enabled = true;
            }

            if ((Frequency.PeriodUnitFlag == OrderExecPeriodUnitKind.Week)
               || ((Frequency.PeriodUnitFlag == OrderExecPeriodUnitKind.Day)))
            {
               ResetHourListBox(Frequency.ExecuteTime);
               listBoxHour.Enabled = true;
            }
            frequencyDetail.Text = Frequency.Text;
         }
         else
            frequencyDetail.Text = "";

         frequencyDetail.Enabled = listBoxHour.Enabled;
      }

      /// <summary>
      /// ����Ƶ����ϸ��Ϣ�����޸ĵ�ǰƵ�����ú����)
      /// </summary>
      /// <returns>Ƶ�ε���ϸ��Ϣ</returns>
      private string CheckFrequencyDetail()
      {
         // �����ж�ѡ�е�ִ�����ڡ�ʱ�������Ƿ񳬹�Ƶ�ι涨�������������Ļ������ĩλ��ʼȥ�������ѡ��
         // ��ִ�����ں�ִ��ʱ��ѡ��ͬ����Ƶ�ζ���Ȼ����ʾƵ������
         if (Frequency != null)
         {
            StringBuilder warningMsg = new StringBuilder();
            int moreCount;
            // ִ�����ڵ�λ����ʱ�����ѡ�е�ִ�����������Ƿ񳬹�Ĭ��ֵ
            if (Frequency.PeriodUnitFlag == OrderExecPeriodUnitKind.Week)
            {
               moreCount = listBoxWeekDay.CheckedIndices.Count - Frequency.Period;
               if (moreCount > 0)
               {
                  UncheckListBox(listBoxWeekDay, moreCount);
                  warningMsg.AppendLine(ConstMessages.CheckSelectedTooManyExecuteDate);
               }
               else if (moreCount < 0)
               {
                  ResetWeedDayListBox(Frequency.WeekDays);
                  warningMsg.AppendLine(ConstMessages.CheckSelectedTooFewExecuteDate);
               }
            }

            // ���ڵ�λ���ܡ���ʱ�����ѡ�е�ִ��ʱ�����Ƿ񳬹�Ĭ��ֵ
            if ((Frequency.PeriodUnitFlag == OrderExecPeriodUnitKind.Week)
               || ((Frequency.PeriodUnitFlag == OrderExecPeriodUnitKind.Day)))
            {
               moreCount = listBoxHour.CheckedIndices.Count - Frequency.ExecuteTimesPerPeriod;
               if (moreCount > 0)
               {
                  UncheckListBox(listBoxHour, moreCount);
                  warningMsg.AppendLine(ConstMessages.CheckSelectedTooManyExecuteTime);
               }
               else if (moreCount < 0)
               {
                  ResetHourListBox(Frequency.ExecuteTime);
                  warningMsg.AppendLine(ConstMessages.CheckSelectedTooFewExecuteTime);
               }
            }

            Frequency.WeekDays = CombExecuteDay();
            Frequency.ExecuteTime = CombExecuteTime();

            frequencyDetail.ErrorText = warningMsg.ToString();
            return Frequency.Text;
         }
         else
         {
            return "";
         }
      }

      /// <summary>
      /// ��ָ��CheckListBox�е�������Ŀ��ΪUnchecked
      /// </summary>
      /// <param name="listBox"></param>
      private static void UncheckListBox(CheckedListBoxControl listBox)
      {
         foreach (CheckedListBoxItem item in listBox.Items)
            item.CheckState = CheckState.Unchecked;
      }

      /// <summary>
      /// ��ָ��CheckListBox�е����n��ѡ�е���Ŀ��ΪUnchecked
      /// </summary>
      /// <param name="listBox"></param>
      /// <param name="lastCount"></param>
      private static void UncheckListBox(CheckedListBoxControl listBox, int lastCount)
      {
         int count = 0;
         for (int i = listBox.Items.Count - 1; i >= 0; i--)
         {
            if (listBox.Items[i].CheckState != CheckState.Checked)
               continue;

            listBox.Items[i].CheckState = CheckState.Unchecked;
            count++;
            if (count == lastCount)
               break;
         }
      }

      /// <summary>
      /// �ô�����ַ�����ʼ������ѡ��ListBox��
      /// </summary>
      /// <param name="days">��Ӧ�������е�ĳ�������϶��ɵ��ַ���</param>
      private void ResetWeedDayListBox(string days)
      {
         foreach (char dw in days)
         {
            if ((dw < 49) || (dw > 55))
               continue;

            listBoxWeekDay.Items[dw - 49].CheckState = CheckState.Checked;
         }
      }

      /// <summary>
      /// ��ѡ�е�ִ��������ϳ��ַ���
      /// </summary>
      /// <returns></returns>
      private string CombExecuteDay()
      {
         if (!listBoxWeekDay.Enabled)
            return "";

         StringBuilder dws = new StringBuilder();
         for (int i = 0; i < listBoxWeekDay.Items.Count; i++)
         {
            if (listBoxWeekDay.Items[i].CheckState != CheckState.Checked)
               continue;

            dws.Append(i + 1);
         }
         return dws.ToString();
      }

      /// <summary>
      /// �ô����ִ��ʱ������ַ�����ʼ��ʱ��ѡ��ListBox
      /// </summary>
      /// <param name="hourCombination">ִ��ʱ������ַ���</param>
      private void ResetHourListBox(string hourCombination)
      {
         string[] hours = hourCombination.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
         for (int i = 0; i <= hours.Length - 1; i++)
         {
            listBoxHour.Items[listBoxHour.FindStringExact(hours[i])].CheckState = CheckState.Checked;
         }
      }

      /// <summary>
      /// ��ѡ�е�ִ��ʱ����ϳ��ַ���
      /// </summary>
      /// <returns></returns>
      private string CombExecuteTime()
      {
         if ((!listBoxHour.Enabled) || (listBoxHour.CheckedIndices.Count < 1))
            return "";

         StringBuilder hours = new StringBuilder();
         for (int i = 0; i < listBoxHour.Items.Count; i++)
         {
            if (listBoxHour.Items[i].CheckState != CheckState.Checked)
               continue;
            if (hours.Length > 0)
               hours.Append(",");
            hours.Append(listBoxHour.Items[i].Value);
         }

         return hours.ToString();
      }
      #endregion

      #region event handle
      private void FrequencyInputor_Resize(object sender, EventArgs e)
      {
         if (!m_IsInit)
         {
            // ͬ���޸��ڲ��ؼ��ĳߴ�
            m_IsInit = true;
            frequencyDetail.Width = this.Size.Width - frequencyInput.Width;
            this.Height = frequencyInput.Height;
            m_IsInit = false;
         }
      }

      private void frequencyInput_CodeValueChanged(object sender, EventArgs e)
      {
         ResetFrequencyDetailUI();
      }

      private void frequencyDetail_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
      {
         frequencyDetail.Text = CheckFrequencyDetail();
      }

      private void frequencyDetail_TextChanged(object sender, EventArgs e)
      {
         frequencyDetail.ToolTip = frequencyDetail.Text;
      }

      private void listBox_DrawItem(object sender, ListBoxDrawItemEventArgs e)
      {
         if ((sender as CheckedListBoxControl).GetItemCheckState(e.Index) == CheckState.Checked)
         {
            e.Appearance.BackColor = Color.FromArgb(163, 224, 148);
         }
      }
      #endregion
   }
}
