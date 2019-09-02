using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Ent
{
    /// <summary>
    /// Name: Permission
    /// Description: Entity class to model Permission
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-15: Creation
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// PermissionId
        /// </summary>
        public Guid PermissionId { get; set; }
        /// <summary>
        /// UpPermissionId
        /// </summary>
        public Guid UpPermissionId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
        
    }
}
