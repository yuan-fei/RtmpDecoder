using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace RtmpDecoderUI
{
  public class ByteArrayViewer : RichTextBox
  {
    private readonly Font mTextFont = new Font("Courier New", 9);
    private byte[] mBytes;
    protected ByteArrayStringFormatter ByteArrayStringFormatter { get; set; }

    public ByteArrayViewer():this(16,AdditionType.AscII|AdditionType.Indicator){}
    

    public ByteArrayViewer(int bytesPerLine, AdditionType additions)
    {
      this.Multiline = true;
      this.Font = mTextFont;
      this.ByteArrayStringFormatter = new ByteArrayStringFormatter(bytesPerLine,additions);
    }


    public virtual byte[] ByteArray
    {
      get { return mBytes; }
      set
      {
        mBytes = value;
        if (value != null)
        {
          this.Text = ByteArrayStringFormatter.GetFormatedString(mBytes);
        }
        else
        {
          this.Text = "";
        }
      }
      
    }

    //hide the caret

    [DllImport("user32.dll", EntryPoint = "ShowCaret")]

    public static extern long ShowCaret(IntPtr hwnd);

    [DllImport("user32.dll", EntryPoint = "HideCaret")]

    public static extern long HideCaret(IntPtr hwnd);
    private int WM_SETFOCUS = 0x0007;

    protected override void WndProc(ref Message m)
    {
      if (m.Msg != WM_SETFOCUS)
      {
        base.WndProc(ref m);
      }
      HideCaret(this.Handle);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.ResumeLayout(false);

    }

    
  }


  
}