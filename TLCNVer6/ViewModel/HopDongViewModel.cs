using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TLCNVer6.Models;
namespace TLCNVer6.ViewModel
{
    public class HopDongViewModel
    {
        public string MaHD { get; set; }
        public string TinhChat { get; set; }
        public string MaKho { get; set; }
        public string MaDV { get; set; }
        public DateTime? NgayKi { get; set; }
        public string NguoiLap { get; set; }
        public List<ChiTietHD> ChiTietHD { get; set; }

    }
}