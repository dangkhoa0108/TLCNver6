namespace TLCNVer6.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QuanLyKhoDuocPhamDbContext : DbContext
    {
        public QuanLyKhoDuocPhamDbContext()
            : base("name=QuanLyKhoDuocPhamDbContext")
        {
        }

        public virtual DbSet<ChiTietHD> ChiTietHDs { get; set; }
        public virtual DbSet<ChiTietPN> ChiTietPNs { get; set; }
        public virtual DbSet<ChiTietPX> ChiTietPXes { get; set; }
        public virtual DbSet<DonViGiaoNhan> DonViGiaoNhans { get; set; }
        public virtual DbSet<Kho> Khoes { get; set; }
        public virtual DbSet<LoaiKho> LoaiKhoes { get; set; }
        public virtual DbSet<LoaiMatHang> LoaiMatHangs { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<MatHang> MatHangs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThongTinHopDong> ThongTinHopDongs { get; set; }
        public virtual DbSet<ThongTinPN> ThongTinPNs { get; set; }
        public virtual DbSet<ThongTinPX> ThongTinPXes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonViGiaoNhan>()
                .HasMany(e => e.ThongTinHopDongs)
                .WithOptional(e => e.DonViGiaoNhan)
                .HasForeignKey(e => e.MaDV);

            modelBuilder.Entity<DonViGiaoNhan>()
                .HasMany(e => e.ThongTinHopDongs1)
                .WithOptional(e => e.DonViGiaoNhan1)
                .HasForeignKey(e => e.MaDV);

            modelBuilder.Entity<DonViGiaoNhan>()
                .HasMany(e => e.ThongTinPNs)
                .WithOptional(e => e.DonViGiaoNhan)
                .HasForeignKey(e => e.MaDonVi);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.ThongTinHopDongs)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.NguoiLap);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.ThongTinPNs)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.NguoiLap);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.ThongTinPXes)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.NguoiLap);

            modelBuilder.Entity<ThongTinHopDong>()
                .HasMany(e => e.ChiTietHDs)
                .WithRequired(e => e.ThongTinHopDong)
                .HasForeignKey(e => e.IDHD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinPN>()
                .HasMany(e => e.ChiTietPNs)
                .WithOptional(e => e.ThongTinPN)
                .HasForeignKey(e => e.IDPN);

            modelBuilder.Entity<ThongTinPX>()
                .HasMany(e => e.ChiTietPXes)
                .WithOptional(e => e.ThongTinPX)
                .HasForeignKey(e => e.IDPX);
        }
    }
}
