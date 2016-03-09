using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpDecoder
{
  public class ApplicationSettings
  {
    private const int DefaultChunkSize = 128;

    private static ApplicationSettings sInstance=new ApplicationSettings();
    private ApplicationSettings()
    {
      
    }

    public static ApplicationSettings Instance
    {
      get
      {
        return sInstance;
      }
    }

    private int mDecodingChunkSize = DefaultChunkSize;
    public int DecodingChunkSize
    {
      get { return mDecodingChunkSize; }
      set { mDecodingChunkSize = value; }
    }
  }
}
