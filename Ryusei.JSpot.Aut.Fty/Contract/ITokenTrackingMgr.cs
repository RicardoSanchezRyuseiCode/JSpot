using Ryusei.JSpot.Auth.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Contract
{
    /// <summary>
    /// Name: ITokenTrackingMgr
    /// Description: Interface to define the behavior of TokenTrackingMgr
    /// Author: Ricardo Sanchez Romero 
    /// LogBook:
    ///     2019/08/16: Creation
    /// </summary>
    public interface ITokenTrackingMgr
    {
        /// <summary>
        /// Name: TokenTracking
        /// Description: Method to get by id
        /// </summary>
        /// <param name="tokenTrackingId">TokenTrackingId</param>
        /// <returns></returns>
        TokenTracking GetById(Guid tokenTrackingId);
        /// <summary>
        /// Name: Save
        /// Descrpition: Method to save a token tracking
        /// </summary>
        /// <param name="tokenTracking">TokenTracking</param>
        void Save(TokenTracking tokenTracking);
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete token tracking by tokenTrackingId
        /// </summary>
        /// <param name="tokenTrackingId">tokenTrackingId</param>
        void Delete(Guid tokenTrackingId);
    }
}
