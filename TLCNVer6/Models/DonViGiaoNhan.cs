namespace TLCNVer6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonViGiaoNhan")]
    public partial class DonViGiaoNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonViGiaoNhan()
        {
            ChiTietPXes = new HashSet<ChiTietPX>();
            ThongTinHopDongs = new HashSet<ThongTinHopDong>();
            ThongTinHopDongs1 = new HashSet<ThongTinHopDong>();
            ThongTinPNs = new HashSet<ThongTinPN>();
        }

        [Key]
        [StringLength(10)]
        public string MaDV { get; set; }

        [StringLength(50)]
        public string TenDV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPX> ChiTietPXes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinHopDong> ThongTinHopDongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinHopDong> ThongTinHopDongs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinPN> ThongTinPNs { get; set; }
    }
}
