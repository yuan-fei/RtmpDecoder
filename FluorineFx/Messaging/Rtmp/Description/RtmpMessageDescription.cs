using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluorineFx.Messaging.Rtmp.SO;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public abstract class BaseRtmpMessageDescription:BaseCompositeDescription
  {
    public abstract string MessageType { get; }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.RTMP_MESSAGE,MessageType);
    }
  }

  public class UnImplementedMessageDescription : BaseCompositeDescription
  {
    public UnImplementedMessageDescription(IDescription valueDescription)
    {
      ValueDescription = valueDescription;
    }

    public IDescription ValueDescription { get; set; }

    public override List<IDescription> SubDescriptions()
    {
      return new List<IDescription>() { ValueDescription };
    }

    public override string Describe()
    {
      return DescriptionStrings.UNIMPLEMENTED_MESSAGE_DESCRIPTION;
    }
  }

  public class UnImplementedMessageValueDescription:BaseSingleValueDescription
  {
    public UnImplementedMessageValueDescription(BytesRange bytesRange, object value) : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.UNIMPLEMENTED_VALUE_DESCRIPTION, Value);
    }
  }

  public class ChunkSizeMessageDescription:BaseRtmpMessageDescription
  {
    public ChunkSizeMessageDescription(IDescription chunkSize)
    {
      ChunkSize = chunkSize;
    }

    public IDescription ChunkSize { get; private set; }

    public override List<IDescription> SubDescriptions()
    {
      return new List<IDescription> {ChunkSize};
    }

    public override string MessageType
    {
      get { return DescriptionStrings.CHUNCK_SIZE_MESSAGE; }
    }
  }

  public class ChunkSizeDescription:BaseSingleValueDescription
  {
    public ChunkSizeDescription(BytesRange bytesRange, object value) : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.CHUNCK_SIZE, Value);
    }
  }

  public class SharedObjectMessageDescription:BaseRtmpMessageDescription
  {
    public SharedObjectMessageDescription(IDescription soName, IDescription soVersion, IDescription isPersist, IDescription unknown)
    {
      SharedObjectName = soName;
      SharedObjectVersion = soVersion;
      SharedObjectPersistency = isPersist;
      SharedObjectUnknown = unknown;
      SharedObjectEvents=new List<IDescription>();
    }

    public IDescription SharedObjectName { get; private set; }
    public IDescription SharedObjectVersion { get; private set; }
    public IDescription SharedObjectPersistency { get; private set; }
    public IDescription SharedObjectUnknown { get; private set; }
    public List<IDescription> SharedObjectEvents { get; private set; }

    public void AddSharedObjectEventsDescription(SharedObjectEventDescription sharedObjectEvent)
    {
      SharedObjectEvents.Add(sharedObjectEvent);
    }


    public override List<IDescription> SubDescriptions()
    {
      List<IDescription> descriptions=new List<IDescription>() { SharedObjectName, SharedObjectVersion, SharedObjectPersistency,SharedObjectUnknown };
      descriptions.AddRange(SharedObjectEvents);
      return descriptions;
    }

    public override string MessageType
    {
      get { return DescriptionStrings.SHARED_OBJECT_MESSAGE; }
    }
  }

  public class SharedObjectNameDescription:BaseSingleValueDescription
  {
    public SharedObjectNameDescription(BytesRange bytesRange, object value) : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.SHARED_OBJECT_NAME, Value);
    }
  }

  public class SharedObjectVersionDescription : BaseSingleValueDescription
  {
    public SharedObjectVersionDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.SHARED_OBJECT_VERSION, Value);
    }
  }

  public class SharedObjectPersistencyDescription : BaseDescription
  {
    public static SharedObjectPersistencyDescription GetInstanceFromValue(BytesRange bytesRange,bool value)
    {
      return new SharedObjectPersistencyDescription(bytesRange, string.Format(DescriptionStrings.SHARED_OBJECT_PERSISTENCY, value));
    }

    private SharedObjectPersistencyDescription(BytesRange bytesRange, string descriptionStr) : base(bytesRange, descriptionStr)
    {
    }
  }

  public class SharedObjectUnknownDescription : BaseSingleValueDescription
  {
    public SharedObjectUnknownDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.SHARED_OBJECT_VERSION, Value);
    }
  }

  public class SharedObjectEventDescription:IDescription
  {
    public SharedObjectEventDescription(IDescription sharedObjectActionType, IDescription sharedObjectActionLength, IDescription sharedObjectEventKey, IDescription sharedObjectEventValue)
    {
      SharedObjectActionType = sharedObjectActionType;
      SharedObjectActionLength = sharedObjectActionLength;
      SharedObjectEventKey = sharedObjectEventKey;
      SharedObjectEventValue = sharedObjectEventValue;
      BytesRange = BytesRange.Range(sharedObjectActionType.BytesRange.Start, SharedObjectEventValue.BytesRange.End);
    }

    public IDescription SharedObjectActionType { get; private set; }
    public IDescription SharedObjectActionLength { get; private set; }
    public IDescription SharedObjectEventKey { get; private set; }
    public IDescription SharedObjectEventValue {get; private set; }

    public BytesRange BytesRange { get; private set; }

    public List<IDescription> SubDescriptions()
    {
      return new List<IDescription>(){SharedObjectActionType, SharedObjectActionLength, SharedObjectEventKey, SharedObjectEventValue};
    }

    public string Describe()
    {
      return DescriptionStrings.SHARED_OBJECT_EVENT;
    }
  }

  public class SharedObjectActionTypeDescription : BaseDescription
  {
    public static SharedObjectActionTypeDescription GetInstanceFromValue(BytesRange bytesRange, int value)
    {
      switch ((SharedObjectEventType)value)
      {
        case SharedObjectEventType.SERVER_CONNECT:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_SERVER_CONNECT);
        case SharedObjectEventType.SERVER_DISCONNECT:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_SERVER_DISCONNECT);
        case SharedObjectEventType.SERVER_SET_ATTRIBUTE:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_SERVER_SET_ATTRIBUTE);
        case SharedObjectEventType.CLIENT_UPDATE_DATA:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_CLIENT_UPDATE_DATA);
        case SharedObjectEventType.CLIENT_UPDATE_ATTRIBUTE:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_CLIENT_UPDATE_ATTRIBUTE);
        case SharedObjectEventType.SERVER_SEND_MESSAGE:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_SERVER_SEND_MESSAGE);
        case SharedObjectEventType.CLIENT_STATUS:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_CLIENT_STATUS);
        case SharedObjectEventType.CLIENT_CLEAR_DATA:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_CLIENT_CLEAR_DATA);
        case SharedObjectEventType.CLIENT_DELETE_DATA:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_CLIENT_DELETE_DATA);
        case SharedObjectEventType.SERVER_DELETE_ATTRIBUTE:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_SERVER_DELETE_ATTRIBUTE);
        case SharedObjectEventType.CLIENT_INITIAL_DATA:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_CLIENT_INITIAL_DATA);
        case SharedObjectEventType.CLIENT_SEND_MESSAGE:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_CLIENT_SEND_MESSAGE);
        case SharedObjectEventType.CLIENT_DELETE_ATTRIBUTE:
          return new SharedObjectActionTypeDescription(bytesRange, DescriptionStrings.SHARED_OBJECT_ACTION_TYPE_CLIENT_DELETE_ATTRIBUTE);
      }
      return null;
    }

    private SharedObjectActionTypeDescription(BytesRange bytesRange, string descriptionStr)
      : base(bytesRange, descriptionStr)
    {
    }
  }

  public class SharedObjectEventContentLengthDescription : BaseSingleValueDescription
  {
    public SharedObjectEventContentLengthDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.SHARED_OBJECT_ACTION_LENGTH, Value);
    }
  }


  public class SharedObjectEventKeyDescription : BaseSingleValueDescription
  {
    public SharedObjectEventKeyDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.SHARED_OBJECT_EVENT_KEY, Value);
    }
  }

  public class SharedObjectEventValueDescription : BaseSingleValueDescription
  {
    public SharedObjectEventValueDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.SHARED_OBJECT_EVENT_VALUE, Value);
    }
  }

  public class ServerBandwidthMessageDescription : BaseRtmpMessageDescription
  {
    public ServerBandwidthMessageDescription(IDescription serverBandwidth)
    {
      ServerBandwidth = serverBandwidth;
    }

    public IDescription ServerBandwidth { get; private set; }

    public override List<IDescription> SubDescriptions()
    {
      return new List<IDescription> { ServerBandwidth };
    }

    public override string MessageType
    {
      get { return DescriptionStrings.SERVER_BANDWIDTH_MESSAGE; }
    }
  }

  public class ServerBandwidthDescription : BaseSingleValueDescription
  {
    public ServerBandwidthDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.SERVER_BANDWIDTH, Value);
    }
  }

  public class ClientBandwidthMessageDescription : BaseRtmpMessageDescription
  {
    public ClientBandwidthMessageDescription(IDescription clientBandwidth, IDescription clientBandwidthLimit)
    {
      ClientBandwidth = clientBandwidth;
      ClientBandwidthLimit = clientBandwidthLimit;
    }

    public IDescription ClientBandwidth { get; private set; }
    public IDescription ClientBandwidthLimit { get; private set; }

    public override List<IDescription> SubDescriptions()
    {
      return new List<IDescription> { ClientBandwidth, ClientBandwidthLimit };
    }

    public override string MessageType
    {
      get { return DescriptionStrings.CLIENT_BANDWIDTH_MESSAGE; }
    }
  }

  public class ClientBandwidthDescription : BaseSingleValueDescription
  {
    public ClientBandwidthDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.CLIENT_BANDWIDTH, Value);
    }
  }

  public class ClientBandwidthLimitTypeDescription:BaseDescription
  {
    public static ClientBandwidthLimitTypeDescription GetInstanceFromValue(BytesRange bytesRange, int value)
    {
      switch (value)
      {
        case 0:
          return new ClientBandwidthLimitTypeDescription(bytesRange,DescriptionStrings.CLIENT_BANDWIDTH_LIMIT_TYPE_HARD);
        case 1:
          return new ClientBandwidthLimitTypeDescription(bytesRange, DescriptionStrings.CLIENT_BANDWIDTH_LIMIT_TYPE_SOFT);
        case 2:
          return new ClientBandwidthLimitTypeDescription(bytesRange, DescriptionStrings.CLIENT_BANDWIDTH_LIMIT_TYPE_DYNAMIC);
      }
      return null;
    }

    private ClientBandwidthLimitTypeDescription(BytesRange bytesRange, string descriptionStr) : base(bytesRange, descriptionStr)
    {
    }
  }

  public class InvokeMessageDescription : BaseRtmpMessageDescription
  {
    public InvokeMessageDescription(IDescription action)
    {
      Action = action;
    }

    public IDescription Action { get; private set; }
    public IDescription InvokeId { get; set; }
    public IDescription ConnectionParameters { get; set; }
    public IDescription Parameters { get; set; }

    public override List<IDescription> SubDescriptions()
    {
      List<IDescription> subDescriptions = new List<IDescription>() { Action ,InvokeId,ConnectionParameters,Parameters};
      subDescriptions.RemoveAll(d => d == null);
      return subDescriptions;
    }

    public override string  MessageType
    {
      get { return DescriptionStrings.INVOKE_MESSAGE; }
    }
  }

  public class InvokeActionDescription : BaseSingleValueDescription
  {
    public InvokeActionDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.INVOKE_ACTION, Value);
    }
  }

  public class InvokeIdDescription : BaseSingleValueDescription
  {
    public InvokeIdDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.INVOKE_ID, Value);
    }
  }

  public class InvokeConnectionParametersDescription : BaseSingleValueDescription
  {
    public InvokeConnectionParametersDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.INVOKE_CONNECTION_PARAMETERS, Value);
    }
  }

  public class InvokeParametersDescription : BaseCompositeDescription
  {

    public InvokeParametersDescription(List<IDescription> invokeParameterDescription)
    {
      InvokeParameterDescription = invokeParameterDescription;
    }

    public List<IDescription> InvokeParameterDescription { get; set; }

    public override List<IDescription> SubDescriptions()
    {
      return InvokeParameterDescription;
    }

    public override string Describe()
    {
      return DescriptionStrings.INVOKE_PARAMETERS;
      
    }
  }

  public class InvokeParameterDescription : BaseSingleValueDescription
  {
    public InvokeParameterDescription(BytesRange bytesRange, object value)
      : base(bytesRange, value)
    {
    }

    public override string Describe()
    {
      return string.Format(DescriptionStrings.INVOKE_PARAMETER, Value);
    }
  }
}
