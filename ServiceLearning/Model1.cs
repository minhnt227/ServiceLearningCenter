using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ServiceLearning
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<DANH_GIA> DANH_GIA { get; set; }
        public virtual DbSet<DANHGIA_DETAILS> DANHGIA_DETAILS { get; set; }
        public virtual DbSet<DOI_TAC> DOI_TAC { get; set; }
        public virtual DbSet<GIANG_VIEN> GIANG_VIEN { get; set; }
        public virtual DbSet<HANG_MUC> HANG_MUC { get; set; }
        public virtual DbSet<HD_DOITAC> HD_DOITAC { get; set; }
        public virtual DbSet<HD_GIANGVIEN> HD_GIANGVIEN { get; set; }
        public virtual DbSet<HD_SINHVIEN> HD_SINHVIEN { get; set; }
        public virtual DbSet<HD_TAITRO> HD_TAITRO { get; set; }
        public virtual DbSet<HOAT_DONG> HOAT_DONG { get; set; }
        public virtual DbSet<KHOA> KHOAs { get; set; }
        public virtual DbSet<PHU_TRACH> PHU_TRACH { get; set; }
        public virtual DbSet<SINH_VIEN> SINH_VIEN { get; set; }
        public virtual DbSet<TAI_CHINH> TAI_CHINH { get; set; }
        public virtual DbSet<TAI_TRO> TAI_TRO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DANH_GIA>()
                .Property(e => e.MaDanhGia)
                .IsFixedLength();

            modelBuilder.Entity<DANH_GIA>()
                .Property(e => e.MSSV)
                .IsFixedLength();

            modelBuilder.Entity<DANH_GIA>()
                .HasMany(e => e.DANHGIA_DETAILS)
                .WithRequired(e => e.DANH_GIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DANHGIA_DETAILS>()
                .Property(e => e.MaDanhGia)
                .IsFixedLength();

            modelBuilder.Entity<DOI_TAC>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<DOI_TAC>()
                .HasMany(e => e.HD_DOITAC)
                .WithRequired(e => e.DOI_TAC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GIANG_VIEN>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<GIANG_VIEN>()
                .Property(e => e.Khoa)
                .IsFixedLength();

            modelBuilder.Entity<GIANG_VIEN>()
                .HasMany(e => e.HD_GIANGVIEN)
                .WithRequired(e => e.GIANG_VIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GIANG_VIEN>()
                .HasMany(e => e.PHU_TRACH)
                .WithRequired(e => e.GIANG_VIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HANG_MUC>()
                .HasMany(e => e.DANHGIA_DETAILS)
                .WithRequired(e => e.HANG_MUC)
                .HasForeignKey(e => e.HangMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HD_GIANGVIEN>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<HD_SINHVIEN>()
                .Property(e => e.MSSV)
                .IsFixedLength();

            modelBuilder.Entity<HOAT_DONG>()
                .HasMany(e => e.HD_DOITAC)
                .WithRequired(e => e.HOAT_DONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOAT_DONG>()
                .HasMany(e => e.HD_GIANGVIEN)
                .WithRequired(e => e.HOAT_DONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOAT_DONG>()
                .HasMany(e => e.HD_SINHVIEN)
                .WithRequired(e => e.HOAT_DONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOAT_DONG>()
                .HasMany(e => e.HD_TAITRO)
                .WithRequired(e => e.HOAT_DONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHOA>()
                .Property(e => e.MaKhoa)
                .IsFixedLength();

            modelBuilder.Entity<KHOA>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<KHOA>()
                .HasMany(e => e.GIANG_VIEN)
                .WithOptional(e => e.KHOA1)
                .HasForeignKey(e => e.Khoa);

            modelBuilder.Entity<KHOA>()
                .HasMany(e => e.SINH_VIEN)
                .WithOptional(e => e.KHOA1)
                .HasForeignKey(e => e.Khoa);

            modelBuilder.Entity<PHU_TRACH>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<SINH_VIEN>()
                .Property(e => e.MSSV)
                .IsFixedLength();

            modelBuilder.Entity<SINH_VIEN>()
                .Property(e => e.Khoa)
                .IsFixedLength();

            modelBuilder.Entity<SINH_VIEN>()
                .HasMany(e => e.HD_SINHVIEN)
                .WithRequired(e => e.SINH_VIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAI_CHINH>()
                .Property(e => e.UEF)
                .HasPrecision(12, 0);

            modelBuilder.Entity<TAI_CHINH>()
                .Property(e => e.TaiTro)
                .HasPrecision(12, 0);

            modelBuilder.Entity<TAI_TRO>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<TAI_TRO>()
                .HasMany(e => e.HD_TAITRO)
                .WithRequired(e => e.TAI_TRO)
                .WillCascadeOnDelete(false);
        }
    }
}
