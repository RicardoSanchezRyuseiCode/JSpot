using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: ITransportMgr
    /// Descrpition: Interface to define the behavior of ITransportMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface ITransportMgr
    {
        /// <summary>
        /// Name: GetById
        /// Description: Method to get transport by id
        /// </summary>
        /// <param name="transportId">TransportId</param>
        /// <returns>Transport</returns>
        Transport GetById(Guid transportId);
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get event id by travel sense
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <param name="travelSense">TravelSense</param>
        /// <returns></returns>
        IEnumerable<Transport> GetByEventId(Guid eventId, bool travelSense);
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get by event id
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        IEnumerable<Transport> GetByEventId(Guid eventId);
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of transport by collection of eventId
        /// </summary>
        /// <param name="collectionEventId">Collection of eventId</param>
        /// <returns></returns>
        IEnumerable<Transport> GetByEventId(IEnumerable<Guid> collectionEventId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save transport
        /// </summary>
        /// <param name="transport">Transport</param>
        void Save(Transport transport);
        /// <summary>
        /// Name: UpdateIsFull
        /// Description: Method to update is full
        /// </summary>
        /// <param name="transportId">TransportId</param>
        /// <param name="isFull">IsFull</param>
        void UpdateIsFull(Guid transportId, bool isFull);
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate transport
        /// </summary>
        /// <param name="transportId">TransportId</param>
        void Deactivate(Guid transportId);
    }
}
