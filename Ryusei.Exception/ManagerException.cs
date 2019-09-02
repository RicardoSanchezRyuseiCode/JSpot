using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Exception
{
    /// <summary>
    /// Name: ManagerException
    /// Description: Exception class to define level of error in the system
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class ManagerException : System.Exception
    {
        public ManagerException(string message) : base(message) { }

        public ManagerException(string message, System.Exception exception) : base(message, exception) { }
    }
}
