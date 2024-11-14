using _23DH114201_My_Store.Models.ViewModel;
using _23DH114201_My_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _23DH114201_My_Store.Controllers
{
    public class OrderController : Controller
    {
        private MyStoreeEntities db = new MyStoreeEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        //GET: Order/Checkout
        [Authorize]
        public ActionResult Checkout()
        {
            //Kiểm tra giỏ hàng trong session
            //nếu giỏ hàng rỗng hoặc không có sản phẩm thì chuyển hướng về trang chủ
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "CustomerHome");
            }
            //Xác thực người dùng đã đăng nhập chưa, nếu chưa thì chuyển hướng tới trang Đăng nhập
            var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            if (user == null) { return RedirectToAction("Login", "Account"); }
            //Lấy thông tin khách hàng từ CSDL, nếu chưa có thì chuyển hướng tới trang Đăng nhập
            //nếu có rồi thì lấy địa chỉ của khách hàng và gán vào ShippingAddress của CheckoutVM
            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null) { return RedirectToAction("Login", "Account"); }

            var model = new CheckoutVM
            {//Tạo dữ liệu hiển thị cho CheckOutVM
                CarItems = cart, //Lấy danh sách các sản phẩm trong giỏ hàng
                TotalAmount = cart.Sum(item => item.TotalPrice),//Tổng giá trị các mặt hàng trong giỏ
                OrderDate = DateTime.Now, //Mắc định lấy thời điểm đặt hàng
                ShippingAddress = customer.CustomerAddress, //Lấy địa chỉ mặt định từ bảng Customer
                CustomerID = customer.CustomerID, // lấy mẫ khách hàng từ bảng customer
                Username = customer.Username // Lấy tên đăng nhập từ bảng Customer};
            };
            return View(model);
        }

        //Post: order//Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var cart = Session["Cart"] as List<CartItem>;
                //Nếu giỏ hàng rỗng sẽ điều hướng tới trang Home 
                if (cart == null || !cart.Any()) { return RedirectToAction("Index", "Home"); }
                //Nếu người dùng chưa đăng nhập sẽ điều hướng tới trang Login
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if (user == null) { return RedirectToAction("Login", "Account"); }
                //Nếu khách hàng không khớp với tên đăng nhập, sẽ điều hướng tới trang Login
                var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
                if (customer == null) { return RedirectToAction("Login", "Account"); }
                //Nếu người dùng chọn thanh toán bằng Paypal, sẽ điều hướng tới trang PaymentWithPaypal
                if (model.PaymentMethod == "Paypal")
                {
                    return RedirectToAction("PaymentWithPaypal", "PayPal", model);
                }
                //thiêt lập paymentstatus dựa trên paymentmethod
                string paymentStatus = "Chưa thanh toán";
                switch (model.PaymentMethod)
                {
                    case "Tiền mặt": paymentStatus = "Thanh toán tiền mặt"; break;
                    case "Paypal": paymentStatus = "Thanh toán paypal"; break;
                    case "Mua trước trả sau": paymentStatus = "Trả góp"; break;
                    default: paymentStatus = "Chưa thanh toán"; break;
                }
                //tạo đơn hàng và chi tiết đơn hàng liên quan
                var order = new Order
                {
                    CustomerID = customer.CustomerID,
                    OrderDate = model.OrderDate,
                    TotalAmount = model.TotalAmount,
                    PaymentStatus = paymentStatus,
                    PaymentMethod = model.PaymentMethod,
                    DeliveryMethod = model.DeliveryMethod,
                    ShippingAddress = model.ShippingAddress,
                    OrderDetails = cart.Select(item => new OrderDetail
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice,
                    }).ToList()
                };
                //Lưu đơn hàng vào CSDL
                db.Orders.Add(order);
                db.SaveChanges();
                //Xóa giỏ hàng sau khi đặt hàng thành công
                Session["Cart"] = null;
                //Điều hướng tới trang xác nhận đơn hàng
                return RedirectToAction("OrderSuccess", new { id = order.OrderID });
            }
            return View(model);
        }
    }
}