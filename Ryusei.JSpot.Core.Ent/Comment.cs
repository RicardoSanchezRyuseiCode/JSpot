using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Ent
{
    /// <summary>
    /// Name: Comment
    /// Description: Entity class to model Comment
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// CommentId
        /// </summary>
        public Guid CommentId { get; set; }
        /// <summary>
        /// TransportId
        /// </summary>
        public Guid TransportId { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// CommentText
        /// </summary>
        public string CommentText { get; set; }
        /// <summary>
        /// CommentDate
        /// </summary>
        public string CommentDate { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}
