using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// Ƥ�Խ�����봰��
   /// </summary>
   internal partial class SkinTestInputForm : Form
   {
      /// <summary>
      /// ѡ�е�Ƥ�Խ��
      /// </summary>
      public SkinTestResultKind SkinTestResult
      {
         get
         {
            switch (selResult.SelectedIndex)
            {
               case 0:
                  return SkinTestResultKind.Plus;
               case 1:
                  return SkinTestResultKind.Minus;
               default:
                  return SkinTestResultKind.MinusTreeDay;
            }
         }
      }

      /// <summary>
      /// ����Ƿ���ѡ������
      /// </summary>
      public bool HadChoiceOne
      {
         get { return _hadChoiceOne; }
      }
      private bool _hadChoiceOne;

      /// <summary>
      /// ����Ƿ�ֻ��ʾ��ʾ��Ϣ
      /// </summary>
      public bool ShowMessageOnly
      {
         get { return _showMessageOnly; }
         set
         {
            _showMessageOnly = value;
            //panelSelect.Visible = !value;
            //if (!value)
            //   panelSelect.BringToFront();
         }
      }
      private bool _showMessageOnly;

      /// <summary>
      /// ��Ҫ��ʾ��������Ϣ����
      /// </summary>
      public string Message
      {
         get { return labMessage.Text; }
         set { labMessage.Text = value; }
      }

      public SkinTestInputForm()
      {
         InitializeComponent();
      }

      /// <summary>
      /// �Ѿ�����ѡ�����
      /// </summary>
      public event EventHandler HadMadeChoice
      {
         add
         {
            onHadMadeChoice = (EventHandler)Delegate.Combine(onHadMadeChoice, value);
         }
         remove
         {
            onHadMadeChoice = (EventHandler)Delegate.Remove(onHadMadeChoice, value);
         }
      }
      private EventHandler onHadMadeChoice;

      private void DoHadMadeChoice()
      {
         if (onHadMadeChoice != null)
            onHadMadeChoice(this, new EventArgs());
      }

      private void btnOk_Click(object sender, EventArgs e)
      {
         _hadChoiceOne = !ShowMessageOnly;
         DialogResult = DialogResult.OK;
         DoHadMadeChoice();
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
         _hadChoiceOne = false;
         DialogResult = DialogResult.Cancel;
         DoHadMadeChoice();
      }

      private void SkinTestInputForm_Shown(object sender, EventArgs e)
      {
         selResult.SelectedIndex = 0;
         _hadChoiceOne = false;
      }
   }
}
