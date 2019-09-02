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
    /// Name: Address
    /// Description: Data Access Object for Address
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    internal class AddressDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to get a collection of Address
        /// </summary>
        /// <param name="top"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        internal IEnumerable<Address> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Address> results = new List<Address>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            *
                                            from
	                                            Core.[Address] 
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Address>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: MEthod to save an Address
        /// </summary>
        /// <param name="address">Address</param>
        internal void Save(Address address)
        {
            // Define statement
            string statement = "insert into Core.Address(AddressId, EventId, Street, ExternalNumber, InternalNumber, Neighborhood, City, State, Country, ZipCode, Latitude, Longitude, Active)values(@AddressId, @EventId, @Street, @ExternalNumber, @InternalNumber, @Neighborhood, @City, @State, @Country, @ZipCode, @Latitude, @Longitude, @Active)";
            // Set defual data
            address.AddressId = Guid.NewGuid();
            address.Active = true;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, address);
            }
        }
    }
}
