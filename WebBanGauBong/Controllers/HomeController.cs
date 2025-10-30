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
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult HienThiSanPham(string name, string image)
        {
            name = name.Trim().ToLower();
            ViewBag.Ten = name.ToUpper();
            ViewBag.Anh = image;

            bool loai = csdl.Category.Select(t => t.CategoryName.ToLower()).ToList().Contains(name);
            if (loai == true)
            {
                string cateID = csdl.Category.ToList().Find(t => t.CategoryName.ToLower().Contains(name)).CategoryID;
                return PartialView(csdl.Product.ToList().FindAll(t => t.CategoryID.Contains(cateID)));
            }    

            return PartialView(csdl.Product.ToList().FindAll(t => t.ProductName.ToLower().Contains(name)));
        }

    }
}