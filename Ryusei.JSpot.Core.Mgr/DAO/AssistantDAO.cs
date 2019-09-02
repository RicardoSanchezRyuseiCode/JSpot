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
    /// Name: AssistantDAO
    /// Description: Data Access Object for Assistant
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    internal class AssistantDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of Assistant
        /// </summary>
        /// <param name="top"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        internal IEnumerable<Assistant> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Assistant> results = new List<Assistant>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            * 
                                            from 
	                                            Core.Assistant A
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
                results = dbConnection.Query<Assistant, User, Assistant>(query,(A, U) => {
                    A.User = U;
                    return A;
                }, @params, splitOn: "UserId");
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save an assistant
        /// </summary>
        /// <param name="assistant">Assistant</param>
        internal void Save(Assistant assistant)
        {
            // Define statement
            string statement = "insert into Core.Assistant(UserId, EventId, IsOwner)values(@UserId, @EventId, @IsOwner)";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, assistant);
            }
        }
        /// <summary>
        /// Name: Update
        /// Description: Method to update assistant information
        /// </summary>
        /// <param name="assistant">Assistant</param>
        internal void Update(Assistant assistant)
        {
            // Define statement
            string statement = "update Core.Assistant set IsOwner = @IsOwner where UserId = @UserId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, assistant);
            }
        }
    }
}
