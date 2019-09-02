using Ryusei.Exception;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Prm;
using Ryusei.JSpot.Auth.Wrap;
using Ryusei.Logger.Wrap;
using Ryusei.Web.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ryusei.JSpot.Auth.WebApi.Controllers
{
    /// <summary>
    /// Name: RegisterController
    /// Description: Controller to expose the endpoint for Register
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    [RoutePrefix("api/Register")]
    public class RegisterController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        public const string SERVER = "Auth Server";
        public const string ERROR_REGISTER_USER = "Ryusei.Auth.Ctrl.RegisterController.ErrorRegisterUser";
        public const string ERROR_VALIDATE_USER = "Ryusei.Auth.Ctrl.RegisterController.ErrorValidateUser";
        public const string ERROR_DEACTIVATING_USER = "Ryusei.Auth.Ctrl.RegisterController.ErrorDeactivatingUser";
        public const string ERROR_REQUEST_RESET_PASSWORD = "Ryusei.Auth.Ctrl.RegisterController.ErrorRequestResetPassword";
        public const string ERROR_RESET_PASSWORD = "Ryusei.Auth.Ctrl.RegisterController.ErrorResetPassword";
        #endregion

        #region [Attributes]
        /// <summary>
        /// RegisterWrapper
        /// </summary>
        private RegisterWrapper RegisterWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default controller
        /// </summary>
        public RegisterController()
        {
            this.RegisterWrapper = RegisterWrapper.GetInstance();
            this.SystemLogWrapper = SystemLogWrapper.GetInstance();
        }
        #endregion

        #region [Endpoints]
        /// <summary>
        /// Name: CreateNewUser
        /// Description: Endpoint to register new user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateNewUser")]
        [AllowAnonymous]
        public IHttpActionResult CreateNewUser(User user)
        {
            try
            {
                this.RegisterWrapper.CreateNewUser(user);
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = string.Format("{0}, {1}", mex.Message, mex.StackTrace) });
            }
            catch (WrapperException wex)
            {
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = string.Format("{0}, {1}", wex.InnerException.Message, wex.InnerException.StackTrace) });
            }
            catch (System.Exception)
            {
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error creating new user", ERROR_REGISTER_USER);
            }
        }
        /// <summary>
        /// Name: ValidateNewUser
        /// Description: Endpoint to validate new user
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateNewUser")]
        [AllowAnonymous]
        public IHttpActionResult ValidateNewUser([FromBody]string token)
        {
            try
            {
                // Subscript user
                this.RegisterWrapper.ValidateUser(token);
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception)
            {
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error validation new user", ERROR_VALIDATE_USER);
            }
        }
        /// <summary>
        /// Name: DeactivateUser
        /// Description: Endpoint to deactivate user
        /// </summary>
        /// <param name="userId">User It</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeactivateUser/{userId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult DeactivateUser([FromUri]Guid userId)
        {
            try
            {
                this.RegisterWrapper.DeactivateUser(userId);
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
                this.SystemLogWrapper.Register(SERVER,  this.GetUserDataId(), SystemLogWrapper.TYPE_WARNING, wex);
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register(SERVER,  this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error deactivating user", ERROR_DEACTIVATING_USER);
            }
        }
        /// <summary>
        /// Name: RequestResetPassword
        /// Description: Endpoint to request reset password
        /// </summary>
        /// <param name="requestPasswordPrm">Request Password Prm</param>
        /// <returns></returns>
        [HttpPost]
        [Route("RequestResetPassword")]
        [AllowAnonymous]
        public IHttpActionResult RequestResetPassword(RequestResetPasswordPrm requestPasswordPrm)
        {
            try
            {
                // Subscript user
                this.RegisterWrapper.RequestResetPassword(requestPasswordPrm.Email, requestPasswordPrm.CaptchaToken);
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception)
            {
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error on request password reset", ERROR_REQUEST_RESET_PASSWORD);
            }
        }
        /// <summary>
        /// Name: ResetPassword
        /// Description: Method to reset password
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public IHttpActionResult ResetPassword(ResetPasswordPrm resetPasswordPrm)
        {
            try
            {
                // Subscript user
                this.RegisterWrapper.ResetPassword(resetPasswordPrm.Token, resetPasswordPrm.Password);
                // return the response
                return Ok(new GeneralResponse() { Error = false, Message = "" });
            }
            catch (ManagerException mex)
            {
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = mex.Message });
            }
            catch (WrapperException wex)
            {
                // Throw the exception
                return Ok(new GeneralResponse() { Error = true, Message = wex.Message });
            }
            catch (System.Exception)
            {
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error on password reset", ERROR_RESET_PASSWORD);
            }
        }

        #endregion
    }
}
