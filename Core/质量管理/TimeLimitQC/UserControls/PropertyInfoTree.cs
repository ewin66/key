using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Reflection;
using DrectSoft.Core.TimeLimitQC;
using System.Xml;

namespace DrectSoft.Core.TimeLimitQC
{
    public partial class PropertyInfoTree : TreeList
    {
        Type _currentClass;
        QCParams _qcParams = new QCParams();

        /// <summary>
        /// ������������
        /// </summary>
        public QCParams QcParams
        {
            get { return _qcParams; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public PropertyInfoTree()
        {
            InitializeComponent();
            this.FocusedNodeChanged += new FocusedNodeChangedEventHandler(PropertyInfoTree_FocusedNodeChanged);
            this.CellValueChanged += new CellValueChangedEventHandler(PropertyInfoTree_CellValueChanged);
        }

        /// <summary>
        /// ����2
        /// </summary>
        /// <param name="container"></param>
        public PropertyInfoTree(IContainer container):this()
        {
            container.Add(this);
        }

        void PropertyInfoTree_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            //TreeListNode levelroot = this.FocusedNode;
            //if (levelroot != null && levelroot.Nodes.Count == 0)
            //{
            //    PropertyInfo pi = levelroot.Tag as PropertyInfo;
            //    if (pi == null) return;
            //    Type t = pi.PropertyType;
            //    if (t.IsValueType) return;
            //    if (t == typeof(string)) return;
            //    FillTreeList(pi.PropertyType.GetProperties(BindingFlags.Public|BindingFlags.Instance), levelroot);
            //}
        }

        void PropertyInfoTree_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
           if (e.Column.FieldName == "ֵ" || e.Column.FieldName == "���")
           {
              string currproperty = e.Node["id"].ToString();
              string pv = ((e.Node["ֵ"] == null) || (string.IsNullOrEmpty(e.Node["ֵ"].ToString())))
					  ? string.Empty : e.Node["ֵ"].ToString();

              CompareOp op = (e.Node["���"] == null) ? CompareOp.None : (CompareOp)e.Node["���"];
              QCParam qcp = new QCParam(currproperty, op, pv);
              if (_qcParams.Settings.ContainsKey(currproperty))
              {
               //  if (!string.IsNullOrEmpty(pv))
                    _qcParams.Settings[currproperty] = qcp;
					  //else
					  //   _qcParams.Settings.Remove(currproperty);
              }
              else if (!string.IsNullOrEmpty(pv))
                 _qcParams.Settings.Add(currproperty, qcp);
           }
        }

        string GetParentPropertyName(TreeListNode node)
        {
            if (node == null || node.ParentNode == null) return string.Empty;
            string propertyName = node["����"].ToString();
            propertyName = GetParentPropertyName(node.ParentNode) + "." + propertyName;
            return propertyName;
        }

        /// <summary>
        /// ����������ʾ����
        /// </summary>
        public Type Class
        {
            get { return _currentClass; }
        }

        void ReloadTree()
        {
            this.ClearNodes();
            if (_currentClass == null) return;
            TreeListNode root = this.AppendNode(_currentClass, null);
            root.Tag = _currentClass;
            root.SetValue("����", _currentClass.Name);
            root.SetValue("id", _currentClass.Name);
            PropertyInfo[] pis = _currentClass.GetProperties();
            FillTreeList(pis, root);
        }

        void FillTreeList(PropertyInfo[] pis, TreeListNode root)
        {
            for (int i = 0; i < pis.Length; i++)
            {
                TreeListNode levelroot = this.AppendNode(pis[i], root);
                levelroot.Tag = pis[i];
                Type t = pis[i].PropertyType;
                object[] attrs = t.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    levelroot.SetValue("����", ((DisplayNameAttribute)attrs[0]).DisplayName);
                else
                    levelroot.SetValue("����", pis[i].Name);
                levelroot.SetValue("id", GetParentPropertyName(root) + "." + pis[i].Name);
            }
        }

        /// <summary>
        /// �������Լ���
        /// </summary>
        /// <param name="objtype"></param>
        public void SetClassParams(Type objtype)
        { 
            _currentClass = objtype;
            ReloadTree();
        }

        /// <summary>
        /// ����������������
        /// </summary>
        public void SetClassParams(string qcparams)
        {
            _qcParams = new QCParams(qcparams);
            FillPropValue();
        }

		 void FillPropValue()
		 {
			 foreach (TreeListNode node in this.Nodes)
			 {
				 node.SetValue("���", string.Empty);
				 node.SetValue("ֵ", string.Empty);
			 }
			 foreach (string key in _qcParams.Settings.Keys)
			 {
				 TreeListNode tnode = this.FindNodeByFieldValue("id", key);
				 if (tnode != null)
				 {
					 tnode.SetValue("���", _qcParams.Settings[key].Op);
					 tnode.SetValue("ֵ", _qcParams.Settings[key].PropValue);
				 }
			 }
		 }

        /// <summary>
        /// ͨ��ö�ٳ�ʼ����������
        /// </summary>
        /// <param name="evalue"></param>
        public void SetClassParams(Enum2Chinese.ChineseEnum evalue)
        {
            this.ClearNodes();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(QcResource.PropertiesDefine);
            string xpath = "qc/qct[@name='" + evalue.Name +"']/pp";
            XmlNodeList xnodes = xdoc.SelectNodes(xpath);
            foreach (XmlNode xnode in xnodes)
            {
                TreeListNode root = this.AppendNode(xnode, null);
                root.SetValue("����", xnode.Attributes["name"].Value);
                root.SetValue("id", xnode.Attributes["id"].Value);
            }
        }
    }
}
