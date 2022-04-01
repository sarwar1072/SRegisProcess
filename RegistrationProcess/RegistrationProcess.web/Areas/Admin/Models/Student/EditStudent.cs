using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.Student
{
    public class EditStudent:StudentBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public EditStudent() : base() { }
        public EditStudent(IStudentService studentService) : base(studentService) { }
        
        public void Edit()
        {
            var student = new Framework.Entities.Student()
            {
                Id=Id,
                Name=Name,
                DateOfBirth=DateOfBirth
            };
            _studentService.EditStudent(student);
        }
        public void Load(int id)
        {
            var student = _studentService.GetStudent(id);
            if(student != null)
            {
                Id = student.Id;
                Name = student.Name;
                DateOfBirth = student.DateOfBirth;
            }
        }               
    }
}
