using Ryusei.JSpot.Auth.Ent;
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
    /// Name: InvitationWrapper
    /// Description: Wrapper class for Invitation
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// </summary>
    public class InvitationWrapper
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static InvitationWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private IInvitationMgr IInvitationMgr { get; set; }
        /// <summary>
        /// IAssistantMgr
        /// </summary>
        private IAssistantMgr IAssistantMgr { get; set; }
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IEventMgr IEventMgr { get; set; }
        /// <summary>
        /// EmailWrapper
        /// </summary>
        private EmailWrapper EmailWrapper { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static InvitationWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private InvitationWrapper()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IInvitationMgr = coreBuilder.GetManager<IInvitationMgr>(CoreBuilder.IINVITATIONMGR);
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
        public static InvitationWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new InvitationWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Accept
        /// Description: Method to accept an invitation
        /// </summary>
        /// <param name="invitationId"></param>
        /// <param name="userId"></param>
        public void Accept(Guid invitationId, Guid userId)
        {
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Get the invitation
                Invitation invitation = this.IInvitationMgr.GetById(invitationId);
                // Deactivate the invitation
                this.IInvitationMgr.Deactivate(invitationId, true);
                // Save relation of event and user
                this.IAssistantMgr.Save(new Assistant()
                {
                    EventId = invitation.Event.EventId,
                    UserId = userId,
                    IsOwner = false
                });
                // Complete the scope
                scope.Complete();
            }
        }
        /// <summary>
        /// Name: Reject
        /// Description: Method to reject an invitation
        /// </summary>
        /// <param name="invitationId">InvitationId</param>
        /// <param name="userId">UserId</param>
        public void Reject(Guid invitationId, string rejectedUsername, string rejectedUserLastname, string rejectedEmail)
        {
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Get the invitation
                Invitation invitation = this.IInvitationMgr.GetById(invitationId);
                // Deactivate the invitation
                this.IInvitationMgr.Deactivate(invitationId, false);
                // Get the owners of event to notify thereject
                IEnumerable<User> collectionOwners = this.IEventMgr.GetOwners(invitation.EventId);
                // Send email
                this.EmailWrapper.SendMailInvitationReject(collectionOwners, rejectedUsername, rejectedUserLastname, rejectedEmail, invitation.Event);
                // Complete the scope
                scope.Complete();
            }
        }
        /// <summary>
        /// Name: Import
        /// Description: Method to import a list of emails
        /// </summary>
        /// <param name="emails">Emails</param>
        /// <param name="eventId">EventId</param>
        public void Import(IEnumerable<string> emails, Guid eventId)
        {
            // open trasaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Get event information
                Event @event = this.IEventMgr.GetById(eventId);
                // Get current invitations
                IEnumerable<Invitation> currentInvitations = this.IInvitationMgr.GetByEventId(eventId);
                Dictionary<string, Invitation> dicCurrentInvitations = new Dictionary<string, Invitation>();
                foreach (Invitation invitation in currentInvitations)
                {
                    dicCurrentInvitations.Add(invitation.Email, invitation);
                }
                // Loop in emails list to create invitation
                ICollection<Invitation> collectionInvitations = new List<Invitation>();
                ICollection<Invitation> collectionToDelete = new List<Invitation>();
                foreach (string email in emails)
                {
                    // Check if invitation is not duplicate
                    if (!dicCurrentInvitations.ContainsKey(email))
                        collectionInvitations.Add(new Invitation()
                        {
                            Email = email,
                            EventId = eventId,
                            Name = ""
                        });
                    else {
                        // check status of invitation
                        Invitation invitation = dicCurrentInvitations[email];
                        if (invitation.ResponseDate != null && !invitation.Answer)
                        {
                            // if invitation was rejected can send again invitation
                            collectionInvitations.Add(new Invitation()
                            {
                                Email = email,
                                EventId = eventId,
                                Name = ""
                            });
                            collectionToDelete.Add(invitation);
                        }
                    }
                }
                // Delete invitations
                this.IInvitationMgr.Delete(collectionToDelete);
                // Save invitations
                this.IInvitationMgr.Save(collectionInvitations);
                // Sent invitations
                if(collectionInvitations.Count > 0)
                    // Send emails
                    this.EmailWrapper.SendMailInvitations(collectionInvitations, @event);
                // complete the scope
                scope.Complete();
            }
        }
        #endregion
    }
}
