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
    /// Name: EventGroupDepartmentDAO
    /// Description: Data access object class for EventGroupDepartmentDAO
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-24: Creation
    /// </summary>
    internal class EventGroupDepartmentDAO
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
        public EventGroupDepartmentDAO()
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
        internal IEnumerable<EventGroupDepartment> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<EventGroupDepartment> results = new List<EventGroupDepartment>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            EVTD.*,
                                                EVT.*,
                                                D.*
                                            from
	                                            Core.EventGroup_Department EVTD
                                            inner join
	                                            Core.Department D
                                            on
	                                            EVTD.DepartmentId = D.DepartmentId
                                            inner join
	                                            Core.EventGroup EVT
                                            on
	                                            EVT.EventGroupId = EVT.EventGroupId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<EventGroupDepartment,EventGroup, Department, EventGroupDepartment>(query, (EVTD, EVT, D) => {
                    EVTD.EventGroup = EVT;
                    EVTD.Department = D;
                    return EVTD;
                }, @params, splitOn: "EventId, DepartmentId");
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of departments
        /// </summary>
        /// <param name="collectionEventGroups">CollectionDepartments</param>
        public void Save(IEnumerable<EventGroupDepartment> collectionEventGroups)
        {
            // Define statement
            string statement = "insert into Core.EventGroup_Department(EventGroupId, DepartmentId)values(@EventGroupId, @DepartmentId)";
            // Define params
            Dictionary<string, Type> dicPropertyNameType = new Dictionary<string, Type>
            {
                { "EventGroupId", typeof(Guid) },
                { "DepartmentId", typeof(Guid) }
            };
            // Execute
            this.DAO.ExecuteBatch(statement, collectionEventGroups, dicPropertyNameType, Data.DAO.OPERATION_TYPE_INSERT);
        }
        #endregion
    }
}
