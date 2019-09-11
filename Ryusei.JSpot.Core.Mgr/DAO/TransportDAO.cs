using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Ryusei.JSpot.Auth.Ent;

namespace Ryusei.JSpot.Core.Mgr.DAO
{
    /// <summary>
    /// Name: TransportDAO
    /// Description: Data Access Object fro Transport
    /// </summary>
    internal class TransportDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of transport
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns></returns>
        internal IEnumerable<Transport> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Transport> results = new List<Transport>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            T.*,
	                                            C.*,
                                                U.*
                                            from
	                                            Core.Transport T
                                            inner join
	                                            Core.Car C
                                            on
	                                            T.CarId = C.CarId
                                            inner join
                                                Auth.[User] U
                                            on
                                                U.UserId = C.UserId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Transport, Car, User, Transport>(query, (T, C , U) => {
                    C.User = U;
                    T.Car = C;
                    return T;
                }, @params, splitOn: "CarId, UserId");
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a transport
        /// </summary>
        /// <param name="transport">Transport</param>
        internal void Save(Transport transport)
        {
            // Define statement
            string statement = @"insert into Core.Transport(
                                    TransportId, EventId, CarId, DepartureLatitude, DepartureLongitude, ArriveLatitude, ArriveLongitude, DepartureDate, TravelSense, SexType, IsFull, Active, Description
                                )values(
                                    @TransportId, @EventId, @CarId, @DepartureLatitude, @DepartureLongitude, @ArriveLatitude, @ArriveLongitude, @DepartureDate, @TravelSense, @SexType, @IsFull, @Active, @Description
                                )";
            // Set default data
            transport.TransportId = Guid.NewGuid();
            transport.DepartureDate = transport.DepartureDate.ToUniversalTime();
            transport.IsFull = false;
            transport.Active = true;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, transport);
            }
        }
        /// <summary>
        /// Name: UpdateIsFull
        /// Description. Method to update is full of transport
        /// </summary>
        /// <param name="transportId">TransportId</param>
        /// <param name="isFull">IsFull</param>
        internal void UpdateIsFull(Guid transportId, bool isFull)
        {
            // Define statement
            string statement = @"update Core.Transport set IsFull = @IsFull where TransportId = @TransportId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, new { TransportId = transportId, IsFull = isFull });
            }
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate transport id
        /// </summary>
        /// <param name="transportId">TransportId</param>
        internal void Deactivate(Guid transportId)
        {
            // Define statement
            string statement = @"update Core.Transport set Active = @Active where TransportId = @TransportId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, new { TransportId = transportId, Active = false });
            }
        }
    }
}
