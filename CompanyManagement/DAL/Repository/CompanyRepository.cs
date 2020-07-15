using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Configuration;
using CompanyManagement.Models;
using Dapper;

namespace CompanyManagement.DAL.Repository
{
    public class CompanyRepository
    {
        private readonly IDbConnection _connection;

        public CompanyRepository()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            _connection = factory.CreateConnection();
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            _connection.Open();
        }

        public List<object> GetAddressGroup()
        {
            return _connection.Query("spAddressGroup", commandType: CommandType.StoredProcedure).ToList();
        }

        public List<object> GetAddressGroupShow(string groupKey)
        {
            return _connection.Query("spAddressGroupShow", new { groupKey }, commandType: CommandType.StoredProcedure).ToList();
        }
        public List<Company> Get(int? pageNumber, int? numberOfRecord)
        {
            return _connection.Query<Company>("spCompanyPageList", new
            {
                pageNumber,
                numberOfRecord
            }, commandType: CommandType.StoredProcedure).ToList();
        }

        public int GetNumberOfRecord()
        {
            return int.Parse(_connection.ExecuteScalar("spCountRecords", commandType: CommandType.StoredProcedure).ToString());
        }

        public Company GetById(int id)
        {
            return _connection.Query<Company>("spSelectCompany", new { companyid = id },
                commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public int Create(Company company, string email)
        {
            var result = _connection.ExecuteScalar("spCreateCompany", new
            {
                company.Name,
                NoE = company.NumberofEmployee,
                company.Address,
                company.ImgPath,
                company.EstablishmentDay,
                createbyuser = email,
                updatebyuser = email
            }, commandType: CommandType.StoredProcedure);

            return int.Parse(result.ToString());
        }

        public void Edit(Company company, string email)
        {
            _connection.Execute("spEditCompany", new
            {
                company.CompanyID,
                company.Name,
                company.NumberofEmployee,
                company.Address,
                company.ImgPath,
                company.EstablishmentDay,
                updatebyuser = email
            },
                commandType: CommandType.StoredProcedure);
        }

        public void Delete(int comid, string email)
        {
            _connection.Execute("spDeleteCompany", new { companyid = comid, userid = email },
                commandType: CommandType.StoredProcedure);
        }
    }
}