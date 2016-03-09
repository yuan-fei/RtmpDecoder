using System;
using System.Windows.Forms;

namespace RtmpDecoderUI
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.Views = new System.Windows.Forms.TabControl();
      this.chunksView = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.messageView = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sketchBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sketchBoardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.byteSegmentSelector1 = new RtmpDecoderUI.ByteSegmentSelecter();
      this.chunksTreeView = new RtmpDecoderUI.ByteSegmentDescriptionTreeView();
      this.byteSegmentViewer1 = new RtmpDecoderUI.ByteSegmentViewer();
      this.byteSegmentViewer2 = new RtmpDecoderUI.ByteSegmentViewer();
      this.messageTreeView = new RtmpDecoderUI.ByteSegmentDescriptionTreeView();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.Views.SuspendLayout();
      this.chunksView.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.messageView.SuspendLayout();
      this.tableLayoutPanel4.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 24);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.Views);
      this.splitContainer1.Size = new System.Drawing.Size(909, 523);
      this.splitContainer1.SplitterDistance = 189;
      this.splitContainer1.SplitterWidth = 10;
      this.splitContainer1.TabIndex = 0;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.byteSegmentSelector1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.51832F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.48167F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(909, 189);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(903, 21);
      this.label1.TabIndex = 0;
      this.label1.Text = "Loaded Data";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // Views
      // 
      this.Views.Controls.Add(this.chunksView);
      this.Views.Controls.Add(this.messageView);
      this.Views.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Views.Location = new System.Drawing.Point(0, 0);
      this.Views.Name = "Views";
      this.Views.SelectedIndex = 0;
      this.Views.Size = new System.Drawing.Size(909, 324);
      this.Views.TabIndex = 0;
      // 
      // chunksView
      // 
      this.chunksView.Controls.Add(this.tableLayoutPanel3);
      this.chunksView.Location = new System.Drawing.Point(4, 22);
      this.chunksView.Name = "chunksView";
      this.chunksView.Padding = new System.Windows.Forms.Padding(3);
      this.chunksView.Size = new System.Drawing.Size(901, 298);
      this.chunksView.TabIndex = 1;
      this.chunksView.Text = "Chunks";
      this.chunksView.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.ColumnCount = 2;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.62011F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.37989F));
      this.tableLayoutPanel3.Controls.Add(this.chunksTreeView, 0, 0);
      this.tableLayoutPanel3.Controls.Add(this.byteSegmentViewer1, 1, 0);
      this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 1;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(895, 292);
      this.tableLayoutPanel3.TabIndex = 0;
      // 
      // messageView
      // 
      this.messageView.Controls.Add(this.tableLayoutPanel4);
      this.messageView.Location = new System.Drawing.Point(4, 22);
      this.messageView.Name = "messageView";
      this.messageView.Padding = new System.Windows.Forms.Padding(3);
      this.messageView.Size = new System.Drawing.Size(901, 298);
      this.messageView.TabIndex = 2;
      this.messageView.Text = "Message";
      this.messageView.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanel4
      // 
      this.tableLayoutPanel4.ColumnCount = 2;
      this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.62011F));
      this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.37989F));
      this.tableLayoutPanel4.Controls.Add(this.byteSegmentViewer2, 1, 0);
      this.tableLayoutPanel4.Controls.Add(this.messageTreeView, 0, 0);
      this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
      this.tableLayoutPanel4.Name = "tableLayoutPanel4";
      this.tableLayoutPanel4.RowCount = 1;
      this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel4.Size = new System.Drawing.Size(895, 292);
      this.tableLayoutPanel4.TabIndex = 0;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
      this.tableLayoutPanel2.TabIndex = 0;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.sketchBoardToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(909, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem1,
            this.loadToolStripMenuItem,
            this.settingsToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // loadToolStripMenuItem1
      // 
      this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
      this.loadToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
      this.loadToolStripMenuItem1.Text = "Load";
      this.loadToolStripMenuItem1.Click += new System.EventHandler(this.loadToolStripMenuItem1_Click);
      // 
      // loadToolStripMenuItem
      // 
      this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
      this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.loadToolStripMenuItem.Text = "LoadSample";
      this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
      // 
      // sketchBoardToolStripMenuItem
      // 
      this.sketchBoardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sketchBoardToolStripMenuItem1});
      this.sketchBoardToolStripMenuItem.Name = "sketchBoardToolStripMenuItem";
      this.sketchBoardToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
      this.sketchBoardToolStripMenuItem.Text = "Edit";
      // 
      // sketchBoardToolStripMenuItem1
      // 
      this.sketchBoardToolStripMenuItem1.Name = "sketchBoardToolStripMenuItem1";
      this.sketchBoardToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
      this.sketchBoardToolStripMenuItem1.Text = "SketchBoard";
      this.sketchBoardToolStripMenuItem1.Click += new System.EventHandler(this.sketchBoardToolStripMenuItem1_Click);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // settingsToolStripMenuItem
      // 
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.settingsToolStripMenuItem.Text = "Settings";
      this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
      // 
      // byteSegmentSelector1
      // 
      this.byteSegmentSelector1.BackColor = System.Drawing.SystemColors.Window;
      this.byteSegmentSelector1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.byteSegmentSelector1.ByteArray = null;
      this.byteSegmentSelector1.ByteSegments = null;
      this.byteSegmentSelector1.Cursor = System.Windows.Forms.Cursors.Default;
      this.byteSegmentSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.byteSegmentSelector1.Font = new System.Drawing.Font("Courier New", 9F);
      this.byteSegmentSelector1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.byteSegmentSelector1.Location = new System.Drawing.Point(3, 24);
      this.byteSegmentSelector1.Name = "byteSegmentSelector1";
      this.byteSegmentSelector1.ReadOnly = true;
      this.byteSegmentSelector1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
      this.byteSegmentSelector1.Size = new System.Drawing.Size(903, 162);
      this.byteSegmentSelector1.TabIndex = 0;
      this.byteSegmentSelector1.Text = "";
      // 
      // chunksTreeView
      // 
      this.chunksTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.chunksTreeView.Location = new System.Drawing.Point(3, 3);
      this.chunksTreeView.Name = "chunksTreeView";
      this.chunksTreeView.RootDescription = null;
      this.chunksTreeView.Size = new System.Drawing.Size(276, 286);
      this.chunksTreeView.TabIndex = 0;
      // 
      // byteSegmentViewer1
      // 
      this.byteSegmentViewer1.ByteArray = null;
      this.byteSegmentViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.byteSegmentViewer1.Font = new System.Drawing.Font("Courier New", 9F);
      this.byteSegmentViewer1.Location = new System.Drawing.Point(285, 3);
      this.byteSegmentViewer1.Name = "byteSegmentViewer1";
      this.byteSegmentViewer1.Size = new System.Drawing.Size(607, 286);
      this.byteSegmentViewer1.TabIndex = 1;
      this.byteSegmentViewer1.Text = "";
      // 
      // byteSegmentViewer2
      // 
      this.byteSegmentViewer2.ByteArray = null;
      this.byteSegmentViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.byteSegmentViewer2.Font = new System.Drawing.Font("Courier New", 9F);
      this.byteSegmentViewer2.Location = new System.Drawing.Point(285, 3);
      this.byteSegmentViewer2.Name = "byteSegmentViewer2";
      this.byteSegmentViewer2.Size = new System.Drawing.Size(607, 286);
      this.byteSegmentViewer2.TabIndex = 0;
      this.byteSegmentViewer2.Text = "";
      // 
      // messageTreeView
      // 
      this.messageTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.messageTreeView.Location = new System.Drawing.Point(3, 3);
      this.messageTreeView.Name = "messageTreeView";
      this.messageTreeView.RootDescription = null;
      this.messageTreeView.Size = new System.Drawing.Size(276, 286);
      this.messageTreeView.TabIndex = 1;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(909, 547);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Form1";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.Views.ResumeLayout(false);
      this.chunksView.ResumeLayout(false);
      this.tableLayoutPanel3.ResumeLayout(false);
      this.messageView.ResumeLayout(false);
      this.tableLayoutPanel4.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private ByteSegmentSelecter byteSegmentSelector1;
    private System.Windows.Forms.TabControl Views;
    private System.Windows.Forms.TabPage chunksView;
    private System.Windows.Forms.TabPage messageView;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
    private ByteSegmentDescriptionTreeView chunksTreeView;
    private ByteSegmentViewer byteSegmentViewer1;
    private TableLayoutPanel tableLayoutPanel4;
    private ByteSegmentViewer byteSegmentViewer2;
    private ByteSegmentDescriptionTreeView messageTreeView;
    private ToolStripMenuItem loadToolStripMenuItem1;
    private OpenFileDialog openFileDialog1;
    private ToolStripMenuItem sketchBoardToolStripMenuItem;
    private ToolStripMenuItem sketchBoardToolStripMenuItem1;
    private ToolStripMenuItem settingsToolStripMenuItem;

  }
}

