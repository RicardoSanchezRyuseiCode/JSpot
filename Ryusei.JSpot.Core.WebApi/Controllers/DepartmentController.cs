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
    /// Name: DepartmentController
    /// Description: Cotnroller to expose the endpoints for DepartmentController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Department")]
    public class DepartmentController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {

        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_DEPARTMENT = "Jspot.Core.Ctrl.DepartmentCtrl.ErrorInGet";
        #endregion

        #region [Attributes]
        /// <summary>
        /// DepartmentMgr
        /// </summary>
        private IDepartmentMgr IDepartmentMgr { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IDepartmentMgr = coreBuilder.GetManager<IDepartmentMgr>(CoreBuilder.IDEPARTMENTMGR);
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEventId
        /// Description: Endpoint to get a collection of Department
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByEventId/{eventId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<Department> GetByEventId(Guid eventId)
        {
            try
            {
                return this.IDepartmentMgr.GetByEventId(eventId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting Department", ERROR_IN_GET_DEPARTMENT);
            }
        }
        #endregion
    }
}
