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
        public ActionResult DatPhong(string NgayDen, string NgayVe)
        {
            string MA_PHONG = (string)RouteData.Values["id"];
            var phong = db.PHONGs.Where(m => m.MA_PHONG == MA_PHONG).First();

            DateTime dateNgayDat, dateNgayDen, dateNgayTra;

            dateNgayDat = DateTime.Today;
            dateNgayDen = Convert.ToDateTime(NgayDen);
            dateNgayTra = Convert.ToDateTime(NgayVe);

            // Calculate the difference between the two dates
            TimeSpan difference = dateNgayTra - dateNgayDen;

            // Get the number of days as an integer value
            int SoNgayThue = difference.Days + 1;


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
            ViewBag.NgayDat = dateNgayDat.ToString("dd/MM/yyyy");
            ViewBag.NgayDen = dateNgayDen.ToString("dd/MM/yyyy");
            ViewBag.NgayTra = dateNgayTra.ToString("dd/MM/yyyy");
            ViewBag.ThanhTien = ThanhTien;
            return View("DatPhongThanhCong");
        }



    }
}