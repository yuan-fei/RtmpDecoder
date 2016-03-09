using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.IO;

namespace FluorineFx.Messaging.Rtmp.Description
{
  public interface IDescriptionPrinter
  {
    void Print(IDescription root);
  }

  public class DescriptionPrinter:IDescriptionPrinter
  {
    IndentedTextWriter mPrinter;
    public DescriptionPrinter(TextWriter writer)
    {
      mPrinter = new IndentedTextWriter(writer);
    }

    #region IDescriptionPrinter Members

    public void Print(IDescription root)
    {
      if (root != null)
      {
        mPrinter.WriteLine(root.Describe());
        mPrinter.Indent++;
        foreach (IDescription subDescription in root.SubDescriptions())
        {
          Print(subDescription);
        }
        mPrinter.Indent--;
      }
    }

    #endregion

    
  }
}
