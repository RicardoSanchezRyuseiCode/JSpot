using Ryusei.JSpot.Auth.Fty;
using Ryusei.JSpot.Auth.Fty.Contract;
using Ryusei.JSpot.Auth.Prm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ryusei.JSpot.Auth.WebApi.Controllers
{
    /// <summary>
    /// Name: PermissionController
    /// Description: Controller to expose endpoint of permission
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-19: Creation
    /// </summary>
    [RoutePrefix("api/Permission")]
    public class PermissionController : ApiController
    {
        #region [Attributes]
        /// <summary>
        /// PermissionMgr
        /// </summary>
        private IPermissionMgr IPermissionMgr { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constroller
        /// </summary>
        public PermissionController()
        {
            AuthBuilder builder = AuthBuilder.GetInstance();
            this.IPermissionMgr = builder.GetManager<IPermissionMgr>(AuthBuilder.IPERMISSIONMGR);
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: HaveAccess
        /// Description: Method to check permissions of user for a resource
        /// </summary>
        /// <param name="permisionPrm">PermissionParams</param>
        /// <returns>true or false</returns>
        [HttpPost]
        [Route("HaveAccess")]
        [AllowAnonymous]
        public bool HaveAccess(PermissionPrm permissionPrm)
        {
            try
            {
                return this.IPermissionMgr.HaveAccess(permissionPrm.UserDataId, permissionPrm.Action, permissionPrm.Controller, permissionPrm.Server);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
