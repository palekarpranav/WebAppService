using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppService.Repository;

namespace WebAppService.Models
{
    public class EmployeeInfoOperations : IRepository<EmployeeInfo>
    {
        /// <summary>
        /// Get Single record of Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeInfo GetEmployeeById(int id)
        {
            EmployeeEntities employeeEntities = new EmployeeEntities();
            return employeeEntities.EmployeeInfoes.Where(x => x.id == id && x.IsActive == true).FirstOrDefault();
        }
        /// <summary>
        /// Get all Employee list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeInfo> GetEmployees()
        {
            EmployeeEntities employeeEntities = new EmployeeEntities();
            return employeeEntities.EmployeeInfoes;
        }

        /// <summary>
        /// Add new Employee record
        /// </summary>
        /// <param name="t"></param>
        public void NewEmployee(EmployeeInfo t)
        {
            try
            {
                EmployeeEntities employeeEntities = new EmployeeEntities();
                employeeEntities.EmployeeInfoes.Add(t);
                employeeEntities.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Update or delete employee record
        /// </summary>
        /// <param name="t"></param>
        /// <param name="IsDeleted"></param>
        public void UpdateOrDeleteEmployee(EmployeeInfo t, bool IsDeleted)
        {
            EmployeeEntities employeeEntities = new EmployeeEntities();
            var record = employeeEntities.EmployeeInfoes.Where(x => x.id == t.id).FirstOrDefault();
            if (record != null)
            {
                if (IsDeleted)
                {
                    record.IsActive = false;
                }
                else
                {
                    record.FirstName = t.FirstName;
                    record.LastName = t.LastName;
                    record.Address = t.Address;
                    record.Phone = t.Phone;

                }
                employeeEntities.SaveChanges();
            }
            else
            {
                throw new Exception("Record not found ");
            }
        }
    }
}