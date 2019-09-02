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
    /// Name: MenuItemMgr
    /// Description: Manager class to implement the behavior of MenuItem
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class MenuItemMgr : IMenuItemMgr
    {
        #region [Constants]
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static MenuItemMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private MenuItemDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static MenuItemMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private MenuItemMgr()
        {
            this.DAO = new MenuItemDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static MenuItemMgr GetInstance()
        {
            return Singleton ?? (Singleton = new MenuItemMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Get
        /// Description: Method to get a collection of elements
        /// </summary>
        /// <returns>Collection of MenuItem</returns>
        public IEnumerable<MenuItem> Get()
        {
            // Define filter
            string filter = "Active = @Active";
            // Define params
            object @params = new { Active = true };
            // return the results
            return this.DAO.Select(filter: filter, @params: @params);
        }
        #endregion
    }
}
