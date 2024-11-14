using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _23DH114201_My_Store.Models.ViewModel
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Tên Đăng nhập")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}