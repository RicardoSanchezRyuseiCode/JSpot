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
    public class EventGroupDepartmentMgr : IEventGroupDepartmentMgr
    {
        #region [Constants]
        public const string ERROR_EVENTGROUP_ALREADY_EXIST = "Jspot.Core.Mgr.EventGroupMgr.ErrorAlreadyExist";
        public const string ERROR_NAMES_REPEATED = "Jspot.Core.Mgr.EventGroupMgr.ErrorNamesRepeated";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static EventGroupDepartmentMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private EventGroupDepartmentDAO EventGroupDepartmentDAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static EventGroupDepartmentMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private EventGroupDepartmentMgr()
        {
            this.EventGroupDepartmentDAO = new EventGroupDepartmentDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static EventGroupDepartmentMgr GetInstance()
        {
            return Singleton ?? (Singleton = new EventGroupDepartmentMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEventGroupId
        /// Description: Method to get a collection of EventGroupId
        /// </summary>
        /// <param name="eventGroupId">EventGruopId</param>
        /// <returns></returns>
        public IEnumerable<EventGroupDepartment> GetByEventGroupId(Guid eventGroupId)
        {
            // Define filter
            string filter = "EVT.EventGroupId = @EventGroupId and D.Active = @Active";
            // Define order
            string order = "";
            // Define params
            object @params = new { EventGroupId = eventGroupId, Active = true };
            // return the objects
            return this.EventGroupDepartmentDAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of EventGroupDepartment
        /// </summary>
        /// <param name="collectionEventGroupDepartment">CollectionEventGroupDepartment</param>
        public void Save(IEnumerable<EventGroupDepartment> collectionEventGroupDepartment)
        {
            // Check if relation already exist
            this.EventGroupDepartmentDAO.Save(collectionEventGroupDepartment);
        }
        #endregion
    }
}
