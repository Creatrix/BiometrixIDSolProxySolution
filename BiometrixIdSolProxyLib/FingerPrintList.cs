// Decompiled with JetBrains decompiler
// Type: BiometrixIDSolProxyLib.FingerPrintList
// Assembly: BiometrixIdSolProxyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B0DD19B3-7EB3-4F71-A183-2D7565C618ED
// Assembly location: D:\Recovery\BiometrixIDSolProxy\Bin\BiometrixIdSolProxyLib.dll

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace BiometrixIDSolProxyLib
{
  [CollectionDataContract(ItemName = "FingerPrint", Namespace = "http://www.creatrixinc.com/biometrix/soap/biometrixidsolproxyservice")]
  public class FingerPrintList : Collection<FingerPrintType>
  {
  }
}
