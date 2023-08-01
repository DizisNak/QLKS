using QLKS_CNPM_LT.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.Function
{
    public class LoaiPhong_Func
    {
        private QLKS_CNPMEntities db;


        public LoaiPhong_Func()
        {
            db = new QLKS_CNPMEntities();
        }

        public IQueryable<LOAIPHONG> LOAIPHONGs
        {
            get { return db.LOAIPHONGs; }
        }

        public string Insert(LOAIPHONG model)
        {
            db.LOAIPHONGs.Add(model);
            db.SaveChanges();
            return model.MaLoai;
        }

        public string Update(LOAIPHONG model)
        {
            LOAIPHONG dbEntry = db.LOAIPHONGs.Find(model.MaLoai);
            if (dbEntry == null)
            {
                return null;
            }
            dbEntry.MaLoai = model.MaLoai;
            dbEntry.TenLoai = model.TenLoai;
            dbEntry.DuongDanAnh = model.DuongDanAnh;
            dbEntry.GhiChu = model.GhiChu;
            db.SaveChanges();
            return model.MaLoai;
        }


        public string Delete(string MaLoai)
        {
            LOAIPHONG dbEntry = db.LOAIPHONGs.Find(MaLoai);
            if (dbEntry == null)
            {
                return null;
            }
            db.LOAIPHONGs.Remove(dbEntry);
            db.SaveChanges();
            return MaLoai;
        }
    }
}
