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
    /// Name: EventController
    /// Description: Cotnroller to expose the endpoints for EventController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Event")]
    public class EventController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_EVENT = "Jspot.Core.Ctrl.EventCtrl.ErrorInGet";
        public const string ERROR_CREATING_EVENT = "Jspot.Core.Ctrl.EventCtrl.ErrorCreatingEvent";
        #endregion

        #region [Attributes]
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IEventMgr IEventMgr { get; set; }
        /// <summary>
        /// EventWrapper
        /// </summary>
        private EventWrapper EventWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IEventMgr = coreBuilder.GetManager<IEventMgr>(CoreBuilder.IEVENTMGR);

            this.EventWrapper = EventWrapper.GetInstance();

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
        public IEnumerable<Event> GetCurrent()
        {
            try
            {
                return this.IEventMgr.GetByUser(this.GetUserDataId());
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting Event", ERROR_IN_GET_EVENT);
            }
        }
        /// <summary>
        /// Name: GetById
        /// Description: Method to get event by id
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{EventId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public Event GetById(Guid eventId)
        {
            try
            {
                return this.IEventMgr.GetById(eventId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting Event", ERROR_IN_GET_EVENT);
            }
        }
        /// <summary>
        /// Name: Create
        /// Description: Endpoint to create parameter
        /// </summary>
        /// <param name="eventCreatePrm">Event Create Prm</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Create(EventCreatePrm eventCreatePrm)
        {
            try
            {
                this.EventWrapper.Create(this.GetUserDataId(), eventCreatePrm);
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
                throw ExceptionResponse.ThrowException("Error creating event", ERROR_CREATING_EVENT);
            }
        }
        #endregion
    }
}
