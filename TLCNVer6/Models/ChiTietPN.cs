namespace TLCNVer6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPN")]
    public partial class ChiTietPN
    {
        public int ID { get; set; }

        public int? IDPN { get; set; }

        [StringLength(10)]
        public string MaMatHang { get; set; }

        [StringLength(10)]
        public string MaKho { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGia { get; set; }

        public double? Sum { get; set; }

        public virtual Kho Kho { get; set; }

        public virtual MatHang MatHang { get; set; }

        public virtual ThongTinPN ThongTinPN { get; set; }
    }
}
