using Ryusei.JSpot.Auth.Ent;
using Ryusei.JSpot.Core.Ent;
using Ryusei.Message.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Wrap
{
    /// <summary>
    /// Name: EmailWrapper
    /// Description: Wrapper for email interaction
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class EmailWrapper
    {
        #region [Constanst]
        /// <summary>
        /// Email Register New User
        /// </summary>
        public const string EMAIL_INVITATION_REJECT = @"
            <h2>Estimados organizadores del evento: {0}</h2>
            <p>Enviamos esta notification ya que el usuario: {1}, ha rechazado su invitación al evento antes mencionado.</p>
        ";

        public const string EMAIL_INVITATION = @"
            <h2>Hola buen dia</h2>
            <p>
                Has recibido esta invitacion para participar en el evento: {0}, a través de <a href=""{1}"">JSpot</a>, si aún no te has registrado te invitamos a que lo hagas y si ya estas registrado no olvides
                aceptar la invitación.
            </p>
        ";

        public const string EMAIL_ADDED_AS_OWNER = @"
            <h2>Estimado: {0} {1}</h2>
            <p>Te informamos que has sido escogido como organizador para el evento {2}, ahora podras ver información adicional al visualizar el evento.</p>
        ";

        public const string EMAIL_REMOVE_AS_OWNER = @"
            <h2>Estimado: {0} {1}</h2>
            <p>Te informamos que ya no erer organizador para el evento {2}, por lo que dejaras de ver cierta información  al visualizar el evento.</p>
        ";

        public const string EMAIL_SIGNIN = @"
            <h2>Estimado: {0}</h2>
            <p>Esta es una confirmación de inscripcion al grupo: {1}, en el evento: {2} recuerda checar los detalles para que no tengas ningún problema</p>
        ";

        public const string EMAIL_SIGNIN_NOTICE = @"
            <h2>Estimados organizadores del evento: {0}</h2>
            <p>Te informamos que el usuario: {1} ({2}), se ha inscrito al grupo {3}</p>
        ";

        public const string EMAIL_GROUP_FULL = @"
            <h2>Estimados organizadores del evento: {0}</h2>
            <p>Te informamos que el grupo: {1}, se ha completado</p>
        ";


        public const string EMAIL_TRANSPORT_CONFIRMATION = @"
            <h2>Estimado: {0}</h2>
            <p>Esta es una confirmación del transporte seleccionado: {1}, para el evento: {2} recuerda checar los detalles para que no tengas ningún problema</p>
        ";

        public const string EMAIL_TRANSPORT_NOTICE = @"
            <h2>Estimado: {0}</h2>
            <p>Esta es una notificación para informarte que el usuario: {1}, ha elegido tu auto {2} como opción de transporte para el evento {3}</p>
        ";

        public const string EMAIL_TRANSPORT_FULL = @"
            <h2>Estimado: {0}</h2>
            <p>Esta es una notificación para informarte que tu auto {1} esta completo de pasajeros para el evento {2}</p>
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
        public void SendMailInvitationReject(IEnumerable<User> collectionUser, string rejectedUserName, string rejectedUserLastname, string rejectedUserEmail, Event @event)
        {
            Message.Email.Address addressBook = new Message.Email.Address();
            foreach (User user in collectionUser)
            {
                addressBook.To.Add(user.Email);
            }
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_INVITATION_REJECT, @event.Name, string.Format("{0} {1} ({2})", rejectedUserName, rejectedUserLastname, rejectedUserEmail)));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Invitación Rechazada ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendMailAddedAsOwner
        /// Description: Method to send email when user id added as owner
        /// </summary>
        /// <param name="user"></param>
        /// <param name="event"></param>
        public void SendMailAddedAsOwner(User user, Event @event)
        {
            Message.Email.Address addressBook = new Message.Email.Address();
            addressBook.To.Add(user.Email);
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_ADDED_AS_OWNER, @event.Name, string.Format("{0} {1} ({2})", user.Name, user.Lastname, @event.Name)));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Evento ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendMailRemoveAsOwner
        /// Description: Method to send email when user is removed as owner
        /// </summary>
        /// <param name="user"></param>
        /// <param name="event"></param>
        public void SendMailRemoveAsOwner(User user, Event @event)
        {
            Message.Email.Address addressBook = new Message.Email.Address();
            addressBook.To.Add(user.Email);
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_REMOVE_AS_OWNER, @event.Name, string.Format("{0} {1} ({2})", user.Name, user.Lastname, @event.Name)));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Evento ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendMailInvitations
        /// Description: Method to send email invitation for user
        /// </summary>
        /// <param name="collectionInvitations">Collection invitations</param>
        /// <param name="event">Event</param>
        public void SendMailInvitations(IEnumerable<Invitation> collectionInvitations, Event @event)
        {
            foreach (Invitation invitation in collectionInvitations)
            {
                Message.Email.Address addressBook = new Message.Email.Address();
                addressBook.To.Add(invitation.Email);
                // Place holders
                Dictionary<string, string> placeDicc = new Dictionary<string, string>();
                placeDicc.Add("@@message", string.Format(EMAIL_INVITATION, @event.Name, ConfigurationManager.AppSettings["JSPOT::FRONTEND:URL"]));
                // Send the email
                Client emailClient = Client.GetInstance(this.EmailConfig);
                emailClient.SendMailList(addressBook, ".:: JSpot - Invitación ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
            }
            /*// Create address book
            Message.Email.Address addressBook = new Message.Email.Address();
            // Loop in invitations
            foreach (Invitation invitation in collectionInvitations)
            {
                addressBook.BCC.Add(invitation.Email);
            }
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_INVITATION, @event.Name, ConfigurationManager.AppSettings["JSPOT::FRONTEND:URL"]));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Invitación ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);*/
        }
        /// <summary>
        /// Name: SendMailSignIn
        /// Description: Method to SendMailSignIn
        /// </summary>
        /// <param name="eventGroup"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="lastname"></param>
        public void SendMailSignIn(EventGroup eventGroup, Event @event,  string email, string name, string lastname)
        {
            // Create address book
            Message.Email.Address addressBook = new Message.Email.Address();
            addressBook.To.Add(email);
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_SIGNIN, string.Format("{0} {1}", name, lastname), eventGroup.Name, @event.Name));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Inscripción ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendMailSignInOwners
        /// Description: Method to send mail of sign in to owners
        /// </summary>
        /// <param name="eventGroup"></param>
        /// <param name="assistant"></param>
        public void SendMailSignInOwners(EventGroup eventGroup, Event @event, string email, string name, string lastname, IEnumerable<Assistant> collectionAssistant)
        {
            // Create address book
            Message.Email.Address addressBook = new Message.Email.Address();
            foreach (Assistant assistant in collectionAssistant)
            {
                addressBook.To.Add(assistant.User.Email);
            }
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_SIGNIN_NOTICE, @event.Name, string.Format("{0} {1}", name, lastname), email, eventGroup.Name));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Aviso ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendMailGroupFull
        /// Description: Method to send mail when group is full
        /// </summary>
        /// <param name="eventGroup"></param>
        /// <param name="event"></param>
        public void SendMailGroupFull(EventGroup eventGroup, Event @event, IEnumerable<Assistant> collectionAssistant)
        {
            // Create address book
            Message.Email.Address addressBook = new Message.Email.Address();
            foreach (Assistant assistant in collectionAssistant)
            {
                addressBook.To.Add(assistant.User.Email);
            }
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_GROUP_FULL, @event.Name, eventGroup.Name));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Grupo Completo ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendMailTransportPassengerConfirmation
        /// Description: Method to send email of confirmation for passenger added
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="name">Name</param>
        /// <param name="lastname">Lastname</param>
        /// <param name="transport">Transport</param>
        /// <param name="event">Event</param>
        public void SendMailTransportPassengerConfirmation(string email, string name, string lastname, Transport transport,  Event @event)
        {
            // Create address book
            Message.Email.Address addressBook = new Message.Email.Address();
            addressBook.To.Add(email);            
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_TRANSPORT_CONFIRMATION, string.Format("{0} {1}", name, lastname), string.Format("{0} - {1}", transport.Car.Model, transport.Car.Brand), @event.Name));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Transporte ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendMailTransportPassengerAdded
        /// Description: Method to send email to transport owner of confirmation
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="email">Email</param>
        /// <param name="name">Name</param>
        /// <param name="lastname">Lastname</param>
        /// <param name="transport">Transport</param>
        /// <param name="event">Event</param>
        public void SendMailTransportPassengerAdded(User user, string email, string name, string lastname, Transport transport, Event @event)
        {
            // Create address book
            Message.Email.Address addressBook = new Message.Email.Address();
            addressBook.To.Add(user.Email);
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_TRANSPORT_NOTICE, string.Format("{0} {1}", user.Name, user.Lastname), string.Format("{0} {1} ({2})", name, lastname, email), string.Format("{0} - {1}", transport.Car.Model, transport.Car.Brand), @event.Name ));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Transporte ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        /// <summary>
        /// Name: SendMailTransportFull
        /// Description: Method to send email to owner transport when tranposrt is full
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        public void SendMailTransportFull(User user, Transport transport, Event @event)
        {
            // Create address book
            Message.Email.Address addressBook = new Message.Email.Address();
            addressBook.To.Add(user.Email);
            // Place holders
            Dictionary<string, string> placeDicc = new Dictionary<string, string>();
            placeDicc.Add("@@message", string.Format(EMAIL_TRANSPORT_FULL, string.Format("{0} {1}", user.Name, user.Lastname), string.Format("{0} - {1}", transport.Car.Model, transport.Car.Brand), @event.Name));
            // Send the email
            Client emailClient = Client.GetInstance(this.EmailConfig);
            emailClient.SendMailList(addressBook, ".:: JSpot - Transporte Completo ::.", this.EmailTemplate, ImgInMail: this.ImageDictionary, PlaceHolders: placeDicc);
        }
        #endregion
    }
}
