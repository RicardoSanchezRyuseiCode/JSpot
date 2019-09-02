using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ryusei.JSpot.Auth.Ent;

namespace Ryusei.JSpot.Core.Mgr.DAO
{
    /// <summary>
    /// Name: EventDAO
    /// Description: Data Access Object for Event
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    internal class EventDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to get a collection of Event
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of events</returns>
        internal IEnumerable<Event> SelectEvent(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Event> results = new List<Event>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            EVT.*
                                            from
	                                            Core.Event EVT
                                            inner join
	                                            Core.Assistant A
                                            on
	                                            EVT.EventId = A.EventId
                                            inner join
	                                            Auth.[User] U
                                            on
	                                            A.UserId = U.UserId 
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Event>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: SelectUser
        /// Description: Method to select collection of user
        /// </summary>
        /// <param name="top"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        internal IEnumerable<User> SelectUser(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<User> results = new List<User>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            U.*
                                            from
	                                            Core.Event EVT
                                            inner join
	                                            Core.Assistant A
                                            on
	                                            EVT.EventId = A.EventId
                                            inner join
	                                            Auth.[User] U
                                            on
	                                            A.UserId = U.UserId 
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<User>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Event
        /// Description: Method to save event
        /// </summary>
        /// <param name="event">Event</param>
        internal void Save(Event @event)
        {
            // Define statement
            string statement = "insert into Core.Event(EventId, Code, Name, Description, StartDate, EndDate, Active)values(@EventId, @Code, @Name, @Description, @StartDate, @EndDate, @Active)";
            // Setd efautl data
            @event.EventId = Guid.NewGuid();
            @event.Code = string.Format("evt_{0}", DateTime.Now.ToUniversalTime().ToString("yyyyMMddhhmmss"));
            @event.StartDate = @event.StartDate.ToUniversalTime();
            @event.EndDate = @event.EndDate.ToUniversalTime();
            @event.Active = true;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, @event);
            }
        }
    }
}
