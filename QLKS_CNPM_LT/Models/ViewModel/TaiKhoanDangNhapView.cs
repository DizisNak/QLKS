using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.ViewModel
{
    public class TaiKhoanDangNhapView
    {
        [Required(ErrorMessage = "Không được để trống Tài Khoản")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Gmail { get; set; }

        [Required(ErrorMessage = "Không được để trống Mật Khẩu")]
        public string PASS { get; set; }

        public bool TuDongDangNhap { get; set; }
    }
}