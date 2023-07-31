using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.Function
{
    public class TaiKhoan_Func
    {

        private QLKS_CNPMEntities db;

        public TaiKhoan_Func()
        {
            db = new QLKS_CNPMEntities();
        }


        public IQueryable<TAIKHOAN> TaiKhoans
        {
            get { return db.TAIKHOANs; }
        }


        public string Insert(TAIKHOAN model)
        {
            db.TAIKHOANs.Add(model);
            db.SaveChanges();
            return model.ID_TK;
        }

        public string Update(TAIKHOAN model)
        {
            TAIKHOAN dbEntry = db.TAIKHOANs.Find(model.ID_TK);
            if (dbEntry == null)
            {
                return null;
            }
            dbEntry.ID_TK = model.ID_TK;
            dbEntry.TenTK = model.TenTK;
            dbEntry.PASS = model.PASS;
            dbEntry.ANH = model.ANH;
            dbEntry.SDT = model.SDT;
            dbEntry.Gmail = model.Gmail;
            dbEntry.LOAITK = model.LOAITK;
            db.SaveChanges();
            return model.ID_TK;
        }


        public string Delete(string ID_TK)
        {
            TAIKHOAN dbEntry = db.TAIKHOANs.Find(ID_TK);
            if (dbEntry == null)
            {
                return null;
            }
            db.TAIKHOANs.Remove(dbEntry);
            db.SaveChanges();
            return ID_TK;
        }

    }
}