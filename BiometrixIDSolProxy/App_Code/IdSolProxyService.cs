using BiometrixIDSolProxyLib;
using IDS.IMS.Common;
using IDS.IMS.Common.Commands;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Serialization;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
[ServiceBehavior(Namespace = Constants.Namespace, IncludeExceptionDetailInFaults = true)]
public class IDSolProxyService : IIDSolProxyService
{

    private static Logger log = LogManager.GetLogger("AFISServer");

    public IDSolCommandResponse Execute(BiometrixIDSolProxyLib.IDSolRequestType request)
    {
        log.Info("Execute Called, Command Type: " + request.CommandType);

        IDSolCommandResponse resp = new IDSolCommandResponse();
        resp.RequestId = request.RequestId;
        resp.CommandType = request.CommandType;

        if (request.CommandType == CommandTypes.ENROLL)
        {
            log.Info("Executing Enroll Command...");
            int cmdId = IMSUtil.Enroll(request);
            log.Info("Enroll Command Id: " + cmdId);
            resp.CommandId = cmdId;
        }
        if (request.CommandType == CommandTypes.IDENTIFY)
        {
            log.Info("Executing Identify Command...");
            int cmdId = IMSUtil.Identify(request);
            log.Info("Identify Command Id: " + cmdId);
            resp.CommandId = cmdId;

        }
        if (request.CommandType == CommandTypes.VERIFY)
        {
            log.Info("Executing Verify Command...");
            
            int cmdId = IMSUtil.Verify(request);
            log.Info("Verify Command Id: " + cmdId);
            resp.CommandId = cmdId;

        }
        if (request.CommandType == CommandTypes.GET_ENROLL_RESULT)
        {
            log.Info("Executing Get Enroll Result Command...");
            try
            {
                IDS.IMS.Common.Commands.EnrollCommandResult ecr = IMSUtil.GetEnrollCommandResult(request.CommandId);
                resp.CommandId = request.CommandId;
                if (ecr != null)
                {
                    if (ecr.Matches != null)
                    {
                        foreach (Match m in ecr.Matches)
                        {
                            IDSolCommandResult crr = processMatch(m);
                            resp.Results.Add(crr);
                        }
                    }
                    else
                    {
                        log.Info("Command Id: " + request.CommandId + " No matches found.");
                    }
                }
            }
            catch (Exception e)
            {
                log.ErrorException(e.Message, e);
                throw e;
            }
        }
        if (request.CommandType == CommandTypes.GET_IDENTIFY_RESULT)
        {
            log.Info("Executing Get Identify Result Command...");
            try
            {
                IDS.IMS.Common.Commands.IdentifyCommandResult icr = IMSUtil.GetIdentifyCommandResult(request.CommandId);
                resp.CommandId = request.CommandId;
                if (icr != null)
                {
                    log.Info("ICR is not null...");
                    if (icr.Matches != null)
                    {
                        log.Info("Matches is not null...");
                        foreach (Match m in icr.Matches)
                        {
                            log.Info("Match Found: " + m.ExternalId);
                            IDSolCommandResult crr = processMatch(m);
                            resp.Results.Add(crr);
                        }
                    }
                    else
                    {
                        log.Info("Command Id: " + request.CommandId + " No matches found.");
                    }
                }
                else
                {
                    log.Info("ICR is null...");
                }
                
            }
            catch (Exception e)
            {
                log.ErrorException(e.Message, e);
                throw e;
            }
        }
        if (request.CommandType == CommandTypes.GET_VERIFY_RESULT)
        {
            log.Info("Executing Get Verify Result Command...");
            try
            {

                IDS.IMS.Common.Commands.VerifyCommandResult vcr = IMSUtil.GetVerifyCommandResult(request.CommandId);
                resp.CommandId = request.CommandId;
                if (vcr != null)
                {
                    log.Info("VCR is not null...");
                    if (vcr.Matches != null)
                    {
                        log.Info("Matches is not null...");
                        foreach (Match m in vcr.Matches)
                        {
                            log.Info("Match Found: " + m.ExternalId);
                            //IDSolCommandResult crr = processMatch(m);
                            IDSolCommandResult crr = new IDSolCommandResult();
                            crr.PersonId = m.ExternalId;
                            crr.Score = m.Score;
                            resp.Results.Add(crr);
                        }
                    }
                    else
                    {
                        log.Info("Command Id: " + request.CommandId + " No matches found.");
                    }
                }
                else
                {
                    log.Info("VCR is null...");
                }
                
                
            }
            catch (Exception e)
            {
                log.ErrorException(e.Message, e);
                throw e;
            }
        }
        if (request.CommandType == CommandTypes.GET_PERSON)
        {
            log.Info("Executing Get Person Command...");
            try
            {
                Person p = IMSUtil.GetPerson(request.PersonId);
                IDSolCommandResult result = new IDSolCommandResult();
                IDS.Common.BioAPI.CompositeTemplate ct = IMSUtil.GetCredentials(request.PersonId);
                foreach (IDS.Common.BioAPI.ITemplate it in ct.Collection)
                {
                    log.Info("Template Type: " + it.DataType + "\tData Type: " + it.GetType().FullName);
                    if (it.GetType() == typeof(IDS.Common.BioAPI.FacialTemplate))
                    {
                        IDS.Common.BioAPI.FacialTemplate ft = (IDS.Common.BioAPI.FacialTemplate)it;
                        //Util.WriteArrayToFile("c:\\logs\\" + request.PersonId + ".jpg", ft.Identifier.RawData);
                        result.FaceImage = ft.Identifier.RawData;

                    }
                    if (it.GetType() == typeof(IDS.Common.BioAPI.FingerprintTemplate))
                    {
                        IDS.Common.BioAPI.FingerprintTemplate ft = (IDS.Common.BioAPI.FingerprintTemplate)it;

                        IDS.Common.BioAPI.EFinger finger = ft.Finger;

                    }
                }

                result.FirstName = (String)p["FIRSTNAME"];
                result.LastName = (String)p["LASTNAME"];
                result.Gender = (String)p["GENDER"];
                result.DateOfBirth = (DateTime)p["BIRTHDAY"];
                result.PersonId = p.ExternalId;
                resp.Results.Add(result);
            }
            catch (Exception e)
            {
                
                log.ErrorException(e.Message, e);
                throw e;
            }
        }

        return resp;


    }

    private IDSolCommandResult processMatch(Match m)

    {
        IDSolCommandResult crr = new IDSolCommandResult();
        crr.PersonId = m.ExternalId;
        crr.Score = m.Score;
        
        Person p = IMSUtil.GetPerson(m.ExternalId);
        if (p != null)
        {
            try
            {
                crr.FirstName = (String)p["FIRSTNAME"];
            }
            catch { }
            try
            {
                crr.LastName = (String)p["LASTNAME"];
            }
            catch { }
            try
            {
                crr.Gender = (String)p["GENDER"];
            }
            catch { }
            try
            {
                crr.DateOfBirth = (DateTime)p["BIRTHDAY"];
            }
            catch { }
            log.Info("DOB: " + crr.DateOfBirth);
            IDS.Common.BioAPI.CompositeTemplate ct = IMSUtil.GetCredentials(m.ExternalId);
            foreach (IDS.Common.BioAPI.ITemplate it in ct.Collection)
            {
                log.Info("Template Type: " + it.DataType + "\tData Type: " + it.GetType().FullName);
                if (it.GetType() == typeof(IDS.Common.BioAPI.FacialTemplate))
                {
                    IDS.Common.BioAPI.FacialTemplate ft = (IDS.Common.BioAPI.FacialTemplate)it;
                    crr.FaceImage = ft.Identifier.RawData;
                }
                if (it.GetType() == typeof(IDS.Common.BioAPI.FingerprintTemplate))
                {
                    //leave blank for now
                }
            }
        }
        return crr;
    }
}
