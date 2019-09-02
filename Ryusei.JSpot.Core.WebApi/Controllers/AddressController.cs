using Ryusei.JSpot.Auth.WebBase;
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
    /// Name: AddressController
    /// Description: Cotnroller to expose the endpoints for Address
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Address")]
    public class AddressController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_ADDRESS = "Jspot.Core.Ctrl.AddressCtrl.ErrorInGet";
        #endregion

        #region [Attributes]
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IAddressMgr IAddressMgr { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AddressController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IAddressMgr = coreBuilder.GetManager<IAddressMgr>(CoreBuilder.IADDRESSMGR);

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
        public Address GetById(Guid eventId)
        {
            try
            {
                return this.IAddressMgr.GetByEventId(eventId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting Address", ERROR_IN_GET_ADDRESS);
            }
        }

        #endregion
    }
}
