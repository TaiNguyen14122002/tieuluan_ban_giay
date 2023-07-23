using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tieuluan_ban_giay.Models;

namespace tieuluan_ban_giay.Areas.Quan_tri.Controllers
{
    public class QuanLyTheLoaiController : Controller
    {
        QLBangiayyEntities db = new QLBangiayyEntities();
        // GET: Quan_tri/QuanLyTheLoai
        public ActionResult Index(int ? page)
        {
            int iSize = 6;
            int PageNum = (page ?? 1);
            return View(db.The_Loai.ToList().OrderBy(row => row.Ma_TheLoai).ToPagedList(PageNum, iSize));
        }
        [HttpGet]
        public ActionResult ThemTheLoai()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemTheLoai(The_Loai tl)
        {

            db.The_Loai.Add(tl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SuaTheLoai(int id)
        {
            The_Loai tl = db.The_Loai.Where(row => row.Ma_TheLoai == id).SingleOrDefault();
            return View(tl);
        }
        [HttpPost]
        public ActionResult SuaTheLoai(int id, The_Loai tll)
        {
            The_Loai tl = db.The_Loai.Where(row => row.Ma_TheLoai == tll.Ma_TheLoai).SingleOrDefault();
            tl.Ten_TheLoai = tll.Ten_TheLoai;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChiTietTheLoai(int id)
        {
            The_Loai tl = db.The_Loai.Where(row => row.Ma_TheLoai == id).SingleOrDefault();
            return View(tl);
        }
        [HttpGet]
        public ActionResult Xoa(int id)
        {
            The_Loai tl = db.The_Loai.Where(row => row.Ma_TheLoai == id).SingleOrDefault();
            return View(tl);
        }
        [HttpPost]
        public ActionResult Xoa(int id, The_Loai tll)
        {
            The_Loai tl = db.The_Loai.Where(row => row.Ma_TheLoai == tll.Ma_TheLoai).SingleOrDefault();
            db.The_Loai.Remove(tl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}