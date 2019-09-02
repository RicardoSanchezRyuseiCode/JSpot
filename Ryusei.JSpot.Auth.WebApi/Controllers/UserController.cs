using Ryusei.Exception;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Fty;
using Ryusei.JSpot.Auth.Fty.Contract;
using Ryusei.JSpot.Auth.Wrap;
using Ryusei.Logger.Wrap;
using Ryusei.Web.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ryusei.JSpot.Auth.WebApi.Controllers
{
    /// <summary>
    /// Name: UserController
    /// Description: Controller to expose the endpoints of user controller
    /// Author: Ricardo Sanchez Romero (ricardo.rsz.sanchez@faurecia.com)
    /// Logbook:
    ///     2019-08-22: Creation
    /// </summary>
    [RoutePrefix("api/User")]
    public class UserController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
        #region [Constants]
        private const string SERVER = "Auth Server";
        public const string ERROR_IN_GET_CURRENT_USER = "Ryusei.Auth.Ctrl.UserCtrl.ErrorInGetCurrentUser";
        public const string ERROR_UPDATING_USER = "Ryusei.Auth.Ctrl.UserCtrl.ErrorUpdatingUser";
        public const string ERROR_UPDATING_USER_PASSWORD = "Ryusei.Auth.Ctrl.UserCtrl.ErrorUpdatingUserPassword";
        public const string ERROR_UPLOADING_PROFILE_PICTURE = "Ryusei.Auth.Ctrl.UserCtrl.ErrorUploadingProfilePicture";
        public const string ERROR_DOWNLOADING_PROFILE_PICTURE = "Ryusei.Auth.Ctrl.UserCtrl.ErrorDownloadingProfilePicture";
        #endregion

        #region [Attributes]
        /// <summary>
        /// IUserMgr
        /// </summary>
        private IUserMgr IUserMgr { get; set; }
        /// <summary>
        /// RegisterWrapper
        /// </summary>
        private UserWrapper UserWrapper { get; set; }
        /// <summary>
        /// SystemLogWrapper
        /// </summary>
        private SystemLogWrapper SystemLogWrapper { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserController()
        {
            AuthBuilder authBuilder = AuthBuilder.GetInstance();
            this.IUserMgr = authBuilder.GetManager<IUserMgr>(AuthBuilder.IUSERMGR);

            this.UserWrapper = UserWrapper.GetInstance();

            this.SystemLogWrapper = SystemLogWrapper.GetInstance();
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetCurrentUser
        /// Description: Method to get current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrentUser")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public User GetCurrentUser()
        {
            try
            {
                return this.IUserMgr.GetById(this.GetUserDataId());
            }
            catch (System.Exception ex)
            {
                // Save entry in log
                this.SystemLogWrapper.Register("CRM Server", this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error getting current user", ERROR_IN_GET_CURRENT_USER);
            }
        }
        /// <summary>
        /// Name: Update
        /// Description: Endpoint to update user information
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult Update(User user)
        {
            try
            {
                // Assign id
                user.UserId = this.GetUserDataId();
                // Update user
                this.IUserMgr.Update(user);
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
                throw ExceptionResponse.ThrowException("Error updating user", ERROR_UPDATING_USER);
            }
        }
        /// <summary>
        /// Name: UpdatePassword
        /// Description: Method to update password
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdatePassword")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult UpdatePassword([FromBody] string password)
        {
            try
            {
                // Update password
                this.UserWrapper.ChangePassword(this.GetUserDataId(), password);
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
                throw ExceptionResponse.ThrowException("Error updating user password", ERROR_UPDATING_USER_PASSWORD);
            }
        }
        /// <summary>
        /// Name: UploadProfilePicture
        /// Description: Endpoint to upload profile picture
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadProfilePicture")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public async Task<IHttpActionResult> UploadProfilePicture()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            // Define the stream provider
            MultipartMemoryStreamProvider memoryStreamProvider = new MultipartMemoryStreamProvider();
            try
            {
                Guid userDataId = this.GetUserDataId();
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(memoryStreamProvider).ContinueWith(async (task) => {
                    // Get Http Content
                    HttpContent content = memoryStreamProvider.Contents[0];
                    // Get the stream
                    Stream stream = await content.ReadAsStreamAsync();
                    // Save the file
                    this.UserWrapper.UploadProfilePicture(userDataId, stream);
                });
                // Return the response
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
                this.SystemLogWrapper.Register(SERVER,  this.GetUserDataId(), SystemLogWrapper.TYPE_ERROR, ex);
                // Throw the exception
                throw ExceptionResponse.ThrowException("Error uploading profile picture", ERROR_UPLOADING_PROFILE_PICTURE);
            }
        }

        /// <summary>
        /// Name: DownloadProfilePicture
        /// Description: Method to download profile picture
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [HttpGet]
        [Route("DownloadProfilePicture")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult DownloadProfilePicture()
        {
            try
            {
                // User data id
                Guid userDataId = this.GetUserDataId();
                // Return the response
                return Ok(new GeneralResponse() { Error = false, Message = this.UserWrapper.DownloadProfilePhoto(userDataId) });
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
                throw ExceptionResponse.ThrowException("Error downloading profile picture", ERROR_DOWNLOADING_PROFILE_PICTURE);
            }
        }
        /// <summary>
        /// Name: DownloadProfilePictureById
        /// Description: Method to get picture of user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadProfilePictureById/{userId:guid}")]
        [Ryusei.JSpot.Auth.Attr.WebApi.Authorize(ServerName = SERVER)]
        public IHttpActionResult DownloadProfilePictureById(Guid userId)
        {
            try
            {
                // Return the response
                return Ok(new GeneralResponse() { Error = false, Message = this.UserWrapper.DownloadProfilePhoto(userId) });
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
                throw ExceptionResponse.ThrowException("Error downloading profile picture", ERROR_DOWNLOADING_PROFILE_PICTURE);
            }
        }
        #endregion
    }
}
