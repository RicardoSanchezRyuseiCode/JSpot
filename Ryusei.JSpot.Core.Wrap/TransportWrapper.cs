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
    /// Name: TransportWrapper
    /// Description: 
    /// </summary>
    public class TransportWrapper
    {

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static TransportWrapper Singleton { get; set; }
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
        static TransportWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private TransportWrapper()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.IPassengerMgr = coreBuilder.GetManager<IPassengerMgr>(CoreBuilder.IPASSENGERMGR);
            this.ITransportMgr = coreBuilder.GetManager<ITransportMgr>(CoreBuilder.ITRANSPORTMGR);
            this.ICarMgr = coreBuilder.GetManager<ICarMgr>(CoreBuilder.ICARMGR);
            this.IEventMgr = coreBuilder.GetManager<IEventMgr>(CoreBuilder.IEVENTMGR);
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static TransportWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new TransportWrapper());
        }
        #endregion


        #region [Methods]
        /// <summary>
        /// Name: Create
        /// Descripition: Method to create transport
        /// </summary>
        /// <param name="transport">Transport</param>
        /// <param name="userId">UserId</param>
        public void Create(Transport transport, Guid userId)
        {
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Create transport
                this.ITransportMgr.Save(transport);
                // Create passenger
                this.IPassengerMgr.Save(new Passenger() {
                    TransportId = transport.TransportId,
                    UserId = userId
                });
                // Complete the scope
                scope.Complete();
            }
        }

        #endregion
    }
}
