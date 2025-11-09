using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Products
{
    public class ProductSpecParams
    {
        private int maxPageSize = 100;
        private int pageSize;
        private string? search;

        public string? Sort { get; set; }
        public bool? IsDescending { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }


        public string? Search
        {
            get => search;
            set
            {
                search = value?.ToLower();
            }
        }

    }
}