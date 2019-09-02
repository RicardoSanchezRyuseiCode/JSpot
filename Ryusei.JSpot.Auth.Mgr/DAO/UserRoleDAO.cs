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
    /// Name: UserRoleDAO
    /// Description: Data Access Object for UserRole
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class UserRoleDAO
    {
        /// <summary>
        /// Name: SelectRole
        /// Description: Method to select Role information
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of Role ojects</returns>
        internal IEnumerable<Role> SelectRole(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Role> results = new List<Role>();
            // query
            string query = string.Format("select * from [Auth].[User_Role] UDR inner join [Auth].[Role] R on UDR.RoleId = R.RoleId  ", top);
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
        /// <summary>
        /// Name: SelectUserData
        /// Description: Method to select a collection of UserData
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of UserData</returns>
        internal IEnumerable<User> SelectUser(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<User> results = new List<User>();
            // query
            string query = string.Format("select {0} * from [Auth].[User_Role] UDR inner join [Auth].[User] UD on UDR.UserId = UD.UserId  ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<User>(query, @params);
            }
            // list contacts
            return results;
        }

        /// <summary>
        /// Name: Save
        /// Description: Method to save a userDataRole Object
        /// </summary>
        /// <param name="userDataRole">UserDataRole</param>
        internal int Save(IEnumerable<UserRole> listUserDataRole)
        {
            // define statement
            string statement = "insert into [Auth].[User_Role](UserId, RoleId)values(@UserId, @RoleId)";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                return dbConnection.Execute(statement, listUserDataRole);
            }
        }
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete a collection of userDataRole
        /// </summary>
        /// <param name="listUserDataRole">listUserDataRole</param>
        internal int Delete(IEnumerable<UserRole> listUserDataRole)
        {
            // define statement
            string statement = "delete from [Auth].[User_Role] where UserId = @UserId and RoleId = @RoleId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                return dbConnection.Execute(statement, listUserDataRole);
            }
        }

    }
}
