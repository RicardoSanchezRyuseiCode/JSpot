using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: Transport
    /// Description: Entity class to model Transport
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class Transport
    {
        /// <summary>
        /// TransportId
        /// </summary>
        public Guid TransportId { get; set; }
        /// <summary>
        /// EventId
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// CarId
        /// </summary>
        public Guid CarId { get; set; }
        /// <summary>
        /// DepartureLatitude
        /// </summary>
        public float DepartureLatitude { get; set; }
        /// <summary>
        /// DepartureLongitude
        /// </summary>
        public float DepartureLongitude { get; set; }
        /// <summary>
        /// ArriveLatitude
        /// </summary>
        public float ArriveLatitude { get; set; }
        /// <summary>
        /// ArriveLongitude
        /// </summary>
        public float ArriveLongitude { get; set; }
        /// <summary>
        /// DepartureDate
        /// </summary>
        public DateTime DepartureDate { get; set; }
        /// <summary>
        /// Travel Sense
        /// </summary>
        public bool TravelSense { get; set; }
        /// <summary>
        /// SexType
        /// </summary>
        public short SexType { get; set; }
        /// <summary>
        /// IsFull
        /// </summary>
        public bool IsFull { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Car
        /// </summary>
        public Car Car { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}
