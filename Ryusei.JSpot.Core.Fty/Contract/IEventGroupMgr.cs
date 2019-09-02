using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IEventGroupMgr
    /// Descrpition: Interface to define the behavior of IEventGroupMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IEventGroupMgr
    {
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of event group by id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        IEnumerable<EventGroup> GetByEventId(Guid eventId);
        /// <summary>
        /// Name: GetById
        /// Description: Method to get by id
        /// </summary>
        /// <param name="eventGroupId"></param>
        /// <returns></returns>
        EventGroup GetById(Guid eventGroupId);
        /// <summary>
        /// Name: GetByUserId
        /// Description: Method to get EventGroup by UserId
        /// </summary>
        /// <returns></returns>
        IEnumerable<EventGroup> GetByUserIdEventId(Guid userId, Guid eventId);
        /// <summary>
        /// Name: Save
        /// Descritpion: Method to save a collection of EventGroup
        /// </summary>
        /// <param name="collectionEventGroup">CollectionEventGroup</param>
        void Save(IEnumerable<EventGroup> collectionEventGroup);
    }
}
