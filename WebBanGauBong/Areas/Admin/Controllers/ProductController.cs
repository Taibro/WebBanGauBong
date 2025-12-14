using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebBanGauBong.Areas.Admin.Security;
using WebBanGauBong.Models;

namespace WebBanGauBong.Areas.Admin.Controllers
{
    [CheckAthourize(Roles = "Admin")]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        QL_THU_BONG csdl = new QL_THU_BONG();
        public ActionResult ProductPage()
        {
            List<Product> ds = csdl.Product.ToList();
            return View(ds);
        }

        public ActionResult _ModalThemSanPham()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult TimKiemSanPham(string keyword, string status)
        {
            int IsEnabled = int.Parse(status);
            List<Product> ds = null;
            if (IsEnabled == 2)
            {
                ds = csdl.Product.ToList().FindAll(t => t.ProductName.ToLower().Trim().Contains(keyword.ToLower().Trim()) || t.ProductID.ToString().Equals(keyword));
            }
            else
            {
                ds = csdl.Product.ToList().FindAll(t => (t.ProductName.ToLower().Trim().Contains(keyword.ToLower().Trim()) || t.ProductID.ToString().Equals(keyword)) && t.Isenabled == IsEnabled);
            }

            return View("ProductPage", ds);
        }

        public ActionResult DisableSanPham(int productID, int type)
        {
            try
            {
                // Disable san pham
                Product product = csdl.Product.FirstOrDefault(t => t.ProductID == productID);
                product.Isenabled = type;

                // Xoa san pham co trong gio hang
                List<ShoppingCartItem> cartItems = csdl.ShoppingCartItem.ToList().FindAll(t => t.ProductSize.ProductID == productID);
                csdl.ShoppingCartItem.RemoveRange(cartItems);

                // Cancel san pham 
                List<Orders> lsOrder = csdl.Orders.ToList().FindAll(t => t.Status.Equals("Đang chờ xác nhận") && t.OrderDetail.Any(sp => sp.ProductSize.ProductID == productID));
                foreach (var item in lsOrder)
                {
                    Orders od = csdl.Orders.FirstOrDefault(t => t.OrderID == item.OrderID);
                    od.Status = "Đã hủy";
                }

                csdl.SaveChanges();

                TempData["DisableSuccess"] = $"Disable sản phẩm mã {productID} thành công";
                return RedirectToAction("ProductPage");
            }
            catch (Exception e)
            {
                TempData["DisableError"] = $"Disable sản phẩm mã {productID} không thành công \n {e.Message}";
                return RedirectToAction("ProductPage");
            }
        }

        public ActionResult DeleteSanPham(int productID)
        {
            try
            {
                // Kiem tra don hang ton tai san pham
                List<Orders> lsOrder = csdl.Orders.ToList().FindAll(t => (t.OrderDetail.Any(sp => sp.ProductSize.ProductID == productID)));
                if (lsOrder.Count() == 0)
                {
                    // Xoa discount cua san pham
                    List<Discount> lsDiscount = csdl.Discount.ToList().FindAll(t => t.ProductID == productID);
                    csdl.Discount.RemoveRange(lsDiscount);

                    // Xoa image cua san pham
                    List<ProductImages> lsProductImage = csdl.ProductImages.ToList().FindAll(t => t.ProductID == productID);
                    csdl.ProductImages.RemoveRange(lsProductImage);

                    // Xoa rating cua san pham
                    List<Rating> lsRating = csdl.Rating.ToList().FindAll(t => t.ProductID == productID);
                    csdl.Rating.RemoveRange(lsRating);

                    // Xoa san pham co trong gio hang
                    List<ShoppingCartItem> cartItems = csdl.ShoppingCartItem.ToList().FindAll(t => t.ProductSize.ProductID == productID);
                    csdl.ShoppingCartItem.RemoveRange(cartItems);

                    // Xoa size san pham
                    List<ProductSize> lsSize = csdl.ProductSize.ToList().FindAll(t => t.ProductID == productID);
                    csdl.ProductSize.RemoveRange(lsSize);

                    // Xoa san pham
                    Product product = csdl.Product.FirstOrDefault(t => t.ProductID == productID);
                    csdl.Product.Remove(product);

                    csdl.SaveChanges();
                }

                TempData["DeleteSuccess"] = $"Không thể sản phẩm mã {productID} do có đơn hàng";
                return RedirectToAction("ProductPage");
            }
            catch (Exception e)
            {
                TempData["DeleteError"] = $"Xóa sản phẩm mã {productID} không thành công \n {e.Message}";
                return RedirectToAction("ProductPage");
            }
        }

        public ActionResult _ModalSuaSanPham(int productID)
        {
            Product pd = csdl.Product.FirstOrDefault(t => t.ProductID == productID);
            return PartialView(pd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductOnSubmit(Product product, IEnumerable<HttpPostedFileBase> images)
        {
            using (var transaction = csdl.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        csdl.Product.Add(product);
                        csdl.SaveChanges();

                        string FileName = "";
                        string path = "";
                        string dir = "/Content/Images/";
                        string physicalDir = Server.MapPath(dir);
                        if (Directory.Exists(physicalDir) == false)
                        {
                            Directory.CreateDirectory(physicalDir);
                        }
                        if (images != null && images.Any())
                        {
                            foreach (var file in images)
                            {
                                if (file != null && file.ContentLength > 0)
                                {
                                    FileName = Path.GetFileName(file.FileName);

                                    path = Path.Combine(physicalDir, FileName);
                                    file.SaveAs(path);

                                    ProductImages pi = new ProductImages();
                                    pi.ProductID = product.ProductID;
                                    pi.ImageURL = FileName;
                                    csdl.ProductImages.Add(pi);
                                }
                            }
                            csdl.SaveChanges();

                            transaction.Commit();
                        }

                    }
                    TempData["AddSuccess"] = "Thêm sản phẩm thành công";
                    return RedirectToAction("ProductPage");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["AddError"] = $"Thêm sản phẩm thất bại {e.Message}";
                    TempData["ShowModalAddProduct"] = true;
                    return RedirectToAction("ProductPage");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSanPhamOnSubmit(Product product, IEnumerable<HttpPostedFileBase> images)
        {
            using (var transaction = csdl.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Product updateProduct = csdl.Product.FirstOrDefault(t => t.ProductID == product.ProductID);
                        bool productEqual = product.ProductName == updateProduct.ProductName && product.Isenabled == updateProduct.Isenabled;
                        if (productEqual == false)
                        {
                            updateProduct.ProductName = product.ProductName;
                            updateProduct.Isenabled = product.Isenabled;
                            csdl.SaveChanges();
                        }

                        // Xoa anh cu
                        List<ProductImages> lsProductImg = csdl.ProductImages.ToList().FindAll(t => t.ProductID == product.ProductID);

                        if (images.Count() != 1)
                        {
                            csdl.ProductImages.RemoveRange(lsProductImg);
                            csdl.SaveChanges();

                            // Them anh moi
                            string FileName = "";
                            string path = "";
                            string dir = "/Content/Images/";
                            string physicalDir = Server.MapPath(dir);
                            if (Directory.Exists(physicalDir) == false)
                            {
                                Directory.CreateDirectory(physicalDir);
                            }

                            foreach (var file in images)
                            {
                                if (file != null && file.ContentLength > 0)
                                {
                                    FileName = Path.GetFileName(file.FileName);

                                    path = Path.Combine(physicalDir, FileName);
                                    file.SaveAs(path);

                                    ProductImages pi = new ProductImages();
                                    pi.ProductID = product.ProductID;
                                    pi.ImageURL = FileName;
                                    csdl.ProductImages.Add(pi);
                                }
                            }
                            csdl.SaveChanges();

                        }

                        // Xoa size cu
                        var oldSizes = csdl.ProductSize.Where(t => t.ProductID == product.ProductID).ToList();
                        if (oldSizes.Count > 0)
                        {
                            csdl.ProductSize.RemoveRange(oldSizes);
                            csdl.SaveChanges();
                        }

                        if (product.ProductSize != null && product.ProductSize.Count > 0)
                        {
                            foreach (var item in product.ProductSize)
                            {
                                ProductSize ps = new ProductSize();
                                ps.ProductID = product.ProductID;
                                ps.SizeName = item.SizeName;
                                ps.Price = item.Price;
                                ps.StockQuantity = item.StockQuantity;

                                csdl.ProductSize.Add(ps);

                            }
                            csdl.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    TempData["UpdateSuccess"] = "Cập nhật sản phẩm thành công";
                    return RedirectToAction("ProductPage");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["UpdateError"] = $"Cập nhật sản phẩm thất bại {e.Message}";
                    TempData["ShowModalUpdateProduct"] = true;
                    return RedirectToAction("ProductPage");
                }
            }
        }


    }
}