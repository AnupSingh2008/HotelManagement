using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelManagement.Models;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using HotelManagement.Security;

namespace HotelManagement.Controllers
{

    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployee _employee;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDataProtector protector;
        public HomeController(ILogger<HomeController> logger, IEmployee emp, 
            IHostingEnvironment hostingEnvironment,
                              IDataProtectionProvider dataProtectionProvider,
                              DataProtectionPurposeStrings dataProtectionPurposeStrings
)
        {
            _logger = logger;
            _employee = emp;
            _hostingEnvironment = hostingEnvironment;
            this.protector = dataProtectionProvider.CreateProtector(
    dataProtectionPurposeStrings.EmployeeIdRouteValue);

        }

        [AllowAnonymous]
        [Route("~/home")]
        [Route("~/")]
        public ViewResult Index()
        {
            return View("~/views/home/index.cshtml");
        }

        [AllowAnonymous]
        [Route("~/home/users")]
        public ViewResult Users()
        {
            var model = _employee.GetAllEmployees().Select(e =>
            {
                // Encrypt the ID value and store in EncryptedId property
                e.EncryptedId = protector.Protect(e.Id.ToString());
                return e;
            });
            return View(model);
        }
        [Route("{id}")]
        public ViewResult Details(string id)
        {
            //var model = _employee.GetEmployee(1);
            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details";
            //return View(model);

            string decryptedId = protector.Unprotect(id);
            int decryptedIntId = Convert.ToInt32(decryptedId);

            Employee employee = _employee.GetEmployee(decryptedIntId);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                employee = employee,
                pageTitle = "Employee Details"

            };
            return View(homeDetailsViewModel);

        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Create(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Employee newEmployee = _employee.Add(employee);
        //       // return RedirectToAction("Details", new { id = employee.Id });
        //    }
        //    return View();
        //}

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = string.Empty;

                //if (model.Photo != null && model.Photo.Count >=1)
                //{
                //    foreach (IFormFile photo in model.Photo)
                //    {
                //        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Name + ".jpg";
                //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //    }


                //}
                if (model.Photo != null)
                {

                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Name + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employee.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });

            }
            return View();
        }

        [HttpGet]

        public ViewResult Edit(int id)
        {
            Employee employee = _employee.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(employeeEditViewModel);

        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employee.GetEmployee(model.Id);
                employee.Id = model.Id;
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadEmployeePhoto(model);
                }

                Employee updateEmployee = _employee.Update(employee);
                return RedirectToAction("index");

            }
            return View();
        }

        private string ProcessUploadEmployeePhoto(EmployeeEditViewModel model)
        {
            string uniqueFileName = string.Empty;

            if (model.Photo != null)
            {

                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Name + ".jpg";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


