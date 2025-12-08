using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGauBong.Models;
using System.Data.Entity;
using System.Data.SqlTypes;

namespace WebBanGauBong.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        QL_THU_BONG csdl = new QL_THU_BONG();
        public ActionResult CartPage()
        {
            ShoppingCart cart;
            if (Session["User"] != null)
            {
                Users user = Session["User"] as Users;
                cart = csdl.ShoppingCart.FirstOrDefault(t => t.UserID == user.UserID);
                Session["Cart"] = cart;
            }
            else
            {
                return RedirectToAction("LoginPage", "User");
            }
            return PartialView(cart);
        }

        [HttpPost]
        public ActionResult AddToCart(int productSizeID, int quantity)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LoginPage", "User");
            }

            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            ShoppingCartItem sp = csdl.ShoppingCartItem.FirstOrDefault(t => t.ProductSizeID == productSizeID && t.ShoppingCartID == cart.ShoppingCartID);
            try
            {
                if (sp == null)
                {
                    ProductSize pd = csdl.ProductSize.FirstOrDefault(t => t.ProductSizeID == productSizeID);
                    sp = new ShoppingCartItem
                    {
                        ShoppingCartID = cart.ShoppingCartID,
                        ProductSizeID = productSizeID,
                        Quantity = quantity,
                        UnitPrice = pd.Price
                    };

                    csdl.ShoppingCartItem.Add(sp);
                }
                else
                {
                    sp.Quantity += quantity;
                    //csdl.Entry(sp).State = EntityState.Modified;
                }
                csdl.SaveChanges();

                // Cập nhật lại session cart
                Session["Cart"] = csdl.ShoppingCart.FirstOrDefault(t => t.ShoppingCartID == cart.ShoppingCartID);

                // Cờ hiệu
                TempData["ShowCart"] = true;

                return RedirectToAction("Detail", "Product", new { id = sp.ProductSize.Product.ProductID });
            }
            catch (Exception e)
            {
                ViewBag.Error = $"Lỗi thêm không thành công\n {e.Message}";
                return RedirectToAction("Detail", "Product", new { id = sp.ProductSize.Product.ProductID });
            }
        }

        public ActionResult UpdateQuantity(int productSizeID, int type) // 1 tang, -1 giam
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            ShoppingCartItem sp = csdl.ShoppingCartItem.FirstOrDefault(t => t.ProductSizeID == productSizeID && t.ShoppingCartID == cart.ShoppingCartID);

            try
            {
                if (type == 1)
                {
                    sp.Quantity += 1;
                }
                else
                {
                    sp.Quantity -= 1;
                    if (sp.Quantity <= 0)
                    {
                        csdl.ShoppingCartItem.Remove(sp);
                    }
                }
                csdl.SaveChanges();

                // Cập nhật lại session cart
                Session["Cart"] = csdl.ShoppingCart.FirstOrDefault(t => t.ShoppingCartID == cart.ShoppingCartID);

                // Cờ hiệu
                TempData["ShowCart"] = true;

                return RedirectToAction("Detail", "Product", new { id = sp.ProductSize.Product.ProductID });
            }
            catch (Exception e)
            {
                ViewBag.Error = $"Lỗi thay đổi không thành công\n {e.Message}";
                return RedirectToAction("Index", "Product");
            }
        }

        public ActionResult DeleteProductInCart(int productSizeID)
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            ShoppingCartItem sp = csdl.ShoppingCartItem.FirstOrDefault(t => t.ProductSizeID == productSizeID && t.ShoppingCartID == cart.ShoppingCartID);

            try
            {

                csdl.ShoppingCartItem.Remove(sp);

                csdl.SaveChanges();

                // Cập nhật lại session cart
                Session["Cart"] = csdl.ShoppingCart.FirstOrDefault(t => t.ShoppingCartID == cart.ShoppingCartID);

                // Cờ hiệu
                TempData["ShowCart"] = true;

                return RedirectToAction("Index", "Product");
            }
            catch (Exception e)
            {
                ViewBag.Error = $"Lỗi xóa không thành công\n {e.Message}";
                return RedirectToAction("Index", "Product");
            }
        }




    }
}