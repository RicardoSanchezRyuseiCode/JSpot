using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: EventGroupDepartment
    /// Description: Entity class to model EventGroupDepartment
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class EventGroupDepartment
    {
        /// <summary>
        /// EventGroupId
        /// </summary>
        public Guid EventGroupId { get; set; }
        /// <summary>
        /// DepartmentId
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// EventGroup
        /// </summary>
        public EventGroup EventGroup { get; set; }
        /// <summary>
        /// Department
        /// </summary>
        public Department Department { get; set; }
    }
}
