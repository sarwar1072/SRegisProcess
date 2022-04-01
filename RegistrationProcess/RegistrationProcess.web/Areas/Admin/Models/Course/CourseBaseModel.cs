using Autofac;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.Course
{
    public class CourseBaseModel:AdminBaseModel,IDisposable
    {
        protected ICourseService _courseService;
        public CourseBaseModel()
        {
            _courseService = Startup.AutofacContainer.Resolve<ICourseService>();    
        }

        public CourseBaseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public void Dispose()
        {
            _courseService.Dispose();
        }
    }
}
