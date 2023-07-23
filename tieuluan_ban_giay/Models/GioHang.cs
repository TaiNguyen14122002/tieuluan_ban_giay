using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tieuluan_ban_giay.Models
{
    public class GioHang
    {
        
        QLBangiayyEntities db = new QLBangiayyEntities();
        public int Ma_Giay { get; set; }
        public string Ten_Giay { get; set; }
        public string HinhMinhHoa { get; set; }
        public double DonGia { get; set; }
        public int So_Luong { get; set; }
        public double Thanh_Tien {
            get
            {
                return DonGia * So_Luong;
            }
        }
        public GioHang(int MG)
        {
            Ma_Giay = MG;
            Giay G = db.Giays.Single(row => row.Ma_Giay == Ma_Giay);
            Ten_Giay = G.Ten_Giay;
            HinhMinhHoa = G.HinhMinhHoa;
            DonGia = double.Parse(G.DonGia.ToString());
            So_Luong = 1;
        }

    }
}