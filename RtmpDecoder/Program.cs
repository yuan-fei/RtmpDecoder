using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using FluorineFx.Messaging.Rtmp;
using FluorineFx.Messaging.Rtmp.Description;

namespace RtmpDecoder
{
  class Program
  {
    static void Main(string[] args)
    {
      //CodeGenerator();
      MainTest();
      //TestTab();
      
      Console.ReadLine();
    }

    private static void MainTest()
    {
      string[] b = "a  b".Split();

      TextFileLoader textFileLoader = new TextFileLoader("./data/bytesLong.txt");
      byte[] bytes = textFileLoader.Load();
      RtmpDecoder decoder = new RtmpDecoder();
      List<object> packets = decoder.DecodePackets(bytes);
      foreach (object packet in packets)
      {
        RtmpPacket rtmpPacket = packet as RtmpPacket;
        if (rtmpPacket != null)
        {
          PacketDescription mInfo = rtmpPacket.PacketDescription;
          DescriptionPrinter printer = new DescriptionPrinter(Console.Out);
          Console.WriteLine("Chunks: ");
          mInfo.ChunkView.Chunks.ForEach(printer.Print);
          Console.WriteLine();
          Console.WriteLine("Message: ");
          printer.Print(mInfo.MessageView);
        }
      }
      packets.ForEach(Console.WriteLine);
    }

    public static void CodeGenerator()
    {
      string[] variable = {"SERVER_CONNECT","SERVER_DISCONNECT","SERVER_SET_ATTRIBUTE","CLIENT_UPDATE_DATA","CLIENT_UPDATE_ATTRIBUTE","SERVER_SEND_MESSAGE","CLIENT_STATUS","CLIENT_CLEAR_DATA","CLIENT_DELETE_DATA","SERVER_DELETE_ATTRIBUTE","CLIENT_INITIAL_DATA","CLIENT_SEND_MESSAGE","CLIENT_DELETE_ATTRIBUTE"};
      StreamWriter fs=new StreamWriter("a.txt");
      
      foreach (var s in variable)
      {
        fs.WriteLine(string.Format("case SharedObjectEventType.{0}:",s));
        fs.WriteLine(string.Format("return new SharedObjectActionTypeDescription(bytesRange,DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_{0});",s));
      }
      fs.Close();
    }
    public static void TestTab()
    {
      Console.WriteLine("00000000\t000\tC.........IG");
    }
  }
}
