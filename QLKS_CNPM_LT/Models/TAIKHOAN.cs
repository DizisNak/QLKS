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

    public partial class TAIKHOAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAIKHOAN()
        {
            this.HOADONs = new HashSet<HOADON>();
            this.HOANTIENs = new HashSet<HOANTIEN>();
            this.PHONGs = new HashSet<PHONG>();
            this.YEUTHICHes = new HashSet<YEUTHICH>();
            ANH = "~/Content/profileImg/profileImg.png";
        }
    
        public string ID_TK { get; set; }

        [Required(ErrorMessage = "Không được để trống Tên Tài Khoản")]
        public string TenTK { get; set; }

        [Required(ErrorMessage = "Không được để trống Mail")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Gmail { get; set; }

        [Required(ErrorMessage = "Không được để trống Tên Ảnh")]
        public string ANH { get; set; }

        [Required(ErrorMessage = "Không được để trống Mật khẩu")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,20}$", ErrorMessage = "Mật khẩu không hợp lệ.")]
        public string PASS { get; set; }

        [Required(ErrorMessage = "Không được để trống Số Điện Thoại")]
        [RegularExpression(@"^(?:\+84|0)(?:[1-9]\d{8}|3\d{8})$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; }

        public string LOAITK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOANTIEN> HOANTIENs { get; set; }
        public virtual LOAITAIKHOAN LOAITAIKHOAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHONG> PHONGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YEUTHICH> YEUTHICHes { get; set; }
    }
}
