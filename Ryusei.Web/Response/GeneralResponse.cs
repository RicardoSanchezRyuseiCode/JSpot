using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Web.Response
{
    /// <summary>
    /// Name: GeneralResponse
    /// Description: Class to define a general web response
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     12/05/2018: Creation
    /// </summary>
    public class GeneralResponse
    {
        #region [Attributes]
        /// <summary>
        /// Error
        /// </summary>
        public bool Error { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public object Message { get; set; }
        #endregion
    }
}
