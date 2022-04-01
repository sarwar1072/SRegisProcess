using Framework.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.StudentRegistration
{
    public class CreatStRegistration:StudentRegitrationBaseModel
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public bool IspaymentComplete { get; set; }
        public DateTime EnrollDate { get; set; }
        public CreatStRegistration() : base() { }
        public CreatStRegistration(IStudentRegistrationService studentRegistrationService)
            : base(studentRegistrationService) { }
        
        public void AddRegistration()
        {
            var add = new Framework.Entities.StudentRegistration() 
            {
                CourseId=CourseId,
                StudentId=StudentId,
                EnrollDate=EnrollDate,
                IspaymentComplete= IspaymentComplete,
            
            };
            _studentRegistrationService.AddRegistration(add);
        }
        public IList<SelectListItem> GetStudentList()
        {
            IList<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in _studentRegistrationService.GetStudents())
            {
                var ctg = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                };
                listItems.Add(ctg);
            }
            return listItems;
        }

        public IList<SelectListItem> GetCourseList()
        {
            IList<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in _studentRegistrationService.GetCourses())
            {
                var ctg = new SelectListItem
                {
                    Text = item.Title,
                    Value = item.Id.ToString()
                };
                listItems.Add(ctg);
            }
            return listItems;
        }

    }
}
