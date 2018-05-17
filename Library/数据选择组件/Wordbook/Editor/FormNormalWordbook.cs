using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// BaseWordbook���Եı༭����
   /// </summary>
   public partial class FormNormalWordbook : Form
   {
      #region properties
      /// <summary>
      /// ��ǰѡ����ֵ���
      /// </summary>
      public BaseWordbook Wordbook
      {
         get { return _wordbook; }
         set 
         {
            _wordbook = value; 
            // ��ѡ�е��ֵ��ʼ������
            InitializeEditBox();
         }
      }
      private BaseWordbook _wordbook;
      #endregion

      /// <summary>
      /// ������ͨ�ֵ�������ô���
      /// </summary>
      public FormNormalWordbook()
      {
         InitializeComponent();

         dgvInputParas.AutoGenerateColumns = false;
         splitContainer1.Panel1MinSize = 170;
         splitContainer1.Panel2MinSize = 420;
         tabControl1.SelectedTab = tabPageBasic;
      }

      #region private methods
      /// <summary>
      /// �õ�ǰѡ�е��ֵ������Ϣ��ʼ���༭����
      /// </summary>
      private void InitializeEditBox()
      {
         if (_wordbook == null)
            return;

         // ������Ϣ
         tBoxName.Text = _wordbook.Caption;
         tBoxClassName.Text = _wordbook.WordbookName;
         tBoxQuery.Text = _wordbook.QuerySentence;
         tBoxExtra.Text = _wordbook.ExtraCondition;
         // ��ʼֵ
         treeViewFields.Nodes.Clear();
         TreeNode node;
         foreach (string field in _wordbook.CurrentMatchFields)
         { 
            node = new TreeNode(field);
            treeViewFields.Nodes.Add(node);
         }

         dgvInputParas.DataSource = _wordbook.Parameters.Convert2DataTable();
         tBoxDefValue.Text = _wordbook.ParameterValueComb;
         // ����ʾ����(������ʾ������̬���ɿؼ�)
         GenerateShowStylePlans();
      }

      /// <summary>
      /// ������ʾ������̬����RadioButton��DataGridView�����
      /// </summary>
      private void GenerateShowStylePlans()
      {
         // ���������̬���ɵĿؼ�
         DataGridView gridView;
         DataGridViewColumnCollection cols;
         for (int i = tabPageShow.Controls.Count - 1; i >= 0; i--)
         {
            // �����DataGridView�������ͷŴ�����Column
            gridView = tabPageShow.Controls[i] as DataGridView;
            if (gridView == null)
            {
               tabPageShow.Controls[i].Dispose();
            }
            else
            {
               cols = gridView.Columns;
               foreach (DataGridViewColumn col in cols)
                  col.Dispose();
               gridView.Dispose();
            }
         }

         RadioButton radBtn;
         DataGridViewColumn newCol;
         for (int index = 0; index < _wordbook.ShowStyles.Count; index++)
         {
            // ����RadioButton
            radBtn = new RadioButton();
            radBtn.AutoSize = true;
            radBtn.FlatStyle = FlatStyle.Popup;
            radBtn.Location = new Point(5, 12 + 44 * index);
            radBtn.Name = "rdBtn_" + index.ToString(CultureInfo.CurrentCulture);
            radBtn.Text = "";
            tabPageShow.Controls.Add(radBtn);
            // Ĭ��ѡ�е�һ������
            if (index == 0)
               radBtn.Checked = true;

            // ����DataGridView
            gridView = new DataGridView();
            gridView.Name = "gridView_" + index.ToString(CultureInfo.CurrentCulture);
            gridView.AllowUserToAddRows = false;
            gridView.ReadOnly = true;
            gridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridView.Location = new Point(22, 2 + 44 * index);
            gridView.RowHeadersWidth = 4;
            gridView.RowTemplate.Height = 23;
            gridView.Size = new System.Drawing.Size(380, 42);
            gridView.ScrollBars = ScrollBars.Both;
   
            // ����DataGridView��Column����
            //cols = new DataGridViewColumnCollection(gridView);
            foreach (GridColumnStyle style in _wordbook.ShowStyles[index])
            {
               newCol = new DataGridViewTextBoxColumn();
               newCol.Frozen = true;
               newCol.ReadOnly = true;
               //newCol.DefaultCellStyle = dgvInputParas.DefaultCellStyle;
               newCol.HeaderText = style.Caption;
               newCol.Width = style.Width;
               gridView.Columns.Add(newCol);
            }
            tabPageShow.Controls.Add(gridView);
         }
      }

      /// <summary>
      /// ���������ֶ����������ֶε�λ��
      /// </summary>
      /// <param name="isUp">����</param>
      private void ChangeFieldsRowPosition(bool isUp)
      {
         TreeNode node = treeViewFields.SelectedNode;
         int index = node.Index;

         if (((index <= 0) && (isUp))
            || ((index >= (treeViewFields.Nodes.Count - 1)) && (!isUp)))
            return;

         treeViewFields.Nodes.Remove(node);
         if (isUp)
            treeViewFields.Nodes.Insert(index - 1, node);
         else
            treeViewFields.Nodes.Insert(index + 1, node);
         treeViewFields.SelectedNode = node;
      }

      /// <summary>
      /// ��������������޸�
      /// </summary>
      private void SaveChanges()
      {
         if (_wordbook == null)
            return;

         // ��������
         if (tBoxExtra.Text.Trim().Length > 0)
            _wordbook.ExtraCondition = tBoxExtra.Text.Trim();

         // ��Ϊ�����е��ֶ�
         if (treeViewFields.Nodes.Count > 0)
         {
            StringBuilder values = new StringBuilder(treeViewFields.Nodes[0].Text);
            for (int index = 1; index < treeViewFields.Nodes.Count; index++ )
            {
               values.Append(SeparatorSign.ListSeparator);
               values.Append(treeViewFields.Nodes[index].Text);
            }
            _wordbook.MatchFieldComb = values.ToString();
         }

         // ����Ĭ��ֵ
         if (tBoxDefValue.Text.Trim().Length > 0)
            _wordbook.ParameterValueComb = tBoxDefValue.Text.Trim();

         // ��ʾ����
         RadioButton radBtn;
         foreach (Control ctrl in tabPageShow.Controls)
         {
            radBtn = ctrl as RadioButton;
            if (radBtn != null)
            {
               if (radBtn.Checked)
               {
                  string[] splits = radBtn.Name.Split('_');
                  _wordbook.SelectedStyleIndex = Convert.ToInt32(splits[1], CultureInfo.CurrentCulture);
                  break;
               }
            }
         }
      }
      #endregion

      #region events
      private void btnOk_Click(object sender, EventArgs e)
      {
         // ����༭����ֵ
         SaveChanges();
      }

      private void btnUp_Click(object sender, EventArgs e)
      {
         ChangeFieldsRowPosition(true);
      }

      private void btnDown_Click(object sender, EventArgs e)
      {
         ChangeFieldsRowPosition(false);
      }

      private void dgvInputParas_Leave(object sender, EventArgs e)
      {
         StringBuilder values = new StringBuilder();
         DataRowView sourceRow;
         
         // ���������Ĭ��ֵ�Ĳ���         
         foreach(DataGridViewRow row in dgvInputParas.Rows)
         {
            sourceRow = row.DataBoundItem as DataRowView;
            if (Convert.ToBoolean(sourceRow["Enable"], CultureInfo.CurrentCulture))
            {
               if (values.Length > 0)
                  values.Append(SeparatorSign.ListSeparator);
               values.Append(sourceRow["Name"].ToString().Trim());
               values.Append(SeparatorSign.ListSeparator);
               values.Append(row.Cells["ColValue"].EditedFormattedValue);
            }
         }
         tBoxDefValue.Text = values.ToString();
      }

      private void wordbookTree_TreeDoubleClick(object sender, EventArgs e)
      {
         if (wordbookTree.CurrentBookInfo.TypeName != null)
         {
            _wordbook = WordbookStaticHandle.GetWordbook(wordbookTree.CurrentBookInfo.TypeName);
            // ��ѡ�е��ֵ��ʼ������
            InitializeEditBox();
         }
      }
      #endregion
   }
}