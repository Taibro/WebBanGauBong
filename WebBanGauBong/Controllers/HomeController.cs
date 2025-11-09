using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGauBong.Models;

namespace WebBanGauBong.Controllers
{
    public class HomeController : Controller
    {
        QL_THU_BONG csdl = new QL_THU_BONG();
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HienThiSanPham(string id, string image)
        {
            //ViewBag.Ten = id.ToUpper();
            ViewBag.Anh = image;

            Category loai = csdl.Category.FirstOrDefault(t => t.CategoryID.Equals(id));
            if (loai != null)
            {
                ViewBag.Ten = loai.CategoryName.ToUpper();
                return PartialView(loai.Product.ToList());
            }    

            return PartialView(csdl.Product.ToList());
        }

        public ActionResult CardSanPham(int id)
        {
            Product sp = csdl.Product.First(t => t.ProductID == id);
            return PartialView(sp);
        }
    }
}