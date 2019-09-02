using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Ent
{
    /// <summary>
    /// Name: RolePermission
    /// Description: Entity class to model RoleMenuItem
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-15: Creation
    /// </summary>
    public class RoleMenuItem
    {
        /// <summary>
        /// RoleId
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// PermissionId
        /// </summary>
        public Guid PermissionId { get; set; }
    }
}
