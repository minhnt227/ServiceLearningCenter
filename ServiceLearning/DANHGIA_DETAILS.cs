namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DANHGIA_DETAILS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaDanhGia { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HangMuc { get; set; }

        public int? MucDo { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public virtual DANH_GIA DANH_GIA { get; set; }

        public virtual HANG_MUC HANG_MUC { get; set; }
    }
}
