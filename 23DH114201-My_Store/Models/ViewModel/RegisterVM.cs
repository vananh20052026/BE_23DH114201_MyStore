using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _23DH114201_My_Store.Models.ViewModel
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = "Tên Đăng nhập")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không đúng")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Họ tên")]
        public string CustomerName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Địa chỉ Email")]
        public string CustomerEmail { get; set; }

        [Required]
        [Display(Name = "Địa chỉ nhà")]
        public string CustomerAddress { get; set; }
    }
}