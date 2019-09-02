using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Logger.Ent
{
    /// <summary>
    /// Name: SystemLog
    /// Description: Entity class to model system log entry
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class SystemLog
    {
        /// <summary>
        /// SystemLogId
        /// </summary>
        public Guid SystemLogId { get; set; }
        /// <summary>
        /// Server
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// UserDataId
        /// </summary>
        public Guid UserDataId { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// StackTrace
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// InnerMessage
        /// </summary>
        public string InnerMessage { get; set; }
        /// <summary>
        /// InnerStackTrace
        /// </summary>
        public string InnerStackTrace { get; set; }
        /// <summary>
        /// RegisterDate
        /// </summary>
        public DateTime RegisterDate { get; set; }
    }
}
