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
    /// Name: ParticipantMgr
    /// Description: Manager class to implement the behavior of IParticipantMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class ParticipantMgr : IParticipantMgr
    {
        #region [Constants]
        public const string ERROR_USER_ALREADY_EXIST = "Jspot.Core.Mgr.ParticipantMgr.ErrorAlreadyExist";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static ParticipantMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private ParticipantDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static ParticipantMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private ParticipantMgr()
        {
            this.DAO = new ParticipantDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static ParticipantMgr GetInstance()
        {
            return Singleton ?? (Singleton = new ParticipantMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEventGroupId
        /// Description: Method to get participant by EventGroupId
        /// </summary>
        /// <param name="eventGroupId">EventGroupId</param>
        /// <returns></returns>
        public IEnumerable<Participant> GetByEventGroupId(Guid eventGroupId)
        {
            // Define filter
            string filter = "P.EventGroupId = @EventGroupId and U.Active = @Active";
            // Define order
            string order = "U.Email";
            // Define params
            object @params = new { EventGroupId = eventGroupId, Active = true };
            // return
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByIds
        /// Description: Method to get a collection of Participant
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="eventGroupId">EventGroupId</param>
        /// <returns>CollectionParticipant</returns>
        public IEnumerable<Participant> GetByIds(Guid userId, Guid eventGroupId)
        {
            // Define filter
            string filter = " P.EventGroupId = @EventGroupId and U.Active = @Active and U.UserId = @UserId";
            // Define order
            string order = "U.Email";
            // Define params
            object @params = new { EventGroupId = eventGroupId, UserId = userId, Active = true };
            // return
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }

        /// <summary>
        /// Name: Save
        /// Description: Method to save a participant
        /// </summary>
        /// <param name="participant">Participant</param>
        public void Save(Participant participant)
        {
            // Check if already exist
            if (this.GetByIds(participant.UserId, participant.EventGroupId).Count() > 0)
                throw new ManagerException(ERROR_USER_ALREADY_EXIST, new System.Exception("Participant already exist"));
            // Save participant
            this.DAO.Save(participant);
        }
        #endregion
    }
}
