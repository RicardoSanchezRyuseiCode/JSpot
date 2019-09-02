using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: Address
    /// Description: Entity class to model Address
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class Address
    {
        /// <summary>
        /// AddressId
        /// </summary>
        public Guid AddressId { get; set; }
        /// <summary>
        /// EventId
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// Stress
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// ExternalNumber
        /// </summary>
        public string ExternalNumber { get; set; }
        /// <summary>
        /// InternalNumber
        /// </summary>
        public string InternalNumber { get; set; }
        /// <summary>
        /// Neighborhood
        /// </summary>
        public string Neighborhood { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// ZipCode
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public float Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public float Longitude { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}
