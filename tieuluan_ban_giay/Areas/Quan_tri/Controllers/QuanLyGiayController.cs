using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tieuluan_ban_giay.Models;
using PagedList;
using System.IO;

namespace tieuluan_ban_giay.Areas.Quan_tri.Controllers
{
    public class QuanLyGiayController : Controller
    {
        QLBangiayyEntities db = new QLBangiayyEntities();
        // GET: Quan_tri/QuanLyGiay
        public ActionResult Index(int ? page, Giay g)
        {
            ViewBag.Mota = g.MoTa;
            int iSize = 6;
            int iPageNum = (page ?? 1);
            return View(db.Giays.ToList().OrderBy(n => n.Ma_Giay).ToPagedList(iPageNum,iSize));
        }
        [HttpGet]
        public ActionResult XoaGiay( int id)
        {
            Giay giay = db.Giays.Where(row => row.Ma_Giay == id).SingleOrDefault();
            return View(giay);
        }
        [HttpPost]
        public ActionResult XoaGiay( int id, Giay gg)
        {
            Giay giay = db.Giays.Where(row => row.Ma_Giay == gg.Ma_Giay).SingleOrDefault();
            db.Giays.Remove(giay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChiTietGiay(int id)
        {
            Giay g = db.Giays.Where(row => row.Ma_Giay == id).SingleOrDefault();
            ViewBag.Ma_TheLoai = new SelectList(db.The_Loai.ToList().OrderBy(row => row.Ten_TheLoai), "Ma_TheLoai", "Ten_TheLoai");

            return View(g);
        }
        [HttpGet]
        public ActionResult SuaGiay(int id)
        {
            Giay giay = db.Giays.Where(row => row.Ma_Giay == id).SingleOrDefault();

            ViewBag.Ma_TheLoai = new SelectList(db.The_Loai.ToList().OrderBy(row => row.Ten_TheLoai), "Ma_TheLoai", "Ten_TheLoai");
            return View(giay);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult SuaGiay( Giay g, FormCollection f, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUpload2)
        {
            var giay = db.Giays.AsEnumerable().SingleOrDefault(n => n.Ma_Giay == int.Parse(f["iMaSach"]));
            ViewBag.Ma_TheLoai = new SelectList(db.The_Loai.ToList().OrderBy(n => n.Ten_TheLoai), "Ma_TheLoai", "Ten_TheLoai", g.Ma_TheLoai);
            if (ModelState.IsValid)
            {
                if (fFileUpload != null && fFileUpload2 != null)
                {

                    
                    //Lấy tên file ( khai báo system.io)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var sFileName2 = Path.GetFileName(fFileUpload2.FileName);

                    //Lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Content/img/Shoes/nike"), sFileName);
                    var path2 = Path.Combine(Server.MapPath("~/Content/img/Shoes/nike/Hinh_2"), sFileName2);

                    //Kiểm tra ảnh đã tồn tại
                    if (!System.IO.File.Exists(path) || !System.IO.File.Exists(path2))
                    {
                        System.IO.File.Delete(path);
                        
                    }
                    else
                    {
                        fFileUpload.SaveAs(path);
                        fFileUpload2.SaveAs(path2);
                        giay.HinhMinhHoa = sFileName;
                        giay.Hinh_1 = sFileName2;
                    }
                    
                }
                giay.Ten_Giay = f["TenGiay"];
                giay.DonGia = decimal.Parse(f["DonGia"]);
                giay.MoTa = f["MoTa"].Replace("<p>"," ").Replace("<p>","\n");
                giay.Gia_Goc = decimal.Parse(f["GiaGoc"]);
                giay.Ma_TheLoai = int.Parse(f["Ma_TheLoai"]);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giay);

        }
        [HttpGet]
        public ActionResult ThemGiay()
        {
            ViewBag.Ma_TheLoai = new SelectList(db.The_Loai.ToList().OrderBy(row => row.Ten_TheLoai), "Ma_TheLoai", "Ten_TheLoai");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemGiay(Giay g, FormCollection f, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUpload2)
        {
            
            ViewBag.TheLoai = new SelectList(db.The_Loai.ToList().OrderBy(row => row.Ten_TheLoai), "Ma_TheLoai", "Ten_TheLoai");
            if(fFileUpload == null && fFileUpload2 == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh giày";
                ViewBag.Ma_TheLoai = new SelectList(db.The_Loai.ToList().OrderBy(n => n.Ten_TheLoai), "Ma_TheLoai", "Ten_TheLoai", int.Parse(f["Ma_TheLoai"]));


            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Lấy tên file ( khai báo system.io)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var sFileName2 = Path.GetFileName(fFileUpload2.FileName);
                   
                    //Lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Content/img/Shoes/nike"), sFileName);
                    var path2 = Path.Combine(Server.MapPath("~/Content/img/Shoes/nike/Hinh_2"), sFileName2);
                    
                    //Kiểm tra ảnh đã tồn tại
                    if (!System.IO.File.Exists(path) || !System.IO.File.Exists(path2))
                    {
                        fFileUpload.SaveAs(path);
                        fFileUpload2.SaveAs(path2);
                    }
                    g.Ten_Giay = f["TenGiay"];
                    g.DonGia =decimal.Parse(f["DonGia"]);
                    g.MoTa = f["MoTa"].Replace("<p>", " ").Replace("<p>", "\n");
                    g.HinhMinhHoa = sFileName;
                    g.Hinh_1 = sFileName2;
                    g.Gia_Goc =decimal.Parse(f["GiaGoc"]);
                    g.Ma_TheLoai = int.Parse(f["Ma_TheLoai"]);
                    db.Giays.Add(g);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                
            }
            return View();
        }
    }
}