using QLKS_CNPM_LT.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace QLKS_CNPM_LT.Models.Function
{
    public class Phong_Func
    {
        private QLKS_CNPMEntities db;


        public Phong_Func()
        {
            db = new QLKS_CNPMEntities();
        }

        public IQueryable<PHONG> PHONGs
        {
            get { return db.PHONGs; }
        }
        public string Insert(PHONG model)
        {
            db.PHONGs.Add(model);
            db.SaveChanges();
            return model.MA_PHONG;
        }

        public string UpdateTrangThai(PHONG model)
        {
            PHONG dbEntry = db.PHONGs.Find(model.MA_PHONG);
            if (dbEntry == null)
            {
                return null;
            }
            dbEntry.DaDuyet = model.DaDuyet;
            db.SaveChanges();
            return model.MA_PHONG;
        }

        public string Update(PHONG model)
        {
            PHONG dbEntry = db.PHONGs.Find(model.MA_PHONG);
            if (dbEntry == null)
            {
                return null;
            }
            dbEntry.MA_PHONG = model.MA_PHONG;
            dbEntry.TENPhong = model.TENPhong;
            dbEntry.MaLoai = model.MaLoai;
            dbEntry.GIA = model.GIA;
            dbEntry.DiaChi = model.DiaChi;
            dbEntry.ID_TK = model.ID_TK;
            dbEntry.TRANGTHAI = model.TRANGTHAI;
            dbEntry.NoiDung = model.NoiDung;
            dbEntry.DaDuyet = model.DaDuyet;

            db.SaveChanges();
            return model.MA_PHONG;
        }

        public string Delete(string MaPhong)
        {
            PHONG dbEntry = db.PHONGs.Find(MaPhong);
            if (dbEntry == null)
            {
                return null;
            }
            db.PHONGs.Remove(dbEntry);
            db.SaveChanges();
            return MaPhong;
        }

        public byte? DuyetPhong(string MaPhong)
        {
            PHONG dbEntry = db.PHONGs.Find(MaPhong);
            if(dbEntry.DaDuyet == 1)
            {
                dbEntry.DaDuyet = 0;
            }
            else
            {
                dbEntry.DaDuyet = 1;
            }
            db.SaveChanges();
            return dbEntry.DaDuyet;
        }


        public List<PhongView> toanBoPhong()
        {
            List<PhongView> listPhongView;
            var query = from s in db.LOAIPHONGs
                        join r in db.PHONGs on s.MaLoai equals r.MaLoai
                        select new PhongView
                        {
                            MA_PHONG = r.MA_PHONG,
                            TENPhong = r.TENPhong,
                            TRANGTHAI = r.TRANGTHAI,
                            GIA = r.GIA,
                            MALOAI = r.MaLoai,
                            ANH = r.ANH,
                            DaDuyet = r.DaDuyet
                        };
            listPhongView = query.ToList();
            return listPhongView;
        }
    }
}