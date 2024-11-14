using _23DH114201_My_Store.Models.ViewModel;
using _23DH114201_My_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace _23DH114201_My_Store.Controllers
{
    public class CartController : Controller
    {
        //private readonly ApplicationDbContext db = new ApplicationDbContext();
        private MyStoreeEntities db = new MyStoreeEntities();

        // Hàm lấy dịch vụ giỏ hàng
        private CartService GetCartService()
        {
            return new CartService(Session);
        }

        //Hiển thị giỏ hàng không gom nhóm theo danh mục
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }

        //Thêm sản phẩm vào giỏ
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                var cartService = GetCartService();
                cartService.GetCart().AddItem(product.ProductID, product.ProductImage,
                    product.ProductName, product.ProductPrice, quantity, product.Category.CategoryName);
            }
            return RedirectToAction("Index");
        }

        //Xóa sản phẩm khỏi giỏ
        public ActionResult RemoveFromCart(int id)
        {
            var cartService = GetCartService();
            cartService.GetCart().RemoveItem(id);
            return RedirectToAction("Index");
        }

        //Làm trống giỏ hàng
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var carService = GetCartService();
            carService.GetCart().UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }
    }
}