using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Storage.Fty.Section
{
    /// <summary>
    /// Name: StorageSection
    /// Description: Configuration class to section
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@faurecia.com)
    /// LogBook:
    ///     23-08-2019: Creation
    /// </summary>
    public class StorageSection : ConfigurationSection
    {
        /// <summary>
        /// Instances
        /// </summary>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public StorageManagerCollection Instances
        {
            get { return (StorageManagerCollection)this[""]; }
            set { this[""] = value; }
        }
    }
}
