using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ryusei.JSpot.Auth.Ent;

namespace Ryusei.JSpot.Auth.Mgr.DAO
{
    /// <summary>
    /// Name: RoleMenuItemDAO
    /// Description: Data Access Object for RoleMenuItem
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class RoleMenuItemDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to get a collection of RoleMenuItem objects
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of RoleMenuItem</returns>
        internal IEnumerable<RoleMenuItem> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<RoleMenuItem> results = new List<RoleMenuItem>();
            // query
            string query = string.Format(@" 
                                            select {0}
	                                            RMI.* 
                                            from 
	                                            [Auth].[Role] R 
                                            inner join 
	                                            [Auth].[Role_MenuItem] RMI 
                                            on 
	                                            R.RoleId = RMI.RoleId 
                                            inner join 
	                                            [Auth].[MenuItem] MI 
                                            on 
	                                            RMI.MenuItemId = MI.MenuItemId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<RoleMenuItem>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: SelectMenuItem
        /// Description: Method to get a collection of MenuItem
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of MenuItem</returns>
        internal IEnumerable<MenuItem> SelectMenuItem(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<MenuItem> results = new List<MenuItem>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            MI.*, R.Weight 
                                            from 
	                                            [Auth].[Role] R 
                                            inner join
	                                            [Auth].[Role_MenuItem] RMI 
                                            on 
	                                            R.RoleId = RMI.RoleId 
                                            inner join 
	                                            [Auth].[MenuItem] MI 
                                            on 
	                                            RMI.MenuItemId = MI.MenuItemId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<MenuItem>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: SelectRole
        /// Description: Method to selet a collection of Role
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
                                            select {0}
	                                            R.*
                                            from 
	                                            [Auth].[Role_MenuItem] RMI 
                                            inner join 
	                                            [Auth].[Role] R 
                                            on 
	                                            RMI.RoleId = R.RoleId
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
