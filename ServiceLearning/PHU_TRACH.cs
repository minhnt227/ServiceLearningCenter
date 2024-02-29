namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PHU_TRACH
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string MaGV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string NamHoc { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public virtual GIANG_VIEN GIANG_VIEN { get; set; }
    }
}
