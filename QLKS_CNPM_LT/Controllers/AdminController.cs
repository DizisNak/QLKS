using QLKS_CNPM_LT.Models;
using QLKS_CNPM_LT.Models.Function;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKS_CNPM_LT.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private QLKS_CNPMEntities db = new QLKS_CNPMEntities();
        private int MaxPhanTuMoiTrang = 8;
        public ActionResult DSTaiKhoan()
        {
            var list = db.TAIKHOANs.ToList();
            //int TongPhanTu = list.Count;
            //int SoTrang = (TongPhanTu - 1) / MaxPhanTuMoiTrang + 1;
            //int Trang = 1;
            //try
            //{
            //    string id = RouteData.Values["id"].ToString();
            //    Trang = Convert.ToInt16(id);
            //    if (Trang > SoTrang) Trang = SoTrang;
            //}
            //catch (Exception e) { }
            //int PhanTuDau = (Trang - 1) * MaxPhanTuMoiTrang;
            //int SoPhanTu = MaxPhanTuMoiTrang;
            //if (Trang == SoTrang) SoPhanTu = TongPhanTu - (SoTrang - 1) * MaxPhanTuMoiTrang;
            //var listMoiTrang = list.GetRange(PhanTuDau, SoPhanTu);
            //ViewBag.STT = PhanTuDau;
            //ViewBag.Trang = Trang;
            //ViewBag.SoTrang = SoTrang;
            return View(list);
        }

        public ActionResult ThemTaiKhoan()
        {
            return View(new TAIKHOAN());
        }

        public ActionResult XoaTaiKhoan()
        {
            string ID_TK = RouteData.Values["id"].ToString();
            // Trước khi xóa Tài Khoản phải xóa thông tin đặt phòng
            var listDatPhong = db.HOADONs.Where(m => m.MAKH == ID_TK).ToList();
            var HamDP = new HoaDon_Func();
            foreach (HOADON dp in listDatPhong)
                HamDP.Delete(dp.MAHD);
            // Sau đó mới xóa Tài Khoản
            var HamTK = new TaiKhoan_Func();
            HamTK.Delete(ID_TK);
            return RedirectToAction("DSTaiKhoan", "Admin");
        }

        [HttpPost]
        public ActionResult ThemTaiKhoan(TAIKHOAN tk, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var taiKhoan = db.TAIKHOANs.Find(tk.ID_TK);
                if (taiKhoan != null)
                {
                    ModelState.AddModelError("TenTaiKhoan", "Tài Khoản đã tồn tại");
                    return View(tk);
                }

                var ID_TK = db.TAIKHOANs.ToList();
                string MATK = ""; // The final formatted string to be generated

                if (ID_TK.Count == 0)
                {
                    MATK = "KH01";
                }
                else
                {
                    int lastMaDatPhongNumber = int.Parse(ID_TK.Last().ID_TK.Substring(2));
                    int nextMaDatPhongNumber = lastMaDatPhongNumber + 1;
                    MATK = "KH" + nextMaDatPhongNumber.ToString("D2");
                }

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Data/profileImg/"), fileName);
                    file.SaveAs(path);
                }

                var taoTK = new TAIKHOAN()
                {
                    ID_TK = MATK,
                    TenTK = tk.TenTK,
                    PASS = tk.PASS,
                    SDT = tk.SDT,
                    Gmail = tk.Gmail,
                    LOAITK = tk.LOAITK,
                    ANH = tk.ANH
                };

                var hTK = new TaiKhoan_Func();
                hTK.Insert(taoTK);
                return RedirectToAction("DSTaiKhoan", "Admin");
            }
            return View(tk);
        }

        public ActionResult SuaTaiKhoan()
        {
            string ID_TK = RouteData.Values["id"].ToString();
            var taiKhoan = db.TAIKHOANs.Find(ID_TK);
            return View(taiKhoan);
        }


        [HttpPost]
        public ActionResult SuaTaiKhoan(TAIKHOAN tk, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("/Data/profileImg/"), fileName);
                    file.SaveAs(path);
                }
                var HamTK = new TaiKhoan_Func();
                HamTK.Update(tk);
                return RedirectToAction("DSTaiKhoan", "Admin");
            }
            return View(tk);
                
            
        }

    }
}