using Newtonsoft.Json;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
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
        public string RegisterUser(string kw, string pw)
        {
            return JsonConvert.SerializeObject(User.Register(kw, pw));
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public string LoginUser(string kw, string pw)
        {
            return JsonConvert.SerializeObject(User.Login(kw, pw));
        }
    }
}