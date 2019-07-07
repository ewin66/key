using DrectSoft.Resources;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace DrectSoft.JobManager
{
    /// <summary>
    /// ����ͬ��������
    /// </summary>
    public class SynchTreeView : TreeView
    {
        //public const string OtherSystem = "�Ѽ��ص���־";

        public Collection<TreeNode> SelectedNodes
        {
            get
            {
                if (_selectedNodes == null)
                    _selectedNodes = new Collection<TreeNode>();
                return _selectedNodes;
            }
        }
        private Collection<TreeNode> _selectedNodes;

        #region ctors
        public SynchTreeView(JobConfig jobConfig)
        {
            ImageList list = new ImageList();
            list.ImageSize = new Size(16, 16);
            list.Images.Add(ResourceManager.GetSmallIcon(ResourceNames.Perform, IconType.Normal), Color.Magenta);
            list.Images.Add(ResourceManager.GetSmallIcon(ResourceNames.Stop, IconType.Normal), Color.Magenta);
            list.Images.Add(ResourceManager.GetSmallIcon(ResourceNames.FolderOpen, IconType.Normal), Color.Magenta);
            list.Images.Add(ResourceManager.GetSmallIcon(ResourceNames.NewDocument, IconType.Normal), Color.Magenta);
            list.Images.Add(ResourceManager.GetSmallIcon(ResourceNames.Delete, IconType.Normal), Color.Magenta);
            this.ImageList = list;
            this.CheckBoxes = false;
            this.HideSelection = false;

            CreateTree(jobConfig);


            this.AfterSelect += new TreeViewEventHandler(SynchTreeView_AfterSelect);
        }
        #endregion

        #region event handler
        public event EventHandler<TreeViewEventArgs> SelectedNodeChanged;
        protected void OnSelectedNodeChanged(TreeViewEventArgs e)
        {
            if (SelectedNodeChanged != null)
                SelectedNodeChanged(this, e);
        }
        #endregion

        #region public methods
        //public TreeNode AddNode(SynchMissionConfigration config)
        //{
        //   if (config == null)
        //      return null;
        //   TreeNode rootNode = null, targetNode = null;
        //   if (string.IsNullOrEmpty(config.MissionSystemName))
        //   {//���û��ϵͳ���ƣ�ȫ�����Ѽ�����־������
        //      if (!this.Nodes.ContainsKey(config.MissionName))
        //      {
        //         if (this.Nodes.ContainsKey(OtherSystem))
        //            rootNode = this.Nodes[this.Nodes.IndexOfKey(OtherSystem)];
        //         else
        //            rootNode = this.Nodes.Add(OtherSystem, OtherSystem);
        //         SetNodeIcon(rootNode);
        //      }
        //      else
        //         return this.Nodes[this.Nodes.IndexOfKey(config.MissionName)];
        //   }
        //   else
        //   {//��ϵͳ���ƣ�����Ѿ�������ڵ㣬������棬�����¿�һ��ϵͳ�ڵ�
        //      if (this.Nodes.ContainsKey(config.MissionSystemName))
        //         rootNode = this.Nodes[this.Nodes.IndexOfKey(config.MissionSystemName)];
        //      else
        //         rootNode = this.Nodes.Add(config.MissionSystemName, config.MissionSystemName);
        //      SetNodeIcon(rootNode);
        //   }
        //   targetNode = rootNode.Nodes.Add(config.MissionName, config.MissionName);
        //   targetNode.Tag = config;
        //   SetNodeIcon(targetNode);
        //   return targetNode;
        //}

        public void SetNodeIcon(TreeNode node)
        {
            if (node == null)
                return;

            Job job = node.Tag as Job;

            if (job != null)
            {
                if (job.Action == null)
                    node.ImageIndex = 4;
                else
                    node.ImageIndex = job.Enable ? 0 : 1;
            }
            else //����Ƕ����ڵ㣬���Ϊ�ļ��м���
                node.ImageIndex = 2;

            node.SelectedImageIndex = node.ImageIndex;
        }
        #endregion

        #region private
        void SynchTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnSelectedNodeChanged(e);
        }


        private int FindTreeNode(string key)
        {
            if (string.IsNullOrEmpty(key))
                return -1;
            for (int index = 0; index < SelectedNodes.Count; index++)
            {
                if (SelectedNodes[index].Name == key)
                    return index;
            }
            return -1;
        }

        private void CreateTree(JobConfig jobConfig)
        {
            if (jobConfig != null)
            {
                TreeNode root;
                TreeNode child;

                foreach (SystemsJobDefine system in jobConfig.JobsOfSystem)
                {
                    // �������ڵ�
                    root = Nodes.Add(system.Name);
                    SetNodeIcon(root);

                    foreach (Job job in system.Jobs)
                    {
                        child = root.Nodes.Add(job.Name);
                        child.Tag = job;
                        SetNodeIcon(child);
                    }
                }
            }
        }
        #endregion
    }

}
