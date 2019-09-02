using Ryusei.JSpot.Auth.Fty.Contract;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Mgr.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Mgr
{
    /// <summary>
    /// Name: TokenTrackingMgr
    /// Description: Manager class to implement the behavior of TokenTracking
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class TokenTrackingMgr : ITokenTrackingMgr
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static TokenTrackingMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// TokenTrackingDAO
        /// </summary>
        private TokenTrackingDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static TokenTrackingMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private TokenTrackingMgr()
        {
            this.DAO = new TokenTrackingDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static TokenTrackingMgr GetInstance()
        {
            return Singleton ?? (Singleton = new TokenTrackingMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: TokenTracking
        /// Description: Method to get by id
        /// </summary>
        /// <param name="tokenTrackingId">TokenTrackingId</param>
        /// <returns></returns>
        public TokenTracking GetById(Guid tokenTrackingId)
        {
            // Define filter
            string filter = "TokenTrackingId = @TokenTrackingId";
            // Define params
            object @params = new { TokenTrackingId = tokenTrackingId };
            // Get the results
            IEnumerable<TokenTracking> results = this.DAO.Select(filter: filter, @params: @params);
            // return
            return results.Count() > 0 ? results.ElementAt(0) : null;

        }
        /// <summary>
        /// Name: Save
        /// Descrpition: Method to save a token tracking
        /// </summary>
        /// <param name="tokenTracking">TokenTracking</param>
        public void Save(TokenTracking tokenTracking)
        {
            this.DAO.Save(tokenTracking);
        }
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete token tracking by TokenTrackingId
        /// </summary>
        /// <param name="tokenTrackingId">tokenTrackingId</param>
        public void Delete(Guid tokenTrackingId)
        {
            this.DAO.Delete(tokenTrackingId);
        }
        #endregion
    }
}
