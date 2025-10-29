using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Entities.Products.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Specifications
{
    internal class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, string>
    {
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            AddBrandAndCategoryIncludes();
        }

        public ProductWithBrandAndCategorySpecifications(string id) : base(id)
        {
            AddBrandAndCategoryIncludes();
        }

        private void AddBrandAndCategoryIncludes()
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Category);
        }
    }
}
