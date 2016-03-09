using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public static class DescriptionStrings
  {
    public static string UNIMPLEMENTED_MESSAGE_DESCRIPTION = "Unknown Message";
    public static string UNIMPLEMENTED_VALUE_DESCRIPTION = "{0}";
    public static string CHUNCK_VIEW_DESCRIPTION = "Chunk View";
    public static string MESSAGE_VIEW_DESCRIPTION = "Message View";
    public static string RTMP_CHUNK = "RTMP Chunk";
    public static string RTMP_HEADER = "RTMP Header";
    public static string CHUNCK_BASIC_HEADER = "ChunkBasicHeader";
    public static string CHUNCK_MESSAGE_HEADER = "ChunkMessageHeader (type {0})";
    public static string HEADER_TYPE_0 = "HeaderType = 0 (ChunkMessageHeader has 4 fields: Timestamp, Message size, Message Data Type, and StreamId)";
    public static string HEADER_TYPE_1 = "HeaderType = 1 (ChunkMessageHeader has 3 fields: Timestamp, Message size, and Message Data Type)";
    public static string HEADER_TYPE_2 = "HeaderType = 2 (ChunkMessageHeader has 1 field: Timestamp)";
    public static string HEADER_TYPE_3 = "HeaderType = 3 (ChunkMessageHeader has 0 field)";
    public static string CHANNEL_ID_TYPE_SMALL = "ChannelIdType = {0} : (2 <= (channelId = channelIdType) <= 63)";
    public static string CHANNEL_ID_TYPE_MIDDLE = "ChannelIdType = 0: (64 <= (channelId = 2nd byte+64) <= 319)";
    public static string CHANNEL_ID_TYPE_LARGE = "ChannelIdType = 1: (64 <= (channelId = 3rd byte*256 + 2nd byte + 64) <= 65599)";
    public static string CHANNEL_ID = "ChannelId = {0}";
    public static string TIMESTAMP = "TimeStamp = {0}";
    public static string TIMESTAMP_MAX = "TimeStamp = 0XFFFF, use TimeStamp extended for timestamp";
    public static string MESSAGE_SIZE = "Message Size = {0}";
    public static string STREAM_ID = "StreamId = {0}";
    public static string MESSAGE_DATA_TYPE_TypeUnknown = "Message Type = \"Unknown\" (Protocol Control Message)";
    public static string MESSAGE_DATA_TYPE_TypeChunkSize = "Message Data Type = \"ChunkSize\" (Protocol Control Message)";
    public static string MESSAGE_DATA_TYPE_TypeBytesRead = "Message Data Type = \"BytesRead\" (Protocol Control Message)";
    public static string MESSAGE_DATA_TYPE_TypePing = "Message Data Type = \"Ping\" (Protocol Control Message)";
    public static string MESSAGE_DATA_TYPE_TypeServerBandwidth = "Message Data Type = \"ServerBandwidth\" (Protocol Control Message)";
    public static string MESSAGE_DATA_TYPE_TypeClientBandwidth = "Message Data Type = \"ClientBandwidth\" (Protocol Control Message)";
    public static string MESSAGE_DATA_TYPE_TypeAudioData = "Message Data Type = \"AudioData\"";
    public static string MESSAGE_DATA_TYPE_TypeVideoData = "Message Data Type = \"VideoData\"";
    public static string MESSAGE_DATA_TYPE_TypeFlexStreamEnd = "Message Data Type = \"FlexStreamEnd\"";
    public static string MESSAGE_DATA_TYPE_TypeFlexSharedObject = "Message Data Type = \"FlexSharedObject\"";
    public static string MESSAGE_DATA_TYPE_TypeFlexInvoke = "Message Data Type = \"FlexInvoke\" (Command Message)";
    public static string MESSAGE_DATA_TYPE_TypeNotify = "Message Data Type = \"Notify\"";
    public static string MESSAGE_DATA_TYPE_TypeSharedObject = "Message Data Type = \"SharedObject\"";
    public static string MESSAGE_DATA_TYPE_TypeInvoke = "Message Data Type = \"Invoke\" (Command Message)";
    public static string TIMESTAMP_EXTENDED = "TimeStamp Extended = {0}";

    public static string CHUNCK_DATA = "RTMP Chunk Data";    

    public static string RTMP_MESSAGE = "RTMP Message (type {0})";
    public static string CHUNCK_SIZE_MESSAGE = "ChunkSize";
    public static string CHUNCK_SIZE = "Chunk Size of Sender = {0}";
    public static string SHARED_OBJECT_MESSAGE = "SharedObject";
    public static string SHARED_OBJECT_NAME = "Shared Object Name = {0}";
    public static string SHARED_OBJECT_VERSION = "Shared Object Version = {0}";
    public static string SHARED_OBJECT_PERSISTENCY = "Shared Object Persistency = {0}";
    public static string SHARED_OBJECT_UNKNOWN = "Unknown 4 bytes = {0}";
    public static string SHARED_OBJECT_EVENT = "Shared Object Event";
    
    public static string SHARED_OBJECT_ACTION_TYPE_SERVER_CONNECT = "Shared Object Action = SERVER_CONNECT";
    public static string SHARED_OBJECT_ACTION_TYPE_SERVER_DISCONNECT = "Shared Object Action = SERVER_DISCONNECT";
    public static string SHARED_OBJECT_ACTION_TYPE_SERVER_SET_ATTRIBUTE = "Shared Object Action = SERVER_SET_ATTRIBUTE";
    public static string SHARED_OBJECT_ACTION_TYPE_CLIENT_UPDATE_DATA = "Shared Object Action = CLIENT_UPDATE_DATA";
    public static string SHARED_OBJECT_ACTION_TYPE_CLIENT_UPDATE_ATTRIBUTE = "Shared Object Action = CLIENT_UPDATE_ATTRIBUTE";
    public static string SHARED_OBJECT_ACTION_TYPE_SERVER_SEND_MESSAGE = "Shared Object Action = SERVER_SEND_MESSAGE";
    public static string SHARED_OBJECT_ACTION_TYPE_CLIENT_STATUS = "Shared Object Action = CLIENT_STATUS";
    public static string SHARED_OBJECT_ACTION_TYPE_CLIENT_CLEAR_DATA = "Shared Object Action = CLIENT_CLEAR_DATA";
    public static string SHARED_OBJECT_ACTION_TYPE_CLIENT_DELETE_DATA = "Shared Object Action = CLIENT_DELETE_DATA";
    public static string SHARED_OBJECT_ACTION_TYPE_SERVER_DELETE_ATTRIBUTE = "Shared Object Action = SERVER_DELETE_ATTRIBUTE";
    public static string SHARED_OBJECT_ACTION_TYPE_CLIENT_INITIAL_DATA = "Shared Object Action = CLIENT_INITIAL_DATA";
    public static string SHARED_OBJECT_ACTION_TYPE_CLIENT_SEND_MESSAGE = "Shared Object Action = CLIENT_SEND_MESSAGE";
    public static string SHARED_OBJECT_ACTION_TYPE_CLIENT_DELETE_ATTRIBUTE = "Shared Object Action = CLIENT_DELETE_ATTRIBUTE";

    public static string SHARED_OBJECT_ACTION_LENGTH = "Shared Object Event Content Length = {0}";
    public static string SHARED_OBJECT_EVENT_KEY = "Shared Object Event Key = {0}";
    public static string SHARED_OBJECT_EVENT_VALUE = "Shared Object Event Value = {0}";

    public static string SERVER_BANDWIDTH_MESSAGE = "Server Bandwidth";
    public static string SERVER_BANDWIDTH = "Server Bandwidth = {0}";

    public static string CLIENT_BANDWIDTH_MESSAGE = "Client Bandwidth";
    public static string CLIENT_BANDWIDTH = "Client Bandwidth = {0}";
    public static string CLIENT_BANDWIDTH_LIMIT_TYPE_HARD = "Client Bandwidth Limit Type = Hard";
    public static string CLIENT_BANDWIDTH_LIMIT_TYPE_SOFT = "Client Bandwidth Limit Type = Soft";
    public static string CLIENT_BANDWIDTH_LIMIT_TYPE_DYNAMIC = "Client Bandwidth Limit Type = Dynamic";

    public static string INVOKE_MESSAGE = "Invoke";
    public static string INVOKE_ACTION = "Action = {0}";
    public static string INVOKE_ID = "Invoke Id = {0}";
    public static string INVOKE_CONNECTION_PARAMETERS = "Connection Parameters = {0}";
    public static string INVOKE_PARAMETERS = "Parameters";
    public static string INVOKE_PARAMETER = "{0}";

  }
}
