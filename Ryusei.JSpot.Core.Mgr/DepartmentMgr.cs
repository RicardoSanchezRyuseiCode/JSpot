using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty.Contract;
using Ryusei.JSpot.Core.Mgr.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Mgr
{
    /// <summary>
    /// Name: DepartmentMgr
    /// Description: Manager class to implement the behavior of IDepartmentMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class DepartmentMgr : IDepartmentMgr
    {
        #region [Constants]
        public const string ERROR_DEPARTMENT_ALREADY_EXIST = "Jspot.Core.Mgr.DepartmentMgr.ErrorAlreadyExist";
        public const string ERROR_NAMES_REPEATED = "Jspot.Core.Mgr.DepartmentMgr.ErrorNamesRepeated";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static DepartmentMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private DepartmentDAO DepartmentDAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static DepartmentMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private DepartmentMgr()
        {
            this.DepartmentDAO = new DepartmentDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static DepartmentMgr GetInstance()
        {
            return Singleton ?? (Singleton = new DepartmentMgr());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get collection of department by collection of eventId
        /// </summary>
        /// <param name="collectionEventId">Collection EventId</param>
        public IEnumerable<Department> GetByEventId(IEnumerable<Guid> collectionEventId)
        {
            // Define filter
            string filter = string.Format("Active = 1 and EventId in ({0})", string.Join(",", collectionEventId.Select(x => string.Format("'{0}'", x))));
            // Define order
            string order = "";
            // define params
            object @params = null;
            // return the results
            return this.DepartmentDAO.Select(filter:filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetByEventId
        /// Description: Method to get a collection of department by eventid
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns></returns>
        public IEnumerable<Department> GetByEventId(Guid eventId)
        {
            // Define filter
            string filter = "EventId = @EventId and Active = @Active";
            // Define order
            string order = "Name";
            // define params
            object @params = new { EventId = eventId, Active = true };
            // return the results
            return this.DepartmentDAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: ValidDeparment
        /// Description: Method to check if department is valid
        /// </summary>
        private void ValidDeparment(IEnumerable<Department> currentDepartments, Department department)
        {
            foreach (Department currentDepartment in currentDepartments)
            {
                if (currentDepartment.Name.ToLower().Equals(department.Name.ToLower()))
                    throw new ManagerException(ERROR_DEPARTMENT_ALREADY_EXIST, new System.Exception(string.Format("A department with name: {0}, already exist", department.Name)));
            }
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a collection deparment
        /// </summary>
        /// <param name="collectionDepartment">Collection Department</param>
        public void Save(IEnumerable<Department> collectionDepartment)
        {
            // Check if name are repeated
            if (collectionDepartment.Count() != collectionDepartment.Select(x => x.Name).Distinct().Count())
                throw new ManagerException(ERROR_NAMES_REPEATED, new System.Exception("Collection of departments to create containts repeated names"));
            // Get current deparments
            IEnumerable<Department> currentDepartments = this.GetByEventId(collectionDepartment.Select(x => x.EventId).Distinct());
            // Check if some deparemtn already exist
            foreach (Department newDepartment in collectionDepartment)
            {
                ValidDeparment(currentDepartments, newDepartment);
            }
            this.DepartmentDAO.Save(collectionDepartment);
        }
        #endregion
    }
}
