namespace TLCNVer6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinPX")]
    public partial class ThongTinPX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThongTinPX()
        {
            ChiTietPXes = new HashSet<ChiTietPX>();
        }

        public int ID { get; set; }

        [StringLength(10)]
        public string MaPX { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLap { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GiaTriDen { get; set; }

        [StringLength(10)]
        public string NguoiLap { get; set; }

        [StringLength(10)]
        public string MaKho { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPX> ChiTietPXes { get; set; }

        public virtual Kho Kho { get; set; }

        public virtual Login Login { get; set; }
    }
}
