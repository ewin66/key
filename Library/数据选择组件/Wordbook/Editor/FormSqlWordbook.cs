using System;
using System.Globalization;
using System.Windows.Forms;

namespace DrectSoft.Wordbook
{
    /// <summary>
    /// SqlWordbook���Եı༭����
    /// </summary>
    public partial class FormSqlWordbook : Form
    {
        #region properties
        /// <summary>
        /// ��ǰѡ����ֵ���
        /// </summary>
        public SqlWordbook Wordbook
        {
            get { return _wordbook; }
            set
            {
                _wordbook = value;
                // ��ѡ�е��ֵ��ʼ������
                InitializeEditBox();
            }
        }
        private SqlWordbook _wordbook;
        #endregion

        /// <summary>
        /// ����Sql�ֵ����ô���
        /// </summary>
        public FormSqlWordbook()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �õ�ǰѡ�е��ֵ������Ϣ��ʼ���༭����
        /// </summary>
        private void InitializeEditBox()
        {
            if (_wordbook == null)
                return;

            // ������Ϣ
            tBoxName.Text = _wordbook.WordbookName;
            tBoxQuery.Text = _wordbook.QuerySentence;
            tBoxCodeField.Text = _wordbook.CodeField;
            tBoxNameField.Text = _wordbook.NameField;
            tBoxFilter.Text = _wordbook.MatchFieldComb;
            lvColumnStyles.Items.Clear();
            foreach (GridColumnStyle style in _wordbook.DefaultGridStyle)
                lvColumnStyles.Items.Add(new ListViewItem(new string[] { 
               style.FieldName, style.Caption, style.Width.ToString(CultureInfo.CurrentCulture)}));
            tBoxFieldName.Text = String.Empty;
            tBoxCaption.Text = String.Empty;
            tBoxWidth.Text = String.Empty;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // ����༭����ֵ
            SaveChanges();
        }

        /// <summary>
        /// ��������������޸�
        /// </summary>
        private void SaveChanges()
        {
            try
            {
                CheckValue();
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show(ae.Message, "��ʾ", MessageBoxButtons.OK
                   , MessageBoxIcon.Information
                   , MessageBoxDefaultButton.Button1
                   , MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            catch
            {
                throw;
            }

            GridColumnStyleCollection styleCollection = new GridColumnStyleCollection();
            if (lvColumnStyles.Items.Count > 0)
            {
                foreach (ListViewItem item in lvColumnStyles.Items)
                    styleCollection.Add(new GridColumnStyle(item.Text
                       , item.SubItems[1].Text
                       , Convert.ToInt32(item.SubItems[2].Text, CultureInfo.CurrentCulture)));
            }
            _wordbook = new SqlWordbook(tBoxName.Text.Trim()
               , tBoxQuery.Text.Trim()
               , tBoxCodeField.Text.Trim()
               , tBoxNameField.Text.Trim()
               , styleCollection
               , tBoxFilter.Text.Trim());
        }

        /// <summary>
        /// �������ֵ�Ƿ�Ϸ�
        /// </summary>
        /// <returns></returns>
        private void CheckValue()
        {
            if (tBoxName.Text.Trim().Length == 0)
            {
                throw new ArgumentNullException(MessageStringManager.GetString("SqlWordbookNeedName"));
            }

            if (tBoxQuery.Text.Trim().Length == 0)
            {
                throw new ArgumentNullException(MessageStringManager.GetString("SqlWordbookNeedQurySentence"));
            }

            if (tBoxCodeField.Text.Trim().Length == 0)
            {
                throw new ArgumentNullException(MessageStringManager.GetString("SqlWordbookNeedCodeFieldName"));
            }

            if (tBoxNameField.Text.Trim().Length == 0)
            {
                throw new ArgumentNullException(MessageStringManager.GetString("SqlWordbookNeedNameFieldName"));
            }
        }

        private void lvColumnStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = (lvColumnStyles.SelectedItems != null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string field = tBoxFieldName.Text.Trim();
            string caption = tBoxCaption.Text.Trim();
            string width = tBoxWidth.Text.Trim();
            if ((field.Length == 0) || (caption.Length == 0))
                return;
            try
            {
                Convert.ToInt32(width, CultureInfo.CurrentCulture);
            }
            catch
            {
                return;
            }

            lvColumnStyles.Items.Add(new ListViewItem(new string[] {
            field, caption, width}, -1));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvColumnStyles.SelectedItems.Count > 0)
            {
                lvColumnStyles.Items.Remove(lvColumnStyles.SelectedItems[0]);
            }
        }

    }
}