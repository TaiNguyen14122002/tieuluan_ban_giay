using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tieuluan_ban_giay.Models;
using PagedList;

namespace tieuluan_ban_giay.Areas.Quan_tri.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        QLBangiayyEntities db = new QLBangiayyEntities();
        // GET: Quan_tri/QuanLyNguoiDung
        public ActionResult Index(int ? page)
        {
            int iSize = 6;
            int PageNum =(page ?? 1);
            return View(db.Khach_Hang.ToList().OrderBy(row=>row.Ma_Khach_Hang).ToPagedList(PageNum,iSize));
        }
        [HttpGet]
        public ActionResult ThemNguoiDung()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemNguoiDung(Khach_Hang kh)
        {
            db.Khach_Hang.Add(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaNguoiDung(int id)
        {
            Khach_Hang kh = db.Khach_Hang.Where(row => row.Ma_Khach_Hang == id).SingleOrDefault();
            return View(kh);
        }
        [HttpPost]
        public ActionResult XoaNguoiDung(Khach_Hang khh)
        {
            Khach_Hang kh = db.Khach_Hang.Where(row => row.Ma_Khach_Hang == khh.Ma_Khach_Hang).SingleOrDefault();
            db.Khach_Hang.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SuaNguoiDung(int id)
        {
            Khach_Hang kh = db.Khach_Hang.Where(row => row.Ma_Khach_Hang == id).SingleOrDefault();
            return View(kh);
        }
        [HttpPost]
        public ActionResult SuaNguoiDung(Khach_Hang khh)
        {
            Khach_Hang kh = db.Khach_Hang.Where(row => row.Ma_Khach_Hang == khh.Ma_Khach_Hang).SingleOrDefault();
            kh.Ten_Khach_Hang = khh.Ten_Khach_Hang;
            kh.DiaChi_Khach_Hang = khh.DiaChi_Khach_Hang;
            kh.DienThoai_khach_Hang = khh.DienThoai_khach_Hang;
            kh.GioiTinh = khh.GioiTinh;
            kh.Email = khh.Email;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChiTietNguoiDung(int id)
        {
            Khach_Hang kh = db.Khach_Hang.Where(row => row.Ma_Khach_Hang == id).SingleOrDefault();
            return View(kh);
        }
    }
}