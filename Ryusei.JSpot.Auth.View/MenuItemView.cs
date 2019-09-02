using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.View
{
    /// <summary>
    /// Name: MenuItemView
    /// Description: View class for MenuItem
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class MenuItemView
    {
        /// <summary>
        /// MenuItem
        /// </summary>
        public MenuItem MenuItem { get; set; }
        /// <summary>
        /// ChildMenuItem
        /// </summary>
        public IEnumerable<MenuItemView> ListMenuItem { get; set; }
    }
}
