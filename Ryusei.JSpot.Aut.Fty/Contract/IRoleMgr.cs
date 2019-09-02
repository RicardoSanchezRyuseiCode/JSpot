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
    public interface IRoleMgr
    {
        /// <summary>
        /// Name: Get
        /// Description: Method to get a list of role
        /// </summary>
        /// <returns>Collection of Role</returns>
        IEnumerable<Role> Get();
        /// <summary>
        /// Name: GetById
        /// Description: Method to get a role by id
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <returns>Role</returns>
        Role GetById(Guid roleId);
        /// <summary>
        /// Name: GetDefaultRole
        /// Description: Method to get a role marked as defualt
        /// </summary>
        /// <returns>Role marked as default</returns>
        Role GetDefaultRole();
    }
}
