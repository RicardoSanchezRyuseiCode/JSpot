using Ryusei.JSpot.Core.Ent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ryusei.JSpot.Auth.Ent;

namespace Ryusei.JSpot.Core.Mgr.DAO
{
    /// <summary>
    /// Name: CarDAO
    /// Description: Data Access Object for Car
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-27: Creation
    /// </summary>
    internal class CarDAO
    {
        #region [Methods]
        /// <summary>
        /// Name: Select
        /// Description: Method to get a collection of Cars
        /// </summary>
        /// <param name="top">Top</param>
        /// <param name="filter">Filter</param>
        /// <param name="order">Order</param>
        /// <param name="params">Params</param>
        /// <returns>Collection of Car</returns>
        internal IEnumerable<Car> Select(string top = "", string filter = "", string order = "", object @params = null)
        {
            // result
            IEnumerable<Car> results = new List<Car>();
            // query
            string query = string.Format(@"
                                            select {0}
                                             * 
                                            from 
	                                            Core.Car C
                                            inner join
	                                            Auth.[User] U
                                            on
	                                            C.UserId = U.UserId
                                            ", top);
            // add filters
            query = (!filter.Equals("")) ? string.Format("{0} where {1}", query, filter) : query;
            // add order
            query = (!order.Equals("")) ? string.Format("{0} order by {1}", query, order) : query;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                results = dbConnection.Query<Car, User, Car>(query,(C,U) => {
                    C.User = U;
                    return C;
                }, @params, splitOn: "UserId");
            }
            // list contacts
            return results;
        }
        /// <summary>
        /// Name: Save
        /// Descrpition: Method to save a car
        /// </summary>
        /// <param name="car">Car</param>
        internal void Save(Car car)
        {
            // Define statement
            string statement = "insert into Core.Car(CarId, UserId, Brand, Model, Color, Spots, Active)values(@CarId, @UserId, @Brand, @Model, @Color, @Spots, @Active)";
            // Set default data
            car.CarId = Guid.NewGuid();
            car.Active = true;
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, car);
            }
        }
        /// <summary>
        /// Name: Update
        /// Description: Method to update car
        /// </summary>
        /// <param name="car">Car</param>
        internal void Update(Car car)
        {
            // Define statement
            string statement = "update Core.Car set Brand = @Brand, Model = @Model, Color = @Color, Spots = @Spots  where CarId = @CarId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, car);
            }
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate a car
        /// </summary>
        /// <param name="carId">Car Id</param>
        internal void Deactivate(Guid carId)
        {
            // Define statement
            string statement = "update Core.Car set Active = @Active where CarId = @CarId";
            // Execute
            using (IDbConnection dbConnection = Data.DAO.GetInstance(Data.DbType.SqlServer))
            {
                // Get results
                dbConnection.Execute(statement, new { CarId = carId, Active = false });
            }
        }
        #endregion
    }
}
