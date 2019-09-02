using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Attr.Common
{
    /// <summary>
    /// Name: ClaimType
    /// Description: Class to define Claim Types
    /// Author: Ricardo Sanchez Romero
    /// LogBook:
    ///     12/04/2018: Creation
    /// </summary>
    public class ClaimType
    {
        /// <summary>
        /// UserDataId
        /// </summary>
        public const string USER_ID = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";
        /// <summary>
        /// Name
        /// </summary>
        public const string NAME = "Name";
        /// <summary>
        /// Lastname
        /// </summary>
        public const string LASTNAME = "Lastname";
        /// <summary>
        /// Impersonation
        /// </summary>
        public const string IMPERSONATION = "Impersonation";
    }
}
