using Ryusei.Logger.Fty.Section;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Logger.Fty
{
    public class LoggerBuilder
    {
        #region [Constants]
        public const string SECTION_NAME = "loggerManagerSection";
        public const string ISYSTEMLOGMGR = "ISystemLogMgr";
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static LoggerBuilder Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// CRMSection
        /// </summary>
        private LoggerSection LoggerSection { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static LoggerBuilder()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private LoggerBuilder()
        {
            this.LoggerSection = ConfigurationManager.GetSection(SECTION_NAME) as LoggerSection;
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static LoggerBuilder GetInstance()
        {
            return Singleton == null ? Singleton = new LoggerBuilder() : Singleton;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetManager
        /// Description: Method to create and get a manager
        /// </summary>
        /// <typeparam name="T">Type of manager</typeparam>
        /// <param name="manager">Manager</param>
        /// <returns></returns>
        public T GetManager<T>(string manager)
        {
            // Get the definition of manager from configuration
            string typeName = this.LoggerSection.Instances[manager].Type;
            // Get the type
            Type type = Type.GetType(typeName);
            // Get definition of method info
            MethodInfo methodInfo = type.GetMethod("GetInstance");
            // execute the method and return the result
            return (T)methodInfo.Invoke(null, null);
        }
        #endregion
    }
}
