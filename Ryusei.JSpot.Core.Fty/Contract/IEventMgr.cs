using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IEventMgr
    /// Descrpition: Interface to define the behavior of IEventMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IEventMgr
    {
        /// <summary>
        /// Name: GetByUser
        /// Description: Method to get a collection of events of the user
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        IEnumerable<Event> GetByUser(Guid userId);
        /// <summary>
        /// Name: GetByUser
        /// Description: Method to get a collection of events
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="endDate">EndDate</param>
        /// <returns></returns>
        IEnumerable<Event> GetByUser(Guid userId, DateTime endDate);
        /// <summary>
        /// Name: GetOwners
        /// Description: Method to get a collection of owners for the event
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        IEnumerable<User> GetOwners(Guid eventId);
        /// <summary>
        /// Name: GetById
        /// Description: Method to get event by id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Event GetById(Guid eventId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save event
        /// </summary>
        /// <param name="event">Event</param>
        void Save(Event @event);
    }
}
