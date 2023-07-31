using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.Function
{
    public class HoaDon_Func
    {
        private QLKS_CNPMEntities db;
        public HoaDon_Func()
        {
            db = new QLKS_CNPMEntities();
        }

        public IQueryable<HOADON> HOADONs
        {
            get { return db.HOADONs; }
        }

        public string Insert(HOADON model)
        {
            db.HOADONs.Add(model);
            db.SaveChanges();
            return model.MAHD;
        }

        public string Delete(string MaDatPhong)
        {
            HOADON dbEntry = db.HOADONs.Find(MaDatPhong);
            if (dbEntry == null)
            {
                return null;
            }
            db.HOADONs.Remove(dbEntry);
            db.SaveChanges();
            return MaDatPhong;
        }
    }
}
