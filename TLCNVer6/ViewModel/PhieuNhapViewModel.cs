using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TLCNVer6.Models;
namespace TLCNVer6.ViewModel
{
    public class PhieuNhapViewModel
    {
        public string MaPN { get; set; }
        public DateTime? NgayLap { get; set; }
        public DateTime? GiaTriDen { get; set; }
        public string NguoiLap { get; set; }
        public string MaDV { get; set; }
        public List<ChiTietPN> ChiTietPN { get; set; }
    }
}