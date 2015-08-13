using BiometrixIDSolProxyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract(Namespace = Constants.Namespace)]
public interface IIDSolProxyService
{

    [OperationContract]
    //[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, UriTemplate = "identify")]
    IDSolCommandResponse Execute(IDSolRequestType request);

	// TODO: Add your service operations here
}

