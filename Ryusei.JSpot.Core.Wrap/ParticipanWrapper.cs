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
    public class ParticipanWrapper
    {
        #region [Constant]
        private const string EROR_GRUOP_FULL = "Jspot.Core.Wrap.ParticipantWrap.ErrorGroupFull";
        private const string ERROR_INVALID_USER_DEPARTMENT = "Jspot.Core.Wrap.ParticipantWrap.ErrorInvalidUserDepartment";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static ParticipanWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        private IEventMgr IEventMgr { get; set; }
        /// <summary>
        /// IEventGroupMgr
        /// </summary>
        private IEventGroupMgr IEventGroupMgr { get; set; }
        /// <summary>
        /// ParticipantMgr
        /// </summary>
        private IParticipantMgr IParticipantMgr { get; set; }
        /// <summary>
        /// IEventGroupDepartmentMgr
        /// </summary>
        private IEventGroupDepartmentMgr IEventGroupDepartmentMgr { get; set; }
        /// <summary>
        /// IUserDepartmentMgr
        /// </summary>
        private IUserDepartmentMgr IUserDepartmentMgr { get; set; }
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
        static ParticipanWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private ParticipanWrapper()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IEventMgr = coreBuilder.GetManager<IEventMgr>(CoreBuilder.IEVENTMGR);
            this.IEventGroupMgr = coreBuilder.GetManager<IEventGroupMgr>(CoreBuilder.IEVENTGROUPMGR);
            this.IEventGroupDepartmentMgr = coreBuilder.GetManager<IEventGroupDepartmentMgr>(CoreBuilder.IEVENTGROUPDEPARTMENTMGR);
            this.IAssistantMgr = coreBuilder.GetManager<IAssistantMgr>(CoreBuilder.IASSISTANTMGR);
            this.IParticipantMgr = coreBuilder.GetManager<IParticipantMgr>(CoreBuilder.IPARTICIPANTMGR);
            this.IUserDepartmentMgr = coreBuilder.GetManager<IUserDepartmentMgr>(CoreBuilder.IUSERDEPARTMENTMGR);

            this.EmailWrapper = EmailWrapper.GetInstance();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static ParticipanWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new ParticipanWrapper());
        }
        #endregion

        #region [Methods]

        /// <summary>
        /// Name: Create
        /// Description: Method to create participant
        /// </summary>
        /// <param name="participant">Participant to create</param>
        public void Create(Participant participant, string email, string name, string lastname)
        {
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope())
            {
                // Get event gorup
                EventGroup eventGroup = this.IEventGroupMgr.GetById(participant.EventGroupId);
                // Get event
                Event @event = this.IEventMgr.GetById(eventGroup.EventId);
                // Get assistants owners
                IEnumerable<Assistant> collectionAssistants = this.IAssistantMgr.GetByEventId(@event.EventId).Where(x => x.IsOwner);
                // Get list of participants
                IEnumerable<Participant> participants = this.IParticipantMgr.GetByEventGroupId(participant.EventGroupId);
                // Check if have capacity
                if (eventGroup.Capacity - participants.Count() <= 0)
                    throw new WrapperException(EROR_GRUOP_FULL, new System.Exception("Group is full"));
                // Get the departments of event group
                IEnumerable<EventGroupDepartment> DepartmentsOfGroup = this.IEventGroupDepartmentMgr.GetByEventGroupId(participant.EventGroupId);
                IEnumerable<UserDepartment> DepartmentsOfUser = this.IUserDepartmentMgr.GetByUserIdEventGroupId(participant.UserId, participant.EventGroupId);
                // With departments of group check if some department of user is valid for the group
                bool flag = false;
                foreach (UserDepartment userDepartment in DepartmentsOfUser)
                {
                    foreach (EventGroupDepartment eventGroupDepartment in DepartmentsOfGroup)
                    {
                        if (userDepartment.DepartmentId == eventGroupDepartment.DepartmentId)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                    throw new WrapperException(ERROR_INVALID_USER_DEPARTMENT, new System.Exception("User is not added to some valid group"));
                // if all is valid save the paticipant
                this.IParticipantMgr.Save(participant);
                // Send email of sign in 
                this.EmailWrapper.SendMailSignIn(eventGroup, @event, email, name, lastname);
                // Send email to owners
                this.EmailWrapper.SendMailSignInOwners(eventGroup, @event, email, name, lastname, collectionAssistants);
                // Get participants again to check the capacity
                // Get list of participants
                participants = this.IParticipantMgr.GetByEventGroupId(participant.EventGroupId);
                // Check if have capacity
                if (eventGroup.Capacity - participants.Count() <= 0)
                    this.EmailWrapper.SendMailGroupFull(eventGroup, @event, collectionAssistants);
                // Complete the scope
                scope.Complete();
            }
        }

        #endregion
    }
}
