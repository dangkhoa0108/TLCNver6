using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLCNVer6.ViewModel
{
    public class ChiTietHopDongViewModel
    {
        public int? ID { get; set; }
        public string MaHD { get; set; }
        public string TenMatHang { get; set; }
        public int? SoLuong { get; set; }
        public double? DonGia { get; set; }
        public double? Tong { get; set; }

    }
}