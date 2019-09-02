using Ryusei.Exception;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty.Contract;
using Ryusei.JSpot.Core.Mgr.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Mgr
{
    /// <summary>
    /// Name: EventMgr
    /// Description: Manager class to implement the behavior of IEventMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class EventMgr : IEventMgr
    {
        #region [Constants]
        public const string ERROR_EVENT_ALREADY_EXIST = "Jspot.Core.Mgr.EventMgr.ErrorEventAlreadyExist";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static EventMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private EventDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static EventMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private EventMgr()
        {
            this.DAO = new EventDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static EventMgr GetInstance()
        {
            return Singleton ?? (Singleton = new EventMgr());
        }
        #endregion


        #region [Methods]
        /// <summary>
        /// Name: GetByUser
        /// Description: Method to get a collection of events of the user
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        public IEnumerable<Event> GetByUser(Guid userId)
        {
            // Define filter
            string filter = "U.Active = @Active and U.UserId = @UserId and EVT.Active = @Active";
            // Define order
            string order = "EVT.StartDate";
            // Define params
            object @params = new { Active = true, UserId = userId };
            // Return the results
            return this.DAO.SelectEvent(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByUser
        /// Description: Method to get a collection of events
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="endDate">EndDate</param>
        /// <returns></returns>
        public IEnumerable<Event> GetByUser(Guid userId, DateTime endDate)
        {
            // Define filter
            string filter = "U.Active = @Active and U.UserId = @UserId and EVT.Active = @Active and EVT.EndDate > @EndDate";
            // Define order
            string order = "EVT.StartDate";
            // Define params
            object @params = new { Active = true, UserId = userId, EndDate = endDate };
            // Return the results
            return this.DAO.SelectEvent(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetOwners
        /// Description: Method to get a collection of owners for the event
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        public IEnumerable<User> GetOwners(Guid eventId)
        {
            // Define filter
            string filter = "U.Active = @Active and EVT.EventId = @EventId and EVT.Active = @Active and A.IsOwner = @IsOwner";
            // Define order
            string order = "U.Email";
            // Define params
            object @params = new { Active = true, EventId = eventId, IsOwner = true };
            // Return the results
            return this.DAO.SelectUser(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByName
        /// Description: Method to get by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Event GetByName(string name)
        {
            // Define filter
            string filter = "U.Active = @Active and LOWER(EVT.Name) = LOWER(@Name) and EVT.Active = @Active";
            // Define order
            string order = "EVT.StartDate";
            // Define params
            object @params = new { Active = true, Name = name };
            // Get the results
            IEnumerable<Event> results = this.DAO.SelectEvent(filter: filter, order: order, @params: @params);
            // Return
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: GetById
        /// Description: Method to get event by id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event GetById(Guid eventId)
        {
            // Define filter
            string filter = "U.Active = @Active and EVT.EventId = @EventId and EVT.Active = @Active";
            // Define order
            string order = "EVT.StartDate";
            // Define params
            object @params = new { Active = true, EventId = eventId };
            // Get the results
            IEnumerable<Event> results = this.DAO.SelectEvent(filter: filter, order: order, @params: @params);
            // Return
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save event
        /// </summary>
        /// <param name="event">Event</param>
        public void Save(Event @event)
        {
            // Check if an event with same name already exist
            if (this.GetByName(@event.Name) != null)
                throw new ManagerException(ERROR_EVENT_ALREADY_EXIST, new System.Exception("An event with name: {0}, already exist"));
            // Save event
            this.DAO.Save(@event);
        }
        #endregion
    }
}
