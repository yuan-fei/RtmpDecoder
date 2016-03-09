using System.Collections.Generic;
using System.Linq;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public class PacketDescription
  {
    public PacketDescription()
    {
      ChunkRanges=new PacketRange();
    }
    public PacketRange ChunkRanges { get; private set; }
    public ChunkViewDescription ChunkView { get; set; }
    public MessageViewDescription MessageView { get; set; }
  }

  public class PacketRange
  {
    public PacketRange()
    {
      mRanges=new List<BytesRange>();
    }
    private List<BytesRange> mRanges;
    public List<BytesRange> Ranges
    {
      get { return mRanges; }
    }

    public void Add(BytesRange range)
    {
      if(mRanges.Count>0)
      {
        BytesRange rangeLow = mRanges.Last();
        if(rangeLow.End.ByteIndex==range.Start.ByteIndex-1)
        {
          mRanges.RemoveAt(mRanges.Count-1);
          mRanges.Add(BytesRange.Range(rangeLow.Start, range.End));
        }
      }
      else
      {
        mRanges.Add(range);
      }
    }
  }

  public class MessageViewDescription:IDescription
  {
    public MessageViewDescription(IDescription messageHeader, IDescription messageContent, byte[] bytes)
    {
      MessageHeader = messageHeader;
      MessageContent = messageContent;
      Bytes = bytes;
    }

    public byte[] Bytes { get; set; }
    public IDescription MessageHeader { get; set; }
    public IDescription MessageContent { get; set; }

    public BytesRange BytesRange
    {
      get
      {
        List<IDescription> subDescriptions = SubDescriptions();
        return BytesRange.Range(subDescriptions.First().BytesRange.Start.ByteIndex, subDescriptions.Last().BytesRange.End.ByteIndex);
      }
    }

    public List<IDescription> SubDescriptions()
    {
      List<IDescription> subDescriptions = new List<IDescription>() {MessageHeader, MessageContent};
      subDescriptions.RemoveAll(d => d == null);
      return subDescriptions;
    }

    public string Describe()
    {
      return DescriptionStrings.MESSAGE_VIEW_DESCRIPTION;
    }
  }

  public class ChunkViewDescription:IDescription
  {
    public ChunkViewDescription()
    {
      Chunks=new List<RtmpChunkDescription>();
    }

    public void AddRtmpChunkDescription(RtmpChunkDescription chunkDescription)
    {
      Chunks.Add(chunkDescription);
    }

    public byte[] Bytes { get; set; }
    public List<RtmpChunkDescription> Chunks { get; private set; }


    #region IDescription Members

    public BytesRange BytesRange
    {
      get { return Description.BytesRange.Range(Chunks.First().BytesRange.Start,Chunks.Last().BytesRange.End); }
    }

    public List<IDescription> SubDescriptions()
    {
      return Chunks.ConvertAll(c=>(IDescription)c);
    }

    public string Describe()
    {
      return DescriptionStrings.CHUNCK_VIEW_DESCRIPTION;
    }

    #endregion
  }

  
}