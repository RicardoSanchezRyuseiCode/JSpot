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
    /// Name: DepartmentController
    /// Description: Cotnroller to expose the endpoints for DepartmentController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/UserDepartment")]
    public class UserDepartmentController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_USERDEPARTMENT = "Jspot.Core.Ctrl.UserDepartmentCtrl.ErrorInGet";
        public const string ERROR_CREATING_USER_DEPARTMENT = "Jspot.Core.Ctrl.UserDepartmentCtrl.ErrorCreatingUserDepartment";
        #endregion

        #region [Attributes]
        /// <summary>
        /// DepartmentMgr
        /// </summary>
        private IUserDepartmentMgr IUserDepartmentMgr { get; set; }
        /// <summary>
        /// UserDepartmentWrapper
        /// </summary>
        private UserDepartmentWrapper UserDepartmentWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserDepartmentController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IUserDepartmentMgr = coreBuilder.GetManager<IUserDepartmentMgr>(CoreBuilder.IUSERDEPARTMENTMGR);

            this.UserDepartmentWrapper = UserDepartmentWrapper.GetInstance();
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
        [Route("GetByUserIdEventId/{userId:guid}/{eventId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<UserDepartment> GetByIds(Guid userId, Guid eventId)
        {
            try
            {
                return this.IUserDepartmentMgr.GetByUserIdEventId(userId, eventId);
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting User Department", ERROR_IN_GET_USERDEPARTMENT);
            }
        }
        /// <summary>
        /// Name: Create
        /// Description Endpoint to create collection of UserDepartment
        /// </summary>
        /// <param name="userDeppartmentCreatePrm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Create(UserDepartmentCreatePrm userDepartmentCreatePrm)
        {
            try
            {
                this.UserDepartmentWrapper.Create(userDepartmentCreatePrm);
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
                throw ExceptionResponse.ThrowException("Error creating user department", ERROR_CREATING_USER_DEPARTMENT);
            }
        }
        #endregion
    }
}
