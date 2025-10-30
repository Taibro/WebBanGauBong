using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGauBong.Models;

namespace WebBanGauBong.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        QL_THU_BONG csdl = new QL_THU_BONG();
        public ActionResult Index()
        {
            return View(csdl.Product.ToList());
        }

        public ActionResult HienThiMenu()
        {
            ViewBag.KichThuocNut = new List<int> { 30, 32, 33, 35, 37, 38, 40, 42, 43, 45, 47, 48, 50, 52, 53, 54, 55, 57, 58, 60, 63, 65, 68, 70, 72, 75, 78, 80, 85, 90, 95, 100, 105, 110, 115, 115, 120, 125, 125, 130, 135, 140, 150, 160, 165, 170, 175, 180, 200 };
            return PartialView(csdl.Category.ToList());
        }

        [HttpPost]
        public ActionResult TimKiemTheoLoai(FormCollection form)
        {
            List<Product> sanPhamHienTai = Session["DanhSachSanPhamHienTai"] != null ? Session["DanhSachSanPhamHienTai"] as List<Product> : csdl.Product.ToList();
            List<Product> sanPhamTheoLoai = null;
            List<Product> sanPhamTheoGia = new List<Product>();
            List<Product> sanPhamTheoSize = new List<Product>();

            List<int> dsSize = Session["Sizes"] != null ? Session["Sizes"] as List<int> : new List<int>();

            var idLoai = form["loai"];
            var minGia = int.Parse(form["min-value"]);
            var maxGia = int.Parse(form["max-value"]);
            var kichThuoc = form["size"] != null ? int.Parse(form["size"]) : 0;
            if (kichThuoc != 0)
            {
                if (dsSize.Contains(kichThuoc)) dsSize.Remove(kichThuoc);
                else dsSize.Add(kichThuoc);
            }

            TempData["GiaMax"] = maxGia;
            TempData["GiaMin"] = minGia;

            Session["Sizes"] = dsSize;

            // Lọc theo giá
            foreach (Product sp in sanPhamHienTai)
            {
                foreach (var size in sp.ProductSize.ToList())
                {
                    if (size.Price >= minGia && size.Price <= maxGia)
                    {
                        sanPhamTheoGia.Add(sp);
                        break;
                    }

                }
            }

            // Lọc theo size
            foreach (Product sp in sanPhamHienTai)
            {
                foreach (ProductSize size in sp.ProductSize.ToList())
                {
                    if (dsSize.Contains((int)size.SizeName))
                    {
                        sanPhamTheoSize.Add(sp);
                        break;
                    }

                }
            }
            // Lọc theo loại
            if (idLoai != null)
            {
                List<string> dsLoai = idLoai.Split(',').ToList();
                TempData["CheckedLoai"] = dsLoai;
                sanPhamTheoLoai = csdl.Product.ToList().FindAll(t => dsLoai.Contains(t.CategoryID.ToString()));

                if (Session["DanhSachSanPhamHienTai"] != null)
                {
                    return View("Index", sanPhamTheoLoai);
                }
            }

            else
            {
                sanPhamTheoLoai = sanPhamHienTai;
            }

            List<Product> finals = null;
            if (sanPhamTheoSize.Count > 0)
                finals = sanPhamTheoLoai.Intersect(sanPhamTheoGia).Intersect(sanPhamTheoSize).ToList();
            else
            {
                finals = sanPhamTheoLoai.Intersect(sanPhamTheoGia).ToList();
            }

            return View("Index", finals);
        }

        public ActionResult TimKiemTheoTen(string name)
        {
            List<Product> ds = csdl.Product.ToList().FindAll(sp => sp.ProductName.ToLower().Trim().Contains(name.ToLower().Trim()));
            Session["DanhSachSanPhamHienTai"] = ds;

            return View("Index", ds);
        }

        public ActionResult TimKiemTheoSize(int min, int max)
        {
            List<Product> dsSP = new List<Product>();
            List<int> buttonSizeClicked = new List<int>();
            foreach (var item in csdl.Product.ToList())
            {
                foreach (var size in item.ProductSize.ToList())
                {
                    if (size.SizeName >= min && size.SizeName <= max)
                    {
                        dsSP.Add(item);
                        buttonSizeClicked.Add((int)size.SizeName);
                    }
                }
            }
            Session["DanhSachSanPhamHienTai"] = dsSP;
            Session["Sizes"] = buttonSizeClicked;
            return View("Index", dsSP);
        }

        public ActionResult TimKiemTheoGia(int min, int max)
        {
            TempData["GiaMax"] = max;
            TempData["GiaMin"] = min;

            List<Product> ds = new List<Product>();
            foreach (var item in csdl.Product.ToList())
            {
                foreach (var s in item.ProductSize.ToList())
                {
                    if (s.Price >= min && s.Price <= max)
                    {
                        ds.Add(item);
                        break;
                    }
                }
            }
            Session["DanhSachSanPhamHienTai"] = ds;
            return View("Index", ds);
        }


        public ActionResult Detail(string id)
        {
            Product sp = csdl.Product.ToList().Find(t => t.ProductID.ToLower().Trim() == id.ToLower().Trim());
            
            ViewBag.ListHinhAnhPhu = sp.ProductImages.ToList();
            return View(sp);
        }

    }
}