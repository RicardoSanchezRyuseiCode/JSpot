using Ryusei.Logger.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Logger.Fty.Contract
{
    /// <summary>
    /// Name: ISystemLogMgr
    /// Description: Interface to define the behavior of ISystemLogMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public interface ISystemLogMgr
    {
        /// <summary>
        /// Name: GetByRange
        /// Description: Method to get by collection of SystemLog by Range
        /// </summary>
        /// <param name="initialDate">InitialDate</param>
        /// <param name="endDate">EndDate</param>
        /// <returns>Collection SystemLog</returns>
        IEnumerable<SystemLog> GetByRange(DateTime initialDate, DateTime endDate);
        /// <summary>
        /// Name: GetByCompanyRange
        /// Description: Method to get collection by compayDataId and Range
        /// </summary>
        /// <param name="companyDataId">CompanyDataId</param>
        /// <param name="initialDate">IntiialDate</param>
        /// <param name="endDate">EndDate</param>
        /// <returns>Collection SystemLog</returns>
        IEnumerable<SystemLog> GetByCompanyRange(Guid companyDataId, DateTime initialDate, DateTime endDate);
        /// <summary>
        /// Name: Save
        /// Description: Method to save a systemLog register
        /// </summary>
        /// <param name="systemLog">SystemLog</param>
        void Save(SystemLog systemLog);
    }
}
