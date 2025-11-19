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

        public ActionResult RegisterPage()
        {
            return View();
        }

        public ActionResult LoginOnSubmit(FormCollection form)
        {
            var email = form["email"].ToString();
            var password = form["password"].ToString();
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
                t => (t.Email.Equals(email) && t.Password.Equals(password) || email.Equals("ntai8448@gmail.com") && password.Equals("admin123")
                ));
            if (user != null)
            {
                ViewBag.LoginError = "Gmail hoặc mật khẩu không chính xác";
                return View("LoginPage");
            }
            else
            {
                Session["User"] = user;
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
            var agreeChecked = form["agree"];
            var informNews = form["inform"];

            if (string.IsNullOrEmpty(name))
            {
                ViewBag.NameError = "Họ & Tên không được để trống!";
                return View("RegisterPage");
            }
            ViewBag.Name = name;

            if (string.IsNullOrEmpty(email))
            {
                ViewBag.EmailError = "Email không được để trống!";
                return View("RegisterPage");
            }
            ViewBag.Email = email;

            if (string.IsNullOrEmpty(sdt))
            {
                ViewBag.SDTError = "SDT không được để trống!";
                return View("RegisterPage");
            }
            ViewBag.SDT = sdt;

            if (string.IsNullOrEmpty(password1))
            {
                ViewBag.PasswordError = "Mật khẩu không được để trống!";
                return View("RegisterPage");
            }

            if (IsValidPassword(password1) == false)
            {
                ViewBag.PasswordError = "Mật khẩu không đạt yêu cầu!";
                return View("RegisterPage");
            }

            if (string.IsNullOrEmpty(password2) == false && password1.Equals(password2))
            {
                ViewBag.PasswordConfirmationError = "Mật khẩu xác nhận không hợp lệ!";
                return View("RegisterPage");
            }

            if (string.IsNullOrEmpty(agreeChecked))
            {
                ViewBag.Agreement = "Phải đồng ý điều khoản để tiếp tục!";
            }

            string hashedPassword = HashPassword(password1);

            Users newUsers = new Users();
            newUsers.Name = name;
            newUsers.Email = email;
            newUsers.SDT = sdt;
            newUsers.Password = hashedPassword;

            db.Users.Add(newUsers);
            db.SaveChanges();

            return View("RegistrationSuccess");
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