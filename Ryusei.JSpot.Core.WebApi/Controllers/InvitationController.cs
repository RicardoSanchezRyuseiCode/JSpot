using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty;
using Ryusei.JSpot.Core.Fty.Contract;
using Ryusei.JSpot.Core.Prm;
using Ryusei.JSpot.Core.Wrap;
using Ryusei.Logger.Wrap;
using Ryusei.Web.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ryusei.JSpot.Core.WebApi.Controllers
{
    /// <summary>
    /// Name: InvitationController
    /// Description: Cotnroller to expose the endpoints for InvitationController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Invitation")]
    public class InvitationController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_INVITATION = "Jspot.Core.Ctrl.InvitationCtrl.ErrorInGet";
        public const string ERROR_ACCEPTING_INVITATION = "Jspot.Core.Ctrl.InvitationCtrl.ErrorAcceptingInvitation";
        public const string ERROR_REJECTING_INVITATION = "Jspot.Core.Ctrl.InvitationCtrl.ErrorRejectingInvitation";
        #endregion

        #region [Attributes]
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IInvitationMgr IInvitationMgr { get; set; }
        /// <summary>
        /// InvitationWrapper
        /// </summary>
        private InvitationWrapper InvitationWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InvitationController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IInvitationMgr = coreBuilder.GetManager<IInvitationMgr>(CoreBuilder.IINVITATIONMGR);

            this.InvitationWrapper = InvitationWrapper.GetInstance();

            SystemLogWrapper = SystemLogWrapper.GetInstance();
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetCurrent
        /// Description: Endpoint to get list of current events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrent")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<Invitation> GetCurrent()
        {
            try
            {
                return this.IInvitationMgr.GetByEmail(this.GetUserDataEmail());
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting invitations", ERROR_IN_GET_INVITATION);
            }
        }
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of Invitation by Id
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByEventId/{eventId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<Invitation> GetByEventId(Guid eventId)
        {
            try
            {
                return this.IInvitationMgr.GetByEventId(eventId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting invitations", ERROR_IN_GET_INVITATION);
            }
        }
        /// <summary>
        /// Name: Accept
        /// Description: Endpoint to accept invitation
        /// </summary>
        /// <param name="invitationId">InvitationId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Accept/{invitationId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Accept([FromUri]Guid invitationId)
        {
            try
            {
                // Update user
                this.InvitationWrapper.Accept(invitationId, this.GetUserDataId());
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, mex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, wex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error accepting invitation", ERROR_ACCEPTING_INVITATION);
            }
        }
        /// <summary>
        /// Name: Reject
        /// Description: Method to reject invitation
        /// </summary>
        /// <param name="invitationId">InvitationId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Reject/{invitationId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Reject([FromUri]Guid invitationId)
        {
            try
            {
                // Update user
                this.InvitationWrapper.Reject(invitationId, this.GetUsername(), this.GetLastname(), this.GetUserDataEmail());
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, mex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, wex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error accepting invitation", ERROR_REJECTING_INVITATION);
            }
        }
        /// <summary>
        /// Name: Import
        /// Description: Method to import a list of email to send invitation
        /// </summary>
        /// <param name="emails">list of emails</param>
        /// <param name="eventId">eventid</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Import")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Import(InvitationImportPrm invitationImportPrm)
        {
            try
            {
                // Update user
                this.InvitationWrapper.Import(invitationImportPrm.CollectionEmail, invitationImportPrm.EventId);
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, mex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, wex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error accepting invitation", ERROR_REJECTING_INVITATION);
            }
        }
        #endregion
    }
}
