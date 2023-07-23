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
        private qlks_CNPMEntities db;


        public Phong_Func()
        {
            db = new qlks_CNPMEntities();
        }

        public IQueryable<PHONG> PHONGs
        {
            get { return db.PHONGs; }
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
                            DADUYET = r.DADUYET
                        };
            listPhongView = query.ToList();
            return listPhongView;
        }

        




    }
}