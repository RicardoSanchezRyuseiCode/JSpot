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
    /// Name: RolePermissionMgr
    /// Description: Manager class to implement the behavior of RolePermission
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class RolePermissionMgr : IRolePermissionMgr
    {
        #region [Constants]
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static RolePermissionMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private RolePermissionDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static RolePermissionMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private RolePermissionMgr()
        {
            this.DAO = new RolePermissionDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static RolePermissionMgr GetInstance()
        {
            return Singleton ?? (Singleton = new RolePermissionMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByIds
        /// Descrpition: Method to get a RolePermision by ids
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <param name="permissionId">PermissionId</param>
        /// <returns></returns>
        private RolePermission GetByIds(Guid roleId, Guid permissionId)
        {
            // Define filter 
            string filter = "RP.RoleId = @RoleId and RP.PermissionId = @PermissionId and R.Active = @Active and P.Active = @Active";
            // Define parameters
            object @params = new { RoleId = roleId, PermissionId = permissionId, Active = true };
            // Get the results
            IEnumerable<RolePermission> results = this.DAO.Select(filter: filter, @params: @params);
            // return 
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: GetByRoleId
        /// Description: Method to get Permissions By RoleId
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <returns>CollectionOfPermissions</returns>
        public IEnumerable<Permission> GetByRoleId(Guid roleId)
        {
            // Define filter 
            string filter = "RP.RoleId = @RoleId and P.Active = @Active";
            // Define parameters
            object @params = new { RoleId = roleId, Active = true };
            // Get the results
            return this.DAO.SelectPermission(filter: filter, @params: @params);
        }
        /// <summary>
        /// Name: GetByPermissionId
        /// Description:Method to get Roles by PermissionId
        /// </summary>
        /// <param name="permissionId">PermissionId</param>
        /// <returns>CollectionOfRoles</returns>
        public IEnumerable<Role> GetByPermissionId(Guid permissionId)
        {
            // Define filter 
            string filter = "RP.PermissionId = @PermissionId and P.Active = @Active";
            // Define parameters
            object @params = new { PermissionId = permissionId, Active = true };
            // Get the results
            return this.DAO.SelectRole(filter: filter, @params: @params);
        }
        #endregion
    }
}
