using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ryusei.JSpot.WebApp.Areas.Auth.Controllers
{
    /// <summary>
    /// Name: LogincController
    /// Descrpition: Controller to expose view interfaces of Index
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-21: Creation
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Name: Index
        /// Description: Method to expose index view
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}