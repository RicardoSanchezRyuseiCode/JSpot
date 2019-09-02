using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Ryusei.JSpot.WebApp.Startup))]
namespace Ryusei.JSpot.WebApp
{
    /// <summary>
    /// Name: Startup
    /// Description: Method to define startup
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions { });
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}