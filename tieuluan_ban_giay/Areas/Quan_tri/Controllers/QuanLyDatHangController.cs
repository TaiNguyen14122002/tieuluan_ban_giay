using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tieuluan_ban_giay.Models;
using PagedList.Mvc;
using PagedList;

namespace tieuluan_ban_giay.Areas.Quan_tri.Controllers
{
    public class QuanLyDatHangController : Controller
    {
        QLBangiayyEntities db = new QLBangiayyEntities();
        // GET: Quan_tri/QuanLyDatHang
        public ActionResult Index(int ? page)
        {
            ViewBag.HienThi = " Quản lý đặt hàng";
            int iSize = 6;
            int iPageNum = (page ?? 1);
            return View(db.Don_Dat_Hang.ToList().OrderBy(n => n.So_Don_Hang).ToPagedList(iPageNum, iSize));
        }
        public ActionResult Them()
        {
            Console.WriteLine("Không thể thêm đặt hàng ở trang quản trị");
            return View();
        }
        [HttpGet]
        public ActionResult Sua(int id)
        {
            Don_Dat_Hang ddh = db.Don_Dat_Hang.Where(row => row.So_Don_Hang == id).SingleOrDefault();
            return View(ddh);
        }
        [HttpPost]
        public ActionResult Sua(Don_Dat_Hang dh)
        {
            Don_Dat_Hang ddh = db.Don_Dat_Hang.Where(row => row.So_Don_Hang == dh.So_Don_Hang).SingleOrDefault();
            //Cap nhap
            ddh.Ten_Khach_Hang = dh.Ten_Khach_Hang;
            ddh.DiaChi_Khach_Hang = dh.DiaChi_Khach_Hang;
            ddh.DienThoai_khach_Hang = dh.DienThoai_khach_Hang;
            ddh.TriGia = dh.TriGia;
            ddh.HinhThucThanhToan = dh.HinhThucThanhToan;
            ddh.HinhThucGiaoHang = dh.HinhThucGiaoHang;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult chitiet(int id)
        {
            Don_Dat_Hang ddh = db.Don_Dat_Hang.Where(row => row.So_Don_Hang == id).SingleOrDefault();
            return View(ddh);
        }
        [HttpGet]
        public ActionResult Xoa(int id)
        {
            Don_Dat_Hang ddh = db.Don_Dat_Hang.Where(row => row.So_Don_Hang == id).SingleOrDefault();
            return View(ddh);
        }
        [HttpPost]
        public ActionResult Xoa(int id, Don_Dat_Hang dh)
        {
            Don_Dat_Hang ddh = db.Don_Dat_Hang.Where(row => row.So_Don_Hang == dh.So_Don_Hang).SingleOrDefault();
            db.Don_Dat_Hang.Remove(ddh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}