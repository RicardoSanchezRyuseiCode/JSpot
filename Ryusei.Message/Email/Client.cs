using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Message.Email
{
    public class Client
    {
        #region Atributos
        /// <summary>
        /// Connection configuration for SMTP
        /// </summary>
        private Configuration Configuration { get; set; }
        #endregion

        #region Atributos Estaticos
        /// <summary>
        /// Variable for singleton
        /// </summary>
        private static Client manager;
        #endregion

        #region Constructor
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="Configuration">Configuration for SMTP</param>
        private Client(Configuration Configuration)
        {
            this.Configuration = Configuration;
        }
        #endregion

        #region Constructor Estatico
        /// <summary>
        /// Static Construct
        /// </summary>
        static Client()
        {
            Client.manager = null;
        }
        #endregion

        #region Singleton
        /// <summary>
        /// Name: GetInstance
        /// Description: Singleton access method to use the email client, in the first time is necesario send the configuration
        /// in the next request if want change the configuration cand pass the argument
        /// </summary>
        /// <param name="Configuration">Configuration for Smtp</param>
        /// <returns>Email cliente</returns>
        public static Client GetInstance(Configuration Configuration = null)
        {
            // construcción objecto primera vez
            if (object.ReferenceEquals(Client.manager, null))
            {
                if (object.ReferenceEquals(Configuration, null))
                    throw new Exception("GetInstance First Time - Missing SmtpConfig");
                Client.manager = new Client(Configuration);
            }
            else
            {
                // Cambio de configuración 
                if (!object.ReferenceEquals(Configuration, null))
                    Client.manager = new Client(Configuration);
            }
            return Client.manager;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Name: ReplacePlaceHolders
        /// Description: Method to replace placeholders for values in a string
        /// </summary>
        /// <param name="OriginalString">String with placeholders</param>
        /// <param name="Placeholders">Key - value placeholders</param>
        /// <returns>String with placeholders replaced</returns>
        public static string ReplacePlaceHolders(string OriginalString, Dictionary<string, string> Placeholders)
        {
            if (!string.IsNullOrEmpty(OriginalString))
            {
                string newString = OriginalString;
                if (!object.ReferenceEquals(Placeholders, null))
                {
                    foreach (KeyValuePair<string, string> pair in Placeholders)
                    {
                        newString = newString.Replace(pair.Key, pair.Value);
                    }
                }
                return newString;
            }
            else
                throw new Exception("Empty or Null OriginalString");
        }
        /// <summary>
        /// Name: CreateMailMessage
        /// Description: Method for set the basic configuration of an email message
        /// </summary>
        /// <param name="Address">Address Book</param>
        /// <param name="Subject">Subject</param>
        /// <param name="Attachments">Attachments</param>
        /// <returns>Basic Message</returns>
        private MailMessage CreateMailMessage(Address Address, string Subject, List<string> listAttachments = null)
        {
            // Paso 1: Creamos nuestro mensaje
            MailMessage mail = new MailMessage();
            // Paso 2: Agregamos las direcciones
            foreach (string emailAddress in Address.To)
            {
                mail.To.Add(emailAddress);
            }
            foreach (string emailAddress in Address.CC)
            {
                mail.CC.Add(emailAddress);
            }
            foreach (string emailAddress in Address.BCC)
            {
                mail.Bcc.Add(emailAddress);
            }
            // Paso 3: Agregamos los attachments
            if (!(listAttachments is null))
            {
                foreach (string attach in listAttachments)
                {
                    mail.Attachments.Add(new Attachment(attach));
                }
            }
            // Paso 4: Agregamos asunto
            mail.Subject = Subject;
            // Paso 5: Agregamos mensaje
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(this.Configuration.From, this.Configuration.FromName);
            return mail;
        }
        private MailMessage CreateMailMessage(Address Address, string Subject, Dictionary<string, Stream> dicAttachments = null)
        {
            // Paso 1: Creamos nuestro mensaje
            MailMessage mail = new MailMessage();
            // Paso 2: Agregamos las direcciones
            if (Address.To.Count == 0)
                throw new Exception("List Address \"to\" is empty");
            foreach (string emailAddress in Address.To)
            {
                mail.To.Add(emailAddress);
            }
            foreach (string emailAddress in Address.CC)
            {
                mail.CC.Add(emailAddress);
            }
            foreach (string emailAddress in Address.BCC)
            {
                mail.Bcc.Add(emailAddress);
            }
            // Paso 3: Agregamos los attachments
            if (!(dicAttachments is null))
            {
                foreach (string attachName in dicAttachments.Keys)
                {
                    // Create attachment
                    Attachment attachment = new Attachment(dicAttachments[attachName], attachName);
                    attachment.ContentDisposition.FileName = attachName;
                    mail.Attachments.Add(attachment);
                }
            }
            // Paso 4: Agregamos asunto
            mail.Subject = Subject;
            // Paso 5: Agregamos mensaje
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(this.Configuration.From, this.Configuration.FromName);
            return mail;
        }
        /// <summary>
        /// Name: SendMail
        /// Description: Privete message to send the email
        /// </summary>
        /// <param name="mail">Message to send</param>
        private void SendMail(MailMessage mail)
        {
            SmtpClient sender = new SmtpClient(this.Configuration.Host, this.Configuration.Port);
            if (this.Configuration.User != null && this.Configuration.Password != null)
                sender.Credentials = new NetworkCredential(this.Configuration.User, this.Configuration.Password);
            sender.EnableSsl = this.Configuration.SSLConnection;
            sender.Send(mail);
        }
        /// <summary>
        /// Name: SendMail
        /// Description: Method to send email of text (plain or html)
        /// </summary>
        /// <param name="Address">AddressBook</param>
        /// <param name="Subject">Subject</param>
        /// <param name="Message">Messsage to send</param>
        /// <param name="PlaceHolders">Message placeholders</param>
        /// <param name="listAttachments">Attatchments list</param>
        public void SendMail(Address Address, string Subject, string Message, Dictionary<string, string> PlaceHolders = null, List<string> listAttachments = null)
        {
            MailMessage mail = this.CreateMailMessage(Address, Subject, listAttachments);
            string newMessage = ReplacePlaceHolders(Message, PlaceHolders);
            mail.Body = newMessage;
            this.SendMail(mail);
        }
        /// <summary>
        /// Name: SendMail
        /// Description: Method to send an email with image
        /// </summary>
        /// <param name="Address">Address Book</param>
        /// <param name="Subject">Subject</param>
        /// <param name="Message">Message to send</param>
        /// <param name="ImgInMail">Dictionary of images key = ContentId, Value = PathImg</param>
        /// <param name="PlaceHolders">Messages placeholders</param>
        /// <param name="dicAttachments">Attachments list</param>
        public void SendMailList(Address Address, string Subject, string Message, Dictionary<string, string> ImgInMail, Dictionary<string, string> PlaceHolders = null, List<string> listAttachments = null)
        {
            MailMessage mail = this.CreateMailMessage(Address, Subject, listAttachments);
            string newMessage = ReplacePlaceHolders(Message, PlaceHolders);
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(newMessage, null, MediaTypeNames.Text.Plain);
            mail.AlternateViews.Add(plainView);
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(newMessage, null, MediaTypeNames.Text.Html);
            foreach (KeyValuePair<string, string> Img in ImgInMail)
            {
                LinkedResource lkr = new LinkedResource(Img.Value, "image/png")
                {
                    ContentId = Img.Key
                };
                htmlView.LinkedResources.Add(lkr);
            }
            mail.AlternateViews.Add(htmlView);
            this.SendMail(mail);
        }
        /// <summary>
        /// Name: SendMail
        /// </summary>
        /// <param name="Address">Address</param>
        /// <param name="Subject">Subject</param>
        /// <param name="Message">Message</param>
        /// <param name="ImgInMail">ImgInMail</param>
        /// <param name="PlaceHolders"></param>
        /// <param name="dicAttachments"></param>
        public void SendMailDictionary(Address Address, string Subject, string Message, Dictionary<string, string> ImgInMail, Dictionary<string, string> PlaceHolders = null, Dictionary<string, Stream> dicAttachments = null)
        {
            MailMessage mail = this.CreateMailMessage(Address, Subject, dicAttachments);
            string newMessage = ReplacePlaceHolders(Message, PlaceHolders);
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(newMessage, null, MediaTypeNames.Text.Plain);
            mail.AlternateViews.Add(plainView);
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(newMessage, null, MediaTypeNames.Text.Html);
            foreach (KeyValuePair<string, string> Img in ImgInMail)
            {
                LinkedResource lkr = new LinkedResource(Img.Value, "image/png")
                {
                    ContentId = Img.Key
                };
                htmlView.LinkedResources.Add(lkr);
            }
            mail.AlternateViews.Add(htmlView);
            this.SendMail(mail);
        }
        #endregion
    }
}
