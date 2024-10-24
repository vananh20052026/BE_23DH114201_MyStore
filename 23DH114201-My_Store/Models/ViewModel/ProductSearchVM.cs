using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace _23DH114201_My_Store.Models.ViewModel
{
    public class ProductSearchVM
    {
        //tiêu chí để search theo tên, mô tả sản phẩm
        //hoặc loại sản phẩm
        public string SearchTerm { get; set; }

        //các tiêu chí để search theo giá
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        //Thứ tự sắp xếp
        public string SortOrder { get; set; }

        //Các thuộc tính hỗ trợ phân trang 
        public int PageNumber { get; set; } //Trang hiện tại
        public int PageSize { get; set; } = 10; //Số sản phẩm của mỗi trang

        //danh sách các sản phẩm thỏa điều kiện tìm kiếm
        //public List<Product> Products  { get; set; }

        //danh sách sản phẩm phân trang
        public PagedList.IPagedList<Product> Products { get; set; }
    }
}