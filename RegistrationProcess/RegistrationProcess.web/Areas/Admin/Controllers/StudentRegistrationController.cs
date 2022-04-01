using Autofac;
using Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistrationProcess.web.Areas.Admin.Models;
using RegistrationProcess.web.Areas.Admin.Models.StudentRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        public StudentRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<StudentRegistrationModel>();
            return View(model);
        }

        public IActionResult CreateRegistration()
        {
            var model = new CreatStRegistration();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRegistration(CreatStRegistration model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.AddRegistration();
                    model.Response = new ResponseModel("Registration Successfull", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch(DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel("Fail.try again", ResponseType.Failure);
                }
            }
            return View(model);
        }

        public IActionResult GetRegistration()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<StudentRegistrationModel>();
            var data = model.GetStudentRegistration(tableModel);
            return Json(data);
        }
    }
}
