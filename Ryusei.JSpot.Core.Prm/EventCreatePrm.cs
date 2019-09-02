using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Prm
{
    /// <summary>
    /// Name: EventPrm
    /// Description: Parameter class for event
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-28: Creation
    /// </summary>
    public class EventCreatePrm
    {
        /// <summary>
        /// Event
        /// </summary>
        public Event Event { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// Department
        /// </summary>
        public IEnumerable<Department> CollectionDepartment { get; set; }
        /// <summary>
        /// EventGroup
        /// </summary>
        public IEnumerable<EventGroupCreatePrm> CollectionEventGroupCreatePrm { get; set; }
    }
}
