using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tieuluan_ban_giay.Models;
using PagedList;
using PagedList.Mvc;
using System.Net.Mail;
using System.Web.Helpers;
using System.Net;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace tieuluan_ban_giay.Controllers
{
    public class HomeController : Controller
    {
        QLBangiayyEntities db = new QLBangiayyEntities();
        public ActionResult Index(string search="")
        {
            
            List<Giay> giay = db.Giays.Where(row=>row.Ten_Giay.Contains(search)).Take(8).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                giay = giay.Where(row=>row.Ten_Giay.Contains(search)).ToList();
            }
            ViewBag.search = search;
            return View(giay);
        }
        [HttpPost]
        public ActionResult TimKiem( String search = "")
        {
            
            List<Giay> products = db.Giays.Where(p => p.Ten_Giay.Contains(search)).ToList();
            ViewBag.search = search;
            return View(products);
        }
        public ActionResult Thongtincuahang()
        {
            return View();
        }
        public ActionResult Lienhe()
        {
            return View();
        }
        public ActionResult Subkit_Lienhe()
        {
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
            var response = Request["g-recaptcha-response"];
            string secretkey = "6LegpB8jAAAAAA-etpgdLFdLiK_8Svr1VjEOJxqw";
            var client =  new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretkey,response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            var sTenDN = f["TenDN"];
            var sMatKhau = f["MatKhau"];
            if (sTenDN.Equals(""))
            {
                ViewBag.ThongBao = "Tên đăng nhập không được để trống";
            }
            else if (sMatKhau.Equals(""))
            {
                ViewBag.ThongBao = "Mật khẩu không được để trống";
            }
            else
            {
                Khach_Hang kh = db.Khach_Hang.SingleOrDefault(n => n.TenDN_Khach_Hang == sTenDN && n.MatKhau_Khach_Hang == sMatKhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Sing_up()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sing_up(Khach_Hang kh, FormCollection f)
        {
            
            var MatKhau = f["MatKhau"];
            var TenDN = f["TenDN"];
            var Ten = f["Ten"];
            var Email = f["Email"];
            var DienThoai = f["DienThoai"];
            if (DienThoai.Equals("")) {
                ViewBag.Error = "Số điện thoại không được để trống";
            }
            else if(DienThoai.Length > 10 || DienThoai.Length <10)
            {
                ViewBag.Error = "Số điện thoại phải là 10 số";
            }
            else if (Email.Equals(""))
            {
                ViewBag.Error = "Email không được để trống";
            }
            else if (Ten.Equals(""))
            {
                ViewBag.Error = "Tên người dùng không được để trống";
            }
            else if (TenDN.Equals(""))
            {
                ViewBag.Error = "Tên đăng nhập không được để trống";
            }
            else if (MatKhau.Equals(""))
            {
                ViewBag.Error = "Mật khẩu không được để trống";
            }
            else
            {
                kh.DienThoai_khach_Hang = DienThoai;
                kh.Email = Email;
                kh.Ten_Khach_Hang = Ten;
                kh.TenDN_Khach_Hang = TenDN;
                kh.MatKhau_Khach_Hang = MatKhau;
                db.Khach_Hang.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Corret_DangKy");
            }
            return View();
        }
        public ActionResult Corret_Dangky()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult DanhMuc()
        {
            return PartialView(db.The_Loai.ToList());
        }
        public ActionResult SachTheoTheLoai(int ? page, int id)
        {
            if (page == null) page = 1;
            int iSize = 8;
            int iPageNum = (page ?? 1);
            
            var TheLoai = from x in db.Giays where x.Ma_TheLoai == id orderby x.Ma_TheLoai ascending select x;
            
            return View(TheLoai.ToPagedList(iPageNum, iSize));
        }
        public ActionResult ChiTietSanPham(int id)
        {
            Giay CT_Giay = db.Giays.Find(id);
            return View(CT_Giay);
        }
        [ChildActionOnly]
        public ActionResult CoTheBanCungThich()
        {
            List<Giay> giay = db.Giays.Take(4).ToList();
            return View(giay);
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [NonAction]
        public void SendVerificationLinkEmail(string EmailID, string activationCode, string EmailFor = "VerifyAccount" )
        {
           
            var verifyUrl = "/Home/" + EmailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("Tainguyenlq.0123@gmail.com", "Lấy lại mật khẩu");
            var toEmail = new MailAddress(EmailID);
            var fromEmailPassword = "********";
            string subject = "";
            string body = "";
            if(EmailFor == "VerifyAccount")
            {
                subject = "Tài khoản của bạn đã tạo thành công";
                body = " "+ "<a href ='"+link+"'>"+ link +"</a>";
            }
            else if (EmailFor == "ResetPassword")
            {
                subject = "Quên mật khẩu";
                body = "Nhấn vào đây để đổi mật khẩu" + "<a href ='" + link + "'> ResetPassword</a>";
            }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, "cizaekvjoqmyjoqi")
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        [HttpPost]
        public ActionResult ForgotPassword(Khach_Hang khh)
        {
            string message = "";
            bool status = false;
            Khach_Hang kh = db.Khach_Hang.Where(row => row.Email == khh.Email).SingleOrDefault();
            if (kh != null)
            {
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(kh.Email, resetCode, "ResetPassword");
                kh.ResetPasswordCode = resetCode;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                ViewBag.ThongBao = "Vui lòng kiểm tra Email để đặt lại mật khẩu";
            }
            else
            {
                message = "Email không tồn tại";
            }
            return View();
        }
        public ActionResult ResetPassword( string id, Khach_Hang khh)
        {
            khh.ResetPasswordCode = id;
            Khach_Hang kh = db.Khach_Hang.Where(row => row.ResetPasswordCode == khh.ResetPasswordCode).FirstOrDefault();
            if (kh != null)
            {
                ResetPassword model = new ResetPassword();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult ResetPassword( ResetPassword model)
        {
            var message = "";
            Khach_Hang kh = db.Khach_Hang.Where(row => row.ResetPasswordCode == model.ResetCode).FirstOrDefault();
            if (kh != null)
            {
                kh.MatKhau_Khach_Hang = Crypto.Hash(model.NewPassword);
                kh.ResetPasswordCode = "";
                db.Configuration.ValidateOnSaveEnabled =false;
                db.SaveChanges();
                message = " Cập nhập thành công";
            }
            else
            {
                message = "Cập nhập mật khẩu thất bại ";
            }
            ViewBag.ThongBao = message;
            return View(model);
        }
        
        
        
    }
}