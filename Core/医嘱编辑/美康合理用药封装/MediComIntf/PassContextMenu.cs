using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DrectSoft.Common.MediComIntf
{
   /// <summary>
   ///
   /// </summary>
   public partial class PassMenu : System.Windows.Forms.ContextMenuStrip
    {
        private System.Windows.Forms.ToolStripMenuItem �ٴ�ҩ����Ϣ�ο�ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ������ҩ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ҩƷ˵����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ҩ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ֵToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ר����ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ҩ��ҩ���໥����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ҩ��ʳ���໥����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ע�����������ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ע�����������ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����֢ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ������ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��������ҩToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��ͯ��ҩToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��������ҩToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��������ҩToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ҩ����Ϣ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ҩƷ�����ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��ҩ;�������ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ҽԺҩƷ��ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ϵͳ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��ҩ�о�ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��ҩ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ���ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ʷ����״̬����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

        private PassCheckHelper _passCheckhlp;

        /// <summary>
        ///
        /// </summary>
        public PassCheckHelper PassCheckhlp
        {
            get { return _passCheckhlp; }
            set { _passCheckhlp = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public PassMenu()
            : this(null)
        { }

        /// <summary>
        ///
        /// </summary>
        public PassMenu(PassCheckHelper passCheckHelper)
        {
            Init();
            _passCheckhlp = passCheckHelper;
        }

        void Init()
        {
            this.�ٴ�ҩ����Ϣ�ο�ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.������ҩ����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ҩƷ˵����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ҩ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ֵToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ר����ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ҩ��ҩ���໥����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ҩ��ʳ���໥����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ע�����������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ע�����������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����֢ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��������ҩToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��ͯ��ҩToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��������ҩToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��������ҩToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ҩ����Ϣ����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ҩƷ�����ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��ҩ;�������ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ҽԺҩƷ��ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ϵͳ����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��ҩ�о�ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��ҩ����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.���ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ʷ����״̬����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();

            // 
            // contextMenuStrip1
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.�ٴ�ҩ����Ϣ�ο�ToolStripMenuItem,
            this.������ҩ����ToolStripMenuItem,
            this.ҩƷ˵����ToolStripMenuItem,
            this.����ҩ��ToolStripMenuItem,
            this.����ֵToolStripMenuItem,
            this.toolStripSeparator1,
            this.ר����ϢToolStripMenuItem,
            this.toolStripSeparator2,
            this.ҩ����Ϣ����ToolStripMenuItem,
            this.toolStripSeparator3,
            this.ҩƷ�����ϢToolStripMenuItem,
            this.��ҩ;�������ϢToolStripMenuItem,
            this.ҽԺҩƷ��ϢToolStripMenuItem,
            this.toolStripSeparator4,
            this.ϵͳ����ToolStripMenuItem,
            this.��ҩ�о�ToolStripMenuItem,
            this.��ҩ����ToolStripMenuItem,
            this.���ToolStripMenuItem,
            this.toolStripSeparator5,
            this.����ʷ����״̬����ToolStripMenuItem,
            this.toolStripSeparator6,
            this.����ToolStripMenuItem});
            this.Name = "passContextMenuTrip";
            this.Size = new System.Drawing.Size(229, 446);
            // 
            // �ٴ�ҩ����Ϣ�ο�ToolStripMenuItem
            // 
            this.�ٴ�ҩ����Ϣ�ο�ToolStripMenuItem.Name = "�ٴ�ҩ����Ϣ�ο�ToolStripMenuItem";
            this.�ٴ�ҩ����Ϣ�ο�ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.�ٴ�ҩ����Ϣ�ο�ToolStripMenuItem.Text = "�ٴ�ҩ����Ϣ�ο�";
            this.�ٴ�ҩ����Ϣ�ο�ToolStripMenuItem.Tag = 101;
            this.�ٴ�ҩ����Ϣ�ο�ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ������ҩ����ToolStripMenuItem
            // 
            this.������ҩ����ToolStripMenuItem.Name = "������ҩ����ToolStripMenuItem";
            this.������ҩ����ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.������ҩ����ToolStripMenuItem.Text = "������ҩ����";
            this.������ҩ����ToolStripMenuItem.Tag = 103;
            this.������ҩ����ToolStripMenuItem.Click +=new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ҩƷ˵����ToolStripMenuItem
            // 
            this.ҩƷ˵����ToolStripMenuItem.Name = "ҩƷ˵����ToolStripMenuItem";
            this.ҩƷ˵����ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.ҩƷ˵����ToolStripMenuItem.Text = "ҩƷ˵����";
            this.ҩƷ˵����ToolStripMenuItem.Tag = 102;
            this.ҩƷ˵����ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ����ҩ��ToolStripMenuItem
            // 
            this.����ҩ��ToolStripMenuItem.Name = "����ҩ��ToolStripMenuItem";
            this.����ҩ��ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.����ҩ��ToolStripMenuItem.Text = "����ҩ��";
            this.����ҩ��ToolStripMenuItem.Tag = 107;
            this.����ҩ��ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ����ֵToolStripMenuItem
            // 
            this.����ֵToolStripMenuItem.Name = "����ֵToolStripMenuItem";
            this.����ֵToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.����ֵToolStripMenuItem.Text = "����ֵ";
            this.����ֵToolStripMenuItem.Tag = 104;
            this.����ֵToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ר����ϢToolStripMenuItem
            // 
            this.ר����ϢToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ҩ��ҩ���໥����ToolStripMenuItem,
            this.ҩ��ʳ���໥����ToolStripMenuItem,
            this.toolStripSeparator7,
            this.����ע�����������ToolStripMenuItem,
            this.����ע�����������ToolStripMenuItem,
            this.toolStripSeparator8,
            this.����֢ToolStripMenuItem,
            this.������ToolStripMenuItem,
            this.toolStripSeparator9,
            this.��������ҩToolStripMenuItem,
            this.��ͯ��ҩToolStripMenuItem,
            this.��������ҩToolStripMenuItem,
            this.��������ҩToolStripMenuItem});
            this.ר����ϢToolStripMenuItem.Name = "ר����ϢToolStripMenuItem";
            this.ר����ϢToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.ר����ϢToolStripMenuItem.Text = "ר����Ϣ";
            // 
            // ҩ��ҩ���໥����ToolStripMenuItem
            // 
            this.ҩ��ҩ���໥����ToolStripMenuItem.Name = "ҩ��ҩ���໥����ToolStripMenuItem";
            this.ҩ��ҩ���໥����ToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.ҩ��ҩ���໥����ToolStripMenuItem.Text = "ҩ��-ҩ���໥����";
            this.ҩ��ҩ���໥����ToolStripMenuItem.Tag = 201;
            this.ҩ��ҩ���໥����ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ҩ��ʳ���໥����ToolStripMenuItem
            // 
            this.ҩ��ʳ���໥����ToolStripMenuItem.Name = "ҩ��ʳ���໥����ToolStripMenuItem";
            this.ҩ��ʳ���໥����ToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.ҩ��ʳ���໥����ToolStripMenuItem.Text = "ҩ��-ʳ���໥����";
            this.ҩ��ʳ���໥����ToolStripMenuItem.Tag = 202;
            this.ҩ��ʳ���໥����ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ����ע�����������ToolStripMenuItem
            // 
            this.����ע�����������ToolStripMenuItem.Name = "����ע�����������ToolStripMenuItem";
            this.����ע�����������ToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.����ע�����������ToolStripMenuItem.Text = "����ע�����������";
            this.����ע�����������ToolStripMenuItem.Tag = 203;
            this.����ע�����������ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ����ע�����������ToolStripMenuItem
            // 
            this.����ע�����������ToolStripMenuItem.Name = "����ע�����������ToolStripMenuItem";
            this.����ע�����������ToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.����ע�����������ToolStripMenuItem.Text = "����ע�����������";
            this.����ע�����������ToolStripMenuItem.Tag = 204;
            this.����ע�����������ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ����֢ToolStripMenuItem
            // 
            this.����֢ToolStripMenuItem.Name = "����֢ToolStripMenuItem";
            this.����֢ToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.����֢ToolStripMenuItem.Text = "����֢";
            this.����֢ToolStripMenuItem.Tag = 205;
            this.����֢ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ������ToolStripMenuItem
            // 
            this.������ToolStripMenuItem.Name = "������ToolStripMenuItem";
            this.������ToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.������ToolStripMenuItem.Text = "������";
            this.������ToolStripMenuItem.Tag = 206;
            this.������ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ��������ҩToolStripMenuItem
            // 
            this.��������ҩToolStripMenuItem.Name = "��������ҩToolStripMenuItem";
            this.��������ҩToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.��������ҩToolStripMenuItem.Text = "��������ҩ";
            this.��������ҩToolStripMenuItem.Tag = 207;
            this.��������ҩToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ��ͯ��ҩToolStripMenuItem
            // 
            this.��ͯ��ҩToolStripMenuItem.Name = "��ͯ��ҩToolStripMenuItem";
            this.��ͯ��ҩToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.��ͯ��ҩToolStripMenuItem.Text = "��ͯ��ҩ";
            this.��ͯ��ҩToolStripMenuItem.Tag = 208;
            this.��ͯ��ҩToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ��������ҩToolStripMenuItem
            // 
            this.��������ҩToolStripMenuItem.Name = "��������ҩToolStripMenuItem";
            this.��������ҩToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.��������ҩToolStripMenuItem.Text = "��������ҩ";
            this.��������ҩToolStripMenuItem.Tag = 209;
            this.��������ҩToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ��������ҩToolStripMenuItem
            // 
            this.��������ҩToolStripMenuItem.Name = "��������ҩToolStripMenuItem";
            this.��������ҩToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.��������ҩToolStripMenuItem.Text = "��������ҩ";
            this.��������ҩToolStripMenuItem.Tag = 210;
            this.��������ҩToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ҩ����Ϣ����ToolStripMenuItem
            // 
            this.ҩ����Ϣ����ToolStripMenuItem.Name = "ҩ����Ϣ����ToolStripMenuItem";
            this.ҩ����Ϣ����ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.ҩ����Ϣ����ToolStripMenuItem.Text = "ҩ����Ϣ����";
            this.ҩ����Ϣ����ToolStripMenuItem.Tag = 106;
            this.ҩ����Ϣ����ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ҩƷ�����ϢToolStripMenuItem
            // 
            this.ҩƷ�����ϢToolStripMenuItem.Name = "ҩƷ�����ϢToolStripMenuItem";
            this.ҩƷ�����ϢToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.ҩƷ�����ϢToolStripMenuItem.Text = "ҩƷ�����Ϣ";
            this.ҩƷ�����ϢToolStripMenuItem.Tag = 13;
            this.ҩƷ�����ϢToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ��ҩ;�������ϢToolStripMenuItem
            // 
            this.��ҩ;�������ϢToolStripMenuItem.Name = "��ҩ;�������ϢToolStripMenuItem";
            this.��ҩ;�������ϢToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.��ҩ;�������ϢToolStripMenuItem.Text = "��ҩ;�������Ϣ";
            this.��ҩ;�������ϢToolStripMenuItem.Tag = 14;
            this.��ҩ;�������ϢToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ҽԺҩƷ��ϢToolStripMenuItem
            // 
            this.ҽԺҩƷ��ϢToolStripMenuItem.Name = "ҽԺҩƷ��ϢToolStripMenuItem";
            this.ҽԺҩƷ��ϢToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.ҽԺҩƷ��ϢToolStripMenuItem.Text = "ҽԺҩƷ��Ϣ";
            this.ҽԺҩƷ��ϢToolStripMenuItem.Tag = 105;
            this.ҽԺҩƷ��ϢToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ϵͳ����ToolStripMenuItem
            // 
            this.ϵͳ����ToolStripMenuItem.Name = "ϵͳ����ToolStripMenuItem";
            this.ϵͳ����ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.ϵͳ����ToolStripMenuItem.Text = "ϵͳ����";
            this.ϵͳ����ToolStripMenuItem.Tag = 11;
            this.ϵͳ����ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ��ҩ�о�ToolStripMenuItem
            // 
            this.��ҩ�о�ToolStripMenuItem.Name = "��ҩ�о�ToolStripMenuItem";
            this.��ҩ�о�ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.��ҩ�о�ToolStripMenuItem.Text = "��ҩ�о�";
            this.��ҩ�о�ToolStripMenuItem.Tag = 12;
            this.��ҩ�о�ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // ��ҩ����ToolStripMenuItem
            // 
            this.��ҩ����ToolStripMenuItem.Name = "��ҩ����ToolStripMenuItem";
            this.��ҩ����ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.��ҩ����ToolStripMenuItem.Text = "��ҩ����";
            this.��ҩ����ToolStripMenuItem.Tag = 6;
            this.��ҩ����ToolStripMenuItem.Click += new EventHandler(��ҩ����ToolStripMenuItem_Click);
            // 
            // ���ToolStripMenuItem
            // 
            this.���ToolStripMenuItem.Name = "���ToolStripMenuItem";
            this.���ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.���ToolStripMenuItem.Text = "���";
            this.���ToolStripMenuItem.Tag = 3;
            this.���ToolStripMenuItem.Click += new EventHandler(���ToolStripMenuItem_Click);
            // 
            // ����ʷ����״̬����ToolStripMenuItem
            // 
            this.����ʷ����״̬����ToolStripMenuItem.Name = "����ʷ����״̬����ToolStripMenuItem";
            this.����ʷ����״̬����ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.����ʷ����״̬����ToolStripMenuItem.Text = "����ʷ/����״̬����";
            this.����ʷ����״̬����ToolStripMenuItem.Tag = 21;
            this.����ʷ����״̬����ToolStripMenuItem.Click += new EventHandler(����ʷ����״̬����ToolStripMenuItem_Click);
            // 
            // ����ToolStripMenuItem
            // 
            this.����ToolStripMenuItem.Name = "����ToolStripMenuItem";
            this.����ToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.����ToolStripMenuItem.Text = "����";
            this.����ToolStripMenuItem.Tag = 301;
            this.����ToolStripMenuItem.Click += new EventHandler(TagToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(219, 6);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(219, 6);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(219, 6);
        }

        void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int commandId = Convert.ToInt32((sender as System.Windows.Forms.ToolStripMenuItem).Tag);
            if (_passCheckhlp != null && _passCheckhlp.HasPatient()
                && _passCheckhlp.CurrentDrugInfos != null
                && _passCheckhlp.CurrentDrugInfos.Count > 0)
            {
                _passCheckhlp.DoCommand(commandId);
            }
        }

        void ��ҩ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int commandId = Convert.ToInt32((sender as System.Windows.Forms.ToolStripMenuItem).Tag);
            if (_passCheckhlp != null && _passCheckhlp.HasPatient()
                && !string.IsNullOrEmpty(_passCheckhlp.CurrentDrugIndex))
            {
                _passCheckhlp.PassSetWarnDrug(_passCheckhlp.CurrentDrugIndex);
                _passCheckhlp.DoCommand(commandId);
            }
        }

        void ����ʷ����״̬����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int commandId = Convert.ToInt32((sender as System.Windows.Forms.ToolStripMenuItem).Tag);
            if (_passCheckhlp != null && _passCheckhlp.HasPatient())
                _passCheckhlp.DoCommand(commandId);
        }

        void TagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int commandId = Convert.ToInt32((sender as System.Windows.Forms.ToolStripMenuItem).Tag);
            if (_passCheckhlp != null)
            {
                if (_passCheckhlp.PassPopMenuEnable(commandId.ToString()))
                    _passCheckhlp.DoCommand(commandId);
            }
        }

    }
}
