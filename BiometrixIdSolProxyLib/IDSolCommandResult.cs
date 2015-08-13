// Decompiled with JetBrains decompiler
// Type: BiometrixIDSolProxyLib.IDSolCommandResult
// Assembly: BiometrixIdSolProxyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B0DD19B3-7EB3-4F71-A183-2D7565C618ED
// Assembly location: D:\Recovery\BiometrixIDSolProxy\Bin\BiometrixIdSolProxyLib.dll

using System;
using System.Runtime.Serialization;

namespace BiometrixIDSolProxyLib
{
  public class IDSolCommandResult
  {
    private string personId;
    private int score;
    private string firstName;
    private string lastName;
    private DateTime dateOfBirth;
    private string gender;
    private byte[] faceImage;

    [DataMember(Order = 1)]
    public string PersonId
    {
      get
      {
        return this.personId;
      }
      set
      {
        this.personId = value;
      }
    }

    [DataMember(Order = 2)]
    public int Score
    {
      get
      {
        return this.score;
      }
      set
      {
        this.score = value;
      }
    }

    [DataMember(Order = 3)]
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

    [DataMember(Order = 4)]
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

    [DataMember(Order = 5)]
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

    [DataMember(Order = 6)]
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

    [DataMember(Order = 7)]
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
