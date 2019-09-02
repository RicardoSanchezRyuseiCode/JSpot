using Ryusei.JSpot.Auth.Fty.Contract;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Mgr.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Mgr
{
    /// <summary>
    /// Name: PermissionMgr
    /// Description: Manager class to implement the behavior of Permission
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class PermissionMgr : IPermissionMgr
    {
        #region [Constants]
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static PermissionMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private PermissionDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static PermissionMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private PermissionMgr()
        {
            this.DAO = new PermissionDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static PermissionMgr GetInstance()
        {
            return Singleton ?? (Singleton = new PermissionMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByApplicationId
        /// Description: Method to get a collection of Permissions by ApplicationId
        /// </summary>
        
        /// <returns>Collection of Permissions</returns>
        public IEnumerable<Permission> Get()
        {
            // Define filter
            string filter = "Active = @Active";
            // Define parameters
            object @params = new { Active = true };
            // Return the results
            return this.DAO.Select(filter: filter, @params: @params);
        }
        /// <summary>
        /// Name: HaveAccess
        /// Description: Method to check if a user have acces to a specific resource
        /// </summary>
        /// <param name="userDataId">UserDataId</param>
        /// <param name="actionName">ActionName</param>
        /// <param name="controllerName">ControllerName</param>
        /// <param name="severName">ServerName</param>
        /// <returns></returns>
        public bool HaveAccess(Guid userDataId, string actionName, string controllerName, string serverName)
        {

            //System.IO.File.WriteAllText(
            //                     HostingEnvironment.MapPath(System.IO.Path.Combine("~/App_Data/Files/test_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt")),
            //                     string.Format("{0} - {1} - {2} - {3} - {4}", userDataId, applicationId, actionName, controllerName, serverName));
            return this.DAO.HaveAccess(userDataId, actionName, controllerName, serverName) > 0;
        }
        #endregion
    }
}
