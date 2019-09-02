using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Ent
{
    /// <summary>
    /// Name: Role
    /// Description: Entity class to model Role
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-15: Creation
    /// </summary>
    public class Role
    {
        /// <summary>
        /// RoleId
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// IsDefault
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}
