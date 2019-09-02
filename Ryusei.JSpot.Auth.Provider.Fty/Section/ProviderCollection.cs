using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Provider.Fty.Section
{
    /// <summary>
    /// Name: ProviderCollection
    /// Description: Class to define element of section in configuration
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class ProviderCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Name:CreateNewElement
        /// Descrpition: Method to create new Element
        /// </summary>
        /// <returns>Provider</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new Provider();
        }
        /// <summary>
        /// Name: GetElementKey
        /// Description: Method to get configuration element
        /// </summary>
        /// <param name="element">Element</param>
        /// <returns>Configuration Element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Provider)element).Name;
        }
        /// <summary>
        /// Name: Method to select a configuration element
        /// </summary>
        /// <param name="elementName">ElementName</param>
        /// <returns></returns>
        public new Provider this[string elementName]
        {
            get
            {
                return this.OfType<Provider>().FirstOrDefault(item => item.Name == elementName);
            }
        }
    }
}
