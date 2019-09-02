using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Prm
{
    /// <summary>
    /// Name: InvitationImportPrm
    /// Description: Parameter class for Invitation
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-28: Creation
    /// </summary>
    public class InvitationImportPrm
    {
        public IEnumerable<string> CollectionEmail { get; set; }

        public Guid EventId { get; set; }
    }
}
