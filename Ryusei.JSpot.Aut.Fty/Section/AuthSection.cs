using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Fty.Section
{
    /// <summary>
    /// Name: AuthSection
    /// Description: Configuration section for web config
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class AuthSection : ConfigurationSection
    {
        /// <summary>
        /// Instances
        /// </summary>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public AuthManagerCollection Instances
        {
            get { return (AuthManagerCollection)this[""]; }
            set { this[""] = value; }
        }
    }
}
