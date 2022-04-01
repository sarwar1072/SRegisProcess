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

       
        internal string Delete(int Id)
        {
            var deleteRegistration = _studentRegistrationService.DeleteRegistration(Id);
            return deleteRegistration.Id.ToString();
        }


    }
}
