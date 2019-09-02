using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Fty;
using Ryusei.JSpot.Auth.Fty.Contract;
using Ryusei.JSpot.Auth.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Wrap
{
    /// <summary>
    /// Name: MenuItemController
    /// Description: Wrapper for logic of MenuItem
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class MenuItemWrapper
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static MenuItemWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// IDataabaseMgr
        /// </summary>
        private IMenuItemMgr IMenuItemMgr { get; set; }
        /// <summary>
        /// IRoleMenuItemMgr
        /// </summary>
        private IRoleMenuItemMgr IRoleMenuItemMgr { get; set; }
        /// <summary>
        /// IUserDataRoleApplicationMgr
        /// </summary>
        private IUserRoleMgr IUserRoleMgr { get; set; }
        /// <summary>
        /// IUserMgr
        /// </summary>
        private IUserMgr IUserMgr { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static MenuItemWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private MenuItemWrapper()
        {
            AuthBuilder authBuilder = AuthBuilder.GetInstance();
            this.IUserMgr = authBuilder.GetManager<IUserMgr>(AuthBuilder.IUSERMGR);
            this.IMenuItemMgr = authBuilder.GetManager<IMenuItemMgr>(AuthBuilder.IMENUITEMMGR);
            this.IRoleMenuItemMgr = authBuilder.GetManager<IRoleMenuItemMgr>(AuthBuilder.IROLEMENUITEMMGR);
            this.IUserRoleMgr = authBuilder.GetManager<IUserRoleMgr>(AuthBuilder.IUSERROLEMGR);
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static MenuItemWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new MenuItemWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: BuildStructure
        /// Description: Method to build the structure of menu
        /// </summary>
        /// <param name="listMenuItem">ListMenuItem</param>
        /// <param name="menuItemFatherId">MenuItemFatherId</param>
        /// <returns>Collection of menuItem View</returns>
        private IEnumerable<MenuItemView> BuildStructure(IEnumerable<MenuItem> listMenuItem, Guid? menuItemFatherId)
        {
            // Get child elements of father
            IEnumerable<MenuItem> childElements = listMenuItem.Where(x => x.UpMenuItemId == menuItemFatherId);
            // Define results
            List<MenuItemView> listResult = new List<MenuItemView>();
            // loop in child elements
            foreach (MenuItem childElement in childElements)
            {
                listResult.Add(new MenuItemView()
                {
                    MenuItem = childElement,
                    ListMenuItem = BuildStructure(listMenuItem, childElement.MenuItemId)
                });
            }
            // return sorted list
            return listResult.OrderBy(x => x.MenuItem.Order);//.ThenBy(x => x.MenuItem.Weight);
        }
        /// <summary>
        /// Name: GetUserMenu
        /// Description: MEthod to get user menu by email and application
        /// </summary>
        /// <param name="emailUser">Email User</param>
        /// <param name="applicationId">Application Id</param>
        /// <returns></returns>
        public IEnumerable<MenuItemView> GetUserMenu(string emailUser)
        {
            // Get user
            User user = this.IUserMgr.GetByEmail(emailUser);
            // Get collection of roles
            IEnumerable<Role> listRole = this.IUserRoleMgr.GetRoleByUserId(user.UserId);
            // Get menu items
            IEnumerable<MenuItem> listMenuItem = this.IRoleMenuItemMgr.GetByRoleIds(listRole.Select(x => x.RoleId));
            // With menu items we need to arm the menu item view structure
            IEnumerable<MenuItemView> listMenuItemView = BuildStructure(listMenuItem, null);
            // return the result
            return listMenuItemView;
        }
        #endregion
    }
}
