using Autofac;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.Student
{
    public class StudentBaseModel:AdminBaseModel,IDisposable
    {
        protected IStudentService _studentService;
        public StudentBaseModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public StudentBaseModel()
        {
            _studentService= Startup.AutofacContainer.Resolve<IStudentService>();
        }
        public void Dispose()
        {
            _studentService.Dispose();
        }
    }
}
