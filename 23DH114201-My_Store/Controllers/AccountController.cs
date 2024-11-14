using _23DH114201_My_Store.Models.ViewModel;
using _23DH114201_My_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Policy;
using System.Security.Cryptography;
using System.Runtime.Remoting.Messaging;
using System.Web.Security;

namespace _23DH114201_My_Store.Controllers
{
    public class AccountController : Controller
    {
        MyStoreeEntities db = new MyStoreeEntities();
        // GET: Account/Register

        public ActionResult Register()
        {
            return View();

        }
        // POST: Account/Register

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                    return View(model);
                }
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "Customer"
                };
                db.Users.Add(user);
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,
                };
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // POST: Account/Login
        public ActionResult Login()
        {
            return View();

        }
        // POST: Account/Register

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username
                    && u.Password == model.Password
                    && u.UserRole == "Customer");
                if (user != null)
                {
                    //Lưu trạng thái đăng nhập vào session
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;

                    //Lưu thông tin xác thực người dùng vào cookie
                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    return RedirectToActionPermanent("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }
        
        //GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        //GET: Account/Profile
        [Authorize]
        public ActionResult ProfileInfo()
        {
            var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null)
            {
                return RedirectToAction("Index", "CustomerHome");
            }

            var model = new RegisterVM
            {
                Username = user.Username,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                CustomerEmail = customer.CustomerEmail,
                CustomerAddress = customer.CustomerAddress,
            };

            return View(model);
        }

        //POST: Account/Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ProfileInfo(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
                if (customer == null)
                {
                    return RedirectToAction("Index", "CustomerHome");
                }

                customer.CustomerName = model.CustomerName;
                customer.CustomerPhone = model.CustomerPhone;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CustomerAddress = model.CustomerAddress;

                db.SaveChanges();

                return RedirectToAction("Profile");
            }
            return View(model);
        }

        //GET: Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //POST: Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                user.Password = model.Password; // Lưu ý: Nên mã khóa mật khẩu trước khi lưu
                db.SaveChanges();

                return RedirectToAction("ProfileInfo");
            }

            return View(model);
        }

        //GET: Account/UpdateAccount/5
        public ActionResult UpdateAccount(int id)
        {
            return View();
        }

        //POST: Account/UpdateAccount/5
        [HttpPost]
        public ActionResult UpdateAccount(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: Account/ChangePassword/5
        public ActionResult ChangePassword(int id)
        {
            return View();
        }

        //POST: Account/ChangePassword/5
        [HttpPost]
        public ActionResult ChangePassword(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}