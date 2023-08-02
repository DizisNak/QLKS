using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.ViewModel
{
    public class LichSuView
    {
        public string MaDatPhong { get; set; }
        public string TenPhong { get; set; }
        public string NgayDat { get; set; }
        public string NgayDen { get; set; }
        public string NgayTra { get; set; }
        public string DichVu { get; set; }
        public double? ThanhTien { get; set; }
        public bool CoTheHuy { get; set; }
    }
}