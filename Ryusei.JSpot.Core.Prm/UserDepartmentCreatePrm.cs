using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Prm
{
    /// <summary>
    /// Name: UserDepartmentCreatePrm
    /// Description: Parameter class to define UserDepartment create parameter
    /// </summary>
    public class UserDepartmentCreatePrm
    {
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// EventId
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// CollectionDepartmentId
        /// </summary>
        public IEnumerable<Guid> CollectionDepartmentId { get; set; }
    }
}
