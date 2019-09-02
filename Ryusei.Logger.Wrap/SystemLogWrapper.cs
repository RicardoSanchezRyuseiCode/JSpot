using Ryusei.Logger.Ent;
using Ryusei.Logger.Fty;
using Ryusei.Logger.Fty.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Logger.Wrap
{
    /// <summary>
    /// Name: SystemLogWrapper
    /// Description: Wrapper class to encapsulate the behavior of SystemLoggerWrapper
    /// Author: Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class SystemLogWrapper
    {
        #region [Constants]
        public const string TYPE_INFO = "INFO";

        public const string TYPE_WARNING = "WARNING";

        public const string TYPE_ERROR = "ERROR";

        public const string TYPE_DEBUG = "DEBUG";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static SystemLogWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// IDataabaseMgr
        /// </summary>
        private ISystemLogMgr ISystemLogMgr { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static SystemLogWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private SystemLogWrapper()
        {
            LoggerBuilder loggerBuilder = LoggerBuilder.GetInstance();
            this.ISystemLogMgr = loggerBuilder.GetManager<ISystemLogMgr>(LoggerBuilder.ISYSTEMLOGMGR);
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static SystemLogWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new SystemLogWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Register
        /// Description: Method to register an entry in the log
        /// </summary>
        /// <param name="server">Server</param>
        /// <param name="companyDataId">CompanyDataId</param>
        /// <param name="userDataId">UserDataId</param>
        /// <param name="source">Source</param>
        /// <param name="type">Type</param>
        /// <param name="exception">Exception</param>
        public void Register(string server, Guid userDataId, string type, Exception exception)
        {
            // Define object
            SystemLog systemLog = new SystemLog();
            // Assign basic properties
            systemLog.Server = server;
            systemLog.UserDataId = userDataId;
            systemLog.Type = type;
            systemLog.Message = exception.Message;
            systemLog.StackTrace = exception.StackTrace;
            if (exception.InnerException != null)
            {
                systemLog.InnerMessage = exception.InnerException.Message;
                systemLog.InnerStackTrace = exception.InnerException.StackTrace;
            }
            // Save the entry to log
            this.ISystemLogMgr.Save(systemLog);
        }
        #endregion
    }
}
