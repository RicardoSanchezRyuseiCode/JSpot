using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Data
{
    /// <summary>
    /// Name: DAO
    /// Description: Class to define functionalties to get generic database connectors
    /// Author: Ricardo Sanchez Romero
    /// LogBook:
    ///     12/04/2018: Creation
    /// </summary>
    public class DAO
    {
        #region [Constants]
        /// <summary>
        /// Operation Type Insert
        /// </summary>
        public const string OPERATION_TYPE_INSERT = "Insert";
        /// <summary>
        /// Operation Type Update
        /// </summary>
        public const string OPERATION_TYPE_UPDATE = "Update";
        /// <summary>
        /// Operation Type Delete
        /// </summary>
        public const string OPERATION_TYPE_DELETE = "Delete";
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get and instance of IDBConnection
        /// </summary>
        /// <returns>IDBConnection</returns>
        public static IDbConnection GetInstance(Type dbType)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultDataBase"].ToString();
            return (IDbConnection)Activator.CreateInstance(dbType, new object[] { connectionString });
        }
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get and instance of IDBConnection
        /// </summary>
        /// <param name="dbType">Type of DBMS to use</param>
        /// <param name="connectionString">Connection string to connect</param>
        /// <returns></returns>
        public static IDbConnection GetInstance(Type dbType, string connectionString)
        {
            return (IDbConnection)Activator.CreateInstance(dbType, new object[] { connectionString });
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public DAO() { }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: AddColumnsAndParameters
        /// Description: Method to add Columns and parameters
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="listSqlParameter"></param>
        /// <param name="columnName"></param>
        /// <param name="type"></param>
        private void AddColumnsAndParameters(DataTable dataTable, List<SqlParameter> listSqlParameter, string columnName, Type type)
        {
            // Add BroadcastMetaDataId
            dataTable.Columns.Add(new DataColumn(columnName, type));
            // Create SQL parameter
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = string.Format("@{0}", columnName);
            parameter.SourceColumn = columnName;
            listSqlParameter.Add(parameter);
        }
        /// <summary>
        /// Name: ExecuteBatch
        /// Description: Method to execute batch
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="listSqlParameter">List SQL Parameter</param>
        /// <param name="statement">Statement</param>
        /// <param name="operation">Operation</param>
        /// <param name="connectionString">ConnectionString</param>
        private void ExecuteBatch(DataTable dataTable, List<SqlParameter> listSqlParameter, string statement, string operation, int batchSize, string connectionString = "")
        {
            // Accept the changes of table
            dataTable.AcceptChanges();
            // Update the row type
            foreach (DataRow dataRow in dataTable.Rows)
            {
                switch (operation)
                {
                    case OPERATION_TYPE_INSERT: dataRow.SetAdded(); break;
                    case OPERATION_TYPE_UPDATE: dataRow.SetModified(); break;
                    case OPERATION_TYPE_DELETE: dataRow.Delete(); break;
                }
            }
            // Define connection string 
            if (string.IsNullOrEmpty(connectionString))
                connectionString = ConfigurationManager.ConnectionStrings["DefaultDataBase"].ToString();
            // Open connection with database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the command
                SqlCommand command = new SqlCommand(statement, connection);
                // Add the parameters
                foreach (SqlParameter parameter in listSqlParameter)
                {
                    command.Parameters.Add(parameter);
                }
                // Create the adapter and set the command
                SqlDataAdapter adapter = new SqlDataAdapter();
                // Set the command 
                switch (operation)
                {
                    case OPERATION_TYPE_INSERT: adapter.InsertCommand = command; adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None; break;
                    case OPERATION_TYPE_UPDATE: adapter.UpdateCommand = command; adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None; break;
                    case OPERATION_TYPE_DELETE: adapter.DeleteCommand = command; adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None; break;
                }
                // Set the batch size
                adapter.UpdateBatchSize = batchSize;
                // Execute the batch operation
                adapter.Update(dataTable);
                // Clean the comand
                command.Parameters.Clear();
            }
            // Clean the rows of table
            dataTable.Rows.Clear();
        }
        /// <summary>
        /// Name: ExecuteBatch
        /// Description: Method to execute batch opetation 
        /// </summary>
        /// <typeparam name="T">CollectionType</typeparam>
        /// <param name="statement">Statement</param>
        /// <param name="collection">Collection</param>
        /// <param name="dicPropertyTypes">CollectionPropertyTypes (Dont put nullable types)</param>
        /// <param name="connectionString">connectionstring</param>
        public void ExecuteBatch<T>(string statement, IEnumerable<T> collection, Dictionary<string, Type> dicPropertyTypes, string operationType, string connectionString = "")
        {
            // Create data table
            DataTable dataTable = new DataTable();
            // Create list of SQL Parameter
            List<SqlParameter> listSqlParameter = new List<SqlParameter>();
            // Get batch size
            int batchSize = int.Parse(ConfigurationManager.AppSettings["RYUSEI::DATA::SQLBATCHSIZE"]);
            // Assing columns and parameters
            foreach (string propertTypeName in dicPropertyTypes.Keys)
            {
                this.AddColumnsAndParameters(dataTable, listSqlParameter, propertTypeName, dicPropertyTypes[propertTypeName]);
            }
            // Loop in list 
            foreach (T element in collection)
            {
                // Create new
                DataRow dataRow = dataTable.NewRow();
                // Loop in collection 
                foreach (string propertyName in dicPropertyTypes.Keys)
                {
                    dataRow[propertyName] = element.GetType().GetProperty(propertyName).GetValue(element, null);
                }
                // Add the row to the table
                dataTable.Rows.Add(dataRow);
                // Check if the size is greather than batch
                if (dataTable.Rows.Count >= batchSize)
                    this.ExecuteBatch(dataTable, listSqlParameter, statement, operationType, dataTable.Rows.Count, connectionString);
            }
            // Check if we have remaining elements
            if (dataTable.Rows.Count > 0)
                this.ExecuteBatch(dataTable, listSqlParameter, statement, operationType, dataTable.Rows.Count, connectionString);
        }
        #endregion
    }
}
