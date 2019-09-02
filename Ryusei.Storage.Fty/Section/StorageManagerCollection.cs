using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Storage.Fty.Section
{
    /// <summary>
    /// Name: StorageManagerCollection
    /// Description: Configuration class to define a collection elements
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@faurecia.com)
    /// LogBook:
    ///     23-08-2019: Creation
    /// </summary>
    public class StorageManagerCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Name: CreateNewElement
        /// Descrpition: Method to create a new element in section
        /// </summary>
        /// <returns>Configuration Element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new StorageManager();
        }
        /// <summary>
        /// Name: GetElementKey
        /// Description: Method to get an element key
        /// </summary>
        /// <param name="element">key of element</param>
        /// <returns>Element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((StorageManager)element).Name;
        }
        /// <summary>
        /// Name: this
        /// Descrpition: Method to get a CRM Manager
        /// </summary>
        /// <param name="elementName">ElementName</param>
        /// <returns>CRMManager</returns>
        public new StorageManager this[string elementName]
        {
            get
            {
                return this.OfType<StorageManager>().FirstOrDefault(item => item.Name == elementName);
            }
        }
    }
}
