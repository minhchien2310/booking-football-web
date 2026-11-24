using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatSanBanh.Models
{
    public partial class QuanLyDatSanBanhContext : DbContext
    {
        public QuanLyDatSanBanhContext()
        {
        }

        public QuanLyDatSanBanhContext(DbContextOptions<QuanLyDatSanBanhContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BangGia> BangGia { get; set; } = null!;
        public virtual DbSet<ChiTietDichVu> ChiTietDichVus { get; set; } = null!;
        public virtual DbSet<DanhGia> DanhGia { get; set; } = null!;
        public virtual DbSet<DatSan> DatSans { get; set; } = null!;
        public virtual DbSet<DatSanCoDinh> DatSanCoDinhs { get; set; } = null!;
        public virtual DbSet<DichVu> DichVus { get; set; } = null!;
        public virtual DbSet<KhoiPhucMatKhau> KhoiPhucMatKhaus { get; set; } = null!;
        public virtual DbSet<MaGiamGia> MaGiamGia { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<SanBong> SanBongs { get; set; } = null!;
        public virtual DbSet<ThanhToan> ThanhToans { get; set; } = null!;
        public virtual DbSet<ThongBao> ThongBaos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangGia>(entity =>
            {
                entity.HasKey(e => e.MaBangGia)
                    .HasName("PK__BangGia__5DAC5A685D488A2A");

                entity.Property(e => e.GiaMoiGio).HasColumnType("decimal(12, 0)");
            });

            modelBuilder.Entity<ChiTietDichVu>(entity =>
            {
                entity.HasKey(e => new { e.MaDatSan, e.MaDichVu })
                    .HasName("PK__ChiTietD__9873AF3B885264DF");

                entity.ToTable("ChiTietDichVu");

                entity.Property(e => e.DonGiaLucDat).HasColumnType("decimal(12, 0)");

                entity.HasOne(d => d.MaDatSanNavigation)
                    .WithMany(p => p.ChiTietDichVus)
                    .HasForeignKey(d => d.MaDatSan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDi__MaDat__72C60C4A");

                entity.HasOne(d => d.MaDichVuNavigation)
                    .WithMany(p => p.ChiTietDichVus)
                    .HasForeignKey(d => d.MaDichVu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDi__MaDic__73BA3083");
            });

            modelBuilder.Entity<DanhGia>(entity =>
            {
                entity.HasKey(e => e.MaDanhGia)
                    .HasName("PK__DanhGia__AA9515BFD09B7740");

                entity.HasIndex(e => e.MaDatSan, "UQ__DanhGia__747DC2D2282281B6")
                    .IsUnique();

                entity.Property(e => e.BinhLuan).HasMaxLength(500);

                entity.Property(e => e.NgayDanhGia).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.MaDatSanNavigation)
                    .WithOne(p => p.DanhGia)
                    .HasForeignKey<DanhGia>(d => d.MaDatSan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DanhGia__MaDatSa__01142BA1");

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DanhGia__MaNguoi__02084FDA");

                entity.HasOne(d => d.MaSanNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaSan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DanhGia__MaSan__02FC7413");
            });

            modelBuilder.Entity<DatSan>(entity =>
            {
                entity.HasKey(e => e.MaDatSan)
                    .HasName("PK__DatSan__747DC2D37BF5E0D3");

                entity.ToTable("DatSan");

                entity.HasIndex(e => new { e.MaSan, e.NgayDat, e.TrangThai }, "IX_DatSan_San_Ngay");

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.Property(e => e.GiaSan).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.NgayDat).HasColumnType("date");

                entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TienCoc).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.TienGiam).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.TongTien).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ChoThanhToan')");

                entity.HasOne(d => d.MaDatSanCoDinhNavigation)
                    .WithMany(p => p.DatSans)
                    .HasForeignKey(d => d.MaDatSanCoDinh)
                    .HasConstraintName("FK__DatSan__MaDatSan__6477ECF3");

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.DatSans)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DatSan__MaNguoiD__628FA481");

                entity.HasOne(d => d.MaSanNavigation)
                    .WithMany(p => p.DatSans)
                    .HasForeignKey(d => d.MaSan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DatSan__MaSan__6383C8BA");

                entity.HasOne(d => d.MaVoucherNavigation)
                    .WithMany(p => p.DatSans)
                    .HasForeignKey(d => d.MaVoucher)
                    .HasConstraintName("FK__DatSan__MaVouche__656C112C");
            });

            modelBuilder.Entity<DatSanCoDinh>(entity =>
            {
                entity.HasKey(e => e.MaDatSanCoDinh)
                    .HasName("PK__DatSanCo__056C329B75B9193B");

                entity.ToTable("DatSanCoDinh");

                entity.Property(e => e.CacThuTrongTuan)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TienCoc).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.TongTien).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('DangHoatDong')");

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.DatSanCoDinhs)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DatSanCoD__MaNgu__59FA5E80");

                entity.HasOne(d => d.MaSanNavigation)
                    .WithMany(p => p.DatSanCoDinhs)
                    .HasForeignKey(d => d.MaSan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DatSanCoD__MaSan__5AEE82B9");
            });

            modelBuilder.Entity<DichVu>(entity =>
            {
                entity.HasKey(e => e.MaDichVu)
                    .HasName("PK__DichVu__C0E6DE8FCEB8D104");

                entity.ToTable("DichVu");

                entity.Property(e => e.DangKinhDoanh)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DonGia).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.DonViTinh).HasMaxLength(30);

                entity.Property(e => e.TenDichVu).HasMaxLength(100);
            });

            modelBuilder.Entity<KhoiPhucMatKhau>(entity =>
            {
                entity.HasKey(e => e.MaKhoiPhuc)
                    .HasName("PK__KhoiPhuc__6BA08216493B2CAF");

                entity.ToTable("KhoiPhucMatKhau");

                entity.Property(e => e.MaOtp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaOTP");

                entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.KhoiPhucMatKhaus)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KhoiPhucM__MaNgu__3F466844");
            });

            modelBuilder.Entity<MaGiamGia>(entity =>
            {
                entity.HasKey(e => e.MaVoucher)
                    .HasName("PK__MaGiamGi__0AAC5B1182251ADA");

                entity.HasIndex(e => e.MaCode, "UQ__MaGiamGi__152C7C5C3DE32891")
                    .IsUnique();

                entity.Property(e => e.GiamToiDa).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.GioiHanLuotDung).HasDefaultValueSql("((100))");

                entity.Property(e => e.MaCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDung)
                    .HasName("PK__NguoiDun__C539D762C5DA108F");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.SoDienThoai, "UQ__NguoiDun__0389B7BD04A21068")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D105340BCCB462")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen).HasMaxLength(100);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThaiHoatDong)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.VaiTro)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('KhachHang')");
            });

            modelBuilder.Entity<SanBong>(entity =>
            {
                entity.HasKey(e => e.MaSan)
                    .HasName("PK__SanBong__3188C826A8F5FFD9");

                entity.ToTable("SanBong");

                entity.Property(e => e.HinhAnh)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MoTa).HasMaxLength(500);

                entity.Property(e => e.TenSan).HasMaxLength(100);

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('HoatDong')");

                entity.HasMany(d => d.MaSanChas)
                    .WithMany(p => p.MaSanCons)
                    .UsingEntity<Dictionary<string, object>>(
                        "CauHinhGopSan",
                        l => l.HasOne<SanBong>().WithMany().HasForeignKey("MaSanCha").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CauHinhGo__MaSan__49C3F6B7"),
                        r => r.HasOne<SanBong>().WithMany().HasForeignKey("MaSanCon").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CauHinhGo__MaSan__4AB81AF0"),
                        j =>
                        {
                            j.HasKey("MaSanCha", "MaSanCon").HasName("PK__CauHinhG__A0B9F025A88458AB");

                            j.ToTable("CauHinhGopSan");
                        });

                entity.HasMany(d => d.MaSanCons)
                    .WithMany(p => p.MaSanChas)
                    .UsingEntity<Dictionary<string, object>>(
                        "CauHinhGopSan",
                        l => l.HasOne<SanBong>().WithMany().HasForeignKey("MaSanCon").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CauHinhGo__MaSan__4AB81AF0"),
                        r => r.HasOne<SanBong>().WithMany().HasForeignKey("MaSanCha").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CauHinhGo__MaSan__49C3F6B7"),
                        j =>
                        {
                            j.HasKey("MaSanCha", "MaSanCon").HasName("PK__CauHinhG__A0B9F025A88458AB");

                            j.ToTable("CauHinhGopSan");
                        });
            });

            modelBuilder.Entity<ThanhToan>(entity =>
            {
                entity.HasKey(e => e.MaThanhToan)
                    .HasName("PK__ThanhToa__D4B258441274F87C");

                entity.ToTable("ThanhToan");

                entity.Property(e => e.LoaiThanhToan)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MaGiaoDich)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhuongThuc)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SoTien).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.ThoiGianThanhToan).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ThanhCong')");

                entity.HasOne(d => d.MaDatSanNavigation)
                    .WithMany(p => p.ThanhToans)
                    .HasForeignKey(d => d.MaDatSan)
                    .HasConstraintName("FK__ThanhToan__MaDat__778AC167");

                entity.HasOne(d => d.MaDatSanCoDinhNavigation)
                    .WithMany(p => p.ThanhToans)
                    .HasForeignKey(d => d.MaDatSanCoDinh)
                    .HasConstraintName("FK__ThanhToan__MaDat__787EE5A0");
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => e.MaThongBao)
                    .HasName("PK__ThongBao__04DEB54EA3419FE4");

                entity.ToTable("ThongBao");

                entity.Property(e => e.DenNgay).HasColumnType("date");

                entity.Property(e => e.HienThi)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoiDung).HasMaxLength(1000);

                entity.Property(e => e.TieuDe).HasMaxLength(150);

                entity.Property(e => e.TuNgay).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
