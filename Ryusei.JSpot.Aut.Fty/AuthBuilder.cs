using Ryusei.JSpot.Auth.Fty.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace Ryusei.JSpot.Auth.Fty
{
    /// <summary>
    /// Name: AuthBuilder
    /// Description: Configuration section for web config
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class AuthBuilder
    {
        #region [Constants]
        public const string SECTION_NAME = "authManager";
        public const string IACCESSMGR = "IAccessMgr";
        public const string IMENUITEMMGR = "IMenuItemMgr";
        public const string IPERMISSIONMGR = "IPermissionMgr";
        public const string IROLEMENUITEMMGR = "IRoleMenuItemMgr";
        public const string IROLEMGR = "IRoleMgr";
        public const string IROLEPERMISSIONMGR = "IRolePermissionMgr";
        public const string IUSERMGR = "IUserMgr";
        public const string IUSERROLEMGR = "IUserRoleMgr";
        public const string ITOKENTRACKINGMGR = "ITokenTrackingMgr";
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static AuthBuilder Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// CRMSection
        /// </summary>
        private AuthSection CRMSection { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static AuthBuilder()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private AuthBuilder()
        {
            this.CRMSection = ConfigurationManager.GetSection(SECTION_NAME) as AuthSection;
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static AuthBuilder GetInstance()
        {
            return Singleton == null ? Singleton = new AuthBuilder() : Singleton;
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
            string typeName = this.CRMSection.Instances[manager].Type;
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
