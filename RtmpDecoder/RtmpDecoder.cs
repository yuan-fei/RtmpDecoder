using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluorineFx.Messaging.Rtmp;
using FluorineFx.Util;

namespace RtmpDecoder
{
  public class RtmpDecoder
  {

    public List<object> DecodePackets(byte[] bytes)
    {
      RtmpContext context = new RtmpContext(RtmpMode.Client);
      context.SetReadChunkSize(ApplicationSettings.Instance.DecodingChunkSize);
      context.State = RtmpState.Connected;
      MemoryStream memStream = new MemoryStream(bytes,0,bytes.Length,true,true);
      ByteBuffer buffer = new ByteBuffer(memStream);
      List<object> packets = RtmpProtocolDecoder.DecodeBuffer(context, buffer);
      return packets;
    }

    public List<object> DecodeHandShake(byte[] bytes)
    {
      RtmpContext context = new RtmpContext(RtmpMode.Client);
      context.SetReadChunkSize(ApplicationSettings.Instance.DecodingChunkSize);
      context.State = RtmpState.Handshake;
      MemoryStream memStream = new MemoryStream(bytes, 0, bytes.Length, true, true);
      ByteBuffer buffer = new ByteBuffer(memStream);
      List<object> packets = RtmpProtocolDecoder.DecodeBuffer(context, buffer);
      return packets;
    }

    public List<object> DecodeConnect(byte[] bytes)
    {
      RtmpContext context = new RtmpContext(RtmpMode.Client);
      context.SetReadChunkSize(ApplicationSettings.Instance.DecodingChunkSize);
      context.State = RtmpState.Connect;
      MemoryStream memStream = new MemoryStream(bytes, 0, bytes.Length, true, true);
      ByteBuffer buffer = new ByteBuffer(memStream);
      List<object> packets = RtmpProtocolDecoder.DecodeBuffer(context, buffer);
      return packets;
    }
  }
}
