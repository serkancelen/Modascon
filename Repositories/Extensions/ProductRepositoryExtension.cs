using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products, int? categoryId)
        {
            if (categoryId == 0)
                return products;
            else
                return products.Where(prd => prd.CategoryId.Equals(categoryId));
        }
        public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return products;
            else
                return products.Where(prd => prd.ProductName.ToLower().Contains(searchTerm.ToLower()) || prd.Summary.ToLower().Contains(searchTerm.ToLower()));

        }
        public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, decimal minPrice, decimal maxPrice, bool validPrice)
        {
            if (validPrice)

                return products.Where(prd => prd.Price >= minPrice && prd.Price <= maxPrice);

            else
                return products;
        }
        public static IQueryable<Product> ToPaginate(this IQueryable<Product> products, int pageNumber, int pageSize)
        {
            return products.Skip(((pageNumber -1)*pageSize)).Take(pageSize);
        }
    }
}
