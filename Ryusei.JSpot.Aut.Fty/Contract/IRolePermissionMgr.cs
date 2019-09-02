using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Contract
{
    /// <summary>
    /// Name: IRolePermissionMgr
    /// Description: Interface to define the behavior of RolePermissionMgr
    /// Author: Ricardo Sanchez Romero 
    /// LogBook:
    ///     2019/08/16: Creation
    /// </summary>
    public interface IRolePermissionMgr
    {
        /// <summary>
        /// Name: GetByRoleId
        /// Description: Method to get Permissions By RoleId
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <returns>CollectionOfPermissions</returns>
        IEnumerable<Permission> GetByRoleId(Guid roleId);
        /// <summary>
        /// Name: GetByPermissionId
        /// Description:Method to get Roles by PermissionId
        /// </summary>
        /// <param name="permissionId">PermissionId</param>
        /// <returns>CollectionOfRoles</returns>
        IEnumerable<Role> GetByPermissionId(Guid permissionId);
    }
}
