namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HOAT_DONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOAT_DONG()
        {
            DANH_GIA = new HashSet<DANH_GIA>();
            HD_DOITAC = new HashSet<HD_DOITAC>();
            HD_GIANGVIEN = new HashSet<HD_GIANGVIEN>();
            HD_SINHVIEN = new HashSet<HD_SINHVIEN>();
            HD_TAITRO = new HashSet<HD_TAITRO>();
            TAI_CHINH = new HashSet<TAI_CHINH>();
        }

        [Key]
        public int MaHD { get; set; }

        [Column(TypeName = "ntext")]
        public string TenHoatDong { get; set; }

        [StringLength(50)]
        public string Loai { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public bool? Hide { get; set; }

        public DateTime? CreatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANH_GIA> DANH_GIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_DOITAC> HD_DOITAC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_GIANGVIEN> HD_GIANGVIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_SINHVIEN> HD_SINHVIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_TAITRO> HD_TAITRO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAI_CHINH> TAI_CHINH { get; set; }
    }
}
