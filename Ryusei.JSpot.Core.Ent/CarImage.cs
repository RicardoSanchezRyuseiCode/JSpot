using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: CarImage
    /// Description: Entity class to model CarImage
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class CarImage
    {
        /// <summary>
        /// CarImageId
        /// </summary>
        public Guid CarImageId { get; set; }
        /// <summary>
        /// CarId
        /// </summary>
        public Guid CarId { get; set; }
        /// <summary>
        /// ImagePath
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// ImageThumb
        /// </summary>
        public string ImageThumb { get; set; }
        /// <summary>
        /// IsMain
        /// </summary>
        public bool IsMain { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}
