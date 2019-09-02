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
    /// Name: PermissionDAO
    /// Description: Data Access Object for Permission
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class PermissionDAO
    {
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of elements
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of permissions</returns>
        internal IEnumerable<Permission> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Permission> results = new List<Permission>();
            // query
            string query = string.Format("select {0} * from [Auth].[Permission] ", top);
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
        /// Name: SelectChilds
        /// Description: Method to select the father and child elements from a Permission Id (Up to Down)
        /// </summary>
        /// <param name="permissionId">PermissionId</param>
        /// <returns>Collection of permission</returns>
        internal IEnumerable<Permission> SelectChilds(Guid permissionId)
        {
            // result
            IEnumerable<Permission> results = new List<Permission>();
            // query
            string query = @"
                                with PermissionStructureDown(PermissionId, UpPermissionId, Name, Type, Active, Level)
                                as 
                                (
		                                -- Anchor member definition
		                                select
			                                P.PermissionId, 
			                                P.UpPermissionId, 
			                                P.Name, 
			                                P.Type,
			                                P.Active,
			                                0 as Level
		                                from 
			                                Auth.Permission P
		                                where 
				                                P.UpPermissionId = @PermissionId
			                                and 
				                                P.Active = 1  
                                    -----------------------------------------------
                                    union all 
                                    -----------------------------------------------
		                                -- Recursive member definition
		                                select
			                                P.PermissionId, 
			                                P.UpPermissionId, 
			                                P.Name, 
			                                P.Type,
			                                P.Active,
		                                    Level + 1
		                                from 
			                                Auth.Permission P
		                                inner join
			                                PermissionStructureDown PSW
		                                on
			                                P.UpPermissionId = PSW.PermissionId
		                                where
			                                P.Active = 1
                                )
	                                select
		                                PermissionId, 
		                                UpPermissionId, 
		                                Name, 
		                                Type,
		                                Active
	                                from
		                                Auth.Permission
	                                where 
			                                PermissionId = @PermissionId 
		                                and
			                                Active = 1
                                union
	                                select 
		                                PermissionId, 
		                                UpPermissionId, 
		                                Name, 
		                                Type,
		                                Active
	                                from 
		                                PermissionStructureDown
                                ";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Permission>(query, new { PermissionId = permissionId });
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: HaveAccess
        /// Description: Method to check if a user have acces to a specific resource
        /// </summary>
        /// <param name="userDataId">UserDataId</param>
        /// <param name="applicationId">ApplicationId</param>
        /// <param name="actionName">ActionName</param>
        /// <param name="controllerName">ControllerName</param>
        /// <param name="severName">ServerName</param>
        /// <returns></returns>
        internal int HaveAccess(Guid userId, string actionName, string controllerName, string serverName)
        {
            // Define statement
            string statement = @"select 
	                                count(*)
                                from
	                                Auth.Permission ACTION
                                inner join
	                                Auth.Permission CONTROLLER
                                on
	                                ACTION.Type = 'ACTION' and CONTROLLER.Type = 'CONTROLLER' and ACTION.UpPermissionId = CONTROLLER.PermissionId
                                inner join 
	                                Auth.Permission SERVER
                                on
	                                CONTROLLER.Type = 'CONTROLLER' and SERVER.Type = 'SERVER' and CONTROLLER.UpPermissionId = SERVER.PermissionId
                                inner join
	                                Auth.Role_Permission ROLE_ACTION
                                on
	                                ACTION.PermissionId = ROLE_ACTION.PermissionId
                                inner join
	                                Auth.Role ROLE
                                on
	                                ROLE_ACTION.RoleId = ROLE.RoleId
                                inner join
	                                Auth.User_Role USER_ROLE
                                on
	                                ROLE.RoleId = USER_ROLE.RoleId
                                inner join
	                                Auth.[User] USERDATA
                                on
	                                USER_ROLE.UserId = USERDATA.UserId
                                where
	                                USERDATA.UserId = @userId
                                and
	                                ACTION.Name = @actionName
                                and
	                                CONTROLLER.Name = @controllerName
                                and
	                                SERVER.Name = @serverName
                                and
	                                ACTION.Active = 1 and CONTROLLER.Active = 1 and SERVER.Active = 1
                                and
	                                ROLE.Active = 1
                                and 
	                                USERDATA.Active = 1 ";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                return dbConnection.ExecuteScalar<int>(statement, new { userId, actionName, controllerName, serverName });
            }
        }
    }
}
