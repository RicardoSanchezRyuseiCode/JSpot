using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty;
using Ryusei.JSpot.Core.Fty.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ryusei.JSpot.Core.Wrap
{
    /// <summary>
    /// Name: AssistantWrapper
    /// Description: Wrapper class for assistant
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2016-08-27: Creation
    /// </summary>
    public class AssistantWrapper
    {
        #region [Constants]
        public const string ERROR_EMPTY_OWNERS = "Jspot.Core.Wrap.AssistantWrap.ErrorEmptyOwners";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static AssistantWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IEventMgr IEventMgr { get; set; }
        /// <summary>
        /// EmailWrapper
        /// </summary>
        private EmailWrapper EmailWrapper { get; set; }
        /// <summary>
        /// IAssistantMgr
        /// </summary>
        private IAssistantMgr IAssistantMgr { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static AssistantWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private AssistantWrapper()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IAssistantMgr = coreBuilder.GetManager<IAssistantMgr>(CoreBuilder.IASSISTANTMGR);
            this.IEventMgr = coreBuilder.GetManager<IEventMgr>(CoreBuilder.IEVENTMGR);

            this.EmailWrapper = EmailWrapper.GetInstance();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static AssistantWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new AssistantWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: UpdateOwner
        /// Description: Method to update information of owner
        /// </summary>
        /// <param name="assistant">Assistant</param>
        public void UpdateOwner(Assistant assistant)
        {
            // open transaction
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Get Assistan full information
                Assistant currentAssistant = this.IAssistantMgr.GetByIds(assistant.UserId, assistant.EventId);
                // Get event information
                Event @event = this.IEventMgr.GetById(assistant.EventId);
                // Update assistan
                this.IAssistantMgr.Update(assistant);
                // Afte update check if we have owners for the event if not trow exception
                IEnumerable<Assistant> collectionAssistant = this.IAssistantMgr.GetByEventId(assistant.EventId);
                if (collectionAssistant.FirstOrDefault(x => x.IsOwner) == null)
                    throw new WrapperException(ERROR_EMPTY_OWNERS, new System.Exception("The event need to have at least one owner"));
                // Check the king of update
                if (assistant.IsOwner)
                    // Send email added as owner
                    this.EmailWrapper.SendMailAddedAsOwner(currentAssistant.User, @event);
                else
                    this.EmailWrapper.SendMailRemoveAsOwner(currentAssistant.User, @event);
                // Complete scope
                scope.Complete();
            }
        }

        #endregion
    }
}
