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
    /// Name: RoleMenuItemMgr
    /// Description: Manager class to implement the behavior of RoleMenuItem
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class RoleMenuItemMgr : IRoleMenuItemMgr
    {
        #region [Constants]
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static RoleMenuItemMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private RoleMenuItemDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static RoleMenuItemMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private RoleMenuItemMgr()
        {
            this.DAO = new RoleMenuItemDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static RoleMenuItemMgr GetInstance()
        {
            return Singleton ?? (Singleton = new RoleMenuItemMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByIds
        /// Description: Method to get by Ids
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <param name="menuItemId">MenuItemId</param>
        /// <returns>RoleMenuItem</returns>
        public RoleMenuItem GetByIds(Guid roleId, Guid menuItemId)
        {
            // Define filter
            string filter = "RMI.RoleId = @RoleId and RMI.MenuItemId = @MenuItemId and R.Active = @Active and MI.Active = @Active";
            // Define params
            object @params = new { RoleId = roleId, MenuItemId = menuItemId, Active = true };
            // Get results
            IEnumerable<RoleMenuItem> results = this.DAO.Select(filter: filter, @params: @params);
            // return 
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: GetByRoleIds
        /// Description: Method to get a list of menuItems by a collection of Role Ids
        /// </summary>
        /// <param name="listRoleId">Collection of RolesIdss</param>
        /// <returns>Collection of menuItem</returns>
        public IEnumerable<MenuItem> GetByRoleIds(IEnumerable<Guid> listRoleId)
        {
            // Define top
            string top = "DISTINCT";
            // Create filter
            string filter = string.Format("RMI.RoleId in ({0}) and MI.Active = @Active", string.Join(",", listRoleId.Select(x => string.Format("'{0}'", x.ToString())).ToArray()));
            // Define parameters
            object @params = new { Active = true };
            // return the results
            return this.DAO.SelectMenuItem(top: top, filter: filter, @params: @params);
        }
        /// <summary>
        /// Name: GetByMenuItemId
        /// Description: Method to get a collection of role by menuItemId
        /// </summary>
        /// <param name="menuItemId">MenuItemId</param>
        /// <returns>Collection of Role</returns>
        public IEnumerable<Role> GetByMenuItemId(Guid menuItemId)
        {
            // Create filter
            string filter = "RMI.MenuItemId = @MenuItemId and R.Active = @Active";
            // Define parameters
            object @params = new { Active = true };
            // return the results
            return this.DAO.SelectRole(filter: filter, @params: @params);
        }
        #endregion
    }
}
