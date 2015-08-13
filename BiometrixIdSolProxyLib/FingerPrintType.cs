// Decompiled with JetBrains decompiler
// Type: BiometrixIDSolProxyLib.FingerPrintType
// Assembly: BiometrixIdSolProxyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B0DD19B3-7EB3-4F71-A183-2D7565C618ED
// Assembly location: D:\Recovery\BiometrixIDSolProxy\Bin\BiometrixIdSolProxyLib.dll

using System.Runtime.Serialization;

namespace BiometrixIDSolProxyLib
{
  [DataContract(Name = "FingerPrintType", Namespace = "http://www.creatrixinc.com/biometrix/soap/biometrixidsolproxyservice")]
  [KnownType(typeof (FingerPositionsType))]
  public class FingerPrintType
  {
    private FingerPositionsType? fingerPositionField;
    private byte[] fingerPrintImageField;
    private string binaryFingerPrintTemplateField;
    private string textFingerPrintTemplateField;

    [DataMember]
    public FingerPositionsType? FingerPosition
    {
      get
      {
        return this.fingerPositionField;
      }
      set
      {
        this.fingerPositionField = value;
      }
    }

    [DataMember]
    public byte[] FingerPrintImage
    {
      get
      {
        return this.fingerPrintImageField;
      }
      set
      {
        this.fingerPrintImageField = value;
      }
    }

    [DataMember]
    public string BinaryFingerPrintTemplate
    {
      get
      {
        return this.binaryFingerPrintTemplateField;
      }
      set
      {
        this.binaryFingerPrintTemplateField = value;
      }
    }

    [DataMember]
    public string TextFingerPrintTemplate
    {
      get
      {
        return this.textFingerPrintTemplateField;
      }
      set
      {
        this.textFingerPrintTemplateField = value;
      }
    }
  }
}
