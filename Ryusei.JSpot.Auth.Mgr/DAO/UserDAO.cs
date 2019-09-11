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
    /// Name: UserDAO
    /// Description: Data Access Object for User
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    internal class UserDAO
    {
        /// <summary>
        /// Name: SelectRole
        /// Description: Method to get a collection of User Objects
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns></returns>
        internal IEnumerable<User> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<User> results = new List<User>();
            // query
            string query = string.Format("select {0} * from [Auth].[User] ", top);
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
        /// Description: Method to save a company data id
        /// </summary>
        /// <param name="userData">UseDataId</param>
        internal void Save(User userData)
        {
            // Define statement
            string statement = "insert into [Auth].[User](UserId, Name, Lastname, Email, Phone, Company, Job, Photo, IsValidated, Password, Active)values(@UserId, @Name, @Lastname, @Email, @Phone, @Company, @Job, @Photo, @IsValidated, @Password, @Active)";
            // Set default data
            DateTime currentDate = DateTime.Now.ToUniversalTime();
            userData.UserId = Guid.NewGuid();
            userData.IsValidated = true;
            userData.Job = "";
            userData.Active = true;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, userData);
            }
        }
        /// <summary>
        /// Update
        /// Description: Method to update a userData
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        internal int Update(User userData)
        {
            // Define statement
            string statement = "update [Auth].[User] set Name = @Name, Lastname = @Lastname, Phone = @Phone, Company = @Company, Job = @Job where UserId = @UserId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                return dbConnection.Execute(statement, userData);
            }
        }
        /// <summary>
        /// Name: UpdatePhoto
        /// Description: Method to update a photo
        /// </summary>
        /// <param name="userDataId">UserId</param>
        /// <param name="photo">Photo</param>
        /// <returns></returns>
        internal int UpdatePhoto(Guid userDataId, string photo)
        {
            // Define statement
            string statement = "update [Auth].[User] set Photo = @Photo where UserId = @UserId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                return dbConnection.Execute(statement, new { Photo = photo, UserId = userDataId });
            }
        }
        /// <summary>
        /// Name: UpdateIsValidated
        /// Description: Method to update IsValidated
        /// </summary>
        /// <param name="userDataId">UserId</param>
        /// <param name="isValidated">IsValidates</param>
        /// <returns>Affected elements</returns>
        internal int UpdateIsValidated(Guid userDataId, bool isValidated)
        {
            // Define statement
            string statement = "update [Auth].[User] set IsValidated = @IsValidated where UserId = @UserId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                return dbConnection.Execute(statement, new { IsValidated = isValidated, UserId = userDataId });
            }
        }
        /// <summary>
        /// Name: UpdatePassword
        /// Description: Method to update password
        /// </summary>
        /// <param name="userDataId">UserId</param>
        /// <param name="password">Password</param>
        /// <returns>Affected elements</returns>
        internal int UpdatePassword(Guid userDataId, string password)
        {
            // Define statement
            string statement = "update [Auth].[User] set Password = @Password where UserId = @UserId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                return dbConnection.Execute(statement, new { Password = password, UserId = userDataId });
            }
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate a User
        /// </summary>
        /// <param name="userDataId">UserId</param>
        /// <returns>Affected elements</returns>
        internal int Deactivate(Guid userDataId)
        {
            // Define statement
            string statement = "update [Auth].[User] set Active = @Active where UserId = @UserId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                return dbConnection.Execute(statement, new { Active = false, UserId = userDataId });
            }
        }
    }
}
