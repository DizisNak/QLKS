using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKS_CNPM_LT.Models.Function
{
    public class LoaiTaiKhoan_Func
    {
        private QLKS_CNPMEntities db;

        public LoaiTaiKhoan_Func()
        {
            db = new QLKS_CNPMEntities();
        }


        public IQueryable<LOAITAIKHOAN> TaiKhoans
        {
            get { return db.LOAITAIKHOANs; }
        }


        public string Insert(LOAITAIKHOAN model)
        {
            db.LOAITAIKHOANs.Add(model);
            db.SaveChanges();
            return model.ID_LOAITK;
        }

        public string Update(LOAITAIKHOAN model)
        {
            LOAITAIKHOAN dbEntry = db.LOAITAIKHOANs.Find(model.ID_LOAITK);
            if (dbEntry == null)
            {
                return null;
            }
            dbEntry.ID_LOAITK = model.ID_LOAITK;
            dbEntry.TENLOAITK = model.TENLOAITK;
            db.SaveChanges();
            return model.ID_LOAITK;
        }


        public string Delete(string ID_TK)
        {
            LOAITAIKHOAN dbEntry = db.LOAITAIKHOANs.Find(ID_TK);
            if (dbEntry == null)
            {
                return null;
            }
            db.LOAITAIKHOANs.Remove(dbEntry);
            db.SaveChanges();
            return ID_TK;
        }
    }
}