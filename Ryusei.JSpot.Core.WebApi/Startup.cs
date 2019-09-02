using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Ryusei.JSpot.Core.WebApi.Startup))]
namespace Ryusei.JSpot.Core.WebApi
{
    /// <summary>
    /// Name: Startup
    /// Description: Startup class for project
    /// Author: Ricardo Sanchez Romero (ricardo.rsz.sanchez@faurecia.com)
    /// LogBook:
    ///     27/06/2018: Creation
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Name: Configuration
        /// Description: Method to make initial configuration of project
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions { });
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}