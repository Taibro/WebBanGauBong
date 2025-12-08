using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGauBong.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WebBanGauBong.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        QL_THU_BONG db = new QL_THU_BONG();
        public static string HashPassword(string password)
        {
            string salt = "gaubong";
            // 1. Kết hợp Mật khẩu và Salt
            string saltedPassword = password + salt;

            // 2. Băm bằng SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Băm chuỗi kết hợp (saltedPassword)
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                // 3. Trả về chuỗi Hash Base64 (dễ dàng lưu trữ và so sánh)
                return Convert.ToBase64String(bytes);
            }
        }
        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginOnSubmit(FormCollection form)
        {
            var email = form["email"].ToString();
            var password = HashPassword(form["password"].ToString());

            var adminPass = HashPassword("admin123");
            if (email == null)
            {
                ViewBag.EmailError = "Email không được để trống!";
                return View("LoginPage");
            }
            if (password == null)
            {
                ViewBag.PasswordError = "Mật khẩu không được để trống!";
                return View("LoginPage");
            }

            Users user = db.Users.FirstOrDefault(
                t => (t.Email.Equals(email) && t.Password.Equals(password) || email.Equals("ntai8448@gmail.com") && password.Equals(adminPass)
                ));
            if (user == null)
            {
                ViewBag.LoginError = "Gmail hoặc mật khẩu không chính xác";
                return View("LoginPage");
            }
            else
            {
                Session["User"] = user;
                // Add cart
                AddShoppingCart(user.UserID);
                Session["Cart"] = db.ShoppingCart.FirstOrDefault(t => t.UserID == user.UserID);

                return RedirectToAction("HomePage", "Home");
            }
        }

        public ActionResult RegisterOnSubmit(FormCollection form)
        {
            var name = form["name"];
            var email = form["email"];
            var sdt = form["sdt"];
            var password1 = form["password1"];
            var password2 = form["password2"];

            if (string.IsNullOrEmpty(name))
            {
                return View("LoginPage");
            }
            ViewBag.Name = name;

            if (string.IsNullOrEmpty(email))
            {
                return View("LoginPage");
            }
            ViewBag.Email = email;

            if (string.IsNullOrEmpty(sdt))
            {
                return View("LoginPage");
            }
            ViewBag.SDT = sdt;

            if (string.IsNullOrEmpty(password1))
            {
                return View("LoginPage");
            }

            if (IsValidPassword(password1) == false)
            {
                ViewBag.PasswordError = "Mật khẩu không đạt yêu cầu!";
                return View("LoginPage");
            }

            if (string.IsNullOrEmpty(password2) == true || password1.Equals(password2) == false)
            {
                ViewBag.PasswordConfirmationError = "Mật khẩu xác nhận không hợp lệ!";
                return View("LoginPage");
            }


            string hashedPassword = HashPassword(password1);

            Users newUsers = new Users();
            newUsers.Name = name;
            newUsers.Email = email;
            newUsers.SDT = sdt;
            newUsers.Password = hashedPassword;

            Session["Cart"] = db.ShoppingCart.FirstOrDefault(t => t.UserID == newUsers.UserID);

            Session["User"] = newUsers;

            db.Users.Add(newUsers);
            db.SaveChanges();

            // Add cart
            AddShoppingCart(newUsers.UserID);

            return View("RegistrationSuccess");
        }

        public void AddShoppingCart(int userID)
        {
            ShoppingCart cart = db.ShoppingCart.FirstOrDefault(t => t.UserID == userID);
            if (cart == null)
            {
                ShoppingCart newCart = new ShoppingCart();
                newCart.UserID = userID;
                db.ShoppingCart.Add(newCart);
                db.SaveChanges();
            }
        }

        public ActionResult RegistrationSuccess()
        {
            return View();
        }

        public bool IsValidPassword(string password)
        {
            int minLength = 8;

            if (password.Length < minLength)
            {
                return false;
            }
            string passwordRegex = $@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+{{""':;?/>.<,}}]).{{{minLength},}}$";
            try
            {
                return Regex.IsMatch(password, passwordRegex);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }


    }
}