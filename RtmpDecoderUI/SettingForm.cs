using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RtmpDecoder;

namespace RtmpDecoderUI
{
  public partial class SettingForm : Form
  {
    public SettingForm()
    {
      InitializeComponent();
      this.textBox1.Text = ApplicationSettings.Instance.DecodingChunkSize.ToString();
    }

    

    private void button1_Click(object sender, EventArgs e)
    {
      ApplicationSettings.Instance.DecodingChunkSize = int.Parse(this.textBox1.Text);
      this.DialogResult = DialogResult.OK;
      Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      Close();
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      int value;
      if(int.TryParse(textBox1.Text, out value))
      {
        if(value<128||value>65536)
        {
          label2.Text = "Size out of range";
          this.button1.Enabled = false;
        }
        else
        {
          label2.Text = "";
          this.button1.Enabled = true;
        }
      }
    }
  }
}
