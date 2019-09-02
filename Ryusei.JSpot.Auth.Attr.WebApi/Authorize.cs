using Newtonsoft.Json;
using RestSharp;
using Ryusei.JSpot.Auth.Attr.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Ryusei.JSpot.Auth.Attr.WebApi
{
    public class Authorize : AuthorizationFilterAttribute
    {
        #region [Attributes]
        /// <summary>
        /// Server
        /// </summary>
        public string ServerName { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public Authorize() { }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: OnAuthorizationAsync
        /// Description: Method to make the validations to see if user is available to get a resource
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            // Get User Identity
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            // Check if the user is authenticated
            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }
            
            // Get user dataId
            Guid userDataId = Guid.Parse(principal.Claims.First(x => x.Type.Equals(ClaimType.USER_ID)).Value);
            // Get controller name
            string controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            // Get action name
            string actionName = "";
            foreach (System.Reflection.CustomAttributeData attribute in ((System.Web.Http.Controllers.ReflectedHttpActionDescriptor)actionContext.ActionDescriptor).MethodInfo.CustomAttributes)
            {
                if (attribute.AttributeType.Name.Equals("RouteAttribute"))
                {
                    actionName = attribute.ConstructorArguments[0].Value.ToString();
                    break;
                }
            }
            // Create rest request to get the information
            RestClient restClient = new RestClient(ConfigurationManager.AppSettings["ApiGateway"]);
            RestRequest restRequest = new RestRequest("Auth/api/Permission/HaveAccess", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(new
            {
                UserDataId = userDataId.ToString(),
                Action = actionName,
                Controller = controllerName,
                Server = ServerName
            });
            // Execute request
            IRestResponse response = restClient.Execute(restRequest);
            // Check if result is ok
            if (!response.IsSuccessful)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }
            // Check the result
            dynamic jsonResponse = JsonConvert.DeserializeObject<bool>(response.Content);
            if (!jsonResponse)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }
            return Task.FromResult<object>(null);
        }
        #endregion
    }
}
