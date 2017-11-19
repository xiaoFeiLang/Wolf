using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TestMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int Age { get; set; }
        public class EmployeeDBContext : DbContext
        {
            public DbSet<Employee> Employees { get; set; }
        }
    }
}