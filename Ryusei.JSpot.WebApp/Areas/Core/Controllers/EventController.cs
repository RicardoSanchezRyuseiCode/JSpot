using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ryusei.JSpot.WebApp.Areas.Core.Controllers
{
    /// <summary>
    /// Name: EventController
    /// Description: Controller to expose user interfaces of event
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class EventController : Controller
    {
        /// <summary>
        /// Name: Index
        /// Description: Controller to expose index view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Ryusei.JSpot.Auth.Attr.Mvc.Authorize(ServerName = "Jspot Web Server")]
        public ActionResult Index(string id)
        {
            return View((object)id);
        }
    }
}