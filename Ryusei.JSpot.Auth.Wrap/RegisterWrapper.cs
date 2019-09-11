using CryptSharp;
using Newtonsoft.Json;
using RestSharp;
using Ryusei.Exception;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Fty;
using Ryusei.JSpot.Auth.Fty.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace Ryusei.JSpot.Auth.Wrap
{
    /// <summary>
    /// Name: RegisterWrapper
    /// Description: Wrapper class to encapsulate register operations
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class RegisterWrapper
    {
        #region [Constants]
        private const string EXCEPTION_VALIDATE_NEW_USER_NOT_FOUND = "Ryusei.Auth.Wrap.RegisterWrapper.ExceptionValidateNewUserNotFound";
        private const string EXCEPTION_VALIDATE_NEW_USER_ALREADY_VALIDATED = "Ryusei.Auth.Wrap.RegisterWrapper.ExceptionValidateAlreadValidated";
        private const string EXCEPTION_INVALID_CAPTCHA = "Ryusei.Auth.Wrap.RegisterWrapper.ExceptionInvalidCaptcha";
        private const string EXCEPTION_INVALID_RESET_PASSWORD_LINK = "Ryusei.Auth.Wrap.RegisterWrapper.ExceptionInvalidResetPasswordLink";
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static RegisterWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]

        #region [Auth Managers]
        /// <summary>
        /// IUserMgr
        /// </summary>
        private IUserMgr IUserMgr { get; set; }
        /// <summary>
        /// IUserRoleMgr
        /// </summary>
        private IUserRoleMgr IUserRoleMgr { get; set; }
        /// <summary>
        /// IRoleMgr
        /// </summary>
        private IRoleMgr IRoleMgr { get; set; }
        #endregion

        #region [Auth Wrappers]
        /// <summary>
        /// EmailWrapper
        /// </summary>
        private EmailWrapper EmailWrapper { get; set; }
        #endregion

        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static RegisterWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private RegisterWrapper()
        {
            // Auth entities
            AuthBuilder authBuilder = AuthBuilder.GetInstance();
            this.IUserMgr = authBuilder.GetManager<IUserMgr>(AuthBuilder.IUSERMGR);
            this.IUserRoleMgr = authBuilder.GetManager<IUserRoleMgr>(AuthBuilder.IUSERROLEMGR);
            this.IRoleMgr = authBuilder.GetManager<IRoleMgr>(AuthBuilder.IROLEMGR);
            // Wrapper
            this.EmailWrapper = EmailWrapper.GetInstance();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static RegisterWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new RegisterWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: CreateValidationLink
        /// Description: Method to create a validation link after a user is register
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        protected string CreateValidationLink(string email)
        {
            // Get front end URL
            string frontEndURL = ConfigurationManager.AppSettings["JSPOT::FrontEndURL"];
            string frontEndController = ConfigurationManager.AppSettings["JSPOT::FrontEndValidationController"];
            // Get current date time
            DateTime currentTime = DateTime.Now.ToUniversalTime();
            // Conver the email and user to json object
            object parameter = new { Email = email, CreationDate = currentTime };
            string strParameter = JsonConvert.SerializeObject(parameter);
            // Encrypt the json string
            string encryptedParameter = Crypto.AES.Encrypt(strParameter);
            string safeUrl = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(encryptedParameter));
            // return the string
            return string.Format("{0}/{1}/{2}", frontEndURL, frontEndController, safeUrl);
        }
        /// <summary>
        /// Name: CreateResetPasswordLink
        /// Description: Method to create reset password link
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private string CreateResetPasswordLink(string email)
        {
            // Get front end URL
            string frontEndURL = ConfigurationManager.AppSettings["JSPOT::FrontEndURL"];
            string frontEndController = ConfigurationManager.AppSettings["JSPOT::FrontEndRecoverController"];
            // Get current date time
            DateTime currentTime = DateTime.Now.ToUniversalTime();
            // Conver the email and user to json object
            object parameter = new { Email = email, CreationDate = currentTime };
            string strParameter = JsonConvert.SerializeObject(parameter);
            // Encrypt the json string
            string encryptedParameter = Crypto.AES.Encrypt(strParameter);
            string safeUrl = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(encryptedParameter));
            // return the string
            return string.Format("{0}/{1}/{2}", frontEndURL, frontEndController, safeUrl);
        }
        /// <summary>
        /// Name: CreateNewUser
        /// Description: Method to create new user
        /// </summary>
        /// <param name="user"></param>
        public void CreateNewUser(User user)
        {
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Crypt password
                user.Password = Crypter.Blowfish.Crypt(user.Password);
                // Create the user
                this.IUserMgr.Save(user);
                // Get default rolw
                Role role = this.IRoleMgr.GetDefaultRole();
                // Save relation user rol
                this.IUserRoleMgr.Save(new List<UserRole>() { new UserRole() { RoleId = role.RoleId, UserId = user.UserId } });
                // Send email of confirmation
                //this.EmailWrapper.SendMailNewUser(user, CreateValidationLink(user.Email));
                // Complete the scope
                scope.Complete();
            }
        }
        /// <summary>
        /// Name: ValidateUser
        /// Description: Method to validate user after registration
        /// </summary>
        /// <param name="token">Token</param>
        public void ValidateUser(string token)
        {
            byte[] decbuff = HttpServerUtility.UrlTokenDecode(token);
            string encrypString = Encoding.UTF8.GetString(decbuff);
            string jsonToken = Crypto.AES.Decrypt(encrypString);
            // Get object
            dynamic tokenObj = JsonConvert.DeserializeObject<dynamic>(jsonToken);
            string email = (string)tokenObj.Email;
            // Get the user
            User userData = this.IUserMgr.GetByEmail(email);
            if (userData == null)
                throw new WrapperException(EXCEPTION_VALIDATE_NEW_USER_NOT_FOUND, new System.Exception(string.Format("User with email: {0}, was not found to validate the account", tokenObj.Email)));
            if (userData.IsValidated)
                throw new WrapperException(EXCEPTION_VALIDATE_NEW_USER_ALREADY_VALIDATED, new System.Exception(string.Format("User with email: {0}, is already validated", tokenObj.Email)));
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // If the user was found update the validation field
                this.IUserMgr.UpdateIsValidated(userData.UserId, true);
                // Finish scope
                scope.Complete();
            }
        }
        /// <summary>
        /// Name: DeactivateUser
        /// Description: Method to deactivate user
        /// </summary>
        /// <param name="userId">UserId</param>
        public void DeactivateUser(Guid userId)
        {
            // Get the user
            User userData = this.IUserMgr.GetById(userId);
            if (userData == null)
                throw new WrapperException(EXCEPTION_VALIDATE_NEW_USER_NOT_FOUND, new System.Exception(string.Format("User with ID: {0}, was not found to deactivate the account", userId)));
            // Open transaction scope
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // If the user was found update the validation field
                this.IUserMgr.Deactivate(userData.UserId);
                // Finish scope
                scope.Complete();
            }
        }
        /// <summary>
        /// Name: ValidateToken
        /// Description: Method to validate captcha token
        /// </summary>
        /// <param name="captchaToken"></param>
        internal bool ValidateToken(string captchaToken)
        {
            RestClient restClient = new RestClient(ConfigurationManager.AppSettings["Google::RecaptchatUrl"]);
            RestRequest restRequest = new RestRequest(ConfigurationManager.AppSettings["Google::RecaptchatWebApiAction"], Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("secret", ConfigurationManager.AppSettings["Google::RecaptchatKey"].ToString());
            restRequest.AddParameter("response", captchaToken);
            // execute the request
            IRestResponse response = restClient.Execute(restRequest);
            if (!response.IsSuccessful)
                return false;
            dynamic jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
            return ((bool)jsonResponse.success);
        }
        /// <summary>
        /// Name: RequestResetPassword
        /// Description : Method to request reset password of user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="captchaToken"></param>
        public void RequestResetPassword(string email, string captchaToken)
        {
            // Check if the user exist
            User user = this.IUserMgr.GetByEmail(email);
            if (user == null)
                throw new WrapperException(EXCEPTION_VALIDATE_NEW_USER_NOT_FOUND, new System.Exception(string.Format("User with email: {0}, was not found", email)));
            // Validate token
            if (!ValidateToken(captchaToken))
                throw new WrapperException(EXCEPTION_INVALID_CAPTCHA, new System.Exception(string.Format("User with email: {0}, send a invalid captcah token", email)));
            // Send email with information to reset password
            this.EmailWrapper.SendRequestResetPassword(user, CreateResetPasswordLink(user.Email));
        }
        /// <summary>
        /// Name: ChangePassword
        /// Description: Method to change password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        internal void ChangePassword(Guid userId, string newPassword)
        {
            // Encryp the new password
            string encryptedNewPassword = Crypter.Blowfish.Crypt(newPassword);
            // Update the password
            this.IUserMgr.UpdatePassword(userId, encryptedNewPassword);
        }
        /// <summary>
        /// Name: ResetPassword
        /// Description: Method to reset password
        /// </summary>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        public void ResetPassword(string token, string newPassword)
        {
            // Decryp the token
            byte[] decbuff = HttpServerUtility.UrlTokenDecode(token);
            string encrypString = Encoding.UTF8.GetString(decbuff);
            string jsonToken = Crypto.AES.Decrypt(encrypString);
            // Get object
            dynamic tokenObj = JsonConvert.DeserializeObject<dynamic>(jsonToken);
            string email = (string)tokenObj.Email;
            DateTime dateTimeRequested = (DateTime)tokenObj.CreationDate;
            // Check if link is valid
            if ((DateTime.Now.ToUniversalTime() - dateTimeRequested).TotalHours > int.Parse(ConfigurationManager.AppSettings["JSPOT::ToleranceTimeInHours"]))
                throw new WrapperException(EXCEPTION_INVALID_RESET_PASSWORD_LINK, new System.Exception(string.Format("User with email: {0}, use a invalid link for password reset", email)));
            // Check if the user exist
            User user = this.IUserMgr.GetByEmail(email);
            if (user == null)
                throw new WrapperException(EXCEPTION_VALIDATE_NEW_USER_NOT_FOUND, new System.Exception(string.Format("User with email: {0}, was not found", email)));
            // Reset password
            this.ChangePassword(user.UserId, newPassword);
        }
        #endregion
    }
}
