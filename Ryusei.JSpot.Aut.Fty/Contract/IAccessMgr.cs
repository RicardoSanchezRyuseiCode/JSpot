using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Contract
{
    /// <summary>
    /// Name: IAccessMgr
    /// Description: Interface to define the behavior of AccessMgr
    /// Author: Ricardo Sanchez Romero 
    /// LogBook:
    ///     2019/08/16: Creation
    /// </summary>
    public interface IAccessMgr
    {
        /// <summary>
        /// Name: HaveAccess
        /// Description: Method to get and Access object by roleid and permission action
        /// </summary>
        /// <param name="listRoleId">ListRoleId</param>
        /// <param name="actionName">ActionName</param>
        /// <param name="controllerName">ControllerName</param>
        /// <param name="serverName">ServerName</param>
        /// <returns></returns>
        bool HaveAccess(IEnumerable<Guid> listRoleId, string actionName, string controllerName, string serverName);
    }
}
