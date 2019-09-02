using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty;
using Ryusei.JSpot.Core.Fty.Contract;
using Ryusei.Logger.Wrap;
using Ryusei.Web.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ryusei.JSpot.Core.Wrap; 

namespace Ryusei.JSpot.Core.WebApi.Controllers
{
    /// <summary>
    /// Name: PassengerController
    /// Description: Cotnroller to expose the endpoints for PassengerController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Passenger")]
    public class PassengerController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_PASSENGER = "Jspot.Core.Ctrl.PassengerCtrl.ErrorInGet";

        public const string ERROR_CREATING_PASSENGER = "Jspot.Core.Ctrl.PassengerCtrl.ErrorCreation";
        #endregion

        #region [Attributes]
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IPassengerMgr IPassengerMgr { get; set; }
        /// <summary>
        /// ParticipanWrapper
        /// </summary>
        private PassengerWrapper PassengerWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PassengerController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IPassengerMgr = coreBuilder.GetManager<IPassengerMgr>(CoreBuilder.IPASSENGERMGR);

            this.PassengerWrapper = PassengerWrapper.GetInstance();

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
        [Route("GetByTransportId/{transportId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<Passenger> GetByEventGroupId(Guid transportId)
        {
            try
            {
                return this.IPassengerMgr.GetByTransportId(transportId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting Passenger", ERROR_IN_GET_PASSENGER);
            }
        }
        /// <summary>
        /// Name: 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="travelSense"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByUserIdEventIdSense/{userId:guid}/{eventId:guid}/{travelSense:bool}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public Passenger GetByUserIdEventIdSense(Guid userId, Guid eventId, bool travelSense)
        {
            try
            {
                return this.IPassengerMgr.GetByUserIdEventIdSense(userId, eventId, travelSense);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting Passenger", ERROR_IN_GET_PASSENGER);
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
        public IHttpActionResult Create(Passenger passenger)
        {
            try
            {
                this.PassengerWrapper.Create(passenger, this.GetUserDataEmail(), this.GetUsername(), this.GetLastname());
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
                throw ExceptionResponse.ThrowException("Error creating passenger", ERROR_CREATING_PASSENGER);
            }
        }
        #endregion
    }
}
