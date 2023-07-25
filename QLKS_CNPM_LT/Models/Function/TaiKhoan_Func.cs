using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.Function
{
    public class TaiKhoan_Func
    {

        private qlks_CNPMEntities db;

        public TaiKhoan_Func()
        {
            db = new qlks_CNPMEntities();
        }


        public IQueryable<TAIKHOAN> TaiKhoans
        {
            get { return db.TAIKHOANs; }
        }


        public string Insert(TAIKHOAN model)
        {
            db.TAIKHOANs.Add(model);
            db.SaveChanges();
            return model.TenTK;
        }


    }
}