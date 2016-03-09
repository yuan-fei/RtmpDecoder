using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FluorineFx.Messaging.Messages;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public class ObjectDumper
  {
    public static string Dump(object obj)
    {
      return BodyToString(obj, 0);
    }

    /// <summary>
    /// Returns a string that represents body object.
    /// </summary>
    /// <param name="body">An object to trace.</param>
    /// <param name="indentLevel">The indentation level used for tracing object members.</param>
    /// <returns>A string that represents body object.</returns>
    protected static string BodyToString(object body, int indentLevel)
    {
      return BodyToString(body, indentLevel, null);
    }

    /// <summary>
    /// Returns a string that represents body object.
    /// </summary>
    /// <param name="body">An object to trace.</param>
    /// <param name="indentLevel">The indentation level used for tracing object members.</param>
    /// <param name="visited">Dictionary to handle circular references.</param>
    /// <returns>A string that represents body object.</returns>
    static string BodyToString(object body, int indentLevel, IDictionary visited)
    {
      try
      {
        indentLevel = indentLevel + 1;
        if (visited == null && indentLevel > 18)
          return Environment.NewLine + GetFieldSeparator(indentLevel) + "<..max-depth-reached..>";
        return InternalBodyToString(body, indentLevel, visited);
      }
      catch (Exception ex)
      {
        return "Exception in BodyToString: " + ex.Message;
      }
    }

    private static IDictionary CheckVisited(IDictionary visited, object obj)
    {
      if (visited == null)
        visited = new Dictionary<object, bool>();

      else if (!visited.Contains(obj))
        return null;
      visited.Add(obj, true);
      return visited;
    }

    /// <summary>
    /// Returns a string that represents body object.
    /// </summary>
    /// <param name="body">An object to trace.</param>
    /// <param name="indentLevel">The indentation level used for tracing object members.</param>
    /// <param name="visited">Dictionary to handle circular references.</param>
    /// <returns>A string that represents body object.</returns>
    protected static string InternalBodyToString(object body, int indentLevel, IDictionary visited)
    {
      if (body is object[])
      {
        if ((visited = CheckVisited(visited, body)) == null)
          return "<--";

        string sep = GetFieldSeparator(indentLevel);
        StringBuilder sb = new StringBuilder();
        object[] arr = body as object[];
        if (arr.Length > 0)
          sb.Append(GetFieldSeparator(indentLevel - 1));
        sb.Append("[");
        if (arr.Length > 0)
        {
          sb.Append(sep);
          for (int i = 0; i < arr.Length; i++)
          {
            if (i != 0)
            {
              sb.Append(",");
              sb.Append(sep);
            }
            sb.Append(BodyToString(arr[i], indentLevel, visited));
          }
          sb.Append(GetFieldSeparator(indentLevel - 1));
        }
        sb.Append("]");
        return sb.ToString();
      }
      else if (body is IDictionary)
      {
        IDictionary bodyMap = body as IDictionary;
        StringBuilder buf = new StringBuilder();
        buf.Append("{");
        bool first = true;
        foreach (DictionaryEntry entry in bodyMap)
        {
          if (!first)
            buf.Append(", ");
          object key = entry.Key;
          object value = entry.Value;
          buf.Append(key);
          buf.Append("=");
          //if (value == this)
          //  buf.Append("(recursive map as value)");
          //else
            buf.Append(BodyToString(value, indentLevel + 1, visited));
          first = false;
        }
        buf.Append("}");
        return buf.ToString();
      }
      else if (body is MessageBase)
      {
        return (body as MessageBase).ToString(indentLevel);
      }
      else if (body != null)
        return body.ToString();
      else return "null";
    }

    internal static String GetFieldSeparator(int indentLevel)
    {
      return MessageBase.GetFieldSeparator(indentLevel);
    }
  }
}
