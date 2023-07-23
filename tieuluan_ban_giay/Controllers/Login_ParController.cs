using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tieuluan_ban_giay.Controllers
{
    public class Login_ParController : Controller
    {
        // GET: Login_Par
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}