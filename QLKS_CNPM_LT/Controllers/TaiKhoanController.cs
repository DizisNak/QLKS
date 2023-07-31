using QLKS_CNPM_LT.Models;
using QLKS_CNPM_LT.Models.Function;
using QLKS_CNPM_LT.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QLKS_CNPM_LT.Controllers
{
    public class TaiKhoanController : Controller
    {
        private QLKS_CNPMEntities db = new QLKS_CNPMEntities();
        // GET: TaiKhoan

        public ActionResult DangNhap()
        {
            if (Session["TaiKhoan"] != null) return RedirectToAction("Home", "Home");
            return View(new TaiKhoanDangNhapView());
        }

        public ActionResult DangKy()
        {
            if (Session["TaiKhoan"] != null) return RedirectToAction("Home", "Home");
            return View(new TaiKhoanDangKyView());
        }


        [HttpPost]
        public ActionResult DangNhap(TaiKhoanDangNhapView tk)
        {

            if (ModelState.IsValid)
            {
                var list = db.TAIKHOANs.Where(m => m.Gmail.Trim() == tk.Gmail.Trim() && m.PASS == tk.PASS).ToList();
                if (list.Count == 0)
                {
                    ModelState.AddModelError("Gmail", "Tài Khoản hoặc Mật Khẩu không chính xác");
                    return View(tk);
                }
                TAIKHOAN taiKhoan = list.FirstOrDefault();

                if (taiKhoan.LOAITK.Trim().Equals("AD"))
                {
                    Session["TaiKhoan"] = taiKhoan;
                    FormsAuthentication.SetAuthCookie(taiKhoan.Gmail, tk.TuDongDangNhap);
                    return RedirectToAction("Home", "Home");
                }

                if(taiKhoan.LOAITK.Trim().Equals("KH"))
                {
                    FormsAuthentication.SetAuthCookie(taiKhoan.Gmail, tk.TuDongDangNhap);
                    Session["TaiKhoan"] = taiKhoan;
                }
               
                if (tk.TuDongDangNhap)
                {
                    HttpCookie ckTaiKhoan = new HttpCookie("Gmail"), ckMatKhau = new HttpCookie("PASS");
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
                var checkSDT = db.TAIKHOANs.Where(m => m.SDT == tk.SDT).FirstOrDefault();
                var checkGmail = db.TAIKHOANs.Where(m => m.Gmail == tk.Gmail).FirstOrDefault();

                if (checkGmail != null)
                {
                    ModelState.AddModelError("Gmail", "Mail này đã được đăng ký");
                    return View(tk);
                }

                else if (checkSDT != null)
                {
                    ModelState.AddModelError("SDT", "Số điện thoại này đã được đăng ký");
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

                var taoTK = new TAIKHOAN()
                {
                    ID_TK = MATK,
                    TenTK = tk.TenTK,
                    PASS = tk.PASS,
                    SDT = tk.SDT,
                    Gmail = tk.Gmail,
                    LOAITK = "KH"
                };
                var TK = new TaiKhoan_Func();
                TK.Insert(taoTK);
                return View("DangKyThanhCong", taoTK);
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
            HttpCookie ckTaiKhoan = new HttpCookie("Gmail"), ckMatKhau = new HttpCookie("PASS");
            ckTaiKhoan.Expires = DateTime.Now.AddDays(-1);
            ckMatKhau.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(ckTaiKhoan);
            Response.Cookies.Add(ckMatKhau);
            return RedirectToAction("Home", "Home");
        }


        public ActionResult CaNhan()
        {
            if(Session["TaiKhoan"] == null)
            {
                Session["TrangTruoc"] = Request.RawUrl;
                return Redirect("DangNhap");
            }
            var TaiKhoan = (TAIKHOAN)Session["TaiKhoan"];
            var TaiKhoanCaNhan = new TaiKhoanDangKyView
            {
                ID_TK = TaiKhoan.ID_TK,
                TenTK = TaiKhoan.TenTK,
                PASS = TaiKhoan.PASS,
                XacNhanMatKhau = TaiKhoan.PASS,
                Gmail = TaiKhoan.Gmail,
                ANH = TaiKhoan.ANH,
                SDT = TaiKhoan.SDT
            };
            return View(TaiKhoanCaNhan);
        }

        [HttpPost]
        public ActionResult CaNhan(TaiKhoanDangKyView tk, HttpPostedFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/profileImg/"), fileName);
                    file.SaveAs(path);
                }

                var taiKhoan = new TAIKHOAN()
                {
                    ID_TK = tk.ID_TK,
                    TenTK = tk.TenTK,
                    PASS = tk.PASS,
                    SDT = tk.SDT,
                    Gmail = tk.Gmail,
                    ANH = tk.ANH
                };

                Session["TaiKhoan"] = taiKhoan;
                var HamTK = new TaiKhoan_Func();
                HamTK.Update(taiKhoan);
                ViewBag.Success = 1;
                return RedirectToAction("CaNhan", "TaiKhoan");
            }
            return View(tk);
        }
    }
}