﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLKS_CNPM_LT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LOAIPHONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAIPHONG()
        {
            this.PHONGs = new HashSet<PHONG>();
            DuongDanAnh = "/Data/roomImg/DefaultRoom.jpg";
        }


        [Required(ErrorMessage = "Không được để trống Mã Loại")]
        public string MaLoai { get; set; }
        [Required(ErrorMessage = "Không được để trống Tên Loại")]
        public string TenLoai { get; set; }
        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Chưa Chọn Ảnh")]
        public string DuongDanAnh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHONG> PHONGs { get; set; }
    }
}
