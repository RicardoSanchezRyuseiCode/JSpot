using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ryusei.JSpot.Core.WebApi.Controllers
{
    /// <summary>
    /// Name: CommentController
    /// Description: Cotnroller to expose the endpoints for CommentController
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    [RoutePrefix("api/Comment")]
    public class CommentController : Ryusei.JSpot.Auth.WebBase.AuthBaseController
    {
    }
}
