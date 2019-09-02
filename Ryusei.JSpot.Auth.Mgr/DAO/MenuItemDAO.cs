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
    /// Name: MenuItemDAO
    /// Description: Data Access Object for MenuItem
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class MenuItemDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to get a MenuItem collection
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection MenuItem</returns>
        internal IEnumerable<MenuItem> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<MenuItem> results = new List<MenuItem>();
            // query
            string query = string.Format("select {0} * from [Auth].[MenuItem] ", top);
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
        /// Name: SelectChilds
        /// Description: Method to get a collection of childs of MenuItem
        /// </summary>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        internal IEnumerable<MenuItem> SelectChilds(Guid menuItemId)
        {
            // result
            IEnumerable<MenuItem> results = new List<MenuItem>();
            // query
            string query = @"
                                with MenuItemStructureDown(MenuItemId, UpMenuItemId, MainName, AltName, URL, Icon, [Order], IsInitial, Active, Level)
                                as
                                (
		                                select 
			                                MI.MenuItemId, 
			                                MI.UpMenuItemId,
			                                MI.MainName, 
			                                MI.AltName, 
			                                MI.URL, 
			                                MI.Icon, 
			                                MI.[Order], 
			                                MI.IsInitial, 
			                                MI.Active,
			                                0 as Level
		                                from
			                                Auth.MenuItem MI
		                                where
				                                MI.MenuItemId = @MenuItemId
			                                and
				                                MI.Active = 1
	                                -----------------------------------------------
                                    union all 
                                    -----------------------------------------------
		                                select 
			                                MI.MenuItemId, 
			                                MI.UpMenuItemId,
			                                MI.MainName, 
			                                MI.AltName, 
			                                MI.URL, 
			                                MI.Icon, 
			                                MI.[Order], 
			                                MI.IsInitial, 
			                                MI.Active,
		                                    Level + 1
		                                from
			                                Auth.MenuItem MI
		                                inner join
			                                MenuItemStructureDown MISD
		                                on
			                                MI.UpMenuItemId = MISD.MenuItemId
		                                where
			                                MI.Active = 1
                                )
	                                select 
		                                MenuItemId, 
		                                UpMenuItemId,
		                                MainName, 
		                                AltName, 
		                                URL, 
		                                Icon, 
		                                [Order], 
		                                IsInitial, 
		                                Active
	                                from
		                                Auth.MenuItem
	                                where
			                                MenuItemId =  @MenuItemId
		                                and
			                                Active = 1
                                union 
	                                select 
		                                MenuItemId, 
		                                UpMenuItemId,
		                                MainName, 
		                                AltName, 
		                                URL, 
		                                Icon, 
		                                [Order], 
		                                IsInitial, 
		                                Active
	                                from
		                                MenuItemStructureDown
                                ";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<MenuItem>(query, new { MenuItemId = menuItemId });
            }
            // list contacts
            return results;
        }
    }
}
