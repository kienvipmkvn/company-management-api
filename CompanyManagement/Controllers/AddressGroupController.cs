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
    public class AddressGroupController : ApiController
    {
        private CompanyRepository _companyRepository;
        // GET: api/AddressGroup?params
        public List<object> Get(string groupKey)
        {
            _companyRepository = new CompanyRepository();
            if (groupKey == null || groupKey == "")
            {
                return _companyRepository.GetAddressGroup();
            }
            return _companyRepository.GetAddressGroupShow(groupKey);
        }

        // POST: api/AddressGroup
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AddressGroup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AddressGroup/5
        public void Delete(int id)
        {
        }
    }
}
