using Ryusei.JSpot.Auth.Ent;
using Ryusei.Message.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Wrap
{
    public class EmailWrapper
    {
        #region [Constanst]
        /// <summary>
        /// Email Register New User
        /// </summary>
        public const string EMAIL_REGISTER_NEW_USER = @"
            <h2>¡Bienvenido a JSpot!</h2>
            <p>Gracias por registrarte <strong>{0} {1}</strong>, esperamos que tu estancia sea lo mas agradable posible, 
               para terminar con tu registro solo es necesario activar tu cuenta dando click <a href=""{2}"">aqui</a>, si el link 
               no funciona, solo copia y pega la siguiente dirección en tu explorador:</p>
            <p style=""word-break: break-all;"">{2}</p>
        ";
        /// <summary>
        /// Email Recover password
        /// </summary>
        public const string EMAIL_RECOVER_PASSWORD = @"
            <h2>JSpot</h2>
            <p>Hola nuevamente <strong>{0} {1}</strong>, hemos recibido una solicitud para reestablecer tu contraseña, 
               para realizar esta accion da click <a href=""{2}"">aqui</a>, si el link 
               no funciona, solo copia y pega la siguiente dirección en tu explorador:</p>
            <p style=""word-break: break-all;"">{2}</p>
        "; 
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static EmailWrapper Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// EmailTemplate
        /// </summary>
        private string EmailTemplate { get; set; }
        /// <summary>
        /// ImageDictionaty
        /// </summary>
        private Dictionary<string, string> ImageDictionary { get; set; }
        /// <summary>
        /// EmailConfig
        /// </summary>
        private Message.Email.Configuration EmailConfig { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static EmailWrapper()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private EmailWrapper()
        {
            // Get Email Template
            this.EmailTemplate = this.GetEmailTemplate();
            // Get image dictionary
            this.ImageDictionary = new Dictionary<string, string>
            {
                { "Header", System.IO.Path.Combine(ConfigurationManager.AppSettings["ServerHostPath"], "Email/Header.png") },
                { "Bottom", System.IO.Path.Combine(ConfigurationManager.AppSettings["ServerHostPath"], "Email/Bottom.png") }
            };
            // Get email configuration
            this.EmailConfig = new Message.Email.Configuration()
            {
                Host = ConfigurationManager.AppSettings["SMTP:HOST"],
                Port = int.Parse(ConfigurationManager.AppSettings["SMTP:PORT"]),
                User = ConfigurationManager.AppSettings["SMTP:USER"],
                Password = ConfigurationManager.AppSettings["SMTP:PASSWORD"],
                From = ConfigurationManager.AppSettings["SMTP:FROM"],
                SSLConnection = bool.Parse(ConfigurationManager.AppSettings["SMTP:SSL"])
            };

        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static EmailWrapper GetInstance()
        {
            return Singleton == null ? Singleton = new EmailWrapper() : Singleton;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: GetEmailTemplate
        /// Description: Method to get email template
        /// </summary>
        /// <returns></returns>
        private string GetEmailTemplate()
        {
            int countError = 5;
            string emailTemplate = "";
            System.Exception lastEx = null;
            while (countError > 0)
            {
                try
                {
                    emailTemplate = System.IO.File.ReadAllText(System.IO.Path.Combine(ConfigurationManager.AppSettings["ServerHostPath"], "Email/Email.html"));
                    break;
                }
                catch (System.Exception ex)
                {
                    lastEx = ex;
                    countError--;
                    Thread.Sleep(1000);
                }
            }
            if (countError == 0)
                throw lastEx;
            return emailTemplate;
        }
        /// <summary>
        /// Name: SendMailNewUser
        /// Description: Method to send email to new user register in the application
        /// </summary>
        /// <param name="user">User</param>
        public void SendMailNewUser(User user, string link)
        {
            Address addressBook = new Address();
            addressBook.To.Add(user.Email);
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_REGISTER_NEW_USER, user.Name, user.Lastname, link));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Bienvenido ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendRequestResetPassword
        /// Description: Method to send request reset password
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="link">link</param>
        public void SendRequestResetPassword(User user, string link)
        {
            Address addressBook = new Address();
            addressBook.To.Add(user.Email);
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_RECOVER_PASSWORD, user.Name, user.Lastname, link));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Recuperación de contraseña ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        #endregion
    }
}
