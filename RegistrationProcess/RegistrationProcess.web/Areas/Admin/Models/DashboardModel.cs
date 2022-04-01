using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models
{
    public class DashboardModel:AdminBaseModel
    {
        private IStudentRegistrationService  _studentRegistrationService;

        public DashboardModel(IStudentRegistrationService studentRegistrationService)
        {
            _studentRegistrationService = studentRegistrationService;
        }

        public DashboardModel()
        {
        }
        public int count()
        {
            var count = _studentRegistrationService.Count();
            return count;
        }
    }
}
