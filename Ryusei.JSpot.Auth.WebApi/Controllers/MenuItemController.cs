using Ryusei.JSpot.Auth.View;
using Ryusei.JSpot.Auth.Wrap;
using Ryusei.Logger.Wrap;
using Ryusei.Web.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ryusei.JSpot.Auth.WebApi.Controllers
{
    /// <summary>
    /// Name: MenuItemController
    /// Description: Controller to expose the endpoints of MenuItemController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    [RoutePrefix("api/MenuItem")]
    public class MenuItemController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string ERROR_IN_GET = "Ryusei.Auth.Ctrl.MenuItemCtrl.ErrorInGet";
        #endregion

        #region [Attributes]
        /// <summary>
        /// MenuItemWrapper
        /// </summary>
        private MenuItemWrapper MenuItemWrapper { get; set; }
        /// <summary>
        /// LogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Name: MenuItemController
        /// Description: Default constructor
        /// </summary>
        public MenuItemController()
        {
            this.MenuItemWrapper = MenuItemWrapper.GetInstance();
            // Log reference
            this.SystemLogWrapper = SystemLogWrapper.GetInstance();
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetUserMenu
        /// Description: Web service to get MenuItems for user
        /// </summary>
        /// <param name="userDataApplicationPrm">UserDataApplicationPrm</param>
        /// <returns>Collection of MenuItemView</returns>
        [HttpGet]
        [Route("GetUserMenu")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = "Auth Server")]
        public IEnumerable<MenuItemView> GetUserMenu()
        {
            try
            {
                return this.MenuItemWrapper.GetUserMenu(this.GetUserDataEmail());
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register("Auth Server", this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting MenuItem", ERROR_IN_GET);
            }
        }
        #endregion
    }
}
