using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Contract
{
    /// <summary>
    /// Name: IMenuItemMgr
    /// Description: Interface to define the behavior of MenuMgr
    /// Author: Ricardo Sanchez Romero 
    /// LogBook:
    ///     2019/08/16: Creation
    /// </summary>
    public interface IMenuItemMgr
    {
        /// <summary>
        /// Name: Get
        /// Description: Method to get a collection of elements by applicationId
        /// </summary>
        /// <returns>Collection of MenuItem</returns>
        IEnumerable<MenuItem> Get();
    }
}
