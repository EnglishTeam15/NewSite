using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StartWebSiteEnglish.Models
{
    public class PageInfo
    {
        // Кол-во товаров
        public int TotalItems { get; set; }

        // Кол-во товаров на одной странице
        public int ItemsPerPage { get; set; }

        // Номер текущей страницы
        public int CurrentPage { get; set; }

        // Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }

    public class IndexViewModel<T>
    {
        public IEnumerable<T> Materials { get; set; }
        public PageInfo PageInfo { get; set; }
    }


    public class DropDowmModel
    {
        public List<SelectListItem> CountWord { get; set; }
        //public string Text { get; set; }
        public int Id { get; set; }
    }


}