﻿using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Modascon.Models;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;

namespace Modascon.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(ProductRequestParameters p) 
        {
            var products = _manager.ProductService.GetAllProductsWithDetails(p);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
            };
            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            });
        }
        public IActionResult Get([FromRoute(Name ="id")]int id) 
        {
            var model = _manager.ProductService.GetOneProduct(id, false);
            return View(model);
        }
    }
}
