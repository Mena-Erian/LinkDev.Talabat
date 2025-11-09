using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Common
{
    public class Pagination<T>
    {
        public Pagination()
        {

        }
        public Pagination(int pageIndex, int pageSize, int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; } // The Count of all Product Not Just the Size of Response  
        public required IEnumerable<T> Data { get; set; }
    }
}
