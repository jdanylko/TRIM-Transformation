using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TransformationExample.Adapters;
using TransformationExample.Services;

namespace TransformationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var products = _service.GetProducts();
            var adapter = new ProductAdapter(products);
            var xml = adapter.GetXml().InnerXml;

            return Content(xml, "application/xml");
        }
    }
}