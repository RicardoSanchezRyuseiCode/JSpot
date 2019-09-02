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
    /// Name: UserMgr
    /// Description: Manager class to implement the behavior of User
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class UserMgr : IUserMgr
    {
        #region [Constants]
        private const string SAVE_EMAIL_EXIST = "Ryusei.CRM.Mgr.Error.UserMgr.EmailAlreadyExist";
        private const string UPDATE_USER_NOT_FOUND = "Ryusei.CRM.Mgr.Error.UserMgr.UpdateNotFound";
        private const string UPDATE_PHOTO_NOT_FOUND = "Ryusei.CRM.Mgr.Error.UserMgr.UpdatePhotoNotFound";
        private const string UPDATE_IS_VALIDATED_NOT_FOUND = "Ryusei.CRM.Mgr.Error.UserMgr.UpdateIsValidatedNotFound";
        private const string UPDATE_PASSWORD_NOT_FOUND = "Ryusei.CRM.Mgr.Error.UserMgr.UpdatePasswordNotFound";
        private const string DELETE_NOT_FOUND = "Ryusei.CRM.Mgr.Error.UserMgr.DeactivateNotFound";
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static UserMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// Data access object
        /// </summary>
        private UserDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static UserMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private UserMgr()
        {
            this.DAO = new UserDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static UserMgr GetInstance()
        {
            return Singleton == null ? Singleton = new UserMgr() : Singleton;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Get
        /// Description: Method to get a collection of UserData
        /// </summary>
        /// <returns>Collection of UserData</returns>
        public IEnumerable<User> Get()
        {
            // Define filter
            string filter = "Active = @Active";
            // Define order
            string order = "Lastname";
            // Define parameters
            object @params = new { Active = true };
            // return the resutls
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetById
        /// Description: Method to get by Id
        /// </summary>
        /// <param name="userId">UserDataId</param>
        /// <returns>UserData</returns>
        public User GetById(Guid userId)
        {
            // Define filter
            string filter = "UserId = @UserId and Active = @Active";
            // Define parameters
            object @params = new { UserId = userId, Active = true };
            // Get the resutls
            List<User> results = this.DAO.Select(filter: filter, @params: @params).ToList();
            // Return the result
            return results.Count > 0 ? results[0] : null;
        }
        /// <summary>
        /// Name: GetByEmail
        /// Description: Method to get UserData by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>UserData</returns>
        public User GetByEmail(string email)
        {
            // Define filter
            string filter = "LOWER(Email) = LOWER(@Email) and Active = @Active";
            // Define parameters
            object @params = new { Email = email, Active = true };
            // Get the resutls
            List<User> results = this.DAO.Select(filter: filter, @params: @params).ToList();
            // Return the result
            return results.Count > 0 ? results[0] : null;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a userData
        /// </summary>
        /// <param name="user">User</param>
        public void Save(User user)
        {
            // Check if the email is available
            if (this.GetByEmail(user.Email) != null)
                throw new ManagerException(SAVE_EMAIL_EXIST,
                                           new System.Exception(string.Format("A UserData with Email: {0}, already exist", user.Email)));
            // Save
            this.DAO.Save(user);
        }
        /// <summary>
        /// Name: Update
        /// Description: Method to update userData
        /// </summary>
        /// <param name="user">User</param>
        public void Update(User user)
        {
            // Check if exist by id
            if (this.GetById(user.UserId) == null)
                throw new ManagerException(UPDATE_USER_NOT_FOUND,
                                           new System.Exception(string.Format("A UserData with ID to update: {0}, was not found", user.Email)));
            // Update
            this.DAO.Update(user);
        }
        /// <summary>
        /// Name: UpdatePhoto
        /// Description: Method to update the photo of user
        /// </summary>
        /// <param name="userDataId">UserDataId</param>
        /// <param name="photo"></param>
        public void UpdatePhoto(Guid userId, string photo)
        {
            if (this.DAO.UpdatePhoto(userId, photo) <= 0)
                throw new ManagerException(UPDATE_PHOTO_NOT_FOUND,
                                           new System.Exception(string.Format("UserData with ID: {0}, was not found to update photo", userId)));
        }
        /// <summary>
        /// Name: UpdateIsValidated
        /// Description: Method to update IsValidated
        /// </summary>
        /// <param name="userId">UserDataId</param>
        /// <param name="isValidated">IsValidated</param>
        public void UpdateIsValidated(Guid userId, bool isValidated)
        {
            if (this.DAO.UpdateIsValidated(userId, isValidated) <= 0)
                throw new ManagerException(UPDATE_IS_VALIDATED_NOT_FOUND,
                                           new System.Exception(string.Format("UserData with ID: {0}, was not found to update Is Validated", userId)));
        }
        /// <summary>
        /// Name: UpdatePassword
        /// Description: Method to update password
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="password">Password</param>
        public void UpdatePassword(Guid userId, string password)
        {
            if (this.DAO.UpdatePassword(userId, password) <= 0)
                throw new ManagerException(UPDATE_PASSWORD_NOT_FOUND,
                                           new System.Exception(string.Format("UserData with ID: {0}, was not found to update password", userId)));
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate a userData
        /// </summary>
        /// <param name="userId">UserId</param>
        public void Deactivate(Guid userId)
        {
            if (this.DAO.Deactivate(userId) <= 0)
                throw new ManagerException(DELETE_NOT_FOUND,
                                           new System.Exception(string.Format("UserData with ID: {0}, was not found to delete", userId)));

        }
        #endregion
    }
}
