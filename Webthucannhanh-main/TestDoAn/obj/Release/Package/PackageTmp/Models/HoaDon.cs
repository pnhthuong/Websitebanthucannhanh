namespace TestDoAn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        [Display(Name ="Mã Hóa Đơn")]
        public int mahd { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày Lập Hóa Đơn")]
        public DateTime? ngaylaphd { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhân viên")]
        public string manv { get; set; }

        [StringLength(30)]
        [Display(Name = "Tên Khách Hàng")]
        public string khachhang { get; set; }

        [StringLength(11)]
        [Display(Name = "Số điện thoại ")]
        public string sdt { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        public string diachi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
