using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tieuluan_ban_giay.Models;

namespace tieuluan_ban_giay.Areas.Quan_tri.Controllers
{

    public class HomeController : Controller
    {
        QLBangiayyEntities db = new QLBangiayyEntities();
        // GET: Quan_tri/Home
        public ActionResult Index()
        {
            
            if (Session["TaiKhoan2"] ==null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        public ActionResult BanDo()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            if (Session["TaiKhoan"]==null)
            {
                return RedirectToAction("Login");
            }    
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f, Quan_Tri admin)
        {
            var sTenDN = f["TenDN_Admin"];
            var sMatKhau = f["MatKhau"];
            Quan_Tri qt = db.Quan_Tri.Where(row => row.TenDN_Admin == sTenDN).SingleOrDefault();
            if (qt != null)
            {
                
                qt.MatKhau_Admin = sMatKhau;
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ViewBag.ThongBao2 = "Thay đổi mật khẩu thất bại";
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var sTenDN = f["TenDN_Admin"];
            var sMatKhau = f["MatKhau_Admin"];
            Quan_Tri qt = db.Quan_Tri.SingleOrDefault(row => row.TenDN_Admin == sTenDN && row.MatKhau_Admin == sMatKhau);
            if (qt != null)
            {
                Session["TaiKhoan2"] = qt;
                return RedirectToAction("Index");
            }
            ViewBag.ThongBao = "Tên Tài khoản hoặc mật khẩu không đúng";
            return View();
        }
        
    }
}