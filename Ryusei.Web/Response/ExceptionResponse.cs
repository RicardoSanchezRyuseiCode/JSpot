using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ryusei.Web.Response
{
    /// <summary>
    /// Name: ExceptionResponse
    /// Description: Web class to define web response
    /// Author: Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
    /// LogBook:
    ///     2019-08-20: Creation
    /// </summary>
    public class ExceptionResponse
    {
        /// <summary>
        /// Name: ThrowException
        /// Descripttion: Method to manage the way of exception are visualized for clients and applications
        /// </summary>
        /// <param name="Reason">Reason Message</param>
        /// <param name="ExternalMessage">External message for client</param>
        /// <param name="InternalMessage">Internal message for application</param>
        public static HttpResponseException ThrowException(string Reason, string ExternalMessage)
        {
            return new HttpResponseException(
                new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ExternalMessage),
                    ReasonPhrase = Reason
                }
            );
        }
    }
}
