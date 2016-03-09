using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace RtmpDecoderUI
{

  public class ByteSegmentSelecter:ByteSegmentViewer
  {
    private List<List<int[]>> mByteSegments;
    private int mSelectedByteSegmentIndex = -1;

    public ByteSegmentSelecter()
    {
      
    }

    public ByteSegmentSelecter(int bytesPerLine, AdditionType additions)
      : base(bytesPerLine, additions) { }

    public override byte[] ByteArray
    {
      set
      {
        base.ByteArray = value;
        AfterByteArrayChange();
      }
    }

    public virtual List<List<int[]>> ByteSegments
    {
      get { return mByteSegments; }
      set
      {
        CurrentHighlightedStringSegmentRange=null;
        StringSegmentRanges = null;
        mByteSegments = value;
        if (mByteSegments != null)
        {
          StringSegmentRanges = new List<List<SegmentRange>>();
          mByteSegments.ForEach(bs => StringSegmentRanges.Add(ByteArrayStringFormatter.GetStringSegmentRanges(bs)));
        }
        AfterByteSegmentsChange();
      }
    }

    protected List<List<SegmentRange>> StringSegmentRanges { get; private set; }
    public int SelectedByteSegmentIndex 
    {
      get { return mSelectedByteSegmentIndex; }
      private set { mSelectedByteSegmentIndex = value; } 
    }

    public event Action<int> ByteSegmentSelected;

    protected virtual void AfterByteArrayChange()
    {
      OnDataChange();
    }

    protected virtual void AfterByteSegmentsChange()
    {
      OnDataChange();
    }

    private void OnDataChange()
    {
      this.Cursor = Cursors.Default;
      this.MouseMove -= OnMouseMoved;
      this.MouseClick -= OnMouseClicked;
      if (ByteArray != null && ByteSegments != null)
      {
        this.MouseMove += OnMouseMoved;
        this.MouseClick += OnMouseClicked;
      }
    }

    private void OnMouseClicked(object sender, MouseEventArgs e)
    {
      int charIndex = this.GetCharIndexFromPosition(e.Location);
      int index = StringSegmentRanges.FindIndex(rs => rs.Any(r=>r.Contains(charIndex)));
      if(index!=-1)
      {
        SelectedByteSegmentIndex = index;
        //MessageBox.Show(string.Format("Message {0} Selcted", SelectedByteSegmentIndex + 1));
        if (ByteSegmentSelected != null)
        {
          ByteSegmentSelected(index);
        }
      }
    }


    private void OnMouseMoved(object sender, MouseEventArgs e)
    {
      int charIndex = this.GetCharIndexFromPosition(e.Location);
      List<SegmentRange> currentSegRange = StringSegmentRanges.Find(rs => rs.Any(r => r.Contains(charIndex)));
      if (currentSegRange != null)
      {
        CurrentHighlightedStringSegmentRange = currentSegRange;
        if (this.Cursor != Cursors.Hand)
        {
          this.Cursor = Cursors.Hand;
        }
      }
      else
      {
        if (this.Cursor != Cursors.Default)
        {
          this.Cursor = Cursors.Default;
        }
      }
    }

    
  }

  

}
