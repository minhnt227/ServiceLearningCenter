namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HANG_MUC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HANG_MUC()
        {
            DANHGIA_DETAILS = new HashSet<DANHGIA_DETAILS>();
        }

        public int ID { get; set; }

        [Column(TypeName = "ntext")]
        public string TenHangMuc { get; set; }

        public bool? Hide { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA_DETAILS> DANHGIA_DETAILS { get; set; }
    }
}
