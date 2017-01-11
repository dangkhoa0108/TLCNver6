namespace TLCNVer6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinHopDong")]
    public partial class ThongTinHopDong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThongTinHopDong()
        {
            ChiTietHDs = new HashSet<ChiTietHD>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHD { get; set; }

        [StringLength(50)]
        public string TinhChat { get; set; }

        [StringLength(10)]
        public string MaKho { get; set; }

        [StringLength(10)]
        public string MaDV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKi { get; set; }

        [StringLength(10)]
        public string NguoiLap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHD> ChiTietHDs { get; set; }

        public virtual DonViGiaoNhan DonViGiaoNhan { get; set; }

        public virtual DonViGiaoNhan DonViGiaoNhan1 { get; set; }

        public virtual Kho Kho { get; set; }

        public virtual Login Login { get; set; }
    }
}
