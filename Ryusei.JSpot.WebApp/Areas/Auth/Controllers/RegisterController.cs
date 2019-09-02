using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ryusei.JSpot.WebApp.Areas.Auth.Controllers
{
    /// <summary>
    /// Name: RegisterController
    /// Descrpition: Controller to expose view interfaces for register
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-21: Creation
    /// </summary>
    public class RegisterController : Controller
    {
        /// <summary>
        /// Name: Index
        /// Description: Method to get index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Name: ValidateUser
        /// Descripiton: Method to get ValidateUser view
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateUser(string id)
        {
            return View((object)id);
        }
    }
}