using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatSanBanh.Models
{
    public partial class DatSanBanhDbContext : DbContext
    {
        public DatSanBanhDbContext()
        {
        }

        public DatSanBanhDbContext(DbContextOptions<DatSanBanhDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SanBong> SanBongs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=localhost;Database=DatSanBanhDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanBong>(entity =>
            {
                entity.ToTable("SanBong");

                entity.Property(e => e.GiaThue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LoaiSan).HasMaxLength(50);

                entity.Property(e => e.TenSan).HasMaxLength(100);

                entity.Property(e => e.TrangThai)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
