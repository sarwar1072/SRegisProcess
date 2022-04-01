using Framework.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.StudentRegistration
{
    public class StudentRegistrationModel:StudentRegitrationBaseModel
    {
        public StudentRegistrationModel() : base() { }
        public StudentRegistrationModel(IStudentRegistrationService studentRegistrationService)
                                                           : base(studentRegistrationService) { }

        internal object GetStudentRegistration(DataTablesAjaxRequestModel dataTables)
        {
            var data = _studentRegistrationService.GetStudentRegistrations(
                                  dataTables.PageIndex,
                                   dataTables.PageSize,
                                  dataTables.SearchText,
                                  dataTables.GetSortText(new string[] {  "Course", "EnrollDate", "PaymentStutas", "Id" }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.studentRegistrations
                        select new string[]
                        {
                                 record.StudentId.ToString(),
                                record.CourseId.ToString(),
                                record.Course.Title,
                                record.EnrollDate.ToString(),
                                record.IspaymentComplete.ToString(),
                                record.Id.ToString()
                        }
                   ).ToArray()
            };
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

        internal string Delete(int Id)
        {
            var deleteRegistration = _studentRegistrationService.DeleteRegistration(Id);
            return deleteRegistration.Id.ToString();
        }


    }
}
