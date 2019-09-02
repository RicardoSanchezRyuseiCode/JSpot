using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IParticipantMgr
    /// Descrpition: Interface to define the behavior of IParticipantMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IParticipantMgr
    {
        /// <summary>
        /// Name: Save
        /// Description: Method to save a participant
        /// </summary>
        /// <param name="participant">Participant</param>
        void Save(Participant participant);
        /// <summary>
        /// Name: GetByEventGroupId
        /// Description: Method to get participant by EventGroupId
        /// </summary>
        /// <param name="eventGroupId">EventGroupId</param>
        /// <returns></returns>
        IEnumerable<Participant> GetByEventGroupId(Guid eventGroupId);
    }
}
