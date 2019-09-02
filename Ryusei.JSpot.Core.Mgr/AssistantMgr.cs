using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty.Contract;
using Ryusei.JSpot.Core.Mgr.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Mgr
{
    /// <summary>
    /// Name: AssistantMgr
    /// Description: Manager class to implement the behavior of IAssistantMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class AssistantMgr : IAssistantMgr
    {
        #region [Constants]
        public const string ERROR_ASSISTANT_EXIST = "Jspot.Core.Mgr.AssistantMgr.ErrorAssistantExist";

        public const string ERROR_ASSISTANT_NOT_EXIST = "Jspot.Core.Mgr.AssistantMgr.ErrorAssistantNotExist";

        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static AssistantMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private AssistantDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static AssistantMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private AssistantMgr()
        {
            this.DAO = new AssistantDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static AssistantMgr GetInstance()
        {
            return Singleton ?? (Singleton = new AssistantMgr());
        }
        #endregion

        #region [Constants]

        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of Assistant by EventId
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        public IEnumerable<Assistant> GetByEventId(Guid eventId)
        {
            // Define filter
            string filter = "A.EventId = @EventId and U.Active = @Active";
            // Define order
            string order = "U.Name";
            // Define params
            object @params = new { EventId = eventId, Active = true };
            // return the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByIds
        /// Description: Method to get by ids
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="eventId">EventId</param>
        public Assistant GetByIds(Guid userId, Guid eventId)
        {
            // Define filter
            string filter = "U.UserId = @UserId and A.EventId = @EventId and U.Active = @Active";
            // Define order
            string order = "";
            // Define params
            object @params = new { UserId = userId, EventId = eventId, Active = true };
            // Get results
            IEnumerable<Assistant> results = this.DAO.Select(filter: filter, order: order, @params: @params);
            // return
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save assistant
        /// </summary>
        /// <param name="assistant">Assistant</param>
        public void Save(Assistant assistant)
        {
            // Check if relation already exist
            if (this.GetByIds(assistant.UserId, assistant.EventId) != null)
                throw new ManagerException(ERROR_ASSISTANT_EXIST, new System.Exception(string.Format("The user with Id: {0}, is already an assistant for event: {1}", assistant.UserId, assistant.EventId)));
            // If relation not exist save
            this.DAO.Save(assistant);
        }

        /// <summary>
        /// Name: Update
        /// Descrition : Method to update assistan
        /// </summary>
        /// <param name="assistant">Asisstant</param>
        public void Update(Assistant assistant)
        {
            // Check if relation already exist
            if (this.GetByIds(assistant.UserId, assistant.EventId) == null)
                throw new ManagerException(ERROR_ASSISTANT_NOT_EXIST, new System.Exception(string.Format("The user with Id: {0}, not found for event: {1}", assistant.UserId, assistant.EventId)));
            // Update assistant
            this.DAO.Update(assistant);
        }
        #endregion
    }
}
