using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLCNVer6.ViewModel
{
    public class ChiTietPhieuXuatViewModel
    {
        public int? ID { get; set; }
        public string MaPX { get; set; }

        public string TenMatHang { get; set; }

        public string TenDonVi { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGia { get; set; }

        public double? Sum { get; set; }
    }
}