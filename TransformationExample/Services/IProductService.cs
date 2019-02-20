using System.Collections.Generic;
using TransformationExample.Context;

namespace TransformationExample.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
    }
}