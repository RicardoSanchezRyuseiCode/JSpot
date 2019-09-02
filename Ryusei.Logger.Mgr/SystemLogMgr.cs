using Ryusei.Logger.Ent;
using Ryusei.Logger.Fty.Contract;
using Ryusei.Logger.Mgr.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Logger.Mgr
{
    /// <summary>
    /// Name: SystemLogMgr
    /// Description: Manager class to implement the behavior of SystemLogMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class SystemLogMgr : ISystemLogMgr
    {
        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static SystemLogMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private SystemLogDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static SystemLogMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private SystemLogMgr()
        {
            this.DAO = new SystemLogDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static SystemLogMgr GetInstance()
        {
            return Singleton ?? (Singleton = new SystemLogMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByRange
        /// Description: Method to get by collection of SystemLog by Range
        /// </summary>
        /// <param name="initialDate">InitialDate</param>
        /// <param name="endDate">EndDate</param>
        /// <returns>Collection SystemLog</returns>
        public IEnumerable<SystemLog> GetByRange(DateTime initialDate, DateTime endDate)
        {
            // Define filter
            string filter = "RegisterDate between @InitialDate and @EndDate";
            // Define order 
            string order = "RegisterDate desc";
            // Define params 
            object @params = new { InitialDate = initialDate, EndDate = endDate };
            // return the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByCompanyRange
        /// Description: Method to get collection by compayDataId and Range
        /// </summary>
        /// <param name="companyDataId">CompanyDataId</param>
        /// <param name="initialDate">IntiialDate</param>
        /// <param name="endDate">EndDate</param>
        /// <returns>Collection SystemLog</returns>
        public IEnumerable<SystemLog> GetByCompanyRange(Guid companyDataId, DateTime initialDate, DateTime endDate)
        {
            // Define filter
            string filter = "RegisterDate between @InitialDate and @EndDate";
            // Define order 
            string order = "RegisterDate desc";
            // Define params 
            object @params = new { InitialDate = initialDate, EndDate = endDate };
            // return the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a systemLog register
        /// </summary>
        /// <param name="systemLog">SystemLog</param>
        public void Save(SystemLog systemLog)
        {
            this.DAO.Save(systemLog);
        }
        #endregion
    }
}
