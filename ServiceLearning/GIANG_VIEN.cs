namespace ServiceLearning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GIANG_VIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GIANG_VIEN()
        {
            HD_GIANGVIEN = new HashSet<HD_GIANGVIEN>();
            PHU_TRACH = new HashSet<PHU_TRACH>();
        }

        [Key]
        [StringLength(10)]
        public string MaGV { get; set; }

        [StringLength(50)]
        public string HoTenLot { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        [StringLength(10)]
        public string Khoa { get; set; }

        public bool? Hide { get; set; }

        public virtual KHOA KHOA1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_GIANGVIEN> HD_GIANGVIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHU_TRACH> PHU_TRACH { get; set; }
    }
}
