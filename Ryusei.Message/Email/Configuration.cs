using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Message.Email
{
    /// <summary>
    /// Name: Configuration
    /// Description: Class to store configuration
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class Configuration
    {
        #region [Attributes]
        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Flag SSL
        /// </summary>
        public bool SSLConnection { get; set; }
        /// <summary>
        /// User for SMTP
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Password for SMTP
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// From
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// FromName
        /// </summary>
        public string FromName { get; set; }
        #endregion

        #region [Construct]
        /// <summary>
        /// Construct
        /// </summary>
        public Configuration() { }
        /// <summary>
        /// Construct
        /// SSL - False
        /// </summary>
        /// <param name="Host">Host Smtp</param>
        /// <param name="Port">Port Smtp</param>
        /// <param name="User">User</param>
        /// <param name="Password">Password</param>
        public Configuration(string Host, int Port, string User, string Password, string From)
        {
            this.Host = Host;
            this.Port = Port;
            this.SSLConnection = false;
            this.User = User;
            this.Password = Password;
            this.From = From;
        }
        /// <summary>
        /// Construct
        /// SSL Variable
        /// </summary>
        /// <param name="Host">Host Smtp</param>
        /// <param name="Port">Port Smtp</param>
        /// <param name="SSLConnection">Connection SSL</param>
        /// <param name="User">User</param>
        /// <param name="Password">Password</param>
        /// <param name="From">From</param>
        public Configuration(string Host, int Port, bool SSLConnection, string User, string Password, string From)
        {
            this.Host = Host;
            this.Port = Port;
            this.SSLConnection = SSLConnection;
            this.User = User;
            this.Password = Password;
            this.From = From;
        }
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="Host">Host SMTP</param>
        /// <param name="Port">Port SMTP</param>
        /// <param name="From">From</param>
        public Configuration(string Host, int Port, string From, bool SSLConnection = false)
        {
            this.Host = Host;
            this.Port = Port;
            this.User = null;
            this.Password = null;
            this.SSLConnection = SSLConnection;
            this.From = From;
        }
        #endregion
    }
}
