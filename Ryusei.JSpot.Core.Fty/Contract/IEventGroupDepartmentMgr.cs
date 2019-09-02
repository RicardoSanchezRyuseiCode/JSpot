using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: IEventGroupDepartmentMgr
    /// Descrpition: Interface to define the behavior of IEventGroupDepartmentMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface IEventGroupDepartmentMgr
    {
        /// <summary>
        /// Name: GetByEventGroupId
        /// Description: Method to get a collection of EventGroupId
        /// </summary>
        /// <param name="eventGroupId">EventGruopId</param>
        /// <returns></returns>
        IEnumerable<EventGroupDepartment> GetByEventGroupId(Guid eventGroupId);
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection of EventGroup Department
        /// </summary>
        /// <param name="collectionEventGroupDepartment">CollectionEventGroupDepartment</param>
        void Save(IEnumerable<EventGroupDepartment> collectionEventGroupDepartment);
    }
}
