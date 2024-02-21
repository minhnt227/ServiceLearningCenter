namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAI_CHINH
    {
        [Key]
        public int ID_TaiChinh { get; set; }

        public int? MaHD { get; set; }

        public decimal? UEF { get; set; }

        public decimal? TaiTro { get; set; }

        [Column(TypeName = "ntext")]
        public string Khac { get; set; }

        [Column(TypeName = "ntext")]
        public string TieuDe { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Hide { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }
    }
}
