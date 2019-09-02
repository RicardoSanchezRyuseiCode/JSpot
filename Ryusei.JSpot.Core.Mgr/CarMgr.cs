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
    /// Name: CarMgr
    /// Description: Manager class to implement the behavior of ICarMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class CarMgr : ICarMgr
    {
        #region [Constants]

        private const string ERROR_MODEL_ALREADY_EXIST = "Jspot.Core.Mgr.CarMgr.ErrorModelAlreadyExist";
        private const string ERROR_NOT_EXIST = "Jspot.Core.Mgr.CarMgr.ErrorNotExist";

        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static CarMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ApplicationDAO
        /// </summary>
        private CarDAO DAO { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static CarMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private CarMgr()
        {
            this.DAO = new CarDAO();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static CarMgr GetInstance()
        {
            return Singleton ?? (Singleton = new CarMgr());
        }
        #endregion

        #region [Methods]

        /// <summary>
        /// Name: GetByUserId
        /// Description: Method to get a collection of Car
        /// </summary>
        /// <returns>Collection of Cars</returns>
        public IEnumerable<Car> GetByUserId(Guid userId)
        {
            // Define filter
            string filter = "U.UserId = @UserId and C.Active = @Active";
            // Define order
            string order = "Model";
            // Define params
            object @params = new { Active = true, UserId = userId };
            // Return the results
            return this.DAO.Select(filter: filter, order: order, @params: @params);
        }
        /// <summary>
        /// Name: GetById
        /// Description: Method to get car by Id
        /// </summary>
        /// <param name="carId">CarId</param>
        /// <returns></returns>
        public Car GetById(Guid carId)
        {
            // Define filter
            string filter = "C.CarId = @CarId and C.Active = @Active";
            // Define order
            string order = "Model";
            // Define params 
            object @params = new { CarId = carId, Active = true };
            // get the results
            IEnumerable<Car> results = this.DAO.Select(filter: filter, order: order, @params: @params);
            // return the results
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: GetByModel
        /// Description: Method to get a collecion of car by model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Car GetByModel(Guid userId, string model)
        {
            // Define filter
            string filter = "U.UserId = @UserId and LOWER(C.Model) = LOWER(@Model) and C.Active = @Active";
            // Define order
            string order = "Model";
            // Define params
            object @params = new { Active = true, Model = model, UserId = userId };
            // Return the results
            IEnumerable<Car> results = this.DAO.Select(filter: filter, order: order, @params: @params);
            // return 
            return results.Count() > 0 ? results.ElementAt(0) : null;
        }
        /// <summary>
        /// Name: Car
        /// Description: Method to save a car
        /// </summary>
        /// <param name="car">Car</param>
        public void Save(Car car)
        {
            // Check if model already exist
            if (this.GetByModel(car.UserId, car.Model) != null)
                throw new ManagerException(ERROR_MODEL_ALREADY_EXIST, new System.Exception(string.Format("A car with same model already exist for user with id:{0}", car.UserId)));
            // Save the car
            this.DAO.Save(car);
        }
        /// <summary>
        /// Name: Update
        /// Description: Method to update a car
        /// </summary>
        /// <param name="car">Car</param>
        public void Update(Car car)
        {
            // Check if car 
            if(this.GetById(car.CarId) == null)
                throw new ManagerException(ERROR_NOT_EXIST, new System.Exception(string.Format("A car with id:{0} was not found", car.UserId)));
            // Update car
            this.DAO.Update(car);
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate car
        /// </summary>
        /// <param name="carId"></param>
        public void Deactivate(Guid carId)
        {
            // Check if car 
            if (this.GetById(carId) == null)
                throw new ManagerException(ERROR_NOT_EXIST, new System.Exception(string.Format("A car with id:{0} was not found", carId)));
            // Update car
            this.DAO.Deactivate(carId);
        }
        #endregion
    }
}
