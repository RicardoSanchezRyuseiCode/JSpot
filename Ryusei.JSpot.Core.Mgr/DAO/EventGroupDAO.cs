using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ryusei.JSpot.Core.Mgr.DAO
{
    /// <summary>
    /// Name: EventGroupDAO
    /// Description: Data access object class for EventGroupDAO
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-24: Creation
    /// </summary>
    internal class EventGroupDAO
    {
        #region [Attributes]
        /// <summary>
        /// DAO
        /// </summary>
        private Ryusei.JSpot.Data.DAO DAO { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public EventGroupDAO()
        {
            this.DAO = new Data.DAO();
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of departments
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection Department</returns>
        internal IEnumerable<EventGroup> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<EventGroup> results = new List<EventGroup>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            *
                                            from
	                                            Core.EventGroup
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<EventGroup>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: SelectRelationUser
        /// Description: Method to get a collection of EventGroup
        /// </summary>
        /// <param name="top"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        internal IEnumerable<EventGroup> SelectRelationUser(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<EventGroup> results = new List<EventGroup>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            E.* 
                                            from 
	                                            Auth.[User] U
                                            inner join
	                                            Core.User_Department UD
                                            on
	                                            U.UserId = UD.UserId
                                            inner join
	                                            Core.Department D
                                            on
	                                            UD.DepartmentId = D.DepartmentId
                                            inner join
	                                            Core.EventGroup_Department ED
                                            on
	                                            ED.DepartmentId = D.DepartmentId
                                            inner join
	                                            Core.EventGroup E
                                            on
	                                            ED.EventGroupId = E.EventGroupId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<EventGroup>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of departments
        /// </summary>
        /// <param name="collectionEventGroups">CollectionDepartments</param>
        public void Save(IEnumerable<EventGroup> collectionEventGroups)
        {
            // Define statement
            string statement = "insert into Core.EventGroup(EventGroupId, EventId, Name, Description, Capacity, StartDate, EndDate, Active)values(@EventGroupId, @EventId, @Name, @Description, @Capacity, @StartDate, @EndDate, @Active)";
            // loop in data 
            foreach (EventGroup eventGroup in collectionEventGroups)
            {
                // Set default data
                eventGroup.EventGroupId = Guid.NewGuid();
                eventGroup.StartDate = eventGroup.StartDate.ToUniversalTime();
                eventGroup.EndDate = eventGroup.EndDate.ToUniversalTime();
                eventGroup.Active = true;
            }
            // Define params
            Dictionary<string, Type> dicPropertyNameType = new Dictionary<string, Type>
            {
                { "EventGroupId", typeof(Guid) },
                { "EventId", typeof(Guid) },
                { "Name", typeof(string) },
                { "Description", typeof(string) },
                { "Capacity", typeof(int) },
                { "StartDate", typeof(DateTime) },
                { "EndDate", typeof(DateTime) },
                { "Active", typeof(bool) }
            };
            // Execute
            this.DAO.ExecuteBatch(statement, collectionEventGroups, dicPropertyNameType, Data.DAO.OPERATION_TYPE_INSERT);
        }
        #endregion
    }
}
