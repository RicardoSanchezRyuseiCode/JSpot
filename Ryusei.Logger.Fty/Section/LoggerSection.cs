using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Logger.Fty.Section
{
    /// <summary>
    /// Name: LoggerSection
    /// Description: Section definition
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class LoggerSection : ConfigurationSection
    {
        /// <summary>
        /// Instances
        /// </summary>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public LoggerManagerCollection Instances
        {
            get { return (LoggerManagerCollection)this[""]; }
            set { this[""] = value; }
        }
    }
}
