using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RtmpDecoderUI
{
  public partial class SketchBoardForm : Form
  {
    public SketchBoardForm()
    {
      InitializeComponent();
    }

    public string ByteText { get; set; }

    private void button1_Click(object sender, EventArgs e)
    {
      ByteText = this.richTextBox1.Text;
      DialogResult = DialogResult.OK;
      this.Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      this.Close();
    }
  }
}
