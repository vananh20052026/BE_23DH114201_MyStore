using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _23DH114201_My_Store.Models.ViewModel
{
    public class CheckoutVM /*Lưu thông tin form Checkout*/
    {

        public List<CartItem> CarItems { get; set; }

        public int CustomerID { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        public System.DateTime OrderDate { get; set; }

        [Display(Name = "Tổng giá trị")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        public string PaymentStatus { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Địa chỉ giao hàng")]
        public string ShippingAddress { get; set; }

        public string Username { get; set; }

        // Các thuộc tính khác của đơn hàng
        public List<OrderDetail> OrderDetails { get; set; }
        public string DeliveryMethod { get; internal set; }
    }
}