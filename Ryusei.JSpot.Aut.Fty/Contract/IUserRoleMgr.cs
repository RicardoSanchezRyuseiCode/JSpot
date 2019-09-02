using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Contract
{
    /// <summary>
    /// Name: IUserRoleMgr
    /// Description: Interface to define the behavior of UserRoleMgr
    /// Author: Ricardo Sanchez Romero 
    /// LogBook:
    ///     2019/08/16: Creation
    /// </summary>
    public interface IUserRoleMgr
    {
        /// <summary>
        /// Name: GetRoleByUserDataId
        /// Description: Method to get collection Role by UserData id
        /// </summary>
        /// <param name="userDataId">UserDataId</param>
        /// <returns>Role</returns>
        IEnumerable<Role> GetRoleByUserId(Guid userDataId);
        /// <summary>
        /// Name: GetUserByRoleId
        /// Description: Method to get a collection of UserData by RoleId
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <returns>Collection of UserData</returns>
        IEnumerable<User> GetUserByRoleId(Guid roleId);
        /// <summary>
        /// Name: Save
        /// Description: Method to update userDataRole
        /// </summary>
        /// <param name="userRole">UserDataRole</param>
        void Save(IEnumerable<UserRole> listUserRole);
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete a list of userDataRole
        /// </summary>
        /// <param name="listUserRole">Collection of userDataRole</param>
        void Delete(IEnumerable<UserRole> listUserRole);
    }
}
