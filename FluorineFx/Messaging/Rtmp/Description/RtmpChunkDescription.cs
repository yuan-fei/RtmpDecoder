using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public class RtmpChunkDescription : BaseCompositeDescription
  {
    public RtmpChunkDescription(IDescription header, IDescription chunkData)
    {
      Header = header;
      ChunkData = chunkData;
    }
    public IDescription Header { get; set; }
    public IDescription ChunkData { get; set; }

    #region IDescription Members

    public override List<IDescription> SubDescriptions()
    {
      List<IDescription> subDescriptions = new List<IDescription>() { Header, ChunkData };
      subDescriptions.RemoveAll(d => d == null);
      return subDescriptions;
    }

    public override string Describe()
    {
      return DescriptionStrings.RTMP_CHUNK;
    }

    #endregion
  }

  public class RtmpChunkData:BaseDescription
  {
    public RtmpChunkData(BytesRange bytesRange) : base(bytesRange, DescriptionStrings.CHUNCK_DATA) { }
  }
}
