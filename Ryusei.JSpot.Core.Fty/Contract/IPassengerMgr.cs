using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IPassengerMgr
    /// Descrpition: Interface to define the behavior of IPassengerMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IPassengerMgr
    {
        /// <summary>
        /// Name: GetByTransportId
        /// Description: Method to get by transport id
        /// </summary>
        /// <param name="transportId">TransportId</param>
        /// <returns></returns>
        IEnumerable<Passenger> GetByTransportId(Guid transportId);
        /// <summary>
        /// Name: GetByUserIdEventIdSense
        /// Description: Method to get a collection of Passenger
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="travelSense"></param>
        /// <returns></returns>
        Passenger GetByUserIdEventIdSense(Guid userId, Guid eventId, bool travelSense);
        /// <summary>
        /// Name: GetByPassengerId
        /// Description: Method to get a collection of passenger
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Passenger> GetByPassengerId(Guid userId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save a passenger
        /// </summary>
        /// <param name="passenger"></param>
        void Save(Passenger passenger);

    }
}
