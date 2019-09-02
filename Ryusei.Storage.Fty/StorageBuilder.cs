using Ryusei.Storage.Fty.Section;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Storage.Fty
{
    /// <summary>
    /// Name: StorageBuilder
    /// Description: Builder class for Storage Mgr
    /// Author: Ricardo Sanchez Romero (ricardo.rsz.sanchez@faurecia.com)
    /// LogBook:
    ///     25/07/2018: Creation
    /// </summary>
    public class StorageBuilder
    {
        #region [Constants]
        public const string SECTION_NAME = "storageManagerSection";
        public const string ISTORAGEBLOBMGR = "IStorageBlobMgr";
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static StorageBuilder Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// CRMSection
        /// </summary>
        private StorageSection StorageSection { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static StorageBuilder()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private StorageBuilder()
        {
            this.StorageSection = ConfigurationManager.GetSection(SECTION_NAME) as StorageSection;
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static StorageBuilder GetInstance()
        {
            return Singleton == null ? Singleton = new StorageBuilder() : Singleton;
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
            string typeName = this.StorageSection.Instances[manager].Type;
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
