using Newtonsoft.Json;
using RestSharp;
using Ryusei.JSpot.Auth.Attr.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ryusei.JSpot.Auth.Attr.Mvc
{
    /// <summary>
    /// Name: Authorize
    /// Description: Class to define Authorize attribute to see if user have access to server
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-16: Creation
    /// </summary>
    public class Authorize : AuthorizeAttribute
    {
        #region [Attributes]
        /// <summary>
        /// Server Name
        /// </summary>
        public string ServerName { get; set; }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: OnAuthorization
        /// Description: Method to execution on authorization
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // filter context
            filterContext.HttpContext.Response.Cache.SetNoServerCaching();
            filterContext.HttpContext.Response.Cache.SetNoStore();
            // Get User Identity            
            var principal = filterContext.HttpContext.User as ClaimsPrincipal;
            // Check if the user is authenticated
            if (!principal.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            // Get user dataId
            Guid userDataId = Guid.Parse(principal.Claims.First(x => x.Type.Equals(ClaimType.USER_ID)).Value);
            // Get controller name
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            // Get action name
            string actionName = filterContext.RouteData.Values["action"].ToString();
            // Create request to check permissions
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
            // execute the request
            IRestResponse response = restClient.Execute(restRequest);
            // Check if response was successful
            if (!response.IsSuccessful)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            // Get the response
            dynamic jsonResponse = JsonConvert.DeserializeObject<bool>(response.Content);
            if (!jsonResponse)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
        #endregion
    }
}
