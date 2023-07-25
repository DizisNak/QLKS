using QLKS_CNPM_LT.Models;
using QLKS_CNPM_LT.Models.Function;
using QLKS_CNPM_LT.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QLKS_CNPM_LT.Controllers
{
    public class TaiKhoanController : Controller
    {
        private qlks_CNPMEntities db = new qlks_CNPMEntities();
        // GET: TaiKhoan

        public ActionResult DangNhap()
        {
            if (Session["TaiKhoan"] != null) return RedirectToAction("Home", "Home");
            return View(new TaiKhoanDangNhapView());
        }

        public ActionResult DangKy()
        {
            if (Session["TaiKhoan"] != null) return RedirectToAction("TrangChu", "TrangChu");
            return View(new TaiKhoanDangKyView());
        }


        [HttpPost]
        public ActionResult DangNhap(TaiKhoanDangNhapView tk)
        {
            if (ModelState.IsValid)
            {
                var list = db.TAIKHOANs.Where(m => m.TenTK == tk.TenTK && m.PASS == tk.PASS).ToList();
                if (list.Count == 0)
                {
                    ModelState.AddModelError("TenTK", "Tài Khoản hoặc Mật Khẩu không chính xác");
                    return View(tk);
                }
                TAIKHOAN taiKhoan = list.FirstOrDefault();
                //if (taiKhoan.LOAITK == "AD")
                //{
                //    FormsAuthentication.SetAuthCookie(taiKhoan.TenTK, tk.TuDongDangNhap);
                //    return RedirectToAction("DSTaiKhoan", "Admin");
                //}


                FormsAuthentication.SetAuthCookie(taiKhoan.TenTK, tk.TuDongDangNhap);
                Session["TaiKhoan"] = taiKhoan;
                if (tk.TuDongDangNhap)
                {
                    HttpCookie ckTaiKhoan = new HttpCookie("TenTK"), ckMatKhau = new HttpCookie("PASS");
                    ckTaiKhoan.Value = taiKhoan.TenTK; ckMatKhau.Value = taiKhoan.PASS;
                    ckTaiKhoan.Expires = DateTime.Now.AddDays(15);
                    ckMatKhau.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(ckTaiKhoan);
                    Response.Cookies.Add(ckMatKhau);
                }

                if (Session["TrangTruoc"] != null)
                {
                    return Redirect(Session["TrangTruoc"].ToString());
                }
                return RedirectToAction("Home", "Home");
            }
            return View(tk);
        }


        [HttpPost]
        public ActionResult DangKy(TaiKhoanDangKyView tk)
        {
            if (ModelState.IsValid)
            {
                var taiKhoan = db.TAIKHOANs.Find(tk.TenTK);
                if (taiKhoan != null)
                {
                    ModelState.AddModelError("Ten tai khoan", "Tài Khoản đã tồn tại");
                    return View(tk);
                }

                var ID_TK = db.TAIKHOANs.ToList();
                string MAKH = ""; // The final formatted string to be generated

                if (ID_TK.Count == 0)
                {
                    MAKH = "KH01";
                }
                else
                {
                    int lastMaDatPhongNumber = int.Parse(ID_TK.Last().ID_TK.Substring(2));
                    int nextMaDatPhongNumber = lastMaDatPhongNumber + 1;
                    MAKH = "KH" + nextMaDatPhongNumber.ToString("D2");
                }

                taiKhoan = new TAIKHOAN()
                {
                    ID_TK = MAKH,
                    TenTK = tk.TenTK,
                    PASS = tk.PASS,
                    SDT = tk.SoDienThoai,
                    Gmail = tk.Email,
                    LOAITK = "KH"
                };
                var TK = new TaiKhoan_Func();
                TK.Insert(taiKhoan);
                return View("DangKyThanhCong", taiKhoan);
            }
            return View(tk);
        }

        public ActionResult DangKyThanhCong()
        {
            return View(new TAIKHOAN());
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            HttpCookie ckTaiKhoan = new HttpCookie("TenTK"), ckMatKhau = new HttpCookie("PASS");
            ckTaiKhoan.Expires = DateTime.Now.AddDays(-1);
            ckMatKhau.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(ckTaiKhoan);
            Response.Cookies.Add(ckMatKhau);
            return RedirectToAction("Home", "Home");
        }





    }
}