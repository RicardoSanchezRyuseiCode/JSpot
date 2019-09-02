using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Contract
{
    /// <summary>
    /// Name: IUserMgr
    /// Description: Interface to define the behavior of UserMgr
    /// Author: Ricardo Sanchez Romero 
    /// LogBook:
    ///     2019/08/16: Creation
    /// </summary>
    public interface IUserMgr
    {
        /// <summary>
        /// Name: Get
        /// Description: Method to get a collection of UserData
        /// </summary>
        /// <returns>Collection of UserData</returns>
        IEnumerable<User> Get();
        /// <summary>
        /// Name: GetById
        /// Description: Method to get by Id
        /// </summary>
        /// <param name="userDataId">UserDataId</param>
        /// <returns>UserData</returns>
        User GetById(Guid userDataId);
        /// <summary>
        /// Name: GetByEmail
        /// Description: Method to get UserData by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>UserData</returns>
        User GetByEmail(string email);
        /// <summary>
        /// Name: Save
        /// Description: Method to save a userData
        /// </summary>
        /// <param name="user">UserData</param>
        void Save(User user);
        /// <summary>
        /// Name: Update
        /// Description: Method to update userData
        /// </summary>
        /// <param name="userData">UserData</param>
        void Update(User user);
        /// <summary>
        /// Name: UpdatePhoto
        /// Description: Method to update the photo of user
        /// </summary>
        /// <param name="userId">UserDataId</param>
        /// <param name="photo"></param>
        void UpdatePhoto(Guid userId, string photo);
        /// <summary>
        /// Name: UpdateIsValidated
        /// Description: Method to update IsValidated
        /// </summary>
        /// <param name="userId">UserDataId</param>
        /// <param name="IsValidated">IsValidated</param>
        void UpdateIsValidated(Guid userId, bool IsValidated);
        /// <summary>
        /// Name: UpdatePassword
        /// Description: Method to update password
        /// </summary>
        /// <param name="userId">UserDataId</param>
        /// <param name="password">Password</param>
        void UpdatePassword(Guid userId, string password);
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate a userData
        /// </summary>
        /// <param name="userId">UserDataId</param>
        void Deactivate(Guid userId);
    }
}
