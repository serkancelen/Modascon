using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IOrderRepository _orderRepository;

        public RepositoryManager(IProductRepository productRepository, 
            RepositoryContext repositoryContext, 
            ICategoryRepository categoryRepository, 
            IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _context = repositoryContext;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;
        public IContactRepository Contact => _contactRepository;

        public IOrderRepository Order => _orderRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
