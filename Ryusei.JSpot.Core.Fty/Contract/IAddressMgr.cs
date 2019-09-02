using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IAddressMgr
    /// Descrpition: Interface to define the behavior of IAddressMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IAddressMgr
    {
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get Address by EventId
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <returns></returns>
        Address GetByEventId(Guid eventId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save an address
        /// </summary>
        /// <param name="address"></param>
        void Save(Address address);
    }
}
