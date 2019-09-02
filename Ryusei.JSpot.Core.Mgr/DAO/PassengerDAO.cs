using Ryusei.JSpot.Auth.Ent;
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
    /// Name: PassengerDAO
    /// Description: Data Access Object for Passenger
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-31: Creation
    /// </summary>
    internal class PassengerDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to get a collection of Passnger
        /// </summary>
        /// <param name="top"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="params"></param>
        /// <returns>Collection of Passenger</returns>
        internal IEnumerable<Passenger> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Passenger> results = new List<Passenger>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            *
                                            from
	                                            Core.Passenger P
                                            inner join
	                                            Auth.[User] U
                                            on
	                                            P.UserId = U.UserId
                                            inner join
                                                Core.Transport T
                                            on
                                                P.TransportId = T.TransportId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Passenger, User, Transport, Passenger>(query, (P, U, T) => {
                    P.User = U;
                    P.Transport = T;
                    return P;
                }, @params, splitOn: "UserId, TransportId");
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a passenger
        /// </summary>
        /// <param name="passenger"></param>
        internal void Save(Passenger passenger)
        {
            // Define statement
            string statement = @"insert into Core.Passenger(UserId, TransportId)values(@UserId, @TransportId)";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, passenger);
            }
        }

    }
}
