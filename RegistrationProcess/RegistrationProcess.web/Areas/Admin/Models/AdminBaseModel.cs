using Autofac;
using Framework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models
{
    public abstract class AdminBaseModel
    {
        public MenuModel MenuModel { get; set; }
        public ResponseModel Response
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.IsAvailable
                    && _httpContextAccessor.HttpContext.Session.Keys.Contains(nameof(Response)))
                {
                    var response = _httpContextAccessor.HttpContext.Session.Get<ResponseModel>(nameof(Response));
                    _httpContextAccessor.HttpContext.Session.Remove(nameof(Response));
                    return response;
                }
                else
                    return null;
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.Set(nameof(Response),
                    value);
            }
        }

        protected IHttpContextAccessor _httpContextAccessor;
        public AdminBaseModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            SetupMenu();
        }
        public AdminBaseModel()
        {
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            SetupMenu();
        }

        private void SetupMenu()
        {
            MenuModel = new MenuModel
            {
                MenuItems = new List<MenuItem> 
                {
                   new MenuItem
                   {
                       Title="Course",
                       Childs=new List<MenuChildItem>
                       {
                           new MenuChildItem{Title="View Course",Url="/Admin/Course/"},
                           new MenuChildItem{Title="Add Course",Url="/Admin/Course/CreateCourse"}
                       }

                   },

                   new MenuItem
                   {
                       Title="Student",
                       Childs=new List<MenuChildItem>
                       {
                           new MenuChildItem{Title="View Student",Url="/Admin/Student/"},
                           new MenuChildItem{Title="Add Student",Url="/Admin/Student/CreateStudent/"}
                       }
                   },

                   new MenuItem
                   {
                       Title="StudentRegistration",
                       Childs=new List<MenuChildItem>
                       {
                           new MenuChildItem{Title="View Register Student",Url="/Admin/StudentRegistration/"},
                           new MenuChildItem{Title="Add Student",Url="/Admin/StudentRegistration/CreateRegistration/"}
                       }
                   },

                }           
            };
        }

    }
}
