using CryptSharp;
using Ryusei.Exception;
using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Auth.Fty;
using Ryusei.JSpot.Auth.Fty.Contract;
using Ryusei.Storage.Fty;
using Ryusei.Storage.Fty.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Wrap
{
    public class UserWrapper
    {
        #region [Constants]
        private const string USER_DATA_CREATION = "USER_DATA_CREATION";
        private const string USER_DATA_UPDATE = "USER_DATA_UPDATE";
        private const string USER_DATA_DELETE = "USER_DATA_DELETE";

        private const string EXCEPTION_USER_NOT_FOUND = "Ryusei.Auth.Wrap.UserWrapper.ExceptionUserNotFound";
        private const string EXCEPTION_BAD_CREDENTIALS = "Ryusei.Auth.Wrap.UserWrapper.ExceptionBadCredentials";

        private const int IMAGE_WIDTH = 150;
        private const int IMAGE_HEIGHT = 150;
        
        #endregion

        #region [Static Attributes]
        /// <summary>
        ///  Singleton
        /// </summary>
        private static UserWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// IDataabaseMgr
        /// </summary>
        private IUserMgr IUserMgr { get; set; }
        /// <summary>
        /// IUserDataRoleMgr
        /// </summary>
        private IUserRoleMgr IUserRoleMgr { get; set; }
        /// <summary>
        /// IStorageBlobMgr
        /// </summary>
        private IStorageBlobMgr IStorageBlobMgr { get; set; }
        /// <summary>
        /// EmailNotifierWrapper
        /// </summary>
        private EmailWrapper EmailWrapper { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static Constructor
        /// </summary>
        static UserWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private UserWrapper()
        {
            AuthBuilder authBuilder = AuthBuilder.GetInstance();
            this.IUserMgr = authBuilder.GetManager<IUserMgr>(AuthBuilder.IUSERMGR);
            this.IUserRoleMgr = authBuilder.GetManager<IUserRoleMgr>(AuthBuilder.IUSERROLEMGR);
            StorageBuilder storageBuilder = StorageBuilder.GetInstance();
            this.IStorageBlobMgr = storageBuilder.GetManager<IStorageBlobMgr>(StorageBuilder.ISTORAGEBLOBMGR);
            this.EmailWrapper = EmailWrapper.GetInstance();
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance
        /// </summary>
        /// <returns>ApplicationMgr</returns>
        public static UserWrapper GetInstance()
        {
            return Singleton ?? (Singleton = new UserWrapper());
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetByEmail
        /// Description: Method to get a user by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="message">Message</param>
        /// <returns>UserData</returns>
        public User GetByEmail(string email, ref string message)
        {
            // Get the user by email
            User userData = this.IUserMgr.GetByEmail(email);
            if (userData == null)
            {
                message = EXCEPTION_USER_NOT_FOUND;
                return null;
            }
            return userData;
        }
        /// <summary>
        /// Name: ValidateEmailAndPassword
        /// Description: Method to validate email and password of a user
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public User ValidateEmailAndPassword(string email, string password, ref string message)
        {
            // Get the user by email
            User userData = this.IUserMgr.GetByEmail(email);
            if (userData == null)
            {
                message = EXCEPTION_USER_NOT_FOUND;
                return null;
            }
            // Validate the password of user
            if (!Crypter.CheckPassword(password, userData.Password))
            {
                message = EXCEPTION_BAD_CREDENTIALS;
                return null;
            }
            return userData;
        }
        /// <summary>
        /// Name: ChangePassword
        /// Description: Method to change password
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="password">Password</param>
        public void ChangePassword(Guid userId, string password)
        {
            // Check if the user exist
            User user = this.IUserMgr.GetById(userId);
            if (user == null)
                throw new WrapperException(EXCEPTION_USER_NOT_FOUND, new System.Exception(string.Format("User with id: {0}, was not found", userId)));
            // Encryp the new password
            string encryptedNewPassword = Crypter.Blowfish.Crypt(password);
            // Update the password
            this.IUserMgr.UpdatePassword(userId, encryptedNewPassword);
        }
        /// <summary>
        /// Name: ResizeImage
        /// Description: Method to resize image
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Bitmap</returns>
        private Stream ResizeImage(Stream stream, int width, int height)
        {
            Image image = Image.FromStream(stream);


            if (image.PropertyIdList.Contains(0x112))
            {

                var prop = image.GetPropertyItem(0x112);
                int val = BitConverter.ToUInt16(prop.Value, 0);
                var rot = RotateFlipType.RotateNoneFlipNone;

                if (val == 3 || val == 4)
                    rot = RotateFlipType.Rotate180FlipNone;
                else if (val == 5 || val == 6)
                    rot = RotateFlipType.Rotate90FlipNone;
                else if (val == 7 || val == 8)
                    rot = RotateFlipType.Rotate270FlipNone;

                if (val == 2 || val == 4 || val == 5 || val == 7)
                    rot |= RotateFlipType.RotateNoneFlipX;

                if (rot != RotateFlipType.RotateNoneFlipNone)
                    image.RotateFlip(rot);
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            MemoryStream memoryStream = new MemoryStream();
            destImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.Position = 0;
            return memoryStream;
        }
        /// <summary>
        /// Name: UploadProfilePicture
        /// Description: Method to upload a profile picture of user
        /// </summary>
        /// <param name="userId">UserDataId</param>
        /// <param name="profilePicture">ProfilePicture</param>
        public void UploadProfilePicture(Guid userId, Stream profilePicture)
        {
            // Check if the user exist
            User user = this.IUserMgr.GetById(userId);
            if (user == null)
                throw new WrapperException(EXCEPTION_USER_NOT_FOUND, new System.Exception(string.Format("User with Id: {0}, was not found", userId)));
            // Reduce image
            Stream profilePictureResize = this.ResizeImage(profilePicture, IMAGE_WIDTH, IMAGE_HEIGHT);
            // Get storage container
            string storageContainer = ConfigurationManager.AppSettings["STORAGE::CONTAINER::PROFILE_PICTURES"];
            if (!string.IsNullOrEmpty(user.Photo))
                this.IStorageBlobMgr.Delete(storageContainer, user.Photo);
            string photoId = this.IStorageBlobMgr.Upload(storageContainer, profilePictureResize);
            this.IUserMgr.UpdatePhoto(userId, photoId);
        }
        /// <summary>
        /// Name: DownloadProfilePhoto
        /// Description: Method to download profile picture
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string DownloadProfilePhoto(Guid userId)
        {
            // Check if the user exist
            User user = this.IUserMgr.GetById(userId);
            if (user == null)
                throw new WrapperException(EXCEPTION_USER_NOT_FOUND, new System.Exception(string.Format("User with Id: {0}, was not found", userId)));
            if (string.IsNullOrEmpty(user.Photo))
                return "";
            // Get storage container
            string storageContainer = ConfigurationManager.AppSettings["STORAGE::CONTAINER::PROFILE_PICTURES"];
            MemoryStream stream = (MemoryStream)this.IStorageBlobMgr.Download(storageContainer, user.Photo);
            byte[] byteArray = stream.ToArray();
            string base64String = string.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(byteArray));
            return base64String;
        }
        #endregion
    }
}
