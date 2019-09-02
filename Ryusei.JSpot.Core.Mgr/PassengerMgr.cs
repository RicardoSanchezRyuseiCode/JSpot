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
    /// Name: ParticipantMgr
    /// Description: Manager class to implement the behavior of IParticipantMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class PassengerMgr : IPassengerMgr
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static PassengerMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private PassengerDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static PassengerMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private PassengerMgr()
        {
            this.DAO = new PassengerDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static PassengerMgr GetInstance()
        {
            return Singleton ?? (Singleton = new PassengerMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByTransportId
        /// Description: Method to get a collection of Passenger
        /// </summary>
        /// <param name="transportId">TransportId</param>
        /// <returns>Collection Passenger</returns>
        public IEnumerable<Passenger> GetByTransportId(Guid transportId)
        {
            // Define filter
            string filter = "P.TransportId = @TransportId and U.Active = @Active";
            // Define order
            string order = "U.Email";
            // Define params
            object @params = new { TransportId = transportId, Active = true };
            // return the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByPassengerId
        /// Description: Method to get a collection of passenger
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Passenger> GetByPassengerId(Guid userId)
        {
            // Define filter
            string filter = "P.UserId = @UserId and U.Active = @Active";
            // Define order
            string order = "U.Email";
            // Define params
            object @params = new { UserId = userId, Active = true };
            // return the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByUserIdEventIdSense
        /// Description: Method to get a collection of Passenger
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="travelSense"></param>
        /// <returns></returns>
        public Passenger GetByUserIdEventIdSense(Guid userId, Guid eventId, bool travelSense)
        {
            // Define filter
            string filter = "P.UserId = @UserId and T.EventId = @EventId and T.TravelSense = @TravelSense and U.Active = @Active";
            // Define order
            string order = "U.Email";
            // Define params
            object @params = new { UserId = userId, Active = true, EventId = eventId, TravelSense = travelSense };
            // return the results
            IEnumerable< Passenger > results = this.DAO.Select(filter: filter, order: order, @params: @params);
            // return 
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a passenger
        /// </summary>
        /// <param name="passenger">Passenger</param>
        public void Save(Passenger passenger)
        {
            this.DAO.Save(passenger);
        }
        #endregion
    }
}
