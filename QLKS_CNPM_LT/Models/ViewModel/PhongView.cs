using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.ViewModel
{
    public class PhongView
    {
        public string MA_PHONG { get; set; }
        [Required(ErrorMessage = "Không được để trống Tên phòng")]
        public string TENPhong { get; set; }
        public string TRANGTHAI { get; set; }
        [Required(ErrorMessage = "Không được để trống Giá")]
        public double? GIA { get; set; }
        [Required(ErrorMessage = "Không được để trống mã loại")]
        public string MALOAI { get; set; }
        [Required(ErrorMessage = "Không được để trống Ảnh")]
        public string ANH { get; set; }
        public byte? DaDuyet { get; set; }

    }
}