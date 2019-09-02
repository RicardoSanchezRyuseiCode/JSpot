using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ryusei.JSpot.Auth.Mgr.DAO
{
    /// <summary>
    /// Name: RoleDAO
    /// Description: Data Access Object for Role
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class RoleDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of Role items
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of Role</returns>
        internal IEnumerable<Role> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Role> results = new List<Role>();
            // query
            string query = string.Format("select {0} * from [Auth].[Role] ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Role>(query, @params);
            }
            // list contacts
            return results;
        }
    }
}
