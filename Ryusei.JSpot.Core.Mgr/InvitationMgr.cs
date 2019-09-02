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
    /// Name: InvitationMgr
    /// Description: Manager class to implement the behavior of IInvitationMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class InvitationMgr : IInvitationMgr
    {
        #region [Constants]

        private const string ERROR_INVITATION_NOT_FOUND = "Jspot.Core.Mgr.InvitationMgr.ErrorInvitationNotFound";

        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static InvitationMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private InvitationDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static InvitationMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private InvitationMgr()
        {
            this.DAO = new InvitationDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static InvitationMgr GetInstance()
        {
            return Singleton ?? (Singleton = new InvitationMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEmail
        /// Description: Method to get a collection of Invitations by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Collection Invitation</returns>
        public IEnumerable<Invitation> GetByEmail(string email)
        {
            // Define filter
            string filter = "I.ResponseDate is null and EVT.Active = @Active and I.Email = @Email and EVT.StartDate > @CurrentDate";
            // Define order
            string order = "I.SendDate desc";
            // Define params
            object @params = new { Active = true, Email = email, CurrentDate = DateTime.Now.ToUniversalTime() };
            // return results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetById
        /// Description: Method to get invitation by id
        /// </summary>
        /// <param name="invitationId">InvitationId</param>
        /// <returns></returns>
        public Invitation GetById(Guid invitationId)
        {
            // Define filter
            string filter = "I.ResponseDate is null and EVT.Active = @Active and I.InvitationId = @InvitationId";
            // Define order
            string order = "I.SendDate desc";
            // Define params
            object @params = new { Active = true, InvitationId = invitationId };
            // Get  results
            IEnumerable<Invitation> results = this.DAO.Select(filter: filter, order: order, @params: @params);
            // return the result
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }

        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of invitations by event id
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        public IEnumerable<Invitation> GetByEventId(Guid eventId)
        {
            // Define filter 
            string filter = "EVT.Active = @Active and EVT.EventId = @EventId";
            // Define order
            string order = "I.Email";
            // Define params
            object @params = new { EventId = eventId, Active = true };
            // return the result
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of invitations
        /// </summary>
        /// <param name="collectionInvitations">Collection Invitations</param>
        public void Save(IEnumerable<Invitation> collectionInvitations)
        {
            this.DAO.Save(collectionInvitations);
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate invitation
        /// </summary>
        /// <param name="invitationId">InvitationId</param>
        /// <param name="answer">answer</param>
        public void Deactivate(Guid invitationId, bool answer)
        {
            // check is invitation exist
            if (this.GetById(invitationId) == null)
                throw new ManagerException(ERROR_INVITATION_NOT_FOUND, new System.Exception(string.Format("Invitation with Id: {0} was not found to deactivate", invitationId)));
            // Deactivate
            this.DAO.Deactivate(invitationId, answer);
        }
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete invitations
        /// </summary>
        /// <param name="collectionInvitations">Collection Invitations</param>
        public void Delete(IEnumerable<Invitation> collectionInvitations)
        {
            this.DAO.Delete(collectionInvitations);
        }
        #endregion
    }
}
