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
    /// Name: RoleMgr
    /// Description: Manager class to implement the behavior of Role
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class RoleMgr : IRoleMgr
    {
        #region [Constants]
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static RoleMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// Data access object
        /// </summary>
        private RoleDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static RoleMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private RoleMgr()
        {
            this.DAO = new RoleDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static RoleMgr GetInstance()
        {
            return Singleton ?? (Singleton = new RoleMgr());
        }
        #endregion

        #region [Methods]

        /// <summary>
        /// Name: Get
        /// Description: Method to get a list of role
        /// </summary>
        /// <returns>Collection of Role</returns>
        public IEnumerable<Role> Get()
        {
            // Define filter
            string filter = "Active = @Active";
            // Define order
            string order = "Name desc";
            // Define parameters
            object @params = new { Active = true };
            // Get the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetById
        /// Description: Method to get a role by id
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <returns>Role</returns>
        public Role GetById(Guid roleId)
        {
            // Define filter
            string filter = "RoleId = @RoleId and Active = @Active";
            // Define parameters
            object @params = new { RoleId = roleId, Active = true };
            // Get the results
            List<Role> results = this.DAO.Select(filter: filter, @params: @params).ToList();
            // Return the results
            return results.Count > 0 ? results[0] : null;
        }
        /// <summary>
        /// Name: GetDefaultRole
        /// Description: Method to get a role marked as defualt
        /// </summary>
        /// <returns>Role marked as default</returns>
        public Role GetDefaultRole()
        {
            // Define filter
            string filter = "IsDefault = @IsDefault and Active = @Active";
            // Define parameters
            object @params = new { IsDefault = true, Active = true };
            // Get the results
            List<Role> results = this.DAO.Select(filter: filter, @params: @params).ToList();
            // Return the results
            return results.Count > 0 ? results[0] : null;
        }
        #endregion
    }
}
