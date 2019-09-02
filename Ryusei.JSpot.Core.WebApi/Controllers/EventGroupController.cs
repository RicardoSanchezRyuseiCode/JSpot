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

namespace Ryusei.JSpot.Core.WebApi.Controllers
{
    /// <summary>
    /// Name: EventGroupController
    /// Description: Cotnroller to expose the endpoints for EventGroupController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/EventGroup")]
    public class EventGroupController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_EVENT_GROUP = "Jspot.Core.Ctrl.EventGroupCtrl.ErrorInGet";
        #endregion

        #region [Attributes]
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IEventGroupMgr IEventGroupMgr { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventGroupController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IEventGroupMgr = coreBuilder.GetManager<IEventGroupMgr>(CoreBuilder.IEVENTGROUPMGR);

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
        [Route("GetByEventId/{eventId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<EventGroup> GetByEventId(Guid eventId)
        {
            try
            {
                return this.IEventGroupMgr.GetByEventId(eventId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting EventGroup", ERROR_IN_GET_EVENT_GROUP);
            }
        }
        /// <summary>
        /// Name: GetById
        /// Description: Endpoint to get EventGroup By id
        /// </summary>
        /// <param name="eventGroupId">EventGroupId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{eventGroupId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public EventGroup GetById(Guid eventGroupId)
        {
            try
            {
                return this.IEventGroupMgr.GetById(eventGroupId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting EventGroup", ERROR_IN_GET_EVENT_GROUP);
            }
        }
        /// <summary>
        /// Name: GetByUserId
        /// Description: Method to get EventGroup by userId
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns>Collection EventGroup</returns>
        [HttpGet]
        [Route("GetByUserIdEventId/{userId:guid}/{eventId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<EventGroup> GetByUserId(Guid userId, Guid eventId)
        {
            try
            {
                return this.IEventGroupMgr.GetByUserIdEventId(userId, eventId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting EventGroup", ERROR_IN_GET_EVENT_GROUP);
            }
        }
        #endregion
    }
}
