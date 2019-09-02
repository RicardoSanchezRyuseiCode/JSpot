using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IAssistantMgr
    /// Descrpition: Interface to define the behavior of IAssistantMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IAssistantMgr
    {
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of Assistant by EventId
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        IEnumerable<Assistant> GetByEventId(Guid eventId);
        /// <summary>
        /// Name: GetByUserIdEventId
        /// Description: Method to get assistan by ids
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Assistant GetByIds(Guid userId, Guid eventId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save assistant
        /// </summary>
        /// <param name="assistant">Assistant</param>
        void Save(Assistant assistant);
        /// <summary>
        /// Name: Update
        /// Descrition : Method to update assistan
        /// </summary>
        /// <param name="assistant">Asisstant</param>
        void Update(Assistant assistant);
    }
}
