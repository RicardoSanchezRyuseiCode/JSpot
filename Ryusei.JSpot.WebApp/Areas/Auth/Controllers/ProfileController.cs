using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ryusei.JSpot.WebApp.Areas.Auth.Controllers
{
    /// <summary>
    /// Name: ProfileController
    /// Description:Controller to expose interfaces of Profile
    /// Author: Ricardo Sanchez Romero (ricardo.rsz.sanchez@faurecia.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class ProfileController : Controller
    {
        /// <summary>
        /// Name: Index
        /// Description: Method to return index view
        /// </summary>
        /// <returns></returns>
        /// 
        [Ryusei.JSpot.Auth.Attr.Mvc.Authorize(ServerName = "Jspot Web Server")]
        public ActionResult Index()
        {
            return View();
        }
    }
}