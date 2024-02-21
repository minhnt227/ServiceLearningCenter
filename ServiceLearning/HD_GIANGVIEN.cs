namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HD_GIANGVIEN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaGV { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }

        public virtual GIANG_VIEN GIANG_VIEN { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }
    }
}
