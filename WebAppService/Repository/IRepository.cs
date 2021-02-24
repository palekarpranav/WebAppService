using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppService.Repository
{
    public interface IRepository<T> where T : class
    {
        //Create
        //Update
        //Delete

        IEnumerable<T> GetEmployees();
        T GetEmployeeById(int id);
        void NewEmployee(T t);
        void UpdateOrDeleteEmployee(T t, bool IsDeleted);
    }
}
