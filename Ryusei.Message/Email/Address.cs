using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Message.Email
{
    /// <summary>
    /// Name: Address
    /// Description: Class for address
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class Address
    {
        #region [Attributes]
        /// <summary>
        /// Direction Address - "TO"
        /// </summary>
        public List<string> To;
        /// <summary>
        /// Direction Address - "CC"
        /// </summary>
        public List<string> CC;
        /// <summary>
        /// Direction Address - "BCC"
        /// </summary>
        public List<string> BCC;
        #endregion
        /// <summary>
        /// Construct Default
        /// </summary>
        public Address()
        {
            this.To = new List<string>();
            this.CC = new List<string>();
            this.BCC = new List<string>();
        }
    }
}
