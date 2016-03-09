using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public static class Definitions
  {
    static Dictionary<Type,string> sDefinitions=new Dictionary<Type, string>();

    static Definitions()
    {
      AddDefiniation<HeaderTypeDescription>(@"HeaderType identifies one of four format used by the 'chunk message header': 0 -> new, 1 -> same streamId, 2 -> same streamId, message type and message length, 3 -> same streamId, message type, message length, and timestamp");
    }
    public static string GetDefinition<T>()
    {
      return sDefinitions[typeof (T)];
    }

    private static void AddDefiniation<T>(string description)
    {
      sDefinitions.Add(typeof(T),description);
    }
  }
}
