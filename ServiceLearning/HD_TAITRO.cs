namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HD_TAITRO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_TaiTro { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }

        public virtual TAI_TRO TAI_TRO { get; set; }
    }
}
