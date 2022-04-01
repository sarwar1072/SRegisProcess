using Autofac;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.StudentRegistration
{
    public class StudentRegitrationBaseModel:AdminBaseModel,IDisposable
    {
        protected IStudentRegistrationService _studentRegistrationService;
        public StudentRegitrationBaseModel(IStudentRegistrationService studentRegistrationService)
        {
            _studentRegistrationService = studentRegistrationService;
        }
        public StudentRegitrationBaseModel()
        {
            _studentRegistrationService = Startup.AutofacContainer.Resolve<IStudentRegistrationService>();
        }
        public void Dispose()
        {
            _studentRegistrationService.Dispose();
        }
    }
}
