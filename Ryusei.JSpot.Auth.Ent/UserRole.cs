using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Ent
{
    /// <summary>
    /// Name: Role
    /// Description: Entity class to model UserRole
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-15: Creation
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// RoleId
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
