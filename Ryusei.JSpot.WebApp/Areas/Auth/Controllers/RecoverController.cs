using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ryusei.JSpot.WebApp.Areas.Auth.Controllers
{
    /// <summary>
    /// Name: RecoverController
    /// Description: Controller to expose views of Recover controller
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-22: Creation
    /// </summary>
    public class RecoverController : Controller
    {
        /// <summary>
        /// Name: Index
        /// Description: Action to return index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Name: Reset
        /// Description: Action to return reset view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reset(string id)
        {
            return View((object)id);
        }
    }
}