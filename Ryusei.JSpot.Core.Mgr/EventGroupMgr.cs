using Ryusei.Exception;
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
    /// Name: EventGroupDepartmentMgr
    /// Description: Manager class to implement the behavior of IEventGroupDepartmentMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class EventGroupMgr : IEventGroupMgr
    {
        #region [Constants]
        public const string ERROR_EVENTGROUP_ALREADY_EXIST = "Jspot.Core.Mgr.EventGroupMgr.ErrorAlreadyExist";
        public const string ERROR_NAMES_REPEATED = "Jspot.Core.Mgr.EventGroupMgr.ErrorNamesRepeated";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static EventGroupMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private EventGroupDAO EventGroupDAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static EventGroupMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private EventGroupMgr()
        {
            this.EventGroupDAO = new EventGroupDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static EventGroupMgr GetInstance()
        {
            return Singleton ?? (Singleton = new EventGroupMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get collection of event group by collection of eventId
        /// </summary>
        /// <param name="collectionEventId">Collection EventId</param>
        public IEnumerable<EventGroup> GetByEventId(IEnumerable<Guid> collectionEventId)
        {
            // Define filter
            string filter = string.Format("EventId in ({0}) and Active = 1", string.Join(",", collectionEventId.Select(x => string.Format("'{0}'", x))));
            // Define order
            string order = "";
            // define params
            object @params = null;
            // return the results
            return this.EventGroupDAO.Select(filter: filter, order: order, @params: @params);
        }

        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of event group by id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<EventGroup> GetByEventId(Guid eventId)
        {
            // Define filter
            string filter = "EventId = @EventId and Active = @Active";
            // Define order
            string order = "Name";
            // define params
            object @params = new { EventId = eventId, Active = true };
            // return the results
            return this.EventGroupDAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByUserId
        /// Description: Method to get EventGroup by UserId
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EventGroup> GetByUserIdEventId(Guid userId, Guid eventId)
        {
            // Define filter
            string filter = "E.EventId = @EventId and U.UserId = @UserId and U.Active = @Active and D.Active = @Active and E.Active = @Active";
            // Define order
            string order = "Name";
            // define params
            object @params = new { EventId = eventId, UserId = userId, Active = true };
            // return the results
            return this.EventGroupDAO.SelectRelationUser(top: "distinct", filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetById
        /// Description: Method to get by id
        /// </summary>
        /// <param name="eventGroupId"></param>
        /// <returns></returns>
        public EventGroup GetById(Guid eventGroupId)
        {
            // Define filter
            string filter = "EventGroupId = @EventGroupId and Active = @Active";
            // Define order
            string order = "Name";
            // define params
            object @params = new { EventGroupId = eventGroupId, Active = true };
            // get the results
            IEnumerable<EventGroup> result = this.EventGroupDAO.Select(filter: filter, order: order, @params: @params);
            // return 
            return result.Count() > 0 ? result.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: ValidDeparment
        /// Description: Method to check if department is valid
        /// </summary>
        private void ValidDeparment(IEnumerable<EventGroup> currentEventGroups, EventGroup eventGroup)
        {
            foreach (EventGroup currentEventGroup in currentEventGroups)
            {
                if (currentEventGroup.Name.ToLower().Equals(eventGroup.Name.ToLower()))
                    throw new ManagerException(ERROR_EVENTGROUP_ALREADY_EXIST, new System.Exception(string.Format("A event group with name: {0}, already exist", eventGroup.Name)));
            }
        }
        /// <summary>
        /// Name: Save
        /// Descritpion: Method to save a collection of EventGroup
        /// </summary>
        /// <param name="collectionEventGroup">CollectionEventGroup</param>
        public void Save(IEnumerable<EventGroup> collectionEventGroup)
        {
            // Check if name are repeated
            if (collectionEventGroup.Count() != collectionEventGroup.Select(x => x.Name).Distinct().Count())
                throw new ManagerException(ERROR_NAMES_REPEATED, new System.Exception("Collection of event group to create containts repeated names"));
            // Get current deparments
            IEnumerable<EventGroup> currentEventGroups = this.GetByEventId(collectionEventGroup.Select(x => x.EventId).Distinct());
            // Check if some deparemtn already exist
            foreach (EventGroup newEventGroup in collectionEventGroup)
            {
                ValidDeparment(currentEventGroups, newEventGroup);
            }
            this.EventGroupDAO.Save(collectionEventGroup);
        }
        #endregion
    }
}
