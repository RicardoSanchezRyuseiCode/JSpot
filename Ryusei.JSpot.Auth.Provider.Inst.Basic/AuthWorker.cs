using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Ryusei.JSpot.Auth.Attr.Common;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Fty;
using Ryusei.JSpot.Auth.Fty.Contract;
using Ryusei.JSpot.Auth.Wrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Provider.Inst.Basic
{
    /// <summary>
    /// Name: AuthWorker
    /// Description: Worker to realize authentication
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class AuthWorker : OAuthAuthorizationServerProvider
    {
        #region [Attributes]

        #region [CRM Wrapper]
        /// <summary>
        /// UserDataWrapper
        /// </summary>
        private UserWrapper UserWrapper { get; set; }
        #endregion

        #region [CRM Managers]
        /// <summary>
        /// IUserDataMgr
        /// </summary>
        private IUserMgr IUserMgr { get; set; }
        /// <summary>
        /// ITokenTrackingMgr
        /// </summary>
        private ITokenTrackingMgr ITokenTrackingMgr { get; set; }
        #endregion

        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public AuthWorker()
        {
            // Wrapper creation
            this.UserWrapper = UserWrapper.GetInstance();
            // CRM Creation
            AuthBuilder crmBuilder = AuthBuilder.GetInstance();
            this.ITokenTrackingMgr = crmBuilder.GetManager<ITokenTrackingMgr>(AuthBuilder.ITOKENTRACKINGMGR);
            this.IUserMgr = crmBuilder.GetManager<IUserMgr>(AuthBuilder.IUSERMGR);
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: ValidateClientAuthentication
        /// Description: Method to validate an user
        /// </summary>
        /// <param name="context">Context for validation</param>
        /// <returns></returns>
        #pragma warning disable 1998
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        #pragma warning restore 1998
        /// <summary>
        ///  Name: TokenEndpoint
        ///  Description: Method to add properties to the token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
        /// <summary>
        /// Name: GranResourceOwnerCredentials
        /// Description: Method to validate the users
        /// </summary>
        /// <param name="context">Context for validation</param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Add header for CORS
            var request = await context.Request.ReadFormAsync();
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            // Get extra request header to know what kind of authentication need to be done
            string message = "";
            User user = null;
            // Get header to see if have to apply impersonation
            string impersonation = request["impersonation"];
            // Check if is impersonation
            if (string.IsNullOrEmpty(impersonation))
            {
                // If impersonation is null or empty the auth process is normal
                if ((user = this.UserWrapper.ValidateEmailAndPassword(context.UserName, context.Password, ref message)) == null)
                {
                    context.SetError("invalid_grand", message);
                    return;
                }
            }
            else
            {
                // If impersonation is not null, we need to validate credentials but return a ticket with the data of user
                if (this.UserWrapper.ValidateEmailAndPassword(context.UserName, context.Password, ref message) == null)
                {
                    context.SetError("invalid_grand", message);
                    return;
                }
                // If the admin credentials are good get the data of the other user in impersonation variable
                if ((user = this.UserWrapper.GetByEmail(impersonation, ref message)) == null)
                {
                    context.SetError("invalid_grand", message);
                    return;
                }
            }
            // Add claims
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            // Add user data id
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.UserId.ToString()));
            // Add email
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            // Add name
            identity.AddClaim(new Claim(ClaimType.NAME, user.Name));
            // Add lastname
            identity.AddClaim(new Claim(ClaimType.LASTNAME, user.Lastname));
            // Add Impersonation claim
            identity.AddClaim(new Claim(ClaimType.IMPERSONATION, (!string.IsNullOrEmpty(impersonation)).ToString(), ClaimValueTypes.Boolean));
            // Add properties
            AuthenticationProperties props = new AuthenticationProperties(new Dictionary<string, string> {
                    { "Name", user.Name },
                    { "Lastname", user.Lastname },
                    { "Company", user.Company },
                    { "Job", user.Job }
                });
            // Create token
            AuthenticationTicket ticket = new AuthenticationTicket(identity, props);
            // validate token
            context.Validated(ticket);
        }
        #endregion
    }
}
