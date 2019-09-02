using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ryusei.JSpot.Core.Mgr.DAO
{
    /// <summary>
    /// Name: UserDepartmentDAO
    /// Description: Data Access Object for UserDepartment
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-28: Creation
    /// </summary>
    internal class UserDepartmentDAO
    {
        #region [Attributes]
        /// <summary>
        /// DAO
        /// </summary>
        internal Data.DAO DAO { get; set; }
        #endregion

        #region [Constructor]
        internal UserDepartmentDAO()
        {
            this.DAO = new Data.DAO();
        }
        #endregion

        /// <summary>
        /// Name: Select
        /// Description: Method to get a collection of UserDepartment
        /// </summary>
        /// <param name="top"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        internal IEnumerable<UserDepartment> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<UserDepartment> results = new List<UserDepartment>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            UD.*,
	                                            D.*,
	                                            U.*
                                            from
	                                            Core.User_Department UD
                                            inner join
	                                            Auth.[User] U
                                            on
	                                            UD.UserId = U.UserId
                                            inner join
	                                            Core.Department D
                                            on
	                                            UD.DepartmentId = D.DepartmentId
                                            inner join
	                                            Core.EventGroup_Department EVGD
                                            on
	                                            EVGD.DepartmentId = D.DepartmentId
                                            inner join
	                                            Core.EventGroup EVT
                                            on
	                                            EVGD.EventGroupId = EVT.EventGroupId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<UserDepartment, Department, User, UserDepartment>(query, (UD, D, U) => {
                    UD.Department = D;
                    UD.User = U;
                    return UD;
                }, @params, splitOn: "DepartmentId, UserId");
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of user department
        /// </summary>
        /// <param name="collectionUserDepartment">CollectionUserDepartment</param>
        internal void Save(IEnumerable<UserDepartment> collectionUserDepartment)
        {
            // Define statement
            string statement = "insert into Core.User_Department(UserId, DepartmentId)values(@UserId, @DepartmentId)";
            // Define params
            Dictionary<string, Type> dicPropertyNameType = new Dictionary<string, Type>
            {
                { "UserId", typeof(Guid) },
                { "DepartmentId", typeof(Guid) }
            };
            // Execute
            this.DAO.ExecuteBatch(statement, collectionUserDepartment, dicPropertyNameType, Data.DAO.OPERATION_TYPE_INSERT);
        }
    }
}
