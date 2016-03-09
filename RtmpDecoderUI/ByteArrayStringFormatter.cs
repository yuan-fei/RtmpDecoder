using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpDecoderUI
{
  public class ByteArrayStringFormatter
  {
    private const char UNIT_SPACE = ' ';
    private const string INDICATOR_SUFFIX_SPACE = "    ";
    private const string ASCII_PREFFIX_SPACE = "    ";

    
    public ByteArrayStringFormatter():this(16,AdditionType.Indicator|AdditionType.AscII){}
    public ByteArrayStringFormatter(int bytesPerLine) : this(bytesPerLine, AdditionType.Indicator | AdditionType.AscII) { }
    public ByteArrayStringFormatter(AdditionType additionType) : this(16,additionType) { }
    public ByteArrayStringFormatter(int bytesPerLine,AdditionType additionType)
    {
      BytesPerLine = bytesPerLine;
      AdditionType = additionType;
    }

    public int BytesPerLine { get; set; }
    public AdditionType AdditionType { get; private set; }
    #region Constant
    private bool HasIndicator
    {
      get { return 0!=(AdditionType & AdditionType.Indicator); }
    }

    private bool HasAscIIContent
    {
      get { return 0!=(AdditionType & AdditionType.AscII); }
    }

    private int IndicatorLength
    {
      get { return 8; }
    }

    private int ByteLength
    {
      get { return 2; }
    }

    private int AscIILength
    {
      get { return 1; }
    }

    private int UnitSpaceLength
    {
      get { return 1; }
    }

    private int IndicatorSuffixSpaceLength
    {
      get { return INDICATOR_SUFFIX_SPACE.Length; }
    }

    private int AscIIPreffixSpaceLength
    {
      get { return ASCII_PREFFIX_SPACE.Length; }
    }

    private int NewLineLength
    {
      get { return 1; }
    }

    private int BytesContentLengthPerLine 
    { 
      get { return BytesPerLine*(ByteLength+UnitSpaceLength) - 1; }
    }

    private int AscIIContentLengthPerLine 
    { 
      get { return BytesPerLine*(AscIILength+UnitSpaceLength) - 1; }
    }



    private int LengthOfLine
    {
      get
      {
        int lengthOfLine = BytesContentLengthPerLine;
        if (HasIndicator)
        {
          lengthOfLine = IndicatorLength + lengthOfLine + IndicatorSuffixSpaceLength;
        }
        if (HasAscIIContent)
        {
          lengthOfLine = lengthOfLine + AscIIPreffixSpaceLength + AscIIContentLengthPerLine;
        }
        lengthOfLine += NewLineLength;

        return lengthOfLine;
      }
    }
    #endregion

    private int GetStartIndexOfLine(int lineNo)
    {
      int startIndex = lineNo * LengthOfLine;
      if(HasIndicator)
      {
        startIndex +=(IndicatorLength + IndicatorSuffixSpaceLength);
      }
      return startIndex;
    }

    private int GetEndIndexOfLine(int lineNo)
    {
      int startIndex = GetStartIndexOfLine(lineNo);
      return startIndex + BytesContentLengthPerLine-1;
    }

    private int GetStartIndexOfByte(int byteIndex)
    {
      int lineNo = byteIndex / BytesPerLine;
      int offset = byteIndex % BytesPerLine;
      int index = lineNo * LengthOfLine +offset * (ByteLength+UnitSpaceLength);
      if(HasIndicator)
      {
        index += IndicatorLength + IndicatorSuffixSpaceLength;
      }
      return index;
    }

    private int GetEndIndexOfByte(int byteIndex)
    {
      return GetStartIndexOfByte(byteIndex) + ByteLength - 1;
    }

    public SegmentRange GetStringSegmentRange(int startByteIndex, int endByteIndex)
    {
      int startLineNo = startByteIndex / BytesPerLine;
      int linefirstStart = GetStartIndexOfByte(startByteIndex);
      int lineFirstEnd = GetEndIndexOfLine(startLineNo);


      int endLineNo = endByteIndex / BytesPerLine;
      int lineLastStart = GetStartIndexOfLine(endLineNo);
      int lineLastEnd = GetEndIndexOfByte(endByteIndex);
      List<IRangeNode> ranges=new List<IRangeNode>();
      if (lineFirstEnd > lineLastStart)
      {
        //start and end in same line
        ranges.Add(new LineRange(linefirstStart, lineLastEnd));
      }
      else
      {
        //start and end in different lines
        ranges.Add(new LineRange(linefirstStart, lineFirstEnd));
        for (int i = startLineNo + 1; i < endLineNo; i++)
        {
          ranges.Add(new LineRange(GetStartIndexOfLine(i), GetEndIndexOfLine(i)));
        }
        ranges.Add(new LineRange(lineLastStart, lineLastEnd));
      }
      return new SegmentRange(ranges);
    }


    public List<SegmentRange> GetStringSegmentRanges(List<int[]> byteBlockRanges)
    {
      if (byteBlockRanges != null)
      {
        List<SegmentRange> blockRanges = new List<SegmentRange>(byteBlockRanges.Count);
        blockRanges.AddRange(byteBlockRanges.Select(range => GetStringSegmentRange(range[0], range[1])));
        return blockRanges;
      }
      return new List<SegmentRange>(0);
    }

    
    public string GetFormatedString(byte[] bytes)
    {
      return FormatBytes(bytes);
    }

    private string FormatBytes(byte[] bytes)
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < bytes.Length; i = i + BytesPerLine)
      {
        sb.Append(FormatBytesLine(bytes,i));
        if (i + BytesPerLine < bytes.Length)
        {
          sb.AppendLine();
        }
      }
      return sb.ToString();
    }

    private string FormatBytesLine(byte[] bytes,int start)
    {
      StringBuilder sb = new StringBuilder();
      if (HasIndicator)
      {
        sb.Append(start.ToString("X8"));
        sb.Append(INDICATOR_SUFFIX_SPACE);
      }
      StringBuilder sbASC = new StringBuilder();
      int end = start + BytesPerLine;
      string placeHolderSpace=new string(UNIT_SPACE,ByteLength);
      for (int i = start; i < end; i++)
      {
        if (i < bytes.Length)
        {
          sb.Append(ByteToHexString(bytes[i]));
          sbASC.Append(ByteToASCIIString(bytes[i]));
        }
        else
        {
          if (HasAscIIContent)
          {
            sb.Append(placeHolderSpace);
          }
          else
          {
            break;
          }
        }
        sb.Append(UNIT_SPACE);
        sbASC.Append(UNIT_SPACE);
      }
      if (sb.Length > 0)
      {
        sb.Remove(sb.Length - UnitSpaceLength, UnitSpaceLength);
        sbASC.Remove(sbASC.Length - UnitSpaceLength, UnitSpaceLength);
      }
      if (HasAscIIContent)
      {
        sb.Append(ASCII_PREFFIX_SPACE);
        sb.Append(sbASC.ToString());
      }
      return sb.ToString();
    }

    private string ByteToHexString(byte bt)
    {
      return bt.ToString("X2");
    }

    private char ByteToASCIIString(byte bt)
    {
      if (bt < 0x7E && bt > 0x20)
      {
        return (char)bt;
      }
      else
      {
        return '.';
      }

    }

    
  }

  public class Range
  {
    public int Begin { get; set; }
    public int End { get; set; }

    public Range(int begin, int end)
    {
      Begin = begin;
      End = end;
    }

    public bool Contains(int point)
    {
      return point >= Begin && point <= End;
    }

    public int Length
    {
      get { return End - Begin + 1; }
    }
  }

  public interface IRangeNode
  {
    Range Range { get; }
    bool Contains(int point);
    List<IRangeNode> SubRanges { get; }
  }

  class LineRange : IRangeNode
  {
    public LineRange(int start, int end)
    {
      Range = new Range(start, end);
    }

    public Range Range { get; private set; }

    public bool Contains(int point)
    {
      return Range.Contains(point);
    }

    public List<IRangeNode> SubRanges
    {
      get { return new List<IRangeNode>(0); }
    }
  }

  public class SegmentRange : IRangeNode
  {
    public SegmentRange(List<IRangeNode> ranges)
    {
      SubRanges = ranges;
      Range = new Range(SubRanges.First().Range.Begin, SubRanges.Last().Range.End);
    }

    public Range Range { get; private set; }

    public bool Contains(int point)
    {
      if (!Range.Contains(point))
      {
        return false;
      }
      return SubRanges.Any(r => r.Contains(point));
    }

    public List<IRangeNode> SubRanges { get; private set; }
  }

  [Flags]
  public enum AdditionType
  {
    None=0,
    Indicator=1,
    AscII=2
  }
}
