using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.Function
{
    public class HoaDon_Func
    {
        private qlks_CNPMEntities db;
        public HoaDon_Func()
        {
            db = new qlks_CNPMEntities();
        }

        public string Insert(HOADON model)
        {
            db.HOADONs.Add(model);
            db.SaveChanges();
            return model.MAHD;
        }
    }
}
