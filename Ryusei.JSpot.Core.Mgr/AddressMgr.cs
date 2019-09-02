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
    /// Name: AddressMgr
    /// Description: Manager class to implement the behavior of IAddressMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class AddressMgr : IAddressMgr
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static AddressMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private AddressDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static AddressMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private AddressMgr()
        {
            this.DAO = new AddressDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static AddressMgr GetInstance()
        {
            return Singleton ?? (Singleton = new AddressMgr());
        }
        #endregion

        #region [Methods]


        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get Address by EventId
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <returns></returns>
        public Address GetByEventId(Guid eventId)
        {
            // Define filter
            string filter = "EventId = @EventId and Active = @Active";
            // Define order
            string order = "";
            // Define params
            object @params = new { EventId = eventId, Active = true };
            // Get results
            IEnumerable<Address> result = this.DAO.Select(filter: filter, order: order, @params: @params);
            // return 
            return result.Count() > 0 ? result.ElementAt(0) : null;
        }

        /// <summary>
        /// Name: Save
        /// Description: Method to save an address
        /// </summary>
        /// <param name="address"></param>
        public void Save(Address address)
        {
            this.DAO.Save(address);
        }
        #endregion
    }
}
