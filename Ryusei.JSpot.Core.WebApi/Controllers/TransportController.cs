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
    /// Name: TransportController
    /// Description: Cotnroller to expose the endpoints for TransportController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Transport")]
    public class TransportController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_TRANSPORT = "Jspot.Core.Ctrl.TransportCtrl.ErrorInGet";

        public const string ERROR_CREATING_TRANSPORT = "Jspot.Core.Ctrl.TransportCtrl.ErrorCreation";
        #endregion


        #region [Attributes]
        /// <summary>
        /// ITransportMgr
        /// </summary>
        private ITransportMgr ITransportMgr { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        /// <summary>
        /// TransportWrapper
        /// </summary>
        private TransportWrapper TransportWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public TransportController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.ITransportMgr = coreBuilder.GetManager<ITransportMgr>(CoreBuilder.ITRANSPORTMGR);

            this.TransportWrapper = TransportWrapper.GetInstance();

            this.SystemLogWrapper = SystemLogWrapper.GetInstance();

        }
        #endregion

        #region [Methods]

        /// <summary>
        /// Name: GetByEventId
        /// Description: Endport to get collection of transport by id
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns>Collection Transport</returns>
        [HttpGet]
        [Route("GetByEventId/{eventId:guid}/{travelSense:bool}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<Transport> GetByEventId(Guid eventId, bool travelSense)
        {
            try
            {
                return this.ITransportMgr.GetByEventId(eventId, travelSense);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting transport", ERROR_IN_GET_TRANSPORT);
            }
        }
        /// <summary>
        /// Name: GetById
        /// Description: Endpoint to get a transport by id
        /// </summary>
        /// <param name="transportId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{transportId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public Transport GetById(Guid transportId)
        {
            try
            {
                return this.ITransportMgr.GetById(transportId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting transport", ERROR_IN_GET_TRANSPORT);
            }
        }
        /// <summary>
        /// Name: Create
        /// Description: Endpoint to create transport
        /// </summary>
        /// <param name="transport">Transports</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Create(Transport transport)
        {
            try
            {
                this.TransportWrapper.Create(transport, this.GetUserDataId());
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
                throw ExceptionResponse.ThrowException("Error creating transport", ERROR_CREATING_TRANSPORT);
            }
        }
        #endregion
    }
}
