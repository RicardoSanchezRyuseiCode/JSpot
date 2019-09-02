using Ryusei.Exception;
using Ryusei.JSpot.Core.Ent;
using Ryusei.JSpot.Core.Fty;
using Ryusei.JSpot.Core.Fty.Contract;
using Ryusei.JSpot.Core.Wrap;
using Ryusei.Logger.Wrap;
using Ryusei.Web.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ryusei.JSpot.Core.WebApi.Controllers
{
    /// <summary>
    /// Name: CarImageController
    /// Description: Cotnroller to expose the endpoints for CarImageController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Car")]
    public class CarController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "JSpot Core Server";

        public const string ERROR_IN_GET_CAR = "Jspot.Core.Ctrl.CarCtrl.ErrorInGet";
        public const string ERROR_CREATING_CAR = "Jspot.Core.Ctrl.CarCtrl.ErrorCreatingCar";
        public const string ERROR_UPDATING_CAR = "Jspot.Core.Ctrl.CarCtrl.ErrorUpdatingCar";
        public const string ERROR_DEACTIVATING_CAR = "Jspot.Core.Ctrl.CarCtrl.ErrorDeactivatingCar";
        #endregion

        #region [Attributes]
        /// <summary>
        /// ICarMgr
        /// </summary>
        private ICarMgr ICarMgr { get; set; }
        /// <summary>
        /// CarWrapper
        /// </summary>
        private CarWrapper CarWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public CarController()
        {
            CoreBuilder coreBuilder = CoreBuilder.GetInstance();
            this.ICarMgr = coreBuilder.GetManager<ICarMgr>(CoreBuilder.ICARMGR);

            this.CarWrapper = CarWrapper.GetInstance();

            this.SystemLogWrapper = SystemLogWrapper.GetInstance();
        }
        #endregion

        #region [Endpoints]
        /// <summary>
        /// Name: GetCurrent
        /// Description: Methos to get a collection of car by current user
        /// </summary>
        /// <returns>Collection Car</returns>
        [HttpGet]
        [Route("GetCurrent")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IEnumerable<Car> GetCurrent()
        {
            try
            {
                return this.ICarMgr.GetByUserId(this.GetUserDataId());
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting car", ERROR_IN_GET_CAR);
            }
        }
        /// <summary>
        /// Name: Save
        /// Description: Method to save a car
        /// </summary>
        /// <param name="car">Car</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Save(Car car)
        {
            try
            {
                car.UserId = this.GetUserDataId();
                this.ICarMgr.Save(car);
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, mex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, wex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error creating car", ERROR_CREATING_CAR);
            }
        }
        /// <summary>
        /// Name: Update
        /// Description: Method to update a car
        /// </summary>
        /// <param name="car">Car</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Update(Car car)
        {
            try
            {
                car.UserId = this.GetUserDataId();
                this.ICarMgr.Update(car);
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, mex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, wex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error updating car", ERROR_UPDATING_CAR);
            }
        }
        /// <summary>
        /// Name: Deactivate
        /// Description: Method to deactivate a car
        /// </summary>
        /// <param name="carId">CarId</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Deactivate")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Deactivate([FromBody]Guid carId)
        {
            try
            {
                this.CarWrapper.Deactivate(carId, this.GetUserDataId());
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, mex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, wex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER, this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error deactivating car", ERROR_DEACTIVATING_CAR);
            }
        }
        #endregion
    }
}
