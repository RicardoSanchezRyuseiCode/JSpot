using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Section
{
    /// <summary>
    /// Name: AuthManager
    /// Description: Configuration element collection for web config
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class AuthManagerCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Name: CreateNewElement
        /// Descrpition: Method to create a new element in section
        /// </summary>
        /// <returns>Configuration Element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AuthManager();
        }
        /// <summary>
        /// Name: GetElementKey
        /// Description: Method to get an element key
        /// </summary>
        /// <param name="element">key of element</param>
        /// <returns>Element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AuthManager)element).Name;
        }
        /// <summary>
        /// Name: this
        /// Descrpition: Method to get a CRM Manager
        /// </summary>
        /// <param name="elementName">ElementName</param>
        /// <returns>CRMManager</returns>
        public new AuthManager this[string elementName]
        {
            get
            {
                return this.OfType<AuthManager>().FirstOrDefault(item => item.Name == elementName);
            }
        }
    }
}
