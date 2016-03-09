using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace RtmpDecoder
{
  public interface IBytesLoader
  {
    byte[] Load();
  }

  public class TextFileLoader : IBytesLoader
  {
    private readonly string mFilePath;
    public TextFileLoader(string filePath)
    {
      mFilePath = filePath;
    }

    public byte[] Load()
    {
      byte[] bytes=new byte[0];
      if (File.Exists(mFilePath))
      {
        string content=File.ReadAllText(mFilePath);
        string[] hexNums = content.Split((char[])null,StringSplitOptions.RemoveEmptyEntries);
        bytes=new byte[hexNums.Length];
        for (int i = 0; i < hexNums.Length; i++)
        {
          bytes[i] = ConvertHexStringToByte(hexNums[i]);
        }
      }
      return bytes;
    }

    private static byte ConvertHexStringToByte(string hexStr)
    {
      return Byte.Parse(hexStr,NumberStyles.AllowHexSpecifier);
    }
  }

  public class TextLoader : IBytesLoader
  {
    private readonly string mText;
    public TextLoader(string text)
    {
      mText = text;
    }

    public byte[] Load()
    {
      byte[] bytes = new byte[0];

      string[] hexNums = mText.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
      bytes = new byte[hexNums.Length];
      for (int i = 0; i < hexNums.Length; i++)
      {
        bytes[i] = ConvertHexStringToByte(hexNums[i]);
      }
      return bytes;
    }

    private static byte ConvertHexStringToByte(string hexStr)
    {
      return Byte.Parse(hexStr, NumberStyles.AllowHexSpecifier);
    }
  }


  public class BinFileLoader: IBytesLoader
  {
    private readonly string mFilePath;
    public BinFileLoader(string filePath)
    {
      mFilePath = filePath;
    }

    public byte[] Load()
    {
      if(File.Exists(mFilePath))
      {
        return File.ReadAllBytes(mFilePath);
      }
      return new byte[0];
    }
  }
}
