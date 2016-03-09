using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace RtmpDecoderUI
{
  public class ByteSegmentViewer:ByteArrayViewer
  {
    public ByteSegmentViewer(){}

    public ByteSegmentViewer(int bytesPerLine, AdditionType additions)
      : base(bytesPerLine, additions){}

    private List<SegmentRange> mCurrentHighlightedStringSegmentRange;
    public override byte[] ByteArray
    {
      set
      {
        CurrentHighlightedStringSegmentRange = null;
        base.ByteArray = value;
      }
    }
    

    public void HighLight(int startByteIndex, int endByteIndex)
    {
      SegmentRange segmentRange=ByteArrayStringFormatter.GetStringSegmentRange(startByteIndex, endByteIndex);
      CurrentHighlightedStringSegmentRange = new List<SegmentRange>{segmentRange};
    }

    protected List<SegmentRange> CurrentHighlightedStringSegmentRange
    {
      get
      {
        return mCurrentHighlightedStringSegmentRange;
      }
      set
      {
        if (mCurrentHighlightedStringSegmentRange != value)
        {
          if (mCurrentHighlightedStringSegmentRange != null)
          {
            DeHighLightStringSegmentRange(mCurrentHighlightedStringSegmentRange);
          }
          mCurrentHighlightedStringSegmentRange = value;
          if (mCurrentHighlightedStringSegmentRange != null)
          {
            HighLightStringSegmentRange(mCurrentHighlightedStringSegmentRange);
          }
        }
      }
    }

    private void HighLightStringSegmentRange(List<SegmentRange> segmentRange)
    {
      segmentRange.ForEach(s => RefreshStringSegmentRange(s, Color.Yellow));
    }
    private void DeHighLightStringSegmentRange(List<SegmentRange> segmentRange)
    {
      segmentRange.ForEach(s => RefreshStringSegmentRange(s, this.BackColor));
    }

    private void ResetHighLight()
    {
      this.SelectAll();
      this.SelectionBackColor = this.BackColor;
      this.DeselectAll();
    }

    private void RefreshStringSegmentRange(SegmentRange segmentRange, Color color)
    {
      foreach (IRangeNode r in segmentRange.SubRanges)
      {
        this.Select(r.Range.Begin, r.Range.Length);
        this.SelectionBackColor = color;
        this.DeselectAll();
      }
    }

  }
}
