using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistrationProcess.web.Areas.Admin.Models.Course;
using RegistrationProcess.web.Areas.Admin.Models;
using Framework;
using Microsoft.AspNetCore.Authorization;

namespace RegistrationProcess.web.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Administrator")]
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly IConfiguration _configuration;
        public CourseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<CourseModel>();
            return View(model);
        }
        public IActionResult CreateCourse()
        {
            var model = new CreateCourse();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCourse(CreateCourse createCourse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    createCourse.Create();
                    createCourse.Response = new ResponseModel("course created", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    createCourse.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch(Exception ex)
                {
                    createCourse.Response = new ResponseModel("Course add failure", ResponseType.Failure);
                }           
            }
            return View(createCourse);
        }
    
        public IActionResult EditCourse(int id)
        {
            var model = new EditCourse();
            model.Load(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse(EditCourse model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Edit();
                    model.Response = new ResponseModel("Course Edited Successfully", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch(DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel("edit failure", ResponseType.Failure);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCourse(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new CourseModel();
                try
                {
                    var provider = model.Delete(id);
                    model.Response = new ResponseModel($"Course {provider} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Course Delete failed.", ResponseType.Failure);
                   
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult GetCourse()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<CourseModel>();
            var data = model.GetCourse(tableModel);
            return Json(data);
        }
    }
}
