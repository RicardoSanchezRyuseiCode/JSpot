using Ryusei.JSpot.Core.Fty.Section;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty
{
    /// <summary>
    /// Name: CoreBuilder
    /// Description: Builder class to create Core Manager instances
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class CoreBuilder
    {
        #region [Constants]
        private const string SECTION_NAME = "coreManagerSection";

        public const string IADDRESSMGR = "IAddressMgr";
        public const string IASSISTANTMGR = "IAssistantMgr";
        public const string ICARMGR = "ICarMgr";
        public const string ICARIMAGEMGR = "ICarImageMgr";
        public const string ICOMMENTMGR = "ICommentMgr";
        public const string IDEPARTMENTMGR = "IDepartmentMgr";
        public const string IEVENTMGR = "IEventMgr";
        public const string IEVENTGROUPMGR = "IEventGroupMgr";
        public const string IEVENTGROUPDEPARTMENTMGR = "IEventGroupDepartmentMgr";
        public const string IINVITATIONMGR = "IInvitationMgr";
        public const string IPARTICIPANTMGR = "IParticipantMgr";
        public const string IPASSENGERMGR = "IPassengerMgr";
        public const string ITRANSPORTMGR = "ITransportMgr";
        public const string IUSERDEPARTMENTMGR = "IUserDepartmentMgr";
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static CoreBuilder Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// CRMSection
        /// </summary>
        private CoreSection CoreSection { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static CoreBuilder()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private CoreBuilder()
        {
            this.CoreSection = ConfigurationManager.GetSection(SECTION_NAME) as CoreSection;
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static CoreBuilder GetInstance()
        {
            return Singleton ?? (Singleton = new CoreBuilder());
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
            string typeName = this.CoreSection.Instances[manager].Type;
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
