using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Exception
{
    /// <summary>
    /// Name: WrapperException
    /// Description: Exception class to define level of error in the system
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class WrapperException : System.Exception
    {
        public WrapperException(string message) : base(message) { }

        public WrapperException(string message, System.Exception exception) : base(message, exception) { }
    }
}
