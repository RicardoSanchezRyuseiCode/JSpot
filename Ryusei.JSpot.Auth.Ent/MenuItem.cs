using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Ent
{
    /// <summary>
    /// Name: Permission
    /// Description: Entity class to model MenuItem
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-15: Creation
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// MenuItemId
        /// </summary>
        public Guid MenuItemId { get; set; }
        /// <summary>
        /// UpMenuItemId
        /// </summary>
        public Guid? UpMenuItemId { get; set; }
        /// <summary>
        /// MainName
        /// </summary>
        public string MainName { get; set; }
        /// <summary>
        /// AltName
        /// </summary>
        public string AltName { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// IsInitial
        /// </summary>
        public bool IsInitial { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}
