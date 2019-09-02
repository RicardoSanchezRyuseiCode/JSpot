using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Contract
{
    /// <summary>
    /// Name: ICarMgr
    /// Descrpition: Interface to define the behavior of ICarMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public interface ICarMgr
    {
        /// <summary>
        /// Name: GetById
        /// Description: Method to get car by id
        /// </summary>
        /// <param name="carId">CarId</param>
        /// <returns></returns>
        Car GetById(Guid carId);
        /// <summary>
        /// Name: GetByUser
        /// Description: Method to get a collection of Car
        /// </summary>
        /// <returns>Collection of cars</returns>
        IEnumerable<Car> GetByUserId(Guid userId);
        /// <summary>
        /// Name: Car
        /// Description: Method to save a car
        /// </summary>
        /// <param name="car">Car</param>
        void Save(Car car);
        /// <summary>
        /// Name: Update
        /// Description: Method to update a car
        /// </summary>
        /// <param name="car">Car</param>
        void Update(Car car);
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate car
        /// </summary>
        /// <param name="carId"></param>
        void Deactivate(Guid carId);
    }
}
