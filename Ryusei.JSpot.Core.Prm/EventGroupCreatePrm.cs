using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Prm
{
    /// <summary>
    /// Name: EventGroupCreatePrm
    /// Description: Parameter class for Event Group
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-28: Creation
    /// </summary>
    public class EventGroupCreatePrm
    {
        /// <summary>
        /// EventGroup
        /// </summary>
        public EventGroup EventGroup { get; set; }
        /// <summary>
        /// CollectionDepartment
        /// </summary>
        public IEnumerable<Department> CollectionDepartment { get; set; }
    }
}
