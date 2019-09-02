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
    /// Name: RolePermissionDAO
    /// Description: Data Access Object for RolePermission
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class RolePermissionDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of RolePermission
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>RolePermission</returns>
        internal IEnumerable<RolePermission> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<RolePermission> results = new List<RolePermission>();
            // query
            string query = string.Format(@"
                                            select 
	                                            RP.* 
                                            from 
	                                            [Auth].[Role] R 
                                            inner join 
	                                            [Auth].[Role_Permission] RP 
                                            on 
	                                            R.RoleId = RP.RoleId 
                                            inner join
	                                            [Auth].[Permission] P 
                                            on 
	                                            RP.PermissionId = P.PermissionId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<RolePermission>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: SelectPermissions
        /// Description: Method to select permissions
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns></returns>
        internal IEnumerable<Permission> SelectPermission(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Permission> results = new List<Permission>();
            // query
            string query = string.Format(@"
                                            select 
	                                            P.* 
                                            from 
	                                            [Auth].[Role_Permission] RP 
                                            inner join 
	                                            [Auth].[Permission] P 
                                            on 
	                                            RP.PermissionId = P.PermissionId
                                         ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Permission>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: SelectRole
        /// Description: Method to select role
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns></returns>
        internal IEnumerable<Role> SelectRole(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Role> results = new List<Role>();
            // query
            string query = string.Format(@"
                                            select 
	                                            R.* 
                                            from 
	                                            [Auth].[Role_Permission] RP 
                                            inner join 
	                                            [Auth].[Role] R 
                                            on 
	                                            RP.RoleId = R.RoleId
                                          ", top);
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
