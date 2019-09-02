using Ryusei.Exception;
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
    /// Name: TransportMgr
    /// Description: Manager class to implement the behavior of ITransportMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class TransportMgr : ITransportMgr
    {
        #region [Constants]
        public const string ERROR_NOT_FOUND = "Jspot.Core.Mgr.TransportMgr.ErrorNotFound";
        public const string ERROR_ALREADY_EXIST = "Jspot.Core.Mgr.TransportMgr.ErrorAlreadyExist";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static TransportMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private TransportDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static TransportMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private TransportMgr()
        {
            this.DAO = new TransportDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static TransportMgr GetInstance()
        {
            return Singleton ?? (Singleton = new TransportMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetById
        /// Description: Method to get by id
        /// </summary>
        /// <param name="transportId">TransportId</param>
        /// <returns></returns>
        public Transport GetById(Guid transportId)
        {
            // Define filter
            string filter = "T.TransportId = @TransportId and T.Active = @Active and C.Active = @Active";
            // Define order
            string order = "T.DepartureDate";
            // Define params
            object @params = new { TransportId = transportId, Active = true };
            // Get the results
            IEnumerable<Transport> results = this.DAO.Select(filter: filter, order: order, @params: @params);
            // return
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get by event id
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        public IEnumerable<Transport> GetByEventId(Guid eventId)
        {
            // Define filter
            string filter = "T.EventId = @EventId and T.Active = @Active and C.Active = @Active";
            // Define order
            string order = "T.DepartureDate";
            // Define params
            object @params = new { EventId = eventId, Active = true };
            // retun the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByEventIdCarIdSense
        /// Description: Method to get a transport  by eventId, carID and travel sense
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <param name="carId">CarId</param>
        /// <param name="travelSense">TravelSense</param>
        /// <returns></returns>
        public Transport GetByEventIdCarIdSense(Guid eventId, Guid carId, bool travelSense)
        {
            // Define filter
            string filter = "T.EventId = @EventId and C.CarId = @CarId and T.TravelSense = @TravelSense and T.Active = @Active and C.Active = @Active";
            // Define order
            string order = "T.DepartureDate";
            // Define params
            object @params = new { EventId = eventId, CarId = carId, TravelSense = travelSense, Active = true };
            // Get the results
            IEnumerable<Transport> results = this.DAO.Select(filter: filter, order: order, @params: @params);
            // return the results
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get event id by travel sense
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <param name="travelSense">TravelSense</param>
        /// <returns></returns>
        public IEnumerable<Transport> GetByEventId(Guid eventId, bool travelSense)
        {
            // Define filter
            string filter = "T.EventId = @EventId and T.Active = @Active and T.TravelSense = @TravelSense and C.Active = @Active";
            // Define order
            string order = "T.DepartureDate";
            // Define params
            object @params = new { EventId = eventId, Active = true, TravelSense = travelSense };
            // retun the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of transport by collection of eventId
        /// </summary>
        /// <param name="collectionEventId">Collection of eventId</param>
        /// <returns></returns>
        public IEnumerable<Transport> GetByEventId(IEnumerable<Guid> collectionEventId)
        {
            // Check if we have elements
            if (collectionEventId.Count() <= 0)
                return new List<Transport>();
            // Define filter
            string filter = string.Format("T.EventId in ({0}) and T.Active = @Active", string.Join(",", collectionEventId.Select(x => string.Format("'{0}'", x))));
            // Define order
            string order = "T.DepartureDate";
            // Define params
            object @params = new { Active = true };
            // retun the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save transport
        /// </summary>
        /// <param name="transport">Transport</param>
        public void Save(Transport transport)
        {
            // Check if transport already exist
            if (this.GetByEventIdCarIdSense(transport.EventId, transport.CarId, transport.TravelSense) != null)
                throw new ManagerException(ERROR_ALREADY_EXIST, new System.Exception(string.Format("Tranport for event: {0}, with car: {1}, sense: {2}, already exist", transport.EventId, transport.CarId, transport.TravelSense)));
            this.DAO.Save(transport);
        }
        /// <summary>
        /// Name: UpdateIsFull
        /// Description: Method to update is full
        /// </summary>
        /// <param name="transportId">TransportId</param>
        /// <param name="isFull">IsFull</param>
        public void UpdateIsFull(Guid transportId, bool isFull)
        {
            if (this.GetById(transportId) == null)
                throw new ManagerException(ERROR_NOT_FOUND, new System.Exception("Transport to update is full not found"));
            this.DAO.UpdateIsFull(transportId, isFull);
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate transport
        /// </summary>
        /// <param name="transportId">TransportId</param>
        public void Deactivate(Guid transportId)
        {
            if (this.GetById(transportId) == null)
                throw new ManagerException(ERROR_NOT_FOUND, new System.Exception("Transport to deactivate is  not full"));
            this.DAO.Deactivate(transportId);
        }
        #endregion
    }
}
