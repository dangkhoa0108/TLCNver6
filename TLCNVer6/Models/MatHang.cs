namespace TLCNVer6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatHang")]
    public partial class MatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MatHang()
        {
            ChiTietHDs = new HashSet<ChiTietHD>();
            ChiTietPNs = new HashSet<ChiTietPN>();
            ChiTietPXes = new HashSet<ChiTietPX>();
        }

        [Key]
        [StringLength(10)]
        public string MaMatHang { get; set; }

        [StringLength(10)]
        public string MaLoaiMH { get; set; }

        [StringLength(50)]
        public string TenMatHang { get; set; }

        [StringLength(50)]
        public string DonViTinh { get; set; }

        [StringLength(10)]
        public string MaKho { get; set; }

        public int? SoLuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHD> ChiTietHDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPN> ChiTietPNs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPX> ChiTietPXes { get; set; }

        public virtual Kho Kho { get; set; }

        public virtual LoaiMatHang LoaiMatHang { get; set; }
    }
}
