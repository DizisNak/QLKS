using QLKS_CNPM_LT.Models;
using QLKS_CNPM_LT.Models.Function;
using QLKS_CNPM_LT.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace QLKS_CNPM_LT.Controllers
{
    public class PhongController : Controller
    {
        // GET: Phong
        private QLKS_CNPMEntities db = new QLKS_CNPMEntities();

        //[Authorize(Roles = "AD")]
        public ActionResult DSPhong()
        {
            var func_phong = new Phong_Func();
            return View(func_phong.toanBoPhong());
        }


        public ActionResult CTlOAIPhong()
        {

            ViewBag.Success = 0;
            string id = (string)RouteData.Values["id"];
            LOAIPHONG lp = db.LOAIPHONGs.Find(id);
            if (lp == null)
            {
                return View(new List<PHONG>());
            }
            ViewBag.Success = 1;
            ViewBag.TenLoai = lp.TenLoai;
            ViewBag.DuongDanAnh = lp.DuongDanAnh;
            ViewBag.GhiChu = lp.GhiChu;
            var listPhong = db.PHONGs.Where(m => m.MaLoai == id).ToList();
            return View(listPhong);
        }

        public ActionResult DatPhongThanhCong()
        {
            return View();
        }

        public ActionResult DatPhong()
        {
            if (Session["TaiKhoan"] == null)
            {
                Session["TrangTruoc"] = Request.RawUrl;
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            string MA_PHONG = (string)RouteData.Values["id"];
            var phong = db.PHONGs.Where(m => m.MA_PHONG == MA_PHONG).First();
            var loaiPhong = db.LOAIPHONGs.Where(m => m.MaLoai == phong.MaLoai).First();
            return View(phong);
        }

        [HttpPost]
        public ActionResult DatPhong(string NgayDen, string SoNgay)
        {
            string MA_PHONG = (string)RouteData.Values["id"];
            var phong = db.PHONGs.Where(m => m.MA_PHONG == MA_PHONG).First();
            if (SoNgay == "")
            {
                ViewBag.Success = -1;
                var loaiPhong = db.LOAIPHONGs.Where(m => m.MaLoai == phong.MaLoai).First();
                ViewBag.DuongDanAnh = loaiPhong.DuongDanAnh;
                ViewBag.TenLoai = loaiPhong.TenLoai;
                return View(phong);
            }
            int SoNgayThue = Convert.ToInt16(SoNgay);
            DateTime dateNgayDat, dateNgayDen, dateNgayTra;

            dateNgayDat = DateTime.Today;
            dateNgayDen = Convert.ToDateTime(NgayDen);
            dateNgayTra = dateNgayDen.AddDays(SoNgayThue);

            var kiemTraPhongBiDatChua = db.HOADONs.
                Where(m => m.MA_PHONG == MA_PHONG && !(m.NgayDen >= dateNgayTra || m.NgayTra <= dateNgayDen)).ToList(); //NgayDen 20 >= 8 NgayTra || NgayTra 21 <= 30
            if (kiemTraPhongBiDatChua.Count > 0)
            {
                var listDaBiDat = db.HOADONs.Where(m => m.NgayDen < dateNgayTra && m.NgayTra > dateNgayDen).Select(m => m.MA_PHONG);
                var listPhongDatDuoc = db.PHONGs.Where(m => !listDaBiDat.Contains(m.MA_PHONG)).Join(db.LOAIPHONGs, p => p.MaLoai, lp => lp.MaLoai, (p, lp) =>
                    new PhongView
                    {
                        MA_PHONG = p.MA_PHONG,
                        TENPhong = p.TENPhong,
                        GIA = p.GIA,
                        MALOAI = lp.MaLoai,
                        ANH = p.ANH
                    });
                ViewBag.Success = 1;

                var loaiPhong = db.LOAIPHONGs.Where(m => m.MaLoai == phong.MaLoai).First();
                ViewBag.DuongDanAnh = loaiPhong.DuongDanAnh;
                ViewBag.TenLoai = loaiPhong.TenLoai;

                ViewBag.ListDatDuoc = listPhongDatDuoc.ToList();
                return View(phong);
            }
            var listDatPhong = db.HOADONs.ToList();

            //if (listDatPhong.Count == 0) MaDatPhong = 1;
            //else MaDatPhong = listDatPhong.Last().MAHD + 1;

            string MAHD = ""; // The final formatted string to be generated

            if (listDatPhong.Count == 0)
            {
                MAHD = "HD01";
            }
            else
            {
                int lastMaDatPhongNumber = int.Parse(listDatPhong.Last().MAHD.Substring(2));
                int nextMaDatPhongNumber = lastMaDatPhongNumber + 1;
                MAHD = "HD" + nextMaDatPhongNumber.ToString("D2");
            }

            double ThanhTien = 0;
            string DichVuSuDung = "";

            //foreach (DichVu dv in listDV)
            //{
            //    if (Request.Form[dv.MaDichVu] == "on")
            //    {
            //        if (ThanhTien > 0) DichVuSuDung += ", ";
            //        DichVuSuDung += dv.TenDichVu;
            //        ThanhTien += (int)dv.GiaDichVu;
            //    }
            //}
            //if (ThanhTien == 0) DichVuSuDung = "Không Sử Dụng";

            ThanhTien += phong.GIA.Value;
            ThanhTien *= SoNgayThue;
            string ID_TK = ((TAIKHOAN)Session["TaiKhoan"]).ID_TK;

            HOADON hoadon = new HOADON
            {
                MAHD = MAHD,
                MAKH = ID_TK,
                MA_PHONG = MA_PHONG,
                NgayDat = dateNgayDat,
                NgayDen = dateNgayDen,
                NgayTra = dateNgayTra,
                TONGTIEN = (int)ThanhTien
            };
            HoaDon_Func HamDP = new HoaDon_Func();
            HamDP.Insert(hoadon);
            ViewBag.TenPhong = phong.TENPhong;
            ViewBag.GIA = phong.GIA;
            ViewBag.LOAIPHONG = phong.LOAIPHONG.TenLoai;
            ViewBag.GiaThue = phong.ANH;
            ViewBag.NgayDat = dateNgayDat.ToString("dd/MM/yyyy");
            ViewBag.NgayDen = dateNgayDen.ToString("dd/MM/yyyy");
            ViewBag.NgayTra = dateNgayTra.ToString("dd/MM/yyyy");
            ViewBag.DichVu = DichVuSuDung;
            ViewBag.ThanhTien = ThanhTien;
            return View("DatPhongThanhCong");
        }



    }
}