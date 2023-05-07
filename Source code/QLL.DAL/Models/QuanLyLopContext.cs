using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class QuanLyLopContext : DbContext
    {
        public QuanLyLopContext()
        {
        }

        public QuanLyLopContext(DbContextOptions<QuanLyLopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminDb> AdminDbs { get; set; }
        public virtual DbSet<DiemDb> DiemDbs { get; set; }
        public virtual DbSet<GiaoVienDb> GiaoVienDbs { get; set; }
        public virtual DbSet<Hoc> Hocs { get; set; }
        public virtual DbSet<HocSinhDb> HocSinhDbs { get; set; }
        public virtual DbSet<KhoaHocDb> KhoaHocDbs { get; set; }
        public virtual DbSet<LopDb> LopDbs { get; set; }
        public virtual DbSet<MonHocDb> MonHocDbs { get; set; }
        public virtual DbSet<TaiKhoanAdDb> TaiKhoanAdDbs { get; set; }
        public virtual DbSet<TaiKhoanGvdb> TaiKhoanGvdbs { get; set; }
        public virtual DbSet<TaiKhoanHsdb> TaiKhoanHsdbs { get; set; }
        public virtual DbSet<Tkbctdb> Tkbctdbs { get; set; }
        public virtual DbSet<Tkbdb> Tkbdbs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-73SG70D\\SQLEXPRESS;Initial Catalog=QuanLyLop;persist security info=True;User id=sa; password=25251025aA;Pooling = False;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdminDb>(entity =>
            {
                entity.HasKey(e => e.MaAdmin);

                entity.ToTable("AdminDb");

                entity.Property(e => e.MaAdmin).HasMaxLength(5);

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.GioiTinh)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Sdt).HasColumnName("SDT");

                entity.Property(e => e.TenAdmin)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DiemDb>(entity =>
            {
                entity.HasKey(e => new { e.MaMh, e.MaHs, e.MaKh });

                entity.ToTable("DiemDb");

                entity.Property(e => e.MaMh).HasColumnName("MaMH");

                entity.Property(e => e.MaHs)
                    .HasMaxLength(5)
                    .HasColumnName("MaHS");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.HasOne(d => d.MaHsNavigation)
                    .WithMany(p => p.DiemDbs)
                    .HasForeignKey(d => d.MaHs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiemDb_HocSinhDb");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DiemDbs)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiemDb_KhoaHocDb");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.DiemDbs)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiemDb_MonHocDb");
            });

            modelBuilder.Entity<GiaoVienDb>(entity =>
            {
                entity.HasKey(e => e.MaGv);

                entity.ToTable("GiaoVienDb");

                entity.Property(e => e.MaGv)
                    .HasMaxLength(5)
                    .HasColumnName("MaGV");

                entity.Property(e => e.ChuyenNganh)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GioiTinh)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Sdt).HasColumnName("SDT");

                entity.Property(e => e.TenGv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenGV");

                entity.Property(e => e.TrinhDoChuyenMon)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Hoc>(entity =>
            {
                entity.HasKey(e => new { e.MaKh, e.MaLop });

                entity.ToTable("Hoc");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Hocs)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hoc_KhoaHocDb");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.Hocs)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hoc_LopDb");
            });

            modelBuilder.Entity<HocSinhDb>(entity =>
            {
                entity.HasKey(e => e.MaHs);

                entity.ToTable("HocSinhDb");

                entity.Property(e => e.MaHs)
                    .HasMaxLength(5)
                    .HasColumnName("MaHS");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.GioiTinh)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Sdt).HasColumnName("SDT");

                entity.Property(e => e.TenHs)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenHS");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.HocSinhDbs)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HocSinhDb_LopDb");
            });

            modelBuilder.Entity<KhoaHocDb>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("KhoaHocDb");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.NgayBatDau).HasColumnType("datetime");

                entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

                entity.Property(e => e.TenKh)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<LopDb>(entity =>
            {
                entity.HasKey(e => e.MaLop);

                entity.ToTable("LopDb");

                entity.Property(e => e.MoTa).HasMaxLength(255);

                entity.Property(e => e.PhongHoc)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TenLop)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrangThai)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MonHocDb>(entity =>
            {
                entity.HasKey(e => e.MaMh);

                entity.ToTable("MonHocDb");

                entity.Property(e => e.MaMh).HasColumnName("MaMH");

                entity.Property(e => e.TenMh)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenMH");
            });

            modelBuilder.Entity<TaiKhoanAdDb>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK_TaiKhoanAd");

                entity.ToTable("TaiKhoanAdDb");

                entity.HasIndex(e => e.MaAdmin, "Unique_MaAd")
                    .IsUnique();

                entity.HasIndex(e => e.TenDangNhap, "unique_Ten")
                    .IsUnique();

                entity.HasIndex(e => e.TenDangNhap, "unique_tdn")
                    .IsUnique();

                entity.Property(e => e.MaTk).HasColumnName("MaTK");

                entity.Property(e => e.MaAdmin)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TenDangNhap)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.MaAdminNavigation)
                    .WithOne(p => p.TaiKhoanAdDb)
                    .HasForeignKey<TaiKhoanAdDb>(d => d.MaAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaiKhoanAdDb_AdminDb");
            });

            modelBuilder.Entity<TaiKhoanGvdb>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK_TaiKhoanGV");

                entity.ToTable("TaiKhoanGVDb");

                entity.HasIndex(e => e.MaGv, "Unique_MaGV")
                    .IsUnique();

                entity.HasIndex(e => e.TenDangNhap, "unique_tdngv")
                    .IsUnique();

                entity.Property(e => e.MaTk).HasColumnName("MaTK");

                entity.Property(e => e.MaGv)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("MaGV");

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TenDangNhap)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.MaGvNavigation)
                    .WithOne(p => p.TaiKhoanGvdb)
                    .HasForeignKey<TaiKhoanGvdb>(d => d.MaGv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaiKhoanGVDb_GiaoVienDb");
            });

            modelBuilder.Entity<TaiKhoanHsdb>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK_TaiKhoanDb_1");

                entity.ToTable("TaiKhoanHSDb");

                entity.HasIndex(e => e.MaHs, "Unique_HS")
                    .IsUnique();

                entity.HasIndex(e => e.TenDangNhap, "unique_tdnhs")
                    .IsUnique();

                entity.Property(e => e.MaTk).HasColumnName("MaTK");

                entity.Property(e => e.MaHs)
                    .HasMaxLength(5)
                    .HasColumnName("MaHS");

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TenDangNhap)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.MaHsNavigation)
                    .WithOne(p => p.TaiKhoanHsdb)
                    .HasForeignKey<TaiKhoanHsdb>(d => d.MaHs)
                    .HasConstraintName("FK_TaiKhoanDb_HocSinhDb");
            });

            modelBuilder.Entity<Tkbctdb>(entity =>
            {
                entity.HasKey(e => new { e.MaTkb, e.Malop, e.Thu, e.Tiet });

                entity.ToTable("TKBCTDb");

                entity.HasIndex(e => new { e.Thu, e.Tiet, e.MaGv, e.MaTkb }, "IX_TKBCTDb")
                    .IsUnique();

                entity.Property(e => e.MaTkb).HasColumnName("MaTKB");

                entity.Property(e => e.MaGv)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("MaGV");

                entity.Property(e => e.MaMh).HasColumnName("MaMH");

                entity.HasOne(d => d.MaGvNavigation)
                    .WithMany(p => p.Tkbctdbs)
                    .HasForeignKey(d => d.MaGv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TKBCTDb_GiaoVienDb");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.Tkbctdbs)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TKBCTDb_MonHocDb");

                entity.HasOne(d => d.MaTkbNavigation)
                    .WithMany(p => p.Tkbctdbs)
                    .HasForeignKey(d => d.MaTkb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TKBCTDb_TKBDb");

                entity.HasOne(d => d.MalopNavigation)
                    .WithMany(p => p.Tkbctdbs)
                    .HasForeignKey(d => d.Malop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TKBCTDb_LopDb");
            });

            modelBuilder.Entity<Tkbdb>(entity =>
            {
                entity.HasKey(e => e.MaTkb);

                entity.ToTable("TKBDb");

                entity.Property(e => e.MaTkb).HasColumnName("MaTKB");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.TenTkb)
                    .HasMaxLength(50)
                    .HasColumnName("TenTKB");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Tkbdbs)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TKBDb_KhoaHocDb");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
