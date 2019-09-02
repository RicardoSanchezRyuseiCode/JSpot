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
    /// Name: AccessMgr
    /// Description: Manager class to implement the behavior of Access
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class AccessMgr : IAccessMgr  
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static AccessMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private AccessDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static AccessMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private AccessMgr()
        {
            this.DAO = new AccessDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static AccessMgr GetInstance()
        {
            return Singleton ?? (Singleton = new AccessMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: HaveAccess
        /// Description: Method to get and Access object by roleid and permission action
        /// </summary>
        /// <param name="listRoleId">ListRoleId</param>
        /// <param name="actionName">ActionName</param>
        /// <param name="controllerName">ControllerName</param>
        /// <param name="serverName">ServerName</param>
        /// <returns></returns>
        public bool HaveAccess(IEnumerable<Guid> listRoleId, string actionName, string controllerName, string serverName)
        {
            // Define filter
            string filter = string.Format("RP.RoleId in ({0}) and LOWER(Action.Name) = LOWER(@ActionName) and LOWER(Controller.Name) = LOWER(@ControllerName) and  LOWER(Server.Name) = LOWER(@ServerName)", string.Join(",", listRoleId.Select(x => string.Format("'{0}'", x.ToString()).ToArray())));
            // Define params
            object @params = new { ActionName = actionName, ControllerName = controllerName, ServerName = serverName };
            // Get the results
            IEnumerable<Access> results = this.DAO.Select(filter: filter, @params: @params);
            // return the result
            return results.Count() > 0;
        }
        #endregion
    }
}
