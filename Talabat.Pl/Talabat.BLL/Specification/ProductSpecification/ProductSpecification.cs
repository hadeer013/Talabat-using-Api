using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specification.ProductSpecification
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductParams productParams)
            : base(p => (productParams.brandId == null || p.ProductBrandId == productParams.brandId) && (productParams.typeId == null || p.ProductTypeId == productParams.typeId) &&((string.IsNullOrEmpty(productParams.Search))|| p.Name.ToLower().Contains(productParams.Search)))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);
            AppyPagination((productParams.PageIndex - 1) * productParams.PageSize, productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
