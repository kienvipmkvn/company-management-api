using CompanyManagement.DAL.Repository;
using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompanyManagement.Controllers
{
    public class ProductController : ApiController
    {
        ProductRepository _productRepository;
        // GET: api/Product
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Product
        public void Post(Product product)
        {
            _productRepository = new ProductRepository();
            _productRepository.Create(product);
        }

        // PUT: api/Product/5
        public void Put(int id, Product product)
        {
            _productRepository = new ProductRepository();
            _productRepository.Edit(id, product);
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
            _productRepository = new ProductRepository();
            _productRepository.Delete(id);
        }
    }
}
