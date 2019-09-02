using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IUserDepartmentMgr
    /// Description: Interface to define behavior of IUserDepartmentMgr
    /// Author: Ricardo Sanchez Romero (ricardo.rsz.sanchez@faurecia.com)
    /// LogBook:
    ///     2019-08-28: Creation
    /// </summary>
    public interface IUserDepartmentMgr
    {
        /// <summary>
        /// Name: GetByUserId
        /// Description: Method to get
        /// </summary>
        /// <param name="userId">UserId</param>
        IEnumerable<UserDepartment> GetByUserIdEventGroupId(Guid userId, Guid eventGroupId);
        /// <summary>
        /// Name: GetByUserIdEventId
        /// Description: Method to get UserDepartment by userId and eventId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="eventId">eventId</param>
        /// <returns></returns>
        IEnumerable<UserDepartment> GetByUserIdEventId(Guid userId, Guid eventId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of UserDepartment
        /// </summary>
        /// <param name="collectionUserDepartment">CollectionUserDepartment</param>
        void Save(IEnumerable<UserDepartment> collectionUserDepartment);
    }
}
