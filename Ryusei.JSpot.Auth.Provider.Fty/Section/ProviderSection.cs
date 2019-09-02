using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Provider.Fty.Section
{
    /// <summary>
    /// Name: ProviderSection
    /// Description: Class to define section of provider
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class ProviderSection : ConfigurationSection
    {
        /// <summary>
        /// Instances
        /// </summary>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public ProviderCollection Instances
        {
            get { return (ProviderCollection)this[""]; }
            set { this[""] = value; }
        }
    }
}
