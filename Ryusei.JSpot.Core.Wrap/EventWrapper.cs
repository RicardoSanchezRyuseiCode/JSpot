using Ryusei.JSpot.Core.Fty;
using Ryusei.JSpot.Core.Fty.Contract;
using Ryusei.JSpot.Core.Prm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ryusei.JSpot.Core.Wrap
{
    /// <summary>
    /// Name: EventWrapper
    /// Description: Wrapper for Event
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-24: Creation
    /// </summary>
    public class EventWrapper
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static EventWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        private IEventMgr IEventMgr { get; set; }
        /// <summary>
        /// IAddressMgr
        /// </summary>
        private IAddressMgr IAddressMgr { get; set; }
        /// <summary>
        /// IDepartmentMgr
        /// </summary>
        private IDepartmentMgr IDepartmentMgr { get; set; }
        /// <summary>
        /// IEventGroupMgr
        /// </summary>
        private IEventGroupMgr IEventGroupMgr { get; set; }
        /// <summary>
        /// IEventGroupDepartmentMgr
        /// </summary>
        private IEventGroupDepartmentMgr IEventGroupDepartmentMgr { get; set; }
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
        static EventWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private EventWrapper()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IEventMgr = coreBuilder.GetManager<IEventMgr>(CoreBuilder.IEVENTMGR);
            this.IAddressMgr = coreBuilder.GetManager<IAddressMgr>(CoreBuilder.IADDRESSMGR);
            this.IDepartmentMgr = coreBuilder.GetManager<IDepartmentMgr>(CoreBuilder.IDEPARTMENTMGR);
            this.IEventGroupMgr = coreBuilder.GetManager<IEventGroupMgr>(CoreBuilder.IEVENTGROUPMGR);
            this.IEventGroupDepartmentMgr = coreBuilder.GetManager<IEventGroupDepartmentMgr>(CoreBuilder.IEVENTGROUPDEPARTMENTMGR);
            this.IAssistantMgr = coreBuilder.GetManager<IAssistantMgr>(CoreBuilder.IASSISTANTMGR);

            this.EmailWrapper = EmailWrapper.GetInstance();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static EventWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new EventWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Create
        /// Description: Method to create and event
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="eventCreatePrm">EventCreatePrm</param>
        public void Create(Guid userId, EventCreatePrm eventCreatePrm)
        {
            // Open transaction
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Create the event
                this.IEventMgr.Save(eventCreatePrm.Event);
                // Create the assistant
                this.IAssistantMgr.Save(new Ent.Assistant()
                {
                    EventId = eventCreatePrm.Event.EventId,
                    UserId = userId,
                    IsOwner = true
                });
                // Create the address
                eventCreatePrm.Address.EventId = eventCreatePrm.Event.EventId;
                this.IAddressMgr.Save(eventCreatePrm.Address);
                // Create the department
                foreach (Ent.Department department in eventCreatePrm.CollectionDepartment)
                {
                    department.EventId = eventCreatePrm.Event.EventId;
                }
                this.IDepartmentMgr.Save(eventCreatePrm.CollectionDepartment);
                // Create dictionary of deparment
                Dictionary<string, Guid> dicDepartment = new Dictionary<string, Guid>();
                foreach (Ent.Department department in eventCreatePrm.CollectionDepartment)
                {
                    dicDepartment.Add(department.Name.ToUpper(), department.DepartmentId);
                }
                // Create event group
                foreach (EventGroupCreatePrm eventGroupCreatePrm in eventCreatePrm.CollectionEventGroupCreatePrm)
                {
                    eventGroupCreatePrm.EventGroup.EventId = eventCreatePrm.Event.EventId;
                }
                this.IEventGroupMgr.Save(eventCreatePrm.CollectionEventGroupCreatePrm.Select(x => x.EventGroup));
                // Create event group department relation
                ICollection<Ent.EventGroupDepartment> collectionEventGroupDepartment = new List<Ent.EventGroupDepartment>();
                foreach (EventGroupCreatePrm eventGroupCreatePrm in eventCreatePrm.CollectionEventGroupCreatePrm)
                {
                    foreach (Ent.Department department in eventGroupCreatePrm.CollectionDepartment)
                    {
                        collectionEventGroupDepartment.Add(new Ent.EventGroupDepartment()
                        {
                            EventGroupId = eventGroupCreatePrm.EventGroup.EventGroupId,
                            DepartmentId = dicDepartment[department.Name.ToUpper()]
                        });
                    }
                }
                this.IEventGroupDepartmentMgr.Save(collectionEventGroupDepartment);
                // Complete the scope
                scope.Complete();
            }
        }
        #endregion
    }
}
