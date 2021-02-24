using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppService;
using WebAppService.Controllers;
using WebAppService.Models;
using WebAppService.Repository;

namespace UnitTestWebAPI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetReturnsAllEmployee()
        {
            var testProducts = GetTestProducts();
            var controller = new EmployeeInfoController(new EmployeeInfoOperations());

            var result = controller.Get() as List<EmployeeInfo>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        private List<EmployeeInfo> GetTestProducts()
        {
            var testEmployee = new List<EmployeeInfo>();
            testEmployee.Add(new EmployeeInfo { id = 1, FirstName = "Demo1", LastName = "Demo1", Address = "Pune", Phone = "12345" });
            testEmployee.Add(new EmployeeInfo { id = 2, FirstName = "Demo2", LastName = "Demo2", Address = "Pune", Phone = "56789" });
            return testEmployee;
        }
    }
}
