// Decompiled with JetBrains decompiler
// Type: BiometrixIDSolProxyLib.IDSolCommandResponse
// Assembly: BiometrixIdSolProxyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B0DD19B3-7EB3-4F71-A183-2D7565C618ED
// Assembly location: D:\Recovery\BiometrixIDSolProxy\Bin\BiometrixIdSolProxyLib.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BiometrixIDSolProxyLib
{
  [DataContract(Namespace = "http://www.creatrixinc.com/biometrix/soap/biometrixidsolproxyservice")]
  public class IDSolCommandResponse
  {
    private List<IDSolCommandResult> results = new List<IDSolCommandResult>();
    private int commandId;
    private string requestId;
    private CommandTypes commandType;

    [DataMember(Order = 1)]
    public int CommandId
    {
      get
      {
        return this.commandId;
      }
      set
      {
        this.commandId = value;
      }
    }

    [DataMember(Order = 3)]
    public CommandTypes CommandType
    {
      get
      {
        return this.commandType;
      }
      set
      {
        this.commandType = value;
      }
    }

    [DataMember(Order = 4)]
    public string RequestId
    {
      get
      {
        return this.requestId;
      }
      set
      {
        this.requestId = value;
      }
    }

    [DataMember(Order = 5)]
    public List<IDSolCommandResult> Results
    {
      get
      {
        return this.results;
      }
      set
      {
        this.results = value;
      }
    }
  }
}
