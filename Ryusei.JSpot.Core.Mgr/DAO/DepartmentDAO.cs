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
    /// Name: DepartmentDAO
    /// Description: Method to save DepartmentDAO
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-24: Creation
    /// </summary>
    internal class DepartmentDAO
    {
        #region [Attributes]
        /// <summary>
        /// DAO
        /// </summary>
        private Ryusei.JSpot.Data.DAO DAO { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentDAO()
        {
            this.DAO = new Data.DAO();
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Select
        /// Description: Method to select a collection of departments
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection Department</returns>
        internal IEnumerable<Department> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Department> results = new List<Department>();
            // query
            string query = string.Format(@"
                                            select {0}
	                                            *
                                            from
	                                            Core.Department
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Department>(query, @params);
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of departments
        /// </summary>
        /// <param name="collectionDepartments">CollectionDepartments</param>
        public void Save(IEnumerable<Department> collectionDepartments)
        {
            // Define statement
            string statement = "insert into Core.Department(DepartmentId, EventId, Name, Active)values(@DepartmentId, @EventId, @Name, @Active)";
            // loop in data 
            foreach (Department department in collectionDepartments)
            {
                // Set default data
                department.Active = true;
                department.DepartmentId = Guid.NewGuid();
            }
            // Define params
            Dictionary<string, Type> dicPropertyNameType = new Dictionary<string, Type>
            {
                { "DepartmentId", typeof(Guid) },
                { "EventId", typeof(Guid) },
                { "Name", typeof(string) },
                { "Active", typeof(bool) }
            };
            // Execute
            this.DAO.ExecuteBatch(statement, collectionDepartments, dicPropertyNameType, Data.DAO.OPERATION_TYPE_INSERT);
        }
        #endregion
    }
}
