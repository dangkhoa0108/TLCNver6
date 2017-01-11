using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TLCNVer6.ViewModel
{
    public class ChiTietPhieuNhapViewModel
    {
        public int? ID { get; set; }
        public string MaPN { get; set; }

        public string TenMatHang { get; set; }

        public string TenKho { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGia { get; set; }

        public double? Sum { get; set; }
    }
}