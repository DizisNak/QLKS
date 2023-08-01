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
        public string ID_TK { get; set; }
        [Required(ErrorMessage = "Không được để trống Tài Khoản")]
        public string TenTK { get; set; }

        [Required(ErrorMessage = "Không được để trống Mật Khẩu")]
        public string PASS { get; set; }

        [Compare("PASS", ErrorMessage = "Mật Khẩu Không Khớp")]
        public string XacNhanMatKhau { get; set; }

        [Required(ErrorMessage = "Không được để trống Số Điện Thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Không được để trống mail")]
        public string Gmail { get; set; }
        public string ANH { get; set; }
        public string LOAITK { get; set; }
    }
}