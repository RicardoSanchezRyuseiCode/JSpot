using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty;
using Ryusei.JSpot.Core.Fty.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ryusei.JSpot.Core.Wrap
{
    /// <summary>
    /// Name: CarWrapper
    /// Description: Wrapper class for car 
    /// </summary>
    public class CarWrapper
    {
        #region [Constants]

        private const string ERROR_CAR_IS_IN_USE = "Jspot.Core.Wrap.CarWrapper.ErrorCarIsInUse";

        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static CarWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ICarMgr
        /// </summary>
        private ICarMgr ICarMgr { get; set; }
        /// <summary>
        /// ITransportMgr
        /// </summary>
        private ITransportMgr ITransportMgr { get; set; }
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IEventMgr IEventMgr { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static CarWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private CarWrapper()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.ICarMgr = coreBuilder.GetManager<ICarMgr>(CoreBuilder.ICARMGR);
            this.ITransportMgr = coreBuilder.GetManager<ITransportMgr>(CoreBuilder.ITRANSPORTMGR);
            this.IEventMgr = coreBuilder.GetManager<IEventMgr>(CoreBuilder.IEVENTMGR);
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static CarWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new CarWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate car 
        /// </summary>
        /// <param name="carId">CarId</param>
        /// <param name="userId">UserId</param>
        public void Deactivate(Guid carId, Guid userId)
        {
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Get the event of user that not have been finish
                IEnumerable<Event> collectionEvent = this.IEventMgr.GetByUser(userId, DateTime.UtcNow);
                // Get the event id and get the transports for the events
                IEnumerable<Transport> collectionTransport = this.ITransportMgr.GetByEventId(collectionEvent.Select(x => x.EventId));
                // Check if transport is associated with the car to delete
                if (collectionTransport.FirstOrDefault(x => x.CarId == carId) != null)
                    throw new WrapperException(ERROR_CAR_IS_IN_USE, new System.Exception("Car cant be deleted becuase is in use"));
                // Deactivate the cat
                this.ICarMgr.Deactivate(carId);
                // complete the scope
                scope.Complete();
            }
        }
        #endregion
    }
}
