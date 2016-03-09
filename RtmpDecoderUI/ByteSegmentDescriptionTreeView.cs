using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FluorineFx.Messaging.Rtmp.Description;

namespace RtmpDecoderUI
{
  public class ByteSegmentDescriptionTreeView:TreeView
  {
    private IDescription mRootDescription;
    public IDescription RootDescription
    {
      get { return mRootDescription; }
      set
      {
        mRootDescription = value;
        BuildDescriptionTree();
      }
    }

    public event Action<IDescription> DescriptionSelected;

    private void BuildDescriptionTree()
    {
      this.Nodes.Clear();
      this.NodeMouseClick -= OnNodeMouseClick;
      this.NodeMouseDoubleClick -= OnNodeMouseDoubleClick;
      if (RootDescription != null)
      {
        CreateDescriptionNode(this.Nodes, RootDescription);
        this.NodeMouseClick += OnNodeMouseClick;
        this.NodeMouseDoubleClick += OnNodeMouseDoubleClick;
        this.ExpandAll();
      }
    }

    private void CreateDescriptionNode(TreeNodeCollection root, IDescription description)
    {
      TreeNode node = new TreeNode(description.Describe());
      node.Tag = description;
      root.Add(node);
      List<IDescription> subdescriptions = description.SubDescriptions();
      if (subdescriptions != null && subdescriptions.Count > 0)
      {
        subdescriptions.ForEach(d => CreateDescriptionNode(node.Nodes, d));
      }
    }

    void OnNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      IDescription description = (IDescription)e.Node.Tag;
      DescriptionSelected(description);
    }

    void OnNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      IDescription description = (IDescription)e.Node.Tag;
      List<IDescription> descriptions = description.SubDescriptions();
      if (descriptions == null || descriptions.Count == 0)
      {
        MessageBox.Show(description.Describe(), "Node Explorer");
      }
    }
  }
}
