using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Ent
{
    /// <summary>
    /// Name: User
    /// Description: Entity class to model User
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-15: Creation
    /// </summary>
    public class User
    {
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Lastname
        /// </summary>
        public string Lastname { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// Job
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// Photo
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// /IsValidated
        /// </summary>
        public bool IsValidated { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}
