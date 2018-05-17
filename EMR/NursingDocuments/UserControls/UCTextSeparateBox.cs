using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.Core.NursingDocuments.PublicSet;

namespace DrectSoft.Core.NursingDocuments.UserControls
{
    public partial class UCTextSeparateBox : DevExpress.XtraEditors.XtraUserControl
    {
        bool m_IsTransparent=false;

        public UCTextSeparateBox()
        {
            InitializeComponent();
        }

        private void UCTextSeparateBox_Load(object sender, EventArgs e)
        {
            txtSBP.ToolTip = "����ѹ��Χ��1mmHg��250mmHg";
            txtDBP.ToolTip = "����ѹ��Χ��1mmHg��200mmHg";
        }

        /// <summary>
        /// �����Ƿ�͸��
        /// </summary>
        public bool IsTransparent
        {
            get
            { 
                return m_IsTransparent;
            }

            set 
            {
                m_IsTransparent = value;
                if (m_IsTransparent)
                {
                    txtDBP.BackColor = Color.Transparent;
                    txtSBP.BackColor = Color.Transparent;
                    textEdit3.BackColor = Color.Transparent;
                    labelControl1.BackColor = Color.Transparent;
                    //this.BackColor = Color.Transparent;
                    
                }
                else
                {
                    txtDBP.BackColor = Color.White;
                    txtSBP.BackColor = Color.White;
                    textEdit3.BackColor = Color.White;
                    labelControl1.BackColor = Color.White;
                    //this.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// �ж������Ƿ�Ϊ��,true�ǿգ�false��(ֻҪ����һ��Ϊ��)
        /// </summary>
        public bool IsInput
        {
            get
            {
                #region �ж���������з����֣���ǿ�  edit by wyt 2012-11-09
                if (txtDBP.Text.Trim() != "" || txtSBP.Text.Trim() != "")
                {
                    foreach (char c in this.txtDBP.Text.Trim())
                    {
                        if (c < '0' || c > '9')
                        {
                            return true;
                        }
                    }
                    foreach (char c in this.txtSBP.Text.Trim())
                    {
                        if (c < '0' || c > '9')
                        {
                            return true;
                        }
                    }
                }
                #endregion
                return !(txtDBP.Text.Trim() == "" || txtSBP.Text.Trim() == "");  //true�ǿգ�false��
            }
        }

        /// <summary>
        /// Ѫѹֵ
        /// </summary>
        public string BP
        {
            get 
            {
                if (IsInput)
                {
                    #region ���ֻ����һ��򷵻ص�һ��  edit by wyt 2012-11-09
                    if (txtDBP.Text.Trim() == "" || txtSBP.Text.Trim() == "")
                    {
                        return txtSBP.Text.Trim();
                    }
                    #endregion
                    return txtSBP.Text.Trim() + "/" + txtDBP.Text.Trim();
                }
                else
                {
                    txtSBP.Text = "";
                    txtDBP.Text = "";
                    return "";
                }
            }

            set 
            {
                DateTextReset();
                #region ���ֻ��һ�������Ϊ��һ��  edit by wyt 2012-11-09
                if (value.Contains("/"))
                {
                    string[] BPArray = value.Split(new char[] { '/' });
                    if (BPArray.Length == 2)
                    {
                        txtSBP.Text = BPArray[0];
                        txtDBP.Text = BPArray[1];
                    }
                }
                else
                {
                    txtSBP.Text = value;
                }
                #endregion
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void DateTextReset()
        {
            txtDBP.Text = "";
            txtSBP.Text = "";
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public bool CheckData()
        {
            if (!IsInput) return true;//���˳�

            //edit by wyt 2012-11-05 �޸�ʹѪѹ��¼������(��������)
            string IsHidePainHeight = MethodSet.GetConfigValueByKey("IsHidePainHeight_NursingRecord");
            if (IsHidePainHeight.Substring(35, 1) == "0")
            {

                double intBp;

                if (!Dataprocessing.IsNumber(txtSBP.Text.Trim().ToString(), 0))
                {
                    MethodSet.App.CustomMessageBox.MessageShow("����ѹ����Ϊ����");
                    txtSBP.Focus();
                    return false;
                }
                else
                {
                    intBp = double.Parse(txtSBP.Text.Trim());
                    if (!(intBp > 0 && intBp <= 300))//�ʺ����� add by ywk 
                    {
                        MethodSet.App.CustomMessageBox.MessageShow("����ѹ������1mmHg��300mmHg֮�䡣");
                        txtSBP.Focus();
                        return false;
                    }
                }

                if (!Dataprocessing.IsNumber(txtDBP.Text.Trim(), 0))
                {
                    MethodSet.App.CustomMessageBox.MessageShow("����ѹ����Ϊ����");
                    txtDBP.Focus();
                    return false;
                }
                else
                {
                    intBp = Convert.ToInt32(txtDBP.Text.Trim());
                    if (!(intBp > 0 && intBp <= 200))
                    {
                        MethodSet.App.CustomMessageBox.MessageShow("����ѹ������1mmHg��200mmHg֮�䡣");
                        txtDBP.Focus();
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
