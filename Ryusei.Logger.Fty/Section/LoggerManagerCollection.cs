using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Logger.Fty.Section
{
    /// <summary>
    /// Name: LoggerManager
    /// Description: Configuration element collection for section
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class LoggerManagerCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Name: CreateNewElement
        /// Descrpition: Method to create a new element in section
        /// </summary>
        /// <returns>Configuration Element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new LoggerManager();
        }
        /// <summary>
        /// Name: GetElementKey
        /// Description: Method to get an element key
        /// </summary>
        /// <param name="element">key of element</param>
        /// <returns>Element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LoggerManager)element).Name;
        }
        /// <summary>
        /// Name: this
        /// Descrpition: Method to get a Logger Manager
        /// </summary>
        /// <param name="elementName">ElementName</param>
        /// <returns>LoggerManager</returns>
        public new LoggerManager this[string elementName]
        {
            get
            {
                return this.OfType<LoggerManager>().FirstOrDefault(item => item.Name == elementName);
            }
        }
    }
}
