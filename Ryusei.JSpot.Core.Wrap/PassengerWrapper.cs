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
    /// Name: PassengerWrapper
    /// Description: Wrapper class for Passenger
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-29: Creation
    /// </summary>
    public class PassengerWrapper
    {
        #region [Constants]
        private const string ERROR_TRANSPORT_IS_FULL = "Jspot.Core.Wrap.PassengerWrap.ErrorTransportFull";
        private const string ERROR_PASSENGER_ALREADY_ADDED = "Jspot.Core.Wrap.PassengerWrap.ErrorAlreadyAdded";
        private const string ERROR_PASSENGER_ALREADY_HAVE_TRANSPORT = "Jspot.Core.Wrap.PassengerWrap.ErrorAlreadyHaveTransport";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static PassengerWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// PassengerMgr
        /// </summary>
        private IPassengerMgr IPassengerMgr { get; set; }
        /// <summary>
        /// ITransportMgr
        /// </summary>
        private ITransportMgr ITransportMgr { get; set; }
        /// <summary>
        /// ICarMgr
        /// </summary>
        private ICarMgr ICarMgr { get; set; }
        /// <summary>
        /// IEventMgr
        /// </summary>
        private IEventMgr IEventMgr { get; set; }
        /// <summary>
        /// EmailWrapper
        /// </summary>
        private EmailWrapper EmailWrapper { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static PassengerWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private PassengerWrapper()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IPassengerMgr = coreBuilder.GetManager<IPassengerMgr>(CoreBuilder.IPASSENGERMGR);
            this.ITransportMgr = coreBuilder.GetManager<ITransportMgr>(CoreBuilder.ITRANSPORTMGR);
            this.ICarMgr = coreBuilder.GetManager<ICarMgr>(CoreBuilder.ICARMGR);
            this.IEventMgr = coreBuilder.GetManager<IEventMgr>(CoreBuilder.IEVENTMGR);

            this.EmailWrapper = EmailWrapper.GetInstance();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static PassengerWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new PassengerWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Create
        /// Description: Method to create a passenger
        /// </summary>
        /// <param name="passenger">Passenger</param>
        /// <param name="email">Email</param>
        /// <param name="name">Name</param>
        /// <param name="lastname">Lastname</param>
        public void Create(Passenger passenger, string email, string name, string lastname)
        {
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Get transport
                Transport transport = this.ITransportMgr.GetById(passenger.TransportId);
                // Get event
                Event @event = this.IEventMgr.GetById(transport.EventId);
                // Get Car to get owner of transport
                Car car = this.ICarMgr.GetById(transport.CarId);
                // Get current passengers
                IEnumerable<Passenger> transportPassengers = this.IPassengerMgr.GetByTransportId(transport.TransportId);
                // Check if have spots
                if (transport.IsFull)
                    throw new WrapperException(ERROR_TRANSPORT_IS_FULL, new System.Exception("Cannot create passenger becuase transport is full"));
                // Check if passenger already exist
                if( transportPassengers.FirstOrDefault(x => x.UserId == passenger.UserId) != null)
                    throw new WrapperException(ERROR_PASSENGER_ALREADY_ADDED, new System.Exception("User is already added to transport"));
                // Check if passenger dont have other transport associates
                if (this.IPassengerMgr.GetByPassengerId(passenger.UserId).FirstOrDefault(x => x.Transport.TravelSense == transport.TravelSense && x.Transport.EventId == transport.EventId) != null)
                    throw new WrapperException(ERROR_PASSENGER_ALREADY_HAVE_TRANSPORT, new System.Exception("User is already have transport assigned"));
                // Create the passenger
                this.IPassengerMgr.Save(passenger);
                // Check if transport is full
                transportPassengers = this.IPassengerMgr.GetByTransportId(transport.TransportId);
                if (transport.Car.Spots == transportPassengers.Count())
                {
                    // Email to transport owner
                    this.EmailWrapper.SendMailTransportFull(car.User, transport, @event);
                    // Update is full
                    this.ITransportMgr.UpdateIsFull(transport.TransportId, true);
                }
                // Email to user of confirmation
                this.EmailWrapper.SendMailTransportPassengerConfirmation(email, name, lastname, transport, @event);
                // Email to transport owner
                this.EmailWrapper.SendMailTransportPassengerAdded(car.User, email, name, lastname, transport, @event);
                // Complete the scope
                scope.Complete();
            }
        }
        #endregion
    }
}
