using Autofac;
using Framework;
using Microsoft.AspNetCore.Authorization;
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
  //  [Authorize(Roles = "SuperAdmin,Administrator")]
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
        public IActionResult Edit(int id)
        {
            var model = new EditRegistration();
            model.Load(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditRegistration model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Edit();
                    model.Response = new ResponseModel("edited successfully", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRegistration(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new StudentRegistrationModel();
                try
                {
                    var dataprovider = model.Delete(id);
                    model.Response = new ResponseModel($"delete{dataprovider} successfully", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
            }
            return RedirectToAction("Index");
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
