using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty;
using Ryusei.JSpot.Core.Fty.Contract;
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
    /// Name: ParticipantController
    /// Description: Cotnroller to expose the endpoints for ParticipantController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Participant")]
    public class ParticipantController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_PARTICIPANT = "Jspot.Core.Ctrl.ParticipantCtrl.ErrorInGet";

        public const string ERROR_CREATING_PARTICIPANT = "Jspot.Core.Ctrl.ParticipantCtrl.ErrorCreation";
        #endregion

        #region [Attributes]
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IParticipantMgr IParticipantMgr { get; set; }
        /// <summary>
        /// ParticipanWrapper
        /// </summary>
        private ParticipanWrapper ParticipanWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ParticipantController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IParticipantMgr = coreBuilder.GetManager<IParticipantMgr>(CoreBuilder.IPARTICIPANTMGR);

            this.ParticipanWrapper = ParticipanWrapper.GetInstance();

            SystemLogWrapper = SystemLogWrapper.GetInstance();
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEventId
        /// Description: Endpoint to get Address by EventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByEventGroupId/{eventGroupId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<Participant> GetByEventGroupId(Guid eventGroupId)
        {
            try
            {
                return this.IParticipantMgr.GetByEventGroupId(eventGroupId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting EventGroup", ERROR_IN_GET_PARTICIPANT);
            }
        }
        /// <summary>
        /// Name: Create
        /// Description: Endpoint to create  participan
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Create(Participant participant)
        {
            try
            {
                this.ParticipanWrapper.Create(participant, this.GetUserDataEmail(), this.GetUsername(), this.GetLastname());
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
                throw ExceptionResponse.ThrowException("Error creating participant", ERROR_CREATING_PARTICIPANT);
            }
        }

        #endregion
    }
}
