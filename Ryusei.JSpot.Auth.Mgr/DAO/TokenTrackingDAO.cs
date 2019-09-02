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
    /// Name: TokenTrackingDAO
    /// Description: Data Access Object for TokenTracking
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class TokenTrackingDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of tokenTracking
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns></returns>
        internal IEnumerable<TokenTracking> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<TokenTracking> results = new List<TokenTracking>();
            // query
            string query = string.Format("select {0} * from [Auth].[TokenTracking] ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                try
                {
                    // Get results
                    results = dbConnection.Query<TokenTracking>(query, @params);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }                
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save an application
        /// </summary>
        /// <param name="tokenTracking">Application</param>
        internal void Save(TokenTracking tokenTracking)
        {
            // Define statement
            string statement = "insert into [Auth].[TokenTracking](TokenTrackingId, UserId, Token, RequestedDate)values(@TokenTrackingId, @UserId, @Token, @RequestedDate)";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, tokenTracking);
            }
        }
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete a token by UserDataId
        /// </summary>
        /// <param name="tokenTrackingId">TokenTrackingID</param>
        internal void Delete(Guid tokenTrackingId)
        {
            // Define statement
            string statement = "delete [Auth].[TokenTracking] where TokenTrackingId = @TokenTrackingId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, new { TokenTrackingId = tokenTrackingId });
            }
        }
    }
}
