using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty;
using Ryusei.JSpot.Core.Fty.Contract;
using Ryusei.JSpot.Core.Prm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ryusei.JSpot.Core.Wrap
{
    /// <summary>
    /// Name: UserDepartmentWrapper
    /// Description: Wrapper class for UserDepartment
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-29
    /// </summary>
    public class UserDepartmentWrapper
    {
        #region [Constants]
        private const string ERROR_RELATION_ALREADY_EXIST = "Jspot.Core.Wrap.UserDepartmentWrap.ErrorRelationAlreadyExist";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static UserDepartmentWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// IUserDepartmentMgr
        /// </summary>
        private IUserDepartmentMgr IUserDepartmentMgr { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static UserDepartmentWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private UserDepartmentWrapper()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IUserDepartmentMgr = coreBuilder.GetManager<IUserDepartmentMgr>(CoreBuilder.IUSERDEPARTMENTMGR);
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static UserDepartmentWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new UserDepartmentWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Create
        /// Description: Method to create user department
        /// </summary>
        /// <param name="userDepartmentCreatePrm">UserDepartmentCreatePrm</param>
        public void Create(UserDepartmentCreatePrm userDepartmentCreatePrm)
        {
            // Open trasaction param
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Get deparments from usr
                IEnumerable<UserDepartment> collectionUserDepartment = this.IUserDepartmentMgr.GetByUserIdEventId(userDepartmentCreatePrm.UserId, userDepartmentCreatePrm.EventId);
                Dictionary<Guid, UserDepartment> dicUserDeparmtent = new Dictionary<Guid, UserDepartment>();
                foreach (UserDepartment userDepartment in collectionUserDepartment)
                {
                    dicUserDeparmtent.Add(userDepartment.DepartmentId, userDepartment);
                }
                // Check if are not repeated                
                foreach (Guid departmentId in userDepartmentCreatePrm.CollectionDepartmentId)
                {
                    if (dicUserDeparmtent.ContainsKey(departmentId))
                        throw new WrapperException(ERROR_RELATION_ALREADY_EXIST, new System.Exception("User Department Relation already exist"));
                }
                // Create collection of element
                ICollection<UserDepartment> collectionUserDepartmentToCreate = new List<UserDepartment>();
                foreach (Guid departmentId in userDepartmentCreatePrm.CollectionDepartmentId)
                {
                    collectionUserDepartmentToCreate.Add(new UserDepartment()
                    {
                        UserId = userDepartmentCreatePrm.UserId,
                        DepartmentId = departmentId
                    });
                }
                // Save the collection
                this.IUserDepartmentMgr.Save(collectionUserDepartmentToCreate);
                // Scope complete
                scope.Complete();
            }
        }
        #endregion
    }
}