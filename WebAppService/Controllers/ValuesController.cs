using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppService.Models;
using WebAppService.Repository;

namespace WebAppService.Controllers
{
    [Authorize(Roles = "Employee, Admin")]// we can update roles from DB and add up here 
    public class EmployeeInfoController : ApiController
    {
        private readonly IRepository<EmployeeInfo> repository;
        /// <summary>
        /// Constructor injection used Ninject container to resolve dependancy 
        /// </summary>
        /// <param name="repository"></param>
        public EmployeeInfoController(IRepository<EmployeeInfo> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        ///   GET all employee list
        /// </summary>
        /// <returns>List of EmployeeInfo</returns>
        public IEnumerable<EmployeeInfo> Get()
        {
            return repository.GetEmployees().ToList();
        }
        /// <summary>
        ///  GET individual list of employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns> EmployeeInfo object</returns>
        public EmployeeInfo Get(int id)
        {
            return repository.GetEmployeeById(id);
        }

        /// <summary>
        /// Save Data in DB
        /// </summary>
        /// <param name="jsonResult"></param>
        [HttpPost]
        public void Post([FromBody] JObject value)
        {
            var item = JsonConvert.DeserializeObject<EmployeeInfo>(value.ToString());

            EmployeeInfo employeeInfo = new EmployeeInfo();

            employeeInfo.Address = item.Address;
            employeeInfo.Phone = item.Phone;
            employeeInfo.FirstName = item.FirstName;
            employeeInfo.LastName = item.LastName;
            employeeInfo.IsActive = true;
            employeeInfo.createdDate = DateTime.Now;
            repository.NewEmployee(employeeInfo);
        }

        /// <summary>
        /// PUT request
        /// </summary>
        /// <param name="jsonResult"></param>
        [HttpPut]
        public void Put([FromBody] JObject jsonResult)
        {
            var item = JsonConvert.DeserializeObject<EmployeeInfo>(jsonResult.ToString());
            EmployeeInfo employeeInfo = new EmployeeInfo();

            if (item.id == null || item.id == 0)
                throw new Exception("cannot go ahead without Id=>" + item.id);
            else
                employeeInfo.id = item.id;

            if (!string.IsNullOrEmpty(item.Address))
                employeeInfo.Address = item.Address;
            if (!string.IsNullOrEmpty(item.Phone))
                employeeInfo.Phone = item.Phone;
            if (!string.IsNullOrEmpty(item.FirstName))
                employeeInfo.FirstName = item.FirstName;
            if (!string.IsNullOrEmpty(item.LastName))
                employeeInfo.LastName = item.LastName;

            employeeInfo.IsActive = item.IsActive;
            repository.UpdateOrDeleteEmployee(employeeInfo, false);

        }

        /// <summary>
        /// DELETE  requst
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(int id)
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();
            if (id == null || id == 0)
                throw new Exception("cannot go ahead without Id =>" + id);
            else
                employeeInfo.id = id;

            repository.UpdateOrDeleteEmployee(employeeInfo, true);

        }
    }
}
