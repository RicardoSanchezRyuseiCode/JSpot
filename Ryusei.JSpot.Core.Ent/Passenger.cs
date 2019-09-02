using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: Passenger
    /// Description: Entity class to model Passenger
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// TransportId
        /// </summary>
        public Guid TransportId { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Transport
        /// </summary>
        public Transport Transport { get; set; }
    }
}
