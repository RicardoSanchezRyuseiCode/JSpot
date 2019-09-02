using Microsoft.Owin.Security.Infrastructure;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Fty;
using Ryusei.JSpot.Auth.Fty.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Provider.Inst.Basic
{
    /// <summary>
    /// Name: TokenWorker
    /// Description: Worker to manage token
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class TokenWorker : IAuthenticationTokenProvider
    {
        #region [Attributes]
        /// <summary>
        /// ITokenTrackingMgr
        /// </summary>
        private ITokenTrackingMgr ITokenTrackingMgr { get; set; }
        #endregion

        #region [Constants]
        /// <summary>
        /// Default constructor
        /// </summary>
        public TokenWorker()
        {
            AuthBuilder crmBuilder = AuthBuilder.GetInstance();
            this.ITokenTrackingMgr = crmBuilder.GetManager<ITokenTrackingMgr>(AuthBuilder.ITOKENTRACKINGMGR);
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Create
        /// Description: Method to create context
        /// </summary>
        /// <param name="context"></param>
        public void Create(AuthenticationTokenCreateContext context) { }
        /// <summary>
        /// Name: Receive
        /// Description: Method to receive context
        /// </summary>
        /// <param name="context"></param>
        public void Receive(AuthenticationTokenReceiveContext context) { }
        /// <summary>
        /// Name: CreateAsync
        /// Description: Method to create async context
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>Task</returns>
        #pragma warning disable 1998
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            // Define Id of refresh token
            var guid = Guid.NewGuid();
            // Serialize token
            string strRefreshToken = context.SerializeTicket();
            // Save token tracking
            this.ITokenTrackingMgr.Save(new TokenTracking()
            {
                TokenTrackingId = guid,
                Token = strRefreshToken,
                RequestedDate = DateTime.Now.ToUniversalTime(),
                UserId = Guid.Parse(context.Ticket.Identity.Claims.ElementAt(0).Value)
            });
            // Set id to token
            context.SetToken(guid.ToString());
        }
        #pragma warning restore 1998
        /// <summary>
        /// Name: ReceiveAsync
        /// Description: Method to receive async
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        #pragma warning disable 1998
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            /// Add header
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            // Retrive token
            TokenTracking tokenTracking = this.ITokenTrackingMgr.GetById(Guid.Parse(context.Token));
            this.ITokenTrackingMgr.Delete(tokenTracking.TokenTrackingId);
            context.DeserializeTicket(tokenTracking.Token);
        }
        #pragma warning restore 1998
        #endregion
    }
}
