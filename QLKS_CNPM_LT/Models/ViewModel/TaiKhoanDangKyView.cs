using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.ViewModel
{
    public class TaiKhoanDangKyView
    {
        public TaiKhoanDangKyView()
        {
            ANH = "~/Content/profileImg/profileImg.png";
        }
        public string ID_TK { get; set; }
        [Required(ErrorMessage = "Không được để trống Tài Khoản"), MinLength(6, ErrorMessage = "Tối thiểu 6 kí tự"), MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string TenTK { get; set; }

        [Required(ErrorMessage = "Không được để trống Mật khẩu")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,20}$", ErrorMessage = "Mật khẩu phải 8 kí tự, 1 chữ in hoa hoặc 1 ký tự đặc biệt.")]
        public string PASS { get; set; }

        [Compare("PASS", ErrorMessage = "Mật Khẩu Không Khớp")]
        public string XacNhanMatKhau { get; set; }


        [Required(ErrorMessage = "Không được để trống Số Điện Thoại")]
        [RegularExpression(@"^(?:\+84|0)(?:[1-9]\d{8}|3\d{8})$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Không được để trống mail")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Gmail { get; set; }
        [Required(ErrorMessage = "Không được để trống Ảnh")]
        public string ANH { get; set; }

        public string LOAITK { get; set; }

        public bool TuDongDangNhap { get; set; }
    }
}