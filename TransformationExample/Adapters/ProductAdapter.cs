using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TransformationExample.Context;
using TransformationExample.Extensions;

namespace TransformationExample.Adapters
{
    public class ProductAdapter
    {
        private readonly IEnumerable<Product> _products;
        private readonly CompanyNamespaceManager _ns = new CompanyNamespaceManager(new NameValueCollection
        {
            {"p", "http://www.company.com/p"},
            {"a", "http://www.company.com/a"}
        });

        public ProductAdapter(IEnumerable<Product> products)
        {
            _products = products;
        }

        public XmlDocument GetXml()
        {
            var nsList = _ns.GetAll();

            var result = new XDocument(
                new XElement("Products",
                    nsList,
                    GetProducts()
                )
            );

            return result.ToXmlDocument();
        }

        private IEnumerable<XElement> GetProducts()
        {
            return _products.Select(GetProductElement);
        }

        private XElement GetProductElement(Product product, int index)
        {
            return
                new XElement(_ns.Get("p") + "Product",
                    new XElement(_ns.Get("p") + "Name", product.Name),
                    new XElement(_ns.Get("p") + "ProductNumber", product.ProductNumber ),
                    new XElement(_ns.Get("p") + "Class", product.Class),
                    new XElement(_ns.Get("p") + "Color", product.Color),
                    new XElement(_ns.Get("p") + "ListPrice", product.ListPrice),
                    new XElement(_ns.Get("p") + "ProductLine", product.ProductLine),
                    new XElement(_ns.Get("p") + "Size", product.Size),
                    new XElement(_ns.Get("p") + "Style", product.Style)
                    // GetReviews(product.ProductReview)
                );
        }

        // private IEnumerable<XElement> GetReviews(ICollection<ProductReview> review) { }
    }
}
