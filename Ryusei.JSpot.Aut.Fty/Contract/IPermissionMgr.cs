using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Contract
{
    /// <summary>
    /// Name: IPermissionMgr
    /// Description: Interface to define the behavior of PermissionMgr
    /// Author: Ricardo Sanchez Romero 
    /// LogBook:
    ///     2019/08/16: Creation
    /// </summary>
    public interface IPermissionMgr
    {
        /// <summary>
        /// Name: GetByApplicationId
        /// Description: Method to get a collection of Permissions by ApplicationId
        /// </summary>
        /// <returns>Collection of Permissions</returns>
        IEnumerable<Permission> Get();
        /// <summary>
        /// Name: HaveAccess
        /// Description: Method to check if a user have acces to a specific resource
        /// </summary>
        /// <param name="userDataId">UserDataId</param>
        /// <param name="actionName">ActionName</param>
        /// <param name="controllerName">ControllerName</param>
        /// <param name="severName">ServerName</param>
        /// <returns></returns>
        bool HaveAccess(Guid userDataId, string actionName, string controllerName, string severName);
    }
}
