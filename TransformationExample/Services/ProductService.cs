using System;
using System.Collections.Generic;
using System.Linq;
using TransformationExample.Context;

namespace TransformationExample.Services
{
    public class ProductService: IProductService
    {
        private readonly AdventureWorksContext _context;

        public ProductService(AdventureWorksContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            var date = new DateTime(2013, 05, 01);
            return _context.Product.Where(product => product.SellStartDate >= date);
        }
    }
}
