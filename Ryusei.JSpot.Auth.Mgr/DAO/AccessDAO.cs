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
    /// Name: AccessDAO
    /// Description: Data Access Object for Access
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class AccessDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to get a collection of access objects
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns></returns>
        internal IEnumerable<Access> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Access> results = new List<Access>();
            // query
            string query = string.Format(@" select
	                                            Action.*,
	                                            Controller.Name as Controller,
	                                            Server.Name as Server
                                            from
	                                            Auth.Permission Action
                                            inner join
	                                            Auth.Permission Controller
                                            on
	                                            Action.UpPermissionId = Controller.PermissionId and Action.Type = 'ACTION' and Controller.Type = 'CONTROLLER' and Action.Active = 1 and Controller.Active = 1
                                            inner join
	                                            Auth.Permission Server
                                            on
	                                            Controller.UpPermissionId = Server.PermissionId and Controller.Type = 'Controller' and Server.Type = 'SERVER' and Controller.Active = 1 and Server.Active = 1
                                            inner join
	                                            Auth.Role_Permission RP
                                            on
	                                            Action.PermissionId = RP.PermissionId ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Access>(query, @params);
            }
            // list contacts
            return results;
        }
    }
}
