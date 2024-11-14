using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace _23DH114201_My_Store.Models.ViewModel
{
    public class HomeProductVM
    {
        //tiêu chí để search theo tên, mô tả sp
        //hoặc loại sản phẩm
        public string SearchTerm { get; set; }

        // Các thuộc tính hỗ trợ phân trang
        public string PageNumber { get; set; } // Trang hiện tại
        public string PageSize { get; set; }  // Số sản phẩm mỗi trang

        //danh sách sản phẩm nổi bật
        public List<Product> FeatureProducts { get; set; }

        // Danh sách Sản phẩm mới đã phân trang
        public PagedList.IPagedList<Product> NewProducts { get; set;}
    }
}