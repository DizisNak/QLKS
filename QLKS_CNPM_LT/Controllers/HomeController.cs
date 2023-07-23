using QLKS_CNPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKS_CNPM_LT.Controllers
{
    public class HomeController : Controller
    {
        private qlks_CNPMEntities db = new qlks_CNPMEntities();
        public ActionResult Home()
        {
            List<LOAIPHONG> list = db.LOAIPHONGs.ToList();
            return View(list);
        }

            
    }
}