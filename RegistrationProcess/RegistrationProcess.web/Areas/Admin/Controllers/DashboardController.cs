using Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= "SuperAdmin,Administrator")]
    public class DashboardController : Controller
    {
        private SMDbContext _sMDbContext;
        public DashboardController(SMDbContext sMDbContext)
        {
            _sMDbContext = sMDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
