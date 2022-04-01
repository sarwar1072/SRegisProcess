using Autofac;
using RegistrationProcess.web.Areas.Admin.Models.Course;
using RegistrationProcess.web.Areas.Admin.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public WebModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
           
            builder.RegisterType<StudentModel>().AsSelf();
            builder.RegisterType<CourseModel>().AsSelf();
            //builder.RegisterType<StudentRegistrationModel>().AsSelf();
            base.Load(builder);
        }
    }
}
