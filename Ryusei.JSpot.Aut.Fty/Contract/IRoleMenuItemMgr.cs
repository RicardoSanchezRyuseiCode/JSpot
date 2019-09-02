using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Contract
{
    /// <summary>
    /// Name: IRoleMgr
    /// Description: Interface to define the behavior of RoleMgr
    /// Author: Ricardo Sanchez Romero 
    /// LogBook:
    ///     2019/08/16: Creation
    /// </summary>
    public interface IRoleMenuItemMgr
    {
        /// <summary>
        /// Name: GetByRoleIds
        /// Description: Method to get a list of menuItems by a collection of Role Ids
        /// </summary>
        /// <param name="listRoleId">Collection of RolesIdss</param>
        /// <returns>Collection of menuItem</returns>
        IEnumerable<MenuItem> GetByRoleIds(IEnumerable<Guid> listRoleId);
        /// <summary>
        /// Name: GetByMenuItemId
        /// Description: Method to get a collection of role by menuItemId
        /// </summary>
        /// <param name="menuItemId">MenuItemId</param>
        /// <returns>Collection of Role</returns>
        IEnumerable<Role> GetByMenuItemId(Guid menuItemId);
    }
}
