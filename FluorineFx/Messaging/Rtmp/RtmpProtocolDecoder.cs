/*
	FluorineFx open source library 
	Copyright (C) 2007 Zoltan Csibi, zoltan@TheSilentGroup.com, FluorineFx.com 
	
	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.
	
	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	Lesser General Public License for more details.
	
	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/
using System;
using System.Collections;
using System.IO;
#if !(NET_1_1)
using System.Collections.Generic;
#endif
#if !SILVERLIGHT
using FluorineFx.Messaging.Rtmp.Description;
using log4net;
#endif
using FluorineFx.Exceptions;
using FluorineFx.Messaging.Rtmp.Event;
using FluorineFx.Messaging.Rtmp.SO;
using FluorineFx.Messaging.Rtmp.Service;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.Util;
using FluorineFx.Configuration;
using FluorineFx.Context;

namespace FluorineFx.Messaging.Rtmp
{
	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
    [CLSCompliant(false)]
	public sealed class RtmpProtocolDecoder
	{
#if !SILVERLIGHT
        private static readonly ILog log = LogManager.GetLogger(typeof(RtmpProtocolDecoder));
#endif
        /// <summary>
        /// RTMP handshake size.
        /// </summary>
		public const int HandshakeSize = 1536;

		static RtmpProtocolDecoder()
		{
		}
        /// <summary>
        /// Initializes a new instance of the RtmpProtocolDecoder class.
        /// </summary>
		public RtmpProtocolDecoder()
		{
		}
        /// <summary>
        /// Decode channel id.
        /// </summary>
        /// <param name="headerByte">Header byte.</param>
        /// <param name="byteCount">Byte count.</param>
        /// <returns>Channel id.</returns>
		public static byte DecodeChannelId(int headerByte, int byteCount) 
		{
			if (byteCount == 1) 
				return (byte)(headerByte & 0x3f);
			else if (byteCount == 2) 
				return (byte)(64 + (headerByte & 0xff));
			else
				return (byte)(64 + ((headerByte >> 8) & 0xff) + ((headerByte & 0xff) << 8));
		}
        /// <summary>
        /// Decode header size.
        /// </summary>
        /// <param name="headerByte">Header byte.</param>
        /// <param name="byteCount">Byte count.</param>
        /// <returns>Header size.</returns>
		public static byte DecodeHeaderSize(int headerByte, int byteCount) 
		{
			if (byteCount == 1) 
				return (byte)(headerByte >> 6);
			else if (byteCount == 2) 
				return (byte)(headerByte >> 14);
			else 
				return (byte)(headerByte >> 22);
		}
        /// <summary>
        /// Returns the header length from marker value.
        /// </summary>
        /// <param name="headerSize">Header size marker value.</param>
        /// <returns>The header length.</returns>
		public static int GetHeaderLength(byte headerSize) 
		{
			switch((HeaderType)headerSize) 
			{
				case HeaderType.HeaderNew:
					return 12;
				case HeaderType.HeaderSameSource:
					return 8;
				case HeaderType.HeaderTimerChange:
					return 4;
				case HeaderType.HeaderContinue:
					return 1;
				default:
					return -1;
			}
		}

#if !(NET_1_1)
        /// <summary>
        /// Decodes all available objects in the buffer.
        /// </summary>
        /// <param name="context">State for the protocol.</param>
        /// <param name="stream">Buffer of data to be decoded.</param>
        /// <returns>A list of decoded objects, may be empty if nothing could be decoded.</returns>
		public static List<object> DecodeBuffer(RtmpContext context, ByteBuffer stream)
#else
        public static ArrayList DecodeBuffer(RtmpContext context, ByteBuffer stream)
#endif
		{
			// >> HEADER[1] + CLIENT_HANDSHAKE[1536] 
			// << HEADER[1] + SERVER_HANDSHAKE[1536] + CLIENT_HANDSHAKE[1536];
			// >> SERVER_HANDSHAKE[1536] + AMF[n]
#if !(NET_1_1)
            List<object> result = null;
#else
			ArrayList result = null;
#endif
            try
            {
                while (true)
                {
                    long remaining = stream.Remaining;
                    if (context.CanStartDecoding(remaining))
                        context.StartDecoding();
                    else
                        break;

                    if (context.State == RtmpState.Disconnected)
                        break;

                    object decodedObject = Decode(context, stream);
                    if (context.HasDecodedObject)
                    {
#if !(NET_1_1)
                        if (result == null)
                            result = new List<object>();
#else
                        if (result == null)
                            result = new ArrayList();
#endif
                        result.Add(decodedObject);

                    }
                    else if (context.CanContinueDecoding)
                        continue;
                    else
                        break;

                    if (!stream.HasRemaining)
                        break;
                }
            }
            catch (HandshakeFailedException)
            {
                throw;
            }
            catch (Exception)
            {
                Dump(stream);
                throw;
            }
            finally
			{
			    //stream.Compact();
			}
			return result;
		}

        private static void Dump(ByteBuffer buffer)
        {
#if !SILVERLIGHT
            if (FluorineConfiguration.Instance.FluorineSettings.Debug != null && FluorineContext.Current != null)
            {
                if (FluorineConfiguration.Instance.FluorineSettings.Debug.Mode != Debug.Off)
                {
                    try
                    {
                        if (FluorineConfiguration.Instance.FluorineSettings.Debug.DumpPath != null)
                        {
                            string fileName = string.Format("dump_{0}_{1}.bin", DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff"), buffer.Position);
                            if( FluorineContext.Current != null && FluorineContext.Current.Connection != null )
                                fileName = string.Format("dump_{0}_{1}_{2}.bin", FluorineContext.Current.Connection.ConnectionId, DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff"), buffer.Position);
                            //string fileName = "dump_" + Guid.NewGuid().ToString("N") + "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff") + ".bin";
                            IResource resource = FluorineContext.Current.GetResource(Path.Combine(FluorineConfiguration.Instance.FluorineSettings.Debug.DumpPath, fileName));
                            buffer.Dump(resource.File.FullName);
                        }
                    }
                    catch
                    {
                    }
                }
            }
#endif
        }

		private static object Decode(RtmpContext context, ByteBuffer stream)
		{
			//long start = stream.Position;
			switch(context.State)
			{					
				case RtmpState.Connected:
					return DecodePacket(context, stream);
				case RtmpState.Error:
					// Attempt to correct error 
					return null;
				case RtmpState.Connect:
				case RtmpState.Handshake:
					return DecodeHandshake(context, stream);
				default:
					return null;
			}
		}
        /// <summary>
        /// Decodes handshake message.
        /// </summary>
        /// <param name="context">RTMP protocol state.</param>
        /// <param name="stream">Buffer to be decoded.</param>
        /// <returns>Buffer with handshake response.</returns>
		public static object DecodeHandshake(RtmpContext context, ByteBuffer stream) 
		{
			long remaining = stream.Remaining;
			if(context.Mode == RtmpMode.Server)
			{
				if(context.State == RtmpState.Connect)
				{
					if(remaining < HandshakeSize + 1) 
					{
#if !SILVERLIGHT
                        if( log.IsDebugEnabled )
                            log.Debug(__Res.GetString(__Res.Rtmp_HSInitBuffering, remaining));
#endif
						context.SetBufferDecoding(HandshakeSize + 1);
						return null;
					}
					else 
					{
#if !SILVERLIGHT
                        if (log.IsDebugEnabled)
                            log.Debug("Handshake 1st phase");
#endif
                        stream.Get();// skip the header byte
                        byte[] handshake = RtmpHandshake.GetHandshakeResponse(stream);
                        context.SetHandshake(handshake);
                        context.State = RtmpState.Handshake;
                        return handshake;
                    }
				}
				if(context.State == RtmpState.Handshake)
				{
                    //if (log.IsDebugEnabled)
                    //    log.Debug("Handshake reply");

					if(remaining < HandshakeSize)
					{
#if !SILVERLIGHT
                        if( log.IsDebugEnabled )
							log.Debug(__Res.GetString(__Res.Rtmp_HSReplyBuffering, remaining));
#endif
						context.SetBufferDecoding(HandshakeSize);
						return null;
					}				 
					else 
					{
					    // Skip first 8 bytes when comparing the handshake, they seem to be changed when connecting from a Mac client.
                        if (!context.ValidateHandshakeReply(stream, 8, HandshakeSize - 8))
                        {
#if !SILVERLIGHT
                            if (log.IsDebugEnabled)
                                log.Debug("Handshake reply validation failed, disconnecting client.");
#endif
                            stream.Skip(HandshakeSize);
                            context.State = RtmpState.Error;
                            throw new HandshakeFailedException("Handshake validation failed");
                        }
						stream.Skip(HandshakeSize);
						context.State = RtmpState.Connected;
						context.ContinueDecoding();
						return null;
					}
				}
			}
			else
			{
				//Client mode
				if(context.State == RtmpState.Connect)
				{
					int size = (2 * HandshakeSize) + 1;
					if(remaining < size) 
					{
#if !SILVERLIGHT
                        if( log.IsDebugEnabled )
							log.Debug(__Res.GetString(__Res.Rtmp_HSInitBuffering, remaining));
#endif
						context.SetBufferDecoding(size);
						return null;
					}
					else
					{
						ByteBuffer hs = ByteBuffer.Allocate(size);
						ByteBuffer.Put(hs, stream, size);
						hs.Flip();
						context.State = RtmpState.Handshake;
						return hs;
					}
				}
			}
			return null;
		}
        /// <summary>
        /// Decodes a RTMP packet.
        /// </summary>
        /// <param name="context">RTMP protocol state.</param>
        /// <param name="stream">Buffer to be decoded.</param>
        /// <returns>The decoded RTMP packet.</returns>
		public static RtmpPacket DecodePacket(RtmpContext context, ByteBuffer stream)
		{
			int remaining = stream.Remaining;
			// We need at least one byte
			if(remaining < 1) 
            {
#if !SILVERLIGHT
                if( log.IsDebugEnabled )
                    log.Debug(__Res.GetString(__Res.Rtmp_DataBuffering, remaining, 1));
#endif
				context.SetBufferDecoding(1);
				return null;
			}

			int position = (int)stream.Position;
			byte headerByte = stream.Get();
			int headerValue;
			int byteCount;
			if((headerByte & 0x3f) == 0) 
			{
				// Two byte header
				if (remaining < 2) 
				{
					stream.Position = position;
#if !SILVERLIGHT
                    if (log.IsDebugEnabled)
                        log.Debug(__Res.GetString(__Res.Rtmp_DataBuffering, remaining, 2));
#endif
					context.SetBufferDecoding(2);
					return null;
				}
				headerValue = ((int) headerByte & 0xff) << 8 | ((int) stream.Get() & 0xff); 
				byteCount = 2;
			} else if ((headerByte & 0x3f) == 1) {
				// Three byte header
				if (remaining < 3)
				{
					stream.Position = position;
#if !SILVERLIGHT
                    if (log.IsDebugEnabled)
                        log.Debug(__Res.GetString(__Res.Rtmp_DataBuffering, remaining, 3));
#endif
                    context.SetBufferDecoding(3);
					return null;
				}
				headerValue = ((int) headerByte & 0xff) << 16 | ((int) stream.Get() & 0xff) << 8 | ((int) stream.Get() & 0xff); 
				byteCount = 3;
			} else {
				// Single byte header
				headerValue = (int) headerByte & 0xff;
				byteCount = 1;
			}
			byte channelId = DecodeChannelId(headerValue, byteCount);
			if (channelId < 0) 
				throw new ProtocolException("Bad channel id: " + channelId);
			byte headerSize = DecodeHeaderSize(headerValue, byteCount);
			int headerLength = GetHeaderLength(headerSize);
			headerLength += byteCount - 1;

			//if(headerLength > remaining) 
            if (headerLength + byteCount - 1 > remaining)
            {
#if !SILVERLIGHT
                if (log.IsDebugEnabled)
                    log.Debug(__Res.GetString(__Res.Rtmp_HeaderBuffering, remaining));
#endif
                stream.Position = position;
                //context.SetBufferDecoding(headerLength);
                context.SetBufferDecoding(headerLength + byteCount - 1);
                return null;
            }
			// Move the position back to the start
			stream.Position = position;

      RtmpPacket packet = context.GetLastReadPacket(channelId);

      //For later offset recording
      int headerPos = position;

      int headerDescriptionStartPos=0;
      if(packet!=null)
      {
        headerDescriptionStartPos = (int)packet.ChuncksData.Position;
      }
      RtmpHeader header = DecodeHeader(context, context.GetLastReadHeader(channelId), stream, headerDescriptionStartPos);
#if !SILVERLIGHT
            log.Debug("Decoded " + header);
#endif

			if (header == null) 
				throw new ProtocolException("Header is null, check for error");

			// Save the header
			context.SetLastReadHeader(channelId, header);

      int currentHeaderByteCount = (int)stream.Position - position;
			// Check to see if this is a new packet or continue decoding an existing one.
			if(packet == null) 
			{
				packet = new RtmpPacket(header);
        packet.PacketDescription=new PacketDescription();
        stream.Position = position;
        ByteBuffer.Put(packet.MessageData, stream, currentHeaderByteCount);
				context.SetLastReadPacket(channelId, packet);
			}
      //Add chunck header
      stream.Position = position;
      ByteBuffer.Put(packet.ChuncksData, stream, currentHeaderByteCount);
      
			ByteBuffer buf = packet.MessageData;
      //int addSize = (header.Timer == 0xffffff ? 4 : 0);
      int addSize = (int)packet.Header.RtmpHeaderDescription.BytesRange.RangeByteCount;
            //int addSize = 0;
			int readRemaining = header.Size + addSize - (int)buf.Position;
			int chunkSize = context.GetReadChunkSize();
			int readAmount = (readRemaining > chunkSize) ? chunkSize : readRemaining;
			if(stream.Remaining < readAmount) 
			{
#if !SILVERLIGHT
                if( log.IsDebugEnabled )
					log.Debug(__Res.GetString(__Res.Rtmp_ChunkSmall, stream.Remaining, readAmount));
#endif
				//Skip the position back to the start
				stream.Position = position;
				context.SetBufferDecoding(headerLength + readAmount);
				
				//string path = FluorineFx.Context.FluorineContext.Current.GetFullPath(@"log\chunk.bin");
				//stream.Dump(path);
				return null;
			}

      long curPos = packet.ChuncksData.Position;
          RtmpChunkData chunkData = null;
      if (readAmount > 0)
      {
        chunkData = new RtmpChunkData(BytesRange.Range(curPos, curPos + readAmount - 1));
      }
          if(packet.PacketDescription.ChunkView == null)
      {
        packet.PacketDescription.ChunkView=new ChunkViewDescription();
      }
      packet.PacketDescription.ChunkView.AddRtmpChunkDescription(new RtmpChunkDescription(header.RtmpHeaderDescription, chunkData));
      
      position = (int)stream.Position;
      ByteBuffer.Put(packet.ChuncksData, stream, readAmount);
      
      packet.PacketDescription.ChunkRanges.Add(BytesRange.Range(headerPos, stream.Position-1));
      stream.Position = position;

			//http://osflash.org/pipermail/free_osflash.org/2005-September/000261.html
			//http://www.acmewebworks.com/Downloads/openCS/091305-initialMeeting.txt
			ByteBuffer.Put(buf, stream, readAmount);
      
			if(buf.Position < header.Size + addSize) 
			{
				context.ContinueDecoding();
				return null;
			}
            if (buf.Position > header.Size + addSize)
            {
#if !SILVERLIGHT
                log.Warn(string.Format("Packet size expanded from {0} to {1} ({2})", header.Size + addSize, buf.Position, header));
#endif
            }


            try
            {
                position = (int)stream.Position;
                packet.ChuncksData.Flip();
                packet.PacketDescription.ChunkView.Bytes = packet.ChuncksData.ToArray();


                ByteBuffer messageBuffer = buf;
                messageBuffer.Flip();
                byte[] messageBytes=packet.MessageData.ToArray();

                RtmpContext newContext=new RtmpContext(RtmpMode.Client);
                newContext.SetReadChunkSize(int.MaxValue);
                stream.Position = position;

                RtmpHeader newHeader = DecodeHeader(newContext, null, messageBuffer, 0);
                position = (int)messageBuffer.Position;

                packet.Header.RtmpHeaderDescription = newHeader.RtmpHeaderDescription;
                IRtmpEvent message = DecodeMessage(packet.Header, messageBuffer);
                packet.Message = message;
                IDescription messageDescription;
              if (position < messageBuffer.Limit)
              {
                messageDescription = message.RtmpMessageDescription ??
                                     new UnImplementedMessageDescription(new UnImplementedMessageValueDescription(BytesRange.Range(position, messageBuffer.Limit - 1), ((BaseEvent)message).ToStringFields(1)));
              }
              else
              {
                messageDescription = null;
              }
              packet.PacketDescription.MessageView = new MessageViewDescription(packet.Header.RtmpHeaderDescription, messageDescription, messageBytes);

                if (message is ChunkSize)
                {
                    ChunkSize chunkSizeMsg = message as ChunkSize;
                    context.SetReadChunkSize(chunkSizeMsg.Size);
                }
            }
            finally
            {
                context.SetLastReadPacket(channelId, null);
            }
#if !SILVERLIGHT
            if (log.IsDebugEnabled)
            {
                log.Debug("Decoded " + packet.ToString());
            }
#endif
			return packet;
		}

	  /// <summary>
	  /// Decodes RTMP packet header.
	  /// </summary>
	  /// <param name="context">RTMP protocol state.</param>
	  /// <param name="lastHeader">Previous header.</param>
	  /// <param name="stream">Buffer to be decoded.</param>
    /// <param name="startMark">start byte range for Description</param>
	  /// <returns>Decoded RTMP header.</returns>
	  public static RtmpHeader DecodeHeader(RtmpContext context, RtmpHeader lastHeader, ByteBuffer stream, int startMark)
        {
          long initialiPos = startMark;
          long startOffset = stream.Position-startMark;
      long curPos = stream.Position - startOffset;
      
			byte headerByte = stream.Get();
			int headerValue;
			int byteCount = 1;

      HeaderTypeDescription headerTypeDescription;
      ChannelIdTypeDescription channelIdTypeDescription = ChannelIdTypeDescription.GetInstanceFromValue(BytesRange.Range(curPos, 0, curPos, 5), (headerByte & 0x3f));
      BytesRange channelIdRange;
      ChannelIdDescription channelIdDescription;
      ChunkBasicHeaderDescription chunkBasicHeaderDescription;

      curPos = stream.Position - startOffset;

			if ((headerByte & 0x3f) == 0)
			{
			  // Two byte header
        channelIdRange = BytesRange.Range(curPos, curPos);
        headerValue = ((int)headerByte & 0xff) << 8 | ((int)stream.Get() & 0xff); 
				byteCount = 2;
			  
			} 
			else if ((headerByte & 0x3f) == 1) 
			{
				// Three byte header
        channelIdRange = BytesRange.Range(curPos, curPos + 1);
        headerValue = ((int)headerByte & 0xff) << 16 | ((int)stream.Get() & 0xff) << 8 | ((int)stream.Get() & 0xff); 
				byteCount = 3;
			} 
			else 
			{
				// Single byte header
        channelIdRange = BytesRange.Range(initialiPos, 0, initialiPos, 5);
        headerValue = (int)headerByte & 0xff;
				byteCount = 1;
			}
      
			byte channelId = DecodeChannelId(headerValue, byteCount);
      channelIdDescription=new ChannelIdDescription(channelIdRange,channelId);
      
			byte headerSize = DecodeHeaderSize(headerValue, byteCount);
      headerTypeDescription = HeaderTypeDescription.GetInstanceFromValue(BytesRange.Range(initialiPos, 6, initialiPos,7), headerSize);

      chunkBasicHeaderDescription = new ChunkBasicHeaderDescription(headerTypeDescription, channelIdTypeDescription, channelIdDescription);
			
      RtmpHeader header = new RtmpHeader();
			header.ChannelId = channelId;
			header.IsTimerRelative = (HeaderType)headerSize != HeaderType.HeaderNew;

            if ((HeaderType)headerSize != HeaderType.HeaderNew && lastHeader == null)
            {
#if !SILVERLIGHT
                if(log.IsErrorEnabled)
                    log.Error(string.Format("Last header null not new, headerSize: {0}, channelId {1}", headerSize, channelId));
#endif
                lastHeader = new RtmpHeader();
                lastHeader.ChannelId = channelId;
                lastHeader.IsTimerRelative = (HeaderType)headerSize != HeaderType.HeaderNew;
            }


#if !SILVERLIGHT
            if( log.IsDebugEnabled )
				log.Debug(__Res.GetString(__Res.Rtmp_DecodeHeader, Enum.GetName(typeof(HeaderType), (HeaderType)headerSize)));
#endif

      ChunkMessageHeaderDescription chunkMessageHeaderDescription;
      TimeStampDescription ts=null;
      MessageSizeDescription ms=null;
      MessageDataTypeDescription mdt=null;
      StreamIdDescription si=null;
      TimeStampExtendedDescription tse=null;

      curPos = stream.Position - startOffset;
			
      switch((HeaderType)headerSize)
			{
				case HeaderType.HeaderNew:
                    header.Timer = stream.ReadUInt24();// ReadUnsignedMediumInt();
                    header.Size = stream.ReadUInt24();// ReadMediumInt();
					header.DataType = stream.Get();
					header.StreamId = stream.ReadReverseInt();

          ts = new TimeStampDescription(BytesRange.Range(curPos, curPos + 2), header.Timer);
          ms = new MessageSizeDescription(BytesRange.Range(curPos + 3, curPos + 5), header.Size);
			    mdt =
            MessageDataTypeDescription.GetInstanceFromValue(BytesRange.Range(curPos + 6, curPos + 6), header.DataType);
          si = new StreamIdDescription(BytesRange.Range(curPos + 7, curPos + 10), header.StreamId);
			    
          if (header.Timer >= 0xffffff)
          {
            //Extended timestamp
            header.Timer = stream.GetInt();
            tse = new TimeStampExtendedDescription(BytesRange.Range(curPos + 11, curPos + 14), header.Timer);
          }
			    chunkMessageHeaderDescription = ChunkMessageHeaderDescription.OfType0(ts, ms, mdt, si, tse);
					break;
				case HeaderType.HeaderSameSource:
                    header.Timer = stream.ReadUInt24();// ReadUnsignedMediumInt();
                    header.Size = stream.ReadUInt24();// ReadMediumInt();
					header.DataType = stream.Get();
					header.StreamId = lastHeader.StreamId;

          ts = new TimeStampDescription(BytesRange.Range(curPos, curPos + 2), header.Timer);
          ms = new MessageSizeDescription(BytesRange.Range(curPos + 3, curPos + 5), header.Size);
          mdt =
            MessageDataTypeDescription.GetInstanceFromValue(BytesRange.Range(curPos + 6, curPos + 6), header.DataType);
          if (header.Timer >= 0xffffff)
          {
            //Extended timestamp
            header.Timer = stream.GetInt();
            tse = new TimeStampExtendedDescription(BytesRange.Range(curPos + 11, curPos + 14), header.Timer);
          }
          chunkMessageHeaderDescription = ChunkMessageHeaderDescription.OfType1(ts, ms, mdt,  tse);

					break;
				case HeaderType.HeaderTimerChange:
                    header.Timer = stream.ReadUInt24();//ReadUnsignedMediumInt();
                    header.Size = lastHeader.Size;
                    header.DataType = lastHeader.DataType;
                    header.StreamId = lastHeader.StreamId;
                    
                    ts = new TimeStampDescription(BytesRange.Range(curPos, curPos + 2), header.Timer);
                    if (header.Timer >= 0xffffff)
                    {
                      //Extended timestamp
                      header.Timer = stream.GetInt();
                      tse = new TimeStampExtendedDescription(BytesRange.Range(curPos + 11, curPos + 14), header.Timer);
                    }
                    chunkMessageHeaderDescription = ChunkMessageHeaderDescription.OfType2(ts, tse);
					break;
				case HeaderType.HeaderContinue:
                    header.Timer = lastHeader.Timer;
                    header.Size = lastHeader.Size;
                    header.DataType = lastHeader.DataType;
                    header.StreamId = lastHeader.StreamId;
                    header.IsTimerRelative = lastHeader.IsTimerRelative;
			              chunkMessageHeaderDescription = ChunkMessageHeaderDescription.OfType3();
					break;
				default:
#if !SILVERLIGHT
                    log.Error("Unexpected header size: " + headerSize);
#endif
					return null;
			}
      RtmpChunkHeaderDescription rtmpHeaderDescription = new RtmpChunkHeaderDescription(chunkBasicHeaderDescription, chunkMessageHeaderDescription);
      header.RtmpHeaderDescription = rtmpHeaderDescription;
			return header;
		}
        /// <summary>
        /// Decodes RTMP message event.
        /// </summary>
        /// <param name="context">RTMP protocol state.</param>
        /// <param name="header">RTMP header.</param>
        /// <param name="stream">Buffer to be decoded.</param>
        /// <returns>Decoded RTMP event.</returns>
		public static IRtmpEvent DecodeMessage(RtmpHeader header, ByteBuffer stream)
		{
			IRtmpEvent message = null;
            /*
			if(header.Timer == 0xffffff) 
			{
				// Skip first four bytes
                byte[] extendedTimestamp = new byte[4];
                stream.Read(extendedTimestamp, 0, 4);
                log.Warn("Discarding extended timestamp");
				//int unknown = stream.ReadInt32();
			}
            */
			switch(header.DataType) 
			{
                case Constants.TypeChunkSize:
					message = DecodeChunkSize(stream);
					break;
                case Constants.TypeInvoke:
					message = DecodeInvoke(stream);
					break;
                case Constants.TypeFlexInvoke:
					message = DecodeFlexInvoke(stream);
					break;
                case Constants.TypeNotify:
					if( header.StreamId == 0 )
						message = DecodeNotify(stream, header);
					else
						message = DecodeStreamMetadata(stream);
					break;
                case Constants.TypePing:
					message = DecodePing(stream);
					break;
                case Constants.TypeBytesRead:
					message = DecodeBytesRead(stream);
					break;
                case Constants.TypeAudioData:
					message = DecodeAudioData(stream);
					break;
                case Constants.TypeVideoData:
					message = DecodeVideoData(stream);
					break;
                case Constants.TypeSharedObject:
					message = DecodeSharedObject(stream);
					break;
                case Constants.TypeFlexSharedObject:
					message = DecodeFlexSharedObject(stream);
					break;
                case Constants.TypeServerBandwidth:
					message = DecodeServerBW(stream);
					break;
                case Constants.TypeClientBandwidth:
					message = DecodeClientBW(stream);
					break;
				default:
#if !SILVERLIGHT
                    log.Warn("Unknown object type: " + header.DataType);
#endif
					message = DecodeUnknown(stream);
					break;
			}
			message.Header = header;
			message.Timestamp = header.Timer;
			return message;
		}

		static Ping DecodePing(ByteBuffer stream)
		{
			Ping ping = new Ping();
			ping.PingType = stream.GetShort();
			ping.Value2 = stream.GetInt();
			if(stream.HasRemaining)
				ping.Value3 = stream.GetInt();
			if(stream.HasRemaining)
				ping.Value4 = stream.GetInt();
			return ping;
		}

		static AudioData DecodeAudioData(ByteBuffer stream)
		{
			return new AudioData(stream);
		}

		static VideoData DecodeVideoData(ByteBuffer stream)
		{
			return new VideoData(stream);
		}

		static ChunkSize DecodeChunkSize(ByteBuffer stream) 
		{
      long initialPos = stream.Position;
      int size = stream.GetInt();
      ChunkSize chunkSize = new ChunkSize(size);
      ChunkSizeDescription chunkSizeDescription = new ChunkSizeDescription(BytesRange.Range(initialPos, initialPos+3), size);
      ChunkSizeMessageDescription chunkSizeMessageDescription=new ChunkSizeMessageDescription(chunkSizeDescription);
		  chunkSize.RtmpMessageDescription = chunkSizeMessageDescription;
      return chunkSize;
		}

		static BytesRead DecodeBytesRead(ByteBuffer stream)
		{
            return new BytesRead(stream.GetInt());
		}

		static ServerBW DecodeServerBW(ByteBuffer stream)
		{
      long initialPos = stream.Position;
		  int bandwidth=stream.GetInt();
		  ServerBW serverBW = new ServerBW(bandwidth);

      ServerBandwidthDescription serverBandwidthDescription = new ServerBandwidthDescription(BytesRange.Range(initialPos, initialPos +3), bandwidth);
      ServerBandwidthMessageDescription serverBandwidthMessageDescription=new ServerBandwidthMessageDescription(serverBandwidthDescription);
		  serverBW.RtmpMessageDescription = serverBandwidthMessageDescription;
      return serverBW;
		}

		static ClientBW DecodeClientBW(ByteBuffer stream) 
		{
      long initialPos = stream.Position;
      int bandwidth = stream.GetInt();
      byte limitType = stream.Get();
		  ClientBW clientBW = new ClientBW(bandwidth, limitType);
      ClientBandwidthDescription clientBandwidthDescription = new ClientBandwidthDescription(BytesRange.Range(initialPos, initialPos+3), bandwidth);
		  ClientBandwidthLimitTypeDescription clientBandwidthLimitTypeDescription =
		    ClientBandwidthLimitTypeDescription.GetInstanceFromValue(BytesRange.Range(initialPos+4, initialPos+4),limitType);
      ClientBandwidthMessageDescription clientBandwidthMessageDescription=new ClientBandwidthMessageDescription(clientBandwidthDescription,clientBandwidthLimitTypeDescription);
		  clientBW.RtmpMessageDescription = clientBandwidthMessageDescription;
		  return clientBW;
		}

		static Unknown DecodeUnknown(ByteBuffer stream) 
		{
			return new Unknown(stream);
		}

		static Invoke DecodeInvoke(ByteBuffer stream)
		{
			return DecodeNotifyOrInvoke(new Invoke(), stream, null) as Invoke;
		}

		static FlexInvoke DecodeFlexInvoke(ByteBuffer stream)
		{
			int version = stream.ReadByte();
			RtmpReader reader = new RtmpReader(stream);
            string action = reader.ReadData() as string;
			int invokeId = System.Convert.ToInt32(reader.ReadData());
			object cmdData = reader.ReadData();

            object[] parameters = Call.EmptyArguments;
            if (stream.HasRemaining)
            {
#if !(NET_1_1)
                List<object> paramList = new List<object>();
#else
            ArrayList paramList = new ArrayList();
#endif
                while (stream.HasRemaining)
                {
                    object obj = reader.ReadData();
                    paramList.Add(obj);
                }
                parameters = paramList.ToArray();
            }
            /*
            int dotIndex = action == null ? -1 : action.LastIndexOf(".");
            string serviceName = (action == -1) ? null : action.Substring(0, dotIndex);
            string serviceMethod = (dotIndex == -1) ? action : action.Substring(dotIndex + 1, action.Length - dotIndex - 1);
            */
            PendingCall call = new PendingCall(null, action, parameters);
            FlexInvoke invoke = new FlexInvoke(invokeId, cmdData);
            invoke.ServiceCall = call;
			return invoke;
		}

		static Notify DecodeStreamMetadata(ByteBuffer stream) 
		{
            RtmpReader reader = new RtmpReader(stream);
            string action = reader.ReadData() as string;
            object obj = reader.ReadData();
            Notify notify = new Notify();
			int dotIndex = action.LastIndexOf(".");
			string serviceName = (dotIndex == -1) ? null : action.Substring(0, dotIndex);
            string serviceMethod = (dotIndex == -1) ? action : action.Substring(dotIndex + 1, action.Length - dotIndex - 1);
            object[] parameters = new object[1]{obj};
			Call call = new Call(serviceName, serviceMethod, parameters);
			notify.ServiceCall = call;
            return notify;
		}

		static Notify DecodeNotify(ByteBuffer stream, RtmpHeader header)
		{
			return DecodeNotifyOrInvoke(new Notify(), stream, header);
		}


		static Notify DecodeNotifyOrInvoke(Notify notify, ByteBuffer stream, RtmpHeader header)
		{
			long start = stream.Position;
      long before = start;
			RtmpReader reader = new RtmpReader(stream);
			string action = reader.ReadData() as string;
      long current = stream.Position;

      InvokeActionDescription actionDescription = new InvokeActionDescription(BytesRange.Range(before, current - 1), action);
      InvokeMessageDescription invokeMessageDescription = new InvokeMessageDescription(actionDescription);
			if(!(notify is Invoke))
			{
				//Don't decode "NetStream.send" requests
				stream.Position = start;
                notify.Data = ByteBuffer.Allocate(stream.Remaining);
                notify.Data.Put(stream);
				//notify.setData(in.asReadOnlyBuffer());
				return notify;
			}

			if(header == null || header.StreamId == 0)
			{
			  before = current;
				double invokeId = (double)reader.ReadData();
				notify.InvokeId = (int)invokeId;
			  current = stream.Position;
        InvokeIdDescription invokeIdDescription = new InvokeIdDescription(BytesRange.Range(before, current - 1), invokeId);
			  invokeMessageDescription.InvokeId = invokeIdDescription;
			}

            object[] parameters = Call.EmptyArguments;
			if(stream.HasRemaining)
			{
#if !(NET_1_1)
                List<object> paramList = new List<object>();
#else
                ArrayList paramList = new ArrayList();
#endif
			  before = current;
                object obj = reader.ReadData();
			  current = stream.Position;
			  List<IDescription> parameterDescriptionList = new List<IDescription>();
				if (obj is IDictionary||obj==null)
				{
					// for connect we get a map
					notify.ConnectionParameters = obj as IDictionary;
          InvokeConnectionParametersDescription connectionParametersDescription = new InvokeConnectionParametersDescription(BytesRange.Range(before, current - 1), ObjectDumper.Dump(notify.ConnectionParameters));
				  invokeMessageDescription.ConnectionParameters = connectionParametersDescription;
				} 
				else if (obj != null) 
				{
					paramList.Add(obj);
          parameterDescriptionList.Add(new InvokeParameterDescription(BytesRange.Range(before, current - 1), ObjectDumper.Dump(obj)));
				}

				while(stream.HasRemaining)
				{
          before = current;
				  obj=reader.ReadData();
					paramList.Add(obj);
          current = stream.Position;
          parameterDescriptionList.Add(new InvokeParameterDescription(BytesRange.Range(before, current - 1), ObjectDumper.Dump(obj)));
				}
				parameters = paramList.ToArray();
			  if (parameterDescriptionList.Count>0)
			  {
			    InvokeParametersDescription parametersDescription=new InvokeParametersDescription(parameterDescriptionList);
			    invokeMessageDescription.Parameters = parametersDescription;
			  }
			}

			int dotIndex = action.LastIndexOf(".");
			string serviceName = (dotIndex == -1) ? null : action.Substring(0, dotIndex);
            string serviceMethod = (dotIndex == -1) ? action : action.Substring(dotIndex + 1, action.Length - dotIndex - 1);

			if (notify is Invoke)
			{
				PendingCall call = new PendingCall(serviceName, serviceMethod, parameters);
                notify.ServiceCall = call;
			} 
			else 
			{
				Call call = new Call(serviceName, serviceMethod, parameters);
				notify.ServiceCall = call;
			}
		  notify.RtmpMessageDescription = invokeMessageDescription;
			return notify;
		}

		static void DecodeSharedObject(SharedObjectMessage so, ByteBuffer stream, RtmpReader reader)
		{
			// Parse request body
			while(stream.HasRemaining)
			{
			  SharedObjectActionTypeDescription soActionType = null;
			  SharedObjectEventContentLengthDescription soContentLength = null;
			  SharedObjectEventKeyDescription soEventKey = null;
			  SharedObjectEventValueDescription soEventValue = null;

			  int curByteIndex = (int)so.RtmpMessageDescription.BytesRange.End.ByteIndex + 1;
			  
				byte typeCode = reader.ReadByte();
			  soActionType = SharedObjectActionTypeDescription.GetInstanceFromValue(BytesRange.Range(curByteIndex,curByteIndex),typeCode);
			  curByteIndex += 1;

				SharedObjectEventType type = SharedObjectTypeMapping.ToType(typeCode);
				string key = null;
				object value = null;

				int length = stream.GetInt();//reader.ReadInt32();
        soContentLength = new SharedObjectEventContentLengthDescription(BytesRange.Range(curByteIndex, curByteIndex + 3), length);
        curByteIndex += 4;

        long initialPos = stream.Position;
			  long afterKeyPos;
			  long afterValuePos;
				switch(type)
				{
					case SharedObjectEventType.CLIENT_STATUS:
						// Status code
						key = reader.ReadString();
				    afterKeyPos = stream.Position;
            soEventKey = new SharedObjectEventKeyDescription(BytesRange.Range(curByteIndex, (int)(curByteIndex + afterKeyPos-initialPos)-1),key);
				    curByteIndex = (int) (curByteIndex + afterKeyPos - initialPos);
						// Status level
						value = reader.ReadString();
				    afterValuePos = stream.Position;
            soEventValue = new SharedObjectEventValueDescription(BytesRange.Range(curByteIndex, (int)(curByteIndex + afterValuePos- afterKeyPos) - 1), ObjectDumper.Dump(value));
				    curByteIndex = (int) (curByteIndex + afterValuePos - afterKeyPos);
						break;
					case SharedObjectEventType.CLIENT_UPDATE_DATA:
					{
                        key = reader.ReadString();
                        afterKeyPos = stream.Position;
                        soEventKey = new SharedObjectEventKeyDescription(BytesRange.Range(curByteIndex, (int)(curByteIndex + afterKeyPos - initialPos) - 1), key);
                        curByteIndex = (int)(curByteIndex + afterKeyPos - initialPos);

                        value = reader.ReadData();
                        afterValuePos = stream.Position;
                        soEventValue = new SharedObjectEventValueDescription(BytesRange.Range(curByteIndex, (int)(curByteIndex + afterValuePos - afterKeyPos) - 1), ObjectDumper.Dump(value));
                        curByteIndex = (int)(curByteIndex + afterValuePos - afterKeyPos);
                        /*
						key = null;
						// Map containing new attribute values
						Hashtable map = new Hashtable();
						int start = (int)stream.Position;
						while((int)stream.Position - start < length) 
						{
							string tmp = reader.ReadString();
							map[tmp] = reader.ReadData();
						}
						value = map;
                        */
					}
						break;
					default:
						if (type != SharedObjectEventType.SERVER_SEND_MESSAGE && type != SharedObjectEventType.CLIENT_SEND_MESSAGE) 
						{
							if (length > 0) 
							{
								key = reader.ReadString();
                afterKeyPos = stream.Position;
                soEventKey = new SharedObjectEventKeyDescription(BytesRange.Range(curByteIndex, (int)(curByteIndex + afterKeyPos - initialPos) - 1), key);
                curByteIndex = (int)(curByteIndex + afterKeyPos - initialPos);
								if(length > key.Length + 2) 
								{
									value = reader.ReadData();
                  afterValuePos = stream.Position;
                  soEventValue = new SharedObjectEventValueDescription(BytesRange.Range(curByteIndex, (int)(curByteIndex + afterValuePos - afterKeyPos) - 1), ObjectDumper.Dump(value));
                  curByteIndex = (int)(curByteIndex + afterValuePos - afterKeyPos);
								}
							}
						} 
						else 
						{
							int start = (int)stream.Position;
							// the "send" event seems to encode the handler name
							// as complete AMF string including the string type byte
							key = reader.ReadData() as string;
              afterKeyPos = stream.Position;
              soEventKey = new SharedObjectEventKeyDescription(BytesRange.Range(curByteIndex, (int)(curByteIndex + afterKeyPos - initialPos) - 1), key);
              curByteIndex = (int)(curByteIndex + afterKeyPos - initialPos);
							// read parameters
#if !(NET_1_1)
                            List<object> paramList = new List<object>();
#else
							ArrayList paramList = new ArrayList();
#endif
                            while (stream.Position - start < length)
							{
								object tmp = reader.ReadData();
								paramList.Add(tmp);
							}
							value = paramList;
              afterValuePos = stream.Position;
              soEventValue = new SharedObjectEventValueDescription(BytesRange.Range(curByteIndex, (int)(curByteIndex + afterValuePos - afterKeyPos) - 1), ObjectDumper.Dump(value));
              curByteIndex = (int)(curByteIndex + afterValuePos - afterKeyPos);
						}
						break;
				}
        SharedObjectEventDescription soEventDesc=new SharedObjectEventDescription(soActionType,soContentLength,soEventKey,soEventValue);
        ((SharedObjectMessageDescription)so.RtmpMessageDescription).AddSharedObjectEventsDescription(soEventDesc);
				so.AddEvent(type, key, value);
			}
		}

		static ISharedObjectMessage DecodeSharedObject(ByteBuffer stream)
		{
			RtmpReader reader = new RtmpReader(stream);
      long curByte = stream.Position;
      
      long initialPos = stream.Position;
			string name = reader.ReadString();
      long afterNamePos = stream.Position;

			// Read version of SO to modify
			int version = reader.ReadInt32();
			// Read persistence informations
			bool persistent = reader.ReadInt32() == 2;
			// Skip unknown bytes
			//skip(4);
			int unknown=reader.ReadInt32();

		  long soNameEndByteIndex = curByte + (int) (afterNamePos - initialPos) - 1;
      SharedObjectNameDescription soNameDesc = new SharedObjectNameDescription(BytesRange.Range(curByte, soNameEndByteIndex), name);
      SharedObjectVersionDescription soVersionDesc = new SharedObjectVersionDescription(BytesRange.Range(soNameEndByteIndex + 1, soNameEndByteIndex+4),version);
		  SharedObjectPersistencyDescription soPersistency =
		    SharedObjectPersistencyDescription.GetInstanceFromValue(
		      BytesRange.Range(soNameEndByteIndex + 5, soNameEndByteIndex + 8), persistent);
      SharedObjectVersionDescription soUnknownDesc = new SharedObjectVersionDescription(BytesRange.Range(soNameEndByteIndex + 9, soNameEndByteIndex + 12), unknown);
      SharedObjectMessageDescription smd = new SharedObjectMessageDescription(soNameDesc, soVersionDesc, soPersistency, soUnknownDesc);

			SharedObjectMessage so = new SharedObjectMessage(null, name, version, persistent);
		  so.RtmpMessageDescription = smd;

			DecodeSharedObject(so, stream, reader);
			return so;
		}

		static ISharedObjectMessage DecodeFlexSharedObject(ByteBuffer stream)
		{
			// Unknown byte, always 0?
			stream.Skip(1);
			RtmpReader reader = new RtmpReader(stream);
			string name = reader.ReadString();
			// Read version of SO to modify
			int version = reader.ReadInt32();
			// Read persistence informations
			bool persistent = reader.ReadInt32() == 2;
			// Skip unknown bytes
			reader.ReadInt32();

			SharedObjectMessage so = new FlexSharedObjectMessage(null, name, version, persistent);
			DecodeSharedObject(so, stream, reader);
			return so;
		}
	}
}
