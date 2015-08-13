// Decompiled with JetBrains decompiler
// Type: BiometrixIDSolProxyLib.Util
// Assembly: BiometrixIdSolProxyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B0DD19B3-7EB3-4F71-A183-2D7565C618ED
// Assembly location: D:\Recovery\BiometrixIDSolProxy\Bin\BiometrixIdSolProxyLib.dll

using System.IO;

namespace BiometrixIDSolProxyLib
{
  public class Util
  {
    public static void WriteArrayToFile(string file, byte[] array)
    {
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = new FileStream(file, FileMode.Create, FileAccess.ReadWrite);
        fileStream.Write(array, 0, array.Length);
      }
      catch
      {
      }
      finally
      {
        try
        {
          fileStream.Close();
        }
        catch
        {
        }
      }
    }
  }
}
