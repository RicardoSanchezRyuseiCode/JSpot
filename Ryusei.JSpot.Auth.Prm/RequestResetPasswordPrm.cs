using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Prm
{
    /// <summary>
    /// Name: RequestPasswordPrm
    /// Description: Parameter class to define model of password reset
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-22: Creation
    /// </summary>
    public class RequestResetPasswordPrm
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Captcha token
        /// </summary>
        public string CaptchaToken { get; set; }
    }
}
