using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tieuluan_ban_giay.Models;

namespace tieuluan_ban_giay.Controllers
{
    public class GioHangController : Controller
    {
        QLBangiayyEntities db = new QLBangiayyEntities();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int MG, string url)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(row => row.Ma_Giay == MG);
            if (sp == null)
            {
                sp = new GioHang(MG);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.So_Luong++;
            }
            return Redirect(url);
        }
        public int TongSoLuong()
        {
            int TongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                TongSoLuong = lstGioHang.Sum(row=>row.So_Luong);
            }
            return TongSoLuong;
        }
        public double LayTongTien()
        {
            return get_TongTien;
        }

        static double get_TongTien = 0;

        public double TongTien()
        {
            
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                get_TongTien = lstGioHang.Sum(row=>row.Thanh_Tien);
            }
            return get_TongTien;
        }
        public ActionResult GioHang()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult XoaSPKhoiGioHang(int id)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(row=>row.Ma_Giay==id);
            if (sp != null)
            {
                lstGioHang.RemoveAll(row=>row.Ma_Giay==id);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhapHioHang(int id, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(row=>row.Ma_Giay==id);
            if (sp != null)
            {
                sp.So_Luong = int.Parse(f["So_Luong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            Don_Dat_Hang ddh = new Don_Dat_Hang();
            Khach_Hang kh = (Khach_Hang)Session["TaiKhoan"];
            List<GioHang> lstGioHang = LayGioHang();
            ddh.Ma_Khach_Hang = kh.Ma_Khach_Hang;
            string Ngaydathang = DateTime.Now.ToString();
            ddh.Ngay_Dat_Hang = Convert.ToDateTime(Ngaydathang);
            ddh.Ten_Khach_Hang = f["Ten_Khach_Hang"];
            
            ddh.DiaChi_Khach_Hang = f["DiaChi_Khach_Hang"];
            ddh.DienThoai_khach_Hang = f["DienThoai_Khach_Hang"];
            string NgayGiao = string.Format("{0:MM/dd/yyyy}", f["NgayGiao"].ToString());
            ddh.NgayGiaoHang = Convert.ToDateTime(Convert.ToString(NgayGiao));
            ddh.TriGia = (decimal?)TongTien();
            ddh.DaGiao = true.ToString();
            ddh.HinhThucGiaoHang = true.ToString();
            ddh.HinhThucThanhToan = false.ToString();
            db.Don_Dat_Hang.Add(ddh);
           
            try
            {
                db.SaveChanges();
                foreach (var item in lstGioHang)
                {
                    CT_DatHang ctdh = new CT_DatHang();
                    ctdh.So_Don_Hang = ddh.So_Don_Hang;
                    ctdh.Ma_Giay = item.Ma_Giay;
                    ctdh.SoLuong = item.So_Luong;
                    ctdh.DonGia = (decimal)item.DonGia;
                    db.CT_DatHang.Add(ctdh);
                }
                db.SaveChanges();
                return RedirectToAction("XacNhanDonHang", "GioHang");
            }
            catch 
            {
                return RedirectToAction("XacNhanDonHang", "GioHang");
            }
            
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}