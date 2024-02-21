namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DOI_TAC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOI_TAC()
        {
            HD_DOITAC = new HashSet<HD_DOITAC>();
        }

        [Key]
        public int ID_DoiTac { get; set; }

        [StringLength(100)]
        public string TenDoiTac { get; set; }

        [StringLength(50)]
        public string DaiDien { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public bool? Hide { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_DOITAC> HD_DOITAC { get; set; }
    }
}
