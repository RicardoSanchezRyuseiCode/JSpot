using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: Car
    /// Description: Entity class to model Car
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class Car
    {
        /// <summary>
        /// CarId
        /// </summary>
        public Guid CarId { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Brand
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Color
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Spots
        /// </summary>
        public int Spots { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }
    }
}
