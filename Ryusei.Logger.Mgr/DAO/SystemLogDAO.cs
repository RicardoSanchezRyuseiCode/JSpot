using Ryusei.Logger.Ent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ryusei.Logger.Mgr.DAO
{
    /// <summary>
    /// Name: SystemLogDAO
    /// Description: Data Access Object class for SystemLog
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    internal class SystemLogDAO
    {
        #region [Methods]
        /// <summary>
        /// Name: Select
        /// Description: Methdo to select a collection of system log
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of system log</returns>
        internal IEnumerable<SystemLog> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<SystemLog> results = new List<SystemLog>();
            // query
            string query = string.Format(@"select {0} * from Logger.SystemLog", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = JSpot.Data.DAO.GetInstance(JSpot.Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<SystemLog>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a system log
        /// </summary>
        /// <param name="systemLog">SystemLog</param>
        internal void Save(SystemLog systemLog)
        {
            // Set default data
            systemLog.SystemLogId = Guid.NewGuid();
            systemLog.RegisterDate = DateTime.Now.ToUniversalTime();
            // Define sentence
            string sentence = @"insert into Logger.SystemLog(SystemLogId, Server, UserDataId, [Type], Message, StackTrace, InnerMessage, InnerStackTrace, RegisterDate)values(@SystemLogId, @Server, @UserDataId, @Type, @Message, @StackTrace, @InnerMessage, @InnerStackTrace, @RegisterDate)";
            // Execute
            using (IDbConnection dbConnection = JSpot.Data.DAO.GetInstance(JSpot.Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(sentence, systemLog);
            }
        }
        #endregion
    }
}
