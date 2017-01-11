namespace TLCNVer6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinPN")]
    public partial class ThongTinPN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThongTinPN()
        {
            ChiTietPNs = new HashSet<ChiTietPN>();
        }

        public int ID { get; set; }

        [StringLength(10)]
        public string MaPN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLap { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GiaTriDen { get; set; }

        [StringLength(10)]
        public string NguoiLap { get; set; }

        [StringLength(10)]
        public string MaDonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPN> ChiTietPNs { get; set; }

        public virtual DonViGiaoNhan DonViGiaoNhan { get; set; }

        public virtual Login Login { get; set; }
    }
}
