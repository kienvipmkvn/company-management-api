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
        public IEnumerable<Product> Get()
        {
            _productRepository = new ProductRepository();
            return _productRepository.GetProducts();
        }

        // GET: api/Product/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Product
        public int Post(Product product)
        {
            _productRepository = new ProductRepository();
            return _productRepository.Create(product);
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
