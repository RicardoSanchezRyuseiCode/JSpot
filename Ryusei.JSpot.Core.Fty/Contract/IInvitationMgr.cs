using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IInvitationMgr
    /// Descrpition: Interface to define the behavior of IInvitationMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IInvitationMgr
    {
        /// <summary>
        /// Name: GetByEmail
        /// Description: Method to get a collection of Invitations by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Collection Invitation</returns>
        IEnumerable<Invitation> GetByEmail(string email);
        /// <summary>
        /// Name: GetById
        /// Description: Method to get invitation by id
        /// </summary>
        /// <param name="invitationId">InvitationId</param>
        /// <returns></returns>
        Invitation GetById(Guid invitationId);
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of invitations by event id
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        IEnumerable<Invitation> GetByEventId(Guid eventId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of invitations
        /// </summary>
        /// <param name="collectionInvitations">Collection Invitations</param>
        void Save(IEnumerable<Invitation> collectionInvitations);
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate invitation
        /// </summary>
        /// <param name="invitationId">InvitationId</param>
        /// <param name="answer">Answer</param>
        void Deactivate(Guid invitationId, bool answer);
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete invitations
        /// </summary>
        /// <param name="collectionInvitations">Collection Invitations</param>
        void Delete(IEnumerable<Invitation> collectionInvitations);
    }
}
