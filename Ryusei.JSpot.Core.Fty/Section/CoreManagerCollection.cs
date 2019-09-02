using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Section
{
    /// <summary>
    /// Name: CoreManagerCollection
    /// Description: Configuration element to define collection of CoreManager
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class CoreManagerCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Name: CreateNewElement
        /// Descrpition: Method to create a new element in section
        /// </summary>
        /// <returns>Configuration Element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CoreManager();
        }
        /// <summary>
        /// Name: GetElementKey
        /// Description: Method to get an element key
        /// </summary>
        /// <param name="element">key of element</param>
        /// <returns>Element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CoreManager)element).Name;
        }
        /// <summary>
        /// Name: this
        /// Descrpition: Method to get a CRM Manager
        /// </summary>
        /// <param name="elementName">ElementName</param>
        /// <returns>CRMManager</returns>
        public new CoreManager this[string elementName]
        {
            get
            {
                return this.OfType<CoreManager>().FirstOrDefault(item => item.Name == elementName);
            }
        }
    }
}
