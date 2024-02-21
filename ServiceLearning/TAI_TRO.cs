namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAI_TRO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAI_TRO()
        {
            HD_TAITRO = new HashSet<HD_TAITRO>();
        }

        [Key]
        public int ID_TaiTro { get; set; }

        [StringLength(100)]
        public string TenTaiTro { get; set; }

        [StringLength(50)]
        public string DaiDien { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public bool? Hide { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_TAITRO> HD_TAITRO { get; set; }
    }
}
