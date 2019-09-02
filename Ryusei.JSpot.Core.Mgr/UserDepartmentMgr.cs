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
    /// Name: UserDepartmentMgr
    /// Description: Manager class to implement the behavior of IUserDepartmentMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-28: Creation
    /// </summary>
    public class UserDepartmentMgr : IUserDepartmentMgr 
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static UserDepartmentMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private UserDepartmentDAO UserDepartmentDAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static UserDepartmentMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private UserDepartmentMgr()
        {
            this.UserDepartmentDAO = new UserDepartmentDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static UserDepartmentMgr GetInstance()
        {
            return Singleton ?? (Singleton = new UserDepartmentMgr());
        }
        #endregion

        #region [Methods]

        /// <summary>
        /// Name: GetByUserId
        /// Description: Method to get
        /// </summary>
        /// <param name="userId">UserId</param>
        public IEnumerable<UserDepartment> GetByUserIdEventGroupId(Guid userId, Guid eventGroupId)
        {
            // Define filter
            string filter = "U.UserId = @UserId and U.Active = @Active and EVT.Active = @Active";
            // Define order
            string order = "";
            // Define params
            object @params = new { UserId = userId, Active = true };
            // return the results
            return this.UserDepartmentDAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByUserIdEventId
        /// Description: Method to get UserDepartment by userId and eventId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="eventId">eventId</param>
        /// <returns></returns>
        public IEnumerable<UserDepartment> GetByUserIdEventId(Guid userId, Guid eventId)
        {
            // Define filter
            string filter = "U.UserId = @UserId and EVT.EventId = @EventId and U.Active = @Active and EVT.Active = @Active";
            // Define order
            string order = "";
            // Define params
            object @params = new { UserId = userId, EventId = eventId, Active = true };
            // return the results
            return this.UserDepartmentDAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of UserDepartment
        /// </summary>
        /// <param name="collectionUserDepartment">CollectionUserDepartment</param>
        public void Save(IEnumerable<UserDepartment> collectionUserDepartment)
        {
            // Save the relation
            this.UserDepartmentDAO.Save(collectionUserDepartment);
        }
        #endregion
    }
}
