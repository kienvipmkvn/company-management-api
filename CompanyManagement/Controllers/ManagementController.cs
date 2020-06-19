using CompanyManagement.DAL.Repository;
using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CompanyManagement.Controllers
{
    public class ManagementController : ApiController
    {
        private CompanyRepository _companyRepository;
        private ProductRepository _productRepository;

        //GET: api/Management
        public IEnumerable<Company> GetCompanies()
        {
            _companyRepository = new CompanyRepository();
            _productRepository = new ProductRepository();
            List<Company> companies = _companyRepository.Get();
            foreach (var company in companies)
            {
                List<Product> products = _productRepository.Get(company.CompanyID);
                company.ListProduct = products;
            }
            return companies;
        }

        //PUT: api/Management/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            if (id != company.CompanyID) return BadRequest();
            _companyRepository = new CompanyRepository();
            _productRepository = new ProductRepository();

            List<Product> thisProducts = _productRepository.Get(id);

            List<Product> products = company.ListProduct;

            //tao ban sao
            var thisProductsClone = new List<Product>();
            var productsClone = new List<Product>();
            foreach (var product in thisProducts)
            {
                thisProductsClone.Add(product);
            }
            foreach (var product in products)
            {
                productsClone.Add(product);
            }

            foreach (var product in productsClone)
            {
                _productRepository.Edit(product.ProductID, product);
                foreach (var thisProduct in thisProductsClone)
                {
                    //Nếu đã sửa product thì xoá khỏi mảng
                    if(thisProduct.ProductID == product.ProductID)
                    {
                        thisProducts.Remove(thisProduct);
                        products.Remove(product);
                    }
                }
            }

            if (thisProducts.Count > 0)
            {
                foreach (var product in thisProducts)
                {
                    _productRepository.DeleteFromProduct(id, product.ProductID);
                }
            }

            if(products.Count > 0)
            {
                foreach (var product in products)
                {
                    _productRepository.AddToCompany(id, product);
                }
            }

            _companyRepository.Edit(company, 1);
            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST: api/Management
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            _companyRepository = new CompanyRepository();
            _productRepository = new ProductRepository();

            int id = _companyRepository.Create(company, 1);
            foreach (var product in company.ListProduct)
            {
                _productRepository.AddToCompany(id, product);
            }

            return CreatedAtRoute("DefaultApi", new { id }, company);
        }

        //DELETE: api/Management/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            _companyRepository = new CompanyRepository();
            _companyRepository.Delete(id, 1);
            return Ok();
        }

    }
}
