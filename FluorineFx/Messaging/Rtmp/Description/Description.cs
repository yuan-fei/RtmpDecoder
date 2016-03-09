using System;
using System.Collections.Generic;
using System.Linq;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public class BytePosition : IComparable<BytePosition>
  {
    public BytePosition(long byteIndex,int bitIndex)
    {
      ByteIndex = byteIndex;
      BitIndex = bitIndex;
    }

    public const int MIN_BIT_INDEX = 0;
    public const int MAX_BIT_INDEX = 7;
    public long ByteIndex { get; private set; }
    public int BitIndex { get; private set; }
    public int CompareTo(BytePosition other)
    {
      int byteIndexDiff = (int)(this.ByteIndex - other.ByteIndex);
      return (byteIndexDiff==0)?(this.BitIndex - other.BitIndex):byteIndexDiff;
    }

    public override string ToString()
    {
      return string.Format("Byte {0} Bit {1}",ByteIndex,BitIndex);
    }
  }

  public class BytesRange
  {
    public static BytesRange Range(long startByteIndex, long endByteIndex)
    {
      return Range(new BytePosition(startByteIndex, BytePosition.MIN_BIT_INDEX), new BytePosition(endByteIndex, BytePosition.MAX_BIT_INDEX));
    }

    public static BytesRange Range(long startByteIndex, int startBitIndex, long endByteIndex, int endBitIndex)
    {
      return Range(new BytePosition(startByteIndex, startBitIndex), new BytePosition(endByteIndex, endBitIndex));
    }

    public static BytesRange Range(BytePosition start, BytePosition end)
    {
      return new BytesRange(start, end);
    }

    private BytesRange(BytePosition start, BytePosition end)
    {
      Start = start;
      End = end;
    }

    public BytePosition Start { get; private set; }
    public BytePosition End { get; private set; }
    public long RangeByteCount
    {
      get { return End.ByteIndex - Start.ByteIndex + 1; }
    }

    public override string ToString()
    {
      return string.Format("[{0} - {1}]", Start, End);
    }
  }

  public interface IDescription
  {
    BytesRange BytesRange { get; }
    List<IDescription> SubDescriptions();
    string Describe();
  }

  public abstract class BaseDescription : IDescription
  {
    protected BaseDescription(BytesRange bytesRange, string descriptionStr)
    {
      BytesRange = bytesRange;
      DescriptionString = descriptionStr;
    }

    protected string DescriptionString { get; set; }

    public BytesRange BytesRange { get; private set; }

    public virtual List<IDescription> SubDescriptions()
    {
      return new List<IDescription>(0);
    }

    public virtual string Describe()
    {
      return DescriptionString;
    }

  }

  public abstract class BaseSingleValueDescription : IDescription
  {
    
    protected BaseSingleValueDescription(BytesRange bytesRange,object value)
    {
      BytesRange = bytesRange;
      Value = value;
    }

    protected string DescriptionString { get; set; }

    public object Value { get; protected set; }

    public BytesRange BytesRange { get; private set; }

    public virtual List<IDescription> SubDescriptions()
    {
      return new List<IDescription>(0);      
    }

    public abstract string Describe();

  }

  public abstract class BaseCompositeDescription:IDescription
  {
    public BytesRange BytesRange
    {
      get
      {
        List<IDescription> subDescriptions = SubDescriptions();
        if (subDescriptions.Count > 0)
        {
          return BytesRange.Range(subDescriptions.First().BytesRange.Start.ByteIndex,
                                  subDescriptions.Last().BytesRange.End.ByteIndex);
        }
        return null;
      }
    }
    public abstract List<IDescription> SubDescriptions();
    public abstract string Describe();
  }

  
}
