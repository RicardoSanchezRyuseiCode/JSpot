using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Data
{
    /// <summary>
    /// Name: DbType
    /// Description: Class to define the different database types and connectors
    /// Author: Ricardo Sanchez Romero (RicardoSanchezRomero@outlook.es)
    /// LogBook:
    ///     12/04/2018: Creation
    /// </summary>
    public class DbType
    {
        #region Attributes
        /// <summary>
        /// Type for SqlServer Database instance
        /// </summary>
        public readonly static Type SqlServer;
        /// <summary>
        /// Type for MySql Database instance
        /// </summary>
        public readonly static Type MySql;
        /// <summary>
        /// Type for Oracle Database instance
        /// </summary>
        public readonly static Type Oracle;
        #endregion

        #region Static Construct
        /// <summary>
        /// Static Construct
        /// </summary>
        static DbType()
        {
            DbType.SqlServer = Type.GetType("System.Data.SqlClient.SqlConnection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            DbType.Oracle = Type.GetType("Oracle.ManagedDataAccess.Client.OracleConnection, Oracle.ManagedDataAccess, Version=4.121.2.0, Culture=neutral, PublicKeyToken=89b483f429c47342");
            DbType.MySql = Type.GetType("MySql.Data.MySqlClient.MySqlConnection, MySql.Data, Version=6.9.9.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d");
        }
        #endregion
    }
}
