using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Ent
{
    /// <summary>
    /// Name: Access
    /// Description: Entity class to model Access
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class Access : Permission
    {
        /// <summary>
        /// Controller
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Server
        /// </summary>
        public string Server { get; set; }
    }
}
