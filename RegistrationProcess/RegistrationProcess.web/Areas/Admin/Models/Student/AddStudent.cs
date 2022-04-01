using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Admin.Models.Student
{
    public class AddStudent:StudentBaseModel
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddStudent() : base() { }
        public AddStudent(IStudentService studentService) : base(studentService) { }
        
        public void CreateStudent()
        {
            var student = new Framework.Entities.Student()
            {
                Name=Name,
                DateOfBirth= DateOfBirth
            };
            _studentService.AddStudent(student);
        }
      
    }
}
