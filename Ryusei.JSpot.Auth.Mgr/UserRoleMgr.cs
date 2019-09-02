using Ryusei.Exception;
using Ryusei.JSpot.Auth.Fty.Contract;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Mgr.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Mgr
{
    /// <summary>
    /// Name: UserRoleMgr
    /// Description: Manager class to implement the behavior of UserRole
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class UserRoleMgr : IUserRoleMgr
    {
        #region [Constants]
        private const string NOT_ALL_SAVE = "Ryusei.CRM.Mgr.Error.UserRoleMgr.NotAllSaved";
        private const string NOT_ALL_DELETE = "Ryusei.CRM.Mgr.Error.UserRoleMgr.NotAllDeleted";
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static UserRoleMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// Data access object
        /// </summary>
        private UserRoleDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static UserRoleMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private UserRoleMgr()
        {
            this.DAO = new UserRoleDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static UserRoleMgr GetInstance()
        {
            return Singleton ?? (Singleton = new UserRoleMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetRoleByUserDataId
        /// Description: Method to get collection Role by UserData id
        /// </summary>
        /// <param name="userDataId">UserDataId</param>
        /// <returns>Role</returns>
        public IEnumerable<Role> GetRoleByUserId(Guid userId)
        {
            // define filter
            string filter = "UDR.UserId = @UserId and R.Active = @Active";
            // define parameters
            object @params = new { UserId = userId, Active = true };
            // return the results
            return this.DAO.SelectRole(filter: filter, @params: @params);
        }
        /// <summary>
        /// Name: GetUserDataByRoleId
        /// Description: Method to get a collection of UserData by RoleId
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <returns>Collection of UserData</returns>
        public IEnumerable<User> GetUserByRoleId(Guid roleId)
        {
            // define filter
            string filter = "UDR.RoleId = @RoleId and UD.Active = @Active";
            // define parameters
            object @params = new { RoleId = roleId, Active = true };
            // return the results
            return this.DAO.SelectUser(filter: filter, @params: @params);
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to update userDataRole
        /// </summary>
        /// <param name="userDataRole">UserDataRole</param>
        public void Save(IEnumerable<UserRole> listUserRole)
        {
            int elementsSaved;
            if ((elementsSaved = this.DAO.Save(listUserRole)) < listUserRole.Count())
                throw new ManagerException(NOT_ALL_SAVE,
                                           new System.Exception(string.Format("Not all the User - Role relations was saved, Saved: {0}, Not Saved: {1}", elementsSaved, listUserRole.Count() - elementsSaved)));
        }
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete a list of userDataRole
        /// </summary>
        /// <param name="listUserRole">Collection of userDataRole</param>
        public void Delete(IEnumerable<UserRole> listUserRole)
        {
            int elementsDeleted;
            if ((elementsDeleted = this.DAO.Delete(listUserRole)) < listUserRole.Count())
                throw new ManagerException(NOT_ALL_DELETE,
                                           new System.Exception(string.Format("Not all the User - Role relations was deleted, Saved: {0}, Not Saved: {1}", elementsDeleted, listUserRole.Count() - elementsDeleted)));
        }
        #endregion
    }
}
