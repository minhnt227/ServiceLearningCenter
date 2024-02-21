namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HD_SINHVIEN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MSSV { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }

        public virtual SINH_VIEN SINH_VIEN { get; set; }
    }
}
