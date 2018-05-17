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
                    return  txtSBP.Text.Trim() + "/" + txtDBP.Text.Trim();
                else
                    txtSBP.Text = "";
                    txtDBP.Text ="";
                    return "";
            }

            set 
            {
                DateTextReset();
                
                string[] BPArray = value.Split(new char[] { '/' });
                if (BPArray.Length == 2)
                {
                    txtSBP.Text = BPArray[0];
                    txtDBP.Text = BPArray[1];
                }
               
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

            int intBp;

            if (!Dataprocessing.IsNumber(txtSBP.Text.Trim().ToString(), 0))
            {
                MethodSet.App.CustomMessageBox.MessageShow("����ѹ����Ϊ����!");
                return false;
            }
            else
            {
                intBp = Convert.ToInt32(txtSBP.Text.Trim().ToString());
                if (!(intBp >0 && intBp <= 250))
                {
                    MethodSet.App.CustomMessageBox.MessageShow("����ѹ������1mmHg��250mmHg֮��!");
                    return false;
                }
             }


            if (!Dataprocessing.IsNumber(txtDBP.Text.Trim().ToString(), 0))
            {
                MethodSet.App.CustomMessageBox.MessageShow("����ѹ����Ϊ����!");
                return false;
            }
            else
            {
                intBp = Convert.ToInt32(txtDBP.Text.Trim().ToString());
                if (!(intBp >0 && intBp <= 200))
                {
                    MethodSet.App.CustomMessageBox.MessageShow("����ѹ������1mmHg��200mmHg֮��!");
                    return false;
                }                
            }

            return true;
        }
    }
}
