namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SINH_VIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SINH_VIEN()
        {
            DANH_GIA = new HashSet<DANH_GIA>();
            HD_SINHVIEN = new HashSet<HD_SINHVIEN>();
        }

        [Key]
        [StringLength(10)]
        public string MSSV { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string Khoa { get; set; }

        public bool? Hide { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANH_GIA> DANH_GIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_SINHVIEN> HD_SINHVIEN { get; set; }

        public virtual KHOA KHOA1 { get; set; }
    }
}
