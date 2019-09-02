using Ryusei.JSpot.Auth.Attr.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Ryusei.JSpot.Auth.WebBase
{
    /// <summary>
    /// Name: AuthBaseController
    /// Description: Method to define base controlelr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class AuthBaseController : ApiController
    {
        #region [Attributes]
        public const string ERROR_IS_TRIAL = "Herji.Ctrl.Error.IsTrial";
        public const string ERROR_IMPERSONATION = "Herji.Ctrl.Error.Impersonation";
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public AuthBaseController() :base () { }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetUserDataId
        /// Description: Method to get UserDataId of the logged user
        /// </summary>
        /// <returns></returns>
        protected Guid GetUserDataId()
        {
            // Get context of application
            var context = HttpContext.Current.GetOwinContext();
            // Get user data Id
            return Guid.Parse(context.Authentication.User.Claims.First(x => x.Type.Equals(ClaimType.USER_ID)).Value);
        }
        /// <summary>
        /// Name: UserDataEmail
        /// Description: Method to get UserDataEmail
        /// </summary>
        /// <returns></returns>
        protected string GetUserDataEmail()
        {
            // Get context of application
            var context = HttpContext.Current.GetOwinContext();
            // Get Email of user
            return context.Authentication.User.Identity.Name;
        }
        /// <summary>
        /// Name: GetUsername
        /// Description: Method to get username
        /// </summary>
        /// <returns></returns>
        public string GetUsername()
        {
            // Get context of application
            var context = HttpContext.Current.GetOwinContext();
            // Get user data Id
            return context.Authentication.User.Claims.First(x => x.Type.Equals(ClaimType.NAME)).Value;
        }
        /// <summary>
        /// Name: GetLastname
        /// Descriptino: Method to get lastname
        /// </summary>
        /// <returns></returns>
        public string GetLastname()
        {
            // Get context of application
            var context = HttpContext.Current.GetOwinContext();
            // Get user data Id
            return context.Authentication.User.Claims.First(x => x.Type.Equals(ClaimType.LASTNAME)).Value;
        }
        #endregion
    }
}
