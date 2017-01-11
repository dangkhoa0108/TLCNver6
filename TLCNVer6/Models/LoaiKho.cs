namespace TLCNVer6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiKho")]
    public partial class LoaiKho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiKho()
        {
            Khoes = new HashSet<Kho>();
        }

        [Key]
        [StringLength(10)]
        public string MaLoaiKho { get; set; }

        [StringLength(50)]
        public string TenLoaiKho { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kho> Khoes { get; set; }
    }
}
