using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Storage.Fty.Section
{
    /// <summary>
    /// Name: StorageManager
    /// Description: Configuration class to define an element
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@faurecia.com)
    /// LogBook:
    ///     23-08-2019: Creation
    /// </summary>
    public class StorageManager : ConfigurationElement
    {
        /// <summary>
        /// Name
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }
        /// <summary>
        /// Type
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
