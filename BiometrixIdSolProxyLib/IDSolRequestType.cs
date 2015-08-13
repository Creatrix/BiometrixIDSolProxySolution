// Decompiled with JetBrains decompiler
// Type: BiometrixIDSolProxyLib.IDSolRequestType
// Assembly: BiometrixIdSolProxyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B0DD19B3-7EB3-4F71-A183-2D7565C618ED
// Assembly location: D:\Recovery\BiometrixIDSolProxy\Bin\BiometrixIdSolProxyLib.dll

using System;
using System.Runtime.Serialization;

namespace BiometrixIDSolProxyLib
{
  [KnownType(typeof (FingerPrintType[]))]
  [DataContract(Name = "IDSolRequest", Namespace = "http://www.creatrixinc.com/biometrix/soap/biometrixidsolproxyservice")]
  [KnownType(typeof (byte[]))]
  public class IDSolRequestType
  {
    private string requestId;
    private string personIdField;
    private CommandTypes commandTypeField;
    private int commandId;
    private FingerPrintType[] fingerPrintField;
    private byte[] faceImage;
    private string firstName;
    private string lastName;
    private DateTime dateOfBirth;
    private string gender;

    [DataMember(Order = 1)]
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

    [DataMember(Order = 2)]
    public string PersonId
    {
      get
      {
        return this.personIdField;
      }
      set
      {
        this.personIdField = value;
      }
    }

    [DataMember(Order = 3)]
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

    [DataMember(Order = 4)]
    public CommandTypes CommandType
    {
      get
      {
        return this.commandTypeField;
      }
      set
      {
        this.commandTypeField = value;
      }
    }

    [DataMember(Order = 5)]
    public FingerPrintType[] FingerPrint
    {
      get
      {
        return this.fingerPrintField;
      }
      set
      {
        this.fingerPrintField = value;
      }
    }

    [DataMember(Order = 6)]
    public byte[] FaceImage
    {
      get
      {
        return this.faceImage;
      }
      set
      {
        this.faceImage = value;
      }
    }

    [DataMember(Order = 7)]
    public string FirstName
    {
      get
      {
        return this.firstName;
      }
      set
      {
        this.firstName = value;
      }
    }

    [DataMember(Order = 8)]
    public string LastName
    {
      get
      {
        return this.lastName;
      }
      set
      {
        this.lastName = value;
      }
    }

    [DataMember(Order = 9)]
    public DateTime DateOfBirth
    {
      get
      {
        return this.dateOfBirth;
      }
      set
      {
        this.dateOfBirth = value;
      }
    }

    [DataMember(Order = 10)]
    public string Gender
    {
      get
      {
        return this.gender;
      }
      set
      {
        this.gender = value;
      }
    }
  }
}
