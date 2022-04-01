using Autofac;
using Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistrationProcess.web.Areas.Admin.Models;
using RegistrationProcess.web.Areas.Admin.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<StudentModel>();

            return View(model);
        }
        public IActionResult CreateStudent()
        {
            var model = new AddStudent();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStudent(AddStudent student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    student.CreateStudent();
                    student.Response = new ResponseModel("Student added", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch(DuplicationException ex)
                {
                    student.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch(Exception ex)
                {
                    student.Response = new ResponseModel("Failure", ResponseType.Failure);
                }
            }
            return View(student);
        }
        public IActionResult EditStudent(int id)
        {
            var model = new EditStudent();
            model.Load(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent(EditStudent model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Edit();
                    model.Response = new ResponseModel("SuccessFully Edited", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch(DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel("Failed!try again", ResponseType.Failure);
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStudent(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new StudentModel();
                try
                {
                    var provider = model.Delete(id);
                    model.Response = new ResponseModel($"student {provider} deleted successfully", ResponseType.Success);

                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel("student deletion fail", ResponseType.Failure);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult GetStudent()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<StudentModel>();
            var data = model.GetStudent(tableModel);
            return Json(data);
        }
    }
}
