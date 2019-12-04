using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace HotelManagement.Models
{
    public class SqlEmployeeRepository : IEmployee
    {
        private AppDBContext _context;

        public SqlEmployeeRepository(AppDBContext context)
        {
            _context = context;

        }
        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
          Employee employee =  _context.Employees.Find(id);
            if(employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();

            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
         {
            return _context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return _context.Employees.Find(Id);
        }

        public Employee Update(Employee employeeChange)
        {
          var employee=  _context.Employees.Attach(employeeChange);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employeeChange;
        }
    }

   
}
