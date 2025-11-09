using LinkDev.Talabat.Application.Abstraction.Models.Products;
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
    internal sealed class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, string>
    {
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            AddBrandAndCategoryIncludes();
        }

        public ProductWithBrandAndCategorySpecifications(string id) : base(p => p.Id == id)
        {
            AddBrandAndCategoryIncludes();
        }

        public ProductWithBrandAndCategorySpecifications(int? brandId, int? categoryId)
            : base(
                   p =>
                    (!brandId.HasValue || p.BrandId == brandId.Value)
                    &&
                    (!categoryId.HasValue || p.CategoryId == categoryId.Value)
                 )
        {
        }

        public ProductWithBrandAndCategorySpecifications(string? sort, bool? isDescending, int? brandId, int? categoryId, int pageIndex, int pageSize) : base(
                p =>
                (!brandId.HasValue || p.BrandId == brandId.Value)
                &&
                (!categoryId.HasValue || p.CategoryId == categoryId.Value)
            )
        {
            AddBrandAndCategoryIncludes();

            if (sort is not null)
            {
                AddSorting(sort, isDescending ?? default);
            }

            AddPagination((pageSize * (pageIndex - 1)), pageSize);
        }

        public int BrandId { get; }
        public int CategoryId { get; }

        #region Helper Methods

        private protected override void AddSorting(string sort, bool isDescending)
        {
            //base.AddSorting(sort, IsAscending); // I Don't Need to Call the base
            if (!isDescending)
            {
                switch (sort/*.ToLower()*/) // Be Checked Later
                {
                    case (nameof(ProductToReturnDto.Name)):
                        AddOrderByAsc(p => p.Name);
                        break;
                    case nameof(ProductToReturnDto.Price):
                        AddOrderByAsc(p => p.Price);
                        break;
                    case nameof(ProductToReturnDto.Id):
                        AddOrderByAsc(p => p.Id);
                        break;
                    case nameof(ProductToReturnDto.BrandId):
                        AddOrderByAsc(p => p.BrandId ?? default);
                        break;
                    case nameof(ProductToReturnDto.CategoryId):
                        AddOrderByAsc(p => p.CategoryId ?? default);
                        break;

                    default:
                        goto case nameof(ProductToReturnDto.Name);
                }
            }
            else
            {
                switch (sort)
                {
                    case (nameof(ProductToReturnDto.Name)):
                        AddOrderByDesc(p => p.Name);
                        break;
                    case nameof(ProductToReturnDto.Price):
                        AddOrderByDesc(p => p.Price);
                        break;
                    case nameof(ProductToReturnDto.Id):
                        AddOrderByDesc(p => p.Id);
                        break;
                    case nameof(ProductToReturnDto.BrandId):
                        AddOrderByDesc(p => p.BrandId ?? default);
                        break;
                    case nameof(ProductToReturnDto.CategoryId):
                        AddOrderByDesc(p => p.CategoryId ?? default);
                        break;

                    default:
                        goto case nameof(ProductToReturnDto.Name);
                }
            }
        }

        private void AddBrandAndCategoryIncludes()
        {
            AddIncludes(p => p.Brand!);
            AddIncludes(p => p.Category!);
        }
        #endregion
    }
}
