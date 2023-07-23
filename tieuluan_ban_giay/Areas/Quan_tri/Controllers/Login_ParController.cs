using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tieuluan_ban_giay.Areas.Quan_tri.Controllers
{
    public class Login_ParController : Controller
    {
        // GET: Quan_tri/Login_Par
        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}