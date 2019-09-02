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
    /// Name: AssistantController
    /// Description: Cotnroller to expose the endpoints for AssistantController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Assistant")]
    public class AssistantController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_ASSISTANT = "Jspot.Core.Ctrl.AssistantCtrl.ErrorInGet";

        public const string ERROR_UPDATING_ASSISTANT = "Jspot.Core.Ctrl.AssistantCtrl.ErrorUpdating";

        #endregion

        #region [Attributes]
        /// <summary>
        /// IAssistantMgr
        /// </summary>
        private IAssistantMgr IAssistantMgr { get; set; }
        /// <summary>
        /// AssistantWrapper
        /// </summary>
        private AssistantWrapper AssistantWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public AssistantController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IAssistantMgr = coreBuilder.GetManager<IAssistantMgr>(CoreBuilder.IASSISTANTMGR);

            this.AssistantWrapper = AssistantWrapper.GetInstance();

            this.SystemLogWrapper = SystemLogWrapper.GetInstance();
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEventId
        /// Description: Endpoint to get a collection of Assistant
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns>Collection Assistants</returns>
        [HttpGet]
        [Route("GetByEventId/{eventId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<Assistant> GetByEventId(Guid eventId)
        {
            try
            {
                return this.IAssistantMgr.GetByEventId(eventId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting Assistant", ERROR_IN_GET_ASSISTANT);
            }
        }
        /// <summary>
        /// Name: Update
        /// Description: Endpoint to update assistant
        /// </summary>
        /// <param name="assistant">Assistant</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Update(Assistant assistant)
        {
            try
            {
                this.AssistantWrapper.UpdateOwner(assistant);
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
                throw ExceptionResponse.ThrowException("Error updating assistant", ERROR_UPDATING_ASSISTANT);
            }
        }
        #endregion
    }
}
