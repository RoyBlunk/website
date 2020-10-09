using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Threading;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Website.Classes;

namespace Website.Services.General
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class RegisterLogin
    {
        //[WebInvoke(Method = "POST",
        //   BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //   RequestFormat = WebMessageFormat.Json,
        //   ResponseFormat = WebMessageFormat.Json)]

        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public bool RegisterUser(string kw, string pw)
        {
            return false;
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public bool LoginUser(string kw, string pw)
        {
            return false;
        }
    }
}