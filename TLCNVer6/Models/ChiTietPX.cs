namespace TLCNVer6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPX")]
    public partial class ChiTietPX
    {
        public int ID { get; set; }

        public int? IDPX { get; set; }

        [StringLength(10)]
        public string MaMatHang { get; set; }

        [StringLength(10)]
        public string MaDV { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGia { get; set; }

        public double? Sum { get; set; }

        public virtual DonViGiaoNhan DonViGiaoNhan { get; set; }

        public virtual MatHang MatHang { get; set; }

        public virtual ThongTinPX ThongTinPX { get; set; }
    }
}
