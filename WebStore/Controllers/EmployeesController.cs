using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("NewRoute/[controller]/123")]
    //[Route("Staff")]
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> _Employees = TestData._Employees;

        //[Route("{id}")]
        public IActionResult EmployeeDetails(int id)
        {
            var employee = _Employees.FirstOrDefault(e => e.Id == id);
            if (employee is null) return NotFound();
            return View(employee);
        }
        //[Route("List")]
        public IActionResult Index()
        {
            return View(_Employees);
        }
    }
}
