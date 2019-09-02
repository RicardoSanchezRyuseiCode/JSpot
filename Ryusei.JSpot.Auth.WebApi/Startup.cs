using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Ryusei.JSpot.Auth.Provider.Fty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Ryusei.JSpot.Auth.WebApi.Startup))]
namespace Ryusei.JSpot.Auth.WebApi
{
    public class Startup
    {
        /// <summary>
        /// Name: Configuration
        /// Description: Method to make the configuration of OAUTH
        /// </summary>
        /// <param name="app">IAppBuilder</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
        /// <summary>
        /// Name: ConfigureOAuth
        /// Description: Metho to define the configuration of OAuth
        /// </summary>
        /// <param name="app">IAppBuilder</param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            ProviderBuilder providerBuilder = ProviderBuilder.GetInstance();
            IOAuthAuthorizationServerProvider provider = providerBuilder.GetProvider<IOAuthAuthorizationServerProvider>(ProviderBuilder.IAUTHAUTHORIZATIONSERVERPROVIDER);
            IAuthenticationTokenProvider tokenProvider = providerBuilder.GetProvider<IAuthenticationTokenProvider>(ProviderBuilder.IAUTHENTICATIONTOKENPROVIDER);
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(120),
                Provider = provider,
                RefreshTokenProvider = tokenProvider
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}