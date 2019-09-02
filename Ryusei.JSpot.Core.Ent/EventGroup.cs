using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: EventGroup
    /// Description: Entity class to model EventGroup
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class EventGroup
    {
        /// <summary>
        /// EventGroupId
        /// </summary>
        public Guid EventGroupId { get; set; }
        /// <summary>
        /// EventId
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Capacity
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}
