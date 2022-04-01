using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.Student
{
    public class StudentModel:StudentBaseModel
    {
        public StudentModel(IStudentService studentService) : base(studentService) { }       
        public StudentModel() : base() { }

        internal object GetStudent(DataTablesAjaxRequestModel dataTables)
        {
            var data = _studentService.GetStudents(
                                  dataTables.PageIndex,
                                   dataTables.PageSize,
                                  dataTables.SearchText,
                                  dataTables.GetSortText(new string[] { "Id", "Name", "DateOfBirth" }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.students
                        select new string[]
                        {
                                record.Name,
                                record.DateOfBirth.ToString(),
                                record.Id.ToString()
                        }
                   ).ToArray()

            };
        }
        internal string Delete(int Id)
        {
            var deleteStudent = _studentService.Delete(Id);
            return deleteStudent.Name;

        }

    }
}
