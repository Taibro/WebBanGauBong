using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGauBong.Areas.Admin.Security;
using WebBanGauBong.Models;

namespace WebBanGauBong.Areas.Admin.Controllers
{
    [CheckAthourize(Roles = "Admin")]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        QL_THU_BONG csdl = new QL_THU_BONG();
        public ActionResult OrderPage()
        {
            List<Orders> ds = csdl.Orders.ToList();
            return View(ds);
        }

        public ActionResult _ModalSuaOrder(int orderID)
        {
            Orders od = csdl.Orders.FirstOrDefault(t => t.OrderID == orderID);
            return PartialView(od);
        }

        public ActionResult UpdateOrderStatus(Orders order)
        {
            try
            {
                Orders od = csdl.Orders.FirstOrDefault(t => t.OrderID == order.OrderID);
                od.Status = order.Status;
                csdl.SaveChanges();

                TempData["UpdateOrderSuccess"] = $"Cập nhật đơn hàng thành công";

                return RedirectToAction("OrderPage");
            }
            catch(Exception e)
            {
                TempData["UpdateOrderError"] = $"Cập nhật đơn hàng thất bại \n {e.Message}";
                return RedirectToAction("OrderPage");
            }
        }
    }


}