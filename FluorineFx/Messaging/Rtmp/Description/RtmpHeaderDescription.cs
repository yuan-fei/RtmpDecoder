using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public class RtmpChunkHeaderDescription : BaseCompositeDescription
  {
    public RtmpChunkHeaderDescription(ChunkBasicHeaderDescription chunkBasicHeaderDescription, ChunkMessageHeaderDescription chunkMessageHeaderDescription)
    {
      ChunkBasicHeaderDescription = chunkBasicHeaderDescription;
      ChunkMessageHeaderDescription = chunkMessageHeaderDescription;
    }
    public ChunkBasicHeaderDescription ChunkBasicHeaderDescription { get; private set; }
    public ChunkMessageHeaderDescription ChunkMessageHeaderDescription { get; private set; }

    public override List<IDescription> SubDescriptions()
    {
      List<IDescription> subDescriptions=new List<IDescription>(){ChunkBasicHeaderDescription,ChunkMessageHeaderDescription};
      subDescriptions.RemoveAll(d => d == null);
      return subDescriptions;
    }

    public override string Describe()
    {
      return DescriptionStrings.RTMP_HEADER;
    }
  }

  public class ChunkBasicHeaderDescription:BaseCompositeDescription
  {
    public ChunkBasicHeaderDescription(IDescription headerType, IDescription channelIdType, IDescription channelId)
    {
      HeaderType = headerType;
      ChannelIdType = channelIdType;
      ChannelId = channelId;
      
    }

    public IDescription HeaderType { get; private set; }
    public IDescription ChannelIdType { get; private set; }
    public IDescription ChannelId { get; private set; }

    public override List<IDescription> SubDescriptions()
    {
      List<IDescription> descriptions=new List<IDescription>{ChannelIdType,HeaderType,ChannelId};
      descriptions.Sort((a,b)=>a.BytesRange.Start.CompareTo(b.BytesRange.Start));
      return descriptions;
    }

    public override string Describe()
    {
      return DescriptionStrings.CHUNCK_BASIC_HEADER;
    }

  }

  public class HeaderTypeDescription:BaseDescription
  {

    public static HeaderTypeDescription GetInstanceFromValue(BytesRange bytesRange,int value)
    {
      switch (value)
      {
        case 0:
          return new HeaderTypeDescription(bytesRange,DescriptionStrings.HEADER_TYPE_0);
        case 1:
          return new HeaderTypeDescription(bytesRange,DescriptionStrings.HEADER_TYPE_1);
        case 2:
          return new HeaderTypeDescription(bytesRange,DescriptionStrings.HEADER_TYPE_2);
        case 3:
          return new HeaderTypeDescription(bytesRange,DescriptionStrings.HEADER_TYPE_3);
        default:
          return null;
      }
    }

    private HeaderTypeDescription(BytesRange bytesRange,string description) : base(bytesRange, description){}
  }

  public class ChannelIdTypeDescription:BaseDescription
  {
    public static ChannelIdTypeDescription GetInstanceFromValue(BytesRange bytesRange, int value)
    {
      switch (value)
      {
        case 0:
          return new ChannelIdTypeDescription(bytesRange,DescriptionStrings.CHANNEL_ID_TYPE_LARGE);
        case 1:
          return new ChannelIdTypeDescription(bytesRange,DescriptionStrings.CHANNEL_ID_TYPE_MIDDLE);
        default:
          return new ChannelIdTypeDescription(bytesRange,string.Format(DescriptionStrings.CHANNEL_ID_TYPE_SMALL,value));
      }
    }

    private ChannelIdTypeDescription(BytesRange bytesRange, string description)
      : base(bytesRange,description){}
  }

  public class ChannelIdDescription : BaseSingleValueDescription
  {
    public ChannelIdDescription(BytesRange bytesRange, int channelId)
      : base(bytesRange,channelId){}

    public override string Describe()
    {
      return string.Format(DescriptionStrings.CHANNEL_ID, Value);
    }
  }

  public class ChunkMessageHeaderDescription:BaseCompositeDescription
  {
    public IDescription Timer { get; private set; }
    public IDescription MessageSize { get; private set; }
    public IDescription MessageDataType { get; private set; }
    public IDescription StreamId { get; private set; }
    public IDescription TimerExtended { get; private set; }

    private readonly int mChunkMessageHeaderType;
    public static ChunkMessageHeaderDescription OfType0(IDescription timer, IDescription messageSize, IDescription messageDataType, IDescription streamId, IDescription timerExtended)
    {
      ChunkMessageHeaderDescription instance = new ChunkMessageHeaderDescription(0)
                                                  {
                                                    Timer = timer,
                                                    MessageSize = messageSize,
                                                    MessageDataType = messageDataType,
                                                    StreamId = streamId,
                                                    TimerExtended = timerExtended
                                                  };
      return instance;
    }

    public static ChunkMessageHeaderDescription OfType1(IDescription timer, IDescription messageSize, IDescription messageDataType, IDescription timerExtended)
    {
      ChunkMessageHeaderDescription instance = new ChunkMessageHeaderDescription(1)
                                                  {
                                                    Timer = timer,
                                                    MessageSize = messageSize,
                                                    MessageDataType = messageDataType,
                                                    TimerExtended = timerExtended
                                                  };
      return instance;
    }

    public static ChunkMessageHeaderDescription OfType2(IDescription timer, IDescription timerExtended)
    {
      ChunkMessageHeaderDescription instance = new ChunkMessageHeaderDescription(2)
                                                  {Timer = timer, TimerExtended = timerExtended};
      return instance;
    }

    public static ChunkMessageHeaderDescription OfType3()
    {
      return null;
    }

    private ChunkMessageHeaderDescription(int type)
    {
      mChunkMessageHeaderType = type;
    }

    public override List<IDescription> SubDescriptions()
    {
      List<IDescription> descriptions=new List<IDescription> { Timer, MessageSize, MessageDataType, StreamId, TimerExtended };
      descriptions.RemoveAll(d => d == null);
      return descriptions;
    }

    public override string Describe()
    {
      return String.Format(DescriptionStrings.CHUNCK_MESSAGE_HEADER,mChunkMessageHeaderType);
    }
    
  }

  public class TimeStampDescription : BaseSingleValueDescription
  {
    public TimeStampDescription(BytesRange bytesRange, object value) : base(bytesRange, value){}

    public override string Describe()
    {
      return ((int)Value==0xFFFF)?DescriptionStrings.TIMESTAMP_MAX:String.Format(DescriptionStrings.TIMESTAMP, Value);
    }
  }

  public class MessageSizeDescription: BaseSingleValueDescription
  {
    public MessageSizeDescription(BytesRange bytesRange, object value) : base(bytesRange, value){}

    public override string Describe()
    {
      return String.Format(DescriptionStrings.MESSAGE_SIZE, Value);
    }
  }

  public class MessageDataTypeDescription:BaseDescription
  {
    public static MessageDataTypeDescription GetInstanceFromValue(BytesRange bytesRange, int value)
    {
      switch (value)
      {
        case MessageDataType.TypeUnknown:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeUnknown);
        case MessageDataType.TypeChunkSize:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeChunkSize);
        case MessageDataType.TypeBytesRead:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeBytesRead);
        case MessageDataType.TypePing:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypePing);
        case MessageDataType.TypeServerBandwidth:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeServerBandwidth);
        case MessageDataType.TypeClientBandwidth:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeClientBandwidth);
        case MessageDataType.TypeAudioData:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeAudioData);
        case MessageDataType.TypeVideoData:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeVideoData);
        case MessageDataType.TypeFlexStreamEnd:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeFlexStreamEnd);
        case MessageDataType.TypeFlexSharedObject:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeFlexSharedObject);
        case MessageDataType.TypeFlexInvoke:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeFlexInvoke);
        case MessageDataType.TypeNotify:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeNotify);
        case MessageDataType.TypeSharedObject:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeSharedObject);
        case MessageDataType.TypeInvoke:
          return new MessageDataTypeDescription(bytesRange, DescriptionStrings.MESSAGE_DATA_TYPE_TypeInvoke);
        default:
          return null;
      }
    }
    private MessageDataTypeDescription(BytesRange bytesRange, string descriptionStr) : base(bytesRange, descriptionStr){}
  }

  public class StreamIdDescription:BaseSingleValueDescription
  {
    public StreamIdDescription(BytesRange bytesRange, object value) : base(bytesRange, value){}

    public override string Describe()
    {
      return String.Format(DescriptionStrings.STREAM_ID, Value);
    }
  }

  public class TimeStampExtendedDescription : BaseSingleValueDescription
  {
    public TimeStampExtendedDescription(BytesRange bytesRange, object value) : base(bytesRange, value){}

    public override string Describe()
    {
      return string.Format(DescriptionStrings.TIMESTAMP_EXTENDED, Value);
    }
  }
}
