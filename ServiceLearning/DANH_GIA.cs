namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DANH_GIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DANH_GIA()
        {
            DANHGIA_DETAILS = new HashSet<DANHGIA_DETAILS>();
        }

        [Key]
        [StringLength(10)]
        public string MaDanhGia { get; set; }

        public int? MaHD { get; set; }

        [StringLength(10)]
        public string MSSV { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public bool? Hide { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }

        public virtual SINH_VIEN SINH_VIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA_DETAILS> DANHGIA_DETAILS { get; set; }
    }
}
