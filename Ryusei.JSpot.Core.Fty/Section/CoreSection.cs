using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Core.Fty.Section
{
    /// <summary>
    /// Name: CoreSection
    /// Description: Configuration element to define section
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class CoreSection : ConfigurationSection
    {
        /// <summary>
        /// Instances
        /// </summary>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public CoreManagerCollection Instances
        {
            get { return (CoreManagerCollection)this[""]; }
            set { this[""] = value; }
        }
    }
}
