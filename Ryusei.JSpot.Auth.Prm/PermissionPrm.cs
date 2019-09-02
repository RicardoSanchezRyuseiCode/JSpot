using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Prm
{
    /// <summary>
    /// Name: PermissionPrm
    /// Description: Parameter class to define Permission
    /// </summary>
    public class PermissionPrm
    {
        /// <summary>
        /// UserDataId
        /// </summary>
        public Guid UserDataId { get; set; }
        /// <summary>
        /// ApplicationId
        /// </summary>
        public Guid ApplicationId { get; set; }
        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; set; }
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
