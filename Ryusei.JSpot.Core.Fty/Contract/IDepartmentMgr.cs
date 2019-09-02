using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IDepartmentMgr
    /// Descrpition: Interface to define the behavior of IDepartmentMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IDepartmentMgr
    {
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of department by eventid
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        IEnumerable<Department> GetByEventId(Guid eventId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection deparment
        /// </summary>
        /// <param name="collectionDepartment">Collection Department</param>
        void Save(IEnumerable<Department> collectionDepartment);
    }
}
