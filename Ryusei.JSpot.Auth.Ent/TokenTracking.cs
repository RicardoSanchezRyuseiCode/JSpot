using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Ent
{
    /// <summary>
    /// Name: TokenTracking
    /// Description: Entity class to model TokenTracking
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-15: Creation
    /// </summary>
    public class TokenTracking
    {
        /// <summary>
        /// TokenId
        /// </summary>
        public Guid TokenTrackingId { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// RequestedDate
        /// </summary>
        public DateTime RequestedDate { get; set; }
    }
}
