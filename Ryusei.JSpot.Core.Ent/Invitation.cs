using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: Invitation
    /// Description: Entity class to model Invitation
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class Invitation
    {
        /// <summary>
        /// InvitationId
        /// </summary>
        public Guid InvitationId { get; set; }
        /// <summary>
        /// EventId
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// SendDate
        /// </summary>
        public DateTime SendDate { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Answer { get; set; }
        /// <summary>
        /// ResponseDate
        /// </summary>
        public DateTime? ResponseDate { get; set; }
        /// <summary>
        /// Event
        /// </summary>
        public Event Event { get; set; }
    }
}
