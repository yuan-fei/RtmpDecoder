using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluorineFx.Messaging.Rtmp;

namespace RtmpDecoderUI
{
  public class DataContext
  {
    private static DataContext sInstance = new DataContext();
    public static DataContext Instance { get { return sInstance; } }
    private DataContext() {}

    private byte[] mLoadedBytes;
    public event Action BeforeLoadedBytesChanged;
    public event Action<object> AfterLoadedBytesChanged;
    public byte[] LoadedBytes {
      get { return mLoadedBytes; }
      set
      {
        if (value != mLoadedBytes)
        {
          if (BeforeLoadedBytesChanged!=null)
          {
            BeforeLoadedBytesChanged();
          }
          CurrentPacketIndex = -1;
          object oldValue = mLoadedBytes;
          mLoadedBytes = value;
          DecodeBytes();
          if (AfterLoadedBytesChanged != null)
          {
            AfterLoadedBytesChanged(oldValue);
          }
        }
      }
    }

    private List<object> Packets{get;set;}

    public List<List<int[]>> PacketsRange 
    {
      get
      {
        if (Packets != null&&Packets.Count>0)
        {
          List<List<int[]>> allRanges = new List<List<int[]>>();
          foreach (object t in Packets)
          {
            List<int[]> packetRanges=new List<int[]>();
            RtmpPacket rtmpPacket = (RtmpPacket)t;
            rtmpPacket.PacketDescription.ChunkRanges.Ranges.ForEach(r=>packetRanges.Add(new int[]{(int)r.Start.ByteIndex, (int)r.End.ByteIndex}));
            allRanges.Add(packetRanges);
          }
          return allRanges;
        }
        return null;
      }
    }

    private int mCurrentPacketIndex = -1;
    public int CurrentPacketIndex
    {
      get { return mCurrentPacketIndex; }
      set
      { 
        mCurrentPacketIndex = value;
        if (CurrentPacketChanged != null)
        {
          CurrentPacketChanged();
        }
      }
    }

    public event Action CurrentPacketChanged;
    public object CurrentPacket
    {
      get
      {
        if (CurrentPacketIndex == -1)
        {
          return null;
        }
        return Packets[CurrentPacketIndex]; 
      }
    }

    private void DecodeBytes()
    {
      RtmpDecoder.RtmpDecoder decoder = new RtmpDecoder.RtmpDecoder();
      Packets = decoder.DecodePackets(LoadedBytes);
    }


  }
}
