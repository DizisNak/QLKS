using QLKS_CNPM_LT.Models;
using QLKS_CNPM_LT.Models.Function;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace QLKS_CNPM_LT.Controllers
{
    [Authorize(Roles = "AD")]
    public class AdminController : Controller
    {
        // GET: Admin

        private QLKS_CNPMEntities db = new QLKS_CNPMEntities();


        /*                                     VIEW                                        */
        public ActionResult DSTaiKhoan()
        {
            var list = db.TAIKHOANs.ToList();
            return View(list);
        }

        public ActionResult DSPhong()
        {
            var list = db.PHONGs.ToList();
            return View(list);
        }


        public ActionResult DSLoaiTaiKhoan()
        {
            var list = db.LOAITAIKHOANs.ToList();
            return View(list);
        }


        public ActionResult DSLoaiPhong()
        {
            var list = db.LOAIPHONGs.ToList();
            return View(list);
        }

        public ActionResult DSLichSuPhong()
        {
            var list = db.HOADONs.ToList();
            return View(list);
        }

        public ActionResult ThemTaiKhoan()
        {
            return View(new TAIKHOAN());
        }

        public ActionResult ThemPhong()
        {
            ViewBag.listLoaiPhong = db.LOAIPHONGs.ToList();
            return View(new PHONG());
        }

        public ActionResult ThemLoaiPhong()
        {
            return View(new LOAIPHONG());
        }

        public ActionResult ThemLoaiTaiKhoan()
        {
            return View(new LOAITAIKHOAN());
        }


        /*                                     VIEW                                        */


        /*                                     THÊM                                        */

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
                    int lastIDTK = int.Parse(ID_TK.Last().ID_TK.Substring(2));
                    int nextIDTK = lastIDTK + 1;
                    MATK = "KH" + nextIDTK.ToString("D2");
                }

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Data/profileImg/"), fileName);
                    file.SaveAs(path);
                }

                var taoTK = new TAIKHOAN()
                {
                    ID_TK = MATK.Trim(),
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

        [HttpPost]
        public ActionResult ThemPhong(PHONG p, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var idLP = db.PHONGs.Find(p.MA_PHONG);
                var tenLP = db.PHONGs.Find(p.TENPhong);
                if (idLP != null)
                {
                    ModelState.AddModelError("MA_PHONG", "ID này đã có");
                    return View(p);
                }
                if (tenLP != null)
                {
                    ModelState.AddModelError("TENPhong", "Tên phòng này đã có");
                    return View(p);
                }

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Data/roomImg/"), fileName);
                    file.SaveAs(path);
                    p.ANH = "~/Data/roomImg/" + fileName;
                }

                var ID_PHONG = db.PHONGs.ToList();
                string MAPHONG = ""; // The final formatted string to be generated

                if (ID_PHONG.Count == 0)
                {
                    MAPHONG = "P01";
                }
                else
                {
                    int lastMaDatPhongNumber = int.Parse(ID_PHONG.Last().MA_PHONG.Substring(2));
                    int nextMaDatPhongNumber = lastMaDatPhongNumber + 1;
                    MAPHONG = "P" + nextMaDatPhongNumber.ToString("D2");
                }

                var taoPhong = new PHONG()
                {
                    MA_PHONG = MAPHONG,
                    TENPhong = p.TENPhong,
                    GIA = p.GIA,
                    DiaChi = p.DiaChi,
                    MaLoai = p.MaLoai,
                    ID_TK = p.ID_TK,
                    ANH = p.ANH,
                    TRANGTHAI = p.TRANGTHAI,
                    NoiDung = p.NoiDung,
                    DaDuyet = 1
                };


                var ltkFunc = new Phong_Func();
                ltkFunc.Insert(taoPhong);
                return RedirectToAction("DSPhong", "Admin");
            }
            //p.ANH = "~/Data/roomImg/DefaultRoom.jpg";
            ViewBag.listLoaiPhong = db.LOAIPHONGs.ToList();
            return View(p);
        }




        [HttpPost]
        public ActionResult ThemLoaiPhong(LOAIPHONG lp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var tenLP = db.LOAIPHONGs.Find(lp.TenLoai.Trim());
                if (tenLP != null)
                {
                    ModelState.AddModelError("TenLoai", "Tên loại này đã có");
                    return View(lp);
                }

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Data/roomImg/"), fileName);
                    file.SaveAs(path);
                    lp.DuongDanAnh = "~/Data/roomImg/" + fileName;
                }

                var IDLOAIPHONG = db.LOAIPHONGs.ToList();
                string MALOAI = ""; // The final formatted string to be generated

                if (IDLOAIPHONG.Count == 0)
                {
                    MALOAI = "LP01";
                }
                else
                {
                    int lastMaDatPhongNumber = int.Parse(IDLOAIPHONG.Last().MaLoai.Substring(2));
                    int nextMaDatPhongNumber = lastMaDatPhongNumber + 1;
                    MALOAI = "LP" + nextMaDatPhongNumber.ToString("D2");
                }


                var taolp = new LOAIPHONG()
                {
                    MaLoai = MALOAI,
                    TenLoai = lp.TenLoai,
                    GhiChu = lp.GhiChu,
                    DuongDanAnh = lp.DuongDanAnh
                };

                var ltkFunc = new LoaiPhong_Func();
                ltkFunc.Insert(taolp);
                return RedirectToAction("DSLoaiPhong", "Admin");
            }
            lp.DuongDanAnh = "~/Data/roomImg/DefaultRoom.jpg";
            return View(lp);
        }

        [HttpPost]
        public ActionResult ThemLoaiTaiKhoan(LOAITAIKHOAN ltk)
        {
            if (ModelState.IsValid)
            {

                var taiKhoan = db.LOAITAIKHOANs.Find(ltk.ID_LOAITK);
                if (taiKhoan != null)
                {
                    ModelState.AddModelError("ID_LOAITK", "ID này đã có");
                    return View(ltk);
                }



                var taoLtk = new LOAITAIKHOAN()
                {
                    ID_LOAITK = ltk.ID_LOAITK,
                    TENLOAITK = ltk.TENLOAITK.Trim()
                };
                var ltkFunc = new LoaiTaiKhoan_Func();
                ltkFunc.Insert(taoLtk);
                return RedirectToAction("DSLoaiTaiKhoan", "Admin");
            }
            return View(ltk);
        }

        /*                                     THÊM                                        */



        /*                                     XOÁ                                        */

        [HttpPost]
        public ActionResult DSTaiKhoan(FormCollection form)
        {
            // Trước khi xóa Tài Khoản phải xóa thông tin đặt phòng
            //ids = ID TK
            string[] ids = form["ID_TK"].Split(new char[] { ',' });
            var HamDP = new HoaDon_Func();
            var HamTK = new TaiKhoan_Func();
            foreach (var id in ids)
            {
                var listDatPhong = db.HOADONs.Where(m => m.MAKH == id).ToList();

                foreach (HOADON dp in listDatPhong)
                    HamDP.Delete(dp.MAHD);
                // Sau đó mới xóa Tài Khoản
                HamTK.Delete(id);
            }
            return RedirectToAction("DSTaiKhoan", "Admin");
        }

        [HttpPost]
        public ActionResult DSLoaiTaiKhoan(FormCollection form)
        {
            string[] ids = form["ID_LOAITK"].Split(new char[] { ',' });
            var HamTK = new TaiKhoan_Func();
            var HamHD = new HoaDon_Func();
            var HamPhong = new Phong_Func();

            var HamLTK = new LoaiTaiKhoan_Func();
            foreach (var id in ids)
            {
                var IDTK = db.TAIKHOANs.Where(m => m.LOAITK == id).ToList();
                foreach (TAIKHOAN tk in IDTK)
                {
                    var listHD = db.HOADONs.Where(m => m.MAKH == tk.ID_TK).ToList();
                    var listPhong = db.PHONGs.Where(p => p.ID_TK == tk.ID_TK).ToList();
                    foreach (HOADON hd in listHD)
                    {
                        HamHD.Delete(hd.MAHD);
                    }

                    foreach (PHONG p in listPhong)
                    {
                        HamPhong.Delete(p.MA_PHONG);
                    }

                    HamTK.Delete(tk.ID_TK);
                }
                HamLTK.Delete(id);
            }
            ViewBag.Er = 1;
            return RedirectToAction("DSLoaiTaiKhoan", "Admin");
        }

        [HttpPost]
        public ActionResult DSLoaiPhong(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                // Trước khi xóa Loại Phòng phải xóa các Phòng liên quan
                // Nhưng muốn xóa Phòng phải xóa Đặt Phòng trước

                string[] ids = form["MaLoai"].Split(new char[] { ',' });
                var HamPhong = new Phong_Func();
                var HamDP = new HoaDon_Func();
                var HamLP = new LoaiPhong_Func();
                foreach (string id in ids)
                {
                    var ListPhong = db.PHONGs.Where(m => m.MaLoai == id).ToList();
                    foreach (PHONG p in ListPhong)
                    {
                        var ListHD = db.HOADONs.Where(m => m.MA_PHONG == p.MA_PHONG).ToList();
                        foreach (HOADON dp in ListHD)
                        {
                            HamDP.Delete(dp.MAHD);
                        }
                        HamPhong.Delete(p.MA_PHONG);
                    }
                    HamLP.Delete(id);
                }
            }
            return RedirectToAction("DSLoaiPhong", "Admin");
        }

        [HttpPost]
        public ActionResult DSPhong(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                string[] ids = form["MA_PHONG"].Split(new char[] { ',' });
                var HamPhong = new Phong_Func();
                var HamDP = new HoaDon_Func();
                foreach (string id in ids)
                {
                    var listDatPhong = db.HOADONs.Where(m => m.MA_PHONG == id).ToList();
                    foreach (HOADON dp in listDatPhong)
                        HamDP.Delete(dp.MAHD);
                    HamPhong.Delete(id);
                }
            }
            return RedirectToAction("DSPhong", "Admin");
        }

        public ActionResult XoaLoaiTaiKhoan()
        {

            // Trước khi xóa Loại Tài khoản phải xoá các tài khoản liên quan
            // Nhưng tài khoản liên quan đến Hoá đơn
            // Phải xoá luôn Hoá đơn của tài khoản đó

            string ID_LOAITK = RouteData.Values["id"].ToString();
            if(ID_LOAITK.Trim() == "AD" || ID_LOAITK.Trim() ==  "KH")
            {
                return RedirectToAction("DSLoaiTaiKhoan", "Admin");
            }

            var IDTK = db.TAIKHOANs.Where(m => m.LOAITK == ID_LOAITK).ToList();
            var HamTK = new TaiKhoan_Func();
            var HamHD = new HoaDon_Func();
            var HamPhong = new Phong_Func();
            foreach (TAIKHOAN tk in IDTK)
            {
                var listHD = db.HOADONs.Where(m => m.MAKH == tk.ID_TK).ToList();
                var listPhong = db.PHONGs.Where(p => p.ID_TK == tk.ID_TK).ToList();
                foreach (HOADON hd in listHD)
                {
                    HamHD.Delete(hd.MAHD);
                }

                foreach (PHONG p in listPhong)
                {
                    HamPhong.Delete(p.MA_PHONG);
                }

                HamTK.Delete(tk.ID_TK);
            }
            var HamLTK = new LoaiTaiKhoan_Func();
            HamLTK.Delete(ID_LOAITK);
            ViewBag.Er = 1;
            return RedirectToAction("DSLoaiTaiKhoan", "Admin");

        }

        //public ActionResult XoaLoaiPhong()
        //{
        //    string MaLoai = RouteData.Values["id"].ToString();

        //    // Trước khi xóa Loại Phòng phải xóa các Phòng liên quan
        //    // Nhưng muốn xóa Phòng phải xóa Đặt Phòng trước
        //    var listPhong = db.PHONGs.Where(m => m.MaLoai == MaLoai).ToList();
        //    var HamP = new Phong_Func();
        //    var HamDP = new HoaDon_Func();
        //    foreach (PHONG p in listPhong)
        //    {
        //        var listDatPhong = db.HOADONs.Where(m => m.MA_PHONG == p.MA_PHONG).ToList();
        //        foreach (HOADON dp in listDatPhong)
        //            HamDP.Delete(dp.MAHD);
        //        HamP.Delete(p.MA_PHONG);
        //    }
        //    // Sau đó mới xóa Loại Phòng
        //    var HamLP = new LoaiPhong_Func();
        //    HamLP.Delete(MaLoai);
        //    return RedirectToAction("DSLoaiPhong", "Admin");
        //}

        //public ActionResult XoaTaiKhoan()
        //{
        //    string ID_TK = RouteData.Values["id"].ToString();
        //    // Trước khi xóa Tài Khoản phải xóa thông tin đặt phòng
        //    var listDatPhong = db.HOADONs.Where(m => m.MAKH == ID_TK).ToList();
        //    var HamDP = new HoaDon_Func();
        //    foreach (HOADON dp in listDatPhong)
        //        HamDP.Delete(dp.MAHD);
        //    // Sau đó mới xóa Tài Khoản
        //    var HamTK = new TaiKhoan_Func();
        //    HamTK.Delete(ID_TK);
        //    return RedirectToAction("DSTaiKhoan", "Admin");
        //}

        //public ActionResult XoaPhong()
        //{
        //    string MaPhong = RouteData.Values["id"].ToString();
        //    // Trước khi xóa Phòng phải xóa thông tin đặt phòng
        //    var listDatPhong = db.HOADONs.Where(m => m.MA_PHONG == MaPhong).ToList();
        //    var HamDP = new HoaDon_Func();
        //    foreach (HOADON dp in listDatPhong)
        //        HamDP.Delete(dp.MAHD);
        //    // Sau đó mới xóa Phòng
        //    var HamP = new Phong_Func();
        //    HamP.Delete(MaPhong);
        //    return RedirectToAction("DSPhong", "Admin");
        //}

        /*                                     XOÁ                                        */


        /*                                     SỬA                                        */
        public ActionResult SuaLoaiTaiKhoan()
        {
            string ID = RouteData.Values["id"].ToString();
            var taiKhoan = db.LOAITAIKHOANs.Find(ID);
            return View(taiKhoan);
        }

        public ActionResult SuaTaiKhoan()
        {
            String Using = ((TAIKHOAN)Session["TaiKhoan"]).ID_TK.ToString();
            string ID = RouteData.Values["id"].ToString();
            if(Using == ID)
            {
                return RedirectToAction("DSTaiKhoan", "Admin");
            }
            var taiKhoan = db.TAIKHOANs.Find(ID);
            return View(taiKhoan);
        }



        [HttpPost]
        public ActionResult SuaLoaiTaiKhoan(LOAITAIKHOAN ltk)
        {
            if (ModelState.IsValid)
            {
                var LTK = new LoaiTaiKhoan_Func();
                LTK.Update(ltk);
                return RedirectToAction("DSTaiKhoan", "Admin");
            }
            return View(ltk);
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

        public ActionResult SuaLoaiPhong()
        {
            string ID = RouteData.Values["id"].ToString();
            var loaiphong = db.LOAIPHONGs.Find(ID);
            return View(loaiphong);
        }

        [HttpPost]
        public ActionResult SuaLoaiPhong(LOAIPHONG lp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("/Data/profileImg/"), fileName);
                    file.SaveAs(path);
                }
                var HamTK = new LoaiPhong_Func();
                HamTK.Update(lp);
                return RedirectToAction("DSLoaiPhong", "Admin");
            }
            return View(lp);
        }

        public ActionResult SuaPhong()
        {
            string ID = RouteData.Values["id"].ToString();
            var phong = db.PHONGs.Find(ID);
            return View(phong);
        }

        [HttpPost]
        public ActionResult SuaPhong(PHONG tk, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("/Data/profileImg/"), fileName);
                    file.SaveAs(path);
                }
                var UpdatePhong = new Phong_Func();
                UpdatePhong.Update(tk);
                return RedirectToAction("DSPhong", "Admin");
            }
            return View(tk);
        }

        [HttpPost]
        public JsonResult Duyet(string maPhong)
        {
            var rs = new Phong_Func().DuyetPhong(maPhong);
            return Json(new
            {
                duyet = rs
            });
        }


        /*                                     SỬA                                        */

    }
}