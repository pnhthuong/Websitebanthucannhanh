namespace TestDoAn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguyenLieu")]
    public partial class NguyenLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguyenLieu()
        {
            ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
            ChiTietSanPhams = new HashSet<ChiTietSanPham>();
        }

        [Key]
        [StringLength(10)]
        public string manl { get; set; }

        [Required]
        [StringLength(30)]
        public string tenhh { get; set; }

        [StringLength(30)]
        public string dvt { get; set; }

        [StringLength(50)]
        public string nhacungcap { get; set; }

        [Column(TypeName = "date")]
        public DateTime hsd { get; set; }

        [Required]
        [StringLength(10)]
        public string maloai { get; set; }

        public double dongia { get; set; }

        public int? soluong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; }
    }
}
