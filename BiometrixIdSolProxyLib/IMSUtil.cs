// Decompiled with JetBrains decompiler
// Type: BiometrixIDSolProxyLib.IMSUtil
// Assembly: BiometrixIdSolProxyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B0DD19B3-7EB3-4F71-A183-2D7565C618ED
// Assembly location: D:\Recovery\BiometrixIDSolProxy\Bin\BiometrixIdSolProxyLib.dll

using IDS.Common.BioAPI;
using IDS.IMS.Client;
using IDS.IMS.Common;
using IDS.IMS.Common.Commands;
using NLog;
using System;
using System.Collections.Generic;

namespace BiometrixIDSolProxyLib
{
  public class IMSUtil
  {
    private static Logger log = LogManager.GetLogger("IMSUtil");
    public static ServiceClient client = ServiceClient.Create();

    public static EFinger GetFingerByNumber(FingerPositionsType position)
    {
      EFinger efinger = EFinger.Unknown;
      switch (position)
      {
        case FingerPositionsType.RIGHT_THUMB:
          efinger = EFinger.RightThumb;
          break;
        case FingerPositionsType.RIGHT_INDEX:
          efinger = EFinger.RightIndexFinger;
          break;
        case FingerPositionsType.RIGHT_MIDDLE:
          efinger = EFinger.RightMiddleFinger;
          break;
        case FingerPositionsType.RIGHT_RING:
          efinger = EFinger.RightRingFinger;
          break;
        case FingerPositionsType.RIGHT_LITTLE:
          efinger = EFinger.RightLittleFinger;
          break;
        case FingerPositionsType.LEFT_THUMB:
          efinger = EFinger.LeftThumb;
          break;
        case FingerPositionsType.LEFT_INDEX:
          efinger = EFinger.LeftIndexFinger;
          break;
        case FingerPositionsType.LEFT_MIDDLE:
          efinger = EFinger.LeftMiddleFinger;
          break;
        case FingerPositionsType.LEFT_RING:
          efinger = EFinger.LeftRingFinger;
          break;
        case FingerPositionsType.LEFT_LITTLE:
          efinger = EFinger.LeftLittleFinger;
          break;
      }
      return efinger;
    }

    private static FacialTemplate CreateFaceTemplateFromImage(IDSolRequestType request)
    {
      FacialTemplate facialTemplate = new FacialTemplate();
      facialTemplate.DataType = EBiometricDataType.Raw;
      GenericIdentifier genericIdentifier = new GenericIdentifier();
      genericIdentifier.IdentType = new IdentifierType(EBiometricIdentifierType.FacialFeatures);
      genericIdentifier.RawData = request.FaceImage;
      return facialTemplate;
    }

    private static CompositeTemplate CreateFingerprintTemplateFromTemplate(IDSolRequestType request)
    {
      CompositeTemplate compositeTemplate = new CompositeTemplate();
      compositeTemplate.DataType = EBiometricDataType.Processed;
      for (int index = 0; index < request.FingerPrint.Length; ++index)
      {
        FingerprintTemplate fingerprintTemplate = new FingerprintTemplate();
        fingerprintTemplate.DataType = EBiometricDataType.Processed;
        fingerprintTemplate.Finger = IMSUtil.GetFingerByNumber(request.FingerPrint[index].FingerPosition.Value);
        GenericIdentifier genericIdentifier = new GenericIdentifier();
        genericIdentifier.RawData = request.FingerPrint[index].FingerPrintImage;
        genericIdentifier.IdentType = new IdentifierType();
        genericIdentifier.IdentType.Type = EBiometricIdentifierType.FingerPrint;
        fingerprintTemplate.Identifier = (IIdentifier) genericIdentifier;
        compositeTemplate.Collection.Add((object) fingerprintTemplate);
      }
      return compositeTemplate;
    }

    private static CompositeTemplate CreateTemplateFromImages(IDSolRequestType request)
    {
      CompositeTemplate compositeTemplate = new CompositeTemplate();
      compositeTemplate.DataType = EBiometricDataType.Raw;
      for (int index = 0; index < request.FingerPrint.Length; ++index)
      {
        FingerprintTemplate fingerprintTemplate = new FingerprintTemplate();
        fingerprintTemplate.DataType = EBiometricDataType.Raw;
        fingerprintTemplate.Finger = IMSUtil.GetFingerByNumber(request.FingerPrint[index].FingerPosition.Value);
        GenericIdentifier genericIdentifier = new GenericIdentifier();
        byte[] afisReadyBmp = ImageUtil.GetAFISReadyBMP(request.FingerPrint[index].FingerPrintImage);
        genericIdentifier.RawData = afisReadyBmp;
        genericIdentifier.IdentType = new IdentifierType();
        genericIdentifier.IdentType.Type = EBiometricIdentifierType.FingerPrint;
        fingerprintTemplate.Identifier = (IIdentifier) genericIdentifier;
        compositeTemplate.Collection.Add((object) fingerprintTemplate);
      }
      if (request.FaceImage != null)
      {
        IMSUtil.log.Info("Face Image Is Not Null");
        FacialTemplate facialTemplate = new FacialTemplate();
        facialTemplate.DataType = EBiometricDataType.Raw;
        GenericIdentifier genericIdentifier = new GenericIdentifier();
        genericIdentifier.IdentType = new IdentifierType(EBiometricIdentifierType.FacialFeatures);
        genericIdentifier.RawData = request.FaceImage;
        facialTemplate.Identifier = (IIdentifier) genericIdentifier;
        compositeTemplate.Collection.Add((object) facialTemplate);
      }
      return compositeTemplate;
    }

    public static CommandInfo GetCommandInfo(int commandId)
    {
      return IMSUtil.client.GetCommandInfo(commandId);
    }

    public static List<CommandStatistics> GetCommandStatistics()
    {
      return IMSUtil.client.GetCommandStatistics((string) null);
    }

    public static EnrollCommandArgs GetEnrollCommand(IDSolRequestType request)
    {
      EnrollCommandArgs enrollCommandArgs = new EnrollCommandArgs();
      IDS.IMS.Common.Person person = new IDS.IMS.Common.Person();
      person["EXTERNALID"] = (object) request.PersonId.ToString();
      person["FIRSTNAME"] = (object) request.FirstName;
      person["LASTNAME"] = (object) request.LastName;
      person["GENDER"] = (object) request.Gender;
      IMSUtil.log.Info("DOB: " + (object) request.DateOfBirth);
      person["BIRTHDAY"] = (object) request.DateOfBirth;
      enrollCommandArgs.Person = person;
      enrollCommandArgs.Credentials = TemplateHelper.ITemplateToByteArray((ITemplate) IMSUtil.CreateTemplateFromImages(request));
      return enrollCommandArgs;
    }

    public static IdentifyCommandArgs GetIdentifyCommand(IDSolRequestType request)
    {
      return new IdentifyCommandArgs()
      {
        Credentials = TemplateHelper.ITemplateToByteArray((ITemplate) IMSUtil.CreateTemplateFromImages(request))
      };
    }

    public static VerifyCommandArgs GetVerifyCommand(IDSolRequestType request)
    {
      return new VerifyCommandArgs()
      {
        ExternalID = request.PersonId,
        Credentials = TemplateHelper.ITemplateToByteArray((ITemplate) IMSUtil.CreateTemplateFromImages(request))
      };
    }

    public static int Enroll(IDSolRequestType request)
    {
      return IMSUtil.client.ExecuteCommand((CommandArgsBase) IMSUtil.GetEnrollCommand(request));
    }

    public static int Identify(IDSolRequestType request)
    {
      return IMSUtil.client.ExecuteCommand((CommandArgsBase) IMSUtil.GetIdentifyCommand(request));
    }

    public static int Verify(IDSolRequestType request)
    {
      return IMSUtil.client.ExecuteCommand((CommandArgsBase) IMSUtil.GetVerifyCommand(request));
    }

    public static EnrollCommandResult GetEnrollCommandResult(int cmdId)
    {
      CommandResultBase commandResult = IMSUtil.client.GetCommandResult(cmdId);
      if (commandResult != null && commandResult.GetType() == typeof (ErrorCommandResult))
      {
        ErrorCommandResult errorCommandResult = (ErrorCommandResult) commandResult;
        IMSUtil.log.Error((string) (object) errorCommandResult.CommandId + (object) "," + errorCommandResult.Message + "," + (string) (object) errorCommandResult.Type);
        throw new Exception(errorCommandResult.Message);
      }
      return (EnrollCommandResult) commandResult;
    }

    public static IdentifyCommandResult GetIdentifyCommandResult(int cmdId)
    {
      CommandResultBase commandResult = IMSUtil.client.GetCommandResult(cmdId);
      if (commandResult != null && commandResult.GetType() == typeof (ErrorCommandResult))
      {
        ErrorCommandResult errorCommandResult = (ErrorCommandResult) commandResult;
        IMSUtil.log.Error((string) (object) errorCommandResult.CommandId + (object) "," + errorCommandResult.Message + "," + (string) (object) errorCommandResult.Type);
        throw new Exception(errorCommandResult.Message);
      }
      return (IdentifyCommandResult) commandResult;
    }

    public static VerifyCommandResult GetVerifyCommandResult(int cmdId)
    {
      CommandResultBase commandResult = IMSUtil.client.GetCommandResult(cmdId);
      if (commandResult != null && commandResult.GetType() == typeof (ErrorCommandResult))
      {
        ErrorCommandResult errorCommandResult = (ErrorCommandResult) commandResult;
        IMSUtil.log.Error((string) (object) errorCommandResult.CommandId + (object) "," + errorCommandResult.Message + "," + (string) (object) errorCommandResult.Type);
        throw new Exception(errorCommandResult.Message);
      }
      return (VerifyCommandResult) commandResult;
    }

    public static IDS.IMS.Common.Person GetPerson(string personId)
    {
      return IMSUtil.client.GetPerson(personId);
    }

    public static CompositeTemplate GetCredentials(string personId)
    {
      return (CompositeTemplate) TemplateHelper.ITemplateFromByteArray(IMSUtil.client.GetCredentials(personId));
    }
  }
}
