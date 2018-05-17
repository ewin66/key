using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DrectSoft.Core.NursingDocuments.UserControls
{
    public partial class UCTextGroupBox : DevExpress.XtraEditors.XtraUserControl
    {
        private string m_Befor = "";//�˹�����ǰ������
        private string m_After = "";//�˹������������
        private string m_Labour = "";//�˹�����������

        bool m_Transparent = false;//�Ƿ�͸��

        public UCTextGroupBox()
        {
            InitializeComponent();
        }

        #region ���ñ���ɫ͸��ɫ
        /// <summary>
        /// ���ñ���ɫ͸��ɫ
        /// </summary>
        public bool IsTransparent
        {
            set
            {
                m_Transparent = value;
                if (m_Transparent)
                {

                    txtBefor.BackColor = Color.Transparent;
                    txtAfter.BackColor = Color.Transparent;
                    txtLabour.BackColor = Color.Transparent;
                    panelControl1.BackColor = Color.Transparent;

                    textEdit1.Visible = false;
                    textEdit2.Visible = false;

                    txtBefor.Visible = false;
                    txtAfter.Visible = false;
                    txtLabour.Visible = false;

                }
                else
                {
                    txtBefor.BackColor = Color.White;
                    txtAfter.BackColor = Color.White;
                    txtLabour.BackColor = Color.White;
                    panelControl1.BackColor = Color.White;

                    textEdit1.Visible = true;
                    textEdit2.Visible = true;

                    txtBefor.Visible = true;
                    txtAfter.Visible = true;
                    txtLabour.Visible = true;

                }
            }
        }
        #endregion

        #region ���ÿؼ��Ƿ�����
        /// <summary>
        /// ���ÿؼ��Ƿ�����
        /// </summary>
        /// <param name="Index">��ѡ������</param>
        private void SetCheckeEnabled(int index)
        {
            if (index == 1)
            {
                chkLetOffShit1.Checked = true;
                chkLetOffShit2.Checked = false;
                txtAfter.Enabled = false;
                txtBefor.Enabled = false;
                txtLabour.Enabled = false;
                textEdit1.Enabled = false;
                textEdit2.Enabled = false;
                textEdit1.BackColor = txtLabour.BackColor;
                textEdit2.BackColor = txtLabour.BackColor;

            }
            else if (index == 2)
            {
                chkLetOffShit1.Checked = false;
                chkLetOffShit2.Checked = true;
                txtAfter.Enabled = false;
                txtBefor.Enabled = false;
                txtLabour.Enabled = false;
                textEdit1.Enabled = false;
                textEdit2.Enabled = false;
                textEdit1.BackColor = txtLabour.BackColor;
                textEdit2.BackColor = txtLabour.BackColor;
            }
            else
            {
                chkLetOffShit1.Checked = false;
                chkLetOffShit2.Checked = false;
                txtAfter.Enabled = true;
                txtBefor.Enabled = true;
                txtLabour.Enabled = true;
                textEdit1.Enabled = false;
                textEdit2.Enabled = false;
                textEdit1.BackColor = Color.White;
                textEdit2.BackColor = Color.White;
            }
        }
        #endregion

        #region �ű����
        /// <summary>
        /// �ű����
        /// </summary>
        public string Shit
        {
            get
            {
                if (chkLetOffShit1.Checked)
                {
                    m_Befor = "��";
                    m_Labour = "��";
                    m_After = "��";
                }
                else if (chkLetOffShit2.Checked)
                {
                    m_Befor = "��";
                    m_Labour = "��";
                    m_After = "��";
                }
                else
                {
                    //string pattern = @"^[0-9]*$";
                    //Match m = Regex.Match(this.txtBefor.Text.Trim(), pattern);   // ƥ��������ʽ           
                    //if (!m.Success)//��������
                    //{
                    //    PublicMethod.RadAlterBox("�˹�����ǰ����������������", "��ʾ");
                    //}
                    //string pattern = @"^[0-9]*$";
                    //Match m = Regex.Match(this.txtBefor.Text.Trim(), pattern);   // ƥ��������ʽ           
                    //if (!m.Success)//��������
                    //{
                    //    PublicMethod.RadAlterBox("�˹�����ǰ����������������", "��ʾ");
                    //}
                    if (IsInput)
                    {
                        m_Befor = txtBefor.Text.Trim();
                        m_Labour = txtLabour.Text.Trim();
                        m_After = txtAfter.Text.Trim();
                    }
                    else
                    {
                        m_Befor = "";
                        m_Labour = "";
                        m_After = "";

                        txtBefor.Text = m_Befor;
                        txtLabour.Text = m_Labour;
                        txtAfter.Text = m_After;

                    }
                }


                return m_Befor + ":" + m_Labour + ":" + m_After;
            }

            set
            {
                if (value == "��:��:��")
                {
                    m_Befor = "��";
                    m_Labour = "��";
                    m_After = "��";

                    SetCheckeEnabled(1);

                }
                else if (value == "��:��:��")
                {
                    m_Befor = "��";
                    m_Labour = "��";
                    m_After = "��";

                    SetCheckeEnabled(2);

                }
                else
                {
                    string[] str = value.Split(':');
                    if (str.Length == 3 && value != "::")
                    {
                        m_Befor = str[0];
                        m_Labour = str[1];
                        m_After = str[2];

                    }
                    else
                    {
                        m_Befor = "";
                        m_Labour = "";
                        m_After = "";

                    }

                    txtBefor.Text = m_Befor;
                    txtLabour.Text = m_Labour;
                    txtAfter.Text = m_After;

                    SetCheckeEnabled(-1);
                }


            }
        }
        #endregion

        #region �ж��Ƿ���������
        /// <summary>
        /// �ж��Ƿ���������,true�����ݣ�false������
        /// </summary>
        public bool IsInput
        {
            get
            {
                if (chkLetOffShit1.Checked || chkLetOffShit2.Checked)
                {
                    return true;
                }
                else
                {
                    //if ((txtBefor.Text.Trim()!= "" && txtAfter.Text.Trim() != "" && txtLabour.Text.Trim() != "") ||
                    //    (txtBefor.Text.Trim() != "" && txtAfter.Text.Trim() == "" && txtLabour.Text.Trim() == "") ) 
                    //if (
                    //   (txtAfter.Text.Trim() == "" && txtLabour.Text.Trim() == "")) 
                    //{
                    //    return false;
                    //}
                    //else
                    //{
                    return true;
                    //}
                }
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        public void DateTextReset()
        {
            m_Befor = "";
            m_Labour = "";
            m_After = "";

            txtBefor.Text = m_Befor;
            txtLabour.Text = m_Labour;
            txtAfter.Text = m_After;

            SetCheckeEnabled(-1);

        }
        #endregion


        private void chkLetOffShit_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckEdit).Checked && (sender as CheckEdit).Name.Equals("chkLetOffShit1"))
            {
                chkLetOffShit2.Checked = false;
                SetCheckeEnabled(1);
            }
            else if ((sender as CheckEdit).Checked && (sender as CheckEdit).Name.Equals("chkLetOffShit2"))
            {
                chkLetOffShit1.Checked = false;
                SetCheckeEnabled(2);
            }
            else
            {
                SetCheckeEnabled(-1);
            }
        }

        private void UCTextGroupBox_Load(object sender, EventArgs e)
        {
            txtBefor.ToolTip = "�����ű����";
            txtAfter.ToolTip = "�೦���ű����";
            txtLabour.ToolTip = "�೦��������2EΪ���ι೦";
            chkLetOffShit1.ToolTip = "�����ʾ�˹�����";
            lblLetOffShit1.ToolTip = "�����ʾ�˹�����";
            chkLetOffShit2.ToolTip = "��������ʾ���ʧ��";
            lblLetOffShit2.ToolTip = "��������ʾ���ʧ��";
        }

        private void txtAfter_Enter(object sender, EventArgs e)
        {
            //this.txtAfter;
        }

        //��ȡ������ı��ؼ�
        TextEdit u_ActivateTextEdit = new TextEdit();
        /// <summary>
        /// ����н�����ı������ڴ��ݵ��ϼ�����
        /// add by ywk 2012��11��8��10:33:24
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBefor_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Name.Equals("TextEdit"))
            {
                u_ActivateTextEdit = sender as TextEdit;
            }
            else
            {
                u_ActivateTextEdit = null;
            }
        }
        public bool ISFocused = false;//���ڱ�ʾ�Ƿ��н���Ե����ı�����
        /// <summary>
        /// �õ�ǰ�ı��ؼ����»�ȡ����
        /// add by  ywk 2012��11��8��10:36:07
        /// </summary>
        public void ActivateTextEditFocus()
        {
            if (u_ActivateTextEdit != null)
            {
                ISFocused = true;
                u_ActivateTextEdit.Focus();
            }
        }
    }
}
