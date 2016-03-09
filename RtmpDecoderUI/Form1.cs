using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RtmpDecoder;
using FluorineFx.Messaging.Rtmp;
using FluorineFx.Messaging.Rtmp.Description;

namespace RtmpDecoderUI
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      DataContext.Instance.AfterLoadedBytesChanged += DisplayDecoding;
    }

    private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      openFileDialog1=new OpenFileDialog();
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        try
        {
          TextFileLoader textFileLoader = new TextFileLoader(openFileDialog1.FileName);
          byte[] bytes = textFileLoader.Load();
          Initialize(bytes);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
        }
      }

    }

    private void loadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TextFileLoader textFileLoader = new TextFileLoader("./data/bytesHandshake.txt");
      byte[] bytes = textFileLoader.Load();
      Initialize(bytes);
    }

    private void Initialize(byte[] bytes)
    {
      DataContext.Instance.LoadedBytes = bytes;
      //int[] m1 = { 0, 15 };
      //int[] m2 = { 16, 42 };

      byteSegmentSelector1.ByteArray = bytes;
      byteSegmentSelector1.ByteSegments = DataContext.Instance.PacketsRange;

      byteSegmentSelector1.ByteSegmentSelected -= this.byteSegmentSelector1SegmentSelected;
      DataContext.Instance.CurrentPacketChanged -= this.chunkTreeViewOnCurrentPacketChanged;
      DataContext.Instance.CurrentPacketChanged -= this.messageTreeViewOnCurrentPacketChanged;
      DataContext.Instance.CurrentPacketChanged -= this.byteSegmentViewer1OnCurrentPacketChanged;
      DataContext.Instance.CurrentPacketChanged -= this.byteSegmentViewer2OnCurrentPacketChanged;

      if (DataContext.Instance.PacketsRange != null)
      {
        
        byteSegmentSelector1.ByteSegmentSelected += this.byteSegmentSelector1SegmentSelected;
        DataContext.Instance.CurrentPacketChanged += this.chunkTreeViewOnCurrentPacketChanged;
        DataContext.Instance.CurrentPacketChanged += this.messageTreeViewOnCurrentPacketChanged;
        DataContext.Instance.CurrentPacketChanged += this.byteSegmentViewer1OnCurrentPacketChanged;
        DataContext.Instance.CurrentPacketChanged += this.byteSegmentViewer2OnCurrentPacketChanged;

      }
    }

    private void DisplayDecoding(object obj)
    {
      int decodedPacketsCount = 0;
      if (DataContext.Instance.PacketsRange != null)
      {
        decodedPacketsCount = DataContext.Instance.PacketsRange.Count;
      }
      label1.Text = string.Format("Loaded bytes ({0} RTMP Packets)", decodedPacketsCount);
    }

    private void byteSegmentSelector1SegmentSelected(int index)
    {
      //MessageBox.Show(string.Format("Message {0} Selcted", index + 1));
      DataContext.Instance.CurrentPacketIndex = index;
    }

    private void chunkTreeViewOnCurrentPacketChanged()
    {
      RtmpPacket packet = (RtmpPacket)DataContext.Instance.CurrentPacket;
      if (packet == null)
      {
        chunksTreeView.RootDescription = null;
        chunksTreeView.DescriptionSelected -= byteSegmentViewer1OnDescriptionSelected;
      }
      else
      {
        IDescription chunkViewDescription = packet.PacketDescription.ChunkView;
        chunksTreeView.RootDescription = chunkViewDescription;
        chunksTreeView.DescriptionSelected -= byteSegmentViewer1OnDescriptionSelected;
        chunksTreeView.DescriptionSelected += byteSegmentViewer1OnDescriptionSelected;
      }
    }

    private void messageTreeViewOnCurrentPacketChanged()
    {
      RtmpPacket packet = (RtmpPacket)DataContext.Instance.CurrentPacket;
      if (packet == null)
      {
        messageTreeView.RootDescription = null;
        messageTreeView.DescriptionSelected -= byteSegmentViewer2OnDescriptionSelected;
      }
      else
      {
        IDescription chunkViewDescription = packet.PacketDescription.MessageView;
        messageTreeView.RootDescription = chunkViewDescription;
        messageTreeView.DescriptionSelected -= byteSegmentViewer2OnDescriptionSelected;
        messageTreeView.DescriptionSelected += byteSegmentViewer2OnDescriptionSelected;
      }
    }

    private void byteSegmentViewer1OnCurrentPacketChanged()
    {
      RtmpPacket packet = (RtmpPacket)DataContext.Instance.CurrentPacket;
      if (packet == null)
      {
        byteSegmentViewer1.ByteArray = null;
      }
      else
      {
        ChunkViewDescription chunkViewDescription = packet.PacketDescription.ChunkView;
        byteSegmentViewer1.ByteArray = chunkViewDescription.Bytes;
      }
    }

    private void byteSegmentViewer2OnCurrentPacketChanged()
    {
      RtmpPacket packet = (RtmpPacket)DataContext.Instance.CurrentPacket;
      if (packet == null)
      {
        byteSegmentViewer2.ByteArray = null;
      }
      else
      {
        MessageViewDescription messageViewDescription = packet.PacketDescription.MessageView;
        byteSegmentViewer2.ByteArray = messageViewDescription.Bytes;
      }
    }

    void byteSegmentViewer1OnDescriptionSelected(IDescription description)
    {
      BytesRange bytesRange = description.BytesRange;
      byteSegmentViewer1.HighLight((int)bytesRange.Start.ByteIndex, (int)bytesRange.End.ByteIndex);
    }

    void byteSegmentViewer2OnDescriptionSelected(IDescription description)
    {
      BytesRange bytesRange = description.BytesRange;
      byteSegmentViewer2.HighLight((int)bytesRange.Start.ByteIndex, (int)bytesRange.End.ByteIndex);
    }


    private void sketchBoardToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      SketchBoardForm sketchBoard = new SketchBoardForm();
      if (sketchBoard.ShowDialog() == DialogResult.OK)
      {
        TextLoader loader = new TextLoader(sketchBoard.ByteText);
        byte[] bytes = loader.Load();
        if (bytes.Length > 0)
        {
          Initialize(bytes);
        }
      }
    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SettingForm settingForm=new SettingForm();
      if(settingForm.ShowDialog()==DialogResult.OK)
      {
        Initialize(DataContext.Instance.LoadedBytes);
      }
    }

  }
}
