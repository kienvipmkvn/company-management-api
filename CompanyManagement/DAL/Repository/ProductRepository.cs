using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using Dapper;

namespace CompanyManagement.DAL.Repository
{
    public class ProductRepository
    {
        private readonly IDbConnection _connection;

        public ProductRepository()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            _connection = factory.CreateConnection();
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            _connection.Open();
        }

        public List<Product> Get(int id)
        {
            return _connection.Query<Product>("spProductList", new { companyid = id },
                commandType: CommandType.StoredProcedure).ToList();
        }

        public void Edit(int id, Product product)
        {
            _connection.Execute("spEditProduct", new
            {
                productid = id,
                name = product.Name,
                color = product.Color,
                price = product.Price,
                imgPath = product.ImgPath
            },
                commandType: CommandType.StoredProcedure);
        }

        public int Create(Product product)
        {
            var result = _connection.ExecuteScalar("spCreateProduct", new
            {
                product.Name,
                product.Color,
                product.Price,
                product.ImgPath
            }, commandType: CommandType.StoredProcedure);

            return int.Parse(result.ToString());
        }

        public void AddToCompany(int companyid, Product product)
        {
            int productid = Create(product);

            _connection.Execute("spAddProductToCompany", new { companyid, productid}, commandType: CommandType.StoredProcedure);
        }

        public void DeleteFromProduct(int companyid, int productid)
        {
            _connection.Execute("spDeleteProduct", new { companyid, productid }, commandType: CommandType.StoredProcedure);
        }

        public void Delete(int productid)
        {
            _connection.Execute("spDeleteProduct4ever", new { productid }, commandType: CommandType.StoredProcedure);
        }
    }
}