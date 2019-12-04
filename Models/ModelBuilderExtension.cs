using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public static class ModelBuilderExtension
    {
        public static void seed(this ModelBuilder modelBuilder)
         {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Avyaan",
                    Department = Dept.IT,
                    Email = "Anup4ever28@gmail.com"
                },
                 new Employee
                 {
                     Id = 2,
                     Name = "Anika",
                     Department = Dept.HR,
                     Email = "Anika@gmail.com"
                 }
            );
        }
    }
}
